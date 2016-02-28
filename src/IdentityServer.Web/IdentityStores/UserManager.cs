using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityServer.Web.IdentityStores
{
    public class UserManager : UserManager<IdentityUser, string>
    {
        public UserManager(UserStore userStore)
            : base(userStore)
        {
        }
    }
}