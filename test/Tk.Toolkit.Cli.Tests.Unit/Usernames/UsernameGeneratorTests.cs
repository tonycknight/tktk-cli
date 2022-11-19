using System.Linq;
using FsCheck;
using FsCheck.Xunit;
using Tk.Toolkit.Cli.Usernames;

namespace Tk.Toolkit.Cli.Tests.Unit.Usernames
{
    public class UsernameGeneratorTests
    {        
        [Property(Verbose = true)]
        public bool Generate_GeneratesNonEmptyString(PositiveInt x)
        {
            var gen = new UsernameGenerator(new WordProvider());

            var un = gen.Generate();

            return !string.IsNullOrWhiteSpace(un);
        }

        [Property(Verbose = true, MaxTest = 1000)]
        public bool Generate_HasNoInvalidCharacters(PositiveInt x)
        {
            var gen = new UsernameGenerator(new WordProvider());
            var un = gen.Generate();
            var cs = new[] { ' ', '-' };

            return !un.Any(c => cs.Contains(c));
        }
    }
}
