using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class LinArrayStorageUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<T>(this ILinArrayStorage<T> collection)
        {
            return collection is null || collection.IsEmpty();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> GetMappedValues<T, T2>(this ILinArrayStorage<T> collection, Func<T, T2> valueMapping)
        {
            return collection.GetScalars().Select(valueMapping);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<uint> GetGradesSetUnion<T>(this ILinArrayGradedStorage<T> gradedList1, ILinArrayGradedStorage<T> gradedList2)
        {
            var keysSet = new HashSet<uint>(gradedList1.GetGrades());

            keysSet.UnionWith(gradedList2.GetGrades());

            return keysSet;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<uint> GetGradesSetIntersection<T>(this ILinArrayGradedStorage<T> gradedList1, ILinArrayGradedStorage<T> gradedList2)
        {
            var keysSet = new HashSet<uint>(gradedList1.GetGrades());

            keysSet.IntersectWith(gradedList2.GetGrades());

            return keysSet;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<uint> GetGradesSetDifference<T>(this ILinArrayGradedStorage<T> gradedList1, ILinArrayGradedStorage<T> gradedList2)
        {
            var keysSet = new HashSet<uint>(gradedList1.GetGrades());

            keysSet.ExceptWith(gradedList2.GetGrades());

            return keysSet;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<uint> GetGradesSetSymmetricDifference<T>(this ILinArrayGradedStorage<T> gradedList1, ILinArrayGradedStorage<T> gradedList2)
        {
            var keysSet = new HashSet<uint>(gradedList1.GetGrades());

            keysSet.SymmetricExceptWith(gradedList2.GetGrades());

            return keysSet;
        }




    }
}