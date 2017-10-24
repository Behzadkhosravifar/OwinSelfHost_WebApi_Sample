using System.Web.Http;
using Microsoft.Owin;
using NLog.Owin.Logging;
using Owin;
using Sample;

[assembly: OwinStartup(typeof(Startup))]
namespace Sample
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder app)
        {
            #region Config NLog middleware

            app.UseNLog();
            
            #endregion

            #region Configure Web API for self-host

            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(config);

            #endregion
        }
    }
}