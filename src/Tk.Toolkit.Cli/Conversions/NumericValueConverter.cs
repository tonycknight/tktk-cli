using System.Globalization;
using Tk.Extensions.Linq;

namespace Tk.Toolkit.Cli.Conversions
{
    internal class NumericValueConverter : INumericValueConverter
    {
        public IEnumerable<NumericValue> Convert(NumericValue value)
        {
            if(value is DecimalValue dec)
            {   
                return ConvertDecToHex(dec).Singleton();
            }
            else if(value is HexadecimalValue hex)
            {
                return ConvertHexToDec(hex).Singleton();
            }
            else if(value is BinaryValue bin)
            {
                return ConvertBinToHex(bin).Singleton();
            }
            throw new ArgumentException($"Unrecognised type {value.GetType()}");
        }

        public NumericValue Parse(string value)
        {            
            if(long.TryParse(value, NumberStyles.Integer, null, out long result))
            {
                return new DecimalValue() { Value = result.ToString() };
            }


            if (value.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase))
            {
                value = TrimPrefix(value, "0x");
                if (long.TryParse(value, NumberStyles.HexNumber, null, out result))
                {
                    return new HexadecimalValue() { Value = $"0x{value}" };
                }
            }

            if(value.StartsWith("0b", StringComparison.InvariantCultureIgnoreCase))
            {
                value = TrimPrefix(value, "0b");                
                try 
                { 
                    var x = System.Convert.ToInt64(value, 2);                    
                    return new BinaryValue() { Value = $"0b{value}" };
                }
                catch (FormatException)
                {
                }
            }

            throw new ArgumentException("Unrecognised value");

        }
    
        private NumericValue ConvertDecToHex(DecimalValue value) 
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var v = $"0x{long.Parse(value.Value).ToString("X2")}";
#pragma warning restore CS8604 // Possible null reference argument.
            return new HexadecimalValue() { Value = v };
        }

        private NumericValue ConvertHexToDec(HexadecimalValue value)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var val = TrimPrefix(value.Value, "0x");
#pragma warning restore CS8604 // Possible null reference argument.

            var v = long.Parse(val, NumberStyles.HexNumber).ToString();
            return new DecimalValue() { Value = v };
        }

        private NumericValue ConvertBinToHex(BinaryValue value)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var val = TrimPrefix(value.Value, "0b");
#pragma warning restore CS8604 // Possible null reference argument.
            var v = $"0x{System.Convert.ToInt64(val, 2).ToString("X2")}";
            
            return new HexadecimalValue() { Value = v };
        }

        private string TrimPrefix(string value, string prefix)
        {
            return value.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase) 
                ? value.Substring(prefix.Length)
                : value;
        }
    }
}
