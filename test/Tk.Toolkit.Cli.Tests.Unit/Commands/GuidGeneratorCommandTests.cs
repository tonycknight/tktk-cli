using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using NSubstitute;
using Spectre.Console;
using Tk.Toolkit.Cli.Commands;
using Xunit;

namespace Tk.Toolkit.Cli.Tests.Unit.Commands
{
    public class GuidGeneratorCommandTests
    {
        [Fact]
        public async Task OnExecuteAsync_DefaultArgumnets_ReturnsOk()
        {
            Table? output = null;
            var console = Substitute.For<IAnsiConsole>();
            console.When(ac => ac.Write(Arg.Any<Table>()))
                .Do(cb =>
                {
                    output = cb.Arg<Table>();
                });

            var cmd = new GuidGeneratorCommand(console);

            var rc = await cmd.OnExecuteAsync();

            rc.Should().Be(0);
            AssertTableOutputContainsGuids(output, 5);
        }

        [Property(Verbose = true)]
        public bool OnExecuteAsync_ReturnsOk(PositiveInt count)
        {
            Table? output = null;
            var console = Substitute.For<IAnsiConsole>();
            console.When(ac => ac.Write(Arg.Any<Table>()))
                .Do(cb =>
                {
                    output = cb.Arg<Table>();
                });

            var cmd = new GuidGeneratorCommand(console)
            {
                Generations = count.Get,
            };

            var rc = cmd.OnExecuteAsync().GetAwaiter().GetResult();

            rc.Should().Be(0);
            AssertTableOutputContainsGuids(output, count.Get);

            return true;
        }

        private void AssertTableOutputContainsGuids(Table? table, int count)
        {
            var guidLength = System.Guid.NewGuid().ToString().Length;

            table?.Rows.Count.Should().Be(count);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            foreach (var row in table.Rows)
            {
                foreach (var col in row.OfType<Markup>())
                {
                    // We can't see the rendered string, we'll just have to gauge its length
                    col.Length.Should().Be(guidLength);
                }
                row.Count.Should().Be(1);
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}
