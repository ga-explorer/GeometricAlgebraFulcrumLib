using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Numerical.BivectorSchurDecomposer
{
    /// <summary>
    /// Operations for converting between matrix representations and multivector representations
    /// while maintaining geometric interpretation of the Schur decomposition process
    /// </summary>
    public static class ConversionOperations
    {
        /// <summary>
        /// Extract tridiagonal coefficients from a bivector
        /// </summary>
        /// <param name="bivector">Input bivector</param>
        /// <param name="dimensions">Vector space dimensions</param>
        /// <returns>Array of tridiagonal coefficients</returns>
        public static double[] ExtractTridiagonalCoefficients(XGaFloat64Bivector bivector, int dimensions)
        {
            var coefficients = new double[dimensions - 1];
            
            // Extract coefficients from consecutive basis bivectors
            foreach (var (id, scalar) in bivector)
            {
                var (i, j) = id.FirstIndexPair;
                    
                // Check if this is a consecutive pair
                if (j == i + 1 && i < dimensions - 1) 
                    coefficients[i] = scalar;
            }
            
            return coefficients;
        }

        /// <summary>
        /// Create a test bivector for validation
        /// </summary>
        /// <param name="processor">Geometric algebra processor</param>
        /// <param name="dimensions">Vector space dimensions</param>
        /// <returns>Test bivector</returns>
        public static XGaFloat64Bivector CreateTestBivector(XGaFloat64Processor processor, int dimensions)
        {
            var composer = processor.CreateBivectorComposer();
            
            // Create a simple test bivector with known structure
            for (var i = 0; i < dimensions - 1; i++)
            {
                var coefficient = 1.0 + 0.5 * i; // Varying coefficients
                composer.AddBivectorTerm(i, i + 1, coefficient);
            }
            
            return composer.GetBivector();
        }
    }
}