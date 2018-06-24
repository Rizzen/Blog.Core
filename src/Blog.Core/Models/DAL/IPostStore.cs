using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Core.Models.DAL
{
    public interface IPostStore
    {
        List<Post> Posts { get; }
        Task<string> GetContentByFilename(string name);
    }
}