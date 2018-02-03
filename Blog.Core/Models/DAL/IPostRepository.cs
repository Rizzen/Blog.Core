using System.Collections.Generic;
using System.Linq;

namespace Blog.Core.Models.DAL
{
    public interface IPostRepository
    {
        IList<Post> Posts { get; }
        string GetContentByFilename(string name);
        IList<string> GetContentByFilename(IEnumerable<string> names);
    }
}