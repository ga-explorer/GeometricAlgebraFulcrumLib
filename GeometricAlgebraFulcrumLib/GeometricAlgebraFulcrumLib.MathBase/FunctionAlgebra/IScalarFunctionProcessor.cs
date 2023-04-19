using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.MathBase.FunctionAlgebra
{
    public interface IScalarFunctionProcessor<T>
    {
        IScalarProcessor<T> ScalarProcessor { get; }

        Func<T, T> Negative(Func<T, T> f1);

        Func<T, T> Add(Func<T, T> f1, T f2);

        Func<T, T> Add(T f1, Func<T, T> f2);

        Func<T, T> Add(Func<T, T> f1, Func<T, T> f2);

        Func<T, T> Subtract(Func<T, T> f1, T f2);

        Func<T, T> Subtract(T f1, Func<T, T> f2);

        Func<T, T> Subtract(Func<T, T> f1, Func<T, T> f2);

        Func<T, T> Times(Func<T, T> f1, T f2);

        Func<T, T> Times(T f1, Func<T, T> f2);

        Func<T, T> Times(Func<T, T> f1, Func<T, T> f2);

        Func<T, T> Divide(Func<T, T> f1, T f2);

        Func<T, T> Divide(T f1, Func<T, T> f2);

        Func<T, T> Divide(Func<T, T> f1, Func<T, T> f2);

        Func<T, T> Compose(Func<T, T> f1, Func<T, T> f2);

        T GetDerivativeValue(Func<T, T> scalarFunction, T t);

        T GetDerivativeValue(Func<T, T> scalarFunction, int order, T t);

        Func<T, T> GetDerivative(Func<T, T> scalarFunction);

        Func<T, T> GetDerivative(Func<T, T> scalarFunction, int order);
    }
}