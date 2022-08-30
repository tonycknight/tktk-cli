using McMaster.Extensions.CommandLineUtils;
using Tk.Toolkit.Cli.Commands;
using Tk.Extensions;

namespace Tk.Toolkit.Cli
{
    [Subcommand(typeof(GuidGeneratorCommand))]
    [Subcommand(typeof(PasswordGeneratorCommand))]
    [Subcommand(typeof(DecodeJwtCommand))]
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


        private int OnExecute(CommandLineApplication app)
        {
            var currentVersion = ProgramBootstrap.GetAssembly().GetAppVersion();
            var nugetVersion = new NugetClient().GetLatestNugetVersion();
            var descLines = new List<string>()
            {
                Crayon.Output.Bright.Cyan("tktk"),
                Crayon.Output.Bright.Cyan("An eclectic developer toolkit"),
                Crayon.Output.Bright.Yellow($"Version {currentVersion}"),
            };

            if (currentVersion != nugetVersion)
            {
                descLines.Add(Crayon.Output.Bright.Magenta($"An upgrade is available: {nugetVersion}"));
            }

            app.Description = descLines.Join(Environment.NewLine);
            app.ShowHelp();
            return true.ToReturnCode();
        }
    }
}