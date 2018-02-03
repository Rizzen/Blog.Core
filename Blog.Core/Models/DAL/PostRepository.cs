using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Blog.Core.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;

namespace Blog.Core.Models.DAL
{
    public class PostRepository: IPostRepository
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly Cache<Post> _cache;
        
        public IList<Post> Posts => GetAllPostsFromDefaultDirectory();
        
        private string _path => _hostingEnvironment.ContentRootPath;
        
        public PostRepository(IHostingEnvironment hostingEnvironment, Cache<Post> cache)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        
        private IList<Post> GetAllPostsFromDefaultDirectory()
        {
            return Directory.GetFiles($"{_path}/Views/_posts", "*.cshtml", SearchOption.AllDirectories)
                                     .Select(p => p.Replace(_path, "~"))
                                     .Select(p => new Post {Content = p}) //TODO - Change contents to Filename
                                     .ToList();
        }

        public IList<string> GetContentByFilename(IEnumerable<string> names)
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