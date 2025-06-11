using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;

public static class LinFloat64Vector3DAffineUtils
{
    
    public static double VectorLengthXy(this ITriplet<double> tuple)
    {
        return (tuple.Item1.Square() + tuple.Item2.Square()).Sqrt();
    }

    
    public static double VectorLengthXz(this ITriplet<double> tuple)
    {
        return (tuple.Item1.Square() + tuple.Item3.Square()).Sqrt();
    }

    
    public static double VectorLengthYz(this ITriplet<double> tuple)
    {
        return (tuple.Item2.Square() + tuple.Item3.Square()).Sqrt();
    }

    
    public static LinFloat64Vector3D Lerp(this double t, ITriplet<double> v1, ITriplet<double> v2)
    {
        var s = 1.0d - t;

        return LinFloat64Vector3D.Create(
            s * v1.Item1 + t * v2.Item1,
            s * v1.Item2 + t * v2.Item2,
            s * v1.Item3 + t * v2.Item3
        );
    }

    
    public static IEnumerable<LinFloat64Vector3D> Lerp(this IEnumerable<double> tList, ITriplet<double> v1, ITriplet<double> v2)
    {
        return tList.Select(t => t.Lerp(v1, v2));
    }

    
    public static double Determinant(this ITriplet<double> v1, ITriplet<double> v2, ITriplet<double> v3)
    {
        return v1.Item1 * (v2.Item2 * v3.Item3 - v2.Item3 * v3.Item2) +
               v1.Item2 * (v2.Item3 * v3.Item1 - v2.Item1 * v3.Item3) +
               v1.Item3 * (v2.Item1 * v3.Item2 - v2.Item2 * v3.Item1);
    }

    
    public static LinFloat64Vector3D TranslateBy(this ITriplet<double> vector, double translationX, double translationY, double translationZ)
    {
        return LinFloat64Vector3D.Create(
            translationX + vector.Item1,
            translationY + vector.Item2,
            translationZ + vector.Item3
        );
    }

    
    public static LinFloat64Vector3D TranslateBy(this ITriplet<double> vector, ITriplet<double> translationVector)
    {
        return LinFloat64Vector3D.Create(
            translationVector.Item1 + vector.Item1,
            translationVector.Item2 + vector.Item2,
            translationVector.Item3 + vector.Item3
        );
    }

    
    public static LinFloat64Vector3D ScaleBy(this ITriplet<double> vector, double scaleFactor)
    {
        return LinFloat64Vector3D.Create(
            scaleFactor * vector.Item1,
            scaleFactor * vector.Item2,
            scaleFactor * vector.Item3
        );
    }

    
    public static LinFloat64Vector3D ScaleBy(this ITriplet<double> vector, double scaleFactorX, double scaleFactorY, double scaleFactorZ)
    {
        return LinFloat64Vector3D.Create(
            scaleFactorX * vector.Item1,
            scaleFactorY * vector.Item2,
            scaleFactorZ * vector.Item3
        );
    }

    
    public static LinFloat64Vector3D ScaleBy(this ITriplet<double> vector, ITriplet<double> scaleFactorVector)
    {
        return LinFloat64Vector3D.Create(
            scaleFactorVector.Item1 * vector.Item1,
            scaleFactorVector.Item2 * vector.Item2,
            scaleFactorVector.Item3 * vector.Item3
        );
    }

    
    public static LinFloat64Vector3D XRotateBy(this ITriplet<double> vector, LinFloat64Angle angle)
    {
        var cosAngle = angle.Cos();
        var sinAngle = angle.Sin();

        return LinFloat64Vector3D.Create(
            vector.Item1,
            vector.Item2 * cosAngle - vector.Item3 * sinAngle,
            vector.Item2 * sinAngle + vector.Item3 * cosAngle
        );
    }

    
    public static LinFloat64Vector3D YRotateBy(this ITriplet<double> vector, LinFloat64Angle angle)
    {
        var cosAngle = angle.Cos();
        var sinAngle = angle.Sin();

        return LinFloat64Vector3D.Create(
            vector.Item1 * cosAngle + vector.Item3 * sinAngle,
            vector.Item2,
            -vector.Item1 * sinAngle + vector.Item3 * cosAngle
        );
    }

    
    public static LinFloat64Vector3D ZRotateBy(this ITriplet<double> vector, LinFloat64Angle angle)
    {
        var cosAngle = angle.Cos();
        var sinAngle = angle.Sin();

        return LinFloat64Vector3D.Create(
            vector.Item1 * cosAngle - vector.Item2 * sinAngle,
            vector.Item1 * sinAngle + vector.Item2 * cosAngle,
            vector.Item3
        );
    }

    
    public static LinFloat64Vector3D XRotateByDegrees(this ITriplet<double> vector, double angle)
    {
        return vector.XRotateBy(angle.DegreesToPolarAngle());
    }

    
    public static LinFloat64Vector3D YRotateByDegrees(this ITriplet<double> vector, double angle)
    {
        return vector.YRotateBy(angle.DegreesToPolarAngle());
    }

    
    public static LinFloat64Vector3D ZRotateByDegrees(this ITriplet<double> vector, double angle)
    {
        return vector.ZRotateBy(angle.DegreesToPolarAngle());
    }

    public static LinFloat64Vector3D RotateToUnitVector(this ITriplet<double> vector1, ITriplet<double> unitVector, LinFloat64Angle angle)
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


    
    public static void GetCoordinateSystem(this ITriplet<double> v1, out LinFloat64Vector3D v2, out LinFloat64Vector3D v3)
    {
        v2 = Math.Abs(v1.Item1) > Math.Abs(v1.Item2)
            ? LinFloat64Vector3D.Create(-v1.Item3, 0, v1.Item1) / Math.Sqrt(v1.Item1 * v1.Item1 + v1.Item3 * v1.Item3)
            : LinFloat64Vector3D.Create(0, v1.Item3, -v1.Item2) / Math.Sqrt(v1.Item2 * v1.Item2 + v1.Item3 * v1.Item3);

        v3 = v1.VectorCross(v2);
    }

    
    public static LinFloat64Vector3D GetTriangleNormal(ITriplet<double> p1, ITriplet<double> p2, ITriplet<double> p3)
    {
        //TODO: Test this for numerical stability, maybe select two sides with largest lengths
        var v12 = LinFloat64Vector3D.Create(p2.Item1 - p1.Item1, p2.Item2 - p1.Item2, p2.Item3 - p1.Item3);
        var v23 = LinFloat64Vector3D.Create(p3.Item1 - p2.Item1, p3.Item2 - p2.Item2, p3.Item3 - p2.Item3);

        return v12.VectorCross(v23);

        ////Find vector sides of triangle
        //var v12 = p2 - p1;
        //var v23 = p3 - p2;
        //var v31 = p1 - p3;

        ////Find squared side lengths of triangle
        //var side12 = v12.LengthSquared;
        //var side23 = v23.LengthSquared;
        //var side31 = v31.LengthSquared;

        //double normalX;
        //double normalY;
        //double normalZ;

        ////Find normal to triangle
        //if (side12 < side23)
        //{
        //    if (side12 < side31)
        //        return v23.Cross(v31);

        //    return 
        //}
    }

    
    public static LinFloat64Vector3D GetTriangleUnitNormal(ITriplet<double> p1, ITriplet<double> p2, ITriplet<double> p3)
    {
        //TODO: Test this for numerical stability, maybe select two sides with largest lengths
        var v12 = LinFloat64Vector3D.Create(p2.Item1 - p1.Item1, p2.Item2 - p1.Item2, p2.Item3 - p1.Item3);
        var v23 = LinFloat64Vector3D.Create(p3.Item1 - p2.Item1, p3.Item2 - p2.Item2, p3.Item3 - p2.Item3);

        return v12.VectorUnitCross(v23);
    }

    
    public static LinFloat64Vector3D GetTriangleInverseUnitNormal(ITriplet<double> p1, ITriplet<double> p2, ITriplet<double> p3)
    {
        //TODO: Test this for numerical stability, maybe select two sides with the largest lengths
        var v12 = LinFloat64Vector3D.Create(p2.Item1 - p1.Item1, p2.Item2 - p1.Item2, p2.Item3 - p1.Item3);
        var v23 = LinFloat64Vector3D.Create(p3.Item1 - p2.Item1, p3.Item2 - p2.Item2, p3.Item3 - p2.Item3);

        return v12.VectorUnitCross(v23);
    }


    
    public static LinFloat64Vector3D GetNormal(this ITriplet<double> vector)
    {
        if (vector.IsZeroVector())
            return LinFloat64Vector3D.E1;

        // For smoother motions, find the quaternion q that
        // rotates nearest basis vector e1 to vector, then use q to
        // rotate e2; the basis vector following e1
        var e1 = vector.SelectNearestBasisVector();
        var e2 = e1.NextBasisVector3D();

        if (vector.GetAngleCos(e1).IsNearOne())
            return e2.ToLinVector3D();

        return e1
            .VectorToVectorRotationQuaternion(vector.ToUnitLinVector3D())
            .RotateVector(e2);
    }

    
    public static LinFloat64Vector3D GetNormal(this ITriplet<double> vector, double length)
    {
        return vector.GetNormal().SetLength(length);
    }

    
    public static LinFloat64Vector3D GetUnitNormal(this ITriplet<double> vector)
    {
        return vector.GetNormal();
    }
    
    
    public static Pair<LinFloat64Vector3D> GetNormalPair(this ITriplet<double> vector)
    {
        if (vector.IsZeroVector())
            return new Pair<LinFloat64Vector3D>(
                LinFloat64Vector3D.E1,
                LinFloat64Vector3D.E2
            );

        // For smoother motions, find the quaternion q that
        // rotates nearest basis vector e1 to vector, then use q to
        // rotate e2; the basis vector following e1
        var e1 = vector.SelectNearestBasisVector();
        var e2 = e1.NextBasisVector3D();
        var e3 = e2.NextBasisVector3D();
        
        if (vector.GetAngleCos(e1).IsNearOne())
            return new Pair<LinFloat64Vector3D>(
                e2.ToLinVector3D(), 
                e3.ToLinVector3D()
            );

        // For smoother motions, find the quaternion q that
        // rotates e1 to vector, then use q to rotate e2, e3
        return e1
            .VectorToVectorRotationQuaternion(vector.ToUnitLinVector3D())
            .RotateVectors(e2, e3);
    }

    
    public static Pair<LinFloat64Vector3D> GetUnitNormalPair(this ITriplet<double> vector)
    {
        return vector.GetNormalPair();
    }


    
    public static LinFloat64Vector3D ProjectOnVector(this ITriplet<double> v, ITriplet<double> u)
    {
        var s1 = v.Item1 * u.Item1 + v.Item2 * u.Item2 + v.Item3 * u.Item3;
        var s2 = u.Item1 * u.Item1 + u.Item2 * u.Item2 + u.Item3 * u.Item3;
        var s = s1 / s2;

        return LinFloat64Vector3D.Create(
            u.Item1 * s,
            u.Item2 * s,
            u.Item3 * s
        );
    }

    
    public static LinFloat64Vector3D RejectOnVector(this ITriplet<double> v, ITriplet<double> u)
    {
        var s1 = v.Item1 * u.Item1 + v.Item2 * u.Item2 + v.Item3 * u.Item3;
        var s2 = u.Item1 * u.Item1 + u.Item2 * u.Item2 + u.Item3 * u.Item3;
        var s = s1 / s2;

        return LinFloat64Vector3D.Create(
            v.Item1 - u.Item1 * s,
            v.Item2 - u.Item2 * s,
            v.Item3 - u.Item3 * s
        );
    }

    
    public static LinFloat64Vector3D ProjectOnUnitVector(this ITriplet<double> v, ITriplet<double> u)
    {
        var s = v.Item1 * u.Item1 + v.Item2 * u.Item2 + v.Item3 * u.Item3;

        return LinFloat64Vector3D.Create(
            u.Item1 * s,
            u.Item2 * s,
            u.Item3 * s
        );
    }

    
    public static LinFloat64Vector3D RejectOnAxis(this ITriplet<double> v, LinBasisVector axis)
    {
        return axis.Index switch
        {
            0 => LinFloat64Vector3D.Create(0, v.Item2, v.Item3),
            1 => LinFloat64Vector3D.Create(v.Item1, 0, v.Item3),
            _ => LinFloat64Vector3D.Create(v.Item1, v.Item2, 0)
        };
    }

    
    public static LinFloat64Vector3D RejectOnUnitVector(this ITriplet<double> v, ITriplet<double> u)
    {
        var s = v.Item1 * u.Item1 + v.Item2 * u.Item2 + v.Item3 * u.Item3;

        return LinFloat64Vector3D.Create(
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
    
    public static LinFloat64Vector3D FaceDirection(this ITriplet<double> vector, ITriplet<double> directionVector)
    {
        Debug.Assert(!directionVector.IsAlmostZeroVector());

        return
            (vector.Item1 * directionVector.Item1 + vector.Item2 * directionVector.Item2 + vector.Item3 * directionVector.Item3).IsNegative()
                ? LinFloat64Vector3D.Create(-vector.Item1, -vector.Item2, -vector.Item3)
                : LinFloat64Vector3D.Create(vector.Item1, vector.Item2, vector.Item3);
    }

    public static LinFloat64Vector3D GetCenterOfMassPoint(this IEnumerable<ITriplet<double>> pointsList)
    {
        var centerX = 0.0d;
        var centerY = 0.0d;
        var centerZ = 0.0d;

        var pointsCount = 0;
        foreach (var point in pointsList)
        {
            centerX += point.Item1;
            centerY += point.Item2;
            centerZ += point.Item3;

            pointsCount++;
        }

        var pointsCountInv = 1.0d / pointsCount;

        return LinFloat64Vector3D.Create(centerX * pointsCountInv,
            centerY * pointsCountInv,
            centerZ * pointsCountInv);
    }

    public static LinFloat64Vector3D GetCenterPoint(this IEnumerable<ITriplet<double>> pointsList)
    {
        var minX = double.PositiveInfinity;
        var minY = double.PositiveInfinity;
        var minZ = double.PositiveInfinity;

        var maxX = double.NegativeInfinity;
        var maxY = double.NegativeInfinity;
        var maxZ = double.NegativeInfinity;

        foreach (var point in pointsList)
        {
            if (point.Item1 < minX) minX = point.Item1;
            if (point.Item1 > maxX) maxX = point.Item1;

            if (point.Item2 < minY) minY = point.Item2;
            if (point.Item2 > maxY) maxY = point.Item2;

            if (point.Item3 < minZ) minZ = point.Item3;
            if (point.Item3 > maxZ) maxZ = point.Item3;
        }

        return LinFloat64Vector3D.Create(0.5 * (minX + maxX),
            0.5 * (minY + maxY),
            0.5 * (minZ + maxZ));
    }

    
    public static LinFloat64Vector3D GetDirectionTo(this ITriplet<double> p1, ITriplet<double> p2)
    {
        return LinFloat64Vector3D.Create(p2.Item1 - p1.Item1,
            p2.Item2 - p1.Item2,
            p2.Item3 - p1.Item3);
    }

    
    public static LinFloat64Vector3D GetDirectionFrom(this ITriplet<double> p2, double p1X, double p1Y, double p1Z)
    {
        return LinFloat64Vector3D.Create(p2.Item1 - p1X,
            p2.Item2 - p1Y,
            p2.Item3 - p1Z);
    }

    
    public static LinFloat64Vector3D GetDirectionFrom(this ITriplet<double> p2, ITriplet<double> p1)
    {
        return LinFloat64Vector3D.Create(p2.Item1 - p1.Item1,
            p2.Item2 - p1.Item2,
            p2.Item3 - p1.Item3);
    }

    
    public static LinFloat64Vector3D GetUnitDirectionTo(this ITriplet<double> p1, ITriplet<double> p2)
    {
        var dx = p2.Item1 - p1.Item1;
        var dy = p2.Item2 - p1.Item2;
        var dz = p2.Item3 - p1.Item3;

        var normSquared = dx * dx + dy * dy + dz * dz;

        if (normSquared.IsZero())
            return LinFloat64Vector3D.E1;

        var dInv = 1d / Math.Sqrt(normSquared);

        return LinFloat64Vector3D.Create(dx * dInv, dy * dInv, dz * dInv);
    }

    
    public static LinFloat64Vector3D GetUnitDirectionFrom(this ITriplet<double> p2, ITriplet<double> p1)
    {
        var dx = p2.Item1 - p1.Item1;
        var dy = p2.Item2 - p1.Item2;
        var dz = p2.Item3 - p1.Item3;

        var normSquared = dx * dx + dy * dy + dz * dz;

        if (normSquared.IsZero())
            return LinFloat64Vector3D.E1;

        var dInv = 1d / Math.Sqrt(normSquared);

        return LinFloat64Vector3D.Create(dx * dInv, dy * dInv, dz * dInv);
    }

    
    public static LinFloat64Vector3D GetPointInDirection(this ITriplet<double> p, ITriplet<double> v)
    {
        return LinFloat64Vector3D.Create(p.Item1 + v.Item1,
            p.Item2 + v.Item2,
            p.Item3 + v.Item3);
    }

    
    public static LinFloat64Vector3D GetPointInDirection(this ITriplet<double> p, ITriplet<double> v, double t)
    {
        return LinFloat64Vector3D.Create(p.Item1 + t * v.Item1,
            p.Item2 + t * v.Item2,
            p.Item3 + t * v.Item3);
    }

    
    public static LinFloat64Vector3D AddLength(this ITriplet<double> vector, double length)
    {
        var oldLength = vector.VectorENorm();

        if (oldLength.IsNearZero())
            return LinFloat64Vector3D.Zero;

        var scalingFactor =
            (oldLength + length) / oldLength;

        return LinFloat64Vector3D.Create(vector.Item1 * scalingFactor,
            vector.Item2 * scalingFactor,
            vector.Item3 * scalingFactor);
    }

    /// <summary>
    /// The Euclidean distance between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static double GetDistanceSquaredToPoint(this ITriplet<double> v1, ITriplet<double> v2)
    {
        var vX = v2.Item1 - v1.Item1;
        var vY = v2.Item2 - v1.Item2;
        var vZ = v2.Item3 - v1.Item3;

        return vX * vX + vY * vY + vZ * vZ;
    }

    
    public static LinFloat64Vector3D ReflectVectorOnVector(this ITriplet<double> reflectionVector, ITriplet<double> vector)
    {
        var s = 2 * reflectionVector.VectorESp(vector) / reflectionVector.VectorENormSquared();

        return LinFloat64Vector3D.Create(vector.Item1 - s * reflectionVector.Item1,
            vector.Item2 - s * reflectionVector.Item2,
            vector.Item3 - s * reflectionVector.Item3);
    }

    
    public static Triplet<LinFloat64Vector3D> ReflectVectorsOnVector(this ITriplet<double> reflectionVector, Triplet<ITriplet<double>> vectorsTriplet)
    {
        var (v1, v2, v3) = vectorsTriplet;

        var s = 2 / reflectionVector.VectorENormSquared();

        var s1 = s * reflectionVector.VectorESp(v1);
        var s2 = s * reflectionVector.VectorESp(v2);
        var s3 = s * reflectionVector.VectorESp(v3);

        var rv1 = LinFloat64Vector3D.Create(v1.Item1 - s1 * reflectionVector.Item1,
            v1.Item2 - s1 * reflectionVector.Item2,
            v1.Item3 - s1 * reflectionVector.Item3);

        var rv2 = LinFloat64Vector3D.Create(v2.Item1 - s2 * reflectionVector.Item1,
            v2.Item2 - s2 * reflectionVector.Item2,
            v2.Item3 - s2 * reflectionVector.Item3);

        var rv3 = LinFloat64Vector3D.Create(v3.Item1 - s3 * reflectionVector.Item1,
            v3.Item2 - s3 * reflectionVector.Item2,
            v3.Item3 - s3 * reflectionVector.Item3);

        return new Triplet<LinFloat64Vector3D>(rv1, rv2, rv3);
    }

    
    public static Pair<LinFloat64Vector3D> ReflectVectorsOnVector(this ITriplet<double> reflectionVector, ITriplet<double> v1, ITriplet<double> v2)
    {
        var s = 2 / reflectionVector.VectorENormSquared();

        var s1 = s * reflectionVector.VectorESp(v1);
        var s2 = s * reflectionVector.VectorESp(v2);

        var rv1 = LinFloat64Vector3D.Create(v1.Item1 - s1 * reflectionVector.Item1,
            v1.Item2 - s1 * reflectionVector.Item2,
            v1.Item3 - s1 * reflectionVector.Item3);

        var rv2 = LinFloat64Vector3D.Create(v2.Item1 - s2 * reflectionVector.Item1,
            v2.Item2 - s2 * reflectionVector.Item2,
            v2.Item3 - s2 * reflectionVector.Item3);

        return new Pair<LinFloat64Vector3D>(rv1, rv2);
    }

    
    public static Triplet<LinFloat64Vector3D> ReflectVectorsOnVector(this ITriplet<double> reflectionVector, ITriplet<double> v1, ITriplet<double> v2, ITriplet<double> v3)
    {
        var s = 2 / reflectionVector.VectorENormSquared();

        var s1 = s * reflectionVector.VectorESp(v1);
        var s2 = s * reflectionVector.VectorESp(v2);
        var s3 = s * reflectionVector.VectorESp(v3);

        var rv1 = LinFloat64Vector3D.Create(v1.Item1 - s1 * reflectionVector.Item1,
            v1.Item2 - s1 * reflectionVector.Item2,
            v1.Item3 - s1 * reflectionVector.Item3);

        var rv2 = LinFloat64Vector3D.Create(v2.Item1 - s2 * reflectionVector.Item1,
            v2.Item2 - s2 * reflectionVector.Item2,
            v2.Item3 - s2 * reflectionVector.Item3);

        var rv3 = LinFloat64Vector3D.Create(v3.Item1 - s3 * reflectionVector.Item1,
            v3.Item2 - s3 * reflectionVector.Item2,
            v3.Item3 - s3 * reflectionVector.Item3);

        return new Triplet<LinFloat64Vector3D>(rv1, rv2, rv3);
    }

    public static IEnumerable<LinFloat64Vector3D> ReflectVectorsOnVector(this ITriplet<double> reflectionVector, params ITriplet<double>[] vectorsList)
    {
        var s = 2 / reflectionVector.VectorENormSquared();

        foreach (var vector in vectorsList)
        {
            var s1 = s * reflectionVector.VectorESp(vector);

            yield return LinFloat64Vector3D.Create(vector.Item1 - s1 * reflectionVector.Item1,
                vector.Item2 - s1 * reflectionVector.Item2,
                vector.Item3 - s1 * reflectionVector.Item3);
        }
    }

    
    public static LinFloat64Vector3D ReflectVectorOnUnitVector(this ITriplet<double> reflectionVector, ITriplet<double> vector)
    {
        var s = 2 * reflectionVector.VectorESp(vector);

        return LinFloat64Vector3D.Create(vector.Item1 - s * reflectionVector.Item1,
            vector.Item2 - s * reflectionVector.Item2,
            vector.Item3 - s * reflectionVector.Item3);
    }

    
    public static Triplet<LinFloat64Vector3D> ReflectVectorsOnUnitVector(this ITriplet<double> reflectionVector, Triplet<ITriplet<double>> vectorsTriplet)
    {
        var (v1, v2, v3) = vectorsTriplet;

        var s1 = 2 * reflectionVector.VectorESp(v1);
        var s2 = 2 * reflectionVector.VectorESp(v2);
        var s3 = 2 * reflectionVector.VectorESp(v3);

        var rv1 = LinFloat64Vector3D.Create(v1.Item1 - s1 * reflectionVector.Item1,
            v1.Item2 - s1 * reflectionVector.Item2,
            v1.Item3 - s1 * reflectionVector.Item3);

        var rv2 = LinFloat64Vector3D.Create(v2.Item1 - s2 * reflectionVector.Item1,
            v2.Item2 - s2 * reflectionVector.Item2,
            v2.Item3 - s2 * reflectionVector.Item3);

        var rv3 = LinFloat64Vector3D.Create(v3.Item1 - s3 * reflectionVector.Item1,
            v3.Item2 - s3 * reflectionVector.Item2,
            v3.Item3 - s3 * reflectionVector.Item3);

        return new Triplet<LinFloat64Vector3D>(rv1, rv2, rv3);
    }

    public static IEnumerable<LinFloat64Vector3D> ReflectVectorsOnUnitVector(this ITriplet<double> reflectionVector, params ITriplet<double>[] vectorsList)
    {
        foreach (var vector in vectorsList)
        {
            var s1 = 2 * reflectionVector.VectorESp(vector);

            yield return LinFloat64Vector3D.Create(vector.Item1 - s1 * reflectionVector.Item1,
                vector.Item2 - s1 * reflectionVector.Item2,
                vector.Item3 - s1 * reflectionVector.Item3);
        }
    }

    public static IEnumerable<LinFloat64Vector3D> ReflectVectorsOnUnitVector(this ITriplet<double> reflectionVector, IEnumerable<ITriplet<double>> vectorsList)
    {
        foreach (var vector in vectorsList)
        {
            var s1 = 2 * reflectionVector.VectorESp(vector);

            yield return LinFloat64Vector3D.Create(vector.Item1 - s1 * reflectionVector.Item1,
                vector.Item2 - s1 * reflectionVector.Item2,
                vector.Item3 - s1 * reflectionVector.Item3);
        }
    }

    public static IEnumerable<LinFloat64Vector3D> ReflectVectorsOnVector(this ITriplet<double> reflectionVector, IEnumerable<ITriplet<double>> vectorsList)
    {
        var s = 2 / reflectionVector.VectorENormSquared();

        foreach (var vector in vectorsList)
        {
            var s1 = s * reflectionVector.VectorESp(vector);

            yield return LinFloat64Vector3D.Create(vector.Item1 - s1 * reflectionVector.Item1,
                vector.Item2 - s1 * reflectionVector.Item2,
                vector.Item3 - s1 * reflectionVector.Item3);
        }
    }

    
    public static double GetDistanceSquaredToPoint(this ITriplet<double> p1, double p2X, double p2Y, double p2Z)
    {
        var vX = p2X - p1.Item1;
        var vY = p2Y - p1.Item2;
        var vZ = p2Z - p1.Item3;

        return vX * vX + vY * vY + vZ * vZ;
    }

    
    public static LinFloat64Vector3D SubtractLength(this ITriplet<double> vector, double length)
    {
        var oldLength = vector.VectorENorm();

        if (oldLength.IsNearZero())
            return LinFloat64Vector3D.Zero;

        var scalingFactor =
            (oldLength - length) / oldLength;

        return LinFloat64Vector3D.Create(vector.Item1 * scalingFactor,
            vector.Item2 * scalingFactor,
            vector.Item3 * scalingFactor);
    }

    /// <summary>
    /// The Euclidean distance between the given vectors
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    
    public static double GetDistanceToPoint(this ITriplet<double> p1, ITriplet<double> p2)
    {
        var vX = p2.Item1 - p1.Item1;
        var vY = p2.Item2 - p1.Item2;
        var vZ = p2.Item3 - p1.Item3;

        return Math.Sqrt(vX * vX + vY * vY + vZ * vZ);
    }

    
    public static double GetDistanceToPoint(this ITriplet<double> p1, double p2X, double p2Y, double p2Z)
    {
        var vX = p2X - p1.Item1;
        var vY = p2Y - p1.Item2;
        var vZ = p2Z - p1.Item3;

        return Math.Sqrt(vX * vX + vY * vY + vZ * vZ);
    }

    
    public static LinFloat64Vector3D SetLength(this ITriplet<double> vector, double newLength)
    {
        var oldLength = vector.VectorENorm();

        if (oldLength.IsZero())
            return LinFloat64Vector3D.Zero;

        var scalingFactor = newLength / oldLength;

        return LinFloat64Vector3D.Create(
            vector.Item1 * scalingFactor,
            vector.Item2 * scalingFactor,
            vector.Item3 * scalingFactor
        );
    }
}