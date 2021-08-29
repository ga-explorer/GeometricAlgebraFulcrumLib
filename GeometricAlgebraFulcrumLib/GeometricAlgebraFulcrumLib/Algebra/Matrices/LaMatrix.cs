using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Algebra.Matrices
{
    public sealed record LaMatrix<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<T> operator -(LaMatrix<T> array)
        {
            var processor = array.ScalarsGridProcessor;

            return new LaMatrix<T>(
                processor,
                processor.Negative(array.MatrixStorage)
            );
        }


        public ILaProcessor<T> ScalarsGridProcessor { get; }

        public ILaMatrixEvenStorage<T> MatrixStorage { get; }

        public int RowsCount 
            => MatrixStorage.GetDenseCount1();

        public int ColumnsCount 
            => MatrixStorage.GetDenseCount2();

        public T this[int i, int j] 
            => MatrixStorage.GetValue((ulong) i, (ulong) j, ScalarsGridProcessor.ScalarZero);


        internal LaMatrix([NotNull] ILaProcessor<T> arrayProcessor, [NotNull] ILaMatrixEvenStorage<T> arrayStorage)
        {
            ScalarsGridProcessor = arrayProcessor;
            MatrixStorage = arrayStorage;
        }
    }

    public sealed record LaMatrix<TMatrix, TScalar>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator -(LaMatrix<TMatrix, TScalar> m1)
        {
            var processor = m1.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.MatrixNegative(m1.MatrixStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator +(LaMatrix<TMatrix, TScalar> m1, int m2)
        {
            var processor = m1.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.AddMatrixScalar(
                    m1.MatrixStorage,
                    processor.GetScalarFromInteger(m2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator +(int m1, LaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.AddScalarMatrix(
                    processor.GetScalarFromInteger(m1),
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator +(LaMatrix<TMatrix, TScalar> m1, double m2)
        {
            var processor = m1.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.AddMatrixScalar(
                    m1.MatrixStorage,
                    processor.GetScalarFromFloat64(m2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator +(double m1, LaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.AddScalarMatrix(
                    processor.GetScalarFromFloat64(m1),
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator +(LaMatrix<TMatrix, TScalar> m1, TScalar m2)
        {
            var processor = m1.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.AddMatrixScalar(
                    m1.MatrixStorage,
                    m2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator +(TScalar m1, LaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.AddScalarMatrix(
                    m1,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator +(LaMatrix<TMatrix, TScalar> m1, TMatrix m2)
        {
            var processor = m1.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.AddMatrices(
                    m1.MatrixStorage,
                    m2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator +(TMatrix m1, LaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.AddMatrices(
                    m1,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator +(LaMatrix<TMatrix, TScalar> m1, LaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m1.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.AddMatrices(
                    m1.MatrixStorage,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator -(LaMatrix<TMatrix, TScalar> m1, int m2)
        {
            var processor = m1.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractMatrixScalar(
                    m1.MatrixStorage,
                    processor.GetScalarFromInteger(m2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator -(int m1, LaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractScalarMatrix(
                    processor.GetScalarFromInteger(m1),
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator -(LaMatrix<TMatrix, TScalar> m1, double m2)
        {
            var processor = m1.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractMatrixScalar(
                    m1.MatrixStorage,
                    processor.GetScalarFromFloat64(m2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator -(double m1, LaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractScalarMatrix(
                    processor.GetScalarFromFloat64(m1),
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator -(LaMatrix<TMatrix, TScalar> m1, TScalar m2)
        {
            var processor = m1.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractMatrixScalar(
                    m1.MatrixStorage,
                    m2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator -(TScalar m1, LaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractScalarMatrix(
                    m1,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator -(LaMatrix<TMatrix, TScalar> m1, TMatrix m2)
        {
            var processor = m1.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractMatrices(
                    m1.MatrixStorage,
                    m2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator -(TMatrix m1, LaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractMatrices(
                    m1,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator -(LaMatrix<TMatrix, TScalar> m1, LaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m1.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractMatrices(
                    m1.MatrixStorage,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator *(LaMatrix<TMatrix, TScalar> m1, int m2)
        {
            var processor = m1.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesMatrixScalar(
                    m1.MatrixStorage,
                    processor.GetScalarFromInteger(m2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator *(int m1, LaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesScalarMatrix(
                    processor.GetScalarFromInteger(m1),
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator *(LaMatrix<TMatrix, TScalar> m1, double m2)
        {
            var processor = m1.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesMatrixScalar(
                    m1.MatrixStorage,
                    processor.GetScalarFromFloat64(m2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator *(double m1, LaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesScalarMatrix(
                    processor.GetScalarFromFloat64(m1),
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator *(LaMatrix<TMatrix, TScalar> m1, TScalar m2)
        {
            var processor = m1.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesMatrixScalar(
                    m1.MatrixStorage,
                    m2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator *(TScalar m1, LaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesScalarMatrix(
                    m1,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator *(LaMatrix<TMatrix, TScalar> m1, TMatrix m2)
        {
            var processor = m1.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesMatrices(
                    m1.MatrixStorage,
                    m2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator *(TMatrix m1, LaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesMatrices(
                    m1,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> operator *(LaMatrix<TMatrix, TScalar> m1, LaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m1.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesMatrices(
                    m1.MatrixStorage,
                    m2.MatrixStorage
                )
            );
        }


        public ILaProcessor<TMatrix, TScalar> MatrixProcessor { get; }

        public TMatrix MatrixStorage { get; }

        public int RowsCount 
            => MatrixProcessor.GetDenseRowsCount(MatrixStorage);

        public int ColumnsCount 
            => MatrixProcessor.GetDenseColumnsCount(MatrixStorage);

        public TScalar this[int i, int j] 
            => MatrixProcessor.GetScalar(MatrixStorage, i, j);


        internal LaMatrix([NotNull] ILaProcessor<TMatrix, TScalar> matrixProcessor, [NotNull] TMatrix matrixStorage)
        {
            MatrixProcessor = matrixProcessor;
            MatrixStorage = matrixStorage;
        }
    }
}