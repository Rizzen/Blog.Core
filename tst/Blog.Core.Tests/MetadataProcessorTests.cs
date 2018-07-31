using System.Linq;
using System.Threading.Tasks;
using Blog.Core.DAL.Posts;
using Blog.Core.Domain.Entities;
using Blog.Core.Metadata;
using Moq;
using NUnit.Framework;

namespace Blog.Core.Tests
{
    public class MetadataProcessorTests
    {
        [Test]
        public async Task Processing_Test()
        {
            // Arrange
            var postText = @"---
tags:
- Tag1
- Tag2
title: SimpleTitle
---
<p>Post</p>";
            var store = new Mock<IPostStore>();
            store.Setup(x => x.GetContentByFilename(It.IsAny<string>()))
                .Returns((string val) => Task.FromResult(postText));

            var metadataProcessor = new MetadataProcessor(store.Object);
            var post = new Post {Filename = "MyPost"};
            
            // Act
            var postWithMeta = (await metadataProcessor.GetMetadataForPosts(new [] {post})).First();
            
            // Assert
            Assert.AreEqual(postWithMeta.Tags.Count, 2);
            Assert.AreEqual(postWithMeta.Title, "SimpleTitle");
        }
    }
}