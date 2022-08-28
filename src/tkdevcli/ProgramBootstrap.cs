using Microsoft.Extensions.DependencyInjection;

namespace tkdevcli
{
    internal class ProgramBootstrap
    {
        public static IServiceProvider CreateServiceCollection() =>
           new ServiceCollection()                
                .AddSingleton<Io.IConsoleWriter, Io.ConsoleWriter>()
                .AddSingleton<Passwords.IPasswordGenerator, Passwords.CryptoPasswordGenerator>()
                .BuildServiceProvider();
    }
}
