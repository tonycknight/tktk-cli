using System.Diagnostics.CodeAnalysis;
using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using Tk.Extensions;

namespace Tk.Toolkit.Cli.Commands
{
    [Command("about", Description = "About the app")]
    internal class AboutCommand
    {
        private readonly IAnsiConsole _console;

        public AboutCommand(IAnsiConsole console)
        {
            _console = console;
        }

        [ExcludeFromCodeCoverage]
        public async Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            var currentVersion = ProgramBootstrap.GetAppVersion();
            var nugetVersion = await ProgramBootstrap.GetCurrentNugetVersion();
            var descLines = new List<string>()
            {
                $"[cyan]{app.Parent?.Name}[/]",
                "[cyan]An eclectic developer toolkit[/]",
                $"[yellow]Version {currentVersion} beta[/]",
            };
            
            if (nugetVersion != null && currentVersion != nugetVersion)
            {
                descLines.Add($"[magenta]An upgrade is available: {nugetVersion}[/]");
            }

            var desc = new Markup(descLines.Join(Environment.NewLine) + Environment.NewLine);

            _console.Write(desc);
            
            app.Parent?.ShowHelp();

            return 0;
        }
    }
}
