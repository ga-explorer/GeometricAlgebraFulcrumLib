using System.Runtime.CompilerServices;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Orthogonal;

/// <summary>
/// High-performance dense vector orthonormalizer using Modified Gram-Schmidt 
/// with optimized dense linear algebra operations and numerical stability enhancements.
/// 
/// Algorithm Complexity:
/// - Time: O(k²d) where k is number of vectors, d is dimension
/// - Space: O(kd) for storing the orthonormal basis
/// - Uses vectorized BLAS operations for maximum performance on dense data
/// </summary>
public static class DenseVectorsOrthonormalizer
{
    /// <summary>
    /// Configuration options for orthonormalization operation
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
        public int MaxReorthogonalizations { get; init; } 
            = 2;

        /// <summary>
        /// Whether to perform reorthogonalization (default: true)
        /// </summary>
        public bool ReorthogonalizationEnabled
            => MaxReorthogonalizations >= 1;

        /// <summary>
        /// Whether to use block-wise reorthogonalization for better numerical stability (default: true)
        /// </summary>
        public bool UseBlockReorthogonalization { get; init; } = true;

        /// <summary>
        /// Block size for block-wise operations (default: 64)
        /// </summary>
        public int BlockSize { get; init; } = 64;
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

        /// <summary>
        /// Condition number estimate of the input matrix
        /// </summary>
        public double ConditionNumberEstimate { get; internal set; }
    }

    /// <summary>
    /// Computes multiple inner products simultaneously for better cache utilization.
    /// Returns array of inner products [<vector, basis[0]>, <vector, basis[1]>, ...]
    /// </summary>
    private static double[] ComputeMultipleInnerProducts(Vector<double> vector, IReadOnlyList<Vector<double>> basis)
    {
        var projections = new double[basis.Count];
        
        // Compute all projections in one pass for better cache locality
        for (var i = 0; i < basis.Count; i++) 
            projections[i] = vector.DotProduct(basis[i]);
        
        return projections;
    }

    /// <summary>
    /// Computes the maximum orthogonality error in the basis.
    /// Returns max |<b_i, b_j>| for i ≠ j, and max ||b_i|² - 1|
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
            var normSquared = basis[i].DotProduct(basis[i]);
            var normError = Math.Abs(normSquared - 1.0);
            maxError = Math.Max(maxError, normError);

            // Check orthogonality with other vectors
            for (var j = i + 1; j < basis.Count; j++)
            {
                var innerProduct = basis[i].DotProduct(basis[j]);
                maxError = Math.Max(maxError, Math.Abs(innerProduct));
            }
        }

        return maxError;
    }

    /// <summary>
    /// Orthogonalizes a vector against an existing orthonormal basis using optimized dense operations.
    /// Implements vectorized Modified Gram-Schmidt algorithm.
    /// </summary>
    private static bool OrthogonalizeVector(Vector<double> vector, IReadOnlyList<Vector<double>> basis, Config configOptions)
    {
        if (basis.Count == 0)
            return vector.L2Norm() > configOptions.LinearIndependenceTolerance;

        // Compute all projections simultaneously for better performance
        var projections = ComputeMultipleInnerProducts(vector, basis);

        // Apply all projections: v = v - Σ <v, b_i> * b_i
        for (var i = 0; i < basis.Count; i++)
        {
            var projectionCoeff = projections[i];
            
            if (Math.Abs(projectionCoeff) > configOptions.ZeroTolerance)
            {
                // Use vectorized subtraction: vector -= projectionCoeff * basis[i]
                vector.Subtract(basis[i].Multiply(projectionCoeff), vector);
            }
        }

        // Check if the vector is still linearly independent
        return vector.L2Norm() > configOptions.LinearIndependenceTolerance;
    }

    /// <summary>
    /// Performs block-wise reorthogonalization for improved numerical stability.
    /// This approach is particularly effective for dense vectors.
    /// </summary>
    private static void BlockReorthogonalizeVector(Vector<double> vector, IReadOnlyList<Vector<double>> basis, Config configOptions)
    {
        var blockSize = Math.Min(configOptions.BlockSize, basis.Count);
        
        for (var iteration = 0; iteration < configOptions.MaxReorthogonalizations; iteration++)
        {
            var maxProjection = 0.0;

            // Process basis vectors in blocks
            for (var blockStart = 0; blockStart < basis.Count; blockStart += blockSize)
            {
                var blockEnd = Math.Min(blockStart + blockSize, basis.Count);
                var currentBlock = basis.Skip(blockStart).Take(blockEnd - blockStart).ToList();

                // Compute projections for current block
                var projections = ComputeMultipleInnerProducts(vector, currentBlock);

                // Apply block projections
                for (var i = 0; i < projections.Length; i++)
                {
                    var projection = projections[i];
                    maxProjection = Math.Max(maxProjection, Math.Abs(projection));

                    if (Math.Abs(projection) > configOptions.ZeroTolerance)
                    {
                        vector.Subtract(currentBlock[i].Multiply(projection), vector);
                    }
                }
            }

            // If projections are small enough, we're done
            if (maxProjection < configOptions.OrthogonalityTolerance)
                break;
        }
    }

    /// <summary>
    /// Performs standard reorthogonalization for numerical stability.
    /// </summary>
    private static void StandardReorthogonalizeVector(Vector<double> vector, IReadOnlyList<Vector<double>> basis, Config configOptions)
    {
        for (var iteration = 0; iteration < configOptions.MaxReorthogonalizations; iteration++)
        {
            var maxProjection = 0.0;

            foreach (var basisVector in basis)
            {
                var projection = vector.DotProduct(basisVector);
                maxProjection = Math.Max(maxProjection, Math.Abs(projection));

                if (Math.Abs(projection) > configOptions.ZeroTolerance)
                {
                    vector.Subtract(basisVector.Multiply(projection), vector);
                }
            }

            // If projections are small enough, we're done
            if (maxProjection < configOptions.OrthogonalityTolerance)
                break;
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
    /// Estimates the condition number of the current basis for numerical stability assessment
    /// </summary>
    private static double EstimateConditionNumber(IReadOnlyList<Vector<double>> basis)
    {
        if (basis.Count <= 1)
            return 1.0;

        // Simple estimate based on ratio of maximum to minimum norms
        var norms = basis.Select(v => v.L2Norm()).ToArray();
        var minNorm = norms.Min();
        var maxNorm = norms.Max();

        return minNorm > 0 ? maxNorm / minNorm : double.PositiveInfinity;
    }

    /// <summary>
    /// Projects a vector onto the subspace spanned by the given orthonormal basis
    /// </summary>
    private static Vector<double> ProjectOntoSubspace(Vector<double> vector, IReadOnlyList<Vector<double>> basis, Config configOptions)
    {
        var projection = Vector<double>.Build.Dense(vector.Count, 0.0);

        // Compute all coefficients simultaneously
        var coefficients = ComputeMultipleInnerProducts(vector, basis);

        // Apply projections: projection = Σ <v, b_i> * b_i
        for (var i = 0; i < basis.Count; i++)
        {
            var coefficient = coefficients[i];
            
            if (Math.Abs(coefficient) > configOptions.ZeroTolerance)
            {
                projection.Add(basis[i].Multiply(coefficient), projection);
            }
        }

        return projection;
    }

    
    /// <summary>
    /// Extension method for sparse vectors orthonormalization using this dense implementation
    /// </summary>
    public static Result Orthonormalize(this IReadOnlyList<Vector<double>> inputVectors) 
        => Orthonormalize(inputVectors, Config.DefaultConfig);

    /// <summary>
    /// Computes an orthonormal basis spanning the same subspace as the input dense vectors.
    /// Uses Modified Gram-Schmidt with optimized dense operations for maximum performance.
    /// </summary>
    /// <param name="inputVectors">Collection of dense input vectors</param>
    /// <param name="configOptions">Configuration options</param>
    /// <returns>Orthonormalization result containing the basis and metadata</returns>
    public static Result Orthonormalize(this IReadOnlyList<Vector<double>> inputVectors, Config configOptions)
    {
        if (inputVectors.Count == 0)
        {
            return new Result
            {
                IsSuccessful = true,
                Rank = 0,
                ConditionNumberEstimate = 1.0
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
                // Ensure it's a dense vector for optimal performance
                var workingVector = currentVector is DenseVector
                    ? currentVector.Clone()
                    : Vector<double>.Build.DenseOfVector(currentVector);

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
                    if (configOptions.UseBlockReorthogonalization)
                    {
                        BlockReorthogonalizeVector(workingVector, orthonormalBasis, configOptions);
                    }
                    else
                    {
                        StandardReorthogonalizeVector(workingVector, orthonormalBasis, configOptions);
                    }

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
            result.ConditionNumberEstimate = EstimateConditionNumber(orthonormalBasis);
            result.IsSuccessful = true;
        }
        catch (Exception ex)
        {
            result.IsSuccessful = false;
            throw new InvalidOperationException($"Dense orthonormalization failed: {ex.Message}", ex);
        }

        return result;
    }

    /// <summary>
    /// Verifies that the given vectors form an orthonormal set within tolerance
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool VerifyOrthonormality(this IReadOnlyList<Vector<double>> vectors, double tolerance = 1e-14)
    {
        var maxError = ComputeMaxOrthogonalityError(vectors);
        return maxError <= tolerance;
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