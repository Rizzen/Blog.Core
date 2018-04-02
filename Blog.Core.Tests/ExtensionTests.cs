using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Core.Extensions;
using NUnit.Framework;

namespace Blog.Core.Tests
{
    [TestFixture]
    public class ExtensionTests
    {
        [Test]
        public void Check_ForEach_Extension()
        {
            // Arrange
            var array = Enumerable.Range(0, 100).Select(x => new IntWrapper(x))
                                                .ToArray();
            // Act
            array.ForEach(x => x.Value++);
            
            // Assert
            var checkArray = Enumerable.Range(1, 100).Select(x => new IntWrapper(x))
                                                     .ToArray();
            Assert.AreEqual(array, checkArray);
        }

        private class IntWrapper
        {
            public int Value { get; set; }

            public IntWrapper(int value)
            {
                Value = value;
            }

            public override bool Equals(object obj)
            {
                if (obj is IntWrapper iw)
                    return iw.Value == Value;

                return false;
            }
        }

    }
}