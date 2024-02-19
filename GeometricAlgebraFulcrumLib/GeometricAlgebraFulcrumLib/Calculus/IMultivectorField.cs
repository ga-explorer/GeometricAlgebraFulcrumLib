using GeometricAlgebraFulcrumLib.MathBase.FunctionAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Calculus;

public interface IXGaMultivectorField<T>
{
    XGaProcessor<T> GeometricProcessor { get; }

    IXGaMultivectorFieldProcessor<T> FieldProcessor { get; }

    XGaMultivector<T> GetValue(XGaVector<T> v);

    XGaMultivector<T> GetVectorDerivativeValue(XGaVector<T> v, XGaVector<T> w);
}