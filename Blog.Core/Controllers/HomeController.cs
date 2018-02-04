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

        public ViewResult Index()
        {
            return View(_blog.GetPostFeed());
        }
    }
}