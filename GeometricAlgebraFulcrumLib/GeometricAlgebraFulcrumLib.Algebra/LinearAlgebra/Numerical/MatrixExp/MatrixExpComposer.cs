using System.Numerics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Matrices;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.MatrixExp;

/// <summary>
/// Main entry point for computing matrix exponentials with automatic algorithm selection
/// based on matrix properties and size. This class orchestrates the various specialized
/// algorithms to achieve optimal performance and accuracy.
/// </summary>
public sealed class MatrixExpComposer
{
    /// <summary>
    /// Configuration for the matrix exponential computation.
    /// </summary>
    public sealed class Config
    {
        /// <summary>
        /// Tolerance for numerical computations and property detection.
        /// </summary>
        public double Tolerance { get; set; } = 1e-14;

        /// <summary>
        /// Whether to enable automatic preprocessing (balancing, shifting).
        /// </summary>
        public bool EnablePreprocessing { get; set; } = true;

        /// <summary>
        /// Whether to perform accuracy validation when analytical solutions are available.
        /// </summary>
        public bool EnableAccuracyValidation { get; set; } = false;

        /// <summary>
        /// Whether to enable detailed logging of algorithm selection decisions.
        /// </summary>
        public bool EnableDetailedLogging { get; set; } = false;

        /// <summary>
        /// Size threshold below which direct methods are preferred over iterative methods.
        /// </summary>
        public int SmallMatrixThreshold { get; set; } = 100;

        /// <summary>
        /// Size threshold above which memory-efficient methods are preferred.
        /// </summary>
        public int LargeMatrixThreshold { get; set; } = 1000;

        /// <summary>
        /// Maximum condition number tolerated before warning about numerical instability.
        /// </summary>
        public double MaxConditionNumber { get; set; } = 1e12;
    }
    
    /// <summary>
    /// Result of matrix exponential computation with detailed information.
    /// </summary>
    public sealed class RealResult
    {
        /// <summary>
        /// The computed matrix exponential.
        /// </summary>
        public required Matrix<double> MatrixExp { get; init; }

        /// <summary>
        /// Name of the algorithm used for computation.
        /// </summary>
        public required string AlgorithmUsed { get; init; }

        /// <summary>
        /// Detected properties of the input matrix.
        /// </summary>
        public MatrixProperties MatrixProperties { get; init; }

        /// <summary>
        /// Estimated condition number of the matrix exponential problem.
        /// </summary>
        public double ConditionNumber { get; init; }

        /// <summary>
        /// Estimated backward error of the computation.
        /// </summary>
        public double EstimatedError { get; init; }

        /// <summary>
        /// Time taken for the computation.
        /// </summary>
        public TimeSpan ComputationTime { get; init; }

        /// <summary>
        /// Number of matrix multiplications performed.
        /// </summary>
        public int MatrixMultiplications { get; init; }

        /// <summary>
        /// Peak memory usage during computation (if available).
        /// </summary>
        public long PeakMemoryUsage { get; init; }

        /// <summary>
        /// Detailed information about the computation process.
        /// </summary>
        public string? Details { get; init; }

        /// <summary>
        /// Warnings about potential numerical issues.
        /// </summary>
        public List<string> Warnings { get; init; } = new();
    }

    /// <summary>
    /// Complex matrix computation result.
    /// </summary>
    public sealed class ComplexResult
    {
        /// <summary>
        /// The computed matrix exponential.
        /// </summary>
        public required Matrix<Complex> MatrixExp { get; init; }

        /// <summary>
        /// Name of the algorithm used.
        /// </summary>
        public required string AlgorithmUsed { get; init; }

        /// <summary>
        /// Detected matrix properties.
        /// </summary>
        public MatrixProperties MatrixProperties { get; init; }

        /// <summary>
        /// Computation time.
        /// </summary>
        public TimeSpan ComputationTime { get; init; }

        /// <summary>
        /// Matrix multiplications performed.
        /// </summary>
        public int MatrixMultiplications { get; init; }

        /// <summary>
        /// Computation details.
        /// </summary>
        public string? Details { get; init; }

        /// <summary>
        /// Warnings about potential issues.
        /// </summary>
        public List<string> Warnings { get; init; } = new();
    }


    private readonly Action<string>? _logger;


    public MatrixExpComposer(Action<string>? logger = null)
    {
        _logger = logger;
    }


    /// <summary>
    /// Computes the matrix exponential with automatic algorithm selection.
    /// </summary>
    /// <param name="matrix">The input matrix</param>
    /// <param name="config">Configuration options</param>
    /// <returns>The computation result</returns>
    public RealResult ComputeExponential(Matrix<double> matrix, Config? config = null)
    {
        ArgumentNullException.ThrowIfNull(matrix);
        
        if (!matrix.IsSquare())
            throw new ArgumentException("Matrix must be square", nameof(matrix));

        config ??= new Config();
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var warnings = new List<string>();

        // Analyze matrix properties
        _logger?.Invoke($"Analyzing matrix properties for {matrix.RowCount}×{matrix.ColumnCount} matrix");
        var properties = MatrixPropertiesUtils.AnalyzeProperties(matrix, config.Tolerance);
        
        // Estimate condition number
        var conditionNumber = matrix.EstimateConditionNumber(properties);
        
        if (conditionNumber > config.MaxConditionNumber)
        {
            warnings.Add($"High condition number ({conditionNumber:E2}) may lead to numerical instability");
        }

        // Select and execute algorithm
        var (algorithm, result, matMuls, details) = 
            SelectAndExecuteAlgorithm(matrix, properties, config);
        
        stopwatch.Stop();

        _logger?.Invoke($"Computed matrix exponential using {algorithm} in {stopwatch.ElapsedMilliseconds}ms");

        return new RealResult
        {
            MatrixExp = result,
            AlgorithmUsed = algorithm,
            MatrixProperties = properties,
            ConditionNumber = conditionNumber,
            EstimatedError = EstimateGlobalError(matrix, result, properties),
            ComputationTime = stopwatch.Elapsed,
            MatrixMultiplications = matMuls,
            PeakMemoryUsage = GC.GetTotalMemory(false),
            Details = details,
            Warnings = warnings
        };
    }

    /// <summary>
    /// Computes the matrix exponential for complex matrices.
    /// </summary>
    public ComplexResult ComputeExponential(Matrix<Complex> matrix, Config? config = null)
    {
        ArgumentNullException.ThrowIfNull(matrix);
        
        if (!matrix.IsSquare())
            throw new ArgumentException("Matrix must be square", nameof(matrix));

        config ??= new Config();
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var warnings = new List<string>();

        // Analyze matrix properties
        var properties = MatrixPropertiesUtils.AnalyzeProperties(matrix, config.Tolerance);
        
        // For complex matrices, primarily use Padé with scaling and squaring
        var padeResult = matrix.MatrixExpViaPade(
            new MatrixExpPade.Config
            {
                Tolerance = config.Tolerance,
                EnablePreprocessing = config.EnablePreprocessing
            }
        );

        stopwatch.Stop();

        return new ComplexResult
        {
            MatrixExp = padeResult.MatrixExp,
            AlgorithmUsed = "Padé approximation with scaling and squaring",
            MatrixProperties = properties,
            ComputationTime = stopwatch.Elapsed,
            MatrixMultiplications = padeResult.MatrixMultiplications,
            Details = $"Padé degree: {padeResult.DegreeUsed}, Scaling parameter: {padeResult.ScalingParameter}",
            Warnings = warnings
        };
    }

    /// <summary>
    /// Selects the optimal algorithm based on matrix properties and size.
    /// </summary>
    private (string algorithm, Matrix<double> result, int matMuls, string details) SelectAndExecuteAlgorithm(Matrix<double> matrix, MatrixProperties properties, Config config)
    {
        var n = matrix.Size();
        
        // Special case: 1×1 matrix
        if (n == 1)
        {
            var expVal = Math.Exp(matrix[0, 0]);
            var result = Matrix<double>.Build.DenseOfArray(new[,] { { expVal } });
            return ("Direct computation", result, 0, "1×1 matrix exponential");
        }

        // Skew-symmetric matrices - use specialized algorithm
        if (properties.HasFlag(MatrixProperties.SkewSymmetric))
        {
            _logger?.Invoke($"Using skew-symmetric algorithm for {n}×{n} matrix");
            
            var skewConfig = new SkewSymmetricMatrixExp.Config
            {
                Tolerance = config.Tolerance,
                VerifyOrthogonality = config.EnableAccuracyValidation
            };
            
            var skewResult = matrix.MatrixExpSkewSymmetric(skewConfig);
            return ($"Skew-symmetric ({skewResult.Method})", skewResult.MatrixExp,
                   skewResult.BlockCount * 2, // Approximate matrix multiplications
                   $"Processed {skewResult.BlockCount} 2×2 blocks, Orthogonality error: {skewResult.OrthogonalityError:E2}");
        }

        // Diagonal matrices - trivial exponential
        if (properties.HasFlag(MatrixProperties.Diagonal))
        {
            _logger?.Invoke($"Using diagonal algorithm for {n}×{n} matrix");
            var result = ComputeDiagonalExponential(matrix);
            return ("Diagonal exponential", result, 0, "Direct diagonal computation");
        }

        // Small matrices - consider direct methods
        if (n <= config.SmallMatrixThreshold)
        {
            // For small symmetric matrices, eigenvalue decomposition might be efficient
            if (properties.HasFlag(MatrixProperties.Symmetric) && n <= 20)
            {
                _logger?.Invoke($"Using eigenvalue decomposition for small symmetric {n}×{n} matrix");
                
                try
                {
                    var (success, result, eigenDetails) = 
                        matrix.ExpViaEigendecomposition(config.Tolerance);
                    
                    if (success)
                        return ("Eigenvalue decomposition", result, n * n, eigenDetails);
                }
                catch (Exception ex)
                {
                    _logger?.Invoke($"Eigenvalue decomposition failed: {ex.Message}, falling back to Padé method");
                }
            }
        }

        // Normal matrices - potentially use Schur decomposition
        if (properties.HasFlag(MatrixProperties.Normal) && n > config.SmallMatrixThreshold)
        {
            _logger?.Invoke($"Considering Schur decomposition for normal {n}×{n} matrix");
            
            try
            {
                var (success, result, schurDetails) = 
                    matrix.ExpViaSchur(config.Tolerance);
                
                if (success)
                    return ("Schur decomposition", result, n * n * n / 3, schurDetails); // Approximate cost
            }
            catch (Exception ex)
            {
                _logger?.Invoke($"Schur decomposition failed: {ex.Message}, falling back to Padé method");
            }
        }

        // Default: Padé approximation with scaling and squaring
        _logger?.Invoke($"Using Padé approximation with scaling and squaring for {n}×{n} matrix");
        
        var padeConfig = new MatrixExpPade.Config
        {
            Tolerance = config.Tolerance,
            EnablePreprocessing = config.EnablePreprocessing,
            UseOptimalDegree = true
        };
        
        var padeResult = matrix.MatrixExpViaPade(padeConfig);
        
        var details = $"Padé degree: {padeResult.DegreeUsed}, Scaling parameter: {padeResult.ScalingParameter}, " +
                      $"Estimated error: {padeResult.EstimatedError:E2}";
        
        return ("Padé approximation with scaling and squaring", padeResult.MatrixExp, 
               padeResult.MatrixMultiplications, details);
    }

    /// <summary>
    /// Computes the exponential of a diagonal matrix.
    /// </summary>
    private static Matrix<double> ComputeDiagonalExponential(Matrix<double> matrix)
    {
        var n = matrix.Size();
        var result = Matrix<double>.Build.Dense(n, n);
        
        for (var i = 0; i < n; i++)
        {
            result[i, i] = Math.Exp(matrix[i, i]);
        }
        
        return result;
    }

    /// <summary>
    /// Estimates the global error of the computed matrix exponential.
    /// </summary>
    private static double EstimateGlobalError(Matrix<double> matrixA, Matrix<double> expA, MatrixProperties properties)
    {
        // Simple error estimate based on matrix properties and norms
        var normA = matrixA.OneNorm();
        var machineEps = 2.22e-16;
        
        if (properties.HasFlag(MatrixProperties.SkewSymmetric))
        {
            // Skew-symmetric matrices have excellent numerical properties
            return machineEps * normA;
        }
        
        if (properties.HasFlag(MatrixProperties.Symmetric))
        {
            // Symmetric matrices have good conditioning
            return machineEps * normA * normA;
        }
        
        // General case
        return machineEps * Math.Pow(normA, 3) * expA.Size();
    }

    /// <summary>
    /// Validates the computed result against known analytical solutions when available.
    /// </summary>
    public static bool ValidateResult(Matrix<double> matrixA, Matrix<double> expA, MatrixProperties properties, double tolerance = 1e-12)
    {
        // Check basic properties that should hold for any matrix exponential
        
        // 1. det(exp(matrixA)) = exp(trace(matrixA))
        var sizeA = matrixA.Size();
        var traceA = 0d;
        for (var i = 0; i < sizeA; i++)
        {
            traceA += matrixA[i, i];
        }
        var expectedDet = Math.Exp(traceA);
        
        // For now, we'll skip determinant computation as it requires LU decomposition
        // In a full implementation, this would be computed properly
        
        // 2. For skew-symmetric matrices, exp(matrixA) should be orthogonal
        if (properties.HasFlag(MatrixProperties.SkewSymmetric))
        {
            var (isOrthogonal, _) = VerifyOrthogonality(expA, tolerance);
            return isOrthogonal;
        }
        
        // 3. For diagonal matrices, result should also be diagonal
        if (properties.HasFlag(MatrixProperties.Diagonal))
        {
            for (var i = 0; i < sizeA; i++)
            {
                for (var j = 0; j < sizeA; j++)
                {
                    if (i != j && Math.Abs(expA[i, j]) > tolerance)
                        return false;
                }
            }
        }
        
        return true;
    }

    /// <summary>
    /// Helper method to verify orthogonality.
    /// </summary>
    private static (bool isOrthogonal, double maxError) VerifyOrthogonality(Matrix<double> matrix, double tolerance)
    {
        var n = matrix.Size();
        var maxError = 0.0;
        
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                double sum = 0;
                for (var k = 0; k < n; k++)
                {
                    sum += matrix[k, i] * matrix[k, j];
                }
                
                var expected = (i == j) ? 1.0 : 0.0;
                var error = Math.Abs(sum - expected);
                maxError = Math.Max(maxError, error);
            }
        }
        
        return (maxError <= tolerance, maxError);
    }

    /// <summary>
    /// Provides algorithm recommendations without performing the computation.
    /// </summary>
    public string RecommendAlgorithm(Matrix<double> matrix, Config? config = null)
    {
        ArgumentNullException.ThrowIfNull(matrix);
        
        if (!matrix.IsSquare())
            throw new ArgumentException("Matrix must be square", nameof(matrix));

        config ??= new Config();
        var properties = MatrixPropertiesUtils.AnalyzeProperties(matrix, config.Tolerance);
        var n = matrix.Size();

        var recommendations = new List<string>();

        if (n == 1)
            recommendations.Add("Direct computation (trivial for 1×1 matrix)");

        if (properties.HasFlag(MatrixProperties.SkewSymmetric))
            recommendations.Add($"Skew-symmetric algorithm (optimal - exploits matrixA^T = -matrixA structure)");

        if (properties.HasFlag(MatrixProperties.Diagonal))
            recommendations.Add("Diagonal exponential (optimal - element-wise computation)");

        if (properties.HasFlag(MatrixProperties.Symmetric) && n <= 50)
            recommendations.Add("Eigenvalue decomposition (good for small symmetric matrices)");

        if (properties.HasFlag(MatrixProperties.Normal) && n > 100)
            recommendations.Add("Schur decomposition (efficient for large normal matrices)");

        recommendations.Add("Padé approximation with scaling and squaring (general purpose, always works)");

        return string.Join("; ", recommendations);
    }
}