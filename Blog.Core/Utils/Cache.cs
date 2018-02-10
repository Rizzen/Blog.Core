using System.Collections.Generic;
using System.Linq;

namespace Blog.Core.Utils
{
    public class Cache<T>
    {
        private List<T> _cache;
        
        public Cache(IEnumerable<T> initial)
        {
            if (initial != null)
            {
                _cache = initial.ToList();
            }
        }

        public List<T> Get() => _cache;

        public void Store(IEnumerable<T> toStore)
        {
            if (toStore != null)
            {
                _cache = _cache.Concat(toStore)
                    .Distinct()
                    .ToList();
            }
        }

        public void Delete(IEnumerable<T> toDelete)
        {
            if (toDelete != null)
            {
                _cache = _cache.Except(toDelete)
                    .ToList();
            }
        }

        public void Clear()
        {
            _cache = new List<T>(0);
        }
    }
}