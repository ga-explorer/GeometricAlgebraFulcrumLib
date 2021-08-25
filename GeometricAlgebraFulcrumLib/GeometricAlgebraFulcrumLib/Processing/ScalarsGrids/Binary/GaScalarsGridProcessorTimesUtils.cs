using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Processing.Scalars.Binary;
using GeometricAlgebraFulcrumLib.Processing.ScalarsGrids.Unary;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Grids.Graded;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;

namespace GeometricAlgebraFulcrumLib.Processing.ScalarsGrids.Binary
{
    public static class GaScalarsGridProcessorTimesUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, IGaGridEven<T> grid)
        {
            return grid.MapValues(value => scalarProcessor.Times(scalar, value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEven<T> grid, T scalar)
        {
            return grid.MapValues(value => scalarProcessor.Times(value, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEven<T> grid1, IGaGridEven<T> grid2)
        {
            return scalarProcessor.MapValuesKeysIntersection(grid1, grid2, scalarProcessor.Times);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> vector, T scalar)
        {
            return vector.MapValues(value => scalarProcessor.Times(value, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, IGaListEven<T> vector)
        {
            return vector.MapValues(value => scalarProcessor.Times(scalar, value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> vector1, IGaListEven<T> vector2)
        {
            return scalarProcessor.MapValuesKeysIntersection(vector1, vector2, scalarProcessor.Times);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T TimesInner<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> vector1, IGaListEven<T> vector2)
        {
            return scalarProcessor.Add(
                scalarProcessor
                    .MapValuesKeysIntersection(vector1, vector2, scalarProcessor.Times)
                    .GetValues()
            );
        }

        public static IGaGridEven<T> TimesOuter<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> vector1, IGaListEven<T> vector2)
        {
            return scalarProcessor.MapValuesOuter(vector1, vector2, scalarProcessor.Times);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEvenDense<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEvenDense<T> v1, IGaListEvenDense<T> v2)
        {
            return scalarProcessor.MapValuesKeysIntersection(v1, v2, scalarProcessor.Times);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListGraded<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListGraded<T> gradedList, T scalar)
        {
            return gradedList.MapValues(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListGraded<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, IGaListGraded<T> gradedList)
        {
            return gradedList.MapValues(s => scalarProcessor.Times(scalar, s));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridGraded<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridGraded<T> gradedGrid, T scalar)
        {
            return gradedGrid.MapValues(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridGraded<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, IGaGridGraded<T> gradedGrid)
        {
            return gradedGrid.MapValues(s => scalarProcessor.Times(scalar, s));
        }


        public static IGaGridEvenDense<T> MatrixProduct<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEvenDense<T> grid1, IGaGridEvenDense<T> grid2)
        {
            if (grid1.IsEmpty() || grid2.IsEmpty())
                return GaGridEvenEmpty<T>.EmptyGrid;

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
                            grid1.GetValue((ulong) i, (ulong) k),
                            grid2.GetValue((ulong) k, (ulong) j)
                        )
                    );

                array[i, j] = composer.Scalar;
            }

            return array.CreateEvenGridDenseArray();
        }

        public static IGaGridEven<T> MatrixProduct<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEven<T> grid1, IGaGridEven<T> grid2)
        {
            if (grid1.IsEmpty() || grid2.IsEmpty())
                return GaGridEvenEmpty<T>.EmptyGrid;

            if (grid1 is IGaGridEvenDense<T> g1 && grid2 is IGaGridEvenDense<T> g2)
                return scalarProcessor.MatrixProduct(g1, g2);

            var composer = scalarProcessor.CreateEvenGridComposer();

            foreach (var (key11, key12, scalar1) in grid1.GetKeyValueRecords())
            {
                foreach (var (key21, key22, scalar2) in grid2.GetKeyValueRecords())
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