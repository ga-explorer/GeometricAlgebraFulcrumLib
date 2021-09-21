using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Geometry.Frames
{
    public sealed class GeoFreeFrameSpecs
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFreeFrameSpecs CreateUndefinedSpecs()
        {
            return new GeoFreeFrameSpecs()
            {
                LinearlyIndependent = null,
                EqualNormSquared = null,
                EqualScalarProduct = null,
                UnitNormSquared = null,
                Orthogonal = null
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFreeFrameSpecs CreateLinearlyIndependentSpecs()
        {
            return new GeoFreeFrameSpecs()
            {
                LinearlyIndependent = true,
                EqualNormSquared = null,
                EqualScalarProduct = null,
                UnitNormSquared = null,
                Orthogonal = null
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFreeFrameSpecs CreateUnitBasisSpecs()
        {
            return new GeoFreeFrameSpecs()
            {
                LinearlyIndependent = true,
                EqualNormSquared = true,
                EqualScalarProduct = true,
                UnitNormSquared = true,
                Orthogonal = true
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFreeFrameSpecs CreateScaledBasisSpecs()
        {
            return new GeoFreeFrameSpecs()
            {
                LinearlyIndependent = true,
                EqualNormSquared = true,
                EqualScalarProduct = true,
                UnitNormSquared = null,
                Orthogonal = true
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFreeFrameSpecs CreateSimplexSpecs()
        {
            return new GeoFreeFrameSpecs()
            {
                LinearlyIndependent = true,
                EqualNormSquared = true,
                EqualScalarProduct = true,
                UnitNormSquared = null,
                Orthogonal = false
            };
        }


        /// <summary>
        /// The frame vectors are linearly independent
        /// </summary>
        public bool? LinearlyIndependent { get; init; }

        /// <summary>
        /// The frame vectors are orthogonal
        /// </summary>
        public bool? Orthogonal { get; init; }

        /// <summary>
        /// The frame vectors are orthogonal and have squared norm of +1 or -1
        /// </summary>
        public bool? Orthonormal 
            => Orthogonal.HasValue && UnitNormSquared.HasValue
                ? Orthogonal.Value && UnitNormSquared.Value 
                : null;

        /// <summary>
        /// The frame vectors have equal squared norm
        /// </summary>
        public bool? EqualNormSquared { get; init; }

        /// <summary>
        /// All frame vectors have squared norm of +1 or -1
        /// </summary>
        public bool? UnitNormSquared { get; init; }

        /// <summary>
        /// All pairs of frame vectors have the same scalar product
        /// </summary>
        public bool? EqualScalarProduct { get; init; }
    }
}