using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Orthogonal;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Samples.Algebra.LinearAlgebra.Numerical
{
    public static class DenseVectorsOrthonormalizerSamples
    {
        public static void MainSamples()
        {
            Console.WriteLine("=== Dense Vector Orthonormalization Tests ===\n");

            // Test 1: Linearly independent dense vectors
            Console.WriteLine("Test 1: Linearly Independent Dense Vectors");
            TestLinearlyIndependentVectors();
            Console.WriteLine();

            // Test 2: Partially dependent vectors
            Console.WriteLine("Test 2: Partially Dependent Dense Vectors");
            TestPartiallyDependentVectors();
            Console.WriteLine();

            // Test 3: High-dimensional dense vectors
            Console.WriteLine("Test 3: High-Dimensional Dense Vectors");
            TestHighDimensionalDenseVectors();
            Console.WriteLine();

            // Test 4: Edge cases
            Console.WriteLine("Test 4: Edge Cases");
            TestEdgeCases();
            Console.WriteLine();

            // Test 5: Numerical stability
            Console.WriteLine("Test 5: Numerical Stability Tests");
            TestNumericalStability();
            Console.WriteLine();

            // Test 6: Performance demonstration
            Console.WriteLine("Test 6: Performance Demonstration");
            TestPerformance();
            Console.WriteLine();

            Console.WriteLine("All dense vector tests completed successfully!");
        }

        private static void TestLinearlyIndependentVectors()
        {
            // Create 3 linearly independent dense vectors in R^5
            var vectors = new List<Vector<double>>
            {
                CreateDenseVector([1.0, 2.0, 0.0, 1.5, 0.5]),
                CreateDenseVector([0.0, 1.0, -1.0, 2.0, 1.0]),
                CreateDenseVector([3.0, 0.0, 1.0, -1.0, 2.0])
            };

            PrintVectors("Input vectors:", vectors);

            var result = vectors.Orthonormalize(DenseVectorsOrthonormalizer.Config.DefaultConfig);

            Console.WriteLine($"Rank: {result.Rank}");
            Console.WriteLine($"Max orthogonality error: {result.MaxOrthogonalityError:E3}");
            Console.WriteLine($"Condition number estimate: {result.ConditionNumberEstimate:F3}");
            Console.WriteLine($"Is orthonormal: {DenseVectorsOrthonormalizer.VerifyOrthonormality(result.OrthonormalBasis)}");

            PrintVectors("Orthonormal basis:", result.OrthonormalBasis);

            // Verify reconstruction
            var reconstructionError = DenseVectorsOrthonormalizer.ComputeReconstructionError(
                vectors, result.OrthonormalBasis, DenseVectorsOrthonormalizer.Config.DefaultConfig);
            Console.WriteLine($"Reconstruction error: {reconstructionError:E3}");
        }

        private static void TestPartiallyDependentVectors()
        {
            // Create vectors where some are linear combinations of others
            var v1 = CreateDenseVector([1.0, 2.0, 3.0, 1.0]);
            var v2 = CreateDenseVector([2.0, 1.0, -1.0, 1.5]);
            var v3 = v1.Multiply(2.0).Add(v2.Multiply(1.5)); // v3 = 2*v1 + 1.5*v2
            var v4 = CreateDenseVector([0.0, 0.0, 1.0, 2.0]); // Independent

            var vectors = new List<Vector<double>> { v1, v2, v3, v4 };

            PrintVectors("Input vectors (v3 = 2*v1 + 1.5*v2):", vectors);

            var result = vectors.Orthonormalize();

            Console.WriteLine($"Rank: {result.Rank} (expected: 3)");
            Console.WriteLine($"Basis indices: [{string.Join(", ", result.BasisIndices)}]");
            Console.WriteLine($"Max orthogonality error: {result.MaxOrthogonalityError:E3}");
            Console.WriteLine($"Condition number estimate: {result.ConditionNumberEstimate:F3}");
            Console.WriteLine($"Is orthonormal: {DenseVectorsOrthonormalizer.VerifyOrthonormality(result.OrthonormalBasis)}");

            PrintVectors("Orthonormal basis:", result.OrthonormalBasis);
        }

        private static void TestHighDimensionalDenseVectors()
        {
            const int dimension = 1000;
            const int numVectors = 8;

            Console.WriteLine($"Testing {numVectors} dense vectors in R^{dimension}");

            var random = new Random(42); // Fixed seed for reproducibility
            var vectors = new List<Vector<double>>();

            for (var i = 0; i < numVectors; i++)
            {
                var data = new double[dimension];
                for (var j = 0; j < dimension; j++)
                {
                    data[j] = random.NextDouble() * 2.0 - 1.0; // Random value in [-1, 1]
                }
                vectors.Add(CreateDenseVector(data));
            }

            var startTime = DateTime.Now;
            var result = vectors.Orthonormalize();
            var elapsed = DateTime.Now - startTime;

            Console.WriteLine($"Processing time: {elapsed.TotalMilliseconds:F2} ms");
            Console.WriteLine($"Rank: {result.Rank}");
            Console.WriteLine($"Max orthogonality error: {result.MaxOrthogonalityError:E3}");
            Console.WriteLine($"Condition number estimate: {result.ConditionNumberEstimate:E3}");
            Console.WriteLine($"Is orthonormal: {DenseVectorsOrthonormalizer.VerifyOrthonormality(result.OrthonormalBasis)}");

            // Memory usage estimation
            var estimatedMemoryMB = (numVectors * dimension * sizeof(double)) / (1024.0 * 1024.0);
            Console.WriteLine($"Estimated memory usage: {estimatedMemoryMB:F2} MB");
        }

        private static void TestEdgeCases()
        {
            Console.WriteLine("Testing edge cases:");

            // Test 1: Empty input
            var emptyResult = new List<Vector<double>>().Orthonormalize();
            Console.WriteLine($"Empty input - Rank: {emptyResult.Rank}, Success: {emptyResult.IsSuccessful}");

            // Test 2: Single vector
            var singleVector = new List<Vector<double>>
            {
                CreateDenseVector([3.0, 4.0, 0.0])
            };
            var singleResult = singleVector.Orthonormalize();
            Console.WriteLine($"Single vector - Rank: {singleResult.Rank}, Norm: {singleResult.OrthonormalBasis[0].L2Norm():F6}");

            // Test 3: Zero vector
            var withZeroVector = new List<Vector<double>>
            {
                CreateDenseVector([1.0, 1.0, 0.0]),
                CreateDenseVector([0.0, 0.0, 0.0]), // Zero vector
                CreateDenseVector([0.0, 1.0, 1.0])
            };
            var zeroResult = withZeroVector.Orthonormalize();
            Console.WriteLine($"With zero vector - Rank: {zeroResult.Rank} (expected: 2)");

            // Test 4: Duplicate vectors
            var v = CreateDenseVector([1.0, 2.0, 3.0]);
            var duplicateVectors = new List<Vector<double>> { v, v.Clone(), v.Clone() };
            var duplicateResult = duplicateVectors.Orthonormalize();
            Console.WriteLine($"Duplicate vectors - Rank: {duplicateResult.Rank} (expected: 1)");

            // Test 5: Nearly linearly dependent vectors
            var config = new DenseVectorsOrthonormalizer.Config
            {
                LinearIndependenceTolerance = 1e-10,
                ZeroTolerance = 1e-14
            };
            var nearlyDependentVectors = new List<Vector<double>>
            {
                CreateDenseVector([1.0, 0.0]),
                CreateDenseVector([1e-12, 1.0]) // Nearly dependent
            };
            var nearlyDependentResult = nearlyDependentVectors.Orthonormalize(config);
            Console.WriteLine($"Nearly dependent vectors - Rank: {nearlyDependentResult.Rank}");
        }

        private static void TestNumericalStability()
        {
            Console.WriteLine("Testing numerical stability with ill-conditioned matrices:");

            // Test with vectors that form an ill-conditioned matrix
            var epsilon = 1e-10;
            var vectors = new List<Vector<double>>
            {
                CreateDenseVector([1.0, 1.0, 1.0]),
                CreateDenseVector([1.0, 1.0 + epsilon, 1.0]),
                CreateDenseVector([1.0, 1.0, 1.0 + epsilon])
            };

            PrintVectors("Ill-conditioned input vectors:", vectors);

            // Test with different configurations
            var configs = new[]
            {
                new DenseVectorsOrthonormalizer.Config { UseBlockReorthogonalization = false, MaxReorthogonalizations = 1 },
                new DenseVectorsOrthonormalizer.Config { UseBlockReorthogonalization = true, MaxReorthogonalizations = 2 },
                new DenseVectorsOrthonormalizer.Config { UseBlockReorthogonalization = true, MaxReorthogonalizations = 3 }
            };

            for (var i = 0; i < configs.Length; i++)
            {
                var result = vectors.Orthonormalize(configs[i]);
                Console.WriteLine($"Config {i + 1}: Rank={result.Rank}, MaxError={result.MaxOrthogonalityError:E3}, " +
                                  $"CondNum={result.ConditionNumberEstimate:E3}");
            }
        }

        private static void TestPerformance()
        {
            Console.WriteLine("Performance comparison: Different configurations");

            var dimensions = new[] { 100, 500, 1000 };
            var vectorCounts = new[] { 5, 10, 20 };

            Console.WriteLine("Dim   | Vectors | Block Reorth | Time (ms) | Max Error | Cond Num");
            Console.WriteLine("------|---------|--------------|-----------|-----------|----------");

            foreach (var dim in dimensions)
            {
                foreach (var count in vectorCounts)
                {
                    if (count > dim) continue; // Skip impossible cases

                    var vectors = GenerateRandomDenseVectors(dim, count);

                    // Test both configurations
                    var configs = new[]
                    {
                        new DenseVectorsOrthonormalizer.Config { UseBlockReorthogonalization = false },
                        new DenseVectorsOrthonormalizer.Config { UseBlockReorthogonalization = true }
                    };

                    foreach (var config in configs)
                    {
                        var startTime = DateTime.Now;
                        var result = vectors.Orthonormalize(config);
                        var elapsed = DateTime.Now - startTime;

                        var blockStr = config.UseBlockReorthogonalization ? "Yes" : "No";

                        Console.WriteLine($"{dim,5} | {count,7} | {blockStr,12} | " +
                                          $"{elapsed.TotalMilliseconds,7:F1} | {result.MaxOrthogonalityError,7:E1} | " +
                                          $"{result.ConditionNumberEstimate,6:E1}");
                    }
                }
            }
        }

        private static Vector<double> CreateDenseVector(double[] data)
        {
            return Vector<double>.Build.DenseOfArray(data);
        }

        private static List<Vector<double>> GenerateRandomDenseVectors(int dimension, int count)
        {
            var random = new Random(42);
            var vectors = new List<Vector<double>>();

            for (var i = 0; i < count; i++)
            {
                var data = new double[dimension];
                for (var j = 0; j < dimension; j++)
                {
                    data[j] = random.NextDouble() * 2.0 - 1.0;
                }
                vectors.Add(CreateDenseVector(data));
            }

            return vectors;
        }

        private static void PrintVectors(string title, IReadOnlyList<Vector<double>> vectors, int maxElements = 8)
        {
            Console.WriteLine(title);
            for (var i = 0; i < vectors.Count; i++)
            {
                var vector = vectors[i];
                Console.Write($"  v{i}: [");

                var elements = vector
                    .Take(Math.Min(maxElements, vector.Count))
                    .Select((v, _) => $"{v:F3}")
                    .ToArray();

                Console.Write(string.Join(", ", elements));
                if (vector.Count > maxElements)
                    Console.Write(", ...");

                Console.WriteLine($"] (norm: {vector.L2Norm():F3})");
            }
        }
    }
}