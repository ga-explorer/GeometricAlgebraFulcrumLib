using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals;
using GeometricAlgebraFulcrumLib.Modeling.Signals.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer;

namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems.GeometricFrequency;

public static class PowerSignalVisualizationSample3
{
    public static double FrequencyHz
        => 0.1;

    public static double Frequency
        => Math.Tau * FrequencyHz;

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

    private static Float64SymmetricalComponentsSignal3D GetPowerSignal()
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

        var powerSignal = Float64SymmetricalComponentsSignal3D.Create(
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

        //const double frequency = Math.Tau * 0.1d;

        var omegaMean = powerSignal.GetDarbouxBivectorMean();
        var omegaMeanNorm = omegaMean.Norm();

        Console.WriteLine($"Mean omega: {omegaMean}");
        Console.WriteLine($"Mean omega norm: {omegaMeanNorm}");
        Console.WriteLine($"Mean omega norm Hz: {omegaMeanNorm / (Math.Tau)}");
        Console.WriteLine($"Mean omega norm / frequency: {omegaMeanNorm / Frequency}");
        Console.WriteLine();

        var visualizer = new GrBabylonJsSymmetricalComponentsSignalVisualizer(
            @"D:\Projects\Study\Web\Babylon.js\", 
            powerSignal.SamplingSpecs, 
            powerSignal
        )
        {
            SceneTitle = "Rotated sinusoidal symmetrical components",
            HostUrl = "http://localhost:5200/",
            //LiveReloadWebServer "D:/Projects/Study/Babylon.js/" --port 5200 --UseSsl False --LiveReloadEnabled False --OpenBrowser True

            TrailSampleCount = powerSignal.SampleCount / 2,
            PlotSampleCount = powerSignal.SampleCount / 2,
            FrameSeparationCount = 20,
            ShowAxes = false,
            ShowCopyright = true,
            ShowLeftPanel = false,
            ShowRightPanel = true,

            SceneRenderMethod = renderAnimations 
                ? GrVisualSceneSequenceComposer.RenderImageFilesMethod.PerScene
                : GrVisualSceneSequenceComposer.RenderImageFilesMethod.Disabled,

            RenderGifFileEnabled = false,
            RenderVideoFileEnabled = renderAnimations
        };
        
        var alpha = 
            Float64ScalarSignal
                .FiniteCos(30, 150)
                .DegreesToRadians();

        var beta = 
            72.DegreesToRadians();

        visualizer.SetCamera(alpha, beta, 11);

        visualizer.ComposeSceneSequence();
    }
}