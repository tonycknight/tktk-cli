using Shouldly;
using Xunit;

namespace Tk.Toolkit.Cli.Tests.Unit.End2End
{
    public class GenerateGuidsCommand
    {
        [Fact]
        public void GenerateGuids_ReturnsOk()
        {
            var rc = Program.Main(new[] { "guid" });

            rc.ShouldBe(0);
        }

        [Fact]
        public void GenerateGuids_AppliedGenerations_ReturnsOk()
        {
            var rc = Program.Main(new[] { "guid", "-g 10" });

            rc.ShouldBe(0);
        }

        [Fact]
        public void GenerateGuids_InvalidGenerations_ReturnsFailure()
        {
            var rc = Program.Main(new[] { "guid", "-g a" });

            rc.ShouldBe(1);
        }
    }
}
