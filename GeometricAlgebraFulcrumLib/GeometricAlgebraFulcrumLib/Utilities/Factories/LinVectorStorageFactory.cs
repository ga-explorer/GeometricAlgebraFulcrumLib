using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Collections;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class LinVectorStorageFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, int> indexScalarDictionary)
        {
            return indexScalarDictionary.Count switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => scalarProcessor.CreateLinVectorStorageFromNumber(indexScalarDictionary.First()),
                _ => new LinVectorSparseStorage<T>(indexScalarDictionary.ToDictionary(scalarProcessor.GetScalarFromNumber))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, int> indexToScalarFunc)
        {
            return termsCount switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(scalarProcessor.GetScalarFromNumber(indexToScalarFunc(0))),
                _ => new LinVectorArrayStorage<T>(((ulong) termsCount).RangeToArray(index => scalarProcessor.GetScalarFromNumber(indexToScalarFunc(index))))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params int[] scalarArray)
        {
            var termsCount = scalarArray.Length;

            return termsCount switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(scalarProcessor.GetScalarFromNumber(scalarArray[0])),
                _ => new LinVectorArrayStorage<T>(scalarProcessor.GetScalarsFromNumbers(scalarArray))
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<int> scalarList)
        {
            return scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray());
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, uint> indexScalarDictionary)
        {
            return indexScalarDictionary.Count switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => scalarProcessor.CreateLinVectorStorageFromNumber(indexScalarDictionary.First()),
                _ => new LinVectorSparseStorage<T>(indexScalarDictionary.ToDictionary(scalarProcessor.GetScalarFromNumber))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, uint> indexToScalarFunc)
        {
            return termsCount switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(scalarProcessor.GetScalarFromNumber(indexToScalarFunc(0))),
                _ => new LinVectorArrayStorage<T>(((ulong) termsCount).RangeToArray(index => scalarProcessor.GetScalarFromNumber(indexToScalarFunc(index))))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params uint[] scalarArray)
        {
            var termsCount = scalarArray.Length;

            return termsCount switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(scalarProcessor.GetScalarFromNumber(scalarArray[0])),
                _ => new LinVectorArrayStorage<T>(scalarProcessor.GetScalarsFromNumbers(scalarArray))
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<uint> scalarList)
        {
            return scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray());
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, long> indexScalarDictionary)
        {
            return indexScalarDictionary.Count switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => scalarProcessor.CreateLinVectorStorageFromNumber(indexScalarDictionary.First()),
                _ => new LinVectorSparseStorage<T>(indexScalarDictionary.ToDictionary(scalarProcessor.GetScalarFromNumber))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, long> indexToScalarFunc)
        {
            return termsCount switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(scalarProcessor.GetScalarFromNumber(indexToScalarFunc(0))),
                _ => new LinVectorArrayStorage<T>(((ulong) termsCount).RangeToArray(index => scalarProcessor.GetScalarFromNumber(indexToScalarFunc(index))))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params long[] scalarArray)
        {
            var termsCount = scalarArray.Length;

            return termsCount switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(scalarProcessor.GetScalarFromNumber(scalarArray[0])),
                _ => new LinVectorArrayStorage<T>(scalarProcessor.GetScalarsFromNumbers(scalarArray))
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<long> scalarList)
        {
            return scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray());
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, ulong> indexScalarDictionary)
        {
            return indexScalarDictionary.Count switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => scalarProcessor.CreateLinVectorStorageFromNumber(indexScalarDictionary.First()),
                _ => new LinVectorSparseStorage<T>(indexScalarDictionary.ToDictionary(scalarProcessor.GetScalarFromNumber))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, ulong> indexToScalarFunc)
        {
            return termsCount switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(scalarProcessor.GetScalarFromNumber(indexToScalarFunc(0))),
                _ => new LinVectorArrayStorage<T>(((ulong) termsCount).RangeToArray(index => scalarProcessor.GetScalarFromNumber(indexToScalarFunc(index))))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params ulong[] scalarArray)
        {
            var termsCount = scalarArray.Length;

            return termsCount switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(scalarProcessor.GetScalarFromNumber(scalarArray[0])),
                _ => new LinVectorArrayStorage<T>(scalarProcessor.GetScalarsFromNumbers(scalarArray))
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<ulong> scalarList)
        {
            return scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray());
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, float> indexScalarDictionary)
        {
            return indexScalarDictionary.Count switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => scalarProcessor.CreateLinVectorStorageFromNumber(indexScalarDictionary.First()),
                _ => new LinVectorSparseStorage<T>(indexScalarDictionary.ToDictionary(scalarProcessor.GetScalarFromNumber))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, float> indexToScalarFunc)
        {
            return termsCount switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(scalarProcessor.GetScalarFromNumber(indexToScalarFunc(0))),
                _ => new LinVectorArrayStorage<T>(((ulong) termsCount).RangeToArray(index => scalarProcessor.GetScalarFromNumber(indexToScalarFunc(index))))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params float[] scalarArray)
        {
            var termsCount = scalarArray.Length;

            return termsCount switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(scalarProcessor.GetScalarFromNumber(scalarArray[0])),
                _ => new LinVectorArrayStorage<T>(scalarProcessor.GetScalarsFromNumbers(scalarArray))
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<float> scalarList)
        {
            return scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray());
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, double> indexScalarDictionary)
        {
            return indexScalarDictionary.Count switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => scalarProcessor.CreateLinVectorStorageFromNumber(indexScalarDictionary.First()),
                _ => new LinVectorSparseStorage<T>(indexScalarDictionary.ToDictionary(scalarProcessor.GetScalarFromNumber))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, double> indexToScalarFunc)
        {
            return termsCount switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(scalarProcessor.GetScalarFromNumber(indexToScalarFunc(0))),
                _ => new LinVectorArrayStorage<T>(((ulong) termsCount).RangeToArray(index => scalarProcessor.GetScalarFromNumber(indexToScalarFunc(index))))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params double[] scalarArray)
        {
            var termsCount = scalarArray.Length;

            return termsCount switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(scalarProcessor.GetScalarFromNumber(scalarArray[0])),
                _ => new LinVectorArrayStorage<T>(scalarProcessor.GetScalarsFromNumbers(scalarArray))
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<double> scalarList)
        {
            return scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray());
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> CreateLinVectorStorageFromText<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, string> indexScalarDictionary)
        {
            return indexScalarDictionary.Count switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => scalarProcessor.CreateLinVectorStorageFromText(indexScalarDictionary.First()),
                _ => new LinVectorSparseStorage<T>(indexScalarDictionary.ToDictionary(scalarProcessor.GetScalarFromText))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromText<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, string> indexToScalarFunc)
        {
            return termsCount switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(scalarProcessor.GetScalarFromText(indexToScalarFunc(0))),
                _ => new LinVectorArrayStorage<T>(((ulong) termsCount).RangeToArray(index => scalarProcessor.GetScalarFromText(indexToScalarFunc(index))))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromText<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint termsCount, Func<ulong, string> indexToScalarFunc)
        {
            return termsCount switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(scalarProcessor.GetScalarFromText(indexToScalarFunc(0))),
                _ => new LinVectorArrayStorage<T>(((ulong) termsCount).RangeToArray(index => scalarProcessor.GetScalarFromText(indexToScalarFunc(index))))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromText<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params string[] scalarArray)
        {
            var termsCount = scalarArray.Length;

            return termsCount switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(scalarProcessor.GetScalarFromText(scalarArray[0])),
                _ => new LinVectorArrayStorage<T>(scalarProcessor.GetScalarsFromText(scalarArray))
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromText<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<string> scalarList)
        {
            return scalarProcessor.CreateLinVectorStorageFromText(scalarList.ToArray());
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> CreateLinVectorStorageFromObjects<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, object> indexScalarDictionary)
        {
            return indexScalarDictionary.Count switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => scalarProcessor.CreateLinVectorStorageFromObject(indexScalarDictionary.First()),
                _ => new LinVectorSparseStorage<T>(indexScalarDictionary.ToDictionary(pair => pair.Key, pair => scalarProcessor.GetScalarFromObject(pair.Value)))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromObjects<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, object> indexToScalarFunc)
        {
            return termsCount switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(scalarProcessor.GetScalarFromObject(indexToScalarFunc(0))),
                _ => new LinVectorArrayStorage<T>(((ulong) termsCount).RangeToArray(index => scalarProcessor.GetScalarFromObject(indexToScalarFunc(index))))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromObjects<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params object[] scalarArray)
        {
            var termsCount = scalarArray.Length;

            return termsCount switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(scalarProcessor.GetScalarFromObject(scalarArray[0])),
                _ => new LinVectorArrayStorage<T>(scalarProcessor.GetScalarsFromObjects(scalarArray))
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorageFromObjects<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<object> scalarList)
        {
            return scalarProcessor.CreateLinVectorStorageFromObjects(scalarList.ToArray());
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> CreateLinVectorStorage<T>(this Dictionary<ulong, T> indexScalarDictionary)
        {
            return indexScalarDictionary.Count switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => indexScalarDictionary.First().CreateLinVectorStorage(),
                _ => new LinVectorSparseStorage<T>(indexScalarDictionary)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorage<T>(int termsCount, Func<ulong, T> indexToScalarFunc)
        {
            return termsCount switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(indexToScalarFunc(0)),
                _ => new LinVectorArrayStorage<T>(((ulong) termsCount).RangeToArray(indexToScalarFunc))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorage<T>(params T[] scalarArray)
        {
            var termsCount = scalarArray.Length;

            return termsCount switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(scalarArray[0]),
                _ => new LinVectorArrayStorage<T>(scalarArray)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorage<T>(this IEnumerable<T> scalarList)
        {
            return CreateLinVectorStorage(scalarList.ToArray());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> CreateLinVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, Dictionary<ulong, T> indexScalarDictionary)
        {
            return indexScalarDictionary.Count switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => indexScalarDictionary.First().CreateLinVectorStorage(),
                _ => new LinVectorSparseStorage<T>(indexScalarDictionary)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, T> indexToScalarFunc)
        {
            return termsCount switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(indexToScalarFunc(0)),
                _ => new LinVectorArrayStorage<T>(((ulong) termsCount).RangeToArray(indexToScalarFunc))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint termsCount, Func<ulong, T> indexToScalarFunc)
        {
            return termsCount switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(indexToScalarFunc(0)),
                _ => new LinVectorArrayStorage<T>(((ulong) termsCount).RangeToArray(indexToScalarFunc))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params T[] scalarArray)
        {
            var termsCount = scalarArray.Length;

            return termsCount switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(scalarArray[0]),
                _ => new LinVectorArrayStorage<T>(scalarArray)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<T> scalarList)
        {
            return CreateLinVectorStorage(scalarList.ToArray());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> CreateLinVectorStorage<T>(this IEnumerable<IndexScalarRecord<T>> indexScalarRecords)
        {
            return indexScalarRecords
                .ToDictionary(
                    record => record.Index, 
                    record => record.Scalar
                )
                .CreateLinVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> CreateLinVectorStorage<T>(this IEnumerable<GradeIndexScalarRecord<T>> gradeIndexScalarRecords)
        {
            return gradeIndexScalarRecords
                .ToDictionary(
                    record => BasisBladeUtils.BasisBladeGradeIndexToId(record.Grade, record.Index), 
                    record => record.Scalar
                )
                .CreateLinVectorStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> CreateLinVectorStorage<T>(this IEnumerable<GradeIndexScalarRecord<T>> gradeIndexScalarRecords, Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            return gradeIndexScalarRecords
                .ToDictionary(
                    record => gradeIndexToIndexMapping(record.Grade, record.Index), 
                    record => record.Scalar
                )
                .CreateLinVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> CreateLinVectorStorage<T>(this IEnumerable<GradeIndexScalarRecord<T>> gradeIndexScalarRecords, Func<uint, ulong, T, IndexScalarRecord<T>> gradeIndexScalarToIndexScalarMapping)
        {
            return gradeIndexScalarRecords
                .Select(record => gradeIndexScalarToIndexScalarMapping(record.Grade, record.Index, record.Scalar))
                .CreateLinVectorStorage();
        }

        public static ILinVectorDenseStorage<T> CreateLinVectorDenseStorage<T>(this IEnumerable<IndexScalarRecord<T>> indexScalarRecords, int count, T defaultValue)
        {
            var scalarsArray = new T[count];
            
            for (var i = 0; i < count; i++)
                scalarsArray[i] = defaultValue;

            foreach (var (index, scalar) in indexScalarRecords)
                scalarsArray[index] = scalar;

            return scalarsArray.CreateLinVectorDenseStorage();
        }

        public static ILinVectorDenseStorage<T> CreateLinVectorDenseStorage<T>(this IEnumerable<GradeIndexScalarRecord<T>> indexScalarRecords, int count, T defaultValue)
        {
            var scalarsArray = new T[count];
            
            for (var i = 0; i < count; i++)
                scalarsArray[i] = defaultValue;

            foreach (var (grade, index, scalar) in indexScalarRecords)
            {
                var id = BasisBladeUtils.BasisBladeGradeIndexToId(grade, index);

                scalarsArray[id] = scalar;
            }

            return scalarsArray.CreateLinVectorDenseStorage();
        }

        public static ILinVectorGradedStorage<T> CreateLinVectorGradedStorage<T>(this IEnumerable<IndexScalarRecord<T>> indexScalarRecords)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (id, scalar) in indexScalarRecords)
            {
                var (grade, index) = 
                    id.BasisBladeIdToGradeIndex();

                if (!gradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary))
                {
                    indexScalarDictionary = new Dictionary<ulong, T>();
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
                }

                indexScalarDictionary.Add(index, scalar);
            }

            return gradeIndexScalarDictionary.CreateLinVectorGradedStorage();
        }

        public static ILinVectorGradedStorage<T> CreateLinVectorGradedStorage<T>(this IEnumerable<IndexScalarRecord<T>> indexScalarRecords, Func<ulong, GradeIndexRecord> indexToGradeIndexMapping)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (id, scalar) in indexScalarRecords)
            {
                var (grade, index) = 
                    indexToGradeIndexMapping(id);

                if (!gradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary))
                {
                    indexScalarDictionary = new Dictionary<ulong, T>();
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
                }

                indexScalarDictionary.Add(index, scalar);
            }

            return gradeIndexScalarDictionary.CreateLinVectorGradedStorage();
        }
        
        public static ILinVectorGradedStorage<T> CreateLinVectorGradedStorage<T>(this IEnumerable<IndexScalarRecord<T>> indexScalarRecords, Func<ulong, T, GradeIndexScalarRecord<T>> indexScalarToGradeIndexScalarMapping)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (id, value) in indexScalarRecords)
            {
                var (grade, index, scalar) = 
                    indexScalarToGradeIndexScalarMapping(id, value);

                if (!gradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary))
                {
                    indexScalarDictionary = new Dictionary<ulong, T>();
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
                }

                indexScalarDictionary.Add(index, scalar);
            }

            return gradeIndexScalarDictionary.CreateLinVectorGradedStorage();
        }

        public static ILinVectorGradedStorage<T> CreateLinVectorGradedStorage<T>(this IEnumerable<GradeIndexScalarRecord<T>> gradeIndexScalarRecords)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, index, scalar) in gradeIndexScalarRecords)
            {
                if (!gradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary))
                {
                    indexScalarDictionary = new Dictionary<ulong, T>();
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
                }

                indexScalarDictionary.Add(index, scalar);
            }

            return gradeIndexScalarDictionary.CreateLinVectorGradedStorage();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorListGradedStorage<T> CreateLinVectorListGradedStorage<T>(this uint grade)
        {
            return new LinVectorListGradedStorage<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorListGradedStorage<T> CreateLinVectorListGradedStorage<T>(this uint grade, int capacity)
        {
            return new LinVectorListGradedStorage<T>(capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorListGradedStorage<T> CreateLinVectorListGradedStorage<T>(this uint grade, IEnumerable<ILinVectorStorage<T>> evenLists)
        {
            return new LinVectorListGradedStorage<T>(evenLists);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorListGradedStorage<T> CreateLinVectorListGradedStorage<T>(this IEnumerable<ILinVectorStorage<T>> evenLists, uint grade)
        {
            return new LinVectorListGradedStorage<T>(evenLists);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorSingleGradeStorage<T> CreateLinVectorSingleGradeStorage<T>(this uint grade, ILinVectorStorage<T> vectorStorage)
        {
            if (vectorStorage.IsEmpty())
                return new LinVectorEmptySingleGradeStorage<T>(grade);
            
            if (vectorStorage is ILinVectorSingleScalarStorage<T> singleScalarVectorStorage)
                return new LinVectorSingleScalarGradedStorage<T>(grade, singleScalarVectorStorage);

            if (vectorStorage.GetSparseCount() > 1) 
                return new LinVectorSingleGradeStorage<T>(grade, vectorStorage);

            var (index, value) = vectorStorage.GetMinIndexScalarRecord();

            return new LinVectorSingleScalarGradedStorage<T>(grade, index, value);

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorSingleGradeStorage<T> CreateLinVectorSingleGradeStorage<T>(this ILinVectorStorage<T> vectorStorage, uint grade)
        {
            if (vectorStorage.IsEmpty())
                return new LinVectorSingleGradeStorage<T>(grade, LinVectorEmptyStorage<T>.EmptyStorage);

            if (vectorStorage is ILinVectorSingleScalarStorage<T> singleScalarVectorStorage)
                return new LinVectorSingleScalarGradedStorage<T>(grade, singleScalarVectorStorage);

            if (vectorStorage.GetSparseCount() > 1) 
                return new LinVectorSingleGradeStorage<T>(grade, vectorStorage);

            var (index, value) = vectorStorage.GetMinIndexScalarRecord();

            return new LinVectorSingleScalarGradedStorage<T>(grade, index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorSingleScalarGradedStorage<T> CreateLinVectorSingleScalarGradedStorage<T>(this T value)
        {
            return new LinVectorSingleScalarGradedStorage<T>(0U, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorSingleScalarGradedStorage<T> CreateLinVectorSingleScalarGradedStorage<T>(this T value, uint grade, ulong index)
        {
            return new LinVectorSingleScalarGradedStorage<T>(grade, index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorSingleScalarGradedStorage<T> CreateLinVectorSingleScalarGradedStorage<T>(this uint grade, ulong index, T value)
        {
            return new LinVectorSingleScalarGradedStorage<T>(grade, index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorSingleScalarGradedStorage<T> CreateLinVectorSingleScalarGradedStorage<T>(this uint grade, ILinVectorSingleScalarStorage<T> singleScalarVectorStorage)
        {
            return new LinVectorSingleScalarGradedStorage<T>(grade, singleScalarVectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorSingleScalarGradedStorage<T> CreateLinVectorSingleScalarGradedStorage<T>(this ILinVectorSingleScalarStorage<T> singleScalarVectorStorage, uint grade)
        {
            return new LinVectorSingleScalarGradedStorage<T>(grade, singleScalarVectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorSparseGradedStorage<T> CreateLinVectorSparseGradedStorage<T>(this Dictionary<uint, ILinVectorStorage<T>> gradeIndexScalarDictionary)
        {
            return new LinVectorSparseGradedStorage<T>(gradeIndexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorSparseGradedStorage<T> CreateLinVectorSparseGradedStorage<T>()
        {
            return new LinVectorSparseGradedStorage<T>();
        }

        public static ILinVectorGradedStorage<T> CreateLinVectorGradedStorage<T>(this Dictionary<uint, Dictionary<ulong, T>> gradeIndexScalarDictionary)
        {
            if (gradeIndexScalarDictionary.Count == 0 || gradeIndexScalarDictionary.Values.All(d => d.IsNullOrEmpty()))
                return LinVectorEmptyGradedStorage<T>.EmptyStorage;

            if (gradeIndexScalarDictionary.Count != 1)
            {
                return new LinVectorSparseGradedStorage<T>(
                    gradeIndexScalarDictionary
                        .ToDictionary(
                            dict => dict.CreateLinVectorStorage()
                        )
                );
            }

            var (grade, vectorStorage) = 
                gradeIndexScalarDictionary.First();

            return new LinVectorSingleGradeStorage<T>(
                grade, 
                vectorStorage.CreateLinVectorStorage()
            );
        }

        public static ILinVectorGradedStorage<T> CreateLinVectorGradedStorage<T>(this Dictionary<uint, ILinVectorStorage<T>> gradeIndexScalarDictionary)
        {
            if (gradeIndexScalarDictionary.Count == 0 || gradeIndexScalarDictionary.Values.All(d => d.IsEmpty()))
                return LinVectorEmptyGradedStorage<T>.EmptyStorage;

            if (gradeIndexScalarDictionary.Count != 1) 
                return new LinVectorSparseGradedStorage<T>(gradeIndexScalarDictionary);

            var (grade, vectorStorage) = 
                gradeIndexScalarDictionary.First();

            return new LinVectorSingleGradeStorage<T>(grade, vectorStorage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorRepeatedScalarStorage<T> CreateLinVectorRepeatedScalarStorage<T>(this T value, int count)
        {
            return new LinVectorRepeatedScalarStorage<T>(count, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorDenseStorage<T>(this T value, int count)
        {
            return count switch
            {
                < 1 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(value),
                _ => new LinVectorRepeatedScalarStorage<T>(count, value)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorSingleScalarDenseStorage<T> CreateLinVectorSingleScalarDenseStorage<T>(this T value)
        {
            return new LinVectorSingleScalarDenseStorage<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorSingleScalarStorage<T> CreateLinVectorSingleScalarStorage<T>(this T value, ulong index)
        {
            return index == 0UL
                ? new LinVectorSingleScalarDenseStorage<T>(value)
                : new LinVectorSingleScalarSparseStorage<T>(index, value);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorSingleScalarStorage<T> CreateLinVectorStorage<T>(this KeyValuePair<ulong, T> indexScalarPair)
        {
            var (index, scalar) = indexScalarPair;

            return index == 0UL
                ? new LinVectorSingleScalarDenseStorage<T>(scalar)
                : new LinVectorSingleScalarSparseStorage<T>(index, scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorSingleScalarStorage<T> CreateLinVectorStorageFromNumber<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KeyValuePair<ulong, int> indexScalarPair)
        {
            var (index, value) = indexScalarPair;

            var scalar = scalarProcessor.GetScalarFromNumber(value);

            return index == 0UL
                ? new LinVectorSingleScalarDenseStorage<T>(scalar)
                : new LinVectorSingleScalarSparseStorage<T>(index, scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorSingleScalarStorage<T> CreateLinVectorStorageFromNumber<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KeyValuePair<ulong, uint> indexScalarPair)
        {
            var (index, value) = indexScalarPair;

            var scalar = scalarProcessor.GetScalarFromNumber(value);

            return index == 0UL
                ? new LinVectorSingleScalarDenseStorage<T>(scalar)
                : new LinVectorSingleScalarSparseStorage<T>(index, scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorSingleScalarStorage<T> CreateLinVectorStorageFromNumber<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KeyValuePair<ulong, long> indexScalarPair)
        {
            var (index, value) = indexScalarPair;

            var scalar = scalarProcessor.GetScalarFromNumber(value);

            return index == 0UL
                ? new LinVectorSingleScalarDenseStorage<T>(scalar)
                : new LinVectorSingleScalarSparseStorage<T>(index, scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorSingleScalarStorage<T> CreateLinVectorStorageFromNumber<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KeyValuePair<ulong, ulong> indexScalarPair)
        {
            var (index, value) = indexScalarPair;

            var scalar = scalarProcessor.GetScalarFromNumber(value);

            return index == 0UL
                ? new LinVectorSingleScalarDenseStorage<T>(scalar)
                : new LinVectorSingleScalarSparseStorage<T>(index, scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorSingleScalarStorage<T> CreateLinVectorStorageFromNumber<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KeyValuePair<ulong, float> indexScalarPair)
        {
            var (index, value) = indexScalarPair;

            var scalar = scalarProcessor.GetScalarFromNumber(value);

            return index == 0UL
                ? new LinVectorSingleScalarDenseStorage<T>(scalar)
                : new LinVectorSingleScalarSparseStorage<T>(index, scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorSingleScalarStorage<T> CreateLinVectorStorageFromNumber<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KeyValuePair<ulong, double> indexScalarPair)
        {
            var (index, value) = indexScalarPair;

            var scalar = scalarProcessor.GetScalarFromNumber(value);

            return index == 0UL
                ? new LinVectorSingleScalarDenseStorage<T>(scalar)
                : new LinVectorSingleScalarSparseStorage<T>(index, scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorSingleScalarStorage<T> CreateLinVectorStorageFromText<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KeyValuePair<ulong, string> indexScalarPair)
        {
            var (index, value) = indexScalarPair;

            var scalar = scalarProcessor.GetScalarFromText(value);

            return index == 0UL
                ? new LinVectorSingleScalarDenseStorage<T>(scalar)
                : new LinVectorSingleScalarSparseStorage<T>(index, scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorSingleScalarStorage<T> CreateLinVectorStorageFromObject<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KeyValuePair<ulong, object> indexScalarPair)
        {
            var (index, value) = indexScalarPair;

            var scalar = scalarProcessor.GetScalarFromObject(value);

            return index == 0UL
                ? new LinVectorSingleScalarDenseStorage<T>(scalar)
                : new LinVectorSingleScalarSparseStorage<T>(index, scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorArrayStorage<T> CreateLinVectorArrayStorage<T>(int count)
        {
            return new LinVectorArrayStorage<T>(count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorArrayStorage<T> CreateLinVectorArrayStorage<T>(this T[] valuesArray)
        {
            return new LinVectorArrayStorage<T>(valuesArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorArrayStorage<T> CreateLinVectorArrayStorage<T>(this IEnumerable<T> vectorStorage)
        {
            return new LinVectorArrayStorage<T>(vectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorListStorage<T> CreateLinVectorListStorage<T>()
        {
            return new LinVectorListStorage<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorListStorage<T> CreateLinVectorListStorage<T>(int capacity)
        {
            return new LinVectorListStorage<T>(capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorListStorage<T> CreateLinVectorListStorage<T>(this IEnumerable<T> vectorStorage)
        {
            return new LinVectorListStorage<T>(vectorStorage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorListStorage<T> CreateLinVectorListStorage<T>(this List<T> vectorStorage)
        {
            return new LinVectorListStorage<T>(vectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorMatrixRowDenseStorage<T> CreateLinVectorDenseMatrixRowStorage<T>(this ILinMatrixStorage<T> matrix, ulong index1)
        {
            return new LinVectorMatrixRowDenseStorage<T>(matrix, index1, (_, _) => default);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorMatrixRowDenseStorage<T> CreateLinVectorMatrixRowDenseStorage<T>(this ILinMatrixStorage<T> matrix, ulong index1, T defaultValue)
        {
            return new LinVectorMatrixRowDenseStorage<T>(matrix, index1, (_, _) => defaultValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorMatrixRowDenseStorage<T> CreateLinVectorMatrixRowDenseStorage<T>(this ILinMatrixStorage<T> matrix, ulong index1, Func<T> defaultValueFunc)
        {
            return new LinVectorMatrixRowDenseStorage<T>(matrix, index1, (_, _) => defaultValueFunc());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorMatrixRowDenseStorage<T> CreateLinVectorMatrixRowDenseStorage<T>(this ILinMatrixStorage<T> matrix, ulong index1, Func<ulong, ulong, T> defaultValueFunc)
        {
            return new LinVectorMatrixRowDenseStorage<T>(matrix, index1, defaultValueFunc);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorMatrixColumnDenseStorage<T> CreateLinVectorMatrixColumnDenseStorage<T>(this ILinMatrixStorage<T> matrix, ulong index2)
        {
            return new LinVectorMatrixColumnDenseStorage<T>(matrix, index2, (_, _) => default);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorMatrixColumnDenseStorage<T> CreateLinVectorMatrixColumnDenseStorage<T>(this ILinMatrixStorage<T> matrix, ulong index2, T defaultValue)
        {
            return new LinVectorMatrixColumnDenseStorage<T>(matrix, index2, (_, _) => defaultValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorMatrixColumnDenseStorage<T> CreateLinVectorMatrixColumnDenseStorage<T>(this ILinMatrixStorage<T> matrix, ulong index2, Func<T> defaultValueFunc)
        {
            return new LinVectorMatrixColumnDenseStorage<T>(matrix, index2, (_, _) => defaultValueFunc());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorMatrixColumnDenseStorage<T> CreateLinVectorMatrixColumnDenseStorage<T>(this ILinMatrixStorage<T> matrix, ulong index2, Func<ulong, ulong, T> defaultValueFunc)
        {
            return new LinVectorMatrixColumnDenseStorage<T>(matrix, index2, defaultValueFunc);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorMatrixSliceDenseStorage<T> CreateLinVectorMatrixSliceDenseStorage<T>(this ILinMatrixStorage<T> matrix, int count, Func<ulong, IndexPairRecord> indexMapping)
        {
            return new LinVectorMatrixSliceDenseStorage<T>(matrix, count, indexMapping, (_, _) => default);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorMatrixSliceDenseStorage<T> CreateLinVectorMatrixSliceDenseStorage<T>(this ILinMatrixStorage<T> matrix, int count, Func<ulong, IndexPairRecord> indexMapping, T defaultValue)
        {
            return new LinVectorMatrixSliceDenseStorage<T>(matrix, count, indexMapping, (_, _) => defaultValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorMatrixSliceDenseStorage<T> CreateLinVectorMatrixSliceDenseStorage<T>(this ILinMatrixStorage<T> matrix, int count, Func<ulong, IndexPairRecord> indexMapping, Func<T> defaultValueFunc)
        {
            return new LinVectorMatrixSliceDenseStorage<T>(matrix, count, indexMapping, (_, _) => defaultValueFunc());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorMatrixSliceDenseStorage<T> CreateLinVectorMatrixSliceDenseStorage<T>(this ILinMatrixStorage<T> matrix, int count, Func<ulong, IndexPairRecord> indexMapping, Func<ulong, ulong, T> defaultValueFunc)
        {
            return new LinVectorMatrixSliceDenseStorage<T>(matrix, count, indexMapping, defaultValueFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorComputedDenseStorage<T> CreateLinVectorComputedDenseStorage<T>(int count, Func<ulong, T> indexToScalarMapping)
        {
            return new LinVectorComputedDenseStorage<T>(count, indexToScalarMapping);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorDenseStorage<T>(this IEnumerable<T> vectorStorage)
        {
            return CreateLinVectorDenseStorage(vectorStorage.ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> CreateLinVectorDenseStorage<T>(this IReadOnlyList<T> vectorStorage)
        {
            return vectorStorage.Count switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => new LinVectorSingleScalarDenseStorage<T>(vectorStorage[0]),
                _ => new LinVectorListStorage<T>(vectorStorage)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorSparseStorage<T> CreateLinVectorSparseStorage<T>(this Dictionary<ulong, T> indexScalarDictionary)
        {
            return new LinVectorSparseStorage<T>(indexScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorTreeStorage<T> CreateLinVectorTreeStorage<T>(this IReadOnlyList<ulong> idsList, int treeDepth)
        {
            return new LinVectorTreeStorage<T>(treeDepth, idsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorTreeStorage<T> CreateLinVectorTreeStorage<T>(this IReadOnlyList<ulong> leafNodeIDsList, IReadOnlyCollection<T> leafNodeValuesList, int treeDepth)
        {
            return new LinVectorTreeStorage<T>(treeDepth, leafNodeIDsList, leafNodeValuesList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorTreeStorage<T> CreateLinVectorTreeStorage<T>(this IReadOnlyDictionary<ulong, T> leafNodes)
        {
            var treeDepth = 
                (int) leafNodes.Keys.GetMinVSpaceDimension();

            return new LinVectorTreeStorage<T>(treeDepth, leafNodes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorTreeStorage<T> CreateLinVectorTreeStorage<T>(this IReadOnlyDictionary<ulong, T> leafNodes, int treeDepth)
        {
            return new LinVectorTreeStorage<T>(treeDepth, leafNodes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorTreeStorage<T> CopyToLinVectorTreeStorage<T>(this LinVectorTreeStorage<T> binaryTree)
        {
            return new LinVectorTreeStorage<T>(binaryTree);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorTreeStorage<T> CopyToLinVectorTreeStorage<T>(this LinVectorTreeStorage<T> binaryTree, int treeDepth)
        {
            return new LinVectorTreeStorage<T>(treeDepth, binaryTree);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorMatrixRowDenseStorage<T> CreateLinVectorMatrixRowDenseStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrix, int rowIndex)
        {
            return matrix.CreateLinVectorMatrixRowDenseStorage((ulong) rowIndex, scalarProcessor.ScalarZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorMatrixColumnDenseStorage<T> CreateLinVectorMatrixColumnDenseStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrix, int colIndex)
        {
            return matrix.CreateLinVectorMatrixColumnDenseStorage((ulong) colIndex, scalarProcessor.ScalarZero);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorSparseStorage<T> ToLinVectorSparseStorage<T>(this ILinVectorStorage<T> storage)
        {
            if (storage is LinVectorSparseStorage<T> sparseStorage)
                return sparseStorage;

            return storage
                .GetIndexScalarRecords()
                .ToDictionary(
                    r => r.Index,
                    r => r.Scalar
                )
                .CreateLinVectorSparseStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorTreeStorage<T> ToLinVectorTreeStorage<T>(this ILinVectorStorage<T> storage)
        {
            if (storage is LinVectorTreeStorage<T> treeStorage)
                return treeStorage;

            return storage
                .GetIndexScalarRecords()
                .ToDictionary(
                    r => r.Index,
                    r => r.Scalar
                )
                .CreateLinVectorTreeStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorArrayStorage<T> ToLinVectorArrayStorage<T>(this ILinVectorStorage<T> storage, T defaultScalar)
        {
            if (storage is LinVectorArrayStorage<T> arrayStorage)
                return arrayStorage;

            var array = storage.GetDenseCount().Repeat(defaultScalar).ToArray();

            foreach (var (index, scalar) in storage.GetIndexScalarRecords())
                array[index] = scalar ?? defaultScalar;

            return array.CreateLinVectorArrayStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorListStorage<T> ToLinVectorListStorage<T>(this ILinVectorStorage<T> storage, T defaultScalar)
        {
            if (storage is LinVectorListStorage<T> listStorage)
                return listStorage;

            var array = storage.GetDenseCount().Repeat(defaultScalar).ToArray();

            foreach (var (index, scalar) in storage.GetIndexScalarRecords())
                array[index] = scalar ?? defaultScalar;

            return array.CreateLinVectorListStorage();
        }
    }
}