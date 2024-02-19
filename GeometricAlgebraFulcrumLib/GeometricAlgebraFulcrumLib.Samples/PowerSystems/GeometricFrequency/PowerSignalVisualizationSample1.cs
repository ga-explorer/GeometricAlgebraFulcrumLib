using System;
using System.Globalization;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Applications.PowerSystems;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions.Phasors;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Lite.SignalAlgebra.Composers;
using WebComposerLib.LaTeX.CodeComposer;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Samples.PowerSystems.GeometricFrequency;

public static class PowerSignalVisualizationSample1
{
    public static double FrequencyHz
        => 0.1;

    public static double Frequency
        => 2d * Math.PI * FrequencyHz;

    public static double[] HarmonicFactors { get; }
        = { 1, 2, 7 };

    public static double[,] Magnitudes { get; }
        = {
            { 2, 0, 0 },
            { 4, 0, 0 },
            { 1, 0, 0 },
        };

    //= {
    //    { 3, 0.65, 0.1, -0.6 },
    //    { 4, 0.75, 0.1, -0.4 },
    //    { 2, 0.35, 0.05, -0.5 },
    //};

    //= {
    //    { 4, 0.4, -0.6 },
    //    { 4, 0.4, -0.6 },
    //    { 4, 0.4, -0.6 }
    //};

    //= {
    //    { 3, 0.65, 0.1 },
    //    { 4, 0.75, 0.1 },
    //    { 2, 0.35, 0.05 },
    //};

    //= {
    //    { 4, 0.4, -0.6 },
    //    { 4, 0.4, -0.6 },
    //    { 4, 0.4, -0.6 },
    //};

    //= {
    //    { 4, 0.4, 0.8, -0.2 },
    //    { 4, 0.4, 0.8, -0.2 },
    //    { 4, 0.4, 0.8, -0.2 },
    //};

    //= {
    //    { 3, 0.65, 0.1 },
    //    { 3, 0.65, 0.1 },
    //    { 3, 0.65, 0.1 },
    //};

    //= {
    //    { 4, 0, 0 },
    //    { 4, 0, 0 },
    //    { 4, 0, 0 },
    //};

    //= {
    //    { 4, 0.4, -0.6 },
    //    { 4, 0.4, -0.6 },
    //    { 2, 0.2, -0.3 },
    //};


    public static int HarmonicCount
        => HarmonicFactors.Length;

    public static bool ContainsHarmonics
        => HarmonicCount > 1;


    private static string GetSignalLaTeXCode1()
    {
        var m = Magnitudes;
        var f = HarmonicFactors.Select(
            v => v == 1 ? "" : v.ToString(CultureInfo.InvariantCulture)
        ).ToArray();

        var latexComposer = new LaTeXAlignedEquationComposer
        {
            AddEquationNumber = false
        };

        latexComposer
            .AppendLaTeXBegin()
            .AppendLaTeXEqual(
                @"\boldsymbol{v}\left( t \right)",
                @"v_{1}\left(t\right)\boldsymbol{\sigma}_{1}+v_{2}\left(t\right)\boldsymbol{\sigma}_{2}+v_{3}\left(t\right)\boldsymbol{\sigma}_{3}"
            )
            .AppendLaTeXEqual(
                @"v_{1}\left( t \right)",
                @$"{m[0, 0]} \cos {f[0]} \omega t"
            );

        if (ContainsHarmonics)
            latexComposer
                .AppendLaTeXPlus(@$"{m[0, 1]} \cos {f[1]} \omega t")
                .AppendLaTeXPlus(@$"{m[0, 2]} \cos {f[2]} \omega t");

        latexComposer.AppendLaTeXEqual(
            @"v_{2}\left( t \right)",
            @$"{m[1, 0]} \cos {f[0]} \left( \omega t - \frac{{2\pi}}{{3}} \right)"
        );

        if (ContainsHarmonics)
            latexComposer
                .AppendLaTeXPlus(@$"{m[1, 1]} \cos {f[1]} \left( \omega t - \frac{{2\pi}}{{3}} \right)")
                .AppendLaTeXPlus(@$"{m[1, 2]} \cos {f[2]} \left( \omega t - \frac{{2\pi}}{{3}} \right)");

        latexComposer.AppendLaTeXEqual(
            @"v_{3}\left( t \right)",
            @$"{m[2, 0]} \cos {f[0]} \left( \omega t + \frac{{2\pi}}{{3}} \right)"
        );

        if (ContainsHarmonics)
            latexComposer
                .AppendLaTeXPlus(@$"{m[2, 1]} \cos {f[1]} \left( \omega t + \frac{{2\pi}}{{3}} \right)")
                .AppendLaTeXPlus(@$"{m[2, 2]} \cos {f[2]} \left( \omega t + \frac{{2\pi}}{{3}} \right)");

        latexComposer.AppendLaTeXEnd();

        return latexComposer.ToString();
    }

    private static string GetSignalLaTeXCode()
    {
        var latexComposer = new LaTeXAlignedEquationComposer
        {
            AddEquationNumber = false
        };

        latexComposer.AppendLaTeXBegin();

        latexComposer.AppendLaTeXEqual(
            @"\boldsymbol{v}\left( t \right)",
            @"v_{1}\left(t\right)\boldsymbol{\sigma}_{1}+v_{2}\left(t\right)\boldsymbol{\sigma}_{2}+v_{3}\left(t\right)\boldsymbol{\sigma}_{3}"
        );

        latexComposer.AppendLaTeXEqual(
            @"\boldsymbol{v}_{1}\left( t \right)",
            @"200\sqrt{2}\sin\left(\omega t\right)"
        ).AppendLaTeXPlus(
            @"20\sqrt{2}\sin\left(2\omega t\right)"
        ).AppendLaTeXMinus(
            @"30\sqrt{2}\sin\left(7\omega t\right)"
        );

        latexComposer.AppendLaTeXEqual(
            @"\boldsymbol{v}_{2}\left( t \right)",
            @"200\sqrt{2}\sin\left(\omega t-\frac{2\pi}{3}\right)"
        ).AppendLaTeXPlus(
            @"20\sqrt{2}\sin\left(2\left(\omega t-\frac{2\pi}{3}\right)\right)"
        ).AppendLaTeXMinus(
            @"30\sqrt{2}\sin\left(7\left(\omega t-\frac{2\pi}{3}\right)\right)"
        );

        latexComposer.AppendLaTeXEqual(
            @"\boldsymbol{v}_{3}\left( t \right)",
            @"100\sqrt{2}\sin\left(\omega t+\frac{2\pi}{3}\right)"
        ).AppendLaTeXPlus(
            @"10\sqrt{2}\sin\left(2\left(\omega t+\frac{2\pi}{3}\right)\right)"
        ).AppendLaTeXMinus(
            @"15\sqrt{2}\sin\left(7\left(\omega t+\frac{2\pi}{3}\right)\right)"
        );

        latexComposer.AppendLaTeXEnd();

        return latexComposer.ToString();
    }

    private static PowerSignal3D GetPowerSignal1()
    {
        const int cycleCount = 2;
        const int sampleCount = 500 * cycleCount;
        const double tMin = 0;

        var tMax = cycleCount / FrequencyHz;
        var samplingRate = sampleCount / tMax;

        var tValues =
            tMin.GetLinearRange(
                tMax,
                sampleCount,
                true
            ).CreateSignal(samplingRate);

        var phase1 = DfPlus.Create(
            HarmonicCount.GetRange().Select(i => DfSinPhasor.Create(
                Magnitudes[0, i],
                Frequency * HarmonicFactors[i]
            ))
        );

        var phase2 = DfPlus.Create(
            HarmonicCount.GetRange().Select(i => DfSinPhasor.Create(
                Magnitudes[1, i],
                Frequency * HarmonicFactors[i],
                -2d * Math.PI / 3 * HarmonicFactors[i]
            ))
        );

        var phase3 = DfPlus.Create(
            HarmonicCount.GetRange().Select(i => DfSinPhasor.Create(
                Magnitudes[2, i],
                Frequency * HarmonicFactors[i],
                2d * Math.PI / 3 * HarmonicFactors[i]
            ))
        );

        var powerSignal = PowerSignal3D.Create(
            tValues,
            phase1,
            phase2,
            phase3
        );

        powerSignal.LaTeXCode = GetSignalLaTeXCode();

        powerSignal.InitializeComponents();

        return powerSignal;
    }

    private static PowerSignal3D GetPowerSignal()
    {
        const int cycleCount = 2;
        const int sampleCount = 500 * cycleCount;
        const double tMin = 0;

        var tMax = cycleCount / FrequencyHz;
        var samplingRate = sampleCount / tMax;

        var tValues =
            tMin.GetLinearRange(
                tMax,
                sampleCount,
                true
            ).CreateSignal(samplingRate);

        // 3.12327 sin(0.628319 t + 1.88399)
        // 2.97467 sin(0.628319 t - 0.459056)
        // -1.54809 sin(0.628319 t + 2.64468)

        var phase1 = DfSinPhasor.Create(3.12327, Frequency, 1.88399);
        var phase2 = DfSinPhasor.Create(2.97467, Frequency, -0.459056);
        var phase3 = DfSinPhasor.Create(-1.54809, Frequency, 2.64468);

        //var phase1 = CosFunction.Create(2, Frequency, 0.DegreesToAngle());
        //var phase2 = CosFunction.Create(4, Frequency, 230.DegreesToAngle());
        //var phase3 = CosFunction.Create(1, Frequency, 135.DegreesToAngle());

        var powerSignal = PowerSignal3D.Create(
            tValues,
            phase1,
            phase2,
            phase3
        );

        powerSignal.LaTeXCode = GetSignalLaTeXCode();

        powerSignal.InitializeComponents();

        return powerSignal;
    }

    public static void Execute()
    {
        const bool renderAnimations = true;

        var powerSignal = GetPowerSignal();

        var omegaMean = powerSignal.GetDarbouxBivectorMean();
        var omegaMeanNorm = omegaMean.Norm();

        Console.WriteLine($"Mean omega: {omegaMean}");
        Console.WriteLine($"Mean omega norm: {omegaMeanNorm}");
        Console.WriteLine($"Mean omega norm Hz: {omegaMeanNorm / (2 * Math.PI)}");
        Console.WriteLine($"Mean omega norm / frequency: {omegaMeanNorm / Frequency}");
        Console.WriteLine();

        var cameraAlphaValues =
            30d.DegreesToRadians().GetCosRange(
                150d.DegreesToRadians(),
                powerSignal.SampleCount,
                1,
                true
            ).CreateSignal(powerSignal.SamplingRate);

        var cameraBetaValues =
            Enumerable
                .Repeat(2 * Math.PI / 5, powerSignal.SampleCount)
                .CreateSignal(powerSignal.SamplingRate);

        var visualizer = new PowerSignalVisualizer3D(cameraAlphaValues, cameraBetaValues, powerSignal)
        {
            Title = "Unbalanced 3-phase sinusoidal signal",
            WorkingFolder = @"D:\Projects\Study\Web\Babylon.js\",
            HostUrl = "http://localhost:5200/",
            //LiveReloadWebServer "D:/Projects/Study/Babylon.js/" --port 5200 --UseSsl False --LiveReloadEnabled False --OpenBrowser True

            CameraDistance = 11,
            CameraRotationCount = 1,
            TrailSampleCount = powerSignal.SampleCount / 2,
            PlotSampleCount = powerSignal.SampleCount / 2,
            FrameSeparationCount = 20,
            ShowCopyright = true,
            ShowLeftPanel = false,
            ShowRightPanel = true,

            GeneratePng = renderAnimations,
            GenerateAnimatedGif = false,
            GenerateMp4 = renderAnimations
        };

        visualizer.GenerateSnapshots();
    }
}