using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using BuildHelpers;
using Nuke.Common;
using HtmlAgilityPack;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using System.IO.Compression;
using Nuke.Common.Execution;
using Nuke.Common.Tools.Npm;
using Nuke.Common.Tools.NSwag;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.MSBuild;
using Nuke.Common.Tools.GitVersion;
using System.Security.Cryptography;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.CompressionTasks;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.Npm.NpmTasks;
using static Nuke.Common.IO.TextTasks;


[UnsetVisualStudioEnvironmentVariables]
class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Package);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    private string ModuleVersion = "";
    [Solution] readonly Solution? Solution;
    AbsolutePath DocsDirectory => RootDirectory / "docs";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
    AbsolutePath InstallDirectory => RootDirectory.Parent.Parent / "Install" / "Module";
    AbsolutePath WebProjectDirectory => RootDirectory / "module.web";
    AbsolutePath WebPublishedProjectDirectory => RootDirectory / "resources";

    Target Clean => _ => _
        .Before(Restore)
        .Before(Package)
        .Executes(() =>
        {
            EnsureCleanDirectory(ArtifactsDirectory);

        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s.SetProjectFile(Solution.GetProject("Module")));
        });

    Target Compile => _ => _
        .DependsOn(Clean)
        .DependsOn(Restore)
        .DependsOn(SetManifestVersions)
        .Executes(() =>
        {
            var moduleAssemblyName = Solution.Name;
            var assemblyVersion = ModuleVersion;
            var fileVersion = ModuleVersion;

            MSBuildTasks.MSBuild(s => s
                .SetProjectFile(Solution.GetProject("Module"))
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(assemblyVersion)
                .SetFileVersion(fileVersion));
        });

    Target InstallNpmPackages => _ => _
        .Executes(() =>
        {
            NpmLogger = (type, output) =>
            {
                if (type == OutputType.Std)
                {
                    Serilog.Log.Information(output);
                }
                if (type == OutputType.Err)
                {
                    if (output.StartsWith("npm WARN", StringComparison.OrdinalIgnoreCase))
                    {
                        Serilog.Log.Warning(output);
                    }
                    else
                    {
                        Serilog.Log.Error(output);
                    }
                }
            };
            NpmInstall(s =>
                s.SetProcessWorkingDirectory(WebProjectDirectory));
        });

    Target BuildFrontEnd => _ => _
        .DependsOn(InstallNpmPackages)
        .Executes(() =>
        {
            NpmRun(s => s
                .SetProcessWorkingDirectory(WebProjectDirectory)
                .AddArguments("build")
            );
        });


    Target DeployFrontEnd => _ => _
        .DependsOn(BuildFrontEnd)
        .Executes(() =>
           {
               var assetsDestination = RootDirectory / "resources";
               CopyDirectoryRecursively(RootDirectory / "module.web" / "public", assetsDestination, DirectoryExistsPolicy.Merge, FileExistsPolicy.Overwrite);

               //var pages = GlobFiles(RootDirectory / "resources/", "*.html");
               //foreach (var page in pages)
               //{
               //    var htmlPage = new StringBuilder();
               //    var content = ReadAllText(page);
               //    var htmlDoc = new HtmlDocument();
               //    htmlDoc.LoadHtml(content);
               //    htmlPage.AppendLine(content);

               //    var styles = htmlDoc.DocumentNode.SelectNodes("//link[@rel='stylesheet']");
               //    if (styles != null)
               //    {
               //        var cssFiles = styles.ToList();
               //        foreach (var css in cssFiles)
               //            htmlPage.AppendLine("[CSS:{ path: '~" + WebPublishedProjectDirectory + css.GetAttributes("href").First().Value + "'}]");
               //    }
               //    var scripts = htmlDoc.DocumentNode.SelectNodes("//script[@src]");
               //    if (scripts != null)
               //    {
               //        var jsFiles = htmlDoc.DocumentNode.SelectNodes("//script[@src]").ToList();
               //        foreach (var js in jsFiles)
               //            htmlPage.AppendLine("[JavaScript:{ path: '~" + WebPublishedProjectDirectory + js.GetAttributes("src").First().Value + "', provider:'DnnFormBottomProvider'}]");
               //    }
               //    WriteAllText(page, htmlPage.ToString(), System.Text.Encoding.UTF8);
               //}
           });


    Target SetManifestVersions => _ => _
        .Executes(() =>
        {
            var manifest = GlobFiles(RootDirectory, "manifest.dnn").FirstOrDefault();
            if (manifest != null)
            {
                var doc = new XmlDocument();
                doc.Load(manifest);
                var packages = doc.SelectNodes("dotnetnuke/packages/package");
                if (packages != null)
                    ModuleVersion = packages[0].Attributes["version"]?.Value;
            }
        });

    /// <summary>
    /// Package the module
    /// </summary>
    Target Package => _ => _
        .DependsOn(Clean)
        .DependsOn(SetManifestVersions)
        .DependsOn(Compile)
        .DependsOn(DeployFrontEnd)
        .DependsOn(Swagger)
        .Executes(() =>
        {
            var stagingDirectory = ArtifactsDirectory / "staging";
            EnsureCleanDirectory(stagingDirectory);

            // Resources
            Compress(RootDirectory / "resources", stagingDirectory / "resources.zip", f => (f.Name != "resources.zip.manifest"));

            // Install files
            var installFiles = GlobFiles(RootDirectory, "LICENSE", "manifest.dnn", "ReleaseNotes.html");
            installFiles.ForEach(i => CopyFileToDirectory(i, stagingDirectory));

            // SQL DataProvider
            var sqlDataProviders = GlobFiles(RootDirectory / "SqlDataProviders", "*.SqlDataProvider");
            sqlDataProviders.ForEach(i => CopyFileToDirectory(i, stagingDirectory));


            // Swagger File 
            if (File.Exists(DocsDirectory / $"DnnReactDemo__{ModuleVersion}.json"))
            {
                var swagger = GlobFiles(DocsDirectory, $"DnnReactDemo__{ModuleVersion}.json");
                swagger.ForEach(i => CopyFileToDirectory(i, stagingDirectory));
            }


            // Libraries
            var manifest = GlobFiles(RootDirectory, "*.dnn").FirstOrDefault();
            var assemblies = GlobFiles(RootDirectory / "bin" / Configuration, "*.dll");
            var manifestAssemblies = Helpers.GetAssembliesFromManifest(manifest);
            assemblies.ForEach(assembly =>
            {
                var assemblyFile = new FileInfo(assembly);
                var assemblyIncludedInManifest = manifestAssemblies.Any(a => a == assemblyFile.Name);

                if (assemblyIncludedInManifest)
                {
                    CopyFileToDirectory(assembly, stagingDirectory / "bin", FileExistsPolicy.Overwrite);
                }
            });

            // Install package
            string fileName = new DirectoryInfo(RootDirectory).Name + "_";
            fileName += ModuleVersion;
            fileName += "_install.zip";
            ZipFile.CreateFromDirectory(stagingDirectory, ArtifactsDirectory / fileName);
            DeleteDirectory(stagingDirectory);

            var artifact = ArtifactsDirectory / fileName;
            string hash;
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(artifact))
                {
                    var hashBytes = md5.ComputeHash(stream);
                    hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                }
            }

            var hashMd = new StringBuilder();
            hashMd.AppendLine($"## MD5 Checksums");
            hashMd.AppendLine($"| File       | Checksum |");
            hashMd.AppendLine($"|------------|----------|");
            hashMd.AppendLine($"| {fileName} | {hash}   |");
            hashMd.AppendLine();
            File.WriteAllText(ArtifactsDirectory / "checksums.md", hashMd.ToString());

            // Open folder
            //if (IsWin)
            //{
            //    CopyFileToDirectory(ArtifactsDirectory / fileName, InstallDirectory, FileExistsPolicy.Overwrite);
            //
            //    // Uncomment next line if you would like a package task to auto-open the package in explorer.
            //    // Process.Start("explorer.exe", ArtifactsDirectory);
            //}


            Serilog.Log.Information("Packaging succeeded!");
        });

    Target Swagger => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            EnsureCleanDirectory(DocsDirectory);

            var swaggerFile = DocsDirectory / $"DnnReactDemo__{ModuleVersion}.json";

            NSwagTasks.NSwagWebApiToOpenApi(c => c
                .AddAssembly(RootDirectory / "bin" / Configuration / "DnnReactDemo.dll")
                .SetInfoTitle("DnnReactDemo")
                .SetInfoVersion(ModuleVersion)
                .SetProcessArgumentConfigurator(a => a.Add("/DefaultUrlTemplate:{{controller}}/{{action}}"))
                .SetOutput(swaggerFile));
        });


    /// <summary>
    /// Lauch in deploy mode, updates the module on the current local site.
    /// </summary>
    Target Deploy => _ => _
        .DependsOn(DeployBinaries)
        .DependsOn(DeployFrontEnd)
        .Executes(() =>
        {

        });

    Target DeployBinaries => _ => _
    .OnlyWhenDynamic(() => RootDirectory.Parent.ToString().EndsWith("DesktopModules", StringComparison.OrdinalIgnoreCase))
    .DependsOn(Compile)
    .Executes(() =>
    {
        var manifest = GlobFiles(RootDirectory, "*.dnn").FirstOrDefault();
        var assemblyFiles = Helpers.GetAssembliesFromManifest(manifest);
        var files = GlobFiles(RootDirectory, "bin/Debug/*.dll", "bin/Debug/*.pdb", "bin/Debug/*.xml");
        foreach (var file in files)
        {
            var fileInfo = new FileInfo(file);
            if (assemblyFiles.Contains(fileInfo.Name))
            {
                Helpers.CopyFileToDirectoryIfChanged(file, RootDirectory.Parent.Parent / "bin");
            }
        }
    });

}
