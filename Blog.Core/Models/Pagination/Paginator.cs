using System.Collections.Generic;
using System.Linq;
using Blog.Core.Models.DAL;
using Blog.Core.Models.Templating;

namespace Blog.Core.Models.Pagination
{
    public class Paginator
    {
        private readonly BlogContext _blog;
        private readonly int _pageNum;
        private readonly int _postsPerPage;

        private IEnumerable<Post> _posts;

        public IEnumerable<Post> Posts =>_posts ?? (_posts = _blog.Posts.Skip((_pageNum - 1) * _postsPerPage)
                                                                        .Take(_postsPerPage)
                                                                        .ToList());

        public Paginator(BlogContext blog, int pageNum, int postsPerPage)
        {
            _blog = blog;
            _pageNum = pageNum;
            _postsPerPage = postsPerPage;
        }
    }
}