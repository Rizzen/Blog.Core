using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Blog.Core.Models.DAL;
using Blog.Core.Models.Templating.Razor;
using Microsoft.AspNetCore.Hosting;

namespace Blog.Core.Models.Templating
{
    public class BlogContext
    {
        //BUG Harcoded for now
        public int PostsPerPage = 10;
        
        private readonly IPostRepository _postRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly RazorEngine _engine;

        public IEnumerable<Post> Posts => _postRepository.Posts;
        
        public BlogContext(IPostRepository postRepository,
                           IHostingEnvironment hostingEnvironment)
        {
            _postRepository = postRepository;
            _hostingEnvironment = hostingEnvironment;
            _engine = new RazorEngine();
        }

        public async Task<PageContext> GetPostFeed()
        {
            //var result = 
            return new PageContext
            {
                Posts = await ProcessTemplate(_postRepository.Posts)
            };
        }
        
        //TODO move to PostsProcessor
        private IEnumerable<Post> ProcessHeaders(IEnumerable<Post> posts)
        {
            return posts;
        }

        //TODO move to PostsProcessor
        private async Task<IEnumerable<Post>> ProcessTemplate(IEnumerable<Post> posts)
        {
            var result = new List<Post>();
            
            var contents = posts.ToDictionary(x => x.Contents.Split('\\')
                                                         .Last()
                                                         .Replace(".cshtml", ""),
                                              x => File.ReadAllText(x.Contents.Replace("~", _hostingEnvironment.ContentRootPath)));

            var model = new object();
            
            foreach (var content in contents)
            {
                var compiledView = await _engine.ProcessTemplate(content.Key, content.Value, model);
                
                result.Add(new Post
                {
                    Title = "Пост",
                    Contents = compiledView
                });
            }
            
            return result;
        }
    }
}