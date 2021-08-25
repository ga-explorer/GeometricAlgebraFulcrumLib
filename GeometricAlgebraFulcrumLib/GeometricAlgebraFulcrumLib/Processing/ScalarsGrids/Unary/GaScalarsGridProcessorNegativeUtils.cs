using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Grids.Graded;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;

namespace GeometricAlgebraFulcrumLib.Processing.ScalarsGrids.Unary
{
    public static class GaScalarsGridProcessorNegativeUtils
    {
        


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> evenList)
        {
            return evenList.MapValues(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListGraded<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListGraded<T> gradedList)
        {
            return gradedList.MapValues(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEven<T> evenGrid)
        {
            return evenGrid.MapValues(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridGraded<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridGraded<T> gradedGrid)
        {
            return gradedGrid.MapValues(scalarProcessor.Negative);
        }


    }
}
