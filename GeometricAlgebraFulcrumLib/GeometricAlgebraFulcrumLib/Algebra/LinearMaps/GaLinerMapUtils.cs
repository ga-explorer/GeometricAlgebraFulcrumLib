namespace GeometricAlgebraFulcrumLib.Algebra.LinearMaps
{
    public static class GaLinerMapUtils
    {

        public static T[,] GetMultivectorsMappingArray<T>(this IGaGeneralUnilinearMap<T> linearMap)
        {
            var processor = linearMap.ScalarsGridProcessor;
            var rowsCount = (int) linearMap.GaSpaceDimension;
            var colsCount = rowsCount;
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
        
        public static T[,] GetMultivectorsMappingArray<T>(this IGaGeneralUnilinearMap<T> linearMap, int rowsCount, int colsCount)
        {
            var processor = linearMap.ScalarsGridProcessor;
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