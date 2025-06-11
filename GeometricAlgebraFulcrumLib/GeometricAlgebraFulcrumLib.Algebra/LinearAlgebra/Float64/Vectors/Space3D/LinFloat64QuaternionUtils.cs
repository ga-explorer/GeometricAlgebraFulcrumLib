using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float32;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

public static class LinFloat64QuaternionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Quaternion ToQuaternion(this Quaternion quaternion)
    {
        return LinFloat64Quaternion.Create(
            quaternion.W, 
            -quaternion.X, 
            -quaternion.Y, 
            -quaternion.Z
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZero(this Quaternion quaternion, float zeroEpsilon = Float32Utils.ZeroEpsilon)
    {
        return quaternion.X.IsNearZero(zeroEpsilon) &&
               quaternion.Y.IsNearZero(zeroEpsilon) &&
               quaternion.Z.IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearNormalized(this Quaternion quaternion, float zeroEpsilon = Float32Utils.ZeroEpsilon)
    {
        return (quaternion.LengthSquared() - 1f).IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion ToSystemNumericsQuaternion(this ILinFloat64Vector3D vector)
    {
        return new Quaternion(
            (float)vector.X,
            (float)vector.Y,
            (float)vector.Z,
            0f
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion ToSystemNumericsQuaternion(this ILinFloat64Vector3D vector, double scalar)
    {
        return new Quaternion(
            (float)vector.X,
            (float)vector.Y,
            (float)vector.Z,
            (float)scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetScalarPart(this Quaternion quaternion)
    {
        return quaternion.W;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetVectorPart(this Quaternion quaternion)
    {
        return LinFloat64Vector3D.Create(
            quaternion.X,
            quaternion.Y,
            quaternion.Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<double, LinFloat64Vector3D> GetScalarVectorParts(this Quaternion quaternion)
    {
        return new Tuple<double, LinFloat64Vector3D>(
            quaternion.W,
            LinFloat64Vector3D.Create(quaternion.X, quaternion.Y, quaternion.Z)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion Conjugate(this Quaternion quaternion)
    {
        return Quaternion.Conjugate(quaternion);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion Inverse(this Quaternion quaternion)
    {
        return Quaternion.Inverse(quaternion);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion Normalize(this Quaternion quaternion)
    {
        return Quaternion.Normalize(quaternion);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion Concatenate(this Quaternion quaternion1, Quaternion quaternion2)
    {
        return Quaternion.Concatenate(
            quaternion1,
            quaternion2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion Concatenate(this Quaternion quaternion1, Quaternion quaternion2, Quaternion quaternion3)
    {
        return Quaternion.Concatenate(
            Quaternion.Concatenate(
                quaternion1,
                quaternion2
            ),
            quaternion3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion Concatenate(this Quaternion quaternion1, params Quaternion[] quaternionList)
    {
        return quaternionList.Aggregate(
            quaternion1,
            Quaternion.Concatenate
        );
    }

}