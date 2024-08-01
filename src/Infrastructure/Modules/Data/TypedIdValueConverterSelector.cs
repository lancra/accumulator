using System.Collections.Concurrent;
using Accumulator.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Accumulator.Infrastructure.Modules.Data;

/// <summary>
/// Provides selection of value converters for strongly-typed identifiers.
/// </summary>
/// <remarks>
/// Based on https://andrewlock.net/strongly-typed-ids-in-ef-core-using-strongly-typed-entity-ids-to-avoid-primitive-obsession-part-4/.
/// </remarks>
internal sealed class TypedIdValueConverterSelector : ValueConverterSelector
{
    private readonly ConcurrentDictionary<(Type ModelType, Type ProviderType), ValueConverterInfo> _valueConverters = new();

    public TypedIdValueConverterSelector(ValueConverterSelectorDependencies dependencies)
        : base(dependencies)
    {
    }

    public override IEnumerable<ValueConverterInfo> Select(Type modelClrType, Type? providerClrType = null)
    {
        var baseConverters = base.Select(modelClrType, providerClrType);
        foreach (var converter in baseConverters)
        {
            yield return converter;
        }

        var underlyingModelType = UnwrapNullableType(modelClrType);
        var underlyingProviderType = UnwrapNullableType(providerClrType);

        if (underlyingModelType is not null)
        {
            var isTypedId = underlyingModelType.BaseType is not null &&
                underlyingModelType.BaseType.IsGenericType &&
                underlyingModelType.BaseType.GetGenericTypeDefinition() == typeof(TypedId<>);
            if (isTypedId)
            {
                underlyingProviderType ??= underlyingModelType.BaseType!.GenericTypeArguments[0];

                var converterType = typeof(TypedIdValueConverter<,>).MakeGenericType([underlyingModelType, underlyingProviderType]);
                yield return _valueConverters.GetOrAdd(
                    (underlyingModelType, underlyingProviderType),
                    _ => new ValueConverterInfo(
                        modelClrType: modelClrType,
                        providerClrType: underlyingProviderType,
                        factory: valueConverterInfo => (ValueConverter)Activator.CreateInstance(
                            converterType,
                            valueConverterInfo.MappingHints)!));
            }
        }
    }

    private static Type? UnwrapNullableType(Type? type)
        => type is not null
        ? Nullable.GetUnderlyingType(type) ?? type
        : null;
}
