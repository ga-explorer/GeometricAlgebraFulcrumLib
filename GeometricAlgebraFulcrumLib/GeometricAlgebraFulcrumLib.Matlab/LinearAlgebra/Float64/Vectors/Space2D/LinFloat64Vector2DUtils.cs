using System;
using System.Diagnostics;
using System.Numerics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;

public static class LinFloat64Vector2DUtils
{
    
    public static LinFloat64Vector2D RoundScalars(this IPair<double> vector)
    {
        return LinFloat64Vector2D.Create(
            Math.Round(vector.Item1),
            Math.Round(vector.Item2)
        );
    }


    
    public static LinFloat64Vector2D ClampTo(this IPair<double> vector, IPair<double> maxTuple)
    {
        return LinFloat64Vector2D.Create(
            vector.Item1.ClampTo(maxTuple.Item1),
            vector.Item2.ClampTo(maxTuple.Item2)
        );
    }

    
    public static LinFloat64Vector2D ClampTo(this IPair<double> vector, IPair<double> minTuple, IPair<double> maxTuple)
    {
        return LinFloat64Vector2D.Create(
            vector.Item1.ClampTo(minTuple.Item1, maxTuple.Item1),
            vector.Item2.ClampTo(minTuple.Item2, maxTuple.Item2)
        );
    }

    
    public static LinFloat64Vector2D ClampToSymmetric(this IPair<double> vector, IPair<double> maxTuple)
    {
        return LinFloat64Vector2D.Create(
            vector.Item1.ClampToSymmetric(maxTuple.Item1),
            vector.Item2.ClampToSymmetric(maxTuple.Item2)
        );
    }


    
    public static bool IsZero(this IPair<double> vector)
    {
        return vector.VectorENorm().IsZero();
    }

    
    public static bool IsNearZero(this IPair<double> vector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector.VectorENorm().IsNearZero(zeroEpsilon);
    }

    
    public static bool IsNearUnit(this IPair<double> vector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector.VectorENormSquared().IsNearOne(zeroEpsilon);
    }

    
    public static bool IsNearOrthonormalWith(this IPair<double> vector1, IPair<double> vector2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector1.IsNearUnit(zeroEpsilon) &&
               vector2.IsNearUnit(zeroEpsilon) &&
               vector1.VectorESp(vector2).IsNearZero(zeroEpsilon);
    }

    
    public static bool IsNearOrthonormalWithUnit(this IPair<double> vector1, IPair<double> vector2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        Debug.Assert(
            vector2.IsNearUnit(zeroEpsilon)
        );

        return vector1.IsNearUnit(zeroEpsilon) &&
               vector1.VectorESp(vector2).IsNearZero(zeroEpsilon);
    }

    
    public static bool IsNearParallelTo(this IPair<double> vector1, IPair<double> vector2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector1.GetAngleCos(vector2).Abs().IsNearOne(zeroEpsilon);
    }

    
    public static bool IsNearOppositeTo(this IPair<double> vector1, IPair<double> vector2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector1.GetAngleCos(vector2).IsNearMinusOne(zeroEpsilon);
    }

    
    public static bool IsNearParallelToUnit(this IPair<double> vector1, IPair<double> vector2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector1.GetAngleCosWithUnit(vector2).Abs().IsNearOne(zeroEpsilon);
    }

    
    public static bool IsNearOppositeToUnit(this IPair<double> vector1, IPair<double> vector2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector1.GetAngleCosWithUnit(vector2).IsNearMinusOne(zeroEpsilon);
    }

    
    public static bool IsNearOrthogonalTo(this IPair<double> vector1, IPair<double> vector2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector1.VectorESp(vector2).IsNearZero(zeroEpsilon);
    }

    
    public static bool IsVectorBasis(this IPair<double> vector, int basisIndex)
    {
        return basisIndex switch
        {
            0 => vector.Item1.IsOne() && vector.Item2.IsZero(),
            1 => vector.Item1.IsZero() && vector.Item2.IsOne(),
            _ => false
        };
    }

    
    public static bool IsNearVectorBasis(this IPair<double> vector, int basisIndex, double zeroEpsilon = 1e-12d)
    {
        var vector2 = basisIndex switch
        {
            0 => LinFloat64Vector2D.E1,
            1 => LinFloat64Vector2D.E2,
            _ => throw new InvalidOperationException()
        };

        return vector.VectorSubtract(vector2).IsNearZero(zeroEpsilon);
    }

    
    public static bool IsFinite(this IPair<double> vector)
    {
        return !(
            double.IsNaN(vector.Item1) || double.IsInfinity(vector.Item1) ||
            double.IsNaN(vector.Item2) || double.IsInfinity(vector.Item2)
        );
    }

    
    
    public static Pair<LinFloat64Vector2D> GetComponentVectors(this IPair<double> vector)
    {
        return new Pair<LinFloat64Vector2D>(
            LinFloat64Vector2D.Create(vector.Item1, 0d),
            LinFloat64Vector2D.Create(0d, vector.Item2)
        );
    }

    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector is near 
    /// zero it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    
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
    
    public static LinFloat64Vector2D VectorNegativeUnit(this IPair<double> vector)
    {
        var s = vector.VectorENorm();
        if (s.IsNearZero())
            return vector.ToLinVector2D();

        s = -1.0d / s;
        return LinFloat64Vector2D.Create(vector.Item1 * s, vector.Item2 * s);
    }

    
    public static LinFloat64Vector2D VectorAdd(this IPair<double> v1, IPair<double> v2)
    {
        return LinFloat64Vector2D.Create(v1.Item1 + v2.Item1,
            v1.Item2 + v2.Item2);
    }

    
    public static LinFloat64Vector2D VectorSubtract(this IPair<double> v1, IPair<double> v2)
    {
        return LinFloat64Vector2D.Create(v1.Item1 - v2.Item1,
            v1.Item2 - v2.Item2);
    }

    
    public static LinFloat64Vector2D VectorTimes(this IPair<double> v1, double v2)
    {
        return LinFloat64Vector2D.Create(
            v1.Item1 * v2,
            v1.Item2 * v2
        );
    }

    
    public static LinFloat64Vector2D VectorTimes(this double v1, IPair<double> v2)
    {
        return LinFloat64Vector2D.Create(
            v1 * v2.Item1,
            v1 * v2.Item2
        );
    }
    
    
    public static LinFloat64Vector2D VectorComponentTimes(this IPair<double> v1, IPair<double> v2)
    {
        return LinFloat64Vector2D.Create(
            v1.Item1 * v2.Item1,
            v1.Item2 * v2.Item2
        );
    }

    
    public static LinFloat64Vector2D VectorDivide(this IPair<double> v1, double v2)
    {
        v2 = 1d / v2;

        return LinFloat64Vector2D.Create(v1.Item1 * v2,
            v1.Item2 * v2);
    }

    
    public static LinFloat64Vector2D VectorDivideByENorm(this IPair<double> vector)
    {
        var norm = vector.VectorENorm();

        return norm.IsZero()
            ? vector.ToLinVector2D()
            : vector.VectorDivide(norm);
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    
    public static bool IsUnitVector(this IPair<double> vector)
    {
        return vector
            .VectorENormSquared()
            .IsOne();
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    
    public static bool IsNearUnitVector(this IPair<double> vector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector
            .VectorENormSquared()
            .IsNearEqual(1.0d, zeroEpsilon);
    }


    
    public static Tuple<double, LinFloat64Vector2D> ToLengthAndUnitDirection(this IPair<double> vector)
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
    
    public static double VectorESpAbs(this IPair<double> v1, IPair<double> v2)
    {
        return Math.Abs(v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2);
    }

    /// <summary>
    /// The Euclidean length of this vector when it represents a vector
    /// </summary>
    public static double VectorENorm(this IPair<double> vector)
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
    
    public static double VectorESp(this IPair<double> v1, IPair<double> v2)
    {
        return v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2;
    }

    /// <summary>
    /// The Euclidean squared length of this vector when it represents a vector
    /// </summary>
    public static double VectorENormSquared(this IPair<double> vector)
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

    public static Complex VectorToComplexNumber(this IPair<double> vector)
    {
        return new Complex(
            vector.Item1,
            vector.Item2
        );
    }

    public static Complex VectorToUnitComplexNumber(this IPair<double> vector)
    {
        var norm = vector.VectorENorm();

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
    
    public static IntegerSign VectorCrossSign(this IPair<double> v1, IPair<double> v2)
    {
        return (v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1).Sign();
    }

}