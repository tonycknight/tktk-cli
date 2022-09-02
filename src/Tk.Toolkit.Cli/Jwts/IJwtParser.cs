namespace Tk.Toolkit.Cli.Jwts
{
    internal interface IJwtParser
    {
        IEnumerable<(string, string)> Parse(string jwt);
    }
}
