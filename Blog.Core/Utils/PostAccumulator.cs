using System.Collections.Generic;
using Blog.Core.Models;

namespace Blog.Core.Utils
{
    /// <summary>Finds all posts, presented in Views/_posts</summary>
    public class PostAccumulator
    {
        public List<Post> Posts { get; }
    }
}