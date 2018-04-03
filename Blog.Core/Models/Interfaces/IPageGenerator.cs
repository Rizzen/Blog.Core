using Blog.Core.Models.Contexts;

namespace Blog.Core.Models.Interfaces
{
    public interface IPageGenerator
    {
        IPageContext GetContextForPage(int pageNum);
        IPageContext GetSinglePostPageContext(string postName);
        IPageContext GetMetadataOnlyContext();
    }
}