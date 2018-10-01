using System.Collections.Generic;
using Blog.Core.Caching.Caching;
using Blog.Core.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Blog.Core.Tests
{
    [TestFixture]
    public class CachingTests
    {
        [Test]
        public void CacheTest_StoreOnePostIntoEmptyCache()
        {
            // Arrange
            var cache = new PostCache(new ConcurrentCache<Post>());
            var post = new Post {Filename = "MyPost"};

            // Act
            cache.Store(new List<Post> {post});

            // Assert
            cache.Posts.Count.Should().Be(1);
        }

        [Test]
        public void CacheTest_StoreTwoPostsIntoEmptyCache()
        {
            // Arrange
            var cache = new PostCache(new ConcurrentCache<Post>());
            var posts = new List<Post>
            {
                new Post {Filename = "MyPost"},
                new Post {Filename = "MySecondPost"}
            };

            // Act
            cache.Store(posts);

            // Assert
            cache.Posts.Count.Should().Be(2);
        }

        [Test]
        public void CacheTest_StoreOnePostIntoNotEmptyCache()
        {
            // Arrange
            var storedPosts = new List<Post>
            {
                new Post {Filename = "MyPost"},
                new Post {Filename = "MySecondPost"},
                new Post {Filename = "MyAnotherOnePost"}
            };
            var cache = new PostCache(new ConcurrentCache<Post>(storedPosts));
            var post = new Post {Filename = "MyAwesomePost"};

            // Act
            cache.Store(new List<Post> {post});

            // Assert
            cache.Posts.Count.Should().Be(4);
        }

        [Test]
        public void CacheTest_StoreTwoPostsIntoNotEmptyCache()
        {
            // Arrange
            var storedPosts = new List<Post>
            {
                new Post {Filename = "MyPost"},
                new Post {Filename = "MySecondPost"},
                new Post {Filename = "MyAnotherOnePost"}
            };
            var cache = new PostCache(new ConcurrentCache<Post>(storedPosts));
            var posts = new List<Post>
            {
                new Post {Filename = "MyAwesomePost"},
                new Post {Filename = "MySecondAwesomePost"}
            };

            // Act
            cache.Store(posts);

            // Assert
            cache.Posts.Count.Should().Be(5);
        }

        [Test]
        public void CacheTest_StoreOnePostWithCollisionsInCache()
        {
            // Arrange
            var storedPosts = new List<Post>
            {
                new Post {Filename = "MyPost"},
                new Post {Filename = "MySecondPost"},
                new Post {Filename = "MyAnotherOnePost"}
            };
            var cache = new PostCache(new ConcurrentCache<Post>(storedPosts));
            var posts = new List<Post>
            {
                new Post {Filename = "MyPost"}
            };

            // Act
            cache.Store(posts);

            // Assert
            cache.Posts.Count.Should().Be(3);
        }

        [Test]
        public void CacheTest_StoreTwoPostsWithCollisionsInCache()
        {
            // Arrange
            var storedPosts = new List<Post>
            {
                new Post {Filename = "MyPost"},
                new Post {Filename = "MySecondPost"},
                new Post {Filename = "MyAnotherOnePost"}
            };
            var cache = new PostCache(new ConcurrentCache<Post>(storedPosts));
            var posts = new List<Post>
            {
                new Post {Filename = "MyPost"},
                new Post {Filename = "MySecondPost"},
            };

            // Act
            cache.Store(posts);

            // Assert
            cache.Posts.Count.Should().Be(3);
        }

        [Test]
        public void CacheTest_StoreThreePostsWithCollisionsInCache()
        {
            // Arrange
            var storedPosts = new List<Post>
            {
                new Post {Filename = "MyPost"},
                new Post {Filename = "MySecondPost"},
                new Post {Filename = "MyAnotherOnePost"}
            };
            var cache = new PostCache(new ConcurrentCache<Post>(storedPosts));
            var posts = new List<Post>
            {
                new Post {Filename = "MyPost"},
                new Post {Filename = "AwesomePost"},
            };

            // Act
            cache.Store(posts);

            // Assert
            cache.Posts.Count.Should().Be(4);
        }
    }
}