using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Numerical.BivectorSchurDecomposer
{
    /// <summary>
    /// Result of bivector Schur decomposition using native multivector operations
    /// </summary>
    public class BivectorSchurResult
    {
        #region Properties
        
        /// <summary>
        /// Original bivector that was decomposed
        /// </summary>
        public XGaFloat64Bivector OriginalBivector { get; }
        
        /// <summary>
        /// Orthogonal transformation rotor/versor: R such that OriginalBivector = R * SchurForm * R†
        /// </summary>
        public XGaFloat64Multivector OrthogonalRotor { get; }
        
        /// <summary>
        /// Schur form of the bivector (block-diagonal with 2×2 rotation blocks)
        /// </summary>
        public XGaFloat64Bivector SchurForm { get; }
        
        /// <summary>
        /// Decomposed canonical blocks representing orthogonal rotation planes
        /// </summary>
        public List<CanonicalBlock> CanonicalBlocks { get; }
        
        /// <summary>
        /// Decomposition quality metrics
        /// </summary>
        public DecompositionMetrics Metrics { get; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Initialize bivector Schur decomposition result
        /// </summary>
        /// <param name="originalBivector">Original bivector</param>
        /// <param name="orthogonalRotor">Orthogonal transformation rotor</param>
        /// <param name="schurForm">Schur form bivector</param>
        /// <param name="canonicalBlocks">Optional canonical blocks</param>
        public BivectorSchurResult(
            XGaFloat64Bivector originalBivector, 
            XGaFloat64Multivector orthogonalRotor, 
            XGaFloat64Bivector schurForm,
            List<CanonicalBlock> canonicalBlocks = null)
        {
            OriginalBivector = originalBivector ?? throw new ArgumentNullException(nameof(originalBivector));
            OrthogonalRotor = orthogonalRotor ?? throw new ArgumentNullException(nameof(orthogonalRotor));
            SchurForm = schurForm ?? throw new ArgumentNullException(nameof(schurForm));
            CanonicalBlocks = canonicalBlocks ?? new List<CanonicalBlock>();
            
            Metrics = ComputeMetrics();
        }
        
        #endregion
        
        #region Validation Methods
        
        /// <summary>
        /// Verify the decomposition quality by reconstructing the original bivector
        /// </summary>
        /// <param name="tolerance">Numerical tolerance for verification</param>
        /// <returns>True if decomposition is accurate within tolerance</returns>
        public bool VerifyDecomposition(double tolerance = Float64Utils.DefaultTolerance)
        {
            // Reconstruct: B_original = R * B_schur * R†
            var rotorReverse = OrthogonalRotor.Reverse();
            var reconstructed = OrthogonalRotor.Gp(SchurForm).Gp(rotorReverse).GetBivectorPart();
            
            // Compare with original
            var difference = (OriginalBivector - reconstructed).ENormSquared();
            return Math.Sqrt(difference) < tolerance;
        }
        
        /// <summary>
        /// Verify orthogonality of the transformation rotor
        /// </summary>
        /// <param name="tolerance">Numerical tolerance</param>
        /// <returns>True if rotor preserves norms</returns>
        public bool VerifyOrthogonality(double tolerance = Float64Utils.DefaultTolerance)
        {
            // For rotors: R * R† = 1 (up to scaling)
            // For versors: normalization may be needed
            var product = OrthogonalRotor.Gp(OrthogonalRotor.Reverse());
            var scalar = product.GetScalarPart().ScalarValue;
            
            // Check if it's approximately a scalar multiple of identity
            var nonScalarPart = product.Subtract(product.GetScalarPart());
            var nonScalarNorm = nonScalarPart.ENormSquared();
            
            return Math.Sqrt(nonScalarNorm) < tolerance && Math.Abs(scalar) > tolerance;
        }
        
        #endregion
        
        #region Private Methods
        
        /// <summary>
        /// Compute decomposition quality metrics
        /// </summary>
        /// <returns>Computed metrics</returns>
        private DecompositionMetrics ComputeMetrics()
        {
            var isAccurate = VerifyDecomposition();
            var isOrthogonal = VerifyOrthogonality();
            
            // Compute reconstruction error
            var rotorReverse = OrthogonalRotor.Reverse();
            var reconstructed = OrthogonalRotor.Gp(SchurForm).Gp(rotorReverse).GetBivectorPart();
            var error = (OriginalBivector - reconstructed).ENormSquared();
            
            return new DecompositionMetrics
            {
                IsAccurate = isAccurate,
                IsOrthogonal = isOrthogonal,
                ReconstructionError = Math.Sqrt(error),
                OriginalNorm = OriginalBivector.ENorm(),
                SchurFormNorm = SchurForm.ENorm()
            };
        }
        
        #endregion
    }
    
    /// <summary>
    /// Result of matrix-based Schur decomposition with bivector correspondence
    /// </summary>
    public class MatrixSchurResult
    {
        #region Properties
        
        /// <summary>
        /// Original skew-symmetric matrix
        /// </summary>
        public Matrix<double> OriginalMatrix { get; }
        
        /// <summary>
        /// Orthogonal matrix Q such that A = Q * T * Q^T
        /// </summary>
        public Matrix<double> OrthogonalMatrix { get; }
        
        /// <summary>
        /// Schur form matrix T (block-diagonal)
        /// </summary>
        public Matrix<double> SchurMatrix { get; }
        
        /// <summary>
        /// Corresponding bivector decomposition result
        /// </summary>
        public BivectorSchurResult BivectorResult { get; }
        
        /// <summary>
        /// Matrix dimensions
        /// </summary>
        public int Dimensions => OriginalMatrix.RowCount;
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Initialize matrix Schur decomposition result
        /// </summary>
        /// <param name="originalMatrix">Original skew-symmetric matrix</param>
        /// <param name="orthogonalMatrix">Orthogonal transformation matrix</param>
        /// <param name="schurMatrix">Schur form matrix</param>
        /// <param name="bivectorResult">Corresponding bivector result</param>
        public MatrixSchurResult(
            Matrix<double> originalMatrix,
            Matrix<double> orthogonalMatrix,
            Matrix<double> schurMatrix,
            BivectorSchurResult bivectorResult)
        {
            OriginalMatrix = originalMatrix ?? throw new ArgumentNullException(nameof(originalMatrix));
            OrthogonalMatrix = orthogonalMatrix ?? throw new ArgumentNullException(nameof(orthogonalMatrix));
            SchurMatrix = schurMatrix ?? throw new ArgumentNullException(nameof(schurMatrix));
            BivectorResult = bivectorResult ?? throw new ArgumentNullException(nameof(bivectorResult));
        }
        
        #endregion
        
        #region Validation Methods
        
        /// <summary>
        /// Verify matrix decomposition: A = Q * T * Q^T
        /// </summary>
        /// <param name="tolerance">Numerical tolerance</param>
        /// <returns>True if decomposition is accurate</returns>
        public bool VerifyMatrixDecomposition(double tolerance = Float64Utils.DefaultTolerance)
        {
            var n = Dimensions;
            
            // Compute Q * T * Q^T
            var reconstructed = 
                OrthogonalMatrix * SchurMatrix * OrthogonalMatrix.Transpose();
            
            // Compare with original
            for (var i = 0; i < n; i++)
            for (var j = 0; j < n; j++)
            {
                if (Math.Abs(reconstructed[i, j] - OriginalMatrix[i, j]) > tolerance)
                    return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// Verify orthogonality of transformation matrix: Q^T * Q = I
        /// </summary>
        /// <param name="tolerance">Numerical tolerance</param>
        /// <returns>True if matrix is orthogonal</returns>
        public bool VerifyMatrixOrthogonality(double tolerance = Float64Utils.DefaultTolerance)
        {
            return OrthogonalMatrix.IsOrthogonal(tolerance);
        }
        
        #endregion
        
        #region Helper Methods
        
        private double[,] MatrixMultiply(double[,] a, double[,] b)
        {
            var rows = a.GetLength(0);
            var cols = b.GetLength(1);
            var inner = a.GetLength(1);
            var result = new double[rows, cols];
            
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    for (var k = 0; k < inner; k++)
                    {
                        result[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            
            return result;
        }
        
        private double[,] MatrixTranspose(double[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            var result = new double[cols, rows];
            
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }
            
            return result;
        }
        
        #endregion
    }
    
    /// <summary>
    /// Represents a canonical 2×2 block corresponding to a rotation plane
    /// </summary>
    public class CanonicalBlock
    {
        #region Properties
        
        /// <summary>
        /// Rotation frequency (magnitude of the bivector in this plane)
        /// </summary>
        public double Frequency { get; }
        
        /// <summary>
        /// Basis vectors spanning the rotation plane
        /// </summary>
        public (XGaFloat64Vector, XGaFloat64Vector) BasisVectors { get; }
        
        /// <summary>
        /// Corresponding bivector for this rotation plane
        /// </summary>
        public XGaFloat64Bivector PlaneBivector { get; }
        
        /// <summary>
        /// Indices of the basis vectors in the original space
        /// </summary>
        public (int, int) BasisIndices { get; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Initialize canonical block
        /// </summary>
        /// <param name="frequency">Rotation frequency</param>
        /// <param name="basisVectors">Basis vectors of the plane</param>
        /// <param name="planeBivector">Bivector representing the plane</param>
        /// <param name="basisIndices">Indices of basis vectors</param>
        public CanonicalBlock(
            double frequency,
            (XGaFloat64Vector, XGaFloat64Vector) basisVectors,
            XGaFloat64Bivector planeBivector,
            (int, int) basisIndices)
        {
            Frequency = frequency;
            BasisVectors = basisVectors;
            PlaneBivector = planeBivector ?? throw new ArgumentNullException(nameof(planeBivector));
            BasisIndices = basisIndices;
        }
        
        #endregion
        
        #region Methods
        
        /// <summary>
        /// Get the 2×2 matrix representation of this block
        /// </summary>
        /// <returns>2×2 skew-symmetric matrix</returns>
        public double[,] GetMatrixBlock()
        {
            return new double[,]
            {
                { 0, Frequency },
                { -Frequency, 0 }
            };
        }
        
        /// <summary>
        /// Get eigenvalues of this block (±iω)
        /// </summary>
        /// <returns>Complex eigenvalues</returns>
        public (System.Numerics.Complex, System.Numerics.Complex) GetEigenvalues()
        {
            var omega = Math.Abs(Frequency);
            return (
                new System.Numerics.Complex(0, omega),
                new System.Numerics.Complex(0, -omega)
            );
        }
        
        #endregion
    }
    
    /// <summary>
    /// Metrics for evaluating decomposition quality
    /// </summary>
    public class DecompositionMetrics
    {
        /// <summary>
        /// Whether the decomposition accurately reconstructs the original
        /// </summary>
        public bool IsAccurate { get; set; }
        
        /// <summary>
        /// Whether the transformation preserves orthogonality
        /// </summary>
        public bool IsOrthogonal { get; set; }
        
        /// <summary>
        /// Numerical error in reconstruction
        /// </summary>
        public double ReconstructionError { get; set; }
        
        /// <summary>
        /// Norm of the original bivector/matrix
        /// </summary>
        public double OriginalNorm { get; set; }
        
        /// <summary>
        /// Norm of the Schur form
        /// </summary>
        public double SchurFormNorm { get; set; }
        
        /// <summary>
        /// Relative error as fraction of original norm
        /// </summary>
        public double RelativeError => OriginalNorm > 0 ? ReconstructionError / OriginalNorm : 0;
    }
}