//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
//using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
//using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
//using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
//using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
//using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;

//namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Vectors
//{
//    public static class LinVectorFactory
//    {
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVector<T>(this ILinearAlgebraProcessor<T> processor, ILinVectorStorage<T> storage)
//        {
//            return new LinVector<T>(
//                processor,
//                storage
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVector<T>(this ILinVectorStorage<T> storage, ILinearAlgebraProcessor<T> processor)
//        {
//            return new LinVector<T>(
//                processor,
//                storage
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVector<T>(this ILinearAlgebraProcessor<T> processor, ILinVectorGradedStorage<T> storage)
//        {
//            return new LinVector<T>(
//                processor,
//                storage.ToVectorStorage(BasisBladeUtils.BasisBladeGradeIndexToId)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVector<T>(this ILinVectorGradedStorage<T> storage, ILinearAlgebraProcessor<T> processor)
//        {
//            return new LinVector<T>(
//                processor,
//                storage.ToVectorStorage(BasisBladeUtils.BasisBladeGradeIndexToId)
//            );
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, IReadOnlyDictionary<ulong, int> indexScalarDictionary)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, int termsCount, Func<ulong, int> indexToScalarFunc)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, params int[] scalarArray)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, IEnumerable<int> scalarList)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
//            );
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, IReadOnlyDictionary<ulong, uint> indexScalarDictionary)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, int termsCount, Func<ulong, uint> indexToScalarFunc)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, params uint[] scalarArray)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, IEnumerable<uint> scalarList)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
//            );
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, IReadOnlyDictionary<ulong, long> indexScalarDictionary)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, int termsCount, Func<ulong, long> indexToScalarFunc)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, params long[] scalarArray)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, IEnumerable<long> scalarList)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
//            );
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, IReadOnlyDictionary<ulong, ulong> indexScalarDictionary)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, int termsCount, Func<ulong, ulong> indexToScalarFunc)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, params ulong[] scalarArray)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, IEnumerable<ulong> scalarList)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
//            );
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, IReadOnlyDictionary<ulong, float> indexScalarDictionary)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, int termsCount, Func<ulong, float> indexToScalarFunc)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, params float[] scalarArray)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, IEnumerable<float> scalarList)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
//            );
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, IReadOnlyDictionary<ulong, double> indexScalarDictionary)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, int termsCount, Func<ulong, double> indexToScalarFunc)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, params double[] scalarArray)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromNumbers<T>(this ILinearAlgebraProcessor<T> linearProcessor, IEnumerable<double> scalarList)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
//            );
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromText<T>(this ILinearAlgebraProcessor<T> linearProcessor, IReadOnlyDictionary<ulong, string> indexScalarDictionary)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromText(indexScalarDictionary)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromText<T>(this ILinearAlgebraProcessor<T> linearProcessor, int termsCount, Func<ulong, string> indexToScalarFunc)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromText(termsCount, indexToScalarFunc)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromText<T>(this ILinearAlgebraProcessor<T> linearProcessor, params string[] scalarArray)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromText(scalarArray)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromText<T>(this ILinearAlgebraProcessor<T> linearProcessor, IEnumerable<string> scalarList)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromText(scalarList.ToArray())
//            );
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromObjects<T>(this ILinearAlgebraProcessor<T> linearProcessor, IReadOnlyDictionary<ulong, object> indexScalarDictionary)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromObjects(indexScalarDictionary)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromObjects<T>(this ILinearAlgebraProcessor<T> linearProcessor, int termsCount, Func<ulong, object> indexToScalarFunc)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromObjects(termsCount, indexToScalarFunc)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromObjects<T>(this ILinearAlgebraProcessor<T> linearProcessor, params object[] scalarArray)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromObjects(scalarArray)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorFromObjects<T>(this ILinearAlgebraProcessor<T> linearProcessor, IEnumerable<object> scalarList)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorageFromObjects(scalarList.ToArray())
//            );
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVector<T>(this ILinearAlgebraProcessor<T> linearProcessor, Dictionary<ulong, T> indexScalarDictionary)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorage(indexScalarDictionary)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVector<T>(this ILinearAlgebraProcessor<T> linearProcessor, int termsCount, Func<ulong, T> indexToScalarFunc)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorage(termsCount, indexToScalarFunc)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVector<T>(this ILinearAlgebraProcessor<T> linearProcessor, params T[] scalarArray)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorage(scalarArray)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVector<T>(this ILinearAlgebraProcessor<T> linearProcessor, IEnumerable<T> scalarList)
//        {
//            return new LinVector<T>(
//                linearProcessor,
//                linearProcessor.CreateLinVectorStorage(scalarList.ToArray())
//            );
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorZero<T>(this ILinearAlgebraProcessor<T> processor)
//        {
//            return new LinVector<T>(
//                processor,
//                LinVectorEmptyStorage<T>.EmptyStorage
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorRepeatedScalar<T>(this ILinearAlgebraProcessor<T> processor, int count, T value)
//        {
//            return new LinVector<T>(
//                processor,
//                value.CreateLinVectorDenseStorage(count)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorTerm<T>(this ILinearAlgebraProcessor<T> processor, T value)
//        {
//            return new LinVector<T>(
//                processor,
//                new LinVectorSingleScalarDenseStorage<T>(value)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorTerm<T>(this ILinearAlgebraProcessor<T> processor, ulong index, T value)
//        {
//            return new LinVector<T>(
//                processor,
//                value.CreateLinVectorSingleScalarStorage(index)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVector<T>(this ILinearAlgebraProcessor<T> processor, IEnumerable<RGaKvIndexScalarRecord<T>> indexScalarRecords)
//        {
//            return new LinVector<T>(
//                processor,
//                indexScalarRecords.CreateLinVectorStorage()
//            );
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorBibolar<T>(this ILinearAlgebraProcessor<T> processor, IEnumerable<char> basisVectorSignatures)
//        {
//            return new LinVector<T>(
//                processor,
//                processor.CreateLinVectorStorageBibolar(basisVectorSignatures)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorBibolar<T>(this ILinearAlgebraProcessor<T> processor, IEnumerable<int> basisVectorSignatures)
//        {
//            return new LinVector<T>(
//                processor,
//                processor.CreateLinVectorStorageBibolar(basisVectorSignatures)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static LinVector<T> CreateLinVectorBibolar<T>(this ILinearAlgebraProcessor<T> processor, params int[] basisVectorSignatures)
//        {
//            return new LinVector<T>(
//                processor,
//                processor.CreateLinVectorStorageBibolar(basisVectorSignatures)
//            );
//        }
//    }
//}