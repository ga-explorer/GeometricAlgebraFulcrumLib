using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;

public static class MathDf
{
    public static DfConstant Zero
        => DfConstant.Zero;

    public static DfConstant One
        => DfConstant.One;

    public static DfConstant Pi
        => DfConstant.Pi;

    public static DfConstant E
        => DfConstant.E;

    public static DfConstant Degree
        => DfConstant.Degree;

    public static DfVar X
        => DfVar.DefaultFunction;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialBasicFunction Number(double value)
    {
        return DfConstant.Create(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialFunction XPow(double powerValue)
    {
        return powerValue switch
        {
            0d => One,
            1d => X,
            _ => DfPowerScalar.Create(X, powerValue, false)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialFunction XPow(int powerValue, double scalarValue)
    {
        return scalarValue * XPow(powerValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfExp Exp(DifferentialFunction f)
    {
        return DfExp.Create(f);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfSin Sin(DifferentialFunction f)
    {
        return DfSin.Create(f);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfCos Cos(DifferentialFunction f)
    {
        return DfCos.Create(f);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Tuple<double, DifferentialFunction>> SeparateConstants(this IEnumerable<DifferentialFunction> argList)
    {
        return argList.Select(arg => arg switch
        {
            DfConstant argConstant =>
                new Tuple<double, DifferentialFunction>(argConstant.Value, DfConstant.One),

            DfTimes argTimes =>
                argTimes.SeparateConstant(),

            _ =>
                new Tuple<double, DifferentialFunction>(1d, arg)
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<double> GetValues(this IEnumerable<double> xValues, DifferentialFunction f)
    {
        return xValues.Select(f.GetValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<double> GetValues(this DifferentialFunction f, IEnumerable<double> xValues)
    {
        return xValues.Select(f.GetValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal SampleFunction(this Float64SampledTimeSignal tSignal, DifferentialFunction f)
    {
        return tSignal.MapSamples(f.GetValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Float64SampledTimeSignal> SampleDerivatives2(this Float64SampledTimeSignal tSignal, DifferentialFunction f)
    {
        var fDt1 = f.GetDerivative1();
        var fDt2 = fDt1.GetDerivative1();

        return new Pair<Float64SampledTimeSignal>(
            tSignal.MapSamples(fDt1.GetValue),
            tSignal.MapSamples(fDt2.GetValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<Float64SampledTimeSignal> SampleDerivatives3(this Float64SampledTimeSignal tSignal, DifferentialFunction f)
    {
        var fDt1 = f.GetDerivative1();
        var fDt2 = fDt1.GetDerivative1();
        var fDt3 = fDt2.GetDerivative1();

        return new Triplet<Float64SampledTimeSignal>(
            tSignal.MapSamples(fDt1.GetValue),
            tSignal.MapSamples(fDt2.GetValue),
            tSignal.MapSamples(fDt3.GetValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<Float64SampledTimeSignal> SampleDerivatives4(this Float64SampledTimeSignal tSignal, DifferentialFunction f)
    {
        var fDt1 = f.GetDerivative1();
        var fDt2 = fDt1.GetDerivative1();
        var fDt3 = fDt2.GetDerivative1();
        var fDt4 = fDt3.GetDerivative1();

        return new Quad<Float64SampledTimeSignal>(
            tSignal.MapSamples(fDt1.GetValue),
            tSignal.MapSamples(fDt2.GetValue),
            tSignal.MapSamples(fDt3.GetValue),
            tSignal.MapSamples(fDt4.GetValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Float64SampledTimeSignal> SampleFunctionDerivatives1(this Float64SampledTimeSignal tSignal, DifferentialFunction f)
    {
        var fDt1 = f.GetDerivative1();

        return new Pair<Float64SampledTimeSignal>(
            tSignal.MapSamples(f.GetValue),
            tSignal.MapSamples(fDt1.GetValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<Float64SampledTimeSignal> SampleFunctionDerivatives2(this Float64SampledTimeSignal tSignal, DifferentialFunction f)
    {
        var fDt1 = f.GetDerivative1();
        var fDt2 = fDt1.GetDerivative1();

        return new Triplet<Float64SampledTimeSignal>(
            tSignal.MapSamples(f.GetValue),
            tSignal.MapSamples(fDt1.GetValue),
            tSignal.MapSamples(fDt2.GetValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<Float64SampledTimeSignal> SampleFunctionDerivatives3(this Float64SampledTimeSignal tSignal, DifferentialFunction f)
    {
        var fDt1 = f.GetDerivative1();
        var fDt2 = fDt1.GetDerivative1();
        var fDt3 = fDt2.GetDerivative1();

        return new Quad<Float64SampledTimeSignal>(
            tSignal.MapSamples(f.GetValue),
            tSignal.MapSamples(fDt1.GetValue),
            tSignal.MapSamples(fDt2.GetValue),
            tSignal.MapSamples(fDt3.GetValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<Float64SampledTimeSignal> SampleFunctionDerivatives4(this Float64SampledTimeSignal tSignal, DifferentialFunction f)
    {
        var fDt1 = f.GetDerivative1();
        var fDt2 = fDt1.GetDerivative1();
        var fDt3 = fDt2.GetDerivative1();
        var fDt4 = fDt3.GetDerivative1();

        return new Quint<Float64SampledTimeSignal>(
            tSignal.MapSamples(f.GetValue),
            tSignal.MapSamples(fDt1.GetValue),
            tSignal.MapSamples(fDt2.GetValue),
            tSignal.MapSamples(fDt3.GetValue),
            tSignal.MapSamples(fDt4.GetValue)
        );
    }

}