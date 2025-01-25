using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

public static class Float64AffineMapFactory3D
{
    public static Float64IdentityAffineMap3D IdentityMap3D
        => Float64IdentityAffineMap3D.Instance;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64TranslateAffineMap3D CreateTranslateMap3D(this ILinFloat64Vector3D translationVector)
    {
        return new Float64TranslateAffineMap3D(translationVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64UniformScaleAffineMap3D CreateUniformScaleMap3D(this double scalingFactor)
    {
        return new Float64UniformScaleAffineMap3D(scalingFactor);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RotateByAxisAngleMap3D CreateRotateMap3D(this IFloat64Tuple3D normal, Float64PlanarAngle angle)
    //{
    //    return RotateByAxisAngleMap3D.Create(normal, angle);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RotateByAxisToVectorMap3D CreateRotateMap3D(this Axis3D axis, IFloat64Tuple3D unitVector)
    //{
    //    return RotateByAxisToVectorMap3D.Create(axis, unitVector);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RotateByVectorToAxisMap3D CreateRotateMap3D(this IFloat64Tuple3D unitVector, Axis3D axis)
    //{
    //    return RotateByVectorToAxisMap3D.Create(unitVector, axis);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RotateByVectorToVectorMap3D CreateRotateMap3D(this IFloat64Tuple3D unitVector1, IFloat64Tuple3D unitVector2)
    //{
    //    return RotateByVectorToVectorMap3D.Create(unitVector1, unitVector2);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RotateUniformScaleMap3D CreateRotateScaleMap3D(this IFloat64Tuple3D normal, Float64PlanarAngle angle, double scalingFactor)
    //{
    //    return RotateByAxisAngleMap3D.Create(normal, angle).CreateRotateScaleMap3D(scalingFactor);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RotateUniformScaleMap3D CreateRotateScaleMap3D(this Axis3D axis, IFloat64Tuple3D unitVector, double scalingFactor)
    //{
    //    return RotateByAxisToVectorMap3D.Create(axis, unitVector).CreateRotateScaleMap3D(scalingFactor);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RotateUniformScaleMap3D CreateRotateScaleMap3D(this IFloat64Tuple3D unitVector, Axis3D axis, double scalingFactor)
    //{
    //    return RotateByVectorToAxisMap3D.Create(unitVector, axis).CreateRotateScaleMap3D(scalingFactor);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RotateUniformScaleMap3D CreateRotateScaleMap3D(this IFloat64Tuple3D unitVector1, IFloat64Tuple3D unitVector2, double scalingFactor)
    //{
    //    return RotateByVectorToVectorMap3D.Create(unitVector1, unitVector2).CreateRotateScaleMap3D(scalingFactor);
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64RotateUniformScaleAffineMap3D CreateRotateScaleMap3D(this IFloat64RotateAffineMap3D rotateMap, double scalingFactor)
    {
        return new Float64RotateUniformScaleAffineMap3D()
        {
            RotateMap = rotateMap,
            ScalingFactor = scalingFactor
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64RotateUniformScaleTranslateAffineMap3D CreateRotateScaleTranslateMap3D(this IFloat64RotateAffineMap3D rotateMap, double scalingFactor, ILinFloat64Vector3D translationVector)
    {
        return new Float64RotateUniformScaleTranslateAffineMap3D()
        {
            RotateMap = rotateMap,
            ScalingFactor = scalingFactor,
            TranslationVector = translationVector.ToLinVector3D()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64TranslateUniformScaleRotateAffineMap3D CreateTranslateScaleRotateMap3D(this ILinFloat64Vector3D translationVector, double scalingFactor, IFloat64RotateAffineMap3D rotateMap)
    {
        return new Float64TranslateUniformScaleRotateAffineMap3D()
        {
            RotateMap = rotateMap,
            ScalingFactor = scalingFactor,
            TranslationVector = translationVector.ToLinVector3D()
        };
    }
}