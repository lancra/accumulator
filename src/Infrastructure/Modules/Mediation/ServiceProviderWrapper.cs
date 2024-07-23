using Autofac;

namespace Accumulator.Infrastructure.Modules.Mediation;

internal sealed class ServiceProviderWrapper : IServiceProvider
{
    private readonly ILifetimeScope _lifetimeScope;

    public ServiceProviderWrapper(ILifetimeScope lifetimeScope) => _lifetimeScope = lifetimeScope;

    public object? GetService(Type serviceType) => _lifetimeScope.ResolveOptional(serviceType);
}
