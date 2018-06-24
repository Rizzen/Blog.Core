using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Domain.Entities;

namespace Blog.Core.DAL.Posts
{
    public interface IPostStore
    {
        List<Post> Posts { get; }
        Task<string> GetContentByFilename(string name);
    }
}