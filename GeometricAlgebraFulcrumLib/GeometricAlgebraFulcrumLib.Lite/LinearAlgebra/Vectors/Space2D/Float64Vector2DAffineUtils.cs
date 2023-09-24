using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

public static class Float64Vector2DAffineUtils
{
    
    /// <summary>
    /// Returns a new vector orthogonal to this one.
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetNormal(this IFloat64Vector2D vector)
    {
        return Float64Vector2D.Create(-vector.Y, vector.X);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetUnitNormal(this IFloat64Vector2D vector)
    {
        var normInv = 1d / vector.ENorm();

        return Float64Vector2D.Create(
            -vector.Y * normInv,
            vector.X * normInv
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Determinant(this IFloat64Vector2D v1, IFloat64Vector2D v2)
    {
        return v1.X * v2.Y - v1.Y * v2.X;
    }

    /// <summary>
    /// The Euclidean distance between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetDistanceToPoint(this IFloat64Vector2D v1, IFloat64Vector2D v2)
    {
        var vX = v2.X - v1.X;
        var vY = v2.Y - v1.Y;

        return Math.Sqrt(vX * vX + vY * vY);
    }

    /// <summary>
    /// The Euclidean distance between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetDistanceSquaredToPoint(this IFloat64Vector2D v1, IFloat64Vector2D v2)
    {
        var vX = v2.X - v1.X;
        var vY = v2.Y - v1.Y;

        return vX * vX + vY * vY;
    }

    /// <summary>
    /// The Euclidean distance between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2X"></param>
    /// <param name="v2Y"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetDistanceSquaredToPoint(this IFloat64Vector2D v1, double v2X, double v2Y)
    {
        var vX = v2X - v1.X;
        var vY = v2Y - v1.Y;

        return vX * vX + vY * vY;
    }

    /// <summary>
    /// The Euclidean distance between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2X"></param>
    /// <param name="v2Y"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetDistanceToPoint(this IFloat64Vector2D v1, double v2X, double v2Y)
    {
        var vX = v2X - v1.X;
        var vY = v2Y - v1.Y;

        return Math.Sqrt(vX * vX + vY * vY);
    }
    
    public static Float64Vector2D GetCenterOfMassPoint(this IEnumerable<IFloat64Vector2D> pointsList)
    {
        var centerX = 0.0d;
        var centerY = 0.0d;

        var pointsCount = 0;
        foreach (var point in pointsList)
        {
            centerX += point.X;
            centerY += point.Y;

            pointsCount++;
        }

        var pointsCountInv = 1.0d / pointsCount;

        return Float64Vector2D.Create(
            centerX * pointsCountInv,
            centerY * pointsCountInv
        );
    }

    public static Float64Vector2D GetCenterPoint(this IEnumerable<IFloat64Vector2D> pointsList)
    {
        var minX = double.PositiveInfinity;
        var minY = double.PositiveInfinity;

        var maxX = double.NegativeInfinity;
        var maxY = double.NegativeInfinity;

        foreach (var point in pointsList)
        {
            if (point.X < minX) minX = point.X;
            if (point.X > maxX) maxX = point.X;

            if (point.Y < minY) minY = point.Y;
            if (point.Y > maxY) maxY = point.Y;
        }

        return Float64Vector2D.Create(
            0.5 * (minX + maxX),
            0.5 * (minY + maxY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<double> GetDistancesToPoints(this IFloat64Vector2D point, IEnumerable<IFloat64Vector2D> pointsList)
    {
        return pointsList.Select(point.GetDistanceToPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<double> GetDistancesSquaredToPoints(this IFloat64Vector2D point, IEnumerable<IFloat64Vector2D> pointsList)
    {
        return pointsList.Select(point.GetDistanceSquaredToPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetDirectionTo(this IFloat64Vector2D p1, IFloat64Vector2D p2)
    {
        return Float64Vector2D.Create(
            p2.X - p1.X,
            p2.Y - p1.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetDirectionTo(this IFloat64Vector2D p1, double p2X, double p2Y)
    {
        return Float64Vector2D.Create(
            p2X - p1.X,
            p2Y - p1.Y
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetDirectionFrom(this IFloat64Vector2D p2, double p1X, double p1Y)
    {
        return Float64Vector2D.Create(
            p2.X - p1X,
            p2.Y - p1Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetUnitDirectionTo(this IFloat64Vector2D p1, IFloat64Vector2D p2)
    {
        var dx = p2.X - p1.X;
        var dy = p2.Y - p1.Y;
        var dInv = (dx * dx + dy * dy).Sqrt().Inverse();

        return Float64Vector2D.Create(dx * dInv, dy * dInv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetUnitDirectionFrom(this IFloat64Vector2D p2, IFloat64Vector2D p1)
    {
        var dx = p2.X - p1.X;
        var dy = p2.Y - p1.Y;
        var dInv = 1 / Math.Sqrt(dx * dx + dy * dy);

        return Float64Vector2D.Create(dx * dInv, dy * dInv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetPointInDirection(this IFloat64Vector2D p, IFloat64Vector2D v)
    {
        return Float64Vector2D.Create(
            p.X + v.X,
            p.Y + v.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetPointInDirection(this IFloat64Vector2D p, IFloat64Vector2D v, double t)
    {
        return Float64Vector2D.Create(
            p.X + t * v.X,
            p.Y + t * v.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetDirectionFrom(this IFloat64Vector2D p2, IFloat64Vector2D p1)
    {
        return Float64Vector2D.Create(
            p2.X - p1.X,
            p2.Y - p1.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Lerp(this double t, IFloat64Vector2D v1, IFloat64Vector2D v2)
    {
        var s = 1.0d - t;

        return Float64Vector2D.Create(
            s * v1.X + t * v2.X,
            s * v1.Y + t * v2.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Float64Vector2D> Lerp(this IEnumerable<double> tList, IFloat64Vector2D v1, IFloat64Vector2D v2)
    {
        return tList.Select(t => t.Lerp(v1, v2));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D TranslateBy(this IFloat64Vector2D vector, double translationX, double translationY)
    {
        return Float64Vector2D.Create(
            translationX + vector.X,
            translationY + vector.Y
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D TranslateBy(this IFloat64Vector2D vector, IFloat64Vector2D translationVector)
    {
        return Float64Vector2D.Create(
            translationVector.X + vector.X,
            translationVector.Y + vector.Y
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ScaleBy(this IFloat64Vector2D vector, double scaleFactor)
    {
        return Float64Vector2D.Create(
            scaleFactor * vector.X,
            scaleFactor * vector.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ScaleBy(this IFloat64Vector2D vector, double scaleFactorX, double scaleFactorY)
    {
        return Float64Vector2D.Create(
            scaleFactorX * vector.X,
            scaleFactorY * vector.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ScaleBy(this IFloat64Vector2D vector, IFloat64Vector2D scaleFactorVector)
    {
        return Float64Vector2D.Create(
            scaleFactorVector.X * vector.X,
            scaleFactorVector.Y * vector.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D RotateBy(this IFloat64Vector2D vector, Float64PlanarAngle angle)
    {
        return angle.Rotate(vector);
    }
    
    public static Float64Vector2D RotateToUnitVector(this IFloat64Vector2D vector1, IFloat64Vector2D unitVector, Float64PlanarAngle angle)
    {
        Debug.Assert(
            vector1.IsNearUnit() &&
            unitVector.IsNearUnit()
        );

        // Create a unit normal to u in the u-v rotational plane
        var v1Dot2 = unitVector.ESp(vector1);

        var v1 = v1Dot2.Abs().IsNearOne()
            ? vector1.GetNormal()
            : unitVector.Subtract(vector1.Times(v1Dot2));
        
        var v1Length = v1.ENorm();

        Debug.Assert(
            v1.ESp(vector1).IsNearZero() &&
            !v1Length.IsNearZero()
        );
        
        // Compute a rotated version of v in the u-v rotational plane by the given angle
        return vector1
            .Times(angle.Cos())
            .Add(v1.Times(angle.Sin() / v1Length));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ReflectVectorOnVector(this IFloat64Vector2D reflectionVector, IFloat64Vector2D vector)
    {
        var s = 
            2 * reflectionVector.ESp(vector) / 
            reflectionVector.ENormSquared();

        return Float64Vector2D.Create(
            vector.X - s * reflectionVector.X,
            vector.Y - s * reflectionVector.Y
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Float64Scalar> AnalyzeOnVectors(this IFloat64Vector2D v, IFloat64Vector2D u1, IFloat64Vector2D u2)
    {
        var s1 = 
            (v.X * u1.X + v.Y * u1.Y) /
            (u1.X * u1.X + u1.Y * u1.Y).Sqrt();

        var s2 = 
            (v.X * u2.X + v.Y * u2.Y) /
            (u2.X * u2.X + u2.Y * u2.Y).Sqrt();

        return new Pair<Float64Scalar>(s1, s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ProjectOnVector(this IFloat64Vector2D v, IFloat64Vector2D u)
    {
        var s1 = v.X * u.X + v.Y * u.Y;
        var s2 = u.X * u.X + u.Y * u.Y;
        var s = s1 / s2;

        return Float64Vector2D.Create(u.X * s, u.Y * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ProjectOnUnitVector(this IFloat64Vector2D v, IFloat64Vector2D u)
    {
        var s = v.X * u.X + v.Y * u.Y;

        return Float64Vector2D.Create(u.X * s, u.Y * s);
    }

    /// <summary>
    /// Returns a copy of this vector if its dot product with the other vector is positive, else
    /// it returns the vector's negative
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="directionVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D FaceDirection(this IFloat64Vector2D vector, IFloat64Vector2D directionVector)
    {
        Debug.Assert(!directionVector.IsZero());

        return
            (vector.X * directionVector.X + vector.Y * directionVector.Y).IsNegative()
                ? Float64Vector2D.Create(-vector.X, -vector.Y)
                : Float64Vector2D.Create(vector.X, vector.Y);
    }

    public static IEnumerable<Float64Vector2D> ReflectVectorsOnVector(this IFloat64Vector2D reflectionVector, params IFloat64Vector2D[] vectorsList)
    {
        var s = 2 / reflectionVector.ENormSquared();

        foreach (var vector in vectorsList)
        {
            var s1 = s * reflectionVector.ESp(vector);

            yield return Float64Vector2D.Create(
                vector.X - s1 * reflectionVector.X,
                vector.Y - s1 * reflectionVector.Y
            );
        }
    }

    public static IEnumerable<Float64Vector2D> ReflectVectorsOnVector(this IFloat64Vector2D reflectionVector, IEnumerable<IFloat64Vector2D> vectorsList)
    {
        var s = 2 / reflectionVector.ENormSquared();

        foreach (var vector in vectorsList)
        {
            var s1 = s * reflectionVector.ESp(vector);

            yield return Float64Vector2D.Create(
                vector.X - s1 * reflectionVector.X,
                vector.Y - s1 * reflectionVector.Y
            );
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ReflectVectorOnUnitVector(this IFloat64Vector2D reflectionVector, IFloat64Vector2D vector)
    {
        var s = 2 * reflectionVector.ESp(vector);

        return Float64Vector2D.Create(
            vector.X - s * reflectionVector.X,
            vector.Y - s * reflectionVector.Y
        );
    }

    public static IEnumerable<Float64Vector2D> ReflectVectorsOnUnitVector(this IFloat64Vector2D reflectionVector, params IFloat64Vector2D[] vectorsList)
    {
        foreach (var vector in vectorsList)
        {
            var s1 = 2 * reflectionVector.ESp(vector);

            yield return Float64Vector2D.Create(
                vector.X - s1 * reflectionVector.X,
                vector.Y - s1 * reflectionVector.Y
            );
        }
    }

    public static IEnumerable<Float64Vector2D> ReflectVectorsOnUnitVector(this IFloat64Vector2D reflectionVector, IEnumerable<IFloat64Vector2D> vectorsList)
    {
        foreach (var vector in vectorsList)
        {
            var s1 = 2 * reflectionVector.ESp(vector);

            yield return Float64Vector2D.Create(
                vector.X - s1 * reflectionVector.X,
                vector.Y - s1 * reflectionVector.Y
            );
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D SetLength(this IFloat64Vector2D vector, double newLength)
    {
        var oldLength = vector.ENorm();

        if (oldLength.IsZero())
            return Float64Vector2D.Zero;

        var scalingFactor = newLength / oldLength;

        return Float64Vector2D.Create(
            vector.X * scalingFactor,
            vector.Y * scalingFactor
        );
    }
}