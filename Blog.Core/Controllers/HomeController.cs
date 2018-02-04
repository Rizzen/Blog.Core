using System.Threading.Tasks;
using Blog.Core.Models;
using Blog.Core.Models.Contexts;
using Blog.Core.Models.DAL;
using Blog.Core.Models.Interfaces;
using Blog.Core.Models.Templating;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IBlogContext _blogContext;
        private readonly BlogMain _blog;
        
        public HomeController(IHostingEnvironment hostingEnvironment, BlogMain blog, IBlogContext blogContext)
        {
            _hostingEnvironment = hostingEnvironment;
            _blogContext = blogContext;
            _blog = blog;
        }

        public ViewResult Index()
        {
            return View(_blog.GetPostFeed());
        }
    }
}