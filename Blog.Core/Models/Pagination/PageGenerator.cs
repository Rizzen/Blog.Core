using Blog.Core.Models.Contexts;
using Blog.Core.Models.DAL;
using Blog.Core.Models.Settings;
using Blog.Core.Models.Templating;
using Microsoft.Extensions.Options;

namespace Blog.Core.Models.Pagination
{
    public class PageGenerator
    {
        private readonly BlogContext _blogContext;
        private readonly PostFacade _facade;
        private readonly int _postsPerPage;
        
        public PageGenerator(BlogContext blogContext, PostFacade facade, IOptions<SiteSettings> siteSettings)
        {
            _blogContext = blogContext;
            _facade = facade;
            _postsPerPage = siteSettings.Value.PostsPerPage;
        }

        public PageContext GetContextForPage(int pageNum)
        {
            return new PageContext(_blogContext, _facade, pageNum, _postsPerPage);
        }

        public PageContext GetMetadataOnlyContext()
        {
            return new PageContext(_blogContext);
        }
    }
}