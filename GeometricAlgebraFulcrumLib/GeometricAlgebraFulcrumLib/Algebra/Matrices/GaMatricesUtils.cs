using System;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;
using GeometricAlgebraFulcrumLib.Storage.Utils;

namespace GeometricAlgebraFulcrumLib.Algebra.Matrices
{
    public static class GaMatricesUtils
    {
        public static T[,] CreateZeroMatrix<T>(this IGaScalarProcessor<T> scalarProcessor, int rowsCount, int colsCount)
        {
            var matrix = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
                matrix[i, j] = scalarProcessor.ZeroScalar;

            return matrix;
        }

        public static T[,] CreateZeroMatrix<T>(this IGaScalarProcessor<T> scalarProcessor, int size)
        {
            var matrix = new T[size, size];

            for (var i = 0; i < size; i++)
            for (var j = 0; j < size; j++)
                matrix[i, j] = scalarProcessor.ZeroScalar;

            return matrix;
        }

        public static T[,] CreateIdentityMatrix<T>(this IGaScalarProcessor<T> scalarProcessor, int size)
        {
            var matrix = new T[size, size];

            for (var i = 0; i < size; i++)
            for (var j = 0; j < size; j++)
                matrix[i, j] = i == j
                    ? scalarProcessor.OneScalar
                    : scalarProcessor.ZeroScalar;

            return matrix;
        }

        public static T[,] Add<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] matrix, T scalar)
        {
            var rowsCount = matrix.GetLength(0);
            var colsCount = matrix.GetLength(1);
            var newMatrix = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
                newMatrix[i, j] = scalarProcessor.Add(matrix[i, j], scalar);

            return newMatrix;
        }

        public static T[,] Add<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, T[,] matrix)
        {
            var rowsCount = matrix.GetLength(0);
            var colsCount = matrix.GetLength(1);
            var newMatrix = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
                newMatrix[i, j] = scalarProcessor.Add(scalar, matrix[i, j]);

            return newMatrix;
        }

        public static T[,] Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] matrix, T scalar)
        {
            var rowsCount = matrix.GetLength(0);
            var colsCount = matrix.GetLength(1);
            var newMatrix = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
                newMatrix[i, j] = scalarProcessor.Subtract(matrix[i, j], scalar);

            return newMatrix;
        }

        public static T[,] Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, T[,] matrix)
        {
            var rowsCount = matrix.GetLength(0);
            var colsCount = matrix.GetLength(1);
            var newMatrix = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
                newMatrix[i, j] = scalarProcessor.Subtract(scalar, matrix[i, j]);

            return newMatrix;
        }

        public static T[,] Times<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] matrix, T scalar)
        {
            var rowsCount = matrix.GetLength(0);
            var colsCount = matrix.GetLength(1);
            var newMatrix = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
                newMatrix[i, j] = scalarProcessor.Times(matrix[i, j], scalar);

            return newMatrix;
        }

        public static T[,] Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, T[,] matrix)
        {
            var rowsCount = matrix.GetLength(0);
            var colsCount = matrix.GetLength(1);
            var newMatrix = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
                newMatrix[i, j] = scalarProcessor.Times(scalar, matrix[i, j]);

            return newMatrix;
        }

        public static T[,] Divide<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] matrix, T scalar)
        {
            var rowsCount = matrix.GetLength(0);
            var colsCount = matrix.GetLength(1);
            var newMatrix = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
                newMatrix[i, j] = scalarProcessor.Divide(matrix[i, j], scalar);

            return newMatrix;
        }

        public static T[,] MatrixTimes<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] matrix1, T[,] matrix2)
        {
            var rowsCount = matrix1.GetLength(0);
            var colsCount = matrix2.GetLength(1);
            var innerCount = matrix1.GetLength(1);

            if (innerCount != matrix2.GetLength(0))
                throw new InvalidOperationException();

            var matrix = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            {
                for (var j = 0; j < colsCount; j++)
                {
                    var scalar = scalarProcessor.ZeroScalar;

                    for (var k = 0; k < innerCount; k++)
                    {
                        scalar = scalarProcessor.Add(
                            scalar, 
                            scalarProcessor.Times(matrix1[i, k], matrix2[k, j])
                        );
                    }

                    matrix[i, j] = scalar;
                }
            }

            return matrix;
        }

        public static IGaStorageVector<T> MapVector<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] matrix, IGaStorageVector<T> vector)
        {
            var rowsCount = matrix.GetLength(0);
            var colsCount = matrix.GetLength(1);
            var composer = new GaStorageComposerVector<T>(scalarProcessor);

            foreach (var (index, scalar) in vector.IndexScalarDictionary)
            {
                if (index >= (ulong) colsCount)
                    continue;

                var j = (int) index;
                for (var i = 0; i < rowsCount; i++)
                {
                    var matrixScalar = matrix[i, j];

                    if (ReferenceEquals(matrixScalar, null))
                        continue;

                    composer.AddTerm(
                        (ulong) i,
                        scalarProcessor.Times(scalar, matrixScalar)
                    );
                }
            }

            composer.RemoveZeroTerms();

            return composer.GetVector();
        }


        public static T[,] CreateRotationMatrixToVector<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> sourceVector, IGaStorageVector<T> targetVector, int size)
        {
            var matrix1 = scalarProcessor.CreateRotationMatrixFromE1(targetVector, size);
            var matrix2 = scalarProcessor.CreateRotationMatrixToE1(sourceVector, size);

            return scalarProcessor.MatrixTimes(matrix1, matrix2);
        }

        public static T[,] CreateRotationMatrixFromVector<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> targetVector, IGaStorageVector<T> sourceVector, int size)
        {
            var matrix1 = scalarProcessor.CreateRotationMatrixFromE1(targetVector, size);
            var matrix2 = scalarProcessor.CreateRotationMatrixToE1(sourceVector, size);

            return scalarProcessor.MatrixTimes(matrix1, matrix2);
        }

        /// <summary>
        /// This method creates a rotation matrix which rotates the basis vector e_1 into
        /// the given unit vector
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scalarProcessor"></param>
        /// <param name="targetVector"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static T[,] CreateRotationMatrixFromE1<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> targetVector, int size)
        {
            if (size < 2)
                throw new ArgumentOutOfRangeException(nameof(size));

            // Special case: vector == e_1
            var v1 = scalarProcessor.GetTermScalarByIndex(targetVector, 0);
            if (scalarProcessor.IsOne(v1))
                return scalarProcessor.CreateIdentityMatrix(size);

            v1 = scalarProcessor.Add(scalarProcessor.OneScalar, v1);

            // Special case: vector == -e_1
            if (scalarProcessor.IsZero(v1))
            {
                //TODO: Handle this case
                throw new InvalidOperationException();
            }

            var matrix = 
                scalarProcessor.CreateZeroMatrix(size);

            var indexScalarPairs = 
                targetVector.IndexScalarDictionary;

            // Fill first row
            foreach (var (index, scalar) in indexScalarPairs)
            {
                if (index == 0)
                    continue;

                matrix[0, index] = 
                    scalarProcessor.Negative(scalar);
            }

            // Fill first column
            foreach (var (index, scalar) in indexScalarPairs)
                matrix[index, 0] = scalar;

            // Fill diagonal
            foreach (var (index, scalar) in indexScalarPairs)
            {
                if (index == 0)
                    continue;

                matrix[index, index] = 
                    scalarProcessor.Subtract(
                        scalarProcessor.OneScalar,
                        scalarProcessor.Divide(
                            scalarProcessor.Square(scalar),
                            v1
                        )
                    );
            }

            // Fill remaining items
            foreach (var (index1, scalar1) in indexScalarPairs)
            {
                foreach (var (index2, scalar2) in indexScalarPairs)
                {
                    if (index1 == 0 || index2 == 0 || index1 == index2)
                        continue;

                    matrix[index1, index2] = 
                        scalarProcessor.Divide(
                            scalarProcessor.NegativeTimes(scalar1, scalar2),
                            v1
                        );
                }
            }

            return matrix;
        }

        /// <summary>
        /// This method creates a rotation matrix which rotates the given unit vector
        /// into the basis vector e_1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scalarProcessor"></param>
        /// <param name="sourceVector"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static T[,] CreateRotationMatrixToE1<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> sourceVector, int size)
        {
            if (size < 2)
                throw new ArgumentOutOfRangeException(nameof(size));

            // Special case: vector == e_1
            var v1 = scalarProcessor.GetTermScalarByIndex(sourceVector, 0);
            if (scalarProcessor.IsOne(v1))
                return scalarProcessor.CreateIdentityMatrix(size);

            v1 = scalarProcessor.Add(scalarProcessor.OneScalar, v1);

            // Special case: vector == -e_1
            if (scalarProcessor.IsZero(v1))
            {
                //TODO: Handle this case
                throw new InvalidOperationException();
            }

            var matrix = 
                scalarProcessor.CreateZeroMatrix(size);

            var indexScalarPairs = 
                sourceVector.IndexScalarDictionary;

            // Fill first column
            foreach (var (index, scalar) in indexScalarPairs)
            {
                if (index == 0)
                    continue;

                matrix[index, 0] = 
                    scalarProcessor.Negative(scalar);
            }

            // Fill first row
            foreach (var (index, scalar) in indexScalarPairs)
                matrix[0, index] = scalar;

            // Fill diagonal
            foreach (var (index, scalar) in indexScalarPairs)
            {
                if (index == 0)
                    continue;

                matrix[index, index] = 
                    scalarProcessor.Subtract(
                        scalarProcessor.OneScalar,
                        scalarProcessor.Divide(
                            scalarProcessor.Square(scalar),
                            v1
                        )
                    );
            }

            // Fill remaining items
            foreach (var (index1, scalar1) in indexScalarPairs)
            {
                foreach (var (index2, scalar2) in indexScalarPairs)
                {
                    if (index1 == 0 || index2 == 0 || index1 == index2)
                        continue;

                    matrix[index1, index2] = 
                        scalarProcessor.Divide(
                            scalarProcessor.NegativeTimes(scalar1, scalar2),
                            v1
                        );
                }
            }

            return matrix;
        }
    }
}
