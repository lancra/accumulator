using Autofac;
using Serilog;

namespace Accumulator.Infrastructure.Modules.Logging;

internal sealed class LoggingModule : Module
{
    private readonly ILogger _logger;

    public LoggingModule(ILogger logger) => _logger = logger;

    protected override void Load(ContainerBuilder builder)
        => builder.RegisterInstance(_logger)
        .As<ILogger>()
        .SingleInstance();
}
