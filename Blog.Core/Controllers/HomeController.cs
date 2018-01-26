using System.Threading.Tasks;
using Blog.Core.Models;
using Blog.Core.Models.DAL;
using Blog.Core.Models.Templating;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly BlogContext _postsProcessor;
        
        public HomeController(IHostingEnvironment hostingEnvironment,
                              BlogContext postsProcessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _postsProcessor = postsProcessor;
        }

        public async Task<ViewResult> Index()
        {
            return View(await _postsProcessor.GetPostFeed());
        }
    }
}