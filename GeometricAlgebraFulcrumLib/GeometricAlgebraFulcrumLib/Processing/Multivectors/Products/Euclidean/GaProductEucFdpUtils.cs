using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean
{
    public static class GaProductEucFdpUtils
    {
        public static IGaStorageKVector<T> EFdp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv1, IGaStorageKVector<T> mv2)
        {
            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;
            if (grade1 == grade2)
                return scalarProcessor.CreateStorageZeroScalar();

            var grade = (uint) Math.Abs(grade2 - grade1);

            var composer = 
                scalarProcessor.CreateStorageKVectorComposer();

            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            var indexScalarPairs2 = 
                mv2.IndexScalarList;

            foreach (var (index1, scalar1) in indexScalarPairs1.GetKeyValueRecords())
            {
                var id1 = index1.BasisBladeIndexToId(grade1);

                foreach (var (index2, scalar2) in indexScalarPairs2.GetKeyValueRecords())
                {
                    var id2 = index2.BasisBladeIndexToId(grade2);

                    var signature = 
                        GaBasisBladeProductUtils.EFdpSignature(id1, id2);

                    if (signature == 0) 
                        continue;

                    var index = (id1 ^ id2).BasisBladeIdToIndex();
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(index, scalar);
                    else
                        composer.SubtractTerm(index, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateStorageKVector(grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> EFdp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            return scalarProcessor.BilinearProduct(mv1, mv2, GaBasisBladeProductUtils.EFdpSignature);
        }
    }
}