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
        public string SinglePostPage(string postName)
        {
            // var a = View
            return "hello";
        }
    }
}