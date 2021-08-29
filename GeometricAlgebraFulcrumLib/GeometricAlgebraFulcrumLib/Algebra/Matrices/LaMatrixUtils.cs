using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.Matrices
{
    public static class LaMatrixUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<int> GetSize<TMatrix, TScalar>(this LaMatrix<TMatrix, TScalar> matrix)
        {
            return matrix.MatrixProcessor.GetDenseSize(matrix.MatrixStorage);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<LaMatrixColumn<TMatrix, TScalar>> GetColumns<TMatrix, TScalar>(this LaMatrix<TMatrix, TScalar> matrix)
        {
            return Enumerable
                .Range(0, matrix.ColumnsCount)
                .Select(colIndex =>
                    new LaMatrixColumn<TMatrix, TScalar>(
                        matrix.MatrixProcessor,
                        matrix.MatrixStorage,
                        colIndex
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixColumn<TMatrix, TScalar> GetColumn<TMatrix, TScalar>(this LaMatrix<TMatrix, TScalar> matrix, int colIndex)
        {
            return new LaMatrixColumn<TMatrix, TScalar>(
                matrix.MatrixProcessor,
                matrix.MatrixStorage,
                colIndex
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixColumnScaled<TMatrix, TScalar> GetScaledColumn<TMatrix, TScalar>(this LaMatrix<TMatrix, TScalar> matrix, int colIndex, TScalar scalingFactor)
        {
            return new LaMatrixColumnScaled<TMatrix, TScalar>(
                matrix.MatrixProcessor,
                matrix.MatrixStorage,
                colIndex,
                scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixColumnMapped<TMatrix, TScalar> GetMappedColumn<TMatrix, TScalar>(this LaMatrix<TMatrix, TScalar> matrix, int colIndex, Func<TScalar, TScalar> scalarMapping)
        {
            return new LaMatrixColumnMapped<TMatrix, TScalar>(
                matrix.MatrixProcessor,
                matrix.MatrixStorage,
                colIndex,
                scalarMapping
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<LaMatrixRow<TMatrix, TScalar>> GetRows<TMatrix, TScalar>(this LaMatrix<TMatrix, TScalar> matrix)
        {
            return Enumerable
                .Range(0, matrix.RowsCount)
                .Select(rowIndex =>
                    new LaMatrixRow<TMatrix, TScalar>(
                        matrix.MatrixProcessor,
                        matrix.MatrixStorage,
                        rowIndex
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixRow<TMatrix, TScalar> GetRow<TMatrix, TScalar>(this LaMatrix<TMatrix, TScalar> matrix, int rowIndex)
        {
            return new LaMatrixRow<TMatrix, TScalar>(
                matrix.MatrixProcessor,
                matrix.MatrixStorage,
                rowIndex
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixRowScaled<TMatrix, TScalar> GetScaledRow<TMatrix, TScalar>(this LaMatrix<TMatrix, TScalar> matrix, int rowIndex, TScalar scalingFactor)
        {
            return new LaMatrixRowScaled<TMatrix, TScalar>(
                matrix.MatrixProcessor,
                matrix.MatrixStorage,
                rowIndex,
                scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixRowMapped<TMatrix, TScalar> GetMappedRow<TMatrix, TScalar>(this LaMatrix<TMatrix, TScalar> matrix, int rowIndex, Func<TScalar, TScalar> scalarMapping)
        {
            return new LaMatrixRowMapped<TMatrix, TScalar>(
                matrix.MatrixProcessor,
                matrix.MatrixStorage,
                rowIndex,
                scalarMapping
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> MapMatrixItems<TMatrix, TScalar>(this LaMatrix<TMatrix, TScalar> matrix, Func<TScalar, TScalar> scalarMapping)
        {
            var processor = matrix.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.MapMatrixItems(matrix.MatrixStorage, scalarMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> MapMatrixItems<TMatrix, TScalar>(this LaMatrix<TMatrix, TScalar> matrix, Func<int, int, TScalar, TScalar> scalarMapping)
        {
            var processor = matrix.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.MapMatrixItems(matrix.MatrixStorage, scalarMapping)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> GetAdjoint<TMatrix, TScalar>(this LaMatrix<TMatrix, TScalar> matrix)
        {
            var processor = matrix.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.MatrixTranspose(matrix.MatrixStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> GetInverse<TMatrix, TScalar>(this LaMatrix<TMatrix, TScalar> matrix)
        {
            var processor = matrix.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.MatrixInverse(matrix.MatrixStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrix<TMatrix, TScalar> GetInverseAdjoint<TMatrix, TScalar>(this LaMatrix<TMatrix, TScalar> matrix)
        {
            var processor = matrix.MatrixProcessor;

            return new LaMatrix<TMatrix, TScalar>(
                processor,
                processor.MatrixInverseTranspose(matrix.MatrixStorage)
            );
        }


        
    }
}
