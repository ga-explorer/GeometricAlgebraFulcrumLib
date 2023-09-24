using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense
{
    public sealed class LinMatrixMappedDenseStorage<T> :
        LinMatrixImmutableDenseStorageBase<T>
    {
        public ILinMatrixDenseStorage<T> SourceStorage { get; }
        
        public Func<ulong, ulong, RGaKvIndexPairRecord> IndexMapping { get; }

        public Func<ulong, ulong, T, T> IndexScalarMapping { get; }

        public override int Count1 
            => SourceStorage.Count1;

        public override int Count2 
            => SourceStorage.Count2;


        internal LinMatrixMappedDenseStorage(ILinMatrixDenseStorage<T> source, Func<ulong, ulong, RGaKvIndexPairRecord> indexMapping)
        {
            SourceStorage = source;
            IndexMapping = indexMapping;
            IndexScalarMapping = (_, _, scalar) => scalar;
        }
        
        internal LinMatrixMappedDenseStorage(ILinMatrixDenseStorage<T> source, Func<ulong, ulong, T, T> indexScalarMapping)
        {
            SourceStorage = source;
            IndexMapping = (index1, index2) => new RGaKvIndexPairRecord(index1, index2);
            IndexScalarMapping = indexScalarMapping;
        }
        
        internal LinMatrixMappedDenseStorage(ILinMatrixDenseStorage<T> source, Func<ulong, ulong, RGaKvIndexPairRecord> indexMapping, Func<ulong, ulong, T, T> indexScalarMapping)
        {
            SourceStorage = source;
            IndexMapping = indexMapping;
            IndexScalarMapping = indexScalarMapping;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(ulong index1, ulong index2)
        {
            var index = IndexMapping(index1, index2);

            return IndexScalarMapping(index.KvIndex1, index.KvIndex2, SourceStorage.GetScalar(index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetCopy()
        {
            return this;
        }
        
        public override IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetDenseRows(IEnumerable<ulong> rowIndexList)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetDenseColumns(IEnumerable<ulong> columnIndexList)
        {
            throw new NotImplementedException();
        }
    }
}