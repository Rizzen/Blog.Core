using Blog.Core.Models.DAL;
using Blog.Core.Models.Templating;

namespace Blog.Core.Models.Pagination
{
    public class PageGenerator
    {
        private readonly BlogContext _blogContext;
        private readonly IPostRepository _postRepository;

        public PageGenerator(BlogContext blogContext, IPostRepository postRepository)
        {
            _blogContext = blogContext;
            _postRepository = postRepository;
        }

        public PageContext GetContext()
        {
            return new PageContext();
        }
    }
}