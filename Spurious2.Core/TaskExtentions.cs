using System.Runtime.CompilerServices;

namespace Spurious2.Core;

public static class TaskExtensions
{
    public static ConfiguredTaskAwaitable ConfigAwait(this Task task)
    {
        return task.ConfigureAwait(false);
    }

    public static ConfiguredAsyncDisposable ConfigAwait(this IAsyncDisposable task)
    {
        return task.ConfigureAwait(false);
    }

    public static ConfiguredTaskAwaitable<T> ConfigAwait<T>(this Task<T> task)
    {
        return task.ConfigureAwait(false);
    }

    public static ConfiguredCancelableAsyncEnumerable<T> ConfigAwait<T>(this IAsyncEnumerable<T> task)
    {
        return task.ConfigureAwait(false);
    }
}
