﻿using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Processing.Products.Euclidean
{
    public static class GaProductEucFdpUtils
    {
        public static IGasKVector<T> EFdp<T>(this IGasKVector<T> mv1, IGasKVector<T> mv2)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;
            if (grade1 == grade2)
                return scalarProcessor.CreateZeroScalar();

            var grade = (uint) Math.Abs(grade2 - grade1);

            var composer = 
                new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            var indexScalarPairs1 = 
                mv1.GetIndexScalarPairs();

            var indexScalarPairs2 = 
                mv2.GetIndexScalarDictionary();

            foreach (var (index1, scalar1) in indexScalarPairs1)
            {
                var id1 = GaBasisUtils.BasisBladeId(grade1, index1);

                foreach (var (index2, scalar2) in indexScalarPairs2)
                {
                    var id2 = GaBasisUtils.BasisBladeId(grade2, index2);

                    var signature = 
                        GaBasisUtils.EFdpSignature(id1, id2);

                    if (signature == 0) 
                        continue;

                    var index = (id1 ^ id2).BasisBladeIndex();
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(index, scalar);
                    else
                        composer.SubtractTerm(index, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.GetKVectorStorage();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> EFdp<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return mv1.BilinearProduct(mv2, GaBasisUtils.EFdpSignature);
        }
    }
}