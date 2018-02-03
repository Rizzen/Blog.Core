using Blog.Core.Models.DAL;
using Blog.Core.Models.Templating;

namespace Blog.Core.Models.Pagination
{
    public class PageGenerator
    {
        private readonly BlogContext _blogContext;
        private readonly PostFacade _facade;

        public PageGenerator(BlogContext blogContext, PostFacade facade)
        {
            _blogContext = blogContext;
            _facade = facade;
        }

        public PageContext GetContextForPage(int pageNum)
        {
            return new PageContext(_blogContext, _facade, pageNum, _blogContext.PostsPerPage);
        }
    }
}