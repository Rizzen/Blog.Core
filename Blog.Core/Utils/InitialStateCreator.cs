using Blog.Core.Models;
using Blog.Core.Models.DAL;
using Blog.Core.Models.Templating.Interfaces;

namespace Blog.Core.Utils
{
    public class InitialStateCreator
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostsProcessor _postsProcessor;
        private readonly Cache<Post> _cache;
        
        public InitialStateCreator(IPostRepository postRepository, IPostsProcessor postsProcessor, Cache<Post> cache)
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