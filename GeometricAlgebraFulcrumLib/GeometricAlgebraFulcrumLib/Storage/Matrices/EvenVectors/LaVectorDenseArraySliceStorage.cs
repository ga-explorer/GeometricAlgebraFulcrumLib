using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors
{
    public sealed class LaVectorDenseArraySliceStorage<T> :
        LaVectorImmutableDenseStorageBase<T>
    {
        public ILaMatrixEvenStorage<T> SourceGrid { get; }

        public Func<ulong, IndexPairRecord> KeyMapping { get; }

        public Func<ulong, ulong, T> DefaultValueFunc { get; }

        public override int Count { get; }
        

        internal LaVectorDenseArraySliceStorage([NotNull] ILaMatrixEvenStorage<T> array, int count, [NotNull] Func<ulong, IndexPairRecord> indexMapping, [NotNull] Func<ulong, ulong, T> defaultValueFunc)
        {
            SourceGrid = array;
            Count = count;
            KeyMapping = indexMapping;
            DefaultValueFunc = defaultValueFunc;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(ulong index)
        {
            return SourceGrid.GetValue(KeyMapping(index), DefaultValueFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorEvenStorage<T> GetCopy()
        {
            return this;
        }
    }
}