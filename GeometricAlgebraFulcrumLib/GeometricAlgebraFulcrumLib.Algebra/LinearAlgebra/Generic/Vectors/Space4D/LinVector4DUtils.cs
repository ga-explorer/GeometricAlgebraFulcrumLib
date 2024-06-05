using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space4D;

public static class LinVector4DUtils
{
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector4D<T> ClampTo<T>(this IQuad<Scalar<T>> vector, IQuad<Scalar<T>> maxTuple)
    //{
    //    return LinVector4D<T>.Create(
    //        vector.Item1.ClampTo(maxTuple.Item1),
    //        vector.Item2.ClampTo(maxTuple.Item2),
    //        vector.Item3.ClampTo(maxTuple.Item3),
    //        vector.Item4.ClampTo(maxTuple.Item4)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector4D<T> ClampTo<T>(this IQuad<Scalar<T>> vector, IQuad<Scalar<T>> minTuple, IQuad<Scalar<T>> maxTuple)
    //{
    //    return LinVector4D<T>.Create(
    //        vector.Item1.ClampTo(minTuple.Item1, maxTuple.Item1),
    //        vector.Item2.ClampTo(minTuple.Item2, maxTuple.Item2),
    //        vector.Item3.ClampTo(minTuple.Item3, maxTuple.Item3),
    //        vector.Item4.ClampTo(minTuple.Item4, maxTuple.Item4)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector4D<T> ClampToSymmetric<T>(this IQuad<Scalar<T>> vector, IQuad<Scalar<T>> maxTuple)
    //{
    //    return LinVector4D<T>.Create(
    //        vector.Item1.ClampToSymmetric(maxTuple.Item1),
    //        vector.Item2.ClampToSymmetric(maxTuple.Item2),
    //        vector.Item3.ClampToSymmetric(maxTuple.Item3),
    //        vector.Item4.ClampToSymmetric(maxTuple.Item4)
    //    );
    //}


    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IScalarProcessor<T> GetScalarProcessor<T>(this IQuad<Scalar<T>> vector)
    {
        return vector.Item1.ScalarProcessor;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> VectorAdd<T>(this IQuad<Scalar<T>> vector1, IQuad<Scalar<T>> vector2)
    {
        return LinVector4D<T>.Create(
            vector1.Item1 + vector2.Item1,
            vector1.Item2 + vector2.Item2,
            vector1.Item3 + vector2.Item3,
            vector1.Item4 + vector2.Item4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> VectorSubtract<T>(this IQuad<Scalar<T>> vector1, IQuad<Scalar<T>> vector2)
    {
        return LinVector4D<T>.Create(
            vector1.Item1 - vector2.Item1,
            vector1.Item2 - vector2.Item2,
            vector1.Item3 - vector2.Item3,
            vector1.Item4 - vector2.Item4
        );
    }
    
    
    ///// <summary>
    ///// Find the angle between this vector and another
    ///// </summary>
    ///// <param name="v1"></param>
    ///// <param name="v2"></param>
    ///// <returns></returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Scalar<T> GetAngleCos<T>(this IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    //{
    //    var t1 = v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3 + v1.Item4 * v2.Item4;
    //    var t2 = v1.Item1 * v1.Item1 + v1.Item2 * v1.Item2 + v1.Item3 * v1.Item3 + v1.Item4 * v1.Item4;
    //    var t3 = v2.Item1 * v2.Item1 + v2.Item2 * v2.Item2 + v2.Item3 * v2.Item3 + v2.Item4 * v2.Item4;

    //    return t1 / (t2 * t3).Sqrt();
    //}

    ///// <summary>
    ///// Find the angle between this vector and another
    ///// </summary>
    ///// <param name="v1"></param>
    ///// <param name="v2"></param>
    ///// <returns></returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Scalar<T> GetAngleCosWithUnit<T>(this IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    //{
    //    Debug.Assert(
    //        v2.IsNearUnit()
    //    );

    //    var t1 =
    //        v1.Item1 * v2.Item1 +
    //        v1.Item2 * v2.Item2 +
    //        v1.Item3 * v2.Item3 +
    //        v1.Item4 * v2.Item4;

    //    var t2 =
    //        v1.Item1 * v1.Item1 +
    //        v1.Item2 * v1.Item2 +
    //        v1.Item3 * v1.Item3 +
    //        v1.Item4 * v1.Item4;

    //    return t1 / t2.Sqrt();
    //}

    ///// <summary>
    ///// Find the angle between this vector and another
    ///// </summary>
    ///// <param name="v1"></param>
    ///// <param name="v2"></param>
    ///// <returns></returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinAngle<T> GetAngle<T>(this IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    //{
    //    return v1.GetScalarProcessor().CreateAngle(v1.GetAngleCos(v2).ArcCos());
    //}

    ///// <summary>
    ///// Find the angle between this vector and another
    ///// </summary>
    ///// <param name="v1"></param>
    ///// <param name="v2"></param>
    ///// <returns></returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Scalar<T> GetUnitVectorsAngleCos<T>(this IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    //{
    //    Debug.Assert(
    //        v1.IsNearUnitVector() &&
    //        v2.IsNearUnitVector()
    //    );

    //    return v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3 + v1.Item4 * v2.Item4;
    //}

    ///// <summary>
    ///// Find the angle between this vector and another
    ///// </summary>
    ///// <param name="v1"></param>
    ///// <param name="v2"></param>
    ///// <returns></returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinAngle<T> GetUnitVectorsAngle<T>(this IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    //{
    //    return v1.ScalarProcessor.CreateAngle(
    //        v1.GetUnitVectorsAngleCos(v2).ArcCos()
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> ToVector4D<T>(this LinUnitBasisVector2D axis, IScalarProcessor<T> scalarProcessor)
    {
        return axis switch
        {
            LinUnitBasisVector2D.PositiveX => LinVector4D<T>.E1(scalarProcessor),
            LinUnitBasisVector2D.NegativeX => LinVector4D<T>.NegativeE1(scalarProcessor),
            LinUnitBasisVector2D.PositiveY => LinVector4D<T>.E2(scalarProcessor),
            _ => LinVector4D<T>.NegativeE2(scalarProcessor)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> ToVector4D<T>(this LinUnitBasisVector3D axis, IScalarProcessor<T> scalarProcessor)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => LinVector4D<T>.E1(scalarProcessor),
            LinUnitBasisVector3D.NegativeX => LinVector4D<T>.NegativeE1(scalarProcessor),
            LinUnitBasisVector3D.PositiveY => LinVector4D<T>.E2(scalarProcessor),
            LinUnitBasisVector3D.NegativeY => LinVector4D<T>.NegativeE2(scalarProcessor),
            LinUnitBasisVector3D.PositiveZ => LinVector4D<T>.E3(scalarProcessor),
            _ => LinVector4D<T>.NegativeE3(scalarProcessor)
        };
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsXAxis(this LinUnitBasisVector4D axis)
    //{
    //    return axis is LinUnitBasisVector4D.PositiveX or LinUnitBasisVector4D.NegativeX;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsYAxis(this LinUnitBasisVector4D axis)
    //{
    //    return axis is LinUnitBasisVector4D.PositiveY or LinUnitBasisVector4D.NegativeY;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsZAxis(this LinUnitBasisVector4D axis)
    //{
    //    return axis is LinUnitBasisVector4D.PositiveZ or LinUnitBasisVector4D.NegativeZ;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsWAxis<T>(this LinUnitBasisVector4D axis)
    //{
    //    return axis is LinUnitBasisVector4D.PositiveW or LinUnitBasisVector4D.NegativeW;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsNegative<T>(this LinUnitBasisVector4D axis)
    //{
    //    return axis is
    //        LinUnitBasisVector4D.NegativeX or
    //        LinUnitBasisVector4D.NegativeY or
    //        LinUnitBasisVector4D.NegativeZ or
    //        LinUnitBasisVector4D.NegativeW;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsOppositeTo<T>(this LinUnitBasisVector4D axis1, LinUnitBasisVector4D axis2)
    //{
    //    return axis1 switch
    //    {
    //        LinUnitBasisVector4D.PositiveX => axis2 == LinUnitBasisVector4D.NegativeX,
    //        LinUnitBasisVector4D.PositiveY => axis2 == LinUnitBasisVector4D.NegativeY,
    //        LinUnitBasisVector4D.PositiveZ => axis2 == LinUnitBasisVector4D.NegativeZ,
    //        LinUnitBasisVector4D.PositiveW => axis2 == LinUnitBasisVector4D.NegativeW,
    //        LinUnitBasisVector4D.NegativeX => axis2 == LinUnitBasisVector4D.PositiveX,
    //        LinUnitBasisVector4D.NegativeY => axis2 == LinUnitBasisVector4D.PositiveY,
    //        LinUnitBasisVector4D.NegativeZ => axis2 == LinUnitBasisVector4D.PositiveZ,
    //        _ => axis2 == LinUnitBasisVector4D.PositiveW
    //    };
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnitBasisVector4D SelectNearestAxis<T>(this IQuad<Scalar<T>> unitVector)
    {
        return unitVector.GetMaxAbsComponentIndex() switch
        {
            0 => unitVector.Item1.IsPositive()
                ? LinUnitBasisVector4D.PositiveX
                : LinUnitBasisVector4D.NegativeX,

            1 => unitVector.Item2.IsPositive()
                ? LinUnitBasisVector4D.PositiveY
                : LinUnitBasisVector4D.NegativeY,

            2 => unitVector.Item3.IsPositive()
                ? LinUnitBasisVector4D.PositiveZ
                : LinUnitBasisVector4D.NegativeZ,

            _ => unitVector.Item3.IsPositive()
                ? LinUnitBasisVector4D.PositiveZ
                : LinUnitBasisVector4D.NegativeW
        };
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static int GetIndex(this LinUnitBasisVector4D axis)
    //{
    //    return axis switch
    //    {
    //        LinUnitBasisVector4D.PositiveX => 0,
    //        LinUnitBasisVector4D.NegativeX => 0,
    //        LinUnitBasisVector4D.PositiveY => 1,
    //        LinUnitBasisVector4D.NegativeY => 1,
    //        LinUnitBasisVector4D.PositiveZ => 2,
    //        LinUnitBasisVector4D.NegativeZ => 2,
    //        LinUnitBasisVector4D.PositiveW => 3,
    //        LinUnitBasisVector4D.NegativeW => 3,
    //        _ => throw new InvalidOperationException()
    //    };
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static IntegerSign GetSign(this LinUnitBasisVector4D axis)
    //{
    //    return axis switch
    //    {
    //        LinUnitBasisVector4D.PositiveX => IntegerSign.Positive,
    //        LinUnitBasisVector4D.NegativeX => IntegerSign.Negative,
    //        LinUnitBasisVector4D.PositiveY => IntegerSign.Positive,
    //        LinUnitBasisVector4D.NegativeY => IntegerSign.Negative,
    //        LinUnitBasisVector4D.PositiveZ => IntegerSign.Positive,
    //        LinUnitBasisVector4D.NegativeZ => IntegerSign.Negative,
    //        LinUnitBasisVector4D.PositiveW => IntegerSign.Positive,
    //        LinUnitBasisVector4D.NegativeW => IntegerSign.Negative,
    //        _ => throw new InvalidOperationException()
    //    };
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinUnitBasisVector4D ToAxis4D(this int axisIndex, bool isNegative = false)
    //{
    //    if (isNegative)
    //        return axisIndex switch
    //        {
    //            0 => LinUnitBasisVector4D.NegativeX,
    //            1 => LinUnitBasisVector4D.NegativeY,
    //            2 => LinUnitBasisVector4D.NegativeZ,
    //            3 => LinUnitBasisVector4D.PositiveW,
    //            _ => throw new IndexOutOfRangeException()
    //        };

    //    return axisIndex switch
    //    {
    //        0 => LinUnitBasisVector4D.PositiveX,
    //        1 => LinUnitBasisVector4D.PositiveY,
    //        2 => LinUnitBasisVector4D.PositiveZ,
    //        3 => LinUnitBasisVector4D.PositiveW,
    //        _ => throw new IndexOutOfRangeException()
    //    };
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> ToTuple4D<T>(this LinUnitBasisVector4D axis, IScalarProcessor<T> scalarProcessor)
    {
        return axis switch
        {
            LinUnitBasisVector4D.PositiveX => LinVector4D<T>.E1(scalarProcessor),
            LinUnitBasisVector4D.NegativeX => LinVector4D<T>.NegativeE1(scalarProcessor),
            LinUnitBasisVector4D.PositiveY => LinVector4D<T>.E2(scalarProcessor),
            LinUnitBasisVector4D.NegativeY => LinVector4D<T>.NegativeE2(scalarProcessor),
            LinUnitBasisVector4D.PositiveZ => LinVector4D<T>.E3(scalarProcessor),
            LinUnitBasisVector4D.NegativeZ => LinVector4D<T>.NegativeE3(scalarProcessor),
            LinUnitBasisVector4D.PositiveW => LinVector4D<T>.E4(scalarProcessor),
            _ => LinVector4D<T>.NegativeE4(scalarProcessor)
        };
    }


    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector is near 
    /// zero it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> ToNegativeVector<T>(this IQuad<Scalar<T>> vector)
    {
        return LinVector4D<T>.Create(-vector.Item1, -vector.Item2, -vector.Item3, -vector.Item4);
    }

    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector is near 
    /// zero it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> ToNegativeUnitVector<T>(this IQuad<Scalar<T>> vector)
    {
        var s = vector.ENorm();
        if (s.IsZero())
            return vector.ToTuple4D();

        s = 1.0d / s;
        return LinVector4D<T>.Create(vector.Item1 * s, vector.Item2 * s, vector.Item3 * s, vector.Item4 * s);
    }

    /// <summary>
    /// The Euclidean distance between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetDistanceToPoint<T>(this IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    {
        var vX = v2.Item1 - v1.Item1;
        var vY = v2.Item2 - v1.Item2;
        var vZ = v2.Item3 - v1.Item3;
        var vW = v2.Item4 - v1.Item4;

        return (vX * vX + vY * vY + vZ * vZ + vW * vW).Sqrt();
    }

    /// <summary>
    /// The Euclidean distance between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetDistanceSquaredToPoint<T>(this IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    {
        var vX = v2.Item1 - v1.Item1;
        var vY = v2.Item2 - v1.Item2;
        var vZ = v2.Item3 - v1.Item3;
        var vW = v2.Item4 - v1.Item4;

        return vX * vX + vY * vY + vZ * vZ + vW * vW;
    }

    /// <summary>
    /// Returns a unit vector from the given one. If the length of the given vector is near zero
    /// it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="scalarProcessor"></param>
    /// <param name="zeroAsSymmetric"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> ToUnitVector<T>(this IQuad<Scalar<T>> vector, IScalarProcessor<T> scalarProcessor, bool zeroAsSymmetric = true)
    {
        var s = vector.ENorm();

        if (s.IsZero())
            return zeroAsSymmetric
                ? LinVector4D<T>.UnitSymmetric(scalarProcessor)
                : LinVector4D<T>.Zero(scalarProcessor);

        s = 1.0d / s;
        return LinVector4D<T>.Create(vector.Item1 * s, vector.Item2 * s, vector.Item3 * s, vector.Item4 * s);
    }


    /// <summary>
    /// The Euclidean squared length of this vector when it represents a vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> ENorm<T>(this IQuad<Scalar<T>> vector)
    {
        return (vector.Item1 * vector.Item1 + vector.Item2 * vector.Item2 + vector.Item3 * vector.Item3 + vector.Item4 * vector.Item4).Sqrt();
    }

    /// <summary>
    /// The Euclidean squared length of this vector when it represents a vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> ENormSquared<T>(this IQuad<Scalar<T>> vector)
    {
        return vector.Item1 * vector.Item1 +
               vector.Item2 * vector.Item2 +
               vector.Item3 * vector.Item3 +
               vector.Item4 * vector.Item4;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> ENorm<T>(Scalar<T> vectorX, Scalar<T> vectorY, Scalar<T> vectorZ, Scalar<T> vectorW)
    {
        return (vectorX * vectorX + vectorY * vectorY + vectorZ * vectorZ + vectorW * vectorW).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> ENormSquared<T>(Scalar<T> vectorX, Scalar<T> vectorY, Scalar<T> vectorZ, Scalar<T> vectorW)
    {
        return vectorX * vectorX +
               vectorY * vectorY +
               vectorZ * vectorZ +
               vectorW * vectorW;
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearUnitVector<T>(this IQuad<Scalar<T>> vector)
    {
        return vector.ENormSquared().IsNearOne();
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> ESp<T>(this IQuad<Scalar<T>> v1, LinUnitBasisVector4D v2)
    {
        return v2 switch
        {
            LinUnitBasisVector4D.PositiveX => v1.Item1,
            LinUnitBasisVector4D.PositiveY => v1.Item2,
            LinUnitBasisVector4D.PositiveZ => v1.Item3,
            LinUnitBasisVector4D.PositiveW => v1.Item4,
            LinUnitBasisVector4D.NegativeX => -v1.Item1,
            LinUnitBasisVector4D.NegativeY => -v1.Item2,
            LinUnitBasisVector4D.NegativeZ => -v1.Item3,
            _ => -v1.Item4,
        };
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near zero
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsAlmostZeroVector<T>(this IQuad<Scalar<T>> vector)
    {
        return vector.ENormSquared().IsNearZero();
    }


    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUnitVector<T>(this IQuad<Scalar<T>> vector)
    {
        return vector.ENormSquared().IsNearOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> Add<T>(this IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    {
        return LinVector4D<T>.Create(
            v1.Item1 + v2.Item1,
            v1.Item2 + v2.Item2,
            v1.Item3 + v2.Item3,
            v1.Item4 + v2.Item4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> Subtract<T>(this IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    {
        return LinVector4D<T>.Create(
            v1.Item1 - v2.Item1,
            v1.Item2 - v2.Item2,
            v1.Item3 - v2.Item3,
            v1.Item4 - v2.Item4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> Times<T>(this IQuad<Scalar<T>> v1, Scalar<T> v2)
    {
        return LinVector4D<T>.Create(
            v1.Item1 * v2,
            v1.Item2 * v2,
            v1.Item3 * v2,
            v1.Item4 * v2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> Times<T>(this Scalar<T> v1, IQuad<Scalar<T>> v2)
    {
        return LinVector4D<T>.Create(
            v1 * v2.Item1,
            v1 * v2.Item2,
            v1 * v2.Item3,
            v1 * v2.Item4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> Divide<T>(this IQuad<Scalar<T>> v1, Scalar<T> v2)
    {
        v2 = 1d / v2;

        return LinVector4D<T>.Create(
            v1.Item1 * v2,
            v1.Item2 * v2,
            v1.Item3 * v2,
            v1.Item4 * v2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> GetFloat64Tuple4D<T>(this Random random, IScalarProcessor<T> scalarProcessor)
    {
        return LinVector4D<T>.Create(
            scalarProcessor.ScalarFromRandom(random, 0, 1),
            scalarProcessor.ScalarFromRandom(random, 0, 1),
            scalarProcessor.ScalarFromRandom(random, 0, 1),
            scalarProcessor.ScalarFromRandom(random, 0, 1)
        );
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="v3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> ESp<T>(this IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2, IQuad<Scalar<T>> v3)
    {
        return new Pair<Scalar<T>>(
            v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3 + v1.Item4 * v2.Item4,
            v1.Item1 * v3.Item1 + v1.Item2 * v3.Item2 + v1.Item3 * v3.Item3 + v1.Item4 * v3.Item4
        );
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> ESp<T>(this IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    {
        return v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3 + v1.Item4 * v2.Item4;
    }

    /// <summary>
    /// The absolute value of the Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> ESpAbs<T>(this IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    {
        return (v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3 + v1.Item4 * v2.Item4).Abs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> RejectOnUnitVector<T>(this IQuad<Scalar<T>> v, IQuad<Scalar<T>> u)
    {
        var s = v.Item1 * u.Item1 + v.Item2 * u.Item2 + v.Item3 * u.Item3 + v.Item4 * u.Item4;

        return LinVector4D<T>.Create(
            v.Item1 - u.Item1 * s,
            v.Item2 - u.Item2 * s,
            v.Item3 - u.Item3 * s,
            v.Item4 - u.Item4 * s
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> ProjectOnVector<T>(this IQuad<Scalar<T>> v, IQuad<Scalar<T>> u)
    {
        var s1 = v.Item1 * u.Item1 + v.Item2 * u.Item2 + v.Item3 * u.Item3 + v.Item4 * u.Item4;
        var s2 = u.Item1 * u.Item1 + u.Item2 * u.Item2 + u.Item3 * u.Item3 + u.Item4 * u.Item4;
        var s = s1 / s2;

        return LinVector4D<T>.Create(
            u.Item1 * s,
            u.Item2 * s,
            u.Item3 * s,
            u.Item4 * s
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> ProjectOnUnitVector<T>(this IQuad<Scalar<T>> v, IQuad<Scalar<T>> u)
    {
        var s = v.Item1 * u.Item1 + v.Item2 * u.Item2 + v.Item3 * u.Item3 + v.Item4 * u.Item4;

        return LinVector4D<T>.Create(
            u.Item1 * s,
            u.Item2 * s,
            u.Item3 * s,
            u.Item4 * s
        );
    }


    public static LinVector4D<T> ToTuple4D<T>(this IEnumerable<Scalar<T>> scalarList)
    {
        var scalarArray = new Scalar<T>[4];

        var i = 0;
        foreach (var scalar in scalarList)
            scalarArray[i++] = scalar;

        return LinVector4D<T>.Create(
            scalarArray[0],
            scalarArray[1],
            scalarArray[2],
            scalarArray[3]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZero<T>(this IQuad<Scalar<T>> vector)
    {
        return vector.ENorm().IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearUnit<T>(this IQuad<Scalar<T>> vector)
    {
        return vector.ENormSquared().IsNearOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthonormalWith<T>(this IQuad<Scalar<T>> vector1, IQuad<Scalar<T>> vector2)
    {
        return vector1.IsNearUnit() &&
               vector2.IsNearUnit() &&
               vector1.ESp(vector2).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthonormalWithUnit<T>(this IQuad<Scalar<T>> vector1, IQuad<Scalar<T>> vector2)
    {
        Debug.Assert(
            vector2.IsNearUnit()
        );

        return vector1.IsNearUnit() &&
               vector1.ESp(vector2).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelTo<T>(this IQuad<Scalar<T>> vector1, IQuad<Scalar<T>> vector2)
    {
        return vector1.GetAngleCos(vector2).Abs().IsNearOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelToUnit<T>(this IQuad<Scalar<T>> vector1, IQuad<Scalar<T>> vector2)
    {
        return vector1.GetAngleCosWithUnit(vector2).Abs().IsNearOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthogonalTo<T>(this IQuad<Scalar<T>> vector1, IQuad<Scalar<T>> vector2)
    {
        return vector1.ESp(vector2).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVectorBasis<T>(this IQuad<Scalar<T>> vector, int basisIndex)
    {
        return basisIndex switch
        {
            0 => vector.Item1.IsOne() && vector.Item2.IsZero() && vector.Item3.IsZero() && vector.Item4.IsZero(),
            1 => vector.Item1.IsZero() && vector.Item2.IsOne() && vector.Item3.IsZero() && vector.Item4.IsZero(),
            2 => vector.Item1.IsZero() && vector.Item2.IsZero() && vector.Item3.IsOne() && vector.Item4.IsZero(),
            3 => vector.Item1.IsZero() && vector.Item2.IsZero() && vector.Item3.IsZero() && vector.Item4.IsOne(),
            _ => false
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearVectorBasis<T>(this IQuad<Scalar<T>> vector, int basisIndex)
    {
        var scalarProcessor = vector.GetScalarProcessor();

        var vector2 = basisIndex switch
        {
            0 => LinVector4D<T>.E1(scalarProcessor),
            1 => LinVector4D<T>.E2(scalarProcessor),
            2 => LinVector4D<T>.E3(scalarProcessor),
            3 => LinVector4D<T>.E4(scalarProcessor),
            _ => throw new InvalidOperationException()
        };

        return vector.VectorSubtract(vector2).IsNearZero();
    }

    public static Tuple<bool, Scalar<T>, LinUnitBasisVector4D> TryVectorToAxis<T>(this LinVector4D<T> vector)
    {
        var scalarProcessor = vector.ScalarProcessor;

        // Find if the given scaling vector is parallel to a basis vector
        var basisIndex = -1;
        for (var i = 0; i < 4; i++)
        {
            if (vector.GetItem(i).IsZero()) continue;

            if (basisIndex >= 0)
            {
                basisIndex = -2;
                break;
            }

            basisIndex = i;
        }

        if (basisIndex < 0)
            return new Tuple<bool, Scalar<T>, LinUnitBasisVector4D>(
                false,
                scalarProcessor.Zero,
                LinUnitBasisVector4D.PositiveX
            );

        var scalar = vector.GetItem(basisIndex);

        return new Tuple<bool, Scalar<T>, LinUnitBasisVector4D>(
            true,
            scalar.Abs(),
            basisIndex.ToAxis4D(scalar < 0)
        );
    }

    public static Tuple<bool, Scalar<T>, LinUnitBasisVector4D> TryVectorToAxis<T>(this IQuad<Scalar<T>> vector)
    {
        var scalarProcessor = vector.GetScalarProcessor();

        // Find if the given scaling vector is parallel to a basis vector
        var basisIndex = -1;
        for (var i = 0; i < 4; i++)
        {
            if (vector.GetItem(i).IsZero()) continue;

            if (basisIndex >= 0)
            {
                basisIndex = -2;
                break;
            }

            basisIndex = i;
        }

        if (basisIndex < 0)
            return new Tuple<bool, Scalar<T>, LinUnitBasisVector4D>(
                false,
                scalarProcessor.Zero,
                LinUnitBasisVector4D.PositiveX
            );

        var scalar = vector.GetItem(basisIndex);

        return new Tuple<bool, Scalar<T>, LinUnitBasisVector4D>(
            true,
            scalar.Abs(),
            basisIndex.ToAxis4D(scalar < 0)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> Lerp<T>(this Scalar<T> t, IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    {
        var s = 1.0d - t;

        return LinVector4D<T>.Create(s * v1.Item1 + t * v2.Item1,
            s * v1.Item2 + t * v2.Item2,
            s * v1.Item3 + t * v2.Item3,
            s * v1.Item4 + t * v2.Item4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinVector4D<T>> Lerp<T>(this IEnumerable<Scalar<T>> tList, IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    {
        return tList.Select(t => t.Lerp(v1, v2));
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsFinite<T>(this IQuad<Scalar<T>> vector)
    //{
    //    return !(
    //        Scalar<T>.IsNaN(vector.Item1) || Scalar<T>.IsInfinity(vector.Item1) ||
    //        Scalar<T>.IsNaN(vector.Item2) || Scalar<T>.IsInfinity(vector.Item2) ||
    //        Scalar<T>.IsNaN(vector.Item3) || Scalar<T>.IsInfinity(vector.Item3) ||
    //        Scalar<T>.IsNaN(vector.Item4) || Scalar<T>.IsInfinity(vector.Item4)
    //    );
    //}

    /// <summary>
    /// The index of the largest absolute component in this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMaxAbsComponentIndex<T>(this IQuad<Scalar<T>> vector)
    {
        var absX = vector.Item1.Abs();
        var absY = vector.Item2.Abs();
        var absZ = vector.Item3.Abs();
        var absW = vector.Item4.Abs();

        var index = 0;
        var maxValue = absX;

        if (maxValue < absY)
        {
            index = 1;
            maxValue = absY;
        }

        if (maxValue < absZ)
        {
            index = 2;
            maxValue = absZ;
        }

        if (maxValue < absW)
        {
            index = 3;
            maxValue = absW;
        }

        return index;
    }

    /// <summary>
    /// The index of the largest absolute component in this vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMinAbsComponentIndex<T>(this IQuad<Scalar<T>> vector)
    {
        var absX = vector.Item1.Abs();
        var absY = vector.Item2.Abs();
        var absZ = vector.Item3.Abs();
        var absW = vector.Item4.Abs();

        var index = 0;
        var minValue = absX;

        if (minValue > absY)
        {
            index = 1;
            minValue = absY;
        }

        if (minValue > absZ)
        {
            index = 2;
            minValue = absZ;
        }

        if (minValue > absW)
        {
            index = 3;
            minValue = absW;
        }

        return index;
    }

    /// <summary>
    /// Returns a new vector containing component-wise minimum values of the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> Min<T>(this IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    {
        return LinVector4D<T>.Create(
            v1.Item1 < v2.Item1 ? v1.Item1 : v2.Item1,
            v1.Item2 < v2.Item2 ? v1.Item2 : v2.Item2,
            v1.Item3 < v2.Item3 ? v1.Item3 : v2.Item3,
            v1.Item4 < v2.Item4 ? v1.Item4 : v2.Item4
        );
    }

    /// <summary>
    /// Returns a new vector containing component-wise maximum values of the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> Max<T>(this IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    {
        return LinVector4D<T>.Create(
            v1.Item1 > v2.Item1 ? v1.Item1 : v2.Item1,
            v1.Item2 > v2.Item2 ? v1.Item2 : v2.Item2,
            v1.Item3 > v2.Item3 ? v1.Item3 : v2.Item3,
            v1.Item4 > v2.Item4 ? v1.Item4 : v2.Item4
        );
    }

    ///// <summary>
    ///// Returns a new vector containing component-wise ceiling values of the given vector
    ///// </summary>
    ///// <param name="vector"></param>
    ///// <returns></returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector4D<T> Ceiling<T>(this IQuad<Scalar<T>> vector)
    //{
    //    return LinVector4D<T>.Create(
    //        Math.Ceiling(vector.Item1),
    //        Math.Ceiling(vector.Item2),
    //        Math.Ceiling(vector.Item3),
    //        Math.Ceiling(vector.Item4)
    //    );
    //}

    ///// <summary>
    ///// Returns a new vector containing component-wise floor values of the given vector
    ///// </summary>
    ///// <param name="vector"></param>
    ///// <returns></returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector4D<T> Floor<T>(this IQuad<Scalar<T>> vector)
    //{
    //    return LinVector4D<T>.Create(
    //        Math.Floor(vector.Item1),
    //        Math.Floor(vector.Item2),
    //        Math.Floor(vector.Item3),
    //        Math.Floor(vector.Item4)
    //    );
    //}

    /// <summary>
    /// Returns a new vector containing component-wise absolute values of the given vector
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> Abs<T>(this IQuad<Scalar<T>> vector)
    {
        return LinVector4D<T>.Create(
            vector.Item1.Abs(),
            vector.Item2.Abs(),
            vector.Item3.Abs(),
            vector.Item4.Abs()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> ToTuple4D<T>(this IPair<Scalar<T>> vector)
    {
        return LinVector4D<T>.Create(
            vector.Item1,
            vector.Item2,
            vector.GetScalarProcessor().Zero,
            vector.GetScalarProcessor().Zero
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector4D<T> ToTuple4D<T>(this IntTuple2D vector)
    //{
    //    return LinVector4D<T>.Create(vector.Item1, vector.Item2, 0.0d, 0.0d);
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> ToTuple4D<T>(this ITriplet<Scalar<T>> vector)
    {
        return LinVector4D<T>.Create(
            vector.Item1,
            vector.Item2,
            vector.Item3,
            vector.GetScalarProcessor().Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> ToTuple4D<T>(this IQuad<Scalar<T>> vector)
    {
        return vector is LinVector4D<T> t
            ? t
            : LinVector4D<T>.Create(vector.Item1, vector.Item2, vector.Item3, vector.Item4);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector4D<T> ToTuple4D<T>(this IntTuple3D vector)
    //{
    //    return LinVector4D<T>.Create(vector.ItemX, vector.ItemY, vector.ItemZ, 0.0d);
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> GetRealTuple<T>(this ILinComplexVector4D<T> vector)
    {
        return LinVector4D<T>.Create(
            vector.Item1.Real,
            vector.Item2.Real,
            vector.Item3.Real,
            vector.Item4.Real
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> GetImaginaryTuple<T>(this ILinComplexVector4D<T> vector)
    {
        return LinVector4D<T>.Create(
            vector.Item1.Imaginary,
            vector.Item2.Imaginary,
            vector.Item3.Imaginary,
            vector.Item4.Imaginary
        );
    }

    /// <summary>
    /// Returns a permuted version of the components of this vector
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="xIndex"></param>
    /// <param name="yIndex"></param>
    /// <param name="zIndex"></param>
    /// <param name="wIndex"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> Permute<T>(this LinVector4D<T> vector, int xIndex, int yIndex, int zIndex, int wIndex)
    {
        return LinVector4D<T>.Create(
            vector[xIndex],
            vector[yIndex],
            vector[zIndex],
            vector[wIndex]
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
    /// <param name="wIndex"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> SafePermute<T>(this LinVector4D<T> vector, int xIndex, int yIndex, int zIndex, int wIndex)
    {
        return LinVector4D<T>.Create(
            vector[xIndex.Mod(4)],
            vector[yIndex.Mod(4)],
            vector[zIndex.Mod(4)],
            vector[wIndex.Mod(4)]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetComponent<T>(this IQuad<Scalar<T>> vector, int index)
    {
        return index switch
        {
            0 => vector.Item1,
            1 => vector.Item2,
            2 => vector.Item3,
            3 => vector.Item4,
            _ => vector.GetScalarProcessor().Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Scalar<T>> GetComponents<T>(this IQuad<Scalar<T>> vector)
    {
        yield return vector.Item1;
        yield return vector.Item2;
        yield return vector.Item3;
        yield return vector.Item4;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Scalar<T>> GetComponents<T>(this IEnumerable<IQuad<Scalar<T>>> vectorsList)
    {
        return vectorsList.SelectMany(t => t.GetComponents());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> ComponentsProduct<T>(this IQuad<Scalar<T>> vector1, IQuad<Scalar<T>> vector2)
    {
        return LinVector4D<T>.Create(
            vector1.Item1 * vector2.Item1,
            vector1.Item2 * vector2.Item2,
            vector1.Item3 * vector2.Item3,
            vector1.Item4 * vector2.Item4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> ComponentsProduct<T>(this IQuad<Scalar<T>> vector1, IQuad<Scalar<T>> vector2, IQuad<Scalar<T>> vector3)
    {
        return LinVector4D<T>.Create(
            vector1.Item1 * vector2.Item1 * vector3.Item1,
            vector1.Item2 * vector2.Item2 * vector3.Item2,
            vector1.Item3 * vector2.Item3 * vector3.Item3,
            vector1.Item4 * vector2.Item4 * vector3.Item4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> GetTupleXPair<T>(this IReadOnlyList<IQuad<Scalar<T>>> itemArray, int index)
    {
        return new Pair<Scalar<T>>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<Scalar<T>> GetTupleXTriplet<T>(this IReadOnlyList<IQuad<Scalar<T>>> itemArray, int index)
    {
        return new Triplet<Scalar<T>>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<Scalar<T>> GetTupleXQuad<T>(this IReadOnlyList<IQuad<Scalar<T>>> itemArray, int index)
    {
        return new Quad<Scalar<T>>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1,
            itemArray[index + 3].Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<Scalar<T>> GetTupleXQuint<T>(this IReadOnlyList<IQuad<Scalar<T>>> itemArray, int index)
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
    public static Hexad<Scalar<T>> GetTupleXHexad<T>(this IReadOnlyList<IQuad<Scalar<T>>> itemArray, int index)
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
    public static Pair<Scalar<T>> GetTupleYPair<T>(this IReadOnlyList<IQuad<Scalar<T>>> itemArray, int index)
    {
        return new Pair<Scalar<T>>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<Scalar<T>> GetTupleYTriplet<T>(this IReadOnlyList<IQuad<Scalar<T>>> itemArray, int index)
    {
        return new Triplet<Scalar<T>>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<Scalar<T>> GetTupleYQuad<T>(this IReadOnlyList<IQuad<Scalar<T>>> itemArray, int index)
    {
        return new Quad<Scalar<T>>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2,
            itemArray[index + 3].Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<Scalar<T>> GetTupleYQuint<T>(this IReadOnlyList<IQuad<Scalar<T>>> itemArray, int index)
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
    public static Hexad<Scalar<T>> GetTupleYHexad<T>(this IReadOnlyList<IQuad<Scalar<T>>> itemArray, int index)
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
    public static Pair<Scalar<T>> GetTupleZPair<T>(this IReadOnlyList<IQuad<Scalar<T>>> itemArray, int index)
    {
        return new Pair<Scalar<T>>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<Scalar<T>> GetTupleZTriplet<T>(this IReadOnlyList<IQuad<Scalar<T>>> itemArray, int index)
    {
        return new Triplet<Scalar<T>>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3,
            itemArray[index + 2].Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<Scalar<T>> GetTupleZQuad<T>(this IReadOnlyList<IQuad<Scalar<T>>> itemArray, int index)
    {
        return new Quad<Scalar<T>>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3,
            itemArray[index + 2].Item3,
            itemArray[index + 3].Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<Scalar<T>> GetTupleZQuint<T>(this IReadOnlyList<IQuad<Scalar<T>>> itemArray, int index)
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
    public static Hexad<Scalar<T>> GetTupleZHexad<T>(this IReadOnlyList<IQuad<Scalar<T>>> itemArray, int index)
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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> GetTupleWPair<T>(this IReadOnlyList<IQuad<Scalar<T>>> itemArray, int index)
    {
        return new Pair<Scalar<T>>(
            itemArray[index].Item4,
            itemArray[index + 1].Item4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<Scalar<T>> GetTupleWTriplet<T>(this IReadOnlyList<IQuad<Scalar<T>>> itemArray, int index)
    {
        return new Triplet<Scalar<T>>(
            itemArray[index].Item4,
            itemArray[index + 1].Item4,
            itemArray[index + 2].Item4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<Scalar<T>> GetTupleWQuad<T>(this IReadOnlyList<IQuad<Scalar<T>>> itemArray, int index)
    {
        return new Quad<Scalar<T>>(
            itemArray[index].Item4,
            itemArray[index + 1].Item4,
            itemArray[index + 2].Item4,
            itemArray[index + 3].Item4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<Scalar<T>> GetTupleWQuint<T>(this IReadOnlyList<IQuad<Scalar<T>>> itemArray, int index)
    {
        return new Quint<Scalar<T>>(
            itemArray[index].Item4,
            itemArray[index + 1].Item4,
            itemArray[index + 2].Item4,
            itemArray[index + 3].Item4,
            itemArray[index + 4].Item4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Hexad<Scalar<T>> GetTupleWHexad<T>(this IReadOnlyList<IQuad<Scalar<T>>> itemArray, int index)
    {
        return new Hexad<Scalar<T>>(
            itemArray[index].Item4,
            itemArray[index + 1].Item4,
            itemArray[index + 2].Item4,
            itemArray[index + 3].Item4,
            itemArray[index + 4].Item4,
            itemArray[index + 5].Item4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> RotateToUnitVector<T>(this IQuad<Scalar<T>> vector1, IQuad<Scalar<T>> unitVector, LinAngle<T> angle)
    {
        Debug.Assert(
            vector1.IsNearUnit() &&
            unitVector.IsNearUnit()
        );

        // Create a unit normal to u in the u-v rotational plane
        var v1 = unitVector.Subtract(vector1.Times(unitVector.ESp(vector1)));
        var v1Length = v1.ENorm();

        Debug.Assert(
            !v1Length.IsNearZero()
        );

        // Compute a rotated version of v in the u-v rotational plane by the given angle
        return vector1
            .Times(angle.Cos())
            .Add(v1.Times(angle.Sin() / v1Length));
    }
}