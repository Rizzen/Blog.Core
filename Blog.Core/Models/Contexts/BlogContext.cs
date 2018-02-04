using System.Collections.Generic;
using Blog.Core.Models.DAL;

namespace Blog.Core.Models.Contexts
{
    public class BlogContext
    {
        //BUG Harcoded for now
        //TODO Move to site setting
        public int PostsPerPage = 10;
        
        private readonly IPostRepository _postRepository;
        public List<Post> Posts => _postRepository.Posts;
        
        public BlogContext(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
    }
}