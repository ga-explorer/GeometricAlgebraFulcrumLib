using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaScalarsGridProcessorTimesUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, ILaMatrixEvenStorage<T> grid)
        {
            return grid.MapScalars(value => scalarProcessor.Times(scalar, value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixEvenStorage<T> grid, T scalar)
        {
            return grid.MapScalars(value => scalarProcessor.Times(value, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixEvenStorage<T> grid1, ILaMatrixEvenStorage<T> grid2)
        {
            return scalarProcessor.MapValuesKeysIntersection(grid1, grid2, scalarProcessor.Times);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> vector, T scalar)
        {
            return vector.MapScalars(value => scalarProcessor.Times(value, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, ILaVectorEvenStorage<T> vector)
        {
            return vector.MapScalars(value => scalarProcessor.Times(scalar, value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> vector1, ILaVectorEvenStorage<T> vector2)
        {
            return scalarProcessor.MapValuesKeysIntersection(vector1, vector2, scalarProcessor.Times);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T TimesInner<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> vector1, ILaVectorEvenStorage<T> vector2)
        {
            return scalarProcessor.Add(
                scalarProcessor
                    .MapValuesKeysIntersection(vector1, vector2, scalarProcessor.Times)
                    .GetScalars()
            );
        }

        public static ILaMatrixEvenStorage<T> TimesOuter<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> vector1, ILaVectorEvenStorage<T> vector2)
        {
            return scalarProcessor.MapValuesOuter(vector1, vector2, scalarProcessor.Times);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorDenseEvenStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorDenseEvenStorage<T> v1, ILaVectorDenseEvenStorage<T> v2)
        {
            return scalarProcessor.MapValuesKeysIntersection(v1, v2, scalarProcessor.Times);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorGradedStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorGradedStorage<T> gradedList, T scalar)
        {
            return gradedList.MapScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorGradedStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, ILaVectorGradedStorage<T> gradedList)
        {
            return gradedList.MapScalars(s => scalarProcessor.Times(scalar, s));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixGradedStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixGradedStorage<T> gradedGrid, T scalar)
        {
            return gradedGrid.MapScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixGradedStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, ILaMatrixGradedStorage<T> gradedGrid)
        {
            return gradedGrid.MapScalars(s => scalarProcessor.Times(scalar, s));
        }


        public static ILaMatrixDenseEvenStorage<T> MatrixProduct<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixDenseEvenStorage<T> grid1, ILaMatrixDenseEvenStorage<T> grid2)
        {
            if (grid1.IsEmpty() || grid2.IsEmpty())
                return LaMatrixEmptyStorage<T>.EmptyMatrix;

            var count1 = grid1.Count1;
            var count2 = grid2.Count2;
            var countInner = Math.Min(grid1.Count2, grid2.Count1);
            var array = new T[count1, count2];
            var composer = scalarProcessor.CreateScalarComposer();

            for (var i = 0; i < count1; i++)
            for (var j = 0; j < count2; j++)
            {
                composer.Clear();

                for (var k = 0; k < countInner; k++)
                    composer.AddScalar(
                        scalarProcessor.Times(
                            grid1.GetScalar((ulong) i, (ulong) k),
                            grid2.GetScalar((ulong) k, (ulong) j)
                        )
                    );

                array[i, j] = composer.Scalar;
            }

            return array.CreateEvenGridDenseArray();
        }

        public static ILaMatrixEvenStorage<T> MatrixProduct<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixEvenStorage<T> grid1, ILaMatrixEvenStorage<T> grid2)
        {
            if (grid1.IsEmpty() || grid2.IsEmpty())
                return LaMatrixEmptyStorage<T>.EmptyMatrix;

            if (grid1 is ILaMatrixDenseEvenStorage<T> g1 && grid2 is ILaMatrixDenseEvenStorage<T> g2)
                return scalarProcessor.MatrixProduct(g1, g2);

            var composer = scalarProcessor.CreateLaMatrixSparseEvenStorageComposer();

            foreach (var (key11, key12, scalar1) in grid1.GetIndexScalarRecords())
            {
                foreach (var (key21, key22, scalar2) in grid2.GetIndexScalarRecords())
                {
                    if (key12 != key21) 
                        continue;

                    composer.AddTerm(
                        key11, 
                        key22, 
                        scalarProcessor.Times(scalar1, scalar2)
                    );
                }
            }

            return composer.CreateEvenGrid();
        }
    }
}