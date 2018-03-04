using System.Collections.Generic;
using System.Linq;
using Blog.Core.Models.Interfaces;
using Blog.Core.Models.Settings;
using Blog.Core.Models.Templating.Interfaces;
using Microsoft.Extensions.Options;

namespace Blog.Core.Models.Contexts
{
    public class BlogContext: IBlogContext
    {
        private readonly IPostFacade _facade;
        private readonly int _postsPerPage;

        
        public List<Post> Posts => _facade.GetAllPostsMetadataOnly();

        public int TotalPages => Posts.Count / _postsPerPage;
        
        public List<string> Tags => Posts.SelectMany(x => x.Tags)
                                         .Distinct()
                                         .ToList();
        
        public BlogContext(IPostFacade facade, IOptions<SiteSettings> siteSetting)
        {
            _postsPerPage = siteSetting.Value.PostsPerPage;
            _facade = facade;
        }
    }
}