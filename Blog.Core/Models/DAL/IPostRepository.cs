using System.Collections.Generic;
using System.Linq;

namespace Blog.Core.Models.DAL
{
    public interface IPostRepository
    {
        List<Post> Posts { get; }
        string GetContentByFilename(string name);
        List<string> GetContentByFilename(IEnumerable<string> names);
    }
}