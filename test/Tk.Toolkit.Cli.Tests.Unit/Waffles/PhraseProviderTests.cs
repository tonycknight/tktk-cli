using System.Linq;
using FsCheck.Xunit;
using Tk.Toolkit.Cli.Waffle;

namespace Tk.Toolkit.Cli.Tests.Unit.Waffles
{
    public class PhraseProviderTests
    {
        [Property(Verbose = true)]
        public bool GetPhrase_ReturnsNonEmptySet(PhraseKind kind)
        {
            var pp = new PhraseProvider();

            var r = pp.GetPhrases(kind);

            return r.Length > 0 &&
                   r.All(s => s != null);
        }
    }
}
