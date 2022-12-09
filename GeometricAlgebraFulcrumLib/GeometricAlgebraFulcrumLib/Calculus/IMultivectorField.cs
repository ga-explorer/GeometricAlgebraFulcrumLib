using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.FunctionAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Calculus;

public interface IMultivectorField<T>
{
    IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

    IMultivectorFieldProcessor<T> FieldProcessor { get; }

    GaMultivector<T> GetValue(GaVector<T> v);

    GaMultivector<T> GetVectorDerivativeValue(GaVector<T> v, GaVector<T> w);
}