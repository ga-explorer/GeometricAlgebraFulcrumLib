using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Composers;
using GeometricAlgebraFulcrumLib.Storage.Terms;
using GeometricAlgebraFulcrumLib.Structures.Even;

namespace GeometricAlgebraFulcrumLib.Storage.Factories
{
    public static class GaStorageMultivectorSparseFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageComposerMultivectorSparse<T> CreateStorageComposerSparseMultivector<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return new GaStorageComposerMultivectorSparse<T>(
                scalarProcessor
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> CreateStorageSparseMultivector<T>(this Dictionary<ulong, T> idScalarDictionary)
        {
            return GaStorageMultivectorSparse<T>.Create(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> CreateStorageSparseMultivector<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            var idScalarDictionary =
                termsList.CopyToDictionary().CreateEvenDictionary();

            return GaStorageMultivectorSparse<T>.Create(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> CreateStorageSparseMultivector<T>(this IEnumerable<Tuple<ulong, T>> termsList)
        {
            var idScalarDictionary =
                termsList
                    .ToDictionary(
                        t => t.Item1, 
                        t => t.Item2
                    )
                    .CreateEvenDictionary();

            return GaStorageMultivectorSparse<T>.Create(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> CreateStorageSparseMultivector<T>(this IEnumerable<Tuple<uint, ulong, T>> termsList)
        {
            var idScalarDictionary =
                termsList
                    .ToDictionary(
                        t => GaBasisUtils.BasisBladeId(t.Item1, t.Item2), 
                        t => t.Item3
                    )
                    .CreateEvenDictionary();

            return GaStorageMultivectorSparse<T>.Create(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> CreateStorageSparseMultivector<T>(this IEnumerable<GaTerm<T>> termsList)
        {
            var idScalarDictionary =
                termsList
                    .ToDictionary(
                        t => t.BasisBlade.Id, 
                        t => t.Scalar
                    )
                    .CreateEvenDictionary();

            return GaStorageMultivectorSparse<T>.Create(idScalarDictionary);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> SumToStorageSparseMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            var storage = 
                new GaStorageComposerMultivectorSparse<T>(scalarProcessor);

            return storage
                .AddTerms(termsList)
                .RemoveZeroTerms()
                .GetSparseMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> SumToStorageSparseMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            var storage = 
                new GaStorageComposerMultivectorSparse<T>(scalarProcessor);

            return storage
                .AddTerms(termsList)
                .RemoveZeroTerms()
                .GetSparseMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> SumToStorageSparseMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<uint, ulong, T>> termsList)
        {
            var storage = 
                new GaStorageComposerMultivectorSparse<T>(scalarProcessor);

            return storage
                .AddTerms(termsList)
                .RemoveZeroTerms()
                .GetSparseMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> SumToStorageSparseMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<GaTerm<T>> termsList)
        {
            var storage = 
                new GaStorageComposerMultivectorSparse<T>(scalarProcessor);

            return storage
                .AddTerms(termsList)
                .RemoveZeroTerms()
                .GetSparseMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorSparse<T> SumToStorageSparseMultivector<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = 
                new GaStorageComposerMultivectorSparse<T>(scalarProcessor);
            
            return storage
                .AddTerms(termsList)
                .RemoveZeroTerms()
                .GetSparseMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorSparse<T> SumToStorageSparseMultivector<T>(this IEnumerable<Tuple<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = 
                new GaStorageComposerMultivectorSparse<T>(scalarProcessor);
            
            return storage
                .AddTerms(termsList)
                .RemoveZeroTerms()
                .GetSparseMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorSparse<T> SumToStorageSparseMultivector<T>(this IEnumerable<Tuple<uint, ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = 
                new GaStorageComposerMultivectorSparse<T>(scalarProcessor);
            
            return storage
                .AddTerms(termsList)
                .RemoveZeroTerms()
                .GetSparseMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorSparse<T> SumToStorageSparseMultivector<T>(this IEnumerable<GaTerm<T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = 
                new GaStorageComposerMultivectorSparse<T>(scalarProcessor);
            
            return storage
                .AddTerms(termsList)
                .RemoveZeroTerms()
                .GetSparseMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> CreateStorageTreeMultivector<T>(this IReadOnlyDictionary<ulong, T> idScalarDictionary, int treeDepth)
        {
            return GaStorageMultivectorSparse<T>.Create(
                idScalarDictionary.CreateEvenDictionaryTree(treeDepth)
            );
        }

        public static IGaStorageMultivectorSparse<T> CreateStorageTreeMultivector<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaStorageComposerMultivectorSparse<T>(scalarProcessor);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetTreeMultivector();
        }

        public static IGaStorageMultivectorSparse<T> CreateStorageTreeMultivector<T>(this IEnumerable<Tuple<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaStorageComposerMultivectorSparse<T>(scalarProcessor);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetTreeMultivector();
        }

        public static IGaStorageMultivectorSparse<T> CreateStorageTreeMultivector<T>(this IEnumerable<Tuple<uint, ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaStorageComposerMultivectorSparse<T>(scalarProcessor);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetTreeMultivector();
        }

        public static IGaStorageMultivectorSparse<T> CreateStorageTreeMultivector<T>(this IEnumerable<GaTerm<T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaStorageComposerMultivectorSparse<T>(scalarProcessor);

            storage.SetTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetTreeMultivector();
        }


        public static IGaStorageMultivectorSparse<T> SumToStorageTreeMultivector<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaStorageComposerMultivectorSparse<T>(scalarProcessor);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetTreeMultivector();
        }

        public static IGaStorageMultivectorSparse<T> SumToStorageTreeMultivector<T>(this IEnumerable<Tuple<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaStorageComposerMultivectorSparse<T>(scalarProcessor);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetTreeMultivector();
        }

        public static IGaStorageMultivectorSparse<T> SumToStorageTreeMultivector<T>(this IEnumerable<Tuple<uint, ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaStorageComposerMultivectorSparse<T>(scalarProcessor);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetTreeMultivector();
        }

        public static IGaStorageMultivectorSparse<T> SumToStorageTreeMultivector<T>(this IEnumerable<GaTerm<T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaStorageComposerMultivectorSparse<T>(scalarProcessor);

            storage.AddTerms(termsList);

            storage.RemoveZeroTerms();

            return storage.GetTreeMultivector();
        }
    }
}