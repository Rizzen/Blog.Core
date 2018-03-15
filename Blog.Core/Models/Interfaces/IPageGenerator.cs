using Blog.Core.Models.Contexts;

namespace Blog.Core.Models.Interfaces
{
    public interface IPageGenerator
    {
        PageContext GetContextForPage(int pageNum);
        PageContext GetMetadataOnlyContext();
    }
}