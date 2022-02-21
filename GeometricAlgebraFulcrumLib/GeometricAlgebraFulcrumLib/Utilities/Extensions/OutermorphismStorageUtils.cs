using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    internal static class OutermorphismStorageUtils
    {
        public static VectorStorage<T> MapVector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, OutermorphismStorage<T> om, VectorStorage<T> vector)
        {
            var composer = scalarProcessor.CreateVectorStorageComposer();

            foreach (var (index, scalar) in vector.GetIndexScalarRecords())
            {
                var mappedKVector = om.GetMappedBasisVector(index);

                if (mappedKVector.IsEmpty())
                    continue;

                composer.AddScaledTerms(
                    scalar, 
                    mappedKVector.GetLinVectorIndexScalarStorage().GetIndexScalarRecords()
                );
            }

            composer.RemoveZeroTerms();

            return composer.CreateVectorStorage();
        }

        public static BivectorStorage<T> MapBivector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, OutermorphismStorage<T> om, BivectorStorage<T> bivector)
        {
            var composer = scalarProcessor.CreateVectorStorageComposer();

            foreach (var (index, scalar) in bivector.GetIndexScalarRecords())
            {
                var mappedKVector = om.GetMappedBasisBivector(index);

                if (mappedKVector.IsEmpty())
                    continue;

                composer.AddScaledTerms(
                    scalar, 
                    mappedKVector.GetLinVectorIndexScalarStorage().GetIndexScalarRecords()
                );
            }

            composer.RemoveZeroTerms();

            return composer.CreateBivectorStorage();
        }
        
        public static KVectorStorage<T> MapKVector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, OutermorphismStorage<T> om, KVectorStorage<T> kVector)
        {
            var grade = kVector.Grade;
            var composer = scalarProcessor.CreateVectorStorageComposer();

            foreach (var (index, scalar) in kVector.GetIndexScalarRecords())
            {
                var mappedKVector = om.GetMappedBasisBlade(grade, index);

                if (mappedKVector.IsEmpty())
                    continue;

                composer.AddScaledTerms(
                    scalar, 
                    mappedKVector.GetLinVectorIndexScalarStorage().GetIndexScalarRecords()
                );
            }

            composer.RemoveZeroTerms();

            return composer.CreateKVectorStorage(grade);
        }
        
        public static IMultivectorGradedStorage<T> MapGradedMultivector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, OutermorphismStorage<T> om, IMultivectorGradedStorage<T> multivector)
        {
            var composer = scalarProcessor.CreateMultivectorGradedStorageComposer();

            foreach (var (grade, kVector) in multivector.GetLinVectorGradedStorage().GetGradeStorageRecords())
            {
                var kVectorComposer = composer[grade];

                foreach (var (index, scalar) in kVector.GetIndexScalarRecords())
                {
                    var mappedKVector = om.GetMappedBasisBlade(grade, index);

                    if (mappedKVector.IsEmpty())
                        continue;

                    kVectorComposer.AddScaledTerms(
                        scalar, 
                        mappedKVector.GetLinVectorIndexScalarStorage().GetIndexScalarRecords()
                    );
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateMultivectorGradedStorage();
        }

        public static MultivectorStorage<T> MapSparseMultivector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, OutermorphismStorage<T> om, MultivectorStorage<T> multivector)
        {
            var composer = scalarProcessor.CreateVectorStorageComposer();

            foreach (var (id, scalar) in multivector.GetIdScalarRecords())
            {
                var mappedKVector = om.GetMappedBasisBlade(id);

                if (mappedKVector.IsEmpty())
                    continue;

                composer.AddScaledTerms(
                    scalar, 
                    mappedKVector.GetLinVectorIndexScalarStorage().GetIndexScalarRecords()
                );
            }

            composer.RemoveZeroTerms();

            return composer.CreateMultivectorStorageSparse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> MapMultivector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, OutermorphismStorage<T> om, IMultivectorStorage<T> multivector)
        {
            return multivector switch
            {
                VectorStorage<T> mv => scalarProcessor.MapVector(om, mv),
                BivectorStorage<T> mv => scalarProcessor.MapBivector(om, mv),
                KVectorStorage<T> mv => scalarProcessor.MapKVector(om, mv),
                IMultivectorGradedStorage<T> mv => scalarProcessor.MapGradedMultivector(om, mv),
                MultivectorStorage<T> mv => scalarProcessor.MapSparseMultivector(om, mv),
                _ => throw new InvalidOperationException()
            };
        }
    }
}