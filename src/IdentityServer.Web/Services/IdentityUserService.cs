using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.AspNetIdentity;
using Microsoft.AspNet.Identity.EntityFramework;
using UserManager = IdentityServer.Web.IdentityStores.UserManager;

namespace IdentityServer.Web.Services
{
    public class IdentityUserService : AspNetIdentityUserService<IdentityUser, string>
    {
        public IdentityUserService(UserManager userManager) : base(userManager)
        {
        }
    }
}