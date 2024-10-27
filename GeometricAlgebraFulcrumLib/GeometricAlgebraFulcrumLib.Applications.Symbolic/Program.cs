using GeometricAlgebraFulcrumLib.Applications.Symbolic.Robotics;

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
        HansenProblemSample.GenerateCGaCodeOpt();

        //InverseKinematics6RSamples.MetaprogrammingExample2();
    }
}