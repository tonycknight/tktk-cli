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
                return new NumericValue[]
                {
                    ConvertDecToHex(dec),
                    ConvertDecToBin(dec)
                };                
            }
            else if(value is HexadecimalValue hex)
            {
                return new NumericValue[]
                {
                    ConvertHexToDec(hex),
                    ConvertHexToBin(hex),
                };
            }
            else if(value is BinaryValue bin)
            {
                return new NumericValue[] 
                {
                    ConvertBinToDec(bin),
                    ConvertBinToHex(bin),
                };
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
            var v = $"0x{long.Parse(value.Value!).ToString("X2")}";
            return new HexadecimalValue() { Value = v };
        }

        private NumericValue ConvertHexToDec(HexadecimalValue value)
        {
            var val = TrimPrefix(value.Value!, "0x");

            var v = long.Parse(val, NumberStyles.HexNumber).ToString();
            return new DecimalValue() { Value = v };
        }

        private NumericValue ConvertBinToHex(BinaryValue value)
        {
            var val = TrimPrefix(value.Value!, "0b");
            var v = $"0x{System.Convert.ToInt64(val, 2).ToString("X2")}";
            
            return new HexadecimalValue() { Value = v };
        }

        private NumericValue ConvertBinToDec(BinaryValue value)
        {
            var val = TrimPrefix(value.Value!, "0b");
            var v = System.Convert.ToInt64(val, 2).ToString();
            return new DecimalValue() { Value = v };
        }

        private NumericValue ConvertDecToBin(DecimalValue value)
        {
            var x = long.Parse(value.Value!);
            var v = $"0b{System.Convert.ToString(x, 2)}";

            return new BinaryValue() { Value = v }; 
        }

        private NumericValue ConvertHexToBin(HexadecimalValue value)
        {
            var val = TrimPrefix(value.Value!, "0x");

            var v = long.Parse(val, NumberStyles.HexNumber).ToString();

            return ConvertDecToBin(new DecimalValue() {  Value = v});
        }

        private string TrimPrefix(string value, string prefix)
        {
            return value.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase) 
                ? value.Substring(prefix.Length)
                : value;
        }
    }
}
