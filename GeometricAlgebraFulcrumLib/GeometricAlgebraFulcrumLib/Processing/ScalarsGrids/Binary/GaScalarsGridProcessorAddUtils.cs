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
    public static class GaScalarsGridProcessorAddUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> Add<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> vector, T scalar)
        {
            return vector.MapValues(value => scalarProcessor.Add(value, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> Add<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, IGaListEven<T> vector)
        {
            return vector.MapValues(value => scalarProcessor.Add(scalar, value));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEvenDense<T> Add<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEvenDense<T> v1, IGaListEvenDense<T> v2)
        {
            return scalarProcessor.MapValuesKeysUnion(v1, v2, scalarProcessor.Add);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> Add<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> v1, IGaListEven<T> v2)
        {
            return scalarProcessor.MapValuesKeysUnion(v1, v2, scalarProcessor.Add);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListGraded<T> Add<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade1, IGaListEven<T> v1, uint grade2, IGaListEven<T> v2)
        {
            return grade1 == grade2
                ? new GaListGradedSingleGrade<T>(
                    grade1,
                    scalarProcessor.Add(v1, v2)
                )
                : new GaListGradedSparse<T>()
                    .AddList(grade1, v1)
                    .AddList(grade2, v2)
                    .GetCompactList();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListGraded<T> Add<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListGraded<T> v1, IGaListGraded<T> v2)
        {
            if (v1.IsEmpty())
                return v2.IsEmpty() 
                    ? GaListGradedEmpty<T>.EmptyList 
                    : v2;

            return scalarProcessor
                .CreateGradedListComposer()
                .SetTerms(v1)
                .AddTerms(v2)
                .RemoveZeroTerms()
                .CreateGradedList();
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> Add<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEven<T> grid, T scalar)
        {
            return grid.MapValues(value => scalarProcessor.Add(value, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> Add<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, IGaGridEven<T> grid)
        {
            return grid.MapValues(value => scalarProcessor.Add(scalar, value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEvenDense<T> Add<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEvenDense<T> v1, IGaGridEvenDense<T> v2)
        {
            return scalarProcessor.MapValuesKeysUnion(v1, v2, scalarProcessor.Add);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> Add<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEven<T> v1, IGaGridEven<T> v2)
        {
            return scalarProcessor.MapValuesKeysUnion(v1, v2, scalarProcessor.Add);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridGraded<T> Add<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridGraded<T> v1, IGaGridGraded<T> v2)
        {
            if (v1.IsEmpty())
                return v2.IsEmpty() 
                    ? GaGridGradedEmpty<T>.EmptyGrid 
                    : v2;

            return scalarProcessor
                .CreateGradedGridComposer()
                .SetTerms(v1)
                .AddTerms(v2)
                .RemoveZeroTerms()
                .CreateGradedGrid();
        }
    }
}