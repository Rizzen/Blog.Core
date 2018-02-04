using Blog.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Core.Controllers
{
    public class ContentController : Controller
    {
        private readonly BlogMain _blog;

        public ContentController(BlogMain blog)
        {
            _blog = blog;
        }

        public ViewResult Content()
        {
            return View(_blog.GetBlogContent());
        } 
    }
}