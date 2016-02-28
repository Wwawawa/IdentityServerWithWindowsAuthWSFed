using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityServer.Web.IdentityStores
{
    public class RoleStore : RoleStore<IdentityRole>
    {
        public RoleStore(IdentityServer.Web.IdentityStores.Context context) : base(context) { }
    }
}