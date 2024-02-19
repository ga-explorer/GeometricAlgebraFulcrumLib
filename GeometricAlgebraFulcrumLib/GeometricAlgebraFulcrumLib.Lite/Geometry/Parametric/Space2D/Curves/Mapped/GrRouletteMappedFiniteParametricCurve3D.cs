using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.Maps.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves.Mapped;

public class GrRouletteMappedFiniteParametricCurve2D :
    IArcLengthCurve2D
{
    public IArcLengthCurve2D BaseCurve { get; }

    public RouletteMap2D RouletteMap { get; }
        
    public Float64ScalarRange ParameterRange
        => BaseCurve.ParameterRange;



    public GrRouletteMappedFiniteParametricCurve2D(IArcLengthCurve2D baseCurve, RouletteMap2D rouletteMap)
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
    public Float64Vector2D GetPoint(double parameterValue)
    {
        return RouletteMap.MapPoint(BaseCurve.GetPoint(parameterValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D GetDerivative1Point(double parameterValue)
    {
        return BaseCurve.GetDerivative1Point(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame2D GetFrame(double parameterValue)
    {
        var frame = BaseCurve.GetFrame(parameterValue);

        var point = RouletteMap.MapPoint(frame.Point);
        var tangent = RouletteMap.RotationAngle.Rotate(frame.Tangent);

        return ParametricCurveLocalFrame2D.Create(
            parameterValue,
            point,
            tangent
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetLength()
    {
        return BaseCurve.GetLength();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ParameterToLength(double parameterValue)
    {
        return BaseCurve.ParameterToLength(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar LengthToParameter(double length)
    {
        return BaseCurve.LengthToParameter(length);
    }
}