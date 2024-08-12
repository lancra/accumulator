namespace Accumulator.Dev.Targets.Build;

internal sealed class PublishTarget : ITarget
{
    private static readonly ExecutableProject[] Projects =
    [
        new("cli", "src/Cli"),
    ];

    public void Setup(Bullseye.Targets targets)
        => targets.Add(
            BuildTargets.Publish,
            "Publishes executable projects and dependencies to a folder for deployment.",
            dependsOn: [BuildTargets.Dotnet],
            forEach: Projects,
            async project => await DotnetCli

                // Build is required (i.e. --no-build is prohibited) due to an issue with single file publishing.
                // Issue: https://github.com/dotnet/sdk/issues/17526
                .RunAsync(
                    $"publish {project.Path}",
                    "--self-contained",
                    $"--output {string.Format(null, ArtifactPaths.PublishedExecutableFormat, project.Name)}")
                .ConfigureAwait(false));
}
