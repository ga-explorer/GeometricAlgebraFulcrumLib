using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Generic;

public interface IXGaMultivectorField<T>
{
    XGaProcessor<T> GeometricProcessor { get; }

    IXGaMultivectorFieldProcessor<T> FieldProcessor { get; }

    XGaMultivector<T> GetValue(XGaVector<T> v);

    XGaMultivector<T> GetVectorDerivativeValue(XGaVector<T> v, XGaVector<T> w);
}