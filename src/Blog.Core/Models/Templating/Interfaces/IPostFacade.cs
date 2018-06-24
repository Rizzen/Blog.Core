using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Domain.Entities;
using Blog.Core.Models.Interfaces;

namespace Blog.Core.Models.Templating.Interfaces
{
    public interface IPostFacade
    {
        Task<List<Post>> GenRenderedPosts(IList<Post> input, IPageContext model);
        List<Post> GetAllPostsMetadataOnly();
    }
}