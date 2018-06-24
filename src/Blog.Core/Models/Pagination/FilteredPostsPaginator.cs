using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Domain.Entities;
using Blog.Core.Models.Interfaces;
using Blog.Core.Models.Templating.Interfaces;

namespace Blog.Core.Models.Pagination
{
    public class FilteredPostsPaginator: IPaginator
    {
        public int PageNumber { get; } = 0;
        public int PageCount { get; } = 0;
        
        private readonly IPostFacade _facade;
        private readonly IPageContext _pageContext;
        
        private List<Post> _post;

        public List<Post> Posts => _post;
        
        public FilteredPostsPaginator(IPostFacade facade,
                                      IPageContext pageContext, 
                                      Func<IEnumerable<Post>, IEnumerable<Post>> filter)
        {
            _pageContext = pageContext;
            _facade = facade;
            _post = filter(_pageContext.Blog.Posts).OrderByDescending(x => x.DateTime).ToList(); 
        }
        
        public async Task<IPaginator> InitializeAsync()
        {
            _post = (await _facade.GenRenderedPosts(_post, _pageContext)).ToList();
            return this;
        }
    }
}