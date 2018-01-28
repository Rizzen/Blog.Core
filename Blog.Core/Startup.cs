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
using Blog.Core.Models.Templating;
using Blog.Core.Models.Templating.Processing;
using Blog.Core.Models.Templating.Razor;

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
            
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<BlogContext>();
            services.AddTransient<PostsProcessor>();
            services.AddTransient<RazorEngine>();
            
            services.AddSingleton<PostAccumulator>();
            
            services.AddSingleton<PostCache>();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.ApplicationServices.GetService<PostAccumulator>();
            
            app.UseMvc(routes => {
                routes.MapRoute(
                    "Default", 
                    "{controller}/{action}",
                    new { controller = "Home", action = "Index"}
                );
                routes.MapRoute(name: "index",
                                    template: "{controller=Home}/{action=Index}");
                routes.MapRoute(name: "about",
                                    template: "{controller=About}/{action=Index}");
            });
        }
        
        private string GetPathToSettingsFile(string settingsFileName)
        {
            return "Settings" + Path.DirectorySeparatorChar + settingsFileName;
        }
    }
}