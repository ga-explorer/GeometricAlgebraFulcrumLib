using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Processing.ScalarsGrids.Unary;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Grids.Graded;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;

namespace GeometricAlgebraFulcrumLib.Processing.ScalarsGrids.Binary
{
    public static class GaScalarsGridProcessorDivideUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEven<T> grid1, IGaGridEven<T> grid2)
        {
            return scalarProcessor.MapValuesKeysIntersection(grid1, grid2, scalarProcessor.Divide);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEven<T> grid, T scalar)
        {
            return grid.MapValues(value => scalarProcessor.Divide(value, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, IGaGridEven<T> grid)
        {
            return grid.MapValues(value => scalarProcessor.Divide(scalar, value));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> vector, T scalar)
        {
            return vector.MapValues(value => scalarProcessor.Divide(value, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, IGaListEven<T> vector)
        {
            return vector.MapValues(value => scalarProcessor.Add(scalar, value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> vector1, IGaListEven<T> vector2)
        {
            return scalarProcessor.MapValuesKeysIntersection(vector1, vector2, scalarProcessor.Divide);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListGraded<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListGraded<T> gradedList, T scalar)
        {
            return gradedList.MapValues(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListGraded<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, IGaListGraded<T> gradedList)
        {
            return gradedList.MapValues(s => scalarProcessor.Divide(scalar, s));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridGraded<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridGraded<T> gradedGrid, T scalar)
        {
            return gradedGrid.MapValues(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridGraded<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, IGaGridGraded<T> gradedGrid)
        {
            return gradedGrid.MapValues(s => scalarProcessor.Divide(scalar, s));
        }
    }
}