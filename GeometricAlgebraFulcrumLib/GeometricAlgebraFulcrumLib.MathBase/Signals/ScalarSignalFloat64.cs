using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.Differential.Functions.Interpolators;
using MathNet.Numerics.IntegralTransforms;
using TextComposerLib.Text;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.MathBase.Signals
{
    public sealed record FrequencyDataRecord<T>(int Index, T Frequency, T EnergyRatio);

    public sealed class ScalarSignalFloat64 :
        IReadOnlyList<double>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 Create(double samplingRate)
        {
            return new ScalarSignalFloat64(samplingRate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 Create(double samplingRate, int sampleCount)
        {
            return new ScalarSignalFloat64(samplingRate, sampleCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 Create(double samplingRate, IEnumerable<double> sampleList, bool isReadOnly)
        {
            return new ScalarSignalFloat64(samplingRate, sampleList, isReadOnly);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 CreateConstant(double samplingRate, int sampleCount, double value, bool isReadOnly)
        {
            return new ScalarSignalFloat64(
                samplingRate, 
                Enumerable.Repeat(value, sampleCount), 
                isReadOnly
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 CreateNonPeriodic(int sampleCount, double tMin, double tMax, Func<double, double> scalarFunc, bool isReadOnly)
        {
            var sampleList = 
                tMin.GetLinearRange(tMax, sampleCount, false).Select(scalarFunc);

            var samplingRate = sampleCount / (tMax - tMin);

            return new ScalarSignalFloat64(samplingRate, sampleList, isReadOnly);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 CreatePeriodic(int sampleCount, double periodTime, Func<double, double> scalarFunc, bool isReadOnly)
        {
            var sampleList = 
                0d.GetLinearRange(periodTime, sampleCount, true).Select(scalarFunc);

            return new ScalarSignalFloat64(sampleCount / periodTime, sampleList, isReadOnly);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 CreatePeriodic(double samplingRate, int sampleCount, double periodTime, Func<double, double> scalarFunc, bool isReadOnly)
        {
            var sampleList = 
                0d.GetLinearRange(periodTime, sampleCount, true).Select(scalarFunc);

            return new ScalarSignalFloat64(samplingRate, sampleList, isReadOnly);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 CreateConcat(double samplingRate, IEnumerable<double> samples1, IEnumerable<double> samples2)
        {
            return Create(samplingRate, samples1.Concat(samples2), false);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 CreateRandomUniform(double samplingRate, int sampleCount, System.Random randomGenerator)
        {
            var sampleList =
                sampleCount.GetRange().Select(_ => randomGenerator.NextDouble());

            return new ScalarSignalFloat64(
                samplingRate,
                sampleList,
                false
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 CreateRandomUniform(double samplingRate, int sampleCount, System.Random randomGenerator, double minValue, double maxValue)
        {
            var sampleList =
                sampleCount.GetRange().Select(_ => randomGenerator.NextDouble(minValue, maxValue));

            return new ScalarSignalFloat64(
                samplingRate,
                sampleList,
                false
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 CreateRandomGaussian(double samplingRate, int sampleCount, System.Random randomGenerator)
        {
            var sampleList =
                sampleCount.GetRange().Select(_ => randomGenerator.NextGaussian());

            return new ScalarSignalFloat64(
                samplingRate,
                sampleList,
                false
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 CreateRandomGaussian(double samplingRate, int sampleCount, System.Random randomGenerator, double mu, double sigma)
        {
            var sampleList =
                sampleCount.GetRange().Select(_ => sigma * randomGenerator.NextGaussian() + mu);

            return new ScalarSignalFloat64(
                samplingRate,
                sampleList,
                false
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 operator +(ScalarSignalFloat64 signal)
        {
            return signal.MapSamples(s => s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 operator -(ScalarSignalFloat64 signal)
        {
            return signal.MapSamples(s => -s);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 operator +(ScalarSignalFloat64 signal1, double signal2)
        {
            return signal1.MapSamples(s => s + signal2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 operator +(double signal1, ScalarSignalFloat64 signal2)
        {
            return signal2.MapSamples(s => signal1 + s);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 operator -(ScalarSignalFloat64 signal1, double signal2)
        {
            return signal1.MapSamples(s => s - signal2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 operator -(double signal1, ScalarSignalFloat64 signal2)
        {
            return signal2.MapSamples(s => signal1 - s);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 operator *(ScalarSignalFloat64 signal1, double signal2)
        {
            return signal1.MapSamples(s => s * signal2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 operator *(double signal1, ScalarSignalFloat64 signal2)
        {
            return signal2.MapSamples(s => signal1 * s);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 operator /(ScalarSignalFloat64 signal1, double signal2)
        {
            return signal1.MapSamples(s => s / signal2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 operator /(double signal1, ScalarSignalFloat64 signal2)
        {
            return signal2.MapSamples(s => signal1 / s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 operator +(ScalarSignalFloat64 signal1, ScalarSignalFloat64 signal2)
        {
            if (signal1.SamplingRate != signal2.SamplingRate)
                throw new InvalidOperationException();

            return signal1.MapSamples(signal2, (s1, s2) => s1 + s2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 operator -(ScalarSignalFloat64 signal1, ScalarSignalFloat64 signal2)
        {
            if (signal1.SamplingRate != signal2.SamplingRate)
                throw new InvalidOperationException();

            return signal1.MapSamples(signal2, (s1, s2) => s1 - s2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 operator *(ScalarSignalFloat64 signal1, ScalarSignalFloat64 signal2)
        {
            if (signal1.SamplingRate != signal2.SamplingRate)
                throw new InvalidOperationException();

            return signal1.MapSamples(signal2, (s1, s2) => s1 * s2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 operator /(ScalarSignalFloat64 signal1, ScalarSignalFloat64 signal2)
        {
            if (signal1.SamplingRate != signal2.SamplingRate)
                throw new InvalidOperationException();

            return signal1.MapSamples(signal2, (s1, s2) => s1 / s2);
        }


        private readonly List<double> _sampleList;


        public double SamplingRate { get; }

        public bool IndexCheck { get; set; } = false;

        public int Count 
            => _sampleList.Count;

        public SignalSamplingSpecs SamplingSpecs 
            => new SignalSamplingSpecs(Count, SamplingRate);

        public bool IsReadOnly { get; }

        public double this[int index]
        {
            get => 
                index >= 0 && index < _sampleList.Count 
                    ? _sampleList[index]
                    : IndexCheck ? throw new IndexOutOfRangeException() : 0;
            set
            {
                if (IsReadOnly)
                    throw new ReadOnlyException();
                
                _sampleList[index] = value.NaNToZero();
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ScalarSignalFloat64(double samplingRate)
        {
            if (double.IsNaN(samplingRate) || double.IsInfinity(samplingRate) || samplingRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(samplingRate));

            _sampleList = new List<double>();
            SamplingRate = samplingRate;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ScalarSignalFloat64(double samplingRate, int sampleCount)
        {
            if (double.IsNaN(samplingRate) || double.IsInfinity(samplingRate) || samplingRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(samplingRate));

            _sampleList = new List<double>(Enumerable.Repeat(0d, sampleCount));
            SamplingRate = samplingRate;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ScalarSignalFloat64(double samplingRate, IEnumerable<double> sampleList, bool isReadOnly)
        {
            if (double.IsNaN(samplingRate) || double.IsInfinity(samplingRate) || samplingRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(samplingRate));

            _sampleList = new List<double>(sampleList.Select(s => s.NaNToZero()));
            IsReadOnly = isReadOnly;
            SamplingRate = samplingRate;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero()
        {
            return _sampleList.All(s => s.IsZero());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(double epsilon = 1e-7d)
        {
            return _sampleList.All(s => s.IsNearZero(epsilon));
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<int> GetSampleIndexFromTime(double t)
        {
            //TODO: Is this correct?
            if (t < 0)
                return new Pair<int>(0, 0);

            if (t > SamplingSpecs.MaxTime)
                return new Pair<int>(Count - 1, Count - 1);

            //if (t < 0 || t > SamplingSpecs.MaxTime)
            //    throw new ArgumentOutOfRangeException(nameof(t));

            var index = t / SamplingSpecs.TimeResolution;

            return new Pair<int>(
                (int) Math.Floor(index), 
                (int) Math.Ceiling(index)
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetSubSignal(int index, int count)
        {
            var sampleList =
                Enumerable.Range(index, count).Select(i => _sampleList[i]);

            return new ScalarSignalFloat64(
                SamplingRate, 
                sampleList, 
                IsReadOnly
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetSubSignal(int index, int count, bool isReadOnly)
        {
            var sampleList =
                Enumerable.Range(index, count).Select(i => _sampleList[i]);

            return new ScalarSignalFloat64(
                SamplingRate, 
                sampleList, 
                isReadOnly
            );
        }

        public ScalarSignalFloat64 ReSample(int sampleCount)
        {
            var tMin = SamplingSpecs.MinTime;
            var tMax = SamplingSpecs.MaxTime;

            var samplingRate = 
                SamplingRate * ((sampleCount - 1d) / (Count - 1d));

            var tValues = 
                tMin.GetLinearRange(tMax, sampleCount, false).ToArray();

            return tValues
                .Select(LinearInterpolation)
                .CreateSignal(samplingRate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 DownSampleByFactor(int factor)
        {
            return DownSampleByFactor(0, factor);
        }

        public ScalarSignalFloat64 DownSampleByFactor(int index, int factor)
        {
            if (factor < 2)
                throw new ArgumentOutOfRangeException(nameof(factor), "Sampling factor must be 2 or more");

            var sampleList = new List<double>();
            for (var i = index; i < _sampleList.Count; i += factor)
                sampleList.Add(_sampleList[i]);

            return new ScalarSignalFloat64(
                SamplingRate / factor, 
                sampleList, 
                IsReadOnly
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 AppendSample(double sampleValue)
        {
            _sampleList.Add(sampleValue.NaNToZero());

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 AppendSamples(IEnumerable<double> sampleValues)
        {
            _sampleList.AddRange(sampleValues.Select(s => s.NaNToZero()));

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 AppendSamples(params double[] sampleValues)
        {
            _sampleList.AddRange(sampleValues.Select(s => s.NaNToZero()));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 PrependSample(double sampleValue)
        {
            _sampleList.Insert(0, sampleValue.NaNToZero());

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 PrependSamples(IEnumerable<double> sampleValues)
        {
            _sampleList.InsertRange(0, sampleValues.Select(s => s.NaNToZero()));

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 PrependSamples(params double[] sampleValues)
        {
            _sampleList.InsertRange(0, sampleValues.Select(s => s.NaNToZero()));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 InsertSample(int index, double sampleValue)
        {
            _sampleList.Insert(index, sampleValue.NaNToZero());

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 InsertSamples(int index, IEnumerable<double> sampleValues)
        {
            _sampleList.InsertRange(index, sampleValues.Select(s => s.NaNToZero()));

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 InsertSamples(int index, params double[] sampleValues)
        {
            _sampleList.InsertRange(index, sampleValues.Select(s => s.NaNToZero()));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex[] GetFourierArray(FourierOptions options = FourierOptions.Default)
        {
            var complexSamplesArray = 
                _sampleList.Select(v => (Complex) v).ToArray();

            Fourier.Forward(complexSamplesArray, options);

            return complexSamplesArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 MapSamples(Func<double, double> sampleMapping)
        {
            return _sampleList
                .Select(sampleMapping)
                .CreateSignal(SamplingRate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 MapSamples(Func<int, double, double> sampleMapping)
        {
            return _sampleList
                .Select((s, i) => sampleMapping(i, s))
                .CreateSignal(SamplingRate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 MapSamples(IReadOnlyList<double> signal2, Func<double, double, double> sampleMapping)
        {
            var sampleCount = Math.Max(Count, signal2.Count);
            var sampleList = new double[sampleCount];

            for (var i = 0; i < sampleCount; i++)
            {
                var s1 = i < Count ? _sampleList[i] : 0d;
                var s2 = i < signal2.Count ? signal2[i] : 0d;

                sampleList[i] = sampleMapping(s1, s2);
            }
            
            return sampleList.CreateSignal(SamplingRate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 MapSamples(IReadOnlyList<double> signal2, Func<int, double, double, double> sampleMapping)
        {
            var sampleCount = Math.Max(Count, signal2.Count);
            var sampleList = new double[sampleCount];

            for (var i = 0; i < sampleCount; i++)
            {
                var s1 = i < Count ? _sampleList[i] : 0d;
                var s2 = i < signal2.Count ? signal2[i] : 0d;

                sampleList[i] = sampleMapping(i, s1, s2);
            }
            
            return sampleList.CreateSignal(SamplingRate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 AddRandomUniform(System.Random randomGenerator)
        {
            return MapSamples(
                s => s + randomGenerator.NextDouble()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 AddRandomUniform(System.Random randomGenerator, double minValue, double maxValue)
        {
            return MapSamples(
                s => s + randomGenerator.NextDouble(minValue, maxValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 AddRandomGaussian(System.Random randomGenerator)
        {
            return MapSamples(
                s => s + randomGenerator.NextGaussian()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 AddRandomGaussian(System.Random randomGenerator, double mu, double sigma)
        {
            return MapSamples(
                s => s + sigma * randomGenerator.NextGaussian() + mu
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Sqrt()
        {
            return MapSamples(Math.Sqrt);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Cbrt()
        {
            return MapSamples(Math.Cbrt);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Square()
        {
            return MapSamples(s => Math.Pow(s, 2));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Cube()
        {
            return MapSamples(s => Math.Pow(s, 3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Power(double powerScalar)
        {
            return MapSamples(s => Math.Pow(s, powerScalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Power(IReadOnlyList<double> powerScalarSignal)
        {
            return MapSamples(powerScalarSignal, Math.Pow);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Exp()
        {
            return MapSamples(Math.Exp);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Log()
        {
            return MapSamples(s => Math.Log(s));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Log2()
        {
            return MapSamples(Math.Log2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Log10()
        {
            return MapSamples(Math.Log10);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Log(double baseScalar)
        {
            return MapSamples(s => Math.Log(s, baseScalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Log(IReadOnlyList<double> baseScalarSignal)
        {
            return MapSamples(baseScalarSignal, Math.Log);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Abs()
        {
            return MapSamples(Math.Abs);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Floor()
        {
            return MapSamples(Math.Floor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Ceiling()
        {
            return MapSamples(Math.Ceiling);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Truncate()
        {
            return MapSamples(Math.Truncate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Sin()
        {
            return MapSamples(Math.Sin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Cos()
        {
            return MapSamples(Math.Cos);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Tan()
        {
            return MapSamples(Math.Tan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Asin()
        {
            return MapSamples(Math.Asin);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Acos()
        {
            return MapSamples(Math.Acos);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Atan()
        {
            return MapSamples(Math.Atan);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Sinh()
        {
            return MapSamples(Math.Sinh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Cosh()
        {
            return MapSamples(Math.Cosh);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Tanh()
        {
            return MapSamples(Math.Tanh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Asinh()
        {
            return MapSamples(Math.Asinh);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Acosh()
        {
            return MapSamples(Math.Acosh);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Atanh()
        {
            return MapSamples(Math.Atanh);
        }

        public Pair<double> GetMinMaxValues()
        {
            var minValue = double.PositiveInfinity;
            var maxValue = double.NegativeInfinity;

            foreach (var value in _sampleList)
            {
                if (minValue > value) minValue = value;
                if (maxValue < value) maxValue = value;
            }

            return new Pair<double>(minValue, maxValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 FlipX()
        {
            return Create(
                SamplingRate,
                ((IEnumerable<double>)_sampleList).Reverse(), 
                IsReadOnly
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 FlipY()
        {
            var (minValue, maxValue) = GetMinMaxValues();
            var minMaxPlus = minValue + maxValue;

            return MapSamples(s => minMaxPlus - s);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 FlipXy()
        {
            var (minValue, maxValue) = GetMinMaxValues();
            var minMaxPlus = minValue + maxValue;

            return Create(
                SamplingRate,
                _sampleList.Select(s => minMaxPlus - s).Reverse(), 
                IsReadOnly
            );
        }

        
        public double EnergyFft()
        {
            // Compute FFT
            var real = _sampleList.ToArray();
            var imaginary = Enumerable.Repeat(0d, real.Length).ToArray();
            var sampleCount = real.Length;

            Fourier.Forward(real, imaginary, FourierOptions.Default);

            // Compute AC energy
            var energy = 0d;
            for (var freqIndex = 0; freqIndex < sampleCount; freqIndex++)
                energy += (real[freqIndex].Square() + imaginary[freqIndex].Square()) / (2 * Math.PI);

            return energy;
        }

        public double EnergyDcFft()
        {
            // Compute FFT
            var real = _sampleList.ToArray();
            var imaginary = Enumerable.Repeat(0d, real.Length).ToArray();

            Fourier.Forward(real, imaginary, FourierOptions.Default);

            // Compute DC energy
            return (real[0].Square() + imaginary[0].Square()) / (2 * Math.PI);
        }
        
        public double EnergyAcFft()
        {
            // Compute FFT
            var real = _sampleList.ToArray();
            var imaginary = Enumerable.Repeat(0d, real.Length).ToArray();
            var sampleCount = real.Length;

            Fourier.Forward(real, imaginary, FourierOptions.Default);

            // Compute AC energy
            var energy = 0d;
            for (var freqIndex = 1; freqIndex < sampleCount; freqIndex++)
                energy += (real[freqIndex].Square() + imaginary[freqIndex].Square()) / (2 * Math.PI);

            return energy;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Energy()
        {
            var energy1 = 
                _sampleList.Sum(s => s * s) / (2 * Math.PI);

            var energy2 = EnergyFft();
            Debug.Assert(
                (energy1 - energy2).IsNearZero() ||
                ((energy1 - energy2) / energy1).IsNearZero()
            );

            return energy1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double EnergyDc()
        {
            // The DC energy is the mean square value of the signal
            var energy1 = 
                _sampleList.Sum().Square() / (2 * Math.PI * Count);
                //Mean().Square() * Count / (2 * Math.PI);
            
            var energy2 = EnergyDcFft();
            Debug.Assert(
                (energy1 - energy2).IsNearZero(1e-7) ||
                ((energy1 - energy2) / energy1).IsNearZero(1e-7)
            );

            return energy1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double EnergyAc()
        {
            var energy1 = Energy() - EnergyDc();
            
            var energy2 = EnergyAcFft();
            
            //Debug.Assert(
            //    (energy1 - energy2).IsNearZero() ||
            //    ((energy1 - energy2) / energy1).IsNearZero()
            //);

            return energy1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Mean()
        {
            return _sampleList.Sum() / _sampleList.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MeanSquare()
        {
            return _sampleList.Sum(d => d * d) / _sampleList.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double RootMeanSquare()
        {
            return Math.Sqrt(_sampleList.Sum(s => s * s) / _sampleList.Count);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Sum()
        {
            return _sampleList.Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double SumOfSquares()
        {
            return _sampleList.Sum(s => s * s);
        }
        
        /// <summary>
        /// Compute peak signal to noise ratio (PSNR) in Db. This signal is assumed to
        /// be the original signal and the given input is its reconstructed version
        /// https://en.wikipedia.org/wiki/Peak_signal-to-noise_ratio
        /// </summary>
        /// <param name="reconstructedSignal"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double PeakSignalToNoiseRatioDb(ScalarSignalFloat64 reconstructedSignal)
        {
            var (min, max) = GetMinMaxValues();
            var maxMinDiff = max - min;
            var meanSquareError = (this - reconstructedSignal).MeanSquare();

            return 20 * maxMinDiff.Log10() - 10 * meanSquareError.Log10();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetSampledTimeSignal()
        {
            return SamplingSpecs.GetSampledTimeSignal();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetSampledTimeSignal(int sampleCount)
        {
            return SamplingSpecs.GetSampledTimeSignal(sampleCount);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetSampledTimeSignal(int firstSampleIndex, int sampleCount)
        {
            return SamplingSpecs.GetSampledTimeSignal(firstSampleIndex, sampleCount);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] GetSampledTimeArray()
        {
            return SamplingSpecs.GetSampledTimeArray();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] GetSampledTimeArray(int sampleCount)
        {
            return SamplingSpecs.GetSampledTimeArray(sampleCount);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] GetSampledTimeArray(int firstSampleIndex, int sampleCount)
        {
            return SamplingSpecs.GetSampledTimeArray(firstSampleIndex, sampleCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] GetArray()
        {
            return _sampleList.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] GetArray(int sampleCount)
        {
            return _sampleList.Take(sampleCount).ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] GetArray(int firstSampleIndex, int sampleCount)
        {
            return _sampleList.Skip(firstSampleIndex).Take(sampleCount).ToArray();
        }


        public ScalarSignalFloat64 GetLinearPaddedSignal()
        {
            var sampleCount = Count;

            var paddedSignalSamples = new List<double>(_sampleList);

            // The padded signal always has an odd number of samples
            var u1 = _sampleList[^1];
            var u2 = _sampleList[0];
            for (var i = 0; i < sampleCount - 1; i++)
            {
                var t = (i + 1) / (double) sampleCount;
                var u = (1 - t) * u1 + t * u2;

                paddedSignalSamples.Add(u);
            }

            return paddedSignalSamples.CreateSignal(SamplingRate);
        }
        
        /// <summary>
        /// Apply FFT to given real sampled signal and find the frequency indices of the
        /// dominant frequency using a ratio of the total signal energy
        /// </summary>
        /// <param name="energyThreshold"></param>
        /// <param name="freqCountThreshold"></param>
        /// <returns></returns>
        public IEnumerable<int> GetDominantFrequencyIndexSet(double energyThreshold = 0.998d, int freqCountThreshold = int.MaxValue)
        {
            // Compute FFT
            var real = _sampleList.ToArray();
            var imaginary = Enumerable.Repeat(0d, real.Length).ToArray();
            var sampleCount = real.Length;

            Fourier.Forward(real, imaginary, FourierOptions.Default);

            //Compute frequency sample energy, and total energy (not including 0 and negative frequencies)
            var energyDictionary = new SortedDictionary<double, int>();
            var energySum = 0d;

            // Ignore negative frequencies from the spectrum,
            // they will be added later using the real symmetry of the signal
            var freqIndexMax = sampleCount.IsOdd()
                ? (sampleCount - 1) / 2
                : (sampleCount - 2) / 2;

            for (var freqIndex = 1; freqIndex <= freqIndexMax; freqIndex++)
            {
                var energy =
                    (real[freqIndex].Square() + imaginary[freqIndex].Square()) / (2 * Math.PI);

                energyDictionary.Add(energy, freqIndex);

                energySum += energy;
            }

            // Find frequencies with most energy, but always include 0 frequency
            var threshold = energyThreshold * energySum;
            var indexSet = new HashSet<int> { 0 };

            freqCountThreshold--;
            foreach (var (energy, freqIndex) in energyDictionary.Reverse())
            {
                indexSet.Add(freqIndex);

                freqCountThreshold--;
                threshold -= energy;

                if (threshold < 0d || freqCountThreshold <= 0)// || indexSet.Count < 2)
                    break;
            }

            return indexSet;
        }

        /// <summary>
        /// Apply FFT to given real sampled signal and find the frequency indices of the
        /// dominant frequency using a ratio of the total signal energy
        /// </summary>
        /// <param name="energyThreshold"></param>
        /// <returns></returns>
        public IEnumerable<FrequencyDataRecord<double>> GetDominantFrequencyDataRecords(double energyThreshold = 0.998d)
        {
            // Compute FFT
            var real = _sampleList.ToArray();
            var imaginary = Enumerable.Repeat(0d, real.Length).ToArray();
            var sampleCount = real.Length;

            Fourier.Forward(real, imaginary, FourierOptions.Default);

            //Compute frequency sample energy, and total energy (not including 0 and negative frequencies)
            var energyDictionary = new SortedDictionary<double, int>();
            var energySum = 0d;

            // Ignore negative frequencies from the spectrum,
            // they will be added later using the real symmetry of the signal
            var freqIndexMax = sampleCount.IsOdd()
                ? (sampleCount - 1) / 2
                : (sampleCount - 2) / 2;

            for (var freqIndex = 1; freqIndex <= freqIndexMax; freqIndex++)
            {
                var energy =
                    (real[freqIndex].Square() + imaginary[freqIndex].Square()) / (2 * Math.PI);

                energyDictionary.Add(energy, freqIndex);

                energySum += energy;
            }

            // Find frequencies with most energy, but always include 0 frequency
            var threshold = energyThreshold * energySum;
            var indexSet = new List<FrequencyDataRecord<double>>()
            {
                new FrequencyDataRecord<double>(0, 0, 0)
            };

            var df = SamplingRate / (Count - 1);
            foreach (var (energy, freqIndex) in energyDictionary.Reverse())
            {
                indexSet.Add(new FrequencyDataRecord<double>(
                    freqIndex,
                    df * freqIndex,
                    energy / energySum
                ));

                threshold -= energy;

                if (threshold <= 0d)// || indexSet.Count < 2)
                    break;
            }

            return indexSet;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarFourierSeries CreateFourierSeries(double energyThreshold)
        {
            var frequencyIndexSet = 
                GetDominantFrequencyIndexSet(energyThreshold);

            return ScalarFourierSeries.Create(_sampleList, SamplingRate, frequencyIndexSet);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarFourierSeries CreateFourierSeries()
        {
            var indexCount = Count.IsOdd() 
                ? (Count + 1) / 2 : Count / 2 + 1;

            var frequencyIndexSet = 
                Enumerable.Range(0, indexCount);

            return ScalarFourierSeries.Create(_sampleList, SamplingRate, frequencyIndexSet);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarFourierSeries CreateFourierInterpolator(double snrThreshold, double energyThreshold)
        {
            var frequencyIndexSet = 
                GetDominantFrequencyIndexSet(energyThreshold);

            return ScalarFourierSeries.Create(
                this, 
                snrThreshold, 
                frequencyIndexSet
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarFourierSeries CreateFourierInterpolator(IEnumerable<int> frequencyIndexSet)
        {
            return ScalarFourierSeries.Create(_sampleList, SamplingRate, frequencyIndexSet);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarFourierSeries CreateFourierInterpolator(double snrThreshold, IEnumerable<int> frequencyIndexSet)
        {
            return ScalarFourierSeries.Create(
                this, 
                snrThreshold, 
                frequencyIndexSet
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 FourierInterpolate(double energyThreshold = 0.998)
        {
            var frequencyIndexSet = 
                GetDominantFrequencyIndexSet(energyThreshold);

            return FourierInterpolate(frequencyIndexSet);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 FourierInterpolate(IEnumerable<int> frequencyIndexSet)
        {
            var interpolator = CreateFourierInterpolator(frequencyIndexSet);

            return 0d
                .GetLinearRange((Count - 1) / SamplingRate, Count)
                .Select(interpolator.GetValue)
                .CreateSignal(SamplingRate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfLinearSplineSignalInterpolator GetLinearInterpolator(DfLinearSplineSignalInterpolatorOptions options)
        {
            return DfLinearSplineSignalInterpolator.Create(this, options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double LinearInterpolation(double t)
        {
            var sampleIndex = SamplingRate * t;

            if (sampleIndex < 0 || sampleIndex > _sampleList.Count - 1)
                return 0;

            var i1 = (int) Math.Floor(sampleIndex);
            var i2 = (int) Math.Ceiling(sampleIndex);

            t = sampleIndex - Math.Truncate(sampleIndex);

            return (1 - t) * _sampleList[i1] + t * _sampleList[i2];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 FilterSpikes(double histogramTrimPercentage = 1e-2d)
        {
            if (histogramTrimPercentage == 0d)
                return this;

            return ScalarLog2HistogramFloat64
                .Create(this)
                .Trim(histogramTrimPercentage)
                .FilterSignal(this);
        }

        public ScalarSignalFloat64 Repeat(int count)
        {
            var sampleList = new List<double>(Count * count);
            
            for (var i = 0; i < count; i++)
                sampleList.AddRange(_sampleList);

            return new ScalarSignalFloat64(SamplingRate, sampleList, IsReadOnly);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetRunningAverageSignal(int sampleCount)
        {
            return GetRunningAverageSignal(sampleCount, sampleCount);
        }

        public ScalarSignalFloat64 GetRunningAverageSignal(int pastSampleCount, int futureSampleCount)
        {
            if (pastSampleCount < 0)
                throw new ArgumentOutOfRangeException(nameof(pastSampleCount));

            if (futureSampleCount < 0)
                throw new ArgumentOutOfRangeException(nameof(futureSampleCount));

            var signal = new ScalarSignalFloat64(SamplingRate, Count);

            for (var index = 0; index < Count; index++)
            {
                var index1 = Math.Max(index - pastSampleCount, 0);
                var index2 = Math.Min(index + futureSampleCount, Count - 1);
                var count = index2 - index1 + 1;

                var average = 0d;
                for (var i = index1; i <= index2; i++)
                    average += this[i];

                average /= count;

                signal[index] = average;
            }

            return signal;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetRunningAverageSignal(IEnumerable<int> sampleCountArray)
        {
            return sampleCountArray.Aggregate(
                this, 
                (signal, sampleCount) => 
                    signal.GetRunningAverageSignal(sampleCount)
                );
        }

        public ScalarSignalSpectrumComplex GetFourierSpectrum()
        {
            var scalingFactor = Math.Sqrt(Count);

            // Compute FFT
            var real = _sampleList.ToArray();
            var imaginary = Enumerable.Repeat(0d, real.Length).ToArray();
            var sampleCount = real.Length;

            Fourier.Forward(real, imaginary, FourierOptions.Default);
            
            var spectrum = new ScalarSignalSpectrumComplex(sampleCount, SamplingRate);
            for (var index = 0; index < sampleCount; index++)
            {
                var value = new Complex(
                    real[index] / scalingFactor, 
                    imaginary[index] / scalingFactor
                );

                spectrum.Add(index, value);
            }

            return spectrum;
        }
        
        public ScalarSignalSpectrumFloat64 GetEnergySpectrum()
        {
            // Compute FFT
            var real = _sampleList.ToArray();
            var imaginary = Enumerable.Repeat(0d, real.Length).ToArray();
            var sampleCount = real.Length;

            Fourier.Forward(real, imaginary, FourierOptions.Default);
            
            var energySpectrum = new ScalarSignalSpectrumFloat64(sampleCount, SamplingRate);
            for (var index = 0; index < sampleCount; index++)
            {
                var energy = 
                    (real[index].Square() + imaginary[index].Square()) / (2 * Math.PI);

                energySpectrum.Add(index, energy);
            }

            return energySpectrum;
        }
        
        public ScalarSignalSpectrumComplex GetFourierSpectrum(DfFourierSignalInterpolatorOptions spectrumThresholdSpecs)
        {
            if (spectrumThresholdSpecs.EnergyAcPercentThreshold is <= 0 or > 1d)
                throw new ArgumentOutOfRangeException();

            if (spectrumThresholdSpecs.SignalToNoiseRatioThreshold <= 1d)
                throw new ArgumentOutOfRangeException();

            // Compute complete Fourier spectrum of signal
            var scalarSpectrumFull = 
                (ScalarSignalSpectrumComplex) GetFourierSpectrum().RemoveHighFrequencySamples(spectrumThresholdSpecs.FrequencyThreshold);

            // Compute the energy spectrum of signal
            var energySpectrumFull = 
                (ScalarSignalSpectrumFloat64) GetEnergySpectrum().RemoveHighFrequencySamples(spectrumThresholdSpecs.FrequencyThreshold);

            var samplingSpecs = energySpectrumFull.SamplingSpecs;
            
            // Define time axis values
            var tValues = 
                samplingSpecs.GetSampledTimeSignal();

            // Add DC components to final spectrum
            var vectorSpectrum = 
                new ScalarSignalSpectrumComplex(samplingSpecs) { scalarSpectrumFull.SamplesDc };

            // Test total AC energy threshold
            var vectorSignalEnergyAc = EnergyAc();
            if (vectorSignalEnergyAc < spectrumThresholdSpecs.EnergyAcThreshold)
            {
                // Add a single frequency to the spectrum
                var (energySample1, energySample2) = 
                    energySpectrumFull
                        .SamplePairsAc
                        .OrderByDescending(p => energySpectrumFull.GetValueSumAc(p))
                        .First();

                vectorSpectrum.Add(scalarSpectrumFull.GetSample(energySample1.Index));
                vectorSpectrum.Add(scalarSpectrumFull.GetSample(energySample2.Index));

                return vectorSpectrum;
            }

            // Select all energy spectrum AC sample pairs
            var energySamplePairs = 
                energySpectrumFull
                    .SamplePairsAc
                    .OrderByDescending(p => 
                        energySpectrumFull.GetValueSumAc(p)
                    ).ToArray();

            // Compute energy threshold for selecting suitable spectrum samples
            var energy = energySpectrumFull.Sum(s => s.Value);
            var energyThreshold = spectrumThresholdSpecs.EnergyAcPercentThreshold * energy;

            // Define initial error signal for gradually computing SNR
            var sumOfSquares = SumOfSquares();
            var errorSignal = this - vectorSpectrum.GetRealSignal(tValues);

            var frequencyCountThreshold = spectrumThresholdSpecs.FrequencyCountThreshold;

            foreach (var (energySample1, energySample2) in energySamplePairs)
            {
                var index1 = energySample1.Index;
                var index2 = energySample2.Index;

                frequencyCountThreshold--;

                if (index1 == index2)
                {
                    // Add the selected samples to spectrum
                    var sample1 = scalarSpectrumFull.GetSample(index1);

                    vectorSpectrum.Add(sample1);

                    if (frequencyCountThreshold <= 0)
                        return vectorSpectrum;

                    // Update energy threshold
                    energyThreshold -= energySpectrumFull.GetValueAc(index1);
                    
                    //Console.WriteLine($"Energy = {(1 - energyThreshold / energy):P5}");

                    // Test energy threshold stop condition
                    if (energyThreshold < 0)
                        return vectorSpectrum;
                    
                    // Update error signal
                    errorSignal -= scalarSpectrumFull.GetRealSignal(sample1, tValues);
                }
                else
                {
                    // Add the selected samples to spectrum
                    var sample1 = scalarSpectrumFull.GetSample(index1);
                    var sample2 = scalarSpectrumFull.GetSample(index2);

                    vectorSpectrum.Add(sample1);
                    vectorSpectrum.Add(sample2);
                    
                    if (frequencyCountThreshold <= 0)
                        return vectorSpectrum;

                    // Update energy threshold
                    energyThreshold -= energySpectrumFull.GetValueAc(index1);
                    energyThreshold -= energySpectrumFull.GetValueAc(index2);
                    
                    //Console.WriteLine($"Energy = {(1 - energyThreshold / energy):P5}");

                    // Test energy threshold stop condition
                    if (energyThreshold < 0)
                        return vectorSpectrum;

                    // Update error signal
                    errorSignal -= scalarSpectrumFull.GetRealSignal(sample1, tValues);
                    errorSignal -= scalarSpectrumFull.GetRealSignal(sample2, tValues);
                }
                
                // Test SNR threshold stop condition
                var signalToNoiseRatio = 
                    sumOfSquares / errorSignal.SumOfSquares();

                //Console.WriteLine($"SNR = {signalToNoiseRatio:G}");

                if (signalToNoiseRatio >= spectrumThresholdSpecs.SignalToNoiseRatioThreshold)
                    break;
            }

            Console.WriteLine();

            return vectorSpectrum;
        }

        public ScalarSignalSpectrumComplex GetFourierSpectrum(double energyPercentThreshold, double signalToNoiseRatioThreshold)
        {
            if (energyPercentThreshold is <= 0 or > 1d)
                throw new ArgumentOutOfRangeException(nameof(energyPercentThreshold));

            if (signalToNoiseRatioThreshold <= 1d)
                throw new ArgumentOutOfRangeException(nameof(signalToNoiseRatioThreshold));

            var tValues = 
                GetSampledTimeSignal();

            var fullSpectrum = 
                GetFourierSpectrum();


            var spectrum = new ScalarSignalSpectrumComplex(SamplingSpecs);

            // Add DC components
            foreach (var spectrumSample in fullSpectrum.SamplesDc)
                spectrum.Add(spectrumSample);

            // Add significant AC components
            var minEnergyAc = energyPercentThreshold * fullSpectrum.GetEnergyAc();
            var sumOfSquares = SumOfSquares();
            var errorSignal = this - spectrum.GetRealSignal(tValues);
            var sampleList =
                fullSpectrum
                    .SamplePairsAc
                    .OrderByDescending(s => fullSpectrum.GetEnergyAc(s));

            foreach (var (spectrumSample1, spectrumSample2) in sampleList)
            {
                spectrum.Add(spectrumSample1);
                minEnergyAc -= fullSpectrum.GetEnergyAc(spectrumSample1.Index);

                if (spectrumSample1.Index != spectrumSample2.Index)
                {
                    spectrum.Add(spectrumSample2);
                    minEnergyAc -= fullSpectrum.GetEnergyAc(spectrumSample2.Index);
                }
                
                if (minEnergyAc <= 0)
                    break;
                
                errorSignal -= fullSpectrum.GetRealSignal(spectrumSample1, tValues);

                if (spectrumSample1.Index != spectrumSample2.Index)
                    errorSignal -= fullSpectrum.GetRealSignal(spectrumSample2, tValues);

                var signalToNoiseRatio = 
                    sumOfSquares / errorSignal.SumOfSquares();

                if (signalToNoiseRatio >= signalToNoiseRatioThreshold)
                    break;
            }

            spectrum.RemoveZeroValueSamples();

            return spectrum;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<double> GetEnumerator()
        {
            return _sampleList.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public override string ToString()
        {
            return _sampleList
                .Select(v => v.ToString("G"))
                .Concatenate(", ", "{", "}");
        }
    }
}
