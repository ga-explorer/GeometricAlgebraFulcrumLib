using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Sparse
{
    public sealed class LinMatrixColumnsListStorage<T> :
        ILinMatrixSparseStorage<T>
    {
        public ILinVectorStorage<ILinVectorStorage<T>> ColumnsList { get; }


        public int ColumnsCount 
            => ColumnsList.GetSparseCount();


        internal LinMatrixColumnsListStorage(ILinVectorStorage<ILinVectorStorage<T>> columnsList)
        {
            ColumnsList = columnsList;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return ColumnsList.IsEmpty() || 
                   ColumnsList.GetScalars().All(c => c.IsEmpty());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return ColumnsList.GetScalars().Sum(c => c.GetSparseCount());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return ColumnsList.GetScalars().SelectMany(c => c.GetScalars());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount1()
        {
            return ColumnsList.IsEmpty()
                ? 0
                : ColumnsList.GetScalars().Max(c => c.GetSparseCount());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount2()
        {
            return ColumnsList.GetSparseCount();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices1()
        {
            return ColumnsList.GetScalars().SelectMany(c => c.GetIndices()).Distinct();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices2()
        {
            return ColumnsList.GetIndices().Select(i => i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexPairRecord> GetIndices()
        {
            foreach (var (index2, column) in ColumnsList.GetIndexScalarRecords())
                foreach (var index1 in column.GetIndices())
                    yield return new RGaKvIndexPairRecord(index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexPairScalarRecord<T>> GetIndexScalarRecords()
        {
            foreach (var (index2, column) in ColumnsList.GetIndexScalarRecords())
            foreach (var (index1, scalar) in column.GetIndexScalarRecords())
                yield return new RGaKvIndexPairScalarRecord<T>(index1, index2, scalar);
        }

        public IEnumerable<RGaKvIndexPairRecord> GetEmptyIndices(ulong maxIndex1, ulong maxIndex2)
        {
            for (var index2 = 0UL; index2 < maxIndex2; index2++)
            {
                if (!ColumnsList.TryGetScalar(index2, out var vector))
                {
                    for (var index1 = 0UL; index1 < maxIndex1; index1++)
                        yield return new RGaKvIndexPairRecord(index1, index2);
                }
                else
                {
                    foreach (var index1 in vector.GetEmptyIndices(maxIndex1))
                        yield return new RGaKvIndexPairRecord(index1, index2);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexPairRecord> GetEmptyIndices(RGaKvIndexPairRecord maxIndex)
        {
            var (index1, index2) = maxIndex;
            
            return GetEmptyIndices(index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(ulong index1, ulong index2)
        {
            return 
                ColumnsList.TryGetScalar(index2, out var vector) && 
                vector.TryGetScalar(index1, out var scalar) 
                    ? scalar 
                    : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(RGaKvIndexPairRecord index)
        {
            var (index1, index2) = index;
            
            return 
                ColumnsList.TryGetScalar(index2, out var vector) && 
                vector.TryGetScalar(index1, out var scalar) 
                    ? scalar 
                    : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(ulong index1, ulong index2)
        {
            return 
                ColumnsList.TryGetScalar(index2, out var vector) && 
                vector.ContainsIndex(index1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(RGaKvIndexPairRecord index)
        {
            var (index1, index2) = index;
            
            return 
                ColumnsList.TryGetScalar(index2, out var vector) && 
                vector.ContainsIndex(index1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex1()
        {
            return ColumnsList.GetScalars().SelectMany(rowVector => rowVector.GetIndices()).Min();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex2()
        {
            return ColumnsList.GetIndices().Min();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaKvIndexPairRecord GetMinIndex()
        {
            return new RGaKvIndexPairRecord(
                GetMinIndex1(),
                GetMinIndex2()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex1()
        {
            return ColumnsList.GetScalars().SelectMany(rowVector => rowVector.GetIndices()).Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex2()
        {
            return ColumnsList.GetIndices().Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaKvIndexPairRecord GetMaxIndex()
        {
            return new RGaKvIndexPairRecord(
                GetMaxIndex1(),
                GetMaxIndex2()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(ulong index1, ulong index2, out T scalar)
        {
            if (ColumnsList.TryGetScalar(index2, out var vector) && vector.TryGetScalar(index1, out scalar))
                return true;

            scalar = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(RGaKvIndexPairRecord index, out T scalar)
        {
            var (index1, index2) = index;
            
            if (ColumnsList.TryGetScalar(index2, out var vector) && vector.TryGetScalar(index1, out scalar))
                return true;

            scalar = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetCopy()
        {
            return this;
        }

        public ILinMatrixStorage<T> GetPermutation(Func<ulong, ulong, RGaKvIndexPairRecord> indexMapping)
        {
            return GetIndexScalarRecords().MapRecords(indexMapping).CreateLinMatrixStorage();
        }

        public ILinMatrixStorage<T> GetPermutation(Func<RGaKvIndexPairRecord, RGaKvIndexPairRecord> indexMapping)
        {
            return GetIndexScalarRecords().MapRecords(indexMapping).CreateLinMatrixStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T2> MapScalars<T2>(Func<T, T2> scalarMapping)
        {
            return new LinMatrixColumnsListStorage<T2>(
                ColumnsList.MapScalars(vector => vector.MapScalars(scalarMapping))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexScalarMapping)
        {
            return new LinMatrixColumnsListStorage<T2>(
                ColumnsList.MapScalars((index2, vector) => 
                    vector.MapScalars((index1, scalar) => 
                        indexScalarMapping(index1, index2, scalar))
                )
            );
        }

        public ILinMatrixStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter)
        {
            var indexValueDictionary = new Dictionary<RGaKvIndexPairRecord, T>();

            foreach (var (index2, vector) in ColumnsList.GetIndexScalarRecords())
            foreach (var (index1, scalar) in vector.GetIndexScalarRecords())
            {
                if (!indexFilter(index1, index2)) continue;
                
                var index = new RGaKvIndexPairRecord(index1, index2); 

                if (indexValueDictionary.ContainsKey(index))
                    indexValueDictionary[index] = scalar;
                else
                    indexValueDictionary.Add(index, scalar);
            }

            return indexValueDictionary.CreateLinMatrixStorage();
        }

        public ILinMatrixStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexScalarFilter)
        {
            var indexValueDictionary = new Dictionary<RGaKvIndexPairRecord, T>();

            foreach (var (index2, vector) in ColumnsList.GetIndexScalarRecords())
            foreach (var (index1, scalar) in vector.GetIndexScalarRecords())
            {
                if (!indexScalarFilter(index1, index2, scalar)) continue;
                
                var index = new RGaKvIndexPairRecord(index1, index2); 

                if (indexValueDictionary.ContainsKey(index))
                    indexValueDictionary[index] = scalar;
                else
                    indexValueDictionary.Add(index, scalar);
            }

            return indexValueDictionary.CreateLinMatrixStorage();
        }

        public ILinMatrixStorage<T> FilterByScalar(Func<T, bool> scalarFilter)
        {
            var indexValueDictionary = new Dictionary<RGaKvIndexPairRecord, T>();

            foreach (var (index2, vector) in ColumnsList.GetIndexScalarRecords())
            foreach (var (index1, scalar) in vector.GetIndexScalarRecords())
            {
                if (!scalarFilter(scalar)) continue;
                
                var index = new RGaKvIndexPairRecord(index1, index2); 

                if (indexValueDictionary.ContainsKey(index))
                    indexValueDictionary[index] = scalar;
                else
                    indexValueDictionary.Add(index, scalar);
            }

            return indexValueDictionary.CreateLinMatrixStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetTranspose()
        {
            return new LinMatrixRowsListStorage<T>(ColumnsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactStorage(out ILinMatrixStorage<T> evenStorage)
        {
            if (IsEmpty())
            {
                evenStorage = LinMatrixEmptyStorage<T>.EmptyStorage;
                return true;
            }

            evenStorage = this;
            return false;
        }

        public ILinMatrixGradedStorage<T> ToMatrixGradedStorage(Func<ulong, ulong, RGaGradeKvIndexPairRecord> indexToGradeIndexMapping)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<RGaKvIndexPairRecord, T>>();

            foreach (var (id2, vector) in ColumnsList.GetIndexScalarRecords())
            foreach (var (id1, scalar) in vector.GetIndexScalarRecords())
            {
                var (grade, index1, index2) = indexToGradeIndexMapping(id1, id2);

                if (!gradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary))
                {
                    indexScalarDictionary = new Dictionary<RGaKvIndexPairRecord, T>();
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
                }

                var index = new RGaKvIndexPairRecord(index1, index2);

                indexScalarDictionary.Add(index, scalar);
            }

            return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
        }

        public ILinMatrixGradedStorage<T> ToMatrixGradedStorage(Func<ulong, ulong, T, RGaGradeKvIndexPairScalarRecord<T>> indexScalarToGradeIndexScalarMapping)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<RGaKvIndexPairRecord, T>>();

            foreach (var (id2, vector) in ColumnsList.GetIndexScalarRecords())
            foreach (var (id1, value) in vector.GetIndexScalarRecords())
            {
                var (grade, index1, index2, scalar) = indexScalarToGradeIndexScalarMapping(id1, id2, value);

                if (!gradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary))
                {
                    indexScalarDictionary = new Dictionary<RGaKvIndexPairRecord, T>();
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
                }

                var index = new RGaKvIndexPairRecord(index1, index2);

                indexScalarDictionary.Add(index, scalar);
            }

            return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetRow(ulong index1)
        {
            return GetIndexScalarRecords()
                .FilterRecords((i1, _) => i1 == index1)
                .Select(indexPairScalar => new RGaKvIndexScalarRecord<T>(indexPairScalar.KvIndex2, indexPairScalar.Scalar))
                .CreateLinVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetColumn(ulong index2)
        {
            return ColumnsList.TryGetScalar(index2, out var vector)
                ? vector
                : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetRows()
        {
            var indexPairScalarDictionary = new Dictionary<ulong, Dictionary<ulong, T>>();

            foreach (var (index1, index2, scalar) in GetIndexScalarRecords())
            {
                if (!indexPairScalarDictionary.TryGetValue(index1, out var indexScalarDictionary))
                {
                    indexScalarDictionary = new Dictionary<ulong, T>();
                    indexPairScalarDictionary.Add(index1, indexScalarDictionary);
                }

                indexScalarDictionary.Add(index2, scalar);
            }

            return indexPairScalarDictionary.Select(
                pair => new RGaKvIndexLinVectorStorageRecord<T>(
                    pair.Key, 
                    pair.Value.CreateLinVectorStorage()
                )
            );
        }

        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetRows(Func<ulong, bool> rowIndexFilter)
        {
            var indexPairScalarDictionary = new Dictionary<ulong, Dictionary<ulong, T>>();

            foreach (var (index1, index2, scalar) in GetIndexScalarRecords())
            {
                if (!rowIndexFilter(index1)) continue;

                if (!indexPairScalarDictionary.TryGetValue(index1, out var indexScalarDictionary))
                {
                    indexScalarDictionary = new Dictionary<ulong, T>();
                    indexPairScalarDictionary.Add(index1, indexScalarDictionary);
                }

                indexScalarDictionary.Add(index2, scalar);
            }

            return indexPairScalarDictionary.Select(
                pair => new RGaKvIndexLinVectorStorageRecord<T>(
                    pair.Key, 
                    pair.Value.CreateLinVectorStorage()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetColumns()
        {
            return ColumnsList
                .GetIndexScalarRecords()
                .Select(pair => 
                    new RGaKvIndexLinVectorStorageRecord<T>(pair.KvIndex, pair.Scalar)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetColumns(Func<ulong, bool> columnIndexFilter)
        {
            return ColumnsList
                .GetIndexScalarRecords()
                .Where(r => columnIndexFilter(r.KvIndex))
                .Select(pair => 
                    new RGaKvIndexLinVectorStorageRecord<T>(pair.KvIndex, pair.Scalar)
                );
        }

        public ILinVectorStorage<T> CombineRows(IReadOnlyList<T> scalarList, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            ILinVectorStorage<T> vector = null;

            var count = scalarList.Count;
            for (var rowIndex = 0; rowIndex < count; rowIndex++)
            {
                var rowVector = GetRow((ulong) rowIndex);
                if (rowVector.IsEmpty()) continue;

                var scalingFactor = scalarList[rowIndex];
                var scaledVector = scalingFunc(scalingFactor, rowVector);

                vector = vector is null
                    ? scaledVector
                    : reducingFunc(vector, scaledVector);
            }

            return vector ?? LinVectorEmptyStorage<T>.EmptyStorage;
        }

        public ILinVectorStorage<T> CombineRows(IEnumerable<RGaKvIndexScalarRecord<T>> rowIndexScalarRecords,
            Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc,
            Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            ILinVectorStorage<T> vector = null;

            foreach (var (rowIndex, scalingFactor) in rowIndexScalarRecords)
            {
                var rowVector = GetRow(rowIndex);
                if (rowVector.IsEmpty()) continue;

                var scaledVector = scalingFunc(scalingFactor, rowVector);

                vector = vector is null
                    ? scaledVector
                    : reducingFunc(vector, scaledVector);
            }

            return vector ?? LinVectorEmptyStorage<T>.EmptyStorage;
        }

        public ILinVectorStorage<T> CombineColumns(IReadOnlyList<T> scalarList, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            ILinVectorStorage<T> vector = null;

            var count = scalarList.Count;
            for (var columnIndex = 0; columnIndex < count; columnIndex++)
            {
                if (!ColumnsList.TryGetScalar(columnIndex, out var columnVector))
                    continue;

                var scalingFactor = scalarList[columnIndex];
                var scaledVector = scalingFunc(scalingFactor, columnVector);

                vector = vector is null
                    ? scaledVector
                    : reducingFunc(vector, scaledVector);
            }

            return vector ?? LinVectorEmptyStorage<T>.EmptyStorage;
        }

        public ILinVectorStorage<T> CombineColumns(IEnumerable<RGaKvIndexScalarRecord<T>> columnIndexScalarRecords,
            Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc,
            Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            ILinVectorStorage<T> vector = null;

            foreach (var (columnIndex, scalingFactor) in columnIndexScalarRecords)
            {
                if (!ColumnsList.TryGetScalar(columnIndex, out var columnVector))
                    continue;

                var scaledVector = scalingFunc(scalingFactor, columnVector);

                vector = vector is null
                    ? scaledVector
                    : reducingFunc(vector, scaledVector);
            }

            return vector ?? LinVectorEmptyStorage<T>.EmptyStorage;
        }
    }
}