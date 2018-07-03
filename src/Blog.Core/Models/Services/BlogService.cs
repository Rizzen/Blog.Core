using System;
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
        
        private int PageCount => _cache.Posts.Count / _siteSettings.PostsPerPage;

        public BlogService(IPostCache cache, IPostStore store, RazorEngine engine, IOptions<SiteSettings> siteSettings)
        {
            _cache = cache;
            _store = store;
            _engine = engine;
            _siteSettings = siteSettings.Value;
        }

        public async Task<IBlogModel> CreateModel(int pageNum)
        {
            var posts = _cache.Posts.OrderByDescending(x => x.DateTime)
                                    .Skip((pageNum - 1) * _siteSettings.PostsPerPage)
                                    .Take(_siteSettings.PostsPerPage)
                                    .ToList();
            
            var metaModel = new BlogModel {Posts = _cache.Posts, Page = new Page(posts, pageNum), PageCount = PageCount};
            
            var postsWithContent = await FillPostsWithContent(posts, metaModel);
            
            var page = new Page(postsWithContent, pageNum);
            
            return new BlogModel {Posts = _cache.Posts, Page = page, PageCount = PageCount};
        }
 
        public async Task<IBlogModel> GetFilteredPostsModel(Func<IEnumerable<Post>, IEnumerable<Post>> filter)
        {
            var posts = filter(_cache.Posts).ToList();
            var metaModel = new BlogModel {Posts = _cache.Posts, Page = new Page(posts, 0), PageCount = PageCount};
            
            var postsWithContent = await FillPostsWithContent(posts, metaModel);
            
            var page = new Page(postsWithContent, 0);
            
            return new BlogModel {Posts = _cache.Posts, Page = page, PageCount = PageCount};
        }

        private async Task<List<Post>> FillPostsWithContent(IEnumerable<Post> posts, BlogModel model)
        {
            return (await Task.WhenAll(posts.Select(x => FillPostWithContent(x, model)))).ToList();
            
            async Task<Post> FillPostWithContent(Post post, BlogModel metaModel)
            {
                var content = (await _store.GetContentByFilename(post.Filename)).ExcludeHeader();
                var result = new Post(post)
                {
                    Content = await _engine.ProcessTemplateAsync(post.Filename, content, metaModel)
                };
                return result;
            }
        }
    }
}