using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

public static class LinFloat64Vector3DComponentUtils
{
    /// <summary>
    /// The value of the smallest component in this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetMinComponent(this ITriplet<Float64Scalar> vector)
    {
        return vector.Item1 < vector.Item2
            ? vector.Item1 < vector.Item3 ? vector.Item1 : vector.Item3
            : vector.Item2 < vector.Item3 ? vector.Item2 : vector.Item3;
    }

    /// <summary>
    /// The value of the largest component in this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetMaxComponent(this ITriplet<Float64Scalar> vector)
    {
        return vector.Item1 > vector.Item2
            ? vector.Item1 > vector.Item3 ? vector.Item1 : vector.Item3
            : vector.Item2 > vector.Item3 ? vector.Item2 : vector.Item3;
    }

    /// <summary>
    /// The index of the smallest component in this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMinComponentIndex(this ITriplet<Float64Scalar> vector)
    {
        return vector.Item1 < vector.Item2 ? vector.Item1 < vector.Item3 ? 0 : 2 : vector.Item2 < vector.Item3 ? 1 : 2;
    }

    /// <summary>
    /// The index of the largest component in this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMaxComponentIndex(this ITriplet<Float64Scalar> vector)
    {
        return vector.Item1 > vector.Item2 ? vector.Item1 > vector.Item3 ? 0 : 2 : vector.Item2 > vector.Item3 ? 1 : 2;
    }

    /// <summary>
    /// The index of the largest absolute component in this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMaxAbsComponentIndex(this ITriplet<Float64Scalar> vector)
    {
        var absX = Math.Abs(vector.Item1);
        var absY = Math.Abs(vector.Item2);
        var absZ = Math.Abs(vector.Item3);

        if (absX > absY)
            return absX > absZ ? 0 : 2;

        return absY > absZ ? 1 : 2;
    }

    /// <summary>
    /// The index of the largest absolute component in this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMinAbsComponentIndex(this ITriplet<Float64Scalar> vector)
    {
        var absX = Math.Abs(vector.Item1);
        var absY = Math.Abs(vector.Item2);
        var absZ = Math.Abs(vector.Item3);

        if (absX < absY)
            return absX < absZ ? 0 : 2;

        return absY < absZ ? 1 : 2;
    }

    /// <summary>
    /// Returns a new vector containing component-wise minimum values of the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D Min(this ITriplet<Float64Scalar> v1, ITriplet<Float64Scalar> v2)
    {
        return LinFloat64Vector3D.Create(v1.Item1 < v2.Item1 ? v1.Item1 : v2.Item1,
            v1.Item2 < v2.Item2 ? v1.Item2 : v2.Item2,
            v1.Item3 < v2.Item3 ? v1.Item3 : v2.Item3);
    }

    /// <summary>
    /// Returns a new vector containing component-wise maximum values of the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D Max(this ITriplet<Float64Scalar> v1, ITriplet<Float64Scalar> v2)
    {
        return LinFloat64Vector3D.Create(v1.Item1 > v2.Item1 ? v1.Item1 : v2.Item1,
            v1.Item2 > v2.Item2 ? v1.Item2 : v2.Item2,
            v1.Item3 > v2.Item3 ? v1.Item3 : v2.Item3);
    }

    /// <summary>
    /// Returns a permuted version of the components of this vector
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="xIndex"></param>
    /// <param name="yIndex"></param>
    /// <param name="zIndex"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D Permute(this ITriplet<Float64Scalar> vector, int xIndex, int yIndex, int zIndex)
    {
        return LinFloat64Vector3D.Create(
            vector.GetComponent(1 << xIndex),
            vector.GetComponent(1 << yIndex),
            vector.GetComponent(1 << zIndex)
        );
    }

    /// <summary>
    /// Returns a permuted version of the components of this vector. The given indices are always
    /// converted to a valid range using modulus operation
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="xIndex"></param>
    /// <param name="yIndex"></param>
    /// <param name="zIndex"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D SafePermute(this ITriplet<Float64Scalar> vector, int xIndex, int yIndex, int zIndex)
    {
        return LinFloat64Vector3D.Create(
            vector.GetComponent(1 << xIndex.Mod(3)),
            vector.GetComponent(1 << yIndex.Mod(3)),
            vector.GetComponent(1 << zIndex.Mod(3))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetComponent(this ITriplet<Float64Scalar> vector, int index)
    {
        return index switch
        {
            0 => vector.Item1,
            1 => vector.Item2,
            2 => vector.Item3,
            _ => Float64Scalar.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetX(this LinUnitBasisVector3D vector)
    {
        return vector switch
        {
            LinUnitBasisVector3D.PositiveX => Float64Scalar.One,
            LinUnitBasisVector3D.NegativeX => Float64Scalar.NegativeOne,
            _ => Float64Scalar.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetY(this LinUnitBasisVector3D vector)
    {
        return vector switch
        {
            LinUnitBasisVector3D.PositiveY => Float64Scalar.One,
            LinUnitBasisVector3D.NegativeY => Float64Scalar.NegativeOne,
            _ => Float64Scalar.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetZ(this LinUnitBasisVector3D vector)
    {
        return vector switch
        {
            LinUnitBasisVector3D.PositiveZ => Float64Scalar.One,
            LinUnitBasisVector3D.NegativeZ => Float64Scalar.NegativeOne,
            _ => Float64Scalar.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetComponent(this LinUnitBasisVector3D vector, LinUnitBasisVector3D axis)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => vector.GetX(),
            LinUnitBasisVector3D.NegativeX => -vector.GetX(),
            LinUnitBasisVector3D.PositiveY => vector.GetY(),
            LinUnitBasisVector3D.NegativeY => -vector.GetY(),
            LinUnitBasisVector3D.PositiveZ => vector.GetZ(),
            LinUnitBasisVector3D.NegativeZ => -vector.GetZ(),
            _ => Float64Scalar.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetComponent(this ITriplet<Float64Scalar> vector, LinUnitBasisVector3D axis)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => vector.Item1,
            LinUnitBasisVector3D.NegativeX => -vector.Item1,
            LinUnitBasisVector3D.PositiveY => vector.Item2,
            LinUnitBasisVector3D.NegativeY => -vector.Item2,
            LinUnitBasisVector3D.PositiveZ => vector.Item3,
            LinUnitBasisVector3D.NegativeZ => -vector.Item3,
            _ => Float64Scalar.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Float64Scalar> GetComponents(this ITriplet<Float64Scalar> vector)
    {
        yield return vector.Item1;
        yield return vector.Item2;
        yield return vector.Item3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Float64Scalar> GetComponents(this IEnumerable<ITriplet<Float64Scalar>> vectorsList)
    {
        return vectorsList.SelectMany(t => t.GetComponents());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D MapComponents(this ITriplet<Float64Scalar> vector, Func<double, double> scalarMapping)
    {
        return LinFloat64Vector3D.Create(scalarMapping(vector.Item1),
            scalarMapping(vector.Item2),
            scalarMapping(vector.Item3));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ComponentsMin(this ITriplet<Float64Scalar> vector, double scalar)
    {
        return LinFloat64Vector3D.Create(Math.Min(vector.Item1, scalar),
            Math.Min(vector.Item2, scalar),
            Math.Min(vector.Item3, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ComponentsMax(this ITriplet<Float64Scalar> vector, double scalar)
    {
        return LinFloat64Vector3D.Create(Math.Max(vector.Item1, scalar),
            Math.Max(vector.Item2, scalar),
            Math.Max(vector.Item3, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ComponentsMin(this ITriplet<Float64Scalar> vector)
    {
        return Math.Min(vector.Item1, Math.Min(vector.Item2, vector.Item3));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ComponentsMax(this ITriplet<Float64Scalar> vector)
    {
        return Math.Max(vector.Item1, Math.Max(vector.Item2, vector.Item3));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ComponentsAbs(this ITriplet<Float64Scalar> vector)
    {
        return LinFloat64Vector3D.Create(Math.Abs(vector.Item1),
            Math.Abs(vector.Item2),
            Math.Abs(vector.Item3));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ComponentsProduct(this ITriplet<Float64Scalar> vector1, ITriplet<Float64Scalar> vector2)
    {
        return LinFloat64Vector3D.Create(vector1.Item1 * vector2.Item1,
            vector1.Item2 * vector2.Item2,
            vector1.Item3 * vector2.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ComponentsProduct(this ITriplet<Float64Scalar> vector1, ITriplet<Float64Scalar> vector2, ITriplet<Float64Scalar> vector3)
    {
        return LinFloat64Vector3D.Create(vector1.Item1 * vector2.Item1 * vector3.Item1,
            vector1.Item2 * vector2.Item2 * vector3.Item2,
            vector1.Item3 * vector2.Item3 * vector3.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> GetTupleXPair(this IReadOnlyList<ITriplet<Float64Scalar>> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<double> GetTupleXTriplet(this IReadOnlyList<ITriplet<Float64Scalar>> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<double> GetTupleXQuad(this IReadOnlyList<ITriplet<Float64Scalar>> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1,
            itemArray[index + 3].Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<double> GetTupleXQuint(this IReadOnlyList<ITriplet<Float64Scalar>> itemArray, int index)
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
    public static Hexad<double> GetTupleXHexad(this IReadOnlyList<ITriplet<Float64Scalar>> itemArray, int index)
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
    public static Pair<double> GetTupleYPair(this IReadOnlyList<ITriplet<Float64Scalar>> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<double> GetTupleYTriplet(this IReadOnlyList<ITriplet<Float64Scalar>> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<double> GetTupleYQuad(this IReadOnlyList<ITriplet<Float64Scalar>> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2,
            itemArray[index + 3].Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<double> GetTupleYQuint(this IReadOnlyList<ITriplet<Float64Scalar>> itemArray, int index)
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
    public static Hexad<double> GetTupleYHexad(this IReadOnlyList<ITriplet<Float64Scalar>> itemArray, int index)
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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> GetTupleZPair(this IReadOnlyList<ITriplet<Float64Scalar>> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<double> GetTupleZTriplet(this IReadOnlyList<ITriplet<Float64Scalar>> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3,
            itemArray[index + 2].Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<double> GetTupleZQuad(this IReadOnlyList<ITriplet<Float64Scalar>> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3,
            itemArray[index + 2].Item3,
            itemArray[index + 3].Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<double> GetTupleZQuint(this IReadOnlyList<ITriplet<Float64Scalar>> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3,
            itemArray[index + 2].Item3,
            itemArray[index + 3].Item3,
            itemArray[index + 4].Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Hexad<double> GetTupleZHexad(this IReadOnlyList<ITriplet<Float64Scalar>> itemArray, int index)
    {
        return new Hexad<double>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3,
            itemArray[index + 2].Item3,
            itemArray[index + 3].Item3,
            itemArray[index + 4].Item3,
            itemArray[index + 5].Item3
        );
    }


    // TODO: Make more of these
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D CeilingToLinVector3D(this ITriplet<Float64Scalar> vector)
    {
        return vector.ToLinVector3D(s => Math.Ceiling(s.ScalarValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D FloorToLinVector3D(this ITriplet<Float64Scalar> vector)
    {
        return vector.ToLinVector3D(s => Math.Floor(s.ScalarValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D AbsToLinVector3D(this ITriplet<Float64Scalar> vector)
    {
        return vector.ToLinVector3D(s => Math.Abs(s.ScalarValue));
    }

}