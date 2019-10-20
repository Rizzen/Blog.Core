using System.Threading.Tasks;
using Blog.Core.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Core.Controllers
{
    public class ContentController : Controller
    {
        private readonly IBlogService _blog;

        public ContentController(IBlogService blog)
        {
            _blog = blog;
        }

        public async Task<ViewResult> Content() 
            => View(await _blog.CreateModel(0));
    }
}