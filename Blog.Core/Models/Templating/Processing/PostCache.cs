using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Blog.Core.Models.DAL;
using Blog.Core.Models.Settings;
using Blog.Core.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Blog.Core.Models.Templating.Processing
{
    public class PostCache: IDisposable
    {
        private readonly ICache<Post> _cache;
        private readonly IPostDAO _postDao;

        public List<Post> Posts => _cache.Get();

        public PostCache(ICache<Post> cache, IPostDAO repository)
        {
            _cache = cache;
            _postDao = repository;
            CheckAndUpdate();
        }

        public void CheckAndUpdate()
        {
            var currentPosts = _postDao.Posts;
            var cached = _cache.Get();
            
            var delta = CalculateDelta(cached, currentPosts);

            _cache.Remove(delta.Item1);
            _cache.Store(delta.Item2);
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

        public void Dispose()
        {
            Console.WriteLine("Disposed");
        }
    }
}