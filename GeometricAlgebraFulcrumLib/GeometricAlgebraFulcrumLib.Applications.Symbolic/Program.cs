using GeometricAlgebraFulcrumLib.Applications.Symbolic.EllipseFitting;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic;

internal class Program
{
    static void Main(string[] args)
    {
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
        //HansenProblemSample.GenerateCGaCodeOpt();

        //InverseKinematics6RSamples.MetaprogrammingExample2();

        //GradedMultivectorLibSample.GenerateCode();


        //JacobiSymmetricEigenDecomposer.SampleUse();
        //EigenDecomposeSamples.Example4X4();
        //EigenDecomposeSamples.Example2();


        var code = JacobiSymmetricEigenCodeComposer.ComposeCode();

        File.WriteAllText(
            @"D:\Projects\Papers\Active\2023-Conic Fitting\FittingBenchmark\evd.cs",
            code
        );
    }
}