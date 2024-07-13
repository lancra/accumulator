using System.Text;

namespace Accumulator.Dev;

internal static class ArtifactPaths
{
    public const string Root = "artifacts";

    public const string PublishedExecutables = $"{Root}/publish";
    public static readonly CompositeFormat PublishedExecutableFormat = CompositeFormat.Parse($"{PublishedExecutables}/{{0}}");
}
