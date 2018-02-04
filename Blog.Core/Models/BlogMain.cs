using Blog.Core.Models.Contexts;
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

        public PageContext GetPostFeed()
        {
            //BUG for now
            return _pageGenerator.GetContextForPage(1);
        }

        public PageContext GetBlogContent()
        {
            return _pageGenerator.GetMetadataOnlyContext();
        }
    }
}