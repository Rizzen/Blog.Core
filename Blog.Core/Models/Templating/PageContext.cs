using System.Collections.Generic;
using System.Linq;

namespace Blog.Core.Models.Templating
{
    public class PageContext
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}