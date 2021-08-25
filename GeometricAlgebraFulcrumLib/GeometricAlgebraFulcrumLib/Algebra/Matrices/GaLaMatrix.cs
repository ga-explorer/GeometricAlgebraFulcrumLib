using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Matrices;

namespace GeometricAlgebraFulcrumLib.Algebra.Matrices
{
    public sealed record GaLaMatrix<TMatrix, TScalar>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator -(GaLaMatrix<TMatrix, TScalar> m1)
        {
            var processor = m1.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.MatrixNegative(m1.MatrixStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator +(GaLaMatrix<TMatrix, TScalar> m1, int m2)
        {
            var processor = m1.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.AddMatrixScalar(
                    m1.MatrixStorage,
                    processor.IntegerToScalar(m2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator +(int m1, GaLaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.AddScalarMatrix(
                    processor.IntegerToScalar(m1),
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator +(GaLaMatrix<TMatrix, TScalar> m1, double m2)
        {
            var processor = m1.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.AddMatrixScalar(
                    m1.MatrixStorage,
                    processor.Float64ToScalar(m2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator +(double m1, GaLaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.AddScalarMatrix(
                    processor.Float64ToScalar(m1),
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator +(GaLaMatrix<TMatrix, TScalar> m1, TScalar m2)
        {
            var processor = m1.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.AddMatrixScalar(
                    m1.MatrixStorage,
                    m2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator +(TScalar m1, GaLaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.AddScalarMatrix(
                    m1,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator +(GaLaMatrix<TMatrix, TScalar> m1, TMatrix m2)
        {
            var processor = m1.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.AddMatrices(
                    m1.MatrixStorage,
                    m2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator +(TMatrix m1, GaLaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.AddMatrices(
                    m1,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator +(GaLaMatrix<TMatrix, TScalar> m1, GaLaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m1.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.AddMatrices(
                    m1.MatrixStorage,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator -(GaLaMatrix<TMatrix, TScalar> m1, int m2)
        {
            var processor = m1.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractMatrixScalar(
                    m1.MatrixStorage,
                    processor.IntegerToScalar(m2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator -(int m1, GaLaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractScalarMatrix(
                    processor.IntegerToScalar(m1),
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator -(GaLaMatrix<TMatrix, TScalar> m1, double m2)
        {
            var processor = m1.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractMatrixScalar(
                    m1.MatrixStorage,
                    processor.Float64ToScalar(m2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator -(double m1, GaLaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractScalarMatrix(
                    processor.Float64ToScalar(m1),
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator -(GaLaMatrix<TMatrix, TScalar> m1, TScalar m2)
        {
            var processor = m1.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractMatrixScalar(
                    m1.MatrixStorage,
                    m2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator -(TScalar m1, GaLaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractScalarMatrix(
                    m1,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator -(GaLaMatrix<TMatrix, TScalar> m1, TMatrix m2)
        {
            var processor = m1.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractMatrices(
                    m1.MatrixStorage,
                    m2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator -(TMatrix m1, GaLaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractMatrices(
                    m1,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator -(GaLaMatrix<TMatrix, TScalar> m1, GaLaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m1.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.SubtractMatrices(
                    m1.MatrixStorage,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator *(GaLaMatrix<TMatrix, TScalar> m1, int m2)
        {
            var processor = m1.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesMatrixScalar(
                    m1.MatrixStorage,
                    processor.IntegerToScalar(m2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator *(int m1, GaLaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesScalarMatrix(
                    processor.IntegerToScalar(m1),
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator *(GaLaMatrix<TMatrix, TScalar> m1, double m2)
        {
            var processor = m1.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesMatrixScalar(
                    m1.MatrixStorage,
                    processor.Float64ToScalar(m2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator *(double m1, GaLaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesScalarMatrix(
                    processor.Float64ToScalar(m1),
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator *(GaLaMatrix<TMatrix, TScalar> m1, TScalar m2)
        {
            var processor = m1.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesMatrixScalar(
                    m1.MatrixStorage,
                    m2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator *(TScalar m1, GaLaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesScalarMatrix(
                    m1,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator *(GaLaMatrix<TMatrix, TScalar> m1, TMatrix m2)
        {
            var processor = m1.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesMatrices(
                    m1.MatrixStorage,
                    m2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator *(TMatrix m1, GaLaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m2.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesMatrices(
                    m1,
                    m2.MatrixStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> operator *(GaLaMatrix<TMatrix, TScalar> m1, GaLaMatrix<TMatrix, TScalar> m2)
        {
            var processor = m1.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.TimesMatrices(
                    m1.MatrixStorage,
                    m2.MatrixStorage
                )
            );
        }


        public IGaMatrixProcessor<TMatrix, TScalar> MatrixProcessor { get; }

        public TMatrix MatrixStorage { get; }

        public int RowsCount 
            => MatrixProcessor.GetDenseRowsCount(MatrixStorage);

        public int ColumnsCount 
            => MatrixProcessor.GetDenseColumnsCount(MatrixStorage);

        public TScalar this[int i, int j] 
            => MatrixProcessor.GetScalar(MatrixStorage, i, j);


        internal GaLaMatrix([NotNull] IGaMatrixProcessor<TMatrix, TScalar> matrixProcessor, [NotNull] TMatrix matrixStorage)
        {
            MatrixProcessor = matrixProcessor;
            MatrixStorage = matrixStorage;
        }
    }
}