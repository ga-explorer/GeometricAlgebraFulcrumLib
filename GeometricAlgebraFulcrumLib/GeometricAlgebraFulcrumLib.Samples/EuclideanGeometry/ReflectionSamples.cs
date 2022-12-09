using System;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Processors;
using GeometricAlgebraFulcrumLib.Processors.MatrixAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Text;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using MathNet.Numerics.LinearAlgebra;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Maps.SpaceND.Reflection;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;

namespace GeometricAlgebraFulcrumLib.Samples.EuclideanGeometry;

public static class ReflectionSamples
{
    public static void ReflectionMatrixToHyperPlaneReflectionsSample(int n, int reflectionCount)
    {
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

        var reflectionSequence = 
            HyperPlaneNormalReflectionSequence.Create(n);

        for (var i = 0; i < reflectionCount; i++)
            reflectionSequence.AppendMap(
                random.GetHyperPlaneReflection(n)
            );

        var matrix =
            reflectionSequence.GetMatrix();

        reflectionSequence = 
            HyperPlaneNormalReflectionSequence.CreateFromReflectionMatrix(matrix);

        Debug.Assert(
            (reflectionSequence.GetMatrix() - matrix)
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
            random.GetOrthogonalMatrix(n);

        var reflectionSequence = 
            matrix.GetHyperPlaneNormalReflectionSequence();

        var matrix1 = 
            reflectionSequence.GetMatrix().GetHyperPlaneNormalReflectionSequence().GetMatrix();
        
        Debug.Assert(
            (matrix1 - reflectionSequence.GetMatrix()).L2Norm().IsNearZero()
        );

        Debug.Assert(
            (matrix - reflectionSequence.GetMatrix()).L2Norm().IsNearZero()
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
            reflectionSequence.GetMatrix();

        for (var k = 0; k < reflectionSequence.Count; k++)
        {
            var reflection = reflectionSequence[k];

            var x = reflection.ReflectionNormal;
            var y = reflectionSequence.MapVector(x);

            Debug.Assert(
                (y + x).GetVectorNormSquared().IsNearZero()
            );

            y = (reflectionMatrix * Vector<double>.Build.DenseOfEnumerable(x)).ToArray().CreateTuple();

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
            var reflectionSequence = HyperPlaneNormalReflectionSequence.CreateRandomOrthogonal(
                random,
                n,
                reflectionCount
            );

            var reflectionMatrix = 
                reflectionSequence.GetMatrix();

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
                var v2 = (reflectionMatrix * u.ToVector()).ToTuple();

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
                    var x = random.GetFloat64Tuple(n);

                    var y1 = reflectionSequence.MapVector(x);
                    var y2 = (reflectionMatrix * x.ToVector()).ToTuple();
                
                    Debug.Assert(
                        (y1 - y2).GetVectorNormSquared().IsNearZero()
                    );
                }
            }
        }

        
        for (var reflectionCount = 1; reflectionCount <= 2 * n; reflectionCount++)
        {
            // Create a reflection sequence of 1 or more general reflections
            var reflectionSequence = HyperPlaneNormalReflectionSequence.CreateRandom(
                random,
                n,
                reflectionCount
            );

            var reflectionMatrix = 
                reflectionSequence.GetMatrix();

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
                var x = random.GetFloat64Tuple(n);

                var y1 = reflectionSequence.MapVector(x);
                var y2 = (reflectionMatrix * x.ToVector()).ToTuple();
                
                Debug.Assert(
                    (y1 - y2).GetVectorNormSquared().IsNearZero()
                );
            }
        }
    }
}