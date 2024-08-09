using Microsoft.Extensions.Configuration;

namespace Accumulator.Infrastructure.Modules.Data;

/// <summary>
/// Provides extensions for database configuration.
/// </summary>
public static class DatabaseConfigurationExtensions
{
    /// <summary>
    /// Gets the database connection string from the configuration.
    /// </summary>
    /// <param name="configuration">The configuration to retrieve from.</param>
    /// <returns>The database connection string.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the connection string is not configured.</exception>
    public static string GetDatabaseConnectionString(this IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        var connectionStringSection = configuration.GetSection(AccumulatorEnvironmentVariables.Database);
        var connectionString = connectionStringSection.Value;

        return !string.IsNullOrEmpty(connectionString)
            ? connectionString
            : throw new InvalidOperationException("No database connection string configured for the application.");
    }
}
