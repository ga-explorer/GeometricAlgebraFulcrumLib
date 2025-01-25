using System;

namespace GeometricAlgebraFulcrumLib.Mathematica
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Samples.Algebra.GeometricAlgebra.SymbolicRotorsSample.Example6();

            //VoltageSags3Phase4WireSample.Execute();
            //SnelliusPothenotProblemSample.SymbolicCGaCassini()

            //SnelliusPothenotProblemSample.GenerateVGaCode();
            //SnelliusPothenotProblemSample.GeneratePGaPacoCode();
            //SnelliusPothenotProblemSample.GenerateCGaPacoCode();
            //SnelliusPothenotProblemSample.GenerateCGaCollinsParallelCode();
            //SnelliusPothenotProblemSample.GenerateCGaCollinsCode();
            //SnelliusPothenotProblemSample.GenerateCGaCassiniCode();

            //HansenProblemSample.GenerateTrigCode();
            //HansenProblemSample.GenerateComplexCode();
            //HansenProblemSample.GenerateVGaCode();
            //HansenProblemSample.GenerateCGaCode();
            //GaalopComparisonsSamples.TriangleInterpolationExample();

            //Samples.ComplexAlgebra.Sample1.Example1();

            //ModelingVsAlgebraSamples.AlgebraExample();
            //ModelingVsAlgebraSamples.ModelingExample();

            //SpaceOfSpheresSamples.SphereEquation();

            Console.WriteLine();
            Console.WriteLine("Press any key to end ..");
            Console.ReadKey();
        }
    }
}
