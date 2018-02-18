using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace Blog.Core.Models.DAL
{
    public class PostRepository: IPostRepository
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        
        public List<Post> Posts => GetAllPostsWithNames();
        
        private string _path => _hostingEnvironment.ContentRootPath;
        
        public PostRepository(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        
        private List<Post> GetAllPostsWithNames()
        {
            return Directory.GetFiles($"{_path}/Views/_posts", "*.cshtml", SearchOption.AllDirectories)
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