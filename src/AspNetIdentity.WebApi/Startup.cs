using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using AspNetIdentity.WebApi;
using AspNetIdentity.WebApi.Data;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Newtonsoft.Json.Serialization;
using Owin;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security;

[assembly: OwinStartup(typeof(Startup))]

namespace AspNetIdentity.WebApi {

    public class Startup {

        public void Configuration(IAppBuilder app) {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            var httpConfig = new HttpConfiguration();
            ConfigureOAuthTokenGeneration(app);
            ConfigureWebApi(httpConfig);

            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions {
                TicketDataFormat = new TicketDataFormat(new Beginor.Owin.Secrity.Aes.AesDataProtector("MySecurityAesKey"))
            });

            PropertiesDataFormat f = new PropertiesDataFormat(

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(httpConfig);
        }

        private void ConfigureOAuthTokenGeneration(IAppBuilder app) {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Plugin the OAuth bearer JSON Web Token tokens generation and Consumption will be here
        }

        private void ConfigureWebApi(HttpConfiguration config) {
            config.MapHttpAttributeRoutes();
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

    }

}
