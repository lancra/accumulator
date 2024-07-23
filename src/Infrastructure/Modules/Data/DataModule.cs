using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;

namespace Accumulator.Infrastructure.Modules.Data;

internal sealed class DataModule : Module
{
    private readonly string _connectionString;
    private readonly ILoggerFactory _loggerFactory;

    public DataModule(string connectionString, ILoggerFactory loggerFactory)
    {
        _connectionString = connectionString;
        _loggerFactory = loggerFactory;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(
            component =>
            {
                var options = new DbContextOptionsBuilder<AccumulatorDbContext>()
                    .UseSqlite(_connectionString)
                    .UseLoggerFactory(_loggerFactory)
                    .ReplaceService<IValueConverterSelector, TypedIdValueConverterSelector>()
                    .Options;
                return new AccumulatorDbContext(options);
            })
            .AsSelf()
            .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(GetType().Assembly)
            .Where(type => type.Name.EndsWith("Repository", StringComparison.OrdinalIgnoreCase))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope()
            .FindConstructorsWith(new AllConstructorFinder());
    }
}
