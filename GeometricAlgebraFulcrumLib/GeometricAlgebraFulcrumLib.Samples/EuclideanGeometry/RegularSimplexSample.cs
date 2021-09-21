using System;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.Text;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Samples.EuclideanGeometry
{
    public static class RegularSimplexSample
    {
        public static void Execute()
        {
            var scalarProcessor = ScalarAlgebraMathematicaProcessor.DefaultProcessor;
            var textComposer = TextMathematicaComposer.DefaultComposer;

            //var scalarProcessor = ScalarAlgebraFloat64Processor.DefaultProcessor;
            //var textComposer = TextFloat64Composer.DefaultComposer;

            //var scalarProcessor = ScalarAlgebraAngouriMathProcessor.DefaultProcessor;
            //var textComposer = TextAngouriMathComposer.DefaultComposer;

            for (var n = 2U; n < 10U; n++)
            {
                var vSpaceDimension = n + 1;

                var processor = 
                    scalarProcessor.CreateGeometricAlgebraEuclideanProcessor(vSpaceDimension);

                var originVector = processor.CreateVectorZero();
                
                // The all ones vector is the core of this geometric construction
                var onesVector = processor
                    .CreateVectorOnes((int) vSpaceDimension);
                    //.MapScalars(expr => Mfs.N[expr].Evaluate());

                // This hyperspace is the orthogonal complement of the all-ones vector
                var hyperSpace =
                    processor.CreateSubspace(
                    processor
                            .Dual(onesVector.VectorStorage)
                            .GetKVectorPart(n)
                        );

                // Basis vectors of larger GA space
                var basisVectors =
                    vSpaceDimension
                        .GetRange()
                        .Select(i => processor.CreateVectorBasis(i));

                // Simplex centroid to vertex vectors are projections of basis vectors onto hyperSpace
                var simplexVertices =
                    basisVectors
                        .Select(basisVector => hyperSpace.Project(basisVector).CreateVector(processor))
                        .ToArray();

                // Take the average of the first n basis vectors of the larger GA space
                var avgVector = 
                    processor.CreateVectorRepeatedScalar(
                        (int) n, 
                        processor.Inverse(processor.GetScalarFromNumber(n))
                    );

                // Projecting the average vector onto the hyperplane gives a radius of the inner sphere
                var innerSphereRadius = 
                    processor.Norm(hyperSpace.Project(avgVector));

                var outerSphereRadius =
                    processor.Norm(simplexVertices[0].VectorStorage);

                // The radius ratio of outer to inner spheres is equal to n
                var sphereRatio = 
                    processor.Divide(outerSphereRadius, innerSphereRadius);

                var rotor = processor.CreateEuclideanRotor(
                    onesVector,
                    processor.CreateVectorBasisStorage(n)
                );

                var vertices =
                    simplexVertices.Select(v => rotor.OmMapVector(v));

                Console.WriteLine($"n = {n}");
                Console.WriteLine();

                Console.WriteLine($"Inner Sphere Radius = {textComposer.GetScalarText(innerSphereRadius)}");
                Console.WriteLine();

                Console.WriteLine($"Outer Sphere Radius = {textComposer.GetScalarText(outerSphereRadius)}");
                Console.WriteLine();

                Console.WriteLine($"Sphere Ratio = {textComposer.GetScalarText(sphereRatio)}");
                Console.WriteLine();

                Console.WriteLine($"Vertex Positions in {n + 1} dimensions:");
                foreach (var position in simplexVertices)
                    Console.WriteLine($"   {textComposer.GetMultivectorText(position)}");
                Console.WriteLine();

                Console.WriteLine($"Vertex Positions in {n} dimensions:");
                foreach (var position in vertices)
                    Console.WriteLine($"   {textComposer.GetMultivectorText(position)}");
                Console.WriteLine();

                Console.WriteLine();
            }
        }
    }
}
