using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves.Bezier;

public sealed class BezierCurveNDegree2D :
    IFloat64ParametricCurve2D
{
    public bool IsValid() => ControlPoints.All(p => p.IsValid());

    public List<LinFloat64Vector2D> ControlPoints { get; }
        = new List<LinFloat64Vector2D>();

    public int Degree
        => ControlPoints.Count - 1;
        
    public Float64ScalarRange ParameterRange 
        => Float64ScalarRange.Infinite;


    public BezierCurveNDegree2D GetDerivativeCurve()
    {
        var result = new BezierCurveNDegree2D();

        for (var n = 0; n < Degree; n++)
            result.ControlPoints.Add(Degree * (ControlPoints[n + 1] - ControlPoints[n]));

        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetPoint(double t)
    {
        return t.DeCasteljau(ControlPoints.ToArray());
    }

    public LinFloat64Vector2D GetDerivative1Point(double t)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame2D GetFrame(double parameterValue)
    {
        return ParametricCurveLocalFrame2D.Create(
            parameterValue,
            GetPoint(parameterValue),
            GetDerivative1Point(parameterValue)
        );
    }
}