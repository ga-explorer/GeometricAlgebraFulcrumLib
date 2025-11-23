namespace GeometricAlgebraFulcrumLib.Modeling.Signals.Filters
{
    /// <summary>
    /// Implementation of the 1-Euro filter.
    /// Casiez, Roussel, and Vogel, UIST 2012.
    /// </summary>
    public sealed class OneEuroFilter :
        IFloat64SignalFilter
    {
        private static double Alpha(double cutoff, double freq)
        {
            var tau = 1.0 / (2 * Math.PI * cutoff);
            var te = 1.0 / freq;
            return 1.0 / (1.0 + tau / te);
        }

        private static double ExponentialSmoothing(double alpha, double x, double xPrev)
        {
            return alpha * x + (1.0 - alpha) * xPrev;
        }
        
        /// <summary>
        /// Filters an array of samples (uniformly sampled).
        /// freq = sampling frequency (Hz)
        /// </summary>
        public static OneEuroFilter Create(double freq = 60.0, double minCutoff = 1.0, double beta = 0.007, double dCutoff = 1.0)
        {
            return new OneEuroFilter(freq, minCutoff, beta, dCutoff);
        }


        private readonly double _freq;          // Sampling frequency [Hz]
        private readonly double _minCutoff;     // Minimum cutoff frequency [Hz]
        private readonly double _beta;          // Speed coefficient
        private readonly double _dCutoff;       // Derivative cutoff frequency [Hz]
        private double _xPrev;                  // Previous filtered value
        private double _dxPrev;                 // Previous filtered derivative
        private bool _initialized;


        private OneEuroFilter(double freq, double minCutoff = 1.0, double beta = 0.007, double dCutoff = 1.0)
        {
            if (freq <= 0)
                throw new ArgumentOutOfRangeException(nameof(freq), "Sampling frequency must be > 0.");

            _freq = freq;
            _minCutoff = minCutoff;
            _beta = beta;
            _dCutoff = dCutoff;
        }

        
        private double Filter(double x, double dx = double.NaN)
        {
            if (!_initialized)
            {
                _xPrev = x;
                _dxPrev = 0.0;
                _initialized = true;
                return x;
            }

            // Estimate derivative if not provided
            if (double.IsNaN(dx))
                dx = (x - _xPrev) * _freq;

            // Low-pass filter the derivative
            var alphaD = Alpha(_dCutoff, _freq);
            var dxHat = ExponentialSmoothing(alphaD, dx, _dxPrev);

            // Adaptive cutoff based on signal velocity
            var cutoff = _minCutoff + _beta * Math.Abs(dxHat);
            var alpha = Alpha(cutoff, _freq);

            // Filtered value
            var xHat = ExponentialSmoothing(alpha, x, _xPrev);

            // Save for next iteration
            _xPrev = xHat;
            _dxPrev = dxHat;

            return xHat;
        }
        

        public OneEuroFilter Reset()
        {
            _initialized = false;
            return this;
        }

        /// <summary>
        /// Filters an array of samples (uniformly sampled).
        /// </summary>
        public double[] Filter(IReadOnlyList<double> input)
        {
            var output = new double[input.Count];
            for (var i = 0; i < input.Count; i++)
                output[i] = Filter(input[i]);
            return output;
        }
        
        /// <summary>
        /// Filters a sequence of samples (uniformly sampled).
        /// </summary>
        public IEnumerable<double> Filter(IEnumerable<double> input)
        {
            return input.Select(sample => Filter(sample));
        }
    }
}
