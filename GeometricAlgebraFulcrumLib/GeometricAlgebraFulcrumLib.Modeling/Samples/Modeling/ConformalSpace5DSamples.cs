﻿using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Operations;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling
{
    public static class ConformalSpace5DSamples
    {
        public static void Example1()
        {
            // The pre-defined scalar processor for 64-bits
            // floating point numbers
            var scalarProcessor = ScalarProcessorOfFloat64.Instance;

            // Create the CGA space object based on the selected
            // kind of scalars
            var cga = CGaGeometricSpace5D<double>.Create(scalarProcessor);

            // Encode 4 points as CGA null vectors
            var a = cga.EncodeIpnsRound.Point(3.5, 4.3, 2.6);
            var b = cga.EncodeIpnsRound.Point(-2.1, 3.4, 5);
            var c = cga.EncodeIpnsRound.Point(7.4, -1.5, -4.5);
            var d = cga.EncodeIpnsRound.Point(3, -2, 5);

            // Use the outer product to define the OPNS blade encoding a
            // sphere passing through points a,b,c
            var sphere = a.Op(b).Op(c).Op(d);

            // Encode a line passing through a point parallel to
            // a direction vector
            var line = cga.EncodeOpnsFlat.Line(
                scalarProcessor.Vector3D(3.5, 4.3, 2.6),
                scalarProcessor.Vector3D(1, 1, 1)
            );

            // Project line on sphere to get a circle
            var circle = line.ProjectOpnsOn(sphere);

            // Decode the circle to separate its individual Euclidean
            // geometric components
            var circleComponents = circle.Decode.OpnsRound.Element();

            // Center of circle:
            var center = circleComponents.CenterToVector3D();

            // Radius of circle:
            var radius = circleComponents.RealRadius;

            // Direction bivector of circle
            var bivector = circleComponents.DirectionToBivector3D();

            // Normal direction to circle
            var normal = circleComponents.NormalDirectionToVector3D();

            Console.WriteLine($"Center  : {center}");
            Console.WriteLine($"Radius  : {radius}");
            Console.WriteLine($"Bivector: {bivector}");
            Console.WriteLine($"Normal  : {normal}");
            Console.WriteLine();
        }
    }
}
