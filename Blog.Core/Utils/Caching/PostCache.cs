using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Core.Models.DAL;
using Blog.Core.Models.Templating.Interfaces;
using Blog.Core.Utils;

namespace Blog.Core.Models.Templating.Processing
{
    public class PostCache
    {
        private readonly ICache<Post> _cache;
        private readonly IPostStore _postStore;
        private readonly IPostsProcessor _postsProcessor;
        
        public List<Post> Posts => _cache.Get();

        public PostCache(ICache<Post> cache, IPostStore postStore, IPostsProcessor postsProcessor)
        {
            _cache = cache;
            _postStore = postStore;
            _postsProcessor = postsProcessor;
            
            CheckAndUpdate();
        }

        public async void CheckAndUpdate()
        {
            var currentPosts = _postStore.Posts;
            var cached = _cache.Get();
            
            var delta = CalculateDelta(cached, currentPosts);

            _cache.Remove(delta.Item1);
            var toadd = await _postsProcessor.ProcessMetadata(delta.Item2);
            _cache.Store(toadd);
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