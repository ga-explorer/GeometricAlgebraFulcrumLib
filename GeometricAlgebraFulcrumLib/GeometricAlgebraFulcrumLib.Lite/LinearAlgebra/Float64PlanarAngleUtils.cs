using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;

public static class Float64PlanarAngleUtils
{
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle GetAngle(this System.Random randomGenerator)
    {
        return randomGenerator
            .GetScaledNumber(360d)
            .DegreesToAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle GetAngle(this System.Random randomGenerator, Float64PlanarAngle maxAngle)
    {
        var maxValue = 
            maxAngle.GetAngleInPositiveRange().Degrees.Value;

        return randomGenerator
            .GetScaledNumber(maxValue)
            .DegreesToAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle GetAngle(this System.Random randomGenerator, Float64PlanarAngle minAngle, Float64PlanarAngle maxAngle)
    {
        var minValue = minAngle.GetAngleInPositiveRange().Degrees.Value;
        var maxValue = maxAngle.GetAngleInPositiveRange().Degrees.Value;

        return randomGenerator
            .GetLinearMappedNumber(minValue, maxValue)
            .DegreesToAngle();
    }

        
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetAngleCosWithUnit(this IFloat64Vector2D v1, IFloat64Vector2D v2)
    {
        Debug.Assert(
            v2.IsNearUnit()
        );

        var t1 =
            v1.X.Value * v2.X.Value +
            v1.Y.Value * v2.Y.Value;

        var t2 = Math.Sqrt(
            v1.X.Value * v1.X.Value +
            v1.Y.Value * v1.Y.Value
        );

        return Float64Utils.Clamp((t1 / t2), -1d, 1d);
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetAngleCosWithUnit(this IFloat64Vector3D v1, IFloat64Vector3D v2)
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

        return Float64Utils.Clamp((t1 / t2), -1d, 1d);
    }
        
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetAngleCos(this IFloat64Vector2D v1, IFloat64Vector2D v2)
    {
        var t1 = v1.X * v2.X + v1.Y * v2.Y;
        var t2 = v1.X * v1.X + v1.Y * v1.Y;
        var t3 = v2.X * v2.X + v2.Y * v2.Y;

        return (t1 / Math.Sqrt(t2 * t3)).Clamp(-1d, 1d);
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetAngleCos(this IFloat64Vector3D v1, IFloat64Vector3D v2)
    {
        var t1 = v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        var t2 = v1.X * v1.X + v1.Y * v1.Y + v1.Z * v1.Z;
        var t3 = v2.X * v2.X + v2.Y * v2.Y + v2.Z * v2.Z;

        return (t1 / Math.Sqrt(t2 * t3)).Clamp(-1d, 1d);
    }
        
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle GetAngle(this IFloat64Vector2D v1, IFloat64Vector2D v2)
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
    public static Float64PlanarAngle GetAngle(this IFloat64Vector3D v1, IFloat64Vector3D v2)
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
    public static Float64PlanarAngle GetAngleWithUnit(this IFloat64Vector2D v1, IFloat64Vector2D v2)
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
    public static Float64PlanarAngle GetAngleWithUnit(this IFloat64Vector3D v1, IFloat64Vector3D v2)
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
    public static double GetUnitVectorsAngleCos(this IFloat64Vector2D v1, IFloat64Vector2D v2)
    {
        Debug.Assert(
            v1.IsNearUnitVector() &&
            v2.IsNearUnitVector()
        );

        return Float64Utils.Clamp(
            (v1.X.Value * v2.X.Value + v1.Y.Value * v2.Y.Value), 
            -1, 
            1
        );
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetUnitVectorsAngleCos(this IFloat64Vector3D v1, IFloat64Vector3D v2)
    {
        Debug.Assert(
            v1.IsNearUnitVector() &&
            v2.IsNearUnitVector()
        );

        return Float64Utils.Clamp((v1.X.Value * v2.X.Value +
                                   v1.Y.Value * v2.Y.Value +
                                   v1.Z.Value * v2.Z.Value
            ), -1, 1);
    }
        
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle GetUnitVectorsAngle(this IFloat64Vector2D v1, IFloat64Vector2D v2)
    {
        return v1.GetUnitVectorsAngleCos(v2).ArcCos();
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle GetUnitVectorsAngle(this IFloat64Vector3D v1, IFloat64Vector3D v2)
    {
        return v1.GetUnitVectorsAngleCos(v2).ArcCos();
    }
        
    /// <summary>
    /// Find the angle between points (p1, p0, p2); i.e. p0 is the head of the angle
    /// </summary>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle GetPointsAngle(this IFloat64Vector2D p0, IFloat64Vector2D p1, IFloat64Vector2D p2)
    {
        var v1 = Float64Vector2D.Create(
            p1.X - p0.X,
            p1.Y - p0.Y
        );

        var v2 = Float64Vector2D.Create(
            p2.X - p0.X,
            p2.Y - p0.Y
        );

        return v1.GetAngle(v2);
    }

    /// <summary>
    /// Find the angle between points (p1, p0, p2); i.e. p0 is the head of the angle
    /// </summary>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle GetPointsAngle(this IFloat64Vector3D p0, IFloat64Vector3D p1, IFloat64Vector3D p2)
    {
        var v1 = Float64Vector3D.Create(
            p1.X - p0.X,
            p1.Y - p0.Y,
            p1.Z - p0.Z
        );

        var v2 = Float64Vector3D.Create(
            p2.X - p0.X,
            p2.Y - p0.Y,
            p2.Z - p0.Z
        );

        return v1.GetAngle(v2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle DegreesToAngle(this int angleInDegrees)
    {
        return Float64PlanarAngle.CreateFromDegrees(angleInDegrees);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle DegreesToAngle(this int angleInDegrees, Float64PlanarAngleRange range)
    {
        return Float64PlanarAngle.CreateFromDegrees(angleInDegrees, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle DegreesToAngle(this double angleInDegrees)
    {
        return Float64PlanarAngle.CreateFromDegrees(angleInDegrees);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle DegreesToAngle(this double angleInDegrees, Float64PlanarAngleRange range)
    {
        return Float64PlanarAngle.CreateFromDegrees(angleInDegrees, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle RadiansToAngle(this double angleInRadians)
    {
        return Float64PlanarAngle.CreateFromRadians(angleInRadians);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle RadiansToAngle(this double angleInRadians, Float64PlanarAngleRange range)
    {
        return Float64PlanarAngle.CreateFromRadians(angleInRadians, range);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle Lerp(this double t, Float64PlanarAngle angle1, Float64PlanarAngle angle2)
    {
        Debug.Assert(t.IsValid() && t is >= 0 and <= 1);

        return ((1.0d - t) * angle1.Degrees + t * angle2.Degrees).DegreesToAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle Lerp(this double t, Float64PlanarAngle angle2)
    {
        Debug.Assert(t.IsValid() && t is >= 0 and <= 1);

        return (t * angle2.Degrees).DegreesToAngle();
    }

        
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetVectorsAngleCos(this IFloat64Vector2D v1, IFloat64Vector2D v2)
    {
        var t1 = v1.X * v2.X + v1.Y * v2.Y;
        var t2 = v1.X * v1.X + v1.Y * v1.Y;
        var t3 = v2.X * v2.X + v2.Y * v2.Y;

        return t1 / (t2 * t3).Sqrt();
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlanarAngle GetVectorsAngle(this IFloat64Vector2D v1, IFloat64Vector2D v2)
    {
        var t1 = v1.X * v2.X + v1.Y * v2.Y;
        var t2 = v1.X * v1.X + v1.Y * v1.Y;
        var t3 = v2.X * v2.X + v2.Y * v2.Y;

        var cosAngle = t1 / (t2 * t3).Sqrt();

        return cosAngle.ArcCos();
    }
}