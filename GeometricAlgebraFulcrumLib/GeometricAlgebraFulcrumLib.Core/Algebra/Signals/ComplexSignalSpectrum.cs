using System.Collections.Immutable;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64Complex;
using GeometricAlgebraFulcrumLib.Core.Algebra.Signals.Composers;
using MathNet.Numerics;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.Signals;

public sealed class ComplexSignalSpectrum : 
    ScalarSignalSpectrum<Complex>
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


    public ComplexSignalSpectrum(int sampleCount, double samplingRate) 
        : base(sampleCount, samplingRate)
    {
    }

    public ComplexSignalSpectrum(Float64SignalSamplingSpecs samplingSpecs) 
        : base(samplingSpecs)
    {
    }

    internal ComplexSignalSpectrum(Float64SignalSamplingSpecs samplingSpecs, Dictionary<int, SignalSpectrumSample> indexSampleDictionary) 
        : base(samplingSpecs, indexSampleDictionary)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override bool IsZeroValue(Complex value)
    {
        return value.Real.IsZero() && value.Imaginary.IsZero();
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

        
    public ComplexSignalSpectrum RemoveZeroValueSamples(double zeroEpsilon)
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
    protected override ScalarSignalSpectrum<Complex> CreateSignalSpectrum(Float64SignalSamplingSpecs samplingSpecs, Dictionary<int, SignalSpectrumSample> indexSampleDictionary)
    {
        return new ComplexSignalSpectrum(SamplingSpecs, indexSampleDictionary);
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
    public Float64Signal GetRealSignal(SignalSpectrumSample spectrumSample, IEnumerable<double> tValues)
    {
        return tValues
            .Select(t => GetValue(spectrumSample, t).RotateToReal())
            .CreateSignal(SamplingRate);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Signal GetRealSignalDt1(SignalSpectrumSample spectrumSample, IEnumerable<double> tValues)
    {
        return tValues
            .Select(t => GetValueDt1(spectrumSample, t).RotateToReal())
            .CreateSignal(SamplingRate);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Signal GetRealSignalDt2(SignalSpectrumSample spectrumSample, IEnumerable<double> tValues)
    {
        return tValues
            .Select(t => GetValueDt2(spectrumSample, t).RotateToReal())
            .CreateSignal(SamplingRate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Signal GetRealSignalDt(SignalSpectrumSample spectrumSample, int degree, IEnumerable<double> tValues)
    {
        return tValues
            .Select(t => GetValueDt(spectrumSample, degree, t).RotateToReal())
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
    public Complex GetValueDt(int order, double t)
    {
        return order switch
        {
            < 0 => throw new ArgumentOutOfRangeException(nameof(order)),
            0 => GetValue(t),
            1 => GetValueDt1(t),
            2 => GetValueDt2(t),
            _ => Samples
                .Select(sample => GetValueDt(sample, order, t))
                .Aggregate(Complex.Zero, (a, b) => a + b)
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Complex> GetValues(IEnumerable<double> tValues)
    {
        return tValues.Select(GetValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Signal GetRealSignal(Float64Signal tValues)
    {
        var complexSignal =
            tValues.Select(GetValue).ToImmutableArray();

        //Debug.Assert(
        //    complexSignal.All(c => c.Imaginary.IsNearZero(1e-7))
        //);

        return complexSignal
            .Select(c => c.RotateToReal())
            .CreateSignal(tValues.SamplingRate);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Signal GetRealSignalDt1(Float64Signal tValues)
    {
        return tValues
            .Select(t => GetValueDt1(t).RotateToReal())
            .CreateSignal(SamplingRate);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Signal GetRealSignalDt2(Float64Signal tValues)
    {
        return tValues
            .Select(t => GetValueDt2(t).RotateToReal())
            .CreateSignal(SamplingRate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Signal GetRealSignalDt(int degree, Float64Signal tValues)
    {
        return tValues
            .Select(t => GetValueDt(degree, t).RotateToReal())
            .CreateSignal(SamplingRate);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComplexSignalSpectrum GetSpectrumDt1()
    {
        return (ComplexSignalSpectrum) MapValuesByFrequencyValue(
            (frequency, value) => Complex.ImaginaryOne * frequency * value
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComplexSignalSpectrum GetSpectrumDt2()
    {
        return (ComplexSignalSpectrum) MapValuesByFrequencyValue(
            (frequency, value) => -frequency * frequency * value
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComplexSignalSpectrum GetSpectrumDt(int degree)
    {
        return degree switch
        {
            < 0 => throw new ArgumentOutOfRangeException(nameof(degree)),
            0 => (ComplexSignalSpectrum) GetCopy(),
            1 => GetSpectrumDt1(),
            2 => GetSpectrumDt2(),
            _ => (ComplexSignalSpectrum) MapValuesByFrequencyValue(
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


    public Float64SignalSpectrum GetEnergySpectrum()
    {
        var scalingFactor = SampleCount / (2 * Math.PI);
        var energySpectrum = new Float64SignalSpectrum(SamplingSpecs);

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
            sample1.Index == sample2.Index ? 0d : GetEnergyAc(sample2.Index);

        return energy1 + energy2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetEnergy(int index, Float64Signal tValues)
    {
        var spectrumSample = this[index];

        return tValues.Sum(t => 
            GetValue(spectrumSample, t).MagnitudeSquared() / (2 * Math.PI)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetEnergyDc(int index, Float64Signal tValues)
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
    public double GetEnergyAc(int index, Float64Signal tValues)
    {
        if (!IsSampleIndexAc(index))
            return 0;

        return GetEnergy(index, tValues) - GetEnergyDc(index, tValues);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetEnergy(Float64Signal tValues)
    {
        return tValues.Sum(t => 
            GetValue(t).MagnitudeSquared() / (2 * Math.PI)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetEnergyDc(Float64Signal tValues)
    {
        return tValues
            .Select(GetValue)
            .Sum()
            .MagnitudeSquared() / (2 * Math.PI * Count);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetEnergyAc(Float64Signal tValues)
    {
        return GetEnergy(tValues) - GetEnergyDc(tValues);
    }


    public string GetTextDescription(string signalName, double totalEnergyAc, Float64Signal tValues)
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

    public string GetTextDescription(Float64Signal vectorSignal1)
    {
        var composer = new LinearTextComposer();

        var tValues = SamplingSpecs.GetSampledTimeSignal();

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

        composer
            .AppendLine("Error Signal:")
            .IncreaseIndentation()
            .AppendLine($"Energy ratio: {vectorSignal2.Energy() / vectorSignal1.Energy():P3}")
            .AppendLine($"Energy ratio DC: {vectorSignal2.EnergyDc() / vectorSignal1.EnergyDc():P3}")
            .AppendLine($"Energy ratio AC: {vectorSignal2.EnergyAc() / vectorSignal1.EnergyAc():P3}")
            .AppendLine($"Signal to noise ratio: {vectorSignal1.PeakSignalToNoiseRatioDb(vectorSignal2)}")
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