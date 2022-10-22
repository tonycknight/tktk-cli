using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using Tk.Toolkit.Cli.Waffle;

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

        [Option(CommandOptionType.SingleValue, Description = "The RNG's seed integer.", LongName = "seed", ShortName = "s")]
        public int? Seed { get; set; }

        [Option(CommandOptionType.SingleValue, Description = "The render mode.", LongName = "render", ShortName = "r")]
        public RenderMode Render { get; set; } = RenderMode.Text;

        public int OnExecute()
        {
            var rng = Seed.HasValue ? new Rng(Seed.Value) : new Rng();
            var gen = new WaffleGenerator(rng);
                        
            var sb = gen.Generate(Paragraphs, IncludeTitle, Render);

            _console.Write(sb.ToString());

            return true.ToReturnCode();
        }
    }
}
