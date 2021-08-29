using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class GaMultivectorSparseStorageFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorSparseEvenStorageComposer<T> CreateStorageSparseMultivectorComposer<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return new LaVectorSparseEvenStorageComposer<T>(scalarProcessor);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivectorSparseStorage<T> CreateStorageSparseMultivector<T>(this Dictionary<ulong, T> idScalarDictionary)
        {
            return GaMultivectorSparseStorage<T>.Create(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivectorSparseStorage<T> CreateStorageSparseMultivector<T>(this IEnumerable<IndexScalarRecord<T>> termsList)
        {
            var idScalarDictionary =
                termsList
                    .CreateDictionary()
                    .CreateLaVectorStorage();

            return GaMultivectorSparseStorage<T>.Create(idScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivectorSparseStorage<T> CreateStorageSparseMultivector<T>(this ILaVectorEvenStorage<T> termsList)
        {
            return GaMultivectorSparseStorage<T>.Create(termsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivectorSparseStorage<T> CreateStorageSparseMultivector<T>(this IEnumerable<GradeIndexScalarRecord<T>> termsList)
        {
            var idScalarDictionary =
                termsList
                    .CreateDictionary(GaBasisBladeUtils.BasisBladeGradeIndexToId)
                    .CreateLaVectorStorage();

            return GaMultivectorSparseStorage<T>.Create(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivectorSparseStorage<T> CreateStorageSparseMultivector<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            var idScalarDictionary =
                termsList
                    .ToDictionary(
                        t => t.BasisBlade.Id, 
                        t => t.Scalar
                    )
                    .CreateLaVectorStorage();

            return GaMultivectorSparseStorage<T>.Create(idScalarDictionary);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivectorSparseStorage<T> SumToStorageSparseMultivector<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<IndexScalarRecord<T>> termsList)
        {
            return scalarProcessor
                .CreateStorageSparseMultivectorComposer()
                .AddTerms(termsList)
                .RemoveZeroTerms()
                .CreateGaMultivectorSparseStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorSparseStorage<T> SumToStorageSparseMultivector<T>(this IEnumerable<IndexScalarRecord<T>> termsList, IScalarProcessor<T> scalarProcessor)
        {
            return scalarProcessor
                .CreateStorageSparseMultivectorComposer()
                .AddTerms(termsList)
                .RemoveZeroTerms()
                .CreateGaMultivectorSparseStorage();
        }
    }
}