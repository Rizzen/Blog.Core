using System.Collections.Generic;
using System.Linq;

namespace Blog.Core.Models.DAL
{
    public interface IPostRepository
    {
        IEnumerable<Post> Posts { get; }
        string GetContentByFilename(string name);
    }
}