using System.Reflection;
using Autofac;
using Core.Persistence.Configuration;
using Core.Persistence.Repositories;
using Core.Services;
using Core.Web.Config;

namespace Core
{
    public static class DiConfig
    {
        public static ContainerBuilder Configure(IAppConfig config, ContainerBuilder builder)
        {
            builder.Register(c => config).As<IAppConfig>();
            
            var persistence = Assembly.GetAssembly(typeof(UserRepository));
            builder.RegisterAssemblyTypes(persistence)
                .Where(t => t.Name.EndsWith("Repository") || t.Name.EndsWith("Query"))
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerLifetimeScope();

            var services = Assembly.GetAssembly(typeof(UserService));
            builder.RegisterAssemblyTypes(services)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<DatabaseConnectionStringProvider>().As<IDatabaseConnectionString>();
            builder.RegisterType<DatabaseConnection>().AsSelf().InstancePerLifetimeScope();

            return builder;
        }
    }
}