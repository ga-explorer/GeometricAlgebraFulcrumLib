using System;
using System.Diagnostics.CodeAnalysis;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors
{
    public sealed class LaVectorMappedDenseStorage<T> :
        LaVectorImmutableDenseStorageBase<T>
    {
        public ILaVectorDenseEvenStorage<T> SourceStorage { get; }
        
        public Func<ulong, ulong> IndexMapping { get; }

        public Func<ulong, T, T> IndexScalarMapping { get; }

        public override int Count 
            => SourceStorage.Count;
        

        internal LaVectorMappedDenseStorage([NotNull] ILaVectorDenseEvenStorage<T> source, [NotNull] Func<ulong, ulong> indexMapping)
        {
            SourceStorage = source;
            IndexMapping = indexMapping;
            IndexScalarMapping = (_, scalar) => scalar;
        }
        
        internal LaVectorMappedDenseStorage([NotNull] ILaVectorDenseEvenStorage<T> source, [NotNull] Func<ulong, T, T> indexScalarMapping)
        {
            SourceStorage = source;
            IndexMapping = index => index;
            IndexScalarMapping = indexScalarMapping;
        }
        
        internal LaVectorMappedDenseStorage([NotNull] ILaVectorDenseEvenStorage<T> source, [NotNull] Func<ulong, ulong> indexMapping, [NotNull] Func<ulong, T, T> indexScalarMapping)
        {
            SourceStorage = source;
            IndexMapping = indexMapping;
            IndexScalarMapping = indexScalarMapping;
        }


        public override T GetScalar(ulong index)
        {
            index = IndexMapping(index);

            return IndexScalarMapping(index, SourceStorage.GetScalar(index));
        }

        public override ILaVectorEvenStorage<T> GetCopy()
        {
            return this;
        }
    }
}