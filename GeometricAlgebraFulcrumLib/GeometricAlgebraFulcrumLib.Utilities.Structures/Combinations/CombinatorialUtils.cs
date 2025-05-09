namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;

public static class CombinatorialUtils
{
    /// <summary>
    /// https://stackoverflow.com/questions/1952153/what-is-the-best-way-to-find-all-combinations-of-items-in-an-array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static IEnumerable<IEnumerable<T>> GetPermutationsRepeated<T>(this IReadOnlyList<T> list, int length)
    {
        //Input:  {1,2,3,4}, 2
        //Output: {1,1} {1,2} {1,3} {1,4} {2,1} {2,2} {2,3} {2,4} {3,1} {3,2} {3,3} {3,4} {4,1} {4,2} {4,3} {4,4}

        if (length == 1) return list.Select(t => new[] { t });

        return GetPermutationsRepeated(
            list, 
            length - 1
        ).SelectMany(
            _ => list, 
            (t1, t2) => t1.Concat([t2])
        );
    }

    /// <summary>
    /// https://stackoverflow.com/questions/1952153/what-is-the-best-way-to-find-all-combinations-of-items-in-an-array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static IEnumerable<IEnumerable<T>> GetPermutationsDistinct<T>(this IReadOnlyList<T> list, int length)
    {
        //Input:  {1,2,3,4}, 2
        //Output: {1,2} {1,3} {1,4} {2,1} {2,3} {2,4} {3,1} {3,2} {3,4} {4,1} {4,2} {4,3}

        if (length == 1) return list.Select(t => new[] { t });

        return GetPermutationsDistinct(
            list, 
            length - 1
        ).SelectMany(
            t => list.Where(o => !t.Contains(o)),
            (t1, t2) => t1.Concat([t2])
        );
    }

    /// <summary>
    /// https://stackoverflow.com/questions/1952153/what-is-the-best-way-to-find-all-combinations-of-items-in-an-array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static IEnumerable<IEnumerable<T>> GetCombinationsRepeated<T>(this IReadOnlyList<T> list, int length) where T : IComparable
    {
        //Input:  {1,2,3,4}, 2
        //Output: {1,1} {1,2} {1,3} {1,4} {2,2} {2,3} {2,4} {3,3} {3,4} {4,4}

        if (length == 1) return list.Select(t => new[] { t });

        return GetCombinationsRepeated(
            list, 
            length - 1
        ).SelectMany(
            t => list.Where(o => o.CompareTo(t.Last()) >= 0), 
            (t1, t2) => t1.Concat([t2])
        );
    }

    /// <summary>
    /// https://stackoverflow.com/questions/1952153/what-is-the-best-way-to-find-all-combinations-of-items-in-an-array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static IEnumerable<IEnumerable<T>> GetCombinationsDistinct<T>(this IReadOnlyList<T> list, int length) where T : IComparable
    {
        //Input:  {1,2,3,4}, 2
        //Output: {1,2} {1,3} {1,4} {2,3} {2,4} {3,4}

        if (length == 1) return list.Select(t => new[] { t });

        return GetCombinationsDistinct(
            list, 
            length - 1
        ).SelectMany(
            t => list.Where(o => o.CompareTo(t.Last()) > 0), 
            (t1, t2) => t1.Concat([t2])
        );
    }
}