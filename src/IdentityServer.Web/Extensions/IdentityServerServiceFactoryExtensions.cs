using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer.Web.Services;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.EntityFramework;
using Context = IdentityServer.Web.IdentityStores.Context;
using UserManager = IdentityServer.Web.IdentityStores.UserManager;
using UserStore = IdentityServer.Web.IdentityStores.UserStore;


namespace IdentityServer.Web.Extensions
{
    public static class IdentityServerServiceFactoryExtensions
    {
        public static IdentityServerServiceFactory Configure(this IdentityServerServiceFactory factory, string connectionString)
        {

            var serviceOptions = new EntityFrameworkServiceOptions { ConnectionString = connectionString };
            factory.RegisterOperationalServices(serviceOptions);

            //Use client and scope stores from database
            factory.RegisterConfigurationServices(serviceOptions);

            factory.Register(new Registration<Context>(resolver => new Context(connectionString)));
            factory.Register(new Registration<UserStore>());
            factory.Register(new Registration<UserManager>());
            factory.UserService = new Registration<IUserService, IdentityUserService>();

            return factory;

        }
    }
}