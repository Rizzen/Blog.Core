using System.Collections.Generic;
using System.Linq;
using Blog.Core.Models.Interfaces;
using Blog.Core.Models.Settings;
using Blog.Core.Models.Templating.Interfaces;
using Blog.Core.Models.Templating.Processing;
using Microsoft.Extensions.Options;

namespace Blog.Core.Models.Contexts
{
    public class BlogContext: IBlogContext
    {
        private readonly IPostFacade _facade;

        public int PageCount { get; }

        public List<Post> Posts => _facade.GetAllPostsMetadataOnly();
       
        public List<Tag> Tags => Posts.SelectMany(x => x.Tags)
                                         .Distinct()
                                         .ToList();
        
        public BlogContext(IPostFacade facade, IOptions<SiteSettings> siteSetting)
        {
            var postsPerPage = siteSetting.Value.PostsPerPage;
            _facade = facade;
            
            PageCount = Posts.Count / postsPerPage;
        }
    }
}