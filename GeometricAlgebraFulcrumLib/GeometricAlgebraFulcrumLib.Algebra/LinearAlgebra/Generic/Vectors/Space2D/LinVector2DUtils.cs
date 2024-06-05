using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;

public static class LinVector2DUtils
{
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector2D<T> ClampTo<T>(this IPair<Scalar<T>> vector, IPair<Scalar<T>> maxTuple)
    //{
    //    return LinVector2D<T>.Create(
    //        vector.Item1.ClampTo(maxTuple.Item1),
    //        vector.Item2.ClampTo(maxTuple.Item2)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector2D<T> ClampTo<T>(this IPair<Scalar<T>> vector, IPair<Scalar<T>> minTuple, IPair<Scalar<T>> maxTuple)
    //{
    //    return LinVector2D<T>.Create(
    //        vector.Item1.ClampTo(minTuple.Item1, maxTuple.Item1),
    //        vector.Item2.ClampTo(minTuple.Item2, maxTuple.Item2)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector2D<T> ClampToSymmetric<T>(this IPair<Scalar<T>> vector, IPair<Scalar<T>> maxTuple)
    //{
    //    return LinVector2D<T>.Create(
    //        vector.Item1.ClampToSymmetric(maxTuple.Item1),
    //        vector.Item2.ClampToSymmetric(maxTuple.Item2)
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IScalarProcessor<T> GetScalarProcessor<T>(this IPair<Scalar<T>> vector)
    {
        return vector.Item1.ScalarProcessor;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ToVector2D<T>(this IPair<Scalar<T>> vector)
    {
        return vector as LinVector2D<T>
            ?? LinVector2D<T>.Create(vector.Item1, vector.Item2);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool VectorIsZero<T>(this IPair<Scalar<T>> vector)
    {
        return vector.VectorENorm().IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool VectorIsNearZero<T>(this IPair<Scalar<T>> vector)
    {
        return vector.VectorENorm().IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool VectorIsNearUnit<T>(this IPair<Scalar<T>> vector)
    {
        return vector.VectorENormSquared().IsNearOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool VectorIsNearOrthonormalWith<T>(this IPair<Scalar<T>> vector1, IPair<Scalar<T>> vector2)
    {
        return vector1.VectorIsNearUnit() &&
               vector2.VectorIsNearUnit() &&
               vector1.VectorESp(vector2).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool VectorIsNearOrthonormalWithUnit<T>(this IPair<Scalar<T>> vector1, IPair<Scalar<T>> vector2)
    {
        Debug.Assert(
            vector2.VectorIsNearUnit()
        );

        return vector1.VectorIsNearUnit() &&
               vector1.VectorESp(vector2).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool VectorIsNearParallelTo<T>(this IPair<Scalar<T>> vector1, IPair<Scalar<T>> vector2)
    {
        return vector1.GetAngleCos(vector2).Abs().IsNearOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool VectorIsNearOppositeTo<T>(this IPair<Scalar<T>> vector1, IPair<Scalar<T>> vector2)
    {
        return vector1.GetAngleCos(vector2).IsNearMinusOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool VectorIsNearParallelToUnit<T>(this IPair<Scalar<T>> vector1, IPair<Scalar<T>> vector2)
    {
        return vector1.GetAngleCosWithUnit(vector2).Abs().IsNearOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool VectorIsNearOppositeToUnit<T>(this IPair<Scalar<T>> vector1, IPair<Scalar<T>> vector2)
    {
        return vector1.GetAngleCosWithUnit(vector2).IsNearMinusOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool VectorIsNearOrthogonalTo<T>(this IPair<Scalar<T>> vector1, IPair<Scalar<T>> vector2)
    {
        return vector1.VectorESp(vector2).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool VectorIsVectorBasis<T>(this IPair<Scalar<T>> vector, int basisIndex)
    {
        return basisIndex switch
        {
            0 => vector.Item1.IsOne() && vector.Item2.IsZero(),
            1 => vector.Item1.IsZero() && vector.Item2.IsOne(),
            _ => false
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool VectorIsNearVectorBasis<T>(this IPair<Scalar<T>> vector, int basisIndex)
    {
        var scalarProcessor = vector.GetScalarProcessor();

        var vector2 = basisIndex switch
        {
            0 => LinVector2D<T>.E1(scalarProcessor),
            1 => LinVector2D<T>.E2(scalarProcessor),
            _ => throw new InvalidOperationException()
        };

        return vector.VectorSubtract(vector2).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool VectorIsFinite<T>(this IPair<Scalar<T>> vector)
    {
        return vector.Item1.IsValid() && 
               vector.Item2.IsValid() &&
               vector.Item1.IsFiniteNumber() &&
               vector.Item2.IsFiniteNumber();
    }


    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector is near 
    /// zero it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> VectorNegative<T>(this IPair<Scalar<T>> vector)
    {
        return LinVector2D<T>.Create(-vector.Item1, -vector.Item2);
    }

    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector is near 
    /// zero it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> VectorNegativeUnit<T>(this IPair<Scalar<T>> vector)
    {
        var s = vector.VectorENorm();
        if (s.IsNearZero())
            return vector.ToVector2D();

        s = -1.0d / s;
        return LinVector2D<T>.Create(vector.Item1 * s, vector.Item2 * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> VectorAdd<T>(this IPair<Scalar<T>> v1, IPair<Scalar<T>> v2)
    {
        return LinVector2D<T>.Create(v1.Item1 + v2.Item1, v1.Item2 + v2.Item2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> VectorSubtract<T>(this IPair<Scalar<T>> v1, IPair<Scalar<T>> v2)
    {
        return LinVector2D<T>.Create(v1.Item1 - v2.Item1, v1.Item2 - v2.Item2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> VectorTimes<T>(this IPair<Scalar<T>> v1, Scalar<T> v2)
    {
        return LinVector2D<T>.Create(v1.Item1 * v2, v1.Item2 * v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> VectorTimes<T>(this Scalar<T> v1, IPair<Scalar<T>> v2)
    {
        return LinVector2D<T>.Create(v1 * v2.Item1, v1 * v2.Item2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> VectorDivide<T>(this IPair<Scalar<T>> v1, Scalar<T> v2)
    {
        v2 = 1d / v2;

        return LinVector2D<T>.Create(v1.Item1 * v2, v1.Item2 * v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> VectorDivideByNorm<T>(this IPair<Scalar<T>> vector)
    {
        var norm = vector.VectorENorm();

        return norm.IsZero()
            ? vector.ToVector2D()
            : vector.VectorDivide(norm);
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUnitVector<T>(this IPair<Scalar<T>> vector)
    {
        return vector
            .VectorENormSquared()
            .IsOne();
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearUnitVector<T>(this IPair<Scalar<T>> vector)
    {
        return vector.VectorENormSquared().IsNearOne();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<Scalar<T>, LinVector2D<T>> VectorToLengthAndUnitDirection<T>(this IPair<Scalar<T>> vector)
    {
        var length = (vector.Item1 * vector.Item1 + vector.Item2 * vector.Item2).Sqrt();

        var lengthInv = 1 / length;

        return Tuple.Create(
            length,
            LinVector2D<T>.Create(vector.Item1 * lengthInv, vector.Item2 * lengthInv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComplexNumber<T> VectorESp<T>(this ILinComplexVector2D<T> v1, ILinComplexVector2D<T> v2)
    {
        return v1.Item1 * v2.Item1.Conjugate() +
               v1.Item2 * v2.Item2.Conjugate();
    }

    /// <summary>
    /// The absolute value of the Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> VectorESpAbs<T>(this IPair<Scalar<T>> v1, IPair<Scalar<T>> v2)
    {
        return (v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2).Abs();
    }

    /// <summary>
    /// The Euclidean length of this vector when it represents a vector
    /// </summary>
    public static Scalar<T> VectorENorm<T>(this IPair<Scalar<T>> vector)
    {
        return (vector.Item1 * vector.Item1 + vector.Item2 * vector.Item2).Sqrt();
    }
    
    public static Scalar<T> VectorENorm<T>(this IScalarProcessor<T> scalarProcessor, T vectorX, T vectorY)
    {
        return (scalarProcessor.Square(vectorX) + scalarProcessor.Square(vectorY)).Sqrt();
    }

    public static Scalar<T> VectorENorm<T>(Scalar<T> vectorX, Scalar<T> vectorY)
    {
        return (vectorX * vectorX + vectorY * vectorY).Sqrt();
    }

    public static Scalar<T> VectorENorm<T>(this ILinComplexVector2D<T> vector)
    {
        return ((vector.Item1 * vector.Item1.Conjugate()).Real + (vector.Item2 * vector.Item2.Conjugate()).Real).Sqrt();
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> VectorESp<T>(this IPair<Scalar<T>> v1, IPair<Scalar<T>> v2)
    {
        return v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2;
    }

    /// <summary>
    /// The Euclidean squared length of this vector when it represents a vector
    /// </summary>
    public static Scalar<T> VectorENormSquared<T>(this IPair<Scalar<T>> vector)
    {
        return vector.Item1 * vector.Item1 +
               vector.Item2 * vector.Item2;
    }

    public static Scalar<T> VectorENormSquared<T>(Scalar<T> vectorX, Scalar<T> vectorY)
    {
        return vectorX * vectorX +
               vectorY * vectorY;
    }

    public static Scalar<T> VectorENormSquared<T>(this ILinComplexVector2D<T> vector)
    {
        return (vector.Item1 * vector.Item1.Conjugate()).Real +
               (vector.Item2 * vector.Item2.Conjugate()).Real;
    }

    public static ComplexNumber<T> VectorToComplexNumber<T>(this IPair<Scalar<T>> vector)
    {
        return new ComplexNumber<T>(
            vector.Item1,
            vector.Item2
        );
    }

    public static ComplexNumber<T> VectorToUnitComplexNumber<T>(this IPair<Scalar<T>> vector)
    {
        var norm = vector.VectorENorm();

        return norm.IsNearZero()
            ? ComplexNumber<T>.Zero(vector.GetScalarProcessor())
            : new ComplexNumber<T>(
                vector.Item1 / norm,
                vector.Item2 / norm
            );
    }

}