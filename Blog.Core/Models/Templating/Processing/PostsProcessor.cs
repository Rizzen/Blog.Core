using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Extensions;
using Blog.Core.Models.DAL;
using Blog.Core.Models.Templating.Razor;

namespace Blog.Core.Models.Templating.Processing
{
    public class PostsProcessor
    {
        private readonly IPostRepository _postRepository;
        private readonly RazorEngine _engine;
        
        public PostsProcessor(IPostRepository postRepository, RazorEngine engine)
        {
            _postRepository = postRepository;
            _engine = engine;
        }

        public IEnumerable<Post> ProcessMetadata(IEnumerable<Post> input)
        {
            return input.Select(ProcessMetadata);
        }
        
        public Post ProcessMetadata(Post input)
        {
            if (input.Content.IsNullOrEmpty())
                input.Content = _postRepository.GetContentByFilename(input.Filename);
            
            return input.ProcessTags()
                        .ExcludeHeader();
        }

        public async Task<IEnumerable<Post>> ProcessTemplatesAsync(IEnumerable<Post> input, PageContext pageContext)
        {
            var tasks = await Task.WhenAll(input.Select(async x => await ProcessTemplateAsync(x, pageContext)));
            return tasks.ToList();
        }

        public async Task<Post> ProcessTemplateAsync(Post input, PageContext pageContext)
        {
            input.Content = await _engine.ProcessTemplateAsync(input.Filename, input.Content, pageContext);
            return input;
        }
    }
}