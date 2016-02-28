using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using IdentityAdmin.Configuration;
using IdentityManager.Configuration;
using IdentityManager.Extensions;
using IdentityServer.Web.Extensions;
using IdentityServer.Web.IdentityStores;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.WsFederation;
using Owin;

namespace IdentityServer.Web
{
    public class Startup
    {
        public const string IDENTITY_PATH = "identity";

        public void Configuration(IAppBuilder app)
        {
#if DEBUG
            log4net.Config.XmlConfigurator.Configure();
#endif
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies",
                CookieName = "idsrv",

            });


            app.Map(@"/" + IDENTITY_PATH, id =>
            {
                
                var factory = new IdentityServerServiceFactory().Configure("identityDbConnection");
                factory.ViewService  = new IdentityServer3.Core.Configuration.Registration<IViewService, CustomViewService>();
                var options = new IdentityServerOptions
                {
                    Factory = factory,
                    SiteName = "Abhyankars Security Token Service",
                    IssuerUri = "http://abhyankars.net/",
                    PublicOrigin = ConfigurationManager.AppSettings["origin"] + @"/",
                    SigningCertificate = LoadCertificate(),
                    RequireSsl = false,
                    LoggingOptions = new LoggingOptions() {EnableHttpLogging = true, EnableKatanaLogging = true, EnableWebApiDiagnostics = true, WebApiDiagnosticsIsVerbose = true},
#if DEBUG
                    AuthenticationOptions = new IdentityServer3.Core.Configuration.AuthenticationOptions
                    {
                        IdentityProviders = ConfigureIdentityProviders
                    },
                    CspOptions = new CspOptions
                    {
                        Enabled = false,
                        //ScriptSrc = "ajax.googleapis.com",
                        //StyleSrc = "maxcdn.bootstrapcdn.com; font-src fonts.gstatic.com localhost:13207",
                    },
#endif
                    //https://fonts.gstatic.com/s/opensans/v13/k3k702ZOKiLJc3WVjuplzD0LW-43aMEzIO6XUTLjad8.woff2
                };
                id.UseIdentityServer(options);
            });
            //Identity Manager
            app.Map("/identityadmin", adminApp =>
            {

                adminApp.UseOpenIdConnectAuthentication(new Microsoft.Owin.Security.OpenIdConnect.OpenIdConnectAuthenticationOptions
                {
                    AuthenticationType = "oidc",
                    Authority = ConfigurationManager.AppSettings["origin"] + @"/" + IDENTITY_PATH,
                    ClientId = "idmgr_client",
                    RedirectUri = ConfigurationManager.AppSettings["origin"] + @"/identityadmin/#/",
                    ResponseType = "id_token",
                    UseTokenLifetime = false,
                    Scope = "openid idmgr",
                    SignInAsAuthenticationType = "Cookies",

                });
                adminApp.UseIdentityManager(new IdentityManagerOptions()
                {
                    Factory = new IdentityManagerServiceFactory().Configure("identityDbConnection"),
                    SecurityConfiguration = new HostSecurityConfiguration
                    {
                        RequireSsl = false,
                        HostAuthenticationType = "Cookies",
                        AdminRoleName = "Administrator",
                        ShowLoginButton = true,
                        RoleClaimType = "role",
                        NameClaimType = "name"
                    },
                    DisableUserInterface = false,
                });
            });

            //Client Admin
            app.Map("/authadmin", authAdmin =>
            {
                authAdmin.UseOpenIdConnectAuthentication(new Microsoft.Owin.Security.OpenIdConnect.OpenIdConnectAuthenticationOptions
                {
                    AuthenticationType = "oidc",
                    Authority = ConfigurationManager.AppSettings["origin"] + @"/" + IDENTITY_PATH,
                    ClientId = "idAdmin",
                    RedirectUri = ConfigurationManager.AppSettings["origin"] + @"/authadmin/#/",
                    ResponseType = "id_token",
                    UseTokenLifetime = false,
                    Scope = "openid idAdmin",
                    SignInAsAuthenticationType = "Cookies",

                });
                var factory = new IdentityAdminServiceFactory();
                factory.Configure();
                authAdmin.UseIdentityAdmin(new IdentityAdminOptions
                {
                    Factory = factory,
                    AdminSecurityConfiguration = new IdentityAdmin.Configuration.AdminHostSecurityConfiguration()
                    {
                        RequireSsl = false,
                        HostAuthenticationType = "Cookies",
                        AdminRoleName = "Administrator",
                        ShowLoginButton = true,
                        RoleClaimType = "role",
                        NameClaimType = "name",

                    },
                    DisableUserInterface = false

                });
            });

        }
        

        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\certificates\idsrv3test.pfx",
                    AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }

        private void ConfigureIdentityProviders(IAppBuilder appBuilder, string signInAsType)
        {
            var windowsAuthentication = new WsFederationAuthenticationOptions
            {
                AuthenticationType = "windows",
                Caption = "Windows",
                SignInAsAuthenticationType = signInAsType,
                MetadataAddress = "http://localhost:51209/",
                Wtrealm = "urn:win"

            };
            appBuilder.UseWsFederationAuthentication(windowsAuthentication);
        }
    }
}