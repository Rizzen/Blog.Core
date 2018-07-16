using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Domain.Entities;

namespace Blog.Core.Templating
{
    public class TemplateEngine : ITemplateEngine
    {
        public Task<Post> ProcessTemplateAsync(IEnumerable<Post> input, object pageContext)
        {
            throw new System.NotImplementedException();
        }
    }
}