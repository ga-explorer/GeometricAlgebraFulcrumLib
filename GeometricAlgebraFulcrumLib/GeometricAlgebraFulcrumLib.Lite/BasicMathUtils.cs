using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Lite
{
    public static class BasicMathUtils
    {
        

        #region General Operations
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Clamp<T>(this T value, T lowerLimit, T upperLimit) where T : IComparable<T>
        {
            if (value.CompareTo(lowerLimit) < 0) return lowerLimit;
            if (value.CompareTo(upperLimit) > 0) return upperLimit;

            return value;
        }

        #endregion


        #region Integer Operations
        public static IEnumerable<double> GetRegularSamples(this int samplesCount, double firstValue, double lastValue, bool excludeFirst = false, bool excludeLast = false)
        {
            var firstSampleIndex = 0;
            var newSamplesCount = samplesCount - 1;

            if (excludeFirst)
            {
                firstSampleIndex++;
                newSamplesCount++;
            }

            if (excludeLast)
            {
                newSamplesCount++;
            }

            return
                Enumerable
                    .Range(firstSampleIndex, samplesCount)
                    .Select(i => i / (double)newSamplesCount)
                    .Select(t => (1.0d - t) * firstValue + t * lastValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetRegularSamplesTwoPi(this int samplesCount)
        {
            return Enumerable
                .Range(0, samplesCount)
                .Select(i => 
                    MathNet.Numerics.Constants.Pi2 * i / samplesCount
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetRegularSamplesPi(this int samplesCount)
        {
            return Enumerable
                .Range(0, samplesCount)
                .Select(i => Math.PI * i / samplesCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetRegularSamples(this int samplesCount, double angleMax)
        {
            return Enumerable
                .Range(0, samplesCount)
                .Select(i => angleMax * i / samplesCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOf2(this int n)
        {
            return n > 0 && ~(n & (n - 1)) > 0;
        }

        /// <summary>
        /// Finds the smallest power of 2 that is >= n
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Power2Ceiling(this int n)
        {
            n--;

            n |= n >> 1;
            n |= n >> 2;
            n |= n >> 4;
            n |= n >> 8;
            n |= n >> 16;

            return n + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOf2(this long n)
        {
            return n > 0 && ~(n & (n - 1)) > 0;
        }

        /// <summary>
        /// Finds the smallest power of 2 that is >= n
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Power2Ceiling(this long n)
        {
            n--;

            n |= n >> 1;
            n |= n >> 2;
            n |= n >> 4;
            n |= n >> 8;
            n |= n >> 16;

            return n + 1;
        }
        #endregion
    }
}
