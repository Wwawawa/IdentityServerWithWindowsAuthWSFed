using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using IdentityServer.WindowsAuthentication.Services;


namespace IdentityServer.Windows.WSFederationServer.Services
{
    public class AdditionalWindowsClaimsProvider : ICustomClaimsProvider
    {
        public Task TransformAsync(CustomClaimsProviderContext context)
        {
            // find name claim on outgoing subject
            var nameClaim = context.OutgoingSubject.Claims.FirstOrDefault(c => c.Type == "name");
            
            return Task.FromResult(0);
        }
    }
}