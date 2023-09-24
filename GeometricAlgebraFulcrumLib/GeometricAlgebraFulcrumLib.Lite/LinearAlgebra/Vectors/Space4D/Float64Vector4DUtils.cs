using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Tuples;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;

public static class Float64Vector4DUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D ClampTo(this IFloat64Vector4D tuple, IFloat64Vector4D maxTuple)
    {
        return Float64Vector4D.Create(
            tuple.X.ClampTo(maxTuple.X),
            tuple.Y.ClampTo(maxTuple.Y),
            tuple.Z.ClampTo(maxTuple.Z),
            tuple.W.ClampTo(maxTuple.W)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D ClampTo(this IFloat64Vector4D tuple, IFloat64Vector4D minTuple, IFloat64Vector4D maxTuple)
    {
        return Float64Vector4D.Create(
            tuple.X.ClampTo(minTuple.X, maxTuple.X),
            tuple.Y.ClampTo(minTuple.Y, maxTuple.Y),
            tuple.Z.ClampTo(minTuple.Z, maxTuple.Z),
            tuple.W.ClampTo(minTuple.W, maxTuple.W)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D ClampToSymmetric(this IFloat64Vector4D tuple, IFloat64Vector4D maxTuple)
    {
        return Float64Vector4D.Create(
            tuple.X.ClampToSymmetric(maxTuple.X),
            tuple.Y.ClampToSymmetric(maxTuple.Y),
            tuple.Z.ClampToSymmetric(maxTuple.Z),
            tuple.W.ClampToSymmetric(maxTuple.W)
        );
    }
    

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetAngleCos(this IFloat64Vector4D v1, IFloat64Vector4D v2)
    {
        var t1 = v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z + v1.W * v2.W;
        var t2 = v1.X * v1.X + v1.Y * v1.Y + v1.Z * v1.Z + v1.W * v1.W;
        var t3 = v2.X * v2.X + v2.Y * v2.Y + v2.Z * v2.Z + v2.W * v2.W;

        return (t1 / Math.Sqrt(t2 * t3)).Clamp(-1d, 1d);
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetAngleCosWithUnit(this IFloat64Vector4D v1, IFloat64Vector4D v2)
    {
        Debug.Assert(
            v2.IsNearUnit()
        );

        var t1 =
            v1.X.Value * v2.X.Value +
            v1.Y.Value * v2.Y.Value +
            v1.Z.Value * v2.Z.Value +
            v1.W.Value * v2.W.Value;

        var t2 =
            v1.X.Value * v1.X.Value +
            v1.Y.Value * v1.Y.Value +
            v1.Z.Value * v1.Z.Value +
            v1.W.Value * v1.W.Value;

        return Float64Utils.Clamp((t1 / Math.Sqrt(t2)), -1d, 1d);
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle GetAngle(this IFloat64Vector4D v1, IFloat64Vector4D v2)
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
    public static double GetUnitVectorsAngleCos(this IFloat64Vector4D v1, IFloat64Vector4D v2)
    {
        Debug.Assert(
            v1.IsNearUnitVector() &&
            v2.IsNearUnitVector()
        );

        return Float64Utils.Clamp((v1.X.Value * v2.X.Value +
                                   v1.Y.Value * v2.Y.Value +
                                   v1.Z.Value * v2.Z.Value +
                                   v1.W.Value * v2.W.Value
                    ), -1, 1);
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle GetUnitVectorsAngle(this IFloat64Vector4D v1, IFloat64Vector4D v2)
    {
        return Math.Acos(
            v1.GetUnitVectorsAngleCos(v2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D ToTuple4D(this LinUnitBasisVector2D axis)
    {
        return axis switch
        {
            LinUnitBasisVector2D.PositiveX => Float64Vector4D.E1,
            LinUnitBasisVector2D.NegativeX => Float64Vector4D.NegativeE1,
            LinUnitBasisVector2D.PositiveY => Float64Vector4D.E2,
            _ => Float64Vector4D.NegativeE2
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D ToTuple4D(this LinUnitBasisVector3D axis)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => Float64Vector4D.E1,
            LinUnitBasisVector3D.NegativeX => Float64Vector4D.NegativeE1,
            LinUnitBasisVector3D.PositiveY => Float64Vector4D.E2,
            LinUnitBasisVector3D.NegativeY => Float64Vector4D.NegativeE2,
            LinUnitBasisVector3D.PositiveZ => Float64Vector4D.E3,
            _ => Float64Vector4D.NegativeE3
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsXAxis(this LinUnitBasisVector4D axis)
    {
        return axis is LinUnitBasisVector4D.PositiveX or LinUnitBasisVector4D.NegativeX;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsYAxis(this LinUnitBasisVector4D axis)
    {
        return axis is LinUnitBasisVector4D.PositiveY or LinUnitBasisVector4D.NegativeY;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZAxis(this LinUnitBasisVector4D axis)
    {
        return axis is LinUnitBasisVector4D.PositiveZ or LinUnitBasisVector4D.NegativeZ;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsWAxis(this LinUnitBasisVector4D axis)
    {
        return axis is LinUnitBasisVector4D.PositiveW or LinUnitBasisVector4D.NegativeW;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNegative(this LinUnitBasisVector4D axis)
    {
        return axis is
            LinUnitBasisVector4D.NegativeX or
            LinUnitBasisVector4D.NegativeY or
            LinUnitBasisVector4D.NegativeZ or
            LinUnitBasisVector4D.NegativeW;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOppositeTo(this LinUnitBasisVector4D axis1, LinUnitBasisVector4D axis2)
    {
        return axis1 switch
        {
            LinUnitBasisVector4D.PositiveX => axis2 == LinUnitBasisVector4D.NegativeX,
            LinUnitBasisVector4D.PositiveY => axis2 == LinUnitBasisVector4D.NegativeY,
            LinUnitBasisVector4D.PositiveZ => axis2 == LinUnitBasisVector4D.NegativeZ,
            LinUnitBasisVector4D.PositiveW => axis2 == LinUnitBasisVector4D.NegativeW,
            LinUnitBasisVector4D.NegativeX => axis2 == LinUnitBasisVector4D.PositiveX,
            LinUnitBasisVector4D.NegativeY => axis2 == LinUnitBasisVector4D.PositiveY,
            LinUnitBasisVector4D.NegativeZ => axis2 == LinUnitBasisVector4D.PositiveZ,
            _ => axis2 == LinUnitBasisVector4D.PositiveW
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnitBasisVector4D SelectNearestAxis(this IFloat64Vector4D unitVector)
    {
        return unitVector.GetMaxAbsComponentIndex() switch
        {
            0 => unitVector.X.IsPositive() 
                ? LinUnitBasisVector4D.PositiveX 
                : LinUnitBasisVector4D.NegativeX,

            1 => unitVector.Y.IsPositive() 
                ? LinUnitBasisVector4D.PositiveY 
                : LinUnitBasisVector4D.NegativeY,

            2 => unitVector.Z.IsPositive() 
                ? LinUnitBasisVector4D.PositiveZ 
                : LinUnitBasisVector4D.NegativeZ,

            _ => unitVector.Z.IsPositive() 
                ? LinUnitBasisVector4D.PositiveZ 
                : LinUnitBasisVector4D.NegativeW
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetIndex(this LinUnitBasisVector4D axis)
    {
        return axis switch
        {
            LinUnitBasisVector4D.PositiveX => 0,
            LinUnitBasisVector4D.NegativeX => 0,
            LinUnitBasisVector4D.PositiveY => 1,
            LinUnitBasisVector4D.NegativeY => 1,
            LinUnitBasisVector4D.PositiveZ => 2,
            LinUnitBasisVector4D.NegativeZ => 2,
            LinUnitBasisVector4D.PositiveW => 3,
            LinUnitBasisVector4D.NegativeW => 3,
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign GetSign(this LinUnitBasisVector4D axis)
    {
        return axis switch
        {
            LinUnitBasisVector4D.PositiveX => IntegerSign.Positive,
            LinUnitBasisVector4D.NegativeX => IntegerSign.Negative,
            LinUnitBasisVector4D.PositiveY => IntegerSign.Positive,
            LinUnitBasisVector4D.NegativeY => IntegerSign.Negative,
            LinUnitBasisVector4D.PositiveZ => IntegerSign.Positive,
            LinUnitBasisVector4D.NegativeZ => IntegerSign.Negative,
            LinUnitBasisVector4D.PositiveW => IntegerSign.Positive,
            LinUnitBasisVector4D.NegativeW => IntegerSign.Negative,
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnitBasisVector4D ToAxis4D(this int axisIndex, bool isNegative = false)
    {
        if (isNegative)
            return axisIndex switch
            {
                0 => LinUnitBasisVector4D.NegativeX,
                1 => LinUnitBasisVector4D.NegativeY,
                2 => LinUnitBasisVector4D.NegativeZ,
                3 => LinUnitBasisVector4D.PositiveW,
                _ => throw new IndexOutOfRangeException()
            };

        return axisIndex switch
        {
            0 => LinUnitBasisVector4D.PositiveX,
            1 => LinUnitBasisVector4D.PositiveY,
            2 => LinUnitBasisVector4D.PositiveZ,
            3 => LinUnitBasisVector4D.PositiveW,
            _ => throw new IndexOutOfRangeException()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D ToTuple4D(this LinUnitBasisVector4D axis)
    {
        return axis switch
        {
            LinUnitBasisVector4D.PositiveX => Float64Vector4D.E1,
            LinUnitBasisVector4D.NegativeX => Float64Vector4D.NegativeE1,
            LinUnitBasisVector4D.PositiveY => Float64Vector4D.E2,
            LinUnitBasisVector4D.NegativeY => Float64Vector4D.NegativeE2,
            LinUnitBasisVector4D.PositiveZ => Float64Vector4D.E3,
            LinUnitBasisVector4D.NegativeZ => Float64Vector4D.NegativeE3,
            LinUnitBasisVector4D.PositiveW => Float64Vector4D.E4,
            _ => Float64Vector4D.NegativeE4
        };
    }


    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector is near 
    /// zero it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D ToNegativeVector(this IFloat64Vector4D vector)
    {
        return Float64Vector4D.Create(-vector.X, -vector.Y, -vector.Z, -vector.W);
    }

    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector is near 
    /// zero it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D ToNegativeUnitVector(this IFloat64Vector4D vector)
    {
        var s = vector.ENorm();
        if (s.IsAlmostZero())
            return vector.ToTuple4D();

        s = 1.0d / s;
        return Float64Vector4D.Create(vector.X * s, vector.Y * s, vector.Z * s, vector.W * s);
    }

    /// <summary>
    /// The Euclidean distance between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetDistanceToPoint(this IFloat64Vector4D v1, IFloat64Vector4D v2)
    {
        var vX = v2.X - v1.X;
        var vY = v2.Y - v1.Y;
        var vZ = v2.Z - v1.Z;
        var vW = v2.W - v1.W;

        return Math.Sqrt(vX * vX + vY * vY + vZ * vZ + vW * vW);
    }

    /// <summary>
    /// The Euclidean distance between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetDistanceSquaredToPoint(this IFloat64Vector4D v1, IFloat64Vector4D v2)
    {
        var vX = v2.X - v1.X;
        var vY = v2.Y - v1.Y;
        var vZ = v2.Z - v1.Z;
        var vW = v2.W - v1.W;

        return vX * vX + vY * vY + vZ * vZ + vW * vW;
    }

    /// <summary>
    /// Returns a unit vector from the given one. If the length of the given vector is near zero
    /// it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="zeroAsSymmetric"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D ToUnitVector(this IFloat64Vector4D vector, bool zeroAsSymmetric = true)
    {
        var s = vector.ENorm();

        if (s.IsZero())
            return zeroAsSymmetric
                ? Float64Vector4D.UnitSymmetric
                : Float64Vector4D.Zero;

        s = 1.0d / s;
        return Float64Vector4D.Create(vector.X * s, vector.Y * s, vector.Z * s, vector.W * s);
    }


    /// <summary>
    /// The Euclidean squared length of this tuple when it represents a vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ENorm(this IFloat64Vector4D vector)
    {
        return Math.Sqrt(
            vector.X * vector.X +
            vector.Y * vector.Y +
            vector.Z * vector.Z +
            vector.W * vector.W
        );
    }

    /// <summary>
    /// The Euclidean squared length of this tuple when it represents a vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ENormSquared(this IFloat64Vector4D vector)
    {
        return vector.X * vector.X +
               vector.Y * vector.Y +
               vector.Z * vector.Z +
               vector.W * vector.W;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ENorm(double vectorX, double vectorY, double vectorZ, double vectorW)
    {
        return Math.Sqrt(
            vectorX * vectorX +
            vectorY * vectorY +
            vectorZ * vectorZ +
            vectorW * vectorW
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ENormSquared(double vectorX, double vectorY, double vectorZ, double vectorW)
    {
        return vectorX * vectorX +
               vectorY * vectorY +
               vectorZ * vectorZ +
               vectorW * vectorW;
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearUnitVector(this IFloat64Vector4D vector)
    {
        return vector
            .ENormSquared()
            .IsNearEqual(1.0d);
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ESp(this IFloat64Vector4D v1, LinUnitBasisVector4D v2)
    {
        return v2 switch
        {
            LinUnitBasisVector4D.PositiveX => v1.X,
            LinUnitBasisVector4D.PositiveY => v1.Y,
            LinUnitBasisVector4D.PositiveZ => v1.Z,
            LinUnitBasisVector4D.PositiveW => v1.W,
            LinUnitBasisVector4D.NegativeX => -v1.X,
            LinUnitBasisVector4D.NegativeY => -v1.Y,
            LinUnitBasisVector4D.NegativeZ => -v1.Z,
            _ => -v1.W,
        };
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near zero
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsAlmostZeroVector(this IFloat64Vector4D vector)
    {
        return vector
            .ENormSquared()
            .IsAlmostZero();
    }


    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUnitVector(this IFloat64Vector4D vector)
    {
        return vector
            .ENormSquared()
            .IsAlmostEqual(1.0d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D Add(this IFloat64Vector4D v1, IFloat64Vector4D v2)
    {
        return Float64Vector4D.Create(
            v1.X + v2.X,
            v1.Y + v2.Y,
            v1.Z + v2.Z,
            v1.W + v2.W
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D Subtract(this IFloat64Vector4D v1, IFloat64Vector4D v2)
    {
        return Float64Vector4D.Create(
            v1.X - v2.X,
            v1.Y - v2.Y,
            v1.Z - v2.Z,
            v1.W - v2.W
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D Times(this IFloat64Vector4D v1, double v2)
    {
        return Float64Vector4D.Create(
            v1.X * v2,
            v1.Y * v2,
            v1.Z * v2,
            v1.W * v2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D Times(this double v1, IFloat64Vector4D v2)
    {
        return Float64Vector4D.Create(
            v1 * v2.X,
            v1 * v2.Y,
            v1 * v2.Z,
            v1 * v2.W
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D Divide(this IFloat64Vector4D v1, double v2)
    {
        v2 = 1d / v2;

        return Float64Vector4D.Create(
            v1.X * v2,
            v1.Y * v2,
            v1.Z * v2,
            v1.W * v2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D GetFloat64Tuple4D(this System.Random random)
    {
        return Float64Vector4D.Create(
            random.NextDouble(),
            random.NextDouble(),
            random.NextDouble(),
            random.NextDouble()
        );
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="v3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Float64Scalar> ESp(this IFloat64Vector4D v1, IFloat64Vector4D v2, IFloat64Vector4D v3)
    {
        return new Pair<Float64Scalar>(
            v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z + v1.W * v2.W,
            v1.X * v3.X + v1.Y * v3.Y + v1.Z * v3.Z + v1.W * v3.W
        );
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar ESp(this IFloat64Vector4D v1, IFloat64Vector4D v2)
    {
        return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z + v1.W * v2.W;
    }

    /// <summary>
    /// The absolute value of the Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar ESpAbs(this IFloat64Vector4D v1, IFloat64Vector4D v2)
    {
        return Math.Abs(v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z + v1.W * v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D RejectOnUnitVector(this IFloat64Vector4D v, IFloat64Vector4D u)
    {
        var s = v.X * u.X + v.Y * u.Y + v.Z * u.Z + v.W * u.W;

        return Float64Vector4D.Create(
            v.X - u.X * s,
            v.Y - u.Y * s,
            v.Z - u.Z * s,
            v.W - u.W * s
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D ProjectOnVector(this IFloat64Vector4D v, IFloat64Vector4D u)
    {
        var s1 = v.X * u.X + v.Y * u.Y + v.Z * u.Z + v.W * u.W;
        var s2 = u.X * u.X + u.Y * u.Y + u.Z * u.Z + u.W * u.W;
        var s = s1 / s2;

        return Float64Vector4D.Create(
            u.X * s,
            u.Y * s,
            u.Z * s,
            u.W * s
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D ProjectOnUnitVector(this IFloat64Vector4D v, IFloat64Vector4D u)
    {
        var s = v.X * u.X + v.Y * u.Y + v.Z * u.Z + v.W * u.W;

        return Float64Vector4D.Create(
            u.X * s,
            u.Y * s,
            u.Z * s,
            u.W * s
        );
    }


    public static Float64Vector4D ToTuple4D(this IEnumerable<double> scalarList)
    {
        var scalarArray = new double[4];

        var i = 0;
        foreach (var scalar in scalarList)
            scalarArray[i++] = scalar;

        return Float64Vector4D.Create(
            scalarArray[0],
            scalarArray[1],
            scalarArray[2],
            scalarArray[3]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZero(this IFloat64Vector4D vector, double epsilon = 1e-12)
    {
        return vector.ENorm().IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearUnit(this IFloat64Vector4D vector, double epsilon = 1e-12)
    {
        return vector.ENormSquared().IsNearOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthonormalWith(this IFloat64Vector4D vector1, IFloat64Vector4D vector2, double epsilon = 1e-12)
    {
        return vector1.IsNearUnit(epsilon) &&
               vector2.IsNearUnit(epsilon) &&
               vector1.ESp(vector2).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthonormalWithUnit(this IFloat64Vector4D vector1, IFloat64Vector4D vector2, double epsilon = 1e-12)
    {
        Debug.Assert(
            vector2.IsNearUnit(epsilon)
        );

        return vector1.IsNearUnit(epsilon) &&
               vector1.ESp(vector2).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelTo(this IFloat64Vector4D vector1, IFloat64Vector4D vector2, double epsilon = 1e-12)
    {
        return vector1.GetAngleCos(vector2).Abs().IsNearOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelToUnit(this IFloat64Vector4D vector1, IFloat64Vector4D vector2, double epsilon = 1e-12)
    {
        return vector1.GetAngleCosWithUnit(vector2).Abs().IsNearOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthogonalTo(this IFloat64Vector4D vector1, IFloat64Vector4D vector2, double epsilon = 1e-12)
    {
        return vector1.ESp(vector2).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVectorBasis(this IFloat64Vector4D vector, int basisIndex)
    {
        return basisIndex switch
        {
            0 => vector.X.IsOne() && vector.Y.IsZero() && vector.Z.IsZero() && vector.W.IsZero(),
            1 => vector.X.IsZero() && vector.Y.IsOne() && vector.Z.IsZero() && vector.W.IsZero(),
            2 => vector.X.IsZero() && vector.Y.IsZero() && vector.Z.IsOne() && vector.W.IsZero(),
            3 => vector.X.IsZero() && vector.Y.IsZero() && vector.Z.IsZero() && vector.W.IsOne(),
            _ => false
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearVectorBasis(this IFloat64Vector4D vector, int basisIndex, double epsilon = 1e-12d)
    {
        var vector2 = basisIndex switch
        {
            0 => Float64Vector4D.E1,
            1 => Float64Vector4D.E2,
            2 => Float64Vector4D.E3,
            3 => Float64Vector4D.E4,
            _ => throw new InvalidOperationException()
        };

        return (vector - vector2).IsNearZero(epsilon);
    }

    public static Tuple<bool, double, LinUnitBasisVector4D> TryVectorToAxis(this Float64Vector4D vector)
    {
        // Find if the given scaling vector is parallel to a basis vector
        var basisIndex = -1;
        for (var i = 0; i < 4; i++)
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
            return new Tuple<bool, double, LinUnitBasisVector4D>(
                false,
                0d,
                LinUnitBasisVector4D.PositiveX
            );

        var scalar = vector.GetItem(basisIndex);

        return new Tuple<bool, double, LinUnitBasisVector4D>(
            true,
            scalar.Abs(),
            basisIndex.ToAxis4D(scalar < 0)
        );
    }

    public static Tuple<bool, double, LinUnitBasisVector4D> TryVectorToAxis(this IFloat64Vector4D vector)
    {
        // Find if the given scaling vector is parallel to a basis vector
        var basisIndex = -1;
        for (var i = 0; i < 4; i++)
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
            return new Tuple<bool, double, LinUnitBasisVector4D>(
                false,
                0d,
                LinUnitBasisVector4D.PositiveX
            );

        var scalar = vector.GetItem(basisIndex);

        return new Tuple<bool, double, LinUnitBasisVector4D>(
            true,
            scalar.Abs(),
            basisIndex.ToAxis4D(scalar < 0)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D Lerp(this double t, IFloat64Vector4D v1, IFloat64Vector4D v2)
    {
        var s = 1.0d - t;

        return Float64Vector4D.Create(s * v1.X + t * v2.X,
            s * v1.Y + t * v2.Y,
            s * v1.Z + t * v2.Z,
            s * v1.W + t * v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Float64Vector4D> Lerp(this IEnumerable<double> tList, IFloat64Vector4D v1, IFloat64Vector4D v2)
    {
        return tList.Select(t => t.Lerp(v1, v2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFinite(this IFloat64Vector4D tuple)
    {
        return !(
            double.IsNaN(tuple.X) || double.IsInfinity(tuple.X) ||
            double.IsNaN(tuple.Y) || double.IsInfinity(tuple.Y) ||
            double.IsNaN(tuple.Z) || double.IsInfinity(tuple.Z) ||
            double.IsNaN(tuple.W) || double.IsInfinity(tuple.W)
        );
    }

    /// <summary>
    /// The index of the largest absolute component in this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMaxAbsComponentIndex(this IFloat64Vector4D tuple)
    {
        var absX = Math.Abs(tuple.X);
        var absY = Math.Abs(tuple.Y);
        var absZ = Math.Abs(tuple.Z);
        var absW = Math.Abs(tuple.W);

        var index = 0;
        var maxValue = absX;

        if (maxValue < absY)
        {
            index = 1;
            maxValue = absY;
        }

        if (maxValue < absZ)
        {
            index = 2;
            maxValue = absZ;
        }

        if (maxValue < absW)
        {
            index = 3;
            maxValue = absW;
        }

        return index;
    }

    /// <summary>
    /// The index of the largest absolute component in this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMinAbsComponentIndex(this IFloat64Vector4D tuple)
    {
        var absX = Math.Abs(tuple.X);
        var absY = Math.Abs(tuple.Y);
        var absZ = Math.Abs(tuple.Z);
        var absW = Math.Abs(tuple.W);

        var index = 0;
        var minValue = absX;

        if (minValue > absY)
        {
            index = 1;
            minValue = absY;
        }

        if (minValue > absZ)
        {
            index = 2;
            minValue = absZ;
        }

        if (minValue > absW)
        {
            index = 3;
            minValue = absW;
        }

        return index;
    }

    /// <summary>
    /// Returns a new tuple containing component-wise minimum values of the given tuples
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D Min(this IFloat64Vector4D v1, IFloat64Vector4D v2)
    {
        return Float64Vector4D.Create(
            v1.X < v2.X ? v1.X : v2.X,
            v1.Y < v2.Y ? v1.Y : v2.Y,
            v1.Z < v2.Z ? v1.Z : v2.Z,
            v1.W < v2.W ? v1.W : v2.W
        );
    }

    /// <summary>
    /// Returns a new tuple containing component-wise maximum values of the given tuples
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D Max(this IFloat64Vector4D v1, IFloat64Vector4D v2)
    {
        return Float64Vector4D.Create(
            v1.X > v2.X ? v1.X : v2.X,
            v1.Y > v2.Y ? v1.Y : v2.Y,
            v1.Z > v2.Z ? v1.Z : v2.Z,
            v1.W > v2.W ? v1.W : v2.W
        );
    }

    /// <summary>
    /// Returns a new tuple containing component-wise ceiling values of the given tuple
    /// </summary>
    /// <param name="tuple"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D Ceiling(this IFloat64Vector4D tuple)
    {
        return Float64Vector4D.Create(
            Math.Ceiling(tuple.X),
            Math.Ceiling(tuple.Y),
            Math.Ceiling(tuple.Z),
            Math.Ceiling(tuple.W)
        );
    }

    /// <summary>
    /// Returns a new tuple containing component-wise floor values of the given tuple
    /// </summary>
    /// <param name="tuple"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D Floor(this IFloat64Vector4D tuple)
    {
        return Float64Vector4D.Create(
            Math.Floor(tuple.X),
            Math.Floor(tuple.Y),
            Math.Floor(tuple.Z),
            Math.Floor(tuple.W)
        );
    }

    /// <summary>
    /// Returns a new tuple containing component-wise absolute values of the given tuple
    /// </summary>
    /// <param name="tuple"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D Abs(this IFloat64Vector4D tuple)
    {
        return Float64Vector4D.Create(
            Math.Abs(tuple.X),
            Math.Abs(tuple.Y),
            Math.Abs(tuple.Z),
            Math.Abs(tuple.W)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D ToTuple4D(this IFloat64Vector2D tuple)
    {
        return Float64Vector4D.Create(
            tuple.X, 
            tuple.Y, 
            Float64Scalar.Zero, 
            Float64Scalar.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D ToTuple4D(this IntTuple2D tuple)
    {
        return Float64Vector4D.Create(tuple.X, tuple.Y, 0.0d, 0.0d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D ToTuple4D(this IFloat64Vector3D tuple)
    {
        return Float64Vector4D.Create(
            tuple.X, 
            tuple.Y, 
            tuple.Z, 
            Float64Scalar.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D ToTuple4D(this IFloat64Vector4D tuple)
    {
        return tuple is Float64Vector4D t
            ? t
            : Float64Vector4D.Create(tuple.X, tuple.Y, tuple.Z, tuple.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D ToTuple4D(this IntTuple3D tuple)
    {
        return Float64Vector4D.Create(tuple.ItemX, tuple.ItemY, tuple.ItemZ, 0.0d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D GetRealTuple(this IComplexVector4D tuple)
    {
        return Float64Vector4D.Create(
            tuple.X.Real,
            tuple.Y.Real,
            tuple.Z.Real,
            tuple.W.Real
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D GetImaginaryTuple(this IComplexVector4D tuple)
    {
        return Float64Vector4D.Create(
            tuple.X.Imaginary,
            tuple.Y.Imaginary,
            tuple.Z.Imaginary,
            tuple.W.Imaginary
        );
    }

    /// <summary>
    /// Returns a permuted version of the components of this tuple
    /// </summary>
    /// <param name="tuple"></param>
    /// <param name="xIndex"></param>
    /// <param name="yIndex"></param>
    /// <param name="zIndex"></param>
    /// <param name="wIndex"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D Permute(this Float64Vector4D tuple, int xIndex, int yIndex, int zIndex, int wIndex)
    {
        return Float64Vector4D.Create(
            tuple[xIndex], 
            tuple[yIndex], 
            tuple[zIndex], 
            tuple[wIndex]
        );
    }

    /// <summary>
    /// Returns a permuted version of the components of this tuple. The given indices are always
    /// converted to a valid range using modulus operation
    /// </summary>
    /// <param name="tuple"></param>
    /// <param name="xIndex"></param>
    /// <param name="yIndex"></param>
    /// <param name="zIndex"></param>
    /// <param name="wIndex"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D SafePermute(this Float64Vector4D tuple, int xIndex, int yIndex, int zIndex, int wIndex)
    {
        return Float64Vector4D.Create(
            tuple[xIndex.Mod(4)], 
            tuple[yIndex.Mod(4)], 
            tuple[zIndex.Mod(4)], 
            tuple[wIndex.Mod(4)]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetComponent(this IFloat64Vector4D tuple, int index)
    {
        return index switch
        {
            0 => tuple.X,
            1 => tuple.Y,
            2 => tuple.Z,
            3 => tuple.W,
            _ => 0d
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<double> GetComponents(this IFloat64Vector4D tuple)
    {
        yield return tuple.X;
        yield return tuple.Y;
        yield return tuple.Z;
        yield return tuple.W;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<double> GetComponents(this IEnumerable<IFloat64Vector4D> tuplesList)
    {
        return tuplesList.SelectMany(t => t.GetComponents());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D ComponentsProduct(this IFloat64Vector4D tuple1, IFloat64Vector4D tuple2)
    {
        return Float64Vector4D.Create(
            tuple1.X * tuple2.X,
            tuple1.Y * tuple2.Y,
            tuple1.Z * tuple2.Z,
            tuple1.W * tuple2.W
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D ComponentsProduct(this IFloat64Vector4D tuple1, IFloat64Vector4D tuple2, IFloat64Vector4D tuple3)
    {
        return Float64Vector4D.Create(
            tuple1.X * tuple2.X * tuple3.X,
            tuple1.Y * tuple2.Y * tuple3.Y,
            tuple1.Z * tuple2.Z * tuple3.Z,
            tuple1.W * tuple2.W * tuple3.W
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> GetTupleXPair(this IReadOnlyList<IFloat64Vector4D> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].X,
            itemArray[index + 1].X
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<double> GetTupleXTriplet(this IReadOnlyList<IFloat64Vector4D> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].X,
            itemArray[index + 1].X,
            itemArray[index + 2].X
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<double> GetTupleXQuad(this IReadOnlyList<IFloat64Vector4D> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].X,
            itemArray[index + 1].X,
            itemArray[index + 2].X,
            itemArray[index + 3].X
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<double> GetTupleXQuint(this IReadOnlyList<IFloat64Vector4D> itemArray, int index)
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
    public static Hexad<double> GetTupleXHexad(this IReadOnlyList<IFloat64Vector4D> itemArray, int index)
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
    public static Pair<double> GetTupleYPair(this IReadOnlyList<IFloat64Vector4D> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Y,
            itemArray[index + 1].Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<double> GetTupleYTriplet(this IReadOnlyList<IFloat64Vector4D> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Y,
            itemArray[index + 1].Y,
            itemArray[index + 2].Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<double> GetTupleYQuad(this IReadOnlyList<IFloat64Vector4D> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Y,
            itemArray[index + 1].Y,
            itemArray[index + 2].Y,
            itemArray[index + 3].Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<double> GetTupleYQuint(this IReadOnlyList<IFloat64Vector4D> itemArray, int index)
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
    public static Hexad<double> GetTupleYHexad(this IReadOnlyList<IFloat64Vector4D> itemArray, int index)
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
    public static Pair<double> GetTupleZPair(this IReadOnlyList<IFloat64Vector4D> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Z,
            itemArray[index + 1].Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<double> GetTupleZTriplet(this IReadOnlyList<IFloat64Vector4D> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Z,
            itemArray[index + 1].Z,
            itemArray[index + 2].Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<double> GetTupleZQuad(this IReadOnlyList<IFloat64Vector4D> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Z,
            itemArray[index + 1].Z,
            itemArray[index + 2].Z,
            itemArray[index + 3].Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<double> GetTupleZQuint(this IReadOnlyList<IFloat64Vector4D> itemArray, int index)
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
    public static Hexad<double> GetTupleZHexad(this IReadOnlyList<IFloat64Vector4D> itemArray, int index)
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
    public static Pair<double> GetTupleWPair(this IReadOnlyList<IFloat64Vector4D> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].W,
            itemArray[index + 1].W
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<double> GetTupleWTriplet(this IReadOnlyList<IFloat64Vector4D> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].W,
            itemArray[index + 1].W,
            itemArray[index + 2].W
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<double> GetTupleWQuad(this IReadOnlyList<IFloat64Vector4D> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].W,
            itemArray[index + 1].W,
            itemArray[index + 2].W,
            itemArray[index + 3].W
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<double> GetTupleWQuint(this IReadOnlyList<IFloat64Vector4D> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].W,
            itemArray[index + 1].W,
            itemArray[index + 2].W,
            itemArray[index + 3].W,
            itemArray[index + 4].W
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Hexad<double> GetTupleWHexad(this IReadOnlyList<IFloat64Vector4D> itemArray, int index)
    {
        return new Hexad<double>(
            itemArray[index].W,
            itemArray[index + 1].W,
            itemArray[index + 2].W,
            itemArray[index + 3].W,
            itemArray[index + 4].W,
            itemArray[index + 5].W
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D RotateToUnitVector(this IFloat64Vector4D vector1, IFloat64Vector4D unitVector, Float64PlanarAngle angle)
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