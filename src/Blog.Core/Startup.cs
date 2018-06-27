using System;
using System.IO;
using Blog.Core.Caching.Caching;
using Blog.Core.DAL.Posts;
using Blog.Core.Domain.Entities;
using Blog.Core.Domain.Settings;
using Blog.Core.Metadata;
using Blog.Core.Models.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Blog.Core.Razor.Razor;
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
            
            services.AddSingleton<RazorEngine>();
            
            services.AddTransient<InitialStateCreator>();
            
            services.AddScoped<IPostStore, PostStore>();
            services.AddScoped<IPostCache, PostCache>();
            services.AddScoped<IMetadataProcessor, MetadataProcessor>();
            services.AddScoped<IBlogService, BlogService>();
            
            services.AddSingleton<ICache<Post>, ConcurrentCache<Post>>();
        }
        
        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            
            var initor = serviceProvider.GetService<InitialStateCreator>();
            initor.Init();
            
            app.UseMvc(routes => {
                routes.MapRoute(name: "single", 
                                template: "Posts/{postName}",
                                defaults: new {Controller = "Home", action="SinglePostPage"});
                
                routes.MapRoute(name: "pagination",
                                template: "Home/Page/{page}",
                                defaults: new {Controller = "Home", action = "Index"});
                
                routes.MapRoute(name: "tags",
                                template: "Posts/tag/{tag}",
                                defaults: new {Controller = "Home", action = "PostsWithTagPage"});
                
                routes.MapRoute(name: "default", 
                                template: "{controller=Home}/{action=Index}");
                
                routes.MapRoute(name: "about",
                                template: "{controller=About}/{action=Index}");
                
                routes.MapRoute(name: "content",
                                template: "{controller=Content}/{action=Content}");
            });
        }
        
        private string GetPathToSettingsFile(string settingsFileName)
        {
            return "Settings" + Path.DirectorySeparatorChar + settingsFileName;
        }
    }
}