using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Collections;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Sparse;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class LinMatrixStorageFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MatrixGradedStorageComposer<T> CreateLinMatrixGradedStorageComposer<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return new MatrixGradedStorageComposer<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MatrixSparseStorageComposer<T> CreateLinMatrixSparseStorageComposer<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return new MatrixSparseStorageComposer<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MatrixDenseStorageComposer<T> CreateLinMatrixDenseStorageComposer<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int count1, int count2)
        {
            return new MatrixDenseStorageComposer<T>(scalarProcessor, count1, count2);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixColumnsListStorage<T> CreateLinMatrixColumnsListStorage<T>(this Dictionary<ulong, ILinVectorStorage<T>> indexVectorDictionary)
        {
            return new LinMatrixColumnsListStorage<T>(indexVectorDictionary.CreateLinVectorStorage());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixColumnsListStorage<T> CreateLinMatrixColumnsListStorage<T>(this IEnumerable<ILinVectorStorage<T>> columnsList)
        {
            return new LinMatrixColumnsListStorage<T>(columnsList.CreateLinVectorArrayStorage());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixColumnsListStorage<T> CreateLinMatrixColumnsListStorage<T>(this ILinVectorStorage<ILinVectorStorage<T>> columnsList)
        {
            return new LinMatrixColumnsListStorage<T>(columnsList);
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixRowsListStorage<T> CreateLinMatrixRowsListStorage<T>(this Dictionary<ulong, ILinVectorStorage<T>> indexVectorDictionary)
        {
            return new LinMatrixRowsListStorage<T>(indexVectorDictionary.CreateLinVectorStorage());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixRowsListStorage<T> CreateLinMatrixRowsListStorage<T>(this IEnumerable<ILinVectorStorage<T>> rowsList)
        {
            return new LinMatrixRowsListStorage<T>(rowsList.CreateLinVectorArrayStorage());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixRowsListStorage<T> CreateLinMatrixRowsListStorage<T>(this ILinVectorStorage<ILinVectorStorage<T>> rowsList)
        {
            return new LinMatrixRowsListStorage<T>(rowsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixListGradedStorage<T> CreateLinMatrixListGradedStorage<T>(this uint grade)
        {
            return new LinMatrixListGradedStorage<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixListGradedStorage<T> CreateLinMatrixListGradedStorage<T>(this uint grade, int capacity)
        {
            return new LinMatrixListGradedStorage<T>(capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixListGradedStorage<T> CreateLinMatrixListGradedStorage<T>(this uint grade, IEnumerable<ILinMatrixStorage<T>> matrixStorageList)
        {
            return new LinMatrixListGradedStorage<T>(matrixStorageList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixListGradedStorage<T> CreateLinMatrixListGradedStorage<T>(this IEnumerable<ILinMatrixStorage<T>> matrixStorageList, uint grade)
        {
            return new LinMatrixListGradedStorage<T>(matrixStorageList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixSingleGradeStorage<T> CreateLinMatrixSingleGradeStorage<T>(this uint grade, ILinMatrixStorage<T> evenMatrix)
        {
            if (evenMatrix.IsEmpty())
                return new LinMatrixEmptySingleGradeStorage<T>(grade);
            
            if (evenMatrix is ILinMatrixSingleScalarStorage<T> singleKeyMatrix)
                return new LinMatrixSingleScalarGradedStorage<T>(grade, singleKeyMatrix);

            if (evenMatrix.GetSparseCount() > 1) 
                return new LinMatrixSingleGradeStorage<T>(grade, evenMatrix);

            var (index1, index2, value) = evenMatrix.GetMinIndexScalarRecord();

            return new LinMatrixSingleScalarGradedStorage<T>(grade, index1, index2, value);

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixSingleGradeStorage<T> CreateLinMatrixSingleGradeStorage<T>(this ILinMatrixStorage<T> evenMatrix, uint grade)
        {
            if (evenMatrix.IsEmpty())
                return new LinMatrixSingleGradeStorage<T>(grade, LinMatrixEmptyStorage<T>.EmptyStorage);

            if (evenMatrix is ILinMatrixSingleScalarStorage<T> singleKeyMatrix)
                return new LinMatrixSingleScalarGradedStorage<T>(grade, singleKeyMatrix);

            if (evenMatrix.GetSparseCount() > 1) 
                return new LinMatrixSingleGradeStorage<T>(grade, evenMatrix);

            var (index1, index2, value) = evenMatrix.GetMinIndexScalarRecord();

            return new LinMatrixSingleScalarGradedStorage<T>(grade, index1, index2, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixSingleScalarGradedStorage<T> CreateLinMatrixSingleScalarGradedStorage<T>(this T value)
        {
            return new LinMatrixSingleScalarGradedStorage<T>(0U, 0UL, 0UL, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixSingleScalarGradedStorage<T> CreateLinMatrixSingleScalarGradedStorage<T>(this T value, uint grade, ulong index1, ulong index2)
        {
            return new LinMatrixSingleScalarGradedStorage<T>(grade, index1, index2, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixSingleScalarGradedStorage<T> CreateLinMatrixSingleScalarGradedStorage<T>(this T value, uint grade, IndexPairRecord index)
        {
            return new LinMatrixSingleScalarGradedStorage<T>(grade, index.Index1, index.Index2, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixSingleScalarGradedStorage<T> CreateLinMatrixSingleScalarGradedStorage<T>(this uint grade, ulong index1, ulong index2, T value)
        {
            return new LinMatrixSingleScalarGradedStorage<T>(grade, index1, index2, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixSingleScalarGradedStorage<T> CreateLinMatrixSingleScalarGradedStorage<T>(this uint grade, T value, IndexPairRecord index)
        {
            return new LinMatrixSingleScalarGradedStorage<T>(grade, index.Index1, index.Index2, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixSingleScalarGradedStorage<T> CreateLinMatrixSingleScalarGradedStorage<T>(this uint grade, ILinMatrixSingleScalarStorage<T> singleKeyMatrix)
        {
            return new LinMatrixSingleScalarGradedStorage<T>(grade, singleKeyMatrix);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixSingleScalarGradedStorage<T> CreateLinMatrixSingleScalarGradedStorage<T>(this ILinMatrixSingleScalarStorage<T> singleKeyMatrix, uint grade)
        {
            return new LinMatrixSingleScalarGradedStorage<T>(grade, singleKeyMatrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixSparseGradedStorage<T> CreateLinMatrixSparseGradedStorage<T>(this Dictionary<uint, ILinMatrixStorage<T>> gradeIndexScalarDictionary)
        {
            return new LinMatrixSparseGradedStorage<T>(gradeIndexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixSparseGradedStorage<T> CreateLinMatrixSparseGradedStorage<T>()
        {
            return new LinMatrixSparseGradedStorage<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixGradedStorage<T> CreateLinMatrixGradedStorage<T>(this IEnumerable<GradeIndexPairScalarRecord<T>> gradeIndexScalarRecords)
        {
            return gradeIndexScalarRecords
                .GroupBy(r => r.Grade)
                .ToDictionary(
                    g => g.Key,
                    g =>
                        g.ToDictionary(
                            s => new IndexPairRecord(s.Index1, s.Index2),
                            s => s.Scalar
                        )
                ).CreateLinMatrixGradedStorage();
        }

        public static ILinMatrixGradedStorage<T> CreateLinMatrixGradedStorage<T>(this Dictionary<uint, Dictionary<IndexPairRecord, T>> gradeIndexScalarDictionary)
        {
            if (gradeIndexScalarDictionary.Count == 0 || gradeIndexScalarDictionary.Values.All(d => d.IsNullOrEmpty()))
                return LinMatrixEmptyGradedStorage<T>.EmptyStorage;

            if (gradeIndexScalarDictionary.Count != 1)
            {
                return new LinMatrixSparseGradedStorage<T>(
                    gradeIndexScalarDictionary
                        .ToDictionary(
                            gradeDict => gradeDict.Key,
                            gradeDict => gradeDict.Value.CreateLinMatrixStorage()
                        )
                );
            }

            var (grade, evenMatrix) = 
                gradeIndexScalarDictionary.First();

            return new LinMatrixSingleGradeStorage<T>(
                grade, 
                evenMatrix.CreateLinMatrixStorage()
            );
        }

        public static ILinMatrixGradedStorage<T> CreateLinMatrixGradedStorage<T>(this Dictionary<uint, ILinMatrixStorage<T>> gradeIndexScalarDictionary)
        {
            if (gradeIndexScalarDictionary.Count == 0 || gradeIndexScalarDictionary.Values.All(d => d.IsEmpty()))
                return LinMatrixEmptyGradedStorage<T>.EmptyStorage;

            if (gradeIndexScalarDictionary.Count != 1) 
                return new LinMatrixSparseGradedStorage<T>(gradeIndexScalarDictionary);

            var (grade, evenMatrix) = 
                gradeIndexScalarDictionary.First();

            return new LinMatrixSingleGradeStorage<T>(grade, evenMatrix);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixDiagonalStorage<T> CreateLinMatrixDiagonalStorage<T>(this ILinVectorStorage<T> sourceList)
        {
            return new LinMatrixDiagonalStorage<T>(sourceList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> CreateLinMatrixRowStorage<T>(this ILinVectorStorage<T> sourceList)
        {
            return sourceList is ILinVectorDenseStorage<T> denseList
                ? new LinMatrixRowDenseStorage<T>(denseList)
                : sourceList.CreateLinVectorSingleScalarStorage(0).CreateLinMatrixRowsListStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> CreateLinMatrixRowStorage<T>(this ILinVectorStorage<T> sourceList, ulong index1)
        {
            return index1 == 0 && sourceList is ILinVectorDenseStorage<T> denseList
                ? new LinMatrixRowDenseStorage<T>(denseList)
                : sourceList.CreateLinVectorSingleScalarStorage(index1).CreateLinMatrixRowsListStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> CreateLinMatrixColumnStorage<T>(this ILinVectorStorage<T> sourceList)
        {
            return sourceList is ILinVectorDenseStorage<T> denseList
                ? new LinMatrixColumnDenseStorage<T>(denseList)
                : sourceList.CreateLinVectorSingleScalarStorage(0).CreateLinMatrixColumnsListStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> CreateLinMatrixColumnStorage<T>(this ILinVectorStorage<T> sourceList, ulong index2)
        {
            return index2 == 0 && sourceList is ILinVectorDenseStorage<T> denseList
                ? new LinMatrixColumnDenseStorage<T>(denseList)
                : sourceList.CreateLinVectorSingleScalarStorage(index2).CreateLinMatrixColumnsListStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixRepeatedScalarStorage<T> CreateLinMatrixRepeatedScalarStorage<T>(this T value, int count1, int count2)
        {
            return new LinMatrixRepeatedScalarStorage<T>(count1, count2, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixRowDenseStorage<T> CreateLinMatrixRowStorage<T>(this ILinVectorDenseStorage<T> sourceList)
        {
            return new LinMatrixRowDenseStorage<T>(sourceList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixColumnDenseStorage<T> CreateLinMatrixColumnStorage<T>(this ILinVectorDenseStorage<T> sourceList)
        {
            return new LinMatrixColumnDenseStorage<T>(sourceList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixDenseStorage<T> CreateLinMatrixDenseStorage<T>(this T value, int count1, int count2)
        {
            if (count1 < 0 || count2 < 0)
                throw new ArgumentOutOfRangeException();

            var count = count1 * count2;

            return count switch
            {
                < 1 => LinMatrixEmptyStorage<T>.EmptyStorage,
                1 => new LinMatrixSingleScalarDenseStorage<T>(value),
                _ => new LinMatrixRepeatedScalarStorage<T>(count1, count2, value)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixSingleScalarDenseStorage<T> CreateLinMatrixSingleScalarDenseStorage<T>(this T value)
        {
            return new LinMatrixSingleScalarDenseStorage<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixSingleScalarStorage<T> CreateLinMatrixSingleScalarStorage<T>(this T value, ulong index1, ulong index2)
        {
            return index1 == 0UL && index2 == 0UL
                ? new LinMatrixSingleScalarDenseStorage<T>(value)
                : new LinMatrixSingleScalarSparseStorage<T>(index1, index2, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixSingleScalarStorage<T> CreateLinMatrixSingleScalarStorage<T>(this T value, IndexPairRecord index)
        {
            var (index1, index2) = index;

            return index1 == 0UL && index2 == 0UL
                ? new LinMatrixSingleScalarDenseStorage<T>(value)
                : new LinMatrixSingleScalarSparseStorage<T>(index1, index2, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixSingleScalarStorage<T> CreateLinMatrixSingleScalarStorage<T>(this IndexPairRecord index, T value)
        {
            var (index1, index2) = index;

            return index1 == 0UL && index2 == 0UL
                ? new LinMatrixSingleScalarDenseStorage<T>(value)
                : new LinMatrixSingleScalarSparseStorage<T>(index, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixSingleScalarStorage<T> CreateLinMatrixSingleScalarStorage<T>(this KeyValuePair<IndexPairRecord, T> indexValue)
        {
            var ((index1, index2), value) = indexValue;

            return index1 == 0UL && index2 == 0UL
                ? new LinMatrixSingleScalarDenseStorage<T>(value)
                : new LinMatrixSingleScalarSparseStorage<T>(index1, index2, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixSingleScalarStorage<T> CreateLinMatrixSingleScalarStorage<T>(this IndexPairScalarRecord<T> indexValue)
        {
            var (index1, index2, value) = indexValue;

            return index1 == 0UL && index2 == 0UL
                ? new LinMatrixSingleScalarDenseStorage<T>(value)
                : new LinMatrixSingleScalarSparseStorage<T>(index1, index1, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixSingleScalarStorage<T> CreateLinMatrixSingleScalarStorage<T>(this IndexScalarRecord<T> indexValue)
        {
            var (index, value) = indexValue;

            return index == 0UL
                ? new LinMatrixSingleScalarDenseStorage<T>(value)
                : new LinMatrixSingleScalarSparseStorage<T>(index, index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixDenseStorage<T> CreateLinMatrixDenseStorage<T>(int count1, int count2)
        {
            return new LinMatrixDenseStorage<T>(count1, count2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixMappedDenseStorage<T> CreateLinMatrixMappedDenseStorage<T>(this ILinMatrixDenseStorage<T> evenDictionaryMatrix, Func<ulong, ulong, IndexPairRecord> indexMapping)
        {
            return new LinMatrixMappedDenseStorage<T>(evenDictionaryMatrix, indexMapping);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixMappedDenseStorage<T> CreateLinMatrixMappedDenseStorage<T>(this ILinMatrixDenseStorage<T> evenDictionaryMatrix, Func<ulong, ulong, T, T> indexValueMapping)
        {
            return new LinMatrixMappedDenseStorage<T>(evenDictionaryMatrix, indexValueMapping);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixMappedDenseStorage<T> CreateLinMatrixMappedDenseStorage<T>(this ILinMatrixDenseStorage<T> evenDictionaryMatrix, Func<ulong, ulong, IndexPairRecord> indexMapping, Func<ulong, ulong, T, T> indexValueMapping)
        {
            return new LinMatrixMappedDenseStorage<T>(evenDictionaryMatrix, indexMapping, indexValueMapping);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixComputedDenseStorage<T> CreateLinMatrixComputedDenseStorage<T>(int count1, int count2, Func<ulong, ulong, T> indexToScalarMapping)
        {
            return new LinMatrixComputedDenseStorage<T>(count1, count2, indexToScalarMapping);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixDenseStorage<T> CreateLinMatrixDenseStorage<T>(this T[,] valuesArray)
        {
            return valuesArray.Length switch
            {
                0 => LinMatrixEmptyStorage<T>.EmptyStorage,
                1 => new LinMatrixSingleScalarDenseStorage<T>(valuesArray[0, 0]),
                _ => new LinMatrixDenseStorage<T>(valuesArray)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixSparseStorage<T> CreateLinMatrixSparseStorage<T>(this Dictionary<IndexPairRecord, T> valuesDictionary)
        {
            return new LinMatrixSparseStorage<T>(valuesDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> CreateLinMatrixStorage<T>(this IEnumerable<IndexPairScalarRecord<T>> indexScalarRecords)
        {
            return indexScalarRecords.ToDictionary(
                record => record.GetIndexPairRecord(),
                record => record.Scalar
            ).CreateLinMatrixStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> CreateLinMatrixStorage<T>(this Dictionary<IndexPairRecord, T> valuesDictionary)
        {
            return valuesDictionary.Count switch
            {
                0 => LinMatrixEmptyStorage<T>.EmptyStorage,
                1 => CreateLinMatrixSingleScalarStorage(valuesDictionary.First()),
                _ => new LinMatrixSparseStorage<T>(valuesDictionary)
            };
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> CreateLinMatrixRowStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorStorage<T> array)
        {
            return array.CreateLinMatrixRowStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> CreateLinMatrixRowStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixStorage<T> array, int rowIndex)
        {
            return array.GetRow((ulong) rowIndex).CreateLinMatrixRowStorage((ulong) rowIndex);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> CreateLinMatrixColumnStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorStorage<T> array)
        {
            return array.CreateLinMatrixColumnStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> CreateLinMatrixColumnStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixStorage<T> array, int columnIndex)
        {
            return array.GetColumn((ulong) columnIndex).CreateLinMatrixColumnStorage((ulong) columnIndex);
        }
    }
}