using System.Reflection;
using Autofac.Core.Activators.Reflection;

namespace Accumulator.Infrastructure.Modules;

internal sealed class AllConstructorFinder : IConstructorFinder
{
    public ConstructorInfo[] FindConstructors(Type targetType)
    {
        var constructors = targetType.GetTypeInfo()
            .DeclaredConstructors.ToArray();

        return constructors.Length > 0 ? constructors : throw new NoConstructorsFoundException(targetType, this);
    }
}
