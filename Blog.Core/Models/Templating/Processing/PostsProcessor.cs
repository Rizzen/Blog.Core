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
        private readonly BlogContext _blog;
        
        public PostsProcessor(IPostRepository postRepository, RazorEngine engine, BlogContext blog)
        {
            _postRepository = postRepository;
            _engine = engine;
            _blog = blog;
        }

        public IEnumerable<Post> ProcessMetadata(IEnumerable<Post> input)
        {
            return input.Select(ProcessMetadata);
        }
        
        public Post ProcessMetadata(Post input)
        {
            if (input.Contents.IsNullOrEmpty())
                input.Contents = _postRepository.GetContentByFilename(input.Filename);
            
            return input.ProcessTags()
                        .ExcludeHeader();
        }

        public async Task<IEnumerable<Post>> ProcessTemplatesAsync(IEnumerable<Post> input)
        {
            var tasks = await Task.WhenAll(input.Select(async x => await ProcessTemplateAsync(x)));
            return tasks.ToList();
        }

        public async Task<Post> ProcessTemplateAsync(Post input)
        {
            input.Contents = await _engine.ProcessTemplateAsync(input.Filename, input.Contents, _blog);
            return input;
        }
    }
}