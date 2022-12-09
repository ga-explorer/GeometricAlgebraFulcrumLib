using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Polynomials;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Algebra.SignalAlgebra.Interpolators
{
    public class VectorPolynomialInterpolator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static VectorPolynomialInterpolator Create(IGeometricAlgebraProcessor<double> scalarProcessor, GaVector<ScalarSignalFloat64> scalarSamples)
        {
            return new VectorPolynomialInterpolator(
                scalarProcessor,
                scalarSamples
            );
        }


        public IGeometricAlgebraProcessor<double> GeometricProcessor { get; } 

        public double SamplingRate { get; }

        public int InterpolationSamples { get; set; } = 128;

        public int PolynomialOrder { get; set; } = 13;

        public int SampleCount { get; }

        public GaVector<ScalarSignalFloat64> VectorSamples { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private VectorPolynomialInterpolator([NotNull] IGeometricAlgebraProcessor<double> scalarProcessor, [NotNull] GaVector<ScalarSignalFloat64> scalarSamples)
        {
            GeometricProcessor = scalarProcessor;
            SamplingRate = scalarSamples.GetSamplingRate();
            VectorSamples = scalarSamples;
            SampleCount = scalarSamples.GetScalars().Max(s => s.Count);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IReadOnlyList<int> GetInterpolationSampleIndexList(double t)
        {
            // Only past samples
            var index1 = 
                (int) Math.Floor(SamplingRate * t) - (InterpolationSamples - 1);

            //// past and future samples
            //var index1 =
            //    (int) Math.Floor(SamplingRate * t) - (InterpolationSamples - 1) / 2; 

            if (index1 < 0)
                index1 = 0;

            else if (index1 >= SampleCount - InterpolationSamples) 
                index1 = SampleCount - InterpolationSamples;

            return Enumerable.Range(index1, InterpolationSamples).ToArray();
        }
        
        
        private IEnumerable<PolynomialFunction<double>> GetInterpolatorList(double t)
        {
            var indexList = GetInterpolationSampleIndexList(t);

            var xValues = 
                indexList.Select(i => i / SamplingRate).ToArray();

            var vSpaceDimension = GeometricProcessor.VSpaceDimension;

            for (var j = 0u; j < vSpaceDimension; j++)
            {
                var samplesArray = 
                    VectorSamples[j].ScalarValue;

                var yValues =
                    indexList.Select(i => samplesArray[i]).ToArray();

                yield return PolynomialFunction<double>.Create(
                    ScalarAlgebraFloat64Processor.DefaultProcessor,
                    Fit.Polynomial(xValues, yValues, PolynomialOrder)
                );
            }
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<double> GetVector(double t)
        {
            var signalTime = (SampleCount - 1) / SamplingRate;
            if (t < 0 || t > signalTime)
                return GeometricProcessor.CreateVectorZero();

            var scalarArray = GetInterpolatorList(t)
                .Select(p => p.GetValue(t))
                .ToArray();
            
            return GeometricProcessor.CreateVector(scalarArray);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<double> GetVectorDt1(double t)
        {
            var signalTime = (SampleCount - 1) / SamplingRate;
            if (t < 0 || t > signalTime)
                return GeometricProcessor.CreateVectorZero();

            var scalarArray = GetInterpolatorList(t)
                .Select(p => p.GetValueDt1(t))
                .ToArray();
            
            return GeometricProcessor.CreateVector(scalarArray);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<double> GetVectorDt2(double t)
        {
            var signalTime = (SampleCount - 1) / SamplingRate;
            if (t < 0 || t > signalTime)
                return GeometricProcessor.CreateVectorZero();

            var scalarArray = GetInterpolatorList(t)
                .Select(p => p.GetValueDt2(t))
                .ToArray();
            
            return GeometricProcessor.CreateVector(scalarArray);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<double> GetVectorDt(double t, int degree = 1)
        {
            var signalTime = (SampleCount - 1) / SamplingRate;
            if (t < 0 || t > signalTime)
                return GeometricProcessor.CreateVectorZero();

            var scalarArray = GetInterpolatorList(t)
                .Select(p => p.GetDerivative(degree).GetValue(t))
                .ToArray();
            
            return GeometricProcessor.CreateVector(scalarArray);
        }

        
        public GaVector<ScalarSignalFloat64> GetVectors()
        {
            var processor = VectorSamples.GeometricProcessor;

            var vectorList = Enumerable
                .Range(0, SampleCount)
                .Select(i => GetVector(i / SamplingRate))
                .ToArray();

            var columnVectorArray = new ScalarSignalFloat64[processor.VSpaceDimension];

            for (var j = 0; j < processor.VSpaceDimension; j++)
            {
                var columnVector = ScalarSignalFloat64.Create(SamplingRate, SampleCount);

                for (var i = 0; i < SampleCount; i++)
                    columnVector[i] = vectorList[i][j];

                columnVectorArray[j] = columnVector;
            }

            return processor.CreateVector(columnVectorArray);
        }
        
        public GaVector<ScalarSignalFloat64> GetVectorsDt1()
        {
            var processor = VectorSamples.GeometricProcessor;

            var vectorList = Enumerable
                .Range(0, SampleCount)
                .Select(i => GetVectorDt1(i / SamplingRate))
                .ToArray();

            var columnVectorArray = new ScalarSignalFloat64[processor.VSpaceDimension];

            for (var j = 0; j < processor.VSpaceDimension; j++)
            {
                var columnVector = ScalarSignalFloat64.Create(SamplingRate, SampleCount);

                for (var i = 0; i < SampleCount; i++)
                    columnVector[i] = vectorList[i][j];

                columnVectorArray[j] = columnVector;
            }

            return processor.CreateVector(columnVectorArray);
        }
        
        public GaVector<ScalarSignalFloat64> GetVectorsDt2()
        {
            var processor = VectorSamples.GeometricProcessor;

            var vectorList = Enumerable
                .Range(0, SampleCount)
                .Select(i => GetVectorDt2(i / SamplingRate))
                .ToArray();

            var columnVectorArray = new ScalarSignalFloat64[processor.VSpaceDimension];

            for (var j = 0; j < processor.VSpaceDimension; j++)
            {
                var columnVector = ScalarSignalFloat64.Create(SamplingRate, SampleCount);

                for (var i = 0; i < SampleCount; i++)
                    columnVector[i] = vectorList[i][j];

                columnVectorArray[j] = columnVector;
            }

            return processor.CreateVector(columnVectorArray);
        }
        
        public GaVector<ScalarSignalFloat64> GetVectorsDt(int degree = 1)
        {
            var processor = VectorSamples.GeometricProcessor;

            var vectorList = Enumerable
                .Range(0, SampleCount)
                .Select(i => GetVectorDt(i / SamplingRate, degree))
                .ToArray();

            var columnVectorArray = new ScalarSignalFloat64[processor.VSpaceDimension];

            for (var j = 0; j < processor.VSpaceDimension; j++)
            {
                var columnVector = ScalarSignalFloat64.Create(SamplingRate, SampleCount);

                for (var i = 0; i < SampleCount; i++)
                    columnVector[i] = vectorList[i][j];

                columnVectorArray[j] = columnVector;
            }

            return processor.CreateVector(columnVectorArray);
        }

    }
}