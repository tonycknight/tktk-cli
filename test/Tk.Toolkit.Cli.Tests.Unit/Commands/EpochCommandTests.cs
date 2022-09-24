using System.Threading.Tasks;
using FluentAssertions;
using FsCheck;
using NSubstitute;
using Spectre.Console;
using Tk.Toolkit.Cli.Commands;
using Tk.Toolkit.Cli.Conversions;
using Xunit;


namespace Tk.Toolkit.Cli.Tests.Unit.Commands
{
    public class EpochCommandTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void OnExecute_DefaultArgumnets_ReturnsError(string value)
        {
            var console = Substitute.For<IAnsiConsole>();
            var cmd = new EpochCommand(console)
            {
                Value = value
            };

            var rc = cmd.OnExecute();

            rc.Should().Be(1);
        }

        [Fact]
        public void OnExecute_ValidIntegerPassed_ReturnsOk()
        {
            var console = Substitute.For<IAnsiConsole>();
            
            var cmd = new EpochCommand(console)
            {
                Value = 1234.ToString(),
            };

            var rc = cmd.OnExecute();

            rc.Should().Be(0);
            console.Received(1).Write(Arg.Any<Text>());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void OnExecute_InvalidIntegerPassed_ReturnsError(string value)
        {
            var console = Substitute.For<IAnsiConsole>();

            var cmd = new EpochCommand(console)
            {
                Value = value,
            };

            var rc = cmd.OnExecute();

            rc.Should().Be(1);
            console.Received(1).Write(Arg.Any<Markup>());
        }

    }
}