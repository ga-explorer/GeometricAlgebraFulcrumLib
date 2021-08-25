using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Structures.Utils
{
    public static class GaCollectionUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<T>(this IGaCollection<T> collection)
        {
            return collection is null || collection.IsEmpty();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> GetMappedValues<T, T2>(this IGaCollection<T> collection, Func<T, T2> valueMapping)
        {
            return collection.GetValues().Select(valueMapping);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<uint> GetGradesSetUnion<T>(this IGaCollectionGraded<T> gradedList1, IGaCollectionGraded<T> gradedList2)
        {
            var keysSet = new HashSet<uint>(gradedList1.GetGrades());

            keysSet.UnionWith(gradedList2.GetGrades());

            return keysSet;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<uint> GetGradesSetIntersection<T>(this IGaCollectionGraded<T> gradedList1, IGaCollectionGraded<T> gradedList2)
        {
            var keysSet = new HashSet<uint>(gradedList1.GetGrades());

            keysSet.IntersectWith(gradedList2.GetGrades());

            return keysSet;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<uint> GetGradesSetDifference<T>(this IGaCollectionGraded<T> gradedList1, IGaCollectionGraded<T> gradedList2)
        {
            var keysSet = new HashSet<uint>(gradedList1.GetGrades());

            keysSet.ExceptWith(gradedList2.GetGrades());

            return keysSet;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<uint> GetGradesSetSymmetricDifference<T>(this IGaCollectionGraded<T> gradedList1, IGaCollectionGraded<T> gradedList2)
        {
            var keysSet = new HashSet<uint>(gradedList1.GetGrades());

            keysSet.SymmetricExceptWith(gradedList2.GetGrades());

            return keysSet;
        }




    }
}