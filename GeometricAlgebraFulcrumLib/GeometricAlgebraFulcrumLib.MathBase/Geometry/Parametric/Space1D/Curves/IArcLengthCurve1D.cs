namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space1D.Curves;

public interface IArcLengthCurve1D :
    IParametricFiniteCurve1D
{
    double GetLength();

    double ParameterToLength(double parameterValue);

    double LengthToParameter(double length);
}