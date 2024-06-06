using System;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;
//using GeometricAlgebraFulcrumLib.Processors.MatrixAlgebra;

namespace GeometricAlgebraFulcrumLib.Samples.Algebra.GeometricAlgebra;

public static class ReflectionSamples
{
    public static void ReflectionMatrixToHyperPlaneReflectionsSample(int n, int reflectionCount)
    {
        //var scalarProcessor =
        //    ScalarProcessorOfFloat64.Instance;

        //var matrixProcessor =
        //    MatrixProcessorOfFloat64.Instance;

        var geometricProcessor =
            RGaFloat64Processor.Euclidean;

        var textComposer =
            TextComposerFloat64.DefaultComposer;

        var laTeXComposer =
            LaTeXComposerFloat64.DefaultComposer;

        var randomComposer =
            geometricProcessor.CreateRGaRandomComposer(n, 10);

        var random = randomComposer.RandomGenerator;

        var reflectionSequence =
            LinFloat64HyperPlaneNormalReflectionSequence.Create(n);

        for (var i = 0; i < reflectionCount; i++)
            reflectionSequence.AppendMap(
                random.GetHyperPlaneNormalReflection(n)
            );

        var matrix =
            reflectionSequence.ToMatrix(n, n);

        reflectionSequence =
            LinFloat64HyperPlaneNormalReflectionSequence.CreateFromReflectionMatrix(matrix);

        Debug.Assert(
            (reflectionSequence.ToMatrix(n, n) - matrix)
            .ToArray()
            .GetVectorNormSquared()
            .IsNearZero()
        );
    }


    public static void Example3()
    {
        const int n = 9;

        var random = new Random(10);

        //var reflectionFactorList =
        //    random
        //        .GetNumbers(n / 2, -5, 5)
        //        .ToArray();

        //var reflectionVectorList =
        //    random
        //        .GetOrthonormalVectors(n, n / 2)
        //        .Select(v => v.ToArray().CreateTuple())
        //        .ToArray();

        //var reflectionSequence = 
        //    VectorDirectionalScalingSequence.Create(n);

        var matrix =
            random.GetMathNetOrthogonalMatrix(n);

        var reflectionSequence =
            matrix.GetHyperPlaneNormalReflectionSequence();

        var matrix1 =
            reflectionSequence.ToMatrix(n, n).GetHyperPlaneNormalReflectionSequence().ToMatrix(n, n);

        Debug.Assert(
            (matrix1 - reflectionSequence.ToMatrix(n, n)).L2Norm().IsNearZero()
        );

        Debug.Assert(
            (matrix - reflectionSequence.ToMatrix(n, n)).L2Norm().IsNearZero()
        );

        for (var k = 0; k < reflectionSequence.Count; k++)
        {
            var reflection = reflectionSequence[k];

            var normalVector =
                reflection.ReflectionNormal;

            Console.WriteLine($"Reflection {k + 1}:");
            Console.WriteLine($"Normal Vector: {normalVector}");
            Console.WriteLine();
        }

        //var y = 
        //    reflection.MapVector(reflectionVector);

        //Debug.Assert(
        //    (y - reflectionFactor * reflectionVector).IsNearZero()
        //);

        var reflectionMatrix =
            reflectionSequence.ToMatrix(n, n);

        for (var k = 0; k < reflectionSequence.Count; k++)
        {
            var reflection = reflectionSequence[k];

            var x = reflection.ReflectionNormal;
            var y = reflectionSequence.MapVector(x);

            Debug.Assert(
                (y + x).GetVectorNormSquared().IsNearZero()
            );

            y = (reflectionMatrix * MathNetNumericsUtils.ToMathNetVector(x, n)).CreateLinVector();

            Debug.Assert(
                (y + x).GetVectorNormSquared().IsNearZero()
            );
        }

        var subspaceList =
            reflectionMatrix.GetSimpleEigenSubspaces();

        var i = 1;
        foreach (var subspace in subspaceList)
        {
            Console.WriteLine($"Subspace {i}:");
            Console.WriteLine(subspace);

            i++;
        }
    }


    /// <summary>
    /// Validate the reflection properties of arbitrary reflection sequences
    /// </summary>
    public static void Example4()
    {
        const int n = 9;

        var random = new Random(10);

        for (var reflectionCount = 1; reflectionCount <= n; reflectionCount++)
        {
            // Create a reflection sequence of 1 or more orthogonal reflections
            var reflectionSequence = LinFloat64HyperPlaneNormalReflectionSequence.CreateRandomOrthogonal(
                random,
                n,
                reflectionCount
            );

            var reflectionMatrix =
                reflectionSequence.ToMatrix(n, n);

            // Make sure the result is actually a reflection matrix
            Debug.Assert(
                reflectionMatrix.Determinant().Abs().IsNearOne(1e-7)
            );

            // Make sure the sequence contains only pair-wise orthogonal reflections
            Debug.Assert(
                reflectionSequence.IsNearOrthogonalReflectionsSequence()
            );

            var subspaceList =
                reflectionMatrix.GetSimpleEigenSubspaces();

            Console.WriteLine($"Orthogonal reflections number: {reflectionCount}");

            var j = 1;
            foreach (var subspace in subspaceList)
            {
                Console.WriteLine($"Subspace {j++}");
                Console.WriteLine(subspace);
            }

            foreach (var reflection in reflectionSequence)
            {
                var u = reflection.ReflectionNormal;
                var v = -reflection.ReflectionNormal;

                var v1 = reflectionSequence.MapVector(u);
                var v2 = (reflectionMatrix * MathNetNumericsUtils.ToMathNetVector(u, n)).CreateLinVector();

                // Make sure each reflection is performed independently from the others
                Debug.Assert(
                    (v - v1).GetVectorNormSquared().IsNearZero()
                );

                Debug.Assert(
                    (v - v2).GetVectorNormSquared().IsNearZero()
                );

                Debug.Assert(
                    (v1 - v2).GetVectorNormSquared().IsNearZero()
                );

                // Make sure reflection matrix multiplication is the same as
                // reflection sequence computations
                for (var i = 0; i < 100; i++)
                {
                    var x = random.GetLinVector(n).CreateLinVector();

                    var y1 = reflectionSequence.MapVector(x);
                    var y2 = (reflectionMatrix * MathNetNumericsUtils.ToMathNetVector(x, n)).CreateLinVector();

                    Debug.Assert(
                        (y1 - y2).GetVectorNormSquared().IsNearZero()
                    );
                }
            }
        }


        for (var reflectionCount = 1; reflectionCount <= 2 * n; reflectionCount++)
        {
            // Create a reflection sequence of 1 or more general reflections
            var reflectionSequence = LinFloat64HyperPlaneNormalReflectionSequence.CreateRandom(
                random,
                n,
                reflectionCount
            );

            var reflectionMatrix =
                reflectionSequence.ToMatrix(n, n);

            // Make sure the result is actually a reflection matrix
            Debug.Assert(
                reflectionMatrix.Determinant().Abs().IsNearOne(1e-7)
            );

            var subspaceList =
                reflectionMatrix.GetSimpleEigenSubspaces();

            Console.WriteLine($"General reflections number: {reflectionCount}");

            var j = 1;
            foreach (var subspace in subspaceList)
            {
                Console.WriteLine($"Subspace {j++}");
                Console.WriteLine(subspace);
            }

            // Make sure reflection matrix multiplication is the same as
            // reflection sequence computations
            for (var i = 0; i < 100; i++)
            {
                var x = random.GetLinVector(n).CreateLinVector();

                var y1 = reflectionSequence.MapVector(x);
                var y2 = (reflectionMatrix * MathNetNumericsUtils.ToMathNetVector(x, n)).CreateLinVector();

                Debug.Assert(
                    (y1 - y2).GetVectorNormSquared().IsNearZero()
                );
            }
        }
    }
}