using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean
{
    public static class GaProductEucFdpUtils
    {
        public static IGaKVectorStorage<T> EFdp<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv1, IGaKVectorStorage<T> mv2)
        {
            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;
            if (grade1 == grade2)
                return scalarProcessor.CreateStorageZeroScalar();

            var grade = (uint) Math.Abs(grade2 - grade1);

            var composer = 
                scalarProcessor.CreateKVectorStorageComposer();

            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            var indexScalarPairs2 = 
                mv2.IndexScalarList;

            foreach (var (index1, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var id1 = index1.BasisBladeIndexToId(grade1);

                foreach (var (index2, scalar2) in indexScalarPairs2.GetIndexScalarRecords())
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

            return composer.CreateGaKVectorStorage(grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> EFdp<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return scalarProcessor.BilinearProduct(mv1, mv2, GaBasisBladeProductUtils.EFdpSignature);
        }
    }
}