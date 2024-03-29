﻿using McMaster.Extensions.CommandLineUtils;
using Tk.Toolkit.Cli.Commands;
using Tk.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace Tk.Toolkit.Cli
{
    [ExcludeFromCodeCoverage]
    [Subcommand(typeof(GuidGeneratorCommand))]
    [Subcommand(typeof(PasswordGeneratorCommand))]
    [Subcommand(typeof(ConvertNumberCommand))]
    [Subcommand(typeof(GenerateWaffleCommand))]
    [Subcommand(typeof(UsernameGeneratorCommand))]
    [Subcommand(typeof(AboutCommand))]
    public class Program
    {
        public static int Main(string[] args)
        {
            using var app = new CommandLineApplication<Program>()
            {
                UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.Throw,
                MakeSuggestionsInErrorMessage = true,
            };

            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection(ProgramBootstrap.CreateServiceCollection());

            try
            {
                return app.Execute(args);
            }
            catch (UnrecognizedCommandParsingException ex)
            {
                Console.WriteLine(Crayon.Output.Bright.Red(ex.Message));
                var possibleMatches = ex.NearestMatches.Select(m => $"{app.Name} {m}").Join(Environment.NewLine);
                if (possibleMatches.Length > 0)
                {
                    Console.WriteLine(Crayon.Output.Bright.Yellow($"Did you mean one of these commands?{Environment.NewLine}{possibleMatches}"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(Crayon.Output.Bright.Red(ex.Message));
            }
            return false.ToReturnCode();
        }


        private int OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();

            return true.ToReturnCode();
        }
    }
}