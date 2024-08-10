using Accumulator.Domain;
using Accumulator.Infrastructure.Domain;
using Autofac;

namespace Accumulator.Infrastructure.Modules.Domain;

internal sealed class DomainModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var infrastructureAssembly = GetType().Assembly;

        builder.RegisterAssemblyTypes(infrastructureAssembly)
            .AsClosedTypesOf(typeof(IGameDefinitionReader<>))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(infrastructureAssembly)
            .As<IGameDefinitionMigrationStrategy>()
            .InstancePerLifetimeScope();

        builder.RegisterType<GameDefinitionMigrator>()
            .As<IGameDefinitionMigrator>()
            .InstancePerLifetimeScope();
    }
}
