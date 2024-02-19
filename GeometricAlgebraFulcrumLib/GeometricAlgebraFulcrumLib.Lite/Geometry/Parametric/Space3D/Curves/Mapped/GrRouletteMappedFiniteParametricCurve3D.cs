using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.Maps.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Mapped;

public class GrRouletteMappedFiniteParametricCurve3D :
    IArcLengthCurve3D
{
    public IArcLengthCurve3D BaseCurve { get; }

    public RouletteMap3D RouletteMap { get; }

    public Float64ScalarRange ParameterRange
        => BaseCurve.ParameterRange;
        

    public GrRouletteMappedFiniteParametricCurve3D(IArcLengthCurve3D baseCurve, RouletteMap3D rouletteMap)
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
    public Float64Vector3D GetPoint(double parameterValue)
    {
        return RouletteMap.MapPoint(BaseCurve.GetPoint(parameterValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D GetDerivative1Point(double parameterValue)
    {
        return BaseCurve.GetDerivative1Point(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
    {
        var frame = BaseCurve.GetFrame(parameterValue);

        var point = RouletteMap.MapPoint(frame.Point);
        var (tangent, normal1, normal2) =
            RouletteMap.RotationQuaternion.RotateVectors(
                frame.Tangent,
                frame.Normal1,
                frame.Normal2
            );

        return ParametricCurveLocalFrame3D.Create(
            parameterValue,
            point,
            tangent,
            normal1,
            normal2
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