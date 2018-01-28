using System.Collections.Generic;
using System.Linq;

namespace Blog.Core.Models.Templating
{
    public class PostCache
    {
        private IEnumerable<Post> _posts;
        
        public PostCache(IEnumerable<Post> initial)
        {
            _posts = initial;
        }

        public IEnumerable<Post> Get() => _posts;

        public void Store(IEnumerable<Post> toStore)
        {
            _posts = _posts.Concat(toStore)
                           .Distinct();
        }

        public void Delete(IEnumerable<Post> toDelete)
        {
            _posts = _posts.Except(toDelete);
        }
    }
}