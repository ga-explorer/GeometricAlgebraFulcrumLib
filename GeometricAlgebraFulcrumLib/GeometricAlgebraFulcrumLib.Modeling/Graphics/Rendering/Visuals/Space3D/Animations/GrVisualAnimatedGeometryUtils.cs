﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars;

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

        var baseCurve = ComputedParametricCurve2D.Create(
            animatedVector.BaseParameterRange,
            t => -animatedVector.BaseCurve.GetPoint(t),
            t => -animatedVector.BaseCurve.GetDerivative1Point(t)
        );

        return GrVisualAnimatedVector2D.Create(
            animatedVector.SamplingSpecs,
            baseCurve,
            animatedVector.BaseParameterRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D? Negative(this GrVisualAnimatedVector3D? animatedVector)
    {
        if (animatedVector is null) return null;

        var baseCurve = ComputedParametricCurve3D.Create(
            animatedVector.BaseParameterRange,
            t => -animatedVector.BaseCurve.GetPoint(t),
            t => -animatedVector.BaseCurve.GetDerivative1Point(t)
        );

        return GrVisualAnimatedVector3D.Create(
            animatedVector.SamplingSpecs,
            baseCurve,
            animatedVector.BaseParameterRange
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar? MapScalar(this GrVisualAnimatedScalar? animatedScalar, Func<double, double> scalarMapping)
    {
        if (animatedScalar is null) return null;

        return GrVisualAnimatedScalar.Create(
            animatedScalar.TemporalScalar.MapValueUsing(scalarMapping),
            animatedScalar.SamplingSpecs
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D? MapVector(this GrVisualAnimatedVector2D? animatedVector, Func<LinFloat64Vector2D, LinFloat64Vector2D> vectorMapping)
    {
        if (animatedVector is null) return null;

        var baseCurve = ComputedParametricCurve2D.Create(
            animatedVector.BaseParameterRange,
            t => vectorMapping(animatedVector.BaseCurve.GetPoint(t))
        );

        return GrVisualAnimatedVector2D.Create(
            animatedVector.SamplingSpecs,
            baseCurve,
            animatedVector.BaseParameterRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D? MapVector(this GrVisualAnimatedVector3D? animatedVector, Func<LinFloat64Vector3D, LinFloat64Vector3D> vectorMapping)
    {
        if (animatedVector is null) return null;

        var baseCurve = ComputedParametricCurve3D.Create(
            animatedVector.BaseParameterRange,
            t => vectorMapping(animatedVector.BaseCurve.GetPoint(t))
        );

        return GrVisualAnimatedVector3D.Create(
            animatedVector.SamplingSpecs,
            baseCurve,
            animatedVector.BaseParameterRange
        );
    }

}