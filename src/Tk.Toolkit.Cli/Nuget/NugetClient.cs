using System.Diagnostics.CodeAnalysis;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;

namespace Tk.Toolkit.Cli.Nuget
{
    [ExcludeFromCodeCoverage]
    internal class NugetClient : INugetClient
    {
        public string GetLatestNugetVersion()
        {
            try
            {
                var packageId = "tktk-cli";

                var cnxToken = CancellationToken.None;
                var logger = new NuGet.Common.NullLogger();
                using var cache = new SourceCacheContext();
                var sourceRepository = Repository.Factory.GetCoreV3(new PackageSource(NuGetConstants.V3FeedUrl));
                var mdr = sourceRepository.GetResource<MetadataResource>();

                var vsn = mdr.GetLatestVersion(packageId, true, false, cache, logger, cnxToken).Result;

                if (vsn == null)
                {
                    return new Version().ToString();
                }

                return vsn.ToString();
            }
            catch (Exception)
            {
                return new Version().ToString();
            }
        }
    }
}
