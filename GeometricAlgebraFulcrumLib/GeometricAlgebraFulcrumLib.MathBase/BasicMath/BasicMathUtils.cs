using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath
{
    public static class BasicMathUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle<T> CreateAngle<T>(this IScalarProcessor<T> scalarProcessor, double radians)
        {
            return new PlanarAngle<T>(
                scalarProcessor, 
                scalarProcessor.GetScalarFromNumber(radians)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle<T> CreateAngle<T>(this IScalarProcessor<T> scalarProcessor, T radians)
        {
            return new PlanarAngle<T>(scalarProcessor, radians);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle<T> CreatePlanarAngle<T>(this IScalarProcessor<T> scalarProcessor, Float64PlanarAngle angle)
        {
            return scalarProcessor.CreateAngle(angle.Radians);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle<T> CreatePlanarAngleFromDegrees<T>(this IScalarProcessor<T> scalarProcessor, int angleInDegrees)
        {
            angleInDegrees = angleInDegrees switch
            {
                < -360 => (angleInDegrees % 720) + 360,
                > 360 => angleInDegrees % 360,
                _ => angleInDegrees
            };

            return scalarProcessor.CreateAngle(
                scalarProcessor.DegreesToRadians(angleInDegrees)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle<T> CreatePlanarAngleFromDegrees<T>(this IScalarProcessor<T> scalarProcessor, long angleInDegrees)
        {
            angleInDegrees = angleInDegrees switch
            {
                < -360L => (angleInDegrees % 720L) + 360L,
                > 360L => angleInDegrees % 360L,
                _ => angleInDegrees
            };

            return scalarProcessor.CreateAngle(
                scalarProcessor.DegreesToRadians(angleInDegrees)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle<T> CreatePlanarAngleFromDegrees<T>(this IScalarProcessor<T> scalarProcessor, float angleInDegrees)
        {
            angleInDegrees = angleInDegrees switch
            {
                < -360f => (angleInDegrees % 720f) + 360f,
                > 360f => angleInDegrees % 360f,
                _ => angleInDegrees
            };

            return scalarProcessor.CreateAngle(
                scalarProcessor.DegreesToRadians(angleInDegrees)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle<T> CreatePlanarAngleFromDegrees<T>(this IScalarProcessor<T> scalarProcessor, double angleInDegrees)
        {
            angleInDegrees = angleInDegrees switch
            {
                < -360d => (angleInDegrees % 720d) + 360d,
                > 360d => angleInDegrees % 360d,
                _ => angleInDegrees
            };

            return scalarProcessor.CreateAngle(
                scalarProcessor.DegreesToRadians(angleInDegrees)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle<T> CreatePlanarAngleFromDegrees<T>(this IScalarProcessor<T> scalarProcessor, T angleInDegrees)
        {
            return scalarProcessor.CreateAngle(
                scalarProcessor.DegreesToRadians(angleInDegrees)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle<T> CreatePlanarAngleFromRadians<T>(this IScalarProcessor<T> scalarProcessor, T angleInRadians)
        {
            return scalarProcessor.CreateAngle(
                angleInRadians
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle<T> CreatePlanarAngleFromUnitVectors<T>(this IScalarProcessor<T> scalarProcessor, IFloat64Tuple2D v1, IFloat64Tuple2D v2)
        {
            var angleInRadians = Math.Acos(v1.VectorDot(v2));

            return scalarProcessor.CreateAngle(
                scalarProcessor.GetScalarFromNumber(angleInRadians)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle<T> CreatePlanarAngleFromUnitVectors<T>(this IScalarProcessor<T> scalarProcessor, IFloat64Tuple3D v1, IFloat64Tuple3D v2)
        {
            var angleInRadians = Math.Acos(v1.VectorDot(v2));

            return scalarProcessor.CreateAngle(
                scalarProcessor.GetScalarFromNumber(angleInRadians)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle<T> CreatePlanarAngleFromUnitVectors<T>(this IScalarProcessor<T> scalarProcessor, IFloat64Tuple4D v1, IFloat64Tuple4D v2)
        {
            var angleInRadians = Math.Acos(v1.VectorDot(v2));

            return scalarProcessor.CreateAngle(
                scalarProcessor.GetScalarFromNumber(angleInRadians)
            );
        }


        #region Complex Numbers Operations
        #endregion


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
