using System;
using System.Collections.Generic;

namespace Blog.Core.Models.Interfaces
{
    public interface IBlog
    {
        IPageContext GetPostFeed(int page);
        IPageContext GetBlogContent();
        IPageContext GetFilteredPostPage(Func<IEnumerable<Post>, IEnumerable<Post>> filter);
    }
}