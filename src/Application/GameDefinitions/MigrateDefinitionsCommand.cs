namespace Accumulator.Application.GameDefinitions;

/// <summary>
/// Provides the command for migrating game definitions into the system.
/// </summary>
public class MigrateDefinitionsCommand : Command
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MigrateDefinitionsCommand"/> class.
    /// </summary>
    /// <param name="directory">The path of the directory containing the game definitions.</param>
    public MigrateDefinitionsCommand(string directory) => Directory = directory;

    /// <summary>
    /// Gets the path of the directory containing the game definitions.
    /// </summary>
    public string Directory { get; }
}
