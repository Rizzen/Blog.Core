using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Models;
using Blog.Core.Models.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Blog.Core.Models.Settings;

namespace Blog.Core
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder().SetBasePath(hostingEnvironment.ContentRootPath)
                                                    .AddJsonFile(GetPathToSettingsFile("siteSetting.json"));
            Configuration = builder.Build();
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<SiteSettings>(Configuration.GetSection("SiteSettings"));
            services.AddSingleton<IPostRepository, PostRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            
            app.UseMvc(routes => {
                
                routes.MapRoute(
                    "Default", // Route name
                    "{controller}/{action}", // URL with parameters
                    new { controller = "Home", action = "Index"} // Parameter defaults
                );
                routes.MapRoute(name: "index",
                                    template: "{controller=Home}/{action=Index}");
                    routes.MapRoute(name: "about",
                                    template: "{controller=About}/{action=Index}");
                    routes.MapRoute("MyRoute", "X{controller}/X{action}/{id}");
            });
        }
        
        private string GetPathToSettingsFile(string settingsFileName)
        {
            return "Settings" + Path.DirectorySeparatorChar + settingsFileName;
            
        }
    }
}