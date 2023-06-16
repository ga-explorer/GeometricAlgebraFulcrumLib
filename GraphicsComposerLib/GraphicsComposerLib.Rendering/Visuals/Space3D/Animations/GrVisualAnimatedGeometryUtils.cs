using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space1D.Curves;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Animations;

public static class GrVisualAnimatedGeometryUtils
{
    public static bool IsNullOrValid(this GrVisualAnimatedGeometry? animatedGeometry)
    {
        return animatedGeometry is null || 
               animatedGeometry.IsValid();
    }

    public static bool IsNullOrValid(this GrVisualAnimatedGeometry? animatedGeometry, Float64Range1D timeRange)
    {
        return animatedGeometry is null || 
               animatedGeometry.IsValid(timeRange);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector1D CreateAnimatedVector(this Float64Range1D timeRange, IParametricCurve1D baseCurve, Float64Range1D baseParameterRange)
    {
        return GrVisualAnimatedVector1D.Create(
            baseCurve,
            baseParameterRange,
            timeRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector1D CreateAnimatedVector(this Float64Range1D timeRange, IParametricCurve1D baseCurve)
    {
        return GrVisualAnimatedVector1D.Create(
            baseCurve,
            timeRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector1D CreateAnimatedVector(this IParametricCurve1D baseCurve, Float64Range1D baseParameterRange, Float64Range1D timeRange)
    {
        return GrVisualAnimatedVector1D.Create(
            baseCurve,
            baseParameterRange,
            timeRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector1D CreateAnimatedVector(this IParametricCurve1D baseCurve, Float64Range1D baseParameterRange, double timeRangeMax)
    {
        return GrVisualAnimatedVector1D.Create(
            baseCurve,
            baseParameterRange,
            Float64Range1D.Create(0d, timeRangeMax)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector1D CreateAnimatedVector(this IParametricCurve1D baseCurve, Float64Range1D timeRange)
    {
        return GrVisualAnimatedVector1D.Create(
            baseCurve,
            timeRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector1D CreateAnimatedVector(this IParametricCurve1D baseCurve, double timeRangeMax)
    {
        var timeRange = Float64Range1D.Create(0d, timeRangeMax);

        return GrVisualAnimatedVector1D.Create(
            baseCurve,
            timeRange
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector(this Float64Range1D timeRange, IParametricCurve2D baseCurve, Float64Range1D baseParameterRange)
    {
        return GrVisualAnimatedVector2D.Create(
            baseCurve,
            baseParameterRange,
            timeRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector(this Float64Range1D timeRange, IParametricCurve2D baseCurve)
    {
        return GrVisualAnimatedVector2D.Create(
            baseCurve,
            timeRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector(this IParametricCurve2D baseCurve, Float64Range1D baseParameterRange, Float64Range1D timeRange)
    {
        return GrVisualAnimatedVector2D.Create(
            baseCurve,
            baseParameterRange,
            timeRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector(this IParametricCurve2D baseCurve, Float64Range1D baseParameterRange, double timeRangeMax)
    {
        return GrVisualAnimatedVector2D.Create(
            baseCurve,
            baseParameterRange,
            Float64Range1D.Create(0d, timeRangeMax)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector(this IParametricCurve2D baseCurve, Float64Range1D timeRange)
    {
        return GrVisualAnimatedVector2D.Create(
            baseCurve,
            timeRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector(this IParametricCurve2D baseCurve, double timeRangeMax)
    {
        var timeRange = Float64Range1D.Create(0d, timeRangeMax);

        return GrVisualAnimatedVector2D.Create(
            baseCurve,
            timeRange
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector(this Float64Range1D timeRange, IParametricCurve3D baseCurve, Float64Range1D baseParameterRange)
    {
        return GrVisualAnimatedVector3D.Create(
            baseCurve,
            baseParameterRange,
            timeRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector(this Float64Range1D timeRange, IParametricCurve3D baseCurve)
    {
        return GrVisualAnimatedVector3D.Create(
            baseCurve,
            timeRange,
            timeRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector(this IParametricCurve3D baseCurve, Float64Range1D baseParameterRange, Float64Range1D timeRange)
    {
        return GrVisualAnimatedVector3D.Create(
            baseCurve,
            baseParameterRange,
            timeRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector(this IParametricCurve3D baseCurve, Float64Range1D baseParameterRange, double timeRangeMax)
    {
        return GrVisualAnimatedVector3D.Create(
            baseCurve,
            baseParameterRange,
            Float64Range1D.Create(0d, timeRangeMax)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector(this IParametricCurve3D baseCurve, Float64Range1D timeRange)
    {
        return GrVisualAnimatedVector3D.Create(
            baseCurve,
            timeRange,
            timeRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector(this IParametricCurve3D baseCurve, double timeRangeMax)
    {
        var timeRange = Float64Range1D.Create(0d, timeRangeMax);

        return GrVisualAnimatedVector3D.Create(
            baseCurve,
            timeRange,
            timeRange
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVectorMesh3D CreateAnimatedVectorMesh(this Float64Range1D timeRange, int count1, int count2)
    {
        return GrVisualAnimatedVectorMesh3D.Create(timeRange, count1, count2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVectorMesh3D CreateAnimatedVectorMesh(this Float64Range1D timeRange, GrVisualAnimatedVector3D[,] dataArray)
    {
        return GrVisualAnimatedVectorMesh3D.Create(timeRange, dataArray);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVectorMesh3D CreateAnimatedVectorMesh(this GrVisualAnimatedVector3D[,] dataArray, Float64Range1D timeRange)
    {
        return GrVisualAnimatedVectorMesh3D.Create(timeRange, dataArray);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D? Negative(this GrVisualAnimatedVector3D? animatedVector)
    {
        if (animatedVector is null) return null;

        var baseCurve = ComputedParametricCurve3D.Create(time => -animatedVector.GetPoint(time),
            time => -animatedVector.GetDerivative1Point(time));

        return GrVisualAnimatedVector3D.Create(
            baseCurve,
            animatedVector.TimeRange,
            animatedVector.TimeRange
        );
    }

}