using Accumulator.Domain;

namespace Accumulator.Application.GameDefinitions;

internal sealed class MigrateDefinitionsCommandHandler : ICommandHandler<MigrateDefinitionsCommand>
{
    private readonly IGameDefinitionMigrator _migrator;

    public MigrateDefinitionsCommandHandler(IGameDefinitionMigrator migrator) => _migrator = migrator;

    public async Task Handle(MigrateDefinitionsCommand command, CancellationToken cancellationToken)
        => await _migrator.MigrateAllAsync(command.Directory, cancellationToken)
        .ConfigureAwait(false);
}
