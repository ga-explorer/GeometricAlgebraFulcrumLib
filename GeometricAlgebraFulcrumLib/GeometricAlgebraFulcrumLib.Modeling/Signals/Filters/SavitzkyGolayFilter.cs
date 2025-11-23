using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Modeling.Signals.Filters
{
    /// <summary>
    /// Savitzky–Golay smoothing (and optionally differentiation) filter for uniformly sampled signals.
    /// Fits a polynomial of given order within a sliding window and computes convolution coefficients.
    /// </summary>
    public sealed class SavitzkyGolayFilter : 
        IFloat64SignalFilter
    {
        /// <summary>
        /// Strategy for handling boundaries (window extends beyond data ends).
        /// </summary>
        public enum BoundaryHandling
        {
            Clamp,      // repeat edge value (i.e., clamp index to 0 or N-1)
            Mirror,     // mirror the data around edges
            PadWithZeros, // pad with zeros
            ShrinkWindow  // shrink window size near boundaries
        }
        
        /// <summary>
        /// Compute convolution coefficients for the SG filter (smoothing or derivative) at the centre point.
        /// </summary>
        private static Vector<double> ComputeCoefficients(int windowSize, int polyOrder, int derivativeOrder, double samplingInterval)
        {
            var half = windowSize / 2;

            // Construct design matrix: rows for i = -half..+half, columns for c=0..polyOrder
            var A = DenseMatrix.Create(windowSize, polyOrder + 1,
                (r, c) => Math.Pow(r - half, c));

            // If sampling interval h ≠ 1, scale x‐values: x = (i* h) so use (r-half)*h:
            if (Math.Abs(samplingInterval - 1.0) > double.Epsilon)
            {
                for (var r = 0; r < windowSize; r++)
                {
                    var x = (r - half) * samplingInterval;
                    for (var c = 0; c <= polyOrder; c++)
                        A[r, c] = Math.Pow(x, c);
                }
            }

            // Form normal equations and compute pseudo‐inverse via e.g., QR or SVD for stability
            // For simplicity we use the normal inverse when well‐conditioned
            var ATA = A.TransposeThisAndMultiply(A);
            var AT = A.Transpose();

            // Solve for coefficient vector u such that predicted value at centre (z=0) or derivative at centre
            // The target vector u is unit vector selecting derivativeOrder in polynomial basis.
            // For smoothing (deriv=0) we want a0 → unit.
            var b = Vector<double>.Build.Dense(polyOrder + 1, 0.0);
            
            // For derivative of order d, the contribution is factorial(d)*a_d (if spaced at h=1)
            // More generally for spacing h: a_d * d! / h^d
            b[derivativeOrder] = Factorial(derivativeOrder) / Math.Pow(samplingInterval, derivativeOrder);
            Vector<double> aCoeffs;
            try
            {
                aCoeffs = ATA.Inverse() * (AT * b);
            }
            catch
            {
                // fallback using pseudo‐inverse via SVD
                aCoeffs = (ATA.PseudoInverse()) * (AT * b);
            }

            // Now coefficients for convolution = aCoeffsᵀ * Aᵀ = (row vector) → weights correspond to each row of A
            // Equivalent: weights = aCoeffsᵀ * (Aᵀ) = (A * aCoeffs) with appropriate ordering
            var weights = A * aCoeffs;

            // For smoothing (deriv=0) we normalize weights to sum=1
            if (derivativeOrder == 0)
            {
                var sum = weights.Sum();
                if (Math.Abs(sum) > double.Epsilon)
                    weights = weights.Divide(sum);
            }

            return weights;

            double Factorial(int n)
            {
                var r = 1.0;
                for (var i = 2; i <= n; i++) r *= i;
                return r;
            }
        }

        /// <summary>
        /// Factory method to simplify creation.
        /// </summary>
        public static SavitzkyGolayFilter Create(int windowSize = 11, int polynomialOrder = 3, int derivativeOrder = 0, double samplingInterval = 1.0, BoundaryHandling boundary = BoundaryHandling.Clamp)
        {
            return new SavitzkyGolayFilter(windowSize, polynomialOrder, derivativeOrder, samplingInterval, boundary);
        }

        private readonly int _windowSize;
        private readonly int _polyOrder;
        private readonly int _derivativeOrder;
        private readonly double _h; // sampling interval
        private readonly Vector<double> _coefficients;
        private readonly BoundaryHandling _boundary;


        /// <summary>
        /// Create a new Savitzky–Golay filter.
        /// </summary>
        /// <param name="windowSize">Odd integer ≥ 3 defining smoothing window size (2m+1).</param>
        /// <param name="polynomialOrder">Polynomial order (typically 2,3, ≤ windowSize-1).</param>
        /// <param name="derivativeOrder">Order of derivative to compute (0 = smoothing).</param>
        /// <param name="samplingInterval">Spacing between samples (h). Default = 1.</param>
        /// <param name="boundary">Boundary handling strategy.</param>
        private SavitzkyGolayFilter(int windowSize = 11, int polynomialOrder = 3, int derivativeOrder = 0, double samplingInterval = 1.0, BoundaryHandling boundary = BoundaryHandling.Clamp)
        {
            if (windowSize < 3 || windowSize % 2 == 0)
                throw new ArgumentException("Window size must be odd integer ≥ 3.", nameof(windowSize));
            if (polynomialOrder < 0 || polynomialOrder >= windowSize)
                throw new ArgumentException("Polynomial order must be nonnegative and less than window size.", nameof(polynomialOrder));
            if (derivativeOrder < 0 || derivativeOrder > polynomialOrder)
                throw new ArgumentException("Derivative order must be ≥0 and ≤ polynomial order.", nameof(derivativeOrder));
            if (samplingInterval <= 0)
                throw new ArgumentException("Sampling interval must be > 0.", nameof(samplingInterval));

            _windowSize = windowSize;
            _polyOrder = polynomialOrder;
            _derivativeOrder = derivativeOrder;
            _h = samplingInterval;
            _boundary = boundary;

            _coefficients = ComputeCoefficients(windowSize, polynomialOrder, derivativeOrder, samplingInterval);
        }


        /// <summary>
        /// Filter (smooth or differentiate) the input signal array.
        /// </summary>
        /// <param name="input">Input samples (uniformly spaced).</param>
        /// <returns>Filtered output (same length).</returns>
        public double[] Filter(IReadOnlyList<double> input)
        {
            if (input == null) 
                throw new ArgumentNullException(nameof(input));

            var n = input.Count;
            var half = _windowSize / 2;
            var output = new double[n];

            for (var i = 0; i < n; i++)
            {
                var acc = 0.0;
                for (var j = -half; j <= half; j++)
                {
                    var idx = i + j;
                    double value;

                    if (idx < 0 || idx >= n)
                    {
                        switch (_boundary)
                        {
                            case BoundaryHandling.Clamp:
                                idx = Math.Clamp(idx, 0, n - 1);
                                value = input[idx];
                                break;
                            case BoundaryHandling.Mirror:
                                idx = (idx < 0) ? -idx : (idx >= n ? 2*(n-1)-idx : idx);
                                value = input[idx];
                                break;
                            case BoundaryHandling.PadWithZeros:
                                value = 0.0;
                                break;
                            case BoundaryHandling.ShrinkWindow:
                                // skip contribution (effectively narrower window)
                                continue;
                            default:
                                value = input[Math.Clamp(idx, 0, n - 1)];
                                break;
                        }
                    }
                    else
                    {
                        value = input[idx];
                    }

                    acc += _coefficients[j + half] * value;
                }

                output[i] = acc;
            }

            return output;
        }

        /// <summary>
        /// Filter using IEnumerable input.
        /// </summary>
        public IEnumerable<double> Filter(IEnumerable<double> input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            return Filter(input.ToArray());
        }
    }
}
