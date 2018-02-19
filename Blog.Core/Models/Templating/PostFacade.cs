using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Models.Contexts;
using Blog.Core.Models.DAL;
using Blog.Core.Models.Interfaces;
using Blog.Core.Models.Templating.Interfaces;
using Blog.Core.Utils;

namespace Blog.Core.Models.Templating
{
    public class PostFacade: IPostFacade
    {
        private readonly IPostStore _postStore;
        private readonly IPostsProcessor _postsProcessor;
        private readonly ICache<Post> _cache;
        
        public PostFacade(IPostStore postStore, IPostsProcessor postsProcessor, ICache<Post> cache)
        {
            _postStore = postStore;
            _postsProcessor = postsProcessor;
            _cache = cache;
        }

        public async Task<List<Post>> GenRenderedPosts(IEnumerable<Post> input, IPageContext model)
        {
            var postsWithContent = GetPostContent(input.ToList());
            var toProcess = _postsProcessor.ProcessMetadata(postsWithContent);
            
            return await _postsProcessor.ProcessTemplatesAsync(toProcess, model);
        }

        public List<Post> GetAllPostsMetadataOnly()
        {
            return _postsProcessor.ProcessMetadata(_postStore.Posts);
        }

        private List<Post> GetPostContent(IList<Post> input)
        {
            foreach (var post in input)
            {
                post.Content = _postStore.GetContentByFilename(post.Filename);
            }

            return input.ToList();
        }
    }
}