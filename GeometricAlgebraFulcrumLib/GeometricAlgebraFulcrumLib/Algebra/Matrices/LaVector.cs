using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Algebra.Matrices
{
    public sealed record LaVector<T> :
        IReadOnlyList<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> operator -(LaVector<T> v1)
        {
            var processor = v1.ScalarsGridProcessor;

            return new LaVector<T>(
                processor,
                processor.Negative(v1.VectorStorage)
            );
        }


        public ILaProcessor<T> ScalarsGridProcessor { get; }

        public ILaVectorEvenStorage<T> VectorStorage { get; }

        public int Count 
            => VectorStorage.GetSparseCount();

        public T this[int index] 
            => VectorStorage.GetScalar((ulong) index);
        

        internal LaVector([NotNull] IScalarProcessor<T> arrayProcessor, [NotNull] ILaVectorEvenStorage<T> vectorStorage)
        {
            ScalarsGridProcessor = (ILaProcessor<T>) arrayProcessor;
            VectorStorage = vectorStorage;
        }
        
        internal LaVector([NotNull] ILaProcessor<T> arrayProcessor, [NotNull] ILaVectorEvenStorage<T> vectorStorage)
        {
            ScalarsGridProcessor = arrayProcessor;
            VectorStorage = vectorStorage;
        }


        public IEnumerator<T> GetEnumerator()
        {
            return VectorStorage.GetScalars().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}