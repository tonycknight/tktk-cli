using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using Tk.Toolkit.Cli.TextFormatting;

namespace Tk.Toolkit.Cli.Commands;

[Command("altcase", Description = "Alternating case")]
internal class AltCaseCommand(IAnsiConsole console, AltCaseTextFormatter formatter)
{
    [Argument(0, Description = "The text to convert.", Name = "value")]
    public string Value { get; set; } = "";

    public int OnExecute()
    {
        var result = formatter.Format(Value);

        console.WriteLine(result);

        return 0;
    }
}

