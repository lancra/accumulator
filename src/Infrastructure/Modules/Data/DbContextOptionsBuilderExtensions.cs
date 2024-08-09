using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Accumulator.Infrastructure.Modules.Data;

/// <summary>
/// Provides extensions for database context options builders.
/// </summary>
public static class DbContextOptionsBuilderExtensions
{
    /// <summary>
    /// Configures the SQLite setup for the system.
    /// </summary>
    /// <param name="builder">The database context options builder to modify.</param>
    /// <param name="connectionString">The database connection string.</param>
    /// <returns>The modified database context options builder.</returns>
    public static DbContextOptionsBuilder UseAccumulatorSqlite(this DbContextOptionsBuilder builder, string connectionString)
        => builder.UseSqlite(connectionString, options => options.MigrationsAssembly("Accumulator.Migrations"));

    /// <summary>
    /// Replaces the default value converter selector with the implementation which supports strongly-typed identifiers.
    /// </summary>
    /// <param name="builder">The database context options builder to modify.</param>
    /// <returns>The modified database context options builder.</returns>
    public static DbContextOptionsBuilder UseTypedIdValueConverterSelector(this DbContextOptionsBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.ReplaceService<IValueConverterSelector, TypedIdValueConverterSelector>();
    }
}
