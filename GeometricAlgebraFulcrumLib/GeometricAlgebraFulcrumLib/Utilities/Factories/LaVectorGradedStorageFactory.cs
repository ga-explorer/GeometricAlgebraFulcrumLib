using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Collections;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class LaVectorGradedStorageFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorGradedStorageComposer<T> CreateLaVectorGradedStorageComposer<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return new LaVectorGradedStorageComposer<T>(scalarProcessor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorListGradedStorage<T> CreateLaVectorListGradedStorage<T>(this uint grade)
        {
            return new LaVectorListGradedStorage<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorListGradedStorage<T> CreateLaVectorListGradedStorage<T>(this uint grade, int capacity)
        {
            return new LaVectorListGradedStorage<T>(capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorListGradedStorage<T> CreateLaVectorListGradedStorage<T>(this uint grade, IEnumerable<ILaVectorEvenStorage<T>> evenLists)
        {
            return new LaVectorListGradedStorage<T>(evenLists);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorListGradedStorage<T> CreateLaVectorListGradedStorage<T>(this IEnumerable<ILaVectorEvenStorage<T>> evenLists, uint grade)
        {
            return new LaVectorListGradedStorage<T>(evenLists);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorSingleGradeStorage<T> CreateLaVectorSingleGradeStorage<T>(this uint grade, ILaVectorEvenStorage<T> evenList)
        {
            if (evenList.IsEmpty())
                return new LaVectorEmptySingleGradeStorage<T>(grade);
            
            if (evenList is ILaVectorSingleIndexEvenStorage<T> singleKeyList)
                return new LaVectorSingleGradeIndexStorage<T>(grade, singleKeyList);

            if (evenList.GetSparseCount() > 1) 
                return new LaVectorSingleGradeStorage<T>(grade, evenList);

            var (index, value) = evenList.GetMinKeyValueRecord();

            return new LaVectorSingleGradeIndexStorage<T>(grade, index, value);

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorSingleGradeStorage<T> CreateLaVectorSingleGradeStorage<T>(this ILaVectorEvenStorage<T> evenList, uint grade)
        {
            if (evenList.IsEmpty())
                return new LaVectorSingleGradeStorage<T>(grade, LaVectorEmptyStorage<T>.ZeroStorage);

            if (evenList is ILaVectorSingleIndexEvenStorage<T> singleKeyList)
                return new LaVectorSingleGradeIndexStorage<T>(grade, singleKeyList);

            if (evenList.GetSparseCount() > 1) 
                return new LaVectorSingleGradeStorage<T>(grade, evenList);

            var (index, value) = evenList.GetMinKeyValueRecord();

            return new LaVectorSingleGradeIndexStorage<T>(grade, index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorSingleGradeIndexStorage<T> CreateLaVectorSingleGradeIndexStorage<T>(this T value)
        {
            return new LaVectorSingleGradeIndexStorage<T>(0U, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorSingleGradeIndexStorage<T> CreateLaVectorSingleGradeIndexStorage<T>(this T value, uint grade, ulong index)
        {
            return new LaVectorSingleGradeIndexStorage<T>(grade, index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorSingleGradeIndexStorage<T> CreateLaVectorSingleGradeIndexStorage<T>(this uint grade, ulong index, T value)
        {
            return new LaVectorSingleGradeIndexStorage<T>(grade, index, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorSingleGradeIndexStorage<T> CreateLaVectorSingleGradeIndexStorage<T>(this uint grade, ILaVectorSingleIndexEvenStorage<T> singleKeyList)
        {
            return new LaVectorSingleGradeIndexStorage<T>(grade, singleKeyList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorSingleGradeIndexStorage<T> CreateLaVectorSingleGradeIndexStorage<T>(this ILaVectorSingleIndexEvenStorage<T> singleKeyList, uint grade)
        {
            return new LaVectorSingleGradeIndexStorage<T>(grade, singleKeyList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorSparseGradedStorage<T> CreateLaVectorSparseGradedStorage<T>(this Dictionary<uint, ILaVectorEvenStorage<T>> gradeKeyValueDictionary)
        {
            return new LaVectorSparseGradedStorage<T>(gradeKeyValueDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorSparseGradedStorage<T> CreateGradedListSparse<T>()
        {
            return new LaVectorSparseGradedStorage<T>();
        }

        public static ILaVectorGradedStorage<T> CreateLaVectorGradedStorage<T>(this Dictionary<uint, Dictionary<ulong, T>> gradeKeyValueDictionary)
        {
            if (gradeKeyValueDictionary.Count == 0 || gradeKeyValueDictionary.Values.All(d => d.IsNullOrEmpty()))
                return LaVectorEmptyGradedStorage<T>.EmptyList;

            if (gradeKeyValueDictionary.Count != 1)
            {
                return new LaVectorSparseGradedStorage<T>(
                    gradeKeyValueDictionary
                        .CopyToDictionary(
                            dict => dict.CreateLaVectorStorage()
                        )
                );
            }

            var (grade, evenList) = 
                gradeKeyValueDictionary.First();

            return new LaVectorSingleGradeStorage<T>(
                grade, 
                evenList.CreateLaVectorStorage()
            );
        }

        public static ILaVectorGradedStorage<T> CreateLaVectorGradedStorage<T>(this Dictionary<uint, ILaVectorEvenStorage<T>> gradeKeyValueDictionary)
        {
            if (gradeKeyValueDictionary.Count == 0 || gradeKeyValueDictionary.Values.All(d => d.IsEmpty()))
                return LaVectorEmptyGradedStorage<T>.EmptyList;

            if (gradeKeyValueDictionary.Count != 1) 
                return new LaVectorSparseGradedStorage<T>(gradeKeyValueDictionary);

            var (grade, evenList) = 
                gradeKeyValueDictionary.First();

            return new LaVectorSingleGradeStorage<T>(grade, evenList);
        }
    }
}