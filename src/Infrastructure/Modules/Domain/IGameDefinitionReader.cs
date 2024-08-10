namespace Accumulator.Infrastructure.Modules.Domain;

/// <summary>
/// Represents a reader for definitions from the game source files.
/// </summary>
/// <typeparam name="TDefinition">The type of definition to read.</typeparam>
internal interface IGameDefinitionReader<out TDefinition>
{
    /// <summary>
    /// Gets the game source file name.
    /// </summary>
    string FileName { get; }

    /// <summary>
    /// Reads all definitions from the file.
    /// </summary>
    /// <param name="path">The path of the file to read.</param>
    /// <returns>The collection of definition objects.</returns>
    IReadOnlyCollection<TDefinition> ReadAll(string path);
}
