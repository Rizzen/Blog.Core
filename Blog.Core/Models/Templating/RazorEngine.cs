using System.Threading.Tasks;
using RazorLight;

namespace Blog.Core.Models.Templating
{
    public class RazorEngine
    {
        private readonly RazorLightEngine _engine;
        
        public RazorEngine()
        {
            _engine = new RazorLightEngineBuilder()
                          .UseMemoryCachingProvider()
                          .Build();
        }

        public async Task<string> ProcessTemplate(string template, object model, string name)
        {
            return await _engine.CompileRenderAsync(name, template, model);
        }
    }
}