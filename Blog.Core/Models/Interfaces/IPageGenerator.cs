using System;
using System.Collections.Generic;

namespace Blog.Core.Models.Interfaces
{
    public interface IPageGenerator
    {
        IPageContext GetContextForPage(int pageNum);
        IPageContext GetMetadataOnlyContext();
        IPageContext GetFilteredPostPageContext(Func<IEnumerable<Post>, IEnumerable<Post>> filter);
    }
}