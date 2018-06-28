using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Caching.Caching;
using Blog.Core.DAL.Posts;
using Blog.Core.Domain.Entities;
using Blog.Core.Domain.Extensions;
using Blog.Core.Domain.Settings;
using Blog.Core.Razor.Razor;
using Microsoft.Extensions.Options;

namespace Blog.Core.Models.Services
{
    public class BlogService: IBlogService
    {
        private readonly IPostCache _cache;
        private readonly IPostStore _store;
        private readonly RazorEngine _engine;
        private readonly SiteSettings _siteSettings;

        public BlogService(IPostCache cache, IPostStore store, RazorEngine engine, IOptions<SiteSettings> siteSettings)
        {
            _cache = cache;
            _store = store;
            _engine = engine;
            _siteSettings = siteSettings.Value;
        }

        /// <summary>Получает посты из кеша, рендерит нужные, отдает страницу</summary>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public async Task<BlogModel> CreateModel(int pageNum)
        {
            var posts = _cache.Posts.OrderByDescending(x => x.DateTime)
                                    .Skip((pageNum - 1) * _siteSettings.PostsPerPage)
                                    .Take(_siteSettings.PostsPerPage)
                                    .ToList();
            var pageCount = (_cache.Posts.Count / _siteSettings.PostsPerPage);
            
            var metaModel = new BlogModel {Posts = _cache.Posts, Page = new Page(posts, pageNum), PageCount = pageCount};
            
            var postsWithContent = await FillPostsWithContent(posts, metaModel);
            
            var page = new Page(postsWithContent, pageNum);
            
            var model = new BlogModel {Posts = _cache.Posts, Page = page, PageCount = pageCount};

            return model;
        }

        private async Task<List<Post>> FillPostsWithContent(IEnumerable<Post> posts, BlogModel model)
        {
            return (await Task.WhenAll(posts.Select(x=> FillPostWithContent(x, model)))).ToList();
            
            async Task<Post> FillPostWithContent(Post post, BlogModel metaModel)
            {
                var content = (await _store.GetContentByFilename(post.Filename)).ExcludeHeader();
                post.Content = await _engine.ProcessTemplateAsync(post.Filename, content, model);
                return post;
            }
        }
    }
}