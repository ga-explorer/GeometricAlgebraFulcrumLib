using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

public static class Float64Vector2DComponentUtils
{
    
    /// <summary>
    /// The value of the smallest component in this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetMinComponent(this IPair<double> tuple)
    {
        return tuple.Item1 <= tuple.Item2 
            ? tuple.Item1 
            : tuple.Item2;
    }

    /// <summary>
    /// The value of the largest component in this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetMaxComponent(this IPair<double> tuple)
    {
        return tuple.Item1 >= tuple.Item2 
            ? tuple.Item1 
            : tuple.Item2;
    }

    /// <summary>
    /// The index of the smallest component of this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMinComponentIndex(this IPair<double> tuple)
    {
        return tuple.Item1 <= tuple.Item2 
            ? 0 : 1;
    }

    /// <summary>
    /// The index of the largest component of this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMaxComponentIndex(this IPair<double> tuple)
    {
        return tuple.Item1 >= tuple.Item2 
            ? 0 : 1;
    }

    /// <summary>
    /// The index of the largest absolute component in this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMinAbsComponentIndex(this IPair<double> tuple)
    {
        return Math.Abs(tuple.Item1) <= Math.Abs(tuple.Item2) 
            ? 0 : 1;
    }

    /// <summary>
    /// The index of the largest absolute component in this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMaxAbsComponentIndex(this IFloat64Vector2D tuple)
    {
        return Math.Abs(tuple.Item1) >= Math.Abs(tuple.Item2) 
            ? 0 : 1;
    }

    /// <summary>
    /// Returns a new tuple containing component-wise minimum values of the given tuples
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Min(this IFloat64Vector2D v1, IFloat64Vector2D v2)
    {
        return Float64Vector2D.Create(
            v1.X < v2.X ? v1.X : v2.X,
            v1.Y < v2.Y ? v1.Y : v2.Y
        );
    }

    /// <summary>
    /// Returns a new tuple containing component-wise maximum values of the given tuples
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Max(this IFloat64Vector2D v1, IFloat64Vector2D v2)
    {
        return Float64Vector2D.Create(
            v1.X > v2.X ? v1.X : v2.X,
            v1.Y > v2.Y ? v1.Y : v2.Y
        );
    }

    /// <summary>
    /// Returns a new tuple containing component-wise ceiling values of the given tuple
    /// </summary>
    /// <param name="tuple"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Ceiling(this IFloat64Vector2D tuple)
    {
        return Float64Vector2D.Create(
            Math.Ceiling(tuple.X),
            Math.Ceiling(tuple.Y)
        );
    }

    /// <summary>
    /// Returns a new tuple containing component-wise floor values of the given tuple
    /// </summary>
    /// <param name="tuple"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Floor(this IFloat64Vector2D tuple)
    {
        return Float64Vector2D.Create(
            Math.Floor(tuple.X),
            Math.Floor(tuple.Y)
        );
    }

    /// <summary>
    /// Returns a new tuple containing component-wise absolute values of the given tuple
    /// </summary>
    /// <param name="tuple"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Abs(this IPair<double> tuple)
    {
        return Float64Vector2D.Create(
            Math.Abs(tuple.Item1),
            Math.Abs(tuple.Item2)
        );
    }

    /// <summary>
    /// Returns a permuted version of the components of this tuple
    /// </summary>
    /// <param name="tuple"></param>
    /// <param name="xIndex"></param>
    /// <param name="yIndex"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Permute(this Float64Vector2D tuple, int xIndex, int yIndex)
    {
        return Float64Vector2D.Create(
            tuple[xIndex], 
            tuple[yIndex]
        );
    }

    /// <summary>
    /// Returns a permuted version of the components of this tuple. The given indices are always
    /// converted to a valid range using modulus operation
    /// </summary>
    /// <param name="tuple"></param>
    /// <param name="xIndex"></param>
    /// <param name="yIndex"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D SafePermute(this Float64Vector2D tuple, int xIndex, int yIndex)
    {
        return Float64Vector2D.Create(
            tuple[xIndex.Mod(2)], 
            tuple[yIndex.Mod(2)]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetComponent(this IFloat64Vector2D tuple, int index)
    {
        return index switch
        {
            0 => tuple.X,
            1 => tuple.Y,
            _ => Float64Scalar.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Float64Scalar> GetComponents(this IFloat64Vector2D tuple)
    {
        yield return tuple.X;
        yield return tuple.Y;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Float64Scalar> GetComponents(this IEnumerable<IFloat64Vector2D> tuplesList)
    {
        return tuplesList.SelectMany(t => t.GetComponents());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> GetTupleXPair(this IReadOnlyList<IFloat64Vector2D> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].X,
            itemArray[index + 1].X
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<double> GetTupleXTriplet(this IReadOnlyList<IFloat64Vector2D> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].X,
            itemArray[index + 1].X,
            itemArray[index + 2].X
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<double> GetTupleXQuad(this IReadOnlyList<IFloat64Vector2D> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].X,
            itemArray[index + 1].X,
            itemArray[index + 2].X,
            itemArray[index + 3].X
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<double> GetTupleXQuint(this IReadOnlyList<IFloat64Vector2D> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].X,
            itemArray[index + 1].X,
            itemArray[index + 2].X,
            itemArray[index + 3].X,
            itemArray[index + 4].X
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Hexad<double> GetTupleXHexad(this IReadOnlyList<IFloat64Vector2D> itemArray, int index)
    {
        return new Hexad<double>(
            itemArray[index].X,
            itemArray[index + 1].X,
            itemArray[index + 2].X,
            itemArray[index + 3].X,
            itemArray[index + 4].X,
            itemArray[index + 5].X
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> GetTupleYPair(this IReadOnlyList<IFloat64Vector2D> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Y,
            itemArray[index + 1].Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<double> GetTupleYTriplet(this IReadOnlyList<IFloat64Vector2D> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Y,
            itemArray[index + 1].Y,
            itemArray[index + 2].Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<double> GetTupleYQuad(this IReadOnlyList<IFloat64Vector2D> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Y,
            itemArray[index + 1].Y,
            itemArray[index + 2].Y,
            itemArray[index + 3].Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<double> GetTupleYQuint(this IReadOnlyList<IFloat64Vector2D> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].Y,
            itemArray[index + 1].Y,
            itemArray[index + 2].Y,
            itemArray[index + 3].Y,
            itemArray[index + 4].Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Hexad<double> GetTupleYHexad(this IReadOnlyList<IFloat64Vector2D> itemArray, int index)
    {
        return new Hexad<double>(
            itemArray[index].Y,
            itemArray[index + 1].Y,
            itemArray[index + 2].Y,
            itemArray[index + 3].Y,
            itemArray[index + 4].Y,
            itemArray[index + 5].Y
        );
    }
}