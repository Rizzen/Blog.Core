using System.Linq;
using Blog.Core.Models.DAL;

namespace Blog.Core.Models.Templating
{
    public class PostsProcessor
    {
        private readonly IPostRepository _postRepository;

        public PostsProcessor(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public PostFeedDto GetPostFeed()
        {
            return new PostFeedDto
            {
                Posts = _postRepository.Posts
            };
        }
    }
}