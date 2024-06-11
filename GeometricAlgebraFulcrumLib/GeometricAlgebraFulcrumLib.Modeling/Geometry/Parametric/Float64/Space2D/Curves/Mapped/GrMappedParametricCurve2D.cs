using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space2D.Curves.Mapped;

public class GrMappedParametricCurve2D :
    IFloat64ParametricCurve2D
{
    public IFloat64ParametricCurve2D BaseCurve { get; }

    public IAffineMap2D Map { get; }

    public Float64ScalarRange ParameterRange
        => BaseCurve.ParameterRange;


    public GrMappedParametricCurve2D(IFloat64ParametricCurve2D baseCurve, IAffineMap2D map)
    {
        BaseCurve = baseCurve;
        Map = map;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return BaseCurve.IsValid() &&
               Map.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetPoint(double parameterValue)
    {
        return Map.MapPoint(BaseCurve.GetPoint(parameterValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetDerivative1Point(double parameterValue)
    {
        return Map.MapPoint(BaseCurve.GetDerivative1Point(parameterValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame2D GetFrame(double parameterValue)
    {
        var frame = BaseCurve.GetFrame(parameterValue);

        return ParametricCurveLocalFrame2D.Create(
            parameterValue,
            Map.MapPoint(frame.Point),
            Map.MapVector(frame.Tangent)
        );
    }
}