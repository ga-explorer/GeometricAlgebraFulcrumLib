using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class GaVectorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGaVector<T>(this IGaProcessor<T> processor, IGaVectorStorage<T> storage)
        {
            return new GaVector<T>(processor, storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGaVector<T>(this IGaVectorStorage<T> storage, IGaProcessor<T> processor)
        {
            return new GaVector<T>(processor, storage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGaVector<T>(this IGaProcessor<T> processor)
        {
            return GaVectorStorage<T>.ZeroVector.CreateGaVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGaVector<T>(this IGaProcessor<T> processor, int index, T scalar)
        {
            return GaVectorStorage<T>.Create((ulong) index, scalar).CreateGaVector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGaVector<T>(this IGaProcessor<T> processor, ulong index, T scalar)
        {
            return GaVectorStorage<T>.Create(index, scalar).CreateGaVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGaVector<T>(this IGaProcessor<T> processor, IndexScalarRecord<T> indexScalarPair)
        {
            return GaVectorStorage<T>.Create(indexScalarPair.Index, indexScalarPair.Scalar).CreateGaVector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGaVector<T>(this IGaProcessor<T> processor, int index)
        {
            return GaVectorStorage<T>.Create((ulong) index, processor.ScalarOne).CreateGaVector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGaVector<T>(this IGaProcessor<T> processor, ulong index)
        {
            return GaVectorStorage<T>.Create(index, processor.ScalarOne).CreateGaVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGaVectorOnes<T>(this IGaProcessor<T> processor, int termsCount)
        {
            return processor.CreateStorageVectorOnes(termsCount).CreateGaVector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGaVectorUnitOnes<T>(this IGaProcessor<T> processor, int termsCount)
        {
            return processor.CreateStorageVectorUnitOnes(termsCount).CreateGaVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGaVector<T>(this IGaProcessor<T> processor, uint termsCount, Func<ulong, T> indexScalarFunc)
        {
            return processor.CreateGaVectorStorage(termsCount, indexScalarFunc).CreateGaVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGaVector<T>(this IGaProcessor<T> processor, Dictionary<ulong, T> indexScalarDictionary)
        {
            return GaVectorStorage<T>.Create(indexScalarDictionary).CreateGaVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGaVector<T>(this IGaProcessor<T> processor, params T[] scalarArray)
        {
            return GaVectorStorage<T>.Create(scalarArray).CreateGaVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGaVector<T>(this IGaProcessor<T> processor, params string[] scalarArray)
        {
            return GaVectorStorage<T>.Create(scalarArray.Select(processor.GetScalarFromText)).CreateGaVector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGaVector<T>(this IGaProcessor<T> processor, int count, Func<int, T> indexToScalarFunc)
        {
            return processor.CreateGaVectorStorage(count, indexToScalarFunc).CreateGaVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGaVector<T>(this IGaProcessor<T> processor, int count, Func<int, string> indexToScalarFunc)
        {
            return processor.CreateGaVectorStorage(count, indexToScalarFunc).CreateGaVector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGaVector<T>(this IGaProcessor<T> processor, IReadOnlyList<T> scalarList)
        {
            return GaVectorStorage<T>.Create(scalarList).CreateGaVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGaVector<T>(this IGaProcessor<T> processor, IEnumerable<T> scalarList)
        {
            return GaVectorStorage<T>.Create(scalarList).CreateGaVector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGaVector<T>(this IGaProcessor<T> processor, IEnumerable<IndexScalarRecord<T>> termsList)
        {
            return processor.CreateGaVectorStorage(termsList).CreateGaVector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGaVector<T>(this IGaProcessor<T> processor, IEnumerable<GaBasisTerm<T>> termsList)
        {
            return processor.CreateGaVectorStorage(termsList).CreateGaVector(processor);
        }
    }
}