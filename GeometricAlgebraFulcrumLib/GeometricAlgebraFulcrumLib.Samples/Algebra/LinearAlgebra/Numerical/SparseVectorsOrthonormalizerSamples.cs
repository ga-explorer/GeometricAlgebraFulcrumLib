using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Orthogonal;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Storage;

namespace GeometricAlgebraFulcrumLib.Samples.Algebra.LinearAlgebra.Numerical
{
    public static class SparseVectorsOrthonormalizerSamples
    {
        public static void MainSamples()
        {
            Console.WriteLine("=== Sparse Vector Orthonormalization Tests ===\n");

            // Note: SparseVectorsOrthonormalizer is a static class

            // Test 1: Linearly independent sparse vectors
            Console.WriteLine("Test 1: Linearly Independent Sparse Vectors");
            TestLinearlyIndependentVectors();
            Console.WriteLine();

            // Test 2: Partially dependent vectors
            Console.WriteLine("Test 2: Partially Dependent Vectors");
            TestPartiallyDependentVectors();
            Console.WriteLine();

            // Test 3: High-dimensional very sparse vectors
            Console.WriteLine("Test 3: High-Dimensional Very Sparse Vectors");
            TestHighDimensionalSparseVectors();
            Console.WriteLine();

            // Test 4: Edge cases
            Console.WriteLine("Test 4: Edge Cases");
            TestEdgeCases();
            Console.WriteLine();

            // Test 5: Performance demonstration
            Console.WriteLine("Test 5: Performance Demonstration");
            TestPerformance();
            Console.WriteLine();

            Console.WriteLine("All tests completed successfully!");
        }

        private static void TestLinearlyIndependentVectors()
        {
            // Create 3 linearly independent sparse vectors in R^10
            var vectors = new List<Vector<double>>
            {
                CreateSparseVector(10, [(0, 1.0), (3, 2.0), (7, 1.5)]),
                CreateSparseVector(10, [(1, 1.0), (4, -1.0), (8, 2.0)]),
                CreateSparseVector(10, [(2, 3.0), (5, 1.0), (9, -1.0)])
            };

            PrintVectors("Input vectors:", vectors);

            var result = vectors.Orthonormalize(SparseVectorsOrthonormalizer.Config.DefaultConfig);

            Console.WriteLine($"Rank: {result.Rank}");
            Console.WriteLine($"Max orthogonality error: {result.MaxOrthogonalityError:E3}");
            Console.WriteLine($"Is orthonormal: {DenseVectorsOrthonormalizer.VerifyOrthonormality(result.OrthonormalBasis)}");

            PrintVectors("Orthonormal basis:", result.OrthonormalBasis);

            // Verify reconstruction
            var reconstructionError = SparseVectorsOrthonormalizer.ComputeReconstructionError(vectors, result.OrthonormalBasis, SparseVectorsOrthonormalizer.Config.DefaultConfig);
            Console.WriteLine($"Reconstruction error: {reconstructionError:E3}");
        }

        private static void TestPartiallyDependentVectors()
        {
            // Create vectors where some are linear combinations of others
            var v1 = CreateSparseVector(8, [(0, 1.0), (2, 2.0), (5, 1.0)]);
            var v2 = CreateSparseVector(8, [(1, 1.0), (3, -1.0), (6, 1.5)]);
            var v3 = v1.Multiply(2.0).Add(v2.Multiply(1.5)); // v3 = 2*v1 + 1.5*v2
            var v4 = CreateSparseVector(8, [(4, 1.0), (7, 2.0)]); // Independent

            var vectors = new List<Vector<double>> { v1, v2, v3, v4 };

            PrintVectors("Input vectors (v3 = 2*v1 + 1.5*v2):", vectors);

            var result = vectors.Orthonormalize(SparseVectorsOrthonormalizer.Config.DefaultConfig);

            Console.WriteLine($"Rank: {result.Rank} (expected: 3)");
            Console.WriteLine($"Basis indices: [{string.Join(", ", result.BasisIndices)}]");
            Console.WriteLine($"Max orthogonality error: {result.MaxOrthogonalityError:E3}");
            Console.WriteLine($"Is orthonormal: {DenseVectorsOrthonormalizer.VerifyOrthonormality(result.OrthonormalBasis)}");

            PrintVectors("Orthonormal basis:", result.OrthonormalBasis);
        }

        private static void TestHighDimensionalSparseVectors()
        {
            const int dimension = 10000;
            const int numVectors = 5;
            const int sparsity = 20; // Only 20 non-zero elements per vector

            Console.WriteLine($"Testing {numVectors} vectors in R^{dimension} with ~{sparsity} non-zeros each");

            var random = new Random(42); // Fixed seed for reproducibility
            var vectors = new List<Vector<double>>();

            for (var i = 0; i < numVectors; i++)
            {
                var nonZeroElements = new List<(int, double)>();
                var usedIndices = new HashSet<int>();

                for (var j = 0; j < sparsity; j++)
                {
                    int index;
                    do
                    {
                        index = random.Next(dimension);
                    } while (usedIndices.Contains(index));

                    usedIndices.Add(index);
                    nonZeroElements.Add((index, random.NextDouble() * 2.0 - 1.0)); // Random value in [-1, 1]
                }

                vectors.Add(CreateSparseVector(dimension, nonZeroElements.ToArray()));
            }

            var startTime = DateTime.Now;
            var result = vectors.Orthonormalize(SparseVectorsOrthonormalizer.Config.DefaultConfig);
            var elapsed = DateTime.Now - startTime;

            Console.WriteLine($"Processing time: {elapsed.TotalMilliseconds:F2} ms");
            Console.WriteLine($"Rank: {result.Rank}");
            Console.WriteLine($"Max orthogonality error: {result.MaxOrthogonalityError:E3}");
            Console.WriteLine($"Is orthonormal: {DenseVectorsOrthonormalizer.VerifyOrthonormality(result.OrthonormalBasis)}");

            // Check sparsity preservation
            foreach (var basisVector in result.OrthonormalBasis)
            {
                if (basisVector is not SparseVector { Storage: SparseVectorStorage<double> storage }) continue;
                
                var nonZeros = storage.ValueCount;
                Console.WriteLine($"Basis vector non-zeros: {nonZeros} (sparsity: {100.0 * nonZeros / dimension:F3}%)");
            }
        }

        private static void TestEdgeCases()
        {
            Console.WriteLine("Testing edge cases:");

            // Test 1: Empty input
            var emptyResult = new List<Vector<double>>().Orthonormalize(SparseVectorsOrthonormalizer.Config.DefaultConfig);
            Console.WriteLine($"Empty input - Rank: {emptyResult.Rank}, Success: {emptyResult.IsSuccessful}");

            // Test 2: Single vector
            var singleVector = new List<Vector<double>>
            {
                CreateSparseVector(5, [(0, 3.0), (2, 4.0)])
            };
            var singleResult = singleVector.Orthonormalize(SparseVectorsOrthonormalizer.Config.DefaultConfig);
            Console.WriteLine($"Single vector - Rank: {singleResult.Rank}, Norm: {singleResult.OrthonormalBasis[0].L2Norm():F6}");

            // Test 3: Zero vector
            var withZeroVector = new List<Vector<double>>
            {
                CreateSparseVector(5, [(0, 1.0), (1, 1.0)]),
                Vector<double>.Build.Sparse(5), // Zero vector
                CreateSparseVector(5, [(2, 1.0), (3, 1.0)])
            };
            var zeroResult = withZeroVector.Orthonormalize(SparseVectorsOrthonormalizer.Config.DefaultConfig);
            Console.WriteLine($"With zero vector - Rank: {zeroResult.Rank} (expected: 2)");

            // Test 4: Duplicate vectors
            var v = CreateSparseVector(4, [(0, 1.0), (3, 2.0)]);
            var duplicateVectors = new List<Vector<double>> { v, v.Clone(), v.Clone() };
            var duplicateResult = duplicateVectors.Orthonormalize(SparseVectorsOrthonormalizer.Config.DefaultConfig);
            Console.WriteLine($"Duplicate vectors - Rank: {duplicateResult.Rank} (expected: 1)");

            // Test 5: Nearly zero vectors
            var customConfig = new SparseVectorsOrthonormalizer.Config
            {
                ZeroTolerance = 1e-10,
                LinearIndependenceTolerance = 1e-10
            };
            var nearlyZeroVectors = new List<Vector<double>>
            {
                CreateSparseVector(3, [(0, 1.0)]),
                CreateSparseVector(3, [(0, 1e-12)]) // Nearly zero
            };
            var nearlyZeroResult = nearlyZeroVectors.Orthonormalize(customConfig);
            Console.WriteLine($"Nearly zero vectors - Rank: {nearlyZeroResult.Rank} (expected: 1)");
        }

        private static void TestPerformance()
        {
            Console.WriteLine("Performance comparison: Sparse vs Dense operations");

            var dimensions = new[] { 1000, 5000, 10000 };
            var sparsityLevels = new[] { 10, 50, 100 }; // Number of non-zero elements

            foreach (var dim in dimensions)
            {
                Console.WriteLine($"\nDimension: {dim}");
                Console.WriteLine("Sparsity | Vectors | Time (ms) | Memory Efficiency");
                Console.WriteLine("---------|---------|-----------|------------------");

                foreach (var sparsity in sparsityLevels)
                {
                    var numVectors = Math.Min(10, sparsity / 2); // Reasonable number of vectors
                    if (numVectors < 2) continue;

                    var vectors = GenerateRandomSparseVectors(dim, numVectors, sparsity);

                    var startTime = DateTime.Now;
                    var result = vectors.Orthonormalize(SparseVectorsOrthonormalizer.Config.DefaultConfig);
                    var elapsed = DateTime.Now - startTime;

                    // Estimate memory efficiency (percentage of dimension that's non-zero)
                    var avgSparsity = vectors.Average(v =>
                    {
                        if (v is SparseVector sv)
                        {
                            return sv.Storage is SparseVectorStorage<double> storage ? 100.0 * storage.ValueCount / dim : 100.0;
                        }
                        return 100.0;
                    });

                    Console.WriteLine($"   {sparsity,2}    |   {numVectors,2}    | {elapsed.TotalMilliseconds,7:F1} | {avgSparsity,6:F2}%");
                }
            }
        }

        private static Vector<double> CreateSparseVector(int dimension, (int index, double value)[] nonZeroElements)
        {
            var vector = Vector<double>.Build.Sparse(dimension);
            foreach (var (index, value) in nonZeroElements)
            {
                vector[index] = value;
            }
            return vector;
        }

        private static List<Vector<double>> GenerateRandomSparseVectors(int dimension, int count, int sparsity)
        {
            var random = new Random(42);
            var vectors = new List<Vector<double>>();

            for (var i = 0; i < count; i++)
            {
                var nonZeroElements = new List<(int, double)>();
                var usedIndices = new HashSet<int>();

                for (var j = 0; j < Math.Min(sparsity, dimension); j++)
                {
                    int index;
                    do
                    {
                        index = random.Next(dimension);
                    } while (usedIndices.Contains(index));

                    usedIndices.Add(index);
                    nonZeroElements.Add((index, random.NextDouble() * 2.0 - 1.0));
                }

                vectors.Add(CreateSparseVector(dimension, nonZeroElements.ToArray()));
            }

            return vectors;
        }

        private static void PrintVectors(string title, IReadOnlyList<Vector<double>> vectors, int maxElements = 10)
        {
            Console.WriteLine(title);
            for (var i = 0; i < vectors.Count; i++)
            {
                var vector = vectors[i];
                Console.Write($"  v{i}: [");

                if (vector is SparseVector sparse)
                {
                    // Print sparse vector in compact form
                    var storage = sparse.Storage as SparseVectorStorage<double>;
                    if (storage != null && storage.ValueCount <= maxElements)
                    {
                        var elements = new List<string>();
                        for (var j = 0; j < storage.ValueCount; j++)
                        {
                            elements.Add($"{storage.Indices[j]}:{storage.Values[j]:F3}");
                        }
                        Console.Write(string.Join(", ", elements));
                    }
                    else
                    {
                        Console.Write($"sparse, nnz={storage?.ValueCount ?? 0}");
                    }
                }
                else
                {
                    // Print first few elements of dense vector
                    var elements = 
                        vector
                            .Take(Math.Min(maxElements, vector.Count))
                            .Select((v, _) => $"{v:F3}")
                            .ToArray();

                    Console.Write(string.Join(", ", elements));
                    if (vector.Count > maxElements)
                        Console.Write(", ...");
                }

                Console.WriteLine($"] (norm: {vector.L2Norm():F3})");
            }
        }
    }
}
