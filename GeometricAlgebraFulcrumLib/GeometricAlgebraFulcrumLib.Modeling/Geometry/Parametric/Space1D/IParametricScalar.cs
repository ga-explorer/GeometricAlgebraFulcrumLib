using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Space1D
{
    public interface IParametricScalar<T>
    {
        ScalarRange<T> ParameterRange { get; }
        
        Scalar<T> GetValue(T parameterValue);

        Scalar<T> GetDerivative1Value(T parameterValue);
    }
}
