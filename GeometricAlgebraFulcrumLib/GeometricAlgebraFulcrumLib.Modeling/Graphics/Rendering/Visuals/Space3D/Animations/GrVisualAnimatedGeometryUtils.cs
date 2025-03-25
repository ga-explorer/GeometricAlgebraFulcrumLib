using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;

public static class GrVisualAnimatedGeometryUtils
{
    public static bool IsNullOrValid(this GrVisualAnimatedGeometry? animatedGeometry)
    {
        return animatedGeometry is null || 
               animatedGeometry.IsValid();
    }

    public static bool IsNullOrValid(this GrVisualAnimatedGeometry? animatedGeometry, Float64ScalarRange timeRange)
    {
        return animatedGeometry is null || 
               animatedGeometry.IsValid();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar? Negative(this GrVisualAnimatedScalar? animatedScalar)
    {
        return animatedScalar is null ? null : -animatedScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D? Negative(this GrVisualAnimatedVector2D? animatedVector)
    {
        if (animatedVector is null) return null;

        var baseCurve = Float64ComputedPath2D.Finite(
            animatedVector.TimeRange,
            t => -animatedVector.GetValue(t),
            t => -animatedVector.GetDerivative1Value(t)
        );

        return GrVisualAnimatedVector2D.Create(
            baseCurve,
            animatedVector.SamplingSpecs
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D? Negative(this GrVisualAnimatedVector3D? animatedVector)
    {
        if (animatedVector is null) return null;

        var baseCurve = Float64ComputedPath3D.Finite(
            animatedVector.TimeRange,
            t => -animatedVector.BasePath.GetValue(t),
            t => -animatedVector.BasePath.GetDerivative1Value(t)
        );

        return GrVisualAnimatedVector3D.Create(
            baseCurve,
            animatedVector.SamplingSpecs
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar? MapScalar(this GrVisualAnimatedScalar? animatedScalar, Func<double, double> scalarMapping)
    {
        if (animatedScalar is null) return null;

        return GrVisualAnimatedScalar.Create(
            animatedScalar.BaseScalar.ToFloat64ScalarSignal(scalarMapping),
            animatedScalar.SamplingSpecs
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D? MapVector(this GrVisualAnimatedVector2D? animatedVector, Func<LinFloat64Vector2D, LinFloat64Vector2D> vectorMapping)
    {
        if (animatedVector is null) return null;

        var baseCurve = Float64ComputedPath2D.Finite(
            animatedVector.TimeRange,
            t => vectorMapping(animatedVector.GetValue(t))
        );

        return GrVisualAnimatedVector2D.Create(
            baseCurve,
            animatedVector.SamplingSpecs
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D? MapVector(this GrVisualAnimatedVector3D? animatedVector, Func<LinFloat64Vector3D, LinFloat64Vector3D> vectorMapping)
    {
        if (animatedVector is null) return null;

        var baseCurve = Float64ComputedPath3D.Finite(
            animatedVector.TimeRange,
            t => vectorMapping(animatedVector.BasePath.GetValue(t))
        );

        return GrVisualAnimatedVector3D.Create(
            baseCurve,
            animatedVector.SamplingSpecs
        );
    }

}