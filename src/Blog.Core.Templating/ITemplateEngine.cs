using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Domain.Entities;

namespace Blog.Core.Templating
{
    public interface ITemplateEngine
    {
        Task<Post> ProcessTemplateAsync(IEnumerable<Post> input, object pageContext);
    }
}