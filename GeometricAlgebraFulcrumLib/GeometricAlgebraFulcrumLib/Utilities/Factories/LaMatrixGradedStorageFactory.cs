using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Collections;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class LaMatrixGradedStorageFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixGradedStorageComposer<T> CreateLaMatrixGradedStorageComposer<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return new LaMatrixGradedStorageComposer<T>(scalarProcessor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixListGradedStorage<T> CreateLaMatrixListGradedStorage<T>(this uint grade)
        {
            return new LaMatrixListGradedStorage<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixListGradedStorage<T> CreateLaMatrixListGradedStorage<T>(this uint grade, int capacity)
        {
            return new LaMatrixListGradedStorage<T>(capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixListGradedStorage<T> CreateLaMatrixListGradedStorage<T>(this uint grade, IEnumerable<ILaMatrixEvenStorage<T>> evenGrids)
        {
            return new LaMatrixListGradedStorage<T>(evenGrids);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixListGradedStorage<T> CreateLaMatrixListGradedStorage<T>(this IEnumerable<ILaMatrixEvenStorage<T>> evenGrids, uint grade)
        {
            return new LaMatrixListGradedStorage<T>(evenGrids);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixSingleGradeStorage<T> CreateLaMatrixSingleGradeStorage<T>(this uint grade, ILaMatrixEvenStorage<T> evenGrid)
        {
            if (evenGrid.IsEmpty())
                return new LaMatrixEmptySingleGradeStorage<T>(grade);
            
            if (evenGrid is ILaMatrixSingleIndexEvenStorage<T> singleKeyGrid)
                return new LaMatrixSingleGradeIndexStorage<T>(grade, singleKeyGrid);

            if (evenGrid.GetSparseCount() > 1) 
                return new LaMatrixSingleGradeStorage<T>(grade, evenGrid);

            var (index1, index2, value) = evenGrid.GetMinKeyValueRecord();

            return new LaMatrixSingleGradeIndexStorage<T>(grade, index1, index2, value);

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixSingleGradeStorage<T> CreateLaMatrixSingleGradeStorage<T>(this ILaMatrixEvenStorage<T> evenGrid, uint grade)
        {
            if (evenGrid.IsEmpty())
                return new LaMatrixSingleGradeStorage<T>(grade, LaMatrixEmptyStorage<T>.EmptyMatrix);

            if (evenGrid is ILaMatrixSingleIndexEvenStorage<T> singleKeyGrid)
                return new LaMatrixSingleGradeIndexStorage<T>(grade, singleKeyGrid);

            if (evenGrid.GetSparseCount() > 1) 
                return new LaMatrixSingleGradeStorage<T>(grade, evenGrid);

            var (index1, index2, value) = evenGrid.GetMinKeyValueRecord();

            return new LaMatrixSingleGradeIndexStorage<T>(grade, index1, index2, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixSingleGradeIndexStorage<T> CreateLaMatrixSingleGradeIndexStorage<T>(this T value)
        {
            return new LaMatrixSingleGradeIndexStorage<T>(0U, 0UL, 0UL, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixSingleGradeIndexStorage<T> CreateLaMatrixSingleGradeIndexStorage<T>(this T value, uint grade, ulong index1, ulong index2)
        {
            return new LaMatrixSingleGradeIndexStorage<T>(grade, index1, index2, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixSingleGradeIndexStorage<T> CreateLaMatrixSingleGradeIndexStorage<T>(this T value, uint grade, IndexPairRecord index)
        {
            return new LaMatrixSingleGradeIndexStorage<T>(grade, index.Index1, index.Index2, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixSingleGradeIndexStorage<T> CreateLaMatrixSingleGradeIndexStorage<T>(this uint grade, ulong index1, ulong index2, T value)
        {
            return new LaMatrixSingleGradeIndexStorage<T>(grade, index1, index2, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixSingleGradeIndexStorage<T> CreateLaMatrixSingleGradeIndexStorage<T>(this uint grade, T value, IndexPairRecord index)
        {
            return new LaMatrixSingleGradeIndexStorage<T>(grade, index.Index1, index.Index2, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixSingleGradeIndexStorage<T> CreateLaMatrixSingleGradeIndexStorage<T>(this uint grade, ILaMatrixSingleIndexEvenStorage<T> singleKeyGrid)
        {
            return new LaMatrixSingleGradeIndexStorage<T>(grade, singleKeyGrid);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixSingleGradeIndexStorage<T> CreateLaMatrixSingleGradeIndexStorage<T>(this ILaMatrixSingleIndexEvenStorage<T> singleKeyGrid, uint grade)
        {
            return new LaMatrixSingleGradeIndexStorage<T>(grade, singleKeyGrid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixSparseGradedStorage<T> CreateLaMatrixSparseGradedStorage<T>(this Dictionary<uint, ILaMatrixEvenStorage<T>> gradeKeyValueDictionary)
        {
            return new LaMatrixSparseGradedStorage<T>(gradeKeyValueDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixSparseGradedStorage<T> CreateLaMatrixSparseGradedStorage<T>()
        {
            return new LaMatrixSparseGradedStorage<T>();
        }

        public static ILaMatrixGradedStorage<T> CreateLaMatrixGradedStorage<T>(this Dictionary<uint, Dictionary<IndexPairRecord, T>> gradeKeyValueDictionary)
        {
            if (gradeKeyValueDictionary.Count == 0 || gradeKeyValueDictionary.Values.All(d => d.IsNullOrEmpty()))
                return LaMatrixEmptyGradedStorage<T>.EmptyGrid;

            if (gradeKeyValueDictionary.Count != 1)
            {
                return new LaMatrixSparseGradedStorage<T>(
                    gradeKeyValueDictionary
                        .CopyToDictionary(
                            dict => dict.CreateEvenGrid()
                        )
                );
            }

            var (grade, evenGrid) = 
                gradeKeyValueDictionary.First();

            return new LaMatrixSingleGradeStorage<T>(
                grade, 
                evenGrid.CreateEvenGrid()
            );
        }

        public static ILaMatrixGradedStorage<T> CreateLaMatrixGradedStorage<T>(this Dictionary<uint, ILaMatrixEvenStorage<T>> gradeKeyValueDictionary)
        {
            if (gradeKeyValueDictionary.Count == 0 || gradeKeyValueDictionary.Values.All(d => d.IsEmpty()))
                return LaMatrixEmptyGradedStorage<T>.EmptyGrid;

            if (gradeKeyValueDictionary.Count != 1) 
                return new LaMatrixSparseGradedStorage<T>(gradeKeyValueDictionary);

            var (grade, evenGrid) = 
                gradeKeyValueDictionary.First();

            return new LaMatrixSingleGradeStorage<T>(grade, evenGrid);
        }
    }
}