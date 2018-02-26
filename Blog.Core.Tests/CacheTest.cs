using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Blog.Core.Models;
using Blog.Core.Models.DAL;
using Blog.Core.Models.Settings;
using Blog.Core.Models.Templating.Interfaces;
using Blog.Core.Models.Templating.Processing;
using Blog.Core.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Moq;
using NUnit.Framework;

namespace Blog.Core.Tests
{
    [TestFixture]
    public class CacheTest
    {
        [Test]
        public void CalcDeltaTest()
        {
            //Arrange
            var postsInFilesystem = new List<Post>()
            {
                new Post {Filename = "first"},
                new Post {Filename = "second"},
                new Post {Filename = "third"},
                new Post {Filename = "forth"},
            };
            
            var cachedPosts = new List<Post>()
            {
                new Post {Filename = "zero"},
                new Post {Filename = "second"},
                new Post {Filename = "third"},
            };
            
            var dao = new Mock<IPostStore>();
            dao.Setup(x => x.Posts).Returns(postsInFilesystem);
                
            var processor = new Mock<IPostsProcessor>();
            processor.Setup(x => x.ProcessMetadata(It.IsAny<IEnumerable<Post>>())).Returns((List<Post> val) => val);
            
            var cache = new ConcurrentCache<Post>(cachedPosts);

            var postCache = new PostCache(cache, dao.Object, processor.Object);
            
            //Act
            postCache.CheckAndUpdate();
            
            postCache.Posts.ForEach(x => Console.WriteLine(x.Filename));
            
            //Assert
            var expectingResult = new List<Post>()
            {
                new Post {Filename = "second"},
                new Post {Filename = "third"},
                new Post {Filename = "first"},
                new Post {Filename = "forth"},
            };

            foreach (var post in expectingResult)
            {
                Assert.Contains(post, postCache.Posts);
            }
        }
        
        
        [Test]
        public void CalcDeltaTest_WithoutDelta()
        {
            //Arrange
            var postsInFilesystem = new List<Post>()
            {
                new Post {Filename = "first"},
                new Post {Filename = "second"},
                new Post {Filename = "third"},
                new Post {Filename = "forth"},
            };
            
            var cachedPosts = new List<Post>()
            {
                new Post {Filename = "first"},
                new Post {Filename = "second"},
                new Post {Filename = "third"},
                new Post {Filename = "forth"},
            };
            
            var repository = new Mock<IPostStore>();
            repository.Setup(x => x.Posts).Returns(postsInFilesystem);
            
            var processor = new Mock<IPostsProcessor>();
            
            var cache = new ConcurrentCache<Post>(cachedPosts);
            var postCache = new PostCache(cache, repository.Object, processor.Object);
            
            //Act
            postCache.CheckAndUpdate();
            
            postCache.Posts.ForEach(x => Console.WriteLine(x.Filename));
            
            //Assert
            var expectingResult = new List<Post>()
            {
                new Post {Filename = "first"},
                new Post {Filename = "second"},
                new Post {Filename = "third"},
                new Post {Filename = "forth"},
            };
            
            foreach (var post in expectingResult)
            {
                Assert.Contains(post, postCache.Posts);
            }
        }
        
        [Test]
        public void CalcDeltaTest_WhenNoData()
        {
            //Arrange
            var postsInFilesystem = new List<Post>();

            var cachedPosts = new List<Post>();
            
            var repository = new Mock<IPostStore>();
            repository.Setup(x => x.Posts).Returns(postsInFilesystem);
            
            var processor = new Mock<IPostsProcessor>();
            
            var cache = new ConcurrentCache<Post>(cachedPosts);
            var postCache = new PostCache(cache, repository.Object, processor.Object);
            
            //Act
            postCache.CheckAndUpdate();
            
            postCache.Posts.ForEach(x => Console.WriteLine(x.Filename));
            
            //Assert
            Assert.AreEqual(postCache.Posts.Count, 0);
        }
        
        [Test]
        public void CalcDeltaTest_WhenNull()
        {
            //Arrange
            var postsInFilesystem = (List<Post>) null;

            var cachedPosts = new List<Post>()
            {
                new Post {Filename = "first"},
                new Post {Filename = "second"},
                new Post {Filename = "third"},
                new Post {Filename = "forth"},
            };
            
            var repository = new Mock<IPostStore>();
            repository.Setup(x => x.Posts).Returns(postsInFilesystem);
            
            var processor = new Mock<IPostsProcessor>();
            
            var cache = new ConcurrentCache<Post>(cachedPosts);
            var postCache = new PostCache(cache, repository.Object, processor.Object);
            
            //Act
            postCache.CheckAndUpdate();
            
            postCache.Posts.ForEach(x => Console.WriteLine(x.Filename));
            
            //Assert
            Assert.AreEqual(postCache.Posts.Count, cachedPosts.Count);
        }
    }
}