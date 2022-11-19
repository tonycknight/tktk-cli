using McMaster.Extensions.CommandLineUtils;
using Tk.Toolkit.Cli.Passwords;
using Spectre.Console;

namespace Tk.Toolkit.Cli.Commands
{
    [Command("pw", Description = "Generate passwords")]
    internal class PasswordGeneratorCommand
    {
        private readonly IAnsiConsole _console;
        private readonly IPasswordGenerator _pwGenerator;
        internal const int DefaultPasswordLength = 32;
        internal const int DefaultPasswordCount = 5;

        public PasswordGeneratorCommand(IAnsiConsole console, IPasswordGenerator pwGenerator)
        {
            _console = console;
            _pwGenerator = pwGenerator;
        }

        [Option(CommandOptionType.SingleValue, Description = "The number of passwords to generate.", LongName = "gen", ShortName = "g")]
        public int Generations { get; set; } = DefaultPasswordCount;

        [Option(CommandOptionType.SingleValue, Description = "The character length of each generated password.", LongName = "len", ShortName = "l")]
        public int PwLength { get; set; } = DefaultPasswordLength;

        public int OnExecute()
        {
            var generations = Generations.ApplyDefault(x => x < 1, DefaultPasswordCount);
            var pwLen = PwLength.ApplyDefault(x => x < 1, DefaultPasswordLength);

            var pws = Enumerable.Range(0, generations)
                                .Select(_ => _pwGenerator.Generate(pwLen))
                                .ToSpectreList();

            _console.Write(pws);

            return true.ToReturnCode();
        }
    }
}
