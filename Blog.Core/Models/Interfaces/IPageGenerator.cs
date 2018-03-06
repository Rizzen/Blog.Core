using Blog.Core.Models.Contexts;

namespace Blog.Core.Models.Interfaces
{
    public interface IPageGenerator
    {
        int PageCount { get; }
        PageContext GetContextForPage(int pageNum);
        PageContext GetMetadataOnlyContext();
    }
}