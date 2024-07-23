using Autofac;

namespace Accumulator.Infrastructure;

internal static class AccumulatorCompositionRoot
{
    private static IContainer? _container;

    public static void SetContainer(IContainer container) => _container = container;

    public static ILifetimeScope BeginLifetimeScope()
    {
        if (_container is null)
        {
            throw new InvalidOperationException("The composition root did not have a valid container.");
        }

        return _container.BeginLifetimeScope();
    }
}
