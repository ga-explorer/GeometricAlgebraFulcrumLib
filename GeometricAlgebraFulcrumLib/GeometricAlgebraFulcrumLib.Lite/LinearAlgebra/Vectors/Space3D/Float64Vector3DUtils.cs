using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Tuples;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

public static class Float64Vector3DUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ProjectOnXAxis(this IFloat64Vector3D vector)
    {
        return Float64Vector3D.Create(
            vector.X, 
            Float64Scalar.Zero, 
            Float64Scalar.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ProjectOnYAxis(this IFloat64Vector3D vector)
    {
        return Float64Vector3D.Create(
            Float64Scalar.Zero, 
            vector.Y, 
            Float64Scalar.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ProjectOnZAxis(this IFloat64Vector3D vector)
    {
        return Float64Vector3D.Create(
            Float64Scalar.Zero, 
            Float64Scalar.Zero,
            vector.Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ProjectOnXyPlane(this IFloat64Vector3D vector)
    {
        return Float64Vector3D.Create(
            vector.X, 
            vector.Y, 
            Float64Scalar.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ProjectOnXzPlane(this IFloat64Vector3D vector)
    {
        return Float64Vector3D.Create(
            vector.X, 
            Float64Scalar.Zero,
            vector.Z
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ProjectOnYzPlane(this IFloat64Vector3D vector)
    {
        return Float64Vector3D.Create(
            Float64Scalar.Zero, 
            vector.Y, 
            vector.Z
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ClampTo(this IFloat64Vector3D tuple, IFloat64Vector3D maxTuple)
    {
        return Float64Vector3D.Create(
            tuple.X.ClampTo(maxTuple.X),
            tuple.Y.ClampTo(maxTuple.Y),
            tuple.Z.ClampTo(maxTuple.Z)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ClampTo(this IFloat64Vector3D tuple, IFloat64Vector3D minTuple, IFloat64Vector3D maxTuple)
    {
        return Float64Vector3D.Create(
            tuple.X.ClampTo(minTuple.X, maxTuple.X),
            tuple.Y.ClampTo(minTuple.Y, maxTuple.Y),
            tuple.Z.ClampTo(minTuple.Z, maxTuple.Z)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ClampToSymmetric(this IFloat64Vector3D tuple, IFloat64Vector3D maxTuple)
    {
        return Float64Vector3D.Create(
            tuple.X.ClampToSymmetric(maxTuple.X),
            tuple.Y.ClampToSymmetric(maxTuple.Y),
            tuple.Z.ClampToSymmetric(maxTuple.Z)
        );
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
    public static bool IsEqual(this IFloat64Vector3D x1, IFloat64Vector3D x2)
    {
        return
            x1.X == x2.X &&
            x1.Y == x2.Y &&
            x1.Z == x2.Z;
    }

    /// <summary>
    /// True if the given values are equal relative to the default accuracy
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="epsilon"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqual(this IFloat64Vector3D x1, IFloat64Vector3D x2, double epsilon = 1e-12d)
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
    public static bool IsNegativeEqual(this IFloat64Vector3D x1, IFloat64Vector3D x2)
    {
        return
            -x1.X == x2.X &&
            -x1.Y == x2.Y &&
            -x1.Z == x2.Z;
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
    public static LinUnitBasisVector3D SelectNearestAxis(this IFloat64Vector3D unitVector)
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


    public static double ENormNormSquared(this IComplexVector3D vector)
    {
        return (vector.X * vector.X.Conjugate()).Real +
               (vector.Y * vector.Y.Conjugate()).Real +
               (vector.Z * vector.Z.Conjugate()).Real;
    }

    /// <summary>
    /// The Euclidean squared length of this tuple when it represents a vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ENorm(this IFloat64Vector3D vector)
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
    public static Tuple<Float64Vector3D, double> GetUnitVectorENormTuple(this IFloat64Vector3D vector)
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
    public static double ENormSquared(this IFloat64Vector3D vector)
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D DivideByNorm(this IFloat64Vector3D vector)
    {
        var norm = vector.ENorm();

        return norm.IsZero() 
            ? vector.ToVector3D() 
            : vector.Divide(norm);
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUnitVector(this IFloat64Vector3D vector)
    {
        return vector
            .ENormSquared()
            .IsAlmostEqual(1.0d);
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearUnitVector(this IFloat64Vector3D vector)
    {
        return vector
            .ENormSquared()
            .IsNearEqual(1.0d);
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near zero
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroVector(this IFloat64Vector3D vector)
    {
        return vector.ENormSquared().IsZero();
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near zero
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsAlmostZeroVector(this IFloat64Vector3D vector)
    {
        return vector
            .ENormSquared()
            .IsAlmostZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearVector(this IFloat64Vector3D vector1, IFloat64Vector3D vector2)
    {
        return vector1.X.IsNearEqual(vector2.X) &&
               vector1.Y.IsNearEqual(vector2.Y) &&
               vector1.Z.IsNearEqual(vector2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearVectorNegative(this IFloat64Vector3D vector1, IFloat64Vector3D vector2)
    {
        return vector1.X.IsNearEqual(-vector2.X) &&
               vector1.Y.IsNearEqual(-vector2.Y) &&
               vector1.Z.IsNearEqual(-vector2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ENormSquared(double vectorX, double vectorY, double vectorZ)
    {
        return vectorX * vectorX +
               vectorY * vectorY +
               vectorZ * vectorZ;
    }

    public static double ENorm(this IComplexVector3D vector)
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
    public static double ESp(this LinUnitBasisVector3D v1, IFloat64Vector3D v2)
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
    public static double ESp(this IFloat64Vector3D v1, LinUnitBasisVector3D v2)
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
    public static double ESp(this IFloat64Vector3D v1, IFloat64Vector3D v2)
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
    public static Pair<double> ESp(this IFloat64Vector3D v1, IFloat64Vector3D v2, IFloat64Vector3D v3)
    {
        return new Pair<double>(
            v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z,
            v1.X * v3.X + v1.Y * v3.Y + v1.Z * v3.Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex ESp(this IComplexVector3D v1, IComplexVector3D v2)
    {
        return v1.X * v2.X.Conjugate() +
               v1.Y * v2.Y.Conjugate() +
               v1.Z * v2.Z.Conjugate();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex ESp(this IComplexVector3D v1, IFloat64Vector3D v2)
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
    public static Float64Scalar ESpAbs(this IFloat64Vector3D v1, IFloat64Vector3D v2)
    {
        return (v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z).Abs();
    }

    /// <summary>
    /// The Euclidean cross product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D VectorCross(this LinUnitBasisVector3D v1, IFloat64Vector3D v2)
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
    public static Float64Vector3D VectorCross(this IFloat64Vector3D v1, IFloat64Vector3D v2)
    {
        return Float64Vector3D.Create(v1.Y * v2.Z - v1.Z * v2.Y,
            v1.Z * v2.X - v1.X * v2.Z,
            v1.X * v2.Y - v1.Y * v2.X);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetVectorCrossNorm(this IFloat64Vector3D v1, IFloat64Vector3D v2)
    {
        var x = v1.Y * v2.Z - v1.Z * v2.Y;
        var y = v1.Z * v2.X - v1.X * v2.Z;
        var z = v1.X * v2.Y - v1.Y * v2.X;

        return Math.Sqrt(x * x + y * y + z * z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetVectorCrossNormSquared(this IFloat64Vector3D v1, IFloat64Vector3D v2)
    {
        var x = v1.Y * v2.Z - v1.Z * v2.Y;
        var y = v1.Z * v2.X - v1.X * v2.Z;
        var z = v1.X * v2.Y - v1.Y * v2.X;

        return x * x + y * y + z * z;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Add(this IFloat64Vector3D v1, IFloat64Vector3D v2)
    {
        return Float64Vector3D.Create(v1.X + v2.X,
            v1.Y + v2.Y,
            v1.Z + v2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Add(this IFloat64Vector3D v1, IFloat64Vector3D v2, IFloat64Vector3D v3)
    {
        return Float64Vector3D.Create(v1.X + v2.X + v3.X,
            v1.Y + v2.Y + v3.Y,
            v1.Z + v2.Z + v3.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Add(this IFloat64Vector3D v1, IFloat64Vector3D v2, IFloat64Vector3D v3, IFloat64Vector3D v4)
    {
        return Float64Vector3D.Create(v1.X + v2.X + v3.X + v4.X,
            v1.Y + v2.Y + v3.Y + v4.Y,
            v1.Z + v2.Z + v3.Z + v4.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Subtract(this IFloat64Vector3D v1, IFloat64Vector3D v2)
    {
        return Float64Vector3D.Create(v1.X - v2.X,
            v1.Y - v2.Y,
            v1.Z - v2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Times(this IFloat64Vector3D v1, double v2)
    {
        return Float64Vector3D.Create(v1.X * v2,
            v1.Y * v2,
            v1.Z * v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Times(this double v1, IFloat64Vector3D v2)
    {
        return Float64Vector3D.Create(v1 * v2.X,
            v1 * v2.Y,
            v1 * v2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D Divide(this IFloat64Vector3D v1, double v2)
    {
        v2 = 1d / v2;

        return Float64Vector3D.Create(v1.X * v2,
            v1.Y * v2,
            v1.Z * v2);
    }

    /// <summary>
    /// Returns the Euclidean cross product between the given vectors as a unit vector
    /// Both vectors are assumed to have z=0 components
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D VectorUnitCrossXy(this IFloat64Vector2D v1, IFloat64Vector2D v2)
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
    public static Float64Vector3D VectorUnitCrossXy_Z(this IFloat64Vector2D v1, double v2Z)
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
    public static Float64Vector3D VectorUnitCross(this LinUnitBasisVector3D v1, IFloat64Vector3D v2)
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
    public static Float64Vector3D VectorUnitCross(this IFloat64Vector3D v1, IFloat64Vector3D v2)
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
    public static Float64Vector3D VectorUnitCross(this IFloat64Vector3D v1, double v2X, double v2Y, double v2Z)
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
    public static Float64Vector3D VectorUnitCrossXy(this IFloat64Vector3D v1, double v2X, double v2Y)
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
    public static Float64Vector3D VectorUnitCrossXy(this IFloat64Vector3D v1, IFloat64Vector2D v2)
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
    public static Float64Vector3D ToUnitVector(this IFloat64Vector3D vector, bool zeroAsSymmetric = true)
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
    public static Float64Vector3D Negative(this IFloat64Vector3D vector)
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
    public static Float64Vector3D NegativeUnitVector(this IFloat64Vector3D vector)
    {
        var s = vector.ENorm();
        if (s.IsAlmostZero())
            return vector.ToVector3D();

        s = 1.0d / s;
        return Float64Vector3D.Create(vector.X * s, vector.Y * s, vector.Z * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<double, Float64Vector3D> ToLengthAndUnitDirection(this IFloat64Vector3D vector)
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
    public static bool IsNearZero(this IFloat64Vector3D vector, double epsilon = 1e-12)
    {
        return vector.ENorm().IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearUnit(this IFloat64Vector3D vector, double epsilon = 1e-12)
    {
        return vector.ENormSquared().IsNearOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthonormalWith(this IFloat64Vector3D vector1, IFloat64Vector3D vector2, double epsilon = 1e-12)
    {
        return vector1.IsNearUnit(epsilon) &&
               vector2.IsNearUnit(epsilon) &&
               vector1.ESp(vector2).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthonormalWithUnit(this IFloat64Vector3D vector1, IFloat64Vector3D vector2, double epsilon = 1e-12)
    {
        Debug.Assert(
            vector2.IsNearUnit(epsilon)
        );

        return vector1.IsNearUnit(epsilon) &&
               vector1.ESp(vector2).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelTo(this IFloat64Vector3D vector1, IFloat64Vector3D vector2, double epsilon = 1e-12)
    {
        return vector1.GetAngleCos(vector2).Abs().IsNearOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOppositeTo(this IFloat64Vector3D vector1, IFloat64Vector3D vector2, double epsilon = 1e-12)
    {
        return vector1.GetAngleCos(vector2).IsNearMinusOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelToUnit(this IFloat64Vector3D vector1, IFloat64Vector3D vector2, double epsilon = 1e-12)
    {
        return vector1.GetAngleCosWithUnit(vector2).Abs().IsNearOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOppositeToUnit(this IFloat64Vector3D vector1, IFloat64Vector3D vector2, double epsilon = 1e-12)
    {
        return vector1.GetAngleCosWithUnit(vector2).IsNearNegativeOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthogonalTo(this IFloat64Vector3D vector1, IFloat64Vector3D vector2, double epsilon = 1e-12)
    {
        return vector1.ESp(vector2).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVectorBasis(this IFloat64Vector3D vector, int basisIndex)
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
    public static bool IsNearVectorBasis(this IFloat64Vector3D vector, int basisIndex, double epsilon = 1e-12d)
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

    public static Tuple<bool, double, LinUnitBasisVector3D> TryVectorToAxis(this IFloat64Vector3D vector)
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
    public static bool IsFinite(this IFloat64Vector3D tuple)
    {
        return tuple.X.IsFinite() &&
               tuple.Y.IsFinite() &&
               tuple.Z.IsFinite();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DistinctTuplesList3D ToDistinctTuplesList(this IEnumerable<IFloat64Vector3D> tuplesList)
    {
        return new DistinctTuplesList3D(tuplesList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D RealPart(this IComplexVector3D tuple)
    {
        return Float64Vector3D.Create(tuple.X.Real,
            tuple.Y.Real,
            tuple.Z.Real);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D ImaginaryPart(this IComplexVector3D tuple)
    {
        return Float64Vector3D.Create(tuple.X.Imaginary,
            tuple.Y.Imaginary,
            tuple.Z.Imaginary);
    }
}