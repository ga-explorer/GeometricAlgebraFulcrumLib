using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Processing.ScalarsGrids.Unary;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Grids.Graded;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Processing.ScalarsGrids.Binary
{
    public static class GaScalarsGridProcessorSubtractUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> vector, T scalar)
        {
            return vector.MapValues(value => scalarProcessor.Subtract(value, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, IGaListEven<T> vector)
        {
            return vector.MapValues(value => scalarProcessor.Subtract(scalar, value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEvenDense<T> Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEvenDense<T> v1, IGaListEvenDense<T> v2)
        {
            return scalarProcessor.MapValuesKeysUnion(v1, v2, scalarProcessor.Subtract);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> v1, IGaListEven<T> v2)
        {
            return scalarProcessor.MapValuesKeysUnion(v1, v2, scalarProcessor.Subtract);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListGraded<T> Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade1, IGaListEven<T> v1, uint grade2, IGaListEven<T> v2)
        {
            return grade1 == grade2
                ? new GaListGradedSingleGrade<T>(
                    grade1,
                    scalarProcessor.Subtract(v1, v2)
                )
                : new GaListGradedSparse<T>()
                    .AddList(grade1, v1)
                    .AddList(grade2, scalarProcessor.Negative(v2))
                    .GetCompactList();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListGraded<T> Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListGraded<T> v1, IGaListGraded<T> v2)
        {
            if (v1.IsEmpty())
                return v2.IsEmpty() 
                    ? GaListGradedEmpty<T>.EmptyList 
                    : scalarProcessor.Negative(v2);

            return scalarProcessor
                .CreateGradedListComposer()
                .SetTerms(v1)
                .SubtractTerms(v2)
                .RemoveZeroTerms()
                .CreateGradedList();
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEven<T> grid, T scalar)
        {
            return grid.MapValues(value => scalarProcessor.Subtract(value, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, IGaGridEven<T> grid)
        {
            return grid.MapValues(value => scalarProcessor.Subtract(scalar, value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEvenDense<T> Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEvenDense<T> v1, IGaGridEvenDense<T> v2)
        {
            return scalarProcessor.MapValuesKeysUnion(v1, v2, scalarProcessor.Subtract);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEven<T> v1, IGaGridEven<T> v2)
        {
            return scalarProcessor.MapValuesKeysUnion(v1, v2, scalarProcessor.Subtract);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridGraded<T> Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridGraded<T> v1, IGaGridGraded<T> v2)
        {
            if (v1.IsEmpty())
                return v2.IsEmpty() 
                    ? GaGridGradedEmpty<T>.EmptyGrid 
                    : scalarProcessor.Negative(v2);

            return scalarProcessor
                .CreateGradedGridComposer()
                .SetTerms(v1)
                .SubtractTerms(v2)
                .RemoveZeroTerms()
                .CreateGradedGrid();
        }
    }
}