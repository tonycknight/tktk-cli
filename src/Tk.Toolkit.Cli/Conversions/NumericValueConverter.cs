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
            throw new ArgumentException($"Unrecognised type {value.GetType()}");
        }

        public NumericValue Parse(string value)
        {            
            if(long.TryParse(value, NumberStyles.Integer, null, out long result))
            {
                return new DecimalValue() { Value = result.ToString() };
            }


            if (value.StartsWith("0x") || value.StartsWith("0X"))
            {
                value = TrimHexadecimalPrefix(value);
                if (long.TryParse(value, NumberStyles.HexNumber, null, out result))
                {
                    return new HexadecimalValue() { Value = $"0x{value}" };
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
            var val = TrimHexadecimalPrefix(value.Value);
#pragma warning restore CS8604 // Possible null reference argument.

            var v = long.Parse(val, NumberStyles.HexNumber).ToString();
            return new DecimalValue() { Value = v };
        }

        private string TrimHexadecimalPrefix(string value)
        {
            return value.StartsWith("0x") || value.StartsWith("0X")
                ? value.Substring(2)
                : value;
        }
    }
}
