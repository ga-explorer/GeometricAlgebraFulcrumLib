using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Polynomials;
using GeometricAlgebraFulcrumLib.Modeling.Signals;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Interpolators;

public class DfChebyshevSignalInterpolator :
    DifferentialSignalInterpolatorFunction
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfChebyshevSignalInterpolator Create(Float64Signal signal, int sampleIndex1, int sampleIndex2, DfChebyshevSignalInterpolatorOptions options)
    {
        signal = signal.GetSmoothedSignal(options);
        var samplingSpecs = signal.SamplingSpecs;

        var sampleCount = sampleIndex2 - sampleIndex1 + 1;
        var (minVarValue, maxVarValue) =
            samplingSpecs.GetSampledTimeValues(sampleIndex1, sampleIndex2);

        var polynomial = DfChebyshevPolynomial.CreateApproximating(
            options.PolynomialDegree,
            signal.LinearInterpolation,
            minVarValue,
            maxVarValue,
            sampleCount
        );

        return new DfChebyshevSignalInterpolator(
            samplingSpecs,
            sampleIndex1,
            sampleIndex2,
            options,
            polynomial
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfChebyshevSignalInterpolator Create(Float64Signal signal, DfChebyshevSignalInterpolatorOptions options)
    {
        signal = signal.GetSmoothedSignal(options);
        var samplingSpecs = signal.SamplingSpecs;

        var polynomial = DfChebyshevPolynomial.CreateApproximating(
            options.PolynomialDegree,
            signal.LinearInterpolation,
            samplingSpecs.MinTime,
            samplingSpecs.MaxTime,
            samplingSpecs.SampleCount
        );

        return new DfChebyshevSignalInterpolator(
            samplingSpecs,
            0,
            samplingSpecs.SampleCount - 1,
            options,
            polynomial
        );
    }


    public DfChebyshevSignalInterpolatorOptions Options { get; }

    public DfChebyshevPolynomial Polynomial { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DfChebyshevSignalInterpolator(Float64SignalSamplingSpecs samplingSpecs, int sampleIndex1, int sampleIndex2, DfChebyshevSignalInterpolatorOptions options, DfChebyshevPolynomial polynomial)
        : base(samplingSpecs, sampleIndex1, sampleIndex2)
    {
        Options = options;
        Polynomial = polynomial;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        return Polynomial.GetValue(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction GetDerivative1()
    {
        var pDt1 = new DfChebyshevSignalInterpolator(
            SamplingSpecs,
            SampleIndex1,
            SampleIndex2,
            Options,
            Polynomial.GetPolynomialDerivative1()
        );

        //return pDt1;

        return Create(
            SamplingSpecs.GetSampledFunctionSignal(pDt1),
            SampleIndex1,
            SampleIndex2,
            Options
        );
    }
}