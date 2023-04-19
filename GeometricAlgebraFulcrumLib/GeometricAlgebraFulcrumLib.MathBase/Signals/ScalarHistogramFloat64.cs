using System.Collections.Immutable;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.MathBase.Signals
{
    public sealed class ScalarHistogramFloat64
    {
        public static ScalarHistogramFloat64 Create(double pinReference, double pinWidth, IEnumerable<double> dataValueList)
        {
            var histSum = 0d;
            var histogramData1 = new SortedDictionary<double, double>();

            foreach (var dataValue in dataValueList)
            {
                if (double.IsNaN(dataValue) || double.IsInfinity(dataValue))
                    throw new ArgumentException(nameof(dataValue));

                var pin = 
                    Math.Round((dataValue - pinReference) / pinWidth) * pinWidth + pinReference;

                Debug.Assert(
                    !double.IsNaN(pin) && 
                    !double.IsInfinity(pin)
                );

                if (histogramData1.TryGetValue(pin, out var hist))
                    histogramData1[pin] = hist + 1;
                else
                    histogramData1.Add(pin, 1);

                histSum++;
            }

            var pinCountTotalInv = 1d / histSum;
            var histogramData =
                histogramData1.ToImmutableSortedDictionary(
                    p => p.Key,
                    p => p.Value * pinCountTotalInv
                );

            return new ScalarHistogramFloat64(pinReference, pinWidth, histogramData);
        }

        private readonly ImmutableSortedDictionary<double, double> _histogramData;


        public double PinReference { get; }

        public double PinWidth { get; }

        public int SparsePinCount 
            => _histogramData.Count;

        public int DensePinCount 
            => 1 + (int) Math.Round((MaxPinValue - MinPinValue) / PinWidth);

        public double MinPinValue 
            => _histogramData.First().Key;

        public double MaxPinValue 
            => _histogramData.Last().Key;

        public double this[double dataValue]
        {
            get
            {
                var pin = 
                    Math.Round((dataValue - PinReference) / PinWidth) * PinWidth + PinReference;

                return _histogramData.TryGetValue(pin, out var hist) 
                    ? hist : 0d;
            }
        }


        public ScalarHistogramFloat64(double pinReference, double pinWidth, ImmutableSortedDictionary<double, double> histogramData)
        {
            if (double.IsNaN(pinWidth) || double.IsInfinity(pinWidth) || pinWidth <= 0)
                throw new ArgumentException(nameof(pinWidth));

            _histogramData = histogramData;
            PinReference = pinReference;
            PinWidth = pinWidth;
        }

    
        public ScalarHistogramFloat64 Trim(double trimPercentage)
        {
            if (trimPercentage < 0d)
                throw new ArgumentException(nameof(trimPercentage));

            var pinHistList =
                _histogramData
                    .OrderBy(p => p.Value)
                    .ThenBy(p => p.Key);

            var histogramData1 = new SortedDictionary<double, double>();

            var histSum = 0d;
            foreach (var (pin, hist) in pinHistList)
            {
                if (trimPercentage <= 0)
                {
                    histogramData1.Add(pin, hist);
                    histSum += hist;

                    continue;
                }

                trimPercentage -= hist;
            }

            var histSumInv = 1d / histSum;
            var histogramData =
                histogramData1.ToImmutableSortedDictionary(
                    p => p.Key,
                    p => p.Value * histSumInv
                );

            return new ScalarHistogramFloat64(PinReference, PinWidth, histogramData);
        }

        //public ScalarSignalFloat64 ToSignal(double tMin, double tMax)
        //{
        //    var tSignal =
        //        tMin.GetLinearRange(tMax, )

        //    var signalArray = new double[DensePinCount];

        //    var i = 0;
        //    foreach (var (_, hist) in _histogramData)
        //        signalArray[i++] = hist;

        //    return signalArray.CreateSignal(1d / PinWidth);
        //}

        private double InterpolateNearestPinValue(double dataValue)
        {
            if (dataValue < MinPinValue) return MinPinValue;
            if (dataValue > MaxPinValue) return MaxPinValue;

            var pin1 = dataValue;
            var pin2 = dataValue;
            var hist1 = 0.5d;
            var hist2 = 0.5d;

            foreach (var (pin, hist) in _histogramData)
            {
                if (dataValue == pin) return pin;

                if (!(dataValue > pin)) continue;

                pin1 = pin;
                hist1 = hist;
                break;
            }

            foreach (var (pin, hist) in _histogramData)
            {
                if (!(dataValue < pin)) continue;

                pin2 = pin;
                hist2 = hist;
                break;
            }

            return (hist1 * pin1 + hist2 * pin2) / (hist1 + hist2);
        }

        public ScalarSignalFloat64 FilterSignal(ScalarSignalFloat64 signal)
        {
            return signal.MapSamples(
                s => this[s].IsNearZero() ? InterpolateNearestPinValue(s) : s
            );
        }
    }
}