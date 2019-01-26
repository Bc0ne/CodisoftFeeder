namespace Feeder.Web.API.Bootstraper
{
    using Autofac;
    using Feeder.Core.FeederServices;
    using Feeder.Data.Context;
    using Feeder.Data.Repositiores;
    using Microsoft.AspNetCore.Hosting;

    public class DependencyResolver : Module
    {
        private readonly IHostingEnvironment _env;

        public DependencyResolver(IHostingEnvironment env)
        {
            _env = env;
        }

        protected override void Load(ContainerBuilder builder)
        {
            LoadModules(builder);
        }

        private void LoadModules(ContainerBuilder builder)
        {
            builder.RegisterType<FeederContext>().InstancePerLifetimeScope();
            builder.RegisterType<CollectionRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<FeedRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<FeederService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
