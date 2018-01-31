using Blog.Core.Models;
using Blog.Core.Models.DAL;
using Blog.Core.Models.Templating;
using Blog.Core.Models.Templating.Processing;

namespace Blog.Core.Utils
{
    public class InitialStateCreator
    {
        private readonly IPostRepository _postRepository;
        private readonly PostsProcessor _postsProcessor;
        private readonly Cache<Post> _cache;
        
        public InitialStateCreator(IPostRepository postRepository, PostsProcessor postsProcessor, Cache<Post> cache)
        {
            _postRepository = postRepository;
            _postsProcessor = postsProcessor;
            _cache = cache;
        }

        public void Init()
        {
            
        }
    }
}