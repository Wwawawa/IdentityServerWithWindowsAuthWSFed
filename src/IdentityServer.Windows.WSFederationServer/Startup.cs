using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using IdentityServer.Windows.WSFederationServer.Extensions;
using IdentityServer.Windows.WSFederationServer.Services;
using IdentityServer.WindowsAuthentication.Configuration;
using Owin;

namespace IdentityServer.Windows.WSFederationServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            log4net.Config.XmlConfigurator.Configure();
            //app.UseWindowsAuthentication();
            app.UseWindowsAuthenticationService(new WindowsAuthenticationOptions
            {
                IdpRealm = "urn:win",
                IdpReplyUrl = "http://localhost:13207/identity/was",
                PublicOrigin = "http://localhost:51209/",
                SigningCertificate = LoadCertificate(),
                CustomClaimsProvider = new AdditionalWindowsClaimsProvider(),
               EnableOAuth2Endpoint = true,
              
            });
        }
        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\Certificates\idsrv3test.pfx",
                AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }
    }
}