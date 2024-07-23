using Accumulator.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Accumulator.Infrastructure;

/// <summary>
/// Provides the database context for the application.
/// </summary>
public class AccumulatorDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AccumulatorDbContext"/> class.
    /// </summary>
    /// <param name="options">The options used to customize the database context.</param>
    public AccumulatorDbContext(DbContextOptions<AccumulatorDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Customizes the model used by the database context.
    /// </summary>
    /// <param name="modelBuilder">The builder used to apply changes to the model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        modelBuilder.Ignore<DomainEvent>();

        base.OnModelCreating(modelBuilder);
    }
}
