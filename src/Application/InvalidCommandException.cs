namespace Accumulator.Application;

/// <summary>
/// Provides an exception for a command that failed validation.
/// </summary>
public class InvalidCommandException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidCommandException"/> class.
    /// </summary>
    /// <param name="errors">The errors encountered when validating the command.</param>
    public InvalidCommandException(IReadOnlyCollection<string> errors)
        => Errors = errors;

    /// <summary>
    /// Gets the errors encountered when validating the command.
    /// </summary>
    public IReadOnlyCollection<string> Errors { get; }
}
