using Blog.Core.Models.Contexts;
using Blog.Core.Models.Pagination;
using Blog.Core.Models.Templating;

namespace Blog.Core.Models
{
    public class BlogMain
    {
        private readonly PageGenerator _pageGenerator;

        public BlogMain(PageGenerator pageGenerator)
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