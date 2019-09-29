using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Mahogany;
using Microsoft.AspNetCore.Routing;

namespace aspnetcore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Enable vertical-sliced folder structure
            var mvcBuilder = services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddMvcOptions(options => options.Conventions.Add(new FeatureConvention()))
                .AddRazorOptions(options =>
                {
                    options.ConfigureFeatureFolders();
                })
                .AddRazorPagesOptions(options =>
                {                    
                    options.RootDirectory = "/";
                    options.Conventions.Add(new PageRouteConvention());
                });

            services.AddProjects(mvcBuilder, MahoganyAssemblies);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
 
            app.UseMvc(routes =>
            {
                routes.MapMahoganyRoute(app.ApplicationServices, MahoganyAssemblies);                

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private IList<Assembly> MahoganyAssemblies
        {
            get
            {
                var mahoganyAssembiles = new List<Assembly>()
                {
                    typeof(project1.BoundedContexts.BoundedContext1.Features.Home.HomeController).GetTypeInfo().Assembly,
                    typeof(project2.BoundedContexts.BoundedContext2.Features.Home.HomeController).GetTypeInfo().Assembly
                };

                return mahoganyAssembiles;
            }
        }
    }
}
