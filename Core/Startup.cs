﻿using System;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.Domain;
using Core.Persistence.Configuration;
using Core.Persistence.Seeding;
using Core.Services;
using Core.Services.Helpers;
using Core.Web.Config;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            AppConfig = Configuration.GetSection("App").Get<AppConfig>();
            _env = env;
        }

        private IConfiguration Configuration { get; }
        private AppConfig AppConfig { get; }
        private static IContainer ApplicationContainer { get; set; }
        private readonly IHostingEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            // services.Configure<CookiePolicyOptions>(options =>
            // {
            //     options.CheckConsentNeeded = context => true;
            //     options.MinimumSameSitePolicy = _env.IsDevelopment() ? SameSiteMode.None : SameSiteMode.Strict;
            // });
           
            services.AddScoped(c =>
            {
                var ctx = c.GetRequiredService<IHttpContextAccessor>().HttpContext;
                var user = ctx.Items["User"] as IUserIdentity;
                return user ?? UserIdentity.NoUser;
            });
            
            services.AddCors(options =>
            {
                options.AddPolicy("VueCorsPolicy", policy =>
                {
                    policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:8080");
                });
            });
            
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            
            var builder = new ContainerBuilder();
            builder.Populate(services);
            DIConfig.Configure(AppConfig, builder);
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            InitialiseDatabase(AppConfig.SeedSettings, env).Wait();
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            // app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseCors("VueCorsPolicy");
            
            app.UseMvc();
        }
        
        private static async Task InitialiseDatabase(SeedSettings seedSettings, IHostingEnvironment env)
        {
            if (seedSettings.ShouldResetDatabase) await ResetDb(env);
            else RunMigrations();
        }
        
        private static void RunMigrations()
        {
            using (var dbInitializer = new DatabaseInitializer(DatabaseConnectionStringProvider.GetConnectionString()))
            {
                Console.WriteLine("Running migrations.");
                dbInitializer.InitializeDatabase();
            }
        }

        private static async Task ResetDb(IHostingEnvironment env)
        {
            using (var dbInitializer = new DatabaseInitializer(DatabaseConnectionStringProvider.GetConnectionString()))
            {
                Console.WriteLine("Dropping database objects");
                dbInitializer.DropDatabase();
                Console.WriteLine("Creating database objects");
                dbInitializer.InitializeDatabase();
                await Seed(env);
                Console.WriteLine("Database ready");
            }
        }

        private static async Task Seed(IHostingEnvironment env)
        {
            Console.WriteLine("Seeding database");
            if (env.IsDevelopment())
                await Devseed.Execute(ApplicationContainer);
        }
    }
}