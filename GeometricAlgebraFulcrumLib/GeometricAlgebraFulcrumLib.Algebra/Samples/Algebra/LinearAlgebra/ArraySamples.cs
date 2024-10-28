using System;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;

namespace GeometricAlgebraFulcrumLib.Samples.Algebra.LinearAlgebra
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
