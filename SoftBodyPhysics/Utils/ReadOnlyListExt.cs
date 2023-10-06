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
        foreach (var item1 in list)
        {
            foreach (var item2 in list)
            {
                if (!object.ReferenceEquals(item1, item2))
                {
                    yield return (item1, item2);
                }
            }
        }
    }
}
