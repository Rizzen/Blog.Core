using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Blog.Core.Models.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Blog.Core.Models.DAL
{
    public class PostStore: IPostStore
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly SiteSettings _siteSettings;
        
        public List<Post> Posts => GetAllPostsWithNames();

        private string _path => $"{_hostingEnvironment.ContentRootPath}{_siteSettings.PostsFolderPath}";
        
        public PostStore(IHostingEnvironment hostingEnvironment, IOptions<SiteSettings> siteSettings)
        {
            _hostingEnvironment = hostingEnvironment;
            _siteSettings = siteSettings.Value;
        }
        
        private List<Post> GetAllPostsWithNames()
        {
            return Directory.GetFiles(_path, "*.cshtml", SearchOption.AllDirectories)
                                     .Select(p => p.Replace(_path, "~"))
                                     .Select(p => new Post {Filename = p})
                                     .ToList();
        }

        public List<string> GetContentByFilename(IEnumerable<string> names)
        {
            return names.Select(GetContentByFilename)
                        .ToList();
        }
        
        public string GetContentByFilename(string name)
        {
            var fullPath = name.Replace("~", _path);
            string result;
            
            try
            {
                result = File.ReadAllText(fullPath);
            }
            catch (Exception e)
            {
                result = $"Content removed or unaccessible. Exception is: {e.Message}";
            }

            return result;
        }
    }
}