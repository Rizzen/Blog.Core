using System.Collections.Generic;
using Blog.Core.Domain.Entities;

namespace Blog.Core.Caching.Caching
{
    public interface IPostCache
    {
        List<Post> Posts { get; }
        void Store(IEnumerable<Post> currentPosts);
        void Remove(IEnumerable<Post> currentPosts);
    }
}