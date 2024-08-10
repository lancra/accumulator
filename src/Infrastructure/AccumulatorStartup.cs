using Accumulator.Infrastructure.Modules.Data;
using Accumulator.Infrastructure.Modules.Domain;
using Accumulator.Infrastructure.Modules.Logging;
using Accumulator.Infrastructure.Modules.Mediation;
using Accumulator.Infrastructure.Modules.Processing;
using Autofac;
using Serilog;
using Serilog.Extensions.Logging;

namespace Accumulator.Infrastructure;

/// <summary>
/// Provides the application start-up configuration for the domain.
/// </summary>
public static class AccumulatorStartup
{
    /// <summary>
    /// Initializes the application.
    /// </summary>
    /// <param name="connectionString">The database connection string.</param>
    /// <param name="logger">The component responsible for logging events.</param>
    public static void Initialize(string connectionString, ILogger logger)
    {
        var containerBuilder = new ContainerBuilder();
        containerBuilder.RegisterModule(new DataModule(connectionString, new SerilogLoggerFactory(logger)));
        containerBuilder.RegisterModule(new DomainModule());
        containerBuilder.RegisterModule(new LoggingModule(logger));
        containerBuilder.RegisterModule(new MediationModule());
        containerBuilder.RegisterModule(new ProcessingModule());

        var container = containerBuilder.Build();
        AccumulatorCompositionRoot.SetContainer(container);
    }
}
