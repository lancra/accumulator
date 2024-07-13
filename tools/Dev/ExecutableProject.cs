namespace Accumulator.Dev;

internal sealed record ExecutableProject(string Name, string Path)
{
    public override string ToString() => Name;
}
