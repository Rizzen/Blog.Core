using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Core.Domain.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> instance, Action<T> action) where T: class
        {
            foreach (var item in instance)
            {
                action(item);
            }
        }
        
        public static async Task ForEachAsync<T>(this IEnumerable<T> list, Func<T, Task> func)
        {
            foreach (var value in list)
            {
                await func(value);
            }
        }
    }
}
