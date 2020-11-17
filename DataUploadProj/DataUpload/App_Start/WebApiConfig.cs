using DataUpload.Application.ServiceDependency;
using System.Web.Http;
using Unity;
using Unity.AspNet.WebApi;

namespace DataUpload
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors();
            IUnityContainer container = new UnityContainer();
            container = ServiceDependencyRegister.RegisterTypes(container);
            config.DependencyResolver = new UnityDependencyResolver(container);
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
