using System.Reflection;
using Autofac;
using Persistence.Configuration;
using Persistence.Repositories;
using Services;
using Web.Config;

namespace Web
{
    // ReSharper disable once InconsistentNaming
    public static class DIConfig
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
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<DatabaseConnectionStringProvider>().As<IDatabaseConnectionString>();
            builder.RegisterType<DatabaseConnection>().AsSelf().InstancePerLifetimeScope();

            return builder;
        }
    }
}