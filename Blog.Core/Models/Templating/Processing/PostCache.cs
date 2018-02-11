using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Core.Models.DAL;
using Blog.Core.Utils;

namespace Blog.Core.Models.Templating.Processing
{
    public class PostCache
    {
        private readonly Cache<Post> _cache;
        private readonly IPostRepository _postRepository;

        public List<Post> Posts => _cache.Get();

        public PostCache(Cache<Post> cache, IPostRepository repository)
        {
            _cache = cache;
            _postRepository = repository;
        }

        public void CheckAndUpdate()
        {
            var currentPosts = _postRepository.Posts;
            var cached = _cache.Get();
            
            var delta = CalculateDelta(cached, currentPosts);
            
            _cache.Delete(delta.Item1);
            _cache.Store(delta.Item2);
        }

        public (List<Post>, List<Post>) CalculateDelta(List<Post> cachedPosts, List<Post> repositoryPosts)
        {
            if (cachedPosts == null || repositoryPosts == null)
            {
                return (new List<Post>(), new List<Post>());
            }
            
            var toRemove = cachedPosts.Except(repositoryPosts).Any()
                           ? cachedPosts.Except(repositoryPosts).ToList()
                           : null;

            var toAdd = repositoryPosts.Except(cachedPosts).Any()
                        ? repositoryPosts.Except(cachedPosts).ToList()
                        : null;
            
            return (toRemove, toAdd);
        }
    }
}