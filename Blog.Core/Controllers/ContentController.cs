using Blog.Core.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Core.Controllers
{
    public class ContentController : Controller
    {
        private readonly IBlog _blog;

        public ContentController(IBlog blog)
        {
            _blog = blog;
        }

        public ViewResult Content()
        {
            return View(_blog.GetBlogContent());
        } 
    }
}