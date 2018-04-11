using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly IPostStore _postStore;
        private readonly RazorEngine _engine;
        
        public PostsProcessor(IPostStore postStore, RazorEngine engine)
        {
            _postStore = postStore;
            _engine = engine;
        }

        public async Task<List<Post>> ProcessMetadata(IEnumerable<Post> input)
        {
            return (await Task.WhenAll(input.Select(ProcessMetadata))).ToList();
        }
        
        public async Task<Post> ProcessMetadata(Post input)
        {
            var header = (await _postStore.GetContentByFilename(input.Filename)).YamlHeader();
            
            input.Tags = ProcessTags(header["tags"] as List<string>);
            input.Title = header["title"] as string;
            input.DateTime = ProcessDateTime(header["date"] as string);
            
            return input;
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

        private List<Tag> ProcessTags(IEnumerable<string> tags) => tags.Select(x => new Tag(x)).ToList();

        private DateTime? ProcessDateTime(string dateString)
        {
            if (DateTime.TryParse(dateString, out var dateTime))
                return dateTime;
            return null;
        }
    }
}