using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Grids.Graded;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;

namespace GeometricAlgebraFulcrumLib.Processing.ScalarsGrids.Unary
{
    public static class GaScalarsGridProcessorIsZeroUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> evenList)
        {
            return evenList.IsEmpty() || evenList.GetValues().All(scalarProcessor.IsZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListGraded<T> gradedList)
        {
            return gradedList.IsEmpty() || gradedList.GetValues().All(scalarProcessor.IsZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEven<T> evenGrid)
        {
            return evenGrid.IsEmpty() || evenGrid.GetValues().All(scalarProcessor.IsZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridGraded<T> gradedGrid)
        {
            return gradedGrid.IsEmpty() || gradedGrid.GetValues().All(scalarProcessor.IsZero);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> evenList, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalarProcessor.IsNearZero(evenList)
                : scalarProcessor.IsZero(evenList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListGraded<T> gradedList, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalarProcessor.IsNearZero(gradedList)
                : scalarProcessor.IsZero(gradedList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEven<T> evenGrid, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalarProcessor.IsNearZero(evenGrid)
                : scalarProcessor.IsZero(evenGrid);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridGraded<T> gradedGrid, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalarProcessor.IsNearZero(gradedGrid)
                : scalarProcessor.IsZero(gradedGrid);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZero<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> evenList)
        {
            return evenList.IsEmpty() || evenList.GetValues().All(scalarProcessor.IsNearZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZero<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListGraded<T> gradedList)
        {
            return gradedList.IsEmpty() || gradedList.GetValues().All(scalarProcessor.IsNearZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZero<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEven<T> evenGrid)
        {
            return evenGrid.IsEmpty() || evenGrid.GetValues().All(scalarProcessor.IsNearZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZero<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridGraded<T> gradedGrid)
        {
            return gradedGrid.IsEmpty() || gradedGrid.GetValues().All(scalarProcessor.IsNearZero);
        }
    }
}