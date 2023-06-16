namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space1D.Curves;

public interface IParametricFiniteCurve1D :
    IParametricCurve1D
{
    double ParameterValueMin { get; }

    double ParameterValueMax { get; }
}