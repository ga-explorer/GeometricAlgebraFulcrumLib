using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.Matrices
{
    public static class GaLaMatrixFactory
    {
        public static T[,] CreateZeroScalarArray<T>(this IGaScalarProcessor<T> scalarProcessor, int rowsCount, int colsCount)
        {
            var matrix = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
                matrix[i, j] = scalarProcessor.GetZeroScalar();

            return matrix;
        }

        public static T[,] CreateZeroScalarArray<T>(this IGaScalarProcessor<T> scalarProcessor, int size)
        {
            var matrix = new T[size, size];

            for (var i = 0; i < size; i++)
            for (var j = 0; j < size; j++)
                matrix[i, j] = scalarProcessor.GetZeroScalar();

            return matrix;
        }

        public static T[,] CreateIdentityScalarArray<T>(this IGaScalarProcessor<T> scalarProcessor, int size)
        {
            var matrix = new T[size, size];

            for (var i = 0; i < size; i++)
            for (var j = 0; j < size; j++)
                matrix[i, j] = i == j
                    ? scalarProcessor.GetOneScalar()
                    : scalarProcessor.GetZeroScalar();

            return matrix;
        }
    }
}