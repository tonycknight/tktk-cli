using System.Diagnostics.CodeAnalysis;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;

namespace Tk.Toolkit.Cli.Nuget
{
    [ExcludeFromCodeCoverage]
    internal class NugetClient : INugetClient
    {
        private const string _packageId = "tktk-cli";
        private const string _packageSource = NuGetConstants.V3FeedUrl;

        public async Task<string?> GetLatestNugetVersionAsync()
        {
            try
            {
                var logger = new NuGet.Common.NullLogger();                
                var sourceRepository = Repository.Factory.GetCoreV3(new PackageSource(_packageSource));
                var mdr = sourceRepository.GetResource<MetadataResource>();

                using var cache = new SourceCacheContext();

                var vsn = await mdr.GetLatestVersion(_packageId, true, false, cache, logger, CancellationToken.None);

                if (vsn == null)
                {
                    return null;
                }

                return vsn.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
