using System;
using System.Collections.Generic;

namespace Blog.Core.Models
{
    /// <summary>Represents the Blog Post</summary>
    public class Post
    {
        /// <summary>Title</summary>
        /// <remarks>Metadata</remarks>
        public string Title { get; set; } 
        
        /// <summary>Author</summary>
        /// <remarks>Metadata</remarks>
        public string Author { get; set; }  
        
        /// <summary>Tags</summary>
        /// <remarks>Metadata</remarks>
        public List<string> Tags { get; set; }
        
        /// <summary>Publishing time</summary>
        /// <remarks>Metadata</remarks>
        public DateTime DateTime { get; set; }
        
        /// <summary>Content</summary>
        public string Content { get; set; } 
        
        /// <summary>Path to cshtml file</summary>
        /// <remarks>Like "~/Views/_posts/example.cshtml"</remarks>
        public string Filename { get; set; }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Post p)
                return string.Equals(p.Filename, Filename);
            
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Filename.GetHashCode();
        }
    }
}