// See https://aka.ms/new-console-template for more information

using GeometricAlgebraFulcrumLib.Applications.PowerSystems.GeometricFrequency;

//InverseKinematics6RSamples.Example1();

namespace GeometricAlgebraFulcrumLib.Applications;

internal static class Program
{
    public static void Main(string[] args)
    {
        PowerSignalVisualizationSample1.ExecutePovRay();

        Console.WriteLine("Press any key to exit..");
        Console.ReadKey();
    }
}