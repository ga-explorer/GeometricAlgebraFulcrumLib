﻿using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Spaces.Conformal;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Spaces;

public static class XGaSpaceComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaEuclideanSpace<T> CreateSpace<T>(this XGaEuclideanProcessor<T> processor, int vSpaceDimensions)
    {
        return new XGaEuclideanSpace<T>(processor, vSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveSpace<T> CreateSpace<T>(this XGaProjectiveProcessor<T> processor, int vSpaceDimensions)
    {
        return new XGaProjectiveSpace<T>(processor, vSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalSpace<T> CreateSpace<T>(this XGaConformalProcessor<T> processor, int vSpaceDimensions)
    {
        return new XGaConformalSpace<T>(processor, vSpaceDimensions);
    }
}