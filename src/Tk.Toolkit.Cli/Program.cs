using McMaster.Extensions.CommandLineUtils;
using Tk.Toolkit.Cli.Commands;
using Tk.Extensions;
using System.Diagnostics.CodeAnalysis;
using Tk.Extensions.Tasks;

namespace Tk.Toolkit.Cli
{
    [ExcludeFromCodeCoverage]
    [Subcommand(typeof(GuidGeneratorCommand))]
    [Subcommand(typeof(PasswordGeneratorCommand))]
    [Subcommand(typeof(DecodeJwtCommand))]
    [Subcommand(typeof(ConvertNumberCommand))]
    [Subcommand(typeof(EpochCommand))]
    public class Program
    {
        public static int Main(string[] args)
        {
            using var app = new CommandLineApplication<Program>();

            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection(ProgramBootstrap.CreateServiceCollection());

            try
            {
                return app.Execute(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(Crayon.Output.Bright.Red(ex.Message));
                return false.ToReturnCode();
            }
        }
         

        private async Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            var currentVersion = ProgramBootstrap.GetAppVersion();
            var nugetVersion = await new Nuget.NugetClient().GetLatestNugetVersionAsync("tktk-cli");
            var descLines = new List<string>()
            {
                Crayon.Output.Bright.Cyan("tktk"),
                Crayon.Output.Bright.Cyan("An eclectic developer toolkit"),
                Crayon.Output.Bright.Yellow($"Version {currentVersion} beta"),
            };

            if (nugetVersion != null && currentVersion != nugetVersion)
            {
                descLines.Add(Crayon.Output.Bright.Magenta($"An upgrade is available: {nugetVersion}"));
            }

            app.Description = descLines.Join(Environment.NewLine);
            app.ShowHelp();

            return true.ToReturnCode();
        }
    }
}