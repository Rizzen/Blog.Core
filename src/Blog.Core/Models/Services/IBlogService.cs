using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Domain.Entities;

namespace Blog.Core.Models.Services
{
    public interface IBlogService
    {
        Task<IBlogModel> CreateModel(int pageNum);
        Task<IBlogModel> GetFilteredPostsModel(Func<IEnumerable<Post>, IEnumerable<Post>> filter);
    }
}