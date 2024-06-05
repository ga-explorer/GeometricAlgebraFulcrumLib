using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Matrices;

namespace GeometricAlgebraFulcrumLib.Core.Samples.Algebra.LinearAlgebra.Matrices
{
    public static class ArraySamples
    {
        public static void Example1()
        {
            var randGen = new Random(10);
            var composer = Float64InvertibleArrayComposer.Create(5);

            composer.ApplyRandomScalingOperations(randGen, 10);

            var matrix = composer.GetMatrix();
            var matrixInverse = composer.GetMatrixInverse();

            Console.WriteLine(matrix);
            Console.WriteLine(matrixInverse);
        }
    }
}
