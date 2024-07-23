namespace Accumulator.Infrastructure.Modules.Processing;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly AccumulatorDbContext _dbContext;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public UnitOfWork(AccumulatorDbContext dbContext, IDomainEventDispatcher domainEventDispatcher)
    {
        _dbContext = dbContext;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await _domainEventDispatcher.DispatchEventsAsync(cancellationToken)
            .ConfigureAwait(false);

        await _dbContext.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
