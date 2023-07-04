using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

public static class Float64Vector3DComponentUtils
{
    
    /// <summary>
    /// The value of the smallest component in this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetMinComponent(this ITriplet<double> tuple)
    {
        return tuple.Item1 < tuple.Item2
            ? tuple.Item1 < tuple.Item3 ? tuple.Item1 : tuple.Item3
            : tuple.Item2 < tuple.Item3 ? tuple.Item2 : tuple.Item3;
    }

    /// <summary>
    /// The value of the largest component in this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetMaxComponent(this ITriplet<double> tuple)
    {
        return tuple.Item1 > tuple.Item2
            ? tuple.Item1 > tuple.Item3 ? tuple.Item1 : tuple.Item3
            : tuple.Item2 > tuple.Item3 ? tuple.Item2 : tuple.Item3;
    }

    /// <summary>
    /// The index of the smallest component in this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMinComponentIndex(this ITriplet<double> tuple)
    {
        return tuple.Item1 < tuple.Item2 ? tuple.Item1 < tuple.Item3 ? 0 : 2 : tuple.Item2 < tuple.Item3 ? 1 : 2;
    }

    /// <summary>
    /// The index of the largest component in this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMaxComponentIndex(this ITriplet<double> tuple)
    {
        return tuple.Item1 > tuple.Item2 ? tuple.Item1 > tuple.Item3 ? 0 : 2 : tuple.Item2 > tuple.Item3 ? 1 : 2;
    }

    /// <summary>
    /// The index of the largest absolute component in this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMaxAbsComponentIndex(this ITriplet<double> tuple)
    {
        var absX = Math.Abs(tuple.Item1);
        var absY = Math.Abs(tuple.Item2);
        var absZ = Math.Abs(tuple.Item3);

        if (absX > absY)
            return absX > absZ ? 0 : 2;

        return absY > absZ ? 1 : 2;
    }

    /// <summary>
    /// The index of the largest absolute component in this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMinAbsComponentIndex(this ITriplet<double> tuple)
    {
        var absX = Math.Abs(tuple.Item1);
        var absY = Math.Abs(tuple.Item2);
        var absZ = Math.Abs(tuple.Item3);

        if (absX < absY)
            return absX < absZ ? 0 : 2;

        return absY < absZ ? 1 : 2;
    }

    /// <summary>
    /// Returns a new tuple containing component-wise minimum values of the given tuples
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Min(this IFloat64Vector3D v1, IFloat64Vector3D v2)
    {
        return Float64Vector3D.Create(v1.X < v2.X ? v1.X : v2.X,
            v1.Y < v2.Y ? v1.Y : v2.Y,
            v1.Z < v2.Z ? v1.Z : v2.Z);
    }

    /// <summary>
    /// Returns a new tuple containing component-wise maximum values of the given tuples
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Max(this IFloat64Vector3D v1, IFloat64Vector3D v2)
    {
        return Float64Vector3D.Create(v1.X > v2.X ? v1.X : v2.X,
            v1.Y > v2.Y ? v1.Y : v2.Y,
            v1.Z > v2.Z ? v1.Z : v2.Z);
    }
    
    /// <summary>
    /// Returns a permuted version of the components of this tuple
    /// </summary>
    /// <param name="tuple"></param>
    /// <param name="xIndex"></param>
    /// <param name="yIndex"></param>
    /// <param name="zIndex"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Permute(this Float64Vector3D tuple, int xIndex, int yIndex, int zIndex)
    {
        return Float64Vector3D.Create(tuple[1 << xIndex],
            tuple[1 << yIndex],
            tuple[1 << zIndex]);
    }

    /// <summary>
    /// Returns a permuted version of the components of this tuple. The given indices are always
    /// converted to a valid range using modulus operation
    /// </summary>
    /// <param name="tuple"></param>
    /// <param name="xIndex"></param>
    /// <param name="yIndex"></param>
    /// <param name="zIndex"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D SafePermute(this Float64Vector3D tuple, int xIndex, int yIndex, int zIndex)
    {
        return Float64Vector3D.Create(tuple[1 << xIndex.Mod(3)],
            tuple[1 << yIndex.Mod(3)],
            tuple[1 << zIndex.Mod(3)]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetComponent(this IFloat64Vector3D tuple, int index)
    {
        return index switch
        {
            0 => tuple.X,
            1 => tuple.Y,
            2 => tuple.Z,
            _ => Float64Scalar.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetX(this LinUnitBasisVector3D tuple)
    {
        return tuple switch
        {
            LinUnitBasisVector3D.PositiveX => Float64Scalar.One,
            LinUnitBasisVector3D.NegativeX => Float64Scalar.NegativeOne,
            _ => Float64Scalar.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetY(this LinUnitBasisVector3D tuple)
    {
        return tuple switch
        {
            LinUnitBasisVector3D.PositiveY => Float64Scalar.One,
            LinUnitBasisVector3D.NegativeY => Float64Scalar.NegativeOne,
            _ => Float64Scalar.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetZ(this LinUnitBasisVector3D tuple)
    {
        return tuple switch
        {
            LinUnitBasisVector3D.PositiveZ => Float64Scalar.One,
            LinUnitBasisVector3D.NegativeZ => Float64Scalar.NegativeOne,
            _ => Float64Scalar.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetComponent(this LinUnitBasisVector3D tuple, LinUnitBasisVector3D axis)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => tuple.GetX(),
            LinUnitBasisVector3D.NegativeX => -tuple.GetX(),
            LinUnitBasisVector3D.PositiveY => tuple.GetY(),
            LinUnitBasisVector3D.NegativeY => -tuple.GetY(),
            LinUnitBasisVector3D.PositiveZ => tuple.GetZ(),
            LinUnitBasisVector3D.NegativeZ => -tuple.GetZ(),
            _ => Float64Scalar.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetComponent(this IFloat64Vector3D tuple, LinUnitBasisVector3D axis)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => tuple.X,
            LinUnitBasisVector3D.NegativeX => -tuple.X,
            LinUnitBasisVector3D.PositiveY => tuple.Y,
            LinUnitBasisVector3D.NegativeY => -tuple.Y,
            LinUnitBasisVector3D.PositiveZ => tuple.Z,
            LinUnitBasisVector3D.NegativeZ => -tuple.Z,
            _ => Float64Scalar.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Float64Scalar> GetComponents(this IFloat64Vector3D tuple)
    {
        yield return tuple.X;
        yield return tuple.Y;
        yield return tuple.Z;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Float64Scalar> GetComponents(this IEnumerable<IFloat64Vector3D> tuplesList)
    {
        return tuplesList.SelectMany(t => t.GetComponents());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D MapComponents(this IFloat64Vector3D tuple, Func<double, double> scalarMapping)
    {
        return Float64Vector3D.Create(scalarMapping(tuple.X),
            scalarMapping(tuple.Y),
            scalarMapping(tuple.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ComponentsMin(this IFloat64Vector3D tuple, double scalar)
    {
        return Float64Vector3D.Create(Math.Min(tuple.X, scalar),
            Math.Min(tuple.Y, scalar),
            Math.Min(tuple.Z, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ComponentsMax(this IFloat64Vector3D tuple, double scalar)
    {
        return Float64Vector3D.Create(Math.Max(tuple.X, scalar),
            Math.Max(tuple.Y, scalar),
            Math.Max(tuple.Z, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ComponentsMin(this IFloat64Vector3D tuple)
    {
        return Math.Min(tuple.X, Math.Min(tuple.Y, tuple.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ComponentsMax(this IFloat64Vector3D tuple)
    {
        return Math.Max(tuple.X, Math.Max(tuple.Y, tuple.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ComponentsAbs(this IFloat64Vector3D tuple)
    {
        return Float64Vector3D.Create(Math.Abs(tuple.X),
            Math.Abs(tuple.Y),
            Math.Abs(tuple.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ComponentsProduct(this IFloat64Vector3D tuple1, IFloat64Vector3D tuple2)
    {
        return Float64Vector3D.Create(tuple1.X * tuple2.X,
            tuple1.Y * tuple2.Y,
            tuple1.Z * tuple2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ComponentsProduct(this IFloat64Vector3D tuple1, IFloat64Vector3D tuple2, IFloat64Vector3D tuple3)
    {
        return Float64Vector3D.Create(tuple1.X * tuple2.X * tuple3.X,
            tuple1.Y * tuple2.Y * tuple3.Y,
            tuple1.Z * tuple2.Z * tuple3.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> GetTupleXPair(this IReadOnlyList<IFloat64Vector3D> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].X,
            itemArray[index + 1].X
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<double> GetTupleXTriplet(this IReadOnlyList<IFloat64Vector3D> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].X,
            itemArray[index + 1].X,
            itemArray[index + 2].X
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<double> GetTupleXQuad(this IReadOnlyList<IFloat64Vector3D> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].X,
            itemArray[index + 1].X,
            itemArray[index + 2].X,
            itemArray[index + 3].X
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<double> GetTupleXQuint(this IReadOnlyList<IFloat64Vector3D> itemArray, int index)
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
    public static Hexad<double> GetTupleXHexad(this IReadOnlyList<IFloat64Vector3D> itemArray, int index)
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
    public static Pair<double> GetTupleYPair(this IReadOnlyList<IFloat64Vector3D> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Y,
            itemArray[index + 1].Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<double> GetTupleYTriplet(this IReadOnlyList<IFloat64Vector3D> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Y,
            itemArray[index + 1].Y,
            itemArray[index + 2].Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<double> GetTupleYQuad(this IReadOnlyList<IFloat64Vector3D> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Y,
            itemArray[index + 1].Y,
            itemArray[index + 2].Y,
            itemArray[index + 3].Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<double> GetTupleYQuint(this IReadOnlyList<IFloat64Vector3D> itemArray, int index)
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
    public static Hexad<double> GetTupleYHexad(this IReadOnlyList<IFloat64Vector3D> itemArray, int index)
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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> GetTupleZPair(this IReadOnlyList<IFloat64Vector3D> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Z,
            itemArray[index + 1].Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<double> GetTupleZTriplet(this IReadOnlyList<IFloat64Vector3D> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Z,
            itemArray[index + 1].Z,
            itemArray[index + 2].Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<double> GetTupleZQuad(this IReadOnlyList<IFloat64Vector3D> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Z,
            itemArray[index + 1].Z,
            itemArray[index + 2].Z,
            itemArray[index + 3].Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<double> GetTupleZQuint(this IReadOnlyList<IFloat64Vector3D> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].Z,
            itemArray[index + 1].Z,
            itemArray[index + 2].Z,
            itemArray[index + 3].Z,
            itemArray[index + 4].Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Hexad<double> GetTupleZHexad(this IReadOnlyList<IFloat64Vector3D> itemArray, int index)
    {
        return new Hexad<double>(
            itemArray[index].Z,
            itemArray[index + 1].Z,
            itemArray[index + 2].Z,
            itemArray[index + 3].Z,
            itemArray[index + 4].Z,
            itemArray[index + 5].Z
        );
    }
    
    // TODO: Make more of these
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Ceiling(this ITriplet<double> tuple)
    {
        return tuple.ToVector3D(Math.Ceiling);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Floor(this ITriplet<double> tuple)
    {
        return tuple.ToVector3D(Math.Floor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Abs(this ITriplet<double> tuple)
    {
        return tuple.ToVector3D(Math.Abs);
    }

}