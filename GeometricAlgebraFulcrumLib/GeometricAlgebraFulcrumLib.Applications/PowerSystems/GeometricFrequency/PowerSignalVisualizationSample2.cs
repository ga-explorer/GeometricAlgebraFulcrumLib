using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Interpolators;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals;
using GeometricAlgebraFulcrumLib.Modeling.Signals.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Utilities;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using OfficeOpenXml;

namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems.GeometricFrequency;

public static class PowerSignalVisualizationSample2
{
    //private static string GetSignalLaTeXCode()
    //{
    //    var m = Magnitudes;
    //    var f = HarmonicFactors.Select(
    //        v => v == 1 ? "" : v.ToString(CultureInfo.InvariantCulture)
    //    ).ToArray();

    //    var latexComposer = new LaTeXAlignedEquationComposer
    //    {
    //        AddEquationNumber = false
    //    };

    //    latexComposer
    //        .AppendLaTeXBegin()
    //        .AppendLaTeXEqual(
    //            @"\boldsymbol{v}\left( t \right)",
    //            @"v_{1}\left(t\right)\boldsymbol{\sigma}_{1}+v_{2}\left(t\right)\boldsymbol{\sigma}_{2}+v_{3}\left(t\right)\boldsymbol{\sigma}_{3}"
    //        )
    //        .AppendLaTeXEqual(
    //            @"v_{1}\left( t \right)",
    //            @$"{m[0, 0]} \cos {f[0]} \omega t"
    //        );

    //    if (ContainsHarmonics)
    //        latexComposer
    //            .AppendLaTeXPlus(@$"{m[0, 1]} \cos {f[1]} \omega t")
    //            .AppendLaTeXPlus(@$"{m[0, 2]} \cos {f[2]} \omega t");

    //    latexComposer.AppendLaTeXEqual(
    //        @"v_{2}\left( t \right)",
    //        @$"{m[1, 0]} \cos {f[0]} \left( \omega t - \frac{{2\pi}}{{3}} \right)"
    //    );

    //    if (ContainsHarmonics)
    //        latexComposer
    //            .AppendLaTeXPlus(@$"{m[1, 1]} \cos {f[1]} \left( \omega t - \frac{{2\pi}}{{3}} \right)")
    //            .AppendLaTeXPlus(@$"{m[1, 2]} \cos {f[2]} \left( \omega t - \frac{{2\pi}}{{3}} \right)");

    //    latexComposer.AppendLaTeXEqual(
    //        @"v_{3}\left( t \right)",
    //        @$"{m[2, 0]} \cos {f[0]} \left( \omega t + \frac{{2\pi}}{{3}} \right)"
    //    );

    //    if (ContainsHarmonics)
    //        latexComposer
    //            .AppendLaTeXPlus(@$"{m[2, 1]} \cos {f[1]} \left( \omega t + \frac{{2\pi}}{{3}} \right)")
    //            .AppendLaTeXPlus(@$"{m[2, 2]} \cos {f[2]} \left( \omega t + \frac{{2\pi}}{{3}} \right)");

    //    latexComposer.AppendLaTeXEnd();

    //    return latexComposer.ToString();
    //}

    private static Triplet<double[]> ReadExcelColumns(string fileName, int firstRowIndex, int rowCount, int firstColIndex)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package =
            new ExcelPackage(new FileInfo(fileName));

        var workSheet =
            package.Workbook.Worksheets[0];

        var phase1 = workSheet.ReadScalarColumn(
            firstRowIndex,
            rowCount,
            firstColIndex
        );

        var phase2 = workSheet.ReadScalarColumn(
            firstRowIndex,
            rowCount,
            firstColIndex + 1
        );

        var phase3 = workSheet.ReadScalarColumn(
            firstRowIndex,
            rowCount,
            firstColIndex + 2
        );

        return new Triplet<double[]>(phase1, phase2, phase3);
    }

    private static Float64PowerSignal3D GetPowerSignal_Aulario3()
    {
        const int cycleCount = 2;
        const int sampleCount = 480 * cycleCount + 1;
        const int downSampleFactor = 4;
        const double samplingRate = 24; //24000;
        const double tMin = 0;
        const double tMax = (sampleCount - 1) / samplingRate;
        const double tDelta = 1d / samplingRate;
        const double magnitudeFactor = 1 / 150d;
        const int bezierDegree = 4;
        const CatmullRomSplineType curveType = CatmullRomSplineType.Centripetal;

        var downSampleCount =
            (int)Math.Ceiling(sampleCount / (double)downSampleFactor);

        var smoothingFactors = new[] { 3, 5, 7, 9 };

        var tValues =
            tMin.GetLinearRange(
                tMax,
                sampleCount,
                true
            ).CreateSignal(samplingRate);

        const string excelPath =
            @"D:\Projects\Books\The Geometric Algebra Cookbook\Geometric Frequency\Data";

        var (phaseFunction1, phaseFunction2, phaseFunction3) =
            ReadExcelColumns(
                    Path.Combine(excelPath, "Aulario3_derivatives.xlsx"),
                    3,
                    sampleCount + 2,
                    5
                )
                .MapItems(p =>
                    p.Select(d => d * magnitudeFactor)
                        .CreateSignal(samplingRate)
                        .GetCatmullRomInterpolator(
                            new DfCatmullRomSplineSignalInterpolatorOptions
                            {
                                BezierDegree = bezierDegree,
                                SmoothingFactors = smoothingFactors,
                                SplineType = curveType
                            }
                        )
                );

        var powerSignal = Float64PowerSignal3D.Create(
            tValues,
            phaseFunction1,
            phaseFunction2,
            phaseFunction3
        );

        powerSignal.LaTeXCode = ""; //GetSignalLaTeXCode();

        powerSignal.InitializeComponents();

        return powerSignal;
    }

    private static Float64PowerSignal3D GetPowerSignal_EMTP()
    {
        const int cycleCount = 3;
        const int sampleCount = 2400;//;850 * cycleCount + 1;
        const int downSampleFactor = 4;
        const double samplingRate = 100; //50000;
        const double tMin = 0;
        const double tMax = (sampleCount - 1) / samplingRate;
        const double tDelta = 1d / samplingRate;
        const double magnitudeFactor = 4 / 22000d;
        const int bezierDegree = 4;
        const CatmullRomSplineType curveType = CatmullRomSplineType.Centripetal;

        var downSampleCount =
            (int)Math.Ceiling(sampleCount / (double)downSampleFactor);

        var smoothingFactors = new[] { 3, 5, 7, 9 };

        var tValues =
            tMin.GetLinearRange(
                tMax,
                sampleCount,
                true
            ).CreateSignal(samplingRate);

        const string excelPath =
            @"D:\Projects\Books\The Geometric Algebra Cookbook\Geometric Frequency\Data";

        var (phaseFunction1, phaseFunction2, phaseFunction3) =
            ReadExcelColumns(
                    Path.Combine(excelPath, "EMTP_transient_ahmad.xlsx"),
                    4000, //12000,
                    sampleCount + 2,
                    2
                )
                .MapItems(p =>
                    p.Select(d => d * magnitudeFactor)
                        .CreateSignal(samplingRate)
                        .GetCatmullRomInterpolator(
                            new DfCatmullRomSplineSignalInterpolatorOptions
                            {
                                BezierDegree = bezierDegree,
                                SmoothingFactors = smoothingFactors,
                                SplineType = curveType
                            }
                        )
                );

        var powerSignal = Float64PowerSignal3D.Create(
            tValues,
            phaseFunction1,
            phaseFunction2,
            phaseFunction3
        );

        powerSignal.LaTeXCode = "";  //GetSignalLaTeXCode();

        powerSignal.InitializeComponents();

        return powerSignal;
    }

    public static void Execute()
    {
        const bool renderAnimations = true;

        var powerSignal = GetPowerSignal_EMTP();

        var visualizer = new GrBabylonJsPowerSignalVisualizer(
            @"D:\Projects\Study\Web\Babylon.js\", 
            powerSignal.SamplingSpecs, 
            powerSignal
        )
        {
            SceneTitle = "EMTP Phase Voltages",
            HostUrl = "http://localhost:5200/",
            //LiveReloadWebServer "D:/Projects/Study/Babylon.js/" --port 5200 --UseSsl False --LiveReloadEnabled False --OpenBrowser True

            TrailSampleCount = 800,
            PlotSampleCount = 400,
            FrameSeparationCount = 20,
            ShowCopyright = true,
            ShowLeftPanel = false,
            ShowRightPanel = true,

            //Mp4FrameRate = 50,
            SceneRenderMethod = renderAnimations 
                ? GrVisualSceneSequenceComposer.RenderImageFilesMethod.PerScene 
                : GrVisualSceneSequenceComposer.RenderImageFilesMethod.Disabled,

            RenderGifFileEnabled = false,
            RenderVideoFileEnabled = renderAnimations
        };
        
        var alpha = 
            Float64ScalarSignal
                .FiniteCos(30, 150)
                .Repeat(2)
                .DegreesToRadians();

        var beta = 
            72.DegreesToRadians();

        visualizer.SetCamera(alpha, beta, 11);

        visualizer.ComposeSceneSequence();
    }
}