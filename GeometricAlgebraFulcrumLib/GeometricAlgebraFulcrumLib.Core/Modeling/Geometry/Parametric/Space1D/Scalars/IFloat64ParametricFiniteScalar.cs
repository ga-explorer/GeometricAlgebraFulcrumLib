namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;

public interface IFloat64ParametricFiniteScalar :
    IFloat64ParametricScalar
{
    double ParameterValueMin { get; }

    double ParameterValueMax { get; }
}