using System.Collections.Generic;
using System.Linq;
using Blog.Core.Models.DAL;
using Blog.Core.Models.Templating;

namespace Blog.Core.Models.Pagination
{
    public class Paginator
    {
        private readonly PostFacade _facade;
        private readonly int _pageNum;
        private readonly int _postsPerPage;

        private IList<Post> _posts;
        
        
        //TODO rebuild to get 
        /*public IEnumerable<Post> Posts => _posts ?? (_posts = _facade.Posts.Skip((_pageNum - 1) * _postsPerPage)
                                                                         .Take(_postsPerPage)
                                                                         .ToList());
        */
        
        public Paginator(PostFacade facade, int pageNum, int postsPerPage)
        {
            _facade = facade;
            _pageNum = pageNum;
            _postsPerPage = postsPerPage;
        }
    }
}