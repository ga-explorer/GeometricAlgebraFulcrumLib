using System;
using System.Linq;
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
            // Select suitable scalar processor for managing computations
            var scalarProcessor = ScalarAlgebraFloat64Processor.DefaultProcessor;

            // Select a suitable text composer for displaying results
            var textComposer = TextFloat64Composer.DefaultComposer;

            // You can also use other kinds of symbolic processors and text composers

            //var scalarProcessor = ScalarAlgebraMathematicaProcessor.DefaultProcessor;
            //var textComposer = LaTeXMathematicaComposer.DefaultComposer;

            //var scalarProcessor = ScalarAlgebraAngouriMathProcessor.DefaultProcessor;
            //var textComposer = TextAngouriMathComposer.DefaultComposer;

            // Make the same construction for dimensions 2, 3, ..., 10
            // Note that there is no explicit use of coordinates, the abstract geometric
            // idea is directly expressed in code
            for (var n = 2U; n < 10U; n++)
            {
                // The dimension of the larger GA space is one more than the dimension
                // of the target space
                var vSpaceDimension = n + 1;

                // Create a Euclidean geometric algebra processor based on the selected
                // scalar processor
                var processor = 
                    scalarProcessor.CreateGeometricAlgebraEuclideanProcessor(vSpaceDimension);
                
                // The ones vector is the core of this geometric construction
                var onesVector = processor
                    .CreateVectorOnes((int) vSpaceDimension);
                    //.MapScalars(expr => Mfs.N[expr].Evaluate());

                // This hyperspace is the orthogonal complement of the all-ones vector
                var hyperSpace = 
                    onesVector.GetDualSubspace();

                // Basis vectors of GA space
                var basisVectors =
                    processor.CreateVectorBasis().ToArray();

                // Simplex centroid to vertex vectors are projections of basis vectors
                // onto hyperSpace
                var simplexVertices =
                    basisVectors.ProjectOn(hyperSpace).ToArray();

                // Take the average of the first n basis vectors of the larger GA space
                var avgVector = 
                    processor.CreateVectorAverageOnes((int) n);

                // Projecting the average vector onto the hyperplane gives a radius of
                // the inner sphere
                var innerSphereRadius = 
                    hyperSpace.Project(avgVector).Norm();

                var outerSphereRadius =
                    simplexVertices[0].Norm();

                // The radius ratio of outer to inner spheres is equal to n
                var sphereRatio = 
                    outerSphereRadius / innerSphereRadius;

                // Find a Euclidean rotor that maps the ones vector into the last
                // basis vector of the GA space
                var rotor = 
                    onesVector.GetEuclideanRotorTo(basisVectors[n]);

                // Rotate the simplex vertices from the GA space to the target space
                // After rotation, the coordinate of the last basis vector is zero,
                // so it can be discarded
                var vertices =
                    simplexVertices.OmMapUsing(rotor).ToArray();


                //Display results
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
