using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.Matrices
{
    public static class GaLaMatrixUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<int> GetSize<TMatrix, TScalar>(this GaLaMatrix<TMatrix, TScalar> matrix)
        {
            return matrix.MatrixProcessor.GetDenseSize(matrix.MatrixStorage);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaLaMatrixColumn<TMatrix, TScalar>> GetColumns<TMatrix, TScalar>(this GaLaMatrix<TMatrix, TScalar> matrix)
        {
            return Enumerable
                .Range(0, matrix.ColumnsCount)
                .Select(colIndex =>
                    new GaLaMatrixColumn<TMatrix, TScalar>(
                        matrix.MatrixProcessor,
                        matrix.MatrixStorage,
                        colIndex
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrixColumn<TMatrix, TScalar> GetColumn<TMatrix, TScalar>(this GaLaMatrix<TMatrix, TScalar> matrix, int colIndex)
        {
            return new GaLaMatrixColumn<TMatrix, TScalar>(
                matrix.MatrixProcessor,
                matrix.MatrixStorage,
                colIndex
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrixColumnScaled<TMatrix, TScalar> GetScaledColumn<TMatrix, TScalar>(this GaLaMatrix<TMatrix, TScalar> matrix, int colIndex, TScalar scalingFactor)
        {
            return new GaLaMatrixColumnScaled<TMatrix, TScalar>(
                matrix.MatrixProcessor,
                matrix.MatrixStorage,
                colIndex,
                scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrixColumnMapped<TMatrix, TScalar> GetMappedColumn<TMatrix, TScalar>(this GaLaMatrix<TMatrix, TScalar> matrix, int colIndex, Func<TScalar, TScalar> scalarMapping)
        {
            return new GaLaMatrixColumnMapped<TMatrix, TScalar>(
                matrix.MatrixProcessor,
                matrix.MatrixStorage,
                colIndex,
                scalarMapping
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaLaMatrixRow<TMatrix, TScalar>> GetRows<TMatrix, TScalar>(this GaLaMatrix<TMatrix, TScalar> matrix)
        {
            return Enumerable
                .Range(0, matrix.RowsCount)
                .Select(rowIndex =>
                    new GaLaMatrixRow<TMatrix, TScalar>(
                        matrix.MatrixProcessor,
                        matrix.MatrixStorage,
                        rowIndex
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrixRow<TMatrix, TScalar> GetRow<TMatrix, TScalar>(this GaLaMatrix<TMatrix, TScalar> matrix, int rowIndex)
        {
            return new GaLaMatrixRow<TMatrix, TScalar>(
                matrix.MatrixProcessor,
                matrix.MatrixStorage,
                rowIndex
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrixRowScaled<TMatrix, TScalar> GetScaledRow<TMatrix, TScalar>(this GaLaMatrix<TMatrix, TScalar> matrix, int rowIndex, TScalar scalingFactor)
        {
            return new GaLaMatrixRowScaled<TMatrix, TScalar>(
                matrix.MatrixProcessor,
                matrix.MatrixStorage,
                rowIndex,
                scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrixRowMapped<TMatrix, TScalar> GetMappedRow<TMatrix, TScalar>(this GaLaMatrix<TMatrix, TScalar> matrix, int rowIndex, Func<TScalar, TScalar> scalarMapping)
        {
            return new GaLaMatrixRowMapped<TMatrix, TScalar>(
                matrix.MatrixProcessor,
                matrix.MatrixStorage,
                rowIndex,
                scalarMapping
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> MapMatrixItems<TMatrix, TScalar>(this GaLaMatrix<TMatrix, TScalar> matrix, Func<TScalar, TScalar> scalarMapping)
        {
            var processor = matrix.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.MapMatrixItems(matrix.MatrixStorage, scalarMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> MapMatrixItems<TMatrix, TScalar>(this GaLaMatrix<TMatrix, TScalar> matrix, Func<int, int, TScalar, TScalar> scalarMapping)
        {
            var processor = matrix.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.MapMatrixItems(matrix.MatrixStorage, scalarMapping)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> GetAdjoint<TMatrix, TScalar>(this GaLaMatrix<TMatrix, TScalar> matrix)
        {
            var processor = matrix.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.MatrixTranspose(matrix.MatrixStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> GetInverse<TMatrix, TScalar>(this GaLaMatrix<TMatrix, TScalar> matrix)
        {
            var processor = matrix.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.MatrixInverse(matrix.MatrixStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaMatrix<TMatrix, TScalar> GetInverseAdjoint<TMatrix, TScalar>(this GaLaMatrix<TMatrix, TScalar> matrix)
        {
            var processor = matrix.MatrixProcessor;

            return new GaLaMatrix<TMatrix, TScalar>(
                processor,
                processor.MatrixInverseTranspose(matrix.MatrixStorage)
            );
        }


        
    }
}
