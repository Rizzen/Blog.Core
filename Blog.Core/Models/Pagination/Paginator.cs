using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Core.Extensions;
using Blog.Core.Models.Interfaces;
using Blog.Core.Models.Templating.Interfaces;

namespace Blog.Core.Models.Pagination
{
    public class Paginator
    {
        private readonly IPostFacade _facade;
        private readonly IPageContext _pageContext;
        
        private readonly List<Post> _posts;

        public int PageNumber { get;}
        
        public int PageCount { get; }
        
        public List<Post> Posts => _posts.Any(x => x.Content.IsNullOrEmpty())
                                                ? _facade.GenRenderedPosts(_posts, _pageContext).Result.ToList()
                                                : _posts;

        public Paginator(IPostFacade facade, IPageContext pageContext, int pageNum, int postsPerPage)
        {
            _pageContext = pageContext;
            _facade = facade;
            
            var pages = (double) _pageContext.Blog.Posts.Count / postsPerPage;
            PageCount = (int) Math.Ceiling(pages);
            PageNumber = pageNum;
            
            _posts = _pageContext.Blog.Posts.Skip((PageNumber - 1) * postsPerPage)
                                            .Take(postsPerPage)
                                            .ToList();
        }
    }
}