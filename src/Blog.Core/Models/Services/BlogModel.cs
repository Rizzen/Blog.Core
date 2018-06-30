using System.Collections.Generic;
using System.Linq;
using Blog.Core.Domain.Entities;

namespace Blog.Core.Models.Services
{
    public class BlogModel
    {
        public List<Post> Posts { get; set; }
        
        public Page Page { get; set; }
        
        public int PageCount { get; set; }
        
        public List<Tag> Tags => Posts.SelectMany(x => x.Tags)
                                      .Distinct()
                                      .ToList();
    }
}