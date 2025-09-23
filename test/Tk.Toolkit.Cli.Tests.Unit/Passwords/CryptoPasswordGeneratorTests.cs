using System.Linq;
using FsCheck;
using FsCheck.Xunit;
using Tk.Toolkit.Cli.Passwords;

namespace Tk.Toolkit.Cli.Tests.Unit.Passwords
{
    public class CryptoPasswordGeneratorTests
    {
        [Property(Verbose = true, Arbitrary = [typeof(PasswordLengths)])]
        public bool PasswordGenerator_AlwaysReturnsCorrectLength(int len)
        {
            var pg = new CryptoPasswordGenerator();

            var pw = pg.Generate(len);

            return pw.Length == len;
        }

        [Property(Verbose = true, Arbitrary = [typeof(PasswordLengths)])]
        public bool PasswordGenerator_AlwaysReturnsAlphanumeric(int len)
        {
            var pg = new CryptoPasswordGenerator();

            var pw = pg.Generate(len);

            return pw.All(c => char.IsLetterOrDigit(c) || "!?@".Contains(c));
        }
    }
}
