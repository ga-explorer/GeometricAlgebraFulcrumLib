using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Applications.PowerSystems;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Algebra.Signals.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer;

namespace GeometricAlgebraFulcrumLib.Samples.PowerSystems.GeometricFrequency;

public static class PowerSignalVisualizationSample3
{
    public static double FrequencyHz
        => 0.1;

    public static double Frequency
        => 2d * Math.PI * FrequencyHz;

    public static Triplet<LinFloat64PolarVector2D> Phasors { get; }
    //= new Triplet<PolarPosition2D>(
    //    0d.DegreesToPolarAngle().ToPolarVector2D(4),
    //    240d.DegreesToPolarAngle().ToPolarVector2D(4),
    //    120.DegreesToPolarAngle().ToPolarVector2D(4)
    //);

        = new Triplet<LinFloat64PolarVector2D>(
            0d.DegreesToPolarAngle().ToPolarVector2D(2),
            230d.DegreesToPolarAngle().ToPolarVector2D(4),
            135d.DegreesToPolarAngle().ToPolarVector2D(1)
        );


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

    private static SymmetricalComponentsSignal3D GetPowerSignal()
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

        var (phasor1, phasor2, phasor3) = Phasors;

        var powerSignal = SymmetricalComponentsSignal3D.Create(
            tValues,
            Frequency,
            phasor1,
            phasor2,
            phasor3
        );

        powerSignal.LaTeXCode = GetSignalLaTeXCode();

        powerSignal.InitializeComponents();

        return powerSignal;
    }

    public static void Execute()
    {
        const bool renderAnimations = true;

        var powerSignal = GetPowerSignal();

        //const double frequency = 2 * Math.PI * 0.1d;

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

        var visualizer = new SymmetricalComponentsSignalVisualizer3D(cameraAlphaValues, cameraBetaValues, powerSignal)
        {
            Title = "Rotated sinusoidal symmetrical components",
            WorkingFolder = @"D:\Projects\Study\Web\Babylon.js\",
            HostUrl = "http://localhost:5200/",
            //LiveReloadWebServer "D:/Projects/Study/Babylon.js/" --port 5200 --UseSsl False --LiveReloadEnabled False --OpenBrowser True

            CameraDistance = 11,
            CameraRotationCount = 1,
            TrailSampleCount = powerSignal.SampleCount / 2,
            PlotSampleCount = powerSignal.SampleCount / 2,
            FrameSeparationCount = 20,
            ShowAxes = false,
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