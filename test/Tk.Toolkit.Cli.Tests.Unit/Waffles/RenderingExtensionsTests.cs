using Shouldly;
using Tk.Toolkit.Cli.Waffle;
using Xunit;

namespace Tk.Toolkit.Cli.Tests.Unit.Waffles
{
    public class RenderingExtensionsTests
    {
        [Theory]
        [InlineData("[h1]", "# ")]
        [InlineData("[h2]", "## ")]
        [InlineData("[h3]", "### ")]
        [InlineData("[b]bo[/b]ld", "**bo**ld")]
        [InlineData("[b]b[i]o[/b]l[/i]d", "**b*o**l*d")]
        public void Markdown_Rendering(string value, string expected)
        {
            value.Render(RenderMode.Markdown)
                 .ShouldBe(expected);
        }

        [Theory]
        [InlineData("[h1]", "\r\n")]
        [InlineData("[h2]", "\r\n")]
        [InlineData("[h3]", "\r\n")]
        [InlineData("[b]bo[/b]ld", "bold")]
        [InlineData("[b]b[i]o[/b]l[/i]d", "bold")]
        public void Text_Rendering(string value, string expected)
        {
            value.Render(RenderMode.Text)
                 .ShouldBe(expected);
        }

        [Theory]
        [InlineData("[h1]", "<h1>")]
        [InlineData("[h2]", "<h2>")]
        [InlineData("[h3]", "<h3>")]
        [InlineData("[b]bo[/b]ld", "<strong>bo</strong>ld")]
        [InlineData("[b]b[i]o[/b]l[/i]d", "<strong>b<i>o</strong>l</i>d")]
        public void Html_Rendering(string value, string expected)
        {
            value.Render(RenderMode.Html)
                 .ShouldBe(expected);
        }
    }
}
