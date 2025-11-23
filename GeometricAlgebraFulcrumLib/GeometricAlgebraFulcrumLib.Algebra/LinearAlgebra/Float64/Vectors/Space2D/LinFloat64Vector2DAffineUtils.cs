using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

public static class LinFloat64Vector2DAffineUtils
{

    /// <summary>
    /// Returns a new vector orthogonal to this one.
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetNormal(this LinFloat64Vector2D vector)
    {
        return LinFloat64Vector2D.Create(-vector.Item2, vector.Item1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetUnitNormal(this LinFloat64Vector2D vector)
    {
        var normInv = 1d / vector.L2Norm();

        return LinFloat64Vector2D.Create(
            -vector.Item2 * normInv,
            vector.Item1 * normInv
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Determinant(this LinFloat64Vector2D v1, LinFloat64Vector2D v2)
    {
        return v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1;
    }

    /// <summary>
    /// The Euclidean distance between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetDistanceToPoint(this LinFloat64Vector2D v1, LinFloat64Vector2D v2)
    {
        var vX = v2.Item1 - v1.Item1;
        var vY = v2.Item2 - v1.Item2;

        return Math.Sqrt(vX * vX + vY * vY);
    }

    /// <summary>
    /// The Euclidean distance between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetDistanceSquaredToPoint(this LinFloat64Vector2D v1, LinFloat64Vector2D v2)
    {
        var vX = v2.Item1 - v1.Item1;
        var vY = v2.Item2 - v1.Item2;

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
    public static double GetDistanceSquaredToPoint(this LinFloat64Vector2D v1, double v2X, double v2Y)
    {
        var vX = v2X - v1.Item1;
        var vY = v2Y - v1.Item2;

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
    public static double GetDistanceToPoint(this LinFloat64Vector2D v1, double v2X, double v2Y)
    {
        var vX = v2X - v1.Item1;
        var vY = v2Y - v1.Item2;

        return Math.Sqrt(vX * vX + vY * vY);
    }

    public static LinFloat64Vector2D GetCenterOfMassPoint(this IEnumerable<LinFloat64Vector2D> pointsList)
    {
        var centerX = 0.0d;
        var centerY = 0.0d;

        var pointsCount = 0;
        foreach (var point in pointsList)
        {
            centerX += point.Item1;
            centerY += point.Item2;

            pointsCount++;
        }

        var pointsCountInv = 1.0d / pointsCount;

        return LinFloat64Vector2D.Create(
            centerX * pointsCountInv,
            centerY * pointsCountInv
        );
    }

    public static LinFloat64Vector2D GetCenterPoint(this IEnumerable<LinFloat64Vector2D> pointsList)
    {
        var minX = double.PositiveInfinity;
        var minY = double.PositiveInfinity;

        var maxX = double.NegativeInfinity;
        var maxY = double.NegativeInfinity;

        foreach (var point in pointsList)
        {
            if (point.Item1 < minX) minX = point.Item1;
            if (point.Item1 > maxX) maxX = point.Item1;

            if (point.Item2 < minY) minY = point.Item2;
            if (point.Item2 > maxY) maxY = point.Item2;
        }

        return LinFloat64Vector2D.Create(
            0.5 * (minX + maxX),
            0.5 * (minY + maxY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<double> GetDistancesToPoints(this LinFloat64Vector2D point, IEnumerable<LinFloat64Vector2D> pointsList)
    {
        return pointsList.Select(p => point.GetDistanceToPoint(p));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<double> GetDistancesSquaredToPoints(this LinFloat64Vector2D point, IEnumerable<LinFloat64Vector2D> pointsList)
    {
        return pointsList.Select(p => point.GetDistanceSquaredToPoint(p));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetDirectionTo(this LinFloat64Vector2D p1, LinFloat64Vector2D p2)
    {
        return LinFloat64Vector2D.Create(
            p2.Item1 - p1.Item1,
            p2.Item2 - p1.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetDirectionTo(this LinFloat64Vector2D p1, double p2X, double p2Y)
    {
        return LinFloat64Vector2D.Create(
            p2X - p1.Item1,
            p2Y - p1.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetDirectionFrom(this LinFloat64Vector2D p2, double p1X, double p1Y)
    {
        return LinFloat64Vector2D.Create(
            p2.Item1 - p1X,
            p2.Item2 - p1Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetUnitDirectionTo(this LinFloat64Vector2D p1, LinFloat64Vector2D p2)
    {
        var dx = p2.Item1 - p1.Item1;
        var dy = p2.Item2 - p1.Item2;
        var dInv = (dx * dx + dy * dy).Sqrt().Inverse();

        return LinFloat64Vector2D.Create(dx * dInv, dy * dInv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetUnitDirectionFrom(this LinFloat64Vector2D p2, LinFloat64Vector2D p1)
    {
        var dx = p2.Item1 - p1.Item1;
        var dy = p2.Item2 - p1.Item2;
        var dInv = 1 / Math.Sqrt(dx * dx + dy * dy);

        return LinFloat64Vector2D.Create(dx * dInv, dy * dInv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetPointInDirection(this LinFloat64Vector2D p, LinFloat64Vector2D v)
    {
        return LinFloat64Vector2D.Create(
            p.Item1 + v.Item1,
            p.Item2 + v.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetPointInDirection(this LinFloat64Vector2D p, LinFloat64Vector2D v, double t)
    {
        return LinFloat64Vector2D.Create(
            p.Item1 + t * v.Item1,
            p.Item2 + t * v.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetDirectionFrom(this LinFloat64Vector2D p2, LinFloat64Vector2D p1)
    {
        return LinFloat64Vector2D.Create(
            p2.Item1 - p1.Item1,
            p2.Item2 - p1.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D Lerp(this double t, LinFloat64Vector2D v1, LinFloat64Vector2D v2)
    {
        var s = 1.0d - t;

        return LinFloat64Vector2D.Create(
            s * v1.Item1 + t * v2.Item1,
            s * v1.Item2 + t * v2.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinFloat64Vector2D> Lerp(this IEnumerable<double> tList, LinFloat64Vector2D v1, LinFloat64Vector2D v2)
    {
        return tList.Select(t => t.Lerp(v1, v2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D TranslateBy(this LinFloat64Vector2D vector, double translationX, double translationY)
    {
        return LinFloat64Vector2D.Create(
            translationX + vector.Item1,
            translationY + vector.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D TranslateBy(this LinFloat64Vector2D vector, LinFloat64Vector2D translationVector)
    {
        return LinFloat64Vector2D.Create(
            translationVector.Item1 + vector.Item1,
            translationVector.Item2 + vector.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D ScaleBy(this LinFloat64Vector2D vector, double scaleFactor)
    {
        return LinFloat64Vector2D.Create(
            scaleFactor * vector.Item1,
            scaleFactor * vector.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D ScaleBy(this LinFloat64Vector2D vector, double scaleFactorX, double scaleFactorY)
    {
        return LinFloat64Vector2D.Create(
            scaleFactorX * vector.Item1,
            scaleFactorY * vector.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D ScaleBy(this LinFloat64Vector2D vector, LinFloat64Vector2D scaleFactorVector)
    {
        return LinFloat64Vector2D.Create(
            scaleFactorVector.Item1 * vector.Item1,
            scaleFactorVector.Item2 * vector.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D RotateBy(this LinFloat64Vector2D vector, LinFloat64Angle angle)
    {
        return angle.Rotate(vector);
    }

    public static LinFloat64Vector2D RotateToUnitVector(this LinFloat64Vector2D vector1, LinFloat64Vector2D unitVector, LinFloat64Angle angle)
    {
        Debug.Assert(
            vector1.IsNearUnit() &&
            unitVector.IsNearUnit()
        );

        // Create a unit normal to u in the u-v rotational plane
        var v1Dot2 = unitVector.VectorDot(vector1);

        var v1 = v1Dot2.Abs().IsNearOne()
            ? vector1.GetNormal()
            : unitVector.VectorSubtract(vector1.VectorTimes(v1Dot2));

        var v1Length = LinFloat64Vector2DUtils.L2Norm(v1);

        Debug.Assert(
            v1.VectorDot(vector1).IsNearZero() &&
            !v1Length.IsNearZero()
        );

        // Compute a rotated version of v in the u-v rotational plane by the given angle
        return vector1
            .VectorTimes(angle.Cos())
            .VectorAdd(v1.VectorTimes(angle.Sin() / v1Length));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D ReflectVectorOnVector(this LinFloat64Vector2D reflectionVector, LinFloat64Vector2D vector)
    {
        var s =
            2 * reflectionVector.VectorDot(vector) /
            reflectionVector.L2NormSquared();

        return LinFloat64Vector2D.Create(
            vector.Item1 - s * reflectionVector.Item1,
            vector.Item2 - s * reflectionVector.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D AnalyzeOnVectors(this LinFloat64Vector2D v, LinFloat64Vector2D u1, LinFloat64Vector2D u2)
    {
        var s1 =
            (v.Item1 * u1.Item1 + v.Item2 * u1.Item2) /
            (u1.Item1 * u1.Item1 + u1.Item2 * u1.Item2).Sqrt();

        var s2 =
            (v.Item1 * u2.Item1 + v.Item2 * u2.Item2) /
            (u2.Item1 * u2.Item1 + u2.Item2 * u2.Item2).Sqrt();

        return LinFloat64Vector2D.Create(s1, s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D ProjectOnVector(this LinFloat64Vector2D v, LinFloat64Vector2D u)
    {
        var s1 = v.Item1 * u.Item1 + v.Item2 * u.Item2;
        var s2 = u.Item1 * u.Item1 + u.Item2 * u.Item2;
        var s = s1 / s2;

        return LinFloat64Vector2D.Create(u.Item1 * s, u.Item2 * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D ProjectOnUnitVector(this LinFloat64Vector2D v, LinFloat64Vector2D u)
    {
        var s = v.Item1 * u.Item1 + v.Item2 * u.Item2;

        return LinFloat64Vector2D.Create(u.Item1 * s, u.Item2 * s);
    }

    /// <summary>
    /// Returns a copy of this vector if its dot product with the other vector is positive, else
    /// it returns the vector's negative
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="directionVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D FaceDirection(this LinFloat64Vector2D vector, LinFloat64Vector2D directionVector)
    {
        Debug.Assert(!directionVector.IsZero());

        return
            (vector.Item1 * directionVector.Item1 + vector.Item2 * directionVector.Item2).IsNegative()
                ? LinFloat64Vector2D.Create(-vector.Item1, -vector.Item2)
                : LinFloat64Vector2D.Create(vector.Item1, vector.Item2);
    }

    public static IEnumerable<LinFloat64Vector2D> ReflectVectorsOnVector(this LinFloat64Vector2D reflectionVector, params LinFloat64Vector2D[] vectorsList)
    {
        var s = 2 / reflectionVector.L2NormSquared();

        foreach (var vector in vectorsList)
        {
            var s1 = s * reflectionVector.VectorDot(vector);

            yield return LinFloat64Vector2D.Create(
                vector.Item1 - s1 * reflectionVector.Item1,
                vector.Item2 - s1 * reflectionVector.Item2
            );
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D ReflectOnX(this LinFloat64Vector2D vector)
    {
        return LinFloat64Vector2D.Create(vector.X, -vector.Y);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D ReflectOnY(this LinFloat64Vector2D vector)
    {
        return LinFloat64Vector2D.Create(-vector.X, vector.Y);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D ReflectOnXy(this LinFloat64Vector2D vector)
    {
        return LinFloat64Vector2D.Create(-vector.X, -vector.Y);
    }

    public static IEnumerable<LinFloat64Vector2D> ReflectVectorsOnVector(this LinFloat64Vector2D reflectionVector, IEnumerable<LinFloat64Vector2D> vectorsList)
    {
        var s = 2 / reflectionVector.L2NormSquared();

        foreach (var vector in vectorsList)
        {
            var s1 = s * reflectionVector.VectorDot(vector);

            yield return LinFloat64Vector2D.Create(
                vector.Item1 - s1 * reflectionVector.Item1,
                vector.Item2 - s1 * reflectionVector.Item2
            );
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D ReflectVectorOnUnitVector(this LinFloat64Vector2D reflectionVector, LinFloat64Vector2D vector)
    {
        var s = 2 * reflectionVector.VectorDot(vector);

        return LinFloat64Vector2D.Create(
            vector.Item1 - s * reflectionVector.Item1,
            vector.Item2 - s * reflectionVector.Item2
        );
    }

    public static IEnumerable<LinFloat64Vector2D> ReflectVectorsOnUnitVector(this LinFloat64Vector2D reflectionVector, params LinFloat64Vector2D[] vectorsList)
    {
        foreach (var vector in vectorsList)
        {
            var s1 = 2 * reflectionVector.VectorDot(vector);

            yield return LinFloat64Vector2D.Create(
                vector.Item1 - s1 * reflectionVector.Item1,
                vector.Item2 - s1 * reflectionVector.Item2
            );
        }
    }

    public static IEnumerable<LinFloat64Vector2D> ReflectVectorsOnUnitVector(this LinFloat64Vector2D reflectionVector, IEnumerable<LinFloat64Vector2D> vectorsList)
    {
        foreach (var vector in vectorsList)
        {
            var s1 = 2 * reflectionVector.VectorDot(vector);

            yield return LinFloat64Vector2D.Create(
                vector.Item1 - s1 * reflectionVector.Item1,
                vector.Item2 - s1 * reflectionVector.Item2
            );
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D SetLength(this LinFloat64Vector2D vector, double newLength)
    {
        var oldLength = vector.L2Norm();

        if (oldLength.IsZero())
            return LinFloat64Vector2D.Zero;

        var scalingFactor = newLength / oldLength;

        return LinFloat64Vector2D.Create(
            vector.Item1 * scalingFactor,
            vector.Item2 * scalingFactor
        );
    }
    
    
    /// <summary>
    /// Circumcenter for a triangle
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public static LinFloat64Vector2D GetCircumcenter(this LinFloat64Vector2D a, LinFloat64Vector2D b, LinFloat64Vector2D c)
    {
        // Using perpendicular bisector intersection
        var d = 2 * (a.Item1 * (b.Item2 - c.Item2) + b.Item1 * (c.Item2 - a.Item2) + c.Item1 * (a.Item2 - b.Item2));
        
        if (d.IsNearZero(1e-18))
            return LinFloat64Vector2D.Create(
                double.PositiveInfinity, 
                double.PositiveInfinity
            );
        
        var a2 = a.Item1 * a.Item1 + a.Item2 * a.Item2;
        var b2 = b.Item1 * b.Item1 + b.Item2 * b.Item2;
        var c2 = c.Item1 * c.Item1 + c.Item2 * c.Item2;
        
        var ux = (a2 * (b.Item2 - c.Item2) + b2 * (c.Item2 - a.Item2) + c2 * (a.Item2 - b.Item2)) / d;
        var uy = (a2 * (c.Item1 - b.Item1) + b2 * (a.Item1 - c.Item1) + c2 * (b.Item1 - a.Item1)) / d;
        
        return LinFloat64Vector2D.Create(ux, uy);
    }

}