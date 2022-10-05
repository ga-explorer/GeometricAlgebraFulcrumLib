using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using MathNet.Numerics.Interpolation;

namespace GeometricAlgebraFulcrumLib.Algebra.SignalProcessing.Interpolators
{
    public class VectorFittingInterpolator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static VectorFittingInterpolator Create(IGeometricAlgebraProcessor<double> geometricProcessor, double samplingRate)
        {
            return new VectorFittingInterpolator(
                geometricProcessor,
                samplingRate
            );
        }


        public IGeometricAlgebraProcessor<double> GeometricProcessor { get; } 

        public double SamplingRate { get; }

        public int InterpolationSamples { get; set; } = 13;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private VectorFittingInterpolator([NotNull] IGeometricAlgebraProcessor<double> geometricProcessor, double samplingRate)
        {
            if (samplingRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(samplingRate));

            GeometricProcessor = geometricProcessor;
            SamplingRate = samplingRate;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IReadOnlyList<int> GetInterpolationSampleIndexList(int samplesCount, double t)
        {
            var index1 = 
                (int) Math.Floor(SamplingRate * t) - (InterpolationSamples - 1) / 2;

            if (index1 < 0)
                index1 = 0;

            else if (index1 >= samplesCount - InterpolationSamples) 
                index1 = samplesCount - InterpolationSamples;

            return Enumerable.Range(index1, InterpolationSamples).ToArray();
        }

        private IEnumerable<NevillePolynomialInterpolation> GetInterpolatorList(IReadOnlyList<GaVector<double>> samples, double t)
        {
            var indexList = GetInterpolationSampleIndexList(samples.Count, t);

            var xValues = 
                indexList.Select(i => i / SamplingRate).ToArray();

            var vSpaceDimension = GeometricProcessor.VSpaceDimension;

            for (var j = 0; j < vSpaceDimension; j++)
            {
                var yValues =
                    indexList.Select(i => samples[i][j].ScalarValue).ToArray();

                //yield return Fit.Polynomial(xValues, yValues, 3, DirectRegressionMethod.QR);
                yield return NevillePolynomialInterpolation.InterpolateSorted(xValues, yValues);
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<double> GetVector(IReadOnlyList<GaVector<double>> samples, double t)
        {
            var signalTime = (samples.Count - 1) / SamplingRate;
            if (t < 0 || t > signalTime)
                return GeometricProcessor.CreateVectorZero();

            var scalarArray = GetInterpolatorList(samples, t)
                .Select(p => p.Interpolate(t))
                .ToArray();
            
            return GeometricProcessor.CreateVector(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<double> GetVectorDt1(IReadOnlyList<GaVector<double>> samples, double t)
        {
            var signalTime = (samples.Count - 1) / SamplingRate;
            if (t < 0 || t > signalTime)
                return GeometricProcessor.CreateVectorZero();

            var scalarArray = GetInterpolatorList(samples, t)
                .Select(p => p.Differentiate(t))
                .ToArray();
            
            return GeometricProcessor.CreateVector(scalarArray);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<double> GetVectorDt2(IReadOnlyList<GaVector<double>> samples, double t)
        {
            var signalTime = (samples.Count - 1) / SamplingRate;
            if (t < 0 || t > signalTime)
                return GeometricProcessor.CreateVectorZero();

            var scalarArray = GetInterpolatorList(samples, t)
                .Select(p => p.Differentiate2(t))
                .ToArray();
            
            return GeometricProcessor.CreateVector(scalarArray);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<GaVector<double>> GetVectorsDt1(IReadOnlyList<GaVector<double>> samples)
        {
            return Enumerable
                .Range(0, samples.Count)
                .Select(i => GetVectorDt1(samples, i / SamplingRate))
                .ToArray();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<GaVector<double>> GetVectorsDt2(IReadOnlyList<GaVector<double>> samples)
        {
            return Enumerable
                .Range(0, samples.Count)
                .Select(i => GetVectorDt2(samples, i / SamplingRate))
                .ToArray();
        }
        

    }
}