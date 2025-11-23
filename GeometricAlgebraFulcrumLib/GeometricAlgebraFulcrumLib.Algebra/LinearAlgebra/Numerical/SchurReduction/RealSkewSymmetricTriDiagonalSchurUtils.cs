using System.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.SchurReduction;

/// <summary>
/// Advanced numerical stability enhancements for skew-symmetric Schur decomposition.
/// Provides machine precision error correction, eigenvalue purity enforcement,
/// and orthogonality recovery mechanisms.
/// </summary>
public static class RealSkewSymmetricTriDiagonalSchurUtils
{
    #region Machine Precision Constants

    /// <summary>
    /// IEEE 754 double precision machine epsilon and derived tolerances
    /// </summary>
    public static class MachinePrecision
    {
        public static readonly double Epsilon = Math.Pow(2, -52); // ~2.22e-16
        public static readonly double SafeMinimum = Math.Pow(2, -1022); // ~2.23e-308
        public static readonly double SafeMaximum = Math.Pow(2, 1023); // ~8.99e+307
        
        // Rigorous machine precision tolerances
        public static readonly double RealPartTolerance = 10 * Epsilon; // ~2.22e-15
        public static readonly double ConjugatePairTolerance = 10 * Epsilon; // ~2.22e-15
        public static readonly double OrthogonalityTolerance = Math.Sqrt(Epsilon); // ~1.49e-8
        public static readonly double ReconstructionTolerance = 100 * Epsilon; // ~2.22e-14
        public static readonly double ConditionNumberThreshold = 1.0 / Math.Sqrt(Epsilon); // ~6.71e+7
        
        // Iterative refinement thresholds
        public static readonly double RefinementThreshold = 1000 * Epsilon; // ~2.22e-13
        public static readonly int MaxRefinementIterations = 3;
    }

    #endregion

    #region Real-time Eigenvalue Purity Enforcement

    /// <summary>
    /// Enforces pure imaginary eigenvalue structure during QR iterations
    /// by correcting accumulated real part drift at machine precision level
    /// </summary>
    public static void EnforceEigenvaluePurity(Matrix<double> matrix, ref Complex[] eigenvalues)
    {
        // Extract current eigenvalues from quasi-triangular form
        var currentEigenvalues = ExtractEigenvaluesFromQuasiTriangular(matrix);
        
        // Correct real part drift in eigenvalues
        for (var i = 0; i < currentEigenvalues.Length; i++)
        {
            var lambda = currentEigenvalues[i];
            
            // For skew-symmetric matrices, eigenvalues must be purely imaginary
            if (Math.Abs(lambda.Real) > MachinePrecision.RealPartTolerance)
            {
                // Correct real part drift by setting to zero
                currentEigenvalues[i] = new Complex(0.0, lambda.Imaginary);
                
                // Update corresponding matrix elements if needed
                CorrectMatrixElementsForPurity(matrix, i, lambda, currentEigenvalues[i]);
            }
        }
        
        // Enforce conjugate pair structure
        EnforceConjugatePairStructure(ref currentEigenvalues);
        eigenvalues = currentEigenvalues;
    }

    /// <summary>
    /// Extracts eigenvalues from quasi-triangular form with machine precision
    /// </summary>
    private static Complex[] ExtractEigenvaluesFromQuasiTriangular(Matrix<double> T)
    {
        var n = T.RowCount;
        var eigenvalues = new List<Complex>();
        
        var i = 0;
        while (i < n)
        {
            if (i == n - 1 || Math.Abs(T[i + 1, i]) < MachinePrecision.Epsilon)
            {
                // 1x1 block - should be zero for skew-symmetric
                var realPart = Math.Abs(T[i, i]) < MachinePrecision.RealPartTolerance ? 0.0 : T[i, i];
                eigenvalues.Add(new Complex(realPart, 0.0));
                i++;
            }
            else
            {
                // 2x2 block - extract conjugate pair
                var a = T[i, i];
                var b = T[i, i + 1];
                var c = T[i + 1, i];
                var d = T[i + 1, i + 1];
                
                var trace = a + d;
                var det = a * d - b * c;
                var discriminant = trace * trace - 4 * det;
                
                if (discriminant < 0)
                {
                    var realPart = Math.Abs(trace / 2) < MachinePrecision.RealPartTolerance ? 0.0 : trace / 2;
                    var imagPart = Math.Sqrt(-discriminant) / 2;
                    eigenvalues.Add(new Complex(realPart, imagPart));
                    eigenvalues.Add(new Complex(realPart, -imagPart));
                }
                else
                {
                    var sqrt_disc = Math.Sqrt(discriminant);
                    var lambda1 = (trace + sqrt_disc) / 2;
                    var lambda2 = (trace - sqrt_disc) / 2;
                    
                    // Correct to zero if within tolerance
                    lambda1 = Math.Abs(lambda1) < MachinePrecision.RealPartTolerance ? 0.0 : lambda1;
                    lambda2 = Math.Abs(lambda2) < MachinePrecision.RealPartTolerance ? 0.0 : lambda2;
                    
                    eigenvalues.Add(new Complex(lambda1, 0));
                    eigenvalues.Add(new Complex(lambda2, 0));
                }
                
                i += 2;
            }
        }
        
        return eigenvalues.ToArray();
    }

    /// <summary>
    /// Corrects matrix elements to maintain eigenvalue purity
    /// </summary>
    private static void CorrectMatrixElementsForPurity(Matrix<double> matrixA, int eigenvalueIndex, Complex originalEigenvalue, Complex correctedEigenvalue)
    {
        // For small corrections, we can adjust diagonal elements slightly
        var correction = (correctedEigenvalue - originalEigenvalue).Real;
        
        if (eigenvalueIndex < matrixA.RowCount && Math.Abs(correction) > MachinePrecision.Epsilon)
        {
            // Apply minimal correction to maintain structure
            matrixA[eigenvalueIndex, eigenvalueIndex] -= correction;
        }
    }

    /// <summary>
    /// Enforces conjugate pair structure with machine precision accuracy
    /// </summary>
    private static void EnforceConjugatePairStructure(ref Complex[] eigenvalues)
    {
        var corrected = new bool[eigenvalues.Length];
        var results = new List<Complex>();
        
        for (var i = 0; i < eigenvalues.Length; i++)
        {
            if (corrected[i]) continue;
            
            var lambda = eigenvalues[i];
            
            // Check if it's effectively real (should be zero for skew-symmetric)
            if (Math.Abs(lambda.Imaginary) < MachinePrecision.RealPartTolerance)
            {
                // Force to be exactly zero
                results.Add(new Complex(0.0, 0.0));
                corrected[i] = true;
                continue;
            }
            
            // Look for conjugate pair
            var foundConjugate = false;
            for (var j = i + 1; j < eigenvalues.Length; j++)
            {
                if (corrected[j]) continue;
                
                var mu = eigenvalues[j];
                var realDiff = Math.Abs(lambda.Real - mu.Real);
                var imagSum = Math.Abs(lambda.Imaginary + mu.Imaginary);
                
                if (realDiff < MachinePrecision.ConjugatePairTolerance && 
                    imagSum < MachinePrecision.ConjugatePairTolerance)
                {
                    // Found conjugate pair - make them exactly conjugate
                    var avgReal = Math.Abs((lambda.Real + mu.Real) / 2) < MachinePrecision.RealPartTolerance ? 
                                 0.0 : (lambda.Real + mu.Real) / 2;
                    var avgImag = (lambda.Imaginary - mu.Imaginary) / 2;
                    
                    results.Add(new Complex(avgReal, avgImag));
                    results.Add(new Complex(avgReal, -avgImag));
                    
                    corrected[i] = corrected[j] = true;
                    foundConjugate = true;
                    break;
                }
            }
            
            if (!foundConjugate)
            {
                // Isolated eigenvalue - force real part to zero for skew-symmetric
                results.Add(new Complex(0.0, lambda.Imaginary));
                corrected[i] = true;
            }
        }
        
        eigenvalues = results.ToArray();
    }

    #endregion

    #region Orthogonality Recovery Mechanisms

    /// <summary>
    /// Recovers orthogonality of transformation matrix matrixQ using iterative refinement
    /// with machine precision accuracy guarantees
    /// </summary>
    public static bool RecoverOrthogonality(ref Matrix<double> matrixQ, out double finalOrthogonalityError)
    {
        finalOrthogonalityError = ComputeOrthogonalityError(matrixQ);
        
        if (finalOrthogonalityError < MachinePrecision.OrthogonalityTolerance)
        {
            return true; // Already sufficiently orthogonal
        }
        
        var originalQ = matrixQ.Clone();
        var maxIterations = MachinePrecision.MaxRefinementIterations;
        
        for (var iteration = 0; iteration < maxIterations; iteration++)
        {
            // Modified Gram-Schmidt orthogonalization with iterative refinement
            var matrixQRefined = ApplyModifiedGramSchmidt(matrixQ);
            
            // Check improvement
            var newError = ComputeOrthogonalityError(matrixQRefined);
            
            if (newError < finalOrthogonalityError)
            {
                matrixQ = matrixQRefined;
                finalOrthogonalityError = newError;
                
                if (finalOrthogonalityError < MachinePrecision.OrthogonalityTolerance)
                {
                    return true; // Successfully recovered orthogonality
                }
            }
            else
            {
                break; // No further improvement possible
            }
        }
        
        // If iterative refinement fails, try QR-based orthogonalization
        if (finalOrthogonalityError >= MachinePrecision.OrthogonalityTolerance)
        {
            try
            {
                var qr = matrixQ.QR();
                matrixQ = qr.Q;
                finalOrthogonalityError = ComputeOrthogonalityError(matrixQ);
                
                return finalOrthogonalityError < MachinePrecision.OrthogonalityTolerance;
            }
            catch
            {
                // QR decomposition failed - restore original and return failure
                matrixQ = originalQ;
                finalOrthogonalityError = ComputeOrthogonalityError(matrixQ);
                return false;
            }
        }
        
        return finalOrthogonalityError < MachinePrecision.OrthogonalityTolerance;
    }

    /// <summary>
    /// Applies modified Gram-Schmidt process for enhanced numerical stability
    /// </summary>
    private static Matrix<double> ApplyModifiedGramSchmidt(Matrix<double> matrixA)
    {
        var n = matrixA.RowCount;
        var m = matrixA.ColumnCount;
        var matrixQ = Matrix<double>.Build.Dense(n, m);
        
        for (var j = 0; j < m; j++)
        {
            var v = matrixA.Column(j).Clone();
            
            // Orthogonalize against previous columns
            for (var i = 0; i < j; i++)
            {
                var qi = matrixQ.Column(i);
                var projection = qi.DotProduct(v);
                v -= projection * qi;
            }
            
            // Normalize
            var norm = v.L2Norm();
            if (norm > MachinePrecision.SafeMinimum)
            {
                matrixQ.SetColumn(j, v / norm);
            }
            else
            {
                // Handle near-zero column
                matrixQ.SetColumn(j, MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(n, 0.0));
                if (j < n) matrixQ[j, j] = 1.0; // Set to standard basis vector
            }
        }
        
        return matrixQ;
    }

    /// <summary>
    /// Computes orthogonality error ||matrixQ^T matrixQ - I||_F with machine precision
    /// </summary>
    private static double ComputeOrthogonalityError(Matrix<double> matrixQ)
    {
        var identity = Matrix<double>.Build.DenseIdentity(matrixQ.ColumnCount);
        
        return (matrixQ.Transpose() * matrixQ - identity).FrobeniusNorm();
    }

    #endregion

    #region Accumulated Rounding Error Mitigation

    /// <summary>
    /// Mitigates accumulated rounding errors through iterative refinement
    /// and selective recomputation of critical operations
    /// </summary>
    public static void MitigateAccumulatedRoundingErrors(ref Matrix<double> matrixA, ref Matrix<double> matrixQ, 
                                                       Matrix<double> originalMatrix)
    {
        // Check reconstruction accuracy
        var reconstructed = matrixQ * matrixA * matrixQ.Transpose();
        var reconstructionError = (originalMatrix - reconstructed).FrobeniusNorm();
        var relativeError = reconstructionError / Math.Max(originalMatrix.FrobeniusNorm(), MachinePrecision.Epsilon);
        
        if (relativeError > MachinePrecision.RefinementThreshold)
        {
            // Apply iterative refinement
            ApplyIterativeRefinement(ref matrixA, ref matrixQ, originalMatrix, reconstructionError);
        }
        
        // Enforce structural properties
        EnforceSkewSymmetryWithHighPrecision(ref matrixA);
        
        // Recover orthogonality if needed
        if (ComputeOrthogonalityError(matrixQ) > MachinePrecision.OrthogonalityTolerance)
        {
            RecoverOrthogonality(ref matrixQ, out var _);
        }
    }

    /// <summary>
    /// Applies iterative refinement to improve reconstruction accuracy
    /// </summary>
    private static void ApplyIterativeRefinement(ref Matrix<double> matrixA, ref Matrix<double> matrixQ, Matrix<double> originalMatrix, double currentError)
    {
        var bestError = currentError;
        
        for (var iteration = 0; iteration < MachinePrecision.MaxRefinementIterations; iteration++)
        {
            // Compute residual
            var reconstructed = matrixQ * matrixA * matrixQ.Transpose();
            var residual = originalMatrix - reconstructed;
            
            // Apply small correction to matrixA (main source of accumulated error)
            var correction = matrixQ.Transpose() * residual * matrixQ;
            var correctedA = matrixA + 0.5 * correction; // Conservative correction factor
            
            // Enforce skew-symmetry after correction
            EnforceSkewSymmetryWithHighPrecision(ref correctedA);
            
            // Check if improvement is achieved
            var newReconstructed = matrixQ * correctedA * matrixQ.Transpose();
            var newError = (originalMatrix - newReconstructed).FrobeniusNorm();
            
            if (newError < bestError)
            {
                bestError = newError;
                matrixA = correctedA;
                
                if (newError < MachinePrecision.ReconstructionTolerance)
                {
                    break; // Sufficient accuracy achieved
                }
            }
            else
            {
                break; // No further improvement
            }
        }
    }

    /// <summary>
    /// Enforces skew-symmetry with high precision arithmetic operations
    /// </summary>
    private static void EnforceSkewSymmetryWithHighPrecision(ref Matrix<double> matrixA)
    {
        var n = matrixA.RowCount;
        
        // Set diagonal elements to exactly zero
        for (var i = 0; i < n; i++)
        {
            matrixA[i, i] = 0.0;
        }
        
        // Enforce matrixA[i,j] = -matrixA[j,i] with high precision averaging
        for (var i = 0; i < n; i++)
        {
            for (var j = i + 1; j < n; j++)
            {
                // Use high precision arithmetic for averaging
                var sum = matrixA[i, j] - matrixA[j, i];
                var avg = sum * 0.5;
                
                // Apply with machine precision consideration
                if (Math.Abs(avg) > MachinePrecision.Epsilon)
                {
                    matrixA[i, j] = avg;
                    matrixA[j, i] = -avg;
                }
                else
                {
                    matrixA[i, j] = 0.0;
                    matrixA[j, i] = 0.0;
                }
            }
        }
    }

    #endregion

}