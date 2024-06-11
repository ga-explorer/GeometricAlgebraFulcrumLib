namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;

public interface IFloat64ParametricFiniteScalar :
    IFloat64ParametricScalar
{
    double ParameterValueMin { get; }

    double ParameterValueMax { get; }
}