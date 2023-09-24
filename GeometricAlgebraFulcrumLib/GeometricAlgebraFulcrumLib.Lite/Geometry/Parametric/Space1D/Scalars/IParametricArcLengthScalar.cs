namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;

public interface IParametricArcLengthScalar :
    IParametricFiniteScalar
{
    double GetLength();

    double ParameterToLength(double parameterValue);

    double LengthToParameter(double length);
}