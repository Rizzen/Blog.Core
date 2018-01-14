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
        private readonly IPostRepository _postRepository;
        private readonly PostsProcessor _postsProcessor;
        
        public HomeController(IHostingEnvironment hostingEnvironment, 
                              IPostRepository postRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _postRepository = postRepository;
            _postsProcessor = new PostsProcessor(postRepository);
        }

        public ViewResult Index()
        {
            return View(_postsProcessor.GetPostFeed());
        }
    }
}