using System.CommandLine;
using Accumulator.Cli.Commands;

namespace Accumulator.Cli;

internal sealed class AccumulatorRootCommand : RootCommand
{
    public AccumulatorRootCommand()
        : base("Supports the transfer of items between the system and the game.")
        => AddCommand(new DefinitionsCommand());
}
