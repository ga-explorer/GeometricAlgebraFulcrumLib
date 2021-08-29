using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices
{
    public sealed class LaMatrixRowStorage<T> :
        ILaMatrixSparseEvenStorage<T>
    {
        public ulong Key1 { get; }

        public ILaVectorEvenStorage<T> SourceList { get; }


        internal LaMatrixRowStorage(ulong index1, [NotNull] ILaVectorEvenStorage<T> sourceList)
        {
            Key1 = index1;
            SourceList = sourceList;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return SourceList.IsEmpty();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return SourceList.GetSparseCount();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return SourceList.GetScalars();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount1()
        {
            return SourceList.GetSparseCount();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount2()
        {
            return SourceList.IsEmpty() ? 0 : 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices1()
        {
            if (!SourceList.IsEmpty()) 
                yield return Key1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices2()
        {
            return SourceList.GetIndices();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexPairRecord> GetIndices()
        {
            return SourceList.GetIndices().Select(index => new IndexPairRecord(Key1, index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexPairScalarRecord<T>> GetIndexScalarRecords()
        {
            return SourceList.GetIndexScalarRecords().Select(indexValue => new IndexPairScalarRecord<T>(Key1, indexValue.Index, indexValue.Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexPairRecord> GetEmptyIndices(ulong maxKey1, ulong maxKey2)
        {
            var maxKey = new IndexPairRecord(maxKey1, maxKey2);

            return new HashSet<IndexPairRecord>(maxKey.GetKeyPairsInRange())
                .Except(SourceList
                    .GetIndices()
                    .Select(index => new IndexPairRecord(Key1, index))
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexPairRecord> GetEmptyIndices(IndexPairRecord maxKey)
        {
            return new HashSet<IndexPairRecord>(maxKey.GetKeyPairsInRange())
                .Except(SourceList
                    .GetIndices()
                    .Select(index => new IndexPairRecord(Key1, index))
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(ulong index1, ulong index2)
        {
            return index1 == Key1
                ? SourceList.GetScalar(index2)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(IndexPairRecord index)
        {
            var (index1, index2) = index;

            return index1 == Key1
                ? SourceList.GetScalar(index2)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(ulong index1, ulong index2)
        {
            return index1 == Key1 && SourceList.ContainsIndex(index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(IndexPairRecord index)
        {
            var (index1, index2) = index;

            return index1 == Key1 && SourceList.ContainsIndex(index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex1()
        {
            return !SourceList.IsEmpty()
                ? Key1
                : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex2()
        {
            return SourceList.GetMinIndex();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexPairRecord GetMinIndex()
        {
            var index = SourceList.GetMinIndex();

            return new IndexPairRecord(Key1, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex1()
        {
            return !SourceList.IsEmpty()
                ? Key1 
                : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex2()
        {
            return SourceList.GetMaxIndex();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexPairRecord GetMaxIndex()
        {
            var index = SourceList.GetMaxIndex();

            return new IndexPairRecord(Key1, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(ulong index1, ulong index2, out T value)
        {
            if (index1 == Key1 && SourceList.TryGetScalar(index2, out value))
                return true;

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(IndexPairRecord index, out T value)
        {
            var (index1, index2) = index;

            if (index1 == Key1 && SourceList.TryGetScalar(index2, out value))
                return true;

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T> GetCopy()
        {
            return this;
        }

        public ILaMatrixEvenStorage<T> MapIndices(Func<ulong, ulong, IndexPairRecord> indexMapping)
        {
            var indexValueDictionary = new Dictionary<IndexPairRecord, T>();

            foreach (var (k, value) in SourceList.GetIndexScalarRecords())
            {
                var (index1, index2) = indexMapping(Key1, k);

                var index = new IndexPairRecord(index1, index2); 

                if (indexValueDictionary.ContainsKey(index))
                    indexValueDictionary[index] = value;
                else
                    indexValueDictionary.Add(index, value);
            }

            return indexValueDictionary.CreateEvenGrid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return new LaMatrixDiagonalStorage<T2>(SourceList.MapScalars(valueMapping));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexValueMapping)
        {
            return new LaMatrixDiagonalStorage<T2>(SourceList.MapScalars((index, value) => indexValueMapping(Key1, index, value)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter)
        {
            return new LaMatrixDiagonalStorage<T>(SourceList.FilterByIndex(index => indexFilter(Key1, index)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexValueFilter)
        {
            return new LaMatrixDiagonalStorage<T>(SourceList.FilterByIndexScalar((index, value) => indexValueFilter(Key1, index, value)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return new LaMatrixDiagonalStorage<T>(SourceList.FilterByScalar(valueFilter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T> Transpose()
        {
            return this;
        }

        public bool TryGetCompactStorage(out ILaMatrixEvenStorage<T> evenGrid)
        {
            if (SourceList.TryGetCompactStorage(out var sourceList))
            {
                var count = sourceList.GetSparseCount();

                evenGrid = count switch
                {
                    0 => LaMatrixEmptyStorage<T>.EmptyMatrix,
                    1 => sourceList.GetMinKeyValueRecord().CreateEvenGridSingleKey(),
                    _ => new LaMatrixRowStorage<T>(Key1, sourceList)
                };

                return true;
            }
            else
            {
                var count = SourceList.GetSparseCount();

                if (count > 1)
                {
                    evenGrid = this;
                    return false;
                }
                
                evenGrid = count == 0
                    ? LaMatrixEmptyStorage<T>.EmptyMatrix
                    : sourceList.GetMinKeyValueRecord().CreateEvenGridSingleKey();

                return true;
            }
        }

        public ILaMatrixGradedStorage<T> ToGradedStorage(Func<ulong, ulong, GradeIndexPairRecord> evenKeyToGradeKeyMapping)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, Dictionary<IndexPairRecord, T>>();

            foreach (var (k, value) in SourceList.GetIndexScalarRecords())
            {
                var (grade, index1, index2) = evenKeyToGradeKeyMapping(Key1, k);

                if (!gradeKeyValueDictionary.TryGetValue(grade, out var indexValueDictionary))
                {
                    indexValueDictionary = new Dictionary<IndexPairRecord, T>();
                    gradeKeyValueDictionary.Add(grade, indexValueDictionary);
                }

                var index = new IndexPairRecord(index1, index2); 

                if (indexValueDictionary.ContainsKey(index))
                    indexValueDictionary[index] = value;
                else
                    indexValueDictionary.Add(index, value);
            }

            return gradeKeyValueDictionary.CreateLaMatrixGradedStorage();
        }

        public ILaMatrixGradedStorage<T> ToGradedStorage(Func<ulong, ulong, T, GradeIndexPairScalarRecord<T>> evenIndexScalarToGradeIndexScalarMapping)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, Dictionary<IndexPairRecord, T>>();

            foreach (var (k, value) in SourceList.GetIndexScalarRecords())
            {
                var (grade, index1, index2, scalar) = evenIndexScalarToGradeIndexScalarMapping(Key1, k, value);

                if (!gradeKeyValueDictionary.TryGetValue(grade, out var indexValueDictionary))
                {
                    indexValueDictionary = new Dictionary<IndexPairRecord, T>();
                    gradeKeyValueDictionary.Add(grade, indexValueDictionary);
                }

                var index = new IndexPairRecord(index1, index2); 

                if (indexValueDictionary.ContainsKey(index))
                    indexValueDictionary[index] = scalar;
                else
                    indexValueDictionary.Add(index, scalar);
            }

            return gradeKeyValueDictionary.CreateLaMatrixGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> GetRow(ulong index1)
        {
            return index1 == Key1
                ? SourceList
                : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> GetColumn(ulong index2)
        {
            return SourceList.TryGetScalar(index2, out var value)
                ? value.CreateLaVectorSingleIndexEvenStorage(index2)
                : LaVectorEmptyStorage<T>.ZeroStorage;
        }
    }
}