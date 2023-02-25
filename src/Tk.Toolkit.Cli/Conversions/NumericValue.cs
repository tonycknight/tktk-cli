namespace Tk.Toolkit.Cli.Conversions
{
    internal abstract class NumericValue
    {
        public NumericValue(string value) => Value = value;

        public string Value { get; }
        public abstract string TrimValue();
    }

    internal sealed class DecimalValue : NumericValue
    {
        public DecimalValue(string value) : base(value)
        {
        }
        public override string TrimValue() => Value;
    }

    internal sealed class HexadecimalValue : NumericValue
    {
        public const string Prefix = "0x";

        public HexadecimalValue(string value) : base(value.EnsurePrefixed(Prefix))
        {
        }
        public override string TrimValue() => Value.TrimPrefix(Prefix);
    }

    internal sealed class BinaryValue : NumericValue
    {
        public const string Prefix = "0b";

        public BinaryValue(string value) : base(value.EnsurePrefixed(Prefix))
        {
        }
        public override string TrimValue() => Value.TrimPrefix(Prefix);
    }
}
