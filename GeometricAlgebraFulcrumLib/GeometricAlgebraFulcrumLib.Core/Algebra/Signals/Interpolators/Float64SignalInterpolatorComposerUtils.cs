using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.Signals.Interpolators;

public static class Float64SignalInterpolatorComposerUtils
{
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarPolynomialInterpolator CreateScalarPolynomialInterpolator(this Float64Signal scalarSignal)
    {
        return ScalarPolynomialInterpolator.Create(scalarSignal, scalarSignal.SamplingRate);
    }

}