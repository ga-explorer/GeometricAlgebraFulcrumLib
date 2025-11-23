using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Numerical.BivectorSchurDecomposer
{
    /// <summary>
    /// Comprehensive examples demonstrating the use of Schur decomposition on skew-symmetric matrices
    /// using direct multivector operations in Euclidean Clifford algebra.
    /// 
    /// This class shows practical applications and the geometric interpretation of the decomposition.
    /// </summary>
    public class SchurDecompositionExample
    {
        public static XGaFloat64Processor Processor 
            => XGaFloat64Processor.Euclidean;


        /// <summary>
        /// Run all example demonstrations
        /// </summary>
        public void RunAllExamples()
        {
            Console.WriteLine("=== BIVECTOR SCHUR DECOMPOSITION EXAMPLES ===");
            Console.WriteLine("Implementing Real Schur Decomposition using Native Multivector Operations");
            Console.WriteLine("Based on the theoretical framework that skew-symmetric matrices ↔ bivectors\n");

            Example1_SimpleRotation();
            Example2_MultipleRotationPlanes();
            Example3_PhysicalInterpretation();
            Example4_MatrixVsBivectorComparison();
            Example5_GeometricEvolution();

            Console.WriteLine("=== ALL EXAMPLES COMPLETED ===");
        }

        /// <summary>
        /// Example 1: Simple 2D rotation - demonstrates basic concepts
        /// </summary>
        private void Example1_SimpleRotation()
        {
            Console.WriteLine("--- EXAMPLE 1: Simple 2D Rotation ---");
            Console.WriteLine("Decomposing a bivector representing infinitesimal rotation in 2D plane\n");

            var framework = new BivectorSchurFramework(2);

            // Create a bivector representing rotation in the xy-plane
            var rotationStrength = Math.PI / 4; // 45 degrees
            var bivector = Processor.VectorTerm(0).Op(Processor.VectorTerm(1)).Times(rotationStrength);

            Console.WriteLine($"Original bivector: {rotationStrength/Math.PI:F3}π * e₀∧e₁");
            Console.WriteLine($"Geometric meaning: Infinitesimal rotation in xy-plane with magnitude {rotationStrength:F3} rad");
            Console.WriteLine($"Matrix representation would be:");
            
            var matrix = bivector.ToSkewSymmetricMatrix();
            Console.WriteLine(matrix);

            Console.WriteLine("\nApplying Schur decomposition...");
            var result = framework.DecomposeBivector(bivector);

            Console.WriteLine($"Schur form: {result.SchurForm}");
            Console.WriteLine($"Orthogonal rotor: {result.OrthogonalRotor}");
            
            Console.WriteLine("\nInterpretation:");
            Console.WriteLine("- The Schur form reveals the canonical rotation structure");
            Console.WriteLine("- For a simple 2D rotation, the decomposition is trivial (already canonical)");
            Console.WriteLine("- The orthogonal rotor represents the change of basis (identity for this case)");
            
            Console.WriteLine($"Reconstruction error: {result.Metrics.ReconstructionError:E}");
            Console.WriteLine("✓ Example 1 completed\n");
        }

        /// <summary>
        /// Example 2: Multiple orthogonal rotation planes in 4D
        /// </summary>
        private void Example2_MultipleRotationPlanes()
        {
            Console.WriteLine("--- EXAMPLE 2: Multiple Orthogonal Rotation Planes in 4D ---");
            Console.WriteLine("Decomposing a bivector with rotations in orthogonal 2D planes\n");

            var framework = new BivectorSchurFramework(4);

            // Create bivector with rotations in two orthogonal planes
            var composer = Processor.CreateBivectorComposer();
            composer.SetBivectorTerm(0, 1, 2.0);  // Strong rotation in xy-plane
            composer.SetBivectorTerm(2, 3, 1.0);  // Weaker rotation in zw-plane

            var bivector = composer.GetBivector();

            Console.WriteLine("Original bivector: 2.0*e₀∧e₁ + 1.0*e₂∧e₃");
            Console.WriteLine("Geometric meaning: Two independent rotations in orthogonal planes");
            Console.WriteLine("- Fast rotation (ω=2.0) in xy-plane");
            Console.WriteLine("- Slow rotation (ω=1.0) in zw-plane");

            var matrix = bivector.ToSkewSymmetricMatrix();
            Console.WriteLine("\nMatrix representation:");
            Console.WriteLine(matrix);

            Console.WriteLine("\nApplying Schur decomposition...");
            var result = framework.DecomposeBivector(bivector);

            Console.WriteLine($"\nSchur form: {result.SchurForm}");
            Console.WriteLine("Interpretation of Schur form:");
            Console.WriteLine("- Reveals the canonical structure: sum of orthogonal rotation planes");
            Console.WriteLine("- Each 2×2 block corresponds to a rotation plane");
            Console.WriteLine("- Eigenvalues are ±2i and ±1i (purely imaginary pairs)");

            // Extract canonical blocks if available
            if (result.CanonicalBlocks.Count > 0)
            {
                Console.WriteLine("\nCanonical blocks:");
                for (var i = 0; i < result.CanonicalBlocks.Count; i++)
                {
                    var block = result.CanonicalBlocks[i];
                    Console.WriteLine($"Block {i + 1}: frequency = {block.Frequency:F3}, eigenvalues = ±{block.Frequency:F3}i");
                }
            }

            Console.WriteLine($"\nReconstruction error: {result.Metrics.ReconstructionError:E}");
            Console.WriteLine("✓ Example 2 completed\n");
        }

        /// <summary>
        /// Example 3: Physical interpretation - angular velocity tensor
        /// </summary>
        private void Example3_PhysicalInterpretation()
        {
            Console.WriteLine("--- EXAMPLE 3: Physical Interpretation - Angular Velocity Tensor ---");
            Console.WriteLine("Interpreting a bivector as an angular velocity tensor in 3D space\n");

            var framework = new BivectorSchurFramework(3);

            // Create bivector representing angular velocity in 3D
            var composer = Processor.CreateBivectorComposer();
            composer.SetBivectorTerm(0, 1, 3.0);   // ωz = 3 rad/s (rotation around z-axis)
            composer.SetBivectorTerm(1, 2, 2.0);   // ωx = 2 rad/s (rotation around x-axis) 
            composer.SetBivectorTerm(0, 2, -1.0);  // ωy = -1 rad/s (rotation around y-axis)

            var bivector = composer.GetBivector();

            Console.WriteLine("Original bivector: 3.0*e₀∧e₁ + 2.0*e₁∧e₂ - 1.0*e₀∧e₂");
            Console.WriteLine("Physical interpretation:");
            Console.WriteLine("- Represents angular velocity of a rigid body");
            Console.WriteLine("- e₀∧e₁ ↔ rotation around z-axis (ωz = 3.0 rad/s)");
            Console.WriteLine("- e₁∧e₂ ↔ rotation around x-axis (ωx = 2.0 rad/s)");
            Console.WriteLine("- e₀∧e₂ ↔ rotation around y-axis (ωy = -1.0 rad/s)");

            var matrix = bivector.ToSkewSymmetricMatrix();
            Console.WriteLine("\nSkew-symmetric matrix (angular velocity tensor):");
            Console.WriteLine(matrix);

            Console.WriteLine("\nApplying Schur decomposition to find principal rotation axes...");
            var result = framework.DecomposeBivector(bivector);

            Console.WriteLine($"Schur form: {result.SchurForm}");
            Console.WriteLine("\nPhysical meaning of decomposition:");
            Console.WriteLine("- Finds orthogonal basis where rotation structure is revealed");
            Console.WriteLine("- Each 2×2 block represents rotation in a principal plane");
            Console.WriteLine("- The orthogonal transformation gives the orientation of principal axes");

            // Convert rotor to matrix to see the rotation
            var rotationMatrix = result.SchurForm.ToSkewSymmetricMatrix();
            Console.WriteLine("\nPrincipal rotation structure:");
            Console.WriteLine(rotationMatrix);

            Console.WriteLine($"\nTotal angular speed: {result.Metrics.OriginalNorm:F3} rad/s");
            Console.WriteLine($"Reconstruction error: {result.Metrics.ReconstructionError:E}");
            Console.WriteLine("✓ Example 3 completed\n");
        }

        /// <summary>
        /// Example 4: Comparison between matrix-based and bivector-based approaches
        /// </summary>
        private void Example4_MatrixVsBivectorComparison()
        {
            Console.WriteLine("--- EXAMPLE 4: Matrix vs Bivector Approach Comparison ---");
            Console.WriteLine("Demonstrating the equivalence and advantages of the geometric approach\n");

            var framework = new BivectorSchurFramework(3);

            // Create a challenging skew-symmetric matrix
            var matrix = (new double[,]
            {
                {  0,  4, -2 },
                { -4,  0,  3 },
                {  2, -3,  0 }
            }).ToMathNetDenseMatrix();

            Console.WriteLine("Input skew-symmetric matrix:");
            Console.WriteLine(matrix);

            // Traditional matrix-based approach
            Console.WriteLine("\n=== MATRIX-BASED APPROACH ===");
            var matrixResult = framework.DecomposeMatrix(matrix);

            Console.WriteLine("Orthogonal matrix Q:");
            Console.WriteLine(matrixResult.OrthogonalMatrix);

            Console.WriteLine("Schur form T:");
            Console.WriteLine(matrixResult.SchurMatrix);

            // Geometric bivector-based approach
            Console.WriteLine("\n=== BIVECTOR-BASED APPROACH ===");
            var bivector = Processor.MatrixToBivector(matrix, framework.Tolerance);
            Console.WriteLine($"Bivector representation: {bivector}");

            var bivectorResult = framework.DecomposeBivector(bivector);
            Console.WriteLine($"Schur form bivector: {bivectorResult.SchurForm}");
            Console.WriteLine($"Orthogonal rotor: {bivectorResult.OrthogonalRotor}");

            // Compare results
            Console.WriteLine("\n=== COMPARISON ===");
            var matrixError = 
                (matrixResult.SchurMatrix - bivectorResult.SchurForm.ToSkewSymmetricMatrix()).L2Norm();
            
            Console.WriteLine($"Difference between approaches: {matrixError:E}");
            Console.WriteLine($"Matrix decomposition accurate: {matrixResult.VerifyMatrixDecomposition()}");
            Console.WriteLine($"Bivector decomposition accurate: {bivectorResult.VerifyDecomposition()}");

            Console.WriteLine("\nAdvantages of bivector approach:");
            Console.WriteLine("✓ Direct geometric interpretation");
            Console.WriteLine("✓ Natural representation of rotational dynamics");
            Console.WriteLine("✓ Coordinate-free operations");
            Console.WriteLine("✓ Clear connection to physical angular velocity");
            
            Console.WriteLine("✓ Example 4 completed\n");
        }

        /// <summary>
        /// Example 5: Geometric evolution - time evolution of rotational systems
        /// </summary>
        private void Example5_GeometricEvolution()
        {
            Console.WriteLine("--- EXAMPLE 5: Geometric Evolution of Rotational Systems ---");
            Console.WriteLine("Using Schur decomposition to analyze time evolution\n");

            var framework = new BivectorSchurFramework(4);

            // Create a bivector representing a dynamical system
            var composer = Processor.CreateBivectorComposer();
            composer.SetBivectorTerm(0, 1, 1.0);   // Slow oscillation
            composer.SetBivectorTerm(2, 3, 5.0);   // Fast oscillation
            composer.SetBivectorTerm(0, 2, 0.5);   // Coupling term
            composer.SetBivectorTerm(1, 3, 0.3);   // Another coupling

            var bivector = composer.GetBivector();

            Console.WriteLine("Dynamical system bivector:");
            Console.WriteLine($"B = {bivector}");
            Console.WriteLine("\nThis represents a 4D rotational system with:");
            Console.WriteLine("- Slow rotation in xy-plane (ω₁ ≈ 1.0)");
            Console.WriteLine("- Fast rotation in zw-plane (ω₂ ≈ 5.0)");
            Console.WriteLine("- Coupling between planes");

            Console.WriteLine("\nApplying Schur decomposition to decouple the system...");
            var result = framework.DecomposeBivector(bivector);

            Console.WriteLine($"Canonical form: {result.SchurForm}");
            
            // Extract frequencies
            var coefficients = ConversionOperations.ExtractTridiagonalCoefficients(
                result.SchurForm, 4);
            
            Console.WriteLine("\nDecoupled normal modes:");
            for (var i = 0; i < coefficients.Length; i++)
            {
                if (Math.Abs(coefficients[i]) > 1e-10)
                {
                    var frequency = Math.Abs(coefficients[i]);
                    var period = 2 * Math.PI / frequency;
                    Console.WriteLine($"Mode {i + 1}: ω = {frequency:F3} rad/unit, T = {period:F3} units");
                }
            }

            Console.WriteLine("\nTime evolution interpretation:");
            Console.WriteLine("- The Schur form reveals the natural frequencies of the system");
            Console.WriteLine("- Each 2×2 block generates periodic motion: exp(ωt * Bᵢ)");
            Console.WriteLine("- The orthogonal transformation gives the normal coordinates");

            Console.WriteLine($"\nSystem complexity (total norm): {result.Metrics.OriginalNorm:F3}");
            Console.WriteLine($"Decomposition accuracy: {result.Metrics.ReconstructionError:E}");
            Console.WriteLine("✓ Example 5 completed\n");
        }

        #region Helper Methods

        private void PrintMatrix(double[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            
            for (var i = 0; i < rows; i++)
            {
                Console.Write("[ ");
                for (var j = 0; j < cols; j++)
                {
                    Console.Write($"{matrix[i, j],8:F3}");
                    if (j < cols - 1) Console.Write(",");
                }
                Console.WriteLine(" ]");
            }
        }

        private double ComputeMatrixError(double[,] matrix1, double[,] matrix2)
        {
            double error = 0;
            var rows = matrix1.GetLength(0);
            var cols = matrix1.GetLength(1);
            
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    var diff = matrix1[i, j] - matrix2[i, j];
                    error += diff * diff;
                }
            }
            
            return Math.Sqrt(error);
        }

        #endregion
    }

    /// <summary>
    /// Command-line interface for running examples
    /// </summary>
    public static class ExampleRunner
    {
        public static void MainExample(string[] args)
        {
            const int n = 4;
            var randgen = new Random(10);
            var ga = XGaFloat64Processor.Euclidean;
            
            var bv = ga.Bivector(
                randgen.GetFloat64Array1D(n * (n - 1) / 2, -1, 1)
            );

            var framework = new BivectorSchurFramework(n);
            var result = framework.DecomposeBivector(bv);
            var isCorrect = result.VerifyDecomposition();
            var isOrthogonal = result.VerifyOrthogonality();

            try
            {
                var examples = new SchurDecompositionExample();
                
                if (args.Length > 0)
                {
                    // Run specific example
                    switch (args[0].ToLower())
                    {
                        case "1":
                        case "simple":
                            examples.RunAllExamples();
                            break;
                        case "all":
                        default:
                            examples.RunAllExamples();
                            break;
                    }
                }
                else
                {
                    examples.RunAllExamples();
                }

                Console.WriteLine("\n" + new string('=', 60));
                Console.WriteLine("SUMMARY: Schur Decomposition using Multivector Operations");
                Console.WriteLine(new string('=', 60));
                Console.WriteLine("This implementation demonstrates how to perform real Schur");
                Console.WriteLine("decomposition on skew-symmetric matrices using NATIVE");
                Console.WriteLine("multivector operations in Euclidean Clifford algebra.");
                Console.WriteLine();
                Console.WriteLine("Key achievements:");
                Console.WriteLine("✓ Direct bivector-to-bivector transformations");
                Console.WriteLine("✓ Householder reflections as versor conjugations");
                Console.WriteLine("✓ QR algorithm using rotor operations");
                Console.WriteLine("✓ Geometric interpretation of results");
                Console.WriteLine("✓ No matrix operations needed after initial conversion");
                Console.WriteLine();
                Console.WriteLine("This bridges numerical linear algebra and geometric algebra,");
                Console.WriteLine("showing how matrix decompositions reveal intrinsic geometric");
                Console.WriteLine("structure of multivectors.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error running examples: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}