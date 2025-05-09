using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Tuples;

public static class TupleUtils
{
    /// <summary>
    /// The Euclidean cross product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntTuple3D VectorCross(this IntTuple3D v1, IntTuple3D v2)
    {
        return new IntTuple3D(
            v1.ItemY * v2.ItemZ - v1.ItemZ * v2.ItemY,
            v1.ItemZ * v2.ItemX - v1.ItemX * v2.ItemZ,
            v1.ItemX * v2.ItemY - v1.ItemY * v2.ItemX
        );
    }

    /// <summary>
    /// The Euclidean distance between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetDistanceSquaredToPoint(this IntTuple2D v1, IntTuple2D v2)
    {
        return (v2 - v1).VectorLengthSquared;
    }

    /// <summary>
    /// The Euclidean distance between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetDistanceSquaredToPoint(this IntTuple3D v1, IntTuple3D v2)
    {
        return (v2 - v1).VectorLengthSquared;
    }


    /// <summary>
    /// Returns a new vector orthogonal to this one.
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntTuple2D GetNormal(this IntTuple2D vector)
    {
        return new IntTuple2D(-vector.Y, vector.X);
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ESp(this IntTuple3D v1, IntTuple3D v2)
    {
        return v1.ItemX * v2.ItemX + v1.ItemY * v2.ItemY + v1.ItemZ * v2.ItemZ;
    }

    /// <summary>
    /// The absolute value of the Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ESpAbs(this IntTuple2D v1, IntTuple2D v2)
    {
        return Math.Abs(v1.X * v2.X + v1.Y * v2.Y);
    }

    /// <summary>
    /// The absolute value of the Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ESpAbs(this IntTuple3D v1, IntTuple3D v2)
    {
        return Math.Abs(v1.ItemX * v2.ItemX + v1.ItemY * v2.ItemY + v1.ItemZ * v2.ItemZ);
    }


    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ESp(this IntTuple2D v1, IntTuple2D v2)
    {
        return v1.X * v2.X + v1.Y * v2.Y;
    }

    /// <summary>
    /// Returns a new tuple containing component-wise minimum values of the given tuples
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntTuple2D Min(this IntTuple2D v1, IntTuple2D v2)
    {
        return new IntTuple2D(
            v1.X < v2.X ? v1.X : v2.X,
            v1.Y < v2.Y ? v1.Y : v2.Y
        );
    }

    /// <summary>
    /// Returns a new tuple containing component-wise minimum values of the given tuples
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntTuple3D Min(this IntTuple3D v1, IntTuple3D v2)
    {
        return new IntTuple3D(
            v1.ItemX < v2.ItemX ? v1.ItemX : v2.ItemX,
            v1.ItemY < v2.ItemY ? v1.ItemY : v2.ItemY,
            v1.ItemZ < v2.ItemZ ? v1.ItemZ : v2.ItemZ
        );
    }


    /// <summary>
    /// Returns a new tuple containing component-wise maximum values of the given tuples
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntTuple2D Max(this IntTuple2D v1, IntTuple2D v2)
    {
        return new IntTuple2D(
            v1.X > v2.X ? v1.X : v2.X,
            v1.Y > v2.Y ? v1.Y : v2.Y
        );
    }

    /// <summary>
    /// Returns a new tuple containing component-wise maximum values of the given tuples
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntTuple3D Max(this IntTuple3D v1, IntTuple3D v2)
    {
        return new IntTuple3D(
            v1.ItemX > v2.ItemX ? v1.ItemX : v2.ItemX,
            v1.ItemY > v2.ItemY ? v1.ItemY : v2.ItemY,
            v1.ItemZ > v2.ItemZ ? v1.ItemZ : v2.ItemZ
        );
    }



    /// <summary>
    /// Returns a new tuple containing component-wise absolute values of the given tuple
    /// </summary>
    /// <param name="tuple"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntTuple2D Abs(this IntTuple2D tuple)
    {
        return new IntTuple2D(
            Math.Abs(tuple.X),
            Math.Abs(tuple.Y)
        );
    }

    /// <summary>
    /// Returns a new tuple containing component-wise absolute values of the given tuple
    /// </summary>
    /// <param name="tuple"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntTuple3D Abs(this IntTuple3D tuple)
    {
        return new IntTuple3D(
            Math.Abs(tuple.ItemX),
            Math.Abs(tuple.ItemY),
            Math.Abs(tuple.ItemZ)
        );
    }



    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntTuple2D ToTuple2DInt(this ILinFloat64Vector2D tuple)
    {
        return new IntTuple2D((int)tuple.X, (int)tuple.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntTuple2D ToTuple2DInt(this IntTuple2D tuple)
    {
        return new IntTuple2D(tuple.X, tuple.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntTuple2D ToTuple2DInt(this ILinFloat64Vector3D tuple)
    {
        return new IntTuple2D((int)tuple.X, (int)tuple.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntTuple2D ToTuple2DInt(this IntTuple3D tuple)
    {
        return new IntTuple2D(tuple.ItemX, tuple.ItemY);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntTuple2D ToTuple2DInt(this ILinFloat64Vector4D tuple)
    {
        return new IntTuple2D((int)tuple.X, (int)tuple.Y);
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static DistinctTuplesList2D ToDistinctTuplesList(this IEnumerable<IFloat64Tuple2D> tuplesList)
    //{
    //    return new DistinctTuplesList2D(tuplesList);
    //}



    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntTuple3D ToTuple3DInt(this ILinFloat64Vector2D tuple)
    {
        return new IntTuple3D((int)tuple.X, (int)tuple.Y, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntTuple3D ToTuple3DInt(this IntTuple2D tuple)
    {
        return new IntTuple3D(tuple.X, tuple.Y, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntTuple3D ToTuple3DInt(this ILinFloat64Vector3D tuple)
    {
        return new IntTuple3D((int)tuple.X, (int)tuple.Y, (int)tuple.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntTuple3D ToTuple3DInt(this IntTuple3D tuple)
    {
        return new IntTuple3D(tuple.ItemX, tuple.ItemY, tuple.ItemZ);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntTuple3D ToTuple3DInt(this ILinFloat64Vector4D tuple)
    {
        return new IntTuple3D((int)tuple.X, (int)tuple.Y, (int)tuple.Z);
    }


    /// <summary>
    /// Returns a permuted version of the components of this tuple
    /// </summary>
    /// <param name="tuple"></param>
    /// <param name="xIndex"></param>
    /// <param name="yIndex"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntTuple2D Permute(this IntTuple2D tuple, int xIndex, int yIndex)
    {
        return new IntTuple2D(tuple[xIndex], tuple[yIndex]);
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
    public static IntTuple3D Permute(this IntTuple3D tuple, int xIndex, int yIndex, int zIndex)
    {
        return new IntTuple3D(tuple[xIndex], tuple[yIndex], tuple[zIndex]);
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
    public static IntTuple2D SafePermute(this IntTuple2D tuple, int xIndex, int yIndex)
    {
        return new IntTuple2D(tuple[xIndex.Mod(2)], tuple[yIndex.Mod(2)]);
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
    public static IntTuple3D SafePermute(this IntTuple3D tuple, int xIndex, int yIndex, int zIndex)
    {
        return new IntTuple3D(tuple[xIndex.Mod(3)], tuple[yIndex.Mod(3)], tuple[zIndex.Mod(3)]);
    }




}