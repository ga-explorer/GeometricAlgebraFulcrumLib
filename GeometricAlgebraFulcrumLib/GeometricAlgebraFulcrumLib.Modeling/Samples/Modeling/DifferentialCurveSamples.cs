using GeometricAlgebraFulcrumLib.Modeling.Calculus.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling;

public static class DifferentialCurveSamples
{
    public static void Example1()
    {
        const double freqHz = 50;
        const double freq = 2 * Math.PI * freqHz;
        var t = MathDf.X;

        var f1 = 220 * MathDf.Cos(freq * t + 2 * Math.PI * 0d / 3d);
        var f2 = 220 * MathDf.Cos(freq * t + 2 * Math.PI * 1d / 3d);
        var f3 = 220 * MathDf.Cos(freq * t + 2 * Math.PI * 2d / 3d);

        var v = DifferentialCurve3D.Create(f1, f2, f3);

        var sDt = v.TangentNormFunction;
        var (vDs1, vDs2, vDs3) =
            v.GetComponentsArcLengthDerivatives3();

        Console.WriteLine(@$"v.X = {f1}");
        Console.WriteLine(@$"v.Y = {f2}");
        Console.WriteLine(@$"v.Z = {f3}");
        Console.WriteLine();

        Console.WriteLine(@$"vNorm = {sDt}");
        Console.WriteLine();

        Console.WriteLine(@$"vDs1.X = {vDs1.Item1}");
        Console.WriteLine(@$"vDs1.Y = {vDs1.Item2}");
        Console.WriteLine(@$"vDs1.Z = {vDs1.Item3}");
        Console.WriteLine();

        Console.WriteLine(@$"vDs2.X = {vDs2.Item1}");
        Console.WriteLine(@$"vDs2.Y = {vDs2.Item2}");
        Console.WriteLine(@$"vDs2.Z = {vDs2.Item3}");
        Console.WriteLine();

        Console.WriteLine(@$"vDs3.X = {vDs3.Item1}");
        Console.WriteLine(@$"vDs3.Y = {vDs3.Item2}");
        Console.WriteLine(@$"vDs3.Z = {vDs3.Item3}");
        Console.WriteLine();
    }
}