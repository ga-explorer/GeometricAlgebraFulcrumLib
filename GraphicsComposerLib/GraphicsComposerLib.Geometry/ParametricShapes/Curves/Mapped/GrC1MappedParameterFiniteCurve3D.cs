using System;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Mapped;

public class GrC1MappedParameterFiniteCurve3D :
    IGraphicsC1ParametricFiniteCurve3D
{
    public IGraphicsC1ParametricFiniteCurve3D BaseCurve { get; }

    /// <summary>
    /// This function takes a number in the range [0, 1] and returns a number
    /// in the range [BaseCurve.ParameterValueMin, BaseCurve.ParameterValueMax]
    /// </summary>
    public Func<double, double> ParameterMapping { get; }

    public double ParameterValueMin
        => 0d;

    public double ParameterValueMax
        => 1d;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrC1MappedParameterFiniteCurve3D(IGraphicsC1ParametricFiniteCurve3D baseCurve)
    {
        BaseCurve = baseCurve;
        ParameterMapping = t => t.CosWave(
            baseCurve.ParameterValueMin,
            baseCurve.ParameterValueMax,
            1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrC1MappedParameterFiniteCurve3D(IGraphicsC1ParametricFiniteCurve3D baseCurve, Func<double, double> parameterMapping)
    {
        BaseCurve = baseCurve;
        ParameterMapping = parameterMapping;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return BaseCurve.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple3D GetPoint(double parameterValue)
    {
        return BaseCurve.GetPoint(
            ParameterMapping(parameterValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple3D GetTangent(double parameterValue)
    {
        return BaseCurve.GetTangent(
            ParameterMapping(parameterValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple3D GetUnitTangent(double parameterValue)
    {
        return GetTangent(parameterValue).ToUnitVector();
    }

    public GrParametricCurveLocalFrame3D GetFrame(double parameterValue)
    {
        return BaseCurve.GetFrame(
            ParameterMapping(parameterValue)
        );
    }
}