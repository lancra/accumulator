namespace Accumulator.Infrastructure;

/// <summary>
/// Provides environment variable support for system configuration.
/// </summary>
public static class AccumulatorEnvironmentVariables
{
    /// <summary>
    /// Gets the prefix used to filter applicable environment variables.
    /// </summary>
    public const string Prefix = "ACCUMULATOR_";

    /// <summary>
    /// Gets the name of the variable which contains the database connection string.
    /// </summary>
    public const string Database = "DATABASE";
}
