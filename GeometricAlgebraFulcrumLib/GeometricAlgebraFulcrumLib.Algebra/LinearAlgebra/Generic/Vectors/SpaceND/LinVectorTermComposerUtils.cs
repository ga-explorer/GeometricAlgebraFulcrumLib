using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

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

}