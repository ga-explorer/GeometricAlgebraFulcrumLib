using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices
{
    public sealed class LaMatrixMappedDenseStorage<T> :
        LaMatrixImmutableDenseStorageBase<T>
    {
        public ILaMatrixDenseEvenStorage<T> SourceStorage { get; }
        
        public Func<ulong, ulong, IndexPairRecord> IndexMapping { get; }

        public Func<ulong, ulong, T, T> IndexScalarMapping { get; }

        public override int Count1 
            => SourceStorage.Count1;

        public override int Count2 
            => SourceStorage.Count2;

        
        internal LaMatrixMappedDenseStorage([NotNull] ILaMatrixDenseEvenStorage<T> source, [NotNull] Func<ulong, ulong, IndexPairRecord> indexMapping)
        {
            SourceStorage = source;
            IndexMapping = indexMapping;
            IndexScalarMapping = (_, _, scalar) => scalar;
        }
        
        internal LaMatrixMappedDenseStorage([NotNull] ILaMatrixDenseEvenStorage<T> source, [NotNull] Func<ulong, ulong, T, T> indexScalarMapping)
        {
            SourceStorage = source;
            IndexMapping = (index1, index2) => new IndexPairRecord(index1, index2);
            IndexScalarMapping = indexScalarMapping;
        }
        
        internal LaMatrixMappedDenseStorage([NotNull] ILaMatrixDenseEvenStorage<T> source, [NotNull] Func<ulong, ulong, IndexPairRecord> indexMapping, [NotNull] Func<ulong, ulong, T, T> indexScalarMapping)
        {
            SourceStorage = source;
            IndexMapping = indexMapping;
            IndexScalarMapping = indexScalarMapping;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(ulong index1, ulong index2)
        {
            var index = IndexMapping(index1, index2);

            return IndexScalarMapping(index.Index1, index.Index2, SourceStorage.GetScalar(index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixEvenStorage<T> GetCopy()
        {
            return this;
        }
    }
}