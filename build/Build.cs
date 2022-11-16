using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.MSBuild.MSBuildTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.GitVersion;
using Serilog;
using Nuke.Common.CI.GitHubActions;

class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.All);

    private const string SolutionFile = "tktk-cli.sln";
    
    [GitVersion]
    private readonly GitVersion GitVersion;

    private readonly GitHubActions Gha;

    private AbsolutePath SourceDirectory => RootDirectory / "src";
    private AbsolutePath TestsDirectory => RootDirectory / "test";
    private AbsolutePath PublishDirectory => RootDirectory / "publish";
    private AbsolutePath PackageDirectory => RootDirectory / "package";
    private AbsolutePath StrykerOutputDirectory => RootDirectory / "StrykerOutput";

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;
        
    [PathExecutable("dotnet")]
    readonly Tool DotNet;

    Target Start => _ => _
        .Executes(() =>
        {
            Log.Information("GitVersion.MajorMinorPatch = {Value}", GitVersion?.MajorMinorPatch);
            Log.Information("GitVersion.FullSemVer = {Value}", GitVersion?.FullSemVer);
            Log.Information("GitVersion.NuGetVersion = {Value}", GitVersion?.NuGetVersion);
            Log.Information("GitVersion.NuGetPreReleaseTag = {Value}", GitVersion?.NuGetPreReleaseTag);            
            Log.Information("GitVersion.FullBuildMetaData = {Value}", GitVersion?.FullBuildMetaData.ToString());

            Log.Information("GHA.RunNumber = {Value}", Gha?.RunNumber);
            Log.Information("GHA.RunId = {Value}", Gha?.RunId);
            Log.Information("GHA.JobId = {Value}", Gha?.JobId);


        });

    Target Clean => _ => _
        .DependsOn(Start)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj")
                           .ForEach(DeleteDirectory);
            TestsDirectory.GlobDirectories("**/bin", "**/obj", "**/TestResults")
                          .ForEach(DeleteDirectory);
            EnsureCleanDirectory(StrykerOutputDirectory);
            EnsureCleanDirectory(PublishDirectory);
            EnsureCleanDirectory(PackageDirectory);

        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() => DotNetRestore(s => s.SetProjectFile(SolutionFile)));

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
            DotNetBuild(x => x.SetProjectFile(SolutionFile)
                              .SetConfiguration(Configuration)
                              .EnableNoRestore())
        );
        
    Target Test => _ => _
        .DependsOn(Restore)   
        .Executes(() => {
            DotNetTest(x => x.SetCollectCoverage(true)
                             .SetCoverletOutput("TestResults") // TODO: not directing...
                             .SetLoggers("trx")
                             .SetCoverletOutputFormat(CoverletOutputFormat.cobertura)
                             .SetNoBuild(false)
                             .SetConfiguration("Debug"));
            
        }
        );

    Target ConsolidateCoverage => _ => _
        .DependsOn(Test)
        .Executes(() => 
            DotNet?.Invoke("reportgenerator -reports:./test/**/coverage.json -reporttypes:Html -targetdir:./publish/codecoverage")
                    );

    Target Pack => _ => _
        .DependsOn(Compile)
        .Executes(() => DotNetPack(x => x.SetProject(SolutionFile)
                                         .SetConfiguration("Release")
                                         .SetAssemblyVersion("0.1.2") // TODO: 
                                         .SetVersion("0.1.2") // TODO: nupkg version
                                         .SetOutputDirectory(PackageDirectory)
                        ));

    Target Stryker => _ => _
        .DependsOn(Restore)
        .Executes(() => {
            Console.WriteLine("Starting Stryker...");

            TestsDirectory.GlobFiles("**/*.csproj")
                          .ForEach(f => DotNet?.Invoke($"dotnet-stryker -tp {f} -b 20 -m \"!**/Waffle/**\" --reporter \"json\" --reporter \"html\""));
                    });

    Target CiBuild => _ => _
        .DependsOn(Compile)
        .DependsOn(Pack)
        .DependsOn(Test)
        .DependsOn(Test)
        .DependsOn(ConsolidateCoverage);

    Target All => _ => _
        .DependsOn(Compile)
        .DependsOn(Pack)
        .DependsOn(ConsolidateCoverage)
        .DependsOn(Stryker);
}

