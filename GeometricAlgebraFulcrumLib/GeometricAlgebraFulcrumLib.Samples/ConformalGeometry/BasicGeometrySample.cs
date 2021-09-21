using System;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Samples.ConformalGeometry
{
    public static class BasicGeometrySample
    {
        public static void Execute()
        {
            var processor =
                ScalarAlgebraAngouriMathProcessor
                    .DefaultProcessor
                    .CreateGeometricAlgebraConformalProcessor(5);

            var textComposer =
                TextAngouriMathComposer.DefaultComposer;

            var latexComposer =
                LaTeXAngouriMathComposer.DefaultComposer;

            var pointA = 
                processor.CreateVectorFromText("A_x", "A_y", "A_z");

            var pointB = 
                processor.CreateVectorFromText("B_x", "B_y", "B_z");

            var pointC = 
                processor.CreateVectorFromText("C_x", "C_y", "C_z");

            var pointD = 
                processor.CreateVectorFromText("D_x", "D_y", "D_z");

            var normalN = 
                processor.CreateVectorFromText("N_x", "N_y", "N_z");

            var normalM = 
                processor.CreateVectorFromText("M_x", "M_y", "M_z");

            // Create IPNS conformal point
            var ipnsA = "a" * processor.CreateIpnsPoint(pointA);

            // Create IPNS conformal sphere
            var ipnsSphere1 = 
                "s_1" * processor.CreateIpnsHyperSphere(pointA, "R_1^2");

            var ipnsSphere2 = 
                "s_2" * processor.CreateIpnsHyperSphere(pointB, "R_2^2");

            // Create IPNS conformal plane
            var ipnsPlane1 = 
                processor.CreateIpnsHyperPlane(normalN, "d_1");
            
            var ipnsPlane2 = 
                processor.CreateIpnsHyperPlane(normalM, "d_2");
            
            Console.WriteLine($"IPNS point = {textComposer.GetMultivectorText(ipnsA.VectorStorage)}");
            Console.WriteLine($"point square = {textComposer.GetScalarText(ipnsA.Square().Simplify())}");
            Console.WriteLine($"point weight = {textComposer.GetScalarText(ipnsA.Weight())}");
            Console.WriteLine($"point distance = {textComposer.GetScalarText(ipnsA.GetDistance(pointB))}");
            Console.WriteLine();
            
            Console.WriteLine($"IPNS plane = {textComposer.GetMultivectorText(ipnsPlane1)}");
            Console.WriteLine($"plane square = {textComposer.GetScalarText(ipnsPlane1.Square())}");
            Console.WriteLine($"plane weight = {textComposer.GetScalarText(ipnsPlane1.Weight())}");
            Console.WriteLine($"point distance = {textComposer.GetScalarText(ipnsPlane1.GetDistance(pointB))}");
            Console.WriteLine($"sphere distance = {textComposer.GetScalarText(ipnsPlane1.GetDistance(ipnsSphere1))}");
            Console.WriteLine($"plane distance = {textComposer.GetScalarText(ipnsPlane1.GetDistance(ipnsPlane2))}");
            Console.WriteLine();

            Console.WriteLine($"IPNS sphere = {textComposer.GetMultivectorText(ipnsSphere1)}");
            Console.WriteLine($"sphere square = {textComposer.GetScalarText(ipnsSphere1.Square())}");
            Console.WriteLine($"sphere weight = {textComposer.GetScalarText(ipnsSphere1.Weight())}");
            Console.WriteLine($"point distance = {textComposer.GetScalarText(ipnsSphere1.GetDistance(pointB))}");
            Console.WriteLine($"sphere distance = {textComposer.GetScalarText(ipnsSphere1.GetDistance(ipnsSphere2))}");
            Console.WriteLine($"plane distance = {textComposer.GetScalarText(ipnsSphere1.GetDistance(ipnsPlane1))}");
            Console.WriteLine();
        }
    }
}
