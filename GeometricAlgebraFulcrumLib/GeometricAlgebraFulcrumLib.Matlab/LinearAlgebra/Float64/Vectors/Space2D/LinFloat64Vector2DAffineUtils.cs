﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;

public static class LinFloat64Vector2DAffineUtils
{

    /// <summary>
    /// Returns a new vector orthogonal to this one.
    /// </summary>
    /// <returns></returns>
    
    public static LinFloat64Vector2D GetNormal(this IPair<double> vector)
    {
        return LinFloat64Vector2D.Create(-vector.Item2, vector.Item1);
    }

    
    public static LinFloat64Vector2D GetUnitNormal(this IPair<double> vector)
    {
        var normInv = 1d / vector.VectorENorm();

        return LinFloat64Vector2D.Create(
            -vector.Item2 * normInv,
            vector.Item1 * normInv
        );
    }

    
    public static double Determinant(this IPair<double> v1, IPair<double> v2)
    {
        return v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1;
    }

    /// <summary>
    /// The Euclidean distance between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static double GetDistanceToPoint(this IPair<double> v1, IPair<double> v2)
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
    
    public static double GetDistanceSquaredToPoint(this IPair<double> v1, IPair<double> v2)
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
    
    public static double GetDistanceSquaredToPoint(this IPair<double> v1, double v2X, double v2Y)
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
    
    public static double GetDistanceToPoint(this IPair<double> v1, double v2X, double v2Y)
    {
        var vX = v2X - v1.Item1;
        var vY = v2Y - v1.Item2;

        return Math.Sqrt(vX * vX + vY * vY);
    }

    public static LinFloat64Vector2D GetCenterOfMassPoint(this IEnumerable<IPair<double>> pointsList)
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

    public static LinFloat64Vector2D GetCenterPoint(this IEnumerable<IPair<double>> pointsList)
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

    
    public static IEnumerable<double> GetDistancesToPoints(this IPair<double> point, IEnumerable<IPair<double>> pointsList)
    {
        return pointsList.Select(point.GetDistanceToPoint);
    }

    
    public static IEnumerable<double> GetDistancesSquaredToPoints(this IPair<double> point, IEnumerable<IPair<double>> pointsList)
    {
        return pointsList.Select(point.GetDistanceSquaredToPoint);
    }

    
    public static LinFloat64Vector2D GetDirectionTo(this IPair<double> p1, IPair<double> p2)
    {
        return LinFloat64Vector2D.Create(
            p2.Item1 - p1.Item1,
            p2.Item2 - p1.Item2
        );
    }

    
    public static LinFloat64Vector2D GetDirectionTo(this IPair<double> p1, double p2X, double p2Y)
    {
        return LinFloat64Vector2D.Create(
            p2X - p1.Item1,
            p2Y - p1.Item2
        );
    }

    
    public static LinFloat64Vector2D GetDirectionFrom(this IPair<double> p2, double p1X, double p1Y)
    {
        return LinFloat64Vector2D.Create(
            p2.Item1 - p1X,
            p2.Item2 - p1Y
        );
    }

    
    public static LinFloat64Vector2D GetUnitDirectionTo(this IPair<double> p1, IPair<double> p2)
    {
        var dx = p2.Item1 - p1.Item1;
        var dy = p2.Item2 - p1.Item2;
        var dInv = (dx * dx + dy * dy).Sqrt().Inverse();

        return LinFloat64Vector2D.Create(dx * dInv, dy * dInv);
    }

    
    public static LinFloat64Vector2D GetUnitDirectionFrom(this IPair<double> p2, IPair<double> p1)
    {
        var dx = p2.Item1 - p1.Item1;
        var dy = p2.Item2 - p1.Item2;
        var dInv = 1 / Math.Sqrt(dx * dx + dy * dy);

        return LinFloat64Vector2D.Create(dx * dInv, dy * dInv);
    }

    
    public static LinFloat64Vector2D GetPointInDirection(this IPair<double> p, IPair<double> v)
    {
        return LinFloat64Vector2D.Create(
            p.Item1 + v.Item1,
            p.Item2 + v.Item2
        );
    }

    
    public static LinFloat64Vector2D GetPointInDirection(this IPair<double> p, IPair<double> v, double t)
    {
        return LinFloat64Vector2D.Create(
            p.Item1 + t * v.Item1,
            p.Item2 + t * v.Item2
        );
    }

    
    public static LinFloat64Vector2D GetDirectionFrom(this IPair<double> p2, IPair<double> p1)
    {
        return LinFloat64Vector2D.Create(
            p2.Item1 - p1.Item1,
            p2.Item2 - p1.Item2
        );
    }

    
    public static LinFloat64Vector2D Lerp(this double t, IPair<double> v1, IPair<double> v2)
    {
        var s = 1.0d - t;

        return LinFloat64Vector2D.Create(
            s * v1.Item1 + t * v2.Item1,
            s * v1.Item2 + t * v2.Item2
        );
    }

    
    public static IEnumerable<LinFloat64Vector2D> Lerp(this IEnumerable<double> tList, IPair<double> v1, IPair<double> v2)
    {
        return tList.Select(t => t.Lerp(v1, v2));
    }

    
    public static LinFloat64Vector2D TranslateBy(this IPair<double> vector, double translationX, double translationY)
    {
        return LinFloat64Vector2D.Create(
            translationX + vector.Item1,
            translationY + vector.Item2
        );
    }

    
    public static LinFloat64Vector2D TranslateBy(this IPair<double> vector, IPair<double> translationVector)
    {
        return LinFloat64Vector2D.Create(
            translationVector.Item1 + vector.Item1,
            translationVector.Item2 + vector.Item2
        );
    }

    
    public static LinFloat64Vector2D ScaleBy(this IPair<double> vector, double scaleFactor)
    {
        return LinFloat64Vector2D.Create(
            scaleFactor * vector.Item1,
            scaleFactor * vector.Item2
        );
    }

    
    public static LinFloat64Vector2D ScaleBy(this IPair<double> vector, double scaleFactorX, double scaleFactorY)
    {
        return LinFloat64Vector2D.Create(
            scaleFactorX * vector.Item1,
            scaleFactorY * vector.Item2
        );
    }

    
    public static LinFloat64Vector2D ScaleBy(this IPair<double> vector, IPair<double> scaleFactorVector)
    {
        return LinFloat64Vector2D.Create(
            scaleFactorVector.Item1 * vector.Item1,
            scaleFactorVector.Item2 * vector.Item2
        );
    }

    
    public static LinFloat64Vector2D RotateBy(this IPair<double> vector, LinFloat64Angle angle)
    {
        return angle.Rotate(vector);
    }

    public static LinFloat64Vector2D RotateToUnitVector(this IPair<double> vector1, IPair<double> unitVector, LinFloat64Angle angle)
    {
        Debug.Assert(
            vector1.IsNearUnit() &&
            unitVector.IsNearUnit()
        );

        // Create a unit normal to u in the u-v rotational plane
        var v1Dot2 = unitVector.VectorESp(vector1);

        var v1 = v1Dot2.Abs().IsNearOne()
            ? vector1.GetNormal()
            : unitVector.VectorSubtract(vector1.VectorTimes(v1Dot2));

        var v1Length = v1.VectorENorm();

        Debug.Assert(
            v1.VectorESp(vector1).IsNearZero() &&
            !v1Length.IsNearZero()
        );

        // Compute a rotated version of v in the u-v rotational plane by the given angle
        return vector1
            .VectorTimes(angle.Cos())
            .VectorAdd(v1.VectorTimes(angle.Sin() / v1Length));
    }

    
    public static LinFloat64Vector2D ReflectVectorOnVector(this IPair<double> reflectionVector, IPair<double> vector)
    {
        var s =
            2 * reflectionVector.VectorESp(vector) /
            reflectionVector.VectorENormSquared();

        return LinFloat64Vector2D.Create(
            vector.Item1 - s * reflectionVector.Item1,
            vector.Item2 - s * reflectionVector.Item2
        );
    }

    
    public static Pair<double> AnalyzeOnVectors(this IPair<double> v, IPair<double> u1, IPair<double> u2)
    {
        var s1 =
            (v.Item1 * u1.Item1 + v.Item2 * u1.Item2) /
            (u1.Item1 * u1.Item1 + u1.Item2 * u1.Item2).Sqrt();

        var s2 =
            (v.Item1 * u2.Item1 + v.Item2 * u2.Item2) /
            (u2.Item1 * u2.Item1 + u2.Item2 * u2.Item2).Sqrt();

        return new Pair<double>(s1, s2);
    }

    
    public static LinFloat64Vector2D ProjectOnVector(this IPair<double> v, IPair<double> u)
    {
        var s1 = v.Item1 * u.Item1 + v.Item2 * u.Item2;
        var s2 = u.Item1 * u.Item1 + u.Item2 * u.Item2;
        var s = s1 / s2;

        return LinFloat64Vector2D.Create(u.Item1 * s, u.Item2 * s);
    }

    
    public static LinFloat64Vector2D ProjectOnUnitVector(this IPair<double> v, IPair<double> u)
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
    
    public static LinFloat64Vector2D FaceDirection(this IPair<double> vector, IPair<double> directionVector)
    {
        Debug.Assert(!directionVector.IsZero());

        return
            (vector.Item1 * directionVector.Item1 + vector.Item2 * directionVector.Item2).IsNegative()
                ? LinFloat64Vector2D.Create(-vector.Item1, -vector.Item2)
                : LinFloat64Vector2D.Create(vector.Item1, vector.Item2);
    }

    public static IEnumerable<LinFloat64Vector2D> ReflectVectorsOnVector(this IPair<double> reflectionVector, params IPair<double>[] vectorsList)
    {
        var s = 2 / reflectionVector.VectorENormSquared();

        foreach (var vector in vectorsList)
        {
            var s1 = s * reflectionVector.VectorESp(vector);

            yield return LinFloat64Vector2D.Create(
                vector.Item1 - s1 * reflectionVector.Item1,
                vector.Item2 - s1 * reflectionVector.Item2
            );
        }
    }

    public static IEnumerable<LinFloat64Vector2D> ReflectVectorsOnVector(this IPair<double> reflectionVector, IEnumerable<IPair<double>> vectorsList)
    {
        var s = 2 / reflectionVector.VectorENormSquared();

        foreach (var vector in vectorsList)
        {
            var s1 = s * reflectionVector.VectorESp(vector);

            yield return LinFloat64Vector2D.Create(
                vector.Item1 - s1 * reflectionVector.Item1,
                vector.Item2 - s1 * reflectionVector.Item2
            );
        }
    }

    
    public static LinFloat64Vector2D ReflectVectorOnUnitVector(this IPair<double> reflectionVector, IPair<double> vector)
    {
        var s = 2 * reflectionVector.VectorESp(vector);

        return LinFloat64Vector2D.Create(
            vector.Item1 - s * reflectionVector.Item1,
            vector.Item2 - s * reflectionVector.Item2
        );
    }

    public static IEnumerable<LinFloat64Vector2D> ReflectVectorsOnUnitVector(this IPair<double> reflectionVector, params IPair<double>[] vectorsList)
    {
        foreach (var vector in vectorsList)
        {
            var s1 = 2 * reflectionVector.VectorESp(vector);

            yield return LinFloat64Vector2D.Create(
                vector.Item1 - s1 * reflectionVector.Item1,
                vector.Item2 - s1 * reflectionVector.Item2
            );
        }
    }

    public static IEnumerable<LinFloat64Vector2D> ReflectVectorsOnUnitVector(this IPair<double> reflectionVector, IEnumerable<IPair<double>> vectorsList)
    {
        foreach (var vector in vectorsList)
        {
            var s1 = 2 * reflectionVector.VectorESp(vector);

            yield return LinFloat64Vector2D.Create(
                vector.Item1 - s1 * reflectionVector.Item1,
                vector.Item2 - s1 * reflectionVector.Item2
            );
        }
    }

    
    public static LinFloat64Vector2D SetLength(this IPair<double> vector, double newLength)
    {
        var oldLength = vector.VectorENorm();

        if (oldLength.IsZero())
            return LinFloat64Vector2D.Zero;

        var scalingFactor = newLength / oldLength;

        return LinFloat64Vector2D.Create(
            vector.Item1 * scalingFactor,
            vector.Item2 * scalingFactor
        );
    }
}