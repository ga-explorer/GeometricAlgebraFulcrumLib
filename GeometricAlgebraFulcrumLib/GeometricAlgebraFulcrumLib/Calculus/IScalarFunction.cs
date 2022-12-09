using GeometricAlgebraFulcrumLib.Processors.FunctionAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Calculus;

public interface IScalarFunction<T>
{
    IScalarAlgebraProcessor<T> ScalarProcessor { get; }

    IScalarFunctionProcessor<T> FunctionProcessor { get; }

    T GetValue(T t);

    T GetDerivativeValue(T t);

    T GetDerivativeValue(T t, int order);

    IScalarFunction<T> GetDerivative();

    IScalarFunction<T> GetDerivative(int order);

    ScalarFunction<T> ToScalarFunction();
}