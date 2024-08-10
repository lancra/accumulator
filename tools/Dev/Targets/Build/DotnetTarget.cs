using System.Collections;
using System.Security.Cryptography;
using static SimpleExec.Command;

namespace Accumulator.Dev.Targets.Build;

internal sealed class DotnetTarget : ITarget
{
    private const string EntityFrameworkTargetsFileName = "EntityFrameworkCore.targets";
    private const string SourceTargetsPath = $"resources/{EntityFrameworkTargetsFileName}";
    private const string DestinationTargetsPathFormat =
        $"{ArtifactPaths.Root}/obj/Accumulator.{{0}}/Accumulator.{{0}}.csproj.{EntityFrameworkTargetsFileName}";

    public void Setup(Bullseye.Targets targets)
        => targets.Add(
            BuildTargets.Dotnet,
            "Builds the solution into a set of output binaries.",
            dependsOn: [BuildTargets.Clean],
            Execute);

    private static async Task Execute()
    {
        await DotnetCli.RunAsync("build", "/warnaserror")
            .ConfigureAwait(false);

        await CopyEntityFrameworkTargets()
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Copy Entity Framework Core targets file to the project and startup project object directories.
    /// </summary>
    /// <remarks>
    /// Fixes the dotnet-ef tool when using the artifacts output and a separate startup project by copying the targets file to each
    /// project's object artifact directory.
    ///
    /// Issue: https://github.com/dotnet/efcore/issues/30725
    /// Copied-from: https://github.com/dotnet/efcore/blob/main/src/dotnet-ef/Resources/EntityFrameworkCore.targets
    /// </remarks>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    private static async Task CopyEntityFrameworkTargets()
    {
        var projectDestinationTargetsPath = string.Format(DestinationTargetsPathFormat, "Migrations");
        var startupDestinationTargetsPath = string.Format(DestinationTargetsPathFormat, "Migrator");

        var sourceHash = await ComputeFileHash(SourceTargetsPath)
            .ConfigureAwait(false);
        var projectDestinationHash = await ComputeFileHash(projectDestinationTargetsPath)
            .ConfigureAwait(false);
        var startupDestinationHash = await ComputeFileHash(startupDestinationTargetsPath)
            .ConfigureAwait(false);

        if (StructuralComparisons.StructuralEqualityComparer.Equals(sourceHash, projectDestinationHash) &&
            StructuralComparisons.StructuralEqualityComparer.Equals(sourceHash, startupDestinationHash))
        {
            return;
        }

        string[] argsFormat =
        [
           "-Command Copy-Item",
            $"-Path {SourceTargetsPath}",
            "-Destination {0}",
            "| Out-Null"
        ];

        var argsStringFormat = string.Join(' ', argsFormat);

        await RunAsync("pwsh", string.Format(argsStringFormat, projectDestinationTargetsPath))
            .ConfigureAwait(false);
        await RunAsync("pwsh", string.Format(argsStringFormat, startupDestinationTargetsPath))
            .ConfigureAwait(false);
    }

    private static async Task<IEnumerable<byte>> ComputeFileHash(string relativePath)
    {
        if (!File.Exists(relativePath))
        {
            return [];
        }

        using var hashAlgorithm = SHA512.Create();
        using var sourceStream = File.OpenRead(relativePath);

        var hashBytes = await hashAlgorithm.ComputeHashAsync(sourceStream)
            .ConfigureAwait(false);
        return hashBytes;
    }
}
