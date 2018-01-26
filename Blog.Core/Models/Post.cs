using System;
using System.Collections.Generic;
using Blog.Core.Utils;

namespace Blog.Core.Models
{
    public class Post
    {
        public string Title { get; set; } 
        
        public string Author { get; set; }  
        
        public string Contents { get; set; } 
        
        public string Filename { get; set; }
        
        public IEnumerable<string> Tags { get; set; }
        
        public DateTime DateTime { get; set; }
    }
}