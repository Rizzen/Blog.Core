using System.Collections.Generic;

namespace Blog.Core.Caching.Caching
{
    public interface ICache<T>
    {
        List<T> Get();
        void Store(IEnumerable<T> toStore);
        void Remove(IEnumerable<T> toRemove);
    }
}