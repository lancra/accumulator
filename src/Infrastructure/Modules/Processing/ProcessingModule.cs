using Accumulator.Application;
using Autofac;
using MediatR;

namespace Accumulator.Infrastructure.Modules.Processing;

internal sealed class ProcessingModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DomainEventDispatcher>()
            .As<IDomainEventDispatcher>()
            .InstancePerLifetimeScope();

        builder.RegisterType<DomainEventAccessor>()
            .As<IDomainEventAccessor>()
            .InstancePerLifetimeScope();

        builder.RegisterType<UnitOfWork>()
            .As<IUnitOfWork>()
            .InstancePerLifetimeScope();

        builder.RegisterGenericDecorator(typeof(UnitOfWorkCommandHandlerDecorator<>), typeof(ICommandHandler<>));
        builder.RegisterGenericDecorator(typeof(UnitOfWorkCommandHandlerDecorator<,>), typeof(ICommandHandler<,>));

        builder.RegisterGenericDecorator(typeof(ValidationCommandHandlerDecorator<>), typeof(ICommandHandler<>));
        builder.RegisterGenericDecorator(typeof(ValidationCommandHandlerDecorator<,>), typeof(ICommandHandler<,>));

        // The first resolved (last registered) decorator must match the IRequestHandler type resolved by MediatR.
        // Other decorators must match the decorated ICommandHandler type from the first resolved decorator.
        builder.RegisterGenericDecorator(typeof(LoggingCommandHandlerDecorator<>), typeof(IRequestHandler<>));
        builder.RegisterGenericDecorator(typeof(LoggingCommandHandlerDecorator<,>), typeof(IRequestHandler<,>));
    }
}
