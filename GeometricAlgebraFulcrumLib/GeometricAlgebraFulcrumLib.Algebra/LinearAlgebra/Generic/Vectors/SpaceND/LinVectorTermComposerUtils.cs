using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;

public static class LinVectorTermComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorTerm<T> CreateZeroLinTerm<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return new LinVectorTerm<T>(
            0.ToLinBasisVector(),
            scalarProcessor.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorTerm<T> CreateZeroLinTerm<T>(this IScalarProcessor<T> scalarProcessor, int index)
    {
        return new LinVectorTerm<T>(
            index.ToLinBasisVector(),
            scalarProcessor.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorTerm<T> CreatePositiveLinTerm<T>(this IScalarProcessor<T> scalarProcessor, int index)
    {
        return new LinVectorTerm<T>(
            index.ToLinBasisVector(),
            scalarProcessor.One
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorTerm<T> CreateNegativeLinTerm<T>(this IScalarProcessor<T> scalarProcessor, int index)
    {
        return new LinVectorTerm<T>(
            index.ToLinBasisVector(),
            scalarProcessor.MinusOne
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorTerm<T> CreateLinTerm<T>(this IScalarProcessor<T> scalarProcessor, int index, T scalar)
    {
        return new LinVectorTerm<T>(
            index.ToLinBasisVector(),
            scalarProcessor.ScalarFromValue(scalar)
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
            scalarProcessor.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorTerm<T> ToPositiveTerm<T>(this LinBasisVector basisVector, IScalarProcessor<T> scalarProcessor)
    {
        return new LinVectorTerm<T>(
            basisVector,
            scalarProcessor.One
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorTerm<T> ToNegativeTerm<T>(this LinBasisVector basisVector, IScalarProcessor<T> scalarProcessor)
    {
        return new LinVectorTerm<T>(
            basisVector,
            scalarProcessor.MinusOne
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