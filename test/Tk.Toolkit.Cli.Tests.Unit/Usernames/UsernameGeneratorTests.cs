using FsCheck;
using FsCheck.Xunit;
using Tk.Toolkit.Cli.Usernames;

namespace Tk.Toolkit.Cli.Tests.Unit.Usernames
{
    public class UsernameGeneratorTests
    {
        [Property(Verbose =true)]
        public bool Generate_GeneratesMinLengthValues(PositiveInt minLength)
        {
            var gen = new UsernameGenerator();

            var un = gen.Generate(minLength.Get);

            return un.Length >= minLength.Get;
        }

        [Property(Verbose = true)]
        public bool Generate_GeneratesNonEmptyString(PositiveInt minLength)
        {
            var gen = new UsernameGenerator();

            var un = gen.Generate(minLength.Get);

            return !string.IsNullOrWhiteSpace(un);
        }
    }
}
