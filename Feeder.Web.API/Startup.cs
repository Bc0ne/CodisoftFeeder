namespace Feeder.Web.API
{
    using Autofac;
    using Feeder.Data.Context;
    using Feeder.Web.API.Bootstraper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Swashbuckle.AspNetCore.Swagger;

    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var connection = Configuration["ConnectionStrings:FeederDbConnection"];
            services.AddDbContext<FeederContext>(options => options.UseSqlServer(connection));

            services.AddMvc(setupAction =>
           {
               setupAction.ReturnHttpNotAcceptable = true;
           });

            services.AddResponseCaching();

            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1",
                   new Info
                   {
                       Version = "v1",
                       Title = "Feeder API",
                       Description = "Feeder - APIs documentation",
                       TermsOfService = "none"
                   });
           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseCaching();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
           {
               c.SwaggerEndpoint("../swagger/v1/swagger.json", "Feeder API V1");
               c.RoutePrefix = "docs";
           });

            app.UseMvc();
        }

        public void ConfigureContainer(ContainerBuilder builder) =>
            builder.RegisterModule(new DependecyResolver(HostingEnvironment));
    }
}
