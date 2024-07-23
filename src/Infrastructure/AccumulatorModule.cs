using Autofac;

namespace Accumulator.Infrastructure;

/// <summary>
/// Represents the external module used by executables.
/// </summary>
public class AccumulatorModule : Module
{
    /// <inheritdoc/>
    protected override void Load(ContainerBuilder builder)
        => builder.RegisterType<AccumulatorExecutor>()
        .As<IAccumulatorExecutor>()
        .InstancePerLifetimeScope();
}
