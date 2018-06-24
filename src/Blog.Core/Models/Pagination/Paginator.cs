using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Extensions;
using Blog.Core.Models.Interfaces;
using Blog.Core.Models.Templating.Interfaces;

namespace Blog.Core.Models.Pagination
{
    public class Paginator: IPaginator
    {
        private readonly IPostFacade _facade;
        private readonly IPageContext _pageContext;
        
        private List<Post> _posts;

        public int PageNumber { get;}
        
        public int PageCount { get; }
        
        public List<Post> Posts => _posts;

        public Paginator(IPostFacade facade, IPageContext pageContext, int pageNum, int postsPerPage)
        {
            _pageContext = pageContext;
            _facade = facade;
            
            var pages = (double) _pageContext.Blog.Posts.Count / postsPerPage;
            PageCount = (int) Math.Ceiling(pages);
            PageNumber = pageNum;
            
            _posts = _pageContext.Blog.Posts.OrderByDescending(x => x.DateTime)
                                            .Skip((PageNumber - 1) * postsPerPage)
                                            .Take(postsPerPage)
                                            .ToList();
        }

        public async Task<IPaginator> InitializeAsync()
        {
            _posts = (await _facade.GenRenderedPosts(_posts, _pageContext)).ToList();
            return this;
        }
    }
}