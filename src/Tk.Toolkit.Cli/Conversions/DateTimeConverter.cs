namespace Tk.Toolkit.Cli.Conversions
{
    internal static class DateTimeExtensions
    {
        public static DateTimeOffset FromEpoch(this long value) => DateTimeOffset.FromUnixTimeSeconds(value);

        public static long ToEpoch(this DateTimeOffset value) => value.ToUnixTimeSeconds();
    }
}
