using McMaster.Extensions.CommandLineUtils;
using tkdevcli.Io;

namespace tkdevcli.Commands
{
    [Command("pw", Description = "Generate passwords")]
    internal class PasswordGeneratorCommand
    {
        private readonly IConsoleWriter _consoleWriter;

        public PasswordGeneratorCommand(IConsoleWriter consoleWriter)
        {
            _consoleWriter = consoleWriter;
        }

        [Option(CommandOptionType.SingleValue, Description = "The number of passwords to generate.", LongName = "gen", ShortName = "g")]
        public int Generations { get; set; }

        [Option(CommandOptionType.SingleValue, Description = "The character length of each generated password.", LongName = "len", ShortName = "l")]
        public int PwLength { get; set; }

        public Task<int> OnExecuteAsync()
        {
            // TODO:

            return Task.FromResult(true.ToReturnCode());
        }
    }
}
