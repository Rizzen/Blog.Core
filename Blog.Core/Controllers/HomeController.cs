using Blog.Core.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlog _blog;
        
        public HomeController(IBlog blog)
        {
            _blog = blog;
        }

        [HttpGet]
        public ViewResult Index(int page = 1)
        {
            return View(_blog.GetPostFeed(page));
        }

        [HttpGet]
        public ViewResult SinglePostPage(string postName)
        {
            return View("SinglePost", _blog.GetSinglePostPage(postName));
        }
    }
}