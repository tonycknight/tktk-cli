using System.Linq;
using FsCheck;
using FsCheck.Xunit;
using Tk.Toolkit.Cli.Passwords;

namespace Tk.Toolkit.Cli.Tests.Unit.Passwords
{
    public class CryptoPasswordGeneratorTests
    {
        [Property(Verbose = true)]
        public bool PasswordGenerator_AlwaysReturnsCorrectLength(PositiveInt len)
        {
            var pg = new CryptoPasswordGenerator();

            var pw = pg.Generate(len.Get);

            return pw.Length == len.Get;
        }

        [Property(Verbose = true)]
        public bool PasswordGenerator_AlwaysReturnsAlphanumeric(PositiveInt len)
        {
            var pg = new CryptoPasswordGenerator();

            var pw = pg.Generate(len.Get);

            return pw.All(char.IsLetterOrDigit);
        }
    }
}
