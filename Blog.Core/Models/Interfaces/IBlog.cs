using Blog.Core.Models.Contexts;

namespace Blog.Core.Models.Interfaces
{
    public interface IBlog
    {
        IPageContext GetPostFeed(int page);
        IPageContext GetBlogContent();
        IPageContext GetSinglePostPage(string postName);
    }
}