using Accumulator.Application;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;

namespace Accumulator.Infrastructure.Modules.Processing;

internal sealed class LoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : Command
{
    private readonly ILogger _logger;
    private readonly ICommandHandler<TCommand> _decoratedHandler;

    public LoggingCommandHandlerDecorator(ILogger logger, ICommandHandler<TCommand> decoratedHandler)
    {
        _logger = logger;
        _decoratedHandler = decoratedHandler;
    }

    public async Task Handle(TCommand command, CancellationToken cancellationToken)
    {
        var commandLog = command.GetType().Name;
        using var context = LogContext.Push(new CommandLogEnricher(command));
        try
        {
            _logger.Information("Executing command {Command}", commandLog);

            await _decoratedHandler.Handle(command, cancellationToken)
                .ConfigureAwait(false);

            _logger.Information("Command {Command} processing succeeded.", commandLog);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Command {Command} processing failed.", commandLog);
            throw;
        }
    }

    private sealed class CommandLogEnricher : ILogEventEnricher
    {
        private readonly Command _command;

        public CommandLogEnricher(Command command) => _command = command;

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            => logEvent.AddOrUpdateProperty(new LogEventProperty("Context", new ScalarValue($"Command:{_command.Id}")));
    }
}
