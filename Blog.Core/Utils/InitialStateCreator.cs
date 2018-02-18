using Blog.Core.Models.Templating.Processing;

namespace Blog.Core.Utils
{
    public class InitialStateCreator
    {
        private readonly PostCache _cache;
        
        public InitialStateCreator(PostCache cache)
        {
            _cache = cache;
        }
        
        public void Init()
        {
            _cache.CheckAndUpdate();
        }
    }
}