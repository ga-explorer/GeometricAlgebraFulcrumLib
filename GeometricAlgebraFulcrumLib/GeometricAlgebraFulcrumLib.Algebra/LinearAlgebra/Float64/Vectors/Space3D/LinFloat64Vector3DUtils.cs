using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Tuples;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

public static class LinFloat64Vector3DUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ProjectOnXAxis(this ITriplet<Float64Scalar> vector)
    {
        return LinFloat64Vector3D.Create(
            vector.Item1,
            Float64Scalar.Zero,
            Float64Scalar.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ProjectOnYAxis(this ITriplet<Float64Scalar> vector)
    {
        return LinFloat64Vector3D.Create(
            Float64Scalar.Zero,
            vector.Item2,
            Float64Scalar.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ProjectOnZAxis(this ITriplet<Float64Scalar> vector)
    {
        return LinFloat64Vector3D.Create(
            Float64Scalar.Zero,
            Float64Scalar.Zero,
            vector.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ProjectOnXyPlane(this ITriplet<Float64Scalar> vector)
    {
        return LinFloat64Vector3D.Create(
            vector.Item1,
            vector.Item2,
            Float64Scalar.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ProjectOnXzPlane(this ITriplet<Float64Scalar> vector)
    {
        return LinFloat64Vector3D.Create(
            vector.Item1,
            Float64Scalar.Zero,
            vector.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ProjectOnYzPlane(this ITriplet<Float64Scalar> vector)
    {
        return LinFloat64Vector3D.Create(
            Float64Scalar.Zero,
            vector.Item2,
            vector.Item3
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ClampTo(this ITriplet<Float64Scalar> tuple, ITriplet<Float64Scalar> maxTuple)
    {
        return LinFloat64Vector3D.Create(
            tuple.Item1.ClampTo(maxTuple.Item1),
            tuple.Item2.ClampTo(maxTuple.Item2),
            tuple.Item3.ClampTo(maxTuple.Item3)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ClampTo(this ITriplet<Float64Scalar> tuple, ITriplet<Float64Scalar> minTuple, ITriplet<Float64Scalar> maxTuple)
    {
        return LinFloat64Vector3D.Create(
            tuple.Item1.ClampTo(minTuple.Item1, maxTuple.Item1),
            tuple.Item2.ClampTo(minTuple.Item2, maxTuple.Item2),
            tuple.Item3.ClampTo(minTuple.Item3, maxTuple.Item3)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ClampToSymmetric(this ITriplet<Float64Scalar> tuple, ITriplet<Float64Scalar> maxTuple)
    {
        return LinFloat64Vector3D.Create(
            tuple.Item1.ClampToSymmetric(maxTuple.Item1),
            tuple.Item2.ClampToSymmetric(maxTuple.Item2),
            tuple.Item3.ClampToSymmetric(maxTuple.Item3)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector3D DirectionToUnitNormal3D(this LinUnitBasisVector3D axis)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => LinFloat64Bivector3D.E32,
            LinUnitBasisVector3D.NegativeX => LinFloat64Bivector3D.E23,
            LinUnitBasisVector3D.PositiveY => LinFloat64Bivector3D.E13,
            LinUnitBasisVector3D.NegativeY => LinFloat64Bivector3D.E31,
            LinUnitBasisVector3D.PositiveZ => LinFloat64Bivector3D.E21,
            LinUnitBasisVector3D.NegativeZ => LinFloat64Bivector3D.E12,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector3D DirectionToUnitNormal3D(this LinUnitBasisVector3D axis, Float64Scalar scalingFactor)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => LinFloat64Bivector3D.E32 * scalingFactor,
            LinUnitBasisVector3D.NegativeX => LinFloat64Bivector3D.E23 * scalingFactor,
            LinUnitBasisVector3D.PositiveY => LinFloat64Bivector3D.E13 * scalingFactor,
            LinUnitBasisVector3D.NegativeY => LinFloat64Bivector3D.E31 * scalingFactor,
            LinUnitBasisVector3D.PositiveZ => LinFloat64Bivector3D.E21 * scalingFactor,
            LinUnitBasisVector3D.NegativeZ => LinFloat64Bivector3D.E12 * scalingFactor,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector3D NormalToUnitDirection3D(this LinUnitBasisVector3D axis)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => LinFloat64Bivector3D.E23,
            LinUnitBasisVector3D.NegativeX => LinFloat64Bivector3D.E32,
            LinUnitBasisVector3D.PositiveY => LinFloat64Bivector3D.E31,
            LinUnitBasisVector3D.NegativeY => LinFloat64Bivector3D.E13,
            LinUnitBasisVector3D.PositiveZ => LinFloat64Bivector3D.E12,
            LinUnitBasisVector3D.NegativeZ => LinFloat64Bivector3D.E21,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector3D NormalToUnitDirection3D(this LinUnitBasisVector3D axis, Float64Scalar scalingFactor)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => LinFloat64Bivector3D.E23 * scalingFactor,
            LinUnitBasisVector3D.NegativeX => LinFloat64Bivector3D.E32 * scalingFactor,
            LinUnitBasisVector3D.PositiveY => LinFloat64Bivector3D.E31 * scalingFactor,
            LinUnitBasisVector3D.NegativeY => LinFloat64Bivector3D.E13 * scalingFactor,
            LinUnitBasisVector3D.PositiveZ => LinFloat64Bivector3D.E12 * scalingFactor,
            LinUnitBasisVector3D.NegativeZ => LinFloat64Bivector3D.E21 * scalingFactor,
            _ => throw new ArgumentOutOfRangeException()
        };
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

    /// <summary>
    /// True if the given values are equal relative to the default accuracy
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqual(this ITriplet<Float64Scalar> x1, ITriplet<Float64Scalar> x2)
    {
        return
            x1.Item1 == x2.Item1 &&
            x1.Item2 == x2.Item2 &&
            x1.Item3 == x2.Item3;
    }

    /// <summary>
    /// True if the given values are equal relative to the default accuracy
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="epsilon"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqual(this ITriplet<Float64Scalar> x1, ITriplet<Float64Scalar> x2, double epsilon = 1e-12d)
    {
        return x1.GetDistanceToPoint(x2).IsNearEqual(epsilon);
    }

    /// <summary>
    /// True if the given values are equal relative to the default accuracy
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNegativeEqual(this ITriplet<Float64Scalar> x1, ITriplet<Float64Scalar> x2)
    {
        return
            -x1.Item1 == x2.Item1 &&
            -x1.Item2 == x2.Item2 &&
            -x1.Item3 == x2.Item3;
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
    public static LinUnitBasisVector3D SelectNearestAxis(this ITriplet<Float64Scalar> unitVector)
    {
        return unitVector.GetMaxAbsComponentIndex() switch
        {
            0 => unitVector.Item1.IsPositive()
                ? LinUnitBasisVector3D.PositiveX
                : LinUnitBasisVector3D.NegativeX,

            1 => unitVector.Item2.IsPositive()
                ? LinUnitBasisVector3D.PositiveY
                : LinUnitBasisVector3D.NegativeY,

            _ => unitVector.Item3.IsPositive()
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


    public static Float64Scalar VectorENormNormSquared(this ITriplet<Complex> vector)
    {
        return (vector.Item1 * vector.Item1.Conjugate()).Real +
               (vector.Item2 * vector.Item2.Conjugate()).Real +
               (vector.Item3 * vector.Item3.Conjugate()).Real;
    }

    /// <summary>
    /// The Euclidean squared length of this tuple when it represents a vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar VectorENorm(this ITriplet<Float64Scalar> vector)
    {
        return Math.Sqrt(
            vector.Item1 * vector.Item1 +
            vector.Item2 * vector.Item2 +
            vector.Item3 * vector.Item3
        );
    }

    /// <summary>
    /// The Euclidean squared length of this tuple when it represents a vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<LinFloat64Vector3D, Float64Scalar> GetUnitVectorENormTuple(this ITriplet<Float64Scalar> vector)
    {
        var length = Math.Sqrt(
            vector.Item1 * vector.Item1 +
            vector.Item2 * vector.Item2 +
            vector.Item3 * vector.Item3
        );

        if (length == 0d)
            return new Tuple<LinFloat64Vector3D, Float64Scalar>(vector.ToLinVector3D(), length);

        var s = 1d / length;
        var unitVector = LinFloat64Vector3D.Create(vector.Item1 * s,
            vector.Item2 * s,
            vector.Item3 * s);

        return new Tuple<LinFloat64Vector3D, Float64Scalar>(unitVector, length);
    }

    /// <summary>
    /// The Euclidean squared length of this tuple when it represents a vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar VectorENormSquared(this ITriplet<Float64Scalar> vector)
    {
        return vector.Item1 * vector.Item1 +
               vector.Item2 * vector.Item2 +
               vector.Item3 * vector.Item3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar VectorENorm(double vectorX, double vectorY, double vectorZ)
    {
        return Math.Sqrt(
            vectorX * vectorX +
            vectorY * vectorY +
            vectorZ * vectorZ
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorDivideByENorm(this ITriplet<Float64Scalar> vector)
    {
        var norm = vector.VectorENorm();

        return norm.IsZero()
            ? vector.ToLinVector3D()
            : vector.VectorDivide(norm);
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUnitVector(this ITriplet<Float64Scalar> vector)
    {
        return vector
            .VectorENormSquared()
            .IsNearOne();
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearUnitVector(this ITriplet<Float64Scalar> vector)
    {
        return vector
            .VectorENormSquared()
            .IsNearEqual(1.0d);
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near zero
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroVector(this ITriplet<Float64Scalar> vector)
    {
        return vector.VectorENormSquared().IsZero();
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near zero
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsAlmostZeroVector(this ITriplet<Float64Scalar> vector)
    {
        return vector
            .VectorENormSquared()
            .IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearVector(this ITriplet<Float64Scalar> vector1, ITriplet<Float64Scalar> vector2)
    {
        return vector1.Item1.IsNearEqual(vector2.Item1) &&
               vector1.Item2.IsNearEqual(vector2.Item2) &&
               vector1.Item3.IsNearEqual(vector2.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearVectorNegative(this ITriplet<Float64Scalar> vector1, ITriplet<Float64Scalar> vector2)
    {
        return vector1.Item1.IsNearEqual(-vector2.Item1) &&
               vector1.Item2.IsNearEqual(-vector2.Item2) &&
               vector1.Item3.IsNearEqual(-vector2.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar VectorENormSquared(double vectorX, double vectorY, double vectorZ)
    {
        return vectorX * vectorX +
               vectorY * vectorY +
               vectorZ * vectorZ;
    }

    public static Float64Scalar VectorENorm(this ITriplet<Complex> vector)
    {
        return Math.Sqrt(
            (vector.Item1 * vector.Item1.Conjugate()).Real +
            (vector.Item2 * vector.Item2.Conjugate()).Real +
            (vector.Item3 * vector.Item3.Conjugate()).Real
        );
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar VectorESp(this LinUnitBasisVector3D v1, ITriplet<Float64Scalar> v2)
    {
        return v1 switch
        {
            LinUnitBasisVector3D.PositiveX => v2.Item1,
            LinUnitBasisVector3D.PositiveY => v2.Item2,
            LinUnitBasisVector3D.PositiveZ => v2.Item3,
            LinUnitBasisVector3D.NegativeX => -v2.Item1,
            LinUnitBasisVector3D.NegativeY => -v2.Item2,
            _ => -v2.Item3,
        };
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar VectorESp(this ITriplet<Float64Scalar> v1, LinUnitBasisVector3D v2)
    {
        return v2 switch
        {
            LinUnitBasisVector3D.PositiveX => v1.Item1,
            LinUnitBasisVector3D.PositiveY => v1.Item2,
            LinUnitBasisVector3D.PositiveZ => v1.Item3,
            LinUnitBasisVector3D.NegativeX => -v1.Item1,
            LinUnitBasisVector3D.NegativeY => -v1.Item2,
            _ => -v1.Item3,
        };
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar VectorESp(this ITriplet<Float64Scalar> v1, ITriplet<Float64Scalar> v2)
    {
        return v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3;
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="v3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Float64Scalar> VectorESp(this ITriplet<Float64Scalar> v1, ITriplet<Float64Scalar> v2, ITriplet<Float64Scalar> v3)
    {
        return new Pair<Float64Scalar>(
            v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3,
            v1.Item1 * v3.Item1 + v1.Item2 * v3.Item2 + v1.Item3 * v3.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex VectorESp(this ITriplet<Complex> v1, ITriplet<Complex> v2)
    {
        return v1.Item1 * v2.Item1.Conjugate() +
               v1.Item2 * v2.Item2.Conjugate() +
               v1.Item3 * v2.Item3.Conjugate();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex VectorESp(this ITriplet<Complex> v1, ITriplet<Float64Scalar> v2)
    {
        return v1.Item1 * v2.Item1 +
               v1.Item2 * v2.Item2 +
               v1.Item3 * v2.Item3;
    }

    /// <summary>
    /// The absolute value of the Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar VectorESpAbs(this ITriplet<Float64Scalar> v1, ITriplet<Float64Scalar> v2)
    {
        return (v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3).Abs();
    }

    /// <summary>
    /// The Euclidean cross product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorCross(this LinUnitBasisVector3D v1, ITriplet<Float64Scalar> v2)
    {
        return v1 switch
        {
            LinUnitBasisVector3D.PositiveX => LinFloat64Vector3D.Create(0, -v2.Item3, v2.Item2),
            LinUnitBasisVector3D.PositiveY => LinFloat64Vector3D.Create(v2.Item3, 0, -v2.Item1),
            LinUnitBasisVector3D.PositiveZ => LinFloat64Vector3D.Create(-v2.Item2, v2.Item1, 0),
            LinUnitBasisVector3D.NegativeX => LinFloat64Vector3D.Create(0, v2.Item3, -v2.Item2),
            LinUnitBasisVector3D.NegativeY => LinFloat64Vector3D.Create(-v2.Item3, 0, v2.Item1),
            _ => LinFloat64Vector3D.Create(v2.Item2, -v2.Item1, 0)
        };
    }

    /// <summary>
    /// The Euclidean cross product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorCross(this ITriplet<Float64Scalar> v1, ITriplet<Float64Scalar> v2)
    {
        return LinFloat64Vector3D.Create(v1.Item2 * v2.Item3 - v1.Item3 * v2.Item2,
            v1.Item3 * v2.Item1 - v1.Item1 * v2.Item3,
            v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar VectorCrossNorm(this ITriplet<Float64Scalar> v1, ITriplet<Float64Scalar> v2)
    {
        var x = v1.Item2 * v2.Item3 - v1.Item3 * v2.Item2;
        var y = v1.Item3 * v2.Item1 - v1.Item1 * v2.Item3;
        var z = v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1;

        return Math.Sqrt(x * x + y * y + z * z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar VectorCrossNormSquared(this ITriplet<Float64Scalar> v1, ITriplet<Float64Scalar> v2)
    {
        var x = v1.Item2 * v2.Item3 - v1.Item3 * v2.Item2;
        var y = v1.Item3 * v2.Item1 - v1.Item1 * v2.Item3;
        var z = v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1;

        return x * x + y * y + z * z;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorAdd(this ITriplet<Float64Scalar> v1, ITriplet<Float64Scalar> v2)
    {
        return LinFloat64Vector3D.Create(v1.Item1 + v2.Item1,
            v1.Item2 + v2.Item2,
            v1.Item3 + v2.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorAdd(this ITriplet<Float64Scalar> v1, ITriplet<Float64Scalar> v2, ITriplet<Float64Scalar> v3)
    {
        return LinFloat64Vector3D.Create(v1.Item1 + v2.Item1 + v3.Item1,
            v1.Item2 + v2.Item2 + v3.Item2,
            v1.Item3 + v2.Item3 + v3.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorAdd(this ITriplet<Float64Scalar> v1, ITriplet<Float64Scalar> v2, ITriplet<Float64Scalar> v3, ITriplet<Float64Scalar> v4)
    {
        return LinFloat64Vector3D.Create(v1.Item1 + v2.Item1 + v3.Item1 + v4.Item1,
            v1.Item2 + v2.Item2 + v3.Item2 + v4.Item2,
            v1.Item3 + v2.Item3 + v3.Item3 + v4.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorSubtract(this ITriplet<Float64Scalar> v1, ITriplet<Float64Scalar> v2)
    {
        return LinFloat64Vector3D.Create(v1.Item1 - v2.Item1,
            v1.Item2 - v2.Item2,
            v1.Item3 - v2.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorTimes(this ITriplet<Float64Scalar> v1, double v2)
    {
        return LinFloat64Vector3D.Create(v1.Item1 * v2,
            v1.Item2 * v2,
            v1.Item3 * v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorTimes(this double v1, ITriplet<Float64Scalar> v2)
    {
        return LinFloat64Vector3D.Create(v1 * v2.Item1,
            v1 * v2.Item2,
            v1 * v2.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorDivide(this ITriplet<Float64Scalar> v1, double v2)
    {
        v2 = 1d / v2;

        return LinFloat64Vector3D.Create(v1.Item1 * v2,
            v1.Item2 * v2,
            v1.Item3 * v2);
    }

    /// <summary>
    /// Returns the Euclidean cross product between the given vectors as a unit vector
    /// Both vectors are assumed to have z=0 components
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorUnitCrossXy(this IPair<Float64Scalar> v1, IPair<Float64Scalar> v2)
    {
        var vz = v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1;

        return LinFloat64Vector3D.Create(
            0d,
            0d,
            vz.IsNegative()
                ? -1
                : vz.IsPositive() ? 1 : 0
        );
    }

    /// <summary>
    /// Returns the Euclidean cross product between the given vectors as a unit vector
    /// The first vector is assumed to have z=0 while the second x=0 and y=0
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2Z"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorUnitCrossXy_Z(this IPair<Float64Scalar> v1, double v2Z)
    {
        var vx = v1.Item2 * v2Z;
        var vy = -v1.Item1 * v2Z;

        var s = Math.Sqrt(vx * vx + vy * vy);

        if (s.IsZero())
            return LinFloat64Vector3D.UnitSymmetric;

        s = 1.0d / s;

        return LinFloat64Vector3D.Create(vx * s, vy * s, 0);
    }

    /// <summary>
    /// Returns the Euclidean cross product between the given vectors as a unit vector
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorUnitCross(this LinUnitBasisVector3D v1, ITriplet<Float64Scalar> v2)
    {
        return v1.VectorCross(v2).ToUnitLinVector3D();
    }

    /// <summary>
    /// Returns the Euclidean cross product between the given vectors as a unit vector
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorUnitCross(this ITriplet<Float64Scalar> v1, ITriplet<Float64Scalar> v2)
    {
        var vx = v1.Item2 * v2.Item3 - v1.Item3 * v2.Item2;
        var vy = v1.Item3 * v2.Item1 - v1.Item1 * v2.Item3;
        var vz = v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1;

        var s = Math.Sqrt(vx * vx + vy * vy + vz * vz);

        if (s.IsZero())
            return LinFloat64Vector3D.UnitSymmetric;

        s = 1.0d / s;
        return LinFloat64Vector3D.Create(vx * s, vy * s, vz * s);
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
    public static LinFloat64Vector3D VectorUnitCross(this ITriplet<Float64Scalar> v1, double v2X, double v2Y, double v2Z)
    {
        var vx = v1.Item2 * v2Z - v1.Item3 * v2Y;
        var vy = v1.Item3 * v2X - v1.Item1 * v2Z;
        var vz = v1.Item1 * v2Y - v1.Item2 * v2X;

        var s = Math.Sqrt(vx * vx + vy * vy + vz * vz);

        if (s.IsZero())
            return LinFloat64Vector3D.UnitSymmetric;

        s = 1.0d / s;
        return LinFloat64Vector3D.Create(vx * s, vy * s, vz * s);
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
    public static LinFloat64Vector3D VectorUnitCrossXy(this ITriplet<Float64Scalar> v1, double v2X, double v2Y)
    {
        var vx = -v1.Item3 * v2Y;
        var vy = v1.Item3 * v2X;
        var vz = v1.Item1 * v2Y - v1.Item2 * v2X;

        var s = Math.Sqrt(vx * vx + vy * vy + vz * vz);

        if (s.IsZero())
            return LinFloat64Vector3D.UnitSymmetric;

        s = 1.0d / s;
        return LinFloat64Vector3D.Create(vx * s, vy * s, vz * s);
    }

    /// <summary>
    /// Returns the Euclidean cross product between the given vectors as a unit vector
    /// The second vector is assumed to have z=0
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorUnitCrossXy(this ITriplet<Float64Scalar> v1, IPair<Float64Scalar> v2)
    {
        var vx = -v1.Item3 * v2.Item2;
        var vy = v1.Item3 * v2.Item1;
        var vz = v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1;

        var s = Math.Sqrt(vx * vx + vy * vy + vz * vz);

        if (s.IsZero())
            return LinFloat64Vector3D.UnitSymmetric;

        s = 1.0d / s;
        return LinFloat64Vector3D.Create(vx * s, vy * s, vz * s);
    }


    /// <summary>
    /// Returns a unit vector from the given one. If the length of the given vector is near zero
    /// it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="zeroAsSymmetric"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ToUnitLinVector3D(this ITriplet<Float64Scalar> vector, bool zeroAsSymmetric = true)
    {
        var s = vector.VectorENorm();

        if (s.IsZero())
            return zeroAsSymmetric
                ? LinFloat64Vector3D.UnitSymmetric
                : LinFloat64Vector3D.Zero;

        s = 1.0d / s;

        return LinFloat64Vector3D.Create(vector.Item1 * s, vector.Item2 * s, vector.Item3 * s);
    }

    /// <summary>
    /// Returns a unit vector from the given one. If the length of the given vector is near zero
    /// it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="zeroUnitVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ToUnitLinVector3D(this ITriplet<Float64Scalar> vector, LinFloat64Vector3D? zeroUnitVector)
    {
        var s = vector.VectorENorm();

        if (s.IsZero())
            return zeroUnitVector
                   ?? throw new DivideByZeroException();

        s = 1.0d / s;

        return LinFloat64Vector3D.Create(vector.Item1 * s, vector.Item2 * s, vector.Item3 * s);
    }

    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector is near 
    /// zero it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorNegative(this ITriplet<Float64Scalar> vector)
    {
        return LinFloat64Vector3D.Create(-vector.Item1, -vector.Item2, -vector.Item3);
    }

    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector is near 
    /// zero it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D NegativeUnitVector(this ITriplet<Float64Scalar> vector)
    {
        var s = vector.VectorENorm();

        if (s.IsZero())
            return vector.ToLinVector3D();

        s = -1.0d / s;

        return LinFloat64Vector3D.Create(vector.Item1 * s, vector.Item2 * s, vector.Item3 * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<Float64Scalar, LinFloat64Vector3D> ToLengthAndUnitDirection(this ITriplet<Float64Scalar> vector)
    {
        var length = Math.Sqrt(
            vector.Item1 * vector.Item1 +
            vector.Item2 * vector.Item2 +
            vector.Item3 * vector.Item3
        );

        var lengthInv = 1 / length;

        return new Tuple<Float64Scalar, LinFloat64Vector3D>(
            length,
            LinFloat64Vector3D.Create(
                vector.Item1 * lengthInv,
                vector.Item2 * lengthInv,
                vector.Item3 * lengthInv
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZero(this ITriplet<Float64Scalar> vector, double epsilon = 1e-12)
    {
        return vector.VectorENorm().IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearUnit(this ITriplet<Float64Scalar> vector, double epsilon = 1e-12)
    {
        return vector.VectorENormSquared().IsNearOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthonormalWith(this ITriplet<Float64Scalar> vector1, ITriplet<Float64Scalar> vector2, double epsilon = 1e-12)
    {
        return vector1.IsNearUnit(epsilon) &&
               vector2.IsNearUnit(epsilon) &&
               vector1.VectorESp(vector2).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthonormalWithUnit(this ITriplet<Float64Scalar> vector1, ITriplet<Float64Scalar> vector2, double epsilon = 1e-12)
    {
        Debug.Assert(
            vector2.IsNearUnit(epsilon)
        );

        return vector1.IsNearUnit(epsilon) &&
               vector1.VectorESp(vector2).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelTo(this ITriplet<Float64Scalar> vector1, ITriplet<Float64Scalar> vector2, double epsilon = 1e-12)
    {
        return vector1.GetAngleCos(vector2).Abs().IsNearOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOppositeTo(this ITriplet<Float64Scalar> vector1, ITriplet<Float64Scalar> vector2, double epsilon = 1e-12)
    {
        return vector1.GetAngleCos(vector2).IsNearMinusOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelToUnit(this ITriplet<Float64Scalar> vector1, ITriplet<Float64Scalar> vector2, double epsilon = 1e-12)
    {
        return vector1.GetAngleCosWithUnit(vector2).Abs().IsNearOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOppositeToUnit(this ITriplet<Float64Scalar> vector1, ITriplet<Float64Scalar> vector2, double epsilon = 1e-12)
    {
        return vector1.GetAngleCosWithUnit(vector2).IsNearNegativeOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthogonalTo(this ITriplet<Float64Scalar> vector1, ITriplet<Float64Scalar> vector2, double epsilon = 1e-12)
    {
        return vector1.VectorESp(vector2).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVectorBasis(this ITriplet<Float64Scalar> vector, int basisIndex)
    {
        return basisIndex switch
        {
            0 => vector.Item1.IsOne() && vector.Item2.IsZero() && vector.Item3.IsZero(),
            1 => vector.Item1.IsZero() && vector.Item2.IsOne() && vector.Item3.IsZero(),
            2 => vector.Item1.IsZero() && vector.Item2.IsZero() && vector.Item3.IsOne(),
            _ => false
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearVectorBasis(this ITriplet<Float64Scalar> vector, int basisIndex, double epsilon = 1e-12d)
    {
        var vector2 = basisIndex switch
        {
            0 => LinFloat64Vector3D.E1,
            1 => LinFloat64Vector3D.E2,
            2 => LinFloat64Vector3D.E3,
            _ => throw new InvalidOperationException()
        };

        return vector.VectorSubtract(vector2).IsNearZero(epsilon);
    }

    public static Tuple<bool, Float64Scalar, LinUnitBasisVector3D> TryVectorToAxis(this LinFloat64Vector3D vector)
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
            return new Tuple<bool, Float64Scalar, LinUnitBasisVector3D>(
                false,
                Float64Scalar.Zero,
                LinUnitBasisVector3D.PositiveX
            );

        var scalar = vector.GetItem(basisIndex);

        return new Tuple<bool, Float64Scalar, LinUnitBasisVector3D>(
            true,
            scalar.Abs(),
            basisIndex.ToAxis3D(scalar < 0)
        );
    }

    public static Tuple<bool, Float64Scalar, LinUnitBasisVector3D> TryVectorToAxis(this ITriplet<Float64Scalar> vector)
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
            return new Tuple<bool, Float64Scalar, LinUnitBasisVector3D>(
                false,
                Float64Scalar.Zero,
                LinUnitBasisVector3D.PositiveX
            );

        var scalar = vector.GetItem(basisIndex);

        return new Tuple<bool, Float64Scalar, LinUnitBasisVector3D>(
            true,
            scalar.Abs(),
            basisIndex.ToAxis3D(scalar < 0)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFinite(this ITriplet<Float64Scalar> tuple)
    {
        return tuple.Item1.IsFinite() &&
               tuple.Item2.IsFinite() &&
               tuple.Item3.IsFinite();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DistinctTuplesList3D ToDistinctTuplesList(this IEnumerable<ITriplet<Float64Scalar>> tuplesList)
    {
        return new DistinctTuplesList3D(tuplesList.Select(t => t.ToLinVector3D()));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D RealPartToLinVector3D(this ITriplet<Complex> tuple)
    {
        return LinFloat64Vector3D.Create(tuple.Item1.Real,
            tuple.Item2.Real,
            tuple.Item3.Real);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ImaginaryPartToLinVector3D(this ITriplet<Complex> tuple)
    {
        return LinFloat64Vector3D.Create(tuple.Item1.Imaginary,
            tuple.Item2.Imaginary,
            tuple.Item3.Imaginary);
    }
}