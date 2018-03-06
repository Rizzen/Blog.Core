using System.Collections.Generic;

namespace Blog.Core.Models.Interfaces
{
    public interface IBlogContext
    {
        List<Post> Posts { get; }
        List<string> Tags { get; }
        int PageCount { get; }
    }
}