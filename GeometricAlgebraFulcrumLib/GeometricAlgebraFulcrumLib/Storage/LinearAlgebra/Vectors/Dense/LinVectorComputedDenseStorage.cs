using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense
{
    public sealed class LinVectorComputedDenseStorage<T> :
        LinVectorImmutableDenseStorageBase<T>
    {
        public Func<ulong, T> IndexToScalarMapping { get; }

        public override int Count { get; }


        internal LinVectorComputedDenseStorage(int count, Func<ulong, T> indexScalarMapping)
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
        public override ILinVectorStorage<T> GetCopy()
        {
            return this;
        }

        public override ILinVectorDenseStorage<T> GetDensePermutation(Func<ulong, ulong> indexMapping)
        {
            var scalarsArray = new T[Count];

            for (var index = 0UL; index < (ulong) Count; index++)
                scalarsArray[indexMapping(index)] = IndexToScalarMapping(index);

            return new LinVectorArrayStorage<T>(scalarsArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerator<T> GetEnumerator()
        {
            return ((ulong) Count).GetRange().Select(GetScalar).GetEnumerator();
        }
    }
}