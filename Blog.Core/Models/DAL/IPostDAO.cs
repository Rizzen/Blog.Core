using System.Collections.Generic;

namespace Blog.Core.Models.DAL
{
    public interface IPostDAO
    {
        List<Post> Posts { get; }
        string GetContentByFilename(string name);
        List<string> GetContentByFilename(IEnumerable<string> names);
    }
}