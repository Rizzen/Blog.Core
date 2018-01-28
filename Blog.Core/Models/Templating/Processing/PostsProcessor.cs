using System.Collections.Generic;
using Blog.Core.Models.DAL;

namespace Blog.Core.Models.Templating.Processing
{
    public class PostsProcessor
    {
        private readonly IPostRepository _postRepository;
        
        public PostsProcessor(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public IEnumerable<Post> ProcessMetadata(IEnumerable<Post> input)
        {
            return new List<Post>();
        }

        public IEnumerable<Post> ProcessTemplate(IEnumerable<Post> input)
        {
            return new List<Post>();
        }
    }
}