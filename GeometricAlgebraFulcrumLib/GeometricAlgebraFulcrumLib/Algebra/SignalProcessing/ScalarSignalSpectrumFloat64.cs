using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath;

namespace GeometricAlgebraFulcrumLib.Algebra.SignalProcessing
{
    public sealed class ScalarSignalSpectrumFloat64 : 
        SignalSpectrum<double>
    {
        protected override double ZeroValue 
            => 0d;

        
        public ScalarSignalSpectrumFloat64(int sampleCount, double samplingRate) 
            : base(sampleCount, samplingRate)
        {
        }

        public ScalarSignalSpectrumFloat64(SignalSamplingSpecs samplingSpecs) 
            : base(samplingSpecs)
        {
        }
        
        public ScalarSignalSpectrumFloat64(SignalSamplingSpecs samplingSpecs, Dictionary<int, SignalSpectrumSample> indexSampleDictionary) 
            : base(samplingSpecs, indexSampleDictionary)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override bool IsZeroValue(double value)
        {
            return value.IsExactZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override double Negative(double value)
        {
            return -value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override double Add(double value1, double value2)
        {
            return value1 + value2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override double Subtract(double value1, double value2)
        {
            return value1 - value2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override double Times(double value1, double value2)
        {
            return value1 * value2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override SignalSpectrum<double> CreateSignalSpectrum(SignalSamplingSpecs samplingSpecs, Dictionary<int, SignalSpectrumSample> indexSampleDictionary)
        {
            return new ScalarSignalSpectrumFloat64(SamplingSpecs, indexSampleDictionary);
        }

        
        public ScalarSignalSpectrumFloat64 RemoveZeroValueSamples(double zeroEpsilon)
        {
            var indexArray = 
                IndexSampleDictionary
                    .Where(p => p.Value.Value.Abs().IsNearZero(zeroEpsilon))
                    .Select(p => p.Key)
                    .ToArray();

            foreach (var index in indexArray)
                IndexSampleDictionary.Remove(index);

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetEnergy(int index)
        {
            return this[index].Value.Square() * SampleCount / (2 * Math.PI);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetEnergyDc(int index)
        {
            return IsSampleIndexDc(index) ? GetEnergy(index) : 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetEnergyAc(int index)
        {
            return IsSampleIndexAc(index) ? GetEnergy(index) : 0d;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetEnergyAc(Pair<SignalSpectrumSample> spectrumSamplePair)
        {
            var (sample1, sample2) = spectrumSamplePair;

            var energy1 = 
                GetEnergyAc(sample1.Index);

            var energy2 = 
                (sample1.Index == sample2.Index) ? 0d : GetEnergyAc(sample2.Index);

            return energy1 + energy2;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValue(int index)
        {
            return this[index].Value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValueDc(int index)
        {
            return IsSampleIndexDc(index) ? GetValue(index) : 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValueAc(int index)
        {
            return IsSampleIndexAc(index) ? GetValue(index) : 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValueSumAc(Pair<SignalSpectrumSample> spectrumSamplePair)
        {
            var (sample1, sample2) = spectrumSamplePair;

            if (sample1.Index == sample2.Index)
                return GetValueAc(sample1.Index);

            var value1 = GetValueAc(sample1.Index);
            var value2 = (sample1.Index == sample2.Index) 
                ? 0d : IsSampleIndexAc(sample2.Index) ? GetValueAc(sample2.Index) : 0d;

            return value1 + value2;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValueAbs(int index)
        {
            return this[index].Value.Abs();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValueAbsDc(int index)
        {
            return IsSampleIndexDc(index) ? GetValueAbs(index) : 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValueAbsAc(int index)
        {
            return IsSampleIndexAc(index) ? GetValueAbs(index) : 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValueAbsSumAc(Pair<SignalSpectrumSample> spectrumSamplePair)
        {
            var (sample1, sample2) = spectrumSamplePair;

            if (sample1.Index == sample2.Index)
                return GetValueAbsAc(sample1.Index);

            var value1 = GetValueAbsAc(sample1.Index);
            var value2 = (sample1.Index == sample2.Index) 
                ? 0d : IsSampleIndexAc(sample2.Index) ? GetValueAbsAc(sample2.Index) : 0d;

            return value1 + value2;
        }


        public ScalarSignalSpectrumFloat64 SelectTopSamplesByCount(int spectrumSampleCount)
        {
            if (spectrumSampleCount < 1)
                throw new ArgumentOutOfRangeException(nameof(spectrumSampleCount));

            var spectrum = new ScalarSignalSpectrumFloat64(SamplingSpecs);

            var samplesList = 
                Samples.OrderByDescending(s => s.Value);

            foreach (var spectrumSample in samplesList)
            {
                spectrum.Add(spectrumSample);

                spectrumSampleCount--;

                if (spectrumSampleCount <= 0)
                    break;
            }

            spectrum.RemoveZeroValueSamples();

            return this;
        }

        public ScalarSignalSpectrumFloat64 SelectTopSamplesByValuePercent(double valuePercent)
        {
            if (valuePercent is <= 0 or > 1)
                throw new ArgumentOutOfRangeException(nameof(valuePercent));

            var minEnergy = valuePercent * ValueSum;

            var spectrum = new ScalarSignalSpectrumFloat64(SamplingSpecs);

            var samplesList = 
                Samples.OrderByDescending(s => s.Value);

            foreach (var spectrumSample in samplesList)
            {
                spectrum.Add(spectrumSample);

                minEnergy -= spectrumSample.Value;

                if (minEnergy <= 0)
                    break;
            }

            spectrum.RemoveZeroValueSamples();

            return this;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return IndexSampleDictionary
                .Values
                .OrderByDescending(spectrumSample => 
                    spectrumSample.Value.Abs()
                )
                .ThenBy(spectrumSample => 
                    GetFrequencyHz(spectrumSample.Index).Abs()
                )
                .ThenBy(spectrumSample => 
                    GetFrequencyHz(spectrumSample.Index).Sign()
                )
                .Select(spectrumSample => 
                    $"({spectrumSample.Value}) Exp[2π({GetFrequencyHz(spectrumSample.Index):G})i t]"
                )
                .ConcatenateText($" + {Environment.NewLine}");
        }
    }
}