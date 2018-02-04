using System.Collections.Generic;
using Blog.Core.Models.Pagination;
using Blog.Core.Models.Templating;

namespace Blog.Core.Models.Contexts
{
    public class PageContext
    {
        //TODO DELETE!!!!111
        public IEnumerable<Post> Posts;
        public PageContext(){}
        //END TODO
        
        public Paginator Paginator { get; }
        public BlogContext Blog { get; }

        public PageContext(BlogContext blog)
        {
            Blog = blog;
        }
        
        //TODO postsPerPage is temporary - pls remove it later
        public PageContext(BlogContext blog, PostFacade facade, int pageNum, int postsPerPage) : this(blog)
        {
            Paginator = new Paginator(facade, this, pageNum, postsPerPage);
        }
    }
}