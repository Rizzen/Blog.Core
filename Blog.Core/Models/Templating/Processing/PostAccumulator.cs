using System;
using System.Linq;
using Blog.Core.Models.DAL;

namespace Blog.Core.Models.Templating.Processing
{
    public class PostAccumulator
    {
        private readonly IPostRepository _postRepository;

        private readonly PostsProcessor _postsProcessor;
        
        public PostAccumulator(IPostRepository postRepository, PostsProcessor postsProcessor)
        {
            _postRepository = postRepository;
            _postsProcessor = postsProcessor;
        }

    }
}