using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

public static class Float64Vector3DAffineUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar LengthXy(this IFloat64Vector3D tuple)
    {
        return (tuple.X.Square() + tuple.Y.Square()).Sqrt();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar LengthXz(this IFloat64Vector3D tuple)
    {
        return (tuple.X.Square() + tuple.Z.Square()).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar LengthYz(this IFloat64Vector3D tuple)
    {
        return (tuple.Y.Square() + tuple.Z.Square()).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Lerp(this double t, IFloat64Vector3D v1, IFloat64Vector3D v2)
    {
        var s = 1.0d - t;

        return Float64Vector3D.Create(s * v1.X + t * v2.X,
            s * v1.Y + t * v2.Y,
            s * v1.Z + t * v2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Float64Vector3D> Lerp(this IEnumerable<double> tList, IFloat64Vector3D v1, IFloat64Vector3D v2)
    {
        return tList.Select(t => t.Lerp(v1, v2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Determinant(this IFloat64Vector3D v1, IFloat64Vector3D v2, IFloat64Vector3D v3)
    {
        return v1.X * (v2.Y * v3.Z - v2.Z * v3.Y) +
               v1.Y * (v2.Z * v3.X - v2.X * v3.Z) +
               v1.Z * (v2.X * v3.Y - v2.Y * v3.X);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D TranslateBy(this IFloat64Vector3D vector, double translationX, double translationY, double translationZ)
    {
        return Float64Vector3D.Create(
            translationX + vector.X,
            translationY + vector.Y,
            translationZ + vector.Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D TranslateBy(this IFloat64Vector3D vector, IFloat64Vector3D translationVector)
    {
        return Float64Vector3D.Create(
            translationVector.X + vector.X,
            translationVector.Y + vector.Y,
            translationVector.Z + vector.Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ScaleBy(this IFloat64Vector3D vector, double scaleFactor)
    {
        return Float64Vector3D.Create(
            scaleFactor * vector.X,
            scaleFactor * vector.Y,
            scaleFactor * vector.Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ScaleBy(this IFloat64Vector3D vector, double scaleFactorX, double scaleFactorY, double scaleFactorZ)
    {
        return Float64Vector3D.Create(
            scaleFactorX * vector.X,
            scaleFactorY * vector.Y,
            scaleFactorZ * vector.Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ScaleBy(this IFloat64Vector3D vector, IFloat64Vector3D scaleFactorVector)
    {
        return Float64Vector3D.Create(
            scaleFactorVector.X * vector.X,
            scaleFactorVector.Y * vector.Y,
            scaleFactorVector.Z * vector.Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D XRotateBy(this IFloat64Vector3D vector, Float64PlanarAngle angle)
    {
        var cosAngle = angle.Cos();
        var sinAngle = angle.Sin();

        return Float64Vector3D.Create(
            vector.X,
            vector.Y * cosAngle - vector.Z * sinAngle,
            vector.Y * sinAngle + vector.Z * cosAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D YRotateBy(this IFloat64Vector3D vector, Float64PlanarAngle angle)
    {
        var cosAngle = angle.Cos();
        var sinAngle = angle.Sin();

        return Float64Vector3D.Create(
            vector.X * cosAngle + vector.Z * sinAngle,
            vector.Y,
            -vector.X * sinAngle + vector.Z * cosAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ZRotateBy(this IFloat64Vector3D vector, Float64PlanarAngle angle)
    {
        var cosAngle = angle.Cos();
        var sinAngle = angle.Sin();

        return Float64Vector3D.Create(
            vector.X * cosAngle - vector.Y * sinAngle,
            vector.X * sinAngle + vector.Y * cosAngle,
            vector.Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D XRotateByDegrees(this IFloat64Vector3D vector, Float64PlanarAngle angle)
    {
        return vector.XRotateBy(angle * Math.PI / 180);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D YRotateByDegrees(this IFloat64Vector3D vector, Float64PlanarAngle angle)
    {
        return vector.YRotateBy(angle * Math.PI / 180);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ZRotateByDegrees(this IFloat64Vector3D vector, Float64PlanarAngle angle)
    {
        return vector.ZRotateBy(angle * Math.PI / 180);
    }
    
    public static Float64Vector3D RotateToUnitVector(this IFloat64Vector3D vector1, IFloat64Vector3D unitVector, Float64PlanarAngle angle)
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
    public static void GetCoordinateSystem(this Float64Vector3D v1, out Float64Vector3D v2, out Float64Vector3D v3)
    {
        v2 = Math.Abs(v1.X) > Math.Abs(v1.Y)
            ? Float64Vector3D.Create(-v1.Z, 0, v1.X) / Math.Sqrt(v1.X * v1.X + v1.Z * v1.Z)
            : Float64Vector3D.Create(0, v1.Z, -v1.Y) / Math.Sqrt(v1.Y * v1.Y + v1.Z * v1.Z);

        v3 = v1.VectorCross(v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetTriangleNormal(IFloat64Vector3D p1, IFloat64Vector3D p2, IFloat64Vector3D p3)
    {
        //TODO: Test this for numerical stability, maybe select two sides with largest lengths
        var v12 = Float64Vector3D.Create(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
        var v23 = Float64Vector3D.Create(p3.X - p2.X, p3.Y - p2.Y, p3.Z - p2.Z);

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetTriangleUnitNormal(IFloat64Vector3D p1, IFloat64Vector3D p2, IFloat64Vector3D p3)
    {
        //TODO: Test this for numerical stability, maybe select two sides with largest lengths
        var v12 = Float64Vector3D.Create(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
        var v23 = Float64Vector3D.Create(p3.X - p2.X, p3.Y - p2.Y, p3.Z - p2.Z);

        return v12.VectorUnitCross(v23);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetTriangleInverseUnitNormal(IFloat64Vector3D p1, IFloat64Vector3D p2, IFloat64Vector3D p3)
    {
        //TODO: Test this for numerical stability, maybe select two sides with largest lengths
        var v12 = Float64Vector3D.Create(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
        var v23 = Float64Vector3D.Create(p3.X - p2.X, p3.Y - p2.Y, p3.Z - p2.Z);

        return v12.VectorUnitCross(v23);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnitBasisVector3D GetNormal(this LinUnitBasisVector3D vector)
    {
        return vector switch
        {
            LinUnitBasisVector3D.PositiveX => LinUnitBasisVector3D.PositiveY,
            LinUnitBasisVector3D.PositiveY => LinUnitBasisVector3D.PositiveZ,
            LinUnitBasisVector3D.PositiveZ => LinUnitBasisVector3D.PositiveX,
            LinUnitBasisVector3D.NegativeX => LinUnitBasisVector3D.NegativeY,
            LinUnitBasisVector3D.NegativeY => LinUnitBasisVector3D.NegativeZ,
            _ => LinUnitBasisVector3D.NegativeX
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnitBasisVector3D GetUnitNormal(this LinUnitBasisVector3D vector)
    {
        return vector switch
        {
            LinUnitBasisVector3D.PositiveX => LinUnitBasisVector3D.PositiveY,
            LinUnitBasisVector3D.PositiveY => LinUnitBasisVector3D.PositiveZ,
            LinUnitBasisVector3D.PositiveZ => LinUnitBasisVector3D.PositiveX,
            LinUnitBasisVector3D.NegativeX => LinUnitBasisVector3D.NegativeY,
            LinUnitBasisVector3D.NegativeY => LinUnitBasisVector3D.NegativeZ,
            _ => LinUnitBasisVector3D.NegativeX
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetNormal(this IFloat64Vector3D vector)
    {
        if (vector.Y.IsZero() && vector.Z.IsZero())
        {
            var s = Math.Sign(vector.X);

            return Float64Vector3D.Create(0d, s, 0d);
        }

        // For smoother motions, find the quaternion q that
        // rotates e1 to vector, then use q to rotate e2
        return LinUnitBasisVector3D
            .PositiveX
            .CreateAxisToVectorRotationQuaternion(vector.ToUnitVector())
            .RotateVector(LinUnitBasisVector3D.PositiveY);

        //var x = vector.X;
        //var y = vector.Y;
        //var z = vector.Z;

        //if (x == 0)
        //    return new Float64Tuple3D(0, -z, y);

        //if (y == 0)
        //    return new Float64Tuple3D(-z, 0, x);

        //if (z == 0)
        //    return new Float64Tuple3D(-y, x, 0);

        //var minComponentIndex =
        //    vector.GetMinAbsComponentIndex();

        //return minComponentIndex switch
        //{
        //    0 => new Float64Tuple3D(-(y + z), x, x),
        //    1 => new Float64Tuple3D(y, -(x + z), y),
        //    _ => new Float64Tuple3D(z, z, -(x + y))
        //};
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Float64Vector3D> GetNormalPair(this IFloat64Vector3D vector)
    {
        if (vector.Y.IsZero() && vector.Z.IsZero())
        {
            var s = Math.Sign(vector.X);

            if (s == 0)
                s = 1;

            return new Pair<Float64Vector3D>(
                Float64Vector3D.Create(0d, s, 0d),
                Float64Vector3D.Create(0d, 0d, s)
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
    public static Float64Vector3D ProjectOnVector(this IFloat64Vector3D v, IFloat64Vector3D u)
    {
        var s1 = v.X * u.X + v.Y * u.Y + v.Z * u.Z;
        var s2 = u.X * u.X + u.Y * u.Y + u.Z * u.Z;
        var s = s1 / s2;

        return Float64Vector3D.Create(u.X * s,
            u.Y * s,
            u.Z * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D RejectOnVector(this IFloat64Vector3D v, IFloat64Vector3D u)
    {
        var s1 = v.X * u.X + v.Y * u.Y + v.Z * u.Z;
        var s2 = u.X * u.X + u.Y * u.Y + u.Z * u.Z;
        var s = s1 / s2;

        return Float64Vector3D.Create(v.X - u.X * s,
            v.Y - u.Y * s,
            v.Z - u.Z * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ProjectOnUnitVector(this IFloat64Vector3D v, IFloat64Vector3D u)
    {
        var s = v.X * u.X + v.Y * u.Y + v.Z * u.Z;

        return Float64Vector3D.Create(
            u.X * s,
            u.Y * s,
            u.Z * s
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D RejectOnAxis(this IFloat64Vector3D v, LinUnitBasisVector3D axis)
    {
        return axis.GetIndex() switch
        {
            0 => Float64Vector3D.Create(0, v.Y.Value, v.Z.Value),
            1 => Float64Vector3D.Create(v.X.Value, 0, v.Z.Value),
            _ => Float64Vector3D.Create(v.X.Value, v.Y.Value, 0)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D RejectOnUnitVector(this IFloat64Vector3D v, IFloat64Vector3D u)
    {
        var s = v.X * u.X + v.Y * u.Y + v.Z * u.Z;

        return Float64Vector3D.Create(v.X - u.X * s,
            v.Y - u.Y * s,
            v.Z - u.Z * s);
    }
    
    /// <summary>
    /// Returns a copy of this vector if its dot product with the other vector is positive, else
    /// it returns the vector's negative
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="directionVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D FaceDirection(this IFloat64Vector3D vector, IFloat64Vector3D directionVector)
    {
        Debug.Assert(!directionVector.IsAlmostZeroVector());

        return
            (vector.X * directionVector.X + vector.Y * directionVector.Y + vector.Z * directionVector.Z).IsNegative()
                ? Float64Vector3D.Create(-vector.X, -vector.Y, -vector.Z)
                : Float64Vector3D.Create(vector.X, vector.Y, vector.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetUnitNormal(this IFloat64Vector3D vector)
    {
        return vector.GetNormal();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Float64Vector3D> GetUnitNormalPair(this IFloat64Vector3D vector)
    {
        return vector.GetNormalPair();
    }
    
    public static Float64Vector3D GetCenterOfMassPoint(this IEnumerable<IFloat64Vector3D> pointsList)
    {
        var centerX = 0.0d;
        var centerY = 0.0d;
        var centerZ = 0.0d;

        var pointsCount = 0;
        foreach (var point in pointsList)
        {
            centerX += point.X;
            centerY += point.Y;
            centerZ += point.Z;

            pointsCount++;
        }

        var pointsCountInv = 1.0d / pointsCount;

        return Float64Vector3D.Create(centerX * pointsCountInv,
            centerY * pointsCountInv,
            centerZ * pointsCountInv);
    }

    public static Float64Vector3D GetCenterPoint(this IEnumerable<IFloat64Vector3D> pointsList)
    {
        var minX = double.PositiveInfinity;
        var minY = double.PositiveInfinity;
        var minZ = double.PositiveInfinity;

        var maxX = double.NegativeInfinity;
        var maxY = double.NegativeInfinity;
        var maxZ = double.NegativeInfinity;

        foreach (var point in pointsList)
        {
            if (point.X < minX) minX = point.X;
            if (point.X > maxX) maxX = point.X;

            if (point.Y < minY) minY = point.Y;
            if (point.Y > maxY) maxY = point.Y;

            if (point.Z < minZ) minZ = point.Z;
            if (point.Z > maxZ) maxZ = point.Z;
        }

        return Float64Vector3D.Create(0.5 * (minX + maxX),
            0.5 * (minY + maxY),
            0.5 * (minZ + maxZ));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetDirectionTo(this IFloat64Vector3D p1, IFloat64Vector3D p2)
    {
        return Float64Vector3D.Create(p2.X - p1.X,
            p2.Y - p1.Y,
            p2.Z - p1.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetDirectionFrom(this IFloat64Vector3D p2, double p1X, double p1Y, double p1Z)
    {
        return Float64Vector3D.Create(p2.X - p1X,
            p2.Y - p1Y,
            p2.Z - p1Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetDirectionFrom(this IFloat64Vector3D p2, IFloat64Vector3D p1)
    {
        return Float64Vector3D.Create(p2.X - p1.X,
            p2.Y - p1.Y,
            p2.Z - p1.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetUnitDirectionTo(this IFloat64Vector3D p1, IFloat64Vector3D p2)
    {
        var dx = p2.X - p1.X;
        var dy = p2.Y - p1.Y;
        var dz = p2.Z - p1.Z;

        var normSquared = dx * dx + dy * dy + dz * dz;

        if (normSquared.IsZero())
            return Float64Vector3D.E1;

        var dInv = 1d / Math.Sqrt(normSquared);

        return Float64Vector3D.Create(dx * dInv, dy * dInv, dz * dInv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetUnitDirectionFrom(this IFloat64Vector3D p2, IFloat64Vector3D p1)
    {
        var dx = p2.X - p1.X;
        var dy = p2.Y - p1.Y;
        var dz = p2.Z - p1.Z;

        var normSquared = dx * dx + dy * dy + dz * dz;

        if (normSquared.IsZero())
            return Float64Vector3D.E1;

        var dInv = 1d / Math.Sqrt(normSquared);

        return Float64Vector3D.Create(dx * dInv, dy * dInv, dz * dInv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetPointInDirection(this IFloat64Vector3D p, IFloat64Vector3D v)
    {
        return Float64Vector3D.Create(p.X + v.X,
            p.Y + v.Y,
            p.Z + v.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetPointInDirection(this IFloat64Vector3D p, IFloat64Vector3D v, double t)
    {
        return Float64Vector3D.Create(p.X + t * v.X,
            p.Y + t * v.Y,
            p.Z + t * v.Z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D AddLength(this IFloat64Vector3D vector, double length)
    {
        var oldLength = vector.ENorm();

        if (oldLength.IsAlmostZero())
            return Float64Vector3D.Zero;

        var scalingFactor =
            (oldLength + length) / oldLength;

        return Float64Vector3D.Create(vector.X * scalingFactor,
            vector.Y * scalingFactor,
            vector.Z * scalingFactor);
    }

    /// <summary>
    /// The Euclidean distance between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetDistanceSquaredToPoint(this IFloat64Vector3D v1, IFloat64Vector3D v2)
    {
        var vX = v2.X - v1.X;
        var vY = v2.Y - v1.Y;
        var vZ = v2.Z - v1.Z;

        return vX * vX + vY * vY + vZ * vZ;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ReflectVectorOnVector(this IFloat64Vector3D reflectionVector, IFloat64Vector3D vector)
    {
        var s = 2 * reflectionVector.ESp(vector) / reflectionVector.ENormSquared();

        return Float64Vector3D.Create(vector.X - s * reflectionVector.X,
            vector.Y - s * reflectionVector.Y,
            vector.Z - s * reflectionVector.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<Float64Vector3D> ReflectVectorsOnVector(this IFloat64Vector3D reflectionVector, Triplet<IFloat64Vector3D> vectorsTriplet)
    {
        var (v1, v2, v3) = vectorsTriplet;

        var s = 2 / reflectionVector.ENormSquared();

        var s1 = s * reflectionVector.ESp(v1);
        var s2 = s * reflectionVector.ESp(v2);
        var s3 = s * reflectionVector.ESp(v3);

        var rv1 = Float64Vector3D.Create(v1.X - s1 * reflectionVector.X,
            v1.Y - s1 * reflectionVector.Y,
            v1.Z - s1 * reflectionVector.Z);

        var rv2 = Float64Vector3D.Create(v2.X - s2 * reflectionVector.X,
            v2.Y - s2 * reflectionVector.Y,
            v2.Z - s2 * reflectionVector.Z);

        var rv3 = Float64Vector3D.Create(v3.X - s3 * reflectionVector.X,
            v3.Y - s3 * reflectionVector.Y,
            v3.Z - s3 * reflectionVector.Z);

        return new Triplet<Float64Vector3D>(rv1, rv2, rv3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Float64Vector3D> ReflectVectorsOnVector(this IFloat64Vector3D reflectionVector, IFloat64Vector3D v1, IFloat64Vector3D v2)
    {
        var s = 2 / reflectionVector.ENormSquared();

        var s1 = s * reflectionVector.ESp(v1);
        var s2 = s * reflectionVector.ESp(v2);

        var rv1 = Float64Vector3D.Create(v1.X - s1 * reflectionVector.X,
            v1.Y - s1 * reflectionVector.Y,
            v1.Z - s1 * reflectionVector.Z);

        var rv2 = Float64Vector3D.Create(v2.X - s2 * reflectionVector.X,
            v2.Y - s2 * reflectionVector.Y,
            v2.Z - s2 * reflectionVector.Z);

        return new Pair<Float64Vector3D>(rv1, rv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<Float64Vector3D> ReflectVectorsOnVector(this IFloat64Vector3D reflectionVector, IFloat64Vector3D v1, IFloat64Vector3D v2, IFloat64Vector3D v3)
    {
        var s = 2 / reflectionVector.ENormSquared();

        var s1 = s * reflectionVector.ESp(v1);
        var s2 = s * reflectionVector.ESp(v2);
        var s3 = s * reflectionVector.ESp(v3);

        var rv1 = Float64Vector3D.Create(v1.X - s1 * reflectionVector.X,
            v1.Y - s1 * reflectionVector.Y,
            v1.Z - s1 * reflectionVector.Z);

        var rv2 = Float64Vector3D.Create(v2.X - s2 * reflectionVector.X,
            v2.Y - s2 * reflectionVector.Y,
            v2.Z - s2 * reflectionVector.Z);

        var rv3 = Float64Vector3D.Create(v3.X - s3 * reflectionVector.X,
            v3.Y - s3 * reflectionVector.Y,
            v3.Z - s3 * reflectionVector.Z);

        return new Triplet<Float64Vector3D>(rv1, rv2, rv3);
    }

    public static IEnumerable<Float64Vector3D> ReflectVectorsOnVector(this IFloat64Vector3D reflectionVector, params IFloat64Vector3D[] vectorsList)
    {
        var s = 2 / reflectionVector.ENormSquared();

        foreach (var vector in vectorsList)
        {
            var s1 = s * reflectionVector.ESp(vector);

            yield return Float64Vector3D.Create(vector.X - s1 * reflectionVector.X,
                vector.Y - s1 * reflectionVector.Y,
                vector.Z - s1 * reflectionVector.Z);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ReflectVectorOnUnitVector(this IFloat64Vector3D reflectionVector, IFloat64Vector3D vector)
    {
        var s = 2 * reflectionVector.ESp(vector);

        return Float64Vector3D.Create(vector.X - s * reflectionVector.X,
            vector.Y - s * reflectionVector.Y,
            vector.Z - s * reflectionVector.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<Float64Vector3D> ReflectVectorsOnUnitVector(this IFloat64Vector3D reflectionVector, Triplet<IFloat64Vector3D> vectorsTriplet)
    {
        var (v1, v2, v3) = vectorsTriplet;

        var s1 = 2 * reflectionVector.ESp(v1);
        var s2 = 2 * reflectionVector.ESp(v2);
        var s3 = 2 * reflectionVector.ESp(v3);

        var rv1 = Float64Vector3D.Create(v1.X - s1 * reflectionVector.X,
            v1.Y - s1 * reflectionVector.Y,
            v1.Z - s1 * reflectionVector.Z);

        var rv2 = Float64Vector3D.Create(v2.X - s2 * reflectionVector.X,
            v2.Y - s2 * reflectionVector.Y,
            v2.Z - s2 * reflectionVector.Z);

        var rv3 = Float64Vector3D.Create(v3.X - s3 * reflectionVector.X,
            v3.Y - s3 * reflectionVector.Y,
            v3.Z - s3 * reflectionVector.Z);

        return new Triplet<Float64Vector3D>(rv1, rv2, rv3);
    }

    public static IEnumerable<Float64Vector3D> ReflectVectorsOnUnitVector(this IFloat64Vector3D reflectionVector, params IFloat64Vector3D[] vectorsList)
    {
        foreach (var vector in vectorsList)
        {
            var s1 = 2 * reflectionVector.ESp(vector);

            yield return Float64Vector3D.Create(vector.X - s1 * reflectionVector.X,
                vector.Y - s1 * reflectionVector.Y,
                vector.Z - s1 * reflectionVector.Z);
        }
    }

    public static IEnumerable<Float64Vector3D> ReflectVectorsOnUnitVector(this IFloat64Vector3D reflectionVector, IEnumerable<IFloat64Vector3D> vectorsList)
    {
        foreach (var vector in vectorsList)
        {
            var s1 = 2 * reflectionVector.ESp(vector);

            yield return Float64Vector3D.Create(vector.X - s1 * reflectionVector.X,
                vector.Y - s1 * reflectionVector.Y,
                vector.Z - s1 * reflectionVector.Z);
        }
    }

    public static IEnumerable<Float64Vector3D> ReflectVectorsOnVector(this IFloat64Vector3D reflectionVector, IEnumerable<IFloat64Vector3D> vectorsList)
    {
        var s = 2 / reflectionVector.ENormSquared();

        foreach (var vector in vectorsList)
        {
            var s1 = s * reflectionVector.ESp(vector);

            yield return Float64Vector3D.Create(vector.X - s1 * reflectionVector.X,
                vector.Y - s1 * reflectionVector.Y,
                vector.Z - s1 * reflectionVector.Z);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetDistanceSquaredToPoint(this IFloat64Vector3D p1, double p2X, double p2Y, double p2Z)
    {
        var vX = p2X - p1.X;
        var vY = p2Y - p1.Y;
        var vZ = p2Z - p1.Z;

        return vX * vX + vY * vY + vZ * vZ;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D SubtractLength(this IFloat64Vector3D vector, double length)
    {
        var oldLength = vector.ENorm();

        if (oldLength.IsAlmostZero())
            return Float64Vector3D.Zero;

        var scalingFactor =
            (oldLength - length) / oldLength;

        return Float64Vector3D.Create(vector.X * scalingFactor,
            vector.Y * scalingFactor,
            vector.Z * scalingFactor);
    }

    /// <summary>
    /// The Euclidean distance between the given vectors
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetDistanceToPoint(this IFloat64Vector3D p1, IFloat64Vector3D p2)
    {
        var vX = p2.X - p1.X;
        var vY = p2.Y - p1.Y;
        var vZ = p2.Z - p1.Z;

        return Math.Sqrt(vX * vX + vY * vY + vZ * vZ);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetDistanceToPoint(this IFloat64Vector3D p1, double p2X, double p2Y, double p2Z)
    {
        var vX = p2X - p1.X;
        var vY = p2Y - p1.Y;
        var vZ = p2Z - p1.Z;

        return Math.Sqrt(vX * vX + vY * vY + vZ * vZ);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D SetLength(this IFloat64Vector3D vector, double newLength)
    {
        var oldLength = vector.ENorm();

        if (oldLength.IsZero())
            return Float64Vector3D.Zero;

        var scalingFactor = newLength / oldLength;

        return Float64Vector3D.Create(
            vector.X * scalingFactor,
            vector.Y * scalingFactor,
            vector.Z * scalingFactor
        );
    }
}