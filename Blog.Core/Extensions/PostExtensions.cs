using System.Collections.Generic;
using Blog.Core.Models;

namespace Blog.Core.Extensions
{
    public static class PostExtensions
    {
        public static Post ProcessTags(this Post post)
        {
            var header = post.Contents.YamlHeader();
            
            if (header.ContainsKey("tags"))
                post.Tags = header["tags"] as IEnumerable<string>;

            return post;
        }

        public static Post ExcludeHeader(this Post post)
        {
            post.Contents = post.Contents.ExcludeHeader();
            return post;
        }
    }
}