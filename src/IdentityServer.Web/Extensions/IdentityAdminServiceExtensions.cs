
using IdentityAdmin.Configuration;
using IdentityAdmin.Core;
using IdentityServer.Web.Services;

namespace IdentityServer.Web.Extensions
{
    public static class IdentityAdminServiceExtensions
    {

        public static void Configure(this IdentityAdminServiceFactory factory)
        {
            factory.IdentityAdminService = new Registration<IIdentityAdminService, IdentityAdminManagerService>();

        }
    }
}