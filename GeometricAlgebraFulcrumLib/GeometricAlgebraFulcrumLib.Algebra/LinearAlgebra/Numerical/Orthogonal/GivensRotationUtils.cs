using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Orthogonal
{
    public static class GivensRotationUtils
    {
        /// <summary>
        /// Fast in-place implementation
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void ZeroItemByRotation(this Matrix<double> matrix, int i, int j)
        {
            if (i == j) throw new ArgumentException("i and j must be different.");
            if (i < 0 || j < 0 || i >= matrix.RowCount || j >= matrix.RowCount)
                throw new ArgumentException("Index out of range.");

            var aii = matrix[i, i];
            var ajj = matrix[j, j];
            var aij = matrix[i, j];

            // Compute rotation parameters
            var (c, s) = ComputeGivensRotationParameters();

            var n = matrix.RowCount;

            // Temporary storage for the entire matrix to avoid overwriting issues
            var temp = new double[n, n];
            for (var p = 0; p < n; p++)
            for (var q = 0; q < n; q++)
                temp[p, q] = matrix[p, q];

            // Apply similarity transformation: G^T * A * G
            // G matrix has: G[i,i] = c, G[i,j] = -s, G[j,i] = s, G[j,j] = c
            // G^T matrix has: G^T[i,i] = c, G^T[i,j] = s, G^T[j,i] = -s, G^T[j,j] = c

            // Step 1: First compute A * G (right multiplication)
            // This affects only columns i and j
            for (var k = 0; k < n; k++)
            {
                var aki = temp[k, i];
                var akj = temp[k, j];
                matrix[k, i] = c * aki + s * akj;      // Column i transformation
                matrix[k, j] = -s * aki + c * akj;     // Column j transformation
            }

            // Step 2: Then compute G^T * (A*G) (left multiplication)
            // This affects only rows i and j
            // But we need to use the updated matrix values from step 1
            for (var k = 0; k < n; k++)
            {
                var ik = matrix[i, k];  // Already updated from step 1
                var jk = matrix[j, k];  // Already updated from step 1
                matrix[i, k] = c * ik + s * jk;       // Row i transformation (G^T[i,i] * row_i + G^T[i,j] * row_j)
                matrix[j, k] = -s * ik + c * jk;      // Row j transformation (G^T[j,i] * row_i + G^T[j,j] * row_j)
            }

            // The (i,j) and (j,i) elements should now be zero (or very close to zero)
            // Set them explicitly to zero to avoid numerical errors
            matrix[i, j] = 0.0;
            matrix[j, i] = 0.0;

            return;

            (double c, double s) ComputeGivensRotationParameters()
            {
                const double tolerance = 1e-25;

                if (Math.Abs(aij) < tolerance)
                    return (1.0, 0.0); // No rotation needed

                // Standard Jacobi angle formula:
                // tan(2θ) = 2 * Aij / (Aii - Ajj)
                // Avoid overflow by using atan2-based computation

                var diff = aii - ajj;
                var theta = (Math.Abs(diff) < tolerance)
                    ? Math.PI / 4  // 45 degrees: equal diagonals → θ = 45°
                    : 0.5 * Math.Atan2(2 * aij, diff);  // in radians

                var cos = Math.Cos(theta);
                var sin = Math.Sin(theta);

                return (cos, sin);
            }
        }

        // Slow reference implementation
        public static Matrix<double> ZeroItemByRotationSlow(this Matrix<double> matrix, int i, int j, double tolerance = Float64Utils.DefaultTolerance)
        {
            var n = matrix.RowCount;

            var mii = matrix[i, i];
            var mjj = matrix[j, j];
            var mij = matrix[i, j];

            var matrixG = CreateGivensRotationSparseMatrix(n, i, j, mii, mjj, mij, tolerance);

            return matrixG.Transpose() * matrix * matrixG;

            static SparseMatrix CreateGivensRotationSparseMatrix(int n, int i, int j, double aii, double ajj, double aij, double tolerance = 1e-25)
            {
                if (i == j)
                    throw new ArgumentException("Indices i and j must be different.");

                if (n <= Math.Max(i, j))
                    throw new ArgumentException("Indices out of range.");

                // Ensure i < j for consistent computation (swap if needed, but track orientation)
                var swap = i > j;
                if (swap)
                {
                    // Swap roles so we always compute for i < j
                    (i, j) = (j, i);
                }

                // Compute rotation angle theta to annihilate off-diagonal element
                double theta;

                if (Math.Abs(aij) < tolerance)
                {
                    // Already zero: no rotation needed
                    theta = 0.0;
                }
                else
                {
                    // Standard Jacobi angle formula:
                    // tan(2θ) = 2 * Aij / (Aii - Ajj)
                    // Avoid overflow by using atan2-based computation

                    var diff = aii - ajj;
                    var ratio = (Math.Abs(diff) < tolerance)
                        ? Math.PI / 4  // 45 degrees: equal diagonals → θ = 45°
                        : 0.5 * Math.Atan2(2 * aij, diff);  // in radians

                    theta = ratio;
                }

                // Construct Givens rotation matrix: identity with (i,i), (i,j), (j,i), (j,j) replaced
                var matrixG = SparseMatrix.CreateIdentity(n);

                var c = Math.Cos(theta);
                var s = Math.Sin(theta);

                // Standard Givens rotation (rotates coordinates counterclockwise in (i,j) plane)
                // G[i,i] = c, G[i,j] = -s, G[j,i] = s, G[j,j] = c
                matrixG.At(i, i, c);
                matrixG.At(i, j, -s);
                matrixG.At(j, i, s);
                matrixG.At(j, j, c);

                return matrixG;
            }
        }

        


        /// <summary>
        /// Applies a sequence of Givens rotations to a matrix from the left
        /// </summary>
        /// <param name="rotations">Sequence of rotations</param>
        /// <param name="matrix">Target matrix</param>
        public static void ApplyLeft(this IEnumerable<GivensRotation> rotations, Matrix<double> matrix)
        {
            foreach (var rotation in rotations)
                rotation.ApplyLeft(matrix);
        }

        /// <summary>
        /// Applies a sequence of Givens rotations to a matrix from the right
        /// </summary>
        /// <param name="rotations">Sequence of rotations</param>
        /// <param name="matrix">Target matrix</param>
        public static void ApplyRight(this IEnumerable<GivensRotation> rotations, Matrix<double> matrix)
        {
            foreach (var rotation in rotations)
                rotation.ApplyRight(matrix);
        }

        /// <summary>
        /// Optimizes a sequence of Givens rotations by identifying patterns
        /// and combining where possible
        /// </summary>
        /// <param name="rotations">Input sequence</param>
        /// <returns>Optimized sequence</returns>
        public static IReadOnlyList<GivensRotation> Optimize(this IEnumerable<GivensRotation> rotations)
        {
            var result = new List<GivensRotation>();
            var rotationList = rotations.ToList();

            // Remove identity rotations
            foreach (var rotation in rotationList)
            {
                if (!rotation.IsIdentity())
                    result.Add(rotation);
            }

            // Additional optimization strategies could be implemented here
            // such as combining adjacent rotations on the same planes

            return result;
        }

    }
}
