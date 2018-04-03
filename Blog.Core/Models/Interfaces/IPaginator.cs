using System.Collections.Generic;

namespace Blog.Core.Models.Interfaces
{
    public interface IPaginator
    {
        int PageNumber { get; }
        int PageCount { get; }

        List<Post> Posts { get; }
    }
}