using GeometricAlgebraFulcrumLib.SymbolicApplications.Samples.Geometry;
using GeometricAlgebraFulcrumLib.SymbolicApplications.Samples.PowerSystems.PowerQuality;

namespace GeometricAlgebraFulcrumLib.SymbolicApplications;

internal class Program
{
    static void Main(string[] args)
    {
        //VoltageSags3Phase4WireSample.Execute();
        //SnelliusPothenotProblemSample.SymbolicCGaCassini();
        SnelliusPothenotProblemSample.GenerateCGaCollinsCode();
    }
}