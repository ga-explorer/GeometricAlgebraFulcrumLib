using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices
{
    public sealed class LaMatrixMappedSparseStorage<T> :
        ILaMatrixSparseEvenStorage<T>
    {
        public ILaMatrixSparseEvenStorage<T> SourceStorage { get; }
        
        public IReadOnlyList<IndexPairRecord> IndexList { get; }

        public Func<ulong, ulong> IndexMapping { get; }

        public Func<ulong, T, T> IndexScalarMapping { get; }

        
        internal LaMatrixMappedSparseStorage([NotNull] ILaMatrixSparseEvenStorage<T> source, [NotNull] IReadOnlyList<IndexPairRecord> indexList, [NotNull] Func<ulong, ulong> indexMapping)
        {
            SourceStorage = source;
            IndexList = indexList;
            IndexMapping = indexMapping;
            IndexScalarMapping = (_, scalar) => scalar;
        }
        
        internal LaMatrixMappedSparseStorage([NotNull] ILaMatrixSparseEvenStorage<T> source, [NotNull] IReadOnlyList<IndexPairRecord> indexList, [NotNull] Func<ulong, T, T> indexScalarMapping)
        {
            SourceStorage = source;
            IndexList = indexList;
            IndexMapping = index => index;
            IndexScalarMapping = indexScalarMapping;
        }
        
        internal LaMatrixMappedSparseStorage([NotNull] ILaMatrixSparseEvenStorage<T> source, [NotNull] IReadOnlyList<IndexPairRecord> indexList, [NotNull] Func<ulong, ulong> indexMapping, [NotNull] Func<ulong, T, T> indexScalarMapping)
        {
            SourceStorage = source;
            IndexList = indexList;
            IndexMapping = indexMapping;
            IndexScalarMapping = indexScalarMapping;
        }

        public bool IsEmpty()
        {
            throw new NotImplementedException();
        }

        public int GetSparseCount()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetScalars()
        {
            throw new NotImplementedException();
        }

        public int GetSparseCount1()
        {
            throw new NotImplementedException();
        }

        public int GetSparseCount2()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ulong> GetIndices1()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ulong> GetIndices2()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IndexPairRecord> GetIndices()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IndexPairScalarRecord<T>> GetIndexScalarRecords()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IndexPairRecord> GetEmptyIndices(ulong maxIndex1, ulong maxIndex2)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IndexPairRecord> GetEmptyIndices(IndexPairRecord maxIndex)
        {
            throw new NotImplementedException();
        }

        public T GetScalar(ulong index1, ulong index2)
        {
            throw new NotImplementedException();
        }

        public T GetScalar(IndexPairRecord index)
        {
            throw new NotImplementedException();
        }

        public bool ContainsIndex(ulong index1, ulong index2)
        {
            throw new NotImplementedException();
        }

        public bool ContainsIndex(IndexPairRecord index)
        {
            throw new NotImplementedException();
        }

        public ulong GetMinIndex1()
        {
            throw new NotImplementedException();
        }

        public ulong GetMinIndex2()
        {
            throw new NotImplementedException();
        }

        public IndexPairRecord GetMinIndex()
        {
            throw new NotImplementedException();
        }

        public ulong GetMaxIndex1()
        {
            throw new NotImplementedException();
        }

        public ulong GetMaxIndex2()
        {
            throw new NotImplementedException();
        }

        public IndexPairRecord GetMaxIndex()
        {
            throw new NotImplementedException();
        }

        public bool TryGetScalar(ulong index1, ulong index2, out T scalar)
        {
            throw new NotImplementedException();
        }

        public bool TryGetScalar(IndexPairRecord index, out T scalar)
        {
            throw new NotImplementedException();
        }

        public ILaMatrixEvenStorage<T> GetCopy()
        {
            throw new NotImplementedException();
        }

        public ILaMatrixEvenStorage<T> MapIndices(Func<ulong, ulong, IndexPairRecord> indexMapping)
        {
            throw new NotImplementedException();
        }

        public ILaMatrixEvenStorage<T2> MapScalars<T2>(Func<T, T2> scalarMapping)
        {
            throw new NotImplementedException();
        }

        public ILaMatrixEvenStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexScalarMapping)
        {
            throw new NotImplementedException();
        }

        public ILaMatrixEvenStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter)
        {
            throw new NotImplementedException();
        }

        public ILaMatrixEvenStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexScalarFilter)
        {
            throw new NotImplementedException();
        }

        public ILaMatrixEvenStorage<T> FilterByScalar(Func<T, bool> scalarFilter)
        {
            throw new NotImplementedException();
        }

        public ILaMatrixEvenStorage<T> Transpose()
        {
            throw new NotImplementedException();
        }

        public bool TryGetCompactStorage(out ILaMatrixEvenStorage<T> evenStorage)
        {
            throw new NotImplementedException();
        }

        public ILaMatrixGradedStorage<T> ToGradedStorage(Func<ulong, ulong, GradeIndexPairRecord> evenIndexToGradeIndexMapping)
        {
            throw new NotImplementedException();
        }

        public ILaMatrixGradedStorage<T> ToGradedStorage(Func<ulong, ulong, T, GradeIndexPairScalarRecord<T>> evenIndexScalarToGradeIndexScalarMapping)
        {
            throw new NotImplementedException();
        }

        public ILaVectorEvenStorage<T> GetRow(ulong index1)
        {
            throw new NotImplementedException();
        }

        public ILaVectorEvenStorage<T> GetColumn(ulong index2)
        {
            throw new NotImplementedException();
        }
    }
}