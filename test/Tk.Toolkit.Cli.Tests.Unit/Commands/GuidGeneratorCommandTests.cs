using System.Linq;
using System.Threading.Tasks;
using FsCheck;
using FsCheck.Xunit;
using NSubstitute;
using Shouldly;
using Spectre.Console;
using Tk.Toolkit.Cli.Commands;
using Xunit;

namespace Tk.Toolkit.Cli.Tests.Unit.Commands
{
    public class GuidGeneratorCommandTests
    {
        [Fact]
        public void OnExecute_DefaultArgumnets_ReturnsOk()
        {
            Table? output = null;
            var console = Substitute.For<IAnsiConsole>();
            console.When(ac => ac.Write(Arg.Any<Table>()))
                .Do(cb =>
                {
                    output = cb.Arg<Table>();
                });

            var cmd = new GuidGeneratorCommand(console);

            var rc = cmd.OnExecute();

            rc.ShouldBe(0);
            AssertTableOutputContainsGuids(output, 5);
        }

        [Property(Verbose = true)]
        public bool OnExecute_PositiveCount_ReturnsOk(PositiveInt count)
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

            var rc = cmd.OnExecute();

            rc.ShouldBe(0);
            AssertTableOutputContainsGuids(output, count.Get);

            return true;
        }

        [Property(Verbose = true)]
        public bool OnExecute_NegativeCount_ReturnsOk(NegativeInt count)
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

            var rc = cmd.OnExecute();

            rc.ShouldBe(0);
            AssertTableOutputContainsGuids(output, GuidGeneratorCommand.DefaultGenerationCount);

            return true;
        }


        private void AssertTableOutputContainsGuids(Table? table, int count)
        {
            var guidLength = System.Guid.NewGuid().ToString().Length;

            table?.Rows.Count.ShouldBe(count);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            foreach (var row in table.Rows)
            {
                foreach (var col in row.OfType<Markup>())
                {
                    // We can't see the rendered string, we'll just have to gauge its length
                    col.Length.ShouldBe(guidLength);
                }
                row.Count.ShouldBe(1);
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}
