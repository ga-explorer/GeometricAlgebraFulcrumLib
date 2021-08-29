using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaGridUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxBasisBladeId1<T>(this ILaMatrixEvenStorage<T> keyValueGrid)
        {
            return keyValueGrid.GetMaxIndex1();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxBasisBladeId2<T>(this ILaMatrixEvenStorage<T> keyValueGrid)
        {
            return keyValueGrid.GetMaxIndex2();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxBasisBladeId1<T>(this ILaMatrixEvenStorage<T> keyValueGrid, uint grade)
        {
            return keyValueGrid.GetMaxIndex1().BasisBladeIndexToId(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxBasisBladeId2<T>(this ILaMatrixEvenStorage<T> keyValueGrid, uint grade)
        {
            return keyValueGrid.GetMaxIndex2().BasisBladeIndexToId(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxBasisBladeId1<T>(this ILaMatrixGradedStorage<T> gradeKeyValueGrid)
        {
            var grade = gradeKeyValueGrid.GetMaxGrade();

            return gradeKeyValueGrid.TryGetStorage(grade, out var keyValueGrid) 
                ? keyValueGrid.GetMaxIndex1().BasisBladeIndexToId(grade) 
                : 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxBasisBladeId1<T>(this ILaMatrixGradedStorage<T> gradeKeyValueGrid, uint grade)
        {
            return gradeKeyValueGrid.TryGetStorage(grade, out var keyValueGrid) 
                ? keyValueGrid.GetMaxIndex1().BasisBladeIndexToId(grade) 
                : 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxBasisBladeId2<T>(this ILaMatrixGradedStorage<T> gradeKeyValueGrid)
        {
            var grade = gradeKeyValueGrid.GetMaxGrade();

            return gradeKeyValueGrid.TryGetStorage(grade, out var keyValueGrid) 
                ? keyValueGrid.GetMaxIndex2().BasisBladeIndexToId(grade) 
                : 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxBasisBladeId2<T>(this ILaMatrixGradedStorage<T> gradeKeyValueGrid, uint grade)
        {
            return gradeKeyValueGrid.TryGetStorage(grade, out var keyValueGrid) 
                ? keyValueGrid.GetMaxIndex2().BasisBladeIndexToId(grade) 
                : 0UL;
        }
    }
}