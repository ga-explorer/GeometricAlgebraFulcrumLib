using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Subspaces;

/// <summary>
/// High-performance sparse vector orthonormalizer using Modified Gram-Schmidt 
/// with reorthogonalization and numerical stability optimizations.
/// 
/// Algorithm Complexity:
/// - Time: O(k²s) where k is number of vectors, s is average sparsity
/// - Space: O(ks) for storing the orthonormal basis
/// - Avoids O(k²d) dense operations by exploiting sparsity throughout
/// </summary>
public sealed class XGaFloat64VectorListOrthonormalizer
{
    /// <summary>
    /// Result of orthonormalization operation
    /// </summary>
    public sealed class Result
    {
        /// <summary>
        /// The orthonormal basis vectors
        /// </summary>
        public IList<XGaFloat64Vector> OrthonormalBasis { get; set; }
            = new List<XGaFloat64Vector>();

        /// <summary>
        /// The rank (number of linearly independent vectors)
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// Indices of the original vectors that were kept in the basis
        /// </summary>
        public IList<int> BasisIndices { get; set; } = new List<int>();

        /// <summary>
        /// Maximum orthogonality error in the result
        /// </summary>
        public double MaxOrthogonalityError { get; set; }

        /// <summary>
        /// Whether the orthonormalization was successful
        /// </summary>
        public bool IsSuccessful { get; set; }
    }


    /// <summary>
    /// Apply Euclidean orthonormality
    /// </summary>
    public bool Euclidean { get; set; } = true;

    /// <summary>
    /// Tolerance for considering a value as zero (default: 1e-14)
    /// </summary>
    public double ZeroTolerance { get; set; } = 1e-14;

    /// <summary>
    /// Tolerance for orthogonality checks (default: 1e-12)
    /// </summary>
    public double OrthogonalityTolerance { get; set; } = 1e-12;

    /// <summary>
    /// Tolerance for linear independence (default: 1e-12)
    /// </summary>
    public double LinearIndependenceTolerance { get; set; } = 1e-12;

    /// <summary>
    /// Maximum number of reorthogonalization iterations (default: 2)
    /// </summary>
    public int MaxReorthogonalizations { get; set; } = 2;

    /// <summary>
    /// Whether to perform reorthogonalization (default: true)
    /// </summary>
    public bool ReorthogonalizationEnabled
        => MaxReorthogonalizations >= 1;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private double InnerProduct(XGaFloat64Vector a, XGaFloat64Vector b)
    {
        return Euclidean ? a.ESp(b) : a.Sp(b);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private double Norm(XGaFloat64Vector a)
    {
        return Euclidean ? a.ENorm() : a.Norm();
    }

    
    /// <summary>
    /// Orthogonalizes a vector against an existing orthonormal basis using sparse operations.
    /// Implements the core of Modified Gram-Schmidt algorithm.
    /// </summary>
    private (bool isLinearlyIndependent, XGaFloat64Vector orthoVector) OrthogonalizeVector(XGaFloat64Vector vector, IList<XGaFloat64Vector> basis)
    {
        foreach (var basisVector in basis)
        {
            // Compute projection coefficient: <v, b_i>
            var projectionCoeff = InnerProduct(vector, basisVector);

            // Subtract projection: v = v - <v, b_i> * b_i
            if (Math.Abs(projectionCoeff) > ZeroTolerance) 
                vector -= basisVector.Times(projectionCoeff);
        }

        // Check if the vector is still linearly independent
        var isLinearlyIndependent = Norm(vector) > LinearIndependenceTolerance;
        
        return (isLinearlyIndependent, vector);
    }

    /// <summary>
    /// Performs reorthogonalization to improve numerical stability.
    /// This is particularly important for sparse vectors where small
    /// errors can accumulate.
    /// </summary>
    private XGaFloat64Vector ReorthogonalizeVector(XGaFloat64Vector vector, IList<XGaFloat64Vector> basis)
    {
        for (var iteration = 0; iteration < MaxReorthogonalizations; iteration++)
        {
            var maxProjection = 0.0;

            foreach (var basisVector in basis)
            {
                var projection = InnerProduct(vector, basisVector);
                maxProjection = Math.Max(maxProjection, Math.Abs(projection));

                if (Math.Abs(projection) > ZeroTolerance)
                    vector -= basisVector.Times(projection);
            }

            // If projections are small enough, we're done
            if (maxProjection < OrthogonalityTolerance)
                break;
        }

        return vector;
    }
    
    /// <summary>
    /// Computes an orthonormal basis spanning the same subspace as the input
    /// sparse vectors. Uses Modified Gram-Schmidt with reorthogonalization for
    /// numerical stability.
    /// </summary>
    /// <param name="inputVectors">Collection of sparse input vectors</param>
    /// <returns>Orthonormalization result containing the basis and metadata</returns>
    public Result Orthonormalize(IList<XGaFloat64Vector> inputVectors)
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
        var orthonormalBasis = new List<XGaFloat64Vector>(inputVectors.Count);
        var basisIndices = new List<int>(inputVectors.Count);

        try
        {
            // Process each input vector using Modified Gram-Schmidt
            for (var i = 0; i < inputVectors.Count; i++)
            {
                var vector = inputVectors[i];

                // Skip zero or near-zero vectors
                if (Norm(vector).IsNearZero(ZeroTolerance))
                    continue;

                // Orthogonalize against existing basis vectors
                (var isLinearlyIndependent, vector) = 
                    OrthogonalizeVector(vector, orthonormalBasis);

                if (!isLinearlyIndependent) continue;

                // Normalize the vector
                var norm = Norm(vector);

                // Vector became zero during orthogonalization
                if (norm <= LinearIndependenceTolerance) continue;

                vector /= norm;

                // Optional reorthogonalization for numerical stability
                if (ReorthogonalizationEnabled)
                {
                    vector = 
                        ReorthogonalizeVector(vector, orthonormalBasis);

                    // Renormalize after reorthogonalization
                    norm = Norm(vector);

                    // Vector became zero during reorthogonalization
                    if (norm <= ZeroTolerance) continue; 

                    vector /= norm;
                }

                orthonormalBasis.Add(vector);
                basisIndices.Add(i);
            }

            result.OrthonormalBasis = orthonormalBasis;
            result.Rank = orthonormalBasis.Count;
            result.BasisIndices = basisIndices;
            result.MaxOrthogonalityError = GetMaxOrthogonalityError(orthonormalBasis);
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
    /// Computes the maximum orthogonality error in the basis.
    /// Returns max |&lt;b_i, b_j>| for i ≠ j, and max ||b_i|² - 1|
    /// </summary>
    public double GetMaxOrthogonalityError(IList<XGaFloat64Vector> basis)
    {
        if (basis.Count <= 1)
            return 0.0;

        var maxError = 0.0;

        // Check orthogonality between different vectors
        for (var i = 0; i < basis.Count; i++)
        {
            // Check normalization error
            var normError = Math.Abs(Norm(basis[i]) - 1.0);
            maxError = Math.Max(maxError, normError);

            // Check orthogonality with other vectors
            for (var j = i + 1; j < basis.Count; j++)
            {
                var innerProduct = InnerProduct(basis[i], basis[j]);

                maxError = Math.Max(maxError, Math.Abs(innerProduct));
            }
        }

        return maxError;
    }

    /// <summary>
    /// Verifies that the given vectors form an orthonormal set within tolerance
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool VerifyOrthonormality(IList<XGaFloat64Vector> vectors, double tolerance = Float64Utils.DefaultTolerance)
    {
        return GetMaxOrthogonalityError(vectors) <= tolerance;
    }


    /// <summary>
    /// Projects a vector onto the subspace spanned by the given basis
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector ReconstructFromSubspace(XGaFloat64Vector vector, IEnumerable<XGaFloat64Vector> basis)
    {
        return Euclidean 
            ? vector.ReconstructFromEuclideanSubspace(basis)
            : vector.ReconstructFromMetricSubspace(basis);
    }

    /// <summary>
    /// Computes the reconstruction error when representing original vectors using the basis
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetMaxReconstructionError(IEnumerable<XGaFloat64Vector> originalVectors, IEnumerable<XGaFloat64Vector> basis)
    {
        return originalVectors.Max(
            original => Norm(
                original - ReconstructFromSubspace(original, basis)
            )
        );
    }
}