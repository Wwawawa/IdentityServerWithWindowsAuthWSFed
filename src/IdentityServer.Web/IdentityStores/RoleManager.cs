using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityServer.Web.IdentityStores
{
    public class RoleManager : RoleManager<IdentityRole>
    {
        public RoleManager(RoleStore roleStore) : base(roleStore) { }
    }
}