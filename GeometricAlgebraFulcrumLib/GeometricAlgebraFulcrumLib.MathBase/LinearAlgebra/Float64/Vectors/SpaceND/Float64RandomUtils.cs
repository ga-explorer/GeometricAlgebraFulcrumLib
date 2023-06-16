using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Matrices;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.SpaceND
{
    public static class Float64RandomUtils
    {
        public static double[] GetFloat64Array1D(this System.Random random, int size, double minValue, double maxValue)
        {
            var array = new double[size];

            for (var i = 0; i < size; i++)
                array[i] = random.GetNumber(minValue, maxValue);

            return array;
        }
    
        public static double[,] GetFloat64Array2D(this System.Random random, int rowCount, int colCount, double minValue = -1, double maxValue = 1)
        {
            var array = new double[rowCount, colCount];

            for (var i = 0; i < rowCount; i++)
            for (var j = 0; j < rowCount; j++)
                array[i, j] = random.GetNumber(minValue, maxValue);

            return array;
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[,] GetCirculantColumnArray(this System.Random random, int size, double minValue = -1d, double maxValue = 1d)
        {
            return Float64ArrayUtils.CreateCirculantColumnArray(
                random.GetNumbers(size, minValue, maxValue).ToImmutableArray()
            );
        }

    }
}