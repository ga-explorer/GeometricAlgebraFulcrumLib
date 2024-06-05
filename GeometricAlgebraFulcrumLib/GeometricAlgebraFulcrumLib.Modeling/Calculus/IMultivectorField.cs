using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus;

public interface IXGaMultivectorField<T>
{
    XGaProcessor<T> GeometricProcessor { get; }

    IXGaMultivectorFieldProcessor<T> FieldProcessor { get; }

    XGaMultivector<T> GetValue(XGaVector<T> v);

    XGaMultivector<T> GetVectorDerivativeValue(XGaVector<T> v, XGaVector<T> w);
}