using System.CommandLine.Invocation;
using System.Diagnostics.CodeAnalysis;

namespace Accumulator.Cli;

internal abstract class CommandHandlerBase : ICommandHandler
{
    [SuppressMessage(
        "Usage",
        "VSTHRD002:Avoid problematic synchronous waits",
        Justification = "This implementation is required for the library interface and is not expected to be used.")]
    public int Invoke(InvocationContext context)
        => InvokeAsync(context)
        .GetAwaiter()
        .GetResult();

    public abstract Task<int> InvokeAsync(InvocationContext context);
}
