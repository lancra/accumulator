using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accumulator.Infrastructure.Modules.Data;

internal abstract class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class
{
    private readonly Dictionary<Type, int> _columnCounters = [];

    protected int NextOrder => NextSubOrder<TEntity>();

    public abstract void Configure(EntityTypeBuilder<TEntity> builder);

    protected int NextSubOrder<TColumnEntity>()
    {
        if (!_columnCounters.TryGetValue(typeof(TColumnEntity), out var counter))
        {
            counter = 0;
            _columnCounters.Add(typeof(TColumnEntity), counter);
        }

        _columnCounters[typeof(TColumnEntity)] = counter + 1;
        return counter;
    }
}
