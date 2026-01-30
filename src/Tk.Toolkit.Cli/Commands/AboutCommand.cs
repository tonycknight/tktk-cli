using System.Diagnostics.CodeAnalysis;
using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using Tk.Extensions;
using Tk.Nuget;

namespace Tk.Toolkit.Cli.Commands
{
    [ExcludeFromCodeCoverage]
    [Command("about", Description = "About the app")]
    internal class AboutCommand
    {
        private readonly IAnsiConsole _console;
        private readonly INugetClient _nuget;

        public AboutCommand(IAnsiConsole console, INugetClient nuget)
        {
            _console = console;
            _nuget = nuget;
        }

        public async Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            var currentVersion = ProgramBootstrap.GetAppVersion();
            var descLines = new List<string>()
            {
                $"[cyan]{app.Parent?.Name}[/]",
                "[cyan]An eclectic developer toolkit[/]",
                $"[yellow]Version {currentVersion} beta[/]",
                $"[yellow]Repo:[/] [white]https://github.com/tonycknight/tktk-cli [/]",
            };
            if (currentVersion != null)
            {
                var nugetVersion = await _nuget.GetUpgradeVersionAsync("tktk-cli", currentVersion, false);
                if (nugetVersion != null)
                {
                    descLines.Add($"[magenta]An upgrade is available: {nugetVersion}[/]");
                }
            }

            var desc = new Markup(descLines.Join(Environment.NewLine) + Environment.NewLine);

            _console.Write(desc);

            app.Parent?.ShowHelp();

            return 0;
        }
    }
}
