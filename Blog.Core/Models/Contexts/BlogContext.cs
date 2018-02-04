using System.Collections.Generic;
using System.Linq;
using Blog.Core.Models.DAL;
using Blog.Core.Models.Interfaces;
using Blog.Core.Models.Templating;
using Blog.Core.Models.Templating.Interfaces;

namespace Blog.Core.Models.Contexts
{
    public class BlogContext: IBlogContext
    {
        private readonly IPostFacade _facade;

        public List<Post> Posts => _facade.GetAllPostsMetadataOnly();
        
        public List<string> Tags => Posts.SelectMany(x => x.Tags)
                                         .Distinct()
                                         .ToList();
        
        public BlogContext(IPostFacade facade)
        {
            _facade = facade;
        }
    }
}