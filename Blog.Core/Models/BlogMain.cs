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

        public IPageContext GetSinglePostPage(string postName)
        {
            return _pageGenerator.GetSinglePostPageContext(postName);
        }
        
        public IPageContext GetBlogContent()
        {
            return _pageGenerator.GetMetadataOnlyContext();
        }
    }
}