using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;

public static class LinVector3DComponentUtils
{

    /// <summary>
    /// The value of the smallest component in this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetMinComponent<T>(this ITriplet<Scalar<T>> vector)
    {
        return vector.Item1 < vector.Item2
            ? vector.Item1 < vector.Item3 ? vector.Item1 : vector.Item3
            : vector.Item2 < vector.Item3 ? vector.Item2 : vector.Item3;
    }

    /// <summary>
    /// The value of the largest component in this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetMaxComponent<T>(this ITriplet<Scalar<T>> vector)
    {
        return vector.Item1 > vector.Item2
            ? vector.Item1 > vector.Item3 ? vector.Item1 : vector.Item3
            : vector.Item2 > vector.Item3 ? vector.Item2 : vector.Item3;
    }

    /// <summary>
    /// The index of the smallest component in this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMinComponentIndex<T>(this ITriplet<Scalar<T>> vector)
    {
        return vector.Item1 < vector.Item2 ? vector.Item1 < vector.Item3 ? 0 : 2 : vector.Item2 < vector.Item3 ? 1 : 2;
    }

    /// <summary>
    /// The index of the largest component in this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMaxComponentIndex<T>(this ITriplet<Scalar<T>> vector)
    {
        return vector.Item1 > vector.Item2 
            ? vector.Item1 > vector.Item3 ? 0 : 2 
            : vector.Item2 > vector.Item3 ? 1 : 2;
    }

    /// <summary>
    /// The index of the largest absolute component in this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMaxAbsComponentIndex<T>(this ITriplet<Scalar<T>> vector)
    {
        var absX = vector.Item1.Abs();
        var absY = vector.Item2.Abs();
        var absZ = vector.Item3.Abs();

        if (absX > absY)
            return absX > absZ ? 0 : 2;

        return absY > absZ ? 1 : 2;
    }

    /// <summary>
    /// The index of the largest absolute component in this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMinAbsComponentIndex<T>(this ITriplet<Scalar<T>> vector)
    {
        var absX = vector.Item1.Abs();
        var absY = vector.Item2.Abs();
        var absZ = vector.Item3.Abs();

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
    public static LinVector3D<T> Min<T>(this ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2)
    {
        return LinVector3D<T>.Create(
            v1.Item1 < v2.Item1 ? v1.Item1 : v2.Item1,
            v1.Item2 < v2.Item2 ? v1.Item2 : v2.Item2,
            v1.Item3 < v2.Item3 ? v1.Item3 : v2.Item3
        );
    }

    /// <summary>
    /// Returns a new vector containing component-wise maximum values of the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> Max<T>(this ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2)
    {
        return LinVector3D<T>.Create(
            v1.Item1 > v2.Item1 ? v1.Item1 : v2.Item1,
            v1.Item2 > v2.Item2 ? v1.Item2 : v2.Item2,
            v1.Item3 > v2.Item3 ? v1.Item3 : v2.Item3
        );
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
    public static LinVector3D<T> Permute<T>(this LinVector3D<T> vector, int xIndex, int yIndex, int zIndex)
    {
        return LinVector3D<T>.Create(
            vector[1 << xIndex],
            vector[1 << yIndex],
            vector[1 << zIndex]
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
    public static LinVector3D<T> SafePermute<T>(this LinVector3D<T> vector, int xIndex, int yIndex, int zIndex)
    {
        return LinVector3D<T>.Create(
            vector[1 << xIndex.Mod(3)],
            vector[1 << yIndex.Mod(3)],
            vector[1 << zIndex.Mod(3)]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetComponent<T>(this ITriplet<Scalar<T>> vector, int index)
    {
        return index switch
        {
            0 => vector.Item1,
            1 => vector.Item2,
            2 => vector.Item3,
            _ => vector.GetScalarProcessor().Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetX<T>(this LinUnitBasisVector3D vector, IScalarProcessor<T> scalarProcessor)
    {
        return vector switch
        {
            LinUnitBasisVector3D.PositiveX => scalarProcessor.One,
            LinUnitBasisVector3D.NegativeX => scalarProcessor.MinusOne,
            _ => scalarProcessor.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetY<T>(this LinUnitBasisVector3D vector, IScalarProcessor<T> scalarProcessor)
    {
        return vector switch
        {
            LinUnitBasisVector3D.PositiveY => scalarProcessor.One,
            LinUnitBasisVector3D.NegativeY => scalarProcessor.MinusOne,
            _ => scalarProcessor.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetZ<T>(this LinUnitBasisVector3D vector, IScalarProcessor<T> scalarProcessor)
    {
        return vector switch
        {
            LinUnitBasisVector3D.PositiveZ => scalarProcessor.One,
            LinUnitBasisVector3D.NegativeZ => scalarProcessor.MinusOne,
            _ => scalarProcessor.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetComponent<T>(this LinUnitBasisVector3D vector, LinUnitBasisVector3D axis, IScalarProcessor<T> scalarProcessor)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => vector.GetX(scalarProcessor),
            LinUnitBasisVector3D.NegativeX => -vector.GetX(scalarProcessor),
            LinUnitBasisVector3D.PositiveY => vector.GetY(scalarProcessor),
            LinUnitBasisVector3D.NegativeY => -vector.GetY(scalarProcessor),
            LinUnitBasisVector3D.PositiveZ => vector.GetZ(scalarProcessor),
            LinUnitBasisVector3D.NegativeZ => -vector.GetZ(scalarProcessor),
            _ => scalarProcessor.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetComponent<T>(this ITriplet<Scalar<T>> vector, LinUnitBasisVector3D axis)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => vector.Item1,
            LinUnitBasisVector3D.NegativeX => -vector.Item1,
            LinUnitBasisVector3D.PositiveY => vector.Item2,
            LinUnitBasisVector3D.NegativeY => -vector.Item2,
            LinUnitBasisVector3D.PositiveZ => vector.Item3,
            LinUnitBasisVector3D.NegativeZ => -vector.Item3,
            _ => vector.GetScalarProcessor().Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Scalar<T>> GetComponents<T>(this ITriplet<Scalar<T>> vector)
    {
        yield return vector.Item1;
        yield return vector.Item2;
        yield return vector.Item3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Scalar<T>> GetComponents<T>(this IEnumerable<ITriplet<Scalar<T>>> vectorsList)
    {
        return vectorsList.SelectMany(t => t.GetComponents());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> MapComponents<T>(this ITriplet<Scalar<T>> vector, Func<Scalar<T>, Scalar<T>> scalarMapping)
    {
        return LinVector3D<T>.Create(scalarMapping(vector.Item1),
            scalarMapping(vector.Item2),
            scalarMapping(vector.Item3));
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector3D<T> ComponentsMin<T>(this ITriplet<Scalar<T>> vector, Scalar<T> scalar)
    //{
    //    return LinVector3D<T>.Create(
    //        Math.Min(vector.Item1, scalar),
    //        Math.Min(vector.Item2, scalar),
    //        Math.Min(vector.Item3, scalar)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector3D<T> ComponentsMax<T>(this ITriplet<Scalar<T>> vector, Scalar<T> scalar)
    //{
    //    return LinVector3D<T>.Create(
    //        Math.Max(vector.Item1, scalar),
    //        Math.Max(vector.Item2, scalar),
    //        Math.Max(vector.Item3, scalar)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Scalar<T> ComponentsMin<T>(this ITriplet<Scalar<T>> vector)
    //{
    //    return Math.Min(vector.Item1, Math.Min(vector.Item2, vector.Item3));
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Scalar<T> ComponentsMax<T>(this ITriplet<Scalar<T>> vector)
    //{
    //    return Math.Max(vector.Item1, Math.Max(vector.Item2, vector.Item3));
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ComponentsAbs<T>(this ITriplet<Scalar<T>> vector)
    {
        return LinVector3D<T>.Create(
            vector.Item1.Abs(),
            vector.Item2.Abs(),
            vector.Item3.Abs()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ComponentsProduct<T>(this ITriplet<Scalar<T>> vector1, ITriplet<Scalar<T>> vector2)
    {
        return LinVector3D<T>.Create(vector1.Item1 * vector2.Item1,
            vector1.Item2 * vector2.Item2,
            vector1.Item3 * vector2.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ComponentsProduct<T>(this ITriplet<Scalar<T>> vector1, ITriplet<Scalar<T>> vector2, ITriplet<Scalar<T>> vector3)
    {
        return LinVector3D<T>.Create(vector1.Item1 * vector2.Item1 * vector3.Item1,
            vector1.Item2 * vector2.Item2 * vector3.Item2,
            vector1.Item3 * vector2.Item3 * vector3.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> GetTupleXPair<T>(this IReadOnlyList<ITriplet<Scalar<T>>> itemArray, int index)
    {
        return new Pair<Scalar<T>>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<Scalar<T>> GetTupleXTriplet<T>(this IReadOnlyList<ITriplet<Scalar<T>>> itemArray, int index)
    {
        return new Triplet<Scalar<T>>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<Scalar<T>> GetTupleXQuad<T>(this IReadOnlyList<ITriplet<Scalar<T>>> itemArray, int index)
    {
        return new Quad<Scalar<T>>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1,
            itemArray[index + 3].Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<Scalar<T>> GetTupleXQuint<T>(this IReadOnlyList<ITriplet<Scalar<T>>> itemArray, int index)
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
    public static Hexad<Scalar<T>> GetTupleXHexad<T>(this IReadOnlyList<ITriplet<Scalar<T>>> itemArray, int index)
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
    public static Pair<Scalar<T>> GetTupleYPair<T>(this IReadOnlyList<ITriplet<Scalar<T>>> itemArray, int index)
    {
        return new Pair<Scalar<T>>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<Scalar<T>> GetTupleYTriplet<T>(this IReadOnlyList<ITriplet<Scalar<T>>> itemArray, int index)
    {
        return new Triplet<Scalar<T>>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<Scalar<T>> GetTupleYQuad<T>(this IReadOnlyList<ITriplet<Scalar<T>>> itemArray, int index)
    {
        return new Quad<Scalar<T>>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2,
            itemArray[index + 3].Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<Scalar<T>> GetTupleYQuint<T>(this IReadOnlyList<ITriplet<Scalar<T>>> itemArray, int index)
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
    public static Hexad<Scalar<T>> GetTupleYHexad<T>(this IReadOnlyList<ITriplet<Scalar<T>>> itemArray, int index)
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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> GetTupleZPair<T>(this IReadOnlyList<ITriplet<Scalar<T>>> itemArray, int index)
    {
        return new Pair<Scalar<T>>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<Scalar<T>> GetTupleZTriplet<T>(this IReadOnlyList<ITriplet<Scalar<T>>> itemArray, int index)
    {
        return new Triplet<Scalar<T>>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3,
            itemArray[index + 2].Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<Scalar<T>> GetTupleZQuad<T>(this IReadOnlyList<ITriplet<Scalar<T>>> itemArray, int index)
    {
        return new Quad<Scalar<T>>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3,
            itemArray[index + 2].Item3,
            itemArray[index + 3].Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<Scalar<T>> GetTupleZQuint<T>(this IReadOnlyList<ITriplet<Scalar<T>>> itemArray, int index)
    {
        return new Quint<Scalar<T>>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3,
            itemArray[index + 2].Item3,
            itemArray[index + 3].Item3,
            itemArray[index + 4].Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Hexad<Scalar<T>> GetTupleZHexad<T>(this IReadOnlyList<ITriplet<Scalar<T>>> itemArray, int index)
    {
        return new Hexad<Scalar<T>>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3,
            itemArray[index + 2].Item3,
            itemArray[index + 3].Item3,
            itemArray[index + 4].Item3,
            itemArray[index + 5].Item3
        );
    }

    // TODO: Make more of these
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector3D<T> Ceiling<T>(this ITriplet<Scalar<T>> vector)
    //{
    //    return vector.ToVector3D(Math.Ceiling);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector3D<T> Floor<T>(this ITriplet<Scalar<T>> vector)
    //{
    //    return vector.ToVector3D(Math.Floor);
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> Abs<T>(this ITriplet<Scalar<T>> vector)
    {
        return vector.ToVector3D(
            s => s.Abs()
        );
    }

}