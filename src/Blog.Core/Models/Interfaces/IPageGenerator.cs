using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Domain.Entities;

namespace Blog.Core.Models.Interfaces
{
    public interface IPageGenerator
    {
        Task<IPageContext> GetContextForPage(int pageNum);
        IPageContext GetMetadataOnlyContext();
        Task<IPageContext> GetFilteredPostPageContext(Func<IEnumerable<Post>, IEnumerable<Post>> filter);
    }
}