using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Core.Extensions;
using Blog.Core.Models.Interfaces;
using Blog.Core.Models.Templating.Interfaces;

namespace Blog.Core.Models.Pagination
{
    public class FilteredPostsPaginator: IPaginator
    {
        public int PageNumber { get; }
        public int PageCount { get; }
        
        private readonly IPostFacade _facade;
        private readonly IPageContext _pageContext;
        private readonly List<Post> _post;

        public List<Post> Posts => _post.Any(x => x.Content.IsNullOrEmpty())
                                    ? _facade.GenRenderedPosts(_post, _pageContext).Result
                                    : _post;

        public FilteredPostsPaginator(IPostFacade facade,
                                      IPageContext pageContext, 
                                      Func<IEnumerable<Post>, IEnumerable<Post>> filter)
        {
            _pageContext = pageContext;
            _facade = facade;
            _post = filter(_pageContext.Blog.Posts).ToList(); 
        }
    }
}