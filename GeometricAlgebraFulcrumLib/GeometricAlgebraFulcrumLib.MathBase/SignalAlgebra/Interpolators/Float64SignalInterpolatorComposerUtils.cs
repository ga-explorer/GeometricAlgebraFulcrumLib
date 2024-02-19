using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra.Interpolators;

public static class Float64SignalInterpolatorComposerUtils
{
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVectorNevilleInterpolator CreateRGaNevillePolynomialInterpolator(this double samplingRate)
    {
        return RGaVectorNevilleInterpolator.Create(samplingRate);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVectorNevilleInterpolator CreateXGaNevillePolynomialInterpolator(this double samplingRate)
    {
        return XGaVectorNevilleInterpolator.Create(samplingRate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorBarycentricInterpolator CreateBarycentricPolynomialInterpolator(this double samplingRate, int vSpaceDimensions)
    {
        return VectorBarycentricInterpolator.Create(vSpaceDimensions, samplingRate);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorFourierInterpolator CreateFourierInterpolator(this IReadOnlyList<XGaFloat64Vector> signalSamples, double samplingRate, double energyThreshold = 0.998d)
    {
        return VectorFourierInterpolator.Create(
            signalSamples,
            samplingRate,
            energyThreshold
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorFourierInterpolator CreateFourierInterpolator(this IReadOnlyList<XGaFloat64Vector> signalSamples, double samplingRate, IEnumerable<int> frequencyIndexSet)
    {
        return VectorFourierInterpolator.Create(
            signalSamples,
            samplingRate,
            frequencyIndexSet
        );
    }
}