using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.DAL.Posts;
using Blog.Core.Domain.Entities;
using Blog.Core.Domain.Extensions;

namespace Blog.Core.Metadata
{
    public class MetadataProcessor : IMetadataProcessor
    {
        private readonly IPostStore _store;

        public MetadataProcessor(IPostStore store)
        {
            _store = store;
        }

        public async Task<List<Post>> GetMetadataForPosts(IEnumerable<Post> posts)
        {
            return (await Task.WhenAll(posts.Select(ProcessMetadata))).ToList();
        }
        
        private async Task<Post> ProcessMetadata(Post input)
        {
            var header = (await _store.GetContentByFilename(input.Filename)).YamlHeader();
            
            if (header.TryGetValue("tags", out var tags))
                input.Tags = ProcessTags(tags as List<string>);
            
            if(header.TryGetValue("title", out var title))
                input.Title = title as string;
                
            if (header.TryGetValue("date", out var date))
                input.DateTime = ProcessDateTime(date as string);
            
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