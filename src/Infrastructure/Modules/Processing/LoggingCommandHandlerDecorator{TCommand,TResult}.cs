using Accumulator.Application;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;

namespace Accumulator.Infrastructure.Modules.Processing;

internal sealed class LoggingCommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
    where TCommand : Command<TResult>
{
    private readonly ILogger _logger;
    private readonly ICommandHandler<TCommand, TResult> _decoratedHandler;

    public LoggingCommandHandlerDecorator(ILogger logger, ICommandHandler<TCommand, TResult> decoratedHandler)
    {
        _logger = logger;
        _decoratedHandler = decoratedHandler;
    }

    public async Task<TResult> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var commandLog = command.GetType().Name;
        using var context = LogContext.Push(new CommandLogEnricher(command));
        try
        {
            _logger.Information("Executing command {Command}", commandLog);

            var result = await _decoratedHandler.Handle(command, cancellationToken)
                .ConfigureAwait(false);

            _logger.Information("Command {Command} processing succeeded.", commandLog);

            return result;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Command {Command} processing failed.", commandLog);
            throw;
        }
    }

    private sealed class CommandLogEnricher : ILogEventEnricher
    {
        private readonly Command<TResult> _command;

        public CommandLogEnricher(Command<TResult> command) => _command = command;

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            => logEvent.AddOrUpdateProperty(new LogEventProperty("Context", new ScalarValue($"Command:{_command.Id}")));
    }
}
