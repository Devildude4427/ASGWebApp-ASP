﻿using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Dapper;
using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Configuration;
using Persistence.Seeding;
using Services;
using Web.Auth;
using Web.Config;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            AppConfig = Configuration.GetSection("App").Get<AppConfig>();
            _env = env;
        }

        public IConfiguration Configuration { get; }
        private AppConfig AppConfig { get; }
        public static IContainer ApplicationContainer { get; private set; }
        private readonly IHostingEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = _env.IsDevelopment() ? SameSiteMode.None : SameSiteMode.Strict;
            });
            
            services.AddAuthentication(o =>
                {
                    o.DefaultScheme = AuthHandler.AuthName;
                })
                .AddAuth(o => { });
            
            services.AddAuthorization(o =>
            {
                o.AddPolicy("Admin", p => p.RequireAssertion(c =>
                {
                    var claim = c.User.FindFirst(ClaimTypes.Role);
                    if (claim == null) return false;

                    if (Enum.TryParse(claim.Value, out UserRole userRole))
                        return userRole >= UserRole.Admin;

                    return false;
                }));
            });
           
            services.AddScoped(c =>
            {
                var ctx = c.GetRequiredService<IHttpContextAccessor>().HttpContext;
                var user = ctx.Items["User"] as IUserIdentity;
                return user ?? UserIdentity.NoUser;
            });
            
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.Name = "Auth";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy =
                    _env.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
                options.Cookie.SameSite = _env.IsDevelopment() ? SameSiteMode.None : SameSiteMode.Strict;
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
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();
            
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
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