using FluentAssertions;
using Xunit;

namespace Tk.Toolkit.Cli.Tests.Unit.End2End
{
    public class GenerateGuids
    {
        [Fact]
        public void GenerateGuids_ReturnsOk()
        {
            var rc = Program.Main(new[] { "guid" });

            rc.Should().Be(0);
        }

        [Fact]
        public void GenerateGuids_AppliedGenerations_ReturnsOk()
        {
            var rc = Program.Main(new[] { "guid", "-g 10" });

            rc.Should().Be(0);
        }
                
        [Fact]
        public void GenerateGuids_InvalidGenerations_ReturnsFailure()
        {
            var rc = Program.Main(new[] { "guid", "-g a" });

            rc.Should().Be(1);
        }

        
    }
}
