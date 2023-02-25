using FluentAssertions;
using NSubstitute;
using Spectre.Console;
using Tk.Toolkit.Cli.Commands;
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
                Values = new[] { value }
            };

            var rc = cmd.OnExecute();

            rc.Should().Be(1);
            console.Received(1).Write(Arg.Any<Markup>());
            console.Received(0).Write(Arg.Any<Text>());
        }

        [Fact]
        public void OnExecute_ValidIntegerPassed_ReturnsOk()
        {
            var console = Substitute.For<IAnsiConsole>();

            var cmd = new EpochCommand(console)
            {
                Values = new[] { 1234.ToString() },
            };

            var rc = cmd.OnExecute();

            rc.Should().Be(0);
            console.Received(1).Write(Arg.Any<Text>());
            console.Received(0).Write(Arg.Any<Markup>());
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
                Values = new[] { value },
            };

            var rc = cmd.OnExecute();

            rc.Should().Be(1);
            console.Received(1).Write(Arg.Any<Markup>());
        }

        [Theory]
        [InlineData("now")]
        [InlineData("1970-01-01T01:01:00")]
        [InlineData("2022-01-01T01:23:56")]
        [InlineData("2022-01-01", "01:23:56")]
        public void OnExecute_ValidDatePassed_ReturnsOk(params string[] values)
        {
            var console = Substitute.For<IAnsiConsole>();

            var cmd = new EpochCommand(console)
            {
                Values = values,
            };

            var rc = cmd.OnExecute();

            rc.Should().Be(0);
            console.Received(1).Write(Arg.Any<Text>());
            console.Received(0).Write(Arg.Any<Markup>());
        }

        [Theory]
        [InlineData("20221201a")]
        [InlineData("abcdef")]
        public void OnExecute_InvalidDatePassed_ReturnsError(string value)
        {
            var console = Substitute.For<IAnsiConsole>();

            var cmd = new EpochCommand(console)
            {
                Values = new[] { value },
            };

            var rc = cmd.OnExecute();

            rc.Should().Be(1);
            console.Received(1).Write(Arg.Any<Markup>());
        }
    }
}