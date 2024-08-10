namespace Accumulator.Dev.Targets.Build;

internal sealed class BundleTarget : ITarget
{
    public void Setup(Bullseye.Targets targets)
        => targets.Add(
            BuildTargets.Bundle,
            "Bundles the Entity Framework Core migrations into an executable.",
            dependsOn: [BuildTargets.Dotnet],
            action: Execute);

    private static async Task Execute()
        => await DotnetCli.RunToolAsync(
            "ef migrations bundle",
            [
                "--project src/Migrations",
                "--startup-project tools/Migrator",
                $"--output {ArtifactPaths.DatabaseMigrationBundle}",
                "--force",
                "--self-contained"
            ])
            .ConfigureAwait(false);
}
