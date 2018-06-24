using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Models.Interfaces;

namespace Blog.Core.Models
{
    public class BlogMain: IBlog
    {
        private readonly IPageGenerator _pageGenerator;

        public BlogMain(IPageGenerator pageGenerator)
        {
            _pageGenerator = pageGenerator;
        }

        public async Task<IPageContext> GetPostFeed(int page = 1)
        {
            return await _pageGenerator.GetContextForPage(page);
        }

        public async Task<IPageContext> GetFilteredPostPage(Func<IEnumerable<Post>, IEnumerable<Post>> filter)
        {
            return await _pageGenerator.GetFilteredPostPageContext(filter);
        }
        
        public IPageContext GetBlogContent()
        {
            return _pageGenerator.GetMetadataOnlyContext();
        }
    }
}