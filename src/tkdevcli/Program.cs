using System;
using McMaster.Extensions.CommandLineUtils;
using tkdevcli.Commands;

namespace tkdevcli
{
    [Subcommand(typeof(GuidGeneratorCommand))]
    [Subcommand(typeof(PasswordGeneratorCommand))]
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
            app.Description = "tkdev";
            app.ShowHelp();
            return true.ToReturnCode();
        }
    }
}