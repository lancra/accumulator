using Autofac.Core;
using Autofac.Features.Variance;

namespace Accumulator.Infrastructure.Modules.Mediation;

internal sealed class ScopedContravariantRegistrationSource : IRegistrationSource
{
    private readonly ContravariantRegistrationSource _source = new();
    private readonly IReadOnlyCollection<Type> _types = [];

    public ScopedContravariantRegistrationSource(params Type[] types)
    {
        ArgumentNullException.ThrowIfNull(types);

        if (!types.All(type => type.IsGenericTypeDefinition))
        {
            throw new ArgumentException("Supplied types for registration source should be generic type definitions.");
        }

        _types = types;
    }

    public bool IsAdapterForIndividualComponents => _source.IsAdapterForIndividualComponents;

    public IEnumerable<IComponentRegistration> RegistrationsFor(
        Service service,
        Func<Service, IEnumerable<ServiceRegistration>> registrationAccessor)
    {
        var components = _source.RegistrationsFor(service, registrationAccessor);
        foreach (var component in components)
        {
            var typeDefinitions = component.Target.Services.OfType<TypedService>()
                .Select(typedService => typedService.ServiceType.GetGenericTypeDefinition());
            if (typeDefinitions.Any(_types.Contains))
            {
                yield return component;
            }
        }
    }
}
