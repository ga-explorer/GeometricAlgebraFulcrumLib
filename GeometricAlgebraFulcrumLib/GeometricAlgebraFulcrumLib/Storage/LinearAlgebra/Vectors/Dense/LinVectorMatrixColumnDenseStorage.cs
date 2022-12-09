using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense
{
    public sealed class LinVectorMatrixColumnDenseStorage<T> :
        LinVectorImmutableDenseStorageBase<T>
    {
        public ILinMatrixStorage<T> SourceMatrix { get; }

        public ulong ColumnIndex { get; set; }

        public Func<ulong, ulong, T> DefaultValueFunc { get; }

        public override int Count 
            => SourceMatrix.GetDenseCount2();
        

        internal LinVectorMatrixColumnDenseStorage([NotNull] ILinMatrixStorage<T> array, ulong index2, [NotNull] Func<ulong, ulong, T> defaultValueFunc)
        {
            SourceMatrix = array;
            ColumnIndex = index2;
            DefaultValueFunc = defaultValueFunc;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(ulong index)
        {
            return SourceMatrix.GetScalar(index, ColumnIndex, DefaultValueFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorStorage<T> GetCopy()
        {
            return new LinVectorMatrixColumnDenseStorage<T>(SourceMatrix, ColumnIndex, DefaultValueFunc);
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