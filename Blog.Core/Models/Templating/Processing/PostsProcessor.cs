using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Post>> ProcessTemplate(IEnumerable<Post> input)
        {
            return await new Task<List<Post>>(() => new List<Post>());
        }
    }
}