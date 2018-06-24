using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Domain.Entities;
using Blog.Core.Models.Interfaces;
using Blog.Core.Models.Pagination;
using Blog.Core.Models.Templating.Interfaces;

namespace Blog.Core.Models.Contexts
{
    public class PageContext: IPageContext
    {
        public IPaginator Paginator { get; private set; }
        public IBlogContext Blog { get; }

        public PageContext(IBlogContext blog)
        {
            Blog = blog;
        }
        
        public PageContext(IBlogContext blog,
                           IPostFacade facade,
                           int pageNum,
                           int postsPerPage) : this(blog)
        {
            Paginator = new Paginator(facade, this, pageNum, postsPerPage);
        }

        public PageContext(IBlogContext blog,
                           IPostFacade facade,
                           Func<IEnumerable<Post>, IEnumerable<Post>> filter) : this(blog)
        {
            Paginator = new FilteredPostsPaginator(facade, this, filter);
        }

        public async Task<PageContext> InitializeAsync()
        {
            Paginator = await Paginator.InitializeAsync();
            return this;
        }
    }
}