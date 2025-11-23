using System.Text;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.EllipseFitting
{
    /// <summary>
    /// A comprehensive matrix operations class providing static methods for linear algebra computations.
    /// This class converts Python/numpy functionality to native C# mathematical operations.
    /// </summary>
    public static class Matrix
    {
        #region Constants

        /// <summary>
        /// Machine epsilon for numerical precision comparisons.
        /// </summary>
        public const double MachineEpsilon = 2.220446049250313e-16;

        /// <summary>
        /// Default tolerance for numerical computations.
        /// </summary>
        public const double DefaultTolerance = 1e-12;

        #endregion

        #region Matrix Creation and Utilities

        /// <summary>
        /// Creates an identity matrix of the specified size.
        /// </summary>
        /// <param name="size">The size of the square identity matrix.</param>
        /// <returns>An identity matrix of the specified size.</returns>
        /// <exception cref="ArgumentException">Thrown when size is less than 1.</exception>
        public static double[,] CreateIdentity(int size)
        {
            if (size < 1)
                throw new ArgumentException("Matrix size must be at least 1.", nameof(size));

            var identity = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                identity[i, i] = 1.0;
            }
            return identity;
        }

        /// <summary>
        /// Creates a zero matrix of the specified dimensions.
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="cols">Number of columns.</param>
        /// <returns>A zero matrix of the specified dimensions.</returns>
        /// <exception cref="ArgumentException">Thrown when rows or cols is less than 1.</exception>
        public static double[,] CreateZeros(int rows, int cols)
        {
            if (rows < 1 || cols < 1)
                throw new ArgumentException("Matrix dimensions must be at least 1.");

            return new double[rows, cols];
        }

        /// <summary>
        /// Creates a deep copy of the input matrix.
        /// </summary>
        /// <param name="matrix">The matrix to clone.</param>
        /// <returns>A deep copy of the input matrix.</returns>
        /// <exception cref="ArgumentNullException">Thrown when matrix is null.</exception>
        public static double[,] Clone(double[,] matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            var clone = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    clone[i, j] = matrix[i, j];
                }
            }
            return clone;
        }

        /// <summary>
        /// Validates that the matrix is square and not null.
        /// </summary>
        /// <param name="matrix">The matrix to validate.</param>
        /// <param name="parameterName">The parameter name for exception reporting.</param>
        /// <exception cref="ArgumentNullException">Thrown when matrix is null.</exception>
        /// <exception cref="ArgumentException">Thrown when matrix is not square.</exception>
        public static void ValidateSquareMatrix(double[,] matrix, string parameterName = "matrix")
        {
            if (matrix == null)
                throw new ArgumentNullException(parameterName);

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            if (rows != cols)
                throw new ArgumentException($"Matrix must be square. Actual dimensions: {rows}x{cols}.", parameterName);

            if (rows == 0)
                throw new ArgumentException("Matrix cannot be empty.", parameterName);
        }

        /// <summary>
        /// Validates that the matrix is symmetric within the specified tolerance.
        /// </summary>
        /// <param name="matrix">The matrix to validate.</param>
        /// <param name="tolerance">The tolerance for symmetry check.</param>
        /// <param name="parameterName">The parameter name for exception reporting.</param>
        /// <exception cref="ArgumentException">Thrown when matrix is not symmetric.</exception>
        public static void ValidateSymmetricMatrix(double[,] matrix, double tolerance = 1e-10, string parameterName = "matrix")
        {
            ValidateSquareMatrix(matrix, parameterName);

            int size = matrix.GetLength(0);
            double maxAsymmetry = 0.0;

            for (int i = 0; i < size; i++)
            {
                for (int j = i + 1; j < size; j++)
                {
                    double asymmetry = Math.Abs(matrix[i, j] - matrix[j, i]);
                    maxAsymmetry = Math.Max(maxAsymmetry, asymmetry);
                }
            }

            if (maxAsymmetry > tolerance)
            {
                throw new ArgumentException(
                    $"Matrix is not symmetric within tolerance {tolerance}. Maximum asymmetry: {maxAsymmetry}.",
                    parameterName);
            }
        }

        #endregion

        #region Matrix Operations

        /// <summary>
        /// Multiplies two matrices A and B, returning the result C = A * B.
        /// </summary>
        /// <param name="matrixA">The first matrix.</param>
        /// <param name="matrixB">The second matrix.</param>
        /// <returns>The product matrix C = A * B.</returns>
        /// <exception cref="ArgumentNullException">Thrown when either matrix is null.</exception>
        /// <exception cref="ArgumentException">Thrown when matrix dimensions are incompatible for multiplication.</exception>
        public static double[,] Multiply(double[,] matrixA, double[,] matrixB)
        {
            if (matrixA == null)
                throw new ArgumentNullException(nameof(matrixA));
            if (matrixB == null)
                throw new ArgumentNullException(nameof(matrixB));

            int rowsA = matrixA.GetLength(0);
            int colsA = matrixA.GetLength(1);
            int rowsB = matrixB.GetLength(0);
            int colsB = matrixB.GetLength(1);

            if (colsA != rowsB)
            {
                throw new ArgumentException(
                    $"Cannot multiply matrices: A is {rowsA}x{colsA}, B is {rowsB}x{colsB}. " +
                    "Number of columns in A must equal number of rows in B.");
            }

            var result = new double[rowsA, colsB];

            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    double sum = 0.0;
                    for (int k = 0; k < colsA; k++)
                    {
                        sum += matrixA[i, k] * matrixB[k, j];
                    }
                    result[i, j] = sum;
                }
            }

            return result;
        }

        /// <summary>
        /// Computes the transpose of a matrix.
        /// </summary>
        /// <param name="matrix">The matrix to transpose.</param>
        /// <returns>The transposed matrix.</returns>
        /// <exception cref="ArgumentNullException">Thrown when matrix is null.</exception>
        public static double[,] Transpose(double[,] matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            var transposed = new double[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    transposed[j, i] = matrix[i, j];
                }
            }

            return transposed;
        }

        /// <summary>
        /// Computes the Euclidean norm (L2 norm) of a vector.
        /// </summary>
        /// <param name="vector">The input vector as a 1D array.</param>
        /// <returns>The Euclidean norm of the vector.</returns>
        /// <exception cref="ArgumentNullException">Thrown when vector is null.</exception>
        public static double VectorNorm(double[] vector)
        {
            if (vector == null)
                throw new ArgumentNullException(nameof(vector));

            double sumOfSquares = 0.0;
            for (int i = 0; i < vector.Length; i++)
            {
                sumOfSquares += vector[i] * vector[i];
            }
            return Math.Sqrt(sumOfSquares);
        }

        /// <summary>
        /// Computes the Frobenius norm of a matrix.
        /// </summary>
        /// <param name="matrix">The input matrix.</param>
        /// <returns>The Frobenius norm of the matrix.</returns>
        /// <exception cref="ArgumentNullException">Thrown when matrix is null.</exception>
        public static double FrobeniusNorm(double[,] matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double sumOfSquares = 0.0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sumOfSquares += matrix[i, j] * matrix[i, j];
                }
            }

            return Math.Sqrt(sumOfSquares);
        }

        /// <summary>
        /// Extracts a column from a matrix as a 1D array.
        /// </summary>
        /// <param name="matrix">The source matrix.</param>
        /// <param name="columnIndex">The index of the column to extract.</param>
        /// <returns>The specified column as a 1D array.</returns>
        /// <exception cref="ArgumentNullException">Thrown when matrix is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when columnIndex is out of range.</exception>
        public static double[] ExtractColumn(double[,] matrix, int columnIndex)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            if (columnIndex < 0 || columnIndex >= cols)
                throw new ArgumentOutOfRangeException(nameof(columnIndex), 
                    $"Column index {columnIndex} is out of range [0, {cols - 1}].");

            var column = new double[rows];
            for (int i = 0; i < rows; i++)
            {
                column[i] = matrix[i, columnIndex];
            }
            return column;
        }

        /// <summary>
        /// Sets a column in a matrix from a 1D array.
        /// </summary>
        /// <param name="matrix">The target matrix.</param>
        /// <param name="columnIndex">The index of the column to set.</param>
        /// <param name="column">The column values to set.</param>
        /// <exception cref="ArgumentNullException">Thrown when matrix or column is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when columnIndex is out of range.</exception>
        /// <exception cref="ArgumentException">Thrown when column length doesn't match matrix rows.</exception>
        public static void SetColumn(double[,] matrix, int columnIndex, double[] column)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));
            if (column == null)
                throw new ArgumentNullException(nameof(column));

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            if (columnIndex < 0 || columnIndex >= cols)
                throw new ArgumentOutOfRangeException(nameof(columnIndex), 
                    $"Column index {columnIndex} is out of range [0, {cols - 1}].");

            if (column.Length != rows)
                throw new ArgumentException(
                    $"Column length {column.Length} doesn't match matrix rows {rows}.", nameof(column));

            for (int i = 0; i < rows; i++)
            {
                matrix[i, columnIndex] = column[i];
            }
        }

        #endregion

        #region Display and Debugging

        /// <summary>
        /// Converts a matrix to a formatted string representation for debugging and display.
        /// </summary>
        /// <param name="matrix">The matrix to convert to string.</param>
        /// <param name="format">The numeric format string (default: "F6").</param>
        /// <returns>A formatted string representation of the matrix.</returns>
        /// <exception cref="ArgumentNullException">Thrown when matrix is null.</exception>
        public static string ToString(double[,] matrix, string format = "F6")
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            var sb = new StringBuilder();

            sb.AppendLine($"Matrix [{rows}x{cols}]:");
            for (int i = 0; i < rows; i++)
            {
                sb.Append("  [");
                for (int j = 0; j < cols; j++)
                {
                    sb.Append(matrix[i, j].ToString(format));
                    if (j < cols - 1) sb.Append(", ");
                }
                sb.AppendLine("]");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts a 1D array to a formatted string representation.
        /// </summary>
        /// <param name="array">The array to convert to string.</param>
        /// <param name="format">The numeric format string (default: "F6").</param>
        /// <returns>A formatted string representation of the array.</returns>
        /// <exception cref="ArgumentNullException">Thrown when array is null.</exception>
        public static string ToString(double[] array, string format = "F6")
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            var sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < array.Length; i++)
            {
                sb.Append(array[i].ToString(format));
                if (i < array.Length - 1) sb.Append(", ");
            }
            sb.Append("]");

            return sb.ToString();
        }

        #endregion
    }

    /// <summary>
    /// Provides QR decomposition functionality using Householder transformations.
    /// This class converts Python/numpy QR decomposition algorithms to native C# implementations.
    /// </summary>
    public static class QRDecomposition
    {
        #region Public Methods

        /// <summary>
        /// Performs QR decomposition of a matrix using Householder transformations.
        /// </summary>
        /// <param name="matrix">The matrix to decompose.</param>
        /// <returns>A tuple containing the orthogonal matrix Q and upper triangular matrix R.</returns>
        /// <exception cref="ArgumentNullException">Thrown when matrix is null.</exception>
        /// <exception cref="ArgumentException">Thrown when matrix is empty.</exception>
        /// <remarks>
        /// The QR decomposition factors a matrix A into the product of an orthogonal matrix Q
        /// and an upper triangular matrix R, such that A = Q * R.
        /// This implementation uses Householder reflections for numerical stability.
        /// </remarks>
        public static (double[,] Q, double[,] R) Decompose(double[,] matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            if (rows == 0 || cols == 0)
                throw new ArgumentException("Matrix cannot be empty.", nameof(matrix));

            // Create working copy of the matrix
            var R = Matrix.Clone(matrix);
            var Q = Matrix.CreateIdentity(rows);

            // Apply Householder transformations
            int minDimension = Math.Min(rows, cols);
            for (int k = 0; k < minDimension; k++)
            {
                // Extract column vector from k-th column, starting from k-th row
                var columnVector = ExtractSubcolumn(R, k, k);

                // Compute Householder vector
                var (householderVector, beta) = ComputeHouseholderVector(columnVector);

                if (Math.Abs(beta) > Matrix.MachineEpsilon)
                {
                    // Apply Householder transformation to R
                    ApplyHouseholderToMatrix(R, householderVector, beta, k, k);

                    // Apply Householder transformation to Q
                    ApplyHouseholderToQ(Q, householderVector, beta, k);
                }
            }

            return (Q, R);
        }

        /// <summary>
        /// Solves the linear system Ax = b using QR decomposition.
        /// </summary>
        /// <param name="matrixA">The coefficient matrix A.</param>
        /// <param name="vectorB">The right-hand side vector b.</param>
        /// <returns>The solution vector x.</returns>
        /// <exception cref="ArgumentNullException">Thrown when matrixA or vectorB is null.</exception>
        /// <exception cref="ArgumentException">Thrown when dimensions are incompatible.</exception>
        public static double[] Solve(double[,] matrixA, double[] vectorB)
        {
            if (matrixA == null)
                throw new ArgumentNullException(nameof(matrixA));
            if (vectorB == null)
                throw new ArgumentNullException(nameof(vectorB));

            int rows = matrixA.GetLength(0);
            int cols = matrixA.GetLength(1);

            if (vectorB.Length != rows)
                throw new ArgumentException($"Vector b length {vectorB.Length} must match matrix rows {rows}.");

            if (rows < cols)
                throw new ArgumentException("Matrix must have at least as many rows as columns for QR solve.");

            // Perform QR decomposition
            var (Q, R) = Decompose(matrixA);

            // Compute Q^T * b
            var QTb = new double[cols];
            for (int i = 0; i < cols; i++)
            {
                QTb[i] = 0.0;
                for (int j = 0; j < rows; j++)
                {
                    QTb[i] += Q[j, i] * vectorB[j];
                }
            }

            // Solve Rx = Q^T * b using back substitution
            var solution = new double[cols];
            for (int i = cols - 1; i >= 0; i--)
            {
                solution[i] = QTb[i];
                for (int j = i + 1; j < cols; j++)
                {
                    solution[i] -= R[i, j] * solution[j];
                }
                solution[i] /= R[i, i];
            }

            return solution;
        }

        #endregion

        #region Private Helper Methods

        /// <summary>
        /// Extracts a subcolumn from a matrix starting from the specified row and column.
        /// </summary>
        /// <param name="matrix">The source matrix.</param>
        /// <param name="startRow">The starting row index.</param>
        /// <param name="columnIndex">The column index.</param>
        /// <returns>A subcolumn as a 1D array.</returns>
        private static double[] ExtractSubcolumn(double[,] matrix, int startRow, int columnIndex)
        {
            int rows = matrix.GetLength(0);
            int subcolumnLength = rows - startRow;
            var subcolumn = new double[subcolumnLength];

            for (int i = 0; i < subcolumnLength; i++)
            {
                subcolumn[i] = matrix[startRow + i, columnIndex];
            }

            return subcolumn;
        }

        /// <summary>
        /// Computes the Householder vector and beta coefficient for the given vector.
        /// </summary>
        /// <param name="vector">The input vector.</param>
        /// <returns>A tuple containing the Householder vector and beta coefficient.</returns>
        private static (double[] householderVector, double beta) ComputeHouseholderVector(double[] vector)
        {
            int n = vector.Length;
            var householderVector = new double[n];
            Array.Copy(vector, householderVector, n);

            double norm = Matrix.VectorNorm(vector);
            
            if (Math.Abs(norm) < Matrix.MachineEpsilon)
            {
                return (householderVector, 0.0);
            }

            // Choose sign to avoid catastrophic cancellation
            double sign = vector[0] >= 0 ? 1.0 : -1.0;
            householderVector[0] += sign * norm;

            double householderNorm = Matrix.VectorNorm(householderVector);
            
            if (Math.Abs(householderNorm) < Matrix.MachineEpsilon)
            {
                return (householderVector, 0.0);
            }

            // Normalize the Householder vector
            for (int i = 0; i < n; i++)
            {
                householderVector[i] /= householderNorm;
            }

            double beta = 2.0;
            return (householderVector, beta);
        }

        /// <summary>
        /// Applies a Householder transformation to a matrix: A := A - beta * v * (v^T * A).
        /// </summary>
        /// <param name="matrix">The matrix to transform.</param>
        /// <param name="householderVector">The Householder vector.</param>
        /// <param name="beta">The beta coefficient.</param>
        /// <param name="startRow">The starting row for the transformation.</param>
        /// <param name="startCol">The starting column for the transformation.</param>
        private static void ApplyHouseholderToMatrix(double[,] matrix, double[] householderVector, 
            double beta, int startRow, int startCol)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int vectorLength = householderVector.Length;

            // Compute w = v^T * A (for the submatrix)
            var w = new double[cols - startCol];
            for (int j = startCol; j < cols; j++)
            {
                w[j - startCol] = 0.0;
                for (int i = 0; i < vectorLength; i++)
                {
                    if (startRow + i < rows)
                    {
                        w[j - startCol] += householderVector[i] * matrix[startRow + i, j];
                    }
                }
            }

            // Apply transformation: A := A - beta * v * w^T
            for (int i = 0; i < vectorLength; i++)
            {
                if (startRow + i < rows)
                {
                    for (int j = startCol; j < cols; j++)
                    {
                        matrix[startRow + i, j] -= beta * householderVector[i] * w[j - startCol];
                    }
                }
            }
        }

        /// <summary>
        /// Applies a Householder transformation to the Q matrix: Q := Q * (I - beta * v * v^T).
        /// </summary>
        /// <param name="Q">The Q matrix to transform.</param>
        /// <param name="householderVector">The Householder vector.</param>
        /// <param name="beta">The beta coefficient.</param>
        /// <param name="startIndex">The starting index for the transformation.</param>
        private static void ApplyHouseholderToQ(double[,] Q, double[] householderVector, double beta, int startIndex)
        {
            int rows = Q.GetLength(0);
            int cols = Q.GetLength(1);
            int vectorLength = householderVector.Length;

            // Compute w = Q * v (for the relevant submatrix)
            var w = new double[rows];
            for (int i = 0; i < rows; i++)
            {
                w[i] = 0.0;
                for (int j = 0; j < vectorLength; j++)
                {
                    if (startIndex + j < cols)
                    {
                        w[i] += Q[i, startIndex + j] * householderVector[j];
                    }
                }
            }

            // Apply transformation: Q := Q - beta * w * v^T
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < vectorLength; j++)
                {
                    if (startIndex + j < cols)
                    {
                        Q[i, startIndex + j] -= beta * w[i] * householderVector[j];
                    }
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// Provides eigenvalue and eigenvector computation using QR algorithm with shifts.
    /// This class implements the standard QR algorithm for computing eigenvalues and eigenvectors
    /// of real matrices, with proper C# conventions and error handling.
    /// </summary>
    public static class EigenSolver
    {
        #region Constants

        /// <summary>
        /// Default maximum number of iterations for the QR algorithm.
        /// </summary>
        public const int DefaultMaxIterations = 1000;

        /// <summary>
        /// Default convergence tolerance.
        /// </summary>
        public const double DefaultTolerance = 1e-12;

        #endregion

        #region Public Methods

        /// <summary>
        /// Computes eigenvalues and eigenvectors of a real symmetric matrix using the QR algorithm.
        /// </summary>
        /// <param name="matrix">The symmetric matrix for which to compute eigenvalues and eigenvectors.</param>
        /// <returns>A tuple containing eigenvalues (sorted in descending order) and corresponding eigenvectors.</returns>
        /// <exception cref="ArgumentNullException">Thrown when matrix is null.</exception>
        /// <exception cref="ArgumentException">Thrown when matrix is not square or symmetric.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the algorithm fails to converge.</exception>
        /// <remarks>
        /// This method implements the QR algorithm with Householder transformations for symmetric matrices.
        /// The eigenvalues are returned in descending order, and each column of the eigenvector matrix
        /// corresponds to the eigenvalue at the same index.
        /// </remarks>
        public static (double[] eigenvalues, double[,] eigenvectors) ComputeEigenDecomposition(double[,] matrix)
        {
            return ComputeEigenDecomposition(matrix, DefaultTolerance, DefaultMaxIterations);
        }

        /// <summary>
        /// Computes eigenvalues and eigenvectors of a real symmetric matrix using the QR algorithm
        /// with custom tolerance and maximum iterations.
        /// </summary>
        /// <param name="matrix">The symmetric matrix for which to compute eigenvalues and eigenvectors.</param>
        /// <param name="tolerance">Convergence tolerance for the QR algorithm.</param>
        /// <param name="maxIterations">Maximum number of iterations allowed.</param>
        /// <returns>A tuple containing eigenvalues (sorted in descending order) and corresponding eigenvectors.</returns>
        /// <exception cref="ArgumentNullException">Thrown when matrix is null.</exception>
        /// <exception cref="ArgumentException">Thrown when matrix is not square, symmetric, or parameters are invalid.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the algorithm fails to converge.</exception>
        public static (double[] eigenvalues, double[,] eigenvectors) ComputeEigenDecomposition(
            double[,] matrix, double tolerance, int maxIterations)
        {
            // Input validation
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            Matrix.ValidateSquareMatrix(matrix);
            Matrix.ValidateSymmetricMatrix(matrix, tolerance);

            if (tolerance <= 0 || tolerance > 1e-6)
                throw new ArgumentException("Tolerance must be positive and not greater than 1e-6.", nameof(tolerance));

            if (maxIterations <= 0)
                throw new ArgumentException("Maximum iterations must be positive.", nameof(maxIterations));

            int size = matrix.GetLength(0);

            // Create working copies
            var workingMatrix = Matrix.Clone(matrix);
            var eigenvectorMatrix = Matrix.CreateIdentity(size);

            // Apply QR algorithm
            bool converged = ApplyQRAlgorithm(workingMatrix, eigenvectorMatrix, tolerance, maxIterations);

            if (!converged)
            {
                throw new InvalidOperationException(
                    $"QR algorithm failed to converge within {maxIterations} iterations. " +
                    "Consider increasing maxIterations or relaxing tolerance.");
            }

            // Extract eigenvalues from diagonal
            var eigenvalues = new double[size];
            for (int i = 0; i < size; i++)
            {
                eigenvalues[i] = workingMatrix[i, i];
            }

            // Normalize eigenvectors
            NormalizeEigenvectors(eigenvectorMatrix);

            // Sort eigenvalues and eigenvectors in descending order
            SortEigenDecomposition(eigenvalues, eigenvectorMatrix);

            return (eigenvalues, eigenvectorMatrix);
        }

        /// <summary>
        /// Verifies the computed eigendecomposition by checking A*v = λ*v for each eigenpair.
        /// </summary>
        /// <param name="originalMatrix">The original matrix.</param>
        /// <param name="eigenvalues">The computed eigenvalues.</param>
        /// <param name="eigenvectors">The computed eigenvectors.</param>
        /// <param name="tolerance">Verification tolerance.</param>
        /// <returns>True if the decomposition is valid within the specified tolerance, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public static bool VerifyEigenDecomposition(double[,] originalMatrix, double[] eigenvalues, 
            double[,] eigenvectors, double tolerance = 1e-10)
        {
            if (originalMatrix == null)
                throw new ArgumentNullException(nameof(originalMatrix));
            if (eigenvalues == null)
                throw new ArgumentNullException(nameof(eigenvalues));
            if (eigenvectors == null)
                throw new ArgumentNullException(nameof(eigenvectors));

            int size = originalMatrix.GetLength(0);

            if (eigenvalues.Length != size || 
                eigenvectors.GetLength(0) != size || 
                eigenvectors.GetLength(1) != size)
            {
                return false;
            }

            // Verify A*v = λ*v for each eigenpair
            for (int i = 0; i < size; i++)
            {
                var eigenvector = Matrix.ExtractColumn(eigenvectors, i);
                var Av = new double[size];

                // Compute A * v
                for (int j = 0; j < size; j++)
                {
                    Av[j] = 0.0;
                    for (int k = 0; k < size; k++)
                    {
                        Av[j] += originalMatrix[j, k] * eigenvector[k];
                    }
                }

                // Check if A*v ≈ λ*v
                for (int j = 0; j < size; j++)
                {
                    double expected = eigenvalues[i] * eigenvector[j];
                    if (Math.Abs(Av[j] - expected) > tolerance)
                    {
                        return false;
                    }
                }

                // Verify eigenvector is normalized
                double norm = Matrix.VectorNorm(eigenvector);
                if (Math.Abs(norm - 1.0) > tolerance)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Applies the QR algorithm with shifts to compute eigenvalues.
        /// </summary>
        /// <param name="matrix">The working matrix (modified in-place).</param>
        /// <param name="eigenvectors">The eigenvector matrix (modified in-place).</param>
        /// <param name="tolerance">Convergence tolerance.</param>
        /// <param name="maxIterations">Maximum iterations allowed.</param>
        /// <returns>True if converged, false otherwise.</returns>
        private static bool ApplyQRAlgorithm(double[,] matrix, double[,] eigenvectors, 
            double tolerance, int maxIterations)
        {
            int size = matrix.GetLength(0);
            int iteration = 0;

            while (iteration < maxIterations)
            {
                // Check for convergence (off-diagonal elements should be small)
                if (IsConverged(matrix, tolerance))
                {
                    return true;
                }

                // Compute shift (use the bottom-right element as a simple shift strategy)
                double shift = matrix[size - 1, size - 1];

                // Apply shift: A := A - shift * I
                for (int i = 0; i < size; i++)
                {
                    matrix[i, i] -= shift;
                }

                // Perform QR decomposition
                var (Q, R) = QRDecomposition.Decompose(matrix);

                // Update matrix: A := R * Q + shift * I
                matrix = Matrix.Multiply(R, Q);
                for (int i = 0; i < size; i++)
                {
                    matrix[i, i] += shift;
                }

                // Update eigenvectors: V := V * Q
                eigenvectors = Matrix.Multiply(eigenvectors, Q);

                iteration++;
            }

            return false; // Failed to converge
        }

        /// <summary>
        /// Checks if the QR algorithm has converged by examining off-diagonal elements.
        /// </summary>
        /// <param name="matrix">The matrix to check for convergence.</param>
        /// <param name="tolerance">The convergence tolerance.</param>
        /// <returns>True if converged, false otherwise.</returns>
        private static bool IsConverged(double[,] matrix, double tolerance)
        {
            int size = matrix.GetLength(0);
            double maxOffDiagonal = 0.0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i != j)
                    {
                        maxOffDiagonal = Math.Max(maxOffDiagonal, Math.Abs(matrix[i, j]));
                    }
                }
            }

            return maxOffDiagonal < tolerance;
        }

        /// <summary>
        /// Normalizes all eigenvectors to unit length.
        /// </summary>
        /// <param name="eigenvectors">The eigenvector matrix to normalize.</param>
        private static void NormalizeEigenvectors(double[,] eigenvectors)
        {
            int size = eigenvectors.GetLength(1);

            for (int j = 0; j < size; j++)
            {
                var column = Matrix.ExtractColumn(eigenvectors, j);
                double norm = Matrix.VectorNorm(column);

                if (norm > Matrix.MachineEpsilon)
                {
                    for (int i = 0; i < column.Length; i++)
                    {
                        column[i] /= norm;
                    }
                    Matrix.SetColumn(eigenvectors, j, column);
                }
            }
        }

        /// <summary>
        /// Sorts eigenvalues and corresponding eigenvectors in descending order.
        /// </summary>
        /// <param name="eigenvalues">The eigenvalue array to sort.</param>
        /// <param name="eigenvectors">The eigenvector matrix to rearrange.</param>
        private static void SortEigenDecomposition(double[] eigenvalues, double[,] eigenvectors)
        {
            int size = eigenvalues.Length;
            var indices = new int[size];
            for (int i = 0; i < size; i++)
            {
                indices[i] = i;
            }

            // Sort indices by eigenvalue magnitude in descending order
            Array.Sort(indices, (i, j) => Math.Abs(eigenvalues[j]).CompareTo(Math.Abs(eigenvalues[i])));

            // Create sorted eigenvalues
            var sortedEigenvalues = new double[size];
            for (int i = 0; i < size; i++)
            {
                sortedEigenvalues[i] = eigenvalues[indices[i]];
            }

            // Rearrange eigenvectors
            var sortedEigenvectors = new double[size, size];
            for (int j = 0; j < size; j++)
            {
                var originalColumn = Matrix.ExtractColumn(eigenvectors, indices[j]);
                Matrix.SetColumn(sortedEigenvectors, j, originalColumn);
            }

            // Copy back sorted results
            Array.Copy(sortedEigenvalues, eigenvalues, size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    eigenvectors[i, j] = sortedEigenvectors[i, j];
                }
            }
        }

        #endregion
    }
}