namespace Accumulator.Dev.Targets.Build;

internal sealed class PublishTarget : ITarget
{
    public void Setup(Bullseye.Targets targets)
        => targets.Add(
            BuildTargets.Publish,
            "Publishes executable projects and dependencies to a folder for deployment.",
            dependsOn: [BuildTargets.Dotnet],
            forEach: Array.Empty<ExecutableProject>(),
            async project => await DotnetCli
                .RunAsync(
                    $"publish {project.Path}",
                    "--no-build",
                    $"--output {string.Format(null, ArtifactPaths.PublishedExecutableFormat, project.Name)}")
                .ConfigureAwait(false));
}
