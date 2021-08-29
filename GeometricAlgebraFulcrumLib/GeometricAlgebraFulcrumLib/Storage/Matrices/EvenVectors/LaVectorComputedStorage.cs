using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors
{
    public sealed class LaVectorComputedStorage<T> :
        LaVectorImmutableDenseStorageBase<T>
    {
        public Func<ulong, T> IndexToScalarMapping { get; }

        public override int Count { get; }
        

        internal LaVectorComputedStorage(int count, [NotNull] Func<ulong, T> indexScalarMapping)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            Count = count;
            IndexToScalarMapping = indexScalarMapping;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(ulong index)
        {
            return index < (ulong) Count
                ? IndexToScalarMapping(index)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorEvenStorage<T> GetCopy()
        {
            return this;
        }
    }
}