using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space2D;

public static class LinFloat64Vector2DUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D RoundScalars(this IPair<Float64Scalar> vector)
    {
        return LinFloat64Vector2D.Create(
            Math.Round(vector.Item1.ScalarValue),
            Math.Round(vector.Item2.ScalarValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D ClampTo(this IPair<Float64Scalar> vector, IPair<Float64Scalar> maxTuple)
    {
        return LinFloat64Vector2D.Create(
            vector.Item1.ClampTo(maxTuple.Item1),
            vector.Item2.ClampTo(maxTuple.Item2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D ClampTo(this IPair<Float64Scalar> vector, IPair<Float64Scalar> minTuple, IPair<Float64Scalar> maxTuple)
    {
        return LinFloat64Vector2D.Create(
            vector.Item1.ClampTo(minTuple.Item1, maxTuple.Item1),
            vector.Item2.ClampTo(minTuple.Item2, maxTuple.Item2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D ClampToSymmetric(this IPair<Float64Scalar> vector, IPair<Float64Scalar> maxTuple)
    {
        return LinFloat64Vector2D.Create(
            vector.Item1.ClampToSymmetric(maxTuple.Item1),
            vector.Item2.ClampToSymmetric(maxTuple.Item2)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(this IPair<Float64Scalar> vector)
    {
        return vector.VectorENorm().IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZero(this IPair<Float64Scalar> vector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector.VectorENorm().IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearUnit(this IPair<Float64Scalar> vector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector.VectorENormSquared().IsNearOne(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthonormalWith(this IPair<Float64Scalar> vector1, IPair<Float64Scalar> vector2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector1.IsNearUnit(zeroEpsilon) &&
               vector2.IsNearUnit(zeroEpsilon) &&
               vector1.VectorESp(vector2).IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthonormalWithUnit(this IPair<Float64Scalar> vector1, IPair<Float64Scalar> vector2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        Debug.Assert(
            vector2.IsNearUnit(zeroEpsilon)
        );

        return vector1.IsNearUnit(zeroEpsilon) &&
               vector1.VectorESp(vector2).IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelTo(this IPair<Float64Scalar> vector1, IPair<Float64Scalar> vector2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector1.GetAngleCos(vector2).Abs().IsNearOne(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOppositeTo(this IPair<Float64Scalar> vector1, IPair<Float64Scalar> vector2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector1.GetAngleCos(vector2).IsNearMinusOne(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelToUnit(this IPair<Float64Scalar> vector1, IPair<Float64Scalar> vector2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector1.GetAngleCosWithUnit(vector2).Abs().IsNearOne(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOppositeToUnit(this IPair<Float64Scalar> vector1, IPair<Float64Scalar> vector2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector1.GetAngleCosWithUnit(vector2).IsNearNegativeOne(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthogonalTo(this IPair<Float64Scalar> vector1, IPair<Float64Scalar> vector2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector1.VectorESp(vector2).IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVectorBasis(this IPair<Float64Scalar> vector, int basisIndex)
    {
        return basisIndex switch
        {
            0 => vector.Item1.IsOne() && vector.Item2.IsZero(),
            1 => vector.Item1.IsZero() && vector.Item2.IsOne(),
            _ => false
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearVectorBasis(this IPair<Float64Scalar> vector, int basisIndex, double zeroEpsilon = 1e-12d)
    {
        var vector2 = basisIndex switch
        {
            0 => LinFloat64Vector2D.E1,
            1 => LinFloat64Vector2D.E2,
            _ => throw new InvalidOperationException()
        };

        return vector.VectorSubtract(vector2).IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFinite(this IPair<Float64Scalar> vector)
    {
        return !(
            double.IsNaN(vector.Item1) || double.IsInfinity(vector.Item1) ||
            double.IsNaN(vector.Item2) || double.IsInfinity(vector.Item2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<LinFloat64Vector2D> GetComponentVectors(this IPair<Float64Scalar> vector)
    {
        return new Pair<LinFloat64Vector2D>(
            LinFloat64Vector2D.Create(vector.Item1, Float64Scalar.Zero),
            LinFloat64Vector2D.Create(Float64Scalar.Zero, vector.Item2)
        );
    }

    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector is near 
    /// zero it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D VectorNegative(this IPair<Float64Scalar> vector)
    {
        return LinFloat64Vector2D.Create(-vector.Item1, -vector.Item2);
    }

    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector is near 
    /// zero it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D VectorNegativeUnit(this IPair<Float64Scalar> vector)
    {
        var s = vector.VectorENorm();
        if (s.IsNearZero())
            return vector.ToLinVector2D();

        s = -1.0d / s;
        return LinFloat64Vector2D.Create(vector.Item1 * s, vector.Item2 * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D VectorAdd(this IPair<Float64Scalar> v1, IPair<Float64Scalar> v2)
    {
        return LinFloat64Vector2D.Create(v1.Item1 + v2.Item1,
            v1.Item2 + v2.Item2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D VectorSubtract(this IPair<Float64Scalar> v1, IPair<Float64Scalar> v2)
    {
        return LinFloat64Vector2D.Create(v1.Item1 - v2.Item1,
            v1.Item2 - v2.Item2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D VectorTimes(this IPair<Float64Scalar> v1, double v2)
    {
        return LinFloat64Vector2D.Create(
            v1.Item1 * v2,
            v1.Item2 * v2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D VectorTimes(this double v1, IPair<Float64Scalar> v2)
    {
        return LinFloat64Vector2D.Create(
            v1 * v2.Item1,
            v1 * v2.Item2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D VectorComponentTimes(this IPair<Float64Scalar> v1, IPair<Float64Scalar> v2)
    {
        return LinFloat64Vector2D.Create(
            v1.Item1 * v2.Item1,
            v1.Item2 * v2.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D VectorDivide(this IPair<Float64Scalar> v1, double v2)
    {
        v2 = 1d / v2;

        return LinFloat64Vector2D.Create(v1.Item1 * v2,
            v1.Item2 * v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D VectorDivideByENorm(this IPair<Float64Scalar> vector)
    {
        var norm = vector.VectorENorm();

        return norm.IsZero()
            ? vector.ToLinVector2D()
            : vector.VectorDivide(norm);
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUnitVector(this IPair<Float64Scalar> vector)
    {
        return vector
            .VectorENormSquared()
            .IsOne();
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearUnitVector(this IPair<Float64Scalar> vector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector
            .VectorENormSquared()
            .IsNearEqual(1.0d, zeroEpsilon);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<double, LinFloat64Vector2D> ToLengthAndUnitDirection(this IPair<Float64Scalar> vector)
    {
        var length = Math.Sqrt(
            vector.Item1 * vector.Item1 +
            vector.Item2 * vector.Item2
        );

        var lengthInv = 1 / length;

        return Tuple.Create(
            length,
            LinFloat64Vector2D.Create(vector.Item1 * lengthInv,
                vector.Item2 * lengthInv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex VectorESp(this IPair<Complex> v1, IPair<Complex> v2)
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
    public static Float64Scalar VectorESpAbs(this IPair<Float64Scalar> v1, IPair<Float64Scalar> v2)
    {
        return Math.Abs(v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2);
    }

    /// <summary>
    /// The Euclidean length of this vector when it represents a vector
    /// </summary>
    public static double VectorENorm(this IPair<Float64Scalar> vector)
    {
        return Math.Sqrt(
            vector.Item1 * vector.Item1 +
            vector.Item2 * vector.Item2
        );
    }

    public static double VectorENorm(double vectorX, double vectorY)
    {
        return Math.Sqrt(
            vectorX * vectorX +
            vectorY * vectorY
        );
    }

    public static double VectorENorm(this IPair<Complex> vector)
    {
        return Math.Sqrt(
            (vector.Item1 * vector.Item1.Conjugate()).Real +
            (vector.Item2 * vector.Item2.Conjugate()).Real
        );
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar VectorESp(this IPair<Float64Scalar> v1, IPair<Float64Scalar> v2)
    {
        return v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2;
    }

    /// <summary>
    /// The Euclidean squared length of this vector when it represents a vector
    /// </summary>
    public static Float64Scalar VectorENormSquared(this IPair<Float64Scalar> vector)
    {
        return vector.Item1 * vector.Item1 +
               vector.Item2 * vector.Item2;
    }

    public static double VectorENormSquared(double vectorX, double vectorY)
    {
        return vectorX * vectorX +
               vectorY * vectorY;
    }

    public static double VectorENormSquared(this IPair<Complex> vector)
    {
        return (vector.Item1 * vector.Item1.Conjugate()).Real +
               (vector.Item2 * vector.Item2.Conjugate()).Real;
    }

    public static Complex VectorToComplexNumber(this IPair<Float64Scalar> vector)
    {
        return new Complex(
            vector.Item1.ScalarValue,
            vector.Item2.ScalarValue
        );
    }

    public static Complex VectorToUnitComplexNumber(this IPair<Float64Scalar> vector)
    {
        var norm = vector.VectorENorm();

        return norm.IsNearZero()
            ? Complex.Zero
            : new Complex(
                vector.Item1.ScalarValue / norm,
                vector.Item2.ScalarValue / norm
            );
    }
    
    /// <summary>
    /// The GA Euclidean cross product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar VectorCross(this IPair<Float64Scalar> v1, IPair<Float64Scalar> v2)
    {
        return v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1;
    }
    
    /// <summary>
    /// The GA Euclidean cross product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign VectorCrossSign(this IPair<Float64Scalar> v1, IPair<Float64Scalar> v2)
    {
        return (v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1).Sign();
    }

}