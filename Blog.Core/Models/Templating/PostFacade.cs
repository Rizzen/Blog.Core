using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Models.Contexts;
using Blog.Core.Models.DAL;
using Blog.Core.Models.Templating.Processing;
using Blog.Core.Utils;

namespace Blog.Core.Models.Templating
{
    public class PostFacade
    {
        private readonly IPostRepository _postRepository;
        private readonly PostsProcessor _postsProcessor;
        private readonly Cache<Post> _cache;
        
        public PostFacade(IPostRepository postRepository, PostsProcessor postsProcessor, Cache<Post> cache)
        {
            _postRepository = postRepository;
            _postsProcessor = postsProcessor;
            _cache = cache;
        }

        //TODO PageContext => IPageContext
        public async Task<List<Post>> GenRenderedPosts(IEnumerable<Post> input, PageContext model)
        {
            var toProcess = GetPostContent(input.ToList());
            return await _postsProcessor.ProcessTemplatesAsync(toProcess, model);
        }

        private List<Post> GetPostContent(IList<Post> input)
        {
            foreach (var post in input)
            {
                post.Content = _postRepository.GetContentByFilename(post.Filename);
            }

            return input.ToList();
        }
    }
}