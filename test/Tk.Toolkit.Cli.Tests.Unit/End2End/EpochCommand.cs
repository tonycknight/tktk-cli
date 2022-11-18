using FluentAssertions;
using Xunit;
using System.Linq;

namespace Tk.Toolkit.Cli.Tests.Unit.End2End
{
    public class EpochCommand
    {
        [Theory]
        [InlineData("now")]
        [InlineData("1970-01-01T01:01:00")]
        [InlineData("2022-01-01T01:23:56")]
        [InlineData("2022-01-01", "01:23:56")]
        public void Epoch_ReturnsOk(params string[] values)
        {
            var rc = Program.Main(new[] { "epoch" }.Concat(values).ToArray() );

            rc.Should().Be(0);
        }
    }
}
