using System.Collections.Generic;
using Blog.Core.Models.Templating.Processing;

namespace Blog.Core.Models.Interfaces
{
    public interface IBlogContext
    {
        List<Post> Posts { get; }
        List<Tag> Tags { get; }
        int PageCount { get; }
    }
}