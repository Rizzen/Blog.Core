using System.Threading.Tasks;
using RazorLight;

namespace Blog.Core.Models.Templating.Razor
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
            return await _engine.CompileRenderAsync(name, template, model);
        }
    }
}