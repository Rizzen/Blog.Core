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
        private readonly PostsProcessor _postsProcessor;
        
        public HomeController(IHostingEnvironment hostingEnvironment,
                              PostsProcessor postsProcessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _postsProcessor = postsProcessor;
        }

        public ViewResult Index()
        {
            return View(_postsProcessor.GetPostFeed());
        }
    }
}