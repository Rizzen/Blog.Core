using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Domain.Entities;
using Blog.Core.Domain.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Blog.Core.DAL.Posts
{
    public class PostStore: IPostStore
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly SiteSettings _siteSettings;
        
        //после разделения не бует нужно, скорее всего
        public List<Post> Posts => GetAllPostsWithNames();

        private string Path => $"{_hostingEnvironment.ContentRootPath}{_siteSettings.PostsFolderPath}/";
        
        public PostStore(IHostingEnvironment hostingEnvironment, IOptions<SiteSettings> siteSettings)
        {
            _hostingEnvironment = hostingEnvironment;
            _siteSettings = siteSettings.Value;
        }
        
        public async Task<string> GetContentByFilename(string name)
        {
            var fullPath = $"{Path}{name}.cshtml";
            string result;
            
            try
            {
                result = await File.ReadAllTextAsync(fullPath);
            }
            catch (Exception e)
            {
                result = await Task.FromResult($"Content removed or unaccessible. Exception is: {e.Message}");
            }

            return result;
        }
        
        private List<Post> GetAllPostsWithNames()
        {
            return Directory.GetFiles(Path, "*.cshtml", SearchOption.AllDirectories)
                            .Select(p => p.Replace(Path, string.Empty)
                                          .Replace(".cshtml", string.Empty))
                            .Select(p => new Post {Filename = p})
                            .ToList();
        }
    }
}