using System;
using System.Web.Http;
using CourseProject.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;

[assembly: OwinStartup(typeof(CourseProject.Startup))]
namespace CourseProject
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
            var config = new HttpConfiguration();

            ConfigureOAuth(app);

            WebApiConfig.Register(config);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
          
            app.UseNinjectMiddleware(() => NinjectConfig.CreateKernel.Value);

            app.UseNinjectWebApi(config);

        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,

                TokenEndpointPath = new PathString("/token"),

                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),

                Provider = new SimpleAuthorizationServerProvider()
               
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}