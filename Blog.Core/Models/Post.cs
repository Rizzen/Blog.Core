using System;
using System.Collections.Generic;

namespace Blog.Core.Models
{
    public class Post
    {
        public string Title { get; set; } 
        
        public string Author { get; set; }  
        
        public string Contents { get; set; } 
        
        public string Filename { get; set; }
        
        public string Path { get; set; }
        
        public IEnumerable<string> Tags { get; set; }
        
        public DateTime DateTime { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Post p)
                return p.Filename == Filename;
            
            return base.Equals(obj);
        }
    }
}