using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Matrices;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class LaVectorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> CreateLaVector<T>(this ILaProcessor<T> processor, ILaVectorEvenStorage<T> storage)
        {
            return new LaVector<T>(
                processor, 
                storage
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> CreateLaVector<T>(this ILaVectorEvenStorage<T> storage, ILaProcessor<T> processor)
        {
            return new LaVector<T>(
                processor, 
                storage
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> CreateLaVector<T>(this ILaProcessor<T> processor, ILaVectorGradedStorage<T> storage)
        {
            return new LaVector<T>(
                processor, 
                storage.ToEvenStorage(GaBasisBladeUtils.BasisBladeGradeIndexToId)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> CreateLaVector<T>(this ILaVectorGradedStorage<T> storage, ILaProcessor<T> processor)
        {
            return new LaVector<T>(
                processor, 
                storage.ToEvenStorage(GaBasisBladeUtils.BasisBladeGradeIndexToId)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> CreateLaVectorZero<T>(this ILaProcessor<T> processor)
        {
            return new LaVector<T>(
                processor,
                LaVectorEmptyStorage<T>.ZeroStorage
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> CreateLaVector<T>(this ILaProcessor<T> processor, T value, int count)
        {
            return new LaVector<T>(
                processor,
                value.CreateLaVectorDenseEvenStorage(count)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> CreateLaVector<T>(this ILaProcessor<T> processor, T value)
        {
            return new LaVector<T>(
                processor,
                new LaVectorZeroIndexStorage<T>(value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> CreateLaVector<T>(this ILaProcessor<T> processor, ulong index, T value)
        {
            return new LaVector<T>(
                processor,
                value.CreateLaVectorSingleIndexEvenStorage(index)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> CreateLaVector<T>(this ILaProcessor<T> processor, KeyValuePair<ulong, T> kayValuePair)
        {
            return new LaVector<T>(
                processor,
                kayValuePair.CreateLaVectorSingleIndexEvenStorage()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> CreateLaVector<T>(this ILaProcessor<T> processor, T[] valuesArray)
        {
            return new LaVector<T>(
                processor,
                new LaVectorDenseStorage<T>(valuesArray)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> CreateLaVector<T>(this ILaProcessor<T> processor, IEnumerable<T> valuesList)
        {
            return new LaVector<T>(
                processor,
                new LaVectorDenseStorage<T>(valuesList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> CreateLaVector<T>(this ILaProcessor<T> processor, int count, Func<ulong, T> indexToScalarMapping)
        {
            return new LaVector<T>(
                processor,
                new LaVectorComputedStorage<T>(count, indexToScalarMapping)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> CreateLaVector<T>(this ILaProcessor<T> processor, ILaVectorDenseEvenStorage<T> evenDictionaryList, Func<ulong, ulong> indexMapping)
        {
            return new LaVector<T>(
                processor,
                new LaVectorMappedDenseStorage<T>(evenDictionaryList, indexMapping)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> CreateLaVector<T>(this ILaProcessor<T> processor, ILaVectorDenseEvenStorage<T> evenDictionaryList, Func<ulong, T, T> indexScalarMapping)
        {
            return new LaVector<T>(
                processor,
                new LaVectorMappedDenseStorage<T>(evenDictionaryList, indexScalarMapping)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> CreateLaVector<T>(this ILaProcessor<T> processor, ILaVectorDenseEvenStorage<T> evenDictionaryList, Func<ulong, ulong> indexMapping, Func<ulong, T, T> indexScalarMapping)
        {
            return new LaVector<T>(
                processor,
                new LaVectorMappedDenseStorage<T>(evenDictionaryList, indexMapping, indexScalarMapping)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> CreateLaVector<T>(this ILaProcessor<T> processor, IReadOnlyList<T> valuesList)
        {
            return new LaVector<T>(
                processor,
                valuesList.CreateLaVectorDenseStorage()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> CreateLaVector<T>(this ILaProcessor<T> processor, Dictionary<ulong, T> valuesDictionary)
        {
            return new LaVector<T>(
                processor,
                new LaVectorSparseStorage<T>(valuesDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> CreateLaVectorStorage<T>(this ILaProcessor<T> processor, Dictionary<ulong, T> valuesDictionary)
        {
            return new LaVector<T>(
                processor,
                valuesDictionary.CreateLaVectorStorage()
            );
        }
    }
}