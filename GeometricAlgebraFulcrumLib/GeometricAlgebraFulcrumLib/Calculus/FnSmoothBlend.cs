using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.FunctionAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Calculus;

/// <summary>
/// https://www.youtube.com/watch?v=vD5g8aVscUI
/// </summary>
public sealed class FnSmoothBlend<T> :
    IScalarFunction<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static FnSmoothBlend<T> Create(Scalar<T> parameterValueMin, Scalar<T> parameterValueMax, IScalarFunction<T> scalarFunction1, IScalarFunction<T> scalarFunction2)
    {
        return new FnSmoothBlend<T>(
            parameterValueMin,
            parameterValueMax,
            scalarFunction1,
            scalarFunction2
        );
    }

    
    public IScalarAlgebraProcessor<T> ScalarProcessor 
        => FunctionProcessor.ScalarProcessor;

    public IScalarFunctionProcessor<T> FunctionProcessor
        => ScalarFunction1.FunctionProcessor;

    public Scalar<T> ParameterValueMin { get; }

    public Scalar<T> ParameterValueMax { get; }

    public IScalarFunction<T> ScalarFunction1 { get; }

    public IScalarFunction<T> ScalarFunction2 { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FnSmoothBlend(Scalar<T> parameterValueMin, Scalar<T> parameterValueMax, IScalarFunction<T> scalarFunction1, IScalarFunction<T> scalarFunction2)
    {
        ParameterValueMin = parameterValueMin;
        ParameterValueMax = parameterValueMax;
        ScalarFunction1 = scalarFunction1;
        ScalarFunction2 = scalarFunction2;
    }


    private Scalar<T> SmoothUnitStepFunction(Scalar<T> t)
    {
        if (t <= ParameterValueMin)
            return ScalarProcessor.CreateScalarZero();

        if (t >= ParameterValueMax)
            return ScalarProcessor.CreateScalarOne();

        t = (t - ParameterValueMin) / (ParameterValueMax - ParameterValueMin);

        var s = 1 - t;
        var x = 1 / t - 1 / s;

        return 1 / (1 + x.Exp());

        //var e1 = Math.Exp(-1d / t);
        //var e2 = Math.Exp(-1d / (1d - t));

        //return e1 / (e1 + e2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetValue(T t)
    {
        var x = SmoothUnitStepFunction(t.CreateScalar(ScalarProcessor));
        var y = 1 - x;

        return ScalarFunction1.GetValue(t) * y + ScalarFunction2.GetValue(t) * x;
    }

    public T GetDerivativeValue(T t)
    {
        throw new NotImplementedException();
    }

    public T GetDerivativeValue(T t, int degree)
    {
        throw new NotImplementedException();
    }

    public IScalarFunction<T> GetDerivative()
    {
        throw new NotImplementedException();
    }

    public IScalarFunction<T> GetDerivative(int degree)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarFunction<T> ToScalarFunction()
    {
        return ScalarFunction<T>.Create(
            FunctionProcessor,
            GetValue
        );
    }
}