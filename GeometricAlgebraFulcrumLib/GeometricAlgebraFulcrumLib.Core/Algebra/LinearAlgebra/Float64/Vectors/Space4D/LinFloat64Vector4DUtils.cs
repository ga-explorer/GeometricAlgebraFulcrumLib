using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Tuples;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space4D;

public static class LinFloat64Vector4DUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D ClampTo(this IQuad<Float64Scalar> tuple, IQuad<Float64Scalar> maxTuple)
    {
        return LinFloat64Vector4D.Create(
            tuple.Item1.ClampTo(maxTuple.Item1),
            tuple.Item2.ClampTo(maxTuple.Item2),
            tuple.Item3.ClampTo(maxTuple.Item3),
            tuple.Item4.ClampTo(maxTuple.Item4)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D ClampTo(this IQuad<Float64Scalar> tuple, IQuad<Float64Scalar> minTuple, IQuad<Float64Scalar> maxTuple)
    {
        return LinFloat64Vector4D.Create(
            tuple.Item1.ClampTo(minTuple.Item1, maxTuple.Item1),
            tuple.Item2.ClampTo(minTuple.Item2, maxTuple.Item2),
            tuple.Item3.ClampTo(minTuple.Item3, maxTuple.Item3),
            tuple.Item4.ClampTo(minTuple.Item4, maxTuple.Item4)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D ClampToSymmetric(this IQuad<Float64Scalar> tuple, IQuad<Float64Scalar> maxTuple)
    {
        return LinFloat64Vector4D.Create(
            tuple.Item1.ClampToSymmetric(maxTuple.Item1),
            tuple.Item2.ClampToSymmetric(maxTuple.Item2),
            tuple.Item3.ClampToSymmetric(maxTuple.Item3),
            tuple.Item4.ClampToSymmetric(maxTuple.Item4)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D ToLinVector4D(this LinUnitBasisVector2D axis)
    {
        return axis switch
        {
            LinUnitBasisVector2D.PositiveX => LinFloat64Vector4D.E1,
            LinUnitBasisVector2D.NegativeX => LinFloat64Vector4D.NegativeE1,
            LinUnitBasisVector2D.PositiveY => LinFloat64Vector4D.E2,
            _ => LinFloat64Vector4D.NegativeE2
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D ToLinVector4D(this LinUnitBasisVector3D axis)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => LinFloat64Vector4D.E1,
            LinUnitBasisVector3D.NegativeX => LinFloat64Vector4D.NegativeE1,
            LinUnitBasisVector3D.PositiveY => LinFloat64Vector4D.E2,
            LinUnitBasisVector3D.NegativeY => LinFloat64Vector4D.NegativeE2,
            LinUnitBasisVector3D.PositiveZ => LinFloat64Vector4D.E3,
            _ => LinFloat64Vector4D.NegativeE3
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
    public static LinUnitBasisVector4D SelectNearestAxis(this IQuad<Float64Scalar> unitVector)
    {
        return unitVector.GetMaxAbsComponentIndex() switch
        {
            0 => unitVector.Item1.IsPositive()
                ? LinUnitBasisVector4D.PositiveX
                : LinUnitBasisVector4D.NegativeX,

            1 => unitVector.Item2.IsPositive()
                ? LinUnitBasisVector4D.PositiveY
                : LinUnitBasisVector4D.NegativeY,

            2 => unitVector.Item3.IsPositive()
                ? LinUnitBasisVector4D.PositiveZ
                : LinUnitBasisVector4D.NegativeZ,

            _ => unitVector.Item3.IsPositive()
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
    public static LinFloat64Vector4D ToLinVector4D(this LinUnitBasisVector4D axis)
    {
        return axis switch
        {
            LinUnitBasisVector4D.PositiveX => LinFloat64Vector4D.E1,
            LinUnitBasisVector4D.NegativeX => LinFloat64Vector4D.NegativeE1,
            LinUnitBasisVector4D.PositiveY => LinFloat64Vector4D.E2,
            LinUnitBasisVector4D.NegativeY => LinFloat64Vector4D.NegativeE2,
            LinUnitBasisVector4D.PositiveZ => LinFloat64Vector4D.E3,
            LinUnitBasisVector4D.NegativeZ => LinFloat64Vector4D.NegativeE3,
            LinUnitBasisVector4D.PositiveW => LinFloat64Vector4D.E4,
            _ => LinFloat64Vector4D.NegativeE4
        };
    }


    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector is near 
    /// zero it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D VectorNegative(this IQuad<Float64Scalar> vector)
    {
        return LinFloat64Vector4D.Create(-vector.Item1, -vector.Item2, -vector.Item3, -vector.Item4);
    }

    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector is near 
    /// zero it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D VectorNegativeUnit(this IQuad<Float64Scalar> vector)
    {
        var s = vector.VectorENorm();
        if (s.IsNearZero())
            return ToLinVector4D(vector);

        s = 1.0d / s;
        return LinFloat64Vector4D.Create(vector.Item1 * s, vector.Item2 * s, vector.Item3 * s, vector.Item4 * s);
    }

    /// <summary>
    /// The Euclidean distance between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetDistanceToPoint(this IQuad<Float64Scalar> v1, IQuad<Float64Scalar> v2)
    {
        var vX = v2.Item1 - v1.Item1;
        var vY = v2.Item2 - v1.Item2;
        var vZ = v2.Item3 - v1.Item3;
        var vW = v2.Item4 - v1.Item4;

        return Math.Sqrt(vX * vX + vY * vY + vZ * vZ + vW * vW);
    }

    /// <summary>
    /// The Euclidean distance between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetDistanceSquaredToPoint(this IQuad<Float64Scalar> v1, IQuad<Float64Scalar> v2)
    {
        var vX = v2.Item1 - v1.Item1;
        var vY = v2.Item2 - v1.Item2;
        var vZ = v2.Item3 - v1.Item3;
        var vW = v2.Item4 - v1.Item4;

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
    public static LinFloat64Vector4D ToUnitLinVector4D(this IQuad<Float64Scalar> vector, bool zeroAsSymmetric = true)
    {
        var s = vector.VectorENorm();

        if (s.IsZero())
            return zeroAsSymmetric
                ? LinFloat64Vector4D.UnitSymmetric
                : LinFloat64Vector4D.Zero;

        s = 1.0d / s;
        return LinFloat64Vector4D.Create(vector.Item1 * s, vector.Item2 * s, vector.Item3 * s, vector.Item4 * s);
    }


    /// <summary>
    /// The Euclidean squared length of this tuple when it represents a vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double VectorENorm(this IQuad<Float64Scalar> vector)
    {
        return Math.Sqrt(
            vector.Item1 * vector.Item1 +
            vector.Item2 * vector.Item2 +
            vector.Item3 * vector.Item3 +
            vector.Item4 * vector.Item4
        );
    }

    /// <summary>
    /// The Euclidean squared length of this tuple when it represents a vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double VectorENormSquared(this IQuad<Float64Scalar> vector)
    {
        return vector.Item1 * vector.Item1 +
               vector.Item2 * vector.Item2 +
               vector.Item3 * vector.Item3 +
               vector.Item4 * vector.Item4;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double VectorENorm(double vectorX, double vectorY, double vectorZ, double vectorW)
    {
        return Math.Sqrt(
            vectorX * vectorX +
            vectorY * vectorY +
            vectorZ * vectorZ +
            vectorW * vectorW
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double VectorENormSquared(double vectorX, double vectorY, double vectorZ, double vectorW)
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
    public static bool IsNearUnitVector(this IQuad<Float64Scalar> vector)
    {
        return vector
            .VectorENormSquared()
            .IsNearEqual(1.0d);
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double VectorESp(this IQuad<Float64Scalar> v1, LinUnitBasisVector4D v2)
    {
        return v2 switch
        {
            LinUnitBasisVector4D.PositiveX => v1.Item1,
            LinUnitBasisVector4D.PositiveY => v1.Item2,
            LinUnitBasisVector4D.PositiveZ => v1.Item3,
            LinUnitBasisVector4D.PositiveW => v1.Item4,
            LinUnitBasisVector4D.NegativeX => -v1.Item1,
            LinUnitBasisVector4D.NegativeY => -v1.Item2,
            LinUnitBasisVector4D.NegativeZ => -v1.Item3,
            _ => -v1.Item4,
        };
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near zero
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZeroVector(this IQuad<Float64Scalar> vector)
    {
        return vector
            .VectorENormSquared()
            .IsNearZero();
    }


    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUnitVector(this IQuad<Float64Scalar> vector)
    {
        return vector
            .VectorENormSquared()
            .IsNearOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D VectorAdd(this IQuad<Float64Scalar> v1, IQuad<Float64Scalar> v2)
    {
        return LinFloat64Vector4D.Create(
            v1.Item1 + v2.Item1,
            v1.Item2 + v2.Item2,
            v1.Item3 + v2.Item3,
            v1.Item4 + v2.Item4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D VectorSubtract(this IQuad<Float64Scalar> v1, IQuad<Float64Scalar> v2)
    {
        return LinFloat64Vector4D.Create(
            v1.Item1 - v2.Item1,
            v1.Item2 - v2.Item2,
            v1.Item3 - v2.Item3,
            v1.Item4 - v2.Item4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D VectorTimes(this IQuad<Float64Scalar> v1, double v2)
    {
        return LinFloat64Vector4D.Create(
            v1.Item1 * v2,
            v1.Item2 * v2,
            v1.Item3 * v2,
            v1.Item4 * v2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D VectorTimes(this double v1, IQuad<Float64Scalar> v2)
    {
        return LinFloat64Vector4D.Create(
            v1 * v2.Item1,
            v1 * v2.Item2,
            v1 * v2.Item3,
            v1 * v2.Item4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D VectorDivide(this IQuad<Float64Scalar> v1, double v2)
    {
        v2 = 1d / v2;

        return LinFloat64Vector4D.Create(
            v1.Item1 * v2,
            v1.Item2 * v2,
            v1.Item3 * v2,
            v1.Item4 * v2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D GetLinVector4D(this Random random)
    {
        return LinFloat64Vector4D.Create(
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
    public static Pair<Float64Scalar> VectorESp(this IQuad<Float64Scalar> v1, IQuad<Float64Scalar> v2, IQuad<Float64Scalar> v3)
    {
        return new Pair<Float64Scalar>(
            v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3 + v1.Item4 * v2.Item4,
            v1.Item1 * v3.Item1 + v1.Item2 * v3.Item2 + v1.Item3 * v3.Item3 + v1.Item4 * v3.Item4
        );
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar VectorESp(this IQuad<Float64Scalar> v1, IQuad<Float64Scalar> v2)
    {
        return v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3 + v1.Item4 * v2.Item4;
    }

    /// <summary>
    /// The absolute value of the Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar VectorESpAbs(this IQuad<Float64Scalar> v1, IQuad<Float64Scalar> v2)
    {
        return Math.Abs(v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3 + v1.Item4 * v2.Item4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D RejectOnUnitVector(this IQuad<Float64Scalar> v, IQuad<Float64Scalar> u)
    {
        var s = v.Item1 * u.Item1 + v.Item2 * u.Item2 + v.Item3 * u.Item3 + v.Item4 * u.Item4;

        return LinFloat64Vector4D.Create(
            v.Item1 - u.Item1 * s,
            v.Item2 - u.Item2 * s,
            v.Item3 - u.Item3 * s,
            v.Item4 - u.Item4 * s
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D ProjectOnVector(this IQuad<Float64Scalar> v, IQuad<Float64Scalar> u)
    {
        var s1 = v.Item1 * u.Item1 + v.Item2 * u.Item2 + v.Item3 * u.Item3 + v.Item4 * u.Item4;
        var s2 = u.Item1 * u.Item1 + u.Item2 * u.Item2 + u.Item3 * u.Item3 + u.Item4 * u.Item4;
        var s = s1 / s2;

        return LinFloat64Vector4D.Create(
            u.Item1 * s,
            u.Item2 * s,
            u.Item3 * s,
            u.Item4 * s
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D ProjectOnUnitVector(this IQuad<Float64Scalar> v, IQuad<Float64Scalar> u)
    {
        var s = v.Item1 * u.Item1 + v.Item2 * u.Item2 + v.Item3 * u.Item3 + v.Item4 * u.Item4;

        return LinFloat64Vector4D.Create(
            u.Item1 * s,
            u.Item2 * s,
            u.Item3 * s,
            u.Item4 * s
        );
    }


    public static LinFloat64Vector4D ToLinVector4D(this IEnumerable<double> scalarList)
    {
        var scalarArray = new double[4];

        var i = 0;
        foreach (var scalar in scalarList)
            scalarArray[i++] = scalar;

        return LinFloat64Vector4D.Create(
            scalarArray[0],
            scalarArray[1],
            scalarArray[2],
            scalarArray[3]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZero(this IQuad<Float64Scalar> vector, double epsilon = 1e-12)
    {
        return vector.VectorENorm().IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearUnit(this IQuad<Float64Scalar> vector, double epsilon = 1e-12)
    {
        return vector.VectorENormSquared().IsNearOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthonormalWith(this IQuad<Float64Scalar> vector1, IQuad<Float64Scalar> vector2, double epsilon = 1e-12)
    {
        return vector1.IsNearUnit(epsilon) &&
               vector2.IsNearUnit(epsilon) &&
               vector1.VectorESp(vector2).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthonormalWithUnit(this IQuad<Float64Scalar> vector1, IQuad<Float64Scalar> vector2, double epsilon = 1e-12)
    {
        Debug.Assert(
            vector2.IsNearUnit(epsilon)
        );

        return vector1.IsNearUnit(epsilon) &&
               vector1.VectorESp(vector2).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelTo(this IQuad<Float64Scalar> vector1, IQuad<Float64Scalar> vector2, double epsilon = 1e-12)
    {
        return vector1.GetAngleCos(vector2).Abs().IsNearOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelToUnit(this IQuad<Float64Scalar> vector1, IQuad<Float64Scalar> vector2, double epsilon = 1e-12)
    {
        return vector1.GetAngleCosWithUnit(vector2).Abs().IsNearOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthogonalTo(this IQuad<Float64Scalar> vector1, IQuad<Float64Scalar> vector2, double epsilon = 1e-12)
    {
        return vector1.VectorESp(vector2).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVectorBasis(this IQuad<Float64Scalar> vector, int basisIndex)
    {
        return basisIndex switch
        {
            0 => vector.Item1.IsOne() && vector.Item2.IsZero() && vector.Item3.IsZero() && vector.Item4.IsZero(),
            1 => vector.Item1.IsZero() && vector.Item2.IsOne() && vector.Item3.IsZero() && vector.Item4.IsZero(),
            2 => vector.Item1.IsZero() && vector.Item2.IsZero() && vector.Item3.IsOne() && vector.Item4.IsZero(),
            3 => vector.Item1.IsZero() && vector.Item2.IsZero() && vector.Item3.IsZero() && vector.Item4.IsOne(),
            _ => false
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearVectorBasis(this IQuad<Float64Scalar> vector, int basisIndex, double epsilon = 1e-12d)
    {
        var vector2 = basisIndex switch
        {
            0 => LinFloat64Vector4D.E1,
            1 => LinFloat64Vector4D.E2,
            2 => LinFloat64Vector4D.E3,
            3 => LinFloat64Vector4D.E4,
            _ => throw new InvalidOperationException()
        };

        return vector.VectorSubtract(vector2).IsNearZero(epsilon);
    }

    public static Tuple<bool, double, LinUnitBasisVector4D> TryVectorToAxis(this IQuad<Float64Scalar> vector)
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
    public static LinFloat64Vector4D Lerp(this double t, IQuad<Float64Scalar> v1, IQuad<Float64Scalar> v2)
    {
        var s = 1.0d - t;

        return LinFloat64Vector4D.Create(s * v1.Item1 + t * v2.Item1,
            s * v1.Item2 + t * v2.Item2,
            s * v1.Item3 + t * v2.Item3,
            s * v1.Item4 + t * v2.Item4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinFloat64Vector4D> Lerp(this IEnumerable<double> tList, IQuad<Float64Scalar> v1, IQuad<Float64Scalar> v2)
    {
        return tList.Select(t => t.Lerp(v1, v2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFinite(this IQuad<Float64Scalar> tuple)
    {
        return !(
            double.IsNaN(tuple.Item1) || double.IsInfinity(tuple.Item1) ||
            double.IsNaN(tuple.Item2) || double.IsInfinity(tuple.Item2) ||
            double.IsNaN(tuple.Item3) || double.IsInfinity(tuple.Item3) ||
            double.IsNaN(tuple.Item4) || double.IsInfinity(tuple.Item4)
        );
    }

    /// <summary>
    /// The index of the largest absolute component in this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMaxAbsComponentIndex(this IQuad<Float64Scalar> tuple)
    {
        var absX = Math.Abs(tuple.Item1);
        var absY = Math.Abs(tuple.Item2);
        var absZ = Math.Abs(tuple.Item3);
        var absW = Math.Abs(tuple.Item4);

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
    public static int GetMinAbsComponentIndex(this IQuad<Float64Scalar> tuple)
    {
        var absX = Math.Abs(tuple.Item1);
        var absY = Math.Abs(tuple.Item2);
        var absZ = Math.Abs(tuple.Item3);
        var absW = Math.Abs(tuple.Item4);

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
    public static LinFloat64Vector4D Min(this IQuad<Float64Scalar> v1, IQuad<Float64Scalar> v2)
    {
        return LinFloat64Vector4D.Create(
            v1.Item1 < v2.Item1 ? v1.Item1 : v2.Item1,
            v1.Item2 < v2.Item2 ? v1.Item2 : v2.Item2,
            v1.Item3 < v2.Item3 ? v1.Item3 : v2.Item3,
            v1.Item4 < v2.Item4 ? v1.Item4 : v2.Item4
        );
    }

    /// <summary>
    /// Returns a new tuple containing component-wise maximum values of the given tuples
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D Max(this IQuad<Float64Scalar> v1, IQuad<Float64Scalar> v2)
    {
        return LinFloat64Vector4D.Create(
            v1.Item1 > v2.Item1 ? v1.Item1 : v2.Item1,
            v1.Item2 > v2.Item2 ? v1.Item2 : v2.Item2,
            v1.Item3 > v2.Item3 ? v1.Item3 : v2.Item3,
            v1.Item4 > v2.Item4 ? v1.Item4 : v2.Item4
        );
    }

    /// <summary>
    /// Returns a new tuple containing component-wise ceiling values of the given tuple
    /// </summary>
    /// <param name="tuple"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D Ceiling(this IQuad<Float64Scalar> tuple)
    {
        return LinFloat64Vector4D.Create(
            Math.Ceiling(tuple.Item1),
            Math.Ceiling(tuple.Item2),
            Math.Ceiling(tuple.Item3),
            Math.Ceiling(tuple.Item4)
        );
    }

    /// <summary>
    /// Returns a new tuple containing component-wise floor values of the given tuple
    /// </summary>
    /// <param name="tuple"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D Floor(this IQuad<Float64Scalar> tuple)
    {
        return LinFloat64Vector4D.Create(
            Math.Floor(tuple.Item1),
            Math.Floor(tuple.Item2),
            Math.Floor(tuple.Item3),
            Math.Floor(tuple.Item4)
        );
    }

    /// <summary>
    /// Returns a new tuple containing component-wise absolute values of the given tuple
    /// </summary>
    /// <param name="tuple"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D Abs(this IQuad<Float64Scalar> tuple)
    {
        return LinFloat64Vector4D.Create(
            Math.Abs(tuple.Item1),
            Math.Abs(tuple.Item2),
            Math.Abs(tuple.Item3),
            Math.Abs(tuple.Item4)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D ToLinVector4D(this IPair<Float64Scalar> tuple)
    {
        return LinFloat64Vector4D.Create(
            tuple.Item1,
            tuple.Item2,
            Float64Scalar.Zero,
            Float64Scalar.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D ToLinVector4D(this IntTuple2D tuple)
    {
        return LinFloat64Vector4D.Create(tuple.Item1, tuple.Item2, 0.0d, 0.0d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D ToLinVector4D(this ITriplet<Float64Scalar> tuple)
    {
        return LinFloat64Vector4D.Create(
            tuple.Item1,
            tuple.Item2,
            tuple.Item3,
            Float64Scalar.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D ToLinVector4D(this IQuad<Float64Scalar> tuple)
    {
        return tuple is LinFloat64Vector4D t
            ? t
            : LinFloat64Vector4D.Create(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D ToLinVector4D(this IntTuple3D tuple)
    {
        return LinFloat64Vector4D.Create(tuple.ItemX, tuple.ItemY, tuple.ItemZ, 0.0d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D RealPartToLinVector4D(this IQuad<Complex> tuple)
    {
        return LinFloat64Vector4D.Create(
            tuple.Item1.Real,
            tuple.Item2.Real,
            tuple.Item3.Real,
            tuple.Item4.Real
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D ImaginaryPartToLinVector4D(this IQuad<Complex> tuple)
    {
        return LinFloat64Vector4D.Create(
            tuple.Item1.Imaginary,
            tuple.Item2.Imaginary,
            tuple.Item3.Imaginary,
            tuple.Item4.Imaginary
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
    public static LinFloat64Vector4D Permute(this IQuad<Float64Scalar> tuple, int xIndex, int yIndex, int zIndex, int wIndex)
    {
        return LinFloat64Vector4D.Create(
            tuple.GetComponent(xIndex),
            tuple.GetComponent(yIndex),
            tuple.GetComponent(zIndex),
            tuple.GetComponent(wIndex)
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
    public static LinFloat64Vector4D SafePermute(this IQuad<Float64Scalar> tuple, int xIndex, int yIndex, int zIndex, int wIndex)
    {
        return LinFloat64Vector4D.Create(
            tuple.GetComponent(xIndex.Mod(4)),
            tuple.GetComponent(yIndex.Mod(4)),
            tuple.GetComponent(zIndex.Mod(4)),
            tuple.GetComponent(wIndex.Mod(4))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetComponent(this IQuad<Float64Scalar> tuple, int index)
    {
        return index switch
        {
            0 => tuple.Item1,
            1 => tuple.Item2,
            2 => tuple.Item3,
            3 => tuple.Item4,
            _ => 0d
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<double> GetComponents(this IQuad<Float64Scalar> tuple)
    {
        yield return tuple.Item1;
        yield return tuple.Item2;
        yield return tuple.Item3;
        yield return tuple.Item4;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<double> GetComponents(this IEnumerable<IQuad<Float64Scalar>> tuplesList)
    {
        return tuplesList.SelectMany(t => t.GetComponents());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D ComponentsProduct(this IQuad<Float64Scalar> tuple1, IQuad<Float64Scalar> tuple2)
    {
        return LinFloat64Vector4D.Create(
            tuple1.Item1 * tuple2.Item1,
            tuple1.Item2 * tuple2.Item2,
            tuple1.Item3 * tuple2.Item3,
            tuple1.Item4 * tuple2.Item4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D ComponentsProduct(this IQuad<Float64Scalar> tuple1, IQuad<Float64Scalar> tuple2, IQuad<Float64Scalar> tuple3)
    {
        return LinFloat64Vector4D.Create(
            tuple1.Item1 * tuple2.Item1 * tuple3.Item1,
            tuple1.Item2 * tuple2.Item2 * tuple3.Item2,
            tuple1.Item3 * tuple2.Item3 * tuple3.Item3,
            tuple1.Item4 * tuple2.Item4 * tuple3.Item4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> GetTupleXPair(this IReadOnlyList<IQuad<Float64Scalar>> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<double> GetTupleXTriplet(this IReadOnlyList<IQuad<Float64Scalar>> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<double> GetTupleXQuad(this IReadOnlyList<IQuad<Float64Scalar>> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1,
            itemArray[index + 3].Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<double> GetTupleXQuint(this IReadOnlyList<IQuad<Float64Scalar>> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1,
            itemArray[index + 3].Item1,
            itemArray[index + 4].Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Hexad<double> GetTupleXHexad(this IReadOnlyList<IQuad<Float64Scalar>> itemArray, int index)
    {
        return new Hexad<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1,
            itemArray[index + 3].Item1,
            itemArray[index + 4].Item1,
            itemArray[index + 5].Item1
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> GetTupleYPair(this IReadOnlyList<IQuad<Float64Scalar>> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<double> GetTupleYTriplet(this IReadOnlyList<IQuad<Float64Scalar>> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<double> GetTupleYQuad(this IReadOnlyList<IQuad<Float64Scalar>> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2,
            itemArray[index + 3].Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<double> GetTupleYQuint(this IReadOnlyList<IQuad<Float64Scalar>> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2,
            itemArray[index + 3].Item2,
            itemArray[index + 4].Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Hexad<double> GetTupleYHexad(this IReadOnlyList<IQuad<Float64Scalar>> itemArray, int index)
    {
        return new Hexad<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2,
            itemArray[index + 3].Item2,
            itemArray[index + 4].Item2,
            itemArray[index + 5].Item2
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> GetTupleZPair(this IReadOnlyList<IQuad<Float64Scalar>> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<double> GetTupleZTriplet(this IReadOnlyList<IQuad<Float64Scalar>> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3,
            itemArray[index + 2].Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<double> GetTupleZQuad(this IReadOnlyList<IQuad<Float64Scalar>> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3,
            itemArray[index + 2].Item3,
            itemArray[index + 3].Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<double> GetTupleZQuint(this IReadOnlyList<IQuad<Float64Scalar>> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3,
            itemArray[index + 2].Item3,
            itemArray[index + 3].Item3,
            itemArray[index + 4].Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Hexad<double> GetTupleZHexad(this IReadOnlyList<IQuad<Float64Scalar>> itemArray, int index)
    {
        return new Hexad<double>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3,
            itemArray[index + 2].Item3,
            itemArray[index + 3].Item3,
            itemArray[index + 4].Item3,
            itemArray[index + 5].Item3
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> GetTupleWPair(this IReadOnlyList<IQuad<Float64Scalar>> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Item4,
            itemArray[index + 1].Item4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<double> GetTupleWTriplet(this IReadOnlyList<IQuad<Float64Scalar>> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Item4,
            itemArray[index + 1].Item4,
            itemArray[index + 2].Item4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<double> GetTupleWQuad(this IReadOnlyList<IQuad<Float64Scalar>> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Item4,
            itemArray[index + 1].Item4,
            itemArray[index + 2].Item4,
            itemArray[index + 3].Item4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<double> GetTupleWQuint(this IReadOnlyList<IQuad<Float64Scalar>> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].Item4,
            itemArray[index + 1].Item4,
            itemArray[index + 2].Item4,
            itemArray[index + 3].Item4,
            itemArray[index + 4].Item4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Hexad<double> GetTupleWHexad(this IReadOnlyList<IQuad<Float64Scalar>> itemArray, int index)
    {
        return new Hexad<double>(
            itemArray[index].Item4,
            itemArray[index + 1].Item4,
            itemArray[index + 2].Item4,
            itemArray[index + 3].Item4,
            itemArray[index + 4].Item4,
            itemArray[index + 5].Item4
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D RotateToUnitLinVector4D(this IQuad<Float64Scalar> vector1, IQuad<Float64Scalar> unitVector, LinFloat64Angle angle)
    {
        Debug.Assert(
            vector1.IsNearUnit() &&
            unitVector.IsNearUnit()
        );

        // Create a unit normal to u in the u-v rotational plane
        var v1 = unitVector.VectorSubtract(vector1.VectorTimes(unitVector.VectorESp(vector1)));
        var v1Length = v1.VectorENorm();

        Debug.Assert(
            !v1Length.IsNearZero()
        );

        // Compute a rotated version of v in the u-v rotational plane by the given angle
        return vector1
            .VectorTimes(angle.Cos())
            .VectorAdd(v1.VectorTimes(angle.Sin() / v1Length));
    }
}