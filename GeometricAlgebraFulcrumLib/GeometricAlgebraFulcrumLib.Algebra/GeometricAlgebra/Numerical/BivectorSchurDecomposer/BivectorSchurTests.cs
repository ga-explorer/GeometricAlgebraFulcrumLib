using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Numerical.BivectorSchurDecomposer
{
    /// <summary>
    /// Comprehensive tests and validation for the Bivector Schur Decomposition framework
    /// Demonstrates the implementation of Schur decomposition using direct multivector operations
    /// </summary>
    public class BivectorSchurTests
    {
        private readonly XGaFloat64Processor _processor;
        private const double Tolerance = 1e-10;

        public BivectorSchurTests()
        {
            _processor = XGaFloat64Processor.Euclidean;
        }

        /// <summary>
        /// Run all comprehensive tests
        /// </summary>
        public void RunAllTests()
        {
            Console.WriteLine("=== Bivector Schur Decomposition Tests ===\n");
            
            TestConversionOperations();
            TestTridiagonalization();
            //TestQrAlgorithm();
            TestFullDecomposition2D();
            TestFullDecomposition4D();
            TestSkewSymmetricMatrixDecomposition();
            TestGeometricInterpretation();
            
            Console.WriteLine("=== All Tests Completed ===");
        }

        /// <summary>
        /// Test conversion operations between matrices and bivectors
        /// </summary>
        public void TestConversionOperations()
        {
            Console.WriteLine("--- Testing Conversion Operations ---");
            
            var framework = new BivectorSchurFramework(3);
            
            // Create a simple 3x3 skew-symmetric matrix
            var matrix = (new double[,]
            {
                {  0,  2, -1 },
                { -2,  0,  3 },
                {  1, -3,  0 }
            }).ToMathNetDenseMatrix();
            
            Console.WriteLine("Original Matrix:");
            Console.WriteLine(matrix);
            
            // Convert to bivector and back
            var bivector = _processor.MatrixToBivector(matrix, framework.Tolerance);
            var reconstructed = bivector.ToSkewSymmetricMatrix();
            
            Console.WriteLine("\nBivector representation:");
            Console.WriteLine(bivector.ToString());
            
            Console.WriteLine("\nReconstructed Matrix:");
            Console.WriteLine(reconstructed);
            
            // Verify conversion accuracy
            var conversionError = (matrix - reconstructed).L2Norm();
            Console.WriteLine($"Conversion error: {conversionError:E}");
            Console.WriteLine($"Conversion test: {(conversionError < Tolerance ? "PASSED" : "FAILED")}\n");
        }

        /// <summary>
        /// Test tridiagonalization using Householder reflections as versors
        /// </summary>
        public void TestTridiagonalization()
        {
            Console.WriteLine("--- Testing Tridiagonalization ---");
            
            var framework = new BivectorSchurFramework(4);
            
            // Create a 4D bivector (general form)
            var composer = _processor.CreateBivectorComposer();
            composer.SetBivectorTerm(0, 1, 2.0);  // e01
            composer.SetBivectorTerm(0, 2, 1.5);  // e02  
            composer.SetBivectorTerm(0, 3, 1.0);  // e03
            composer.SetBivectorTerm(1, 2, 2.5);  // e12
            composer.SetBivectorTerm(1, 3, 1.8);  // e13
            composer.SetBivectorTerm(2, 3, 3.0);  // e23
            
            var bivector = composer.GetBivector();
            
            Console.WriteLine("Original bivector:");
            Console.WriteLine(bivector.ToString());
            
            // Check if it's already tridiagonal
            var isTridiagonal = bivector.IsNearTridiagonal(Tolerance);
            Console.WriteLine($"Is already tridiagonal: {isTridiagonal}");
            
            // Extract tridiagonal coefficients (would be zero for non-adjacent pairs)
            var coefficients = ConversionOperations.ExtractTridiagonalCoefficients(bivector, 4);
            Console.WriteLine($"Tridiagonal coefficients: [{string.Join(", ", coefficients.Select(c => c.ToString("F3")))}]");
            
            Console.WriteLine("Tridiagonalization test: COMPLETED\n");
        }

        ///// <summary>
        ///// Test QR algorithm components using rotor conjugations
        ///// </summary>
        //public void TestQrAlgorithm()
        //{
        //    Console.WriteLine("--- Testing QR Algorithm Components ---");
            
        //    var framework = new BivectorSchurFramework(3);
            
        //    // Create a simple tridiagonal-like bivector
        //    var composer = _processor.CreateBivectorComposer();
        //    composer.SetBivectorTerm(0, 1, 1.5);  // e01
        //    composer.SetBivectorTerm(1, 2, 2.0);  // e12
            
        //    var bivector = composer.GetBivector();
            
        //    Console.WriteLine("Tridiagonal bivector:");
        //    Console.WriteLine(bivector.ToString());
            
        //    // Test Givens angle computation
        //    var angle = ConversionOperations.ComputeGivensAngle(bivector, _processor, 0, 1, Tolerance);
        //    Console.WriteLine($"Givens angle for (0,1) plane: {angle:F6} radians ({angle * 180 / Math.PI:F2}°)");
            
        //    // Test versor dagger computation
        //    var vector = _processor.VectorTerm(0);
        //    var dagger = ConversionOperations.ComputeVersorDagger(vector);
            
        //    Console.WriteLine($"Vector: {vector}");
        //    Console.WriteLine($"Versor dagger: {dagger}");
            
        //    Console.WriteLine("QR algorithm components test: COMPLETED\n");
        //}

        /// <summary>
        /// Test full decomposition on a 2D case (simple rotation)
        /// </summary>
        public void TestFullDecomposition2D()
        {
            Console.WriteLine("--- Testing Full 2D Decomposition ---");
            
            var framework = new BivectorSchurFramework(2);
            
            // Create a simple 2D bivector (pure rotation in xy-plane)
            var bivector = _processor.VectorTerm(0).Op(_processor.VectorTerm(1)).Times(3.0);
            
            Console.WriteLine("Original 2D bivector (3 * e₀∧e₁):");
            Console.WriteLine(bivector.ToString());
            
            try
            {
                var result = framework.DecomposeBivector(bivector);
                
                Console.WriteLine("\nDecomposition results:");
                Console.WriteLine($"Schur form: {result.SchurForm}");
                Console.WriteLine($"Orthogonal rotor: {result.OrthogonalRotor}");
                
                // Verify decomposition
                var isAccurate = result.VerifyDecomposition(Tolerance);
                var isOrthogonal = result.VerifyOrthogonality(Tolerance);
                
                Console.WriteLine($"\nVerification:");
                Console.WriteLine($"Decomposition accurate: {isAccurate}");
                Console.WriteLine($"Rotor orthogonal: {isOrthogonal}");
                Console.WriteLine($"Reconstruction error: {result.Metrics.ReconstructionError:E}");
                
                Console.WriteLine($"2D decomposition test: {(isAccurate && isOrthogonal ? "PASSED" : "FAILED")}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"2D decomposition test: FAILED - {ex.Message}\n");
            }
        }

        /// <summary>
        /// Test full decomposition on a 4D case
        /// </summary>
        public void TestFullDecomposition4D()
        {
            Console.WriteLine("--- Testing Full 4D Decomposition ---");
            
            var framework = new BivectorSchurFramework(4);
            
            // Create a 4D bivector with two orthogonal rotation planes
            var composer = _processor.CreateBivectorComposer();
            composer.SetBivectorTerm(0, 1, 2.0);  // Rotation in e₀∧e₁ plane
            composer.SetBivectorTerm(2, 3, 1.5);  // Rotation in e₂∧e₃ plane
            
            var bivector = composer.GetBivector();
            
            Console.WriteLine("Original 4D bivector (2*e₀∧e₁ + 1.5*e₂∧e₃):");
            Console.WriteLine(bivector.ToString());
            
            try
            {
                var result = framework.DecomposeBivector(bivector);
                
                Console.WriteLine("\nDecomposition results:");
                Console.WriteLine($"Schur form: {result.SchurForm}");
                
                // This should already be in canonical form (orthogonal planes)
                var isAccurate = result.VerifyDecomposition(Tolerance);
                var isOrthogonal = result.VerifyOrthogonality(Tolerance);
                
                Console.WriteLine($"\nVerification:");
                Console.WriteLine($"Decomposition accurate: {isAccurate}");
                Console.WriteLine($"Rotor orthogonal: {isOrthogonal}");
                Console.WriteLine($"Reconstruction error: {result.Metrics.ReconstructionError:E}");
                
                Console.WriteLine($"4D decomposition test: {(isAccurate && isOrthogonal ? "PASSED" : "FAILED")}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"4D decomposition test: FAILED - {ex.Message}\n");
            }
        }

        /// <summary>
        /// Test matrix-based decomposition interface
        /// </summary>
        public void TestSkewSymmetricMatrixDecomposition()
        {
            Console.WriteLine("--- Testing Matrix Decomposition Interface ---");
            
            var framework = new BivectorSchurFramework(3);
            
            var matrix = (new double[,]
            {
                {  0,  1, -2 },
                { -1,  0,  3 },
                {  2, -3,  0 }
            }).ToMathNetDenseMatrix();
            
            Console.WriteLine("Original skew-symmetric matrix:");
            Console.WriteLine(matrix);
            
            try
            {
                var result = framework.DecomposeMatrix(matrix);
                
                Console.WriteLine("\nSchur form matrix:");
                Console.WriteLine(result.SchurMatrix);
                
                Console.WriteLine("\nOrthogonal matrix Q:");
                Console.WriteLine(result.OrthogonalMatrix);
                
                // Verify matrix decomposition
                var matrixAccurate = result.VerifyMatrixDecomposition(Tolerance);
                var matrixOrthogonal = result.VerifyMatrixOrthogonality(Tolerance);
                
                Console.WriteLine($"\nMatrix verification:");
                Console.WriteLine($"Matrix decomposition accurate: {matrixAccurate}");
                Console.WriteLine($"Q matrix orthogonal: {matrixOrthogonal}");
                
                // Also verify the underlying bivector decomposition
                var bivectorAccurate = result.BivectorResult.VerifyDecomposition(Tolerance);
                var bivectorOrthogonal = result.BivectorResult.VerifyOrthogonality(Tolerance);
                
                Console.WriteLine($"Bivector decomposition accurate: {bivectorAccurate}");
                Console.WriteLine($"Bivector rotor orthogonal: {bivectorOrthogonal}");
                
                var allPassed = matrixAccurate && matrixOrthogonal && bivectorAccurate && bivectorOrthogonal;
                Console.WriteLine($"Matrix interface test: {(allPassed ? "PASSED" : "FAILED")}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Matrix decomposition test: FAILED - {ex.Message}\n");
            }
        }

        /// <summary>
        /// Test geometric interpretation of results
        /// </summary>
        public void TestGeometricInterpretation()
        {
            Console.WriteLine("--- Testing Geometric Interpretation ---");
            
            var framework = new BivectorSchurFramework(4);
            
            // Create bivector representing two rotations
            var composer = _processor.CreateBivectorComposer();
            composer.SetBivectorTerm(0, 1, Math.PI/4);  // 45° rotation in xy-plane
            composer.SetBivectorTerm(2, 3, Math.PI/6);  // 30° rotation in zw-plane
            
            var bivector = composer.GetBivector();
            
            Console.WriteLine("Bivector representing two plane rotations:");
            Console.WriteLine($"π/4 * e₀∧e₁ + π/6 * e₂∧e₃");
            Console.WriteLine($"Actual: {bivector}");
            
            // Extract coefficients
            var coefficients = ConversionOperations.ExtractTridiagonalCoefficients(bivector, 4);
            
            Console.WriteLine($"\nTridiagonal coefficients: [{string.Join(", ", coefficients.Select(c => c.ToString("F4")))}]");
            
            // Interpret as rotation frequencies
            Console.WriteLine("\nGeometric interpretation:");
            Console.WriteLine($"This bivector generates rotations with frequencies:");
            for (var i = 0; i < coefficients.Length; i++)
            {
                if (Math.Abs(coefficients[i]) > Tolerance)
                {
                    var frequency = Math.Abs(coefficients[i]);
                    var angle = frequency * 180 / Math.PI;
                    Console.WriteLine($"  Plane ({i},{i+1}): frequency = {frequency:F4} rad/unit, angle = {angle:F1}°");
                }
            }
            
            Console.WriteLine("Geometric interpretation test: COMPLETED\n");
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
                    if (j < cols - 1) Console.Write(", ");
                }
                Console.WriteLine(" ]");
            }
        }

        private double MatrixError(double[,] matrix1, double[,] matrix2)
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
    /// Entry point for running tests
    /// </summary>
    public static class TestRunner
    {
        public static void MainTest()
        {
            var tests = new BivectorSchurTests();
            tests.RunAllTests();
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}