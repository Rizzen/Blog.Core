using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Core.Models.Pagination
{
    public interface IPaginator
    {
        int PageNumber { get; }
        
        int PageCount { get; }

        List<Post> Posts { get; }
    }
}