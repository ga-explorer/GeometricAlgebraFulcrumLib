using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra
{
    public static class LinArrayFactory
    {
        public static T[] CreateArrayZero1D<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int count)
        {
            var exprArray = new T[count];

            for (var i = 0; i < count; i++)
                exprArray[i] = scalarProcessor.ScalarZero;

            return exprArray;
        }

        public static T[,] CreateArrayZero2D<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int count)
        {
            var exprArray = new T[count, count];

            for (var i = 0; i < count; i++)
            for (var j = 0; j < count; j++)
                exprArray[i, j] = scalarProcessor.ScalarZero;

            return exprArray;
        }

        public static T[,] CreateArrayZero2D<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int count1, int count2)
        {
            var exprArray = new T[count1, count2];

            for (var i = 0; i < count1; i++)
            for (var j = 0; j < count2; j++)
                exprArray[i, j] = scalarProcessor.ScalarZero;

            return exprArray;
        }

        public static T[,] CreateArrayIdentity2D<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int size)
        {
            var matrix = new T[size, size];

            for (var i = 0; i < size; i++)
            {
                matrix[i, i] = scalarProcessor.ScalarOne;

                for (var j = 0; j < i; j++)
                    matrix[i, j] = scalarProcessor.ScalarZero;

                for (var j = i + 1; j < size; j++)
                    matrix[i, j] = scalarProcessor.ScalarZero;
            }

            return matrix;
        }
    }
}