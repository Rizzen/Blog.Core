using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Models;
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

        [HttpGet]
        public async Task<ViewResult> Index(int page = 1)
        {
            return View(await _blog.GetPostFeed(page));
        }

        [HttpGet]
        public async Task<ViewResult> SinglePostPage(string postName)
        {
            return await FilteredPostPage(x => x.Where(p => p.Filename == postName));
        }
        
        [HttpGet]
        public async Task<ViewResult> PostsWithTagPage(string tag)
        {
            return await FilteredPostPage(x => x.Where(p => p.Tags.Any(t => t.ResolutionName == tag)));
        }
        
        private async Task<ViewResult> FilteredPostPage(Func<IEnumerable<Post>, IEnumerable<Post>> filter)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));
            return View("Index", await _blog.GetFilteredPostPage(filter));
        }
    }
}