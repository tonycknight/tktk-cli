using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace Tk.Toolkit.Cli
{
    internal static class ProgramBootstrap
    {
        public static IServiceProvider CreateServiceCollection() =>
           new ServiceCollection()
                .AddSingleton<Passwords.IPasswordGenerator, Passwords.CryptoPasswordGenerator>()
                .AddSingleton<Jwts.IJwtParser, Jwts.JwtParser>()
                .AddSingleton<IAnsiConsole>(sp => AnsiConsole.Create(new AnsiConsoleSettings() {  ColorSystem = ColorSystemSupport.TrueColor }))
                .AddSingleton<Nuget.INugetClient, Nuget.NugetClient>()
                .AddSingleton<Conversions.INumericValueConverter, Conversions.NumericValueConverter>()
                .BuildServiceProvider();

        public static string? GetAppVersion()
            => Assembly.GetExecutingAssembly()
                       .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
    }
}
