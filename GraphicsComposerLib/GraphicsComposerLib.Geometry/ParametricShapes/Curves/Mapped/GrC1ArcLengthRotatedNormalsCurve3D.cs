using System;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Mapped;

public class GrC1ArcLengthRotatedNormalsCurve3D :
    IGraphicsC1ArcLengthCurve3D
{
    public IGraphicsC1ArcLengthCurve3D BaseCurve { get; }

    public Func<double, PlanarAngle> AngleFunction { get; }

    public double ParameterValueMin 
        => BaseCurve.ParameterValueMin;

    public double ParameterValueMax 
        => BaseCurve.ParameterValueMax;


    public GrC1ArcLengthRotatedNormalsCurve3D(IGraphicsC1ArcLengthCurve3D baseCurve, Func<double, PlanarAngle> angleFunction)
    {
        BaseCurve = baseCurve;
        AngleFunction = angleFunction;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return BaseCurve.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple3D GetPoint(double parameterValue)
    {
        return BaseCurve.GetPoint(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple3D GetTangent(double parameterValue)
    {
        return BaseCurve.GetTangent(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple3D GetUnitTangent(double parameterValue)
    {
        return BaseCurve.GetUnitTangent(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricCurveLocalFrame3D GetFrame(double parameterValue)
    {
        return BaseCurve
            .GetFrame(parameterValue)
            .RotateNormalsBy(
                AngleFunction(parameterValue)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetLength()
    {
        return BaseCurve.GetLength();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ParameterToLength(double parameterValue)
    {
        return BaseCurve.ParameterToLength(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double LengthToParameter(double length)
    {
        return BaseCurve.LengthToParameter(length);
    }
}