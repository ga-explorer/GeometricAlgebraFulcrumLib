using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Numerical;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;

namespace GeometricAlgebraFulcrumLib.Samples.Algebra.GeometricAlgebra.Numerical
{
    public static class Samples1
    {
        public static void Example1()
        {
            const int n = 7;
            var randGen = new Random(10);

            var matrix = 
                randGen.GetOrthogonalMatrix(n);
            
            var versor = 
                matrix.OrthogonalMatrixToVersor();

            var matrixR = versor.VersorToMatrix(n);
            var error = (matrix - matrixR).FrobeniusNorm();

            Console.WriteLine($"Original matrix: {matrix}");
            Console.WriteLine($"Reconstructed matrix: {matrixR}");
            Console.WriteLine($"Reconstruction error: {error}");
        }
    }
}
