using System;
using System.Collections.Generic;
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

        public IPageContext GetPostFeed(int page = 1)
        {
            return _pageGenerator.GetContextForPage(page);
        }

        public IPageContext GetFilteredPostPage(Func<IEnumerable<Post>, IEnumerable<Post>> filter)
        {
            return _pageGenerator.GetFilteredPostPageContext(filter);
        }
        
        public IPageContext GetBlogContent()
        {
            return _pageGenerator.GetMetadataOnlyContext();
        }
    }
}