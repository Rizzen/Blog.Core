using System.Collections.Generic;
using Blog.Core.Models;

namespace Blog.Core.Extensions
{
    public static class PostExtensions
    {
        public static Post ProcessTags(this Post post)
        {
            var header = post.Content.YamlHeader();
            
            if (header.ContainsKey("tags"))
                post.Tags = header["tags"] as List<string>;

            return post;
        }

        public static Post ProcessTitle(this Post post)
        {
            var header = post.Content.YamlHeader();
            
            if (header.ContainsKey("title"))
                post.Title = header["title"] as string;

            return post;
        }

        public static Post ExcludeHeader(this Post post)
        {
            post.Content = post.Content.ExcludeHeader();
            return post;
        }
    }
}