using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using Tk.Toolkit.Cli.Usernames;

namespace Tk.Toolkit.Cli.Commands
{
    [Command("un", Description = "Generate user names")]
    internal class UsernameGeneratorCommand
    {
        private readonly IAnsiConsole _console;
        private readonly IUsernameGenerator _unGenerator;
        internal const int DefaultUsernameLength = 16;
        internal const int DefaultUsernameCount = 5;

        public UsernameGeneratorCommand(IAnsiConsole console, IUsernameGenerator unGenerator)
        {
            _console = console;
            _unGenerator = unGenerator;
        }

        [Option(CommandOptionType.SingleValue, Description = "The number of usernames to generate.", LongName = "gen", ShortName = "g")]
        public int Generations { get; set; } = DefaultUsernameCount;

        public int OnExecute()
        {
            var generations = Generations.ApplyDefault(x => x < 1, DefaultUsernameCount);
            
            var pws = Enumerable.Range(0, generations)
                                .Select(_ => _unGenerator.Generate())
                                .ToSpectreList();

            _console.Write(pws);

            return true.ToReturnCode();
        }
    }
}
