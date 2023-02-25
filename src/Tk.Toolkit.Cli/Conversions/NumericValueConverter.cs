using System.Globalization;

namespace Tk.Toolkit.Cli.Conversions
{
    internal class NumericValueConverter : INumericValueConverter
    {
        public IEnumerable<NumericValue> Convert(NumericValue value)
        {
            if (value is DecimalValue dec)
            {
                return new NumericValue[]
                {
                    ConvertDecToHex(dec),
                    ConvertDecToBin(dec)
                };
            }
            else if (value is HexadecimalValue hex)
            {
                return new NumericValue[]
                {
                    ConvertHexToDec(hex),
                    ConvertHexToBin(hex),
                };
            }
            else if (value is BinaryValue bin)
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

            if (long.TryParse(value, NumberStyles.Integer, null, out long result))
            {
                return new DecimalValue(result.ToString());
            }


            if (value.StartsWith(HexadecimalValue.Prefix, StringComparison.InvariantCultureIgnoreCase))
            {
                value = value.TrimPrefix(HexadecimalValue.Prefix);
                if (long.TryParse(value, NumberStyles.HexNumber, null, out result))
                {
                    return new HexadecimalValue(value);
                }
            }

            if (value.StartsWith(BinaryValue.Prefix, StringComparison.InvariantCultureIgnoreCase))
            {
                value = value.TrimPrefix(BinaryValue.Prefix);
                try
                {
                    var _ = System.Convert.ToInt64(value, 2);
                    return new BinaryValue(value);
                }
                catch (FormatException)
                {
                }
            }

            throw new ArgumentException("Unrecognised value");

        }

        private NumericValue ConvertDecToHex(DecimalValue value) => ConvertToHex(long.Parse(value.Value));

        private NumericValue ConvertToDec(long value) => new DecimalValue(value.ToString());

        private NumericValue ConvertToHex(long value) => new HexadecimalValue($"{value:X2}");

        private NumericValue ConvertToBin(long value) => new BinaryValue(System.Convert.ToString(value, 2));

        private NumericValue ConvertHexToDec(HexadecimalValue value)
        {
            var val = value.TrimValue();

            return ConvertToDec(long.Parse(val, NumberStyles.HexNumber));
        }

        private NumericValue ConvertBinToHex(BinaryValue value)
        {
            var val = value.TrimValue();

            return ConvertToHex(System.Convert.ToInt64(val, 2));
        }

        private NumericValue ConvertBinToDec(BinaryValue value)
        {
            var val = value.TrimValue();
            return ConvertToDec(System.Convert.ToInt64(val, 2));
        }

        private NumericValue ConvertDecToBin(DecimalValue value) => ConvertToBin(long.Parse(value.Value));

        private NumericValue ConvertHexToBin(HexadecimalValue value)
        {
            var val = value.TrimValue();
            var dec = (DecimalValue)ConvertToDec(long.Parse(val, NumberStyles.HexNumber));
            return ConvertDecToBin(dec);
        }
    }
}
