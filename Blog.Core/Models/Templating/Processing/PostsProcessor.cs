using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Extensions;
using Blog.Core.Models.DAL;
using Blog.Core.Models.Interfaces;
using Blog.Core.Models.Templating.Interfaces;
using Blog.Core.Models.Templating.Razor;

namespace Blog.Core.Models.Templating.Processing
{
    public class PostsProcessor: IPostsProcessor
    {
        private readonly IPostDAO _postDao;
        private readonly RazorEngine _engine;
        
        public PostsProcessor(IPostDAO postDao, RazorEngine engine)
        {
            _postDao = postDao;
            _engine = engine;
        }

        public List<Post> ProcessMetadata(IEnumerable<Post> input)
        {
            return input.Select(ProcessMetadata)
                        .ToList();
        }
        
        public Post ProcessMetadata(Post input)
        {
            if (input.Content.IsNullOrEmpty())
                input.Content = _postDao.GetContentByFilename(input.Filename);
            
            return input.ProcessTags()
                        .ProcessTitle()
                        .ExcludeHeader();
        }

        public async Task<List<Post>> ProcessTemplatesAsync(IEnumerable<Post> input, IPageContext pageContext)
        {
            var tasks = await Task.WhenAll(input.Select(async x => await ProcessTemplateAsync(x, pageContext)));
            return tasks.ToList();
        }

        public async Task<Post> ProcessTemplateAsync(Post input, IPageContext pageContext)
        {
            input.Content = await _engine.ProcessTemplateAsync(input.Filename, input.Content, pageContext);
            return input;
        }
    }
}