using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Collections;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Structures.Composers;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Grids.Graded;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Structures.Factories
{
    public static class GaGridGradedFactory
    {
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static GaGridGradedComposer<T> CreateGradedGridComposer<T>(this IGaScalarProcessor<T> scalarProcessor)
        //{
        //    return new GaGridGradedComposer<T>(scalarProcessor);
        //}
        

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static GaGridGradedSingleGrade<T> CreateGradedGridSingleGrade<T>(this uint grade, IGaGridEven<T> evenGrid)
        //{
        //    return new GaGridGradedSingleGrade<T>(
        //        grade,
        //        evenGrid.IsEmpty() 
        //            ? GaGridEvenEmpty<T>.EmptyGrid
        //            : evenGrid
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static GaGridGradedSingleGrade<T> CreateGradedGridSingleGrade<T>(this IGaGridEven<T> evenGrid, uint grade)
        //{
        //    return new GaGridGradedSingleGrade<T>(
        //        grade,
        //        evenGrid.IsEmpty() 
        //            ? GaGridEvenEmpty<T>.EmptyGrid
        //            : evenGrid
        //    );
        //}



        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static IGaGridGraded<T> CreateGradedGridSingleZeroGrade<T>(this T value)
        //{
        //    return new GaGridGradedSingleGrade<T>(
        //        0U,
        //        value.CreateEvenGridSingleZeroKey()
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static IGaGridGraded<T> CreateGradedGridSingleGrid<T>(this T value, uint grade, ulong key1, ulong key2)
        //{
        //    return new GaGridGradedSingleGrade<T>(
        //        grade, 
        //        value.CreateEvenGridSingleKey(key1, key2)
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static IGaGridGraded<T> CreateGradedGridSingleGrid<T>(this uint grade, ulong key1, ulong key2, T value)
        //{
        //    return new GaGridGradedSingleGrade<T>(
        //        grade, 
        //        value.CreateEvenGridSingleKey(key1, key2)
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static IGaGridGraded<T> CreateGradedGridSingleGrid<T>(this IGaGridEven<T> evenDictionary, uint grade)
        //{
        //    return evenDictionary.IsEmpty()
        //        ? GaGridGradedEmpty<T>.EmptyGrid
        //        : new GaGridGradedSingleGrade<T>(grade, evenDictionary);
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static IGaGridGraded<T> CreateGradedGridSingleGrid<T>(this uint grade, IGaGridEven<T> evenDictionary)
        //{
        //    return evenDictionary.IsEmpty()
        //        ? GaGridGradedEmpty<T>.EmptyGrid
        //        : new GaGridGradedSingleGrade<T>(grade, evenDictionary);
        //}
        

        //public static IGaGridGraded<T> CreateGradedGrid<T>(this Dictionary<uint, Dictionary<GaRecordKeyPair, T>> gradeKeyValueDictionary)
        //{
        //    if (gradeKeyValueDictionary.Count == 0 || gradeKeyValueDictionary.Values.All(d => d.IsNullOrEmpty()))
        //        return GaGridGradedEmpty<T>.EmptyGrid;

        //    if (gradeKeyValueDictionary.Count != 1)
        //    {
        //        return new GaGridGradedSparse<T>(
        //            gradeKeyValueDictionary
        //                .CopyToDictionary(
        //                    dict => dict.CreateEvenGrid()
        //                )
        //        );
        //    }

        //    var (grade, evenDictionary) = 
        //        gradeKeyValueDictionary.First();

        //    return new GaGridGradedSingleGrade<T>(
        //        grade, 
        //        evenDictionary.CreateEvenGrid()
        //    );
        //}
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static GaGridGradedSparse<T> CreateGradedGridSparse<T>(this Dictionary<uint, IGaGridEven<T>> gradeKeyValueDictionary)
        //{
        //    return new GaGridGradedSparse<T>(gradeKeyValueDictionary);
        //}

        //public static IGaGridGraded<T> CreateGradedGrid<T>(this Dictionary<uint, IGaGridEven<T>> gradeKeyValueDictionary)
        //{
        //    if (gradeKeyValueDictionary.Count == 0 || gradeKeyValueDictionary.Values.All(d => d.IsEmpty()))
        //        return GaGridGradedEmpty<T>.EmptyGrid;

        //    if (gradeKeyValueDictionary.Count != 1) 
        //        return new GaGridGradedSparse<T>(gradeKeyValueDictionary);

        //    var (grade, evenDictionary) = 
        //        gradeKeyValueDictionary.First();

        //    return new GaGridGradedSingleGrade<T>(grade, evenDictionary);
        //}


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridGradedComposer<T> CreateGradedGridComposer<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return new GaGridGradedComposer<T>(scalarProcessor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridGradedDenseList<T> CreateGradedGridDenseGrid<T>(this uint grade)
        {
            return new GaGridGradedDenseList<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridGradedDenseList<T> CreateGradedGridDenseGrid<T>(this uint grade, int capacity)
        {
            return new GaGridGradedDenseList<T>(capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridGradedDenseList<T> CreateGradedGridDenseGrid<T>(this uint grade, IEnumerable<IGaGridEven<T>> evenGrids)
        {
            return new GaGridGradedDenseList<T>(evenGrids);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridGradedDenseList<T> CreateGradedGridDenseGrid<T>(this IEnumerable<IGaGridEven<T>> evenGrids, uint grade)
        {
            return new GaGridGradedDenseList<T>(evenGrids);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridGradedSingleGrade<T> CreateGradedGridSingleGrade<T>(this uint grade, IGaGridEven<T> evenGrid)
        {
            if (evenGrid.IsEmpty())
                return new GaGridGradedSingleGradeEmpty<T>(grade);
            
            if (evenGrid is IGaGridEvenSingleKey<T> singleKeyGrid)
                return new GaGridGradedSingleGradeKey<T>(grade, singleKeyGrid);

            if (evenGrid.GetSparseCount() > 1) 
                return new GaGridGradedSingleGrade<T>(grade, evenGrid);

            var (key1, key2, value) = evenGrid.GetMinKeyValueRecord();

            return new GaGridGradedSingleGradeKey<T>(grade, key1, key2, value);

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridGradedSingleGrade<T> CreateGradedGridSingleGrade<T>(this IGaGridEven<T> evenGrid, uint grade)
        {
            if (evenGrid.IsEmpty())
                return new GaGridGradedSingleGrade<T>(grade, GaGridEvenEmpty<T>.EmptyGrid);

            if (evenGrid is IGaGridEvenSingleKey<T> singleKeyGrid)
                return new GaGridGradedSingleGradeKey<T>(grade, singleKeyGrid);

            if (evenGrid.GetSparseCount() > 1) 
                return new GaGridGradedSingleGrade<T>(grade, evenGrid);

            var (key1, key2, value) = evenGrid.GetMinKeyValueRecord();

            return new GaGridGradedSingleGradeKey<T>(grade, key1, key2, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridGradedSingleGradeKey<T> CreateGradedGridSingleGrade<T>(this T value)
        {
            return new GaGridGradedSingleGradeKey<T>(0U, 0UL, 0UL, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridGradedSingleGradeKey<T> CreateGradedGridSingleGrade<T>(this T value, uint grade, ulong key1, ulong key2)
        {
            return new GaGridGradedSingleGradeKey<T>(grade, key1, key2, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridGradedSingleGradeKey<T> CreateGradedGridSingleGrade<T>(this T value, uint grade, GaRecordKeyPair key)
        {
            return new GaGridGradedSingleGradeKey<T>(grade, key.Key1, key.Key2, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridGradedSingleGradeKey<T> CreateGradedGridSingleGrade<T>(this uint grade, ulong key1, ulong key2, T value)
        {
            return new GaGridGradedSingleGradeKey<T>(grade, key1, key2, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridGradedSingleGradeKey<T> CreateGradedGridSingleGrade<T>(this uint grade, T value, GaRecordKeyPair key)
        {
            return new GaGridGradedSingleGradeKey<T>(grade, key.Key1, key.Key2, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridGradedSingleGradeKey<T> CreateGradedGridSingleGrade<T>(this uint grade, IGaGridEvenSingleKey<T> singleKeyGrid)
        {
            return new GaGridGradedSingleGradeKey<T>(grade, singleKeyGrid);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridGradedSingleGradeKey<T> CreateGradedGridSingleGrade<T>(this IGaGridEvenSingleKey<T> singleKeyGrid, uint grade)
        {
            return new GaGridGradedSingleGradeKey<T>(grade, singleKeyGrid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridGradedSparse<T> CreateGradedGridSparse<T>(this Dictionary<uint, IGaGridEven<T>> gradeKeyValueDictionary)
        {
            return new GaGridGradedSparse<T>(gradeKeyValueDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridGradedSparse<T> CreateGradedGridSparse<T>()
        {
            return new GaGridGradedSparse<T>();
        }

        public static IGaGridGraded<T> CreateGradedGrid<T>(this Dictionary<uint, Dictionary<GaRecordKeyPair, T>> gradeKeyValueDictionary)
        {
            if (gradeKeyValueDictionary.Count == 0 || gradeKeyValueDictionary.Values.All(d => d.IsNullOrEmpty()))
                return GaGridGradedEmpty<T>.EmptyGrid;

            if (gradeKeyValueDictionary.Count != 1)
            {
                return new GaGridGradedSparse<T>(
                    gradeKeyValueDictionary
                        .CopyToDictionary(
                            dict => dict.CreateEvenGrid()
                        )
                );
            }

            var (grade, evenGrid) = 
                gradeKeyValueDictionary.First();

            return new GaGridGradedSingleGrade<T>(
                grade, 
                evenGrid.CreateEvenGrid()
            );
        }

        public static IGaGridGraded<T> CreateGradedGrid<T>(this Dictionary<uint, IGaGridEven<T>> gradeKeyValueDictionary)
        {
            if (gradeKeyValueDictionary.Count == 0 || gradeKeyValueDictionary.Values.All(d => d.IsEmpty()))
                return GaGridGradedEmpty<T>.EmptyGrid;

            if (gradeKeyValueDictionary.Count != 1) 
                return new GaGridGradedSparse<T>(gradeKeyValueDictionary);

            var (grade, evenGrid) = 
                gradeKeyValueDictionary.First();

            return new GaGridGradedSingleGrade<T>(grade, evenGrid);
        }
    }
}