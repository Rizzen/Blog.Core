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

        private List<Post> _posts;

        public IList<Post> Posts => _posts.All(x => x.Content.IsNullOrEmpty())
                                                ? _facade.GenRenderedPosts(_posts, _pageContext).Result.ToList()
                                                : _posts;

        public Paginator(IPostFacade facade, IPageContext pageContext, int pageNum, int postsPerPage)
        {
            _facade = facade;
            _pageContext = pageContext;
            _posts = _pageContext.Blog.Posts.Skip((pageNum - 1) * postsPerPage)
                                            .Take(postsPerPage)
                                            .ToList();
        }
    }
}