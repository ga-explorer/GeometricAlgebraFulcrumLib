using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.MatrixExp;

/// <summary>
/// Implements the Padé approximation with scaling and squaring method for computing matrix exponentials.
/// This is based on Algorithm 10.20 from "Functions of Matrices - Theory and Computation" by Higham.
/// </summary>
public static class MatrixExpPade
{
    /// <summary>
    /// Configuration for the Padé scaling and squaring algorithm.
    /// </summary>
    public sealed class Config
    {
        /// <summary>
        /// Maximum degree of Padé approximant to consider.
        /// </summary>
        public int MaxDegree { get; set; } = 13;

        /// <summary>
        /// Tolerance for numerical computations.
        /// </summary>
        public double Tolerance { get; set; } = 1e-15;

        /// <summary>
        /// Whether to use the optimal degree selection strategy.
        /// </summary>
        public bool UseOptimalDegree { get; set; } = true;

        /// <summary>
        /// Whether to enable preprocessing (balancing, shifting).
        /// </summary>
        public bool EnablePreprocessing { get; set; } = false;
    }
    
    /// <summary>
    /// Result of the Padé scaling and squaring computation.
    /// </summary>
    public sealed class Result<T> where T : struct, INumberBase<T>, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>, ISubtractionOperators<T, T, T>
    {
        /// <summary>
        /// The computed matrix exponential.
        /// </summary>
        public required Matrix<T> MatrixExp { get; init; }

        /// <summary>
        /// The degree of Padé approximant used.
        /// </summary>
        public int DegreeUsed { get; init; }

        /// <summary>
        /// The scaling parameter s used (matrixA was scaled by 2^(-s)).
        /// </summary>
        public int ScalingParameter { get; init; }

        /// <summary>
        /// Estimated backward error.
        /// </summary>
        public double EstimatedError { get; init; }

        /// <summary>
        /// Number of matrix multiplications performed.
        /// </summary>
        public int MatrixMultiplications { get; init; }

        /// <summary>
        /// Properties of the input matrix that were detected.
        /// </summary>
        public MatrixProperties MatrixProperties { get; init; }
    }

    /// <summary>
    /// Coefficients for Padé approximants from Table 10.4 in Higham's book.
    /// </summary>
    private static class PadeCoefficients
    {
        public static readonly double[] B3 = [120, 60, 12, 1];
        public static readonly double[] B5 = [30240, 15120, 3360, 420, 30, 1];
        public static readonly double[] B7 = [17297280, 8648640, 1995840, 277200, 25200, 1512, 56, 1];
        public static readonly double[] B9 = [17643225600, 8821612800, 2075673600, 302702400, 30270240, 2162160, 110880, 3960, 90, 1];
        public static readonly double[] B13 = [64764752532480000, 32382376266240000, 7771770303897600, 1187353796428800, 129060195264000, 10559470521600, 670442572800, 33522128640, 1323241920, 40840800, 960960, 16380, 182, 1];
    }
    

    /// <summary>
    /// Maximal values θ_m of ||2^(-s)matrixA|| such that the backward error bound does not exceed u = 2^(-53).
    /// From Table 10.2 in Higham's book.
    /// </summary>
    private static readonly double[] ThetaM =
    [
        0.0,        // m=0 (unused)
        3.7e-8,     // m=1
        5.3e-4,     // m=2
        1.5e-2,     // m=3
        8.5e-2,     // m=4
        2.5e-1,     // m=5
        5.4e-1,     // m=6
        9.5e-1,     // m=7
        1.5e0,      // m=8
        2.1e0,      // m=9
        2.8e0,      // m=10
        3.6e0,      // m=11
        4.5e0,      // m=12
        5.4e0,      // m=13
        6.3e0,      // m=14
        7.3e0,      // m=15
        8.4e0,      // m=16
        9.4e0,      // m=17
        1.1e1,      // m=18
        1.2e1,      // m=19
        1.3e1,      // m=20
        1.4e1       // m=21
    ];

    /// <summary>
    /// Number of matrix multiplications π_m required to evaluate p_m(matrixA) and q_m(matrixA).
    /// From Table 10.3 in Higham's book.
    /// </summary>
    private static readonly int[] PiM =
    [
        0, // m=0 (unused)
        0, // m=1
        1, // m=2
        2, // m=3
        3, // m=4
        3, // m=5
        4, // m=6
        4, // m=7
        5, // m=8
        5, // m=9
        6, // m=10
        6, // m=11
        6, // m=12
        7, // m=13
        7, // m=14
        7, // m=15
        8, // m=16
        8, // m=17
        8, // m=18
        8, // m=19
        8, // m=20
        8  // m=21
    ];

    /// <summary>
    /// Computes the matrix exponential using Padé approximation with scaling and squaring.
    /// </summary>
    /// <param name="matrix">The input matrix</param>
    /// <param name="config">Configuration options</param>
    /// <returns>The computation result</returns>
    public static Result<double> MatrixExpViaPade(this Matrix<double> matrix, Config? config = null)
    {
        ArgumentNullException.ThrowIfNull(matrix);
        
        if (!matrix.IsSquare())
            throw new ArgumentException("Matrix must be square", nameof(matrix));

        config ??= new Config();
        
        // Analyze matrix properties
        var properties = MatrixPropertiesUtils.AnalyzeProperties(matrix, config.Tolerance);
        
        // Preprocessing if enabled
        var preprocessedMatrix = config.EnablePreprocessing ? 
            PreprocessMatrix(matrix, properties) : matrix.Clone();

        // Compute 1-norm for scaling decision
        var norm1 = preprocessedMatrix.OneNorm();

        // Determine optimal degree and scaling
        var (degree, scalingParam) = SelectOptimalParameters(norm1, config, properties);

        // Scale the matrix
        var scaledMatrix = scalingParam > 0 ? 
            ScaleMatrix(preprocessedMatrix, scalingParam) : preprocessedMatrix;

        // Compute Padé approximant
        var padeResult = EvaluatePadeApproximant(scaledMatrix, degree);

        // Repeated squaring
        var result = RepeatedSquaring(padeResult, scalingParam);

        // Estimate error
        var estimatedError = EstimateBackwardError(matrix, result, properties);

        return new Result<double>
        {
            MatrixExp = result,
            DegreeUsed = degree,
            ScalingParameter = scalingParam,
            EstimatedError = estimatedError,
            MatrixMultiplications = PiM[degree] + scalingParam,
            MatrixProperties = properties
        };
    }

    /// <summary>
    /// Computes the matrix exponential for complex matrices.
    /// </summary>
    public static Result<Complex> MatrixExpViaPade(this Matrix<Complex> matrix, Config? config = null)
    {
        ArgumentNullException.ThrowIfNull(matrix);
        
        if (!matrix.IsSquare())
            throw new ArgumentException("Matrix must be square", nameof(matrix));

        config ??= new Config();
        
        // Analyze matrix properties
        var properties = MatrixPropertiesUtils.AnalyzeProperties(matrix, config.Tolerance);
        
        // For complex matrices, we use similar logic but with complex arithmetic
        var preprocessedMatrix = matrix.Clone();

        // Compute 1-norm for scaling decision (using magnitude)
        var norm1 = ComputeComplexOneNorm(preprocessedMatrix);

        // Determine optimal degree and scaling
        var (degree, scalingParam) = SelectOptimalParameters(norm1, config, properties);

        // Scale the matrix
        var scaledMatrix = scalingParam > 0 ? 
            ScaleMatrixComplex(preprocessedMatrix, scalingParam) : preprocessedMatrix;

        // Compute Padé approximant
        var padeResult = EvaluatePadeApproximantComplex(scaledMatrix, degree);

        // Repeated squaring
        var result = RepeatedSquaringComplex(padeResult, scalingParam);

        return new Result<Complex>
        {
            MatrixExp = result,
            DegreeUsed = degree,
            ScalingParameter = scalingParam,
            EstimatedError = 0.0, // Complex error estimation not implemented
            MatrixMultiplications = PiM[degree] + scalingParam,
            MatrixProperties = properties
        };
    }

    /// <summary>
    /// Selects optimal Padé degree and scaling parameter based on matrix norm and properties.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static (int degree, int scaling) SelectOptimalParameters(double norm1, Config config, MatrixProperties properties = MatrixProperties.None)
    {
        // For nilpotent matrices, use lower degrees to avoid numerical instability
        if (properties.HasFlag(MatrixProperties.Nilpotent))
        {
            // Nilpotent matrices work best with lower-degree Padé approximants
            // because higher powers are zero, making high-degree approximants numerically unstable
            int[] nilpotentDegrees = [3, 5];
            
            foreach (var m in nilpotentDegrees)
            {
                if (m > config.MaxDegree) break;
                
                if (norm1 <= ThetaM[m])
                {
                    return (m, 0);
                }
            }
            
            // If norm is too large, use scaling with degree 5 (best for nilpotent)
            var nilpotentDegree = Math.Min(5, config.MaxDegree);
            var nilpotentScaling = ComputeOptimalScaling(norm1, ThetaM[nilpotentDegree]);
            return (nilpotentDegree, nilpotentScaling);
        }
        
        // For very high norm matrices, use conservative scaling to prevent overflow
        if (norm1 > 100.0)
        {
            // Use degree 5 or 7 for stability with aggressive scaling
            var conservativeDegree = Math.Min(7, config.MaxDegree);
            var conservativeScaling = ComputeOptimalScaling(norm1, ThetaM[conservativeDegree]);
            return (conservativeDegree, conservativeScaling);
        }
        
        // Try degrees 3, 5, 7, 9, 13 in order (as suggested in Algorithm 10.20)
        int[] candidateDegrees = [3, 5, 7, 9, 13];
        
        foreach (var m in candidateDegrees)
        {
            if (m > config.MaxDegree) break;
            
            if (norm1 <= ThetaM[m])
            {
                return (m, 0);
            }
        }

        // Use degree 13 with improved scaling
        var degree = Math.Min(13, config.MaxDegree);
        var s = ComputeOptimalScaling(norm1, ThetaM[degree]);
        return (degree, s);
    }

    /// <summary>
    /// Computes optimal scaling parameter with improved numerical stability.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int ComputeOptimalScaling(double norm1, double theta)
    {
        if (norm1 <= theta) return 0;
        
        // Use more conservative scaling for better numerical stability
        var ratio = norm1 / theta;
        var s = (int)Math.Ceiling(Math.Log2(ratio));
        
        // Special handling for the problematic norm range 40-60
        if (norm1 >= 40.0 && norm1 <= 60.0)
        {
            // Use more aggressive scaling for this problematic range
            s = Math.Max(s + 3, 6); // Ensure at least 6 scaling steps
        }
        // Add extra scaling for very large norms to prevent numerical overflow
        else if (norm1 > 50.0)
        {
            s += 2; // Add 2 extra scaling steps for high-norm matrices
        }
        else if (norm1 > 10.0)
        {
            s += 1; // Add 1 extra scaling step for medium-high norm matrices
        }
        
        return Math.Max(s, 0);
    }

    /// <summary>
    /// Scales the matrix by 2^(-s).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Matrix<double> ScaleMatrix(Matrix<double> matrix, int s)
    {
        var scale = Math.Pow(2.0, -s);
        return scale * matrix;
    }

    /// <summary>
    /// Scales the complex matrix by 2^(-s).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Matrix<Complex> ScaleMatrixComplex(Matrix<Complex> matrix, int s)
    {
        var scale = Math.Pow(2.0, -s);
        var scaleComplex = new Complex(scale, 0);
        return scaleComplex * matrix;
    }

    /// <summary>
    /// Evaluates the Padé approximant r_m(matrixA) = p_m(matrixA) / q_m(matrixA).
    /// </summary>
    private static Matrix<double> EvaluatePadeApproximant(Matrix<double> matrixA, int m)
    {
        return m switch
        {
            3 => EvaluatePade3(matrixA),
            5 => EvaluatePade5(matrixA),
            7 => EvaluatePade7(matrixA),
            9 => EvaluatePade9(matrixA),
            13 => EvaluatePade13(matrixA),
            _ => throw new ArgumentException($"Padé degree {m} not supported")
        };
    }

    /// <summary>
    /// Evaluates the Padé approximant for complex matrices.
    /// </summary>
    private static Matrix<Complex> EvaluatePadeApproximantComplex(Matrix<Complex> matrixA, int m)
    {
        return m switch
        {
            3 => EvaluatePade3Complex(matrixA),
            5 => EvaluatePade5Complex(matrixA),
            7 => EvaluatePade7Complex(matrixA),
            9 => EvaluatePade9Complex(matrixA),
            13 => EvaluatePade13Complex(matrixA),
            _ => throw new ArgumentException($"Padé degree {m} not supported")
        };
    }

    /// <summary>
    /// Evaluates the [3/3] Padé approximant to exp(matrixA).
    /// </summary>
    private static Matrix<double> EvaluatePade3(Matrix<double> matrixA)
    {
        var n = matrixA.Size();
        var I = Matrix<double>.Build.DenseIdentity(n);
        var matrixA2 = matrixA * matrixA;

        // Coefficients normalized so that b[3] = 1
        double[] b = [120, 60, 12, 1];

        // matrixU = matrixA * (b[3]*matrixA^2 + b[1]*I)
        var matrixU = matrixA * (b[3] * matrixA2 + b[1] * I);

        // matrixV = b[2]*matrixA^2 + b[0]*I
        var matrixV = b[2] * matrixA2 + b[0] * I;

        // Solve (-matrixU + matrixV) * R = (matrixU + matrixV)
        return SolveLinearSystem(-matrixU + matrixV, matrixU + matrixV);
    }

    /// <summary>
    /// Evaluates the [5/5] Padé approximant to exp(matrixA).
    /// </summary>
    private static Matrix<double> EvaluatePade5(Matrix<double> matrixA)
    {
        var n = matrixA.Size();
        var I = Matrix<double>.Build.DenseIdentity(n);
        var matrixA2 = matrixA * matrixA;
        var matrixA4 = matrixA2 * matrixA2;

        double[] b = [30240, 15120, 3360, 420, 30, 1];

        var matrixU = matrixA * (b[5] * matrixA4 + b[3] * matrixA2 + b[1] * I);
        var matrixV = b[4] * matrixA4 + b[2] * matrixA2 + b[0] * I;

        return SolveLinearSystem(-matrixU + matrixV, matrixU + matrixV);
    }

    /// <summary>
    /// Evaluates the [7/7] Padé approximant to exp(matrixA).
    /// </summary>
    private static Matrix<double> EvaluatePade7(Matrix<double> matrixA)
    {
        var n = matrixA.Size();
        var I = Matrix<double>.Build.DenseIdentity(n);
        var matrixA2 = matrixA * matrixA;
        var matrixA4 = matrixA2 * matrixA2;
        var matrixA6 = matrixA2 * matrixA4;

        double[] b = [17297280, 8648640, 1995840, 277200, 25200, 1512, 56, 1];

        var matrixU = matrixA * (matrixA6 * (b[7] * matrixA6 + b[5] * matrixA4 + b[3] * matrixA2 + b[1] * I));
        var matrixV = matrixA6 * (b[6] * matrixA6 + b[4] * matrixA4 + b[2] * matrixA2) + b[0] * I;

        return SolveLinearSystem(-matrixU + matrixV, matrixU + matrixV);
    }

    /// <summary>
    /// Evaluates the [9/9] Padé approximant to exp(matrixA).
    /// </summary>
    private static Matrix<double> EvaluatePade9(Matrix<double> matrixA)
    {
        var n = matrixA.Size();
        var I = Matrix<double>.Build.DenseIdentity(n);
        var matrixA2 = matrixA * matrixA;
        var matrixA4 = matrixA2 * matrixA2;
        var matrixA6 = matrixA2 * matrixA4;
        var matrixA8 = matrixA4 * matrixA4;

        double[] b = [17643225600, 8821612800, 2075673600, 302702400, 30270240, 2162160, 110880, 3960, 90, 1];

        var matrixU = matrixA * (matrixA8 * (b[9] * matrixA8 + b[7] * matrixA6 + b[5] * matrixA4 + b[3] * matrixA2 + b[1] * I));
        var matrixV = matrixA8 * (b[8] * matrixA8 + b[6] * matrixA6 + b[4] * matrixA4 + b[2] * matrixA2) + b[0] * I;

        return SolveLinearSystem(-matrixU + matrixV, matrixU + matrixV);
    }

    /// <summary>
    /// Evaluates the [13/13] Padé approximant to exp(matrixA) using the efficient scheme from (10.34).
    /// </summary>
    private static Matrix<double> EvaluatePade13(Matrix<double> matrixA)
    {
        var n = matrixA.Size();
        var I = Matrix<double>.Build.DenseIdentity(n);
        var matrixA2 = matrixA * matrixA;
        var matrixA4 = matrixA2 * matrixA2;
        var matrixA6 = matrixA2 * matrixA4;

        var b = PadeCoefficients.B13;

        // Using the efficient scheme from equation (10.34)
        var matrixU = matrixA * (matrixA6 * (b[13] * matrixA6 + b[11] * matrixA4 + b[9] * matrixA2 + b[7] * I) + 
                     b[5] * matrixA4 + b[3] * matrixA2 + b[1] * I);
        
        var matrixV = matrixA6 * (b[12] * matrixA6 + b[10] * matrixA4 + b[8] * matrixA2 + b[6] * I) + 
                b[4] * matrixA4 + b[2] * matrixA2 + b[0] * I;

        return SolveLinearSystem(-matrixU + matrixV, matrixU + matrixV);
    }

    /// <summary>
    /// Complex versions of Padé evaluators.
    /// </summary>
    private static Matrix<Complex> EvaluatePade3Complex(Matrix<Complex> matrixA)
    {
        var n = matrixA.Size();
        var I = Matrix<Complex>.Build.DenseIdentity(n);
        var matrixA2 = matrixA * matrixA;

        var b = Array.ConvertAll(PadeCoefficients.B3, x => new Complex(x, 0));

        var matrixU = matrixA * (b[3] * matrixA2 + b[1] * I);
        var matrixV = b[2] * matrixA2 + b[0] * I;

        return SolveLinearSystemComplex(-matrixU + matrixV, matrixU + matrixV);
    }

    private static Matrix<Complex> EvaluatePade5Complex(Matrix<Complex> matrixA)
    {
        var n = matrixA.Size();
        var I = Matrix<Complex>.Build.DenseIdentity(n);
        var matrixA2 = matrixA * matrixA;
        var matrixA4 = matrixA2 * matrixA2;

        var b = Array.ConvertAll(PadeCoefficients.B5, x => new Complex(x, 0));

        var matrixU = matrixA * (b[5] * matrixA4 + b[3] * matrixA2 + b[1] * I);
        var matrixV = b[4] * matrixA4 + b[2] * matrixA2 + b[0] * I;

        return SolveLinearSystemComplex(-matrixU + matrixV, matrixU + matrixV);
    }

    private static Matrix<Complex> EvaluatePade7Complex(Matrix<Complex> matrixA)
    {
        var n = matrixA.Size();
        var I = Matrix<Complex>.Build.DenseIdentity(n);
        var matrixA2 = matrixA * matrixA;
        var matrixA4 = matrixA2 * matrixA2;
        var matrixA6 = matrixA2 * matrixA4;

        var b = Array.ConvertAll(PadeCoefficients.B7, x => new Complex(x, 0));

        var matrixU = matrixA * (matrixA6 * (b[7] * matrixA6 + b[5] * matrixA4 + b[3] * matrixA2 + b[1] * I));
        var matrixV = matrixA6 * (b[6] * matrixA6 + b[4] * matrixA4 + b[2] * matrixA2) + b[0] * I;

        return SolveLinearSystemComplex(-matrixU + matrixV, matrixU + matrixV);
    }

    private static Matrix<Complex> EvaluatePade9Complex(Matrix<Complex> matrixA)
    {
        var n = matrixA.Size();
        var I = Matrix<Complex>.Build.DenseIdentity(n);
        var matrixA2 = matrixA * matrixA;
        var matrixA4 = matrixA2 * matrixA2;
        var matrixA6 = matrixA2 * matrixA4;
        var matrixA8 = matrixA4 * matrixA4;

        var b = Array.ConvertAll(PadeCoefficients.B9, x => new Complex(x, 0));

        var matrixU = matrixA * (matrixA8 * (b[9] * matrixA8 + b[7] * matrixA6 + b[5] * matrixA4 + b[3] * matrixA2 + b[1] * I));
        var matrixV = matrixA8 * (b[8] * matrixA8 + b[6] * matrixA6 + b[4] * matrixA4 + b[2] * matrixA2) + b[0] * I;

        return SolveLinearSystemComplex(-matrixU + matrixV, matrixU + matrixV);
    }

    private static Matrix<Complex> EvaluatePade13Complex(Matrix<Complex> matrixA)
    {
        var n = matrixA.Size();
        var I = Matrix<Complex>.Build.DenseIdentity(n);
        var matrixA2 = matrixA * matrixA;
        var matrixA4 = matrixA2 * matrixA2;
        var matrixA6 = matrixA2 * matrixA4;

        var b = Array.ConvertAll(PadeCoefficients.B13, x => new Complex(x, 0));

        var matrixU = matrixA * (matrixA6 * (b[13] * matrixA6 + b[11] * matrixA4 + b[9] * matrixA2 + b[7] * I) + 
                     b[5] * matrixA4 + b[3] * matrixA2 + b[1] * I);
        
        var matrixV = matrixA6 * (b[12] * matrixA6 + b[10] * matrixA4 + b[8] * matrixA2 + b[6] * I) + 
                b[4] * matrixA4 + b[2] * matrixA2 + b[0] * I;

        return SolveLinearSystemComplex(-matrixU + matrixV, matrixU + matrixV);
    }

    /// <summary>
    /// Performs repeated squaring to compute matrixX^(2^s).
    /// </summary>
    private static Matrix<double> RepeatedSquaring(Matrix<double> matrixX, int s)
    {
        var result = matrixX.Clone();
        for (var i = 0; i < s; i++)
        {
            result *= result;
        }
        return result;
    }

    /// <summary>
    /// Performs repeated squaring for complex matrices.
    /// </summary>
    private static Matrix<Complex> RepeatedSquaringComplex(Matrix<Complex> matrixX, int s)
    {
        var result = matrixX.Clone();
        for (var i = 0; i < s; i++)
        {
            result *= result;
        }
        return result;
    }

    /// <summary>
    /// Solves the linear system AX = matrixB for matrixX.
    /// This is a simplified implementation - in practice, you'd use LU decomposition.
    /// </summary>
    private static Matrix<double> SolveLinearSystem(Matrix<double> matrixA, Matrix<double> matrixB)
    {
        // For now, we'll use a simple implementation
        // In a full implementation, this should use optimized LAPACK routines
        return InvertMatrix(matrixA) * matrixB;
    }

    /// <summary>
    /// Solves the linear system for complex matrices.
    /// </summary>
    private static Matrix<Complex> SolveLinearSystemComplex(Matrix<Complex> matrixA, Matrix<Complex> matrixB)
    {
        // Simplified implementation
        return InvertMatrixComplex(matrixA) * matrixB;
    }

    /// <summary>
    /// Computes matrix inverse with improved numerical stability.
    /// </summary>
    private static Matrix<double> InvertMatrix(Matrix<double> matrix)
    {
        var n = matrix.Size();
        var augmented = Matrix<double>.Build.Dense(n, 2 * n);
        const double tolerance = Float64Utils.DefaultTolerance;
        
        // Create augmented matrix [matrixA | I]
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                augmented[i, j] = matrix[i, j];
                augmented[i, j + n] = i == j ? 1.0 : 0.0;
            }
        }
        
        // Gauss-Jordan elimination with partial pivoting and tolerance checking
        for (var i = 0; i < n; i++)
        {
            // Find pivot with better strategy for numerical stability
            var pivot = i;
            var maxVal = Math.Abs(augmented[i, i]);
            
            for (var k = i + 1; k < n; k++)
            {
                var val = Math.Abs(augmented[k, i]);
                if (val > maxVal)
                {
                    maxVal = val;
                    pivot = k;
                }
            }
            
            // Check for singularity
            if (Math.Abs(augmented[pivot, i]) < tolerance)
            {
                // Try to find a non-zero pivot in any column from current position
                var foundPivot = false;
                for (var col = i + 1; col < n && !foundPivot; col++)
                {
                    for (var row = i; row < n; row++)
                    {
                        if (Math.Abs(augmented[row, col]) >= tolerance)
                        {
                            // Swap columns in original matrix and identity parts
                            for (var r = 0; r < n; r++)
                            {
                                (augmented[r, i], augmented[r, col]) = (augmented[r, col], augmented[r, i]);
                                (augmented[r, i + n], augmented[r, col + n]) = (augmented[r, col + n], augmented[r, i + n]);
                            }
                            pivot = row;
                            foundPivot = true;
                            break;
                        }
                    }
                }
                
                if (!foundPivot)
                {
                    throw new InvalidOperationException($"Matrix is singular or numerically singular at row {i}");
                }
            }
            
            // Swap rows if needed
            if (pivot != i)
            {
                for (var j = 0; j < 2 * n; j++)
                {
                    (augmented[i, j], augmented[pivot, j]) = (augmented[pivot, j], augmented[i, j]);
                }
            }
            
            // Scale pivot row
            var pivotVal = augmented[i, i];
            if (Math.Abs(pivotVal) < tolerance)
            {
                throw new InvalidOperationException($"Matrix is singular: pivot value {pivotVal} too small");
            }
            
            for (var j = 0; j < 2 * n; j++)
            {
                augmented[i, j] /= pivotVal;
            }
            
            // Eliminate column
            for (var k = 0; k < n; k++)
            {
                if (k != i)
                {
                    var factor = augmented[k, i];
                    for (var j = 0; j < 2 * n; j++)
                    {
                        augmented[k, j] -= factor * augmented[i, j];
                    }
                }
            }
        }
        
        // Extract inverse from right half
        var result = Matrix<double>.Build.Dense(n, n);
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                result[i, j] = augmented[i, j + n];
            }
        }
        
        return result;
    }

    /// <summary>
    /// Computes complex matrix inverse (simplified implementation).
    /// </summary>
    private static Matrix<Complex> InvertMatrixComplex(Matrix<Complex> matrix)
    {
        // Simplified implementation for complex matrices
        var n = matrix.Size();
        var augmented = Matrix<Complex>.Build.Dense(n, 2 * n);
        
        // Create augmented matrix [matrixA | I]
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                augmented[i, j] = matrix[i, j];
                augmented[i, j + n] = i == j ? Complex.One : Complex.Zero;
            }
        }
        
        // Gauss-Jordan elimination with complex numbers
        for (var i = 0; i < n; i++)
        {
            // Find pivot
            var pivot = i;
            for (var k = i + 1; k < n; k++)
            {
                if (augmented[k, i].Magnitude > augmented[pivot, i].Magnitude)
                    pivot = k;
            }
            
            // Swap rows
            if (pivot != i)
            {
                for (var j = 0; j < 2 * n; j++)
                {
                    (augmented[i, j], augmented[pivot, j]) = (augmented[pivot, j], augmented[i, j]);
                }
            }
            
            // Scale pivot row
            var pivotVal = augmented[i, i];
            for (var j = 0; j < 2 * n; j++)
            {
                augmented[i, j] /= pivotVal;
            }
            
            // Eliminate column
            for (var k = 0; k < n; k++)
            {
                if (k != i)
                {
                    var factor = augmented[k, i];
                    for (var j = 0; j < 2 * n; j++)
                    {
                        augmented[k, j] -= factor * augmented[i, j];
                    }
                }
            }
        }
        
        // Extract inverse from right half
        var result = Matrix<Complex>.Build.Dense(n, n);
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                result[i, j] = augmented[i, j + n];
            }
        }
        
        return result;
    }

    /// <summary>
    /// Preprocesses the matrix (balancing, shifting).
    /// </summary>
    private static Matrix<double> PreprocessMatrix(Matrix<double> matrix, MatrixProperties properties)
    {
        var n = matrix.Size();

        // Simple preprocessing: argument reduction via shifting
        if (!properties.HasFlag(MatrixProperties.Symmetric)) 
            return matrix.Clone();

        // For symmetric matrices, shift by average diagonal element
        var trace = 0d;
        for (var i = 0; i < n; i++) 
            trace += matrix[i, i];
        var shift = trace / n;
            
        var result = matrix.Clone();
        for (var i = 0; i < n; i++) 
            result[i, i] -= shift;
        return result;

    }

    /// <summary>
    /// Computes the 1-norm for complex matrices.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double ComputeComplexOneNorm(Matrix<Complex> matrix)
    {
        double maxColSum = 0;
        for (var j = 0; j < matrix.ColumnCount; j++)
        {
            double colSum = 0;
            for (var i = 0; i < matrix.RowCount; i++)
            {
                colSum += matrix[i, j].Magnitude;
            }
            maxColSum = Math.Max(maxColSum, colSum);
        }
        return maxColSum;
    }

    /// <summary>
    /// Estimates the backward error of the computed exponential.
    /// </summary>
    private static double EstimateBackwardError(Matrix<double> matrixA, Matrix<double> expA, MatrixProperties properties)
    {
        // For now, return a simple estimate based on machine precision
        // In a full implementation, this would use the theoretical bounds from the paper
        var machineEps = 2.22e-16; // IEEE 754 double precision
        var normA = matrixA.OneNorm();
        
        if (properties.HasFlag(MatrixProperties.Normal))
        {
            return normA * machineEps;
        }
        
        return normA * normA * machineEps;
    }
}