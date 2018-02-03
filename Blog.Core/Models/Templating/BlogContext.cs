using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Blog.Core.Models.DAL;
using Blog.Core.Models.Templating.Processing;
using Blog.Core.Models.Templating.Razor;
using Microsoft.AspNetCore.Hosting;

namespace Blog.Core.Models.Templating
{
    public class BlogContext
    {
        //BUG Harcoded for now
        //TODO Move to site setting
        public int PostsPerPage = 10;
        
        private readonly IPostRepository _postRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly RazorEngine _engine;       

        public List<Post> Posts => _postRepository.Posts;
        
        public BlogContext(IPostRepository postRepository,
                           IHostingEnvironment hostingEnvironment)
        {
            _postRepository = postRepository;
            _hostingEnvironment = hostingEnvironment;
            _engine = new RazorEngine();
        }

        public async Task<PageContext> GetPostFeed()
        {
            
            return new PageContext
            {
                Posts = await ProcessTemplate(_postRepository.Posts)
            };
        }
        
        //TODO move to PostsProcessor
        private List<Post> ProcessHeaders(IEnumerable<Post> posts)
        {
            return posts.ToList();
        }

        //TODO move to PostsProcessor
        private async Task<List<Post>> ProcessTemplate(IEnumerable<Post> posts)
        {
            var result = new List<Post>();
            
            var contents = posts.ToDictionary(x => x.Content.Split('\\')
                                                         .Last()
                                                         .Replace(".cshtml", ""),
                                              x => File.ReadAllText(x.Content.Replace("~", _hostingEnvironment.ContentRootPath)));

            var model = new object();
            
            foreach (var content in contents)
            {
                var compiledView = await _engine.ProcessTemplateAsync(content.Key, content.Value, model);
                
                result.Add(new Post
                {
                    Title = "Пост",
                    Content = compiledView
                });
            }
            
            return result;
        }
    }
}