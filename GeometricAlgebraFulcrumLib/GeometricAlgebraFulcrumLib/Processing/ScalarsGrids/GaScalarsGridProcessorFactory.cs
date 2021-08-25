using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Processing.ScalarsGrids.Generic;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Processing.ScalarsGrids
{
    public static class GaScalarsGridProcessorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenEmpty<T> ZeroScalarsList<T>()
        {
            return GaListEvenEmpty<T>.EmptyList;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridEvenEmpty<T> ZeroScalarsGrid<T>()
        {
            return GaGridEvenEmpty<T>.EmptyGrid;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> GridRowToVector<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEven<T> grid, int rowIndex)
        {
            return grid.CreateEvenListDenseGridSlice1((ulong) rowIndex, scalarProcessor.GetZeroScalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> GridColumnToVector<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEven<T> grid, int colIndex)
        {
            return grid.CreateEvenListDenseGridSlice2((ulong) colIndex, scalarProcessor.GetZeroScalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> CreateRowVectorGrid<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> array)
        {
            return array.CreateEvenGridRow();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> CreateRowVectorGrid<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEven<T> array, int rowIndex)
        {
            return array.GetRow((ulong) rowIndex).CreateEvenGridRow((ulong) rowIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> CreateColumnVectorGrid<T>(IGaListEven<T> array)
        {
            return array.CreateEvenGridColumn();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> CreateColumnVectorGrid<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEven<T> array, int columnIndex)
        {
            return array.GetColumn((ulong) columnIndex).CreateEvenGridColumn((ulong) columnIndex);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalarsListProcessor<T> CreateScalarsListProcessor<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return new GaScalarsListProcessor<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalarsGridProcessor<T> CreateScalarsGridProcessor<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return new GaScalarsGridProcessor<T>(scalarProcessor);
        }
    }
}