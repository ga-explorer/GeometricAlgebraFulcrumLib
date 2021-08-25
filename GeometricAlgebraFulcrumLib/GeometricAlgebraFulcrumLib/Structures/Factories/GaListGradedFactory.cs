using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Collections;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Structures.Composers;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Structures.Factories
{
    public static class GaListGradedFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListGradedComposer<T> CreateGradedListComposer<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return new GaListGradedComposer<T>(scalarProcessor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListGradedDenseList<T> CreateGradedListDenseList<T>(this uint grade)
        {
            return new GaListGradedDenseList<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListGradedDenseList<T> CreateGradedListDenseList<T>(this uint grade, int capacity)
        {
            return new GaListGradedDenseList<T>(capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListGradedDenseList<T> CreateGradedListDenseList<T>(this uint grade, IEnumerable<IGaListEven<T>> evenLists)
        {
            return new GaListGradedDenseList<T>(evenLists);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListGradedDenseList<T> CreateGradedListDenseList<T>(this IEnumerable<IGaListEven<T>> evenLists, uint grade)
        {
            return new GaListGradedDenseList<T>(evenLists);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListGradedSingleGrade<T> CreateGradedListSingleGrade<T>(this uint grade, IGaListEven<T> evenList)
        {
            if (evenList.IsEmpty())
                return new GaListGradedSingleGradeEmpty<T>(grade);
            
            if (evenList is IGaListEvenSingleKey<T> singleKeyList)
                return new GaListGradedSingleGradeKey<T>(grade, singleKeyList);

            if (evenList.GetSparseCount() > 1) 
                return new GaListGradedSingleGrade<T>(grade, evenList);

            var (key, value) = evenList.GetMinKeyValueRecord();

            return new GaListGradedSingleGradeKey<T>(grade, key, value);

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListGradedSingleGrade<T> CreateGradedListSingleGrade<T>(this IGaListEven<T> evenList, uint grade)
        {
            if (evenList.IsEmpty())
                return new GaListGradedSingleGrade<T>(grade, GaListEvenEmpty<T>.EmptyList);

            if (evenList is IGaListEvenSingleKey<T> singleKeyList)
                return new GaListGradedSingleGradeKey<T>(grade, singleKeyList);

            if (evenList.GetSparseCount() > 1) 
                return new GaListGradedSingleGrade<T>(grade, evenList);

            var (key, value) = evenList.GetMinKeyValueRecord();

            return new GaListGradedSingleGradeKey<T>(grade, key, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListGradedSingleGradeKey<T> CreateGradedListSingleGrade<T>(this T value)
        {
            return new GaListGradedSingleGradeKey<T>(0U, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListGradedSingleGradeKey<T> CreateGradedListSingleGrade<T>(this T value, uint grade, ulong key)
        {
            return new GaListGradedSingleGradeKey<T>(grade, key, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListGradedSingleGradeKey<T> CreateGradedListSingleGrade<T>(this uint grade, ulong key, T value)
        {
            return new GaListGradedSingleGradeKey<T>(grade, key, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListGradedSingleGradeKey<T> CreateGradedListSingleGrade<T>(this uint grade, IGaListEvenSingleKey<T> singleKeyList)
        {
            return new GaListGradedSingleGradeKey<T>(grade, singleKeyList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListGradedSingleGradeKey<T> CreateGradedListSingleGrade<T>(this IGaListEvenSingleKey<T> singleKeyList, uint grade)
        {
            return new GaListGradedSingleGradeKey<T>(grade, singleKeyList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListGradedSparse<T> CreateGradedListSparse<T>(this Dictionary<uint, IGaListEven<T>> gradeKeyValueDictionary)
        {
            return new GaListGradedSparse<T>(gradeKeyValueDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListGradedSparse<T> CreateGradedListSparse<T>()
        {
            return new GaListGradedSparse<T>();
        }

        public static IGaListGraded<T> CreateGradedList<T>(this Dictionary<uint, Dictionary<ulong, T>> gradeKeyValueDictionary)
        {
            if (gradeKeyValueDictionary.Count == 0 || gradeKeyValueDictionary.Values.All(d => d.IsNullOrEmpty()))
                return GaListGradedEmpty<T>.EmptyList;

            if (gradeKeyValueDictionary.Count != 1)
            {
                return new GaListGradedSparse<T>(
                    gradeKeyValueDictionary
                        .CopyToDictionary(
                            dict => dict.CreateEvenList()
                        )
                );
            }

            var (grade, evenList) = 
                gradeKeyValueDictionary.First();

            return new GaListGradedSingleGrade<T>(
                grade, 
                evenList.CreateEvenList()
            );
        }

        public static IGaListGraded<T> CreateGradedList<T>(this Dictionary<uint, IGaListEven<T>> gradeKeyValueDictionary)
        {
            if (gradeKeyValueDictionary.Count == 0 || gradeKeyValueDictionary.Values.All(d => d.IsEmpty()))
                return GaListGradedEmpty<T>.EmptyList;

            if (gradeKeyValueDictionary.Count != 1) 
                return new GaListGradedSparse<T>(gradeKeyValueDictionary);

            var (grade, evenList) = 
                gradeKeyValueDictionary.First();

            return new GaListGradedSingleGrade<T>(grade, evenList);
        }
    }
}