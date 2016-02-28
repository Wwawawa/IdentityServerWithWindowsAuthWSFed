using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityServer.Web.IdentityStores
{
    public class Context : IdentityDbContext<IdentityUser, IdentityRole, String, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public Context(string connectionString):
            base(connectionString)
        {
            
        }
    }
}