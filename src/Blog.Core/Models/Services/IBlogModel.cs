using System.Collections.Generic;
using Blog.Core.Domain.Entities;

namespace Blog.Core.Models.Services
{
    public interface IBlogModel
    {
        List<Post> Posts { get; set; }
        
        Page Page { get; set; }
        
        int PageCount { get; set; }

        List<Tag> Tags { get; }
    }
}