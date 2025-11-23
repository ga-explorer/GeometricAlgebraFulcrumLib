using System.Numerics;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.MathNetNumerics.Schur.Core;

namespace GeometricAlgebraFulcrumLib.Algebra.Utilities.MathNetNumerics.Schur.Examples
{
    /// <summary>
    /// Integration examples demonstrating practical usage within the MathNet.Numerics framework ecosystem.
    /// </summary>
    public static class IntegrationExamples
    {
        /// <summary>
        /// Example 1: Basic Schur decomposition of a real matrix
        /// </summary>
        public static void BasicRealSchurDecomposition()
        {
            Console.WriteLine("=== Basic Real Schur Decomposition ===");
            
            // Create a test matrix
            var matrixA = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(new[,]
            {
                { 4.0, -2.0,  1.0 },
                { 1.0,  1.0, -1.0 },
                { 0.0,  3.0,  2.0 }
            });
            
            Console.WriteLine($"Original matrix matrixA:\n{matrixA.ToMatrixString()}");
            
            // Compute Schur decomposition
            var schur = SchurDecomposition<double>.Compute(matrixA, computeQ: true, balance: true);
            
            Console.WriteLine($"Schur form T:\n{schur.TriangularMatrix.ToMatrixString()}");
            Console.WriteLine($"Unitary matrix Q:\n{schur.UnitaryMatrix.ToMatrixString()}");
            
            Console.WriteLine("Eigenvalues:");
            for (var i = 0; i < schur.Eigenvalues.Count; i++)
            {
                var eig = schur.Eigenvalues[i];
                Console.WriteLine($"λ_{i} = {eig.Real:F6} + {eig.Imaginary:F6}i");
            }
            
            // Verify decomposition: matrixA = Q * T * Q^T
            var reconstructed = schur.UnitaryMatrix * schur.TriangularMatrix * schur.UnitaryMatrix.Transpose();
            var error = (matrixA - reconstructed).FrobeniusNorm();
            Console.WriteLine($"Reconstruction error: {error:E6}");
            Console.WriteLine();
        }
        
        /// <summary>
        /// Example 2: Complex matrix Schur decomposition
        /// </summary>
        public static void ComplexSchurDecomposition()
        {
            Console.WriteLine("=== Complex Schur Decomposition ===");
            
            // Create a complex matrix
            var matrixA = MathNet.Numerics.LinearAlgebra.Complex.DenseMatrix.OfArray(new[,]
            {
                { new Complex(1, 2), new Complex(0, -1), new Complex(2, 0) },
                { new Complex(1, 0), new Complex(0, 3), new Complex(-1, 1) },
                { new Complex(0, 1), new Complex(1, -1), new Complex(3, 0) }
            });
            
            Console.WriteLine($"Original complex matrix matrixA:\n{matrixA.ToMatrixString()}");
            
            // Compute Schur decomposition
            var schur = SchurDecomposition<Complex>.Compute(matrixA, computeQ: true, balance: true);
            
            Console.WriteLine($"Schur form T:\n{schur.TriangularMatrix.ToMatrixString()}");
            
            Console.WriteLine("Eigenvalues:");
            for (var i = 0; i < schur.Eigenvalues.Count; i++)
            {
                var eig = schur.Eigenvalues[i];
                Console.WriteLine($"λ_{i} = {eig.Real:F6} + {eig.Imaginary:F6}i");
            }
            
            // Verify decomposition: matrixA = Q * T * Q^H
            var reconstructed = schur.UnitaryMatrix * schur.TriangularMatrix * schur.UnitaryMatrix.ConjugateTranspose();
            var error = (matrixA - reconstructed).FrobeniusNorm();
            Console.WriteLine($"Reconstruction error: {error:E6}");
            Console.WriteLine();
        }
        
        /// <summary>
        /// Example 3: Comparison with MathNet.Numerics built-in methods
        /// </summary>
        public static void ComparisonWithBuiltInMethods()
        {
            Console.WriteLine("=== Comparison with MathNet.Numerics Built-in Methods ===");
            
            // Create a test matrix
            var matrixA = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(new[,]
            {
                { 2.0, -1.0,  0.0 },
                { -1.0, 2.0, -1.0 },
                { 0.0, -1.0,  2.0 }
            });
            
            Console.WriteLine($"Test matrix matrixA:\n{matrixA.ToMatrixString()}");
            
            // Our Schur decomposition
            var ourSchur = SchurDecomposition<double>.Compute(matrixA, computeQ: true, balance: false);
            Console.WriteLine("Our Schur eigenvalues:");
            for (var i = 0; i < ourSchur.Eigenvalues.Count; i++)
            {
                var eig = ourSchur.Eigenvalues[i];
                Console.WriteLine($"λ_{i} = {eig.Real:F6} + {eig.Imaginary:F6}i");
            }
            
            // MathNet.Numerics eigenvalue computation
            var evd = matrixA.Evd();
            Console.WriteLine("MathNet.Numerics eigenvalues:");
            for (var i = 0; i < evd.EigenValues.Count; i++)
            {
                var eig = evd.EigenValues[i];
                Console.WriteLine($"λ_{i} = {eig.Real:F6} + {eig.Imaginary:F6}i");
            }
            
            Console.WriteLine();
        }
        
        /// <summary>
        /// Example 4: Stability analysis for ill-conditioned matrices
        /// </summary>
        public static void StabilityAnalysisExample()
        {
            Console.WriteLine("=== Stability Analysis for Ill-conditioned Matrices ===");
            
            // Create an ill-conditioned matrix (Hilbert matrix)
            var n = 4;
            var matrixA = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.Create(n, n, (i, j) => 1.0 / (i + j + 1));
            
            Console.WriteLine($"Hilbert matrix (ill-conditioned):\n{matrixA.ToMatrixString()}");
            Console.WriteLine($"Condition number: {matrixA.ConditionNumber():E6}");
            
            // Compute with balancing
            var schurBalanced = SchurDecomposition<double>.Compute(matrixA, computeQ: true, balance: true);
            var errorBalanced = (matrixA - schurBalanced.UnitaryMatrix * schurBalanced.TriangularMatrix * 
                               schurBalanced.UnitaryMatrix.Transpose()).FrobeniusNorm();
            
            // Compute without balancing
            var schurUnbalanced = SchurDecomposition<double>.Compute(matrixA, computeQ: true, balance: false);
            var errorUnbalanced = (matrixA - schurUnbalanced.UnitaryMatrix * schurUnbalanced.TriangularMatrix * 
                                 schurUnbalanced.UnitaryMatrix.Transpose()).FrobeniusNorm();
            
            Console.WriteLine($"Reconstruction error (with balancing):    {errorBalanced:E6}");
            Console.WriteLine($"Reconstruction error (without balancing): {errorUnbalanced:E6}");
            
            Console.WriteLine("Eigenvalues (with balancing):");
            for (var i = 0; i < schurBalanced.Eigenvalues.Count; i++)
            {
                var eig = schurBalanced.Eigenvalues[i];
                Console.WriteLine($"λ_{i} = {eig.Real:E6}");
            }
            Console.WriteLine();
        }
        
        /// <summary>
        /// Example 5: Performance comparison for large matrices
        /// </summary>
        public static void PerformanceComparisonExample()
        {
            Console.WriteLine("=== Performance Comparison ===");
            
            // Create a larger random matrix
            var random = new Random(42); // Fixed seed for reproducibility
            var n = 50;
            var matrixA = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.Create(n, n, (i, j) => random.NextDouble() * 2 - 1);
            
            Console.WriteLine($"Random {n}x{n} matrix created");
            
            // Time our implementation
            var sw = System.Diagnostics.Stopwatch.StartNew();
            var schur = SchurDecomposition<double>.Compute(matrixA, computeQ: true, balance: true);
            sw.Stop();
            
            Console.WriteLine($"Our Schur decomposition time: {sw.ElapsedMilliseconds} ms");
            
            // Verify accuracy
            var reconstructed = schur.UnitaryMatrix * schur.TriangularMatrix * schur.UnitaryMatrix.Transpose();
            var error = (matrixA - reconstructed).FrobeniusNorm();
            Console.WriteLine($"Reconstruction error: {error:E6}");
            
            // Time MathNet.Numerics eigenvalue decomposition for comparison
            sw.Restart();
            var evd = matrixA.Evd();
            sw.Stop();
            
            Console.WriteLine($"MathNet.Numerics EVD time: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine();
        }
        
        /// <summary>
        /// Example 6: Working with specialized matrix types
        /// </summary>
        public static void SpecializedMatrixTypesExample()
        {
            Console.WriteLine("=== Specialized Matrix Types ===");
            
            // Symmetric matrix
            var symmetric = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(new[,]
            {
                { 4.0, 1.0, 2.0 },
                { 1.0, 3.0, 0.5 },
                { 2.0, 0.5, 2.0 }
            });
            
            Console.WriteLine("Symmetric matrix:");
            Console.WriteLine(symmetric.ToMatrixString());
            
            var schurSym = SchurDecomposition<double>.Compute(symmetric, computeQ: true, balance: false);
            Console.WriteLine("Eigenvalues (should all be real):");
            foreach (var eig in schurSym.Eigenvalues)
            {
                Console.WriteLine($"{eig.Real:F6} + {eig.Imaginary:F6}i");
            }
            
            // Skew-symmetric matrix (purely imaginary eigenvalues)
            var skewSym = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(new[,]
            {
                {  0.0,  1.0, -2.0 },
                { -1.0,  0.0,  3.0 },
                {  2.0, -3.0,  0.0 }
            });
            
            Console.WriteLine("\nSkew-symmetric matrix:");
            Console.WriteLine(skewSym.ToMatrixString());
            
            var schurSkew = SchurDecomposition<double>.Compute(skewSym, computeQ: true, balance: false);
            Console.WriteLine("Eigenvalues (should be purely imaginary):");
            foreach (var eig in schurSkew.Eigenvalues)
            {
                Console.WriteLine($"{eig.Real:F6} + {eig.Imaginary:F6}i");
            }
            Console.WriteLine();
        }
        
        /// <summary>
        /// Example 7: Integration with MathNet.Numerics linear algebra operations
        /// </summary>
        public static void LinearAlgebraIntegrationExample()
        {
            Console.WriteLine("=== Linear Algebra Integration ===");
            
            var matrixA = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(new[,]
            {
                { 3.0, 1.0, 0.0 },
                { 0.0, 2.0, 1.0 },
                { 0.0, 0.0, 1.0 }
            });
            
            Console.WriteLine($"Matrix matrixA:\n{matrixA.ToMatrixString()}");
            
            // Compute Schur decomposition
            var schur = SchurDecomposition<double>.Compute(matrixA, computeQ: true, balance: false);
            
            // Use Schur form for matrix functions
            // For example, compute matrix square root using Schur form
            var sqrtT = schur.TriangularMatrix.Clone();
            for (var i = 0; i < sqrtT.RowCount; i++)
            {
                sqrtT[i, i] = Math.Sqrt(sqrtT[i, i]);
            }
            
            // Reconstruct matrixA^(1/2) = Q * sqrt(T) * Q^T
            var sqrtA = schur.UnitaryMatrix * sqrtT * schur.UnitaryMatrix.Transpose();
            
            Console.WriteLine($"Matrix square root:\n{sqrtA.ToMatrixString()}");
            
            // Verify: (matrixA^(1/2))^2 should equal matrixA
            var verification = sqrtA * sqrtA;
            var error = (matrixA - verification).FrobeniusNorm();
            Console.WriteLine($"Square root verification error: {error:E6}");
            Console.WriteLine();
        }
        
        /// <summary>
        /// Run all examples
        /// </summary>
        public static void RunAllExamples()
        {
            BasicRealSchurDecomposition();
            try
            {
                BasicRealSchurDecomposition();
                ComplexSchurDecomposition();
                ComparisonWithBuiltInMethods();
                StabilityAnalysisExample();
                PerformanceComparisonExample();
                SpecializedMatrixTypesExample();
                LinearAlgebraIntegrationExample();
                
                Console.WriteLine("All examples completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error running examples: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}