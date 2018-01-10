using Blog.Core.Models;
using Blog.Core.Models.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IPostRepository _postRepository;
        
        public HomeController(IHostingEnvironment hostingEnvironment, IPostRepository postRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _postRepository = postRepository;
        }

        public ViewResult Index()
        {
            return View(_postRepository);
        }
    }
}