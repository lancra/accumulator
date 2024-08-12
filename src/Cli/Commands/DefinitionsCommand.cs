using System.CommandLine;
using Accumulator.Cli.Commands.Definitions;

namespace Accumulator.Cli.Commands;

internal sealed class DefinitionsCommand : Command
{
    public DefinitionsCommand()
        : base("definitions", "Provides access to game definitions.")
        => AddCommand(new MigrateCommand());
}
