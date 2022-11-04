using System.Collections.Immutable;
using System.Linq;
using GeometricAlgebraFulcrumLib.Applications.Graphics;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GeometricAlgebraFulcrumLib.Samples.Graphics.BabylonJs;

public static class RotorFamilySamples
{
    private const string WorkingPath = @"D:\Projects\Study\Babylon.js";

    public static void Example1()
    {
        const int frameCount = 1001;

        var sourceVector = Tuple3D.E3;
        var targetVector = new Tuple3D(1, 1, 1).ToUnitVector();


        var cameraAlphaValues =
            60d.DegreesToRadians().GetCosRange(
                150d.DegreesToRadians(),
                frameCount,
                1,
                false
            ).ToImmutableArray();

        //var cameraAlphaValues =
        //    Enumerable
        //        .Repeat(120d.DegreesToRadians(), frameCount)
        //        .ToImmutableArray();

        var cameraBetaValues =
            Enumerable
                .Repeat(75d.DegreesToRadians(), frameCount)
                .ToImmutableArray();

        const double thetaEpsilon = 5e-5d;
        var thetaValues =
            (-90d + thetaEpsilon).GetCosRange(90d - thetaEpsilon, frameCount, 1, false)
            .Select(t => t.DegreesToAngle())
            .ToImmutableArray();

        var visualizer = new RotorFamilyVisualizer3D(
            cameraAlphaValues, 
            cameraBetaValues, 
            sourceVector, 
            targetVector,
            thetaValues
        )
        {
            Title = "Rotor Family of Two Vectors",
            WorkingPath = @"D:\Projects\Study\Babylon.js\",
            HostUrl = "http://localhost:5200/", 
            //LiveReloadWebServer "D:/Projects/Study/Babylon.js/" --port 5200 --UseSsl False --LiveReloadEnabled False --OpenBrowser True

            CameraDistance = 15,
            GridUnitCount = 20,
            ShowCopyright = true,
            LaTeXScalingFactor = 1d / 60d,
            DrawRotorTrace = false,

            GenerateHtml = true,
            GeneratePng = true,
            GenerateAnimatedGif = false,
            GenerateMp4 = false
        };

        visualizer.GenerateSnapshots();
    }
}