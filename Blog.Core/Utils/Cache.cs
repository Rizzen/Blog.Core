using System.Collections.Generic;
using System.Linq;

namespace Blog.Core.Utils
{
    public class Cache<T>
    {
        private List<T> _cache;
        
        public Cache(IEnumerable<T> initial)
        {
            _cache = initial.ToList();
        }

        public List<T> Get() => _cache;

        public void Store(IEnumerable<T> toStore)
        {
            _cache = _cache.Concat(toStore)
                           .Distinct()
                           .ToList();
        }

        public void Delete(IEnumerable<T> toDelete)
        {
            _cache = _cache.Except(toDelete)
                           .ToList();
        }
    }
}