using System;
using Autofac;
using Autofac.Core.Lifetime;
using Tests.Integration.Config;
using Web;

namespace Tests.Unit.Config
{
    public class UnitTest : IDisposable
    {
        private readonly TestDatabaseInitializer _db;
        private static IContainer Container { get; set; }

        protected UnitTest()
        {
            InitialiseContainer();
            Console.WriteLine("Setting up database");
            _db = new TestDatabaseInitializer(Container);
        }
        
        private static void InitialiseContainer()
        {
            if (Container != null) return;
            var b = new ContainerBuilder();
            DIConfig.Configure(new TestConfig(), b);
            Container = b.Build();
        }

        protected static ILifetimeScope GetContainer()
        {
            return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
        }
        
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}