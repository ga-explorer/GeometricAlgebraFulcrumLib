using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class GaVectorStorageFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> CopyToGaVectorStorage<T>(this IReadOnlyDictionary<ulong, T> indexScalarDictionary)
        {
            var evenDictionary = indexScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );

            return GaVectorStorage<T>.Create(evenDictionary);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> SumToGaVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<IndexScalarRecord<T>> termsList)
        {
            return scalarProcessor.CreateKVectorStorageComposer()
                .AddTerms(termsList)
                .RemoveZeroTerms()
                .CreateGaVectorStorage();
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>()
        {
            return GaVectorStorage<T>.ZeroVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return GaVectorStorage<T>.ZeroVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(int index, T scalar)
        {
            return GaVectorStorage<T>.Create((ulong) index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(ulong index, T scalar)
        {
            return GaVectorStorage<T>.Create(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, int index, T scalar)
        {
            return GaVectorStorage<T>.Create((ulong) index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, ulong index, T scalar)
        {
            return GaVectorStorage<T>.Create(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this IndexScalarRecord<T> indexScalarPair)
        {
            return GaVectorStorage<T>.Create(indexScalarPair.Index, indexScalarPair.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IndexScalarRecord<T> indexScalarPair)
        {
            return GaVectorStorage<T>.Create(indexScalarPair.Index, indexScalarPair.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, int index)
        {
            return GaVectorStorage<T>.Create((ulong) index, scalarProcessor.ScalarOne);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, ulong index)
        {
            return GaVectorStorage<T>.Create(index, scalarProcessor.ScalarOne);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateStorageVectorOnes<T>(this IScalarProcessor<T> scalarProcessor, int termsCount)
        {
            return GaVectorStorage<T>.Create(
                scalarProcessor.ScalarOne
                    .CreateLaVectorRepeatedScalarStorage(termsCount)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateStorageVectorUnitOnes<T>(this IScalarProcessor<T> scalarProcessor, int termsCount)
        {
            var length = scalarProcessor.Sqrt(termsCount);

            return GaVectorStorage<T>.Create(
                scalarProcessor
                    .Divide(scalarProcessor.ScalarOne, length)
                    .CreateLaVectorRepeatedScalarStorage(termsCount)
            );
        }
        
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, uint termsCount, Func<ulong, T> indexScalarFunc)
        {
            if (termsCount == 0)
                return GaVectorStorage<T>.ZeroVector;

            if (termsCount == 1) 
                return GaVectorStorage<T>.Create(0, indexScalarFunc(0));

            var indexScalarDictionary =
                ((ulong) termsCount).RangeToDictionary(indexScalarFunc);

            return GaVectorStorage<T>.Create(indexScalarDictionary);

        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this Dictionary<ulong, T> indexScalarDictionary)
        {
            return GaVectorStorage<T>.Create(indexScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, Dictionary<ulong, T> indexScalarDictionary)
        {
            return GaVectorStorage<T>.Create(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(params T[] scalarArray)
        {
            return GaVectorStorage<T>.Create(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this IReadOnlyList<T> scalarList)
        {
            return GaVectorStorage<T>.Create(scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this IEnumerable<T> scalarList)
        {
            return GaVectorStorage<T>.Create(scalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this IEnumerable<IndexScalarRecord<T>> termsList)
        {
            return GaVectorStorage<T>.Create(
                termsList.CreateDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this ILaVectorEvenStorage<T> termsList)
        {
            return GaVectorStorage<T>.Create(termsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            return GaVectorStorage<T>.Create(
                termsList.ToDictionary(
                    t => t.BasisBlade.Index, 
                    t => t.Scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarArray)
        {
            return GaVectorStorage<T>.Create(scalarArray);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, params string[] scalarArray)
        {
            return GaVectorStorage<T>.Create(scalarArray.Select(scalarProcessor.GetScalarFromText));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, int count, Func<int, T> indexToScalarFunc)
        {
            return GaVectorStorage<T>.Create(
                count.GetRange().Select(indexToScalarFunc)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, int count, Func<int, string> indexToScalarFunc)
        {
            return GaVectorStorage<T>.Create(
                count.GetRange().Select(index => scalarProcessor.GetScalarFromText(indexToScalarFunc(index)))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> scalarList)
        {
            return GaVectorStorage<T>.Create(scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarList)
        {
            return GaVectorStorage<T>.Create(scalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<IndexScalarRecord<T>> termsList)
        {
            return GaVectorStorage<T>.Create(
                termsList.CreateDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> CreateGaVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<GaBasisTerm<T>> termsList)
        {
            return GaVectorStorage<T>.Create(
                termsList.ToDictionary(
                    t => t.BasisBlade.Index, 
                    t => t.Scalar
                )
            );
        }
    }
}