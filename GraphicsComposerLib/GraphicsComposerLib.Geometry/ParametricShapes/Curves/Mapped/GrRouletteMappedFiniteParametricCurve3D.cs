using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Maps.Space3D;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Mapped;

public class GrRouletteMappedFiniteParametricCurve3D :
    IGraphicsC1ArcLengthCurve3D
{
    public IGraphicsC1ArcLengthCurve3D BaseCurve { get; }

    public RouletteMap3D RouletteMap { get; }
    
    public double ParameterValueMin 
        => BaseCurve.ParameterValueMin;

    public double ParameterValueMax 
        => BaseCurve.ParameterValueMax;


    public GrRouletteMappedFiniteParametricCurve3D(IGraphicsC1ArcLengthCurve3D baseCurve, RouletteMap3D rouletteMap)
    {
        BaseCurve = baseCurve;
        RouletteMap = rouletteMap;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return BaseCurve.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple3D GetPoint(double parameterValue)
    {
        return RouletteMap.MapPoint(BaseCurve.GetPoint(parameterValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple3D GetTangent(double parameterValue)
    {
        return BaseCurve.GetTangent(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple3D GetUnitTangent(double parameterValue)
    {
        return BaseCurve.GetUnitTangent(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricCurveLocalFrame3D GetFrame(double parameterValue)
    {
        var frame = BaseCurve.GetFrame(parameterValue);

        var point = RouletteMap.MapPoint(frame.Point);
        var (tangent, normal1, normal2) = 
            RouletteMap.RotationQuaternion.QuaternionRotate(
                frame.Tangent,
                frame.Normal1,
                frame.Normal2
            );

        return GrParametricCurveLocalFrame3D.Create(
            parameterValue,
            point,
            normal1,
            normal2,
            tangent
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