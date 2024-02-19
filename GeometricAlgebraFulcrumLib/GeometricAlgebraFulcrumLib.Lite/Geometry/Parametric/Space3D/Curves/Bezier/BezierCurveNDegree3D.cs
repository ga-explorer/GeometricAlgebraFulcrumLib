using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Bezier;

public sealed class BezierCurveNDegree3D :
    IParametricCurve3D
{
    public bool IsValid() => ControlPoints.All(p => p.IsValid());

    public List<Float64Vector3D> ControlPoints { get; }
        = new List<Float64Vector3D>();

    public int Degree
        => ControlPoints.Count - 1;
        
    public Float64ScalarRange ParameterRange 
        => Float64ScalarRange.Infinite;


    public BezierCurveNDegree3D GetDerivativeCurve()
    {
        var result = new BezierCurveNDegree3D();

        for (var n = 0; n < Degree; n++)
            result.ControlPoints.Add(Degree * (ControlPoints[n + 1] - ControlPoints[n]));

        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D GetPoint(double t)
    {
        return t.DeCasteljau(ControlPoints.ToArray());
    }

    public Float64Vector3D GetDerivative1Point(double t)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
    {
        return ParametricCurveLocalFrame3D.Create(
            parameterValue,
            GetPoint(parameterValue),
            GetDerivative1Point(parameterValue)
        );
    }
}