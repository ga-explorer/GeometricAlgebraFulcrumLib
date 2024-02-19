using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.FunctionAlgebra;

namespace GeometricAlgebraFulcrumLib.Calculus;

public interface IScalarFunction<T>
{
    IScalarProcessor<T> ScalarProcessor { get; }

    IScalarFunctionProcessor<T> FunctionProcessor { get; }

    T GetValue(T t);

    T GetDerivativeValue(T t);

    T GetDerivativeValue(T t, int order);

    IScalarFunction<T> GetDerivative();

    IScalarFunction<T> GetDerivative(int order);

    ScalarFunction<T> ToScalarFunction();
}