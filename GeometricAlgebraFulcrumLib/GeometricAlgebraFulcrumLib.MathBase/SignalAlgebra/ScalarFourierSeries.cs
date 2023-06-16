using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra.Composers;
using MathNet.Numerics.IntegralTransforms;

namespace GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra
{
    public class ScalarFourierSeries :
        DifferentialBasicFunction
    {
        /// <summary>
        /// Apply FFT to each dimension of the sampled vector signal
        /// </summary>
        /// <param name="signalSamples"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Complex[] GetFourierArray(IEnumerable<double> signalSamples)
        {
            var complexSamplesArray = 
                signalSamples.Select(v => (Complex) v).ToArray();

            Fourier.Forward(complexSamplesArray, FourierOptions.Default);

            return complexSamplesArray;
        }
        
        private static IEnumerable<int> NormalizeFrequencyIndexSet(IEnumerable<int> frequencyIndexList, int sampleCount)
        {
            var freqIndexMax = 
                sampleCount.IsOdd()
                    ? (sampleCount - 1) / 2
                    : (sampleCount - 2) / 2;

            var freqIndexSet = new HashSet<int>(){0};
            foreach (var freqIndex in frequencyIndexList)
            {
                if (freqIndex <= freqIndexMax)
                    freqIndexSet.Add(freqIndex);

                else if (freqIndex < sampleCount)
                    freqIndexSet.Add(sampleCount - freqIndex);

                else
                    throw new InvalidOperationException();
            }

            return freqIndexSet;
        }
        
        private static ScalarFourierSeries Create(IReadOnlyList<Complex> complexSamples, double samplingRate, IEnumerable<int> frequencyIndexList)
        {
            var sampleCount = complexSamples.Count;
            var scalingFactor = Math.Sqrt(sampleCount);
            var signalTime = (sampleCount - 1) / samplingRate;
            var df = 1d / signalTime;

            //// For validation
            //foreach (var freqIndex in frequencyIndexList)
            //{
            //    var freqHz = freqIndex * df;

            //    Console.WriteLine($"Frequency Samples ({freqIndex}, {sampleCount - freqIndex}) = ±{freqHz:G} Hz");
            //    Console.WriteLine();
            //}

            var freqIndexSet = 
                NormalizeFrequencyIndexSet(frequencyIndexList, sampleCount)
                    .Where(i => i > 0)
                    .OrderBy(i => i)
                    .ToArray();

            var interpolator = new ScalarFourierSeries();

            {
                // The index for the positive frequency of the spectrum
                var freqIndex = 0;

                // The positive frequency value
                var freqHz = 0d;
                var freqRad = 0d;

                var complexSample = complexSamples[freqIndex];

                var cosScalar = 
                    complexSample.Real / scalingFactor;

                var sinScalar = 
                    -complexSample.Imaginary / scalingFactor;

                interpolator.SetTerm(
                    freqRad,
                    cosScalar,
                    sinScalar
                );
            }

            //var signalTime = (signalSamples.Count - 1) / samplingRate;
            //var df = 1d / signalTime;

            foreach (var i in freqIndexSet)
            {
                // The index for the positive frequency of the spectrum
                var freqIndex1 = i;
                
                // The index for the negative frequency of the spectrum
                var freqIndex2 = sampleCount - i;

                // The positive frequency value
                var freqHz = i * df;
                var freqRad = 2 * Math.PI * freqHz;

                var complexSample1 = complexSamples[freqIndex1];
                var complexSample2 = complexSamples[freqIndex2];

                var cosScalarScalars = 
                    (complexSample2.Real + complexSample1.Real) / scalingFactor;

                var sinScalarScalars = 
                    (complexSample2.Imaginary - complexSample1.Imaginary) / scalingFactor;

                interpolator.SetTerm(
                    freqRad,
                    cosScalarScalars,
                    sinScalarScalars
                );
            }

            return interpolator;
        }
        
        private static ScalarFourierSeries Create(IReadOnlyList<Complex> complexSamples, Float64Signal scalarSignal, double snrThreshold, IEnumerable<int> frequencyIndexList)
        {
            var sampleCount = complexSamples.Count;
            var scalingFactor = Math.Sqrt(sampleCount);
            var signalTime = (sampleCount - 1) / scalarSignal.SamplingRate;
            var df = 1d / signalTime;

            //// For validation
            //foreach (var freqIndex in frequencyIndexList)
            //{
            //    var freqHz = freqIndex * df;

            //    Console.WriteLine($"Frequency Samples ({freqIndex}, {sampleCount - freqIndex}) = ±{freqHz:G} Hz");
            //    Console.WriteLine();
            //}

            var interpolator = new ScalarFourierSeries();

            {
                // The index for the positive frequency of the spectrum
                var freqIndex = 0;

                // The positive frequency value
                var freqHz = 0d;
                var freqRad = 0d;

                var complexSample = complexSamples[freqIndex];

                var cosScalar = 
                    complexSample.Real / scalingFactor;

                var sinScalar = 
                    -complexSample.Imaginary / scalingFactor;

                interpolator.SetTerm(
                    freqRad,
                    cosScalar,
                    sinScalar
                );
            }

            var tValues =
                0d.GetLinearRange(
                    (scalarSignal.Count - 1) / scalarSignal.SamplingRate, 
                    scalarSignal.Count
                ).CreateSignal(scalarSignal.SamplingRate);
            
            var freqIndexSet = 
                NormalizeFrequencyIndexSet(frequencyIndexList, sampleCount)
                    .Where(i => i > 0)
                    .ToArray();

            // Add the average value of the signal
            var sumOfSquares = scalarSignal.SumOfSquares();
            var errorSignal = scalarSignal - interpolator.GetSignal(tValues);

            foreach (var i in freqIndexSet)
            {
                // The index for the positive frequency of the spectrum
                var freqIndex1 = i;
                
                // The index for the negative frequency of the spectrum
                var freqIndex2 = sampleCount - i;

                // The positive frequency value
                var freqHz = i * df;
                var freqRad = 2 * Math.PI * freqHz;

                var complexSample1 = complexSamples[freqIndex1];
                var complexSample2 = complexSamples[freqIndex2];

                var cosScalarScalars = 
                    (complexSample2.Real + complexSample1.Real) / scalingFactor;

                var sinScalarScalars = 
                    (complexSample2.Imaginary - complexSample1.Imaginary) / scalingFactor;

                var term = new ScalarFourierSeriesTerm(
                    cosScalarScalars, 
                    sinScalarScalars, 
                    freqRad
                );

                interpolator.SetTerm(term);

                errorSignal -= term.GetSignal(tValues);

                var signalToNoiseRatio = 
                    sumOfSquares / errorSignal.SumOfSquares();

                if (signalToNoiseRatio >= snrThreshold)
                    break;
            }

            return interpolator;
        }
        
        
        /// <summary>
        /// Apply FFT to given real sampled signal and find the frequency indices of the
        /// dominant frequency using a ratio of the total signal energy
        /// </summary>
        /// <param name="signalSamples"></param>
        /// <param name="energyThreshold"></param>
        /// <returns></returns>
        public static IEnumerable<int> GetDominantFrequencyIndexSet(IEnumerable<double> signalSamples, double energyThreshold = 0.998d)
        {
            // Compute FFT
            var real = signalSamples.ToArray();
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
                    (real[freqIndex].Square() + imaginary[freqIndex].Square()) * sampleCount / (2 * Math.PI);

                energyDictionary.Add(energy, freqIndex);

                energySum += energy;
            }

            // Find frequencies with most energy, but always include 0 frequency
            var threshold = energyThreshold * energySum;
            var indexSet = new HashSet<int> {0};

            foreach (var (energy, freqIndex) in energyDictionary.Reverse())
            {
                threshold -= energy;

                if (threshold >= 0d)// || indexSet.Count < 2)
                    indexSet.Add(freqIndex);
                else
                    break;
            }
            
            return indexSet;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarFourierSeries Create()
        {
            return new ScalarFourierSeries();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarFourierSeries Create(IEnumerable<double> signalSamples, double samplingRate, IEnumerable<int> frequencyIndexList)
        {
            var complexSamples = 
                GetFourierArray(signalSamples);
            
            return Create(
                complexSamples,
                samplingRate,
                frequencyIndexList
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarFourierSeries Create(Float64Signal signalSamples, double snrThreshold, IEnumerable<int> frequencyIndexList)
        {
            var complexSamples = 
                GetFourierArray(signalSamples);
            
            return Create(
                complexSamples,
                signalSamples,
                snrThreshold,
                frequencyIndexList
            );
        }

        /// <summary>
        /// Create a Fourier interpolator based on the given sampled periodic signal
        /// </summary>
        /// <param name="signalSamples"></param>
        /// <param name="samplingRate"></param>
        /// <param name="energyThreshold"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarFourierSeries Create(IReadOnlyList<double> signalSamples, double samplingRate, double energyThreshold = 0.998d)
        {
            var frequencyIndexSet = GetDominantFrequencyIndexSet(
                signalSamples,
                energyThreshold
            );

            return Create(signalSamples, samplingRate, frequencyIndexSet);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarFourierSeries operator -(ScalarFourierSeries s1)
        {
            var termsDictionary = s1._termsDictionary.ToDictionary(
                pair => pair.Key,
                pair => pair.Value.Negative()
            );

            return new ScalarFourierSeries(termsDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarFourierSeries operator *(double v, ScalarFourierSeries s1)
        {
            var termsDictionary = s1._termsDictionary.ToDictionary(
                pair => pair.Key,
                pair => pair.Value.Times(v)
            );

            return new ScalarFourierSeries(termsDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarFourierSeries operator *(ScalarFourierSeries s1, double v)
        {
            var termsDictionary = s1._termsDictionary.ToDictionary(
                pair => pair.Key,
                pair => pair.Value.Times(v)
            );

            return new ScalarFourierSeries(termsDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarFourierSeries operator /(ScalarFourierSeries s1, double v)
        {
            v = 1d / v;

            var termsDictionary = s1._termsDictionary.ToDictionary(
                pair => pair.Key,
                pair => pair.Value.Times(v)
            );

            return new ScalarFourierSeries(termsDictionary);
        }

        public static ScalarFourierSeries operator +(ScalarFourierSeries s1, ScalarFourierSeries s2)
        {
            var termsDictionary = s1._termsDictionary.ToDictionary(
                pair => pair.Key,
                pair => pair.Value.Negative()
            );

            var s3 = new ScalarFourierSeries(termsDictionary);

            foreach (var term in s2.Terms) 
                s3.AddTerm(term.Frequency, term.CosScalar, term.SinScalar);

            return s3;
        }
        
        public static ScalarFourierSeries operator -(ScalarFourierSeries s1, ScalarFourierSeries s2)
        {
            var termsDictionary = s1._termsDictionary.ToDictionary(
                pair => pair.Key,
                pair => pair.Value.Negative()
            );

            var s3 = new ScalarFourierSeries(termsDictionary);

            foreach (var term in s2.Terms) 
                s3.AddTerm(term.Frequency, -term.CosScalar, -term.SinScalar);

            return s3;
        }


        private readonly Dictionary<double, ScalarFourierSeriesTerm> _termsDictionary;

        public IEnumerable<ScalarFourierSeriesTerm> Terms 
            => _termsDictionary
                .Values
                .OrderByDescending(t => 
                    t.GetEnergy(1)
                );

        
        public override bool CanBeSimplified 
            => false;

        public override bool IsConstant 
            => false;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ScalarFourierSeries(Dictionary<double, ScalarFourierSeriesTerm> termsDictionary)
        {
            _termsDictionary = termsDictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ScalarFourierSeries()
        {
            _termsDictionary = new Dictionary<double, ScalarFourierSeriesTerm>();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarFourierSeries AddTerm(double frequency, double cosScalar, double sinScalar)
        {
            if (_termsDictionary.TryGetValue(frequency.Abs(), out var term))
            {
                term.AddScalars(
                    cosScalar, 
                    frequency >= 0 ? sinScalar : -sinScalar
                );
            }
            else
            {
                term = new ScalarFourierSeriesTerm(cosScalar, sinScalar, frequency);

                _termsDictionary.Add(term.Frequency, term);
            }

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarFourierSeries SubtractTerm(double frequency, double cosScalar, double sinScalar)
        {
            if (_termsDictionary.TryGetValue(frequency.Abs(), out var term))
            {
                term.AddScalars(
                    -cosScalar, 
                    frequency >= 0 ? -sinScalar : sinScalar
                );
            }
            else
            {
                term = new ScalarFourierSeriesTerm(cosScalar, sinScalar, frequency);

                _termsDictionary.Add(term.Frequency, term);
            }

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarFourierSeries SetTerm(ScalarFourierSeriesTerm term)
        {
            var frequency = term.Frequency;
            
            if (_termsDictionary.ContainsKey(frequency))
                _termsDictionary[frequency] = term;
            else
                _termsDictionary.Add(frequency, term);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarFourierSeries SetTerm(double frequency, double cosScalar, double sinScalar)
        {
            var term = new ScalarFourierSeriesTerm(cosScalar, sinScalar, frequency);
            
            frequency = term.Frequency;
            
            if (_termsDictionary.ContainsKey(frequency))
                _termsDictionary[frequency] = term;
            else
                _termsDictionary.Add(frequency, term);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarFourierSeries GetFourierDerivativeN(int order = 1)
        {
            var termsDictionary = 
                _termsDictionary
                    .Values
                    .ToDictionary(
                        term => term.Frequency, 
                        term => term.GetFourierTermDerivativeN(order)
                    );

            return new ScalarFourierSeries(termsDictionary);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetValue(double parameterValue)
        {
            return _termsDictionary.Values.Aggregate(
                0d, 
                (current, term) => current + term.GetScalar(parameterValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Tuple<bool, DifferentialFunction> TrySimplify()
        {
            return new Tuple<bool, DifferentialFunction>(false, this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction Simplify()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction GetDerivative1()
        {
            return GetFourierDerivativeN(1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction GetDerivativeN(int order)
        {
            return GetFourierDerivativeN(order);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetDerivativeNValue(double parameterValue, int order = 1)
        {
            return _termsDictionary.Values.Aggregate(
                0d, 
                (current, term) => current + term.GetScalarDt(parameterValue, order)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal GetSignal(Float64Signal tValuesSignal)
        {
            return tValuesSignal.Select(GetValue).CreateSignal(tValuesSignal.SamplingRate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal GetSignalDt(Float64Signal tValuesSignal)
        {
            return tValuesSignal.Select(GetDerivativeNValue).CreateSignal(tValuesSignal.SamplingRate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<double> GetScalarsDt(IEnumerable<double> parameterValueList, int degree = 1)
        {
            var interpolator = GetFourierDerivativeN(degree);

            return parameterValueList.Select(
                t => interpolator.GetValue(t)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetEnergy(int sampleCount)
        {
            return _termsDictionary
                .Values
                .Sum(t => t.GetEnergy(sampleCount));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetEnergyDc(int sampleCount)
        {
            return _termsDictionary
                .Values
                .Sum(t => t.GetEnergyDc(sampleCount));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetEnergyAc(int sampleCount)
        {
            return _termsDictionary
                .Values
                .Sum(t => t.GetEnergyAc(sampleCount));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetEnergyAc(Float64Signal tValues)
        {
            return _termsDictionary
                .Values
                .Sum(t => t.GetEnergyAc(tValues));
        }

        public string GetCSharpCode()
        {
            var composer = new StringBuilder();

            composer
                .AppendLine("var interpolator = ScalarFourierInterpolator.Create();")
                .AppendLine();

            foreach (var term in Terms)
                composer.AppendLine(term.GetCSharpCode());

            composer.AppendLine();

            return composer.ToString();
        }

        public string GetTextDescription(string signalName, double totalEnergyAc, Float64Signal tValues)
        {
            var sampleCount = tValues.Count;

            var composer = new StringBuilder();

            composer.AppendLine($"Fourier signal {signalName}: Original AC energy = {totalEnergyAc:G}, Interpolator AC energy = {GetEnergyAc(sampleCount):G}");

            var energySum = 0d;

            var termIndex = 0;
            foreach (var term in Terms)
            {
                var energyTime = term.GetEnergyAc(tValues);
                var energyFreq = term.GetEnergyAc(sampleCount);
                var energyTimeFreqRatio = energyTime / energyFreq;
                energySum += energyTime;

                var energyPercent = energyTime / totalEnergyAc;
                var energySumPercent = energySum / totalEnergyAc;

                composer
                    .AppendLine(@$"   Term {termIndex,5}: {term}")
                    .AppendLine(@$"      Frequency    : ±{term.FrequencyHz:G} Hz = ±{term.Frequency:G} rad/sec")
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

        public override string ToString()
        {
            var composer = new StringBuilder();

            foreach (var term in Terms)
                composer.AppendLine(term.ToString());

            composer.AppendLine();

            return composer.ToString();
        }
    }
}