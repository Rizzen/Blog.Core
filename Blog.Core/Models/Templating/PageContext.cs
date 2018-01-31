using System.Collections.Generic;
using System.Linq;
using Blog.Core.Models.Pagination;

namespace Blog.Core.Models.Templating
{
    public class PageContext
    {
        //TODO DELETE!!!!111
        public IEnumerable<Post> Posts;
        public PageContext(){}
        //END TODO
        
        public Paginator Paginator { get; }
        public BlogContext Blog { get; }
        
        //TODO postsPerPage is temporary - pls remove it later
        public PageContext(BlogContext blog, int pageNum, int postsPerPage)
        {
            Paginator = new Paginator(blog, pageNum, postsPerPage);
            Blog = blog;
        }
    }
}