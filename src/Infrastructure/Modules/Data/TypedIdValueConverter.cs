using Accumulator.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Accumulator.Infrastructure.Modules.Data;

internal sealed class TypedIdValueConverter<TTypedId, TKey> : ValueConverter<TTypedId, TKey>
    where TTypedId : TypedId<TKey>
    where TKey : notnull
{
    public TypedIdValueConverter(ConverterMappingHints? mappingHints = default)
        : base(id => id.Value, value => Create(value), mappingHints)
    {
    }

    private static TTypedId Create(TKey value)
        => Activator.CreateInstance(typeof(TTypedId), [value]) is TTypedId typedId
        ? typedId
        : throw new InvalidOperationException(
            $"Unable to generate {typeof(TTypedId).Name} identifier using {value} as {typeof(TKey).Name}.");
}
