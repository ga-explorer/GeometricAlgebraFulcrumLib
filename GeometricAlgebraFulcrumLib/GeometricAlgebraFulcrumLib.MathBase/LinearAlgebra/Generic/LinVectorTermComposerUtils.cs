using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic
{
    public static class LinVectorTermComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorTerm<T> CreateZeroLinTerm<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return new LinVectorTerm<T>(
                0.ToLinBasisVector(),
                scalarProcessor.CreateScalarZero()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorTerm<T> CreateZeroLinTerm<T>(this IScalarProcessor<T> scalarProcessor, int index)
        {
            return new LinVectorTerm<T>(
                index.ToLinBasisVector(),
                scalarProcessor.CreateScalarZero()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorTerm<T> CreatePositiveLinTerm<T>(this IScalarProcessor<T> scalarProcessor, int index)
        {
            return new LinVectorTerm<T>(
                index.ToLinBasisVector(),
                scalarProcessor.CreateScalarOne()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorTerm<T> CreateNegativeLinTerm<T>(this IScalarProcessor<T> scalarProcessor, int index)
        {
            return new LinVectorTerm<T>(
                index.ToLinBasisVector(),
                scalarProcessor.CreateScalarMinusOne()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorTerm<T> CreateLinTerm<T>(this IScalarProcessor<T> scalarProcessor, int index, T scalar)
        {
            return new LinVectorTerm<T>(
                index.ToLinBasisVector(),
                scalarProcessor.CreateScalar(scalar)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorTerm<T> ToLinTerm<T>(this int index, Scalar<T> scalar)
        {
            return new LinVectorTerm<T>(
                index.ToLinBasisVector(),
                scalar
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorTerm<T> ToZeroTerm<T>(this LinBasisVector basisVector, IScalarProcessor<T> scalarProcessor)
        {
            return new LinVectorTerm<T>(
                basisVector,
                scalarProcessor.CreateScalarZero()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorTerm<T> ToPositiveTerm<T>(this LinBasisVector basisVector, IScalarProcessor<T> scalarProcessor)
        {
            return new LinVectorTerm<T>(
                basisVector,
                scalarProcessor.CreateScalarOne()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorTerm<T> ToNegativeTerm<T>(this LinBasisVector basisVector, IScalarProcessor<T> scalarProcessor)
        {
            return new LinVectorTerm<T>(
                basisVector,
                scalarProcessor.CreateScalarMinusOne()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorTerm<T> ToTerm<T>(this LinBasisVector basisVector, Scalar<T> scalar)
        {
            return new LinVectorTerm<T>(basisVector, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorTerm<T> ToTerm<T>(this LinBasisVector basisVector, IScalarProcessor<T> scalarProcessor, T scalarValue)
        {
            return new LinVectorTerm<T>(basisVector, scalarProcessor, scalarValue);
        }
    }
}