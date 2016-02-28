using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityServer.Web.IdentityStores
{
    public class UserStore : UserStore<IdentityUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public UserStore(IdentityServer.Web.IdentityStores.Context context) : base(context) { }
    }
}