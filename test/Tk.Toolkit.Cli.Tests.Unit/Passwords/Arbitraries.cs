using System.Linq;
using FsCheck;
using FsCheck.Fluent;

namespace Tk.Toolkit.Cli.Tests.Unit.Passwords
{
    internal static class PasswordLengths
    {
        public static Arbitrary<int> GetGenerator() =>
            ArbMap.Default.ArbFor<int>().Generator
                .Where(x => x > 4)
                .ToArbitrary();
    }
}
