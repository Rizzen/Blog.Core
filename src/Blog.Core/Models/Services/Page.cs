using System.Collections.Generic;
using System.Linq;
using Blog.Core.Domain.Entities;

namespace Blog.Core.Models.Services
{
    /// <summary>Implies page with renderred posts</summary>
    public class Page
    {
        public List<Post> Posts { get; }
        public int PageNum { get; }

        public Page(IEnumerable<Post> posts, int pageNum)
        {
            Posts = posts.ToList();
            PageNum = pageNum;
        }
    }
}