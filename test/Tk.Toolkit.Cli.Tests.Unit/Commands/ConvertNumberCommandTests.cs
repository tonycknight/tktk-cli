using System.Threading.Tasks;
using FsCheck;
using NSubstitute;
using Shouldly;
using Spectre.Console;
using Tk.Toolkit.Cli.Commands;
using Tk.Toolkit.Cli.Conversions;
using Xunit;

namespace Tk.Toolkit.Cli.Tests.Unit.Commands
{
    public class ConvertNumberCommandTests
    {
        [Fact]
        public void OnExecute_DefaultArgumnets_ReturnsError()
        {
            var console = Substitute.For<IAnsiConsole>();
            var converter = Substitute.For<Tk.Toolkit.Cli.Conversions.INumericValueConverter>();
            var cmd = new ConvertNumberCommand(console, converter);

            var rc = cmd.OnExecute();

            rc.ShouldBe(1);
        }

        [Fact]
        public void OnExecute_ValidIntegerPassed_ReturnsOk()
        {
            Table? output = null;
            var console = Substitute.For<IAnsiConsole>();
            console.When(ac => ac.Write(Arg.Any<Table>()))
                .Do(cb =>
                {
                    output = cb.Arg<Table>();
                });
            var converter = Substitute.For<Tk.Toolkit.Cli.Conversions.INumericValueConverter>();
            converter.Convert(Arg.Any<NumericValue>()).Returns(new[] { new HexadecimalValue("0x1234") });
            var cmd = new ConvertNumberCommand(console, converter)
            {
                Value = 1234.ToString(),
            };

            var rc = cmd.OnExecute();

            rc.ShouldBe(0);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            output.Rows.Count.ShouldBe(1);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}
