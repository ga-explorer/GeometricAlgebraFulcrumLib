using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using MathNet.Numerics.Interpolation;

namespace GeometricAlgebraFulcrumLib.Algebra.SignalAlgebra.Interpolators
{
    public class VectorBarycentricInterpolator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static VectorBarycentricInterpolator Create(IGeometricAlgebraProcessor<double> geometricProcessor, double samplingRate)
        {
            return new VectorBarycentricInterpolator(
                geometricProcessor,
                samplingRate
            );
        }


        public IGeometricAlgebraProcessor<double> GeometricProcessor { get; } 

        public double SamplingRate { get; }

        public int InterpolationSamples { get; set; } = 13;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private VectorBarycentricInterpolator([NotNull] IGeometricAlgebraProcessor<double> geometricProcessor, double samplingRate)
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

        private IEnumerable<Barycentric> GetInterpolatorList(IReadOnlyList<GaVector<double>> samples, double t)
        {
            var indexList = GetInterpolationSampleIndexList(samples.Count, t);

            var xValues = 
                indexList.Select(i => i / SamplingRate).ToArray();

            var vSpaceDimension = GeometricProcessor.VSpaceDimension;

            for (var j = 0; j < vSpaceDimension; j++)
            {
                var yValues =
                    indexList.Select(i => samples[i][j].ScalarValue).ToArray();

                yield return Barycentric.InterpolatePolynomialEquidistantSorted(xValues, yValues);
            }
        }
        
        private IEnumerable<Barycentric> GetInterpolatorList(GaVector<IReadOnlyList<double>> samples, int sampleCount, double t)
        {
            var indexList = GetInterpolationSampleIndexList(sampleCount, t);

            var xValues = 
                indexList.Select(i => i / SamplingRate).ToArray();

            var vSpaceDimension = GeometricProcessor.VSpaceDimension;

            for (var j = 0u; j < vSpaceDimension; j++)
            {
                var samplesArray = samples[j].ScalarValue;

                var yValues =
                    indexList.Select(i => samplesArray[i]).ToArray();

                yield return Barycentric.InterpolatePolynomialEquidistantSorted(xValues, yValues);
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
        public GaVector<double> GetVector(GaVector<IReadOnlyList<double>> samples, int sampleCount, double t)
        {
            var signalTime = (sampleCount - 1) / SamplingRate;
            if (t < 0 || t > signalTime)
                return GeometricProcessor.CreateVectorZero();

            var scalarArray = GetInterpolatorList(samples, sampleCount, t)
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
                .Select(p => (p.Interpolate(t + 1e-15) - p.Interpolate(t)) / 1e-15)
                .ToArray();
            
            return GeometricProcessor.CreateVector(scalarArray);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<double> GetVectorDt1(GaVector<IReadOnlyList<double>> samples, int sampleCount, double t)
        {
            var signalTime = (sampleCount - 1) / SamplingRate;
            if (t < 0 || t > signalTime)
                return GeometricProcessor.CreateVectorZero();

            var scalarArray = GetInterpolatorList(samples, sampleCount, t)
                .Select(p => (p.Interpolate(t + 1e-15) - p.Interpolate(t)) / 1e-15)
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
        
        public GaVector<IReadOnlyList<double>> GetVectorsDt1(GaVector<IReadOnlyList<double>> samples, int sampleCount)
        {
            var processor = samples.GeometricProcessor;

            var vectorList = Enumerable
                .Range(0, sampleCount)
                .Select(i => GetVectorDt1(samples, sampleCount, i / SamplingRate))
                .ToArray();

            var columnVectorArray = new IReadOnlyList<double>[processor.VSpaceDimension];

            for (var j = 0; j < processor.VSpaceDimension; j++)
            {
                var columnVector = new double[sampleCount];

                for (var i = 0; i < sampleCount; i++)
                    columnVector[i] = vectorList[i][j];

                columnVectorArray[j] = columnVector;
            }

            return processor.CreateVector(columnVectorArray);
        }
    }
}