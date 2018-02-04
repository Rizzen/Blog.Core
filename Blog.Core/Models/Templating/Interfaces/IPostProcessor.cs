using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Models.Interfaces;

namespace Blog.Core.Models.Templating.Interfaces
{
    public interface IPostsProcessor
    {
        List<Post> ProcessMetadata(IEnumerable<Post> input);
        Post ProcessMetadata(Post input);
        Task<List<Post>> ProcessTemplatesAsync(IEnumerable<Post> input, IPageContext pageContext);
        Task<Post> ProcessTemplateAsync(Post input, IPageContext pageContext);
    }
}