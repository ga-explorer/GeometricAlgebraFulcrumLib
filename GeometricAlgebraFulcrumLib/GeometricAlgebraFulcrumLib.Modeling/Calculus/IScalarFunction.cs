using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus;

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