using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using NSubstitute;
using Spectre.Console;
using Tk.Toolkit.Cli.Commands;
using Tk.Toolkit.Cli.Passwords;
using Xunit;

namespace Tk.Toolkit.Cli.Tests.Unit.Commands
{
    public class PasswordGeneratorCommandTests
    {
        [Fact]
        public async Task OnExecuteAsync_DefaultArgumnets_ReturnsOk()
        {
            Table? output = null;            
            var pwGen = Substitute.For<IPasswordGenerator>();
            pwGen.Generate(Arg.Any<int>())
                .Returns(ci => GeneratePassword(ci.ArgAt<int>(0)));

            var console = Substitute.For<IAnsiConsole>();
            console.When(ac => ac.Write(Arg.Any<Table>()))
                .Do(cb =>
                {
                    output = cb.Arg<Table>();
                });

            var cmd = new PasswordGeneratorCommand(console, pwGen);
            
            var rc = await cmd.OnExecuteAsync();

            rc.Should().Be(0);
            AssertTableOutputContainsPasswords(output, 5, 16);
        }

        [Property(Verbose = true)]
        public bool OnExecuteAsync_PositiveValues_ReturnsOk(PositiveInt count, PositiveInt pwLen)
        {
            Table? output = null;
            var pwGen = Substitute.For<IPasswordGenerator>();
            pwGen.Generate(Arg.Any<int>())
                .Returns(ci => GeneratePassword(ci.ArgAt<int>(0)));

            var console = Substitute.For<IAnsiConsole>();
            console.When(ac => ac.Write(Arg.Any<Table>()))
                .Do(cb =>
                {
                    output = cb.Arg<Table>();
                });

            var cmd = new PasswordGeneratorCommand(console, pwGen)
            {
                Generations = count.Get,
                PwLength = pwLen.Get,
            };

            var rc = cmd.OnExecuteAsync().GetAwaiter().GetResult();

            rc.Should().Be(0);
            AssertTableOutputContainsPasswords(output, count.Get, pwLen.Get);

            return true;
        }

        [Property(Verbose = true)]
        public bool OnExecuteAsync_NegativeCount_ReturnsOk(NegativeInt count)
        {
            Table? output = null;
            var pwGen = Substitute.For<IPasswordGenerator>();
            pwGen.Generate(Arg.Any<int>())
                .Returns(ci => GeneratePassword(ci.ArgAt<int>(0)));

            var console = Substitute.For<IAnsiConsole>();
            console.When(ac => ac.Write(Arg.Any<Table>()))
                .Do(cb =>
                {
                    output = cb.Arg<Table>();
                });

            var cmd = new PasswordGeneratorCommand(console, pwGen)
            {
                Generations = count.Get,
            };

            var rc = cmd.OnExecuteAsync().GetAwaiter().GetResult();

            rc.Should().Be(0);
            AssertTableOutputContainsPasswords(output, PasswordGeneratorCommand.DefaultPasswordCount, PasswordGeneratorCommand.DefaultPasswordLength);

            return true;
        }

        private string GeneratePassword(int length) => new string('a', length);

        private void AssertTableOutputContainsPasswords(Table? table, int count, int pwLen)
        {            
            table?.Rows.Count.Should().Be(count);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            foreach (var row in table.Rows)
            {
                foreach (var col in row.OfType<Markup>())
                {
                    // We can't see the rendered string, we'll just have to gauge its length
                    col.Length.Should().Be(pwLen);
                }
                row.Count.Should().Be(1);
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}
