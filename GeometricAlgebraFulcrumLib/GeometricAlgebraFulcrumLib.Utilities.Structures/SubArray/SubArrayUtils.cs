namespace GeometricAlgebraFulcrumLib.Utilities.Structures.SubArray;

public static class SubArrayUtils
{
    /// <summary>
    /// Create a sub-array of all items in the given base array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="baseArray"></param>
    /// <param name="isReversed"></param>
    /// <returns></returns>
    public static SubArray<T> GetSubArray<T>(this T[] baseArray, bool isReversed = false)
    {
        return new SubArray<T>(baseArray, isReversed, 0, baseArray.Length - 1);
    }

    /// <summary>
    /// Create a sub-array of the given base array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="baseArray"></param>
    /// <param name="baseFirstIndex"></param>
    /// <param name="baseLastIndex"></param>
    /// <returns></returns>
    public static SubArray<T> GetSubArray<T>(this T[] baseArray, int baseFirstIndex, int baseLastIndex)
    {
        return new SubArray<T>(baseArray, baseFirstIndex, baseLastIndex);
    }

    /// <summary>
    /// Create a sub-array of the given base array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="baseArray"></param>
    /// <param name="isReversed"></param>
    /// <param name="baseIndex1"></param>
    /// <param name="baseIndex2"></param>
    /// <returns></returns>
    public static SubArray<T> GetSubArray<T>(this T[] baseArray, bool isReversed, int baseIndex1, int baseIndex2)
    {
        return new SubArray<T>(baseArray, isReversed, baseIndex1, baseIndex2);
    }

    /// <summary>
    /// Create a single-item sub-array of the given base array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="baseArray"></param>
    /// <param name="baseIndex"></param>
    /// <param name="isReversed"></param>
    /// <returns></returns>
    public static SubArray<T> GetSubArray<T>(this T[] baseArray, bool isReversed, int baseIndex)
    {
        return new SubArray<T>(baseArray, isReversed, baseIndex, baseIndex);
    }
}