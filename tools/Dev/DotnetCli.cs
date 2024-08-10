using System.Text;

namespace Accumulator.Dev;

internal static class DotnetCli
{
    private static readonly CompositeFormat BuiltInArgumentsFormat =
        CompositeFormat.Parse("{0} --configuration Release --verbosity minimal --nologo{1}");

    private static readonly CompositeFormat ToolArgumentsFormat =
        CompositeFormat.Parse("{0} --configuration Release {1}");

    public static async Task RunAsync(string command, params string[] args)
    {
        var fullArguments = ParseArguments(command, args, BuiltInArgumentsFormat);
        await SimpleExec.Command.RunAsync("dotnet", fullArguments)
            .ConfigureAwait(false);
    }

    public static async Task RunToolAsync(string command, params string[] args)
    {
        var fullArguments = ParseArguments(command, args, ToolArgumentsFormat);
        await SimpleExec.Command.RunAsync("dotnet", fullArguments)
            .ConfigureAwait(false);
    }

    private static string ParseArguments(string command, string[] arguments, CompositeFormat argumentsFormat)
    {
        var additionalArgumentsString = string.Empty;
        if (arguments.Length != 0)
        {
            additionalArgumentsString = $" {string.Join(' ', arguments)}";
        }

        var fullArguments = string.Format(null, argumentsFormat, command, additionalArgumentsString);
        return fullArguments;
    }
}
