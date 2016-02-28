using System;
using IdentityManager.AspNetIdentity;
using Microsoft.AspNet.Identity.EntityFramework;
using RoleManager = IdentityServer.Web.IdentityStores.RoleManager;
using UserManager = IdentityServer.Web.IdentityStores.UserManager;

namespace IdentityServer.Web.Services
{
    public class IdentityManagerService : AspNetIdentityManagerService<IdentityUser, String, IdentityRole, String>
    {
        public IdentityManagerService(UserManager userManager, RoleManager roleManager)
           : base(userManager, roleManager)
        {

        }
    }
}