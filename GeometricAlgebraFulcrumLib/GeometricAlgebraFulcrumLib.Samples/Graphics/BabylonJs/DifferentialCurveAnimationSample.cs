using System;
using System.IO;
using DataStructuresLib.Basic;
using DataStructuresLib.Files;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Curves;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions.Phasors;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Adaptive;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GraphicsComposerLib.Rendering.BabylonJs;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Animations;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Basic;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Curves;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Styles;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;
using SixLabors.ImageSharp;
using WebComposerLib.Html.Media;

namespace GeometricAlgebraFulcrumLib.Samples.Graphics.BabylonJs;

public static class DifferentialCurveAnimationSample
{
    private const string WorkingFolder = @"D:\Projects\Study\Web\Babylon.js";
    private const int CanvasWidth = 1024;
    private const int CanvasHeight = 728;
    private const int GridUnitCount = 24;
    private const double CameraDistance = 15;
    private const int FrameRate = 10;


    private static double MaxTime { get; set; }

    private static GrVisualAnimationSpecs AnimationSpecs { get; set; }

    private static Float64Vector3D AxesOrigin { get; }
        = Float64Vector3D.Zero;

    private static GrBabylonJsHtmlComposer3D HtmlComposer { get; set; }

    private static GrBabylonJsSceneComposer3D MainSceneComposer
        => HtmlComposer.FirstSceneComposer;

    private static GrBabylonJsScene MainScene
        => HtmlComposer.FirstScene;

    private static WclHtmlImageDataUrlCache ImageCache
        => HtmlComposer.ImageCache;


    private static void InitializeSceneComposers(int index)
    {
        var mainSceneComposer = new GrBabylonJsSceneComposer3D(
            "mainScene",
            new GrBabylonJsSnapshotSpecs
            {
                Enabled = false,
                Width = CanvasWidth,
                Height = CanvasHeight,
                Precision = 1,
                UsePrecision = true,
                Delay = index == 0 ? 2000 : 1000,
                FileName = $"Frame-{index:D6}.png"
            }
        )
        {
            BackgroundColor = Color.AliceBlue,
            ShowDebugLayer = false,
        };

        HtmlComposer = new GrBabylonJsHtmlComposer3D(mainSceneComposer)
        {
            CanvasWidth = CanvasWidth,
            CanvasHeight = CanvasHeight,
            CanvasFullScreen = false
        };
    }

    private static void InitializeImageCache()
    {
        var workingPath = Path.Combine(WorkingFolder, "images");

        Console.Write("Generating images cache .. ");

        ImageCache.MarginSize = 0;
        ImageCache.BackgroundColor = Color.FromRgba(255, 255, 255, 0);
        
        ImageCache.AddPngFromFile(
            "copyright",
            workingPath.GetFilePath("Copyright.png")
        );

        //for (var i = 0; i < Signal.TimeValues.Count; i++)
        //{
        //    ImageCache.AddPng(
        //            $"SignalPlot-{i:D6}",
        //            Signal.GetSignalPlotImage(i, PlotSampleCount)
        //    );
        //    ImageCache.AddPng(
        //            $"CurvaturePlot-{i:D6}",
        //            Signal.GetCurvaturesPlotImage(i, PlotSampleCount)
        //        );
        //    ImageCache.AddPng(
        //            $"FrequencyHzPlot-{i:D6}",
        //            Signal.GetFrequencyHzPlotImage(i, PlotSampleCount)
        //        );
        //}

        //Console.WriteLine("done.");
        //Console.WriteLine();

        //Console.Write("Generating LaTeX images .. ");

        ImageCache.MarginSize = 0;
        ImageCache.BackgroundColor = Color.FromRgba(255, 255, 255, 0);

        ImageCache.AddLaTeXCode(
            "basis1VectorText",
            @"\boldsymbol{\sigma}_{1}"
        );

        ImageCache.AddLaTeXCode(
            "basis2VectorText",
            @"\boldsymbol{\sigma}_{2}"
        );

        ImageCache.AddLaTeXCode(
            "basis3VectorText",
            @"\boldsymbol{\sigma}_{3}"
        );

        ImageCache.AddLaTeXCode(
            "v1VectorText",
            @"\boldsymbol{v}_{1}"
        );

        ImageCache.AddLaTeXCode(
            "v2VectorText",
            @"\boldsymbol{v}_{2}"
        );

        ImageCache.AddLaTeXCode(
            "v3VectorText",
            @"\boldsymbol{v}_{3}"
        );

        ImageCache.AddLaTeXCode(
            "vVectorText",
            @"\boldsymbol{v}"
        );
        
        ImageCache.AddLaTeXCode(
            "u1VectorText",
            @"\boldsymbol{u}_{1}"
        );

        ImageCache.AddLaTeXCode(
            "u2VectorText",
            @"\boldsymbol{u}_{2}"
        );

        ImageCache.AddLaTeXCode(
            "u3VectorText",
            @"\boldsymbol{u}_{3}"
        );

        ImageCache.AddLaTeXCode(
            "e1VectorText",
            @"\boldsymbol{e}_{1}"
        );

        ImageCache.AddLaTeXCode(
            "e2VectorText",
            @"\boldsymbol{e}_{2}"
        );

        ImageCache.AddLaTeXCode(
            "e3VectorText",
            @"\boldsymbol{e}_{3}"
        );

        ImageCache.AddLaTeXCode(
            "kVectorText",
            @"\hat{\boldsymbol{k}}"
        );

        ImageCache.AddLaTeXCode(
            "e1DsVectorText",
            @"\dot{\boldsymbol{e}}_{1}"
        );

        ImageCache.AddLaTeXCode(
            "e2DsVectorText",
            @"\dot{\boldsymbol{e}}_{2}"
        );

        ImageCache.AddLaTeXCode(
            "e3DsVectorText",
            @"\dot{\boldsymbol{e}}_{3}"
        );

        ImageCache.AddLaTeXCode(
            "omega1BivectorText",
            @"\boldsymbol{\Omega}_{1}"
        );

        ImageCache.AddLaTeXCode(
            "omega2BivectorText",
            @"\boldsymbol{\Omega}_{2}"
        );

        ImageCache.AddLaTeXCode(
            "omega3BivectorText",
            @"\boldsymbol{\Omega}_{3}"
        );


        //ImageCache.MarginSize = 20;
        ////ImageCache.BackgroundColor = Color.FromRgba(32, 32, 255, 16);

        //ImageCache.AddLaTeXCode("symbolicSignalText", Signal.LaTeXCode);

        //for (var i = 0; i < Signal.TimeValues.Count; i++)
        //{
        //    var t = Signal.TimeValues[i];
        //    var (x, y, z) = Signal.GetComponentVectors(t);
        //    var v = x + y + z;

        //    var frame = Signal.FrameList[i]; //GetSignalFrame(t);

        //    var e1 = frame.Direction1.ToUnitVector();
        //    var e2 = frame.Direction2.ToUnitVector();
        //    var e3 = frame.Direction3.ToUnitVector();
        //    var sDt = Signal.GetTangentNormValue(t);

        //    var (kappa1, kappa2) = Signal.CurvatureList[i];

        //    var omega = Signal.DarbouxBivectorList[i];
        //    var omegaNorm = omega.Norm();

        //    var omegaMean = Signal.GetDarbouxBivectorMean(i);
        //    var omegaMeanNorm = omegaMean.Norm();

        //    var frequencyHz = omegaNorm / (2d * Math.PI);
        //    var frequencyHzMean = omegaMeanNorm / (2 * Math.PI);

        //    var e1Ds = kappa1 * e2;
        //    var e2Ds = kappa2 * e3 - kappa1 * e1;
        //    var e3Ds = -kappa2 * e2;

        //    var kVector = omega.UnDual().ToUnitVector();
        //    var kVectorMean = omegaMean.UnDual().ToUnitVector();

        //    ImageCache.AddLaTeXAlignedEquations(
        //        $"signalText-{i:D6}",
        //        new Pair<string>[]
        //        {
        //                new (@"t", @$"{t:F4}"),
        //                new (@"\boldsymbol{v}\left( t \right)", @$"\left( {v.X:F4}, {v.Y:F4}, {v.Z:F4} \right)"),
        //                new (@"\left\Vert \boldsymbol{v}^{\prime}\left(t\right)\right\Vert", @$"s^{{\prime}} \left( t \right) = {sDt:F4}"),
        //                new (@"\boldsymbol{e}_{1}\left( t \right)", @$"\left( {e1.X:F4}, {e1.Y:F4}, {e1.Z:F4} \right)"),
        //                new (@"\boldsymbol{e}_{2}\left( t \right)", @$"\left( {e2.X:F4}, {e2.Y:F4}, {e2.Z:F4} \right)"),
        //                new (@"\boldsymbol{e}_{3}\left( t \right)", @$"\left( {e3.X:F4}, {e3.Y:F4}, {e3.Z:F4} \right)"),
        //                new (@"\left\Vert \boldsymbol{\Omega}_{1}\left(t\right)\right\Vert", @$"\left| s^{{\prime}} \kappa_{{1}} \right| = {kappa1:F4}"),
        //                new (@"\left\Vert \boldsymbol{\Omega}_{2}\left(t\right)\right\Vert", @$"\left| s^{{\prime}} \right| \sqrt{{\kappa_{{1}}^{{2}}+\kappa_{{2}}^{{2}}}} = {omegaNorm:F4}"),
        //                new (@"\left\Vert \boldsymbol{\Omega}_{3}\left(t\right)\right\Vert", @$"\left| s^{{\prime}} \kappa_{{2}} \right| = {kappa2:F4}"),
        //                new (@"f \left( t \right)", $@"{frequencyHz:F4} \textrm{{ Hz}}"),
        //                new (@"\hat{\boldsymbol{k}}\left( t \right)", @$"\left( {kVector.X:F4}, {kVector.Y:F4}, {kVector.Z:F4} \right)"),
        //                new (@"\overline{f}", $@"{frequencyHzMean:F4} \textrm{{ Hz}}"),
        //                new (@"\overline{\boldsymbol{k}}", @$"\left( {kVectorMean.X:F4}, {kVectorMean.Y:F4}, {kVectorMean.Z:F4} \right)")
        //        }
        //    );
        //}

        //var latexImageComposer = new GrLaTeXImageComposer
        //{
        //    LaTeXBinFolder = @"D:\texlive\2021\bin\win32\",
        //    Resolution = 200
        //};

        //ImageCache.GeneratePngBase64Strings(latexImageComposer);

        ImageCache.GeneratePngDataUrlStrings(WorkingFolder);
        //ImageCache.GenerateSvgDataUrlStrings(WorkingPath);

        //var maxWidth = 0;
        //var maxHeight = 0;
        //for (var i = 0; i < Signal.TimeValues.Count; i++)
        //{
        //    var imageData = ImageCache[$"signalText-{i:D6}"];

        //    if (maxWidth < imageData.Width) maxWidth = imageData.Width;
        //    if (maxHeight < imageData.Height) maxHeight = imageData.Height;
        //}

        //SignalTextImageMaxWidth = maxWidth;
        //SignalTextImageMaxHeight = maxHeight;

        Console.WriteLine("done.");
        Console.WriteLine();
    }


    public static DifferentialCurve3D GetCurve1()
    {
        const double freqHz = 0.1d;
        const double freq = 2d * Math.PI * freqHz;
        const double magnitude = 4d;

        MaxTime = 1d / freqHz;

        var curve = DifferentialCurve3D.Create(
            DfCosPhasor.Create(magnitude, freq),
            DfCosPhasor.Create(magnitude, freq, Float64PlanarAngle.Angle120),
            DfCosPhasor.Create(magnitude, freq, Float64PlanarAngle.Angle240)
        );

        return curve;
    }
    
    public static DifferentialCurve3D GetCurve2()
    {
        const double freqHz = 0.04d;
        const double freq = 2d * Math.PI * freqHz;
        
        MaxTime = 1d / freqHz;

        var xAngle = Float64PlanarAngle.Angle0;
        var xFunction =
            DfCosPhasor.Create(4, freq, xAngle) +
            DfCosPhasor.Create(0.3, 3 * freq, 3 * xAngle) +
            DfCosPhasor.Create(0.035, 7 * freq, 7 * xAngle);

        var yAngle = Float64PlanarAngle.Angle120;
        var yFunction =
            DfCosPhasor.Create(4.2, freq, yAngle) +
            DfCosPhasor.Create(0.4, 3 * freq, 3 * yAngle) +
            DfCosPhasor.Create(0.05, 7 * freq, 7 * yAngle);
        
        var zAngle = Float64PlanarAngle.Angle240;
        var zFunction =
            DfCosPhasor.Create(4.7, freq, zAngle) +
            DfCosPhasor.Create(0.45, 3 * freq, 3 * zAngle) +
            DfCosPhasor.Create(0.075, 7 * freq, 7 * zAngle);

        var curve = DifferentialCurve3D.Create(
            xFunction,
            yFunction,
            zFunction
        );

        return curve;
    }

    public static void Example1()
    {
        var vCurve = GetCurve2();
        
        AnimationSpecs = GrVisualAnimationSpecs.Create(FrameRate, MaxTime);

        //var (v1Curve, v2Curve, v3Curve) = 
        //    vCurve.GetComponentCurves();

        var curveDerivative1 = ComputedParametricCurve3D.Create(time => 
            vCurve
                .GetArcLengthDerivative1Point(time)
                .ToUnitVector(false)
            );

        var curveDerivative2 = ComputedParametricCurve3D.Create(time => 
            vCurve
                .GetArcLengthDerivative2Point(time)
                .ToUnitVector(false)
            );
        
        var curveDerivative3 = ComputedParametricCurve3D.Create(time => 
            vCurve
                .GetArcLengthDerivative3Point(time)
                .ToUnitVector(false)
            );

        InitializeSceneComposers(0);
        
        InitializeImageCache();

        MainSceneComposer
            .AddDefaultGrid(GridUnitCount)
            .AddDefaultAxes(AxesOrigin)
            .AddDefaultEnvironment(GridUnitCount)
            .AddDefaultPerspectiveCamera(
                CameraDistance,
                "2 * Math.PI / 20",
                "2 * Math.PI / 5"
            );

        var redMaterial =
            Color.Red.ToBabylonJsSimpleMaterial("redMaterial");

        var greenMaterial =
            Color.Green.ToBabylonJsSimpleMaterial("greenMaterial");

        var blueMaterial =
            Color.Blue.ToBabylonJsSimpleMaterial("blueMaterial");

        var orangeMaterial =
            Color.DarkOrange.ToBabylonJsSimpleMaterial("orangeMaterial");

        MainSceneComposer.AddMaterials(
            redMaterial,
            greenMaterial,
            blueMaterial,
            orangeMaterial
        );
        

        var (v1AnimatedVector, v2AnimatedVector, v3AnimatedVector) = 
            vCurve
                .GetComponentCurves()
                .MapItems(curve =>
                    curve.CreateAnimatedVector(AnimationSpecs.TimeRange)
                );

        var v12AnimatedVector = 
            v1AnimatedVector + v2AnimatedVector;
        
        var v23AnimatedVector = 
            v2AnimatedVector + v3AnimatedVector;
        
        var v31AnimatedVector = 
            v3AnimatedVector + v1AnimatedVector;

        var vAnimatedVector = 
            vCurve.CreateAnimatedVector(AnimationSpecs.TimeRange);
        
        var u1AnimatedVector = 
            curveDerivative1.CreateAnimatedVector(AnimationSpecs.TimeRange);
        
        var u2AnimatedVector = 
            curveDerivative2.CreateAnimatedVector(AnimationSpecs.TimeRange);

        var u3AnimatedVector = 
            curveDerivative3.CreateAnimatedVector(AnimationSpecs.TimeRange);
        

        MainSceneComposer.AddVector(
            GrVisualVector3D.CreateAnimated(
                "vVector",
                orangeMaterial.CreateTubeCurveStyle(0.05),
                vAnimatedVector,
                AnimationSpecs
            )
        );
        
        MainSceneComposer.AddLaTeXText(
            "vVectorText",
            ImageCache,
            vAnimatedVector.AddLength(0.25),
            HtmlComposer.LaTeXScalingFactor,
            AnimationSpecs
        );


        MainSceneComposer.AddVector(
            GrVisualVector3D.CreateAnimated(
                "v1Vector",
                redMaterial.CreateTubeCurveStyle(0.05),
                v1AnimatedVector,
                AnimationSpecs
            )
        );
        
        MainSceneComposer.AddLaTeXText(
            "v1VectorText",
            ImageCache,
            v1AnimatedVector.AddLength(0.25),
            HtmlComposer.LaTeXScalingFactor,
            AnimationSpecs
        );


        MainSceneComposer.AddVector(
            GrVisualVector3D.CreateAnimated(
                "v2Vector",
                greenMaterial.CreateTubeCurveStyle(0.05),
                v2AnimatedVector,
                AnimationSpecs
            )
        );
        
        MainSceneComposer.AddLaTeXText(
            "v2VectorText",
            ImageCache,
            v2AnimatedVector.AddLength(0.25),
            HtmlComposer.LaTeXScalingFactor,
            AnimationSpecs
        );


        MainSceneComposer.AddVector(
            GrVisualVector3D.CreateAnimated(
                "v3Vector",
                blueMaterial.CreateTubeCurveStyle(0.05),
                v3AnimatedVector,
                AnimationSpecs
            )
        );
        
        MainSceneComposer.AddLaTeXText(
            "v3VectorText",
            ImageCache,
            v3AnimatedVector.AddLength(0.25),
            HtmlComposer.LaTeXScalingFactor,
            AnimationSpecs
        );


        var dashedLinesStyle = 
            Color.DarkOrange.CreateDashedLineCurveStyle(3, 2, 16);

        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                "line12v1",
                dashedLinesStyle,
                v12AnimatedVector,
                v1AnimatedVector,
                AnimationSpecs
            )
        );
        
        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                "line12v2",
                dashedLinesStyle,
                v12AnimatedVector,
                v2AnimatedVector,
                AnimationSpecs
            )
        );
        
        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                "line23v2",
                dashedLinesStyle,
                v23AnimatedVector,
                v2AnimatedVector,
                AnimationSpecs
            )
        );

        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                "line23v3",
                dashedLinesStyle,
                v23AnimatedVector,
                v3AnimatedVector,
                AnimationSpecs
            )
        );
        
        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                "line31v3",
                dashedLinesStyle,
                v31AnimatedVector,
                v3AnimatedVector,
                AnimationSpecs
            )
        );

        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                "line31v1",
                dashedLinesStyle,
                v31AnimatedVector,
                v1AnimatedVector,
                AnimationSpecs
            )
        );
        
        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                "line12v",
                dashedLinesStyle,
                v12AnimatedVector,
                vAnimatedVector,
                AnimationSpecs
            )
        );
        
        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                "line23v",
                dashedLinesStyle,
                v23AnimatedVector,
                vAnimatedVector,
                AnimationSpecs
            )
        );

        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                "line31v",
                dashedLinesStyle,
                v31AnimatedVector,
                vAnimatedVector,
                AnimationSpecs
            )
        );


        MainSceneComposer.AddLinePath(
            GrVisualPointPathCurve3D.CreateStatic(
                "curvePath",
                orangeMaterial.CreateTubeCurveStyle(0.03),
                vCurve.CreateAdaptiveCurve3D(
                    AnimationSpecs.TimeRange,
                    new AdaptiveCurveSamplingOptions3D(
                        5d.DegreesToAngle(), 
                        1, 
                        10
                    )
                )
            )
        );


        var curveFrame = GrVisualFrame3D.CreateAnimated(
            "curveFrame",
            new GrVisualFrameStyle3D
            {
                OriginStyle = orangeMaterial.CreateThickSurfaceStyle(0.065),
                Direction1Style = redMaterial.CreateTubeCurveStyle(0.05),
                Direction2Style = greenMaterial.CreateTubeCurveStyle(0.05),
                Direction3Style = blueMaterial.CreateTubeCurveStyle(0.05)
            },
            vAnimatedVector,
            u1AnimatedVector,
            u2AnimatedVector,
            u3AnimatedVector,
            AnimationSpecs
        );

        MainSceneComposer.AddFrame(curveFrame);
        
        MainSceneComposer.AddLaTeXText(
            "u1VectorText",
            ImageCache,
            curveFrame.AnimatedOrigin + 
            curveFrame.AnimatedDirection1.AddLength(0.25),
            HtmlComposer.LaTeXScalingFactor,
            AnimationSpecs
        );
        
        MainSceneComposer.AddLaTeXText(
            "u2VectorText",
            ImageCache,
            curveFrame.AnimatedOrigin + 
            curveFrame.AnimatedDirection2.AddLength(0.25),
            HtmlComposer.LaTeXScalingFactor,
            AnimationSpecs
        );

        MainSceneComposer.AddLaTeXText(
            "u3VectorText",
            ImageCache,
            curveFrame.AnimatedOrigin + 
            curveFrame.AnimatedDirection3.AddLength(0.25),
            HtmlComposer.LaTeXScalingFactor,
            AnimationSpecs
        );
        
        var curveFrameBoundsMaterial = 
            MainSceneComposer.AddOrGetColorMaterial(
                Color.Orange.WithAlpha(0.25f)
            );

        //MainSceneComposer.AddParallelepipedSurface(
        //    GrVisualParallelepipedSurface3D.CreateAnimated(
        //        "curveBounds",
        //        curveFrameBoundsMaterial.CreateThinSurfaceStyle(),
        //        Float64Tuple3D.Zero, 
        //        v1AnimatedVector,
        //        v2AnimatedVector,
        //        v3AnimatedVector,
        //        animationSpecs
        //    )
        //);

        var quaternionCurve = 
            vCurve.GetFrenetFrameRotationQuaternionsCurve();

        var e1AnimatedVector =
            ComputedParametricCurve3D.Create(
                time => quaternionCurve.GetPoint(time).RotateVector(LinUnitBasisVector3D.PositiveX)
            ).CreateAnimatedVector(AnimationSpecs.TimeRange);
        
        var e2AnimatedVector =
            ComputedParametricCurve3D.Create(
                time => quaternionCurve.GetPoint(time).RotateVector(LinUnitBasisVector3D.PositiveY)
            ).CreateAnimatedVector(AnimationSpecs.TimeRange);

        var e3AnimatedVector =
            ComputedParametricCurve3D.Create(
                time => quaternionCurve.GetPoint(time).RotateVector(LinUnitBasisVector3D.PositiveZ)
            ).CreateAnimatedVector(AnimationSpecs.TimeRange);

        MainSceneComposer.AddParallelepipedSurface(
            GrVisualParallelepipedSurface3D.CreateAnimated(
                "curveFrameBounds",
                curveFrameBoundsMaterial.CreateThinSurfaceStyle(),
                vAnimatedVector,
                e1AnimatedVector,
                e2AnimatedVector,
                e3AnimatedVector,
                AnimationSpecs
            )
        );


        var htmlCode = HtmlComposer.GetHtmlCode();

        var htmlFilePath = WorkingFolder.GetFilePath(
            @$"DifferentialCurveAnimation",
            "html"
        );

        File.WriteAllText(htmlFilePath, htmlCode);
    }

}