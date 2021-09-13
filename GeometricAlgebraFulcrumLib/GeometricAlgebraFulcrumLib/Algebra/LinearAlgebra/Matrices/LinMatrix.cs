using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Matrices
{
    public sealed record LinMatrix<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator -(LinMatrix<T> matrix1)
        {
            var processor = matrix1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Negative(matrix1.MatrixStorage)
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator +(LinMatrix<T> v1, Scalar<T> v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Add(v1.MatrixStorage, v2.ScalarValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator +(Scalar<T> v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Add(v1.ScalarValue, v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator +(LinMatrix<T> v1, T v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Add(v1.MatrixStorage, v2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator +(T v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Add(v1, v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator +(LinMatrix<T> v1, int v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Add(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator +(int v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator +(LinMatrix<T> v1, uint v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Add(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator +(uint v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator +(LinMatrix<T> v1, long v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Add(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator +(long v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator +(LinMatrix<T> v1, ulong v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Add(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator +(ulong v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator +(LinMatrix<T> v1, float v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Add(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator +(float v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator +(LinMatrix<T> v1, double v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Add(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator +(double v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator +(LinMatrix<T> matrix1, LinMatrix<T> matrix2)
        {
            var processor = matrix1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Add(matrix1.MatrixStorage, matrix2.MatrixStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator -(LinMatrix<T> v1, Scalar<T> v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Subtract(v1.MatrixStorage, v2.ScalarValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator -(Scalar<T> v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Subtract(v1.ScalarValue, v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator -(LinMatrix<T> v1, T v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Subtract(v1.MatrixStorage, v2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator -(T v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Subtract(v1, v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator -(LinMatrix<T> v1, int v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Subtract(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator -(int v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator -(LinMatrix<T> v1, uint v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Subtract(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator -(uint v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator -(LinMatrix<T> v1, long v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Subtract(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator -(long v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator -(LinMatrix<T> v1, ulong v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Subtract(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator -(ulong v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator -(LinMatrix<T> v1, float v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Subtract(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator -(float v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator -(LinMatrix<T> v1, double v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Subtract(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator -(double v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator -(LinMatrix<T> matrix1, LinMatrix<T> matrix2)
        {
            var processor = matrix1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Subtract(matrix1.MatrixStorage, matrix2.MatrixStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator *(LinMatrix<T> v1, Scalar<T> v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Times(v1.MatrixStorage, v2.ScalarValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator *(Scalar<T> v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Times(v1.ScalarValue, v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator *(LinMatrix<T> v1, T v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Times(v1.MatrixStorage, v2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator *(T v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Times(v1, v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator *(LinMatrix<T> v1, int v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Times(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator *(int v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator *(LinMatrix<T> v1, uint v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Times(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator *(uint v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator *(LinMatrix<T> v1, long v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Times(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator *(long v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator *(LinMatrix<T> v1, ulong v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Times(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator *(ulong v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator *(LinMatrix<T> v1, float v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Times(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator *(float v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator *(LinMatrix<T> v1, double v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Times(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator *(double v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(LinMatrix<T> matrix1, LinVector<T> matrix2)
        {
            var processor = matrix1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.MatrixProduct(matrix1.MatrixStorage, matrix2.VectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> operator *(LinVector<T> matrix1, LinMatrix<T> matrix2)
        {
            var processor = matrix2.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.MatrixProduct(matrix1.VectorStorage, matrix2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator *(LinMatrix<T> matrix1, LinMatrix<T> matrix2)
        {
            var processor = matrix1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.MatrixProduct(matrix1.MatrixStorage, matrix2.MatrixStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator /(LinMatrix<T> v1, Scalar<T> v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Divide(v1.MatrixStorage, v2.ScalarValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator /(Scalar<T> v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Divide(v1.ScalarValue, v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator /(LinMatrix<T> v1, T v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Divide(v1.MatrixStorage, v2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator /(T v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Divide(v1, v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator /(LinMatrix<T> v1, int v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Divide(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator /(int v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Divide(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator /(LinMatrix<T> v1, uint v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Divide(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator /(uint v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Divide(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator /(LinMatrix<T> v1, long v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Divide(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator /(long v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Divide(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator /(LinMatrix<T> v1, ulong v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Divide(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator /(ulong v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Divide(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator /(LinMatrix<T> v1, float v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Divide(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator /(float v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Divide(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator /(LinMatrix<T> v1, double v2)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Divide(v1.MatrixStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> operator /(double v1, LinMatrix<T> v2)
        {
            var processor = v2.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                processor.Divide(processor.GetScalarFromNumber(v1), v2.MatrixStorage)
            );
        }
        
        
        public ILinearAlgebraProcessor<T> LinearProcessor { get; }

        public ILinMatrixStorage<T> MatrixStorage { get; }

        public int RowsCount 
            => MatrixStorage.GetDenseCount1();

        public int ColumnsCount 
            => MatrixStorage.GetDenseCount2();

        public T this[int i, int j] 
            => MatrixStorage.GetScalar((ulong) i, (ulong) j, LinearProcessor.ScalarZero);


        internal LinMatrix([NotNull] ILinearAlgebraProcessor<T> linearProcessor, [NotNull] ILinMatrixStorage<T> arrayStorage)
        {
            LinearProcessor = linearProcessor;
            MatrixStorage = arrayStorage;
        }
    }

    public sealed record LinMatrix<TMatrix, TScalar>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator -(LinMatrix<TMatrix, TScalar> m1)
        {
            var processor = m1.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.MatrixNegative(m1.MatrixStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator +(LinMatrix<TMatrix, TScalar> m1, int m2)
        {
            var processor = m1.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.AddMatrixScalar(
                    m1.MatrixStorage,
                    processor.GetScalarFromNumber(m2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator +(int m1, LinMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.AddScalarMatrix(
                    processor.GetScalarFromNumber(m1),
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator +(LinMatrix<TMatrix, TScalar> m1, double m2)
        {
            var processor = m1.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.AddMatrixScalar(
                    m1.MatrixStorage,
                    processor.GetScalarFromNumber(m2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator +(double m1, LinMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.AddScalarMatrix(
                    processor.GetScalarFromNumber(m1),
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator +(LinMatrix<TMatrix, TScalar> m1, TScalar m2)
        {
            var processor = m1.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.AddMatrixScalar(
                    m1.MatrixStorage,
                    m2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator +(TScalar m1, LinMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.AddScalarMatrix(
                    m1,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator +(LinMatrix<TMatrix, TScalar> m1, TMatrix m2)
        {
            var processor = m1.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.AddMatrices(
                    m1.MatrixStorage,
                    m2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator +(TMatrix m1, LinMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.AddMatrices(
                    m1,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator +(LinMatrix<TMatrix, TScalar> m1, LinMatrix<TMatrix, TScalar> m2)
        {
            var processor = m1.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.AddMatrices(
                    m1.MatrixStorage,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator -(LinMatrix<TMatrix, TScalar> m1, int m2)
        {
            var processor = m1.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractMatrixScalar(
                    m1.MatrixStorage,
                    processor.GetScalarFromNumber(m2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator -(int m1, LinMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractScalarMatrix(
                    processor.GetScalarFromNumber(m1),
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator -(LinMatrix<TMatrix, TScalar> m1, double m2)
        {
            var processor = m1.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractMatrixScalar(
                    m1.MatrixStorage,
                    processor.GetScalarFromNumber(m2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator -(double m1, LinMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractScalarMatrix(
                    processor.GetScalarFromNumber(m1),
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator -(LinMatrix<TMatrix, TScalar> m1, TScalar m2)
        {
            var processor = m1.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractMatrixScalar(
                    m1.MatrixStorage,
                    m2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator -(TScalar m1, LinMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractScalarMatrix(
                    m1,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator -(LinMatrix<TMatrix, TScalar> m1, TMatrix m2)
        {
            var processor = m1.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractMatrices(
                    m1.MatrixStorage,
                    m2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator -(TMatrix m1, LinMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractMatrices(
                    m1,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator -(LinMatrix<TMatrix, TScalar> m1, LinMatrix<TMatrix, TScalar> m2)
        {
            var processor = m1.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractMatrices(
                    m1.MatrixStorage,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator *(LinMatrix<TMatrix, TScalar> m1, int m2)
        {
            var processor = m1.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesMatrixScalar(
                    m1.MatrixStorage,
                    processor.GetScalarFromNumber(m2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator *(int m1, LinMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesScalarMatrix(
                    processor.GetScalarFromNumber(m1),
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator *(LinMatrix<TMatrix, TScalar> m1, double m2)
        {
            var processor = m1.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesMatrixScalar(
                    m1.MatrixStorage,
                    processor.GetScalarFromNumber(m2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator *(double m1, LinMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesScalarMatrix(
                    processor.GetScalarFromNumber(m1),
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator *(LinMatrix<TMatrix, TScalar> m1, TScalar m2)
        {
            var processor = m1.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesMatrixScalar(
                    m1.MatrixStorage,
                    m2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator *(TScalar m1, LinMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesScalarMatrix(
                    m1,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator *(LinMatrix<TMatrix, TScalar> m1, TMatrix m2)
        {
            var processor = m1.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesMatrices(
                    m1.MatrixStorage,
                    m2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator *(TMatrix m1, LinMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesMatrices(
                    m1,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> operator *(LinMatrix<TMatrix, TScalar> m1, LinMatrix<TMatrix, TScalar> m2)
        {
            var processor = m1.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesMatrices(
                    m1.MatrixStorage,
                    m2.MatrixStorage
                )
            );
        }


        public ILinearAlgebraProcessor<TMatrix, TScalar> MatrixProcessor { get; }

        public TMatrix MatrixStorage { get; }

        public int RowsCount 
            => MatrixProcessor.GetDenseRowsCount(MatrixStorage);

        public int ColumnsCount 
            => MatrixProcessor.GetDenseColumnsCount(MatrixStorage);

        public TScalar this[int i, int j] 
            => MatrixProcessor.GetScalar(MatrixStorage, i, j);


        internal LinMatrix([NotNull] ILinearAlgebraProcessor<TMatrix, TScalar> matrixProcessor, [NotNull] TMatrix matrixStorage)
        {
            MatrixProcessor = matrixProcessor;
            MatrixStorage = matrixStorage;
        }
    }
}