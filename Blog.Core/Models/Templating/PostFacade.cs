using System.Collections.Generic;
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


        public IList<Post> GenRenderedPosts(IEnumerable<Post> input, PageContext model)
        {
            return new List<Post>();
        }
    }
}