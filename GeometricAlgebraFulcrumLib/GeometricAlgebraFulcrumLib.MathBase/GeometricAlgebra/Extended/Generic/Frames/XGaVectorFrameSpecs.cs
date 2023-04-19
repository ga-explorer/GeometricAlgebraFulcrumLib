using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Frames
{
    public sealed class XGaVectorFrameSpecs
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaVectorFrameSpecs CreateUndefinedSpecs()
        {
            return new XGaVectorFrameSpecs()
            {
                LinearlyIndependent = null,
                EqualNormSquared = null,
                EqualScalarProduct = null,
                UnitNormSquared = null,
                Orthogonal = null
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaVectorFrameSpecs CreateLinearlyIndependentSpecs()
        {
            return new XGaVectorFrameSpecs()
            {
                LinearlyIndependent = true,
                EqualNormSquared = null,
                EqualScalarProduct = null,
                UnitNormSquared = null,
                Orthogonal = null
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaVectorFrameSpecs CreateUnitBasisSpecs()
        {
            return new XGaVectorFrameSpecs()
            {
                LinearlyIndependent = true,
                EqualNormSquared = true,
                EqualScalarProduct = true,
                UnitNormSquared = true,
                Orthogonal = true
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaVectorFrameSpecs CreateScaledBasisSpecs()
        {
            return new XGaVectorFrameSpecs()
            {
                LinearlyIndependent = true,
                EqualNormSquared = true,
                EqualScalarProduct = true,
                UnitNormSquared = null,
                Orthogonal = true
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaVectorFrameSpecs CreateSimplexSpecs()
        {
            return new XGaVectorFrameSpecs()
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