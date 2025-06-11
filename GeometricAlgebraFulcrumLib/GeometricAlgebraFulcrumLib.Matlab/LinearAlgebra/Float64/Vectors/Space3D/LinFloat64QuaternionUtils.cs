using System;
using System.Linq;
using System.Numerics;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float32;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;

public static class LinFloat64QuaternionUtils
{
    
    public static LinFloat64Quaternion ToQuaternion(this Quaternion quaternion)
    {
        return LinFloat64Quaternion.Create(
            quaternion.W, 
            -quaternion.X, 
            -quaternion.Y, 
            -quaternion.Z
        );
    }
    

    
    public static bool IsNearZero(this Quaternion quaternion, float zeroEpsilon = Float32Utils.ZeroEpsilon)
    {
        return quaternion.X.IsNearZero(zeroEpsilon) &&
               quaternion.Y.IsNearZero(zeroEpsilon) &&
               quaternion.Z.IsNearZero(zeroEpsilon);
    }

    
    public static bool IsNearNormalized(this Quaternion quaternion, float zeroEpsilon = Float32Utils.ZeroEpsilon)
    {
        return (quaternion.LengthSquared() - 1f).IsNearZero(zeroEpsilon);
    }

    
    public static Quaternion ToSystemNumericsQuaternion(this ILinFloat64Vector3D vector)
    {
        return new Quaternion(
            (float)vector.X,
            (float)vector.Y,
            (float)vector.Z,
            0f
        );
    }

    
    public static Quaternion ToSystemNumericsQuaternion(this ILinFloat64Vector3D vector, double scalar)
    {
        return new Quaternion(
            (float)vector.X,
            (float)vector.Y,
            (float)vector.Z,
            (float)scalar
        );
    }

    
    public static double GetScalarPart(this Quaternion quaternion)
    {
        return quaternion.W;
    }

    
    public static LinFloat64Vector3D GetVectorPart(this Quaternion quaternion)
    {
        return LinFloat64Vector3D.Create(
            quaternion.X,
            quaternion.Y,
            quaternion.Z
        );
    }

    
    public static Tuple<double, LinFloat64Vector3D> GetScalarVectorParts(this Quaternion quaternion)
    {
        return new Tuple<double, LinFloat64Vector3D>(
            quaternion.W,
            LinFloat64Vector3D.Create(quaternion.X, quaternion.Y, quaternion.Z)
        );
    }

    
    public static Quaternion Conjugate(this Quaternion quaternion)
    {
        return Quaternion.Conjugate(quaternion);
    }

    
    public static Quaternion Inverse(this Quaternion quaternion)
    {
        return Quaternion.Inverse(quaternion);
    }

    
    public static Quaternion Normalize(this Quaternion quaternion)
    {
        return Quaternion.Normalize(quaternion);
    }

    
    public static Quaternion Concatenate(this Quaternion quaternion1, Quaternion quaternion2)
    {
        return Quaternion.Concatenate(
            quaternion1,
            quaternion2
        );
    }

    
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

    
    public static Quaternion Concatenate(this Quaternion quaternion1, params Quaternion[] quaternionList)
    {
        return quaternionList.Aggregate(
            quaternion1,
            Quaternion.Concatenate
        );
    }

}