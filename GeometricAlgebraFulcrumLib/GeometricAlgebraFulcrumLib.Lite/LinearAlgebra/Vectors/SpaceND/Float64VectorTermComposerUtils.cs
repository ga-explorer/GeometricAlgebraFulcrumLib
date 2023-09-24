using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND
{
    public static class Float64VectorTermComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64VectorTerm CreateZeroLinTerm()
        {
            return new Float64VectorTerm(
                0.ToLinBasisVector(),
                0d
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64VectorTerm CreateZeroLinTerm(this int index)
        {
            return new Float64VectorTerm(
                index.ToLinBasisVector(),
                0d
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64VectorTerm CreatePositiveLinTerm(this int index)
        {
            return new Float64VectorTerm(
                index.ToLinBasisVector(),
                1d
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64VectorTerm CreateNegativeLinTerm(this int index)
        {
            return new Float64VectorTerm(
                index.ToLinBasisVector(),
                -1d
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64VectorTerm CreateLinTerm(this int index, double scalar)
        {
            return new Float64VectorTerm(
                index.ToLinBasisVector(),
                scalar
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64VectorTerm ToZeroTerm(this LinBasisVector basisVector)
        {
            return new Float64VectorTerm(
                basisVector,
                0d
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64VectorTerm ToPositiveTerm(this LinBasisVector basisVector)
        {
            return new Float64VectorTerm(
                basisVector,
                1d
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64VectorTerm ToNegativeTerm(this LinBasisVector basisVector)
        {
            return new Float64VectorTerm(
                basisVector,
                -1d
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64VectorTerm ToTerm(this LinBasisVector basisVector, double scalarValue)
        {
            return new Float64VectorTerm(basisVector, scalarValue);
        }
    }
}