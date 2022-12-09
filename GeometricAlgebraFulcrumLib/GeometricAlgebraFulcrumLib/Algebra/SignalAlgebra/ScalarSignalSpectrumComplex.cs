using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib;
using DataStructuresLib.Basic;
using MathNet.Numerics;
using NumericalGeometryLib.BasicMath;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Algebra.SignalAlgebra
{
    public sealed class ScalarSignalSpectrumComplex : 
        SignalSpectrum<Complex>
    {
        protected override Complex ZeroValue 
            => Complex.Zero;
        

        public new IEnumerable<SignalSpectrumSample> Samples 
            => base
                .Samples
                .OrderByDescending(spectrumSample =>
                    spectrumSample.Value.MagnitudeSquared()
                )
                .ThenBy(spectrumSample =>
                    GetFrequencyHz(spectrumSample.Index).Abs()
                )
                .ThenBy(spectrumSample =>
                    GetFrequencyHz(spectrumSample.Index).Sign()
                );

        public new IEnumerable<SignalSpectrumSample> SamplesDc
            => base
                .SamplesDc
                .OrderByDescending(spectrumSample =>
                    spectrumSample.Value.MagnitudeSquared()
                )
                .ThenBy(spectrumSample =>
                    GetFrequencyHz(spectrumSample.Index).Abs()
                )
                .ThenBy(spectrumSample =>
                    GetFrequencyHz(spectrumSample.Index).Sign()
                );

        public new IEnumerable<SignalSpectrumSample> SamplesAc
            => base
                .SamplesAc
                .OrderByDescending(spectrumSample =>
                    spectrumSample.Value.MagnitudeSquared()
                )
                .ThenBy(spectrumSample =>
                    GetFrequencyHz(spectrumSample.Index).Abs()
                )
                .ThenBy(spectrumSample =>
                    GetFrequencyHz(spectrumSample.Index).Sign()
                );


        public ScalarSignalSpectrumComplex(int sampleCount, double samplingRate) 
            : base(sampleCount, samplingRate)
        {
        }

        public ScalarSignalSpectrumComplex(SignalSamplingSpecs samplingSpecs) 
            : base(samplingSpecs)
        {
        }

        internal ScalarSignalSpectrumComplex(SignalSamplingSpecs samplingSpecs, Dictionary<int, SignalSpectrumSample> indexSampleDictionary) 
            : base(samplingSpecs, indexSampleDictionary)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override bool IsZeroValue(Complex value)
        {
            return value.Real.IsExactZero() && value.Imaginary.IsExactZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override Complex Negative(Complex value)
        {
            return -value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override Complex Add(Complex value1, Complex value2)
        {
            return value1 + value2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override Complex Subtract(Complex value1, Complex value2)
        {
            return value1 - value2;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override Complex Times(Complex value1, Complex value2)
        {
            return value1 * value2;
        }

        
        public ScalarSignalSpectrumComplex RemoveZeroValueSamples(double zeroEpsilon)
        {
            var indexArray = 
                IndexSampleDictionary
                    .Where(p => p.Value.Value.Magnitude.IsNearZero(zeroEpsilon))
                    .Select(p => p.Key)
                    .ToArray();

            foreach (var index in indexArray)
                IndexSampleDictionary.Remove(index);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override SignalSpectrum<Complex> CreateSignalSpectrum(SignalSamplingSpecs samplingSpecs, Dictionary<int, SignalSpectrumSample> indexSampleDictionary)
        {
            return new ScalarSignalSpectrumComplex(SamplingSpecs, indexSampleDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex GetValue(int spectrumSampleIndex, Complex spectrumSampleValue, double t)
        {
            var frequency = GetFrequency(spectrumSampleIndex);

            return spectrumSampleValue * Complex.FromPolarCoordinates(
                1,
                frequency * t
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex GetValue(SignalSpectrumSample spectrumSample, double t)
        {
            var frequency = 
                GetFrequency(spectrumSample.Index);

            return spectrumSample.Value * Complex.Exp(frequency * Complex.ImaginaryOne * t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex GetValueDt1(SignalSpectrumSample spectrumSample, double t)
        {
            var frequency = 
                GetFrequency(spectrumSample.Index) * 
                Complex.ImaginaryOne;

            return spectrumSample.Value * 
                   frequency * 
                   Complex.Exp(frequency * t);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex GetValueDt2(SignalSpectrumSample spectrumSample, double t)
        {
            var frequency = 
                GetFrequency(spectrumSample.Index) * 
                Complex.ImaginaryOne;

            return spectrumSample.Value * 
                   frequency * frequency *
                   Complex.Exp(frequency * t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex GetValueDt(SignalSpectrumSample spectrumSample, int degree, double t)
        {
            if (degree < 0)
                throw new ArgumentOutOfRangeException(nameof(degree));

            if (degree == 0)
                return GetValue(spectrumSample, t);

            var frequency = 
                GetFrequency(spectrumSample.Index) * 
                Complex.ImaginaryOne;

            return spectrumSample.Value * 
                   Complex.Pow(frequency, degree) * 
                   Complex.Exp(frequency * t);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<Complex> GetValues(SignalSpectrumSample spectrumSample, IEnumerable<double> tValues)
        {
            return tValues.Select(t => 
                GetValue(spectrumSample, t)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<Complex> GetValuesDt1(SignalSpectrumSample spectrumSample, IEnumerable<double> tValues)
        {
            return tValues.Select(t => 
                GetValueDt1(spectrumSample, t)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<Complex> GetValuesDt2(SignalSpectrumSample spectrumSample, IEnumerable<double> tValues)
        {
            return tValues.Select(t => 
                GetValueDt2(spectrumSample, t)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<Complex> GetValuesDt(SignalSpectrumSample spectrumSample, int degree, IEnumerable<double> tValues)
        {
            return tValues.Select(t => 
                GetValueDt(spectrumSample, degree, t)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetRealSignal(SignalSpectrumSample spectrumSample, IEnumerable<double> tValues)
        {
            return tValues
                .Select(t => GetValue(spectrumSample, t).Real)
                .CreateSignal(SamplingRate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetRealSignalDt1(SignalSpectrumSample spectrumSample, IEnumerable<double> tValues)
        {
            return tValues
                .Select(t => GetValueDt1(spectrumSample, t).Real)
                .CreateSignal(SamplingRate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetRealSignalDt2(SignalSpectrumSample spectrumSample, IEnumerable<double> tValues)
        {
            return tValues
                .Select(t => GetValueDt2(spectrumSample, t).Real)
                .CreateSignal(SamplingRate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetRealSignalDt(SignalSpectrumSample spectrumSample, int degree, IEnumerable<double> tValues)
        {
            return tValues
                .Select(t => GetValueDt(spectrumSample, degree, t).Real)
                .CreateSignal(SamplingRate);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex GetValue(double t)
        {
            return Samples
                .Select(sample => GetValue(sample, t))
                .Aggregate(Complex.Zero, (a, b) => a + b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex GetValueDt1(double t)
        {
            return Samples
                .Select(sample => GetValueDt1(sample, t))
                .Aggregate(Complex.Zero, (a, b) => a + b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex GetValueDt2(double t)
        {
            return Samples
                .Select(sample => GetValueDt2(sample, t))
                .Aggregate(Complex.Zero, (a, b) => a + b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex GetValueDt(int degree, double t)
        {
            return degree switch
            {
                < 0 => throw new ArgumentOutOfRangeException(nameof(degree)),
                0 => GetValue(t),
                1 => GetValueDt1(t),
                2 => GetValueDt2(t),
                _ => Samples
                    .Select(sample => GetValueDt(sample, degree, t))
                    .Aggregate(Complex.Zero, (a, b) => a + b)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<Complex> GetValues(IEnumerable<double> tValues)
        {
            return tValues.Select(GetValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetRealSignal(ScalarSignalFloat64 tValues)
        {
            var complexSignal =
                tValues.Select(GetValue).ToImmutableArray();

            Debug.Assert(complexSignal.All(c => c.Imaginary.IsNearZero()));

            return complexSignal
                .Select(c => c.Real)
                .CreateSignal(tValues.SamplingRate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetRealSignalDt1(ScalarSignalFloat64 tValues)
        {
            return tValues
                .Select(t => GetValueDt1(t).Real)
                .CreateSignal(SamplingRate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetRealSignalDt2(ScalarSignalFloat64 tValues)
        {
            return tValues
                .Select(t => GetValueDt2(t).Real)
                .CreateSignal(SamplingRate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetRealSignalDt(int degree, ScalarSignalFloat64 tValues)
        {
            return tValues
                .Select(t => GetValueDt(degree, t).Real)
                .CreateSignal(SamplingRate);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalSpectrumComplex GetSpectrumDt1()
        {
            return (ScalarSignalSpectrumComplex) MapValuesByFrequencyValue(
                (frequency, value) => Complex.ImaginaryOne * frequency * value
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalSpectrumComplex GetSpectrumDt2()
        {
            return (ScalarSignalSpectrumComplex) MapValuesByFrequencyValue(
                (frequency, value) => -frequency * frequency * value
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalSpectrumComplex GetSpectrumDt(int degree)
        {
            return degree switch
            {
                < 0 => throw new ArgumentOutOfRangeException(nameof(degree)),
                0 => (ScalarSignalSpectrumComplex) GetCopy(),
                1 => GetSpectrumDt1(),
                2 => GetSpectrumDt2(),
                _ => (ScalarSignalSpectrumComplex) MapValuesByFrequencyValue(
                    (frequency, value) => 
                        Complex.Pow(Complex.ImaginaryOne * frequency, degree) * value
                )
            };
        }


        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetEnergy(SignalSpectrumSample spectrumSample)
        {
            return spectrumSample.Value.MagnitudeSquared() * SampleCount / (2 * Math.PI);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetEnergyDc(SignalSpectrumSample spectrumSample)
        {
            return IsSampleIndexDc(spectrumSample.Index) 
                ? GetEnergy(spectrumSample) 
                : 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetEnergyAc(SignalSpectrumSample spectrumSample)
        {
            return IsSampleIndexAc(spectrumSample.Index) 
                ? GetEnergy(spectrumSample) 
                : 0d;
        }


        public ScalarSignalSpectrumFloat64 GetEnergySpectrum()
        {
            var scalingFactor = SampleCount / (2 * Math.PI);
            var energySpectrum = new ScalarSignalSpectrumFloat64(SamplingSpecs);

            foreach (var (index, value) in IndexSampleDictionary.Values)
                energySpectrum.Set(index, value.MagnitudeSquared() * scalingFactor);

            return energySpectrum;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetEnergy()
        {
            return IndexSampleDictionary.Values.Sum(GetEnergy);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetEnergyDc()
        {
            return IndexSampleDictionary.Values.Sum(GetEnergyDc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetEnergyAc()
        {
            return IndexSampleDictionary.Values.Sum(GetEnergyAc);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetEnergy(int index)
        {
            return this[index].Value.MagnitudeSquared() * SampleCount / (2 * Math.PI);
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
        public double GetEnergy(int index, ScalarSignalFloat64 tValues)
        {
            var spectrumSample = this[index];

            return tValues.Sum(t => 
                GetValue(spectrumSample, t).MagnitudeSquared() / (2 * Math.PI)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetEnergyDc(int index, ScalarSignalFloat64 tValues)
        {
            if (!IsSampleIndexDc(index))
                return 0;

            var spectrumSample = this[index];

            return tValues
                .Select(t => GetValue(spectrumSample, t))
                .Sum()
                .MagnitudeSquared() / (2 * Math.PI * Count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetEnergyAc(int index, ScalarSignalFloat64 tValues)
        {
            if (!IsSampleIndexAc(index))
                return 0;

            return GetEnergy(index, tValues) - GetEnergyDc(index, tValues);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetEnergy(ScalarSignalFloat64 tValues)
        {
            return tValues.Sum(t => 
                GetValue(t).MagnitudeSquared() / (2 * Math.PI)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetEnergyDc(ScalarSignalFloat64 tValues)
        {
            return tValues
                .Select(GetValue)
                .Sum()
                .MagnitudeSquared() / (2 * Math.PI * Count);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetEnergyAc(ScalarSignalFloat64 tValues)
        {
            return GetEnergy(tValues) - GetEnergyDc(tValues);
        }


        public string GetTextDescription(string signalName, double totalEnergyAc, ScalarSignalFloat64 tValues)
        {
            var sampleCount = tValues.Count;

            var composer = new StringBuilder();

            composer.AppendLine($"Fourier signal {signalName}: Original AC energy = {totalEnergyAc:G}, Interpolator AC energy = {GetEnergyAc():G}");
            
            var energySum = 0d;
            var termIndex = 0;
            foreach (var spectrumSample in Samples)
            {
                var energyTime = GetEnergyAc(spectrumSample.Index, tValues);
                var energyFreq = GetEnergyAc(spectrumSample.Index);
                var energyTimeFreqRatio = energyTime / energyFreq;
                energySum += energyTime;

                var energyPercent = energyTime / totalEnergyAc;
                var energySumPercent = energySum / totalEnergyAc;

                var frequency = GetFrequency(spectrumSample.Index);
                var frequencyHz = GetFrequencyHz(spectrumSample.Index);

                composer
                    .AppendLine(@$"{termIndex,5} Term {spectrumSample.Index}: {ToString(spectrumSample)}")
                    .AppendLine(@$"      Frequency    : {frequencyHz:G} Hz = {frequency:G} rad/sec")
                    .AppendLine(@$"      AC Energy (time) : {energyTime:G}")
                    .AppendLine(@$"      AC Energy (freq) : {energyFreq:G}")
                    .AppendLine(@$"      AC Energy Ratio (time / frq): {energyTimeFreqRatio:G}")
                    .AppendLine(@$"      AC Energy %  : {energyPercent:P5}")
                    .AppendLine(@$"      AC Energy Sum: {energySumPercent:P5}")
                    .AppendLine();

                termIndex++;
            }

            return composer.ToString();
        }

        public string GetTextDescription(ScalarSignalFloat64 vectorSignal1)
        {
            var composer = new LinearTextComposer();

            var tValues = SamplingSpecs.GetTimeValuesSignal();

            composer
                .AppendLine("Original Signal:")
                .IncreaseIndentation()
                .AppendLine($"Energy: {vectorSignal1.Energy():G}")
                .AppendLine($"Energy DC: {vectorSignal1.EnergyDc():G}")
                .AppendLine($"Energy AC: {vectorSignal1.EnergyAc():G}")
                .AppendLine()
                .DecreaseIndentation();

            var vectorSignal2 = 
                GetRealSignal(tValues);

            composer
                .AppendLine("Interpolated Signal:")
                .IncreaseIndentation()
                .AppendLine($"Energy: {vectorSignal2.Energy():G}")
                .AppendLine($"Energy DC: {vectorSignal2.EnergyDc():G}")
                .AppendLine($"Energy AC: {vectorSignal2.EnergyAc():G}")
                .AppendLine()
                .DecreaseIndentation();

            var errorSignal = 
                vectorSignal1 - vectorSignal2;

            composer
                .AppendLine("Error Signal:")
                .IncreaseIndentation()
                .AppendLine($"Energy ratio: {vectorSignal2.Energy() / vectorSignal1.Energy():P3}")
                .AppendLine($"Energy ratio DC: {vectorSignal2.EnergyDc() / vectorSignal1.EnergyDc():P3}")
                .AppendLine($"Energy ratio AC: {vectorSignal2.EnergyAc() / vectorSignal1.EnergyAc():P3}")
                .AppendLine($"Signal to noise ratio: {vectorSignal1.SignalToNoiseRatio(errorSignal)}")
                .AppendLine()
                .DecreaseIndentation();
            
            var minFreqHz = FrequencyMinHz;
            var maxFreqHz = FrequencyMaxHz;
            var freqSampleCount = FrequencyIndices.Count();

            composer
                .AppendLine("Spectrum:")
                .IncreaseIndentation()
                .AppendLine($"Frequency range: {minFreqHz:G}, {maxFreqHz:G}")
                .AppendLine($"Frequency sample count: {freqSampleCount}")
                .AppendLine()
                .DecreaseIndentation();

            composer
                .AppendLine()
                .DecreaseIndentation();

            return composer.ToString();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(SignalSpectrumSample spectrumSample)
        {
            var (index, value) = spectrumSample;

            var frequencyHz = GetFrequencyHz(index);

            return $"({value}) Exp[2π({frequencyHz:G})i t]";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return Samples
                .Select(ToString)
                .ConcatenateText($" + {Environment.NewLine}");
        }
    }
}