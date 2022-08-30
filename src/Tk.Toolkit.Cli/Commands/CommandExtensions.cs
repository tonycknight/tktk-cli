using System.Diagnostics;

namespace tkdevcli.Commands
{
    internal static class CommandExtensions
    {
        [DebuggerStepThrough]
        public static int ToReturnCode(this bool value) => value ? 0 : 1;
    }
}