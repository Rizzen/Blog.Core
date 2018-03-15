using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Models;
using Blog.Core.Models.Contexts;
using Blog.Core.Models.Interfaces;
using Blog.Core.Models.Pagination;
using Blog.Core.Models.Templating.Interfaces;
using Blog.Core.Utils.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using NUnit.Framework;

namespace Blog.Core.Tests
{
    [TestFixture]
    public class PaginationTests
    {
        [Test]
        public void Can_Generate_Page_Links()
        {
            //Arrange
            var posts = new List<Post>();
            for (var i = 0; i < 30; i++)
            {
                posts.Add(new Post());
            }
            
            var blog = new Mock<IBlogContext>();
            blog.Setup(x => x.Posts).Returns(posts);
            
            var facade = new Mock<IPostFacade>();
            var context = new Mock<IPageContext>();
            context.Setup(x => x.Blog).Returns(blog.Object);
            
            const int pageNum = 2;
            const int postsPerPage = 10;
            
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>())).Returns("Test/Page1")
                                                                                .Returns("Test/Page2")
                                                                                .Returns("Test/Page3");
            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(f => f.GetUrlHelper(It.IsAny<ActionContext>())).Returns(urlHelper.Object);

            var helper = new PageLinkTagHelper(urlHelperFactory.Object)
            {
                Paginator = new Paginator(facade.Object, context.Object, pageNum, postsPerPage),
                PageAction = "Test"
            };

            var ctx = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(), "");
            
            var content = new Mock<TagHelperContent>();
            
            var output = new TagHelperOutput("div",
                                             new TagHelperAttributeList(),
                                             (cache, encoder) => Task.FromResult(content.Object));
            
            //Act
            helper.Process(ctx, output);
            
            //Assert
            Assert.AreEqual(@"<a href=""Test/Page1"">1</a>"
                          + @"<a href=""Test/Page2"">2</a>"
                          + @"<a href=""Test/Page3"">3</a>",
                          output.Content.GetContent());
        }
    }

}