using System.Runtime.CompilerServices;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Storage;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Orthogonal;

/// <summary>
/// High-performance sparse vector orthonormalizer using Modified Gram-Schmidt 
/// with reorthogonalization and numerical stability optimizations.
/// 
/// Algorithm Complexity:
/// - Time: O(k²s) where k is number of vectors, s is average sparsity
/// - Space: O(ks) for storing the orthonormal basis
/// - Avoids O(k²d) dense operations by exploiting sparsity throughout
/// </summary>
public static class SparseVectorsOrthonormalizer
{
    /// <summary>
    /// Configuration options of orthonormalization operation
    /// </summary>
    public sealed class Config
    {
        public static Config DefaultConfig { get; } = new Config();


        /// <summary>
        /// Tolerance for considering a value as zero (default: 1e-14)
        /// </summary>
        public double ZeroTolerance { get; init; } = 1e-14;

        /// <summary>
        /// Tolerance for orthogonality checks
        /// </summary>
        public double OrthogonalityTolerance { get; init; } = 1e-14;

        /// <summary>
        /// Tolerance for linear independence
        /// </summary>
        public double LinearIndependenceTolerance { get; init; } = 1e-14;

        /// <summary>
        /// Maximum number of reorthogonalization iterations (default: 2)
        /// </summary>
        public int MaxReorthogonalizations { get; set; } 
            = 2;

        /// <summary>
        /// Whether to perform reorthogonalization (default: true)
        /// </summary>
        public bool ReorthogonalizationEnabled
            => MaxReorthogonalizations >= 1;

    }

    /// <summary>
    /// Result of orthonormalization operation
    /// </summary>
    public sealed class Result
    {
        /// <summary>
        /// The orthonormal basis vectors
        /// </summary>
        public IReadOnlyList<Vector<double>> OrthonormalBasis { get; internal set; }
            = new List<Vector<double>>();

        /// <summary>
        /// The rank (number of linearly independent vectors)
        /// </summary>
        public int Rank { get; internal set; }

        /// <summary>
        /// Indices of the original vectors that were kept in the basis
        /// </summary>
        public IReadOnlyList<int> BasisIndices { get; internal set; } = new List<int>();

        /// <summary>
        /// Maximum orthogonality error in the result
        /// </summary>
        public double MaxOrthogonalityError { get; internal set; }

        /// <summary>
        /// Whether the orthonormalization was successful
        /// </summary>
        public bool IsSuccessful { get; internal set; }
    }
    

    /// <summary>
    /// Efficiently computes inner product exploiting sparsity of both vectors.
    /// Time complexity: O(min(nnz(a), nnz(b))) where nnz is number of non-zeros.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double ComputeSparseInnerProduct(Vector<double> a, Vector<double> b)
    {
        // For maximum efficiency with sparse vectors, we iterate over non-zero elements
        if (a is SparseVector sparseA && b is SparseVector sparseB)
            return ComputeSparseInnerProductOptimized(sparseA, sparseB);

        // Fallback to standard dot product if not both sparse
        return a.DotProduct(b);
    }

    /// <summary>
    /// Optimized inner product for two sparse vectors.
    /// Uses intersection of non-zero indices for maximum efficiency.
    /// </summary>
    private static double ComputeSparseInnerProductOptimized(SparseVector a, SparseVector b)
    {
        var result = 0.0;

        // Get non-zero storage for both vectors

        if (a.Storage is not SparseVectorStorage<double> storageA || b.Storage is not SparseVectorStorage<double> storageB)
            return a.DotProduct(b); // Fallback to standard dot product

        // Iterate through the sparser vector's non-zero elements
        var (sparserStorage, denserVector) = storageA.ValueCount <= storageB.ValueCount
            ? (storageA, b)
            : (storageB, a);

        for (var i = 0; i < sparserStorage.ValueCount; i++)
        {
            var index = sparserStorage.Indices[i];
            var value = sparserStorage.Values[i];
            result += value * denserVector[index];
        }

        return result;
    }

    /// <summary>
    /// Computes the maximum orthogonality error in the basis.
    /// Returns max |&lt;b_i, b_j>| for i ≠ j, and max ||b_i|² - 1|
    /// </summary>
    private static double ComputeMaxOrthogonalityError(IReadOnlyList<Vector<double>> basis)
    {
        if (basis.Count <= 1)
            return 0.0;

        var maxError = 0.0;

        // Check orthogonality between different vectors
        for (var i = 0; i < basis.Count; i++)
        {
            // Check normalization error
            var normError = Math.Abs(basis[i].L2Norm() - 1.0);
            maxError = Math.Max(maxError, normError);

            // Check orthogonality with other vectors
            for (var j = i + 1; j < basis.Count; j++)
            {
                var innerProduct = ComputeSparseInnerProduct(basis[i], basis[j]);
                maxError = Math.Max(maxError, Math.Abs(innerProduct));
            }
        }

        return maxError;
    }
    
    /// <summary>
    /// Orthogonalizes a vector against an existing orthonormal basis using sparse operations.
    /// Implements the core of Modified Gram-Schmidt algorithm.
    /// </summary>
    private static bool OrthogonalizeVector(Vector<double> vector, IReadOnlyList<Vector<double>> basis, Config configOptions)
    {
        foreach (var basisVector in basis)
        {
            // Compute projection coefficient: <v, b_i>
            var projectionCoeff = ComputeSparseInnerProduct(vector, basisVector);

            // Subtract projection: v = v - <v, b_i> * b_i
            if (Math.Abs(projectionCoeff) > configOptions.ZeroTolerance)
            {
                // Use sparse subtraction to maintain efficiency
                SubtractScaledVector(vector, basisVector, projectionCoeff, configOptions);
            }
        }

        // Check if the vector is still linearly independent
        return vector.L2Norm() > configOptions.LinearIndependenceTolerance;
    }

    /// <summary>
    /// Performs reorthogonalization to improve numerical stability.
    /// This is particularly important for sparse vectors where small errors can accumulate.
    /// </summary>
    private static void ReorthogonalizeVector(Vector<double> vector, IReadOnlyList<Vector<double>> basis, Config configOptions)
    {
        for (var iteration = 0; iteration < configOptions.MaxReorthogonalizations; iteration++)
        {
            var maxProjection = 0.0;

            foreach (var basisVector in basis)
            {
                var projection = ComputeSparseInnerProduct(vector, basisVector);
                maxProjection = Math.Max(maxProjection, Math.Abs(projection));

                if (Math.Abs(projection) > configOptions.ZeroTolerance)
                    SubtractScaledVector(vector, basisVector, projection, configOptions);
            }

            // If projections are small enough, we're done
            if (maxProjection < configOptions.OrthogonalityTolerance)
                break;
        }
    }

    /// <summary>
    /// Efficiently subtracts a scaled vector: target = target - scale * source
    /// Exploits sparsity to minimize operations.
    /// </summary>
    private static void SubtractScaledVector(Vector<double> target, Vector<double> source, double scale, Config configOptions)
    {
        if (Math.Abs(scale) < configOptions.ZeroTolerance)
            return;

        // If source is sparse, only update non-zero elements
        if (source is SparseVector sparseSrc)
        {
            if (sparseSrc.Storage is SparseVectorStorage<double> storage)
            {
                for (var i = 0; i < storage.ValueCount; i++)
                {
                    var index = storage.Indices[i];
                    var value = storage.Values[i];
                    target[index] -= scale * value;
                }
            }
            else
            {
                // Fallback if cast fails
                target.Subtract(source.Multiply(scale), target);
            }
        }
        else
        {
            // Fallback for dense vectors
            target.Subtract(source.Multiply(scale), target);
        }
    }

    /// <summary>
    /// Checks if a vector is effectively zero within tolerance
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsEffectivelyZero(Vector<double> vector, Config configOptions)
    {
        return vector.L2Norm() <= configOptions.ZeroTolerance;
    }

    /// <summary>
    /// Projects a vector onto the subspace spanned by the given orthonormal basis
    /// </summary>
    private static Vector<double> ProjectOntoSubspace(Vector<double> vector, IReadOnlyList<Vector<double>> basis, Config configOptions)
    {
        var projection = Vector<double>.Build.Sparse(vector.Count);

        foreach (var basisVector in basis)
        {
            var coefficient = ComputeSparseInnerProduct(vector, basisVector);

            if (Math.Abs(coefficient) > configOptions.ZeroTolerance)
            {
                projection = projection.Add(basisVector.Multiply(coefficient));
            }
        }

        return projection;
    }


    /// <summary>
    /// Computes an orthonormal basis spanning the same subspace as the input sparse vectors.
    /// Uses Modified Gram-Schmidt with reorthogonalization for numerical stability.
    /// </summary>
    /// <param name="inputVectors">Collection of sparse input vectors</param>
    /// <param name="configOptions"></param>
    /// <returns>Orthonormalization result containing the basis and metadata</returns>
    public static Result Orthonormalize(this IReadOnlyList<Vector<double>> inputVectors, Config configOptions)
    {
        if (inputVectors.Count == 0)
        {
            return new Result
            {
                IsSuccessful = true,
                Rank = 0
            };
        }

        var result = new Result();
        var orthonormalBasis = new List<Vector<double>>();
        var basisIndices = new List<int>();

        try
        {
            // Process each input vector using Modified Gram-Schmidt
            for (var i = 0; i < inputVectors.Count; i++)
            {
                var currentVector = inputVectors[i];

                // Skip zero or near-zero vectors
                if (IsEffectivelyZero(currentVector, configOptions))
                    continue;

                // Create a working copy to avoid modifying the input
                var workingVector = currentVector.Clone();

                // Orthogonalize against existing basis vectors
                var isLinearlyIndependent = OrthogonalizeVector(
                    workingVector,
                    orthonormalBasis,
                    configOptions
                );

                if (!isLinearlyIndependent) continue;

                // Normalize the vector
                var norm = workingVector.L2Norm();

                if (norm <= configOptions.LinearIndependenceTolerance) continue;

                workingVector /= norm;

                // Optional reorthogonalization for numerical stability
                if (configOptions.ReorthogonalizationEnabled)
                {
                    ReorthogonalizeVector(workingVector, orthonormalBasis, configOptions);

                    // Renormalize after reorthogonalization
                    var newNorm = workingVector.L2Norm();

                    if (newNorm <= configOptions.ZeroTolerance) continue; // Vector became zero during reorthogonalization

                    workingVector /= newNorm;
                }

                orthonormalBasis.Add(workingVector);
                basisIndices.Add(i);
            }

            result.OrthonormalBasis = orthonormalBasis;
            result.Rank = orthonormalBasis.Count;
            result.BasisIndices = basisIndices;
            result.MaxOrthogonalityError = ComputeMaxOrthogonalityError(orthonormalBasis);
            result.IsSuccessful = true;
        }
        catch (Exception ex)
        {
            result.IsSuccessful = false;
            throw new InvalidOperationException($"Orthonormalization failed: {ex.Message}", ex);
        }

        return result;
    }
    

    /// <summary>
    /// Computes the reconstruction error when representing original vectors using the basis
    /// </summary>
    public static double ComputeReconstructionError(this IReadOnlyList<Vector<double>> originalVectors, IReadOnlyList<Vector<double>> basis, Config configOptions)
    {
        var maxError = 0.0;

        foreach (var original in originalVectors)
        {
            // Project onto the basis subspace
            var projection = ProjectOntoSubspace(original, basis, configOptions);

            // Compute reconstruction error
            var error = (original - projection).L2Norm();

            maxError = Math.Max(maxError, error);
        }

        return maxError;
    }
}