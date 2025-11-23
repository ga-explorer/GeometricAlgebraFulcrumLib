using System.Collections.Immutable;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Reflection;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Rotation;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Scaling;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Reflection;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Rotation;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Scaling;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.SubSpaces.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.SubSpaces.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.SubSpaces.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.MathNetNumerics;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical
{
    using RealVector = MathNet.Numerics.LinearAlgebra.Vector<double>;
    using RealMatrix = MathNet.Numerics.LinearAlgebra.Matrix<double>;
    using RealDenseVector = MathNet.Numerics.LinearAlgebra.Double.DenseVector;
    using RealDenseMatrix = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix;
    using RealSparseVector = MathNet.Numerics.LinearAlgebra.Double.SparseVector;
    using RealSparseMatrix = MathNet.Numerics.LinearAlgebra.Double.SparseMatrix;

    using ComplexVector = MathNet.Numerics.LinearAlgebra.Vector<Complex>;
    using ComplexMatrix = MathNet.Numerics.LinearAlgebra.Matrix<Complex>;


    public static class NumericalLinearAlgebraUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double L2Norm(this IEnumerable<double> vector)
        {
            return vector.Sum(s => s * s).Sqrt();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double L2Norm(this IReadOnlyDictionary<int, double> vector)
        {
            return vector.Values.Sum(s => s * s).Sqrt();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double L2NormSquared(this IEnumerable<double> vector)
        {
            return vector.Sum(s => s * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double L2NormSquared(this IReadOnlyDictionary<int, double> vector)
        {
            return vector.Values.Sum(s => s * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double L2NormSquared(this RealVector u)
        {
            return u.Storage.EnumerateNonZero().Sum(s => s * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double L2NormSquared(this RealMatrix matrix)
        {
            return matrix.L2Norm().Square();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double FrobeniusNormSquared(this RealMatrix matrix)
        {
            return matrix.Storage.EnumerateNonZero().Sum(s => s * s);
        }


        public static Pair<int> GetMaxMagnitudeItemPosition(this RealMatrix matrix)
        {
            var maxItemMag = double.NegativeInfinity;
            var maxItemRowIndex = -1;
            var maxItemColIndex = -1;
            
            foreach (var (i, j, item) in matrix.Storage.EnumerateNonZeroIndexed())
            {
                if (i >= j) continue;

                var itemMag = Math.Abs(item);
                if (maxItemMag >= itemMag) continue;

                maxItemMag = itemMag;
                maxItemRowIndex = i;
                maxItemColIndex = j;
            }

            return new Pair<int>(maxItemRowIndex, maxItemColIndex);
        }

        
        /// <summary>
        /// Computes the condition number of the matrix
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ComputeConditionNumber(this RealMatrix matrix)
        {
            try
            {
                var svd = matrix.Svd(false);
                var maxSingular = svd.S.Maximum();
                var minSingular = svd.S.Where(s => Math.Abs(s) > 1e-15).DefaultIfEmpty(1e-15).Min();
                return maxSingular / minSingular;
            }
            catch
            {
                return 1.0; // Return reasonable default if SVD fails
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOrthogonalTo(this RealVector u, RealVector v, double tolerance = Float64Utils.DefaultTolerance)
        {
            return u.VectorDot(v).IsNearZero(tolerance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOrthonormalWith(this RealVector u, RealVector v, double tolerance = Float64Utils.DefaultTolerance)
        {
            return u.DotProduct(v).IsNearZero(tolerance) &&
                   u.L2Norm().IsNearOne(tolerance) &&
                   v.L2Norm().IsNearOne(tolerance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsParallelTo(this RealVector u, RealVector v, double tolerance = Float64Utils.DefaultTolerance)
        {
            var cosAngle =
                u.DotProduct(v) / (u.L2NormSquared() * v.L2NormSquared()).Sqrt();

            return cosAngle.Abs().IsNearOne(tolerance);
        }


        /// <summary>
        /// Compute the L2-norm of a matrix row
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double RowL2Norm(this RealMatrix matrix, int rowIndex)
        {
            var norm2 = 0d;

            for (var j = 0; j < matrix.ColumnCount; j++)
                norm2 += matrix[rowIndex, j].Square();

            return Math.Sqrt(norm2);
        }

        /// <summary>
        /// Compute the L2-norm of a matrix column
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ColumnL2Norm(this RealMatrix matrix, int colIndex)
        {
            var norm2 = 0d;

            for (var i = 0; i < matrix.RowCount; i++)
                norm2 += matrix[i, colIndex].Square();

            return Math.Sqrt(norm2);
        }


        /// <summary>
        /// Compute the dot-product of two matrix rows
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="rowIndex1"></param>
        /// <param name="rowIndex2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double RowDotProduct(this RealMatrix matrix, int rowIndex1, int rowIndex2)
        {
            var dot = 0d;

            for (var j = 0; j < matrix.ColumnCount; j++)
                dot += matrix[rowIndex1, j] * matrix[rowIndex2, j];

            return dot;
        }

        /// <summary>
        /// Compute the dot-product of two matrix columns
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="colIndex1"></param>
        /// <param name="colIndex2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ColumnDotProduct(this RealMatrix matrix, int colIndex1, int colIndex2)
        {
            var dot = 0d;

            for (var i = 0; i < matrix.RowCount; i++)
                dot += matrix[i, colIndex1] * matrix[i, colIndex2];

            return dot;
        }


        /// <summary>
        /// Return the indices of items on the diagonal that are negative
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GetNegativeDiagonalItemIndices(this RealMatrix matrix)
        {
            var n = Math.Min(matrix.RowCount, matrix.ColumnCount);

            for (var i = 0; i < n; i++)
                if (matrix[i, i] < 0) yield return i;
        }

        /// <summary>
        /// Return the number of items on the diagonal that are negative
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetNegativeDiagonalItemsCount(this RealMatrix matrix)
        {
            var n = Math.Min(matrix.RowCount, matrix.ColumnCount);

            var c = 0;
            for (var i = 0; i < n; i++)
                if (matrix[i, i] < 0) c++;
            return c;
        }


        /// <summary>
        /// Verify orthogonality of a square matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="tolerance">Numerical tolerance</param>
        /// <returns>True if matrix is orthogonal</returns>
        public static bool IsOrthogonal(this RealMatrix matrix, double tolerance = Float64Utils.DefaultTolerance)
        {
            var n = matrix.Size();

            // Check if product is identity matrix
            for (var i = 0; i < n; i++)
            {
                if (!matrix.ColumnL2Norm(i).IsNearOne(tolerance))
                    return false;

                for (var j = i + 1; j < n; j++)
                {
                    if (!matrix.ColumnDotProduct(i, j).IsNearZero(tolerance))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Verify orthonormality of the columns of a matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="tolerance">Numerical tolerance</param>
        /// <returns>True if matrix is orthogonal</returns>
        public static bool IsRowOrthogonal(this RealMatrix matrix, double tolerance = Float64Utils.DefaultTolerance)
        {
            // Check if product is identity matrix
            for (var i = 0; i < matrix.RowCount; i++)
            {
                if (!matrix.RowL2Norm(i).IsNearOne(tolerance))
                    return false;

                for (var j = i + 1; j < matrix.RowCount; j++)
                {
                    if (!matrix.RowDotProduct(i, j).IsNearZero(tolerance))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Verify orthonormality of the columns of a matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="tolerance">Numerical tolerance</param>
        /// <returns>True if matrix is orthogonal</returns>
        public static bool IsColumnOrthogonal(this RealMatrix matrix, double tolerance = Float64Utils.DefaultTolerance)
        {
            // Check if product is identity matrix
            for (var i = 0; i < matrix.ColumnCount; i++)
            {
                if (!matrix.ColumnL2Norm(i).IsNearOne(tolerance))
                    return false;

                for (var j = i + 1; j < matrix.ColumnCount; j++)
                {
                    if (!matrix.ColumnDotProduct(i, j).IsNearZero(tolerance))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Verifies that a matrix is orthogonal (matrixQ^T * matrixQ = I).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool isOrthogonal, double maxError) VerifyOrthogonality(this RealMatrix matrix, double tolerance)
        {
            var n = matrix.Size();
            var maxError = 0.0;
            var isOrthogonal = true;

            // Compute matrixQ^T * matrixQ and check if it equals I
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    double sum = 0;
                    for (var k = 0; k < n; k++)
                    {
                        sum += matrix[k, i] * matrix[k, j];
                    }

                    var expected = i == j ? 1.0 : 0.0;
                    var error = Math.Abs(sum - expected);
                    maxError = Math.Max(maxError, error);

                    if (error > tolerance)
                        isOrthogonal = false;
                }
            }

            return (isOrthogonal, maxError);
        }
        
        /// <summary>
        /// Computes the residual norm ||A - Q * T * Q^T||_F
        /// </summary>
        /// <param name="originalMatrix"></param>
        /// <param name="matrixQ"></param>
        /// <param name="matrixT"></param>
        /// <returns></returns>
        public static double ComputeResidualNorm(this RealMatrix originalMatrix, RealMatrix matrixQ, RealMatrix matrixT)
        {
            var residual = originalMatrix - matrixQ * matrixT * matrixQ.Transpose();
        
            return residual.FrobeniusNorm();
        }

        /// <summary>
        /// Computes the orthogonality error ||Q^T Q - I||_F
        /// </summary>
        public static double ComputeOrthogonalityError(this RealMatrix matrixQ)
        {
            var identity = RealMatrix.Build.DenseIdentity(matrixQ.RowCount);
            var error = matrixQ.Transpose() * matrixQ - identity;
            return error.FrobeniusNorm();
        }

        
        /// <summary>
        /// Estimates the numerical condition number of the matrix exponential problem.
        /// This is an approximation based on matrix properties and norms.
        /// </summary>
        /// <param name="matrix">The matrix to analyze</param>
        /// <param name="properties">Pre-computed matrix properties</param>
        /// <returns>Estimated condition number</returns>
        public static double EstimateConditionNumber(this RealMatrix matrix, MatrixProperties properties)
        {
            //matrix.ConditionNumber()
            // For normal matrices, condition number = ||matrixA||_2
            if (properties.HasFlag(MatrixProperties.Symmetric) || properties.HasFlag(MatrixProperties.Orthogonal))
            {
                return matrix.SpectralNorm();
            }

            // For stochastic matrices, condition number = ||matrixA||_∞  
            if (properties.HasFlag(MatrixProperties.Stochastic))
            {
                return matrix.InfinityNorm();
            }

            // General case: upper bound using Frobenius norm
            var frobNorm = matrix.FrobeniusNorm();
            return Math.Exp(frobNorm);
        }

        /// <summary>
        /// Estimates the spectral norm (largest singular value) of a matrix.
        /// This is a rough approximation for performance reasons.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SpectralNorm(this RealMatrix matrix)
        {
            // For symmetric matrices, spectral norm equals largest eigenvalue magnitude
            // For general matrices, this is an approximation
            return Math.Sqrt(matrix.InfinityNorm() * matrix.OneNorm());
        }

        /// <summary>
        /// Computes the 1-norm (maximum column sum) of a matrix.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double OneNorm(this RealMatrix matrix)
        {
            double maxColSum = 0;
            for (var j = 0; j < matrix.ColumnCount; j++)
            {
                double colSum = 0;
                for (var i = 0; i < matrix.RowCount; i++)
                {
                    colSum += Math.Abs(matrix[i, j]);
                }
                maxColSum = Math.Max(maxColSum, colSum);
            }
            return maxColSum;
        }


        public static LinFloat64Vector2D Multiply(this RealMatrix matrix, LinFloat64Vector2D vector)
        {
            var composer = LinFloat64Vector2DComposer.Create();

            var rowCount = matrix.RowCount;
            var colCount = matrix.ColumnCount;

            for (var colIndex = 0; colIndex < 2; colIndex++)
            {
                var scalar = vector[1 << colIndex];

                if (colIndex >= colCount)
                    continue;

                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    composer.AddTerm(
                        rowIndex,
                        scalar * matrix[rowIndex, colIndex]
                    );
            }

            return composer.GetVector();
        }

        public static LinFloat64Vector3D Multiply(this RealMatrix matrix, LinFloat64Vector3D vector)
        {
            var composer = LinFloat64Vector3DComposer.Create();

            var rowCount = matrix.RowCount;
            var colCount = matrix.ColumnCount;

            for (var colIndex = 0; colIndex < 3; colIndex++)
            {
                var scalar = vector[1 << colIndex];

                if (colIndex >= colCount)
                    continue;

                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    composer.AddTerm(
                        rowIndex,
                        scalar * matrix[rowIndex, colIndex]
                    );
            }

            return composer.GetVector();
        }

        public static LinFloat64Vector4D Multiply(this RealMatrix matrix, LinFloat64Vector4D vector)
        {
            var composer = new LinFloat64Vector4DComposer();

            var rowCount = matrix.RowCount;
            var colCount = matrix.ColumnCount;

            for (var colIndex = 0; colIndex < 4; colIndex++)
            {
                var scalar = vector[colIndex];

                if (colIndex >= colCount)
                    continue;

                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    composer.AddTerm(
                        rowIndex,
                        scalar * matrix[rowIndex, colIndex]
                    );
            }

            return composer.GetVector();
        }

        public static LinFloat64Vector Multiply(this RealMatrix matrix, LinFloat64Vector vector)
        {
            var composer = LinFloat64VectorComposer.Create();

            var rowCount = matrix.RowCount;
            var colCount = matrix.ColumnCount;

            foreach (var (colIndex, scalar) in vector)
            {
                if (colIndex >= colCount)
                    continue;

                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    composer.AddTerm(
                        rowIndex,
                        scalar * matrix[rowIndex, colIndex]
                    );
            }

            return composer.GetVector();
        }

        public static LinFloat64Vector Multiply(this RealMatrix matrix, IEnumerable<double> vector)
        {
            var composer = LinFloat64VectorComposer.Create();

            var rowCount = matrix.RowCount;
            var colCount = matrix.ColumnCount;

            var colIndex = 0;
            foreach (var scalar in vector)
            {
                if (colIndex >= colCount)
                    return composer.GetVector();

                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    composer.AddTerm(
                        rowIndex,
                        scalar * matrix[rowIndex, colIndex]
                    );

                colIndex++;
            }

            return composer.GetVector();
        }

        public static LinFloat64Vector Multiply(this RealMatrix matrix, IReadOnlyDictionary<int, double> vector)
        {
            var composer = LinFloat64VectorComposer.Create();

            var rowCount = matrix.RowCount;
            var colCount = matrix.ColumnCount;

            foreach (var (colIndex, scalar) in vector)
            {
                if (colIndex >= colCount)
                    continue;

                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    composer.AddTerm(
                        rowIndex,
                        scalar * matrix[rowIndex, colIndex]
                    );
            }

            return composer.GetVector();
        }

        public static XGaFloat64Vector Multiply(this RealMatrix matrix, XGaFloat64Vector vector)
        {
            var composer = vector.Processor.CreateVectorComposer();

            foreach (var (j, sc) in vector)
            {
                var v = matrix.Column(j.FirstIndex).GetIndexScalarTuples();

                foreach (var (i, sr) in v)
                    composer.AddTerm(i.ToUnitIndexSet(), sc * sr);
            }

            return composer.GetVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RealMatrix MatrixSquare(this RealMatrix matrix)
        {
            return matrix.Multiply(matrix);
        }

        /// <summary>
        /// Computes B^p for integer p using repeated squaring.
        /// Efficient and stable for moderate p.
        /// </summary>
        public static RealMatrix MatrixPower(this RealMatrix matrix, int power)
        {
            if (power < 0)
                throw new ArgumentException("Negative powers not supported");

            if (power == 1) 
                return matrix.Clone();

            var n = matrix.Size();

            RealMatrix result = 
                matrix is RealDenseMatrix 
                    ? RealDenseMatrix.CreateIdentity(n)
                    : RealSparseMatrix.CreateIdentity(n);

            if (power == 0) 
                return result;

            var baseMat = matrix;
            
            while (power > 0)
            {
                if ((power & 1) == 1)
                    result *= baseMat;

                baseMat *= baseMat;
                power >>= 1;
            }

            return result;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[] GetFloat64Array1D(this Random random, int size, double minValue = -1, double maxValue = 1)
        {
            var array = new double[size];

            for (var i = 0; i < size; i++)
                array[i] = random.GetFloat64(minValue, maxValue);

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[,] GetFloat64Array2D(this Random random, int rowCount, int colCount, double minValue = -1, double maxValue = 1)
        {
            var array = new double[rowCount, colCount];

            for (var i = 0; i < rowCount; i++)
                for (var j = 0; j < rowCount; j++)
                    array[i, j] = random.GetFloat64(minValue, maxValue);

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Permutation GetPermutation(this Random random, int size)
        {
            return new Permutation(
                Enumerable.Range(0, size).Shuffled(random).ToArray()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[,] GetPermutationArray2D(this Random random, int size)
        {
            return random.GetPermutation(size).ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RealSparseMatrix GetPermutationMatrix(this Random random, int size)
        {
            return random.GetPermutation(size).ToMatrix();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RealDenseVector GetRealDenseVector(this Random random, int size, double minValue = -1, double maxValue = 1)
        {
            return RealDenseVector.Create(
                size,
                _ => random.GetFloat64(minValue, maxValue)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RealSparseVector GetRealSparseVector(this Random random, int size, double minValue = -1, double maxValue = 1)
        {
            var scalarCount = random.GetInt32(1, size);

            return random.GetRealSparseVector(
                size, scalarCount, minValue, maxValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RealSparseVector GetRealSparseVector(this Random random, int size, int scalarCount, double minValue = -1, double maxValue = 1)
        {
            return RealSparseVector.OfIndexedEnumerable(
                size,
                random
                    .GetDistinctIndices(scalarCount, size)
                    .Select(i => (i, random.GetFloat64(minValue, maxValue)))
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RealDenseMatrix GetRealDenseMatrix(this Random random, int size, double minValue = -1, double maxValue = 1)
        {
            return RealDenseMatrix.Create(
                size,
                size,
                (_, _) => random.GetFloat64(minValue, maxValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RealDenseMatrix GetRealDenseMatrix(this Random random, int rowCount, int colCount, double minValue = -1, double maxValue = 1)
        {
            return RealDenseMatrix.Create(
                rowCount,
                colCount,
                (_, _) => random.GetFloat64(minValue, maxValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RealDenseMatrix GetRealDiagonalDenseMatrix(this Random random, int size, double minValue = -1, double maxValue = 1)
        {
            return RealDenseMatrix.Create(
                size,
                size,
                (i, j) => i == j ? random.GetFloat64(minValue, maxValue) : 0
            );
        }

        public static RealDenseMatrix GetRealSymmetricDenseMatrix(this Random random, int size, double minValue = -1, double maxValue = 1)
        {
            var matrix = RealDenseMatrix.Create(size, size, 0);

            for (var i = 0; i < size; i++)
            {
                matrix[i, i] = random.GetFloat64(minValue, maxValue);

                for (var j = i + 1; j < size; j++)
                {
                    var scalar = random.GetFloat64(minValue, maxValue);

                    matrix[i, j] = scalar;
                    matrix[j, i] = scalar;
                }
            }

            return matrix;
        }

        public static RealDenseMatrix GetRealSkewSymmetricDenseMatrix(this Random random, int size, double minValue = -1, double maxValue = 1)
        {
            var matrix = RealDenseMatrix.Create(size, size, 0);

            for (var i = 0; i < size; i++)
            {
                for (var j = i + 1; j < size; j++)
                {
                    var scalar = random.GetFloat64(minValue, maxValue);

                    matrix[i, j] = scalar;
                    matrix[j, i] = -scalar;
                }
            }

            return matrix;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RealDenseMatrix GetCirculantDenseColumnMatrix(this Random random, int size, double minValue = -1d, double maxValue = 1d)
        {
            return random
                .GetFloat64Array1D(size, minValue, maxValue)
                .ToCirculantDenseColumnMatrix();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<RealMatrix> GetInvertibleMatrixPair(this Random randomGenerator, int n, int maxOpCount = 10)
        {
            var arrayComposer = InvertibleRealMatrixComposer.Create(n);

            arrayComposer.ApplyRandomOperations(randomGenerator, maxOpCount);

            var matrix = arrayComposer.GetMatrix();
            var matrixInverse = arrayComposer.GetMatrixInverse();

            return new Pair<RealMatrix>(matrix, matrixInverse);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RealMatrix GetOrthogonalMatrix(this Random random, int size)
        {
            return random
                .GetFloat64Array2D(size, size)
                .ToMatrix()
                .QR()
                .Q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RealMatrix GetOrthogonalMatrix(this Random random, int rowCount, int colCount)
        {
            Debug.Assert(rowCount >= colCount);

            return random
                .GetFloat64Array2D(rowCount, colCount)
                .ToMatrix()
                .QR()
                .Q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<RealVector> GetOrthonormalVectors(this Random random, int size, int count)
        {
            var matrix = random.GetOrthogonalMatrix(size, count);

            return count.GetRange(
                i => matrix.Column(i)
            ).ToArray();
        }


        /// <summary>
        /// Computes the first column of the inverse of a square matrix
        /// </summary>
        /// <param name="matrix">The input square matrix</param>
        /// <returns>The first column of the inverse matrix</returns>
        public static RealVector FirstColumnOfInverse(this RealMatrix matrix)
        {
            var n = matrix.RowCount;

            if (n != matrix.ColumnCount)
                throw new ArgumentException("Matrix must be square");

            // Create augmented matrix [A|e1] where e1 is the first standard basis vector
            var augmented = new double[n, n + 1];

            // Copy original matrix to the left part of augmented matrix
            for (var i = 0; i < n; i++)
                for (var j = 0; j < n; j++)
                    augmented[i, j] = matrix[i, j];

            // Set up the right part: first column is [1, 0, 0, ..., 0]^T
            for (var i = 0; i < n; i++)
                augmented[i, n] = (i == 0) ? 1.0 : 0.0;

            // Perform Gaussian elimination with partial pivoting
            for (var i = 0; i < n; i++)
            {
                // Find pivot
                var maxRow = i;
                var maxVal = Math.Abs(augmented[i, i]);

                for (var k = i + 1; k < n; k++)
                {
                    var val = Math.Abs(augmented[k, i]);
                    if (val > maxVal)
                    {
                        maxVal = val;
                        maxRow = k;
                    }
                }

                // Check for singular matrix
                if (Math.Abs(augmented[maxRow, i]) < 1e-12)
                    throw new InvalidOperationException("Matrix is singular");

                // Swap rows if needed
                if (maxRow != i)
                {
                    for (var j = i; j <= n; j++)
                    {
                        (augmented[i, j], augmented[maxRow, j]) =
                            (augmented[maxRow, j], augmented[i, j]);
                    }
                }

                // Scale the pivot row
                var pivot = augmented[i, i];
                for (var j = i; j <= n; j++)
                    augmented[i, j] /= pivot;

                // Eliminate column entries below and above the pivot
                for (var k = 0; k < n; k++)
                {
                    if (k == i) continue;

                    var factor = augmented[k, i];
                    for (var j = i; j <= n; j++)
                        augmented[k, j] -= factor * augmented[i, j];
                }
            }

            // Extract the first column of the inverse (last column of augmented matrix)
            var result = new double[n];
            for (var i = 0; i < n; i++)
                result[i] = augmented[i, n];

            return result.ToMathNetDenseVector();
        }

        /// <summary>
        /// Creates an n x n sparse permutation matrix from a permutation array.
        /// The input p[i] specifies that row i has a 1 in column p[i].
        /// </summary>
        /// <param name="permutationArray">Array of unique integers from 0 to n-1 representing the permutation</param>
        /// <param name="assumeValidPermutationArray"></param>
        /// <returns>Sparse permutation matrix P where P[i, p[i]] = 1</returns>
        public static RealSparseMatrix ToSparsePermutationMatrix(this int[] permutationArray, bool assumeValidPermutationArray = false)
        {
            if (permutationArray == null)
                throw new ArgumentNullException(nameof(permutationArray));

            var n = permutationArray.Length;

            if (n == 0)
                return RealSparseMatrix.Create(0, 0, 0);

            if (!assumeValidPermutationArray)
            {
                // Validate input: all values in [0, n-1] and unique
                var seen = new bool[n];
                foreach (var value in permutationArray)
                {
                    if (value < 0 || value >= n)
                        throw new ArgumentException($"Value {value} is out of range [0, {n - 1}].", nameof(permutationArray));

                    if (seen[value])
                        throw new ArgumentException($"Duplicate value {value} detected. Permutation must be unique.", nameof(permutationArray));

                    seen[value] = true;
                }
            }

            // We know there are exactly n non-zero entries (one per row)
            var matrix = RealSparseMatrix.Create(n, n, 0); // pre-allocate n non-zeros

            for (var rowIdx = 0; rowIdx < n; rowIdx++)
            {
                // Set P[i, p[i]] = 1
                var colIdx = permutationArray[rowIdx];
                matrix.At(rowIdx, colIdx, 1.0);
            }

            return matrix;
        }

        /// <summary>
        /// Creates an n x n permutation matrix from a permutation.
        /// The input p[i] specifies that row i has a 1 in column p[i].
        /// </summary>
        /// <param name="permutation">Array of unique integers from 0 to n-1 representing the permutation</param>
        /// <returns>Sparse permutation matrix P where P[i, p[i]] = 1</returns>
        public static double[,] ToArray(this Permutation permutation)
        {
            if (permutation == null)
                throw new ArgumentNullException(nameof(permutation));

            var n = permutation.Dimension;

            if (n == 0)
                return new double[0, 0];

            // We know there are exactly n non-zero entries (one per row)
            var matrix = new double[n, n];

            for (var rowIdx = 0; rowIdx < n; rowIdx++)
            {
                // Set P[i, p[i]] = 1
                var colIdx = permutation[rowIdx];
                matrix[rowIdx, colIdx] = 1.0;
            }

            return matrix;
        }

        /// <summary>
        /// Creates an n x n sparse permutation matrix from a permutation.
        /// The input p[i] specifies that row i has a 1 in column p[i].
        /// </summary>
        /// <param name="permutation">Array of unique integers from 0 to n-1 representing the permutation</param>
        /// <returns>Sparse permutation matrix P where P[i, p[i]] = 1</returns>
        public static RealSparseMatrix ToMatrix(this Permutation permutation)
        {
            if (permutation == null)
                throw new ArgumentNullException(nameof(permutation));

            var n = permutation.Dimension;

            if (n == 0)
                return RealSparseMatrix.Create(0, 0, 0);

            // We know there are exactly n non-zero entries (one per row)
            var matrix = RealSparseMatrix.Create(n, n, 0); // pre-allocate n non-zeros

            for (var rowIdx = 0; rowIdx < n; rowIdx++)
            {
                // Set P[i, p[i]] = 1
                var colIdx = permutation[rowIdx];
                matrix.At(rowIdx, colIdx, 1.0);
            }

            return matrix;
        }


        /// <summary>
        /// See the paper "Generalized Clarke Components for Poly-phase Networks", 1969
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private static double[,] ClarkeRotationArrayOdd(int size)
        {
            var clarkeArray = new double[size, size];

            var s = (2d / size).Sqrt();
            //$"Sqrt[2 / {m}]";

            // m is odd, fill all rows except the last
            var n = (size - 1) / 2;
            for (var k = 0; k < n; k++)
            {
                var rowIndex1 = 2 * k;
                var rowIndex2 = 2 * k + 1;

                clarkeArray[rowIndex1, 0] = s;
                clarkeArray[rowIndex2, 0] = 0d;

                for (var colIndex = 1; colIndex < size; colIndex++)
                {
                    var angle = Math.Tau * (k + 1) * colIndex / size;
                    // $"2 * Pi * {k + 1} * {i} / {m}";

                    var cosAngle = s * Math.Cos(angle); // $"{s} * Cos[{angle}]";
                    var sinAngle = s * Math.Sin(angle); // $"{s} * Sin[{angle}]";

                    clarkeArray[rowIndex1, colIndex] = cosAngle;
                    clarkeArray[rowIndex2, colIndex] = sinAngle;
                }
            }

            //Fill the last column
            var v = 1d / Math.Sqrt(size);
            // $"1 / Sqrt[{m}]";

            for (var colIndex = 0; colIndex < size; colIndex++)
                clarkeArray[size - 1, colIndex] = v;

            return clarkeArray;
        }

        /// <summary>
        /// See the paper "Generalized Clarke Components for Poly-phase Networks", 1969
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private static double[,] ClarkeRotationArrayEven(int size)
        {
            var clarkeArray = new double[size, size];

            var s = Math.Sqrt(2d / size);
            //$"Sqrt[2 / {m}]";

            //m is even, fill all rows except the last two
            var n = (size - 1) / 2;
            for (var k = 0; k < n; k++)
            {
                var rowIndex1 = 2 * k;
                var rowIndex2 = 2 * k + 1;

                clarkeArray[rowIndex1, 0] = s;
                clarkeArray[rowIndex2, 0] = 0d;

                for (var colIndex = 1; colIndex < size; colIndex++)
                {
                    var angle = Math.Tau * (k + 1) * colIndex / size;
                    // $"2 * Pi * {k + 1} * {i} / {m}";

                    var cosAngle = s * Math.Cos(angle); // $"{s} * Cos[{angle}]";
                    var sinAngle = s * Math.Sin(angle); // $"{s} * Sin[{angle}]";

                    clarkeArray[rowIndex1, colIndex] = cosAngle;
                    clarkeArray[rowIndex2, colIndex] = sinAngle;
                }
            }

            //Fill the last two rows
            var v0 = 1d / Math.Sqrt(size);
            // $"1 / Sqrt[{m}]";

            var v1 = -v0;
            // $"-1 / Sqrt[{m}]";

            for (var colIndex = 0; colIndex < size; colIndex++)
            {
                clarkeArray[size - 2, colIndex] = colIndex % 2 == 0 ? v0 : v1;
                clarkeArray[size - 1, colIndex] = v0;
            }

            return clarkeArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RealMatrix ClarkeRotationMatrix(int size)
        {
            var clarkeArray = size % 2 == 0
                ? ClarkeRotationArrayEven(size)
                : ClarkeRotationArrayOdd(size);

            return clarkeArray.ToMathNetDenseMatrix();
        }


        public static RealDenseMatrix ToCirculantDenseColumnMatrix(this double[] column)
        {
            var n = column.Length;
            var array = new double[n, n];

            for (var j = 0; j < n; j++)
                for (var i = 0; i < n; i++)
                    array[i, j] = column[(i - j).Mod(n)];

            return array.ToMathNetDenseMatrix();
        }

        public static RealDenseMatrix ToCirculantDenseColumnMatrix(this RealVector column)
        {
            var n = column.Count;
            var array = new double[n, n];

            for (var j = 0; j < n; j++)
                for (var i = 0; i < n; i++)
                    array[i, j] = column[(i - j).Mod(n)];

            return array.ToMathNetDenseMatrix();
        }

        public static RealSparseMatrix ToCirculantSparseColumnMatrix(this RealVector column)
        {
            var n = column.Count;
            var array = new double[n, n];

            for (var j = 0; j < n; j++)
                for (var i = 0; i < n; i++)
                    array[i, j] = column[(i - j).Mod(n)];

            return array.ToMathNetSparseMatrix();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RealMatrix CreateDenseIdentityMatrix(this int size)
        {
            return RealMatrix.Build.DenseIdentity(size);
        }

        public static ComplexMatrix UnitaryDftMatrix(this int size, bool inverse = false)
        {
            var nSqrtInv = 1d / Math.Sqrt(size);
            var array = new Complex[size, size];

            for (var i = 0; i < size; i++)
                for (var j = 0; j < size; j++)
                {
                    var c = size.NthRootOfOne(i * j);

                    array[i, j] = nSqrtInv * (inverse ? c.Conjugate() : c);
                }

            return array.ToMathNetDenseMatrix();
        }



        public static int MatrixEigenDecomposition(this RealMatrix matrix, out Tuple<double, double[]>[] realPairs, out Tuple<double, double[]>[] imagPairs)
        {
            var sysExpr = matrix.ToComplex().Evd();

            var count = sysExpr.EigenValues.Count;

            realPairs = new Tuple<double, double[]>[count];
            imagPairs = new Tuple<double, double[]>[count];

            //Console.WriteLine("Eigen Vectors Matrix");
            //Console.WriteLine(
            //    GeoTextComposerFloat64.DefaultComposer.GetArrayText(
            //        sysExpr.EigenVectors.Real().ToArray()
            //    )
            //);
            //Console.WriteLine();

            //Console.WriteLine(
            //    GeoTextComposerFloat64.DefaultComposer.GetArrayText(
            //        sysExpr.EigenVectors.Imaginary().ToArray()
            //    )
            //);
            //Console.WriteLine();

            for (var j = 0; j < count; j++)
            {
                var complexEigenValue = sysExpr.EigenValues[j];
                var complexEigenVector = sysExpr.EigenVectors.Column(j);

                realPairs[j] = new Tuple<double, double[]>(
                    complexEigenValue.Real,
                    complexEigenVector.Real().ToArray()
                );

                imagPairs[j] = new Tuple<double, double[]>(
                    complexEigenValue.Imaginary,
                    complexEigenVector.Imaginary().ToArray()
                );
            }

            return count;
        }


        public static IReadOnlyList<Tuple<Complex, ComplexVector>> GetComplexEigenPairs(this RealMatrix matrix)
        {
            var eigenPairList = new List<Tuple<Complex, ComplexVector>>(matrix.RowCount);

            var sysExpr = matrix.ToComplex().Evd();
            var count = sysExpr.EigenValues.Count;

            for (var j = 0; j < count; j++)
            {
                var eigenValue = sysExpr.EigenValues[j];
                var eigenVector = sysExpr.EigenVectors.Column(j);

                eigenPairList.Add(new Tuple<Complex, ComplexVector>(
                    eigenValue,
                    eigenVector
                ));
            }

            return eigenPairList;
        }

        public static IReadOnlyList<Tuple<Complex, ComplexVector>> GetComplexEigenPairs(this ComplexMatrix matrix)
        {
            var eigenPairList = new List<Tuple<Complex, ComplexVector>>(matrix.RowCount);

            var sysExpr = matrix.Evd();
            var count = sysExpr.EigenValues.Count;

            for (var j = 0; j < count; j++)
            {
                var eigenValue = sysExpr.EigenValues[j];
                var eigenVector = sysExpr.EigenVectors.Column(j);

                eigenPairList.Add(new Tuple<Complex, ComplexVector>(
                    eigenValue,
                    eigenVector
                ));
            }

            return eigenPairList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Rotation3D GetRotationPart(this SquareMatrix3 matrix)
        {
            var subspaceList =
                matrix
                    .GetSimpleEigenSubspaces()
                    .Where(s =>
                        s.SubspaceDimensions == 2
                    ).ToImmutableArray();

            if (subspaceList.Length == 0)
                return LinFloat64IdentityLinearMap3D.Instance;

            Debug.Assert(subspaceList.Length == 1);

            return subspaceList[0].GetPlanarRotation();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<LinFloat64PlaneSubspace3D> GetPlaneEigenSubspaces(this SquareMatrix3 matrix)
        {
            return matrix
                .GetSimpleEigenSubspaces()
                .Where(s => s.Subspace is LinFloat64PlaneSubspace3D)
                .Select(s => (LinFloat64PlaneSubspace3D)s.Subspace)
                .ToImmutableArray();
        }

        public static IReadOnlyList<LinFloat64SimpleEigenSubspace3D> GetSimpleEigenSubspaces(this SquareMatrix3 matrix)
        {
            var list1 = new List<LinFloat64SimpleEigenSubspace3D>();

            var sysExpr = matrix.GetComplexMatrix().Evd();
            var count = sysExpr.EigenValues.Count;

            for (var j = 0; j < count; j++)
            {
                var eigenValue = sysExpr.EigenValues[j];

                if (eigenValue.IsNearOne())
                    continue;

                var eigenVector = sysExpr.EigenVectors.Column(j);

                var subspace = new LinFloat64SimpleEigenSubspace3D(
                    eigenValue,
                    eigenVector
                );

                if (subspace.VSpaceDimensions > 0)
                    list1.Add(subspace);
            }

            if (list1.Count == 0)
                return list1;

            list1 = list1
                .OrderByDescending(s => s.SubspaceDimensions)
                .ToList();

            var subspaceList = new List<LinFloat64SimpleEigenSubspace3D>
        {
            list1[0]
        };

            foreach (var subspace in list1.Skip(1))
            {
                if (subspaceList.Any(s => s.NearContains(subspace)))
                    continue;

                subspaceList.Add(subspace);
            }

            return subspaceList;
        }

        public static IReadOnlyList<LinFloat64SimpleEigenSubspace4D> GetSimpleEigenSubspaces(this SquareMatrix4 matrix)
        {
            var list1 = new List<LinFloat64SimpleEigenSubspace4D>();

            var sysExpr = matrix.GetComplexMatrix().Evd();
            var count = sysExpr.EigenValues.Count;

            for (var j = 0; j < count; j++)
            {
                var eigenValue = sysExpr.EigenValues[j];

                if (eigenValue.IsNearOne())
                    continue;

                var eigenVector = sysExpr.EigenVectors.Column(j);

                var subspace = new LinFloat64SimpleEigenSubspace4D(
                    eigenValue,
                    eigenVector
                );

                if (subspace.VSpaceDimensions > 0)
                    list1.Add(subspace);
            }

            list1 = list1
                .OrderByDescending(s => s.SubspaceDimensions)
                .ToList();

            var subspaceList = new List<LinFloat64SimpleEigenSubspace4D>
        {
            list1[0]
        };

            foreach (var subspace in list1.Skip(1))
            {
                if (subspaceList.Any(s => s.NearContains(subspace)))
                    continue;

                subspaceList.Add(subspace);
            }

            return subspaceList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<LinFloat64PlaneSubspace> GetPlaneEigenSubspaces(this RealMatrix matrix)
        {
            return matrix
                .GetSimpleEigenSubspaces()
                .Where(s => s.Subspace is LinFloat64PlaneSubspace)
                .Select(s => (LinFloat64PlaneSubspace)s.Subspace)
                .ToImmutableArray();
        }

        public static IReadOnlyList<LinFloat64SimpleEigenSubspace> GetSimpleEigenSubspaces(this RealMatrix matrix)
        {
            var list1 = new List<LinFloat64SimpleEigenSubspace>(matrix.RowCount);

            var sysExpr = matrix.ToComplex().Evd();
            var count = sysExpr.EigenValues.Count;

            for (var j = 0; j < count; j++)
            {
                var eigenValue = sysExpr.EigenValues[j];

                if (eigenValue.IsNearOne())
                    continue;

                var eigenVector = sysExpr.EigenVectors.Column(j);

                var subspace = new LinFloat64SimpleEigenSubspace(
                    eigenValue,
                    eigenVector
                );

                if (subspace.VSpaceDimensions > 0)
                    list1.Add(subspace);
            }

            list1 = list1
                .OrderByDescending(s => s.SubspaceDimensions)
                .ToList();

            var subspaceList = new List<LinFloat64SimpleEigenSubspace>
        {
            list1[0]
        };

            foreach (var subspace in list1.Skip(1))
            {
                if (subspaceList.Any(s => s.NearContains(subspace)))
                    continue;

                subspaceList.Add(subspace);
            }

            return subspaceList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorDirectionalScalingSequence3D GetVectorDirectionalScalingSequence3D(this SquareMatrix3 matrix)
        {
            var scaling = LinFloat64VectorDirectionalScalingSequence3D.Create();

            var mapList =
                matrix
                    .GetSimpleEigenSubspaces()
                    .SelectMany(s => s.GetVectorDirectionalScalingMaps());

            scaling.AppendMaps(mapList);

            return scaling;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorDirectionalScalingSequence4D GetVectorDirectionalScalingSequence4D(this SquareMatrix4 matrix)
        {
            var scaling = LinFloat64VectorDirectionalScalingSequence4D.Create();

            var mapList =
                matrix
                    .GetSimpleEigenSubspaces()
                    .SelectMany(s => s.GetVectorDirectionalScalingMaps());

            scaling.AppendMaps(mapList);

            return scaling;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorDirectionalScalingSequence GetVectorDirectionalScalingSequence(this RealMatrix matrix)
        {
            var scaling = LinFloat64VectorDirectionalScalingSequence.Create(matrix.RowCount);

            var mapList =
                matrix
                    .GetSimpleEigenSubspaces()
                    .SelectMany(s => s.GetVectorDirectionalScalingMaps());

            scaling.AppendMaps(mapList);

            return scaling;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64HyperPlaneNormalReflectionSequence3D GetHyperPlaneNormalReflectionSequence(this SquareMatrix3 matrix)
        {
            Debug.Assert(
                matrix.Determinant.Abs().IsNearOne()
            );

            var reflection = LinFloat64HyperPlaneNormalReflectionSequence3D.Create();

            var mapList =
                matrix
                    .GetSimpleEigenSubspaces()
                    .SelectMany(s => s.GetHyperPlaneNormalReflectionMaps());

            reflection.AppendMaps(mapList);

            return reflection;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64HyperPlaneNormalReflectionSequence4D GetHyperPlaneNormalReflectionSequence(this SquareMatrix4 matrix)
        {
            Debug.Assert(
                matrix.Determinant.Abs().IsNearOne()
            );

            var reflection = LinFloat64HyperPlaneNormalReflectionSequence4D.Create();

            var mapList =
                matrix
                    .GetSimpleEigenSubspaces()
                    .SelectMany(s => s.GetHyperPlaneNormalReflectionMaps());

            reflection.AppendMaps(mapList);

            return reflection;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64HyperPlaneNormalReflectionSequence GetHyperPlaneNormalReflectionSequence(this RealMatrix matrix)
        {
            Debug.Assert(
                matrix.Determinant().Abs().IsNearOne()
            );

            var reflection = LinFloat64HyperPlaneNormalReflectionSequence.Create(matrix.RowCount);

            var mapList =
                matrix
                    .GetSimpleEigenSubspaces()
                    .SelectMany(s => s.GetHyperPlaneNormalReflectionMaps());

            reflection.AppendMaps(mapList);

            return reflection;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64PlanarRotation3D GetPlanarRotation3D(this SquareMatrix3 matrix)
        {
            var planarSubspace =
                matrix
                    .GetSimpleEigenSubspaces()
                    .FirstOrDefault(s =>
                        s.Subspace is LinFloat64PlaneSubspace3D
                    );

            return planarSubspace is null
                ? LinFloat64PlanarRotation3D.Identity
                : planarSubspace.GetPlanarRotation();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotationSequence4D GetVectorToVectorRotationSequence4D(this SquareMatrix4 matrix)
        {
            Debug.Assert(
                matrix.Determinant.IsNearOne()
            );

            var rotation = LinFloat64VectorToVectorRotationSequence4D.Create();

            var mapList =
                matrix
                    .GetSimpleEigenSubspaces()
                    .SelectMany(s => s.GetVectorToVectorRotationMaps());

            rotation.AppendMaps(mapList);

            return rotation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64PlanarRotationSequence GetVectorToVectorRotationSequence(this RealMatrix matrix)
        {
            //Debug.Assert(
            //    matrix.Determinant().IsNearOne()
            //);

            var rotation = LinFloat64PlanarRotationSequence.Create(matrix.RowCount);

            var mapList =
                matrix
                    .GetSimpleEigenSubspaces()
                    .SelectMany(s => s.GetVectorToVectorRotationMaps());

            rotation.AppendMaps(mapList);

            return rotation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64OrthogonalLinearMapSequence GetBasicLinearMapSequence(this RealMatrix matrix)
        {
            return LinFloat64OrthogonalLinearMapSequence.CreateFromMatrix(matrix);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NegateRows(this RealMatrix matrix, IEnumerable<int> rowIndexList)
        {
            foreach (var rowIdx in rowIndexList)
                for (var colIdx = 0; colIdx < matrix.ColumnCount; colIdx++)
                    matrix[rowIdx, colIdx] = -matrix[rowIdx, colIdx];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NegateRows(this ComplexMatrix matrix, IEnumerable<int> rowIndexList)
        {
            foreach (var rowIdx in rowIndexList)
                for (var colIdx = 0; colIdx < matrix.ColumnCount; colIdx++)
                    matrix[rowIdx, colIdx] = -matrix[rowIdx, colIdx];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NegateColumns(this RealMatrix matrix, IEnumerable<int> colIndexList)
        {
            foreach (var colIdx in colIndexList)
                for (var rowIdx = 0; rowIdx < matrix.RowCount; rowIdx++)
                    matrix[rowIdx, colIdx] = -matrix[rowIdx, colIdx];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NegateColumns(this ComplexMatrix matrix, IEnumerable<int> colIndexList)
        {
            foreach (var colIdx in colIndexList)
                for (var rowIdx = 0; rowIdx < matrix.RowCount; rowIdx++)
                    matrix[rowIdx, colIdx] = -matrix[rowIdx, colIdx];
        }


        /// <summary>
        /// Create a symmetric matrix from a square matrix
        /// </summary>
        /// <param name="matrix">Matrix to validate</param>
        public static RealMatrix ToSymmetric(this RealMatrix matrix)
        {
            var n = matrix.Size();
            var result = new double[n, n];

            for (var i = 0; i < n; i++)
            {
                result[i, i] = matrix[i, i];

                for (var j = i + 1; j < n; j++)
                {
                    var scalar = (matrix[i, j] + matrix[j, i]) / 2;
                    result[i, j] = scalar;
                    result[j, i] = scalar;
                }
            }

            return result.ToMatrix();
        }

        /// <summary>
        /// Create a skew-symmetric matrix from a square matrix
        /// </summary>
        /// <param name="matrix">Matrix to validate</param>
        public static RealMatrix ToSkewSymmetric(this RealMatrix matrix)
        {
            var n = matrix.Size();
            var result = new double[n, n];

            for (var i = 0; i < n; i++)
            {
                for (var j = i + 1; j < n; j++)
                {
                    var scalar = (matrix[i, j] - matrix[j, i]) / 2;
                    result[i, j] = scalar;
                    result[j, i] = -scalar;
                }
            }

            return result.ToMatrix();
        }


        /// <summary>
        /// Validate that a matrix is identity
        /// </summary>
        /// <param name="matrix">Matrix to validate</param>
        /// <param name="tolerance"></param>
        public static bool IsNearIdentity(this RealMatrix matrix, double tolerance = Float64Utils.DefaultTolerance)
        {
            var n = matrix.RowCount;
            if (n != matrix.ColumnCount)
                return false;

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < i; j++)
                {
                    if (!matrix[i, j].IsNearZero(tolerance))
                        return false;
                }

                if (!matrix[i, i].IsNearOne(tolerance))
                    return false;

                for (var j = i + 1; j < n; j++)
                {
                    if (!matrix[i, j].IsNearZero(tolerance))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Validate that a matrix is diagonal with ±1 scalars
        /// </summary>
        /// <param name="matrix">Matrix to validate</param>
        /// <param name="tolerance"></param>
        public static bool IsNearUnitDiagonal(this RealMatrix matrix, double tolerance = Float64Utils.DefaultTolerance)
        {
            var n = matrix.RowCount;
            if (n != matrix.ColumnCount)
                return false;

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < i; j++)
                {
                    if (!matrix[i, j].IsNearZero(tolerance))
                        return false;
                }

                if (!Math.Abs(matrix[i, i]).IsNearOne(tolerance))
                    return false;

                for (var j = i + 1; j < n; j++)
                {
                    if (!matrix[i, j].IsNearZero(tolerance))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Validate that a matrix is diagonal
        /// </summary>
        /// <param name="matrix">Matrix to validate</param>
        /// <param name="tolerance"></param>
        public static bool IsNearDiagonal(this RealMatrix matrix, double tolerance = Float64Utils.DefaultTolerance)
        {
            var n = matrix.RowCount;
            if (n != matrix.ColumnCount)
                return false;

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < i; j++)
                {
                    if (!matrix[i, j].IsNearZero(tolerance))
                        return false;
                }

                for (var j = i + 1; j < n; j++)
                {
                    if (!matrix[i, j].IsNearZero(tolerance))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Validate that a matrix is symmetric
        /// </summary>
        /// <param name="matrix">Matrix to validate</param>
        /// <param name="tolerance"></param>
        public static bool IsNearSymmetric(this RealMatrix matrix, double tolerance = Float64Utils.DefaultTolerance)
        {
            var n = matrix.RowCount;
            if (n != matrix.ColumnCount)
                return false;

            for (var i = 0; i < n; i++)
                for (var j = 0; j < n; j++)
                {
                    if (Math.Abs(matrix[i, j] - matrix[j, i]) > tolerance)
                        return false;
                }

            return true;
        }

        /// <summary>
        /// Validate that a matrix is skew-symmetric
        /// </summary>
        /// <param name="matrix">Matrix to validate</param>
        /// <param name="tolerance"></param>
        public static bool IsNearSkewSymmetric(this RealMatrix matrix, double tolerance = Float64Utils.DefaultTolerance)
        {
            var n = matrix.RowCount;
            if (n != matrix.ColumnCount)
                return false;

            for (var i = 0; i < n; i++)
            {
                // Check diagonal is zero
                if (Math.Abs(matrix[i, i]) > tolerance)
                    return false;

                // Check skew-symmetry
                for (var j = 0; j < n; j++)
                {
                    if (Math.Abs(matrix[i, j] + matrix[j, i]) > tolerance)
                        return false;
                }
            }

            return true;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetVectorNorm(this RealVector vector)
        {
            return vector.DotProduct(vector).Sqrt();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetVectorNormSquared(this RealVector vector)
        {
            return vector.DotProduct(vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorDot(this RealVector vector1, RealVector vector2)
        {
            return vector1.DotProduct(vector2);
        }


        /// <summary>
        /// Reduce a square matrix to upper Hessenberg form using Householder reflections
        /// https://en.wikipedia.org/wiki/Hessenberg_matrix
        /// </summary>
        /// <returns>Tuple of (tridiagonal bivector, accumulated versor)</returns>
        public static (RealMatrix hessenbergMatrix, List<XGaFloat64Vector> householderVectors) ToUpperHessenbergForm(this RealMatrix matrix, double tolerance = Float64Utils.DefaultTolerance)
        {
            // Convert bivector to matrix for Householder reduction
            var vSpaceDimensions = matrix.Size();
            var hessenbergMatrix = matrix;
            var householderVectors = new List<XGaFloat64Vector>(vSpaceDimensions);

            // Apply Householder reflections iteratively
            for (var basisVectorIndex = 0; basisVectorIndex < vSpaceDimensions - 2; basisVectorIndex++)
            {
                // Apply Householder reflection to zero out elements below the subdiagonal
                (matrix, var householderVector) =
                    matrix.ToUpperHessenbergFormStep(basisVectorIndex, tolerance);

                // Accumulate the orthogonal transformations
                if (!householderVector.IsZero)
                    householderVectors.Add(householderVector);
            }

            if (ReferenceEquals(hessenbergMatrix, matrix))
                hessenbergMatrix = matrix.Clone();

            return (hessenbergMatrix, householderVectors);
        }

        /// <summary>
        /// Apply a single Householder reflection step to reduce a square matrix to upper Hessenberg form
        /// https://en.wikipedia.org/wiki/Householder_transformation
        /// </summary>
        /// <param name="matrix">Current matrix</param>
        /// <param name="basisVectorIndex">Current step index</param>
        /// <param name="tolerance"></param>
        /// <returns>Tuple of (reflected matrix, Householder matrix)</returns>
        private static (RealMatrix transformedMatrix, XGaFloat64Vector householderVector) ToUpperHessenbergFormStep(this RealMatrix matrix, int basisVectorIndex, double tolerance = Float64Utils.DefaultTolerance)
        {
            var processor = XGaFloat64Processor.Euclidean;
            var vSpaceDimensions = matrix.Size();
            var householderMatrix = RealDenseMatrix.CreateIdentity(vSpaceDimensions);

            if (basisVectorIndex >= vSpaceDimensions - 1)
                return (matrix, processor.VectorZero);

            // Extract the column vector from position (step+1, step) downward
            var columnLength = vSpaceDimensions - basisVectorIndex - 1;
            if (columnLength <= 0) return (matrix, processor.VectorZero);

            var x = new double[columnLength];
            for (var i = 0; i < columnLength; i++)
                x[i] = matrix[basisVectorIndex + 1 + i, basisVectorIndex];

            // Compute Householder vector
            var norm = 0.0;
            for (var i = 0; i < columnLength; i++)
                norm += x[i] * x[i];
            norm = Math.Sqrt(norm);

            if (norm < tolerance) return (matrix, processor.VectorZero);

            // Create Householder vector v = x - ||x||e₁
            var v = new double[columnLength];
            v[0] = x[0] - (x[0] >= 0 ? norm : -norm);
            for (var i = 1; i < columnLength; i++)
                v[i] = x[i];

            // Normalize v
            var vNorm = 0.0;
            for (var i = 0; i < columnLength; i++)
                vNorm += v[i] * v[i];

            if (vNorm < tolerance)
                return (matrix, processor.VectorZero);

            vNorm = Math.Sqrt(vNorm);
            var composer = processor.CreateVectorComposer();
            for (var i = 0; i < columnLength; i++)
            {
                var scalar = v[i] / vNorm;
                if (scalar.IsNearZero(tolerance))
                {
                    v[i] = 0;
                    continue;
                }

                v[i] = scalar;
                composer.SetVectorTerm(basisVectorIndex + 1 + i, scalar);
            }
            var householderVector = composer.GetVector();

            // Create Householder matrix H = I - 2vvᵀ for the subspace
            var subH = Float64ArrayUtils.CreateIdentity(columnLength);
            for (var i = 0; i < columnLength; i++)
                for (var j = 0; j < columnLength; j++)
                    subH[i, j] -= 2.0 * v[i] * v[j];

            // Embed subH into the full Householder matrix
            for (var i = 0; i < columnLength; i++)
                for (var j = 0; j < columnLength; j++)
                    householderMatrix[basisVectorIndex + 1 + i, basisVectorIndex + 1 + j] = subH[i, j];

            // Apply similarity transformation: A' = H A H (since H is symmetric)
            var transformedMatrix =
                householderMatrix.Multiply(matrix).Multiply(householderMatrix);

            return (transformedMatrix, householderVector);
        }




        //TODO: This is AI generated and needs validation.
        /// <summary>
        /// Computes the Schur decomposition of a real square matrix using the QR algorithm.
        /// A = Q * T * Q^T where Q is orthogonal and T is upper quasi-triangular.
        /// </summary>
        /// <param name="matrix">The square matrix to decompose</param>
        /// <param name="maxIterations">Maximum number of QR iterations (default: 1000)</param>
        /// <param name="tolerance">Convergence tolerance (default: 1e-12)</param>
        /// <returns>Schur decomposition result containing Q and T matrices</returns>
        /// <exception cref="ArgumentException">Thrown when matrix is not square</exception>
        public static SchurDecompositionResult SchurDecomposition(this RealMatrix matrix, int maxIterations = 1000, double tolerance = Float64Utils.DefaultTolerance)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            if (matrix.RowCount != matrix.ColumnCount)
                throw new ArgumentException("Matrix must be square for Schur decomposition.", nameof(matrix));

            if (matrix.RowCount == 0)
                throw new ArgumentException("Matrix cannot be empty.", nameof(matrix));

            var n = matrix.RowCount;

            // Handle 1x1 matrix
            if (n == 1)
            {
                return new SchurDecompositionResult(
                    RealDenseMatrix.CreateIdentity(n),
                    matrix.Clone(),
                    true
                );
            }

            // Make a copy to avoid modifying the original
            var matrixT = matrix.Clone();
            var matrixQ = RealDenseMatrix.CreateIdentity(n);

            // Reduce to Hessenberg form first (improves convergence)
            ReduceToHessenberg(matrixT, matrixQ);

            // Apply QR algorithm with shifts
            var converged = ApplyQrIterations(matrixT, matrixQ, maxIterations, tolerance);

            // Ensure T is properly quasi-triangular
            EnsureQuasiTriangular(matrixT, tolerance);

            return new SchurDecompositionResult(matrixQ, matrixT, IsStrictlyUpperTriangular(matrixT, tolerance));
        }

        /// <summary>
        /// Reduces a matrix to upper Hessenberg form using Householder reflections.
        /// </summary>
        private static void ReduceToHessenberg(RealMatrix matrix, RealMatrix matrixQ)
        {
            var n = matrix.RowCount;

            for (var k = 0; k < n - 2; k++)
            {
                // Extract subvector for Householder reflection
                var x = matrix.SubMatrix(k + 1, n - k - 1, k, 1).Column(0);
                var xNorm = x.L2Norm();

                if (xNorm < 1e-15) continue; // Already zero below

                // Determine sign to avoid cancellation
                var alpha = x[0] >= 0 ? -xNorm : xNorm;
                var beta = 1.0 / Math.Sqrt(2.0 * xNorm * (xNorm - Math.Abs(alpha)));

                // Compute Householder vector
                var v = x.Clone();
                v[0] -= alpha;
                v.Multiply(beta, v);

                // Apply Householder transformation to matrix
                ApplyHouseholder(matrix, v, k + 1, k);

                // Accumulate transformation in Q
                ApplyHouseholderLeft(matrixQ, v, k + 1);
            }
        }

        /// <summary>
        /// Applies Householder transformation to a matrix: A = (I - 2*v*v^T)*A
        /// </summary>
        private static void ApplyHouseholder(RealMatrix matrix, RealVector v, int rowOffset, int colOffset)
        {
            var n = matrix.RowCount;
            var m = v.Count;

            // Compute v^T * A_part
            var vTbyA = RealDenseVector.Create(
                n - colOffset,
                j =>
                    v.DotProduct(
                        matrix.SubMatrix(rowOffset, m, colOffset + j, 1).Column(0)
                    )
                );

            // Compute outer product and subtract 2*v*(v^T*A)
            for (var i = 0; i < m; i++)
            {
                for (var j = 0; j < n - colOffset; j++)
                {
                    matrix[rowOffset + i, colOffset + j] -= 2.0 * v[i] * vTbyA[j];
                }
            }
        }

        /// <summary>
        /// Applies Householder transformation from the left to Q: Q = Q*(I - 2*v*v^T)
        /// </summary>
        private static void ApplyHouseholderLeft(RealMatrix matrixQ, RealVector v, int rowOffset)
        {
            var n = matrixQ.RowCount;
            var m = v.Count;

            // Compute Q_part * v
            var matrixQv = RealDenseVector.Create(n, i =>
                matrixQ.SubMatrix(i, 1, rowOffset, m).Row(0).DotProduct(v));

            // Compute outer product and subtract 2*(Q*v)*v^T
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    matrixQ[i, rowOffset + j] -= 2.0 * matrixQv[i] * v[j];
                }
            }
        }

        /// <summary>
        /// Applies QR iterations with Wilkinson shift to compute Schur form.
        /// </summary>
        private static bool ApplyQrIterations(RealMatrix matrixT, RealMatrix matrixQ, int maxIterations, double tolerance = Float64Utils.DefaultTolerance)
        {
            var n = matrixT.RowCount;
            var iter = 0;

            while (iter < maxIterations)
            {
                // Check for convergence
                if (CheckConvergence(matrixT, tolerance))
                    break;

                // Find the smallest k such that T is block upper triangular
                var k = FindConvergencePoint(matrixT, tolerance);

                if (k >= n - 1)
                    break;

                // Apply QR step with shift
                ApplyQrStep(matrixT, matrixQ, k, n, tolerance);
                iter++;
            }

            return iter < maxIterations;
        }

        /// <summary>
        /// Applies a single QR iteration step with Wilkinson shift.
        /// </summary>
        private static void ApplyQrStep(RealMatrix matrixT, RealMatrix matrixQ, int start, int end, double tolerance = Float64Utils.DefaultTolerance)
        {
            // Compute Wilkinson shift
            var shift = ComputeWilkinsonShift(matrixT, end - 1, tolerance);

            // Deflate by shift
            for (var i = start; i < end; i++)
            {
                matrixT[i, i] -= shift;
            }

            // Perform QR factorization implicitly
            for (var i = start; i < end - 1; i++)
            {
                // Create Givens rotation to zero out T[i+1,i]
                double c, s;
                ComputeGivensRotation(matrixT[i, i], matrixT[i + 1, i], out c, out s);

                // Apply Givens rotation from the left
                ApplyGivensLeft(matrixT, c, s, i, i + 1, start, end);

                // Apply Givens rotation from the right
                ApplyGivensRight(matrixT, c, s, i, i + 1, start, end);

                // Accumulate in Q
                ApplyGivensLeft(matrixQ, c, s, i, i + 1, 0, matrixQ.ColumnCount);
            }

            // Add shift back
            for (var i = start; i < end; i++)
            {
                matrixT[i, i] += shift;
            }
        }

        /// <summary>
        /// Computes Wilkinson shift for QR algorithm.
        /// </summary>
        private static double ComputeWilkinsonShift(RealMatrix matrixT, int idx, double tolerance = Float64Utils.DefaultTolerance)
        {
            if (idx == 0) return matrixT[idx, idx];

            // 2x2 trailing principal submatrix
            var a = matrixT[idx - 1, idx - 1];
            var b = matrixT[idx - 1, idx];
            var c = matrixT[idx, idx - 1];
            var d = matrixT[idx, idx];

            var trace = a + d;
            var det = a * d - b * c;

            // Eigenvalues of the 2x2 matrix
            var discriminant = trace * trace - 4 * det;

            if (discriminant >= 0)
            {
                var sqrtDisc = Math.Sqrt(discriminant);
                var lambda1 = (trace + sqrtDisc) / 2;
                var lambda2 = (trace - sqrtDisc) / 2;

                // Choose eigenvalue closer to d
                return Math.Abs(lambda1 - d) < Math.Abs(lambda2 - d) ? lambda1 : lambda2;
            }

            // Complex eigenvalues - use d as shift
            return d;
        }

        /// <summary>
        /// Computes Givens rotation parameters to zero out element (i+1,i).
        /// </summary>
        private static void ComputeGivensRotation(double a, double b, out double c, out double s)
        {
            if (Math.Abs(b) < 1e-15)
            {
                c = 1.0;
                s = 0.0;
                return;
            }

            if (Math.Abs(a) < 1e-15)
            {
                c = 0.0;
                s = b > 0 ? 1.0 : -1.0;
                return;
            }

            if (Math.Abs(a) > Math.Abs(b))
            {
                var t = b / a;
                var s1 = 1.0 / Math.Sqrt(1.0 + t * t);
                c = Math.Abs(a) / a * s1;
                s = c * t;
            }
            else
            {
                var t = a / b;
                var s1 = 1.0 / Math.Sqrt(1.0 + t * t);
                s = Math.Abs(b) / b * s1;
                c = s * t;
            }
        }

        /// <summary>
        /// Applies Givens rotation from the left: A = G*A
        /// </summary>
        private static void ApplyGivensLeft(RealMatrix matrixA, double c, double s, int i1, int i2, int colStart, int colEnd)
        {
            for (var j = colStart; j < colEnd; j++)
            {
                var a1 = matrixA[i1, j];
                var a2 = matrixA[i2, j];
                matrixA[i1, j] = c * a1 + s * a2;
                matrixA[i2, j] = -s * a1 + c * a2;
            }
        }

        /// <summary>
        /// Applies Givens rotation from the right: A = A*G^T
        /// </summary>
        private static void ApplyGivensRight(RealMatrix matrixA, double c, double s, int i1, int i2, int rowStart, int rowEnd)
        {
            for (var i = rowStart; i < rowEnd; i++)
            {
                var a1 = matrixA[i, i1];
                var a2 = matrixA[i, i2];
                matrixA[i, i1] = c * a1 - s * a2;
                matrixA[i, i2] = s * a1 + c * a2;
            }
        }

        /// <summary>
        /// Checks if the matrix has converged to quasi-triangular form.
        /// </summary>
        private static bool CheckConvergence(RealMatrix matrixT, double tolerance = Float64Utils.DefaultTolerance)
        {
            var n = matrixT.RowCount;
            for (var i = 1; i < n; i++)
            {
                if (Math.Abs(matrixT[i, i - 1]) > tolerance)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Finds the point where subdiagonal elements are negligible.
        /// </summary>
        private static int FindConvergencePoint(RealMatrix matrixT, double tolerance = Float64Utils.DefaultTolerance)
        {
            var n = matrixT.RowCount;
            for (var i = n - 1; i >= 1; i--)
            {
                if (Math.Abs(matrixT[i, i - 1]) > tolerance)
                    return i - 1;
            }

            return 0;
        }

        /// <summary>
        /// Ensures the matrix is properly in quasi-triangular form.
        /// </summary>
        private static void EnsureQuasiTriangular(RealMatrix matrixT, double tolerance = Float64Utils.DefaultTolerance)
        {
            var n = matrixT.RowCount;
            for (var i = 1; i < n; i++)
            {
                if (Math.Abs(matrixT[i, i - 1]) < tolerance)
                    matrixT[i, i - 1] = 0.0;
            }
        }

        /// <summary>
        /// Checks if the matrix is strictly upper triangular (no 2x2 blocks).
        /// </summary>
        private static bool IsStrictlyUpperTriangular(RealMatrix matrixT, double tolerance = Float64Utils.DefaultTolerance)
        {
            var n = matrixT.RowCount;
            for (var i = 0; i < n - 1; i++)
            {
                if (Math.Abs(matrixT[i + 1, i]) > tolerance)
                    return false;
            }

            return true;
        }
    }
}
