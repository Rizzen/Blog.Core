using System.Linq;

namespace Blog.Core.Models.DAL
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts { get; }
    }
}