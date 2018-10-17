namespace Feeder.Web.API.Bootstraper
{
    using Autofac;
    using Feeder.Data.Context;
    using Feeder.Data.Repositiores;
    using Microsoft.AspNetCore.Hosting;

    public class DependecyResolver : Module
    {
        private readonly IHostingEnvironment _env;

        public DependecyResolver(IHostingEnvironment env)
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
        }
    }
}
