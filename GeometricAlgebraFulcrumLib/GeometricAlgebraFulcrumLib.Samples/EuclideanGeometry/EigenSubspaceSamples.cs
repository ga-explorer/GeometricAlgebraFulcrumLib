using System;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Processors;
using GeometricAlgebraFulcrumLib.Processors.MatrixAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Text;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Maps.SpaceND.Scaling;
using NumericalGeometryLib.BasicMath.Matrices;

namespace GeometricAlgebraFulcrumLib.Samples.EuclideanGeometry;

public static class EigenSubspaceSamples
{
    public static void Example1()
    {
        const int n = 5;
        const int reflectionCount = 7;

        var scalarProcessor =
            ScalarAlgebraFloat64Processor.DefaultProcessor;

        var matrixProcessor =
            MatrixAlgebraFloat64Processor.DefaultProcessor;

        var geometricProcessor =
            scalarProcessor.CreateGeometricAlgebraEuclideanProcessor((uint)n);

        var textComposer =
            TextFloat64Composer.DefaultComposer;

        var laTeXComposer =
            LaTeXFloat64Composer.DefaultComposer;

        var randomComposer =
            geometricProcessor.CreateGeometricRandomComposer(10);

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
            VectorDirectionalScalingSequence.CreateFromMatrix(matrix);

        var diffArray = 
            (mapSequence.GetMatrix() - matrix).ToArray();

        Console.WriteLine(textComposer.GetArrayText(diffArray));
        Console.WriteLine();

        Debug.Assert(
            (mapSequence.GetMatrix() - matrix).L2Norm().IsNearZero()
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
