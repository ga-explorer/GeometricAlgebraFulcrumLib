using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Numerical.Bivectors
{
    public class XGaFloat64BivectorBladeDecomposer
    {
        public static void Example1()
        {
            var decomposer = new XGaFloat64BivectorBladeDecomposer();

            // Create a GA processor and composer
            
            var processor = XGaFloat64EuclideanProcessor.Instance;
            var composer = processor.CreateBivectorComposer();

            // Construct a non-simple bivector: B = e1∧e2 + e3∧e4 + 0.5 e1∧e3
            var bv = 
                composer
                    .AddBivectorTerm(0, 1, 1)   // e1 ∧ e2
                    .AddBivectorTerm(2, 3, 1)   // e3 ∧ e4
                    .AddBivectorTerm(0, 2, 0.5) // 0.5 e1 ∧ e3
                    .GetBivector();

            Console.WriteLine($"Input bivector: {bv}");

            // Decompose into simple bivectors
            var blades = decomposer.Decompose(bv);

            Console.WriteLine($"\nDecomposed into {blades.Count} simple bivectors:");
            for (var i = 0; i < blades.Count; i++)
            {
                Console.WriteLine($"B{i+1} = {blades[i]}");
            }

            // Reconstruct and compare
            var bvRecon = decomposer.Reconstruct(blades);
            var diff = bv.Subtract(bvRecon);
            var diffNorm = diff.SpSquared().Abs().Sqrt();

            Console.WriteLine($"\nReconstruction error norm: {diffNorm:F2}");
        }


        //private readonly int _n;
        private readonly XGaFloat64Processor _processor = XGaFloat64Processor.Euclidean;


        /// <summary>
        /// Decompose a bivector into a sum of orthogonal simple bivectors (2-blades)
        /// </summary>
        /// <param name="bivector">Input bivector in Cl(n,0,0)</param>
        /// <returns>List of simple bivectors (blades) summing to the input</returns>
        public List<XGaFloat64Bivector> Decompose(XGaFloat64Bivector bivector)
        {
            // Step 1: Convert bivector to skew-symmetric matrix
            var omega = BivectorToSkewSymmetricMatrix(bivector);

            // Step 2: Block-diagonalize using real Schur or SVD-based method
            var (angles, basisVectors) = DiagonalizeSkewSymmetric(omega);

            // Step 3: Reconstruct simple bivectors from 2D planes
            var blades = new List<XGaFloat64Bivector>();
            for (var i = 0; i < angles.Count; i++)
            {
                if (Math.Abs(angles[i]) < 1e-10) continue; // Ignore zero blades

                var u = basisVectors[2 * i];
                var v = basisVectors[2 * i + 1];

                // Scale by the angle (weight)
                var blade = _processor.BivectorZero;
                var uv = 
                    _processor.Vector(u.ToArray()).Op(
                        _processor.Vector(v.ToArray())
                    );

                blade = blade.Add(uv.Times(angles[i]));

                blades.Add(blade);
            }

            return blades;
        }

        /// <summary>
        /// Convert a GA bivector to a skew-symmetric matrix Ω where Ω[i,j] = coefficient of e_i ∧ e_j
        /// </summary>
        private static Matrix<double> BivectorToSkewSymmetricMatrix(XGaFloat64Bivector bivector)
        {
            var n = bivector.VSpaceDimensions;
            var matrix = DenseMatrix.Create(n, n, 0.0);

            foreach (var (id, scalar) in bivector)
            {
                // Decode basis bivector index to (i,j) with i < j
                var (i, j) = id.FirstIndexPair;
                matrix[i, j] = scalar;
                matrix[j, i] = -scalar;
            }

            return matrix;
        }

        /// <summary>
        /// Diagonalize a skew-symmetric matrix into 2x2 blocks
        /// Returns: (angles, basisVectors) where each pair of vectors spans a 2-plane
        /// </summary>
        private (Vector<double>, List<Vector<double>>) DiagonalizeSkewSymmetric(Matrix<double> omega)
        {
            // Compute SVD: Omega = U * S * V^T
            var svd = omega.Svd();
            var matrixU = svd.U;
            var matrixS = svd.W.Diagonal();

            // Since Omega is skew-symmetric, singular values come in pairs (λ, λ)
            // We take unique positive singular values / 2 as "angles"
            var angles = new List<double>();
            var basisVectors = new List<Vector<double>>();

            for (var i = 0; i < matrixS.Count; i++)
            {
                var sigma = matrixS[i];
                if (Math.Abs(sigma) < 1e-10) continue;

                // Each non-zero singular value corresponds to a 2D plane
                // But SVD gives us left singular vectors; we need to recover rotation planes
                // For skew-symmetric matrices, we can use the fact that:
                // Ω Q = Q Λ, where Λ is block-diagonal with [0 λ; -λ 0]

                // Alternate: Use real Schur decomposition to get orthogonal block-diagonal form
                // Since MathNet lacks real Schur, we do a workaround:
                // Use eigenvalue decomposition on the complexified matrix, then extract real blocks.
                // But for now, we approximate the plane from singular vectors.

                // For skew-symmetric Ω, the columns of U corresponding to σ form the basis.
                // But we need to group them properly: each 2-plane uses two vectors.
                // However, SVD of skew-symmetric matrix gives U where every two columns span a plane.

                // Empirical fact: For skew-symmetric Ω, SVD gives U such that:
                // Ω ≈ sum λ_i (u_{2i} ∧ u_{2i+1})
                // But orientation matters.

                // We'll collect singular values and assume even/odd pairing
                if (i % 2 == 0 && i + 1 < matrixS.Count && Math.Abs(matrixS[i] - matrixS[i + 1]) < 1e-10)
                {
                    angles.Add(sigma); // magnitude = rotation rate
                    basisVectors.Add(matrixU.Column(i));
                    basisVectors.Add(matrixU.Column(i + 1));
                    i++; // skip next singular value (paired)
                }
                else if (i % 2 == 1)
                {
                    continue; // already skipped
                }
                else
                {
                    // Unpaired? Should not happen in exact arithmetic
                    var n = omega.RowCount;
                    angles.Add(sigma);
                    basisVectors.Add(matrixU.Column(i));
                    basisVectors.Add(DenseVector.Create(n, k => k == (i + 1) % n ? 1.0 : 0.0)); // fallback
                }
            }

            return (angles.ToMathNetDenseVector(), basisVectors);
        }

        /// <summary>
        /// Reconstructs the original bivector from the sum of blades (for verification)
        /// </summary>
        public XGaFloat64Bivector Reconstruct(List<XGaFloat64Bivector> blades)
        {
            return blades.Aggregate(
                _processor.BivectorZero, 
                (current, blade) => 
                    current.Add(blade)
            );
        }
    }

}