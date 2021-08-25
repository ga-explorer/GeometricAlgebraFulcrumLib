using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.ScalarsGrids;
using GeometricAlgebraFulcrumLib.Processing.ScalarsGrids.Unary;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Algebra.Arrays
{
    public sealed record GaLaVector<T> :
        IReadOnlyList<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaVector<T> operator -(GaLaVector<T> v1)
        {
            var processor = v1.ScalarsGridProcessor;

            return new GaLaVector<T>(
                processor,
                processor.Negative(v1.VectorStorage)
            );
        }


        public IGaScalarsGridProcessor<T> ScalarsGridProcessor { get; }

        public IGaListEven<T> VectorStorage { get; }

        public int Count 
            => VectorStorage.GetSparseCount();

        public T this[int index] 
            => VectorStorage.GetValue((ulong) index);
        

        internal GaLaVector([NotNull] IGaScalarsGridProcessor<T> arrayProcessor, [NotNull] IGaListEven<T> vectorStorage)
        {
            ScalarsGridProcessor = arrayProcessor;
            VectorStorage = vectorStorage;
        }


        public IEnumerator<T> GetEnumerator()
        {
            return VectorStorage.GetValues().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}