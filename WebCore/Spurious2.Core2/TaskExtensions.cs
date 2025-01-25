using System.Runtime.CompilerServices;

namespace Spurious2.Core2;

public static class TaskExtensions
{
    public static ConfiguredTaskAwaitable ConfigAwait(this Task task)
    {
        ArgumentNullException.ThrowIfNull(task, nameof(task));
        return task.ConfigureAwait(false);
    }

    public static ConfiguredTaskAwaitable<T> ConfigAwait<T>(this Task<T> task)
    {
        ArgumentNullException.ThrowIfNull(task, nameof(task));
        return task.ConfigureAwait(false);
    }

    public static ConfiguredValueTaskAwaitable ConfigAwait(this ValueTask task)
    {
        return task.ConfigureAwait(false);
    }

    public static ConfiguredValueTaskAwaitable<T> ConfigAwait<T>(this ValueTask<T> task)
    {
        return task.ConfigureAwait(false);
    }

    public static ConfiguredCancelableAsyncEnumerable<T> ConfigAwait<T>(this IAsyncEnumerable<T> task)
    {
        ArgumentNullException.ThrowIfNull(task, nameof(task));
        return task.ConfigureAwait(false);
    }
}
