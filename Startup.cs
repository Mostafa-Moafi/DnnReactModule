using DnnReactDemo.Data;
using DnnReactDemo.Data.IRepositories;
using DnnReactDemo.Data.Repositories;
using DnnReactDemo.Services;
using DotNetNuke.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Web.Http;

namespace DnnReactDemo
{
    /// <summary>
    /// Class implementing IDnnStartup interface for configuring services in a DNN module startup.
    /// </summary>
    public class Startup : IDnnStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            #region Config DbContext
            var config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            services.AddScoped<ModuleDbContext, ModuleDbContext>();
            #endregion

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IServiceManager, ServiceManager>();

        }
    }
}
