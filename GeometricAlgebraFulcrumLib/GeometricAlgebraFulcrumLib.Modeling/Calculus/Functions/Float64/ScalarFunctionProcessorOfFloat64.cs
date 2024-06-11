using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Generic;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;

public class ScalarFunctionProcessorOfFloat64 :
    ScalarProcessorOfFloat64,
    IScalarFunctionProcessor<double>
{
    public new static ScalarFunctionProcessorOfFloat64 Instance { get; }
        = new ScalarFunctionProcessorOfFloat64();


    public IScalarProcessor<double> ScalarProcessor
        => this;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected ScalarFunctionProcessorOfFloat64()
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Func<double, double> Negative(Func<double, double> f1)
    {
        return t => -f1(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Func<double, double> Add(Func<double, double> f1, double f2)
    {
        return t => f1(t) + f2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Func<double, double> Add(double f1, Func<double, double> f2)
    {
        return t => f1 + f2(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Func<double, double> Add(Func<double, double> f1, Func<double, double> f2)
    {
        return t => f1(t) + f2(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Func<double, double> Subtract(Func<double, double> f1, double f2)
    {
        return t => f1(t) - f2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Func<double, double> Subtract(double f1, Func<double, double> f2)
    {
        return t => f1 - f2(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Func<double, double> Subtract(Func<double, double> f1, Func<double, double> f2)
    {
        return t => f1(t) - f2(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Func<double, double> Times(Func<double, double> f1, double f2)
    {
        return t => f1(t) * f2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Func<double, double> Times(double f1, Func<double, double> f2)
    {
        return t => f1 * f2(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Func<double, double> Times(Func<double, double> f1, Func<double, double> f2)
    {
        return t => f1(t) * f2(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Func<double, double> Divide(Func<double, double> f1, double f2)
    {
        return t => f1(t) / f2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Func<double, double> Divide(double f1, Func<double, double> f2)
    {
        return t => f1 / f2(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Func<double, double> Divide(Func<double, double> f1, Func<double, double> f2)
    {
        return t => f1(t) / f2(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Func<double, double> Compose(Func<double, double> f1, Func<double, double> f2)
    {
        return t => f2(f1(t));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetDerivativeValue(Func<double, double> scalarFunction, double t)
    {
        return MathNet.Numerics.Differentiate.FirstDerivative(scalarFunction, t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetDerivativeValue(Func<double, double> scalarFunction, int order, double t)
    {
        return MathNet.Numerics.Differentiate.Derivative(scalarFunction, t, order);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Func<double, double> GetDerivative(Func<double, double> scalarFunction)
    {
        return MathNet.Numerics.Differentiate.FirstDerivativeFunc(scalarFunction);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Func<double, double> GetDerivative(Func<double, double> scalarFunction, int order)
    {
        return MathNet.Numerics.Differentiate.DerivativeFunc(scalarFunction, order);
    }
}