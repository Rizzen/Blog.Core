using System.Collections.Generic;
using System.Linq;

namespace Blog.Core.Models.Templating
{
    public class Cache<T>
    {
        private IEnumerable<T> _cache;
        
        public Cache(IEnumerable<T> initial)
        {
            _cache = initial;
        }

        public IEnumerable<T> Get() => _cache;

        public void Store(IEnumerable<T> toStore)
        {
            _cache = _cache.Concat(toStore)
                           .Distinct();
        }

        public void Delete(IEnumerable<T> toDelete)
        {
            _cache = _cache.Except(toDelete);
        }
    }
}