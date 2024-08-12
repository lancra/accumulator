using System.CommandLine;
using System.CommandLine.Invocation;
using Accumulator.Application.GameDefinitions;
using Accumulator.Infrastructure;

namespace Accumulator.Cli.Commands.Definitions;

internal sealed class MigrateCommand : Command
{
    private static readonly Argument<string> SourceArgument =
        new("source", "The directory containing game definition files.");

    public MigrateCommand()
        : base("migrate", "Migrates definitions from game files to the system database.")
        => AddArgument(SourceArgument);

    internal sealed class CommandHandler : CommandHandlerBase
    {
        private readonly IAccumulatorExecutor _executor;

        public CommandHandler(IAccumulatorExecutor executor) => _executor = executor;

        public override async Task<int> InvokeAsync(InvocationContext context)
        {
            var source = context.ParseResult.GetValueForArgument(SourceArgument);
            var command = new MigrateDefinitionsCommand(source);

            await _executor.ExecuteCommandAsync(command, context.GetCancellationToken())
                .ConfigureAwait(false);

            return 0;
        }
    }
}
