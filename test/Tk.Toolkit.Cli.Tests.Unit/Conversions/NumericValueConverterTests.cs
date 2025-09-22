using System;
using System.Collections.Generic;
using System.Linq;
using FsCheck;
using FsCheck.Xunit;
using Shouldly;
using Tk.Toolkit.Cli.Conversions;
using Xunit;

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
        public bool Parse_ValidBinaries(PositiveInt val)
        {
            var value = $"0x{System.Convert.ToString(val.Get, 2)}";
            var conv = new NumericValueConverter();

            var result = conv.Parse(value);

            return result is HexadecimalValue && result.Value == value;
        }

        [Property(Verbose = true)]
        public bool Parse_InvalidIntegers_ExceptionThrown(Guid val)
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

        [Fact]
        public void Parse_InvalidValue_ExceptionThrown()
        {
            var value = "aaa";
            var conv = new NumericValueConverter();

            Func<NumericValue> f = () => conv.Parse(value);

            Should.Throw<ArgumentException>(() => f());
        }

        [Property(Verbose = true)]
        public bool Parse_Convert_DecimalToHexToDecimal(PositiveInt val)
        {
            var value = val.Get.ToString();
            var conv = new NumericValueConverter();

            var dec = conv.Parse(value);

            var hex = conv.Convert(dec).OfType<HexadecimalValue>().Single();

            var result = conv.Convert(hex).OfType<DecimalValue>().Single();

            return result.Value == value;
        }

        [Property(Verbose = true)]
        public bool Parse_Convert_DecimalProducesHexAndBinary(PositiveInt val)
        {
            var value = val.Get.ToString();
            var conv = new NumericValueConverter();

            var dec = conv.Parse(value);

            var results = conv.Convert(dec).ToList();

            var hex = results.OfType<HexadecimalValue>().Single();
            var bin = results.OfType<BinaryValue>().Single();

            return !string.IsNullOrEmpty(hex.Value) && !string.IsNullOrEmpty(bin.Value);
        }

        [Property(Verbose = true)]
        public bool Parse_Convert_HexToDecimalToHex(PositiveInt val)
        {
            var value = $"0x{val.Get.ToString("X2")}";
            var conv = new NumericValueConverter();

            var hex = conv.Parse(value);

            var dec = conv.Convert(hex).OfType<DecimalValue>().Single();

            var result = conv.Convert(dec).OfType<HexadecimalValue>().Single();

            return result.Value == value;
        }


        [Property(Verbose = true)]
        public bool Parse_Convert_HexProducesDecimalAndBinary(PositiveInt val)
        {
            var value = $"0x{val.Get.ToString("X2")}";
            var conv = new NumericValueConverter();

            var hex = conv.Parse(value);

            var results = conv.Convert(hex).ToList();

            var dec = results.OfType<DecimalValue>().Single();
            var bin = results.OfType<BinaryValue>().Single();

            return !string.IsNullOrEmpty(dec.Value) && !string.IsNullOrEmpty(bin.Value);
        }

        [Property(Verbose = true)]
        public bool Parse_Convert_BinaryToHexToDecimal(PositiveInt val)
        {
            var value = $"0b{System.Convert.ToString(val.Get, 2)}";
            var conv = new NumericValueConverter();

            var bin = conv.Parse(value);

            var hex = conv.Convert(bin).OfType<HexadecimalValue>().Single();

            var result = conv.Convert(hex).OfType<DecimalValue>().Single();

            return result.Value == val.Get.ToString();
        }


        [Property(Verbose = true)]
        public bool Parse_Convert_BinaryProducesHexAndBinary(PositiveInt val)
        {
            var value = $"0b{System.Convert.ToString(val.Get, 2)}";
            var conv = new NumericValueConverter();

            var bin = conv.Parse(value);

            var results = conv.Convert(bin).ToList();

            var hex = results.OfType<HexadecimalValue>().Single();
            var dec = results.OfType<DecimalValue>().Single();

            return !string.IsNullOrEmpty(dec.Value) && !string.IsNullOrEmpty(hex.Value);
        }



        [Fact]
        public void Convert_UnknownType_ExceptionThrown()
        {
            var s = new UnrecognisedNumericValue("");

            var conv = new NumericValueConverter();
            Func<IEnumerable<NumericValue>> f = () => conv.Convert(s);
            f.ShouldThrow<ArgumentException>();
        }

    }
}
