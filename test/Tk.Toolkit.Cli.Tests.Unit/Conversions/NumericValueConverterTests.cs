using System;
using System.Linq;
using FsCheck;
using FsCheck.Xunit;
using Tk.Toolkit.Cli.Conversions;

namespace Tk.Toolkit.Cli.Tests.Unit.Conversions
{
    public class NumericValueConverterTests
    {
        [Property(Verbose = true)]
        public bool Parse_ValidIntegers(PositiveInt val)
        {
            var value = val.Get.ToString();
            var conv = new NumericValueConverter();

            var result = conv.Parse(value);

            return result is DecimalValue && result.Value == value;
        }

        [Property(Verbose = true)]
        public bool Parse_ValidHexadecimals(PositiveInt val)
        {
            var value = $"0x{val.Get.ToString("X2")}";
            var conv = new NumericValueConverter();

            var result = conv.Parse(value);

            return result is HexadecimalValue && result.Value == value;
        }
        
        [Property(Verbose = true)]
        public bool Parse_InvalidIntegers(Guid val)
        {
            var value = val.ToString();
            var conv = new NumericValueConverter();

            try
            {
                var result = conv.Parse(value);
                return false;
            }
            catch
            {
                return true;
            }
        }

        [Property(Verbose = true)]
        public bool Parse_Convert_DecimalToHexToDecimal(PositiveInt val)
        {
            var value = val.Get.ToString();
            var conv = new NumericValueConverter();

            var dec = conv.Parse(value);

            var hex = conv.Convert(dec).Single();

            var result = conv.Convert(hex).Single();

            return result is DecimalValue && result.Value == value;
        }

        [Property(Verbose = true)]
        public bool Parse_Convert_HexToDecimalToHex(PositiveInt val)
        {            
            var value = $"0x{val.Get.ToString("X2")}";
            var conv = new NumericValueConverter();

            var hex = conv.Parse(value);

            var dec = conv.Convert(hex).Single();

            var result = conv.Convert(dec).Single();

            return result is HexadecimalValue && result.Value == value;
        }
    }
}
