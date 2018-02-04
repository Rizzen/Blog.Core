using System.Collections.Generic;
using System.Linq;
using Blog.Core.Models.DAL;
using Blog.Core.Models.Templating;

namespace Blog.Core.Models.Contexts
{
    public class BlogContext
    {
        private readonly PostFacade _facade;

        public List<Post> Posts => _facade.GetAllPostsMetadataOnly();
        
        public List<string> Tags => Posts.SelectMany(x => x.Tags)
                                         .Distinct()
                                         .ToList();
        
        public BlogContext(PostFacade facade)
        {
            _facade = facade;
        }
    }
}