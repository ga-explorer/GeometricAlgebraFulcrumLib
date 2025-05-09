using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;

public static class LinVector2DAffineUtils
{

    /// <summary>
    /// Returns a new vector orthogonal to this one.
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> GetNormal<T>(this IPair<Scalar<T>> vector)
    {
        return LinVector2D<T>.Create(-vector.Item2, vector.Item1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> GetUnitNormal<T>(this IPair<Scalar<T>> vector)
    {
        var normInv = 1d / vector.VectorENorm();

        return LinVector2D<T>.Create(
            -vector.Item2 * normInv,
            vector.Item1 * normInv
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Determinant<T>(this IPair<Scalar<T>> v1, IPair<Scalar<T>> v2)
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
    public static Scalar<T> GetDistanceToPoint<T>(this IPair<Scalar<T>> v1, IPair<Scalar<T>> v2)
    {
        var vX = v2.Item1 - v1.Item1;
        var vY = v2.Item2 - v1.Item2;

        return (vX * vX + vY * vY).Sqrt();
    }

    /// <summary>
    /// The Euclidean distance between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetDistanceSquaredToPoint<T>(this IPair<Scalar<T>> v1, IPair<Scalar<T>> v2)
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
    public static Scalar<T> GetDistanceSquaredToPoint<T>(this IPair<Scalar<T>> v1, Scalar<T> v2X, Scalar<T> v2Y)
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
    public static Scalar<T> GetDistanceToPoint<T>(this IPair<Scalar<T>> v1, Scalar<T> v2X, Scalar<T> v2Y)
    {
        var vX = v2X - v1.Item1;
        var vY = v2Y - v1.Item2;

        return (vX * vX + vY * vY).Sqrt();
    }

    public static LinVector2D<T> GetCenterOfMassPoint<T>(this IReadOnlyList<IPair<Scalar<T>>> pointsList)
    {
        var scalarProcessor = pointsList[0].GetScalarProcessor();

        var centerX = scalarProcessor.Zero;
        var centerY = scalarProcessor.Zero;

        var pointsCount = 0;
        foreach (var point in pointsList)
        {
            centerX += point.Item1;
            centerY += point.Item2;

            pointsCount++;
        }

        var pointsCountInv = 1.0d / pointsCount;

        return LinVector2D<T>.Create(
            centerX * pointsCountInv,
            centerY * pointsCountInv
        );
    }

    public static LinVector2D<T> GetCenterPoint<T>(this IReadOnlyList<IPair<Scalar<T>>> pointsList)
    {
        var scalarProcessor = pointsList.First().GetScalarProcessor();

        var minX = scalarProcessor.PositiveInfinity;
        var minY = scalarProcessor.PositiveInfinity;

        var maxX = scalarProcessor.NegativeInfinity;
        var maxY = scalarProcessor.NegativeInfinity;

        foreach (var point in pointsList)
        {
            if (point.Item1 < minX) minX = point.Item1;
            if (point.Item1 > maxX) maxX = point.Item1;

            if (point.Item2 < minY) minY = point.Item2;
            if (point.Item2 > maxY) maxY = point.Item2;
        }

        return LinVector2D<T>.Create(
            0.5 * (minX + maxX),
            0.5 * (minY + maxY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Scalar<T>> GetDistancesToPoints<T>(this IPair<Scalar<T>> point, IEnumerable<IPair<Scalar<T>>> pointsList)
    {
        return pointsList.Select(point.GetDistanceToPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Scalar<T>> GetDistancesSquaredToPoints<T>(this IPair<Scalar<T>> point, IEnumerable<IPair<Scalar<T>>> pointsList)
    {
        return pointsList.Select(point.GetDistanceSquaredToPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> GetDirectionTo<T>(this IPair<Scalar<T>> p1, IPair<Scalar<T>> p2)
    {
        return LinVector2D<T>.Create(
            p2.Item1 - p1.Item1,
            p2.Item2 - p1.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> GetDirectionTo<T>(this IPair<Scalar<T>> p1, Scalar<T> p2X, Scalar<T> p2Y)
    {
        return LinVector2D<T>.Create(
            p2X - p1.Item1,
            p2Y - p1.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> GetDirectionFrom<T>(this IPair<Scalar<T>> p2, Scalar<T> p1X, Scalar<T> p1Y)
    {
        return LinVector2D<T>.Create(
            p2.Item1 - p1X,
            p2.Item2 - p1Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> GetUnitDirectionTo<T>(this IPair<Scalar<T>> p1, IPair<Scalar<T>> p2)
    {
        var dx = p2.Item1 - p1.Item1;
        var dy = p2.Item2 - p1.Item2;
        var dInv = (dx * dx + dy * dy).Sqrt().Inverse();

        return LinVector2D<T>.Create(dx * dInv, dy * dInv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> GetUnitDirectionFrom<T>(this IPair<Scalar<T>> p2, IPair<Scalar<T>> p1)
    {
        var dx = p2.Item1 - p1.Item1;
        var dy = p2.Item2 - p1.Item2;
        var dInv = 1 / (dx * dx + dy * dy).Sqrt();

        return LinVector2D<T>.Create(dx * dInv, dy * dInv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> GetPointInDirection<T>(this IPair<Scalar<T>> p, IPair<Scalar<T>> v)
    {
        return LinVector2D<T>.Create(
            p.Item1 + v.Item1,
            p.Item2 + v.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> GetPointInDirection<T>(this IPair<Scalar<T>> p, IPair<Scalar<T>> v, Scalar<T> t)
    {
        return LinVector2D<T>.Create(
            p.Item1 + t * v.Item1,
            p.Item2 + t * v.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> GetDirectionFrom<T>(this IPair<Scalar<T>> p2, IPair<Scalar<T>> p1)
    {
        return LinVector2D<T>.Create(
            p2.Item1 - p1.Item1,
            p2.Item2 - p1.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Lerp<T>(this Scalar<T> t, IPair<Scalar<T>> v1, IPair<Scalar<T>> v2)
    {
        var s = 1.0d - t;

        return LinVector2D<T>.Create(
            s * v1.Item1 + t * v2.Item1,
            s * v1.Item2 + t * v2.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinVector2D<T>> Lerp<T>(this IEnumerable<Scalar<T>> tList, IPair<Scalar<T>> v1, IPair<Scalar<T>> v2)
    {
        return tList.Select(t => t.Lerp(v1, v2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> TranslateBy<T>(this IPair<Scalar<T>> vector, Scalar<T> translationX, Scalar<T> translationY)
    {
        return LinVector2D<T>.Create(
            translationX + vector.Item1,
            translationY + vector.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> TranslateBy<T>(this IPair<Scalar<T>> vector, IPair<Scalar<T>> translationVector)
    {
        return LinVector2D<T>.Create(
            translationVector.Item1 + vector.Item1,
            translationVector.Item2 + vector.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ScaleBy<T>(this IPair<Scalar<T>> vector, Scalar<T> scaleFactor)
    {
        return LinVector2D<T>.Create(
            scaleFactor * vector.Item1,
            scaleFactor * vector.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ScaleBy<T>(this IPair<Scalar<T>> vector, Scalar<T> scaleFactorX, Scalar<T> scaleFactorY)
    {
        return LinVector2D<T>.Create(
            scaleFactorX * vector.Item1,
            scaleFactorY * vector.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ScaleBy<T>(this IPair<Scalar<T>> vector, IPair<Scalar<T>> scaleFactorVector)
    {
        return LinVector2D<T>.Create(
            scaleFactorVector.Item1 * vector.Item1,
            scaleFactorVector.Item2 * vector.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> RotateBy<T>(this IPair<Scalar<T>> vector, LinAngle<T> angle)
    {
        return angle.Rotate(vector);
    }

    public static LinVector2D<T> RotateToUnitVector<T>(this IPair<Scalar<T>> vector1, IPair<Scalar<T>> unitVector, LinAngle<T> angle)
    {
        Debug.Assert(
            vector1.VectorIsNearUnit() &&
            unitVector.VectorIsNearUnit()
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ReflectVectorOnVector<T>(this IPair<Scalar<T>> reflectionVector, IPair<Scalar<T>> vector)
    {
        var s =
            2 * reflectionVector.VectorESp(vector) /
            reflectionVector.VectorENormSquared();

        return LinVector2D<T>.Create(
            vector.Item1 - s * reflectionVector.Item1,
            vector.Item2 - s * reflectionVector.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> AnalyzeOnVectors<T>(this IPair<Scalar<T>> v, IPair<Scalar<T>> u1, IPair<Scalar<T>> u2)
    {
        var s1 =
            (v.Item1 * u1.Item1 + v.Item2 * u1.Item2) /
            (u1.Item1 * u1.Item1 + u1.Item2 * u1.Item2).Sqrt();

        var s2 =
            (v.Item1 * u2.Item1 + v.Item2 * u2.Item2) /
            (u2.Item1 * u2.Item1 + u2.Item2 * u2.Item2).Sqrt();

        return new Pair<Scalar<T>>(s1, s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ProjectOnVector<T>(this IPair<Scalar<T>> v, IPair<Scalar<T>> u)
    {
        var s1 = v.Item1 * u.Item1 + v.Item2 * u.Item2;
        var s2 = u.Item1 * u.Item1 + u.Item2 * u.Item2;
        var s = s1 / s2;

        return LinVector2D<T>.Create(u.Item1 * s, u.Item2 * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ProjectOnUnitVector<T>(this IPair<Scalar<T>> v, IPair<Scalar<T>> u)
    {
        var s = v.Item1 * u.Item1 + v.Item2 * u.Item2;

        return LinVector2D<T>.Create(u.Item1 * s, u.Item2 * s);
    }

    /// <summary>
    /// Returns a copy of this vector if its dot product with the other vector is positive, else
    /// it returns the vector's negative
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="directionVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> FaceDirection<T>(this IPair<Scalar<T>> vector, IPair<Scalar<T>> directionVector)
    {
        Debug.Assert(!directionVector.VectorIsZero());

        return
            (vector.Item1 * directionVector.Item1 + vector.Item2 * directionVector.Item2).IsNegative()
                ? LinVector2D<T>.Create(-vector.Item1, -vector.Item2)
                : LinVector2D<T>.Create(vector.Item1, vector.Item2);
    }

    public static IEnumerable<LinVector2D<T>> ReflectVectorsOnVector<T>(this IPair<Scalar<T>> reflectionVector, params IPair<Scalar<T>>[] vectorsList)
    {
        var s = 2 / reflectionVector.VectorENormSquared();

        foreach (var vector in vectorsList)
        {
            var s1 = s * reflectionVector.VectorESp(vector);

            yield return LinVector2D<T>.Create(
                vector.Item1 - s1 * reflectionVector.Item1,
                vector.Item2 - s1 * reflectionVector.Item2
            );
        }
    }

    public static IEnumerable<LinVector2D<T>> ReflectVectorsOnVector<T>(this IPair<Scalar<T>> reflectionVector, IEnumerable<IPair<Scalar<T>>> vectorsList)
    {
        var s = 2 / reflectionVector.VectorENormSquared();

        foreach (var vector in vectorsList)
        {
            var s1 = s * reflectionVector.VectorESp(vector);

            yield return LinVector2D<T>.Create(
                vector.Item1 - s1 * reflectionVector.Item1,
                vector.Item2 - s1 * reflectionVector.Item2
            );
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ReflectVectorOnUnitVector<T>(this IPair<Scalar<T>> reflectionVector, IPair<Scalar<T>> vector)
    {
        var s = 2 * reflectionVector.VectorESp(vector);

        return LinVector2D<T>.Create(
            vector.Item1 - s * reflectionVector.Item1,
            vector.Item2 - s * reflectionVector.Item2
        );
    }

    public static IEnumerable<LinVector2D<T>> ReflectVectorsOnUnitVector<T>(this IPair<Scalar<T>> reflectionVector, params IPair<Scalar<T>>[] vectorsList)
    {
        foreach (var vector in vectorsList)
        {
            var s1 = 2 * reflectionVector.VectorESp(vector);

            yield return LinVector2D<T>.Create(
                vector.Item1 - s1 * reflectionVector.Item1,
                vector.Item2 - s1 * reflectionVector.Item2
            );
        }
    }

    public static IEnumerable<LinVector2D<T>> ReflectVectorsOnUnitVector<T>(this IPair<Scalar<T>> reflectionVector, IEnumerable<IPair<Scalar<T>>> vectorsList)
    {
        foreach (var vector in vectorsList)
        {
            var s1 = 2 * reflectionVector.VectorESp(vector);

            yield return LinVector2D<T>.Create(
                vector.Item1 - s1 * reflectionVector.Item1,
                vector.Item2 - s1 * reflectionVector.Item2
            );
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> SetLength<T>(this IPair<Scalar<T>> vector, T newLength)
    {
        Debug.Assert(newLength is not null);

        var oldLength = vector.VectorENorm();

        if (oldLength.IsZero())
            return LinVector2D<T>.Zero(vector.GetScalarProcessor());

        var scalingFactor = newLength / oldLength;

        return LinVector2D<T>.Create(
            vector.Item1 * scalingFactor,
            vector.Item2 * scalingFactor
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> SetLength<T>(this IPair<Scalar<T>> vector, Scalar<T> newLength)
    {
        return vector.SetLength(newLength.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> SetLength<T>(this IPair<Scalar<T>> vector, IScalar<T> newLength)
    {
        return vector.SetLength(newLength.ScalarValue);
    }
}