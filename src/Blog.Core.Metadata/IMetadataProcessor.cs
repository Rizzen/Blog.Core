using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Domain.Entities;

namespace Blog.Core.Metadata
{
    public interface IMetadataProcessor
    {
        Task<List<Post>> GetMetadataForPosts(IEnumerable<Post> posts);
    }
}