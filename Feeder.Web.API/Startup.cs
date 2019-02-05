namespace Feeder.Web.API
{
    using Autofac;
    using Feeder.Data.Context;
    using Feeder.Web.API.Bootstraper;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using Serilog;
    using Swashbuckle.AspNetCore.Swagger;
    using System.Text;

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

            services.AddCors( options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["IdentitySettings:Isuser"],
                        ValidAudience = Configuration["IdentitySettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["IdentitySettings:SecurityKey"]))
                    };
                });

            services.AddMvc(setupAction =>
           {
               setupAction.ReturnHttpNotAcceptable = true;
           });


            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1",
                   new Info
                   {
                       Version = "v1",
                       Title = "Feeder API",
                       Description = "Feeder - APIs documentation",
                       TermsOfService = "none",
                       Contact = new Contact
                       {
                           Name = "Mahmoud",
                           Email = "m.moghni99@gmail.com"
                       }
                   });
           });

            services.AddHttpCacheHeaders(
                (expirationModelOptions) =>
                {
                    expirationModelOptions.MaxAge = 30;
                },
                (validationModelOptions) =>
                {
                    validationModelOptions.MustRevalidate = true;
                });

            services.AddResponseCaching();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var contextService = serviceScope.ServiceProvider.GetService<FeederContext>();

                if (contextService.AllMigrationsApplied())
                {
                    contextService.EnsureMigrated();
                }
            }

            app.UseAuthentication();
            app.UseCors("AllowAllOrigins");

            loggerFactory.AddConsole();
            loggerFactory.AddDebug(LogLevel.Information);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionHandlerFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global exception logger");
                            logger.LogError(500,
                                exceptionHandlerFeature.Error,
                                exceptionHandlerFeature.Error.Message);
                        }
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later");
                    });
                });
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
           {
               c.SwaggerEndpoint("../swagger/v1/swagger.json", "Feeder API V1");
               c.RoutePrefix = "docs";
           });

            app.UseResponseCaching();

            app.UseHttpCacheHeaders();

            app.UseMvc();
        }

        public void ConfigureContainer(ContainerBuilder builder) =>
            builder.RegisterModule(new DependencyResolver(HostingEnvironment));
    }
}
