using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Arrays.Float64;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix<double> GetOrthogonalMatrix(this System.Random random, int size)
        {
            return random
                .GetFloat64Array2D(size, size)
                .ToMatrix()
                .QR()
                .Q;
        }

        public static IReadOnlyList<Vector<double>> GetOrthonormalVectors(this System.Random random, int size, int count)
        {
            count = Math.Min(count, size);

            var matrix = random.GetOrthogonalMatrix(size);

            var vectorArray = new Vector<double>[count];

            for (var i = 0; i < count; i++)
                vectorArray[i] = matrix.Column(i);

            return vectorArray;
        }
    }
}