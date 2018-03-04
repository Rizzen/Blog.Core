using Blog.Core.Models.Contexts;

namespace Blog.Core.Models.Interfaces
{
    public interface IBlog
    {
        PageContext GetPostFeed(int page);
        PageContext GetBlogContent();
    }
}