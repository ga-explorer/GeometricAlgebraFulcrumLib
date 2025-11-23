using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Tuples;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using MathNet.Numerics;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

public static class LinFloat64Vector3DUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFinite(this ITriplet<double> vector)
    {
        return double.IsFinite(vector.Item1) && 
               double.IsFinite(vector.Item2) && 
               double.IsFinite(vector.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFinite(this ITriplet<double> vector, double maxLength)
    {
        return double.IsFinite(vector.Item1) && 
               double.IsFinite(vector.Item2) && 
               double.IsFinite(vector.Item3) &&
               vector.L2Norm() <= maxLength;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ProjectOnXAxis(this ITriplet<double> vector)
    {
        return LinFloat64Vector3D.Create(vector.Item1, 0, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ProjectOnYAxis(this ITriplet<double> vector)
    {
        return LinFloat64Vector3D.Create(0, vector.Item2, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ProjectOnZAxis(this ITriplet<double> vector)
    {
        return LinFloat64Vector3D.Create(0, 0, vector.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ProjectOnXyPlane(this ITriplet<double> vector)
    {
        return LinFloat64Vector3D.Create(vector.Item1, vector.Item2, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ProjectOnXzPlane(this ITriplet<double> vector)
    {
        return LinFloat64Vector3D.Create(vector.Item1, 0, vector.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ProjectOnYzPlane(this ITriplet<double> vector)
    {
        return LinFloat64Vector3D.Create(0, vector.Item2, vector.Item3);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ClampTo(this ITriplet<double> tuple, ITriplet<double> maxTuple)
    {
        return LinFloat64Vector3D.Create(
            tuple.Item1.ClampTo(maxTuple.Item1),
            tuple.Item2.ClampTo(maxTuple.Item2),
            tuple.Item3.ClampTo(maxTuple.Item3)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ClampTo(this ITriplet<double> tuple, ITriplet<double> minTuple, ITriplet<double> maxTuple)
    {
        return LinFloat64Vector3D.Create(
            tuple.Item1.ClampTo(minTuple.Item1, maxTuple.Item1),
            tuple.Item2.ClampTo(minTuple.Item2, maxTuple.Item2),
            tuple.Item3.ClampTo(minTuple.Item3, maxTuple.Item3)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ClampToSymmetric(this ITriplet<double> tuple, ITriplet<double> maxTuple)
    {
        return LinFloat64Vector3D.Create(
            tuple.Item1.ClampToSymmetric(maxTuple.Item1),
            tuple.Item2.ClampToSymmetric(maxTuple.Item2),
            tuple.Item3.ClampToSymmetric(maxTuple.Item3)
        );
    }


    /// <summary>
    /// True if the given values are equal relative to the default accuracy
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqual(this ITriplet<double> x1, ITriplet<double> x2)
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
    /// <param name="tolerance"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqual(this ITriplet<double> x1, ITriplet<double> x2, double tolerance = 1e-12d)
    {
        return x1.GetDistanceToPoint(x2).IsNearZero(tolerance);
    }

    /// <summary>
    /// True if the given values are equal relative to the default accuracy
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNegativeEqual(this ITriplet<double> x1, ITriplet<double> x2)
    {
        return
            -x1.Item1 == x2.Item1 &&
            -x1.Item2 == x2.Item2 &&
            -x1.Item3 == x2.Item3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector SelectNearestBasisVector(this ITriplet<double> unitVector)
    {
        return unitVector.GetMaxAbsComponentIndex() switch
        {
            0 => unitVector.Item1.IsNegative()
                ? LinBasisVector.Nx
                : LinBasisVector.Px,

            1 => unitVector.Item2.IsNegative()
                ? LinBasisVector.Ny
                : LinBasisVector.Py,

            _ => unitVector.Item3.IsNegative()
                ? LinBasisVector.Nz
                : LinBasisVector.Pz
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector ToAxis3D(this int axisIndex, bool isNegative = false)
    {
        if (isNegative)
            return axisIndex switch
            {
                0 => LinBasisVector.Nx,
                1 => LinBasisVector.Ny,
                2 => LinBasisVector.Nz,
                _ => throw new IndexOutOfRangeException()
            };

        return axisIndex switch
        {
            0 => LinBasisVector.Px,
            1 => LinBasisVector.Py,
            2 => LinBasisVector.Pz,
            _ => throw new IndexOutOfRangeException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<LinFloat64Vector3D> GetComponentVectors(this ITriplet<double> vector)
    {
        return new Triplet<LinFloat64Vector3D>(
            LinFloat64Vector3D.Create(vector.Item1, 0, 0),
            LinFloat64Vector3D.Create(0, vector.Item2, 0),
            LinFloat64Vector3D.Create(0, 0, vector.Item3)
        );
    }

    public static Float64Scalar L2NormNormSquared(this ITriplet<Complex> vector)
    {
        return (vector.Item1 * vector.Item1.Conjugate()).Real +
               (vector.Item2 * vector.Item2.Conjugate()).Real +
               (vector.Item3 * vector.Item3.Conjugate()).Real;
    }

    /// <summary>
    /// The Euclidean squared length of this tuple when it represents a vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar L2Norm(this ITriplet<double> vector)
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
    public static Tuple<LinFloat64Vector3D, Float64Scalar> GetUnitL2NormTuple(this ITriplet<double> vector)
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
    public static Float64Scalar L2NormSquared(this ITriplet<double> vector)
    {
        return vector.Item1 * vector.Item1 +
               vector.Item2 * vector.Item2 +
               vector.Item3 * vector.Item3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar L2Norm(double vectorX, double vectorY, double vectorZ)
    {
        return Math.Sqrt(
            vectorX * vectorX +
            vectorY * vectorY +
            vectorZ * vectorZ
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorDivideByENorm(this ITriplet<double> vector)
    {
        var norm = vector.L2Norm();

        return norm.IsZero()
            ? vector.ToLinVector3D()
            : vector.VectorDivide(norm);
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUnitVector(this ITriplet<double> vector)
    {
        return vector
            .L2NormSquared()
            .IsNearOne();
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearUnitVector(this ITriplet<double> vector, double tolerance = Float64Utils.DefaultTolerance)
    {
        return vector
            .L2NormSquared()
            .IsNearEqual(1.0d, tolerance);
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near zero
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroVector(this ITriplet<double> vector)
    {
        return vector.Item1.IsZero() &&
               vector.Item2.IsZero() &&
               vector.Item3.IsZero();
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near zero
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsAlmostZeroVector(this ITriplet<double> vector)
    {
        return vector
            .L2NormSquared()
            .IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearVector(this ITriplet<double> vector1, ITriplet<double> vector2)
    {
        return vector1.Item1.IsNearEqual(vector2.Item1) &&
               vector1.Item2.IsNearEqual(vector2.Item2) &&
               vector1.Item3.IsNearEqual(vector2.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearVectorNegative(this ITriplet<double> vector1, ITriplet<double> vector2)
    {
        return vector1.Item1.IsNearEqual(-vector2.Item1) &&
               vector1.Item2.IsNearEqual(-vector2.Item2) &&
               vector1.Item3.IsNearEqual(-vector2.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar L2NormSquared(double vectorX, double vectorY, double vectorZ)
    {
        return vectorX * vectorX +
               vectorY * vectorY +
               vectorZ * vectorZ;
    }

    public static Float64Scalar L2Norm(this ITriplet<Complex> vector)
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
    public static Float64Scalar VectorDot(this ITriplet<double> v2, LinBasisVector v1)
    {
        if (v1 == LinBasisVector.Px) return v2.Item1;
        if (v1 == LinBasisVector.Nx) return -v2.Item1;

        if (v1 == LinBasisVector.Py) return v2.Item2;
        if (v1 == LinBasisVector.Ny) return -v2.Item2;

        if (v1 == LinBasisVector.Pz) return v2.Item3;
        if (v1 == LinBasisVector.Nz) return -v2.Item3;

        return Float64Scalar.Zero;
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar VectorDot(this ITriplet<double> v1, ITriplet<double> v2)
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
    public static LinFloat64Vector2D VectorDot(this ITriplet<double> v1, ITriplet<double> v2, ITriplet<double> v3)
    {
        return LinFloat64Vector2D.Create(
            v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3,
            v1.Item1 * v3.Item1 + v1.Item2 * v3.Item2 + v1.Item3 * v3.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex VectorDot(this ITriplet<Complex> v1, ITriplet<Complex> v2)
    {
        return v1.Item1 * v2.Item1.Conjugate() +
               v1.Item2 * v2.Item2.Conjugate() +
               v1.Item3 * v2.Item3.Conjugate();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex VectorDot(this ITriplet<Complex> v1, ITriplet<double> v2)
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
    public static Float64Scalar VectorDotAbs(this ITriplet<double> v1, ITriplet<double> v2)
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
    public static LinFloat64Vector3D VectorCross(this ITriplet<double> v1, ITriplet<double> v2)
    {
        return LinFloat64Vector3D.Create(
            v1.Item2 * v2.Item3 - v1.Item3 * v2.Item2,
            v1.Item3 * v2.Item1 - v1.Item1 * v2.Item3,
            v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar VectorCrossNorm(this ITriplet<double> v1, ITriplet<double> v2)
    {
        var x = v1.Item2 * v2.Item3 - v1.Item3 * v2.Item2;
        var y = v1.Item3 * v2.Item1 - v1.Item1 * v2.Item3;
        var z = v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1;

        return Math.Sqrt(x * x + y * y + z * z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar VectorCrossNormSquared(this ITriplet<double> v1, ITriplet<double> v2)
    {
        var x = v1.Item2 * v2.Item3 - v1.Item3 * v2.Item2;
        var y = v1.Item3 * v2.Item1 - v1.Item1 * v2.Item3;
        var z = v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1;

        return x * x + y * y + z * z;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorAdd(this ITriplet<double> v1, ITriplet<double> v2)
    {
        return LinFloat64Vector3D.Create(v1.Item1 + v2.Item1,
            v1.Item2 + v2.Item2,
            v1.Item3 + v2.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorAdd(this ITriplet<double> v1, ITriplet<double> v2, ITriplet<double> v3)
    {
        return LinFloat64Vector3D.Create(v1.Item1 + v2.Item1 + v3.Item1,
            v1.Item2 + v2.Item2 + v3.Item2,
            v1.Item3 + v2.Item3 + v3.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorAdd(this ITriplet<double> v1, ITriplet<double> v2, ITriplet<double> v3, ITriplet<double> v4)
    {
        return LinFloat64Vector3D.Create(v1.Item1 + v2.Item1 + v3.Item1 + v4.Item1,
            v1.Item2 + v2.Item2 + v3.Item2 + v4.Item2,
            v1.Item3 + v2.Item3 + v3.Item3 + v4.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorSubtract(this ITriplet<double> v1, ITriplet<double> v2)
    {
        return LinFloat64Vector3D.Create(v1.Item1 - v2.Item1,
            v1.Item2 - v2.Item2,
            v1.Item3 - v2.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorTimes(this ITriplet<double> v1, double v2)
    {
        return LinFloat64Vector3D.Create(
            v1.Item1 * v2,
            v1.Item2 * v2,
            v1.Item3 * v2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorTimes(this double v1, ITriplet<double> v2)
    {
        return LinFloat64Vector3D.Create(
            v1 * v2.Item1,
            v1 * v2.Item2,
            v1 * v2.Item3
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorComponentTimes(this ITriplet<double> v1, ITriplet<double> v2)
    {
        return LinFloat64Vector3D.Create(
            v1.Item1 * v2.Item1,
            v1.Item2 * v2.Item2,
            v1.Item3 * v2.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorDivide(this ITriplet<double> v1, double v2)
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
    public static LinFloat64Vector3D VectorUnitCrossXy(this IPair<double> v1, IPair<double> v2)
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
    public static LinFloat64Vector3D VectorUnitCrossXy_Z(this IPair<double> v1, double v2Z)
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
    public static LinFloat64Vector3D VectorUnitCross(this ITriplet<double> v1, ITriplet<double> v2)
    {
        var vx = v1.Item2 * v2.Item3 - v1.Item3 * v2.Item2;
        var vy = v1.Item3 * v2.Item1 - v1.Item1 * v2.Item3;
        var vz = v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1;

        var s = (vx * vx + vy * vy + vz * vz).Sqrt();

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
    public static LinFloat64Vector3D VectorUnitCross(this ITriplet<double> v1, double v2X, double v2Y, double v2Z)
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
    public static LinFloat64Vector3D VectorUnitCrossXy(this ITriplet<double> v1, double v2X, double v2Y)
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
    public static LinFloat64Vector3D VectorUnitCrossXy(this ITriplet<double> v1, IPair<double> v2)
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


    public static LinFloat64Vector3D VectorUnitNormal(this ITriplet<double> v1, ITriplet<double> v2, double tolerance = Float64Utils.DefaultTolerance)
    {
        var vx = v1.Item2 * v2.Item3 - v1.Item3 * v2.Item2;
        var vy = v1.Item3 * v2.Item1 - v1.Item1 * v2.Item3;
        var vz = v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1;

        var s = (vx * vx + vy * vy + vz * vz).Sqrt();

        if (s.IsNearZero(tolerance))
            return v1.GetUnitNormal();

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
    public static LinFloat64Vector3D ToUnitLinVector3D(this ITriplet<double> vector, bool zeroAsSymmetric = true)
    {
        var s = vector.L2Norm();

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
    public static LinFloat64Vector3D ToUnitLinVector3D(this ITriplet<double> vector, LinFloat64Vector3D? zeroUnitVector)
    {
        var s = vector.L2Norm();

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
    public static LinFloat64Vector3D VectorNegative(this ITriplet<double> vector)
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
    public static LinFloat64Vector3D NegativeUnitVector(this ITriplet<double> vector)
    {
        var s = vector.L2Norm();

        if (s.IsZero())
            return vector.ToLinVector3D();

        s = -1.0d / s;

        return LinFloat64Vector3D.Create(vector.Item1 * s, vector.Item2 * s, vector.Item3 * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<Float64Scalar, LinFloat64Vector3D> ToLengthAndUnitDirection(this ITriplet<double> vector)
    {
        var length = Math.Sqrt(
            vector.Item1 * vector.Item1 +
            vector.Item2 * vector.Item2 +
            vector.Item3 * vector.Item3
        );

        if (length.IsNearZero())
            return new Tuple<Float64Scalar, LinFloat64Vector3D>(
                Float64Scalar.Zero, 
                LinFloat64Vector3D.E1
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
    public static bool IsNearZero(this ITriplet<double> vector, double tolerance = Float64Utils.DefaultTolerance)
    {
        return vector.L2Norm().IsNearZero(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearUnit(this ITriplet<double> vector, double tolerance = Float64Utils.DefaultTolerance)
    {
        return vector.L2NormSquared().IsNearOne(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthonormalWith(this ITriplet<double> vector1, ITriplet<double> vector2, double tolerance = Float64Utils.DefaultTolerance)
    {
        return vector1.IsNearUnit(tolerance) &&
               vector2.IsNearUnit(tolerance) &&
               vector1.VectorDot(vector2).IsNearZero(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthonormalWithUnit(this ITriplet<double> vector1, ITriplet<double> vector2, double tolerance = Float64Utils.DefaultTolerance)
    {
        Debug.Assert(
            vector2.IsNearUnit(tolerance)
        );

        return vector1.IsNearUnit(tolerance) &&
               vector1.VectorDot(vector2).IsNearZero(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelTo(this ITriplet<double> vector1, ITriplet<double> vector2, double tolerance = Float64Utils.DefaultTolerance)
    {
        return vector1.GetAngleCos(vector2).Abs().IsNearOne(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOppositeTo(this ITriplet<double> vector1, ITriplet<double> vector2, double tolerance = Float64Utils.DefaultTolerance)
    {
        return vector1.GetAngleCos(vector2).IsNearMinusOne(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelToUnit(this ITriplet<double> vector1, ITriplet<double> vector2, double tolerance = Float64Utils.DefaultTolerance)
    {
        return vector1.GetAngleCosWithUnit(vector2).Abs().IsNearOne(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOppositeToUnit(this ITriplet<double> vector1, ITriplet<double> vector2, double tolerance = Float64Utils.DefaultTolerance)
    {
        return vector1.GetAngleCosWithUnit(vector2).IsNearNegativeOne(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthogonalTo(this ITriplet<double> vector1, ITriplet<double> vector2, double tolerance = Float64Utils.DefaultTolerance)
    {
        return vector1.VectorDot(vector2).IsNearZero(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVectorBasis(this ITriplet<double> vector, int basisIndex)
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
    public static bool IsNearVectorBasis(this ITriplet<double> vector, int basisIndex, double tolerance = 1e-12d)
    {
        var vector2 = basisIndex switch
        {
            0 => LinFloat64Vector3D.E1,
            1 => LinFloat64Vector3D.E2,
            2 => LinFloat64Vector3D.E3,
            _ => throw new InvalidOperationException()
        };

        return vector.VectorSubtract(vector2).IsNearZero(tolerance);
    }

    public static Tuple<bool, Float64Scalar, LinBasisVector> TryVectorToAxis(this ITriplet<double> vector)
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
            return new Tuple<bool, Float64Scalar, LinBasisVector>(
                false,
                Float64Scalar.Zero,
                LinBasisVector.Px
            );

        var scalar = vector.GetItem(basisIndex);

        return new Tuple<bool, Float64Scalar, LinBasisVector>(
            true,
            scalar.Abs(),
            basisIndex.ToAxis3D(scalar < 0)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DistinctTuplesList3D ToDistinctTuplesList(this IEnumerable<ITriplet<double>> tuplesList)
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