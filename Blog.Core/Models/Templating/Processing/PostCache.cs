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

        public (List<Post>, List<Post>) CalculateDelta(List<Post> cache, List<Post> repository)
        {
            var toRemove = cache.Except(repository).Any()
                           ? cache.Except(repository).ToList()
                           : null;

            var toAdd = repository.Except(cache).Any()
                        ? repository.Except(cache).ToList()
                        : null;
            
            return (toRemove, toAdd);
        }
    }
}