using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Core.Caching.Caching
{
    // Some kind of HashSet
    public class ConcurrentCache<T>: ICache<T>
    {
        private readonly ConcurrentDictionary<T, byte> _bag;

        public ConcurrentCache()
        {
            _bag = new ConcurrentDictionary<T, byte>();
        }
        
        public ConcurrentCache(IEnumerable<T> initial)
        {
            _bag = new ConcurrentDictionary<T, byte>();
            if (initial != null)
            {
                foreach (var init in initial)
                {
                    _bag.AddOrUpdate(init, default(byte), (k, v) => default(byte));
                }
            }
        }

        public List<T> Get() => _bag.Select(x => x.Key).ToList();
        
        public void Store(IEnumerable<T> toStore)
        {
            var items = toStore?.ToList();
            
            if (items != null && items.Any())
            {
                foreach (var item in items)
                {
                    _bag.AddOrUpdate(item, default(byte), (k, v) => default(byte));
                }
            }
        }

        public void Remove(IEnumerable<T> toRemove)
        {
            var items = toRemove?.ToList();
            
            if (items != null && items.Any())
            {
                foreach (var item in items)
                {
                    _bag.TryRemove(item, out _);
                }
            }
        }
    }
}