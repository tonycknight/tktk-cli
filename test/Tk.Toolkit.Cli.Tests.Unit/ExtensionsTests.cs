using System.Collections.Generic;
using System.Linq;
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


        [Property(Verbose = true)]
        public bool ToSpectreList_RowsMapped(IList<NonEmptyString> values)
        {
            var result = values.Select(s => s.Get).ToSpectreList();

            return result.Rows.Count == values.Count;
        }

        [Property(Verbose = true)]
        public bool ToSpectreColumns_RowsMapped(IList<(NonEmptyString, NonEmptyString)> values)
        {
            var result = values.Select(s => (s.Item1.Get, s.Item2.Get)).ToSpectreColumns();

            return result.Rows.Count == values.Count;
        }
    }
}
