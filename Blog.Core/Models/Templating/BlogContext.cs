using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Blog.Core.Models.DAL;
using Microsoft.AspNetCore.Hosting;
using RazorLight;

namespace Blog.Core.Models.Templating
{
    public class BlogContext
    {
        private readonly IPostRepository _postRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly RazorLightEngine engine;

        public IEnumerable<Post> Posts => _postRepository.Posts;
        
        public BlogContext(IPostRepository postRepository,
                              IHostingEnvironment hostingEnvironment)
        {
            _postRepository = postRepository;
            _hostingEnvironment = hostingEnvironment;
            engine = new RazorLightEngineBuilder()
                          .UseMemoryCachingProvider()
                          .Build();
        }

        public async Task<PageContext> GetPostFeed()
        {
            return new PageContext
            {
                Posts = await ProcessTemplate(_postRepository.Posts)
            };
        }

        private IEnumerable<Post> ProcessHeaders(IEnumerable<Post> posts)
        {
            return posts;
        }

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
                var compiledView = await engine.CompileRenderAsync(content.Key, content.Value, model);
                
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