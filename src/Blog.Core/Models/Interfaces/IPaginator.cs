using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Domain.Entities;

namespace Blog.Core.Models.Interfaces
{
    public interface IPaginator
    {
        int PageNumber { get; }
        int PageCount { get; }

        List<Post> Posts { get; }

        Task<IPaginator> InitializeAsync();
    }
}