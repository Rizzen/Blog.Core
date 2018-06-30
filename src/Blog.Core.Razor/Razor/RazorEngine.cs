using System.Threading.Tasks;
using RazorLight;

namespace Blog.Core.Razor.Razor
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

        public async Task<string> ProcessTemplateAsync(string name, string template, object model)
        {
            return await _engine.CompileRenderStringAsync(name, template, model);
        }
    }
}