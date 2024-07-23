using Accumulator.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Accumulator.Infrastructure.Modules.Processing;

internal sealed class DomainEventAccessor : IDomainEventAccessor
{
    private readonly AccumulatorDbContext _dbContext;

    public DomainEventAccessor(AccumulatorDbContext dbContext) => _dbContext = dbContext;

    public IReadOnlyCollection<DomainEvent> GetAll()
        => GetEntriesWithDomainEvents()
        .SelectMany(entry => entry.Entity.DomainEvents)
        .ToArray();

    public void ClearAll()
    {
        foreach (var entry in GetEntriesWithDomainEvents())
        {
            entry.Entity.ClearDomainEvents();
        }
    }

    private EntityEntry<Entity>[] GetEntriesWithDomainEvents()
        => _dbContext.ChangeTracker.Entries<Entity>()
        .Where(entry => entry.Entity.DomainEvents.Count > 0)
        .ToArray();
}
