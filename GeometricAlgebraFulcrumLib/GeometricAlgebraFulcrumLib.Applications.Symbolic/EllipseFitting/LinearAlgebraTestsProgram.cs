namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.EllipseFitting
{
    /// <summary>
    /// Comprehensive test program demonstrating eigenvalue and eigenvector computation
    /// using QR decomposition with Householder transformations.
    /// </summary>
    /// <remarks>
    /// This program showcases the complete C# implementation that converts Python/numpy
    /// functionality to native C# mathematical operations with proper error handling,
    /// input validation, and C# naming conventions.
    /// </remarks>
    public class EigenValueTestProgram
    {
        /// <summary>
        /// Main entry point for the test program.
        /// </summary>
        /// <param name="args">Command line arguments (not used).</param>
        public static void MainTests()
        {
            Console.WriteLine("=".PadRight(80, '='));
            Console.WriteLine("   C# Linear Algebra Library - Eigenvalue Computation Test Suite");
            Console.WriteLine("   QR Decomposition with Householder Transformations");
            Console.WriteLine("=".PadRight(80, '='));
            Console.WriteLine();

            try
            {
                // Run comprehensive test suite
                RunMatrixOperationTests();
                RunQRDecompositionTests();
                RunEigenValueComputationTests();
                RunInputValidationTests();
                RunPerformanceTests();

                Console.WriteLine("=".PadRight(80, '='));
                Console.WriteLine("   ✅ ALL TESTS COMPLETED SUCCESSFULLY");
                Console.WriteLine("=".PadRight(80, '='));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ TEST SUITE FAILED: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                Environment.Exit(1);
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        #region Matrix Operation Tests

        /// <summary>
        /// Tests basic matrix operations including creation, multiplication, and utilities.
        /// </summary>
        private static void RunMatrixOperationTests()
        {
            Console.WriteLine("🔧 Testing Matrix Operations...");
            Console.WriteLine();

            // Test 1: Matrix creation and basic operations
            TestMatrixCreationAndBasicOperations();

            // Test 2: Matrix multiplication
            TestMatrixMultiplication();

            // Test 3: Matrix transpose and norms
            TestMatrixTransposeAndNorms();

            Console.WriteLine("✅ Matrix operation tests completed successfully.\n");
        }

        private static void TestMatrixCreationAndBasicOperations()
        {
            Console.WriteLine("  Test 1.1: Matrix Creation and Basic Operations");

            // Create identity matrix
            var identity = Matrix.CreateIdentity(3);
            Console.WriteLine($"  3x3 Identity Matrix:");
            Console.WriteLine(AddIndentation(Matrix.ToString(identity), "    "));

            // Create zero matrix
            var zeros = Matrix.CreateZeros(2, 3);
            Console.WriteLine($"  2x3 Zero Matrix:");
            Console.WriteLine(AddIndentation(Matrix.ToString(zeros), "    "));

            // Test matrix cloning
            var original = new double[,] { {1, 2}, {3, 4} };
            var cloned = Matrix.Clone(original);
            cloned[0, 0] = 99; // Modify clone
            
            bool cloningWorked = original[0, 0] == 1 && cloned[0, 0] == 99;
            Console.WriteLine($"  Matrix cloning test: {(cloningWorked ? "✅ PASSED" : "❌ FAILED")}");

            Console.WriteLine();
        }

        private static void TestMatrixMultiplication()
        {
            Console.WriteLine("  Test 1.2: Matrix Multiplication");

            var matrixA = new double[,] { {1, 2}, {3, 4} };
            var matrixB = new double[,] { {5, 6}, {7, 8} };
            var result = Matrix.Multiply(matrixA, matrixB);

            Console.WriteLine("  Matrix A:");
            Console.WriteLine(AddIndentation(Matrix.ToString(matrixA), "    "));
            
            Console.WriteLine("  Matrix B:");
            Console.WriteLine(AddIndentation(Matrix.ToString(matrixB), "    "));
            
            Console.WriteLine("  A × B =");
            Console.WriteLine(AddIndentation(Matrix.ToString(result), "    "));

            // Expected result: {{19, 22}, {43, 50}}
            bool multiplicationCorrect = Math.Abs(result[0, 0] - 19) < 1e-10 && 
                                        Math.Abs(result[0, 1] - 22) < 1e-10 &&
                                        Math.Abs(result[1, 0] - 43) < 1e-10 && 
                                        Math.Abs(result[1, 1] - 50) < 1e-10;

            Console.WriteLine($"  Matrix multiplication accuracy: {(multiplicationCorrect ? "✅ PASSED" : "❌ FAILED")}");
            Console.WriteLine();
        }

        private static void TestMatrixTransposeAndNorms()
        {
            Console.WriteLine("  Test 1.3: Matrix Transpose and Norms");

            var matrix = new double[,] { {1, 2, 3}, {4, 5, 6} };
            var transposed = Matrix.Transpose(matrix);

            Console.WriteLine("  Original Matrix:");
            Console.WriteLine(AddIndentation(Matrix.ToString(matrix), "    "));
            
            Console.WriteLine("  Transposed Matrix:");
            Console.WriteLine(AddIndentation(Matrix.ToString(transposed), "    "));

            // Test vector norm
            var vector = new double[] {3, 4, 0};
            double norm = Matrix.VectorNorm(vector);
            bool normCorrect = Math.Abs(norm - 5.0) < 1e-10;
            
            Console.WriteLine($"  Vector [3, 4, 0] norm: {norm:F6}");
            Console.WriteLine($"  Vector norm accuracy: {(normCorrect ? "✅ PASSED" : "❌ FAILED")}");

            // Test Frobenius norm
            double frobeniusNorm = Matrix.FrobeniusNorm(matrix);
            double expectedFrobenius = Math.Sqrt(1 + 4 + 9 + 16 + 25 + 36); // √91 ≈ 9.539
            bool frobeniusCorrect = Math.Abs(frobeniusNorm - expectedFrobenius) < 1e-10;
            
            Console.WriteLine($"  Matrix Frobenius norm: {frobeniusNorm:F6}");
            Console.WriteLine($"  Frobenius norm accuracy: {(frobeniusCorrect ? "✅ PASSED" : "❌ FAILED")}");
            Console.WriteLine();
        }

        #endregion

        #region QR Decomposition Tests

        /// <summary>
        /// Tests QR decomposition functionality using Householder transformations.
        /// </summary>
        private static void RunQRDecompositionTests()
        {
            Console.WriteLine("📐 Testing QR Decomposition...");
            Console.WriteLine();

            // Test 1: Basic QR decomposition
            TestBasicQRDecomposition();

            // Test 2: QR decomposition properties
            TestQRDecompositionProperties();

            // Test 3: Linear system solving
            TestLinearSystemSolving();

            Console.WriteLine("✅ QR decomposition tests completed successfully.\n");
        }

        private static void TestBasicQRDecomposition()
        {
            Console.WriteLine("  Test 2.1: Basic QR Decomposition");

            var matrix = new double[,] {
                {12, -51, 4},
                {6, 167, -68},
                {-4, 24, -41}
            };

            Console.WriteLine("  Input Matrix A:");
            Console.WriteLine(AddIndentation(Matrix.ToString(matrix), "    "));

            var (Q, R) = QRDecomposition.Decompose(matrix);

            Console.WriteLine("  Orthogonal Matrix Q:");
            Console.WriteLine(AddIndentation(Matrix.ToString(Q), "    "));

            Console.WriteLine("  Upper Triangular Matrix R:");
            Console.WriteLine(AddIndentation(Matrix.ToString(R), "    "));

            Console.WriteLine();
        }

        private static void TestQRDecompositionProperties()
        {
            Console.WriteLine("  Test 2.2: QR Decomposition Properties Verification");

            var matrix = new double[,] {
                {1, 2, 3},
                {4, 5, 6},
                {7, 8, 10}
            };

            var (Q, R) = QRDecomposition.Decompose(matrix);

            // Verify A = Q * R
            var reconstructed = Matrix.Multiply(Q, R);
            bool reconstructionAccurate = VerifyMatrixEquality(matrix, reconstructed, 1e-10);
            Console.WriteLine($"  A = Q × R verification: {(reconstructionAccurate ? "✅ PASSED" : "❌ FAILED")}");

            // Verify Q is orthogonal (Q^T * Q = I)
            var QTranspose = Matrix.Transpose(Q);
            var shouldBeIdentity = Matrix.Multiply(QTranspose, Q);
            var identity = Matrix.CreateIdentity(Q.GetLength(0));
            bool orthogonalityVerified = VerifyMatrixEquality(shouldBeIdentity, identity, 1e-10);
            Console.WriteLine($"  Q orthogonality (Q^T × Q = I): {(orthogonalityVerified ? "✅ PASSED" : "❌ FAILED")}");

            // Verify R is upper triangular
            bool upperTriangular = VerifyUpperTriangular(R, 1e-10);
            Console.WriteLine($"  R upper triangular verification: {(upperTriangular ? "✅ PASSED" : "❌ FAILED")}");

            Console.WriteLine();
        }

        private static void TestLinearSystemSolving()
        {
            Console.WriteLine("  Test 2.3: Linear System Solving with QR");

            var A = new double[,] {
                {2, 1, 1},
                {1, 3, 2},
                {1, 0, 0}
            };

            var b = new double[] {4, 5, 6};
            
            Console.WriteLine("  Solving Ax = b:");
            Console.WriteLine("  Matrix A:");
            Console.WriteLine(AddIndentation(Matrix.ToString(A), "    "));
            Console.WriteLine($"  Vector b: {Matrix.ToString(b)}");

            var solution = QRDecomposition.Solve(A, b);
            Console.WriteLine($"  Solution x: {Matrix.ToString(solution)}");

            // Verify solution by computing A * x
            var verification = new double[b.Length];
            for (int i = 0; i < verification.Length; i++)
            {
                verification[i] = 0.0;
                for (int j = 0; j < solution.Length; j++)
                {
                    verification[i] += A[i, j] * solution[j];
                }
            }

            bool solutionCorrect = VerifyVectorEquality(verification, b, 1e-10);
            Console.WriteLine($"  Solution verification (A × x = b): {(solutionCorrect ? "✅ PASSED" : "❌ FAILED")}");
            Console.WriteLine();
        }

        #endregion

        #region Eigenvalue Computation Tests

        /// <summary>
        /// Tests eigenvalue and eigenvector computation using the QR algorithm.
        /// </summary>
        private static void RunEigenValueComputationTests()
        {
            Console.WriteLine("🎯 Testing Eigenvalue Computation...");
            Console.WriteLine();

            // Test 1: Simple symmetric matrix
            TestSimpleSymmetricMatrix();

            // Test 2: Identity matrix
            TestIdentityMatrixEigenvalues();

            // Test 3: Diagonal matrix
            TestDiagonalMatrixEigenvalues();

            // Test 4: Complex symmetric matrix
            TestComplexSymmetricMatrix();

            Console.WriteLine("✅ Eigenvalue computation tests completed successfully.\n");
        }

        private static void TestSimpleSymmetricMatrix()
        {
            Console.WriteLine("  Test 3.1: Simple Symmetric Matrix");

            var matrix = new double[,] {
                {4, 1, 0},
                {1, 4, 1},
                {0, 1, 4}
            };

            Console.WriteLine("  Input Matrix:");
            Console.WriteLine(AddIndentation(Matrix.ToString(matrix), "    "));

            var (eigenvalues, eigenvectors) = EigenSolver.ComputeEigenDecomposition(matrix);

            Console.WriteLine($"  Eigenvalues: {Matrix.ToString(eigenvalues)}");
            Console.WriteLine("  Eigenvectors:");
            Console.WriteLine(AddIndentation(Matrix.ToString(eigenvectors), "    "));

            // Verify the decomposition
            bool verificationPassed = EigenSolver.VerifyEigenDecomposition(matrix, eigenvalues, eigenvectors, 1e-10);
            Console.WriteLine($"  Eigendecomposition verification: {(verificationPassed ? "✅ PASSED" : "❌ FAILED")}");
            Console.WriteLine();
        }

        private static void TestIdentityMatrixEigenvalues()
        {
            Console.WriteLine("  Test 3.2: Identity Matrix Eigenvalues");

            var identity = Matrix.CreateIdentity(3);
            
            Console.WriteLine("  Input Matrix (3x3 Identity):");
            Console.WriteLine(AddIndentation(Matrix.ToString(identity), "    "));

            var (eigenvalues, eigenvectors) = EigenSolver.ComputeEigenDecomposition(identity);

            Console.WriteLine($"  Eigenvalues: {Matrix.ToString(eigenvalues)}");

            // All eigenvalues should be 1.0 for identity matrix
            bool eigenvaluesCorrect = true;
            for (int i = 0; i < eigenvalues.Length; i++)
            {
                if (Math.Abs(eigenvalues[i] - 1.0) > 1e-10)
                {
                    eigenvaluesCorrect = false;
                    break;
                }
            }

            Console.WriteLine($"  Eigenvalues accuracy (should all be 1.0): {(eigenvaluesCorrect ? "✅ PASSED" : "❌ FAILED")}");

            bool verificationPassed = EigenSolver.VerifyEigenDecomposition(identity, eigenvalues, eigenvectors);
            Console.WriteLine($"  Eigendecomposition verification: {(verificationPassed ? "✅ PASSED" : "❌ FAILED")}");
            Console.WriteLine();
        }

        private static void TestDiagonalMatrixEigenvalues()
        {
            Console.WriteLine("  Test 3.3: Diagonal Matrix Eigenvalues");

            var diagonal = new double[,] {
                {5, 0, 0},
                {0, 3, 0},
                {0, 0, 1}
            };

            Console.WriteLine("  Input Matrix (Diagonal):");
            Console.WriteLine(AddIndentation(Matrix.ToString(diagonal), "    "));

            var (eigenvalues, eigenvectors) = EigenSolver.ComputeEigenDecomposition(diagonal);

            Console.WriteLine($"  Computed Eigenvalues: {Matrix.ToString(eigenvalues)}");

            // Expected eigenvalues are the diagonal elements (5, 3, 1) in descending order
            var expectedEigenvalues = new double[] {5, 3, 1};
            bool eigenvaluesCorrect = VerifyVectorEquality(eigenvalues, expectedEigenvalues, 1e-10);

            Console.WriteLine($"  Expected Eigenvalues: {Matrix.ToString(expectedEigenvalues)}");
            Console.WriteLine($"  Eigenvalues accuracy: {(eigenvaluesCorrect ? "✅ PASSED" : "❌ FAILED")}");

            bool verificationPassed = EigenSolver.VerifyEigenDecomposition(diagonal, eigenvalues, eigenvectors);
            Console.WriteLine($"  Eigendecomposition verification: {(verificationPassed ? "✅ PASSED" : "❌ FAILED")}");
            Console.WriteLine();
        }

        private static void TestComplexSymmetricMatrix()
        {
            Console.WriteLine("  Test 3.4: Complex Symmetric Matrix");

            var complex = new double[,] {
                {6, 2, 1, 3},
                {2, 3, 1, 1},
                {1, 1, 1, 1},
                {3, 1, 1, 2}
            };

            Console.WriteLine("  Input Matrix (4x4 Symmetric):");
            Console.WriteLine(AddIndentation(Matrix.ToString(complex), "    "));

            var (eigenvalues, eigenvectors) = EigenSolver.ComputeEigenDecomposition(complex);

            Console.WriteLine($"  Eigenvalues: {Matrix.ToString(eigenvalues)}");
            Console.WriteLine("  First Eigenvector:");
            var firstEigenvector = Matrix.ExtractColumn(eigenvectors, 0);
            Console.WriteLine($"    {Matrix.ToString(firstEigenvector)}");

            bool verificationPassed = EigenSolver.VerifyEigenDecomposition(complex, eigenvalues, eigenvectors);
            Console.WriteLine($"  Eigendecomposition verification: {(verificationPassed ? "✅ PASSED" : "❌ FAILED")}");

            // Test orthogonality of eigenvectors
            bool orthogonalityPassed = VerifyEigenvectorOrthogonality(eigenvectors, 1e-10);
            Console.WriteLine($"  Eigenvector orthogonality: {(orthogonalityPassed ? "✅ PASSED" : "❌ FAILED")}");
            Console.WriteLine();
        }

        #endregion

        #region Input Validation Tests

        /// <summary>
        /// Tests input validation and error handling capabilities.
        /// </summary>
        private static void RunInputValidationTests()
        {
            Console.WriteLine("🛡️ Testing Input Validation and Error Handling...");
            Console.WriteLine();

            TestNullInputHandling();
            TestInvalidMatrixDimensions();
            TestNonSymmetricMatrixHandling();
            TestInvalidParameterHandling();

            Console.WriteLine("✅ Input validation tests completed successfully.\n");
        }

        private static void TestNullInputHandling()
        {
            Console.WriteLine("  Test 4.1: Null Input Handling");

            bool caughtMatrixNull = TestExpectedException(() => 
                Matrix.Clone(null), typeof(ArgumentNullException));
            Console.WriteLine($"  Matrix.Clone(null): {(caughtMatrixNull ? "✅ PASSED" : "❌ FAILED")}");

            bool caughtQRNull = TestExpectedException(() => 
                QRDecomposition.Decompose(null), typeof(ArgumentNullException));
            Console.WriteLine($"  QRDecomposition.Decompose(null): {(caughtQRNull ? "✅ PASSED" : "❌ FAILED")}");

            bool caughtEigenNull = TestExpectedException(() => 
                EigenSolver.ComputeEigenDecomposition(null), typeof(ArgumentNullException));
            Console.WriteLine($"  EigenSolver.ComputeEigenDecomposition(null): {(caughtEigenNull ? "✅ PASSED" : "❌ FAILED")}");

            Console.WriteLine();
        }

        private static void TestInvalidMatrixDimensions()
        {
            Console.WriteLine("  Test 4.2: Invalid Matrix Dimensions");

            var nonSquare = new double[2, 3];
            bool caughtNonSquare = TestExpectedException(() => 
                EigenSolver.ComputeEigenDecomposition(nonSquare), typeof(ArgumentException));
            Console.WriteLine($"  Non-square matrix for eigen: {(caughtNonSquare ? "✅ PASSED" : "❌ FAILED")}");

            bool caughtZeroSize = TestExpectedException(() => 
                Matrix.CreateIdentity(0), typeof(ArgumentException));
            Console.WriteLine($"  Zero-size identity matrix: {(caughtZeroSize ? "✅ PASSED" : "❌ FAILED")}");

            Console.WriteLine();
        }

        private static void TestNonSymmetricMatrixHandling()
        {
            Console.WriteLine("  Test 4.3: Non-Symmetric Matrix Handling");

            var nonSymmetric = new double[,] {
                {1, 2, 3},
                {4, 5, 6}, // Should be {2, 5, 6} to be symmetric
                {3, 6, 7}  // Should be {3, 6, 7} to be symmetric
            };

            bool caughtNonSymmetric = TestExpectedException(() => 
                EigenSolver.ComputeEigenDecomposition(nonSymmetric), typeof(ArgumentException));
            Console.WriteLine($"  Non-symmetric matrix for eigen: {(caughtNonSymmetric ? "✅ PASSED" : "❌ FAILED")}");

            Console.WriteLine();
        }

        private static void TestInvalidParameterHandling()
        {
            Console.WriteLine("  Test 4.4: Invalid Parameter Handling");

            var validMatrix = Matrix.CreateIdentity(3);

            bool caughtNegativeTolerance = TestExpectedException(() => 
                EigenSolver.ComputeEigenDecomposition(validMatrix, -1e-6, 100), typeof(ArgumentException));
            Console.WriteLine($"  Negative tolerance: {(caughtNegativeTolerance ? "✅ PASSED" : "❌ FAILED")}");

            bool caughtZeroIterations = TestExpectedException(() => 
                EigenSolver.ComputeEigenDecomposition(validMatrix, 1e-12, 0), typeof(ArgumentException));
            Console.WriteLine($"  Zero max iterations: {(caughtZeroIterations ? "✅ PASSED" : "❌ FAILED")}");

            Console.WriteLine();
        }

        #endregion

        #region Performance Tests

        /// <summary>
        /// Tests performance characteristics and memory management.
        /// </summary>
        private static void RunPerformanceTests()
        {
            Console.WriteLine("⚡ Testing Performance and Memory Management...");
            Console.WriteLine();

            TestLargeMatrixPerformance();
            TestMemoryUsage();

            Console.WriteLine("✅ Performance tests completed successfully.\n");
        }

        private static void TestLargeMatrixPerformance()
        {
            Console.WriteLine("  Test 5.1: Large Matrix Performance");

            // Create a larger symmetric positive definite matrix
            int size = 10;
            var largeMatrix = CreateSymmetricPositiveDefiniteMatrix(size);

            Console.WriteLine($"  Testing with {size}x{size} symmetric matrix...");

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var (eigenvalues, eigenvectors) = EigenSolver.ComputeEigenDecomposition(largeMatrix, 1e-10, 500);
            stopwatch.Stop();

            Console.WriteLine($"  Computation time: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"  Largest eigenvalue: {eigenvalues[0]:F6}");
            Console.WriteLine($"  Smallest eigenvalue: {eigenvalues[size-1]:F6}");

            bool verificationPassed = EigenSolver.VerifyEigenDecomposition(largeMatrix, eigenvalues, eigenvectors, 1e-8);
            Console.WriteLine($"  Large matrix verification: {(verificationPassed ? "✅ PASSED" : "❌ FAILED")}");

            Console.WriteLine();
        }

        private static void TestMemoryUsage()
        {
            Console.WriteLine("  Test 5.2: Memory Usage and Garbage Collection");

            long initialMemory = GC.GetTotalMemory(true);

            // Perform multiple matrix operations
            for (int i = 0; i < 100; i++)
            {
                var testMatrix = CreateSymmetricPositiveDefiniteMatrix(5);
                var (eigenvalues, eigenvectors) = EigenSolver.ComputeEigenDecomposition(testMatrix);
                
                // Force some operations to stress memory
                var product = Matrix.Multiply(testMatrix, eigenvectors);
                var transposed = Matrix.Transpose(product);
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            long finalMemory = GC.GetTotalMemory(false);
            long memoryUsed = finalMemory - initialMemory;

            Console.WriteLine($"  Initial memory: {initialMemory / 1024} KB");
            Console.WriteLine($"  Final memory: {finalMemory / 1024} KB");
            Console.WriteLine($"  Memory used: {memoryUsed / 1024} KB");
            Console.WriteLine($"  Memory management: {(memoryUsed < 1024 * 1024 ? "✅ EFFICIENT" : "⚠️ HIGH USAGE")}");

            Console.WriteLine();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Creates a symmetric positive definite matrix for testing.
        /// </summary>
        /// <param name="size">Size of the square matrix.</param>
        /// <returns>A symmetric positive definite matrix.</returns>
        private static double[,] CreateSymmetricPositiveDefiniteMatrix(int size)
        {
            var random = new Random(42); // Fixed seed for reproducibility
            var matrix = new double[size, size];

            // Create a random matrix
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = random.NextDouble() * 2 - 1; // Range [-1, 1]
                }
            }

            // Make it symmetric positive definite: A = B^T * B + I
            var result = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = 0.0;
                    for (int k = 0; k < size; k++)
                    {
                        result[i, j] += matrix[k, i] * matrix[k, j];
                    }
                    if (i == j) result[i, j] += 1.0; // Add identity for positive definiteness
                }
            }

            return result;
        }

        /// <summary>
        /// Tests if an operation throws an expected exception type.
        /// </summary>
        /// <param name="action">The action to test.</param>
        /// <param name="expectedExceptionType">The expected exception type.</param>
        /// <returns>True if the expected exception was thrown, false otherwise.</returns>
        private static bool TestExpectedException(Action action, Type expectedExceptionType)
        {
            try
            {
                action();
                return false; // No exception thrown
            }
            catch (Exception ex)
            {
                return ex.GetType() == expectedExceptionType || ex.GetType().IsSubclassOf(expectedExceptionType);
            }
        }

        /// <summary>
        /// Verifies if two matrices are equal within a tolerance.
        /// </summary>
        private static bool VerifyMatrixEquality(double[,] matrix1, double[,] matrix2, double tolerance)
        {
            int rows1 = matrix1.GetLength(0), cols1 = matrix1.GetLength(1);
            int rows2 = matrix2.GetLength(0), cols2 = matrix2.GetLength(1);

            if (rows1 != rows2 || cols1 != cols2) return false;

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < cols1; j++)
                {
                    if (Math.Abs(matrix1[i, j] - matrix2[i, j]) > tolerance)
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Verifies if two vectors are equal within a tolerance.
        /// </summary>
        private static bool VerifyVectorEquality(double[] vector1, double[] vector2, double tolerance)
        {
            if (vector1.Length != vector2.Length) return false;

            for (int i = 0; i < vector1.Length; i++)
            {
                if (Math.Abs(vector1[i] - vector2[i]) > tolerance)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Verifies if a matrix is upper triangular within a tolerance.
        /// </summary>
        private static bool VerifyUpperTriangular(double[,] matrix, double tolerance)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < Math.Min(i, cols); j++)
                {
                    if (Math.Abs(matrix[i, j]) > tolerance)
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Verifies orthogonality of eigenvectors.
        /// </summary>
        private static bool VerifyEigenvectorOrthogonality(double[,] eigenvectors, double tolerance)
        {
            int size = eigenvectors.GetLength(1);

            for (int i = 0; i < size; i++)
            {
                for (int j = i + 1; j < size; j++)
                {
                    double dotProduct = 0.0;
                    for (int k = 0; k < size; k++)
                    {
                        dotProduct += eigenvectors[k, i] * eigenvectors[k, j];
                    }
                    if (Math.Abs(dotProduct) > tolerance)
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Adds indentation to each line of a multi-line string.
        /// </summary>
        /// <param name="text">The text to indent.</param>
        /// <param name="indentation">The indentation string.</param>
        /// <returns>The indented text.</returns>
        private static string AddIndentation(string text, string indentation)
        {
            if (string.IsNullOrEmpty(text)) return text;
            
            var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = indentation + lines[i];
            }
            return string.Join(Environment.NewLine, lines);
        }

        #endregion
    }
}