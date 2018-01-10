using System.Collections.Generic;
using Blog.Core.Utils;

namespace Blog.Core.Models
{
    public class Post
    {
        public string Title { get; set; } 
        
        public string Author { get; set; }  
        
        public string View { get; set; }   
        
        public List<string> Tags { get; set; }
        
        public PostDateTime PostDateTime { get; set; }
    }
}