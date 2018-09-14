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
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        private IConfiguration Configuration { get; }
        private IHostingEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<BlogContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("7DCYL"), o => o.MigrationsAssembly("Web")
                                                                                       .UseRowNumberForPaging()));

            services.AddIdentity<User, Role>()
                    .AddEntityFrameworkStores<BlogContext>()
                    .AddDefaultTokenProviders();

            services.AddOptions();
            services.AddBusinessLogicServices(Configuration);
            services.AddDataAccessServices();

            // AppSettings Configuration
            services.Configure<AppSettings>(Configuration);

            var skipHttps = Configuration.GetValue<bool>("SkipHTTPS");
            services.Configure<MvcOptions>(options =>
            {
                // Set LocalTest:skipHTTPS to true to skip SSL requrement in 
                // debug mode. This is useful when not using Visual Studio.
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

            // Response Compression
            services.Configure<GzipCompressionProviderOptions>(options => 
            {
                options.Level = CompressionLevel.Optimal;
            });
            services.AddResponseCompression(options => 
            {
                options.EnableForHttps = true; //Remove this during deployment
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes;
            });
        }

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
            app.UseResponseCompression();
            app.UseMvc(routes =>
            {
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
