// MIT License

using DnnReactDemo.Utilities;
using DotNetNuke.Web.Api;

namespace DnnReactDemo.Controllers
{
    /// <summary>
    /// Implements the Dnn IServiceRouteMapper to register this module routes.
    /// </summary>
    public class ServiceRouteMapper : IServiceRouteMapper
    {
        /// <inheritdoc/>
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager?.MapHttpRoute(GlobalParameter.ModuleName, "default", "{controller}/{action}", new[] { typeof(ServiceRouteMapper).Namespace });
        }
    }
}
