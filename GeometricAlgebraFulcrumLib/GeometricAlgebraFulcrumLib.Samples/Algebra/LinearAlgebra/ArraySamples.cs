using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;

namespace GeometricAlgebraFulcrumLib.Samples.Algebra.LinearAlgebra
{
    public static class ArraySamples
    {
        public static void Example1()
        {
            var randGen = new Random(10);
            var composer = InvertibleRealMatrixComposer.Create(5);

            composer.ApplyRandomScalingOperations(randGen, 10);

            var matrix = composer.GetMatrix();
            var matrixInverse = composer.GetMatrixInverse();

            Console.WriteLine(matrix);
            Console.WriteLine(matrixInverse);
        }
    }
}
