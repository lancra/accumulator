using Accumulator.Dev;
using Accumulator.Dev.Targets;
using Autofac;
using SimpleExec;

var container = DevStartup.BuildContainer();

var targets = new Bullseye.Targets();
foreach (var target in container.Resolve<IEnumerable<ITarget>>())
{
    target.Setup(targets);
}

await targets.RunAndExitAsync(args, messageOnly: ex => ex is ExitCodeException)
    .ConfigureAwait(false);
