using System.Collections.Generic;

namespace SoftBodyPhysics.Utils;

internal static class ReadOnlyListExt
{
    public static IEnumerable<(T, T)> GetCartesianProduct<T>(this IReadOnlyList<T> list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            for (int j = i + 1; j < list.Count; j++)
            {
                yield return (list[i], list[j]);
            }
        }
    }

    public static IEnumerable<(T, T)> GetCrossProduct<T>(this IReadOnlyList<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < list.Count; j++)
            {
                if (!object.ReferenceEquals(list[i], list[j]))
                {
                    yield return (list[i], list[j]);
                }
            }
        }
    }
}
