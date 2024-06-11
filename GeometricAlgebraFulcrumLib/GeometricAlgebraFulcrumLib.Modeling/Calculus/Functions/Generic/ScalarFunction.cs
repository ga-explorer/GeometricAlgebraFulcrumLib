using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Generic;

public sealed class ScalarFunction<T> :
    IScalarFunction<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFunction<T> Create(IScalarFunctionProcessor<T> functionProcessor, Func<T, T> scalarFunc)
    {
        return new ScalarFunction<T>(
            functionProcessor,
            scalarFunc
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFunction<T> operator -(ScalarFunction<T> f1)
    {
        var processor = f1.FunctionProcessor;

        return new ScalarFunction<T>(
            processor,
            processor.Negative(f1.ScalarFunc)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFunction<T> operator +(ScalarFunction<T> f1, T f2)
    {
        var processor = f1.FunctionProcessor;

        return new ScalarFunction<T>(
            processor,
            processor.Add(f1.ScalarFunc, f2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFunction<T> operator +(T f1, ScalarFunction<T> f2)
    {
        var processor = f2.FunctionProcessor;

        return new ScalarFunction<T>(
            processor,
            processor.Add(f1, f2.ScalarFunc)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFunction<T> operator +(ScalarFunction<T> f1, ScalarFunction<T> f2)
    {
        var processor = f1.FunctionProcessor;

        return new ScalarFunction<T>(
            processor,
            processor.Add(f1.ScalarFunc, f2.ScalarFunc)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFunction<T> operator -(ScalarFunction<T> f1, T f2)
    {
        var processor = f1.FunctionProcessor;

        return new ScalarFunction<T>(
            processor,
            processor.Subtract(f1.ScalarFunc, f2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFunction<T> operator -(T f1, ScalarFunction<T> f2)
    {
        var processor = f2.FunctionProcessor;

        return new ScalarFunction<T>(
            processor,
            processor.Subtract(f1, f2.ScalarFunc)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFunction<T> operator -(ScalarFunction<T> f1, ScalarFunction<T> f2)
    {
        var processor = f1.FunctionProcessor;

        return new ScalarFunction<T>(
            processor,
            processor.Subtract(f1.ScalarFunc, f2.ScalarFunc)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFunction<T> operator *(ScalarFunction<T> f1, T f2)
    {
        var processor = f1.FunctionProcessor;

        return new ScalarFunction<T>(
            processor,
            processor.Times(f1.ScalarFunc, f2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFunction<T> operator *(T f1, ScalarFunction<T> f2)
    {
        var processor = f2.FunctionProcessor;

        return new ScalarFunction<T>(
            processor,
            processor.Times(f1, f2.ScalarFunc)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFunction<T> operator *(ScalarFunction<T> f1, ScalarFunction<T> f2)
    {
        var processor = f1.FunctionProcessor;

        return new ScalarFunction<T>(
            processor,
            processor.Times(f1.ScalarFunc, f2.ScalarFunc)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFunction<T> operator /(ScalarFunction<T> f1, T f2)
    {
        var processor = f1.FunctionProcessor;

        return new ScalarFunction<T>(
            processor,
            processor.Divide(f1.ScalarFunc, f2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFunction<T> operator /(T f1, ScalarFunction<T> f2)
    {
        var processor = f2.FunctionProcessor;

        return new ScalarFunction<T>(
            processor,
            processor.Divide(f1, f2.ScalarFunc)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFunction<T> operator /(ScalarFunction<T> f1, ScalarFunction<T> f2)
    {
        var processor = f1.FunctionProcessor;

        return new ScalarFunction<T>(
            processor,
            processor.Divide(f1.ScalarFunc, f2.ScalarFunc)
        );
    }


    public IScalarProcessor<T> ScalarProcessor
        => FunctionProcessor.ScalarProcessor;

    public IScalarFunctionProcessor<T> FunctionProcessor { get; }

    public Func<T, T> ScalarFunc { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScalarFunction(IScalarFunctionProcessor<T> functionProcessor, Func<T, T> scalarFunction)
    {
        FunctionProcessor = functionProcessor;
        ScalarFunc = scalarFunction;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetValue(T t)
    {
        return ScalarFunc(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetDerivativeValue(T t)
    {
        return FunctionProcessor.GetDerivativeValue(ScalarFunc, t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetDerivativeValue(T t, int order)
    {
        return FunctionProcessor.GetDerivativeValue(ScalarFunc, order, t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IScalarFunction<T> GetDerivative()
    {
        return new ScalarFunction<T>(
            FunctionProcessor,
            FunctionProcessor.GetDerivative(ScalarFunc)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IScalarFunction<T> GetDerivative(int order)
    {
        return new ScalarFunction<T>(
            FunctionProcessor,
            FunctionProcessor.GetDerivative(ScalarFunc, order)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarFunction<T> ToScalarFunction()
    {
        return this;
    }
}