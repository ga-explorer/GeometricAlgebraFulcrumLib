using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorStorageHipEucUtils
    {
        public static KVectorStorage<T> EHip<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
        {
            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;
            if (grade1 == 0 || grade2 == 0)
                return KVectorStorage<T>.ZeroScalar;

            if (grade1 == grade2)
                return scalarProcessor.CreateKVectorScalarStorage(
                    scalarProcessor.ESp(mv1, mv2)
                );

            var grade = (uint) Math.Abs(grade2 - grade1);

            var composer = 
                scalarProcessor.CreateVectorStorageComposer();

            var indexScalarPairs1 = 
                mv1.GetLinVectorIndexScalarStorage();

            var indexScalarPairs2 = 
                mv2.GetLinVectorIndexScalarStorage();

            foreach (var (index1, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var id1 = index1.BasisBladeIndexToId(grade1);

                foreach (var (index2, scalar2) in indexScalarPairs2.GetIndexScalarRecords())
                {
                    var id2 = index2.BasisBladeIndexToId(grade2);

                    //This is correct because we eliminated the scalar case earlier
                    var signature = 
                        BasisBladeProductUtils.EFdpSignature(id1, id2); 

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

            return composer.CreateKVectorStorage(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> EHip<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return scalarProcessor.BilinearProduct(mv1, mv2, BasisBladeProductUtils.EHipSignature);
        }
    }
}