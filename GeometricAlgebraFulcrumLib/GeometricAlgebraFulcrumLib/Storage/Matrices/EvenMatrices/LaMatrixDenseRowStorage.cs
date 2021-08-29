using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices
{
    public sealed class LaMatrixDenseRowStorage<T> :
        LaMatrixImmutableDenseStorageBase<T>
    {
        public ILaVectorDenseEvenStorage<T> SourceList { get; }

        public override int Count1 
            => 1;

        public override int Count2 
            => SourceList.Count;


        internal LaMatrixDenseRowStorage([NotNull] ILaVectorDenseEvenStorage<T> sourceList)
        {
            SourceList = sourceList;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(ulong index1, ulong index2)
        {
            return index1 == 0
                ? SourceList.GetScalar(index2)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixEvenStorage<T> GetCopy()
        {
            return this;
        }
    }
}