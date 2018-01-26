using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;

namespace Blog.Core.Models.DAL
{
    public class PostRepository: IPostRepository
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public IEnumerable<Post> Posts => GetAllPostsFromDefaultDirectory();

        private string _path => _hostingEnvironment.ContentRootPath;
        
        public PostRepository(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        
        /// <summary>Returns posts from default directory</summary>
        private IEnumerable<Post> GetAllPostsFromDefaultDirectory()
        {
            return Directory.GetFiles($"{_path}/Views/_posts", "*.cshtml", SearchOption.AllDirectories)
                                     .Select(p => p.Replace(_path, "~"))
                                     .Select(p => new Post {Contents = p})
                                     .ToList();
        }
    }
}