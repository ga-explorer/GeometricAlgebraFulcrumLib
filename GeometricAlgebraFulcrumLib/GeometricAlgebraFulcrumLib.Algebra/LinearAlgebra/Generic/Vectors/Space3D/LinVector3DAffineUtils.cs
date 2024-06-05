using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;

public static class LinVector3DAffineUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> LengthXy<T>(this ITriplet<Scalar<T>> vector)
    {
        return (vector.Item1.Square() + vector.Item2.Square()).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> LengthXz<T>(this ITriplet<Scalar<T>> vector)
    {
        return (vector.Item1.Square() + vector.Item3.Square()).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> LengthYz<T>(this ITriplet<Scalar<T>> vector)
    {
        return (vector.Item2.Square() + vector.Item3.Square()).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> Lerp<T>(this Scalar<T> t, ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2)
    {
        var s = 1 - t;

        return LinVector3D<T>.Create(
            s * v1.Item1 + t * v2.Item1,
            s * v1.Item2 + t * v2.Item2,
            s * v1.Item3 + t * v2.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinVector3D<T>> Lerp<T>(this IEnumerable<Scalar<T>> tList, ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2)
    {
        return tList.Select(t => t.Lerp(v1, v2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Determinant<T>(this ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2, ITriplet<Scalar<T>> v3)
    {
        return v1.Item1 * (v2.Item2 * v3.Item3 - v2.Item3 * v3.Item2) +
               v1.Item2 * (v2.Item3 * v3.Item1 - v2.Item1 * v3.Item3) +
               v1.Item3 * (v2.Item1 * v3.Item2 - v2.Item2 * v3.Item1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> TranslateBy<T>(this ITriplet<Scalar<T>> vector, Scalar<T> translationX, Scalar<T> translationY, Scalar<T> translationZ)
    {
        return LinVector3D<T>.Create(
            translationX + vector.Item1,
            translationY + vector.Item2,
            translationZ + vector.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> TranslateBy<T>(this ITriplet<Scalar<T>> vector, ITriplet<Scalar<T>> translationVector)
    {
        return LinVector3D<T>.Create(
            translationVector.Item1 + vector.Item1,
            translationVector.Item2 + vector.Item2,
            translationVector.Item3 + vector.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ScaleBy<T>(this ITriplet<Scalar<T>> vector, Scalar<T> scaleFactor)
    {
        return LinVector3D<T>.Create(
            scaleFactor * vector.Item1,
            scaleFactor * vector.Item2,
            scaleFactor * vector.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ScaleBy<T>(this ITriplet<Scalar<T>> vector, Scalar<T> scaleFactorX, Scalar<T> scaleFactorY, Scalar<T> scaleFactorZ)
    {
        return LinVector3D<T>.Create(
            scaleFactorX * vector.Item1,
            scaleFactorY * vector.Item2,
            scaleFactorZ * vector.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ScaleBy<T>(this ITriplet<Scalar<T>> vector, ITriplet<Scalar<T>> scaleFactorVector)
    {
        return LinVector3D<T>.Create(
            scaleFactorVector.Item1 * vector.Item1,
            scaleFactorVector.Item2 * vector.Item2,
            scaleFactorVector.Item3 * vector.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> XRotateBy<T>(this ITriplet<Scalar<T>> vector, LinAngle<T> angle)
    {
        var cosAngle = angle.Cos();
        var sinAngle = angle.Sin();

        return LinVector3D<T>.Create(
            vector.Item1,
            vector.Item2 * cosAngle - vector.Item3 * sinAngle,
            vector.Item2 * sinAngle + vector.Item3 * cosAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> YRotateBy<T>(this ITriplet<Scalar<T>> vector, LinAngle<T> angle)
    {
        var cosAngle = angle.Cos();
        var sinAngle = angle.Sin();

        return LinVector3D<T>.Create(
            vector.Item1 * cosAngle + vector.Item3 * sinAngle,
            vector.Item2,
            -vector.Item1 * sinAngle + vector.Item3 * cosAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ZRotateBy<T>(this ITriplet<Scalar<T>> vector, LinAngle<T> angle)
    {
        var cosAngle = angle.Cos();
        var sinAngle = angle.Sin();

        return LinVector3D<T>.Create(
            vector.Item1 * cosAngle - vector.Item2 * sinAngle,
            vector.Item1 * sinAngle + vector.Item2 * cosAngle,
            vector.Item3
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector3D<T> XRotateByDegrees<T>(this ITriplet<Scalar<T>> vector, LinAngle<T> angle)
    //{
    //    return vector.XRotateBy(angle * Math.PI / 180);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector3D<T> YRotateByDegrees<T>(this ITriplet<Scalar<T>> vector, LinAngle<T> angle)
    //{
    //    return vector.YRotateBy(angle * Math.PI / 180);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector3D<T> ZRotateByDegrees<T>(this ITriplet<Scalar<T>> vector, LinAngle<T> angle)
    //{
    //    return vector.ZRotateBy(angle * Math.PI / 180);
    //}

    public static LinVector3D<T> RotateToUnitVector<T>(this ITriplet<Scalar<T>> vector1, ITriplet<Scalar<T>> unitVector, LinAngle<T> angle)
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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void GetCoordinateSystem<T>(this LinVector3D<T> v1, out LinVector3D<T> v2, out LinVector3D<T> v3)
    {
        var zero = v1.ScalarProcessor.Zero;

        v2 = v1.Item1.Abs() > v1.Item2.Abs()
            ? LinVector3D<T>.Create(-v1.Item3, zero, v1.Item1) / (v1.Item1 * v1.Item1 + v1.Item3 * v1.Item3).Sqrt()
            : LinVector3D<T>.Create(zero, v1.Item3, -v1.Item2) / (v1.Item2 * v1.Item2 + v1.Item3 * v1.Item3).Sqrt();

        v3 = v1.VectorCross(v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> GetTriangleNormal<T>(ITriplet<Scalar<T>> p1, ITriplet<Scalar<T>> p2, ITriplet<Scalar<T>> p3)
    {
        //TODO: Test this for numerical stability, maybe select two sides with the largest lengths
        var v12 = LinVector3D<T>.Create(p2.Item1 - p1.Item1, p2.Item2 - p1.Item2, p2.Item3 - p1.Item3);
        var v23 = LinVector3D<T>.Create(p3.Item1 - p2.Item1, p3.Item2 - p2.Item2, p3.Item3 - p2.Item3);

        return v12.VectorCross(v23);

        ////Find vector sides of triangle
        //var v12 = p2 - p1;
        //var v23 = p3 - p2;
        //var v31 = p1 - p3;

        ////Find squared side lengths of triangle
        //var side12 = v12.LengthSquared;
        //var side23 = v23.LengthSquared;
        //var side31 = v31.LengthSquared;

        //Scalar<T> normalX;
        //Scalar<T> normalY;
        //Scalar<T> normalZ;

        ////Find normal to triangle
        //if (side12 < side23)
        //{
        //    if (side12 < side31)
        //        return v23.Cross(v31);

        //    return 
        //}
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> GetTriangleUnitNormal<T>(ITriplet<Scalar<T>> p1, ITriplet<Scalar<T>> p2, ITriplet<Scalar<T>> p3)
    {
        //TODO: Test this for numerical stability, maybe select two sides with the largest lengths
        var v12 = LinVector3D<T>.Create(p2.Item1 - p1.Item1, p2.Item2 - p1.Item2, p2.Item3 - p1.Item3);
        var v23 = LinVector3D<T>.Create(p3.Item1 - p2.Item1, p3.Item2 - p2.Item2, p3.Item3 - p2.Item3);

        return v12.VectorUnitCross(v23);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> GetTriangleInverseUnitNormal<T>(ITriplet<Scalar<T>> p1, ITriplet<Scalar<T>> p2, ITriplet<Scalar<T>> p3)
    {
        //TODO: Test this for numerical stability, maybe select two sides with largest lengths
        var v12 = LinVector3D<T>.Create(p2.Item1 - p1.Item1, p2.Item2 - p1.Item2, p2.Item3 - p1.Item3);
        var v23 = LinVector3D<T>.Create(p3.Item1 - p2.Item1, p3.Item2 - p2.Item2, p3.Item3 - p2.Item3);

        return v12.VectorUnitCross(v23);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinUnitBasisVector3D GetNormal<T>(this LinUnitBasisVector3D vector)
    //{
    //    return vector switch
    //    {
    //        LinUnitBasisVector3D.PositiveX => LinUnitBasisVector3D.PositiveY,
    //        LinUnitBasisVector3D.PositiveY => LinUnitBasisVector3D.PositiveZ,
    //        LinUnitBasisVector3D.PositiveZ => LinUnitBasisVector3D.PositiveX,
    //        LinUnitBasisVector3D.NegativeX => LinUnitBasisVector3D.NegativeY,
    //        LinUnitBasisVector3D.NegativeY => LinUnitBasisVector3D.NegativeZ,
    //        _ => LinUnitBasisVector3D.NegativeX
    //    };
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinUnitBasisVector3D GetUnitNormal<T>(this LinUnitBasisVector3D vector)
    //{
    //    return vector switch
    //    {
    //        LinUnitBasisVector3D.PositiveX => LinUnitBasisVector3D.PositiveY,
    //        LinUnitBasisVector3D.PositiveY => LinUnitBasisVector3D.PositiveZ,
    //        LinUnitBasisVector3D.PositiveZ => LinUnitBasisVector3D.PositiveX,
    //        LinUnitBasisVector3D.NegativeX => LinUnitBasisVector3D.NegativeY,
    //        LinUnitBasisVector3D.NegativeY => LinUnitBasisVector3D.NegativeZ,
    //        _ => LinUnitBasisVector3D.NegativeX
    //    };
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> GetNormal<T>(this ITriplet<Scalar<T>> vector)
    {
        if (vector.Item2.IsZero() && vector.Item3.IsZero())
        {
            var s = vector.Item1.Sign();
            var zero = vector.GetScalarProcessor().Zero;

            return LinVector3D<T>.Create(zero, s, zero);
        }

        // For smoother motions, find the quaternion q that
        // rotates e1 to vector, then use q to rotate e2
        return LinUnitBasisVector3D
            .PositiveX
            .CreateAxisToVectorRotationQuaternion(vector.ToUnitVector())
            .RotateVector(LinUnitBasisVector3D.PositiveY);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> GetNormal<T>(this ITriplet<Scalar<T>> vector, Scalar<T> length)
    {
        return vector.GetNormal().SetLength(length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<LinVector3D<T>> GetNormalPair<T>(this ITriplet<Scalar<T>> vector)
    {
        if (vector.Item2.IsZero() && vector.Item3.IsZero())
        {
            var s = vector.Item1.Sign();

            if (s == 0)
                s = vector.GetScalarProcessor().One;

            var zero = vector.GetScalarProcessor().Zero;

            return new Pair<LinVector3D<T>>(
                LinVector3D<T>.Create(zero, s, zero),
                LinVector3D<T>.Create(zero, zero, s)
            );
        }

        // For smoother motions, find the quaternion q that
        // rotates e1 to vector, then use q to rotate e2, e3
        return LinUnitBasisVector3D
            .PositiveX
            .CreateAxisToVectorRotationQuaternion(vector.ToUnitVector())
            .RotateVectors(
                LinUnitBasisVector3D.PositiveY,
                LinUnitBasisVector3D.PositiveZ
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ProjectOnVector<T>(this ITriplet<Scalar<T>> v, ITriplet<Scalar<T>> u)
    {
        var s1 = v.Item1 * u.Item1 + v.Item2 * u.Item2 + v.Item3 * u.Item3;
        var s2 = u.Item1 * u.Item1 + u.Item2 * u.Item2 + u.Item3 * u.Item3;
        var s = s1 / s2;

        return LinVector3D<T>.Create(u.Item1 * s, u.Item2 * s, u.Item3 * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> RejectOnVector<T>(this ITriplet<Scalar<T>> v, ITriplet<Scalar<T>> u)
    {
        var s1 = v.Item1 * u.Item1 + v.Item2 * u.Item2 + v.Item3 * u.Item3;
        var s2 = u.Item1 * u.Item1 + u.Item2 * u.Item2 + u.Item3 * u.Item3;
        var s = s1 / s2;

        return LinVector3D<T>.Create(v.Item1 - u.Item1 * s, v.Item2 - u.Item2 * s, v.Item3 - u.Item3 * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ProjectOnUnitVector<T>(this ITriplet<Scalar<T>> v, ITriplet<Scalar<T>> u)
    {
        var s = v.Item1 * u.Item1 + v.Item2 * u.Item2 + v.Item3 * u.Item3;

        return LinVector3D<T>.Create(
            u.Item1 * s,
            u.Item2 * s,
            u.Item3 * s
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> RejectOnAxis<T>(this ITriplet<Scalar<T>> v, LinUnitBasisVector3D axis)
    {
        var zero = v.GetScalarProcessor().Zero;

        return axis.GetIndex() switch
        {
            0 => LinVector3D<T>.Create(zero, v.Item2, v.Item3),
            1 => LinVector3D<T>.Create(v.Item1, zero, v.Item3),
            _ => LinVector3D<T>.Create(v.Item1, v.Item2, zero)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> RejectOnUnitVector<T>(this ITriplet<Scalar<T>> v, ITriplet<Scalar<T>> u)
    {
        var s = v.Item1 * u.Item1 + v.Item2 * u.Item2 + v.Item3 * u.Item3;

        return LinVector3D<T>.Create(
            v.Item1 - u.Item1 * s,
            v.Item2 - u.Item2 * s,
            v.Item3 - u.Item3 * s
        );
    }

    /// <summary>
    /// Returns a copy of this vector if its dot product with the other vector is positive, else
    /// it returns the vector's negative
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="directionVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> FaceDirection<T>(this ITriplet<Scalar<T>> vector, ITriplet<Scalar<T>> directionVector)
    {
        Debug.Assert(!directionVector.IsNearZeroVector());

        return
            (vector.Item1 * directionVector.Item1 + vector.Item2 * directionVector.Item2 + vector.Item3 * directionVector.Item3).IsNegative()
                ? LinVector3D<T>.Create(-vector.Item1, -vector.Item2, -vector.Item3)
                : LinVector3D<T>.Create(vector.Item1, vector.Item2, vector.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> GetUnitNormal<T>(this ITriplet<Scalar<T>> vector)
    {
        return vector.GetNormal();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<LinVector3D<T>> GetUnitNormalPair<T>(this ITriplet<Scalar<T>> vector)
    {
        return vector.GetNormalPair();
    }

    public static LinVector3D<T> GetCenterOfMassPoint<T>(this IReadOnlyList<ITriplet<Scalar<T>>> pointsList)
    {
        var scalarProcessor = pointsList[0].GetScalarProcessor();

        var centerX = scalarProcessor.Zero;
        var centerY = scalarProcessor.Zero;
        var centerZ = scalarProcessor.Zero;

        var pointsCount = 0;
        foreach (var point in pointsList)
        {
            centerX += point.Item1;
            centerY += point.Item2;
            centerZ += point.Item3;

            pointsCount++;
        }

        return LinVector3D<T>.Create(
            centerX / pointsCount,
            centerY / pointsCount,
            centerZ / pointsCount
        );
    }

    //public static LinVector3D<T> GetCenterPoint<T>(this IEnumerable<ITriplet<Scalar<T>>> pointsList)
    //{
    //    var minX = Scalar<T>.PositiveInfinity;
    //    var minY = Scalar<T>.PositiveInfinity;
    //    var minZ = Scalar<T>.PositiveInfinity;

    //    var maxX = Scalar<T>.NegativeInfinity;
    //    var maxY = Scalar<T>.NegativeInfinity;
    //    var maxZ = Scalar<T>.NegativeInfinity;

    //    foreach (var point in pointsList)
    //    {
    //        if (point.Item1 < minX) minX = point.Item1;
    //        if (point.Item1 > maxX) maxX = point.Item1;

    //        if (point.Item2 < minY) minY = point.Item2;
    //        if (point.Item2 > maxY) maxY = point.Item2;

    //        if (point.Item3 < minZ) minZ = point.Item3;
    //        if (point.Item3 > maxZ) maxZ = point.Item3;
    //    }

    //    return LinVector3D<T>.Create(0.5 * (minX + maxX),
    //        0.5 * (minY + maxY),
    //        0.5 * (minZ + maxZ));
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> GetDirectionTo<T>(this ITriplet<Scalar<T>> p1, ITriplet<Scalar<T>> p2)
    {
        return LinVector3D<T>.Create(
            p2.Item1 - p1.Item1,
            p2.Item2 - p1.Item2,
            p2.Item3 - p1.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> GetDirectionFrom<T>(this ITriplet<Scalar<T>> p2, Scalar<T> p1X, Scalar<T> p1Y, Scalar<T> p1Z)
    {
        return LinVector3D<T>.Create(
            p2.Item1 - p1X,
            p2.Item2 - p1Y,
            p2.Item3 - p1Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> GetDirectionFrom<T>(this ITriplet<Scalar<T>> p2, ITriplet<Scalar<T>> p1)
    {
        return LinVector3D<T>.Create(
            p2.Item1 - p1.Item1,
            p2.Item2 - p1.Item2,
            p2.Item3 - p1.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> GetUnitDirectionTo<T>(this ITriplet<Scalar<T>> p1, ITriplet<Scalar<T>> p2)
    {
        var scalarProcessor = p1.GetScalarProcessor();

        var dx = p2.Item1 - p1.Item1;
        var dy = p2.Item2 - p1.Item2;
        var dz = p2.Item3 - p1.Item3;

        var normSquared = dx * dx + dy * dy + dz * dz;

        if (normSquared.IsZero())
            return LinVector3D<T>.E1(scalarProcessor);

        var dInv = 1d / normSquared.Sqrt();

        return LinVector3D<T>.Create(dx * dInv, dy * dInv, dz * dInv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> GetUnitDirectionFrom<T>(this ITriplet<Scalar<T>> p2, ITriplet<Scalar<T>> p1)
    {
        var scalarProcessor = p1.GetScalarProcessor();

        var dx = p2.Item1 - p1.Item1;
        var dy = p2.Item2 - p1.Item2;
        var dz = p2.Item3 - p1.Item3;

        var normSquared = dx * dx + dy * dy + dz * dz;

        if (normSquared.IsZero())
            return LinVector3D<T>.E1(scalarProcessor);

        var dInv = 1d / normSquared.Sqrt();

        return LinVector3D<T>.Create(dx * dInv, dy * dInv, dz * dInv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> GetPointInDirection<T>(this ITriplet<Scalar<T>> p, ITriplet<Scalar<T>> v)
    {
        return LinVector3D<T>.Create(
            p.Item1 + v.Item1,
            p.Item2 + v.Item2,
            p.Item3 + v.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> GetPointInDirection<T>(this ITriplet<Scalar<T>> p, ITriplet<Scalar<T>> v, Scalar<T> t)
    {
        return LinVector3D<T>.Create(
            p.Item1 + t * v.Item1,
            p.Item2 + t * v.Item2,
            p.Item3 + t * v.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> AddLength<T>(this ITriplet<Scalar<T>> vector, Scalar<T> length)
    {
        var scalarProcessor = vector.GetScalarProcessor();

        var oldLength = vector.VectorENorm();

        if (oldLength.IsNearZero())
            return LinVector3D<T>.Zero(scalarProcessor);

        var scalingFactor =
            (oldLength + length) / oldLength;

        return LinVector3D<T>.Create(vector.Item1 * scalingFactor,
            vector.Item2 * scalingFactor,
            vector.Item3 * scalingFactor);
    }

    /// <summary>
    /// The Euclidean distance between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetDistanceSquaredToPoint<T>(this ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2)
    {
        var vX = v2.Item1 - v1.Item1;
        var vY = v2.Item2 - v1.Item2;
        var vZ = v2.Item3 - v1.Item3;

        return vX * vX + vY * vY + vZ * vZ;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ReflectVectorOnVector<T>(this ITriplet<Scalar<T>> reflectionVector, ITriplet<Scalar<T>> vector)
    {
        var s = 2 * reflectionVector.VectorESp(vector) / reflectionVector.VectorENormSquared();

        return LinVector3D<T>.Create(
            vector.Item1 - s * reflectionVector.Item1,
            vector.Item2 - s * reflectionVector.Item2,
            vector.Item3 - s * reflectionVector.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<LinVector3D<T>> ReflectVectorsOnVector<T>(this ITriplet<Scalar<T>> reflectionVector, Triplet<ITriplet<Scalar<T>>> vectorsTriplet)
    {
        var (v1, v2, v3) = vectorsTriplet;

        var s = 2 / reflectionVector.VectorENormSquared();

        var s1 = s * reflectionVector.VectorESp(v1);
        var s2 = s * reflectionVector.VectorESp(v2);
        var s3 = s * reflectionVector.VectorESp(v3);

        var rv1 = LinVector3D<T>.Create(
            v1.Item1 - s1 * reflectionVector.Item1,
            v1.Item2 - s1 * reflectionVector.Item2,
            v1.Item3 - s1 * reflectionVector.Item3
        );

        var rv2 = LinVector3D<T>.Create(
            v2.Item1 - s2 * reflectionVector.Item1,
            v2.Item2 - s2 * reflectionVector.Item2,
            v2.Item3 - s2 * reflectionVector.Item3
        );

        var rv3 = LinVector3D<T>.Create(
            v3.Item1 - s3 * reflectionVector.Item1,
            v3.Item2 - s3 * reflectionVector.Item2,
            v3.Item3 - s3 * reflectionVector.Item3
        );

        return new Triplet<LinVector3D<T>>(rv1, rv2, rv3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<LinVector3D<T>> ReflectVectorsOnVector<T>(this ITriplet<Scalar<T>> reflectionVector, ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2)
    {
        var s = 2 / reflectionVector.VectorENormSquared();

        var s1 = s * reflectionVector.VectorESp(v1);
        var s2 = s * reflectionVector.VectorESp(v2);

        var rv1 = LinVector3D<T>.Create(
            v1.Item1 - s1 * reflectionVector.Item1,
            v1.Item2 - s1 * reflectionVector.Item2,
            v1.Item3 - s1 * reflectionVector.Item3
        );

        var rv2 = LinVector3D<T>.Create(
            v2.Item1 - s2 * reflectionVector.Item1,
            v2.Item2 - s2 * reflectionVector.Item2,
            v2.Item3 - s2 * reflectionVector.Item3
        );

        return new Pair<LinVector3D<T>>(rv1, rv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<LinVector3D<T>> ReflectVectorsOnVector<T>(this ITriplet<Scalar<T>> reflectionVector, ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2, ITriplet<Scalar<T>> v3)
    {
        var s = 2 / reflectionVector.VectorENormSquared();

        var s1 = s * reflectionVector.VectorESp(v1);
        var s2 = s * reflectionVector.VectorESp(v2);
        var s3 = s * reflectionVector.VectorESp(v3);

        var rv1 = LinVector3D<T>.Create(
            v1.Item1 - s1 * reflectionVector.Item1,
            v1.Item2 - s1 * reflectionVector.Item2,
            v1.Item3 - s1 * reflectionVector.Item3
        );

        var rv2 = LinVector3D<T>.Create(
            v2.Item1 - s2 * reflectionVector.Item1,
            v2.Item2 - s2 * reflectionVector.Item2,
            v2.Item3 - s2 * reflectionVector.Item3
        );

        var rv3 = LinVector3D<T>.Create(
            v3.Item1 - s3 * reflectionVector.Item1,
            v3.Item2 - s3 * reflectionVector.Item2,
            v3.Item3 - s3 * reflectionVector.Item3
        );

        return new Triplet<LinVector3D<T>>(rv1, rv2, rv3);
    }

    public static IEnumerable<LinVector3D<T>> ReflectVectorsOnVector<T>(this ITriplet<Scalar<T>> reflectionVector, params ITriplet<Scalar<T>>[] vectorsList)
    {
        var s = 2 / reflectionVector.VectorENormSquared();

        foreach (var vector in vectorsList)
        {
            var s1 = s * reflectionVector.VectorESp(vector);

            yield return LinVector3D<T>.Create(
                vector.Item1 - s1 * reflectionVector.Item1,
                vector.Item2 - s1 * reflectionVector.Item2,
                vector.Item3 - s1 * reflectionVector.Item3
            );
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ReflectVectorOnUnitVector<T>(this ITriplet<Scalar<T>> reflectionVector, ITriplet<Scalar<T>> vector)
    {
        var s = 2 * reflectionVector.VectorESp(vector);

        return LinVector3D<T>.Create(
            vector.Item1 - s * reflectionVector.Item1,
            vector.Item2 - s * reflectionVector.Item2,
            vector.Item3 - s * reflectionVector.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<LinVector3D<T>> ReflectVectorsOnUnitVector<T>(this ITriplet<Scalar<T>> reflectionVector, Triplet<ITriplet<Scalar<T>>> vectorsTriplet)
    {
        var (v1, v2, v3) = vectorsTriplet;

        var s1 = 2 * reflectionVector.VectorESp(v1);
        var s2 = 2 * reflectionVector.VectorESp(v2);
        var s3 = 2 * reflectionVector.VectorESp(v3);

        var rv1 = LinVector3D<T>.Create(
            v1.Item1 - s1 * reflectionVector.Item1,
            v1.Item2 - s1 * reflectionVector.Item2,
            v1.Item3 - s1 * reflectionVector.Item3
        );

        var rv2 = LinVector3D<T>.Create(
            v2.Item1 - s2 * reflectionVector.Item1,
            v2.Item2 - s2 * reflectionVector.Item2,
            v2.Item3 - s2 * reflectionVector.Item3
        );

        var rv3 = LinVector3D<T>.Create(
            v3.Item1 - s3 * reflectionVector.Item1,
            v3.Item2 - s3 * reflectionVector.Item2,
            v3.Item3 - s3 * reflectionVector.Item3
        );

        return new Triplet<LinVector3D<T>>(rv1, rv2, rv3);
    }

    public static IEnumerable<LinVector3D<T>> ReflectVectorsOnUnitVector<T>(this ITriplet<Scalar<T>> reflectionVector, params ITriplet<Scalar<T>>[] vectorsList)
    {
        foreach (var vector in vectorsList)
        {
            var s1 = 2 * reflectionVector.VectorESp(vector);

            yield return LinVector3D<T>.Create(
                vector.Item1 - s1 * reflectionVector.Item1,
                vector.Item2 - s1 * reflectionVector.Item2,
                vector.Item3 - s1 * reflectionVector.Item3
            );
        }
    }

    public static IEnumerable<LinVector3D<T>> ReflectVectorsOnUnitVector<T>(this ITriplet<Scalar<T>> reflectionVector, IEnumerable<ITriplet<Scalar<T>>> vectorsList)
    {
        foreach (var vector in vectorsList)
        {
            var s1 = 2 * reflectionVector.VectorESp(vector);

            yield return LinVector3D<T>.Create(
                vector.Item1 - s1 * reflectionVector.Item1,
                vector.Item2 - s1 * reflectionVector.Item2,
                vector.Item3 - s1 * reflectionVector.Item3
            );
        }
    }

    public static IEnumerable<LinVector3D<T>> ReflectVectorsOnVector<T>(this ITriplet<Scalar<T>> reflectionVector, IEnumerable<ITriplet<Scalar<T>>> vectorsList)
    {
        var s = 2 / reflectionVector.VectorENormSquared();

        foreach (var vector in vectorsList)
        {
            var s1 = s * reflectionVector.VectorESp(vector);

            yield return LinVector3D<T>.Create(
                vector.Item1 - s1 * reflectionVector.Item1,
                vector.Item2 - s1 * reflectionVector.Item2,
                vector.Item3 - s1 * reflectionVector.Item3
            );
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetDistanceSquaredToPoint<T>(this ITriplet<Scalar<T>> p1, Scalar<T> p2X, Scalar<T> p2Y, Scalar<T> p2Z)
    {
        var vX = p2X - p1.Item1;
        var vY = p2Y - p1.Item2;
        var vZ = p2Z - p1.Item3;

        return vX * vX + vY * vY + vZ * vZ;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> SubtractLength<T>(this ITriplet<Scalar<T>> vector, Scalar<T> length)
    {
        var scalarProcessor = vector.GetScalarProcessor();

        var oldLength = vector.VectorENorm();

        if (oldLength.IsNearZero())
            return LinVector3D<T>.Zero(scalarProcessor);

        var scalingFactor =
            (oldLength - length) / oldLength;

        return LinVector3D<T>.Create(
            vector.Item1 * scalingFactor,
            vector.Item2 * scalingFactor,
            vector.Item3 * scalingFactor
        );
    }

    /// <summary>
    /// The Euclidean distance between the given vectors
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetDistanceToPoint<T>(this ITriplet<Scalar<T>> p1, ITriplet<Scalar<T>> p2)
    {
        var vX = p2.Item1 - p1.Item1;
        var vY = p2.Item2 - p1.Item2;
        var vZ = p2.Item3 - p1.Item3;

        return (vX * vX + vY * vY + vZ * vZ).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetDistanceToPoint<T>(this ITriplet<Scalar<T>> p1, Scalar<T> p2X, Scalar<T> p2Y, Scalar<T> p2Z)
    {
        var vX = p2X - p1.Item1;
        var vY = p2Y - p1.Item2;
        var vZ = p2Z - p1.Item3;

        return (vX * vX + vY * vY + vZ * vZ).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> SetLength<T>(this ITriplet<Scalar<T>> vector, T newLength)
    {
        Debug.Assert(newLength is not null);

        var oldLength = vector.VectorENorm();

        if (oldLength.IsZero())
            return LinVector3D<T>.Zero(vector.GetScalarProcessor());

        var scalingFactor = newLength / oldLength;

        return LinVector3D<T>.Create(
            vector.Item1 * scalingFactor,
            vector.Item2 * scalingFactor,
            vector.Item3 * scalingFactor
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> SetLength<T>(this ITriplet<Scalar<T>> vector, Scalar<T> newLength)
    {
        return vector.SetLength(newLength.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> SetLength<T>(this ITriplet<Scalar<T>> vector, IScalar<T> newLength)
    {
        return vector.SetLength(newLength.ScalarValue);
    }
}