using System.Collections.Generic;
using System.Linq;
using Blog.Core.Domain.Entities;

namespace Blog.Core.Caching.Caching
{
    public class PostCache : IPostCache
    {
        private readonly ICache<Post> _cache;
        
        public List<Post> Posts => _cache.Get();

        public PostCache(ICache<Post> cache)
        {
            _cache = cache;
            
        }

        public void Store(IEnumerable<Post> currentPosts)
        {
            var cached = _cache.Get();
            var delta = CalculateDelta(cached, currentPosts.ToList());
            _cache.Store(delta.Item2);
        }

        public void Remove(IEnumerable<Post> currentPosts)
        {
            var cached = _cache.Get();
            var delta = CalculateDelta(cached, currentPosts.ToList());
            _cache.Remove(delta.Item1);
        }

        private (List<Post>, List<Post>) CalculateDelta(IReadOnlyCollection<Post> cachedPosts,
                                                        IReadOnlyCollection<Post> repositoryPosts)
        {
            if (cachedPosts == null || repositoryPosts == null)
            {
                return (new List<Post>(), new List<Post>());
            }
            
            var toRemove = cachedPosts.Except(repositoryPosts).Any()
                           ? cachedPosts.Except(repositoryPosts).ToList()
                           : new List<Post>();

            var toAdd = repositoryPosts.Except(cachedPosts).Any()
                        ? repositoryPosts.Except(cachedPosts).ToList()
                        : new List<Post>();
            
            return (toRemove, toAdd);
        }
    }
}