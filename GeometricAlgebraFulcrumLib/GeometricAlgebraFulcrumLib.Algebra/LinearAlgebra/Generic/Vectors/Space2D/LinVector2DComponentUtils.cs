using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;

public static class LinVector2DComponentUtils
{
    /// <summary>
    /// The value of the smallest component in this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetMinComponent<T>(this IPair<Scalar<T>> vector)
    {
        return vector.Item1 <= vector.Item2
            ? vector.Item1
            : vector.Item2;
    }

    /// <summary>
    /// The value of the largest component in this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetMaxComponent<T>(this IPair<Scalar<T>> vector)
    {
        return vector.Item1 >= vector.Item2
            ? vector.Item1
            : vector.Item2;
    }

    /// <summary>
    /// The index of the smallest component of this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMinComponentIndex<T>(this IPair<Scalar<T>> vector)
    {
        return vector.Item1 <= vector.Item2
            ? 0 : 1;
    }

    /// <summary>
    /// The index of the largest component of this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMaxComponentIndex<T>(this IPair<Scalar<T>> vector)
    {
        return vector.Item1 >= vector.Item2
            ? 0 : 1;
    }

    /// <summary>
    /// The index of the largest absolute component in this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMinAbsComponentIndex<T>(this IPair<Scalar<T>> vector)
    {
        return vector.Item1.Abs() <= vector.Item2.Abs()
            ? 0 : 1;
    }

    /// <summary>
    /// The index of the largest absolute component in this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMaxAbsComponentIndex<T>(this IPair<Scalar<T>> vector)
    {
        return vector.Item1.Abs() >= vector.Item2.Abs()
            ? 0 : 1;
    }

    /// <summary>
    /// Returns a new vector containing component-wise minimum values of the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Min<T>(this IPair<Scalar<T>> v1, IPair<Scalar<T>> v2)
    {
        return LinVector2D<T>.Create(
            v1.Item1 < v2.Item1 ? v1.Item1 : v2.Item1,
            v1.Item2 < v2.Item2 ? v1.Item2 : v2.Item2
        );
    }

    /// <summary>
    /// Returns a new vector containing component-wise maximum values of the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Max<T>(this IPair<Scalar<T>> v1, IPair<Scalar<T>> v2)
    {
        return LinVector2D<T>.Create(
            v1.Item1 > v2.Item1 ? v1.Item1 : v2.Item1,
            v1.Item2 > v2.Item2 ? v1.Item2 : v2.Item2
        );
    }

    ///// <summary>
    ///// Returns a new vector containing component-wise ceiling values of the given vector
    ///// </summary>
    ///// <param name="vector"></param>
    ///// <returns></returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector2D<T> Ceiling<T>(this IPair<Scalar<T>> vector)
    //{
    //    return LinVector2D<T>.Create(
    //        Math.Ceiling(vector.Item1),
    //        Math.Ceiling(vector.Item2)
    //    );
    //}

    ///// <summary>
    ///// Returns a new vector containing component-wise floor values of the given vector
    ///// </summary>
    ///// <param name="vector"></param>
    ///// <returns></returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector2D<T> Floor<T>(this IPair<Scalar<T>> vector)
    //{
    //    return LinVector2D<T>.Create(
    //        Math.Floor(vector.Item1),
    //        Math.Floor(vector.Item2)
    //    );
    //}

    /// <summary>
    /// Returns a new vector containing component-wise absolute values of the given vector
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Abs<T>(this IPair<Scalar<T>> vector)
    {
        return LinVector2D<T>.Create(
            vector.Item1.Abs(),
            vector.Item2.Abs()
        );
    }

    /// <summary>
    /// Returns a permuted version of the components of this vector
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="xIndex"></param>
    /// <param name="yIndex"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Permute<T>(this LinVector2D<T> vector, int xIndex, int yIndex)
    {
        return LinVector2D<T>.Create(
            vector[xIndex],
            vector[yIndex]
        );
    }

    /// <summary>
    /// Returns a permuted version of the components of this vector. The given indices are always
    /// converted to a valid range using modulus operation
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="xIndex"></param>
    /// <param name="yIndex"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> SafePermute<T>(this LinVector2D<T> vector, int xIndex, int yIndex)
    {
        return LinVector2D<T>.Create(
            vector[xIndex.Mod(2)],
            vector[yIndex.Mod(2)]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetComponent<T>(this IPair<Scalar<T>> vector, int index)
    {
        return index switch
        {
            0 => vector.Item1,
            1 => vector.Item2,
            _ => vector.GetScalarProcessor().Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Scalar<T>> GetComponents<T>(this IPair<Scalar<T>> vector)
    {
        yield return vector.Item1;
        yield return vector.Item2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Scalar<T>> GetComponents<T>(this IEnumerable<IPair<Scalar<T>>> vectorsList)
    {
        return vectorsList.SelectMany(t => t.GetComponents());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> GetTupleXPair<T>(this IReadOnlyList<IPair<Scalar<T>>> itemArray, int index)
    {
        return new Pair<Scalar<T>>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<Scalar<T>> GetTupleXTriplet<T>(this IReadOnlyList<IPair<Scalar<T>>> itemArray, int index)
    {
        return new Triplet<Scalar<T>>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<Scalar<T>> GetTupleXQuad<T>(this IReadOnlyList<IPair<Scalar<T>>> itemArray, int index)
    {
        return new Quad<Scalar<T>>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1,
            itemArray[index + 3].Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<Scalar<T>> GetTupleXQuint<T>(this IReadOnlyList<IPair<Scalar<T>>> itemArray, int index)
    {
        return new Quint<Scalar<T>>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1,
            itemArray[index + 3].Item1,
            itemArray[index + 4].Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Hexad<Scalar<T>> GetTupleXHexad<T>(this IReadOnlyList<IPair<Scalar<T>>> itemArray, int index)
    {
        return new Hexad<Scalar<T>>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1,
            itemArray[index + 3].Item1,
            itemArray[index + 4].Item1,
            itemArray[index + 5].Item1
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> GetTupleYPair<T>(this IReadOnlyList<IPair<Scalar<T>>> itemArray, int index)
    {
        return new Pair<Scalar<T>>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<Scalar<T>> GetTupleYTriplet<T>(this IReadOnlyList<IPair<Scalar<T>>> itemArray, int index)
    {
        return new Triplet<Scalar<T>>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<Scalar<T>> GetTupleYQuad<T>(this IReadOnlyList<IPair<Scalar<T>>> itemArray, int index)
    {
        return new Quad<Scalar<T>>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2,
            itemArray[index + 3].Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<Scalar<T>> GetTupleYQuint<T>(this IReadOnlyList<IPair<Scalar<T>>> itemArray, int index)
    {
        return new Quint<Scalar<T>>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2,
            itemArray[index + 3].Item2,
            itemArray[index + 4].Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Hexad<Scalar<T>> GetTupleYHexad<T>(this IReadOnlyList<IPair<Scalar<T>>> itemArray, int index)
    {
        return new Hexad<Scalar<T>>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2,
            itemArray[index + 3].Item2,
            itemArray[index + 4].Item2,
            itemArray[index + 5].Item2
        );
    }
}