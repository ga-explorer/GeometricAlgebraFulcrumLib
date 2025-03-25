using System.Globalization;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Interpolators;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Phasors;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Signals.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.Matlab;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems.GeometricFrequency;

public static class PowerSignalVisualizationSample1
{
    public static double FrequencyHz
        => 0.1;

    public static double Frequency
        => Math.Tau * FrequencyHz;

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

    private static Float64PowerSignal3D GetPowerSignal1()
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
                (-Math.Tau / 3 * HarmonicFactors[i]).RadiansToDirectedAngle()
            ))
        );

        var phase3 = DfPlus.Create(
            HarmonicCount.GetRange().Select(i => DfSinPhasor.Create(
                Magnitudes[2, i],
                Frequency * HarmonicFactors[i],
                (Math.Tau / 3 * HarmonicFactors[i]).RadiansToDirectedAngle()
            ))
        );

        var powerSignal = Float64PowerSignal3D.Create(
            tValues,
            phase1,
            phase2,
            phase3
        );

        powerSignal.LaTeXCode = GetSignalLaTeXCode();

        powerSignal.InitializeComponents();

        return powerSignal;
    }

    private static Float64PowerSignal3D GetPowerSignal2()
    {
        const int cycleCount = 2;
        const int sampleCount = 1000 * cycleCount;
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

        var phase1 = DfSinPhasor.Create(3.12327, Frequency, 1.88399.RadiansToDirectedAngle());
        var phase2 = DfSinPhasor.Create(2.97467, Frequency, -0.459056.RadiansToDirectedAngle());
        var phase3 = DfSinPhasor.Create(-1.54809, Frequency, 2.64468.RadiansToDirectedAngle());

        //var phase1 = CosFunction.Create(2, Frequency, 0.DegreesToAngle());
        //var phase2 = CosFunction.Create(4, Frequency, 230.DegreesToAngle());
        //var phase3 = CosFunction.Create(1, Frequency, 135.DegreesToAngle());

        var powerSignal = Float64PowerSignal3D.Create(
            tValues,
            phase1,
            phase2,
            phase3
        );

        powerSignal.LaTeXCode = GetSignalLaTeXCode();

        powerSignal.InitializeComponents();

        return powerSignal;
    }
    
    private static Float64PowerSignal3D GetPowerSignal()
    {
        const double signalFrequencyHz = 50;
        //const int cycleCount = 10;
        const int sampleOffset = 0; //59997;
        const int sampleCount = 20001; //59997;
        const int downSampleFactor = 1;
        const double timeDilation = 400;
        const double samplingRate = 100000 / timeDilation; //50000;
        const double tMin = 0;
        const double tMax = (sampleCount - 1) / samplingRate;
        const double tDelta = 1d / samplingRate;
        const double magnitudeFactor = 1.75; // 1 / 80d;
        const int bezierDegree = 4;
        const CatmullRomSplineType curveType = CatmullRomSplineType.Centripetal;

        var downSampleCount =
            (int)Math.Ceiling(sampleCount / (double)downSampleFactor);

        var smoothingFactors = new[] { 3, 5, 7, 9 };

        var matFilePath = @"D:\Projects\Study\POV-Ray\GeometricFrequency\caseR004_a.mat";

        Triplet<double[]> data;

        using (var fileStream = new FileStream(matFilePath, FileMode.Open))
        {
            var reader = new MatFileReader(fileStream);
            
            var matFile = reader.Read();

            var i12 = matFile["R004I_R32"].Value.ConvertTo2dDoubleArray();

            if (i12 is null)
                throw new InvalidOperationException();

            data = new Triplet<double[]>(
                i12.GetRow(0).Skip(sampleOffset).Take(sampleCount).ToArray(),
                i12.GetRow(1).Skip(sampleOffset).Take(sampleCount).ToArray(),
                i12.GetRow(2).Skip(sampleOffset).Take(sampleCount).ToArray()
            );
        }

        var tValues =
            tMin.GetLinearRange(
                tMax,
                sampleCount,
                false
            ).CreateSignal(samplingRate);

        var (phaseFunction1, phaseFunction2, phaseFunction3) =
            data.MapItems(p =>
                p.CreateSignal(
                    samplingRate,
                    d => d * magnitudeFactor
                ).GetCatmullRomInterpolator(
                    new DfCatmullRomSplineSignalInterpolatorOptions
                    {
                        BezierDegree = bezierDegree,
                        SmoothingFactors = smoothingFactors,
                        SplineType = curveType
                    }
                )
            );

        //var (phaseFunction1, phaseFunction2, phaseFunction3) =
        //    data.MapItems(p =>
        //        p.CreateSignal(
        //            samplingRate, 
        //            d => d * magnitudeFactor
        //        ).GetLinearInterpolator(
        //            new DfLinearSplineSignalInterpolatorOptions
        //            {
        //                SmoothingFactors = smoothingFactors
        //            }
        //        )
        //    );

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

    public static void ExecuteBabylonJs()
    {
        const bool renderAnimations = true;

        var powerSignal = GetPowerSignal();

        var omegaMean = powerSignal.GetDarbouxBivectorMean();
        var omegaMeanNorm = omegaMean.Norm();

        Console.WriteLine($"Mean omega: {omegaMean}");
        Console.WriteLine($"Mean omega norm: {omegaMeanNorm}");
        Console.WriteLine($"Mean omega norm Hz: {omegaMeanNorm / (Math.Tau)}");
        Console.WriteLine($"Mean omega norm / frequency: {omegaMeanNorm / Frequency}");
        Console.WriteLine();

        var visualizer = new GrBabylonJsPowerSignalVisualizer(
            @"D:\Projects\Study\Web\Babylon.js\", 
            powerSignal.SamplingSpecs, 
            powerSignal
        )
        {
            SceneTitle = "Unbalanced 3-phase sinusoidal signal",
            HostUrl = "http://localhost:5500/",
            //LiveReloadWebServer "D:/Projects/Study/Babylon.js/" --port 5200 --UseSsl False --LiveReloadEnabled False --OpenBrowser True

            TrailSampleCount = powerSignal.SampleCount / 2,
            PlotSampleCount = powerSignal.SampleCount / 2,
            FrameSeparationCount = 20,
            ShowCopyright = true,
            ShowLeftPanel = false,
            ShowRightPanel = true,
            LaTeXScalingFactor = 1d / 96,
            SceneRenderMethod = renderAnimations 
                ? GrVisualSceneSequenceComposer.RenderImageFilesMethod.PerScene 
                : GrVisualSceneSequenceComposer.RenderImageFilesMethod.Disabled,

            RenderGifFileEnabled = false,
            RenderVideoFileEnabled = renderAnimations
        };
        
        var alpha = 
            Float64ScalarSignal
                .FiniteRamp()
                .MapValueRangeTo(0, Math.Tau);

        var beta = 
            72.DegreesToRadians();

        visualizer.SetCamera(alpha, beta, 11);
        visualizer.SetCanvas720p();

        visualizer.ComposeSceneSequence();
    }
    
    public static void ExecutePovRay()
    {
        const bool renderAnimations = true;

        var powerSignal = GetPowerSignal();

        var omegaMean = powerSignal.GetDarbouxBivectorMean();
        var omegaMeanNorm = omegaMean.Norm();

        Console.WriteLine($"Mean omega: {omegaMean}");
        Console.WriteLine($"Mean omega norm: {omegaMeanNorm}");
        Console.WriteLine($"Mean omega norm Hz: {omegaMeanNorm / (Math.Tau)}");
        Console.WriteLine($"Mean omega norm / frequency: {omegaMeanNorm / Frequency}");
        Console.WriteLine();

        var visualizer = new GrPovRayPowerSignalVisualizer(
            @"D:\Projects\Study\POV-Ray\GeometricFrequency\", 
            Float64SamplingSpecs.Create(16, powerSignal.MaxTime), 
            powerSignal
        )
        {
            SceneTitle = "R004I_R32",

            TimeScaling = 1d / 400,
            SignalScaling = 1/1.75d, //80,

            DefaultRenderingOptions =
            {
                Display = false,
                AntiAlias = true,
                AntiAliasDepth = 2,
                MaxImageBufferMemory = 10240,
                Quality = 9,
                OutputFileType = GrPovRayOutputFileTypeValue.Png,
                //WorkThreads = 2
            },

            ShowGrid = true,
            ShowCopyright = true,
            ShowGuiLayer = true,
            ShowLeftPanel = false,
            ShowRightPanel = true,

            AxesOrigin = LinFloat64Vector3D.Create(0, -3, 0),
            GridUnitCount = 20,
            LaTeXScalingFactor = 1d / 96d,

            ClearOutputFilesEnabled = true,
            ComposeSceneFilesEnabled = true,

            MeanSampleCount = 2000,
            TrailSampleCount = 2000,
            PlotSampleCount = 2000,
            FrameSeparationCount = 100,

            SceneRenderMethod = renderAnimations 
                ? GrVisualSceneSequenceComposer.RenderImageFilesMethod.PerScene 
                : GrVisualSceneSequenceComposer.RenderImageFilesMethod.Disabled,

            RenderGifFileEnabled = false,
            RenderVideoFileEnabled = renderAnimations
        };
        
        var alpha = 
            Float64ScalarSignal
                .FiniteRamp()
                .MapValueRangeTo(90, 360 + 90)
                .DegreesToRadians();

        var beta = 
            72.DegreesToRadians();

        visualizer.SetCamera(alpha, beta, 15);
        visualizer.SetCanvas720p();
        //visualizer.SetFrameRange(601, 605);
        visualizer.ComposeSceneSequence();
        //visualizer.RenderVideoFile();
    }
}