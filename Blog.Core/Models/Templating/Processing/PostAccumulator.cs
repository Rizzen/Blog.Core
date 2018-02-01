using System.Collections.Generic;
using Blog.Core.Models.DAL;

namespace Blog.Core.Models.Templating.Processing
{
    public class PostAccumulator
    {
        private readonly IPostRepository _postRepository;
        private readonly PostsProcessor _postsProcessor;
        private readonly BlogContext _blogContext;
        
        public PostAccumulator(IPostRepository postRepository, PostsProcessor postsProcessor, BlogContext blogContext)
        {
            _postRepository = postRepository;
            _postsProcessor = postsProcessor;
            _blogContext = blogContext;
        }

        public IEnumerable<Post> GetPostsMetadata()
        {
            
            return new List<Post>();
        }

        private Post ProcessPostContent(Post post)
        {
            return new Post();
        }
    }
}