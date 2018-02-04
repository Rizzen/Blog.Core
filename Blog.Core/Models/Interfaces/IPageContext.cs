using Blog.Core.Models.Contexts;
using Blog.Core.Models.Pagination;

namespace Blog.Core.Models.Interfaces
{
    public interface IPageContext
    {
        Paginator Paginator { get; }
        IBlogContext Blog { get; }
    }
}