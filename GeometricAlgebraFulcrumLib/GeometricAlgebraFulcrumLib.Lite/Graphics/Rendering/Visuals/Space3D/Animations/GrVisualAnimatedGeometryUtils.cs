﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Animations;

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
    public static GrVisualAnimatedScalar? Negative(this GrVisualAnimatedScalar? animatedVector)
    {
        if (animatedVector is null) return null;

        var baseCurve = ComputedParametricScalar.Create(
            animatedVector.BaseParameterRange,
            t => -animatedVector.BaseCurve.GetValue(t),
            t => -animatedVector.BaseCurve.GetDerivative1Value(t)
        );

        return GrVisualAnimatedScalar.Create(
            animatedVector.AnimationSpecs,
            baseCurve,
            animatedVector.BaseParameterRange
        );
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
            animatedVector.AnimationSpecs,
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
            animatedVector.AnimationSpecs,
            baseCurve,
            animatedVector.BaseParameterRange
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar? MapScalar(this GrVisualAnimatedScalar? animatedScalar, Func<double, double> scalarMapping)
    {
        if (animatedScalar is null) return null;

        var baseCurve = ComputedParametricScalar.Create(
            animatedScalar.BaseParameterRange,
            t => scalarMapping(animatedScalar.BaseCurve.GetValue(t))
        );

        return GrVisualAnimatedScalar.Create(
            animatedScalar.AnimationSpecs,
            baseCurve,
            animatedScalar.BaseParameterRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D? MapVector(this GrVisualAnimatedVector2D? animatedVector, Func<Float64Vector2D, Float64Vector2D> vectorMapping)
    {
        if (animatedVector is null) return null;

        var baseCurve = ComputedParametricCurve2D.Create(
            animatedVector.BaseParameterRange,
            t => vectorMapping(animatedVector.BaseCurve.GetPoint(t))
        );

        return GrVisualAnimatedVector2D.Create(
            animatedVector.AnimationSpecs,
            baseCurve,
            animatedVector.BaseParameterRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D? MapVector(this GrVisualAnimatedVector3D? animatedVector, Func<Float64Vector3D, Float64Vector3D> vectorMapping)
    {
        if (animatedVector is null) return null;

        var baseCurve = ComputedParametricCurve3D.Create(
            animatedVector.BaseParameterRange,
            t => vectorMapping(animatedVector.BaseCurve.GetPoint(t))
        );

        return GrVisualAnimatedVector3D.Create(
            animatedVector.AnimationSpecs,
            baseCurve,
            animatedVector.BaseParameterRange
        );
    }

}