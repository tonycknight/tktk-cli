namespace tkdevcli.Io
{
    internal interface IConsoleWriter
    {
        void Write(string line);
        void WriteMany(IEnumerable<string> lines);
    }
}
