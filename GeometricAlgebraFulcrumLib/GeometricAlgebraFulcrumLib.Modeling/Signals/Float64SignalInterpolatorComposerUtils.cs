using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Modeling.Signals.Interpolators;

namespace GeometricAlgebraFulcrumLib.Modeling.Signals;

public static class Float64SignalInterpolatorComposerUtils
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVectorPolynomialInterpolator CreatePolynomialInterpolator(this XGaVector<Float64SampledTimeSignal> vectorSamples, double samplingRate)
    {
        return XGaVectorPolynomialInterpolator.Create(vectorSamples);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorFourierInterpolator CreateFourierInterpolator(this XGaVector<Float64SampledTimeSignal> signalSamples, IEnumerable<int> frequencyIndexSet)
    {
        return VectorFourierInterpolator.Create(
            signalSamples,
            frequencyIndexSet
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorFourierInterpolator CreateFourierInterpolator(this XGaVector<Float64SampledTimeSignal> scalarSignal, double energyThreshold = 0.998d)
    {
        return VectorFourierInterpolator.Create(
            scalarSignal,
            energyThreshold
        );
    }
}