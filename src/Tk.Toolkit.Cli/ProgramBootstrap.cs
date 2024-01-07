using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Tk.Nuget;

namespace Tk.Toolkit.Cli
{
    internal static class ProgramBootstrap
    {
        public static IServiceProvider CreateServiceCollection() =>
           new ServiceCollection()
                .AddSingleton<IRng, Rng>()
                .AddSingleton<Passwords.IPasswordGenerator, Passwords.CryptoPasswordGenerator>()
                .AddSingleton<Usernames.IUsernameGenerator, Usernames.UsernameGenerator>()
                .AddSingleton<Waffle.IPhraseProvider, Waffle.PhraseProvider>()
                .AddSingleton<Usernames.IWordProvider, Usernames.WordProvider>()
                .AddSingleton<IAnsiConsole>(sp => AnsiConsole.Create(new AnsiConsoleSettings() { ColorSystem = ColorSystemSupport.TrueColor }))
                .AddNugetClient()
                .AddSingleton<Conversions.INumericValueConverter, Conversions.NumericValueConverter>()
                .BuildServiceProvider();

        public static string? GetAppVersion()
            => Assembly.GetExecutingAssembly()
                       .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
    }
}
