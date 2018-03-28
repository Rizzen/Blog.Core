using System.Collections.Generic;
namespace Blog.Core.Models.Pagination
{
    public class SinglePagePaginator: IPaginator
    {
        public int PageNumber { get; }
        public int PageCount { get; }
        public List<Post> Posts { get; }

        public SinglePagePaginator(int pageNumber, int pageCount)
        {
            PageNumber = pageNumber;
            PageCount = pageCount;
        }
    }
}