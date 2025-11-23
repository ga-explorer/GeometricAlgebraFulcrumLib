using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

public static class LinFloat64Vector2DUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFinite(this IPair<double> vector)
    {
        return double.IsFinite(vector.Item1) && 
               double.IsFinite(vector.Item2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFinite(this IPair<double> vector, double maxLength)
    {
        return double.IsFinite(vector.Item1) && 
               double.IsFinite(vector.Item2) &&
               vector.L2Norm() <= maxLength;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D RoundScalars(this IPair<double> vector)
    {
        return LinFloat64Vector2D.Create(
            Math.Round(vector.Item1),
            Math.Round(vector.Item2)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D ClampTo(this IPair<double> vector, IPair<double> maxTuple)
    {
        return LinFloat64Vector2D.Create(
            vector.Item1.ClampTo(maxTuple.Item1),
            vector.Item2.ClampTo(maxTuple.Item2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D ClampTo(this IPair<double> vector, IPair<double> minTuple, IPair<double> maxTuple)
    {
        return LinFloat64Vector2D.Create(
            vector.Item1.ClampTo(minTuple.Item1, maxTuple.Item1),
            vector.Item2.ClampTo(minTuple.Item2, maxTuple.Item2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D ClampToSymmetric(this IPair<double> vector, IPair<double> maxTuple)
    {
        return LinFloat64Vector2D.Create(
            vector.Item1.ClampToSymmetric(maxTuple.Item1),
            vector.Item2.ClampToSymmetric(maxTuple.Item2)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(this IPair<double> vector)
    {
        return vector.L2Norm().IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZero(this IPair<double> vector, double tolerance = Float64Utils.DefaultTolerance)
    {
        return vector.L2Norm().IsNearZero(tolerance);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqual(this IPair<double> vector1, IPair<double> vector2, double tolerance = Float64Utils.DefaultTolerance)
    {
        return L2Norm(vector1.VectorSubtract(vector2)).IsNearZero(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearUnit(this IPair<double> vector, double tolerance = Float64Utils.DefaultTolerance)
    {
        return vector.L2NormSquared().IsNearOne(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthonormalWith(this IPair<double> vector1, IPair<double> vector2, double tolerance = Float64Utils.DefaultTolerance)
    {
        return vector1.IsNearUnit(tolerance) &&
               vector2.IsNearUnit(tolerance) &&
               vector1.VectorDot(vector2).IsNearZero(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthonormalWithUnit(this IPair<double> vector1, IPair<double> vector2, double tolerance = Float64Utils.DefaultTolerance)
    {
        Debug.Assert(
            vector2.IsNearUnit(tolerance)
        );

        return vector1.IsNearUnit(tolerance) &&
               vector1.VectorDot(vector2).IsNearZero(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelTo(this IPair<double> vector1, IPair<double> vector2, double tolerance = Float64Utils.DefaultTolerance)
    {
        return vector1.GetAngleCos(vector2).Abs().IsNearOne(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOppositeTo(this IPair<double> vector1, IPair<double> vector2, double tolerance = Float64Utils.DefaultTolerance)
    {
        return vector1.GetAngleCos(vector2).IsNearMinusOne(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelToUnit(this IPair<double> vector1, IPair<double> vector2, double tolerance = Float64Utils.DefaultTolerance)
    {
        return vector1.GetAngleCosWithUnit(vector2).Abs().IsNearOne(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOppositeToUnit(this IPair<double> vector1, IPair<double> vector2, double tolerance = Float64Utils.DefaultTolerance)
    {
        return vector1.GetAngleCosWithUnit(vector2).IsNearNegativeOne(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthogonalTo(this IPair<double> vector1, IPair<double> vector2, double tolerance = Float64Utils.DefaultTolerance)
    {
        return vector1.VectorDot(vector2).IsNearZero(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVectorBasis(this IPair<double> vector, int basisIndex)
    {
        return basisIndex switch
        {
            0 => vector.Item1.IsOne() && vector.Item2.IsZero(),
            1 => vector.Item1.IsZero() && vector.Item2.IsOne(),
            _ => false
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearVectorBasis(this IPair<double> vector, int basisIndex, double tolerance = 1e-12d)
    {
        var vector2 = basisIndex switch
        {
            0 => LinFloat64Vector2D.E1,
            1 => LinFloat64Vector2D.E2,
            _ => throw new InvalidOperationException()
        };

        return vector.VectorSubtract(vector2).IsNearZero(tolerance);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<LinFloat64Vector2D> GetComponentVectors(this IPair<double> vector)
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
    public static LinFloat64Vector2D VectorNegative(this IPair<double> vector)
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
    public static LinFloat64Vector2D VectorNegativeUnit(this IPair<double> vector)
    {
        var s = vector.L2Norm();
        if (s.IsNearZero())
            return vector.ToLinVector2D();

        s = -1.0d / s;
        return LinFloat64Vector2D.Create(vector.Item1 * s, vector.Item2 * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D VectorAdd(this IPair<double> v1, IPair<double> v2)
    {
        return LinFloat64Vector2D.Create(v1.Item1 + v2.Item1,
            v1.Item2 + v2.Item2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D VectorAdd(this IPair<double> v1, double v2X, double v2Y)
    {
        return LinFloat64Vector2D.Create(
            v1.Item1 + v2X,
            v1.Item2 + v2Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D VectorSubtract(this IPair<double> v1, IPair<double> v2)
    {
        return LinFloat64Vector2D.Create(v1.Item1 - v2.Item1,
            v1.Item2 - v2.Item2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D VectorSubtract(this IPair<double> v1, double v2X, double v2Y)
    {
        return LinFloat64Vector2D.Create(
            v1.Item1 - v2X,
            v1.Item2 - v2Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D VectorTimes(this IPair<double> v1, double v2)
    {
        return LinFloat64Vector2D.Create(
            v1.Item1 * v2,
            v1.Item2 * v2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D VectorTimes(this double v1, IPair<double> v2)
    {
        return LinFloat64Vector2D.Create(
            v1 * v2.Item1,
            v1 * v2.Item2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D VectorComponentTimes(this IPair<double> v1, IPair<double> v2)
    {
        return LinFloat64Vector2D.Create(
            v1.Item1 * v2.Item1,
            v1.Item2 * v2.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D VectorDivide(this IPair<double> v1, double v2)
    {
        v2 = 1d / v2;

        return LinFloat64Vector2D.Create(v1.Item1 * v2,
            v1.Item2 * v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D VectorDivideByENorm(this IPair<double> vector)
    {
        var norm = vector.L2Norm();

        return norm.IsZero()
            ? vector.ToLinVector2D()
            : vector.VectorDivide(norm);
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUnitVector(this IPair<double> vector)
    {
        return vector
            .L2NormSquared()
            .IsOne();
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearUnitVector(this IPair<double> vector, double tolerance = Float64Utils.DefaultTolerance)
    {
        return vector
            .L2NormSquared()
            .IsNearEqual(1.0d, tolerance);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<double, LinFloat64Vector2D> ToLengthAndUnitDirection(this IPair<double> vector)
    {
        var length = Math.Sqrt(
            vector.Item1 * vector.Item1 +
            vector.Item2 * vector.Item2
        );

        var lengthInv = 1 / length;

        return Tuple.Create(
            length,
            LinFloat64Vector2D.Create(
                vector.Item1 * lengthInv,
                vector.Item2 * lengthInv
            )
        );
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar VectorDot(this IPair<double> v1, IPair<double> v2)
    {
        return v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex VectorDot(this IPair<Complex> v1, IPair<Complex> v2)
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
    public static Float64Scalar VectorDotAbs(this IPair<double> v1, IPair<double> v2)
    {
        return Math.Abs(v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2);
    }

    /// <summary>
    /// The Euclidean length of this vector when it represents a vector
    /// </summary>
    public static double L2Norm(this IPair<double> vector)
    {
        return Math.Sqrt(
            vector.Item1 * vector.Item1 +
            vector.Item2 * vector.Item2
        );
    }

    public static double L2Norm(double vectorX, double vectorY)
    {
        return Math.Sqrt(
            vectorX * vectorX +
            vectorY * vectorY
        );
    }

    public static double L2Norm(this IPair<Complex> vector)
    {
        return Math.Sqrt(
            (vector.Item1 * vector.Item1.Conjugate()).Real +
            (vector.Item2 * vector.Item2.Conjugate()).Real
        );
    }

    /// <summary>
    /// The Euclidean squared length of this vector when it represents a vector
    /// </summary>
    public static Float64Scalar L2NormSquared(this IPair<double> vector)
    {
        return vector.Item1 * vector.Item1 +
               vector.Item2 * vector.Item2;
    }

    public static double L2NormSquared(double vectorX, double vectorY)
    {
        return vectorX * vectorX +
               vectorY * vectorY;
    }

    public static double L2NormSquared(this IPair<Complex> vector)
    {
        return (vector.Item1 * vector.Item1.Conjugate()).Real +
               (vector.Item2 * vector.Item2.Conjugate()).Real;
    }

    public static Complex VectorToComplexNumber(this IPair<double> vector)
    {
        return new Complex(
            vector.Item1,
            vector.Item2
        );
    }

    public static Complex VectorToUnitComplexNumber(this IPair<double> vector)
    {
        var norm = vector.L2Norm();

        return norm.IsNearZero()
            ? Complex.Zero
            : new Complex(
                vector.Item1 / norm,
                vector.Item2 / norm
            );
    }
    
    /// <summary>
    /// The GA Euclidean cross product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double VectorCross(this IPair<double> v1, IPair<double> v2)
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
    public static IntegerSign VectorCrossSign(this IPair<double> v1, IPair<double> v2)
    {
        return (v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1).Sign();
    }

}