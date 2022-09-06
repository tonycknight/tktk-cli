namespace Tk.Toolkit.Cli.Conversions
{
    internal abstract class NumericValue
    {
        public string? Value { get; set; }
    }

    internal sealed class DecimalValue : NumericValue
    {
        
    }

    internal sealed class HexadecimalValue : NumericValue
    {
        
    }

    internal sealed class BinaryValue : NumericValue
    {

    }
}
