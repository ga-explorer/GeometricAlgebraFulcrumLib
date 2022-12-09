using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.SignalAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.SignalAlgebra.Interpolators;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class InterpolatorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorNevilleInterpolator CreateNevillePolynomialInterpolator(this IGeometricAlgebraProcessor<double> geometricProcessor, double samplingRate)
        {
            return VectorNevilleInterpolator.Create(geometricProcessor, samplingRate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorBarycentricInterpolator CreateBarycentricPolynomialInterpolator(this IGeometricAlgebraProcessor<double> geometricProcessor, double samplingRate)
        {
            return VectorBarycentricInterpolator.Create(geometricProcessor, samplingRate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarPolynomialInterpolator CreateScalarPolynomialInterpolator(this ScalarSignalFloat64 scalarSignal)
        {
            return ScalarPolynomialInterpolator.Create(scalarSignal, scalarSignal.SamplingRate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorPolynomialInterpolator CreatePolynomialInterpolator(this IGeometricAlgebraProcessor<double> geometricProcessor, GaVector<ScalarSignalFloat64> vectorSamples, double samplingRate)
        {
            return VectorPolynomialInterpolator.Create(geometricProcessor, vectorSamples);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFourierInterpolator CreateFourierInterpolator(this IReadOnlyList<GaVector<double>> signalSamples, double samplingRate, double energyThreshold = 0.998d)
        {
            return VectorFourierInterpolator.Create(
                signalSamples,
                samplingRate,
                energyThreshold
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFourierInterpolator CreateFourierInterpolator(this IReadOnlyList<GaVector<double>> signalSamples, double samplingRate, IEnumerable<int> frequencyIndexSet)
        {
            return VectorFourierInterpolator.Create(
                signalSamples,
                samplingRate,
                frequencyIndexSet
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFourierInterpolator CreateFourierInterpolator(this IGeometricAlgebraProcessor<double> geometricProcessor, GaVector<ScalarSignalFloat64> signalSamples, IEnumerable<int> frequencyIndexSet)
        {
            return VectorFourierInterpolator.Create(
                geometricProcessor,
                signalSamples,
                frequencyIndexSet
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFourierInterpolator CreateFourierInterpolator(this IGeometricAlgebraProcessor<double> geometricProcessor, GaVector<ScalarSignalFloat64> scalarSignal, double energyThreshold = 0.998d)
        {
            return VectorFourierInterpolator.Create(
                geometricProcessor,
                scalarSignal,
                energyThreshold
            );
        }
    }
}
