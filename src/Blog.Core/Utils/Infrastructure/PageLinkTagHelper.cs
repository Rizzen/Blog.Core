using Blog.Core.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Blog.Core.Utils.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "model")]
    public class PageLinkTagHelper: TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;
        
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        
        public IBlogModel Model { get; set; }
        
        public string PageAction { get; set; }
        
        public string PageClass { get; set; }
        
        public string PageClassNormal { get; set; }
        
        public string PageClassSelected { get; set; }
        
        public string PageLinkClass { get; set; }
        
        public string PageListClass { get; set; }
        
        public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            var result = new TagBuilder("nav");
            var list = new TagBuilder("ul");
            list.AddCssClass(PageListClass);

            for (var i = 1; i <= Model.PageCount; i++)
            {
                var tag = new TagBuilder("li");
                tag.AddCssClass(PageClass);
                
                var a = new TagBuilder("a");
                a.Attributes["href"] = urlHelper.Action(PageAction, new {page = i});
                a.AddCssClass(PageLinkClass);
                
                tag.AddCssClass(i == Model.Page.PageNum
                                         ? PageClassSelected
                                         : PageClassNormal);
                
                a.InnerHtml.Append(i.ToString());
                tag.InnerHtml.AppendHtml(a);
                list.InnerHtml.AppendHtml(tag);
            }

            result.InnerHtml.AppendHtml(list);
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}