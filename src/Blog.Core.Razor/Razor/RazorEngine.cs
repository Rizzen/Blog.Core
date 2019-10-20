using System.Threading.Tasks;

namespace Blog.Core.Razor.Razor
{
    public class RazorEngine
    {
        private readonly ViewRenderService _viewRenderService;
        
        public RazorEngine(ViewRenderService viewRenderService)
        {
            _viewRenderService = viewRenderService;
        }

        public async Task<string> ProcessTemplateByFilenameAsync(string filename, object model) 
            => await _viewRenderService.RenderToStringAsync(filename, model);
    }
}