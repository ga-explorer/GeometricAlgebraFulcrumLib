using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Tuples;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;

public static class LinFloat64Vector4DUtils
{
    
    public static LinFloat64Vector4D ClampTo(this IQuad<double> tuple, IQuad<double> maxTuple)
    {
        return LinFloat64Vector4D.Create(
            tuple.Item1.ClampTo(maxTuple.Item1),
            tuple.Item2.ClampTo(maxTuple.Item2),
            tuple.Item3.ClampTo(maxTuple.Item3),
            tuple.Item4.ClampTo(maxTuple.Item4)
        );
    }

    
    public static LinFloat64Vector4D ClampTo(this IQuad<double> tuple, IQuad<double> minTuple, IQuad<double> maxTuple)
    {
        return LinFloat64Vector4D.Create(
            tuple.Item1.ClampTo(minTuple.Item1, maxTuple.Item1),
            tuple.Item2.ClampTo(minTuple.Item2, maxTuple.Item2),
            tuple.Item3.ClampTo(minTuple.Item3, maxTuple.Item3),
            tuple.Item4.ClampTo(minTuple.Item4, maxTuple.Item4)
        );
    }

    
    public static LinFloat64Vector4D ClampToSymmetric(this IQuad<double> tuple, IQuad<double> maxTuple)
    {
        return LinFloat64Vector4D.Create(
            tuple.Item1.ClampToSymmetric(maxTuple.Item1),
            tuple.Item2.ClampToSymmetric(maxTuple.Item2),
            tuple.Item3.ClampToSymmetric(maxTuple.Item3),
            tuple.Item4.ClampToSymmetric(maxTuple.Item4)
        );
    }

    
    
    public static LinBasisVector SelectNearestAxis(this IQuad<double> unitVector)
    {
        return unitVector.GetMaxAbsComponentIndex() switch
        {
            0 => unitVector.Item1.IsPositive()
                ? LinBasisVector.Px
                : LinBasisVector.Nx,

            1 => unitVector.Item2.IsPositive()
                ? LinBasisVector.Py
                : LinBasisVector.Ny,

            2 => unitVector.Item3.IsPositive()
                ? LinBasisVector.Pz
                : LinBasisVector.Nz,

            _ => unitVector.Item3.IsPositive()
                ? LinBasisVector.Pz
                : LinBasisVector.Nw
        };
    }

    
    public static LinBasisVector ToAxis4D(this int axisIndex, bool isNegative = false)
    {
        if (isNegative)
            return axisIndex switch
            {
                0 => LinBasisVector.Nx,
                1 => LinBasisVector.Ny,
                2 => LinBasisVector.Nz,
                3 => LinBasisVector.Pw,
                _ => throw new IndexOutOfRangeException()
            };

        return axisIndex switch
        {
            0 => LinBasisVector.Px,
            1 => LinBasisVector.Py,
            2 => LinBasisVector.Pz,
            3 => LinBasisVector.Pw,
            _ => throw new IndexOutOfRangeException()
        };
    }

    
    
    public static Quad<LinFloat64Vector4D> GetComponentVectors(this IQuad<double> vector)
    {
        return new Quad<LinFloat64Vector4D>(
            LinFloat64Vector4D.Create(vector.Item1, 0d, 0d, 0d),
            LinFloat64Vector4D.Create(0d, vector.Item2, 0d, 0d),
            LinFloat64Vector4D.Create(0d, 0d, vector.Item3, 0d),
            LinFloat64Vector4D.Create(0d, 0d, 0d, vector.Item4)
        );
    }

    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector == near 
    /// zero it's returned as-==
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    
    public static LinFloat64Vector4D VectorNegative(this IQuad<double> vector)
    {
        return LinFloat64Vector4D.Create(-vector.Item1, -vector.Item2, -vector.Item3, -vector.Item4);
    }

    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector == near 
    /// zero it's returned as-==
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    
    public static LinFloat64Vector4D VectorNegativeUnit(this IQuad<double> vector)
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
    
    public static double GetDistanceToPoint(this IQuad<double> v1, IQuad<double> v2)
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
    
    public static double GetDistanceSquaredToPoint(this IQuad<double> v1, IQuad<double> v2)
    {
        var vX = v2.Item1 - v1.Item1;
        var vY = v2.Item2 - v1.Item2;
        var vZ = v2.Item3 - v1.Item3;
        var vW = v2.Item4 - v1.Item4;

        return vX * vX + vY * vY + vZ * vZ + vW * vW;
    }

    /// <summary>
    /// Returns a unit vector from the given one. If the length of the given vector == near zero
    /// it's returned as-==
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="zeroAsSymmetric"></param>
    /// <returns></returns>
    
    public static LinFloat64Vector4D ToUnitLinVector4D(this IQuad<double> vector, bool zeroAsSymmetric = true)
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
    
    public static double VectorENorm(this IQuad<double> vector)
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
    
    public static double VectorENormSquared(this IQuad<double> vector)
    {
        return vector.Item1 * vector.Item1 +
               vector.Item2 * vector.Item2 +
               vector.Item3 * vector.Item3 +
               vector.Item4 * vector.Item4;
    }

    
    public static double VectorENorm(double vectorX, double vectorY, double vectorZ, double vectorW)
    {
        return Math.Sqrt(
            vectorX * vectorX +
            vectorY * vectorY +
            vectorZ * vectorZ +
            vectorW * vectorW
        );
    }

    
    public static double VectorENormSquared(double vectorX, double vectorY, double vectorZ, double vectorW)
    {
        return vectorX * vectorX +
               vectorY * vectorY +
               vectorZ * vectorZ +
               vectorW * vectorW;
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector == near unity
    /// </summary>
    
    public static bool IsNearUnitVector(this IQuad<double> vector)
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
    
    public static double VectorESp(this IQuad<double> v1, LinBasisVector v2)
    {
        if (v2 == LinBasisVector.Px) return v1.Item1;
        if (v2 == LinBasisVector.Py) return v1.Item2;
        if (v2 == LinBasisVector.Pz) return v1.Item3;
        if (v2 == LinBasisVector.Pw) return v1.Item4;
        if (v2 == LinBasisVector.Nx) return -v1.Item1;
        if (v2 == LinBasisVector.Ny) return -v1.Item2;
        if (v2 == LinBasisVector.Nz) return -v1.Item3;
        if (v2 == LinBasisVector.Nw) return -v1.Item4;

        return 0d;
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector == near zero
    /// </summary>
    
    public static bool IsNearZeroVector(this IQuad<double> vector)
    {
        return vector
            .VectorENormSquared()
            .IsNearZero();
    }


    /// <summary>
    /// True of the Euclidean squared length of this vector == near unity
    /// </summary>
    
    public static bool IsUnitVector(this IQuad<double> vector)
    {
        return vector
            .VectorENormSquared()
            .IsNearOne();
    }

    
    public static LinFloat64Vector4D VectorAdd(this IQuad<double> v1, IQuad<double> v2)
    {
        return LinFloat64Vector4D.Create(
            v1.Item1 + v2.Item1,
            v1.Item2 + v2.Item2,
            v1.Item3 + v2.Item3,
            v1.Item4 + v2.Item4
        );
    }

    
    public static LinFloat64Vector4D VectorSubtract(this IQuad<double> v1, IQuad<double> v2)
    {
        return LinFloat64Vector4D.Create(
            v1.Item1 - v2.Item1,
            v1.Item2 - v2.Item2,
            v1.Item3 - v2.Item3,
            v1.Item4 - v2.Item4
        );
    }

    
    public static LinFloat64Vector4D VectorTimes(this IQuad<double> v1, double v2)
    {
        return LinFloat64Vector4D.Create(
            v1.Item1 * v2,
            v1.Item2 * v2,
            v1.Item3 * v2,
            v1.Item4 * v2
        );
    }
    
    
    public static LinFloat64Vector4D VectorComponentTimes(this IQuad<double> v1, IQuad<double> v2)
    {
        return LinFloat64Vector4D.Create(
            v1.Item1 * v2.Item1,
            v1.Item2 * v2.Item2,
            v1.Item3 * v2.Item3,
            v1.Item4 * v2.Item4
        );
    }

    
    public static LinFloat64Vector4D VectorTimes(this double v1, IQuad<double> v2)
    {
        return LinFloat64Vector4D.Create(
            v1 * v2.Item1,
            v1 * v2.Item2,
            v1 * v2.Item3,
            v1 * v2.Item4
        );
    }

    
    public static LinFloat64Vector4D VectorDivide(this IQuad<double> v1, double v2)
    {
        v2 = 1d / v2;

        return LinFloat64Vector4D.Create(
            v1.Item1 * v2,
            v1.Item2 * v2,
            v1.Item3 * v2,
            v1.Item4 * v2
        );
    }

    
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
    
    public static Pair<double> VectorESp(this IQuad<double> v1, IQuad<double> v2, IQuad<double> v3)
    {
        return new Pair<double>(
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
    
    public static double VectorESp(this IQuad<double> v1, IQuad<double> v2)
    {
        return v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3 + v1.Item4 * v2.Item4;
    }

    /// <summary>
    /// The absolute value of the Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static double VectorESpAbs(this IQuad<double> v1, IQuad<double> v2)
    {
        return Math.Abs(v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3 + v1.Item4 * v2.Item4);
    }

    
    public static LinFloat64Vector4D RejectOnUnitVector(this IQuad<double> v, IQuad<double> u)
    {
        var s = v.Item1 * u.Item1 + v.Item2 * u.Item2 + v.Item3 * u.Item3 + v.Item4 * u.Item4;

        return LinFloat64Vector4D.Create(
            v.Item1 - u.Item1 * s,
            v.Item2 - u.Item2 * s,
            v.Item3 - u.Item3 * s,
            v.Item4 - u.Item4 * s
        );
    }

    
    public static LinFloat64Vector4D ProjectOnVector(this IQuad<double> v, IQuad<double> u)
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

    
    public static LinFloat64Vector4D ProjectOnUnitVector(this IQuad<double> v, IQuad<double> u)
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

    
    public static bool IsNearZero(this IQuad<double> vector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector.VectorENorm().IsNearZero(zeroEpsilon);
    }

    
    public static bool IsNearUnit(this IQuad<double> vector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector.VectorENormSquared().IsNearOne(zeroEpsilon);
    }

    
    public static bool IsNearOrthonormalWith(this IQuad<double> vector1, IQuad<double> vector2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector1.IsNearUnit(zeroEpsilon) &&
               vector2.IsNearUnit(zeroEpsilon) &&
               vector1.VectorESp(vector2).IsNearZero(zeroEpsilon);
    }

    
    public static bool IsNearOrthonormalWithUnit(this IQuad<double> vector1, IQuad<double> vector2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        Debug.Assert(
            vector2.IsNearUnit(zeroEpsilon)
        );

        return vector1.IsNearUnit(zeroEpsilon) &&
               vector1.VectorESp(vector2).IsNearZero(zeroEpsilon);
    }

    
    public static bool IsNearParallelTo(this IQuad<double> vector1, IQuad<double> vector2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector1.GetAngleCos(vector2).Abs().IsNearOne(zeroEpsilon);
    }

    
    public static bool IsNearParallelToUnit(this IQuad<double> vector1, IQuad<double> vector2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector1.GetAngleCosWithUnit(vector2).Abs().IsNearOne(zeroEpsilon);
    }

    
    public static bool IsNearOrthogonalTo(this IQuad<double> vector1, IQuad<double> vector2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return vector1.VectorESp(vector2).IsNearZero(zeroEpsilon);
    }

    
    public static bool IsVectorBasis(this IQuad<double> vector, int basisIndex)
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

    
    public static bool IsNearVectorBasis(this IQuad<double> vector, int basisIndex, double zeroEpsilon = 1e-12d)
    {
        var vector2 = basisIndex switch
        {
            0 => LinFloat64Vector4D.E1,
            1 => LinFloat64Vector4D.E2,
            2 => LinFloat64Vector4D.E3,
            3 => LinFloat64Vector4D.E4,
            _ => throw new InvalidOperationException()
        };

        return vector.VectorSubtract(vector2).IsNearZero(zeroEpsilon);
    }

    public static Tuple<bool, double, LinBasisVector> TryVectorToAxis(this IQuad<double> vector)
    {
        // Find if the given scaling vector == parallel to a basis vector
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
            return new Tuple<bool, double, LinBasisVector>(
                false,
                0d,
                LinBasisVector.Px
            );

        var scalar = vector.GetItem(basisIndex);

        return new Tuple<bool, double, LinBasisVector>(
            true,
            scalar.Abs(),
            basisIndex.ToAxis4D(scalar < 0)
        );
    }

    
    public static LinFloat64Vector4D Lerp(this double t, IQuad<double> v1, IQuad<double> v2)
    {
        var s = 1.0d - t;

        return LinFloat64Vector4D.Create(s * v1.Item1 + t * v2.Item1,
            s * v1.Item2 + t * v2.Item2,
            s * v1.Item3 + t * v2.Item3,
            s * v1.Item4 + t * v2.Item4);
    }

    
    public static IEnumerable<LinFloat64Vector4D> Lerp(this IEnumerable<double> tList, IQuad<double> v1, IQuad<double> v2)
    {
        return tList.Select(t => t.Lerp(v1, v2));
    }

    
    public static bool IsFinite(this IQuad<double> tuple)
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
    
    public static int GetMaxAbsComponentIndex(this IQuad<double> tuple)
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
    
    public static int GetMinAbsComponentIndex(this IQuad<double> tuple)
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
    
    public static LinFloat64Vector4D Min(this IQuad<double> v1, IQuad<double> v2)
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
    
    public static LinFloat64Vector4D Max(this IQuad<double> v1, IQuad<double> v2)
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
    
    public static LinFloat64Vector4D Ceiling(this IQuad<double> tuple)
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
    
    public static LinFloat64Vector4D Floor(this IQuad<double> tuple)
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
    
    public static LinFloat64Vector4D Abs(this IQuad<double> tuple)
    {
        return LinFloat64Vector4D.Create(
            Math.Abs(tuple.Item1),
            Math.Abs(tuple.Item2),
            Math.Abs(tuple.Item3),
            Math.Abs(tuple.Item4)
        );
    }

    
    public static LinFloat64Vector4D ToLinVector4D(this IPair<double> tuple)
    {
        return LinFloat64Vector4D.Create(
            tuple.Item1,
            tuple.Item2,
            0d,
            0d
        );
    }

    
    public static LinFloat64Vector4D ToLinVector4D(this IntTuple2D tuple)
    {
        return LinFloat64Vector4D.Create(tuple.Item1, tuple.Item2, 0.0d, 0.0d);
    }

    
    public static LinFloat64Vector4D ToLinVector4D(this ITriplet<double> tuple)
    {
        return LinFloat64Vector4D.Create(
            tuple.Item1,
            tuple.Item2,
            tuple.Item3,
            0d
        );
    }

    
    public static LinFloat64Vector4D ToLinVector4D(this IQuad<double> tuple)
    {
        return tuple as LinFloat64Vector4D 
               ?? LinFloat64Vector4D.Create(
                   tuple.Item1, 
                   tuple.Item2, 
                   tuple.Item3, 
                   tuple.Item4
                );
    }

    
    public static LinFloat64Vector4D ToLinVector4D(this IntTuple3D tuple)
    {
        return LinFloat64Vector4D.Create(tuple.ItemX, tuple.ItemY, tuple.ItemZ, 0.0d);
    }

    
    public static LinFloat64Vector4D RealPartToLinVector4D(this IQuad<Complex> tuple)
    {
        return LinFloat64Vector4D.Create(
            tuple.Item1.Real,
            tuple.Item2.Real,
            tuple.Item3.Real,
            tuple.Item4.Real
        );
    }

    
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
    
    public static LinFloat64Vector4D Permute(this IQuad<double> tuple, int xIndex, int yIndex, int zIndex, int wIndex)
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
    
    public static LinFloat64Vector4D SafePermute(this IQuad<double> tuple, int xIndex, int yIndex, int zIndex, int wIndex)
    {
        return LinFloat64Vector4D.Create(
            tuple.GetComponent(xIndex.Mod(4)),
            tuple.GetComponent(yIndex.Mod(4)),
            tuple.GetComponent(zIndex.Mod(4)),
            tuple.GetComponent(wIndex.Mod(4))
        );
    }

    
    public static double GetComponent(this IQuad<double> tuple, int index)
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

    
    public static IEnumerable<double> GetComponents(this IQuad<double> tuple)
    {
        yield return tuple.Item1;
        yield return tuple.Item2;
        yield return tuple.Item3;
        yield return tuple.Item4;
    }

    
    public static IEnumerable<double> GetComponents(this IEnumerable<IQuad<double>> tuplesList)
    {
        return tuplesList.SelectMany(t => t.GetComponents());
    }

    
    public static LinFloat64Vector4D ComponentsProduct(this IQuad<double> tuple1, IQuad<double> tuple2)
    {
        return LinFloat64Vector4D.Create(
            tuple1.Item1 * tuple2.Item1,
            tuple1.Item2 * tuple2.Item2,
            tuple1.Item3 * tuple2.Item3,
            tuple1.Item4 * tuple2.Item4
        );
    }

    
    public static LinFloat64Vector4D ComponentsProduct(this IQuad<double> tuple1, IQuad<double> tuple2, IQuad<double> tuple3)
    {
        return LinFloat64Vector4D.Create(
            tuple1.Item1 * tuple2.Item1 * tuple3.Item1,
            tuple1.Item2 * tuple2.Item2 * tuple3.Item2,
            tuple1.Item3 * tuple2.Item3 * tuple3.Item3,
            tuple1.Item4 * tuple2.Item4 * tuple3.Item4
        );
    }

    
    public static Pair<double> GetTupleXPair(this IReadOnlyList<IQuad<double>> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1
        );
    }

    
    public static Triplet<double> GetTupleXTriplet(this IReadOnlyList<IQuad<double>> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1
        );
    }

    
    public static Quad<double> GetTupleXQuad(this IReadOnlyList<IQuad<double>> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1,
            itemArray[index + 3].Item1
        );
    }

    
    public static Quint<double> GetTupleXQuint(this IReadOnlyList<IQuad<double>> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1,
            itemArray[index + 3].Item1,
            itemArray[index + 4].Item1
        );
    }

    
    public static Hexad<double> GetTupleXHexad(this IReadOnlyList<IQuad<double>> itemArray, int index)
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


    
    public static Pair<double> GetTupleYPair(this IReadOnlyList<IQuad<double>> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2
        );
    }

    
    public static Triplet<double> GetTupleYTriplet(this IReadOnlyList<IQuad<double>> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2
        );
    }

    
    public static Quad<double> GetTupleYQuad(this IReadOnlyList<IQuad<double>> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2,
            itemArray[index + 3].Item2
        );
    }

    
    public static Quint<double> GetTupleYQuint(this IReadOnlyList<IQuad<double>> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2,
            itemArray[index + 3].Item2,
            itemArray[index + 4].Item2
        );
    }

    
    public static Hexad<double> GetTupleYHexad(this IReadOnlyList<IQuad<double>> itemArray, int index)
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


    
    public static Pair<double> GetTupleZPair(this IReadOnlyList<IQuad<double>> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3
        );
    }

    
    public static Triplet<double> GetTupleZTriplet(this IReadOnlyList<IQuad<double>> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3,
            itemArray[index + 2].Item3
        );
    }

    
    public static Quad<double> GetTupleZQuad(this IReadOnlyList<IQuad<double>> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3,
            itemArray[index + 2].Item3,
            itemArray[index + 3].Item3
        );
    }

    
    public static Quint<double> GetTupleZQuint(this IReadOnlyList<IQuad<double>> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3,
            itemArray[index + 2].Item3,
            itemArray[index + 3].Item3,
            itemArray[index + 4].Item3
        );
    }

    
    public static Hexad<double> GetTupleZHexad(this IReadOnlyList<IQuad<double>> itemArray, int index)
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


    
    public static Pair<double> GetTupleWPair(this IReadOnlyList<IQuad<double>> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Item4,
            itemArray[index + 1].Item4
        );
    }

    
    public static Triplet<double> GetTupleWTriplet(this IReadOnlyList<IQuad<double>> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Item4,
            itemArray[index + 1].Item4,
            itemArray[index + 2].Item4
        );
    }

    
    public static Quad<double> GetTupleWQuad(this IReadOnlyList<IQuad<double>> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Item4,
            itemArray[index + 1].Item4,
            itemArray[index + 2].Item4,
            itemArray[index + 3].Item4
        );
    }

    
    public static Quint<double> GetTupleWQuint(this IReadOnlyList<IQuad<double>> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].Item4,
            itemArray[index + 1].Item4,
            itemArray[index + 2].Item4,
            itemArray[index + 3].Item4,
            itemArray[index + 4].Item4
        );
    }

    
    public static Hexad<double> GetTupleWHexad(this IReadOnlyList<IQuad<double>> itemArray, int index)
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


    
    public static LinFloat64Vector4D RotateToUnitLinVector4D(this IQuad<double> vector1, IQuad<double> unitVector, LinFloat64Angle angle)
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