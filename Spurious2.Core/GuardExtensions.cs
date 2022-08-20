using Ardalis.GuardClauses;
using System.Diagnostics.CodeAnalysis;

namespace Spurious2.Core;

public static class GuardExtensions
{
    public static void NullValue<T>(this IGuardClause gc, [NotNull] T input, string objectName)
    {
        if (input is null)
        {
            throw new NullReferenceException($"{objectName} was null");
        }
    }
}
