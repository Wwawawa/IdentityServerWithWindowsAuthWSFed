using IdentityManager;
using IdentityManager.Configuration;
using IdentityServer.Web.Services;
using Context = IdentityServer.Web.IdentityStores.Context;
using RoleManager = IdentityServer.Web.IdentityStores.RoleManager;
using RoleStore = IdentityServer.Web.IdentityStores.RoleStore;
using UserManager = IdentityServer.Web.IdentityStores.UserManager;
using UserStore = IdentityServer.Web.IdentityStores.UserStore;

namespace IdentityServer.Web.Extensions
{
    public static class IdentityManagerServiceExtensions
    {
        public static IdentityManagerServiceFactory Configure(this IdentityManagerServiceFactory factory, string connectionString)
        {

            factory.Register(new Registration<Context>(resolver => new Context(connectionString)));
            factory.Register(new Registration<UserStore>());
            factory.Register(new Registration<RoleStore>());
            factory.Register(new Registration<UserManager>());
            factory.Register(new Registration<RoleManager>());

            factory.IdentityManagerService = new IdentityManager.Configuration.Registration<IIdentityManagerService, IdentityManagerService>();

            return factory;
        }
    }
}