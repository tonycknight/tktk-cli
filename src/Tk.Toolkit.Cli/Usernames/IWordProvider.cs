namespace Tk.Toolkit.Cli.Usernames
{
    internal interface IWordProvider
    {
        IList<string> GetNouns();
        IList<string> GetAdjectives();
    }
}
