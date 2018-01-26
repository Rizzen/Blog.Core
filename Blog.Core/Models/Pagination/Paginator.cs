using System.Collections.Generic;

namespace Blog.Core.Models.Pagination
{
    public class Paginator
    {
        public IEnumerable<Post> GetPostsForPage(int pageNumber)
        {
            return new List<Post>();
        }
    }
}