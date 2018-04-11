using System;
using Blog.Core.Extensions;
using NUnit.Framework;

namespace Blog.Core.Tests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void To_Underscore_Case_Test()
        {
            // Arrange
            const string testString = "I <3 c# и платформу .NET";
            const string expected = "i3cиплатформу._n_e_t";
            
            // Act
            var result = testString.ToUnderscoreCase()
                                   .RemoveSpecialCharacters();
            
            // Assert
            Assert.AreEqual(result, expected);
        }
    }
}