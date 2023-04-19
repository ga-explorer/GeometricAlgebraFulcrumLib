using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Frames
{
    public sealed class XGaFloat64VectorFrameSpecs
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64VectorFrameSpecs CreateUndefinedSpecs()
        {
            return new XGaFloat64VectorFrameSpecs()
            {
                LinearlyIndependent = null,
                EqualNormSquared = null,
                EqualScalarProduct = null,
                UnitNormSquared = null,
                Orthogonal = null
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64VectorFrameSpecs CreateLinearlyIndependentSpecs()
        {
            return new XGaFloat64VectorFrameSpecs()
            {
                LinearlyIndependent = true,
                EqualNormSquared = null,
                EqualScalarProduct = null,
                UnitNormSquared = null,
                Orthogonal = null
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64VectorFrameSpecs CreateUnitBasisSpecs()
        {
            return new XGaFloat64VectorFrameSpecs()
            {
                LinearlyIndependent = true,
                EqualNormSquared = true,
                EqualScalarProduct = true,
                UnitNormSquared = true,
                Orthogonal = true
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64VectorFrameSpecs CreateScaledBasisSpecs()
        {
            return new XGaFloat64VectorFrameSpecs()
            {
                LinearlyIndependent = true,
                EqualNormSquared = true,
                EqualScalarProduct = true,
                UnitNormSquared = null,
                Orthogonal = true
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64VectorFrameSpecs CreateSimplexSpecs()
        {
            return new XGaFloat64VectorFrameSpecs()
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