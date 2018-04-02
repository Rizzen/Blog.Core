using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Extensions;
using Blog.Core.Models.DAL;
using Blog.Core.Models.Interfaces;
using Blog.Core.Models.Templating.Interfaces;
using Blog.Core.Models.Templating.Processing;

namespace Blog.Core.Models.Templating
{
    public class PostFacade: IPostFacade
    {
        private readonly IPostStore _postStore;
        private readonly IPostsProcessor _postsProcessor;
        private readonly PostCache _cache;
        
        public PostFacade(IPostStore postStore, IPostsProcessor postsProcessor, PostCache cache)
        {
            _postStore = postStore;
            _postsProcessor = postsProcessor;
            _cache = cache;
        }
        
        public List<Post> GetAllPostsMetadataOnly() => _cache.Posts;
        
        public async Task<List<Post>> GenRenderedPosts(IList<Post> input, IPageContext model)
        {
            await input.ForEachAsync(x => GetRenderedPost(x, model));
            return input.ToList();
        }

        private async Task<Post> GetRenderedPost(Post input, IPageContext model)
        {
            var toProcess = await GetPostContent(input);
            return await _postsProcessor.ProcessTemplateAsync(toProcess, model);
        }

        private async Task<Post> GetPostContent(Post input)
        {
            input.Content = (await _postStore.GetContentByFilename(input.Filename)).ExcludeHeader();
            return input;
        }
    }
}