﻿using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Algebra.Outermorphisms
{
    public interface IGaOutermorphism<T> 
        : IGaSpace
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }

        /// <summary>
        /// det(T) = T[I] lcp BladeInverse(I), where I is the space pseudo-scalar
        /// </summary>
        /// <returns></returns>
        IGasKVector<T> MappedPseudoScalar { get; }

        IReadOnlyList<IGasVector<T>> GetMappedBasisVectors();

        IGasVector<T> MapBasisVector(ulong index);

        IGasBivector<T> MapBasisBivector(ulong index1, ulong index2);

        IGasKVector<T> MapBasisBlade(ulong id);

        IGasKVector<T> MapBasisBlade(uint grade, ulong index);

        IGasVector<T> MapVector(IGasVector<T> vector);

        IGasBivector<T> MapBivector(IGasBivector<T> bivector);

        IGasKVector<T> MapKVector(IGasKVector<T> kVector);

        IGasMultivector<T> MapMultivector(IGasMultivector<T> mv);
    }
}