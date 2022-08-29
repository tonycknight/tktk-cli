namespace tkdevcli
{
    internal static class Extensions
    {
        public static int ApplyDefault(this int value, Func<int, bool> applyDefault, int defaultValue) 
            => applyDefault(value) ? defaultValue : value;
    }
}
