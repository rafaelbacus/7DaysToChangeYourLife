using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using DAL;
using Model;
using Web.Data;
using Web.Services;
using Web.Models;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        private IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<BlogContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("7DCYL"), o => o.MigrationsAssembly("Web")));

            services.AddIdentity<User, Role>()
                    .AddEntityFrameworkStores<BlogContext>()
                    .AddDefaultTokenProviders();

            services.AddOptions();
            services.AddBusinessLogicServices(Configuration);
            services.AddDataAccessServices();

            var skipHttps = Configuration.GetValue<bool>("LocalTest:skipHTTPS");
            services.Configure<MvcOptions>(options => 
            {
                if (Environment.IsDevelopment() && !skipHttps)
                {
                    options.Filters.Add(new RequireHttpsAttribute());
                }
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            // AutoMapper Configuration
            var config = new AutoMapper.MapperConfiguration(cfg => 
            {
                cfg.AddProfile<MapperProfile>();
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                // routes.MapRoute(
                //     name: "login",
                //     template: "login",
                //     defaults: new { controller = "Account", action = "Login" });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            try
            {
                SeedData.SeedAdmin(serviceProvider).Wait(); 
                SeedData.SeedBlog(serviceProvider).Wait();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
