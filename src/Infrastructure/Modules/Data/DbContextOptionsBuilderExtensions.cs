using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Accumulator.Infrastructure.Modules.Data;

/// <summary>
/// Provides extensions for database context options builders.
/// </summary>
public static class DbContextOptionsBuilderExtensions
{
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
