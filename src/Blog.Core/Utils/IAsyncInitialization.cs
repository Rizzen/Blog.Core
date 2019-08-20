using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Utils
{
    public interface IAsyncInitialization
    {
        Task Initialization { get; }
    }
}
