﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves.Mapped;

public class GrMappedParametricCurve3D :
    IParametricCurve3D
{
    public IParametricCurve3D BaseCurve { get; }

    public IFloat64AffineMap3D AffineMap { get; }

    public Float64ScalarRange ParameterRange
        => BaseCurve.ParameterRange;


    public GrMappedParametricCurve3D(IParametricCurve3D baseCurve, IFloat64AffineMap3D map)
    {
        BaseCurve = baseCurve;
        AffineMap = map;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return BaseCurve.IsValid() &&
               AffineMap.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetPoint(double parameterValue)
    {
        return AffineMap.MapPoint(BaseCurve.GetPoint(parameterValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetDerivative1Point(double parameterValue)
    {
        return AffineMap.MapPoint(BaseCurve.GetDerivative1Point(parameterValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
    {
        var frame = BaseCurve.GetFrame(parameterValue);

        return ParametricCurveLocalFrame3D.Create(
            parameterValue,
            AffineMap.MapPoint(frame.Point),
            AffineMap.MapVector(frame.Tangent),
            AffineMap.MapNormal(frame.Normal1),
            AffineMap.MapNormal(frame.Normal2)
        );
    }
}