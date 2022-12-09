using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense
{
    public sealed class LinVectorMatrixRowDenseStorage<T> :
        LinVectorImmutableDenseStorageBase<T>
    {
        public ILinMatrixStorage<T> SourceMatrix { get; }

        public ulong RowIndex { get; set; }

        public Func<ulong, ulong, T> DefaultScalarFunc { get; }

        public override int Count 
            => SourceMatrix.GetDenseCount2();
        
        
        internal LinVectorMatrixRowDenseStorage([NotNull] ILinMatrixStorage<T> array, ulong index1, [NotNull] Func<ulong, ulong, T> defaultValueFunc)
        {
            SourceMatrix = array;
            RowIndex = index1;
            DefaultScalarFunc = defaultValueFunc;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(ulong index)
        {
            return SourceMatrix.GetScalar(RowIndex, index, DefaultScalarFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorStorage<T> GetCopy()
        {
            return new LinVectorMatrixRowDenseStorage<T>(SourceMatrix, RowIndex, DefaultScalarFunc);
        }
 
        public override ILinVectorDenseStorage<T> GetDensePermutation(Func<ulong, ulong> indexMapping)
        {
            var scalarsArray = new T[Count];

            for (var index = 0UL; index < (ulong) Count; index++)
                scalarsArray[indexMapping(index)] = GetScalar(index);

            return new LinVectorArrayStorage<T>(scalarsArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerator<T> GetEnumerator()
        {
            return ((ulong) Count).GetRange().Select(GetScalar).GetEnumerator();
        }
    }
}