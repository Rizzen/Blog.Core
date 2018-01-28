using Blog.Core.Models.DAL;
using Blog.Core.Models.Templating;
using Blog.Core.Models.Templating.Processing;

namespace Blog.Core.Utils
{
    public class InitialStateCreator
    {
        private readonly IPostRepository _postRepository;
        private readonly PostsProcessor _postsProcessor;
        
        public InitialStateCreator(IPostRepository postRepository, PostsProcessor postsProcessor)
        {
            _postRepository = postRepository;
            _postsProcessor = postsProcessor;
        }

        public void Init()
        {
            
        }
    }
}