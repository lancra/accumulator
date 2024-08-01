using Accumulator.Infrastructure;
using Accumulator.Infrastructure.Modules.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();

builder.Configuration.AddEnvironmentVariables(AccumulatorEnvironmentVariables.Prefix);

var connectionString = builder.Configuration.GetValue<string>(AccumulatorEnvironmentVariables.Database);
builder.Services.AddDbContext<AccumulatorDbContext>(options => options
    .UseSqlite(connectionString)
    .UseTypedIdValueConverterSelector());

var app = builder.Build();

await app.RunAsync()
    .ConfigureAwait(false);
