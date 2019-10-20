using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Domain.Entities;
using Blog.Core.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogService _blog;
        
        public HomeController(IBlogService blog) => _blog = blog;

        //[HttpGet("Home/Page/{page}")]
        public async Task<ViewResult> Index(int page = 1)
            => View(await _blog.CreateModel(page));

        //[HttpGet("Posts/{postName}")]
        public async Task<ViewResult> SinglePostPage(string postName) 
            => await FilteredPostPage(x => x.Where(p => p.Filename.Equals(postName)));

        //[HttpGet("Posts/tag/{tag}")]
        public async Task<ViewResult> PostsWithTagPage(string tag) 
            => await FilteredPostPage(x => x.Where(p => p.Tags.Any(t => t.ResolutionName == tag)));

        private async Task<ViewResult> FilteredPostPage(Func<IEnumerable<Post>, IEnumerable<Post>> filter)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));
            return View("Index", await _blog.GetFilteredPostsModel(filter));
        }
    }
}