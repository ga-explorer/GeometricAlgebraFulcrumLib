using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;

public static class LinVector3DUtils
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IScalarProcessor<T> GetScalarProcessor<T>(this ITriplet<Scalar<T>> vector)
    {
        return vector.Item1.ScalarProcessor;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ProjectOnXAxis<T>(this ITriplet<Scalar<T>> vector)
    {
        var scalarProcessor = vector.GetScalarProcessor();

        return LinVector3D<T>.Create(
            vector.Item1,
            scalarProcessor.Zero,
            scalarProcessor.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ProjectOnYAxis<T>(this ITriplet<Scalar<T>> vector)
    {
        var scalarProcessor = vector.GetScalarProcessor();

        return LinVector3D<T>.Create(
            scalarProcessor.Zero,
            vector.Item2,
            scalarProcessor.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ProjectOnZAxis<T>(this ITriplet<Scalar<T>> vector)
    {
        var scalarProcessor = vector.GetScalarProcessor();

        return LinVector3D<T>.Create(
            scalarProcessor.Zero,
            scalarProcessor.Zero,
            vector.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ProjectOnXyPlane<T>(this ITriplet<Scalar<T>> vector)
    {
        var scalarProcessor = vector.GetScalarProcessor();

        return LinVector3D<T>.Create(
            vector.Item1,
            vector.Item2,
            scalarProcessor.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ProjectOnXzPlane<T>(this ITriplet<Scalar<T>> vector)
    {
        var scalarProcessor = vector.GetScalarProcessor();

        return LinVector3D<T>.Create(
            vector.Item1,
            scalarProcessor.Zero,
            vector.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ProjectOnYzPlane<T>(this ITriplet<Scalar<T>> vector)
    {
        var scalarProcessor = vector.GetScalarProcessor();

        return LinVector3D<T>.Create(
            scalarProcessor.Zero,
            vector.Item2,
            vector.Item3
        );
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector3D<T> ClampTo<T>(this ITriplet<Scalar<T>> vector, ITriplet<Scalar<T>> maxTuple)
    //{
    //    return LinVector3D<T>.Create(
    //        vector.Item1.ClampTo(maxTuple.Item1),
    //        vector.Item2.ClampTo(maxTuple.Item2),
    //        vector.Item3.ClampTo(maxTuple.Item3)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector3D<T> ClampTo<T>(this ITriplet<Scalar<T>> vector, ITriplet<Scalar<T>> minTuple, ITriplet<Scalar<T>> maxTuple)
    //{
    //    return LinVector3D<T>.Create(
    //        vector.Item1.ClampTo(minTuple.Item1, maxTuple.Item1),
    //        vector.Item2.ClampTo(minTuple.Item2, maxTuple.Item2),
    //        vector.Item3.ClampTo(minTuple.Item3, maxTuple.Item3)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector3D<T> ClampToSymmetric<T>(this ITriplet<Scalar<T>> vector, ITriplet<Scalar<T>> maxTuple)
    //{
    //    return LinVector3D<T>.Create(
    //        vector.Item1.ClampToSymmetric(maxTuple.Item1),
    //        vector.Item2.ClampToSymmetric(maxTuple.Item2),
    //        vector.Item3.ClampToSymmetric(maxTuple.Item3)
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> DirectionToUnitNormal3D<T>(this LinUnitBasisVector3D axis, IScalarProcessor<T> scalarProcessor)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => LinBivector3D<T>.E32(scalarProcessor),
            LinUnitBasisVector3D.NegativeX => LinBivector3D<T>.E23(scalarProcessor),
            LinUnitBasisVector3D.PositiveY => LinBivector3D<T>.E13(scalarProcessor),
            LinUnitBasisVector3D.NegativeY => LinBivector3D<T>.E31(scalarProcessor),
            LinUnitBasisVector3D.PositiveZ => LinBivector3D<T>.E21(scalarProcessor),
            LinUnitBasisVector3D.NegativeZ => LinBivector3D<T>.E12(scalarProcessor),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> DirectionToUnitNormal3D<T>(this LinUnitBasisVector3D axis, Scalar<T> scalingFactor)
    {
        var scalarProcessor = scalingFactor.ScalarProcessor;

        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => LinBivector3D<T>.E32(scalarProcessor) * scalingFactor,
            LinUnitBasisVector3D.NegativeX => LinBivector3D<T>.E23(scalarProcessor) * scalingFactor,
            LinUnitBasisVector3D.PositiveY => LinBivector3D<T>.E13(scalarProcessor) * scalingFactor,
            LinUnitBasisVector3D.NegativeY => LinBivector3D<T>.E31(scalarProcessor) * scalingFactor,
            LinUnitBasisVector3D.PositiveZ => LinBivector3D<T>.E21(scalarProcessor) * scalingFactor,
            LinUnitBasisVector3D.NegativeZ => LinBivector3D<T>.E12(scalarProcessor) * scalingFactor,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> NormalToUnitDirection3D<T>(this LinUnitBasisVector3D axis, IScalarProcessor<T> scalarProcessor)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => LinBivector3D<T>.E23(scalarProcessor),
            LinUnitBasisVector3D.NegativeX => LinBivector3D<T>.E32(scalarProcessor),
            LinUnitBasisVector3D.PositiveY => LinBivector3D<T>.E31(scalarProcessor),
            LinUnitBasisVector3D.NegativeY => LinBivector3D<T>.E13(scalarProcessor),
            LinUnitBasisVector3D.PositiveZ => LinBivector3D<T>.E12(scalarProcessor),
            LinUnitBasisVector3D.NegativeZ => LinBivector3D<T>.E21(scalarProcessor),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> NormalToUnitDirection3D<T>(this LinUnitBasisVector3D axis, Scalar<T> scalingFactor)
    {
        var scalarProcessor = scalingFactor.ScalarProcessor;

        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => LinBivector3D<T>.E23(scalarProcessor) * scalingFactor,
            LinUnitBasisVector3D.NegativeX => LinBivector3D<T>.E32(scalarProcessor) * scalingFactor,
            LinUnitBasisVector3D.PositiveY => LinBivector3D<T>.E31(scalarProcessor) * scalingFactor,
            LinUnitBasisVector3D.NegativeY => LinBivector3D<T>.E13(scalarProcessor) * scalingFactor,
            LinUnitBasisVector3D.PositiveZ => LinBivector3D<T>.E12(scalarProcessor) * scalingFactor,
            LinUnitBasisVector3D.NegativeZ => LinBivector3D<T>.E21(scalarProcessor) * scalingFactor,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinUnitBasisVector3D GetAxis3D<T>(this LinBasisVectorPair3D axisPair)
    //{
    //    return axisPair switch
    //    {
    //        LinBasisVectorPair3D.Yz => LinUnitBasisVector3D.PositiveX,
    //        LinBasisVectorPair3D.Zy => LinUnitBasisVector3D.NegativeX,
    //        LinBasisVectorPair3D.Zx => LinUnitBasisVector3D.PositiveY,
    //        LinBasisVectorPair3D.Xz => LinUnitBasisVector3D.NegativeY,
    //        LinBasisVectorPair3D.Xy => LinUnitBasisVector3D.PositiveZ,
    //        _ => LinUnitBasisVector3D.NegativeZ
    //    };
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinBasisVectorPair3D GetAxisPair3D<T>(this LinUnitBasisVector3D axis)
    //{
    //    return axis switch
    //    {
    //        LinUnitBasisVector3D.PositiveX => LinBasisVectorPair3D.Yz,
    //        LinUnitBasisVector3D.NegativeX => LinBasisVectorPair3D.Zy,
    //        LinUnitBasisVector3D.PositiveY => LinBasisVectorPair3D.Zx,
    //        LinUnitBasisVector3D.NegativeY => LinBasisVectorPair3D.Xz,
    //        LinUnitBasisVector3D.PositiveZ => LinBasisVectorPair3D.Xy,
    //        _ => LinBasisVectorPair3D.Yx
    //    };
    //}

    /// <summary>
    /// True if the given values are equal relative to the default accuracy
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqual<T>(this ITriplet<Scalar<T>> x1, ITriplet<Scalar<T>> x2)
    {
        return
            x1.Item1 == x2.Item1 &&
            x1.Item2 == x2.Item2 &&
            x1.Item3 == x2.Item3;
    }

    /// <summary>
    /// True if the given values are equal relative to the default accuracy
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqual<T>(this ITriplet<Scalar<T>> x1, ITriplet<Scalar<T>> x2)
    {
        return x1.GetDistanceToPoint(x2).IsNearZero();
    }

    /// <summary>
    /// True if the given values are equal relative to the default accuracy
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNegativeEqual<T>(this ITriplet<Scalar<T>> x1, ITriplet<Scalar<T>> x2)
    {
        return
            -x1.Item1 == x2.Item1 &&
            -x1.Item2 == x2.Item2 &&
            -x1.Item3 == x2.Item3;
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsXAxis<T>(this LinUnitBasisVector3D axis)
    //{
    //    return axis is LinUnitBasisVector3D.PositiveX or LinUnitBasisVector3D.NegativeX;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsYAxis<T>(this LinUnitBasisVector3D axis)
    //{
    //    return axis is LinUnitBasisVector3D.PositiveY or LinUnitBasisVector3D.NegativeY;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsZAxis<T>(this LinUnitBasisVector3D axis)
    //{
    //    return axis is LinUnitBasisVector3D.PositiveZ or LinUnitBasisVector3D.NegativeZ;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsPositive<T>(this LinUnitBasisVector3D axis)
    //{
    //    return axis is LinUnitBasisVector3D.PositiveX or LinUnitBasisVector3D.PositiveY or LinUnitBasisVector3D.PositiveZ;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsPositive<T>(this LinUnitBasisVector4D axis)
    //{
    //    return axis is
    //        LinUnitBasisVector4D.PositiveX or
    //        LinUnitBasisVector4D.PositiveY or
    //        LinUnitBasisVector4D.PositiveZ or
    //        LinUnitBasisVector4D.PositiveW;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsNegative<T>(this LinUnitBasisVector3D axis)
    //{
    //    return axis is LinUnitBasisVector3D.NegativeX or LinUnitBasisVector3D.NegativeY or LinUnitBasisVector3D.NegativeZ;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsOppositeTo<T>(this LinUnitBasisVector3D axis1, LinUnitBasisVector3D axis2)
    //{
    //    return axis1 switch
    //    {
    //        LinUnitBasisVector3D.PositiveX => axis2 == LinUnitBasisVector3D.NegativeX,
    //        LinUnitBasisVector3D.PositiveY => axis2 == LinUnitBasisVector3D.NegativeY,
    //        LinUnitBasisVector3D.PositiveZ => axis2 == LinUnitBasisVector3D.NegativeZ,
    //        LinUnitBasisVector3D.NegativeX => axis2 == LinUnitBasisVector3D.PositiveX,
    //        LinUnitBasisVector3D.NegativeY => axis2 == LinUnitBasisVector3D.PositiveY,
    //        _ => axis2 == LinUnitBasisVector3D.PositiveZ,
    //    };
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnitBasisVector3D SelectNearestAxis<T>(this ITriplet<Scalar<T>> unitVector)
    {
        return unitVector.GetMaxAbsComponentIndex() switch
        {
            0 => unitVector.Item1.IsPositive()
                ? LinUnitBasisVector3D.PositiveX
                : LinUnitBasisVector3D.NegativeX,

            1 => unitVector.Item2.IsPositive()
                ? LinUnitBasisVector3D.PositiveY
                : LinUnitBasisVector3D.NegativeY,

            _ => unitVector.Item3.IsPositive()
                ? LinUnitBasisVector3D.PositiveZ
                : LinUnitBasisVector3D.NegativeZ
        };
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static int GetIndex<T>(this LinUnitBasisVector3D axis)
    //{
    //    return axis switch
    //    {
    //        LinUnitBasisVector3D.PositiveX => 0,
    //        LinUnitBasisVector3D.NegativeX => 0,
    //        LinUnitBasisVector3D.PositiveY => 1,
    //        LinUnitBasisVector3D.NegativeY => 1,
    //        LinUnitBasisVector3D.PositiveZ => 2,
    //        LinUnitBasisVector3D.NegativeZ => 2,
    //        _ => throw new InvalidOperationException()
    //    };
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static IntegerSign GetSign<T>(this LinUnitBasisVector3D axis)
    //{
    //    return axis switch
    //    {
    //        LinUnitBasisVector3D.PositiveX => IntegerSign.Positive,
    //        LinUnitBasisVector3D.NegativeX => IntegerSign.Negative,
    //        LinUnitBasisVector3D.PositiveY => IntegerSign.Positive,
    //        LinUnitBasisVector3D.NegativeY => IntegerSign.Negative,
    //        LinUnitBasisVector3D.PositiveZ => IntegerSign.Positive,
    //        LinUnitBasisVector3D.NegativeZ => IntegerSign.Negative,
    //        _ => throw new InvalidOperationException()
    //    };
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinUnitBasisVector3D ToAxis3D<T>(this int axisIndex, bool isNegative = false)
    //{
    //    if (isNegative)
    //        return axisIndex switch
    //        {
    //            0 => LinUnitBasisVector3D.NegativeX,
    //            1 => LinUnitBasisVector3D.NegativeY,
    //            2 => LinUnitBasisVector3D.NegativeZ,
    //            _ => throw new IndexOutOfRangeException()
    //        };

    //    return axisIndex switch
    //    {
    //        0 => LinUnitBasisVector3D.PositiveX,
    //        1 => LinUnitBasisVector3D.PositiveY,
    //        2 => LinUnitBasisVector3D.PositiveZ,
    //        _ => throw new IndexOutOfRangeException()
    //    };
    //}


    public static Scalar<T> VectorENormNormSquared<T>(this ITriplet<ComplexNumber<T>> vector)
    {
        return (vector.Item1 * vector.Item1.Conjugate()).Real +
               (vector.Item2 * vector.Item2.Conjugate()).Real +
               (vector.Item3 * vector.Item3.Conjugate()).Real;
    }

    /// <summary>
    /// The Euclidean squared length of this vector when it represents a vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> VectorENorm<T>(this ITriplet<Scalar<T>> vector)
    {
        return (vector.Item1 * vector.Item1 + vector.Item2 * vector.Item2 + vector.Item3 * vector.Item3).Sqrt();
    }

    /// <summary>
    /// The Euclidean squared length of this vector when it represents a vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<LinVector3D<T>, Scalar<T>> GetUnitVectorENormTuple<T>(this ITriplet<Scalar<T>> vector)
    {
        var length = (vector.Item1 * vector.Item1 + vector.Item2 * vector.Item2 + vector.Item3 * vector.Item3).Sqrt();

        if (length == 0d)
            return new Tuple<LinVector3D<T>, Scalar<T>>(vector.ToVector3D(), length);

        var unitVector = LinVector3D<T>.Create(
            vector.Item1 / length,
            vector.Item2 / length,
            vector.Item3 / length
        );

        return new Tuple<LinVector3D<T>, Scalar<T>>(unitVector, length);
    }

    /// <summary>
    /// The Euclidean squared length of this vector when it represents a vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> VectorENormSquared<T>(this ITriplet<Scalar<T>> vector)
    {
        return vector.Item1 * vector.Item1 +
               vector.Item2 * vector.Item2 +
               vector.Item3 * vector.Item3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> VectorENorm<T>(Scalar<T> vectorX, Scalar<T> vectorY, Scalar<T> vectorZ)
    {
        return (vectorX * vectorX + vectorY * vectorY + vectorZ * vectorZ).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> VectorDivideByENorm<T>(this ITriplet<Scalar<T>> vector)
    {
        var norm = vector.VectorENorm();

        return norm.IsZero()
            ? vector.ToVector3D()
            : vector.VectorDivide(norm);
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUnitVector<T>(this ITriplet<Scalar<T>> vector)
    {
        return vector.VectorENormSquared().IsNearOne();
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearUnitVector<T>(this ITriplet<Scalar<T>> vector)
    {
        return vector.VectorENormSquared().IsNearOne();
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near zero
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroVector<T>(this ITriplet<Scalar<T>> vector)
    {
        return vector.VectorENormSquared().IsZero();
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near zero
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZeroVector<T>(this ITriplet<Scalar<T>> vector)
    {
        return vector.VectorENormSquared().IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearVector<T>(this ITriplet<Scalar<T>> vector1, ITriplet<Scalar<T>> vector2)
    {
        return vector1.Item1.IsNearEqualTo(vector2.Item1) &&
               vector1.Item2.IsNearEqualTo(vector2.Item2) &&
               vector1.Item3.IsNearEqualTo(vector2.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearVectorNegative<T>(this ITriplet<Scalar<T>> vector1, ITriplet<Scalar<T>> vector2)
    {
        return vector1.Item1.IsNearEqualTo(-vector2.Item1) &&
               vector1.Item2.IsNearEqualTo(-vector2.Item2) &&
               vector1.Item3.IsNearEqualTo(-vector2.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> VectorENormSquared<T>(Scalar<T> vectorX, Scalar<T> vectorY, Scalar<T> vectorZ)
    {
        return vectorX * vectorX +
               vectorY * vectorY +
               vectorZ * vectorZ;
    }

    public static Scalar<T> VectorENorm<T>(this ITriplet<ComplexNumber<T>> vector)
    {
        return ((vector.Item1 * vector.Item1.Conjugate()).Real + (vector.Item2 * vector.Item2.Conjugate()).Real + (vector.Item3 * vector.Item3.Conjugate()).Real).Sqrt();
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> VectorESp<T>(this LinUnitBasisVector3D v1, ITriplet<Scalar<T>> v2)
    {
        return v1 switch
        {
            LinUnitBasisVector3D.PositiveX => v2.Item1,
            LinUnitBasisVector3D.PositiveY => v2.Item2,
            LinUnitBasisVector3D.PositiveZ => v2.Item3,
            LinUnitBasisVector3D.NegativeX => -v2.Item1,
            LinUnitBasisVector3D.NegativeY => -v2.Item2,
            _ => -v2.Item3,
        };
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> VectorESp<T>(this ITriplet<Scalar<T>> v1, LinUnitBasisVector3D v2)
    {
        return v2 switch
        {
            LinUnitBasisVector3D.PositiveX => v1.Item1,
            LinUnitBasisVector3D.PositiveY => v1.Item2,
            LinUnitBasisVector3D.PositiveZ => v1.Item3,
            LinUnitBasisVector3D.NegativeX => -v1.Item1,
            LinUnitBasisVector3D.NegativeY => -v1.Item2,
            _ => -v1.Item3,
        };
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> VectorESp<T>(this ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2)
    {
        return v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3;
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="v3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> VectorESp<T>(this ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2, ITriplet<Scalar<T>> v3)
    {
        return new Pair<Scalar<T>>(
            v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3,
            v1.Item1 * v3.Item1 + v1.Item2 * v3.Item2 + v1.Item3 * v3.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComplexNumber<T> VectorESp<T>(this ITriplet<ComplexNumber<T>> v1, ITriplet<ComplexNumber<T>> v2)
    {
        return v1.Item1 * v2.Item1.Conjugate() +
               v1.Item2 * v2.Item2.Conjugate() +
               v1.Item3 * v2.Item3.Conjugate();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComplexNumber<T> VectorESp<T>(this ITriplet<ComplexNumber<T>> v1, ITriplet<Scalar<T>> v2)
    {
        return v1.Item1 * v2.Item1 +
               v1.Item2 * v2.Item2 +
               v1.Item3 * v2.Item3;
    }

    /// <summary>
    /// The absolute value of the Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> VectorESpAbs<T>(this ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2)
    {
        return (v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3).Abs();
    }

    /// <summary>
    /// The Euclidean cross product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> VectorCross<T>(this LinUnitBasisVector3D v1, ITriplet<Scalar<T>> v2)
    {
        var zero = v2.GetScalarProcessor().Zero;

        return v1 switch
        {
            LinUnitBasisVector3D.PositiveX => LinVector3D<T>.Create(zero, -v2.Item3, v2.Item2),
            LinUnitBasisVector3D.PositiveY => LinVector3D<T>.Create(v2.Item3, zero, -v2.Item1),
            LinUnitBasisVector3D.PositiveZ => LinVector3D<T>.Create(-v2.Item2, v2.Item1, zero),
            LinUnitBasisVector3D.NegativeX => LinVector3D<T>.Create(zero, v2.Item3, -v2.Item2),
            LinUnitBasisVector3D.NegativeY => LinVector3D<T>.Create(-v2.Item3, zero, v2.Item1),
            _ => LinVector3D<T>.Create(v2.Item2, -v2.Item1, zero)
        };
    }

    /// <summary>
    /// The Euclidean cross product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> VectorCross<T>(this ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2)
    {
        return LinVector3D<T>.Create(v1.Item2 * v2.Item3 - v1.Item3 * v2.Item2,
            v1.Item3 * v2.Item1 - v1.Item1 * v2.Item3,
            v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetVectorCrossNorm<T>(this ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2)
    {
        var x = v1.Item2 * v2.Item3 - v1.Item3 * v2.Item2;
        var y = v1.Item3 * v2.Item1 - v1.Item1 * v2.Item3;
        var z = v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1;

        return (x * x + y * y + z * z).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetVectorCrossNormSquared<T>(this ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2)
    {
        var x = v1.Item2 * v2.Item3 - v1.Item3 * v2.Item2;
        var y = v1.Item3 * v2.Item1 - v1.Item1 * v2.Item3;
        var z = v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1;

        return x * x + y * y + z * z;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> VectorAdd<T>(this ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2)
    {
        return LinVector3D<T>.Create(v1.Item1 + v2.Item1,
            v1.Item2 + v2.Item2,
            v1.Item3 + v2.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> VectorAdd<T>(this ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2, ITriplet<Scalar<T>> v3)
    {
        return LinVector3D<T>.Create(v1.Item1 + v2.Item1 + v3.Item1,
            v1.Item2 + v2.Item2 + v3.Item2,
            v1.Item3 + v2.Item3 + v3.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> VectorAdd<T>(this ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2, ITriplet<Scalar<T>> v3, ITriplet<Scalar<T>> v4)
    {
        return LinVector3D<T>.Create(v1.Item1 + v2.Item1 + v3.Item1 + v4.Item1,
            v1.Item2 + v2.Item2 + v3.Item2 + v4.Item2,
            v1.Item3 + v2.Item3 + v3.Item3 + v4.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> VectorSubtract<T>(this ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2)
    {
        return LinVector3D<T>.Create(
            v1.Item1 - v2.Item1,
            v1.Item2 - v2.Item2,
            v1.Item3 - v2.Item3
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> VectorTimes<T>(this ITriplet<Scalar<T>> v1, int v2)
    {
        return LinVector3D<T>.Create(
            v1.Item1 * v2,
            v1.Item2 * v2,
            v1.Item3 * v2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> VectorTimes<T>(this ITriplet<Scalar<T>> v1, Scalar<T> v2)
    {
        return LinVector3D<T>.Create(
            v1.Item1 * v2,
            v1.Item2 * v2,
            v1.Item3 * v2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> VectorTimes<T>(this ITriplet<Scalar<T>> v1, T v2)
    {
        Debug.Assert(v2 is not null);

        return LinVector3D<T>.Create(
            v1.Item1 * v2,
            v1.Item2 * v2,
            v1.Item3 * v2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> VectorTimes<T>(this Scalar<T> v1, ITriplet<Scalar<T>> v2)
    {
        return LinVector3D<T>.Create(v1 * v2.Item1,
            v1 * v2.Item2,
            v1 * v2.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> VectorDivide<T>(this ITriplet<Scalar<T>> v1, Scalar<T> v2)
    {
        v2 = 1d / v2;

        return LinVector3D<T>.Create(v1.Item1 * v2,
            v1.Item2 * v2,
            v1.Item3 * v2);
    }

    /// <summary>
    /// Returns the Euclidean cross product between the given vectors as a unit vector
    /// Both vectors are assumed to have z=0 components
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> VectorUnitCrossXy<T>(this IPair<Scalar<T>> v1, IPair<Scalar<T>> v2)
    {
        var scalarProcessor = v1.GetScalarProcessor();

        var vz = v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1;

        return LinVector3D<T>.Create(
            scalarProcessor.Zero,
            scalarProcessor.Zero,
            vz.IsNegative()
                ? scalarProcessor.MinusOne
                : vz.IsPositive() ? scalarProcessor.One : scalarProcessor.Zero
            );
    }

    /// <summary>
    /// Returns the Euclidean cross product between the given vectors as a unit vector
    /// The first vector is assumed to have z=0 while the second x=0 and y=0
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2Z"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> VectorUnitCrossXy_Z<T>(this IPair<Scalar<T>> v1, Scalar<T> v2Z)
    {
        var scalarProcessor = v1.GetScalarProcessor();

        var vx = v1.Item2 * v2Z;
        var vy = -v1.Item1 * v2Z;

        var s = (vx * vx + vy * vy).Sqrt();

        if (s.IsZero())
            return LinVector3D<T>.UnitSymmetric(scalarProcessor);

        s = 1.0d / s;

        return LinVector3D<T>.Create(vx * s, vy * s, scalarProcessor.Zero);
    }

    /// <summary>
    /// Returns the Euclidean cross product between the given vectors as a unit vector
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> VectorUnitCross<T>(this LinUnitBasisVector3D v1, ITriplet<Scalar<T>> v2)
    {
        return v1.VectorCross(v2).ToUnitVector();
    }

    /// <summary>
    /// Returns the Euclidean cross product between the given vectors as a unit vector
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> VectorUnitCross<T>(this ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2)
    {
        var scalarProcessor = v1.GetScalarProcessor();

        var vx = v1.Item2 * v2.Item3 - v1.Item3 * v2.Item2;
        var vy = v1.Item3 * v2.Item1 - v1.Item1 * v2.Item3;
        var vz = v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1;

        var s = (vx * vx + vy * vy + vz * vz).Sqrt();

        if (s.IsZero())
            return LinVector3D<T>.UnitSymmetric(scalarProcessor);

        s = 1.0d / s;
        return LinVector3D<T>.Create(vx * s, vy * s, vz * s);
    }

    /// <summary>
    /// Returns the Euclidean cross product between the given vectors as a unit vector
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2X"></param>
    /// <param name="v2Y"></param>
    /// <param name="v2Z"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> VectorUnitCross<T>(this ITriplet<Scalar<T>> v1, Scalar<T> v2X, Scalar<T> v2Y, Scalar<T> v2Z)
    {
        var scalarProcessor = v1.GetScalarProcessor();

        var vx = v1.Item2 * v2Z - v1.Item3 * v2Y;
        var vy = v1.Item3 * v2X - v1.Item1 * v2Z;
        var vz = v1.Item1 * v2Y - v1.Item2 * v2X;

        var s = (vx * vx + vy * vy + vz * vz).Sqrt();

        if (s.IsZero())
            return LinVector3D<T>.UnitSymmetric(scalarProcessor);

        s = 1.0d / s;
        return LinVector3D<T>.Create(vx * s, vy * s, vz * s);
    }

    /// <summary>
    /// Returns the Euclidean cross product between the given vectors as a unit vector
    /// The second vector is assumed to have z=0
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2X"></param>
    /// <param name="v2Y"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> VectorUnitCrossXy<T>(this ITriplet<Scalar<T>> v1, Scalar<T> v2X, Scalar<T> v2Y)
    {
        var scalarProcessor = v1.GetScalarProcessor();

        var vx = -v1.Item3 * v2Y;
        var vy = v1.Item3 * v2X;
        var vz = v1.Item1 * v2Y - v1.Item2 * v2X;

        var s = (vx * vx + vy * vy + vz * vz).Sqrt();

        if (s.IsZero())
            return LinVector3D<T>.UnitSymmetric(scalarProcessor);

        s = 1.0d / s;
        return LinVector3D<T>.Create(vx * s, vy * s, vz * s);
    }

    /// <summary>
    /// Returns the Euclidean cross product between the given vectors as a unit vector
    /// The second vector is assumed to have z=0
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> VectorUnitCrossXy<T>(this ITriplet<Scalar<T>> v1, IPair<Scalar<T>> v2)
    {
        var scalarProcessor = v1.GetScalarProcessor();

        var vx = -v1.Item3 * v2.Item2;
        var vy = v1.Item3 * v2.Item1;
        var vz = v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1;

        var s = (vx * vx + vy * vy + vz * vz).Sqrt();

        if (s.IsZero())
            return LinVector3D<T>.UnitSymmetric(scalarProcessor);

        s = 1.0d / s;
        return LinVector3D<T>.Create(vx * s, vy * s, vz * s);
    }


    /// <summary>
    /// Returns a unit vector from the given one. If the length of the given vector is near zero
    /// it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="zeroAsSymmetric"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ToUnitVector<T>(this ITriplet<Scalar<T>> vector, bool zeroAsSymmetric = true)
    {
        var scalarProcessor = vector.GetScalarProcessor();

        var s = vector.VectorENorm();

        if (s.IsZero())
            return zeroAsSymmetric
                ? LinVector3D<T>.UnitSymmetric(scalarProcessor)
                : LinVector3D<T>.Zero(scalarProcessor);

        s = 1.0d / s;

        return LinVector3D<T>.Create(vector.Item1 * s, vector.Item2 * s, vector.Item3 * s);
    }

    /// <summary>
    /// Returns a unit vector from the given one. If the length of the given vector is near zero
    /// it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="zeroUnitVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ToUnitVector<T>(this ITriplet<Scalar<T>> vector, LinVector3D<T>? zeroUnitVector)
    {
        var s = vector.VectorENorm();

        if (s.IsZero())
            return zeroUnitVector
                   ?? throw new DivideByZeroException();

        return LinVector3D<T>.Create(vector.Item1 / s, vector.Item2 / s, vector.Item3 / s);
    }

    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector is near 
    /// zero it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> Negative<T>(this ITriplet<Scalar<T>> vector)
    {
        return LinVector3D<T>.Create(-vector.Item1, -vector.Item2, -vector.Item3);
    }

    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector is near 
    /// zero it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> NegativeUnitVector<T>(this ITriplet<Scalar<T>> vector)
    {
        var s = vector.VectorENorm();

        if (s.IsZero())
            return vector.ToVector3D();

        s = -1.0d / s;

        return LinVector3D<T>.Create(vector.Item1 * s, vector.Item2 * s, vector.Item3 * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<Scalar<T>, LinVector3D<T>> ToLengthAndUnitDirection<T>(this ITriplet<Scalar<T>> vector)
    {
        var length = (vector.Item1 * vector.Item1 + vector.Item2 * vector.Item2 + vector.Item3 * vector.Item3).Sqrt();

        return Tuple.Create(
            length,
            LinVector3D<T>.Create(
                vector.Item1 / length,
                vector.Item2 / length,
                vector.Item3 / length
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZero<T>(this ITriplet<Scalar<T>> vector)
    {
        return vector.VectorENorm().IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearUnit<T>(this ITriplet<Scalar<T>> vector)
    {
        return vector.VectorENormSquared().IsNearOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthonormalWith<T>(this ITriplet<Scalar<T>> vector1, ITriplet<Scalar<T>> vector2)
    {
        return vector1.IsNearUnit() &&
               vector2.IsNearUnit() &&
               vector1.VectorESp(vector2).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthonormalWithUnit<T>(this ITriplet<Scalar<T>> vector1, ITriplet<Scalar<T>> vector2)
    {
        Debug.Assert(
            vector2.IsNearUnit()
        );

        return vector1.IsNearUnit() &&
               vector1.VectorESp(vector2).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelTo<T>(this ITriplet<Scalar<T>> vector1, ITriplet<Scalar<T>> vector2)
    {
        return vector1.GetAngleCos(vector2).Abs().IsNearOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOppositeTo<T>(this ITriplet<Scalar<T>> vector1, ITriplet<Scalar<T>> vector2)
    {
        return vector1.GetAngleCos(vector2).IsNearMinusOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelToUnit<T>(this ITriplet<Scalar<T>> vector1, ITriplet<Scalar<T>> vector2)
    {
        return vector1.GetAngleCosWithUnit(vector2).Abs().IsNearOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOppositeToUnit<T>(this ITriplet<Scalar<T>> vector1, ITriplet<Scalar<T>> vector2)
    {
        return vector1.GetAngleCosWithUnit(vector2).IsNearMinusOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthogonalTo<T>(this ITriplet<Scalar<T>> vector1, ITriplet<Scalar<T>> vector2)
    {
        return vector1.VectorESp(vector2).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVectorBasis<T>(this ITriplet<Scalar<T>> vector, int basisIndex)
    {
        return basisIndex switch
        {
            0 => vector.Item1.IsOne() && vector.Item2.IsZero() && vector.Item3.IsZero(),
            1 => vector.Item1.IsZero() && vector.Item2.IsOne() && vector.Item3.IsZero(),
            2 => vector.Item1.IsZero() && vector.Item2.IsZero() && vector.Item3.IsOne(),
            _ => false
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearVectorBasis<T>(this ITriplet<Scalar<T>> vector, int basisIndex)
    {
        var scalarProcessor = vector.GetScalarProcessor();

        var vector2 = basisIndex switch
        {
            0 => LinVector3D<T>.E1(scalarProcessor),
            1 => LinVector3D<T>.E2(scalarProcessor),
            2 => LinVector3D<T>.E3(scalarProcessor),
            _ => throw new InvalidOperationException()
        };

        return vector.VectorSubtract(vector2).IsNearZero();
    }

    public static Tuple<bool, Scalar<T>, LinUnitBasisVector3D> TryVectorToAxis<T>(this LinVector3D<T> vector)
    {
        var scalarProcessor = vector.GetScalarProcessor();

        // Find if the given scaling vector is parallel to a basis vector
        var basisIndex = -1;
        for (var i = 0; i < 3; i++)
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
            return new Tuple<bool, Scalar<T>, LinUnitBasisVector3D>(
                false,
                scalarProcessor.Zero,
                LinUnitBasisVector3D.PositiveX
            );

        var scalar = vector.GetItem(basisIndex);

        return new Tuple<bool, Scalar<T>, LinUnitBasisVector3D>(
            true,
            scalar.Abs(),
            basisIndex.ToAxis3D(scalar < 0)
        );
    }

    public static Tuple<bool, Scalar<T>, LinUnitBasisVector3D> TryVectorToAxis<T>(this ITriplet<Scalar<T>> vector)
    {
        var scalarProcessor = vector.GetScalarProcessor();

        // Find if the given scaling vector is parallel to a basis vector
        var basisIndex = -1;
        for (var i = 0; i < 3; i++)
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
            return new Tuple<bool, Scalar<T>, LinUnitBasisVector3D>(
                false,
                scalarProcessor.Zero,
                LinUnitBasisVector3D.PositiveX
            );

        var scalar = vector.GetItem(basisIndex);

        return new Tuple<bool, Scalar<T>, LinUnitBasisVector3D>(
            true,
            scalar.Abs(),
            basisIndex.ToAxis3D(scalar < 0)
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsFinite<T>(this ITriplet<Scalar<T>> vector)
    //{
    //    return vector.Item1.IsFinite() &&
    //           vector.Item2.IsFinite() &&
    //           vector.Item3.IsFinite();
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static DistinctTuplesList3D ToDistinctTuplesList<T>(this IEnumerable<ITriplet<Scalar<T>>> vectorsList)
    //{
    //    return new DistinctTuplesList3D(vectorsList);
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> RealPartToLinVector3D<T>(this ITriplet<ComplexNumber<T>> vector)
    {
        return LinVector3D<T>.Create(
            vector.Item1.Real,
            vector.Item2.Real,
            vector.Item3.Real
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ImaginaryPartToLinVector3D<T>(this ITriplet<ComplexNumber<T>> vector)
    {
        return LinVector3D<T>.Create(
            vector.Item1.Imaginary,
            vector.Item2.Imaginary,
            vector.Item3.Imaginary
        );
    }
}