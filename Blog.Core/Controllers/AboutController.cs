using Microsoft.AspNetCore.Mvc;

namespace Blog.Core.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index() => View("About");
    }
}