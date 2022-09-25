using Tk.Toolkit.Cli.Conversions;

namespace Tk.Toolkit.Cli.Tests.Unit.Conversions
{
    internal class UnrecognisedNumericValue : NumericValue
    {
        public UnrecognisedNumericValue(string value) : base(value)
        {
        }

        public override string TrimValue() => "";
    }
}
