using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;

namespace Tk.Toolkit.Cli.Commands
{
    [Command("waffle", Description = "Generate waffle")]
    internal class GenerateWaffleCommand
    {
        private readonly IAnsiConsole _console;

        public GenerateWaffleCommand(IAnsiConsole console)
        {
            _console = console;
        }

        [Option(CommandOptionType.SingleValue, Description = "The number of paragraphs to generate.", LongName = "paragraphs", ShortName = "p")]
        public int Paragraphs { get; set; } = 1;

        [Option(CommandOptionType.NoValue, Description = "Include a title", LongName = "title", ShortName = "t")]
        public bool IncludeTitle { get; set; }

        public int OnExecute()
        {
            var e = new Waffle.WaffleGenerator();
                        
            var sb = e.Generate(Paragraphs, IncludeTitle);

            _console.Write(sb.ToString());

            return true.ToReturnCode();
        }
    }
}
