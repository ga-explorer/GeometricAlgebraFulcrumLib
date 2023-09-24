namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;

public interface IParametricFiniteScalar :
    IParametricScalar
{
    double ParameterValueMin { get; }

    double ParameterValueMax { get; }
}