using System.Linq;

namespace Blog.Core.Models.DAL
{
    public interface IPostRepository
    {
        IQueryable<string> Posts { get; }
        
        string Path { get; }
    }
}