using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;

namespace Spurious2.Core2;

public static class GuardExtensions
{
    [SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
    public static void NullValue<T>(this IGuardClause gc, [NotNull] T input, string objectName)
    {
        if (input is null)
        {
            throw new NullReferenceException($"{objectName} was null");
        }
    }
}
