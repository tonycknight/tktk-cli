using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using NSubstitute;
using Spectre.Console;
using Spectre.Console.Rendering;
using Tk.Toolkit.Cli.Commands;
using Tk.Toolkit.Cli.Jwts;
using Xunit;

namespace Tk.Toolkit.Cli.Tests.Unit.Commands
{
    public class DecodeJwtCommandTests
    {
        [Fact]
        public async Task OnExecuteAsync_DefaultArgumnets_ReturnsError()
        {
            var console = Substitute.For<IAnsiConsole>();
            
            var jwtParser = Substitute.For<IJwtParser>();


            var cmd = new DecodeJwtCommand(console, jwtParser)
            {
                Jwt = "",
            };

            var rc = await cmd.OnExecuteAsync();

            rc.Should().Be(1);
            console.Received(1).Write(Arg.Any<IRenderable>());
            console.Received(1).Write(Arg.Any<Markup>());
        }

        [Property(Verbose = true)]
        public bool OnExecuteAsync_BadJwt_ReturnsError(Guid jwt)
        {
            var console = Substitute.For<IAnsiConsole>();
            var jwtParser = Substitute.For<IJwtParser>();
            jwtParser.Parse(Arg.Any<string>())
                     .Returns(ci => throw new Exception());

            var cmd = new DecodeJwtCommand(console, jwtParser)
            {
                Jwt = jwt.ToString(),
            };

            var rc = cmd.OnExecuteAsync().GetAwaiter().GetResult();

            rc.Should().Be(1);
            console.Received(1).Write(Arg.Any<IRenderable>());
            console.Received(1).Write(Arg.Any<Markup>());

            return true;
        }


        [Property(Verbose = true, Arbitrary = new[] { typeof(Jwts.JwtStringArbitraries) })]
        public bool OnExecuteAsync_JwtProvided_ReturnsOk(string jwt)
        {
            Table? output = null;
            var console = Substitute.For<IAnsiConsole>();
            console.When(ac => ac.Write(Arg.Any<Table>()))
                .Do(cb =>
                {
                    output = cb.Arg<Table>();
                });

            var jwtParser = Substitute.For<IJwtParser>();


            var cmd = new DecodeJwtCommand(console, jwtParser)
            {
                Jwt = jwt,
            };

            var rc = cmd.OnExecuteAsync().GetAwaiter().GetResult();

            rc.Should().Be(0);
            output.Should().NotBeNull();
            AssertTableOutputContainsText(output, 2);

            return true;
        }


        private void AssertTableOutputContainsText(Table? table, int colCount)
        {            
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            foreach (var row in table.Rows)
            {
                var cols = row.OfType<Markup>().ToList();
                cols.Should().HaveCount(colCount);
                cols.OfType<Text>().All(t => t.Length > 1).Should().BeTrue();
                // Sadly, there's no way to inspect the contents of each column
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}
