using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.SignalAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra.Interpolators;

namespace GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra;

public static class Float64SignalInterpolatorComposerUtils
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVectorPolynomialInterpolator CreatePolynomialInterpolator(this XGaVector<Float64Signal> vectorSamples, double samplingRate)
    {
        return XGaVectorPolynomialInterpolator.Create(vectorSamples);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorFourierInterpolator CreateFourierInterpolator(this XGaVector<Float64Signal> signalSamples, IEnumerable<int> frequencyIndexSet)
    {
        return VectorFourierInterpolator.Create(
            signalSamples,
            frequencyIndexSet
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorFourierInterpolator CreateFourierInterpolator(this XGaVector<Float64Signal> scalarSignal, double energyThreshold = 0.998d)
    {
        return VectorFourierInterpolator.Create(
            scalarSignal,
            energyThreshold
        );
    }
}