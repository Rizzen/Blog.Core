using System;
using System.IO;
using Blog.Core.Models;
using Blog.Core.Models.Contexts;
using Blog.Core.Models.DAL;
using Blog.Core.Models.Interfaces;
using Blog.Core.Models.Pagination;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Blog.Core.Models.Settings;
using Blog.Core.Models.Templating;
using Blog.Core.Models.Templating.Interfaces;
using Blog.Core.Models.Templating.Processing;
using Blog.Core.Models.Templating.Razor;
using Blog.Core.Utils;

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
            services.AddOptions();
            
            services.Configure<SiteSettings>(Configuration.GetSection("SiteSettings"));
            
            services.AddTransient<RazorEngine>();
            services.AddTransient<InitialStateCreator>();
            services.AddTransient<ICache<Post>, ConcurrentCache<Post>>();
            
            services.AddScoped<IPostDAO, PostDAO>();
            services.AddScoped<IPostsProcessor, PostsProcessor>();
            services.AddScoped<IPageGenerator, PageGenerator>();
            services.AddScoped<IBlogContext, BlogContext>();
            services.AddScoped<IPostFacade, PostFacade>();
            services.AddScoped<IBlog, BlogMain>();
            
            services.AddSingleton<PostCache>();
        }
        
        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();

            var initor = serviceProvider.GetService<InitialStateCreator>();
            initor.Init();
            
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
                routes.MapRoute(name: "content",
                                    template: "{controller=Content}/{action=Content}");
                routes.MapRoute(name: "some",
                                template: "{controller}/{action}");
            });
        }
        
        private string GetPathToSettingsFile(string settingsFileName)
        {
            return "Settings" + Path.DirectorySeparatorChar + settingsFileName;
        }
    }
}