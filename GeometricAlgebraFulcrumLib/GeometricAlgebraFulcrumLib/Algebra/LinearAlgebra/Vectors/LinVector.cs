using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Vectors
{
    public sealed record LinVector<T> :
        IReadOnlyList<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator -(LinVector<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Negative(v1.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator +(LinVector<T> v1, Scalar<T> v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Add(v1.VectorStorage, v2.ScalarValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator +(Scalar<T> v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Add(v1.ScalarValue, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator +(LinVector<T> v1, T v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Add(v1.VectorStorage, v2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator +(T v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Add(v1, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator +(LinVector<T> v1, int v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Add(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator +(int v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator +(LinVector<T> v1, uint v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Add(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator +(uint v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator +(LinVector<T> v1, long v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Add(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator +(long v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator +(LinVector<T> v1, ulong v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Add(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator +(ulong v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator +(LinVector<T> v1, float v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Add(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator +(float v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator +(LinVector<T> v1, double v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Add(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator +(double v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator +(LinVector<T> v1, LinVector<T> v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Add(v1.VectorStorage, v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator -(LinVector<T> v1, Scalar<T> v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, v2.ScalarValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator -(Scalar<T> v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Subtract(v1.ScalarValue, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator -(LinVector<T> v1, T v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, v2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator -(T v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Subtract(v1, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator -(LinVector<T> v1, int v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator -(int v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator -(LinVector<T> v1, uint v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator -(uint v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator -(LinVector<T> v1, long v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator -(long v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator -(LinVector<T> v1, ulong v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator -(ulong v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator -(LinVector<T> v1, float v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator -(float v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator -(LinVector<T> v1, double v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator -(double v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator -(LinVector<T> v1, LinVector<T> v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(LinVector<T> v1, Scalar<T> v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Times(v1.VectorStorage, v2.ScalarValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(Scalar<T> v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Times(v1.ScalarValue, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(LinVector<T> v1, T v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Times(v1.VectorStorage, v2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(T v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Times(v1, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(LinVector<T> v1, int v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Times(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(int v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(LinVector<T> v1, uint v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Times(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(uint v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(LinVector<T> v1, long v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Times(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(long v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(LinVector<T> v1, ulong v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Times(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(ulong v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(LinVector<T> v1, float v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Times(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(float v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(LinVector<T> v1, double v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Times(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(double v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(LinVector<T> v1, LinVector<T> v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Times(v1.VectorStorage, v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(LinVector<T> v1, Scalar<T> v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, v2.ScalarValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(Scalar<T> v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Divide(v1.ScalarValue, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(LinVector<T> v1, T v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, v2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(T v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Divide(v1, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(LinVector<T> v1, int v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(int v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Divide(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(LinVector<T> v1, uint v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(uint v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Divide(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(LinVector<T> v1, long v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(long v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Divide(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(LinVector<T> v1, ulong v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(ulong v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Divide(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(LinVector<T> v1, float v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(float v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Divide(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(LinVector<T> v1, double v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(double v1, LinVector<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Divide(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator /(LinVector<T> v1, LinVector<T> v2)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, v2.VectorStorage)
            );
        }

        
        public ILinearAlgebraProcessor<T> LinearProcessor { get; }

        public ILinVectorStorage<T> VectorStorage { get; }

        public int Count 
            => VectorStorage.GetDenseCount();

        public T this[int index] 
            => VectorStorage.GetScalar((ulong) index, LinearProcessor.ScalarZero);
        

        internal LinVector([NotNull] IScalarAlgebraProcessor<T> linearProcessor, [NotNull] ILinVectorStorage<T> vectorStorage)
        {
            LinearProcessor = (ILinearAlgebraProcessor<T>) linearProcessor;
            VectorStorage = vectorStorage;
        }
        
        internal LinVector([NotNull] ILinearAlgebraProcessor<T> linearProcessor, [NotNull] ILinVectorStorage<T> vectorStorage)
        {
            LinearProcessor = linearProcessor;
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