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
        
        public async Task<List<Post>> GenRenderedPosts(IEnumerable<Post> input, IPageContext model)
        {
            var toProcess = await GetPostContent(input.ToList());
            
            return await _postsProcessor.ProcessTemplatesAsync(toProcess, model);
        }
        
        public List<Post> GetAllPostsMetadataOnly() => _cache.Posts;

        
        private async Task<List<Post>> GetPostContent(IList<Post> input)
        {
            foreach (var post in input)
            {
                post.Content = (await _postStore.GetContentByFilename(post.Filename)).ExcludeHeader();
            }

            return input.ToList();
        }
    }
}