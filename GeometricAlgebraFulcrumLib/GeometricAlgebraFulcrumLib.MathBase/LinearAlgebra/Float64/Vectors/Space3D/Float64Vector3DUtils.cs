using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

public static class Float64Vector3DUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ToVector3D(this LinUnitBasisVector2D axis)
    {
        return axis switch
        {
            LinUnitBasisVector2D.PositiveX => Float64Vector3D.E1,
            LinUnitBasisVector2D.NegativeX => Float64Vector3D.NegativeE1,
            LinUnitBasisVector2D.PositiveY => Float64Vector3D.E2,
            _ => Float64Vector3D.NegativeE2
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ToVector3D(this LinUnitBasisVector3D axis)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => Float64Vector3D.E1,
            LinUnitBasisVector3D.NegativeX => Float64Vector3D.NegativeE1,
            LinUnitBasisVector3D.PositiveY => Float64Vector3D.E2,
            LinUnitBasisVector3D.NegativeY => Float64Vector3D.NegativeE2,
            LinUnitBasisVector3D.PositiveZ => Float64Vector3D.E3,
            _ => Float64Vector3D.NegativeE3
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ToVector3D(this LinUnitBasisVector3D axis, Float64Scalar scalingFactor)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => Float64Vector3D.Create(scalingFactor, 0, 0),
            LinUnitBasisVector3D.NegativeX => Float64Vector3D.Create(-scalingFactor, 0, 0),
            LinUnitBasisVector3D.PositiveY => Float64Vector3D.Create(0, scalingFactor, 0),
            LinUnitBasisVector3D.NegativeY => Float64Vector3D.Create(0, -scalingFactor, 0),
            LinUnitBasisVector3D.PositiveZ => Float64Vector3D.Create(0, 0, scalingFactor),
            _ => Float64Vector3D.Create(0, 0, -scalingFactor)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Bivector3D Dual3D(this LinUnitBasisVector3D axis)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => Float64Bivector3D.E32,
            LinUnitBasisVector3D.NegativeX => Float64Bivector3D.E23,
            LinUnitBasisVector3D.PositiveY => Float64Bivector3D.E13,
            LinUnitBasisVector3D.NegativeY => Float64Bivector3D.E31,
            LinUnitBasisVector3D.PositiveZ => Float64Bivector3D.E21,
            LinUnitBasisVector3D.NegativeZ => Float64Bivector3D.E12,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Bivector3D Dual3D(this LinUnitBasisVector3D axis, Float64Scalar scalingFactor)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => Float64Bivector3D.E32 * scalingFactor,
            LinUnitBasisVector3D.NegativeX => Float64Bivector3D.E23 * scalingFactor,
            LinUnitBasisVector3D.PositiveY => Float64Bivector3D.E13 * scalingFactor,
            LinUnitBasisVector3D.NegativeY => Float64Bivector3D.E31 * scalingFactor,
            LinUnitBasisVector3D.PositiveZ => Float64Bivector3D.E21 * scalingFactor,
            LinUnitBasisVector3D.NegativeZ => Float64Bivector3D.E12 * scalingFactor,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Bivector3D UnDual3D(this LinUnitBasisVector3D axis)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => Float64Bivector3D.E23,
            LinUnitBasisVector3D.NegativeX => Float64Bivector3D.E32,
            LinUnitBasisVector3D.PositiveY => Float64Bivector3D.E31,
            LinUnitBasisVector3D.NegativeY => Float64Bivector3D.E13,
            LinUnitBasisVector3D.PositiveZ => Float64Bivector3D.E12,
            LinUnitBasisVector3D.NegativeZ => Float64Bivector3D.E21,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Bivector3D UnDual3D(this LinUnitBasisVector3D axis, Float64Scalar scalingFactor)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => Float64Bivector3D.E23 * scalingFactor,
            LinUnitBasisVector3D.NegativeX => Float64Bivector3D.E32 * scalingFactor,
            LinUnitBasisVector3D.PositiveY => Float64Bivector3D.E31 * scalingFactor,
            LinUnitBasisVector3D.NegativeY => Float64Bivector3D.E13 * scalingFactor,
            LinUnitBasisVector3D.PositiveZ => Float64Bivector3D.E12 * scalingFactor,
            LinUnitBasisVector3D.NegativeZ => Float64Bivector3D.E21 * scalingFactor,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetAngleCosWithUnit(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
    {
        Debug.Assert(
            v2.IsNearUnit()
        );

        var t1 =
            v1.X.Value * v2.X.Value +
            v1.Y.Value * v2.Y.Value +
            v1.Z.Value * v2.Z.Value;

        var t2 = Math.Sqrt(
            v1.X.Value * v1.X.Value +
            v1.Y.Value * v1.Y.Value +
            v1.Z.Value * v1.Z.Value
        );

        return (t1 / t2).Clamp(-1d, 1d);
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle GetAngle(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
    {
        return v1.GetAngleCos(v2).ArcCos();
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle GetAngleWithUnit(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
    {
        return v1.GetAngleCosWithUnit(v2).ArcCos();
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetUnitVectorsAngleCos(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
    {
        Debug.Assert(
            v1.IsNearUnitVector() &&
            v2.IsNearUnitVector()
        );

        return (v1.X.Value * v2.X.Value +
                v1.Y.Value * v2.Y.Value +
                v1.Z.Value * v2.Z.Value
            ).Clamp(-1, 1);
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle GetUnitVectorsAngle(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
    {
        return v1.GetUnitVectorsAngleCos(v2).ArcCos();
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnitBasisVector3D GetAxis3D(this LinBasisVectorPair3D axisPair)
    {
        return axisPair switch
        {
            LinBasisVectorPair3D.Yz => LinUnitBasisVector3D.PositiveX,
            LinBasisVectorPair3D.Zy => LinUnitBasisVector3D.NegativeX,
            LinBasisVectorPair3D.Zx => LinUnitBasisVector3D.PositiveY,
            LinBasisVectorPair3D.Xz => LinUnitBasisVector3D.NegativeY,
            LinBasisVectorPair3D.Xy => LinUnitBasisVector3D.PositiveZ,
            _ => LinUnitBasisVector3D.NegativeZ
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVectorPair3D GetAxisPair3D(this LinUnitBasisVector3D axis)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => LinBasisVectorPair3D.Yz,
            LinUnitBasisVector3D.NegativeX => LinBasisVectorPair3D.Zy,
            LinUnitBasisVector3D.PositiveY => LinBasisVectorPair3D.Zx,
            LinUnitBasisVector3D.NegativeY => LinBasisVectorPair3D.Xz,
            LinUnitBasisVector3D.PositiveZ => LinBasisVectorPair3D.Xy,
            _ => LinBasisVectorPair3D.Yx
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsXAxis(this LinUnitBasisVector3D axis)
    {
        return axis is LinUnitBasisVector3D.PositiveX or LinUnitBasisVector3D.NegativeX;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsYAxis(this LinUnitBasisVector3D axis)
    {
        return axis is LinUnitBasisVector3D.PositiveY or LinUnitBasisVector3D.NegativeY;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZAxis(this LinUnitBasisVector3D axis)
    {
        return axis is LinUnitBasisVector3D.PositiveZ or LinUnitBasisVector3D.NegativeZ;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPositive(this LinUnitBasisVector3D axis)
    {
        return axis is LinUnitBasisVector3D.PositiveX or LinUnitBasisVector3D.PositiveY or LinUnitBasisVector3D.PositiveZ;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPositive(this LinUnitBasisVector4D axis)
    {
        return axis is
            LinUnitBasisVector4D.PositiveX or
            LinUnitBasisVector4D.PositiveY or
            LinUnitBasisVector4D.PositiveZ or
            LinUnitBasisVector4D.PositiveW;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNegative(this LinUnitBasisVector3D axis)
    {
        return axis is LinUnitBasisVector3D.NegativeX or LinUnitBasisVector3D.NegativeY or LinUnitBasisVector3D.NegativeZ;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOppositeTo(this LinUnitBasisVector3D axis1, LinUnitBasisVector3D axis2)
    {
        return axis1 switch
        {
            LinUnitBasisVector3D.PositiveX => axis2 == LinUnitBasisVector3D.NegativeX,
            LinUnitBasisVector3D.PositiveY => axis2 == LinUnitBasisVector3D.NegativeY,
            LinUnitBasisVector3D.PositiveZ => axis2 == LinUnitBasisVector3D.NegativeZ,
            LinUnitBasisVector3D.NegativeX => axis2 == LinUnitBasisVector3D.PositiveX,
            LinUnitBasisVector3D.NegativeY => axis2 == LinUnitBasisVector3D.PositiveY,
            _ => axis2 == LinUnitBasisVector3D.PositiveZ,
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnitBasisVector3D SelectNearestAxis(this IFloat64Tuple3D unitVector)
    {
        return unitVector.GetMaxAbsComponentIndex() switch
        {
            0 => unitVector.X.IsPositive() 
                ? LinUnitBasisVector3D.PositiveX 
                : LinUnitBasisVector3D.NegativeX,

            1 => unitVector.Y.IsPositive()
                ? LinUnitBasisVector3D.PositiveY 
                : LinUnitBasisVector3D.NegativeY,

            _ => unitVector.Z.IsPositive() 
                ? LinUnitBasisVector3D.PositiveZ 
                : LinUnitBasisVector3D.NegativeZ
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetIndex(this LinUnitBasisVector3D axis)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => 0,
            LinUnitBasisVector3D.NegativeX => 0,
            LinUnitBasisVector3D.PositiveY => 1,
            LinUnitBasisVector3D.NegativeY => 1,
            LinUnitBasisVector3D.PositiveZ => 2,
            LinUnitBasisVector3D.NegativeZ => 2,
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign GetSign(this LinUnitBasisVector3D axis)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => IntegerSign.Positive,
            LinUnitBasisVector3D.NegativeX => IntegerSign.Negative,
            LinUnitBasisVector3D.PositiveY => IntegerSign.Positive,
            LinUnitBasisVector3D.NegativeY => IntegerSign.Negative,
            LinUnitBasisVector3D.PositiveZ => IntegerSign.Positive,
            LinUnitBasisVector3D.NegativeZ => IntegerSign.Negative,
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnitBasisVector3D ToAxis3D(this int axisIndex, bool isNegative = false)
    {
        if (isNegative)
            return axisIndex switch
            {
                0 => LinUnitBasisVector3D.NegativeX,
                1 => LinUnitBasisVector3D.NegativeY,
                2 => LinUnitBasisVector3D.NegativeZ,
                _ => throw new IndexOutOfRangeException()
            };

        return axisIndex switch
        {
            0 => LinUnitBasisVector3D.PositiveX,
            1 => LinUnitBasisVector3D.PositiveY,
            2 => LinUnitBasisVector3D.PositiveZ,
            _ => throw new IndexOutOfRangeException()
        };
    }

    /// <summary>
    /// Find the angle between points (p1, p0, p2); i.e. p0 is the head of the angle
    /// </summary>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle GetPointsAngle(this IFloat64Tuple3D p0, IFloat64Tuple3D p1, IFloat64Tuple3D p2)
    {
        return Float64Vector3D.Create(p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z).GetAngle(
            Float64Vector3D.Create(p2.X - p0.X, p2.Y - p0.Y, p2.Z - p0.Z)
        );
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
    public static Float64Vector3D TranslateBy(this IFloat64Tuple3D vector, double translationX, double translationY, double translationZ)
    {
        return Float64Vector3D.Create(translationX + vector.X,
            translationY + vector.Y,
            translationZ + vector.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D TranslateBy(this IFloat64Tuple3D vector, IFloat64Tuple3D translationVector)
    {
        return Float64Vector3D.Create(translationVector.X + vector.X,
            translationVector.Y + vector.Y,
            translationVector.Z + vector.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ScaleBy(this IFloat64Tuple3D vector, double scaleFactor)
    {
        return Float64Vector3D.Create(scaleFactor * vector.X,
            scaleFactor * vector.Y,
            scaleFactor * vector.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ScaleBy(this IFloat64Tuple3D vector, double scaleFactorX, double scaleFactorY, double scaleFactorZ)
    {
        return Float64Vector3D.Create(scaleFactorX * vector.X,
            scaleFactorY * vector.Y,
            scaleFactorZ * vector.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ScaleBy(this IFloat64Tuple3D vector, IFloat64Tuple3D scaleFactorVector)
    {
        return Float64Vector3D.Create(scaleFactorVector.X * vector.X,
            scaleFactorVector.Y * vector.Y,
            scaleFactorVector.Z * vector.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D XRotateBy(this IFloat64Tuple3D vector, Float64PlanarAngle angle)
    {
        var cosAngle = Math.Cos(angle);
        var sinAngle = Math.Sin(angle);

        return Float64Vector3D.Create(vector.X,
            vector.Y * cosAngle - vector.Z * sinAngle,
            vector.Y * sinAngle + vector.Z * cosAngle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D YRotateBy(this IFloat64Tuple3D vector, Float64PlanarAngle angle)
    {
        var cosAngle = Math.Cos(angle);
        var sinAngle = Math.Sin(angle);

        return Float64Vector3D.Create(vector.X * cosAngle + vector.Z * sinAngle,
            vector.Y,
            -vector.X * sinAngle + vector.Z * cosAngle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ZRotateBy(this IFloat64Tuple3D vector, Float64PlanarAngle angle)
    {
        var cosAngle = Math.Cos(angle);
        var sinAngle = Math.Sin(angle);

        return Float64Vector3D.Create(vector.X * cosAngle - vector.Y * sinAngle,
            vector.X * sinAngle + vector.Y * cosAngle,
            vector.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D XRotateByDegrees(this IFloat64Tuple3D vector, Float64PlanarAngle angle)
    {
        return vector.XRotateBy(angle * Math.PI / 180);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D YRotateByDegrees(this IFloat64Tuple3D vector, Float64PlanarAngle angle)
    {
        return vector.YRotateBy(angle * Math.PI / 180);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ZRotateByDegrees(this IFloat64Tuple3D vector, Float64PlanarAngle angle)
    {
        return vector.ZRotateBy(angle * Math.PI / 180);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetTriangleNormal(IFloat64Tuple3D p1, IFloat64Tuple3D p2, IFloat64Tuple3D p3)
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
    public static Float64Vector3D GetTriangleUnitNormal(IFloat64Tuple3D p1, IFloat64Tuple3D p2, IFloat64Tuple3D p3)
    {
        //TODO: Test this for numerical stability, maybe select two sides with largest lengths
        var v12 = Float64Vector3D.Create(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
        var v23 = Float64Vector3D.Create(p3.X - p2.X, p3.Y - p2.Y, p3.Z - p2.Z);

        return v12.VectorUnitCross(v23);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetTriangleInverseUnitNormal(IFloat64Tuple3D p1, IFloat64Tuple3D p2, IFloat64Tuple3D p3)
    {
        //TODO: Test this for numerical stability, maybe select two sides with largest lengths
        var v12 = Float64Vector3D.Create(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
        var v23 = Float64Vector3D.Create(p3.X - p2.X, p3.Y - p2.Y, p3.Z - p2.Z);

        return v12.VectorUnitCross(v23);
    }


    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetAngleCos(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
    {
        var t1 = v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        var t2 = v1.X * v1.X + v1.Y * v1.Y + v1.Z * v1.Z;
        var t3 = v2.X * v2.X + v2.Y * v2.Y + v2.Z * v2.Z;

        return (t1 / Math.Sqrt(t2 * t3)).Clamp(-1d, 1d);
    }

    public static double ENormNormSquared(this IComplexTuple3D vector)
    {
        return (vector.X * vector.X.Conjugate()).Real +
               (vector.Y * vector.Y.Conjugate()).Real +
               (vector.Z * vector.Z.Conjugate()).Real;
    }

    /// <summary>
    /// The Euclidean squared length of this tuple when it represents a vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ENorm(this IFloat64Tuple3D vector)
    {
        return Math.Sqrt(
            vector.X * vector.X +
            vector.Y * vector.Y +
            vector.Z * vector.Z
        );
    }

    /// <summary>
    /// The Euclidean squared length of this tuple when it represents a vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<Float64Vector3D, double> GetUnitVectorENormTuple(this IFloat64Tuple3D vector)
    {
        var length = Math.Sqrt(
            vector.X * vector.X +
            vector.Y * vector.Y +
            vector.Z * vector.Z
        );

        if (length == 0d)
            return new Tuple<Float64Vector3D, double>(vector.ToVector3D(), length);

        var s = 1d / length;
        var unitVector = Float64Vector3D.Create(vector.X * s,
            vector.Y * s,
            vector.Z * s);

        return new Tuple<Float64Vector3D, double>(unitVector, length);
    }

    /// <summary>
    /// The Euclidean squared length of this tuple when it represents a vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ENormSquared(this IFloat64Tuple3D vector)
    {
        return vector.X * vector.X +
               vector.Y * vector.Y +
               vector.Z * vector.Z;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ENorm(double vectorX, double vectorY, double vectorZ)
    {
        return Math.Sqrt(
            vectorX * vectorX +
            vectorY * vectorY +
            vectorZ * vectorZ
        );
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUnitVector(this IFloat64Tuple3D vector)
    {
        return vector
            .ENormSquared()
            .IsAlmostEqual(1.0d);
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearUnitVector(this IFloat64Tuple3D vector)
    {
        return vector
            .ENormSquared()
            .IsNearEqual(1.0d);
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near zero
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroVector(this IFloat64Tuple3D vector)
    {
        return vector.ENormSquared().IsZero();
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near zero
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsAlmostZeroVector(this IFloat64Tuple3D vector)
    {
        return vector
            .ENormSquared()
            .IsAlmostZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearVector(this IFloat64Tuple3D vector1, IFloat64Tuple3D vector2)
    {
        return vector1.X.IsNearEqual(vector2.X) &&
               vector1.Y.IsNearEqual(vector2.Y) &&
               vector1.Z.IsNearEqual(vector2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearVectorNegative(this IFloat64Tuple3D vector1, IFloat64Tuple3D vector2)
    {
        return vector1.X.IsNearEqual(-vector2.X) &&
               vector1.Y.IsNearEqual(-vector2.Y) &&
               vector1.Z.IsNearEqual(-vector2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Determinant(this IFloat64Tuple3D v1, IFloat64Tuple3D v2, IFloat64Tuple3D v3)
    {
        return v1.X * (v2.Y * v3.Z - v2.Z * v3.Y) +
               v1.Y * (v2.Z * v3.X - v2.X * v3.Z) +
               v1.Z * (v2.X * v3.Y - v2.Y * v3.X);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ENormSquared(double vectorX, double vectorY, double vectorZ)
    {
        return vectorX * vectorX +
               vectorY * vectorY +
               vectorZ * vectorZ;
    }

    public static double ENorm(this IComplexTuple3D vector)
    {
        return Math.Sqrt(
            (vector.X * vector.X.Conjugate()).Real +
            (vector.Y * vector.Y.Conjugate()).Real +
            (vector.Z * vector.Z.Conjugate()).Real
        );
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ESp(this LinUnitBasisVector3D v1, IFloat64Tuple3D v2)
    {
        return v1 switch
        {
            LinUnitBasisVector3D.PositiveX => v2.X,
            LinUnitBasisVector3D.PositiveY => v2.Y,
            LinUnitBasisVector3D.PositiveZ => v2.Z,
            LinUnitBasisVector3D.NegativeX => -v2.X,
            LinUnitBasisVector3D.NegativeY => -v2.Y,
            _ => -v2.Z,
        };
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ESp(this IFloat64Tuple3D v1, LinUnitBasisVector3D v2)
    {
        return v2 switch
        {
            LinUnitBasisVector3D.PositiveX => v1.X,
            LinUnitBasisVector3D.PositiveY => v1.Y,
            LinUnitBasisVector3D.PositiveZ => v1.Z,
            LinUnitBasisVector3D.NegativeX => -v1.X,
            LinUnitBasisVector3D.NegativeY => -v1.Y,
            _ => -v1.Z,
        };
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ESp(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
    {
        return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="v3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> ESp(this IFloat64Tuple3D v1, IFloat64Tuple3D v2, IFloat64Tuple3D v3)
    {
        return new Pair<double>(
            v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z,
            v1.X * v3.X + v1.Y * v3.Y + v1.Z * v3.Z
        );
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
    public static Float64Vector3D GetNormal(this IFloat64Tuple3D vector)
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
    public static Pair<Float64Vector3D> GetNormalPair(this IFloat64Tuple3D vector)
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
    public static Complex ESp(this IComplexTuple3D v1, IComplexTuple3D v2)
    {
        return v1.X * v2.X.Conjugate() +
               v1.Y * v2.Y.Conjugate() +
               v1.Z * v2.Z.Conjugate();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex ESp(this IComplexTuple3D v1, IFloat64Tuple3D v2)
    {
        return v1.X * v2.X +
               v1.Y * v2.Y +
               v1.Z * v2.Z;
    }

    /// <summary>
    /// The absolute value of the Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar ESpAbs(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
    {
        return (v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z).Abs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ProjectOnVector(this IFloat64Tuple3D v, IFloat64Tuple3D u)
    {
        var s1 = v.X * u.X + v.Y * u.Y + v.Z * u.Z;
        var s2 = u.X * u.X + u.Y * u.Y + u.Z * u.Z;
        var s = s1 / s2;

        return Float64Vector3D.Create(u.X * s,
            u.Y * s,
            u.Z * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D RejectOnVector(this IFloat64Tuple3D v, IFloat64Tuple3D u)
    {
        var s1 = v.X * u.X + v.Y * u.Y + v.Z * u.Z;
        var s2 = u.X * u.X + u.Y * u.Y + u.Z * u.Z;
        var s = s1 / s2;

        return Float64Vector3D.Create(v.X - u.X * s,
            v.Y - u.Y * s,
            v.Z - u.Z * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ProjectOnUnitVector(this IFloat64Tuple3D v, IFloat64Tuple3D u)
    {
        var s = v.X * u.X + v.Y * u.Y + v.Z * u.Z;

        return Float64Vector3D.Create(u.X * s,
            u.Y * s,
            u.Z * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D RejectOnAxis(this IFloat64Tuple3D v, LinUnitBasisVector3D axis)
    {
        return axis.GetIndex() switch
        {
            0 => Float64Vector3D.Create(0, v.Y.Value, v.Z.Value),
            1 => Float64Vector3D.Create(v.X.Value, 0, v.Z.Value),
            _ => Float64Vector3D.Create(v.X.Value, v.Y.Value, 0)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D RejectOnUnitVector(this IFloat64Tuple3D v, IFloat64Tuple3D u)
    {
        var s = v.X * u.X + v.Y * u.Y + v.Z * u.Z;

        return Float64Vector3D.Create(v.X - u.X * s,
            v.Y - u.Y * s,
            v.Z - u.Z * s);
    }

    /// <summary>
    /// The Euclidean cross product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D VectorCross(this LinUnitBasisVector3D v1, IFloat64Tuple3D v2)
    {
        return v1 switch
        {
            LinUnitBasisVector3D.PositiveX => Float64Vector3D.Create(0, -v2.Z, v2.Y),
            LinUnitBasisVector3D.PositiveY => Float64Vector3D.Create(v2.Z, 0, -v2.X),
            LinUnitBasisVector3D.PositiveZ => Float64Vector3D.Create(-v2.Y, v2.X, 0),
            LinUnitBasisVector3D.NegativeX => Float64Vector3D.Create(0, v2.Z, -v2.Y),
            LinUnitBasisVector3D.NegativeY => Float64Vector3D.Create(-v2.Z, 0, v2.X),
            _ => Float64Vector3D.Create(v2.Y, -v2.X, 0)
        };
    }

    /// <summary>
    /// The Euclidean cross product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D VectorCross(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
    {
        return Float64Vector3D.Create(v1.Y * v2.Z - v1.Z * v2.Y,
            v1.Z * v2.X - v1.X * v2.Z,
            v1.X * v2.Y - v1.Y * v2.X);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetVectorCrossNorm(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
    {
        var x = v1.Y * v2.Z - v1.Z * v2.Y;
        var y = v1.Z * v2.X - v1.X * v2.Z;
        var z = v1.X * v2.Y - v1.Y * v2.X;

        return Math.Sqrt(x * x + y * y + z * z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetVectorCrossNormSquared(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
    {
        var x = v1.Y * v2.Z - v1.Z * v2.Y;
        var y = v1.Z * v2.X - v1.X * v2.Z;
        var z = v1.X * v2.Y - v1.Y * v2.X;

        return x * x + y * y + z * z;
    }

    /// <summary>
    /// Returns a copy of this vector if its dot product with the other vector is positive, else
    /// it returns the vector's negative
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="directionVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D FaceDirection(this IFloat64Tuple3D vector, IFloat64Tuple3D directionVector)
    {
        Debug.Assert(!directionVector.IsAlmostZeroVector());

        return
            (vector.X * directionVector.X + vector.Y * directionVector.Y + vector.Z * directionVector.Z).IsNegative()
                ? Float64Vector3D.Create(-vector.X, -vector.Y, -vector.Z)
                : Float64Vector3D.Create(vector.X, vector.Y, vector.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetUnitNormal(this IFloat64Tuple3D vector)
    {
        return vector.GetNormal();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Float64Vector3D> GetUnitNormalPair(this IFloat64Tuple3D vector)
    {
        return vector.GetNormalPair();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Add(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
    {
        return Float64Vector3D.Create(v1.X + v2.X,
            v1.Y + v2.Y,
            v1.Z + v2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Add(this IFloat64Tuple3D v1, IFloat64Tuple3D v2, IFloat64Tuple3D v3)
    {
        return Float64Vector3D.Create(v1.X + v2.X + v3.X,
            v1.Y + v2.Y + v3.Y,
            v1.Z + v2.Z + v3.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Add(this IFloat64Tuple3D v1, IFloat64Tuple3D v2, IFloat64Tuple3D v3, IFloat64Tuple3D v4)
    {
        return Float64Vector3D.Create(v1.X + v2.X + v3.X + v4.X,
            v1.Y + v2.Y + v3.Y + v4.Y,
            v1.Z + v2.Z + v3.Z + v4.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Subtract(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
    {
        return Float64Vector3D.Create(v1.X - v2.X,
            v1.Y - v2.Y,
            v1.Z - v2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Times(this IFloat64Tuple3D v1, double v2)
    {
        return Float64Vector3D.Create(v1.X * v2,
            v1.Y * v2,
            v1.Z * v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Times(this double v1, IFloat64Tuple3D v2)
    {
        return Float64Vector3D.Create(v1 * v2.X,
            v1 * v2.Y,
            v1 * v2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Divide(this IFloat64Tuple3D v1, double v2)
    {
        v2 = 1d / v2;

        return Float64Vector3D.Create(v1.X * v2,
            v1.Y * v2,
            v1.Z * v2);
    }

    public static Float64Vector3D GetCenterOfMassPoint(this IEnumerable<IFloat64Tuple3D> pointsList)
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

    public static Float64Vector3D GetCenterPoint(this IEnumerable<IFloat64Tuple3D> pointsList)
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
    public static Float64Vector3D GetDirectionTo(this IFloat64Tuple3D p1, IFloat64Tuple3D p2)
    {
        return Float64Vector3D.Create(p2.X - p1.X,
            p2.Y - p1.Y,
            p2.Z - p1.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetDirectionFrom(this IFloat64Tuple3D p2, double p1X, double p1Y, double p1Z)
    {
        return Float64Vector3D.Create(p2.X - p1X,
            p2.Y - p1Y,
            p2.Z - p1Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetDirectionFrom(this IFloat64Tuple3D p2, IFloat64Tuple3D p1)
    {
        return Float64Vector3D.Create(p2.X - p1.X,
            p2.Y - p1.Y,
            p2.Z - p1.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetUnitDirectionTo(this IFloat64Tuple3D p1, IFloat64Tuple3D p2)
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
    public static Float64Vector3D GetUnitDirectionFrom(this IFloat64Tuple3D p2, IFloat64Tuple3D p1)
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
    public static Float64Vector3D GetPointInDirection(this IFloat64Tuple3D p, IFloat64Tuple3D v)
    {
        return Float64Vector3D.Create(p.X + v.X,
            p.Y + v.Y,
            p.Z + v.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetPointInDirection(this IFloat64Tuple3D p, IFloat64Tuple3D v, double t)
    {
        return Float64Vector3D.Create(p.X + t * v.X,
            p.Y + t * v.Y,
            p.Z + t * v.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetLinVector3D(this System.Random random)
    {
        return Float64Vector3D.Create(random.NextDouble(),
            random.NextDouble(),
            random.NextDouble());
    }

    /// <summary>
    /// Returns the Euclidean cross product between the given vectors as a unit vector
    /// Both vectors are assumed to have z=0 components
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D VectorUnitCrossXy(this IFloat64Tuple2D v1, IFloat64Tuple2D v2)
    {
        var vz = v1.X * v2.Y - v1.Y * v2.X;

        return Float64Vector3D.Create(
            0d,
            0d,
            vz.IsNegative() 
                ? -1 
                : vz.IsPositive() ? 1 : 0);
    }

    /// <summary>
    /// Returns the Euclidean cross product between the given vectors as a unit vector
    /// The first vector is assumed to have z=0 while the second x=0 and y=0
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2Z"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D VectorUnitCrossXy_Z(this IFloat64Tuple2D v1, double v2Z)
    {
        var vx = v1.Y * v2Z;
        var vy = -v1.X * v2Z;

        var s = Math.Sqrt(vx * vx + vy * vy);

        if (s.IsZero())
            return Float64Vector3D.UnitSymmetric;

        s = 1.0d / s;

        return Float64Vector3D.Create(vx * s, vy * s, 0);
    }

    /// <summary>
    /// Returns the Euclidean cross product between the given vectors as a unit vector
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D VectorUnitCross(this LinUnitBasisVector3D v1, IFloat64Tuple3D v2)
    {
        return v1.VectorCross(v2).ToUnitVector();
    }

    /// <summary>
    /// Returns the Euclidean cross product between the given vectors as a unit vector
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D VectorUnitCross(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
    {
        var vx = v1.Y * v2.Z - v1.Z * v2.Y;
        var vy = v1.Z * v2.X - v1.X * v2.Z;
        var vz = v1.X * v2.Y - v1.Y * v2.X;

        var s = Math.Sqrt(vx * vx + vy * vy + vz * vz);

        if (s.IsZero())
            return Float64Vector3D.UnitSymmetric;

        s = 1.0d / s;
        return Float64Vector3D.Create(vx * s, vy * s, vz * s);
    }

    /// <summary>
    /// Returns the Euclidean cross product between the given vectors as a unit vector
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2X"></param>
    /// <param name="v2Y"></param>
    /// <param name="v2Z"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D VectorUnitCross(this IFloat64Tuple3D v1, double v2X, double v2Y, double v2Z)
    {
        var vx = v1.Y * v2Z - v1.Z * v2Y;
        var vy = v1.Z * v2X - v1.X * v2Z;
        var vz = v1.X * v2Y - v1.Y * v2X;

        var s = Math.Sqrt(vx * vx + vy * vy + vz * vz);

        if (s.IsZero())
            return Float64Vector3D.UnitSymmetric;

        s = 1.0d / s;
        return Float64Vector3D.Create(vx * s, vy * s, vz * s);
    }

    /// <summary>
    /// Returns the Euclidean cross product between the given vectors as a unit vector
    /// The second vector is assumed to have z=0
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2X"></param>
    /// <param name="v2Y"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D VectorUnitCrossXy(this IFloat64Tuple3D v1, double v2X, double v2Y)
    {
        var vx = -v1.Z * v2Y;
        var vy = v1.Z * v2X;
        var vz = v1.X * v2Y - v1.Y * v2X;

        var s = Math.Sqrt(vx * vx + vy * vy + vz * vz);

        if (s.IsZero())
            return Float64Vector3D.UnitSymmetric;

        s = 1.0d / s;
        return Float64Vector3D.Create(vx * s, vy * s, vz * s);
    }

    /// <summary>
    /// Returns the Euclidean cross product between the given vectors as a unit vector
    /// The second vector is assumed to have z=0
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D VectorUnitCrossXy(this IFloat64Tuple3D v1, IFloat64Tuple2D v2)
    {
        var vx = -v1.Z * v2.Y;
        var vy = v1.Z * v2.X;
        var vz = v1.X * v2.Y - v1.Y * v2.X;

        var s = Math.Sqrt(vx * vx + vy * vy + vz * vz);

        if (s.IsZero())
            return Float64Vector3D.UnitSymmetric;

        s = 1.0d / s;
        return Float64Vector3D.Create(vx * s, vy * s, vz * s);
    }


    /// <summary>
    /// Returns a unit vector from the given one. If the length of the given vector is near zero
    /// it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="zeroAsSymmetric"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ToUnitVector(this IFloat64Tuple3D vector, bool zeroAsSymmetric = true)
    {
        var s = vector.ENorm();

        if (s.IsZero())
            return zeroAsSymmetric
                ? Float64Vector3D.UnitSymmetric
                : Float64Vector3D.Zero;

        s = 1.0d / s;
        return Float64Vector3D.Create(vector.X * s, vector.Y * s, vector.Z * s);
    }

    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector is near 
    /// zero it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Negative(this IFloat64Tuple3D vector)
    {
        return Float64Vector3D.Create(-vector.X, -vector.Y, -vector.Z);
    }

    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector is near 
    /// zero it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ToNegativeUnitVector(this IFloat64Tuple3D vector)
    {
        var s = vector.ENorm();
        if (s.IsAlmostZero())
            return vector.ToVector3D();

        s = 1.0d / s;
        return Float64Vector3D.Create(vector.X * s, vector.Y * s, vector.Z * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<double, Float64Vector3D> ToLengthAndUnitDirection(this IFloat64Tuple3D vector)
    {
        var length = Math.Sqrt(
            vector.X * vector.X +
            vector.Y * vector.Y +
            vector.Z * vector.Z
        );

        var lengthInv = 1 / length;

        return Tuple.Create(
            length,
            Float64Vector3D.Create(vector.X * lengthInv,
                vector.Y * lengthInv,
                vector.Z * lengthInv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D AddLength(this IFloat64Tuple3D vector, double length)
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
    public static double GetDistanceSquaredToPoint(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
    {
        var vX = v2.X - v1.X;
        var vY = v2.Y - v1.Y;
        var vZ = v2.Z - v1.Z;

        return vX * vX + vY * vY + vZ * vZ;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ReflectVectorOnVector(this IFloat64Tuple3D reflectionVector, IFloat64Tuple3D vector)
    {
        var s = 2 * reflectionVector.ESp(vector) / reflectionVector.ENormSquared();

        return Float64Vector3D.Create(vector.X - s * reflectionVector.X,
            vector.Y - s * reflectionVector.Y,
            vector.Z - s * reflectionVector.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<Float64Vector3D> ReflectVectorsOnVector(this IFloat64Tuple3D reflectionVector, Triplet<IFloat64Tuple3D> vectorsTriplet)
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
    public static Pair<Float64Vector3D> ReflectVectorsOnVector(this IFloat64Tuple3D reflectionVector, IFloat64Tuple3D v1, IFloat64Tuple3D v2)
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
    public static Triplet<Float64Vector3D> ReflectVectorsOnVector(this IFloat64Tuple3D reflectionVector, IFloat64Tuple3D v1, IFloat64Tuple3D v2, IFloat64Tuple3D v3)
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

    public static IEnumerable<Float64Vector3D> ReflectVectorsOnVector(this IFloat64Tuple3D reflectionVector, params IFloat64Tuple3D[] vectorsList)
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
    public static Float64Vector3D ReflectVectorOnUnitVector(this IFloat64Tuple3D reflectionVector, IFloat64Tuple3D vector)
    {
        var s = 2 * reflectionVector.ESp(vector);

        return Float64Vector3D.Create(vector.X - s * reflectionVector.X,
            vector.Y - s * reflectionVector.Y,
            vector.Z - s * reflectionVector.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<Float64Vector3D> ReflectVectorsOnUnitVector(this IFloat64Tuple3D reflectionVector, Triplet<IFloat64Tuple3D> vectorsTriplet)
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

    public static IEnumerable<Float64Vector3D> ReflectVectorsOnUnitVector(this IFloat64Tuple3D reflectionVector, params IFloat64Tuple3D[] vectorsList)
    {
        foreach (var vector in vectorsList)
        {
            var s1 = 2 * reflectionVector.ESp(vector);

            yield return Float64Vector3D.Create(vector.X - s1 * reflectionVector.X,
                vector.Y - s1 * reflectionVector.Y,
                vector.Z - s1 * reflectionVector.Z);
        }
    }

    public static IEnumerable<Float64Vector3D> ReflectVectorsOnUnitVector(this IFloat64Tuple3D reflectionVector, IEnumerable<IFloat64Tuple3D> vectorsList)
    {
        foreach (var vector in vectorsList)
        {
            var s1 = 2 * reflectionVector.ESp(vector);

            yield return Float64Vector3D.Create(vector.X - s1 * reflectionVector.X,
                vector.Y - s1 * reflectionVector.Y,
                vector.Z - s1 * reflectionVector.Z);
        }
    }

    public static IEnumerable<Float64Vector3D> ReflectVectorsOnVector(this IFloat64Tuple3D reflectionVector, IEnumerable<IFloat64Tuple3D> vectorsList)
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
    public static double GetDistanceSquaredToPoint(this IFloat64Tuple3D p1, double p2X, double p2Y, double p2Z)
    {
        var vX = p2X - p1.X;
        var vY = p2Y - p1.Y;
        var vZ = p2Z - p1.Z;

        return vX * vX + vY * vY + vZ * vZ;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D SubtractLength(this IFloat64Tuple3D vector, double length)
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
    public static double GetDistanceToPoint(this IFloat64Tuple3D p1, IFloat64Tuple3D p2)
    {
        var vX = p2.X - p1.X;
        var vY = p2.Y - p1.Y;
        var vZ = p2.Z - p1.Z;

        return Math.Sqrt(vX * vX + vY * vY + vZ * vZ);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetDistanceToPoint(this IFloat64Tuple3D p1, double p2X, double p2Y, double p2Z)
    {
        var vX = p2X - p1.X;
        var vY = p2Y - p1.Y;
        var vZ = p2Z - p1.Z;

        return Math.Sqrt(vX * vX + vY * vY + vZ * vZ);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D SetLength(this IFloat64Tuple3D vector, double newLength)
    {
        var oldLength = vector.ENorm();

        if (oldLength.IsAlmostZero())
            return Float64Vector3D.Zero;

        var scalingFactor = newLength / oldLength;

        return Float64Vector3D.Create(vector.X * scalingFactor,
            vector.Y * scalingFactor,
            vector.Z * scalingFactor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ToUnitVector(double vectorX, double vectorY, double vectorZ, bool zeroAsSymmetric = true)
    {
        var s = ENorm(vectorX, vectorY, vectorZ);

        if (s.IsZero())
            return zeroAsSymmetric
                ? Float64Vector3D.UnitSymmetric
                : Float64Vector3D.Zero;

        s = 1.0d / s;
        return Float64Vector3D.Create(vectorX * s, vectorY * s, vectorZ * s);
    }

    public static Float64Vector3D ToVector3D(this IEnumerable<double> scalarList, bool makeUnit = false)
    {
        var scalarArray = new double[3];

        var i = 0;
        foreach (var scalar in scalarList)
            scalarArray[i++] = scalar;

        var tuple = Float64Vector3D.Create(scalarArray[0],
            scalarArray[1],
            scalarArray[2]);

        return makeUnit ? tuple.ToUnitVector() : tuple;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ToVector3D(this Float64SphericalUnitVector3D sphericalPosition)
    {
        var sinTheta = Math.Sin(sphericalPosition.Theta);

        return Float64Vector3D.Create(sinTheta * Math.Cos(sphericalPosition.Phi),
            sinTheta * Math.Sin(sphericalPosition.Phi),
            Math.Cos(sphericalPosition.Theta));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ToVector3D(this Float64SphericalUnitVector3D sphericalPosition, double r)
    {
        var rSinTheta = r * Math.Sin(sphericalPosition.Theta);

        return Float64Vector3D.Create(rSinTheta * Math.Cos(sphericalPosition.Phi),
            rSinTheta * Math.Sin(sphericalPosition.Phi),
            r * Math.Cos(sphericalPosition.Theta));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ToVector3D(this IFloat64SphericalVector3D sphericalPosition)
    {
        var rSinTheta = sphericalPosition.R * Math.Sin(sphericalPosition.Theta);

        return Float64Vector3D.Create(rSinTheta * Math.Cos(sphericalPosition.Phi),
            rSinTheta * Math.Sin(sphericalPosition.Phi),
            sphericalPosition.R * Math.Cos(sphericalPosition.Theta));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SphericalVector3D ToSphericalPosition(this IFloat64Tuple3D position)
    {
        var r = Math.Sqrt(
            position.X * position.X +
            position.Y * position.Y +
            position.Z * position.Z
        );

        return new Float64SphericalVector3D(
            Math.Acos(r / position.Z),
            Math.Atan2(position.Y, position.X),
            r
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SphericalUnitVector3D ToUnitSphericalPosition(this IFloat64Tuple3D position)
    {
        var r = Math.Sqrt(
            position.X * position.X +
            position.Y * position.Y +
            position.Z * position.Z
        );

        return new Float64SphericalUnitVector3D(
            Math.Acos(r / position.Z),
            Math.Atan2(position.Y, position.X)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SphericalUnitVector3D ToUnitSphericalPosition(this IFloat64SphericalVector3D sphericalPosition)
    {
        return new Float64SphericalUnitVector3D(
            sphericalPosition.Theta,
            sphericalPosition.Phi
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SphericalVector3D ToSphericalPosition(this Float64SphericalUnitVector3D sphericalPosition, double r)
    {
        return new Float64SphericalVector3D(
            sphericalPosition.Theta,
            sphericalPosition.Phi,
            r
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetUnitVectorR(this IFloat64SphericalVector3D sphericalPosition)
    {
        var sinTheta = Math.Sin(sphericalPosition.Theta);
        var cosTheta = Math.Cos(sphericalPosition.Theta);

        var sinPhi = Math.Sin(sphericalPosition.Phi);
        var cosPhi = Math.Cos(sphericalPosition.Phi);

        return Float64Vector3D.Create(sinTheta * cosPhi,
            sinTheta * sinPhi,
            cosTheta);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetUnitVectorR(this IFloat64Tuple3D vector)
    {
        var r = vector.ENorm();

        var cosTheta = r / vector.Z.Value;
        var sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);

        var phi = Math.Atan2(vector.Y, vector.X);
        var cosPhi = Math.Cos(phi);
        var sinPhi = Math.Sin(phi);

        return Float64Vector3D.Create(
            sinTheta * cosPhi,
            sinTheta * sinPhi,
            cosTheta
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetUnitVectorTheta(this IFloat64SphericalVector3D sphericalPosition)
    {
        var sinTheta = Math.Sin(sphericalPosition.Theta);
        var cosTheta = Math.Cos(sphericalPosition.Theta);

        var sinPhi = Math.Sin(sphericalPosition.Phi);
        var cosPhi = Math.Cos(sphericalPosition.Phi);

        return Float64Vector3D.Create(cosTheta * cosPhi,
            cosTheta * sinPhi,
            -sinTheta);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetUnitVectorTheta(this IFloat64Tuple3D vector)
    {
        var r = vector.ENorm();

        var cosTheta = vector.Z.Value / r;
        var sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);

        var phi = Math.Atan2(vector.Y, vector.X);
        var cosPhi = Math.Cos(phi);
        var sinPhi = Math.Sin(phi);

        return Float64Vector3D.Create(
            cosTheta * cosPhi,
            cosTheta * sinPhi,
            -sinTheta
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetUnitVectorPhi(this IFloat64SphericalVector3D sphericalPosition)
    {
        var sinPhi = Math.Sin(sphericalPosition.Phi);
        var cosPhi = Math.Cos(sphericalPosition.Phi);

        return Float64Vector3D.Create(-sinPhi, cosPhi, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetUnitVectorPhi(this IFloat64Tuple3D vector)
    {
        var phi = Math.Atan2(vector.Y, vector.X);
        var cosPhi = Math.Cos(phi);
        var sinPhi = Math.Sin(phi);

        return Float64Vector3D.Create(-sinPhi, cosPhi, 0);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZero(this IFloat64Tuple3D vector, double epsilon = 1e-12)
    {
        return vector.ENorm().IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearUnit(this IFloat64Tuple3D vector, double epsilon = 1e-12)
    {
        return vector.ENormSquared().IsNearOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthonormalWith(this IFloat64Tuple3D vector1, IFloat64Tuple3D vector2, double epsilon = 1e-12)
    {
        return vector1.IsNearUnit(epsilon) &&
               vector2.IsNearUnit(epsilon) &&
               vector1.ESp(vector2).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthonormalWithUnit(this IFloat64Tuple3D vector1, IFloat64Tuple3D vector2, double epsilon = 1e-12)
    {
        Debug.Assert(
            vector2.IsNearUnit(epsilon)
        );

        return vector1.IsNearUnit(epsilon) &&
               vector1.ESp(vector2).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelTo(this IFloat64Tuple3D vector1, IFloat64Tuple3D vector2, double epsilon = 1e-12)
    {
        return vector1.GetAngleCos(vector2).Abs().IsNearOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOppositeTo(this IFloat64Tuple3D vector1, IFloat64Tuple3D vector2, double epsilon = 1e-12)
    {
        return vector1.GetAngleCos(vector2).IsNearMinusOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelToUnit(this IFloat64Tuple3D vector1, IFloat64Tuple3D vector2, double epsilon = 1e-12)
    {
        return vector1.GetAngleCosWithUnit(vector2).Abs().IsNearOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOppositeToUnit(this IFloat64Tuple3D vector1, IFloat64Tuple3D vector2, double epsilon = 1e-12)
    {
        return vector1.GetAngleCosWithUnit(vector2).IsNearMinusOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthogonalTo(this IFloat64Tuple3D vector1, IFloat64Tuple3D vector2, double epsilon = 1e-12)
    {
        return vector1.ESp(vector2).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVectorBasis(this IFloat64Tuple3D vector, int basisIndex)
    {
        return basisIndex switch
        {
            0 => vector.X.IsOne() && vector.Y.IsZero() && vector.Z.IsZero(),
            1 => vector.X.IsZero() && vector.Y.IsOne() && vector.Z.IsZero(),
            2 => vector.X.IsZero() && vector.Y.IsZero() && vector.Z.IsOne(),
            _ => false
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearVectorBasis(this IFloat64Tuple3D vector, int basisIndex, double epsilon = 1e-12d)
    {
        var vector2 = basisIndex switch
        {
            0 => Float64Vector3D.E1,
            1 => Float64Vector3D.E2,
            2 => Float64Vector3D.E3,
            _ => throw new InvalidOperationException()
        };

        return (vector - vector2).IsNearZero(epsilon);
    }

    public static Tuple<bool, double, LinUnitBasisVector3D> TryVectorToAxis(this Float64Vector3D vector)
    {
        // Find if the given scaling vector is parallel to a basis vector
        var basisIndex = -1;
        for (var i = 0; i < 3; i++)
        {
            if (vector.GetItem(i).IsZero()) continue;

            if (basisIndex >= 0)
            {
                basisIndex = -2;
                break;
            }

            basisIndex = i;
        }

        if (basisIndex < 0)
            return new Tuple<bool, double, LinUnitBasisVector3D>(
                false,
                0d,
                LinUnitBasisVector3D.PositiveX
            );

        var scalar = vector.GetItem(basisIndex);

        return new Tuple<bool, double, LinUnitBasisVector3D>(
            true,
            scalar.Abs(),
            basisIndex.ToAxis3D(scalar < 0)
        );
    }

    public static Tuple<bool, double, LinUnitBasisVector3D> TryVectorToAxis(this IFloat64Tuple3D vector)
    {
        // Find if the given scaling vector is parallel to a basis vector
        var basisIndex = -1;
        for (var i = 0; i < 3; i++)
        {
            if (vector.GetItem(i).IsZero()) continue;

            if (basisIndex >= 0)
            {
                basisIndex = -2;
                break;
            }

            basisIndex = i;
        }

        if (basisIndex < 0)
            return new Tuple<bool, double, LinUnitBasisVector3D>(
                false,
                0d,
                LinUnitBasisVector3D.PositiveX
            );

        var scalar = vector.GetItem(basisIndex);

        return new Tuple<bool, double, LinUnitBasisVector3D>(
            true,
            scalar.Abs(),
            basisIndex.ToAxis3D(scalar < 0)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Lerp(this double t, IFloat64Tuple3D v1, IFloat64Tuple3D v2)
    {
        var s = 1.0d - t;

        return Float64Vector3D.Create(s * v1.X + t * v2.X,
            s * v1.Y + t * v2.Y,
            s * v1.Z + t * v2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Float64Vector3D> Lerp(this IEnumerable<double> tList, IFloat64Tuple3D v1, IFloat64Tuple3D v2)
    {
        return tList.Select(t => t.Lerp(v1, v2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFinite(this IFloat64Tuple3D tuple)
    {
        return tuple.X.IsFinite() &&
               tuple.Y.IsFinite() &&
               tuple.Z.IsFinite();
    }

    /// <summary>
    /// The value of the smallest component in this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetMinComponent(this ITriplet<double> tuple)
    {
        return tuple.Item1 < tuple.Item2
            ? tuple.Item1 < tuple.Item3 ? tuple.Item1 : tuple.Item3
            : tuple.Item2 < tuple.Item3 ? tuple.Item2 : tuple.Item3;
    }

    /// <summary>
    /// The value of the largest component in this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetMaxComponent(this ITriplet<double> tuple)
    {
        return tuple.Item1 > tuple.Item2
            ? tuple.Item1 > tuple.Item3 ? tuple.Item1 : tuple.Item3
            : tuple.Item2 > tuple.Item3 ? tuple.Item2 : tuple.Item3;
    }

    /// <summary>
    /// The index of the smallest component in this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMinComponentIndex(this ITriplet<double> tuple)
    {
        return tuple.Item1 < tuple.Item2 ? tuple.Item1 < tuple.Item3 ? 0 : 2 : tuple.Item2 < tuple.Item3 ? 1 : 2;
    }

    /// <summary>
    /// The index of the largest component in this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMaxComponentIndex(this ITriplet<double> tuple)
    {
        return tuple.Item1 > tuple.Item2 ? tuple.Item1 > tuple.Item3 ? 0 : 2 : tuple.Item2 > tuple.Item3 ? 1 : 2;
    }

    /// <summary>
    /// The index of the largest absolute component in this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMaxAbsComponentIndex(this ITriplet<double> tuple)
    {
        var absX = Math.Abs(tuple.Item1);
        var absY = Math.Abs(tuple.Item2);
        var absZ = Math.Abs(tuple.Item3);

        if (absX > absY)
            return absX > absZ ? 0 : 2;

        return absY > absZ ? 1 : 2;
    }

    /// <summary>
    /// The index of the largest absolute component in this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMinAbsComponentIndex(this ITriplet<double> tuple)
    {
        var absX = Math.Abs(tuple.Item1);
        var absY = Math.Abs(tuple.Item2);
        var absZ = Math.Abs(tuple.Item3);

        if (absX < absY)
            return absX < absZ ? 0 : 2;

        return absY < absZ ? 1 : 2;
    }

    /// <summary>
    /// Returns a new tuple containing component-wise minimum values of the given tuples
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Min(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
    {
        return Float64Vector3D.Create(v1.X < v2.X ? v1.X : v2.X,
            v1.Y < v2.Y ? v1.Y : v2.Y,
            v1.Z < v2.Z ? v1.Z : v2.Z);
    }

    /// <summary>
    /// Returns a new tuple containing component-wise maximum values of the given tuples
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Max(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
    {
        return Float64Vector3D.Create(v1.X > v2.X ? v1.X : v2.X,
            v1.Y > v2.Y ? v1.Y : v2.Y,
            v1.Z > v2.Z ? v1.Z : v2.Z);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ToVector3D(this ITriplet<double> tuple)
    {
        return tuple as Float64Vector3D
               ?? Float64Vector3D.Create(tuple.Item1,
                   tuple.Item2,
                   tuple.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ToVector3D(this ITriplet<double> tuple, Func<double, double> scalarMapping)
    {
        return Float64Vector3D.Create(scalarMapping(tuple.Item1),
            scalarMapping(tuple.Item2),
            scalarMapping(tuple.Item3));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ToVector3D<T>(this ITriplet<T> tuple, Func<T, double> scalarMapping)
    {
        return Float64Vector3D.Create(scalarMapping(tuple.Item1),
            scalarMapping(tuple.Item2),
            scalarMapping(tuple.Item3));
    }


    // TODO: Make more of these
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ScalarCeilingToLinVector3D(this ITriplet<double> tuple)
    {
        return tuple.ToVector3D(Math.Ceiling);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ScalarFloorToLinVector3D(this ITriplet<double> tuple)
    {
        return tuple.ToVector3D(Math.Floor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ScalarAbsToLinVector3D(this ITriplet<double> tuple)
    {
        return tuple.ToVector3D(Math.Abs);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3DComposer ToMutableTuple3D(this IFloat64Tuple3D tuple)
    {
        return Float64Vector3DComposer.Create(tuple.X, tuple.Y, tuple.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D XyToTuple3D(this IFloat64Tuple2D tuple)
    {
        return Float64Vector3D.Create(tuple.X, tuple.Y, 0.0d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D XyToTuple3D(this IntTuple2D tuple)
    {
        return Float64Vector3D.Create(tuple.X, tuple.Y, 0.0d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ToTuple3D(this IntTuple3D tuple)
    {
        return Float64Vector3D.Create(tuple.ItemX, tuple.ItemY, tuple.ItemZ);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D XyzToTuple3D(this IFloat64Tuple4D tuple)
    {
        return Float64Vector3D.Create(tuple.X, tuple.Y, tuple.Z);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DistinctTuplesList3D ToDistinctTuplesList(this IEnumerable<IFloat64Tuple3D> tuplesList)
    {
        return new DistinctTuplesList3D(tuplesList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetRealVector(this IComplexTuple3D tuple)
    {
        return Float64Vector3D.Create(tuple.X.Real,
            tuple.Y.Real,
            tuple.Z.Real);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetImaginaryVector(this IComplexTuple3D tuple)
    {
        return Float64Vector3D.Create(tuple.X.Imaginary,
            tuple.Y.Imaginary,
            tuple.Z.Imaginary);
    }

    /// <summary>
    /// Returns a permuted version of the components of this tuple
    /// </summary>
    /// <param name="tuple"></param>
    /// <param name="xIndex"></param>
    /// <param name="yIndex"></param>
    /// <param name="zIndex"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Permute(this Float64Vector3D tuple, int xIndex, int yIndex, int zIndex)
    {
        return Float64Vector3D.Create(tuple[1 << xIndex],
            tuple[1 << yIndex],
            tuple[1 << zIndex]);
    }

    /// <summary>
    /// Returns a permuted version of the components of this tuple. The given indices are always
    /// converted to a valid range using modulus operation
    /// </summary>
    /// <param name="tuple"></param>
    /// <param name="xIndex"></param>
    /// <param name="yIndex"></param>
    /// <param name="zIndex"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D SafePermute(this Float64Vector3D tuple, int xIndex, int yIndex, int zIndex)
    {
        return Float64Vector3D.Create(tuple[1 << xIndex.Mod(3)],
            tuple[1 << yIndex.Mod(3)],
            tuple[1 << zIndex.Mod(3)]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetComponent(this IFloat64Tuple3D tuple, int index)
    {
        return index switch
        {
            0 => tuple.X,
            1 => tuple.Y,
            2 => tuple.Z,
            _ => Float64Scalar.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetX(this LinUnitBasisVector3D tuple)
    {
        return tuple switch
        {
            LinUnitBasisVector3D.PositiveX => Float64Scalar.One,
            LinUnitBasisVector3D.NegativeX => Float64Scalar.NegativeOne,
            _ => Float64Scalar.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetY(this LinUnitBasisVector3D tuple)
    {
        return tuple switch
        {
            LinUnitBasisVector3D.PositiveY => Float64Scalar.One,
            LinUnitBasisVector3D.NegativeY => Float64Scalar.NegativeOne,
            _ => Float64Scalar.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetZ(this LinUnitBasisVector3D tuple)
    {
        return tuple switch
        {
            LinUnitBasisVector3D.PositiveZ => Float64Scalar.One,
            LinUnitBasisVector3D.NegativeZ => Float64Scalar.NegativeOne,
            _ => Float64Scalar.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetComponent(this LinUnitBasisVector3D tuple, LinUnitBasisVector3D axis)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => tuple.GetX(),
            LinUnitBasisVector3D.NegativeX => -tuple.GetX(),
            LinUnitBasisVector3D.PositiveY => tuple.GetY(),
            LinUnitBasisVector3D.NegativeY => -tuple.GetY(),
            LinUnitBasisVector3D.PositiveZ => tuple.GetZ(),
            LinUnitBasisVector3D.NegativeZ => -tuple.GetZ(),
            _ => Float64Scalar.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetComponent(this IFloat64Tuple3D tuple, LinUnitBasisVector3D axis)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => tuple.X,
            LinUnitBasisVector3D.NegativeX => -tuple.X,
            LinUnitBasisVector3D.PositiveY => tuple.Y,
            LinUnitBasisVector3D.NegativeY => -tuple.Y,
            LinUnitBasisVector3D.PositiveZ => tuple.Z,
            LinUnitBasisVector3D.NegativeZ => -tuple.Z,
            _ => Float64Scalar.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Float64Scalar> GetComponents(this IFloat64Tuple3D tuple)
    {
        yield return tuple.X;
        yield return tuple.Y;
        yield return tuple.Z;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Float64Scalar> GetComponents(this IEnumerable<IFloat64Tuple3D> tuplesList)
    {
        return tuplesList.SelectMany(t => t.GetComponents());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D MapComponents(this IFloat64Tuple3D tuple, Func<double, double> scalarMapping)
    {
        return Float64Vector3D.Create(scalarMapping(tuple.X),
            scalarMapping(tuple.Y),
            scalarMapping(tuple.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ComponentsMin(this IFloat64Tuple3D tuple, double scalar)
    {
        return Float64Vector3D.Create(Math.Min(tuple.X, scalar),
            Math.Min(tuple.Y, scalar),
            Math.Min(tuple.Z, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ComponentsMax(this IFloat64Tuple3D tuple, double scalar)
    {
        return Float64Vector3D.Create(Math.Max(tuple.X, scalar),
            Math.Max(tuple.Y, scalar),
            Math.Max(tuple.Z, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ComponentsMin(this IFloat64Tuple3D tuple)
    {
        return Math.Min(tuple.X, Math.Min(tuple.Y, tuple.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ComponentsMax(this IFloat64Tuple3D tuple)
    {
        return Math.Max(tuple.X, Math.Max(tuple.Y, tuple.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ComponentsAbs(this IFloat64Tuple3D tuple)
    {
        return Float64Vector3D.Create(Math.Abs(tuple.X),
            Math.Abs(tuple.Y),
            Math.Abs(tuple.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ComponentsProduct(this IFloat64Tuple3D tuple1, IFloat64Tuple3D tuple2)
    {
        return Float64Vector3D.Create(tuple1.X * tuple2.X,
            tuple1.Y * tuple2.Y,
            tuple1.Z * tuple2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ComponentsProduct(this IFloat64Tuple3D tuple1, IFloat64Tuple3D tuple2, IFloat64Tuple3D tuple3)
    {
        return Float64Vector3D.Create(tuple1.X * tuple2.X * tuple3.X,
            tuple1.Y * tuple2.Y * tuple3.Y,
            tuple1.Z * tuple2.Z * tuple3.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> GetTupleXPair(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].X,
            itemArray[index + 1].X
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<double> GetTupleXTriplet(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].X,
            itemArray[index + 1].X,
            itemArray[index + 2].X
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<double> GetTupleXQuad(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].X,
            itemArray[index + 1].X,
            itemArray[index + 2].X,
            itemArray[index + 3].X
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<double> GetTupleXQuint(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].X,
            itemArray[index + 1].X,
            itemArray[index + 2].X,
            itemArray[index + 3].X,
            itemArray[index + 4].X
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Hexad<double> GetTupleXHexad(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
    {
        return new Hexad<double>(
            itemArray[index].X,
            itemArray[index + 1].X,
            itemArray[index + 2].X,
            itemArray[index + 3].X,
            itemArray[index + 4].X,
            itemArray[index + 5].X
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> GetTupleYPair(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Y,
            itemArray[index + 1].Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<double> GetTupleYTriplet(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Y,
            itemArray[index + 1].Y,
            itemArray[index + 2].Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<double> GetTupleYQuad(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Y,
            itemArray[index + 1].Y,
            itemArray[index + 2].Y,
            itemArray[index + 3].Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<double> GetTupleYQuint(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].Y,
            itemArray[index + 1].Y,
            itemArray[index + 2].Y,
            itemArray[index + 3].Y,
            itemArray[index + 4].Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Hexad<double> GetTupleYHexad(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
    {
        return new Hexad<double>(
            itemArray[index].Y,
            itemArray[index + 1].Y,
            itemArray[index + 2].Y,
            itemArray[index + 3].Y,
            itemArray[index + 4].Y,
            itemArray[index + 5].Y
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> GetTupleZPair(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Z,
            itemArray[index + 1].Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<double> GetTupleZTriplet(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Z,
            itemArray[index + 1].Z,
            itemArray[index + 2].Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<double> GetTupleZQuad(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Z,
            itemArray[index + 1].Z,
            itemArray[index + 2].Z,
            itemArray[index + 3].Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<double> GetTupleZQuint(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].Z,
            itemArray[index + 1].Z,
            itemArray[index + 2].Z,
            itemArray[index + 3].Z,
            itemArray[index + 4].Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Hexad<double> GetTupleZHexad(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
    {
        return new Hexad<double>(
            itemArray[index].Z,
            itemArray[index + 1].Z,
            itemArray[index + 2].Z,
            itemArray[index + 3].Z,
            itemArray[index + 4].Z,
            itemArray[index + 5].Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D RotateToUnitVector(this IFloat64Tuple3D vector1, IFloat64Tuple3D unitVector, Float64PlanarAngle angle)
    {
        Debug.Assert(
            vector1.IsNearUnit() &&
            unitVector.IsNearUnit()
        );

        // Create a unit normal to u in the u-v rotational plane
        var v1 = unitVector.Subtract(vector1.Times(unitVector.ESp(vector1)));
        var v1Length = v1.ENorm();

        Debug.Assert(
            !v1Length.IsNearZero()
        );

        // Compute a rotated version of v in the u-v rotational plane by the given angle
        return vector1
            .Times(angle.Cos())
            .Add(v1.Times(angle.Sin() / v1Length));
    }
}