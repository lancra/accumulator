using System.Reflection;
using Accumulator.Application;
using Autofac;
using FluentValidation;
using MediatR;

namespace Accumulator.Infrastructure.Modules.Mediation;

internal sealed class MediationModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ServiceProviderWrapper>()
            .As<IServiceProvider>()
            .InstancePerDependency()
            .IfNotRegistered(typeof(IServiceProvider));

        builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        var mediatorOpenTypes = new[]
        {
            typeof(ICommandHandler<>),
            typeof(ICommandHandler<,>),
            typeof(IRequestHandler<>),
            typeof(IRequestHandler<,>),
            typeof(IValidator<>),
        };
        builder.RegisterSource(new ScopedContravariantRegistrationSource(mediatorOpenTypes));

        var applicationAssembly = typeof(InvalidCommandException).Assembly;
        foreach (var type in mediatorOpenTypes)
        {
            builder.RegisterAssemblyTypes(applicationAssembly)
                .AsClosedTypesOf(type)
                .AsImplementedInterfaces()
                .FindConstructorsWith(new AllConstructorFinder());
        }
    }
}
