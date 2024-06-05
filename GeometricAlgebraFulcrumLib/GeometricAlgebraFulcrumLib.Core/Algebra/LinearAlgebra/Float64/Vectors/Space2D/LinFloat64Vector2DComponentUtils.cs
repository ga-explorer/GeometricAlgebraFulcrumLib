using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

public static class LinFloat64Vector2DComponentUtils
{
    /// <summary>
    /// The value of the smallest component in this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetMinComponent(this IPair<Float64Scalar> vector)
    {
        return vector.Item1 <= vector.Item2
            ? vector.Item1
            : vector.Item2;
    }

    /// <summary>
    /// The value of the largest component in this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetMaxComponent(this IPair<Float64Scalar> vector)
    {
        return vector.Item1 >= vector.Item2
            ? vector.Item1
            : vector.Item2;
    }

    /// <summary>
    /// The index of the smallest component of this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMinComponentIndex(this IPair<Float64Scalar> vector)
    {
        return vector.Item1 <= vector.Item2
            ? 0 : 1;
    }

    /// <summary>
    /// The index of the largest component of this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMaxComponentIndex(this IPair<Float64Scalar> vector)
    {
        return vector.Item1 >= vector.Item2
            ? 0 : 1;
    }

    /// <summary>
    /// The index of the largest absolute component in this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMinAbsComponentIndex(this IPair<Float64Scalar> vector)
    {
        return Math.Abs(vector.Item1) <= Math.Abs(vector.Item2)
            ? 0 : 1;
    }

    /// <summary>
    /// The index of the largest absolute component in this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMaxAbsComponentIndex(this IPair<Float64Scalar> vector)
    {
        return Math.Abs(vector.Item1) >= Math.Abs(vector.Item2)
            ? 0 : 1;
    }

    /// <summary>
    /// Returns a new vector containing component-wise minimum values of the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D Min(this IPair<Float64Scalar> v1, IPair<Float64Scalar> v2)
    {
        return LinFloat64Vector2D.Create(
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
    public static LinFloat64Vector2D Max(this IPair<Float64Scalar> v1, IPair<Float64Scalar> v2)
    {
        return LinFloat64Vector2D.Create(
            v1.Item1 > v2.Item1 ? v1.Item1 : v2.Item1,
            v1.Item2 > v2.Item2 ? v1.Item2 : v2.Item2
        );
    }

    /// <summary>
    /// Returns a new vector containing component-wise ceiling values of the given vector
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D Ceiling(this IPair<Float64Scalar> vector)
    {
        return LinFloat64Vector2D.Create(
            Math.Ceiling(vector.Item1),
            Math.Ceiling(vector.Item2)
        );
    }

    /// <summary>
    /// Returns a new vector containing component-wise floor values of the given vector
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D Floor(this IPair<Float64Scalar> vector)
    {
        return LinFloat64Vector2D.Create(
            Math.Floor(vector.Item1),
            Math.Floor(vector.Item2)
        );
    }

    /// <summary>
    /// Returns a new vector containing component-wise absolute values of the given vector
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D Abs(this IPair<Float64Scalar> vector)
    {
        return LinFloat64Vector2D.Create(
            Math.Abs(vector.Item1),
            Math.Abs(vector.Item2)
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
    public static LinFloat64Vector2D Permute(this IPair<Float64Scalar> vector, int xIndex, int yIndex)
    {
        return LinFloat64Vector2D.Create(
            vector.GetComponent(xIndex),
            vector.GetComponent(yIndex)
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
    public static LinFloat64Vector2D SafePermute(this IPair<Float64Scalar> vector, int xIndex, int yIndex)
    {
        return LinFloat64Vector2D.Create(
            vector.GetComponent(xIndex.Mod(2)),
            vector.GetComponent(yIndex.Mod(2))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetComponent(this IPair<Float64Scalar> vector, int index)
    {
        return index switch
        {
            0 => vector.Item1,
            1 => vector.Item2,
            _ => Float64Scalar.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Float64Scalar> GetComponents(this IPair<Float64Scalar> vector)
    {
        yield return vector.Item1;
        yield return vector.Item2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Float64Scalar> GetComponents(this IEnumerable<IPair<Float64Scalar>> vectorsList)
    {
        return vectorsList.SelectMany(t => t.GetComponents());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> GetTupleXPair(this IReadOnlyList<IPair<Float64Scalar>> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<double> GetTupleXTriplet(this IReadOnlyList<IPair<Float64Scalar>> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<double> GetTupleXQuad(this IReadOnlyList<IPair<Float64Scalar>> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1,
            itemArray[index + 3].Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<double> GetTupleXQuint(this IReadOnlyList<IPair<Float64Scalar>> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1,
            itemArray[index + 3].Item1,
            itemArray[index + 4].Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Hexad<double> GetTupleXHexad(this IReadOnlyList<IPair<Float64Scalar>> itemArray, int index)
    {
        return new Hexad<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1,
            itemArray[index + 3].Item1,
            itemArray[index + 4].Item1,
            itemArray[index + 5].Item1
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> GetTupleYPair(this IReadOnlyList<IPair<Float64Scalar>> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<double> GetTupleYTriplet(this IReadOnlyList<IPair<Float64Scalar>> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<double> GetTupleYQuad(this IReadOnlyList<IPair<Float64Scalar>> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2,
            itemArray[index + 3].Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<double> GetTupleYQuint(this IReadOnlyList<IPair<Float64Scalar>> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2,
            itemArray[index + 3].Item2,
            itemArray[index + 4].Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Hexad<double> GetTupleYHexad(this IReadOnlyList<IPair<Float64Scalar>> itemArray, int index)
    {
        return new Hexad<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2,
            itemArray[index + 3].Item2,
            itemArray[index + 4].Item2,
            itemArray[index + 5].Item2
        );
    }
}