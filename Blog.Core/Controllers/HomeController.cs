using System;
using System.Collections.Generic;
using System.Linq;
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
        public ViewResult Index(int page = 1)
        {
            return View(_blog.GetPostFeed(page));
        }

        [HttpGet]
        public ViewResult SinglePostPage(string postName)
        {
            return FilteredPostPage(x => x.Where(p => p.Filename == postName));
        }
        
        [HttpGet]
        public ViewResult PostsWithTagPage(string tag)
        {
            return FilteredPostPage(x => x.Where(p => p.Tags.Any(t => t.ResolutionName == tag)));
        }
        
        private ViewResult FilteredPostPage(Func<IEnumerable<Post>, IEnumerable<Post>> filter)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            return View("Index", _blog.GetFilteredPostPage(filter));
        }
    }
}