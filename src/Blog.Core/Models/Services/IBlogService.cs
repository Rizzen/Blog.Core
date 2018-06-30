using System.Threading.Tasks;

namespace Blog.Core.Models.Services
{
    public interface IBlogService
    {
        Task<BlogModel> CreateModel(int pageNum);
    }
}