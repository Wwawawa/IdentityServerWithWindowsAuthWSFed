using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Owin;

namespace IdentityServer.Windows.WSFederationServer.Extensions
{
    public static class WindowsAuthenticationExtensions
    {
        public static IAppBuilder UseWindowsAuthentication(this IAppBuilder app)
        {
            object value;
            if (app.Properties.TryGetValue("System.Net.HttpListener", out value))
            {
                var listener = value as HttpListener;
                if (listener != null)
                {
                    listener.AuthenticationSchemes =
                        AuthenticationSchemes.Anonymous |
                        AuthenticationSchemes.Negotiate;
                }
            }

            return app;
        }
    }
}