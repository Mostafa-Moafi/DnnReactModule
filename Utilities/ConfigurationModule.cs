// MIT License

using DotNetNuke.Entities.Modules;
using DotNetNuke.Instrumentation;
using System;
using System.Xml;

namespace DnnReactModule.Utilities
{
    /// <summary>
    /// Represents a configuration module that implements the IUpgradeable interface for upgrading modules.
    /// </summary>
    public class ConfigurationModule : IUpgradeable
    {
        private static readonly ILog Logger = LoggerSource.Instance.GetLogger(typeof(ConfigurationModule));

        /// <summary>
        /// Upgrades the module by adding WebDAV module if it does not exist.
        /// </summary>
        /// <param name="Version">The version of the module to upgrade.</param>
        /// <returns>
        /// Returns "Success" if the upgrade is successful, otherwise returns "Failed".
        /// </returns>
        public string UpgradeModule(string Version)
        {
            try
            {
                AddWebDAVModuleRemoveIfNotExists();
                return "Success";
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                return "Failed";
            }
        }

        /// <summary>
        /// Adds a <remove name="WebDAVModule" /> element to the web.config file if it does not already exist in the system.webServer/modules section.
        /// </summary>
        public void AddWebDAVModuleRemoveIfNotExists()
        {
            // Load the web.config file
            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            // Check if the <remove name="WebDAVModule" /> element exists
            bool elementExists = false;
            var modulesElement = doc.SelectNodes("/configuration/system.webServer/modules");
            if (modulesElement.Count > 0)
                foreach (XmlNode node in modulesElement[0].ChildNodes)
                    if (node.Name == "remove" && node.OuterXml.Contains("WebDAVModule"))
                    {
                        elementExists = true;
                        break;
                    }

            // If the element does not exist, add it
            if (!elementExists)
            {
                XmlNode httpModulesNode = doc.SelectSingleNode("/configuration/system.webServer/modules");

                if (httpModulesNode != null)
                {
                    XmlElement removeElement = doc.CreateElement("remove");
                    removeElement.SetAttribute("name", "WebDAVModule");

                    httpModulesNode.AppendChild(removeElement);

                    // Save the modified web.config file
                    doc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                }
            }
        }
    }
}
