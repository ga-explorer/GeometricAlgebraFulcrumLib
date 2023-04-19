using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.MathBase.Signals
{
    public abstract class SignalSpectrum<T> : 
        IReadOnlyList<SignalSpectrum<T>.SignalSpectrumSample>
    {
        public sealed record SignalSpectrumSample(int Index, T Value);


        protected readonly Dictionary<int, SignalSpectrumSample> IndexSampleDictionary;


        public int Count 
            => SamplingSpecs.SampleCount;

        public SignalSamplingSpecs SamplingSpecs { get; }

        public int SampleCount 
            => SamplingSpecs.SampleCount;

        public double SamplingRate 
            => SamplingSpecs.SamplingRate;
        
        public double FrequencyResolution 
            => SamplingSpecs.FrequencyResolution;
        
        public double FrequencyResolutionHz
            => SamplingSpecs.FrequencyResolutionHz;

        public T ValueSum 
            => IndexSampleDictionary
                .Values
                .Select(v => v.Value)
                .Aggregate(ZeroValue, Add);

        public IEnumerable<SignalSpectrumSample> Samples
            => IndexSampleDictionary.Values;
        
        public IEnumerable<SignalSpectrumSample> SamplesDc
            => IndexSampleDictionary
                .Values
                .Where(spectrumSample => IsSampleIndexDc(spectrumSample.Index));
        
        public IEnumerable<SignalSpectrumSample> SamplesAc
            => IndexSampleDictionary
                .Values
                .Where(spectrumSample => IsSampleIndexAc(spectrumSample.Index));
        
        public IEnumerable<Pair<SignalSpectrumSample>> SamplePairsAc
        {
            get
            {
                for (var i1 = 1; i1 < SampleCount; i1++)
                {
                    var i2 = SampleCount - i1;

                    if (i2 < i1)
                        break;

                    yield return new Pair<SignalSpectrumSample>(this[i1], this[i2]);
                }
            }
        }

        public IEnumerable<int> FrequencyIndices
            => Samples.Select(r => r.Index);

        public IEnumerable<double> Frequencies
            => Samples.Select(r => GetFrequency(r.Index));
        
        public IEnumerable<double> FrequenciesHz
            => Samples.Select(r => GetFrequencyHz(r.Index));
        
        public double FrequencyMin 
            => Frequencies.Min();
        
        public double FrequencyMinHz 
            => FrequenciesHz.Min();

        public double FrequencyMax 
            => Frequencies.Max();
        
        public double FrequencyMaxHz 
            => FrequenciesHz.Max();

        public Pair<double> FrequencyRange 
            => Frequencies.GetRange();
        
        public Pair<double> FrequencyRangeHz 
            => FrequenciesHz.GetRange();

        public SignalSpectrumSample this[int index]
        {
            get => GetSample(index);
            set => SetSample(index, value);
        }


        protected abstract T ZeroValue { get; }

        protected abstract bool IsZeroValue(T value);

        protected abstract T Negative(T value);

        protected abstract T Add(T value1, T value2);

        protected abstract T Subtract(T value1, T value2);
        
        protected abstract T Times(T value1, T value2);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected SignalSpectrum(int sampleCount, double samplingRate)
        {
            SamplingSpecs = new SignalSamplingSpecs(sampleCount, samplingRate);
            IndexSampleDictionary = new Dictionary<int, SignalSpectrumSample>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected SignalSpectrum(SignalSamplingSpecs samplingSpecs)
        {
            SamplingSpecs = samplingSpecs;
            IndexSampleDictionary = new Dictionary<int, SignalSpectrumSample>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected SignalSpectrum(SignalSamplingSpecs samplingSpecs, Dictionary<int, SignalSpectrumSample> indexSampleDictionary)
        {
            Debug.Assert(
                indexSampleDictionary.Keys.All(
                    index => index >= 0 && index < samplingSpecs.SampleCount
                )
            );

            SamplingSpecs = samplingSpecs;
            IndexSampleDictionary = indexSampleDictionary;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsSampleIndexDc(int index)
        {
            return SamplingSpecs.IsSampleIndexDc(index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsSampleIndexAc(int index)
        {
            return SamplingSpecs.IsSampleIndexAc(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetFrequency(int index)
        {
            return SamplingSpecs.GetFrequency(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetFrequencyHz(int index)
        {
            return SamplingSpecs.GetFrequencyHz(index);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SignalSpectrum<T> Clear()
        {
            IndexSampleDictionary.Clear();

            return this;
        }

        public SignalSpectrum<T> RemoveZeroValueSamples()
        {
            var indexArray = 
                IndexSampleDictionary
                    .Where(p => IsZeroValue(p.Value.Value))
                    .Select(p => p.Key)
                    .ToArray();

            foreach (var index in indexArray)
                IndexSampleDictionary.Remove(index);

            return this;
        }

        public SignalSpectrum<T> RemoveHighFrequencySamples(double cutoffFrequency)
        {
            var indexSet = new HashSet<int>();

            foreach (var (sample1, sample2) in SamplePairsAc)
            {
                var freq1 = GetFrequency(sample1.Index).Abs();
                var freq2 = GetFrequency(sample2.Index).Abs();

                if (freq1 > cutoffFrequency || freq2 > cutoffFrequency)
                {
                    indexSet.Add(sample1.Index);
                    indexSet.Add(sample2.Index);
                }
            }

            foreach (var index in indexSet)
                IndexSampleDictionary.Remove(index);

            return this;
        } 


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SignalSpectrumSample GetSample(int index)
        {
            if (index < 0 || index >= SampleCount)
                index = index.Mod(SampleCount);

            return IndexSampleDictionary.TryGetValue(index, out var record)
                ? record
                : new SignalSpectrumSample(index, ZeroValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetSample(int index, SignalSpectrumSample sample)
        {
            if (index < 0 || index >= SampleCount)
                index = index.Mod(SampleCount);

            var record = sample ?? new SignalSpectrumSample(index, ZeroValue);

            if (IndexSampleDictionary.ContainsKey(index))
                IndexSampleDictionary[index] = record;
            else
                IndexSampleDictionary.Add(index, record);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SignalSpectrum<T> Set(int index, T value)
        {
            if (index < 0 || index >= SampleCount)
                index = index.Mod(SampleCount);

            if (IndexSampleDictionary.ContainsKey(index))
                IndexSampleDictionary[index] = new SignalSpectrumSample(index, value);
            else
                IndexSampleDictionary.Add(index, new SignalSpectrumSample(index, value));

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SignalSpectrum<T> Set(SignalSpectrumSample spectrumSample)
        {
            return Set(spectrumSample.Index, spectrumSample.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SignalSpectrum<T> Set(IEnumerable<SignalSpectrumSample> spectrumSamples)
        {
            foreach (var (index, value) in spectrumSamples)
                Set(index, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SignalSpectrum<T> Add(int index, T value)
        {
            if (index < 0 || index >= SampleCount)
                index = index.Mod(SampleCount);

            if (IndexSampleDictionary.TryGetValue(index, out var record))
                IndexSampleDictionary[index] = new SignalSpectrumSample(index, Add(record.Value, value));
            else
                IndexSampleDictionary.Add(index, new SignalSpectrumSample(index, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SignalSpectrum<T> Add(SignalSpectrumSample spectrumSample)
        {
            return Add(spectrumSample.Index, spectrumSample.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SignalSpectrum<T> Add(IEnumerable<SignalSpectrumSample> spectrumSamples)
        {
            foreach (var (index, value) in spectrumSamples)
                Add(index, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SignalSpectrum<T> Subtract(int index, T value)
        {
            if (index < 0 || index >= SampleCount)
                index = index.Mod(SampleCount);

            if (IndexSampleDictionary.TryGetValue(index, out var record))
                IndexSampleDictionary[index] = new SignalSpectrumSample(index, Subtract(record.Value, value));
            else
                IndexSampleDictionary.Add(index, new SignalSpectrumSample(index, Negative(value)));

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SignalSpectrum<T> Subtract(SignalSpectrumSample spectrumSample)
        {
            return Subtract(spectrumSample.Index, spectrumSample.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SignalSpectrum<T> Subtract(IEnumerable<SignalSpectrumSample> spectrumSamples)
        {
            foreach (var (index, value) in spectrumSamples)
                Subtract(index, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SignalSpectrum<T> MapValues(Func<T, T> valueMapping)
        {
            var indexSampleDictionary = IndexSampleDictionary.ToDictionary(
                p => p.Key,
                p => new SignalSpectrumSample(
                    p.Value.Index, 
                    valueMapping(p.Value.Value)
                )
            );

            return CreateSignalSpectrum(indexSampleDictionary).RemoveZeroValueSamples();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SignalSpectrum<T> MapValuesByIndexValue(Func<int, T, T> indexValueMapping)
        {
            var indexSampleDictionary = IndexSampleDictionary.ToDictionary(
                p => p.Key,
                p => new SignalSpectrumSample(
                    p.Value.Index, 
                    indexValueMapping(p.Value.Index, p.Value.Value)
                )
            );

            return CreateSignalSpectrum(indexSampleDictionary).RemoveZeroValueSamples();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SignalSpectrum<T> MapValuesByFrequencyValue(Func<double, T, T> frequencyValueMapping)
        {
            var indexSampleDictionary = IndexSampleDictionary.ToDictionary(
                p => p.Key,
                p => new SignalSpectrumSample(
                    p.Value.Index, 
                    frequencyValueMapping(p.Value.Index * FrequencyResolution, p.Value.Value)
                )
            );

            return CreateSignalSpectrum(indexSampleDictionary).RemoveZeroValueSamples();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SignalSpectrum<T> GetCopy()
        {
            var indexSampleDictionary = IndexSampleDictionary.ToDictionary(
                p => p.Key,
                p => p.Value
            );

            return CreateSignalSpectrum(indexSampleDictionary).RemoveZeroValueSamples();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SignalSpectrum<T> ScaleBy(T scalingFactor)
        {
            return MapValues(value => Times(value, scalingFactor));
        }

        protected SignalSpectrum<T> CreateSignalSpectrum(Dictionary<int, SignalSpectrumSample> indexSampleDictionary)
        {
            return CreateSignalSpectrum(SamplingSpecs, indexSampleDictionary);
        }

        protected abstract SignalSpectrum<T> CreateSignalSpectrum(SignalSamplingSpecs samplingSpecs, Dictionary<int, SignalSpectrumSample> indexSampleDictionary);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<SignalSpectrumSample> GetEnumerator()
        {
            return Enumerable.Range(0, Count).Select(i => this[i]).GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return IndexSampleDictionary
                .Values
                .OrderBy(spectrumSample => 
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