using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Algebra.Samples.Algebra.GeometricAlgebra;

public static class EigenSubspaceSamples
{
    public static void Example1()
    {
        const int n = 5;
        const int reflectionCount = 7;

        //var scalarProcessor =
        //    ScalarProcessorOfFloat64.Instance;

        //var matrixProcessor =
        //    MatrixProcessorOfFloat64.Instance;

        var geometricProcessor =
            XGaFloat64Processor.Euclidean;

        var textComposer =
            TextComposerFloat64.DefaultComposer;

        var laTeXComposer =
            LaTeXComposerFloat64.DefaultComposer;

        var randomComposer =
            geometricProcessor.CreateXGaRandomComposer(n, 10);

        var random = randomComposer.RandomGenerator;

        //var reflectionSequence =
        //    HyperPlaneReflectionSequence.Create(n);

        //for (var i = 0; i < reflectionCount; i++)
        //    reflectionSequence.AppendReflection(
        //        random.GetHyperPlaneReflection(n)
        //    );

        //var matrix =
        //    reflectionSequence.GetMatrix();

        //var matrix = 
        //    random.GetCirculantColumnMatrix(n);

        var matrix =
            random.GetFloat64Array2D(n, n).ToMatrix();

        var subspaceList =
            matrix.GetSimpleEigenSubspaces();

        var j = 1;
        foreach (var subspace in subspaceList)
        {
            Console.WriteLine($"Subspace {j}");
            Console.WriteLine($"   Dimensions: {subspace.SubspaceDimensions}");
            Console.WriteLine($"   Eigen Value: {subspace.EigenValue}");

            var k = 1;
            foreach (var vector in subspace.BasisVectors)
            {
                Console.WriteLine($"   Basis Vector {k}: {vector}");
                k++;
            }

            Console.WriteLine();
            j++;
        }

        var mapSequence =
            LinFloat64VectorDirectionalScalingSequence.CreateFromMatrix(matrix);

        var diffArray =
            (mapSequence.ToMatrix(matrix.RowCount, matrix.ColumnCount) - matrix).ToArray();

        Console.WriteLine(textComposer.GetArrayText(diffArray));
        Console.WriteLine();

        Debug.Assert(
            (mapSequence.ToMatrix(matrix.RowCount, matrix.ColumnCount) - matrix).L2Norm().IsNearZero()
        );

        //reflectionSequence =
        //    HyperPlaneReflectionSequence.CreateFromReflectionMatrix(matrix);

        //Debug.Assert(
        //    (reflectionSequence.GetMatrix() - matrix)
        //    .ToArray()
        //    .GetVectorNormSquared()
        //    .IsNearZero()
        //);
    }
}