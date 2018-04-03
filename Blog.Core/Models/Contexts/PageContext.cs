using Blog.Core.Models.Interfaces;
using Blog.Core.Models.Pagination;
using Blog.Core.Models.Templating.Interfaces;

namespace Blog.Core.Models.Contexts
{
    public class PageContext: IPageContext
    {
        public IPaginator Paginator { get; }
        public IBlogContext Blog { get; }

        public PageContext(IBlogContext blog)
        {
            Blog = blog;
        }
        
        public PageContext(IBlogContext blog, IPostFacade facade, int pageNum, int postsPerPage) : this(blog)
        {
            Paginator = new Paginator(facade, this, pageNum, postsPerPage);
        }

        public PageContext(IBlogContext blog, IPostFacade facade, string postName) : this(blog)
        {
            Paginator = new SinglePagePaginator(facade, this, postName);
        }
    }
}