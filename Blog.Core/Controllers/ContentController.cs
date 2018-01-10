using Microsoft.AspNetCore.Mvc;

namespace Blog.Core.Controllers
{
    public class ContentController : Controller
    {
        public IActionResult Index() => View("Content");
    }
}