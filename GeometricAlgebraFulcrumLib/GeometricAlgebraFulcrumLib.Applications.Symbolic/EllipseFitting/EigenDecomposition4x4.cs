using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.EllipseFitting;

/// <summary>
/// A comprehensive static class that implements optimized eigenvalue decomposition for 4x4 real symmetric matrices 
/// using a two-stage approach: Householder tridiagonalization followed by QR algorithm with Wilkinson shifts.
/// </summary>
/// <remarks>
/// This implementation uses advanced numerical algorithms for superior stability and performance:
/// 1. First stage: Householder transformations to reduce the matrix to symmetric tridiagonal form
/// 2. Second stage: QR algorithm with implicit Wilkinson shifts for rapid convergence
/// 3. Deflation techniques to isolate converged eigenvalues
/// 4. Numerical stability enhancements throughout the process
/// 5. Proper handling of edge cases including nearly singular and clustered eigenvalues
/// </remarks>
public static class EigenDecomposition4X4
{
    public static void ExampleUse()
    {
        double[,] symmetricMatrix = {
            { 4, 1, 2, 1 },
            { 1, 5, 1, 2 },
            { 2, 1, 6, 1 },
            { 1, 2, 1, 7 }
        };

        var (eigenvalues, eigenvectors) = ComputeEigenDecomposition(symmetricMatrix);

        var m = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(symmetricMatrix);
        var evd = m.Evd(Symmetricity.Symmetric);

        Console.WriteLine("Eigenvalues:");
        PrintArray(eigenvalues);
        Console.WriteLine(evd.EigenValues.ToString());

        Console.WriteLine("Eigenvectors:");
        PrintMatrix(eigenvectors, 4);
    }
    
    // Optional: Print helpers
    public static void PrintMatrix(double[,] A, int n)
    {
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
                Console.Write($"{A[i, j]:F6} ");
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public static void PrintArray(double[] arr)
    {
        Console.WriteLine();
        foreach (var val in arr)
            Console.Write($"{val:F6} ");
        Console.WriteLine("\n");
    }



    #region Constants

    /// <summary>
    /// Default convergence tolerance for the QR algorithm.
    /// </summary>
    public const double DefaultTolerance = 1e-12;

    /// <summary>
    /// Default maximum number of iterations for the QR algorithm.
    /// </summary>
    public const int DefaultMaxIterations = 100;

    /// <summary>
    /// Numerical tolerance for matrix symmetry validation.
    /// </summary>
    public const double SymmetryTolerance = 1e-10;

    /// <summary>
    /// Minimum tolerance value to prevent numerical instability.
    /// </summary>
    public const double MinTolerance = 1e-16;

    /// <summary>
    /// Maximum tolerance value for practical convergence.
    /// </summary>
    public const double MaxTolerance = 1e-6;

    /// <summary>
    /// Machine epsilon for numerical comparisons.
    /// </summary>
    private const double MachineEpsilon = 2.220446049250313e-16;

    /// <summary>
    /// Tolerance for deflation (detecting converged eigenvalues).
    /// </summary>
    private const double DeflationTolerance = 1e-14;

    #endregion

    #region Public Methods

    /// <summary>
    /// Computes the eigenvalue decomposition of a 4x4 real symmetric matrix using the optimized two-stage algorithm.
    /// </summary>
    /// <param name="matrix">A 4x4 double array representing a symmetric matrix.</param>
    /// <returns>
    /// A tuple containing:
    /// - eigenvalues: A 1-dimensional double array of eigenvalues sorted in descending order
    /// - eigenvectors: A 2-dimensional double array where each column represents an eigenvector 
    ///   corresponding to the eigenvalue at the same index, normalized to unit length
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when the input matrix is null.</exception>
    /// <exception cref="ArgumentException">
    /// Thrown when:
    /// - The matrix is not 4x4
    /// - The matrix is not symmetric within numerical tolerance
    /// - The matrix contains invalid values (NaN or Infinity)
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the algorithm fails to converge within the maximum number of iterations.
    /// </exception>
    /// <example>
    /// <code>
    /// double[,] symmetricMatrix = {
    ///     {4.0, 1.0, 2.0, 0.5},
    ///     {1.0, 3.0, 0.0, 1.0},
    ///     {2.0, 0.0, 2.0, 0.0},
    ///     {0.5, 1.0, 0.0, 1.0}
    /// };
    /// 
    /// var (eigenvalues, eigenvectors) = EigenDecomposition4x4.ComputeEigenDecomposition(symmetricMatrix);
    /// 
    /// // eigenvalues[0] contains the largest eigenvalue
    /// // eigenvectors[i, j] contains the j-th component of the i-th eigenvector
    /// </code>
    /// </example>
    public static (double[] eigenvalues, double[,] eigenvectors) ComputeEigenDecomposition(double[,] matrix)
    {
        return ComputeEigenDecomposition(matrix, DefaultTolerance, DefaultMaxIterations);
    }

    /// <summary>
    /// Computes the eigenvalue decomposition of a 4x4 real symmetric matrix using the optimized two-stage algorithm
    /// with custom tolerance and maximum iterations.
    /// </summary>
    /// <param name="matrix">A 4x4 double array representing a symmetric matrix.</param>
    /// <param name="tolerance">Convergence tolerance for the algorithm. Must be between MinTolerance and MaxTolerance.</param>
    /// <param name="maxIterations">Maximum number of iterations allowed. Must be positive.</param>
    /// <returns>
    /// A tuple containing:
    /// - eigenvalues: A 1-dimensional double array of eigenvalues sorted in descending order
    /// - eigenvectors: A 2-dimensional double array where each column represents an eigenvector 
    ///   corresponding to the eigenvalue at the same index, normalized to unit length
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when the input matrix is null.</exception>
    /// <exception cref="ArgumentException">
    /// Thrown when:
    /// - The matrix is not 4x4
    /// - The matrix is not symmetric within numerical tolerance
    /// - The matrix contains invalid values (NaN or Infinity)
    /// - Tolerance is outside the valid range
    /// - MaxIterations is not positive
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the algorithm fails to converge within the maximum number of iterations.
    /// </exception>
    public static (double[] eigenvalues, double[,] eigenvectors) ComputeEigenDecomposition(
        double[,] matrix, 
        double tolerance, 
        int maxIterations)
    {
        // Input validation
        ValidateInputMatrix(matrix);
        ValidateParameters(tolerance, maxIterations);

        // Stage 1: Householder tridiagonalization
        var (diagonalElements, subdiagonalElements, householderQ) = HouseholderTridiagonalization(matrix);

        // Stage 2: QR algorithm with Wilkinson shifts on tridiagonal matrix
        var (eigenvalues, tridiagonalEigenvectors) = QRAlgorithmWithWilkinsonShifts(
            diagonalElements, subdiagonalElements, tolerance, maxIterations);

        // Back-transform eigenvectors: Q_final = Q_householder * Q_tridiagonal
        var eigenvectors = BackTransformEigenvectors(householderQ, tridiagonalEigenvectors);

        // Normalize eigenvectors to unit length
        NormalizeEigenvectors(eigenvectors);

        // Sort eigenvalues in descending order with corresponding eigenvectors
        SortEigenvaluesAndEigenvectors(eigenvalues, eigenvectors);

        return (eigenvalues, eigenvectors);
    }

    #endregion

    #region Input Validation

    /// <summary>
    /// Validates the input matrix for eigenvalue decomposition.
    /// </summary>
    /// <param name="matrix">The matrix to validate.</param>
    /// <exception cref="ArgumentNullException">Thrown when the matrix is null.</exception>
    /// <exception cref="ArgumentException">
    /// Thrown when the matrix is invalid (not 4x4, not symmetric, or contains invalid values).
    /// </exception>
    private static void ValidateInputMatrix(double[,] matrix)
    {
        if (matrix == null)
        {
            throw new ArgumentNullException(nameof(matrix), "Input matrix cannot be null.");
        }

        // Check dimensions
        if (matrix.GetLength(0) != 4 || matrix.GetLength(1) != 4)
        {
            throw new ArgumentException(
                $"Matrix must be 4x4. Actual dimensions: {matrix.GetLength(0)}x{matrix.GetLength(1)}.",
                nameof(matrix));
        }

        // Check for invalid values and symmetry
        var maxAsymmetry = 0.0;
        int asymmetryRow = -1, asymmetryCol = -1;

        for (var i = 0; i < 4; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                var value = matrix[i, j];

                // Check for invalid values
                if (double.IsNaN(value) || double.IsInfinity(value))
                {
                    throw new ArgumentException(
                        $"Matrix contains invalid value at position [{i}, {j}]: {value}. " +
                        "Matrix elements must be finite real numbers.",
                        nameof(matrix));
                }

                // Check symmetry
                if (j > i) // Only check upper triangle to avoid duplicate checks
                {
                    var diff = Math.Abs(matrix[i, j] - matrix[j, i]);
                    if (diff > maxAsymmetry)
                    {
                        maxAsymmetry = diff;
                        asymmetryRow = i;
                        asymmetryCol = j;
                    }
                }
            }
        }

        if (maxAsymmetry > SymmetryTolerance)
        {
            throw new ArgumentException(
                $"Matrix is not symmetric within tolerance {SymmetryTolerance}. " +
                $"Maximum asymmetry: {maxAsymmetry} at positions [{asymmetryRow}, {asymmetryCol}] " +
                $"with values {matrix[asymmetryRow, asymmetryCol]} and {matrix[asymmetryCol, asymmetryRow]}.",
                nameof(matrix));
        }
    }

    /// <summary>
    /// Validates the algorithm parameters.
    /// </summary>
    /// <param name="tolerance">The convergence tolerance.</param>
    /// <param name="maxIterations">The maximum number of iterations.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when parameters are outside valid ranges.
    /// </exception>
    private static void ValidateParameters(double tolerance, int maxIterations)
    {
        if (tolerance < MinTolerance || tolerance > MaxTolerance)
        {
            throw new ArgumentException(
                $"Tolerance must be between {MinTolerance} and {MaxTolerance}. Actual value: {tolerance}.",
                nameof(tolerance));
        }

        if (maxIterations <= 0)
        {
            throw new ArgumentException(
                $"MaxIterations must be positive. Actual value: {maxIterations}.",
                nameof(maxIterations));
        }
    }

    #endregion

    #region Stage 1: Householder Tridiagonalization

    /// <summary>
    /// Reduces a symmetric 4x4 matrix to symmetric tridiagonal form using Householder transformations.
    /// </summary>
    /// <param name="matrix">The input symmetric matrix.</param>
    /// <returns>
    /// A tuple containing:
    /// - diagonalElements: The diagonal elements of the tridiagonal matrix
    /// - subdiagonalElements: The subdiagonal elements of the tridiagonal matrix
    /// - householderQ: The orthogonal transformation matrix Q such that A = QTQ^T
    /// </returns>
    /// <remarks>
    /// For a 4x4 matrix, we need 2 Householder transformations to achieve tridiagonal form.
    /// The resulting tridiagonal matrix T has the form:
    /// [d0 e0  0  0]
    /// [e0 d1 e1  0]
    /// [ 0 e1 d2 e2]
    /// [ 0  0 e2 d3]
    /// </remarks>
    private static (double[] diagonalElements, double[] subdiagonalElements, double[,] householderQ) HouseholderTridiagonalization(double[,] matrix)
    {
        // Create working copy of the matrix
        var A = CloneMatrix(matrix);
        
        // Initialize orthogonal transformation matrix Q as identity
        var Q = CreateIdentityMatrix();
        
        var diagonal = new double[4];
        var subdiagonal = new double[3]; // Only 3 subdiagonal elements for 4x4

        // Apply Householder transformations to columns 0 and 1
        for (var k = 0; k < 2; k++)
        {
            // Compute Householder vector for column k, rows k+1 to 3
            var (householderVector, beta) = ComputeHouseholderVector(A, k);
            
            if (Math.Abs(beta) > MachineEpsilon)
            {
                // Apply Householder transformation: A := (I - beta*v*v^T) * A * (I - beta*v*v^T)
                ApplyHouseholderTransformation(A, householderVector, beta, k);
                
                // Update orthogonal matrix Q
                ApplyHouseholderToQ(Q, householderVector, beta, k);
            }
        }

        // Extract diagonal and subdiagonal elements
        for (var i = 0; i < 4; i++)
        {
            diagonal[i] = A[i, i];
        }
        
        for (var i = 0; i < 3; i++)
        {
            subdiagonal[i] = A[i, i + 1];
        }

        return (diagonal, subdiagonal, Q);
    }

    /// <summary>
    /// Computes the Householder vector and beta coefficient for the given column.
    /// </summary>
    /// <param name="A">The matrix being transformed.</param>
    /// <param name="k">The column index for which to compute the Householder vector.</param>
    /// <returns>
    /// A tuple containing:
    /// - householderVector: The Householder vector v
    /// - beta: The coefficient beta = 2 / (v^T * v)
    /// </returns>
    private static (double[] householderVector, double beta) ComputeHouseholderVector(double[,] A, int k)
    {
        var n = 4;
        var householderVector = new double[n];
        
        // Extract the subvector from column k, rows k+1 to n-1
        var sigma = 0.0;
        for (var i = k + 1; i < n; i++)
        {
            householderVector[i] = A[i, k];
            sigma += householderVector[i] * householderVector[i];
        }

        var beta = 0.0;
        
        if (Math.Abs(sigma) > MachineEpsilon)
        {
            var mu = Math.Sqrt(householderVector[k + 1] * householderVector[k + 1] + sigma);
            
            if (householderVector[k + 1] <= 0.0)
            {
                householderVector[k + 1] = householderVector[k + 1] - mu;
            }
            else
            {
                householderVector[k + 1] = -sigma / (householderVector[k + 1] + mu);
            }
            
            var v1Squared = householderVector[k + 1] * householderVector[k + 1];
            beta = 2.0 * v1Squared / (sigma + v1Squared);
            
            // Normalize the Householder vector
            for (var i = k + 2; i < n; i++)
            {
                householderVector[i] /= householderVector[k + 1];
            }
            householderVector[k + 1] = 1.0;
        }

        return (householderVector, beta);
    }

    /// <summary>
    /// Applies the Householder transformation to matrix A: A := (I - beta*v*v^T) * A * (I - beta*v*v^T).
    /// </summary>
    /// <param name="A">The matrix to transform.</param>
    /// <param name="v">The Householder vector.</param>
    /// <param name="beta">The beta coefficient.</param>
    /// <param name="k">The current transformation step.</param>
    private static void ApplyHouseholderTransformation(double[,] A, double[] v, double beta, int k)
    {
        var n = 4;
        
        // Compute w = A * v for the active part of the matrix
        var w = new double[n];
        for (var i = k + 1; i < n; i++)
        {
            w[i] = 0.0;
            for (var j = k + 1; j < n; j++)
            {
                w[i] += A[i, j] * v[j];
            }
        }
        
        // Compute alpha = (1/2) * beta * v^T * w
        var alpha = 0.0;
        for (var i = k + 1; i < n; i++)
        {
            alpha += v[i] * w[i];
        }
        alpha *= 0.5 * beta;
        
        // Update w := w - alpha * v
        for (var i = k + 1; i < n; i++)
        {
            w[i] -= alpha * v[i];
        }
        
        // Apply the transformation: A := A - beta * (v * w^T + w * v^T)
        for (var i = k + 1; i < n; i++)
        {
            for (var j = k + 1; j < n; j++)
            {
                A[i, j] -= beta * (v[i] * w[j] + w[i] * v[j]);
            }
        }
        
        // Update the subdiagonal elements
        if (k + 1 < n)
        {
            A[k, k + 1] = A[k + 1, k] = w[k + 1] > 0 ? -Math.Sqrt(w[k + 1] * w[k + 1]) : Math.Sqrt(w[k + 1] * w[k + 1]);
        }
    }

    /// <summary>
    /// Applies the Householder transformation to the orthogonal matrix Q.
    /// </summary>
    /// <param name="Q">The orthogonal matrix to update.</param>
    /// <param name="v">The Householder vector.</param>
    /// <param name="beta">The beta coefficient.</param>
    /// <param name="k">The current transformation step.</param>
    private static void ApplyHouseholderToQ(double[,] Q, double[] v, double beta, int k)
    {
        var n = 4;
        
        // Compute Q := Q * (I - beta * v * v^T)
        for (var i = 0; i < n; i++)
        {
            var sum = 0.0;
            for (var j = k + 1; j < n; j++)
            {
                sum += Q[i, j] * v[j];
            }
            sum *= beta;
            
            for (var j = k + 1; j < n; j++)
            {
                Q[i, j] -= sum * v[j];
            }
        }
    }

    #endregion

    #region Stage 2: QR Algorithm with Wilkinson Shifts

    /// <summary>
    /// Applies the QR algorithm with Wilkinson shifts to find eigenvalues of a symmetric tridiagonal matrix.
    /// </summary>
    /// <param name="diagonal">The diagonal elements of the tridiagonal matrix.</param>
    /// <param name="subdiagonal">The subdiagonal elements of the tridiagonal matrix.</param>
    /// <param name="tolerance">Convergence tolerance.</param>
    /// <param name="maxIterations">Maximum number of iterations.</param>
    /// <returns>
    /// A tuple containing:
    /// - eigenvalues: The computed eigenvalues
    /// - eigenvectors: The eigenvectors of the tridiagonal matrix
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the algorithm fails to converge within the maximum number of iterations.
    /// </exception>
    private static (double[] eigenvalues, double[,] eigenvectors) QRAlgorithmWithWilkinsonShifts(double[] diagonal, double[] subdiagonal, double tolerance, int maxIterations)
    {
        var n = 4;
        var d = (double[])diagonal.Clone();
        var e = (double[])subdiagonal.Clone();
        
        // Initialize eigenvector matrix as identity
        var Z = CreateIdentityMatrix();
        
        var m = n - 1; // Last active index
        var iteration = 0;
        
        while (m > 0 && iteration < maxIterations)
        {
            // Check for deflation (small subdiagonal elements)
            var l = FindDeflationIndex(d, e, m, tolerance);
            
            if (l == m)
            {
                // Single eigenvalue converged
                m--;
                continue;
            }
            
            if (l == m - 1)
            {
                // 2x2 block converged
                Solve2x2Block(d, e, m - 1);
                m -= 2;
                continue;
            }
            
            // Apply QR step with Wilkinson shift
            var shift = ComputeWilkinsonShift(d, e, m);
            ApplyQRStep(d, e, Z, l, m, shift);
            
            iteration++;
        }
        
        if (iteration >= maxIterations)
        {
            throw new InvalidOperationException(
                $"QR algorithm failed to converge within {maxIterations} iterations. " +
                "Consider increasing maxIterations or relaxing tolerance.");
        }
        
        return (d, Z);
    }

    /// <summary>
    /// Finds the deflation index where subdiagonal elements are sufficiently small.
    /// </summary>
    /// <param name="d">Diagonal elements.</param>
    /// <param name="e">Subdiagonal elements.</param>
    /// <param name="m">Current active size.</param>
    /// <param name="tolerance">Convergence tolerance.</param>
    /// <returns>The deflation index.</returns>
    private static int FindDeflationIndex(double[] d, double[] e, int m, double tolerance)
    {
        for (var i = m - 1; i >= 0; i--)
        {
            var threshold = tolerance * (Math.Abs(d[i]) + Math.Abs(d[i + 1]));
            if (Math.Abs(e[i]) <= Math.Max(threshold, DeflationTolerance))
            {
                e[i] = 0.0; // Explicit deflation
                return i + 1;
            }
        }
        return 0;
    }

    /// <summary>
    /// Solves a 2x2 block exactly using the quadratic formula.
    /// </summary>
    /// <param name="d">Diagonal elements.</param>
    /// <param name="e">Subdiagonal elements.</param>
    /// <param name="i">Index of the 2x2 block.</param>
    private static void Solve2x2Block(double[] d, double[] e, int i)
    {
        // Solve the 2x2 eigenvalue problem:
        // | d[i]   e[i]  |
        // | e[i]  d[i+1] |
        
        var a = d[i];
        var b = e[i];
        var c = d[i + 1];
        
        var trace = a + c;
        var det = a * c - b * b;
        var discriminant = trace * trace - 4.0 * det;
        
        if (discriminant >= 0.0)
        {
            var sqrt_disc = Math.Sqrt(discriminant);
            var lambda1 = 0.5 * (trace + sqrt_disc);
            var lambda2 = 0.5 * (trace - sqrt_disc);
            
            // Sort eigenvalues
            if (Math.Abs(lambda1) > Math.Abs(lambda2))
            {
                d[i] = lambda1;
                d[i + 1] = lambda2;
            }
            else
            {
                d[i] = lambda2;
                d[i + 1] = lambda1;
            }
        }
        else
        {
            // Should not happen for real symmetric matrices
            d[i] = 0.5 * trace;
            d[i + 1] = 0.5 * trace;
        }
        
        e[i] = 0.0; // Mark as deflated
    }

    /// <summary>
    /// Computes the Wilkinson shift for improved convergence.
    /// </summary>
    /// <param name="d">Diagonal elements.</param>
    /// <param name="e">Subdiagonal elements.</param>
    /// <param name="m">Current active size.</param>
    /// <returns>The Wilkinson shift.</returns>
    private static double ComputeWilkinsonShift(double[] d, double[] e, int m)
    {
        if (m == 1)
        {
            return d[m];
        }
        
        // Consider the bottom-right 2x2 submatrix
        var a = d[m - 1];
        var b = e[m - 1];
        var c = d[m];
        
        var trace = a + c;
        var det = a * c - b * b;
        var discriminant = trace * trace - 4.0 * det;
        
        if (discriminant >= 0.0)
        {
            var sqrt_disc = Math.Sqrt(discriminant);
            var lambda1 = 0.5 * (trace + sqrt_disc);
            var lambda2 = 0.5 * (trace - sqrt_disc);
            
            // Choose the eigenvalue closer to d[m] (Wilkinson's strategy)
            return Math.Abs(lambda1 - c) < Math.Abs(lambda2 - c) ? lambda1 : lambda2;
        }
        else
        {
            return c; // Fallback
        }
    }

    /// <summary>
    /// Applies a single QR step with the given shift to the active submatrix.
    /// </summary>
    /// <param name="d">Diagonal elements.</param>
    /// <param name="e">Subdiagonal elements.</param>
    /// <param name="Z">Eigenvector matrix to update.</param>
    /// <param name="l">Start index of active submatrix.</param>
    /// <param name="m">End index of active submatrix.</param>
    /// <param name="shift">The shift value.</param>
    private static void ApplyQRStep(double[] d, double[] e, double[,] Z, int l, int m, double shift)
    {
        // Apply shift: T := T - shift * I
        for (var i = l; i <= m; i++)
        {
            d[i] -= shift;
        }
        
        // Apply Givens rotations to perform QR factorization
        for (var i = l; i < m; i++)
        {
            // Compute Givens rotation to zero out e[i]
            var (c, s) = ComputeGivensRotation(d[i], e[i]);
            
            // Apply rotation to the matrix
            ApplyGivensRotation(d, e, c, s, i, i + 1);
            
            // Update eigenvectors
            ApplyGivensToEigenvectors(Z, c, s, i, i + 1);
        }
        
        // Restore shift: T := T + shift * I
        for (var i = l; i <= m; i++)
        {
            d[i] += shift;
        }
    }

    /// <summary>
    /// Computes Givens rotation parameters to zero out the second component.
    /// </summary>
    /// <param name="a">First component.</param>
    /// <param name="b">Second component (to be zeroed).</param>
    /// <returns>A tuple containing the cosine and sine of the rotation angle.</returns>
    private static (double c, double s) ComputeGivensRotation(double a, double b)
    {
        if (Math.Abs(b) < MachineEpsilon)
        {
            return (1.0, 0.0);
        }
        
        if (Math.Abs(b) > Math.Abs(a))
        {
            var tau = -a / b;
            var s = 1.0 / Math.Sqrt(1.0 + tau * tau);
            var c = s * tau;
            return (c, s);
        }
        else
        {
            var tau = -b / a;
            var c = 1.0 / Math.Sqrt(1.0 + tau * tau);
            var s = c * tau;
            return (c, s);
        }
    }

    /// <summary>
    /// Applies Givens rotation to the tridiagonal matrix.
    /// </summary>
    /// <param name="d">Diagonal elements.</param>
    /// <param name="e">Subdiagonal elements.</param>
    /// <param name="c">Cosine of rotation angle.</param>
    /// <param name="s">Sine of rotation angle.</param>
    /// <param name="i">First index.</param>
    /// <param name="j">Second index.</param>
    private static void ApplyGivensRotation(double[] d, double[] e, double c, double s, int i, int j)
    {
        // Apply Givens rotation G(i,j,θ) to the tridiagonal matrix
        var temp1 = c * d[i] - s * e[i];
        var temp2 = s * d[i] + c * e[i];
        
        d[i] = temp1;
        if (i < e.Length)
        {
            e[i] = temp2;
        }
        
        if (j < d.Length)
        {
            var temp3 = c * d[j] - s * e[j - 1];
            var temp4 = s * d[j] + c * e[j - 1];
            
            d[j] = temp3;
            if (j - 1 < e.Length)
            {
                e[j - 1] = temp4;
            }
        }
    }

    /// <summary>
    /// Applies Givens rotation to the eigenvector matrix.
    /// </summary>
    /// <param name="Z">Eigenvector matrix.</param>
    /// <param name="c">Cosine of rotation angle.</param>
    /// <param name="s">Sine of rotation angle.</param>
    /// <param name="i">First column index.</param>
    /// <param name="j">Second column index.</param>
    private static void ApplyGivensToEigenvectors(double[,] Z, double c, double s, int i, int j)
    {
        var n = 4;
        for (var k = 0; k < n; k++)
        {
            var temp1 = c * Z[k, i] - s * Z[k, j];
            var temp2 = s * Z[k, i] + c * Z[k, j];
            Z[k, i] = temp1;
            Z[k, j] = temp2;
        }
    }

    #endregion

    #region Stage 3: Back-transformation and Finalization

    /// <summary>
    /// Back-transforms eigenvectors from tridiagonal form to original form.
    /// </summary>
    /// <param name="householderQ">The Householder transformation matrix.</param>
    /// <param name="tridiagonalEigenvectors">Eigenvectors of the tridiagonal matrix.</param>
    /// <returns>Eigenvectors of the original matrix.</returns>
    private static double[,] BackTransformEigenvectors(double[,] householderQ, double[,] tridiagonalEigenvectors)
    {
        var n = 4;
        var result = new double[n, n];
        
        // Compute Q_householder * Q_tridiagonal
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                result[i, j] = 0.0;
                for (var k = 0; k < n; k++)
                {
                    result[i, j] += householderQ[i, k] * tridiagonalEigenvectors[k, j];
                }
            }
        }
        
        return result;
    }

    /// <summary>
    /// Normalizes all eigenvectors to unit length.
    /// </summary>
    /// <param name="eigenvectors">The eigenvector matrix to normalize.</param>
    private static void NormalizeEigenvectors(double[,] eigenvectors)
    {
        var n = 4;
        
        for (var j = 0; j < n; j++)
        {
            // Compute norm of column j
            var norm = 0.0;
            for (var i = 0; i < n; i++)
            {
                norm += eigenvectors[i, j] * eigenvectors[i, j];
            }
            norm = Math.Sqrt(norm);
            
            // Handle zero or near-zero norm
            if (norm < MachineEpsilon)
            {
                throw new InvalidOperationException(
                    $"Eigenvector {j} has zero or near-zero norm ({norm}). " +
                    "This indicates numerical instability.");
            }
            
            // Normalize
            for (var i = 0; i < n; i++)
            {
                eigenvectors[i, j] /= norm;
            }
        }
    }

    /// <summary>
    /// Sorts eigenvalues in descending order and rearranges corresponding eigenvectors.
    /// </summary>
    /// <param name="eigenvalues">Array of eigenvalues to sort.</param>
    /// <param name="eigenvectors">Matrix of eigenvectors to rearrange.</param>
    private static void SortEigenvaluesAndEigenvectors(double[] eigenvalues, double[,] eigenvectors)
    {
        var n = 4;
        var indices = Enumerable.Range(0, n).ToArray();
        
        // Sort indices by eigenvalue in descending order
        Array.Sort(indices, (i, j) => eigenvalues[j].CompareTo(eigenvalues[i]));
        
        // Create sorted eigenvalues
        var sortedEigenvalues = new double[n];
        for (var i = 0; i < n; i++)
        {
            sortedEigenvalues[i] = eigenvalues[indices[i]];
        }
        
        // Copy sorted eigenvalues back
        Array.Copy(sortedEigenvalues, eigenvalues, n);
        
        // Rearrange eigenvectors according to sorted order
        var tempEigenvectors = CloneMatrix(eigenvectors);
        for (var j = 0; j < n; j++)
        {
            var originalIndex = indices[j];
            for (var i = 0; i < n; i++)
            {
                eigenvectors[i, j] = tempEigenvectors[i, originalIndex];
            }
        }
    }

    #endregion

    #region Utility Methods

    /// <summary>
    /// Creates a deep copy of a 4x4 matrix.
    /// </summary>
    /// <param name="matrix">The matrix to clone.</param>
    /// <returns>A new 4x4 matrix that is a copy of the input.</returns>
    private static double[,] CloneMatrix(double[,] matrix)
    {
        var clone = new double[4, 4];
        for (var i = 0; i < 4; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                clone[i, j] = matrix[i, j];
            }
        }
        return clone;
    }

    /// <summary>
    /// Creates a 4x4 identity matrix.
    /// </summary>
    /// <returns>A 4x4 identity matrix.</returns>
    private static double[,] CreateIdentityMatrix()
    {
        var identity = new double[4, 4];
        for (var i = 0; i < 4; i++)
        {
            identity[i, i] = 1.0;
        }
        return identity;
    }

    #endregion

    #region Numerical Verification

    /// <summary>
    /// Performs a comprehensive numerical verification of the computed eigendecomposition.
    /// </summary>
    /// <param name="originalMatrix">The original input matrix.</param>
    /// <param name="eigenvalues">The computed eigenvalues.</param>
    /// <param name="eigenvectors">The computed eigenvectors.</param>
    /// <param name="tolerance">Tolerance for verification.</param>
    /// <returns>True if the decomposition passes all verification tests, false otherwise.</returns>
    /// <remarks>
    /// This method performs multiple verification checks:
    /// 1. Verifies that A * v_i = λ_i * v_i for each eigenvalue-eigenvector pair
    /// 2. Checks that eigenvectors are normalized to unit length
    /// 3. Verifies orthogonality of eigenvectors
    /// 4. Checks spectral reconstruction: A = Q * Λ * Q^T
    /// </remarks>
    public static bool VerifyDecomposition(
        double[,] originalMatrix, 
        double[] eigenvalues, 
        double[,] eigenvectors, 
        double tolerance = 1e-10)
    {
        if (originalMatrix == null || eigenvalues == null || eigenvectors == null)
            return false;

        if (eigenvalues.Length != 4 || 
            eigenvectors.GetLength(0) != 4 || 
            eigenvectors.GetLength(1) != 4)
            return false;

        // Check 1: Verify A * v_i = λ_i * v_i for each eigenpair
        for (var i = 0; i < 4; i++)
        {
            var eigenvalue = eigenvalues[i];
            
            // Extract eigenvector
            var eigenvector = new double[4];
            for (var j = 0; j < 4; j++)
            {
                eigenvector[j] = eigenvectors[j, i];
            }

            // Compute A * v_i
            var Av = new double[4];
            for (var j = 0; j < 4; j++)
            {
                Av[j] = 0.0;
                for (var k = 0; k < 4; k++)
                {
                    Av[j] += originalMatrix[j, k] * eigenvector[k];
                }
            }

            // Check A * v_i = λ_i * v_i
            for (var j = 0; j < 4; j++)
            {
                var expected = eigenvalue * eigenvector[j];
                var diff = Math.Abs(Av[j] - expected);
                if (diff > tolerance)
                    return false;
            }

            // Check 2: Verify eigenvector is normalized
            var norm = 0.0;
            for (var j = 0; j < 4; j++)
            {
                norm += eigenvector[j] * eigenvector[j];
            }
            if (Math.Abs(norm - 1.0) > tolerance)
                return false;
        }

        // Check 3: Verify orthogonality of eigenvectors
        for (var i = 0; i < 4; i++)
        {
            for (var j = i + 1; j < 4; j++)
            {
                var dotProduct = 0.0;
                for (var k = 0; k < 4; k++)
                {
                    dotProduct += eigenvectors[k, i] * eigenvectors[k, j];
                }
                if (Math.Abs(dotProduct) > tolerance)
                    return false;
            }
        }

        // Check 4: Verify spectral reconstruction A = Q * Λ * Q^T
        var reconstructed = new double[4, 4];
        for (var i = 0; i < 4; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                reconstructed[i, j] = 0.0;
                for (var k = 0; k < 4; k++)
                {
                    reconstructed[i, j] += eigenvectors[i, k] * eigenvalues[k] * eigenvectors[j, k];
                }
            }
        }

        for (var i = 0; i < 4; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                var diff = Math.Abs(reconstructed[i, j] - originalMatrix[i, j]);
                if (diff > tolerance)
                    return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Computes the condition number of the eigenvalue problem for stability assessment.
    /// </summary>
    /// <param name="eigenvalues">The computed eigenvalues.</param>
    /// <returns>The condition number (ratio of largest to smallest eigenvalue magnitude).</returns>
    /// <remarks>
    /// A large condition number indicates that the eigenvalue problem may be ill-conditioned,
    /// meaning small perturbations in the input matrix can lead to large changes in eigenvalues.
    /// </remarks>
    public static double ComputeConditionNumber(double[] eigenvalues)
    {
        if (eigenvalues == null || eigenvalues.Length != 4)
            throw new ArgumentException("Eigenvalues array must contain exactly 4 elements.");

        var maxAbs = 0.0;
        var minAbs = double.MaxValue;

        for (var i = 0; i < 4; i++)
        {
            var abs = Math.Abs(eigenvalues[i]);
            maxAbs = Math.Max(maxAbs, abs);
            if (abs > MachineEpsilon) // Avoid division by zero
            {
                minAbs = Math.Min(minAbs, abs);
            }
        }

        return minAbs > MachineEpsilon ? maxAbs / minAbs : double.PositiveInfinity;
    }

    #endregion
}