using Shouldly;
using Xunit;

namespace Tk.Toolkit.Cli.Tests.Unit.End2End
{
    public class GeneratePasswordsCommand
    {

        [Fact]
        public void GeneratePasswords_ReturnsOk()
        {
            var rc = Program.Main(new[] { "pw" });

            rc.ShouldBe(0);
        }

        [Fact]
        public void GeneratePasswords_AppliedGenerations_ReturnsOk()
        {
            var rc = Program.Main(new[] { "pw", "-g 10" });

            rc.ShouldBe(0);
        }

        [Fact]
        public void GeneratePasswords_InvalidGenerations_ReturnsFailure()
        {
            var rc = Program.Main(new[] { "pw", "-g a" });

            rc.ShouldBe(1);
        }

        [Fact]
        public void GeneratePasswords_AppliedLength_ReturnsOk()
        {
            var rc = Program.Main(new[] { "pw", "-l 10" });

            rc.ShouldBe(0);
        }

        [Fact]
        public void GeneratePasswords_InvalidLength_ReturnsFailure()
        {
            var rc = Program.Main(new[] { "pw", "-l a" });

            rc.ShouldBe(1);
        }
    }
}
