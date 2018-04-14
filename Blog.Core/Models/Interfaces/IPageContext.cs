namespace Blog.Core.Models.Interfaces
{
    public interface IPageContext
    {
        IPaginator Paginator { get; }
        IBlogContext Blog { get; }
    }
}