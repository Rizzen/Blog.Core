using Blog.Core.Models.Pagination;

namespace Blog.Core.Models.Interfaces
{
    public interface IPageContext
    {
        IPaginator Paginator { get; }
        IBlogContext Blog { get; }
    }
}