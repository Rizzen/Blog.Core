using System;
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
                          //.UseFileSystemProject("E:\\SharpProjects\\Blog.Core\\Blog.Core\\Views")
                          .UseMemoryCachingProvider()
                          .Build();
        }

        public async Task<string> ProcessTemplateAsync(string name, string template, object model)
        {
            try
            {
                return await _engine.CompileRenderStringAsync(name, template, model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}