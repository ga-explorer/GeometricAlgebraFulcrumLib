namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.EllipseFitting;

/// <summary>
/// Comprehensive test program for the EigenDecomposition4X4 class.
/// </summary>
/// <remarks>
/// This program tests the eigenvalue decomposition implementation with various matrices including:
/// - Known analytical solutions
/// - Matrices with repeated eigenvalues
/// - Edge cases and boundary conditions
/// - Input validation scenarios
/// - Numerical stability verification
/// </remarks>
public class EigenDecomposition4X4Tests
{
    /// <summary>
    /// Main test runner that executes all test cases.
    /// </summary>
    public static void MainTests()
    {
        Console.WriteLine("=== EigenDecomposition4X4 Comprehensive Test Suite ===\n");

        try
        {
            // Run all test categories
            TestKnownMatrices();
            TestRepeatedEigenvalues();
            TestInputValidation();
            TestEdgeCases();
            TestNumericalStability();
            TestCustomParameters();

            Console.WriteLine("=== ALL TESTS COMPLETED SUCCESSFULLY ===");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ TEST SUITE FAILED: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
        }
    }

    #region Test Categories

    /// <summary>
    /// Tests matrices with known analytical eigenvalue solutions.
    /// </summary>
    private static void TestKnownMatrices()
    {
        Console.WriteLine("🧪 Testing Known Matrices...");

        // Test 1: Diagonal matrix (eigenvalues are the diagonal elements)
        var diagonalMatrix = new[,] {
            {5.0, 0.0, 0.0, 0.0},
            {0.0, 3.0, 0.0, 0.0},
            {0.0, 0.0, 2.0, 0.0},
            {0.0, 0.0, 0.0, 1.0}
        };

        TestMatrix("Diagonal Matrix", diagonalMatrix, [5.0, 3.0, 2.0, 1.0]);

        // Test 2: Simple symmetric matrix with known eigenvalues
        var simpleMatrix = new[,] {
            {4.0, 1.0, 0.0, 0.0},
            {1.0, 4.0, 0.0, 0.0},
            {0.0, 0.0, 2.0, 1.0},
            {0.0, 0.0, 1.0, 2.0}
        };

        // Expected eigenvalues: 5, 3, 3, 1 (block diagonal structure)
        TestMatrix("Block Diagonal Matrix", simpleMatrix, [5.0, 3.0, 3.0, 1.0]);

        // Test 3: Identity matrix
        var identityMatrix = new[,] {
            {1.0, 0.0, 0.0, 0.0},
            {0.0, 1.0, 0.0, 0.0},
            {0.0, 0.0, 1.0, 0.0},
            {0.0, 0.0, 0.0, 1.0}
        };

        TestMatrix("Identity Matrix", identityMatrix, [1.0, 1.0, 1.0, 1.0]);

        // Test 4: Zero matrix
        var zeroMatrix = new[,] {
            {0.0, 0.0, 0.0, 0.0},
            {0.0, 0.0, 0.0, 0.0},
            {0.0, 0.0, 0.0, 0.0},
            {0.0, 0.0, 0.0, 0.0}
        };

        TestMatrix("Zero Matrix", zeroMatrix, [0.0, 0.0, 0.0, 0.0]);

        Console.WriteLine("✅ Known matrices tests completed.\n");
    }

    /// <summary>
    /// Tests matrices with repeated eigenvalues.
    /// </summary>
    private static void TestRepeatedEigenvalues()
    {
        Console.WriteLine("🔄 Testing Repeated Eigenvalues...");

        // Test 1: Matrix with all identical eigenvalues
        var uniformMatrix = new[,] {
            {3.0, 0.0, 0.0, 0.0},
            {0.0, 3.0, 0.0, 0.0},
            {0.0, 0.0, 3.0, 0.0},
            {0.0, 0.0, 0.0, 3.0}
        };

        TestMatrix("Uniform Eigenvalues", uniformMatrix, [3.0, 3.0, 3.0, 3.0]);

        // Test 2: Matrix with double eigenvalues
        var doubleEigenMatrix = new[,] {
            {2.0, 1.0, 0.0, 0.0},
            {1.0, 2.0, 0.0, 0.0},
            {0.0, 0.0, 4.0, 0.0},
            {0.0, 0.0, 0.0, 4.0}
        };

        TestMatrix("Double Eigenvalues", doubleEigenMatrix, [4.0, 4.0, 3.0, 1.0]);

        Console.WriteLine("✅ Repeated eigenvalues tests completed.\n");
    }

    /// <summary>
    /// Tests input validation and error handling.
    /// </summary>
    private static void TestInputValidation()
    {
        Console.WriteLine("🛡️ Testing Input Validation...");

        // Test 1: Null matrix
        TestExpectedException("Null Matrix", 
            () => EigenDecomposition4X4.ComputeEigenDecomposition(null),
            typeof(ArgumentNullException));

        // Test 2: Wrong dimensions
        var wrongSizeMatrix = new double[3, 3];
        TestExpectedException("Wrong Dimensions", 
            () => EigenDecomposition4X4.ComputeEigenDecomposition(wrongSizeMatrix),
            typeof(ArgumentException));

        // Test 3: Non-symmetric matrix
        var nonSymmetricMatrix = new[,] {
            {1.0, 2.0, 0.0, 0.0},
            {3.0, 1.0, 0.0, 0.0}, // Should be 2.0 to be symmetric
            {0.0, 0.0, 1.0, 0.0},
            {0.0, 0.0, 0.0, 1.0}
        };

        TestExpectedException("Non-Symmetric Matrix", 
            () => EigenDecomposition4X4.ComputeEigenDecomposition(nonSymmetricMatrix),
            typeof(ArgumentException));

        // Test 4: Matrix with NaN
        var nanMatrix = new[,] {
            {1.0, 0.0, 0.0, 0.0},
            {0.0, double.NaN, 0.0, 0.0},
            {0.0, 0.0, 1.0, 0.0},
            {0.0, 0.0, 0.0, 1.0}
        };

        TestExpectedException("Matrix with NaN", 
            () => EigenDecomposition4X4.ComputeEigenDecomposition(nanMatrix),
            typeof(ArgumentException));

        // Test 5: Matrix with Infinity
        var infinityMatrix = new[,] {
            {1.0, 0.0, 0.0, 0.0},
            {0.0, 1.0, 0.0, 0.0},
            {0.0, 0.0, double.PositiveInfinity, 0.0},
            {0.0, 0.0, 0.0, 1.0}
        };

        TestExpectedException("Matrix with Infinity", 
            () => EigenDecomposition4X4.ComputeEigenDecomposition(infinityMatrix),
            typeof(ArgumentException));

        // Test 6: Invalid tolerance
        var validMatrix = new[,] {
            {1.0, 0.0, 0.0, 0.0},
            {0.0, 1.0, 0.0, 0.0},
            {0.0, 0.0, 1.0, 0.0},
            {0.0, 0.0, 0.0, 1.0}
        };

        TestExpectedException("Invalid Tolerance (too small)", 
            () => EigenDecomposition4X4.ComputeEigenDecomposition(validMatrix, 1e-20, 100),
            typeof(ArgumentException));

        TestExpectedException("Invalid Tolerance (too large)", 
            () => EigenDecomposition4X4.ComputeEigenDecomposition(validMatrix, 1e-5, 100),
            typeof(ArgumentException));

        // Test 7: Invalid max iterations
        TestExpectedException("Invalid Max Iterations", 
            () => EigenDecomposition4X4.ComputeEigenDecomposition(validMatrix, 1e-12, -1),
            typeof(ArgumentException));

        Console.WriteLine("✅ Input validation tests completed.\n");
    }

    /// <summary>
    /// Tests edge cases and boundary conditions.
    /// </summary>
    private static void TestEdgeCases()
    {
        Console.WriteLine("⚡ Testing Edge Cases...");

        // Test 1: Very small eigenvalues
        var smallEigenvaluesMatrix = new[,] {
            {1e-10, 0.0, 0.0, 0.0},
            {0.0, 1e-12, 0.0, 0.0},
            {0.0, 0.0, 1e-14, 0.0},
            {0.0, 0.0, 0.0, 1e-16}
        };

        TestMatrix("Small Eigenvalues", smallEigenvaluesMatrix, null, 1e-15);

        // Test 2: Large eigenvalues
        var largeEigenvaluesMatrix = new[,] {
            {1e6, 0.0, 0.0, 0.0},
            {0.0, 1e4, 0.0, 0.0},
            {0.0, 0.0, 1e2, 0.0},
            {0.0, 0.0, 0.0, 1.0}
        };

        TestMatrix("Large Eigenvalues", largeEigenvaluesMatrix,
            [1e6, 1e4, 1e2, 1.0]);

        // Test 3: Mixed positive and negative eigenvalues
        var mixedSignMatrix = new[,] {
            {2.0, 0.0, 0.0, 0.0},
            {0.0, 1.0, 0.0, 0.0},
            {0.0, 0.0, -1.0, 0.0},
            {0.0, 0.0, 0.0, -2.0}
        };

        TestMatrix("Mixed Sign Eigenvalues", mixedSignMatrix,
            [2.0, 1.0, -1.0, -2.0]);

        // Test 4: Nearly singular matrix
        var nearlySingularMatrix = new[,] {
            {1.0, 1.0, 1.0, 1.0},
            {1.0, 1.0, 1.0, 1.0},
            {1.0, 1.0, 1.0001, 1.0},  // Slight perturbation
            {1.0, 1.0, 1.0, 1.0}
        };

        TestMatrix("Nearly Singular Matrix", nearlySingularMatrix, null, 1e-10);

        Console.WriteLine("✅ Edge cases tests completed.\n");
    }

    /// <summary>
    /// Tests numerical stability and verification methods.
    /// </summary>
    private static void TestNumericalStability()
    {
        Console.WriteLine("🔬 Testing Numerical Stability...");

        // Test a well-conditioned symmetric matrix
        var testMatrix = new[,] {
            {4.0, 1.0, 2.0, 0.5},
            {1.0, 3.0, 0.0, 1.0},
            {2.0, 0.0, 2.0, 0.0},
            {0.5, 1.0, 0.0, 1.0}
        };

        var (eigenvalues, eigenvectors) = EigenDecomposition4X4.ComputeEigenDecomposition(testMatrix);

        // Verify the decomposition
        var isValid = EigenDecomposition4X4.VerifyDecomposition(
            testMatrix, eigenvalues, eigenvectors, 1e-10);

        Console.WriteLine($"   Matrix decomposition verification: {(isValid ? "✅ PASSED" : "❌ FAILED")}");

        // Additional manual verification
        Console.WriteLine("   Manual verification of A*v = λ*v:");
        for (var i = 0; i < 4; i++)
        {
            // Extract eigenvector
            var eigenvector = new double[4];
            for (var j = 0; j < 4; j++)
            {
                eigenvector[j] = eigenvectors[j, i];
            }

            // Compute A * v
            var Av = new double[4];
            for (var j = 0; j < 4; j++)
            {
                Av[j] = 0.0;
                for (var k = 0; k < 4; k++)
                {
                    Av[j] += testMatrix[j, k] * eigenvector[k];
                }
            }

            // Compute λ * v and check difference
            var maxError = 0.0;
            for (var j = 0; j < 4; j++)
            {
                var expected = eigenvalues[i] * eigenvector[j];
                var error = Math.Abs(Av[j] - expected);
                maxError = Math.Max(maxError, error);
            }

            Console.WriteLine($"     Eigenpair {i}: λ = {eigenvalues[i]:F6}, max error = {maxError:E3}");
        }

        // Test orthogonality of eigenvectors
        Console.WriteLine("   Orthogonality check:");
        for (var i = 0; i < 4; i++)
        {
            for (var j = i + 1; j < 4; j++)
            {
                var dotProduct = 0.0;
                for (var k = 0; k < 4; k++)
                {
                    dotProduct += eigenvectors[k, i] * eigenvectors[k, j];
                }
                Console.WriteLine($"     v{i} · v{j} = {dotProduct:E3}");
            }
        }

        Console.WriteLine("✅ Numerical stability tests completed.\n");
    }

    /// <summary>
    /// Tests custom parameters (tolerance and max iterations).
    /// </summary>
    private static void TestCustomParameters()
    {
        Console.WriteLine("⚙️ Testing Custom Parameters...");

        var testMatrix = new[,] {
            {3.0, 1.0, 0.0, 0.0},
            {1.0, 2.0, 0.5, 0.0},
            {0.0, 0.5, 1.0, 0.0},
            {0.0, 0.0, 0.0, 4.0}
        };

        // Test with relaxed tolerance
        try
        {
            var (eigenvalues1, eigenvectors1) = EigenDecomposition4X4.ComputeEigenDecomposition(
                testMatrix, 1e-8, 50);
            Console.WriteLine("   ✅ Custom tolerance (1e-8) test passed");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"   ❌ Custom tolerance test failed: {ex.Message}");
        }

        // Test with fewer iterations (should still converge for this matrix)
        try
        {
            var (eigenvalues2, eigenvectors2) = EigenDecomposition4X4.ComputeEigenDecomposition(
                testMatrix, 1e-10, 20);
            Console.WriteLine("   ✅ Custom max iterations (20) test passed");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"   ❌ Custom max iterations test failed: {ex.Message}");
        }

        // Test convergence failure with too few iterations
        try
        {
            var (eigenvalues3, eigenvectors3) = EigenDecomposition4X4.ComputeEigenDecomposition(
                testMatrix, 1e-15, 1);
            Console.WriteLine("   ❌ Should have failed with 1 iteration");
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("   ✅ Correctly detected convergence failure with insufficient iterations");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"   ❌ Unexpected exception: {ex.Message}");
        }

        Console.WriteLine("✅ Custom parameters tests completed.\n");
    }

    #endregion

    #region Helper Methods

    /// <summary>
    /// Tests a matrix and compares results with expected eigenvalues.
    /// </summary>
    /// <param name="testName">Name of the test for logging.</param>
    /// <param name="matrix">Matrix to decompose.</param>
    /// <param name="expectedEigenvalues">Expected eigenvalues (null to skip comparison).</param>
    /// <param name="tolerance">Tolerance for comparison.</param>
    private static void TestMatrix(string testName, double[,] matrix, double[] expectedEigenvalues = null, double tolerance = 1e-10)
    {
        Console.WriteLine($"   Testing: {testName}");

        try
        {
            var (eigenvalues, eigenvectors) = EigenDecomposition4X4.ComputeEigenDecomposition(matrix);

            Console.WriteLine($"   Computed eigenvalues: [{string.Join(", ", Array.ConvertAll(eigenvalues, x => x.ToString("F6")))}]");

            if (expectedEigenvalues != null)
            {
                Array.Sort(expectedEigenvalues, (a, b) => b.CompareTo(a)); // Sort expected in descending order
                
                var eigenvaluesMatch = true;
                for (var i = 0; i < 4; i++)
                {
                    if (Math.Abs(eigenvalues[i] - expectedEigenvalues[i]) > tolerance)
                    {
                        eigenvaluesMatch = false;
                        break;
                    }
                }

                if (eigenvaluesMatch)
                {
                    Console.WriteLine($"   ✅ Eigenvalues match expected values within tolerance {tolerance}");
                }
                else
                {
                    Console.WriteLine($"   ❌ Eigenvalues do not match expected values");
                    Console.WriteLine($"   Expected: [{string.Join(", ", Array.ConvertAll(expectedEigenvalues, x => x.ToString("F6")))}]");
                }
            }

            // Verify decomposition
            var isValid = EigenDecomposition4X4.VerifyDecomposition(matrix, eigenvalues, eigenvectors, tolerance);
            Console.WriteLine($"   Decomposition verification: {(isValid ? "✅ PASSED" : "❌ FAILED")}");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"   ❌ FAILED with exception: {ex.Message}");
        }
    }

    /// <summary>
    /// Tests that an operation throws an expected exception.
    /// </summary>
    /// <param name="testName">Name of the test for logging.</param>
    /// <param name="operation">Operation that should throw an exception.</param>
    /// <param name="expectedExceptionType">Expected exception type.</param>
    private static void TestExpectedException(string testName, Action operation, Type expectedExceptionType)
    {
        Console.WriteLine($"   Testing: {testName}");

        try
        {
            operation();
            Console.WriteLine($"   ❌ Expected {expectedExceptionType.Name} but no exception was thrown");
        }
        catch (Exception ex)
        {
            if (ex.GetType() == expectedExceptionType || ex.GetType().IsSubclassOf(expectedExceptionType))
            {
                Console.WriteLine($"   ✅ Correctly threw {ex.GetType().Name}: {ex.Message}");
            }
            else
            {
                Console.WriteLine($"   ❌ Expected {expectedExceptionType.Name} but got {ex.GetType().Name}: {ex.Message}");
            }
        }
    }

    #endregion

}