using FsCheck;
using FsCheck.Xunit;
using Tk.Toolkit.Cli;

namespace Tk.Toolkit.Cli.Tests.Unit
{
    public class ExtensionsTests
    {
        [Property(Verbose = true)]
        public bool ApplyDefault_DefaultApplied(NegativeInt value, PositiveInt defaultValue)
        {
            var r = value.Get.ApplyDefault(x => x <= 0, defaultValue.Get);

            return r == defaultValue.Get;
        }

        [Property(Verbose = true)]
        public bool ApplyDefault_DefaultNotApplied(PositiveInt value, PositiveInt defaultValue)
        {
            var r = value.Get.ApplyDefault(x => x <= 0, defaultValue.Get);

            return r == value.Get;
        }
    }
}
