﻿using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

public static class Float64Vector2DUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ClampTo(this IFloat64Vector2D tuple, IFloat64Vector2D maxTuple)
    {
        return Float64Vector2D.Create(
            tuple.X.ClampTo(maxTuple.X),
            tuple.Y.ClampTo(maxTuple.Y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ClampTo(this IFloat64Vector2D tuple, IFloat64Vector2D minTuple, IFloat64Vector2D maxTuple)
    {
        return Float64Vector2D.Create(
            tuple.X.ClampTo(minTuple.X, maxTuple.X),
            tuple.Y.ClampTo(minTuple.Y, maxTuple.Y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ClampToSymmetric(this IFloat64Vector2D tuple, IFloat64Vector2D maxTuple)
    {
        return Float64Vector2D.Create(
            tuple.X.ClampToSymmetric(maxTuple.X),
            tuple.Y.ClampToSymmetric(maxTuple.Y)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(this IFloat64Vector2D vector)
    {
        return vector.ENorm().IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZero(this IFloat64Vector2D vector, double epsilon = 1e-12)
    {
        return vector.ENorm().IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearUnit(this IFloat64Vector2D vector, double epsilon = 1e-12)
    {
        return vector.ENormSquared().IsNearOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthonormalWith(this IFloat64Vector2D vector1, IFloat64Vector2D vector2, double epsilon = 1e-12)
    {
        return vector1.IsNearUnit(epsilon) &&
               vector2.IsNearUnit(epsilon) &&
               vector1.ESp(vector2).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthonormalWithUnit(this IFloat64Vector2D vector1, IFloat64Vector2D vector2, double epsilon = 1e-12)
    {
        Debug.Assert(
            vector2.IsNearUnit(epsilon)
        );

        return vector1.IsNearUnit(epsilon) &&
               vector1.ESp(vector2).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelTo(this IFloat64Vector2D vector1, IFloat64Vector2D vector2, double epsilon = 1e-12)
    {
        return vector1.GetAngleCos(vector2).Abs().IsNearOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOppositeTo(this IFloat64Vector2D vector1, IFloat64Vector2D vector2, double epsilon = 1e-12)
    {
        return vector1.GetAngleCos(vector2).IsNearMinusOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelToUnit(this IFloat64Vector2D vector1, IFloat64Vector2D vector2, double epsilon = 1e-12)
    {
        return vector1.GetAngleCosWithUnit(vector2).Abs().IsNearOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOppositeToUnit(this IFloat64Vector2D vector1, IFloat64Vector2D vector2, double epsilon = 1e-12)
    {
        return vector1.GetAngleCosWithUnit(vector2).IsNearNegativeOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthogonalTo(this IFloat64Vector2D vector1, IFloat64Vector2D vector2, double epsilon = 1e-12)
    {
        return vector1.ESp(vector2).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVectorBasis(this IFloat64Vector2D vector, int basisIndex)
    {
        return basisIndex switch
        {
            0 => vector.X.IsOne() && vector.Y.IsZero(),
            1 => vector.X.IsZero() && vector.Y.IsOne(),
            _ => false
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearVectorBasis(this IFloat64Vector2D vector, int basisIndex, double epsilon = 1e-12d)
    {
        var vector2 = basisIndex switch
        {
            0 => Float64Vector2D.E1,
            1 => Float64Vector2D.E2,
            _ => throw new InvalidOperationException()
        };

        return (vector - vector2).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFinite(this IFloat64Vector2D tuple)
    {
        return !(
            double.IsNaN(tuple.X) || double.IsInfinity(tuple.X) ||
            double.IsNaN(tuple.Y) || double.IsInfinity(tuple.Y)
        );
    }

    
    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector is near 
    /// zero it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Negative(this IFloat64Vector2D vector)
    {
        return Float64Vector2D.Create(-vector.X, -vector.Y);
    }
    
    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector is near 
    /// zero it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D NegativeUnitVector(this IFloat64Vector2D vector)
    {
        var s = vector.ENorm();
        if (s.IsAlmostZero())
            return vector.ToVector2D();

        s = -1.0d / s;
        return Float64Vector2D.Create(vector.X * s, vector.Y * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Add(this IFloat64Vector2D v1, IFloat64Vector2D v2)
    {
        return Float64Vector2D.Create(v1.X + v2.X,
            v1.Y + v2.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Subtract(this IFloat64Vector2D v1, IFloat64Vector2D v2)
    {
        return Float64Vector2D.Create(v1.X - v2.X,
            v1.Y - v2.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Times(this IFloat64Vector2D v1, double v2)
    {
        return Float64Vector2D.Create(v1.X * v2,
            v1.Y * v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Times(this double v1, IFloat64Vector2D v2)
    {
        return Float64Vector2D.Create(v1 * v2.X,
            v1 * v2.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Divide(this IFloat64Vector2D v1, double v2)
    {
        v2 = 1d / v2;

        return Float64Vector2D.Create(v1.X * v2,
            v1.Y * v2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D DivideByNorm(this IFloat64Vector2D vector)
    {
        var norm = vector.ENorm();

        return norm.IsZero() 
            ? vector.ToVector2D() 
            : vector.Divide(norm);
    }
    
    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUnitVector(this IFloat64Vector2D vector)
    {
        return vector
            .ENormSquared()
            .IsOne();
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearUnitVector(this IFloat64Vector2D vector, double epsilon = 1e-12)
    {
        return vector
            .ENormSquared()
            .IsNearEqual(1.0d, epsilon);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<double, Float64Vector2D> ToLengthAndUnitDirection(this IFloat64Vector2D vector)
    {
        var length = Math.Sqrt(
            vector.X * vector.X +
            vector.Y * vector.Y
        );

        var lengthInv = 1 / length;

        return Tuple.Create(
            length,
            Float64Vector2D.Create(vector.X * lengthInv,
                vector.Y * lengthInv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex ESp(this IComplexVector2D v1, IComplexVector2D v2)
    {
        return v1.X * v2.X.Conjugate() +
               v1.Y * v2.Y.Conjugate();
    }

    /// <summary>
    /// The absolute value of the Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar ESpAbs(this IFloat64Vector2D v1, IFloat64Vector2D v2)
    {
        return Math.Abs(v1.X * v2.X + v1.Y * v2.Y);
    }

    /// <summary>
    /// The Euclidean length of this tuple when it represents a vector
    /// </summary>
    public static double ENorm(this IFloat64Vector2D vector)
    {
        return Math.Sqrt(
            vector.X * vector.X +
            vector.Y * vector.Y
        );
    }

    public static double ENorm(double vectorX, double vectorY)
    {
        return Math.Sqrt(
            vectorX * vectorX +
            vectorY * vectorY
        );
    }

    public static double ENorm(this IComplexVector2D vector)
    {
        return Math.Sqrt(
            (vector.X * vector.X.Conjugate()).Real +
            (vector.Y * vector.Y.Conjugate()).Real
        );
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar ESp(this IFloat64Vector2D v1, IFloat64Vector2D v2)
    {
        return v1.X * v2.X + v1.Y * v2.Y;
    }

    /// <summary>
    /// The Euclidean squared length of this tuple when it represents a vector
    /// </summary>
    public static Float64Scalar ENormSquared(this IFloat64Vector2D vector)
    {
        return vector.X * vector.X +
               vector.Y * vector.Y;
    }

    public static double ENormSquared(double vectorX, double vectorY)
    {
        return vectorX * vectorX +
               vectorY * vectorY;
    }

    public static double ENormSquared(this IComplexVector2D vector)
    {
        return (vector.X * vector.X.Conjugate()).Real +
               (vector.Y * vector.Y.Conjugate()).Real;
    }

}