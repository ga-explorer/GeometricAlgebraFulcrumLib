using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Grids.Graded;

namespace GeometricAlgebraFulcrumLib.Structures.Utils
{
    public static class GaGridUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxBasisBladeId1<T>(this IGaGridEven<T> keyValueGrid)
        {
            return keyValueGrid.GetMaxKey1();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxBasisBladeId2<T>(this IGaGridEven<T> keyValueGrid)
        {
            return keyValueGrid.GetMaxKey2();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxBasisBladeId1<T>(this IGaGridEven<T> keyValueGrid, uint grade)
        {
            return keyValueGrid.GetMaxKey1().BasisBladeIndexToId(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxBasisBladeId2<T>(this IGaGridEven<T> keyValueGrid, uint grade)
        {
            return keyValueGrid.GetMaxKey2().BasisBladeIndexToId(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxBasisBladeId1<T>(this IGaGridGraded<T> gradeKeyValueGrid)
        {
            var grade = gradeKeyValueGrid.GetMaxGrade();

            return gradeKeyValueGrid.TryGetGrid(grade, out var keyValueGrid) 
                ? keyValueGrid.GetMaxKey1().BasisBladeIndexToId(grade) 
                : 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxBasisBladeId1<T>(this IGaGridGraded<T> gradeKeyValueGrid, uint grade)
        {
            return gradeKeyValueGrid.TryGetGrid(grade, out var keyValueGrid) 
                ? keyValueGrid.GetMaxKey1().BasisBladeIndexToId(grade) 
                : 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxBasisBladeId2<T>(this IGaGridGraded<T> gradeKeyValueGrid)
        {
            var grade = gradeKeyValueGrid.GetMaxGrade();

            return gradeKeyValueGrid.TryGetGrid(grade, out var keyValueGrid) 
                ? keyValueGrid.GetMaxKey2().BasisBladeIndexToId(grade) 
                : 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxBasisBladeId2<T>(this IGaGridGraded<T> gradeKeyValueGrid, uint grade)
        {
            return gradeKeyValueGrid.TryGetGrid(grade, out var keyValueGrid) 
                ? keyValueGrid.GetMaxKey2().BasisBladeIndexToId(grade) 
                : 0UL;
        }
    }
}