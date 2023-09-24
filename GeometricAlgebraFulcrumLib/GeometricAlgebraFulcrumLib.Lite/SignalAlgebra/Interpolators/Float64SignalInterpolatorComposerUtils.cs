using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Lite.SignalAlgebra.Interpolators
{
    public static class Float64SignalInterpolatorComposerUtils
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarPolynomialInterpolator CreateScalarPolynomialInterpolator(this Float64Signal scalarSignal)
        {
            return ScalarPolynomialInterpolator.Create(scalarSignal, scalarSignal.SamplingRate);
        }

    }
}
