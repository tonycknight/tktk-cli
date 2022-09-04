namespace Tk.Toolkit.Cli.Conversions
{
    internal interface INumericValueConverter
    {
        NumericValue Parse(string value);

        IEnumerable<NumericValue> Convert(NumericValue value);
    }
}
