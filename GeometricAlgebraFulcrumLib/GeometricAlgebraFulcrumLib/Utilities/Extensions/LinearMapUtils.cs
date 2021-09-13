using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class LinearMapUtils
    {
        public static T[,] GetMultivectorsMappingArray<T>(this IUnilinearMap<T> linearMap, int rowsCount, int colsCount)
        {
            var processor = linearMap.LinearProcessor;
            var array = new T[rowsCount, colsCount];

            for (var index = 0; index < colsCount; index++)
            {
                var mappedBasisBlade = 
                    linearMap.MapBasisBlade((ulong) index);

                for (var i = 0; i < rowsCount; i++)
                    array[i, index] = mappedBasisBlade.TryGetTermScalar((ulong) i, out var scalar)
                        ? scalar
                        : processor.ScalarZero;
            }

            return array;
        }
    }
}