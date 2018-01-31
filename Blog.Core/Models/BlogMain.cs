using Blog.Core.Models.Pagination;
using Blog.Core.Models.Templating;

namespace Blog.Core.Models
{
    public class BlogMain
    {
        private readonly BlogContext _context;
        private readonly PageGenerator _pageGenerator;

        public BlogMain(BlogContext context, PageGenerator pageGenerator)
        {
            _context = context;
            _pageGenerator = pageGenerator;
        }

        public PageContext GetPostFeed()
        {
            //BUG for now
            return _pageGenerator.GetContextForPage(1);
        }
    }
}