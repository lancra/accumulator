using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;
using Accumulator.Cli;
using Accumulator.Cli.Commands.Definitions;
using Accumulator.Infrastructure;
using Accumulator.Infrastructure.Modules.Data;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}] [{Context}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

await new CommandLineBuilder(new AccumulatorRootCommand())
    .UseVersionOption()
    .UseHelp()
    .UseEnvironmentVariableDirective()
    .UseParseDirective()
    .UseSuggestDirective()
    .RegisterWithDotnetSuggest()
    .UseTypoCorrections()
    .UseParseErrorReporting()
    .UseExceptionHandler()
    .CancelOnProcessTermination()
    .UseHost(host => host
        .UseServiceProviderFactory(new AutofacServiceProviderFactory(
            container => container.RegisterModule(new AccumulatorModule())))
        .ConfigureAppConfiguration(config => config.AddEnvironmentVariables(AccumulatorEnvironmentVariables.Prefix))
        .ConfigureServices(
            (host, services) =>
            {
                var connectionString = host.Configuration.GetDatabaseConnectionString();
                AccumulatorStartup.Initialize(connectionString, logger);
            })
        .UseCommandHandler<MigrateCommand, MigrateCommand.CommandHandler>())
    .Build()
    .InvokeAsync(args)
    .ConfigureAwait(false);
