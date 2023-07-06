using System;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Spaces;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Spaces.Conformal;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Text;

namespace GeometricAlgebraFulcrumLib.Samples.ConformalGeometry
{
    public static class BasicGeometrySample
    {
        public static void Execute()
        {
            var processor =
                ScalarProcessorOfAngouriMathEntity
                    .DefaultProcessor
                    .CreateConformalXGaProcessor();

            var space = processor.CreateSpace(5);

            var textComposer =
                TextComposerEntity.DefaultComposer;

            var latexComposer =
                LaTeXAngouriMathComposer.DefaultComposer;

            var pointA = 
                processor.CreateVector("A_x", "A_y", "A_z");

            var pointB = 
                processor.CreateVector("B_x", "B_y", "B_z");

            var pointC = 
                processor.CreateVector("C_x", "C_y", "C_z");

            var pointD = 
                processor.CreateVector("D_x", "D_y", "D_z");

            var normalN = 
                processor.CreateVector("N_x", "N_y", "N_z");

            var normalM = 
                processor.CreateVector("M_x", "M_y", "M_z");

            // Create IPNS conformal point
            var ipnsA = "a" * space.CreateIpnsPoint(pointA);

            // Create IPNS conformal sphere
            var ipnsSphere1 = 
                "s_1" * space.CreateIpnsHyperSphere(pointA, "R_1^2");

            var ipnsSphere2 = 
                "s_2" * space.CreateIpnsHyperSphere(pointB, "R_2^2");

            // Create IPNS conformal plane
            var ipnsPlane1 = 
                space.CreateIpnsHyperPlane(normalN, "d_1");
            
            var ipnsPlane2 = 
                space.CreateIpnsHyperPlane(normalM, "d_2");
            
            Console.WriteLine($"IPNS point = {textComposer.GetMultivectorText(ipnsA.Vector)}");
            Console.WriteLine($"point square = {textComposer.GetScalarText(ipnsA.Square().Scalar)}");
            Console.WriteLine($"point weight = {textComposer.GetScalarText(ipnsA.Weight())}");
            Console.WriteLine($"point distance = {textComposer.GetScalarText(ipnsA.GetDistance(pointB))}");
            Console.WriteLine();
            
            Console.WriteLine($"IPNS plane = {textComposer.GetMultivectorText(ipnsPlane1.Vector)}");
            Console.WriteLine($"plane square = {textComposer.GetScalarText(ipnsPlane1.Square())}");
            Console.WriteLine($"plane weight = {textComposer.GetScalarText(ipnsPlane1.Weight())}");
            Console.WriteLine($"point distance = {textComposer.GetScalarText(ipnsPlane1.GetDistance(pointB))}");
            Console.WriteLine($"sphere distance = {textComposer.GetScalarText(ipnsPlane1.GetDistance(ipnsSphere1))}");
            Console.WriteLine($"plane distance = {textComposer.GetScalarText(ipnsPlane1.GetDistance(ipnsPlane2))}");
            Console.WriteLine();

            Console.WriteLine($"IPNS sphere = {textComposer.GetMultivectorText(ipnsSphere1.Vector)}");
            Console.WriteLine($"sphere square = {textComposer.GetScalarText(ipnsSphere1.Square())}");
            Console.WriteLine($"sphere weight = {textComposer.GetScalarText(ipnsSphere1.Weight())}");
            Console.WriteLine($"point distance = {textComposer.GetScalarText(ipnsSphere1.GetDistance(pointB))}");
            Console.WriteLine($"sphere distance = {textComposer.GetScalarText(ipnsSphere1.GetDistance(ipnsSphere2))}");
            Console.WriteLine($"plane distance = {textComposer.GetScalarText(ipnsSphere1.GetDistance(ipnsPlane1))}");
            Console.WriteLine();
        }
    }
}
