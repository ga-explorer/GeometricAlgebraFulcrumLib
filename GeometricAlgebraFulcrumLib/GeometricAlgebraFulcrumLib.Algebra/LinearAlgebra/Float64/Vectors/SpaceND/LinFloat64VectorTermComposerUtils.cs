﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

public static class LinFloat64VectorTermComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorTerm CreateZeroLinTerm()
    {
        return new LinFloat64VectorTerm(
            0.ToLinBasisVector(),
            0d
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorTerm CreateZeroLinTerm(this int index)
    {
        return new LinFloat64VectorTerm(
            index.ToLinBasisVector(),
            0d
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorTerm CreatePositiveLinTerm(this int index)
    {
        return new LinFloat64VectorTerm(
            index.ToLinBasisVector(),
            1d
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorTerm CreateNegativeLinTerm(this int index)
    {
        return new LinFloat64VectorTerm(
            index.ToLinBasisVector(),
            -1d
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorTerm CreateLinTerm(this int index, double scalar)
    {
        return new LinFloat64VectorTerm(
            index.ToLinBasisVector(),
            scalar
        );
    }


}