using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Structures;
using GeometricAlgebraFulcrumLib.Structures.Composers;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Storage.Factories
{
    public static class GaStorageMultivectorSparseFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenComposerSparse<T> CreateStorageSparseMultivectorComposer<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return new GaListEvenComposerSparse<T>(scalarProcessor);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> CreateStorageSparseMultivector<T>(this Dictionary<ulong, T> idScalarDictionary)
        {
            return GaStorageMultivectorSparse<T>.Create(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> CreateStorageSparseMultivector<T>(this IEnumerable<GaRecordKeyValue<T>> termsList)
        {
            var idScalarDictionary =
                termsList
                    .CreateDictionary()
                    .CreateEvenList();

            return GaStorageMultivectorSparse<T>.Create(idScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> CreateStorageSparseMultivector<T>(this IGaListEven<T> termsList)
        {
            return GaStorageMultivectorSparse<T>.Create(termsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> CreateStorageSparseMultivector<T>(this IEnumerable<GaRecordGradeKeyValue<T>> termsList)
        {
            var idScalarDictionary =
                termsList
                    .CreateDictionary(GaBasisBladeUtils.BasisBladeGradeIndexToId)
                    .CreateEvenList();

            return GaStorageMultivectorSparse<T>.Create(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> CreateStorageSparseMultivector<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            var idScalarDictionary =
                termsList
                    .ToDictionary(
                        t => t.BasisBlade.Id, 
                        t => t.Scalar
                    )
                    .CreateEvenList();

            return GaStorageMultivectorSparse<T>.Create(idScalarDictionary);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> SumToStorageSparseMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<GaRecordKeyValue<T>> termsList)
        {
            return scalarProcessor
                .CreateStorageSparseMultivectorComposer()
                .AddTerms(termsList)
                .RemoveZeroTerms()
                .CreateStorageSparseMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorSparse<T> SumToStorageSparseMultivector<T>(this IEnumerable<GaRecordKeyValue<T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            return scalarProcessor
                .CreateStorageSparseMultivectorComposer()
                .AddTerms(termsList)
                .RemoveZeroTerms()
                .CreateStorageSparseMultivector();
        }
    }
}