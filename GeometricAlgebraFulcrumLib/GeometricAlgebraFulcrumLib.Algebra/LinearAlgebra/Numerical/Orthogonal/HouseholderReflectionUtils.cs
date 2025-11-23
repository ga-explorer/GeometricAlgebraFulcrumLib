using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Orthogonal;

public static class HouseholderReflectionUtils
{
    /// <summary>
    /// Compute Householder reflection vector for zeroing out elements below a pivot
    /// </summary>
    /// <param name="xVector">Input vector</param>
    /// <param name="startIndex">Starting index for the reflection</param>
    /// <param name="tolerance">Numerical tolerance</param>
    /// <returns>Householder reflection vector</returns>
    public static (bool success, Vector<double> householderVector) ComputeHouseholderVector(this IReadOnlyList<double> xVector, int startIndex, double tolerance = Float64Utils.DefaultTolerance)
    {
        var n = xVector.Count;
        
        if (startIndex >= n - 1) 
            return (false, SparseVector.Create(n, 0)); // No reflection needed
            
        // Extract the subvector from startIndex+1 to end
        var subVectorStart = startIndex + 1;
        var subVectorLength = n - subVectorStart;
            
        if (subVectorLength <= 0)
            return (false, SparseVector.Create(n, 0));
            
        // Compute norm of subvector
        var xSubNorm = 0d;
        for (var i = subVectorStart; i < n; i++) 
            xSubNorm += xVector[i] * xVector[i];
        xSubNorm = Math.Sqrt(xSubNorm);
            
        if (xSubNorm < tolerance)
            return (false, SparseVector.Create(n, 0)); // Already in desired form
            
        // Create Householder vector: v = x ± ||x|| e_1
        var householderVector = DenseVector.Create(n, 0);
        householderVector[subVectorStart] = 
            xVector[subVectorStart] + (xVector[subVectorStart] >= 0 ? xSubNorm : -xSubNorm);
            
        for (var i = subVectorStart + 1; i < n; i++) 
            householderVector[i] = xVector[i];
            
        // Normalize if needed
        double householderVectorNorm = 0;
        for (var i = subVectorStart; i < n; i++) 
            householderVectorNorm += householderVector[i] * householderVector[i];
        householderVectorNorm = Math.Sqrt(householderVectorNorm);

        if (householderVectorNorm < tolerance) 
            return (false, SparseVector.Create(n, 0));
            
        for (var i = subVectorStart; i < n; i++) 
            householderVector[i] /= householderVectorNorm;

        return (true, householderVector);
    }


    /// <summary>
    /// Convert a vector v into a reflection matrix that can be used to
    /// reflect other vectors on the hyper-plane orthogonal to v.
    /// </summary>
    /// <param name="v"></param>
    /// <param name="assumeUnitVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DenseMatrix ToDenseHouseholderMatrix(this Vector<double> v, bool assumeUnitVector = false)
    {
        var n = v.Count;
        var matrixI = DenseMatrix.CreateIdentity(n);
        var outer = v.ToColumnMatrix() * v.ToRowMatrix(); // v v^T
        var scalar = assumeUnitVector ? 2d : 2d / v.L2NormSquared();

        return (DenseMatrix)(matrixI - scalar * outer);
    }
    
    /// <summary>
    /// Convert a basis vector v into a reflection matrix that can be used to
    /// reflect other vectors on the hyper-plane orthogonal to v.
    /// </summary>
    /// <param name="basisVectorIndex"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DenseMatrix ToDenseHouseholderMatrix(this int basisVectorIndex, int size)
    {
        return DenseMatrix.CreateDiagonal(
            size, 
            size, 
            i => i == basisVectorIndex ? -1 : 1
        );
    }
    
    /// <summary>
    /// Convert a basis vector v into a reflection matrix that can be used to
    /// reflect other vectors on the hyper-plane orthogonal to v.
    /// </summary>
    /// <param name="basisVectorIndex"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SparseMatrix ToSparseHouseholderMatrix(this int basisVectorIndex, int size)
    {
        return SparseMatrix.CreateDiagonal(
            size, 
            size, 
            i => i == basisVectorIndex ? -1 : 1
        );
    }
    

    /// <summary>
    /// Factorizes a square real matrix M into a product of Householder
    /// reflections with a resultant matrix R:
    /// M = H₀ H₁ ... Hₖ R
    /// Returns the list of Householder vectors v such that H = I - 2 v vᵀ
    /// </summary>
    /// <param name="matrix">Square real matrix (n x n)</param>
    /// <param name="tolerance">Numerical tolerance</param>
    /// <returns>List of Householder vectors (each of length n)</returns>
    public static HouseholderFactorizationResults GetHouseholderFactorization(this Matrix<double> matrix, double tolerance = Float64Utils.DefaultTolerance)
    {
        var n = matrix.Size();
        var householderVectors = new List<Vector<double>>();
        var finalMatrix = matrix;

        for (var k = 0; k < n - 1; k++) // Last column will be ±e_{n-1}, handled by sign
        {
            // Extract subvector from column k, starting at row k
            var x = new double[n - k];
            for (var i = 0; i < n - k; i++)
                x[i] = finalMatrix[i + k, k];

            var xNorm = x.L2Norm();

            if (xNorm < tolerance)
                throw new ArgumentException("Matrix not orthogonal", nameof(matrix)); // Should not happen for orthogonal matrix

            // Choose sign to avoid cancellation
            var sigma = x[0] >= 0 ? xNorm : -xNorm;

            // Compute v = x - sigma * e1
            var v = new double[n - k];
            v[0] = x[0] - sigma;
            for (var i = 1; i < n - k; i++)
                v[i] = x[i];

            var vNorm = v.L2Norm();
            if (vNorm < tolerance)
                continue; // Already in correct form

            // Normalize v
            for (var i = 0; i < n - k; i++)
                v[i] /= vNorm;

            // Embed into full-space vector of length n
            var vFull = new double[n];
            for (var i = 0; i < n - k; i++)
                vFull[k + i] = v[i];

            var householderVector = CreateVector.Dense(vFull);
            householderVectors.Add(householderVector);

            // Construct H = I - 2 v v^T (as operator), apply H * matrixA
            finalMatrix = ApplyHouseholderLeft(finalMatrix, householderVector, k, true);
        }

        if (ReferenceEquals(finalMatrix, matrix))
            finalMatrix = matrix.Clone();
        
        finalMatrix.RemoveSmallScalars(tolerance);

        return new HouseholderFactorizationResults(
            matrix, 
            finalMatrix, 
            householderVectors, 
            tolerance
        );

        // Applies Householder reflection H = I - 2 v v^T from the left:
        // A := H * A
        // Only affects rows starting at index 'startRow'
        static Matrix<double> ApplyHouseholderLeft(Matrix<double> matrix, Vector<double> v, int startRow, bool assumeUnitVector = false)
        {
            var n = matrix.RowCount;

            // Extract active part of v (from startRow to end)
            var vActive = v.SubVector(startRow, n - startRow);
            var matrixAActive = matrix.SubMatrix(startRow, n - startRow, 0, n); // Rows from startRow
            var scalar = assumeUnitVector ? 2d : 2d / vActive.DotProduct(vActive);

            // H * matrixA = matrixA - 2 * v * (v^T * matrixA)
            var vTbyA = vActive.ToRowMatrix().Multiply(matrixAActive); // 1 x n
            var outer = vActive.ToColumnMatrix().Multiply(vTbyA); // (n-k) x n

            var updatedActive = matrixAActive - scalar * outer;

            // Copy back
            var finalMatrix = matrix.Clone();
            for (var i = 0; i < n - startRow; i++)
            for (var j = 0; j < n; j++)
                finalMatrix[i + startRow, j] = updatedActive[i, j];

            return finalMatrix;
        }
    }
    
    /// <summary>
    /// Reduce a square real matrix using Householder reflections
    /// </summary>
    /// <param name="matrix">Input bivector</param>
    /// <param name="tolerance"></param>
    /// <returns>Tuple of (tridiagonal bivector, accumulated versor)</returns>
    public static HouseholderReductionResults GetHouseholderReduction(this Matrix<double> matrix, double tolerance = Float64Utils.DefaultTolerance)
    {
        var n = matrix.Size();
        var householderVectors = new List<Vector<double>>(n - 2);
        
        var reducedMatrix = matrix;
        
        // Apply Householder reflections iteratively
        for (var step = 0; step < n - 2; step++)
        {
            // Apply Householder reflection to zero out elements below the subdiagonal
            (reducedMatrix, var householderVector) = ApplyHouseholderReflectionMatrix(reducedMatrix, step);
            
            // Accumulate the orthogonal transformations
            if (!householderVector.IsZero())
                householderVectors.Add(householderVector);
        }

        if (ReferenceEquals(reducedMatrix, matrix))
            reducedMatrix = matrix.Clone();

        reducedMatrix.RemoveSmallScalars(tolerance);

        return new HouseholderReductionResults(
            matrix, 
            reducedMatrix, 
            householderVectors, 
            tolerance
        );

        // <summary>
        // Apply a single Householder reflection step using matrix operations
        // </summary>
        // <param name="matrix">Current matrix</param>
        // <param name="basisVectorIndex">Current step index</param>
        // <param name="tolerance"></param>
        // <returns>Tuple of (reflected matrix, Householder matrix)</returns>
        static (Matrix<double> reducedMatrix, Vector<double> householderVector) ApplyHouseholderReflectionMatrix(Matrix<double> matrix, int basisVectorIndex, double tolerance = Float64Utils.DefaultTolerance)
        {
            var n = matrix.Size();
            
            if (basisVectorIndex >= n - 1) 
                return (matrix, SparseVector.Create(n, 0));
            
            // Extract the column vector from position (step+1, step) downward
            var columnLength = n - basisVectorIndex - 1;
            if (columnLength <= 0) 
                return (matrix, SparseVector.Create(n, 0));
            
            var x = new double[columnLength];
            for (var i = 0; i < columnLength; i++) 
                x[i] = matrix[basisVectorIndex + 1 + i, basisVectorIndex];
            
            var norm = 0.0;
            for (var i = 0; i < columnLength; i++) 
                norm += x[i] * x[i];
            norm = Math.Sqrt(norm);
            
            if (norm < tolerance) 
                return (matrix, SparseVector.Create(n, 0));
            
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
                return (matrix, SparseVector.Create(n, 0));
            
            vNorm = Math.Sqrt(vNorm);
            for (var i = 0; i < columnLength; i++) 
                v[i] /= vNorm;
            
            // Create Householder matrix H = I - 2vvᵀ for the subspace
            var subH = DenseMatrix.CreateIdentity(columnLength);
            for (var i = 0; i < columnLength; i++)
            for (var j = 0; j < columnLength; j++)
                subH[i, j] -= 2.0 * v[i] * v[j];
            
            // Embed v,subH into the full Householder vector and matrix
            var householderMatrix = DenseMatrix.CreateIdentity(n);
            var householderVector = DenseVector.Create(n, 0);
            for (var i = 0; i < columnLength; i++)
            {
                householderVector[basisVectorIndex + 1 + i] = v[i];

                for (var j = 0; j < columnLength; j++)
                    householderMatrix[basisVectorIndex + 1 + i, basisVectorIndex + 1 + j] = subH[i, j];
            }
            
            // Apply similarity transformation: A' = H A H (since H is symmetric)
            var reducedMatrix = householderMatrix * matrix * householderMatrix;
            
            return (reducedMatrix, householderVector);
        }
    }


    // --------------------------------------------------------
    // Example Usage
    // --------------------------------------------------------
    
    /// <summary>
    /// Householder factorization example for general square matrices
    /// </summary>
    public static void FactorizationExample1()
    {
        const int n = 5;

        // Create a random square matrix via QR factorization
        var rand = new Random(1);
        var matrixQ = rand.GetRealDenseMatrix(n);

        Console.WriteLine("Original matrix Q:");
        Console.WriteLine(matrixQ.ToString());

        // Factorize Q into Householder reflections
        var result = 
            matrixQ.GetHouseholderFactorization(1e-12);

        Console.WriteLine($"\nFactorized into {result.GetHouseholderVectorsCount()} Householder reflections:");

        foreach (var v in result.GetHouseholderVectors())
            Console.WriteLine("   " + v.ToString());
        
        Console.WriteLine("Resultant matrix R:");
        Console.WriteLine(result.FinalMatrix.ToString());

        // Reconstruct Q: H0 * H1 * ... * H_{k-1} * R
        var matrixR = result.ReconstructMatrix(); 

        Console.WriteLine("Reconstructed matrix:");
        Console.WriteLine(matrixR.ToString());

        // Check error
        var error = (matrixQ - matrixR).L2Norm();
        Console.WriteLine($"Reconstruction error: {error}");
    }
    
    /// <summary>
    /// Householder factorization example for symmetric square matrices
    /// </summary>
    public static void FactorizationExample2()
    {
        const int n = 5;

        // Create a random symmetric matrix
        var rand = new Random(1);
        var matrixQ = rand.GetRealSymmetricDenseMatrix(n);

        Console.WriteLine("Original matrix Q:");
        Console.WriteLine(matrixQ.ToString());
        
        // Factorize Q into Householder reflections
        var result = 
            matrixQ.GetHouseholderFactorization(1e-12);
        
        Console.WriteLine("Resultant matrix R:");
        Console.WriteLine(result.FinalMatrix.ToString());

        Console.WriteLine($"\nFactorized into {result.GetHouseholderVectorsCount()} Householder reflections:");

        foreach (var v in result.GetHouseholderVectors())
            Console.WriteLine("   " + v.ToString());

        // Reconstruct Q: H0 * H1 * ... * H_{k-1} * R
        var matrixR = result.ReconstructMatrix(); 

        Console.WriteLine("Reconstructed matrix:");
        Console.WriteLine(matrixR.ToString());

        // Check error
        var error = (matrixQ - matrixR).L2Norm();
        Console.WriteLine($"Reconstruction error: {error}");
    }
    
    /// <summary>
    /// Householder factorization example for skew-symmetric square matrices
    /// </summary>
    public static void FactorizationExample3()
    {
        const int n = 5;

        // Create a random skew-symmetric matrix
        var rand = new Random(1);
        var matrixQ = rand.GetRealSkewSymmetricDenseMatrix(n);
        
        Console.WriteLine("Original matrix Q:");
        Console.WriteLine(matrixQ.ToString());
        
        // Factorize Q into Householder reflections
        var result = 
            matrixQ.GetHouseholderFactorization(1e-12);
        
        Console.WriteLine("Resultant matrix R:");
        Console.WriteLine(result.FinalMatrix.ToString());

        Console.WriteLine($"\nFactorized into {result.GetHouseholderVectorsCount()} Householder reflections:");

        foreach (var v in result.GetHouseholderVectors())
            Console.WriteLine("   " + v.ToString());

        // Reconstruct Q: H0 * H1 * ... * H_{k-1} * R
        var matrixR = result.ReconstructMatrix(); 

        Console.WriteLine("Reconstructed matrix:");
        Console.WriteLine(matrixR.ToString());

        // Check error
        var error = (matrixQ - matrixR).L2Norm();
        Console.WriteLine($"Reconstruction error: {error}");
    }

    /// <summary>
    /// Householder factorization example for orthogonal square matrices
    /// </summary>
    public static void FactorizationExample4()
    {
        const int n = 5;

        // Create a random orthogonal matrix via QR factorization
        var rand = new Random(1);
        var matrixQ = rand.GetOrthogonalMatrix(n);

        Console.WriteLine("Original orthogonal matrix Q:");
        Console.WriteLine(matrixQ.ToString());

        // Factorize Q into Householder reflections
        var result = 
            matrixQ.GetHouseholderFactorization(1e-10);
        
        Debug.Assert(result.IsOrthogonal());

        Console.WriteLine($"\nFactorized into {result.GetHouseholderVectorsCount()} Householder reflections:");

        foreach (var v in result.GetHouseholderVectors())
            Console.WriteLine("   " + v.ToString());

        // Reconstruct Q: H0 * H1 * ... * H_{k-1}
        var matrixR = result.ReconstructMatrix(); 

        Console.WriteLine("Reconstructed matrix:");
        Console.WriteLine(matrixR.ToString());

        // Check error
        var error = (matrixQ - matrixR).L2Norm();
        Console.WriteLine($"Reconstruction error: {error}");
    }
    

    /// <summary>
    /// Householder reduction example for general square matrices
    /// </summary>
    public static void ReductionExample1()
    {
        const int n = 5;

        // Create a random square matrix via QR factorization
        var rand = new Random(1);
        var matrixQ = rand.GetRealDenseMatrix(n);

        Console.WriteLine("Original matrix Q:");
        Console.WriteLine(matrixQ.ToString());

        // Reduce Q using Householder reflections
        var result = 
            matrixQ.GetHouseholderReduction(1e-12);

        Console.WriteLine($"\nReduced using {result.HouseholderVectors.Count} Householder reflections:");

        foreach (var v in result.HouseholderVectors)
            Console.WriteLine("   " + v.ToString());
        
        Console.WriteLine("Reduced matrix R:");
        Console.WriteLine(result.ReducedMatrix.ToString());

        var matrixR = result.ReconstructMatrix(); 

        Console.WriteLine("Reconstructed matrix:");
        Console.WriteLine(matrixR.ToString());

        // Check error
        var error = (matrixQ - matrixR).L2Norm();
        Console.WriteLine($"Reconstruction error: {error}");
    }
    
    /// <summary>
    /// Householder reduction example for symmetric square matrices
    /// </summary>
    public static void ReductionExample2()
    {
        const int n = 5;

        // Create a random square matrix via QR factorization
        var rand = new Random(1);
        var matrixQ = rand.GetRealSymmetricDenseMatrix(n);

        Console.WriteLine("Original matrix Q:");
        Console.WriteLine(matrixQ.ToString());

        // Reduce Q using Householder reflections
        var result = 
            matrixQ.GetHouseholderReduction(1e-12);

        Console.WriteLine($"\nReduced using {result.HouseholderVectors.Count} Householder reflections:");

        foreach (var v in result.HouseholderVectors)
            Console.WriteLine("   " + v.ToString());
        
        Console.WriteLine("Reduced matrix R:");
        Console.WriteLine(result.ReducedMatrix.ToString());

        var matrixR = result.ReconstructMatrix(); 

        Console.WriteLine("Reconstructed matrix:");
        Console.WriteLine(matrixR.ToString());

        // Check error
        var error = (matrixQ - matrixR).L2Norm();
        Console.WriteLine($"Reconstruction error: {error}");
    }
    
    /// <summary>
    /// Householder reduction example for skew-symmetric square matrices
    /// </summary>
    public static void ReductionExample3()
    {
        const int n = 5;

        // Create a random square matrix via QR factorization
        var rand = new Random(1);
        var matrixQ = rand.GetRealSkewSymmetricDenseMatrix(n);

        Console.WriteLine("Original matrix Q:");
        Console.WriteLine(matrixQ.ToString());

        // Reduce Q using Householder reflections
        var result = 
            matrixQ.GetHouseholderReduction(1e-12);

        Console.WriteLine($"\nReduced using {result.HouseholderVectors.Count} Householder reflections:");

        foreach (var v in result.HouseholderVectors)
            Console.WriteLine("   " + v.ToString());
        
        Console.WriteLine("Reduced matrix R:");
        Console.WriteLine(result.ReducedMatrix.ToString());

        var matrixR = result.ReconstructMatrix(); 

        Console.WriteLine("Reconstructed matrix:");
        Console.WriteLine(matrixR.ToString());

        // Check error
        var error = (matrixQ - matrixR).L2Norm();
        Console.WriteLine($"Reconstruction error: {error}");
    }
    
    /// <summary>
    /// Householder reduction example for orthogonal square matrices
    /// </summary>
    public static void ReductionExample4()
    {
        const int n = 5;

        // Create a random square matrix via QR factorization
        var rand = new Random(1);
        var matrixQ = rand.GetOrthogonalMatrix(n);

        Console.WriteLine("Original matrix Q:");
        Console.WriteLine(matrixQ.ToString());

        // Reduce Q using Householder reflections
        var result = 
            matrixQ.GetHouseholderReduction(1e-12);

        Console.WriteLine($"\nReduced using {result.HouseholderVectors.Count} Householder reflections:");

        foreach (var v in result.HouseholderVectors)
            Console.WriteLine("   " + v.ToString());
        
        Console.WriteLine("Reduced matrix R:");
        Console.WriteLine(result.ReducedMatrix.ToString());

        var matrixR = result.ReconstructMatrix(); 

        Console.WriteLine("Reconstructed matrix:");
        Console.WriteLine(matrixR.ToString());

        // Check error
        var error = (matrixQ - matrixR).L2Norm();
        Console.WriteLine($"Reconstruction error: {error}");
    }
}