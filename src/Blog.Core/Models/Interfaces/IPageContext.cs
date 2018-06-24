using System.Threading.Tasks;
using Blog.Core.Models.Contexts;

namespace Blog.Core.Models.Interfaces
{
    public interface IPageContext
    {
        IPaginator Paginator { get; }
        IBlogContext Blog { get; }
        Task<PageContext> InitializeAsync();
    }
}