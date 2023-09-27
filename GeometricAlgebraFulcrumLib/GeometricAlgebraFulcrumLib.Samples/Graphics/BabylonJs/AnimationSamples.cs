using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using DataStructuresLib.Files;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions.Phasors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Circles;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Lines;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Basic;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Surfaces;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space3D.Rotation;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using SixLabors.ImageSharp;
using WebComposerLib.Html.Media;

namespace GeometricAlgebraFulcrumLib.Samples.Graphics.BabylonJs;

public static class AnimationSamples
{
    private const string WorkingFolder = @"D:\Projects\Study\Web\Babylon.js";
    private const int CanvasWidth = 1024;
    private const int CanvasHeight = 728;
    private const int GridUnitCount = 24;
    private const double CameraDistance = 15;

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


    public static void Example1()
    {
        const int frameRate = 25;
        const double maxTime = 3;

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);

        InitializeSceneComposers(0);

        MainSceneComposer
            .AddDefaultGridZx(GridUnitCount)
            .AddDefaultAxes(AxesOrigin)
            .AddDefaultEnvironment(GridUnitCount)
            .AddDefaultPerspectiveCamera(
                CameraDistance,
                "2 * Math.PI / 20",
                "2 * Math.PI / 5"
            );

        var material1 =
            Color.DarkTurquoise.ToBabylonJsSimpleMaterial("material1");

        var material2 =
            Color.LightBlue.ToBabylonJsSimpleMaterial("material2");

        var redMaterial =
            Color.Red.ToBabylonJsSimpleMaterial("redMaterial");

        var greenMaterial =
            Color.Green.ToBabylonJsSimpleMaterial("greenMaterial");

        var blueMaterial =
            Color.Blue.ToBabylonJsSimpleMaterial("blueMaterial");

        MainSceneComposer.AddMaterials(
            material1,
            material2,
            redMaterial,
            greenMaterial,
            blueMaterial
        );

        var curve0 = new ArcLengthLineSegment3D(
            Float64Vector3D.Zero,
            Float64Vector3D.Symmetric * 5
        );

        var curve1 = new ArcLengthLineSegment3D(
            Float64Vector3D.Zero,
            Float64Vector3D.E1 * 5
        );

        var curve2 = new ArcLengthLineSegment3D(
            Float64Vector3D.Zero,
            Float64Vector3D.E2 * 5
        );

        var curve3 = new ArcLengthLineSegment3D(
            Float64Vector3D.Zero,
            Float64Vector3D.E3 * 5
        );


        var path0 = 
            animationSpecs.CreateAnimatedVector3D(
                Float64ScalarRange.ZeroToOne,
                curve0
            );

        var path1 = 
            animationSpecs.CreateAnimatedVector3D(
                Float64ScalarRange.ZeroToOne,
                curve1
            );

        var path2 = 
            animationSpecs.CreateAnimatedVector3D(
                Float64ScalarRange.ZeroToOne,
                curve2
            );

        var path3 = 
            animationSpecs.CreateAnimatedVector3D(
                Float64ScalarRange.ZeroToOne,
                curve3
            );

        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                "line1",
                redMaterial.CreateTubeCurveStyle(0.03),
                path0,
                path1
            )
        );

        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                "line2",
                Color.Green.CreateSolidLineCurveStyle(),
                path0,
                path2
            )
        );

        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                "line3",
                Color.Blue.CreateDashedLineCurveStyle(3, 2, 16),
                path0,
                path3
            )
        );


        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                "point0",
                material2.CreateThickSurfaceStyle(0.1),
                path0
            )
        );

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                "point1",
                redMaterial.CreateThickSurfaceStyle(0.1),
                path1
            )
        );

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                "point2",
                greenMaterial.CreateThickSurfaceStyle(0.1),
                path2
            )
        );

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                "point3",
                blueMaterial.CreateThickSurfaceStyle(0.1),
                path3
            )
        );

        //var visualElementName = "katexEquation1";
        //var visualElementScalingFactor = 1d;
        //var visualElementPosition = Float64Tuple3D.Symmetric;
        //var visualElementVisibility = 1d;

        //MainScene.AddFreeCode(
        //    @$"const {visualElementName}Element = document.getElementById('{visualElementName}') as HTMLCanvasElement;",
        //    @$"katex.render(""c = \\pm\\sqrt{{a^2 + b^2}}"", {visualElementName}Element, {{throwOnError: false}});",
        //    @$"const {visualElementName}ElementRect = {visualElementName}Element.getBoundingClientRect();",
        //    @$"alert('width: ' + {visualElementName}ElementRect.width + ', height: ' + {visualElementName}ElementRect.height);"
        //);
        
        //MainScene.AddHtmlElementTexture(
        //    $"{visualElementName}ElementTexture",
        //    $"{visualElementName}Element",

        //    new GrBabylonJsHtmlElementTexture.HtmlElementTextureOptions()
        //    {
        //        //Format = GrBabylonJsTextureFormat.Alpha
        //        GenerateMipMaps = false,
        //        SamplingMode = GrBabylonJsTextureSamplingMode.Bilinear
        //    },

        //    new GrBabylonJsHtmlElementTexture.HtmlElementTextureProperties
        //    {
        //        HasAlpha = true,
        //        UScale = -1
        //    }
        //);
        
        //MainScene.AddStandardMaterial(
        //    $"{visualElementName}Material",

        //    new GrBabylonJsStandardMaterial.StandardMaterialProperties
        //    {
        //        DiffuseTexture = $"{visualElementName}Texture",
        //        UseAlphaFromDiffuseTexture = true,
        //        BackFaceCulling = true,
        //        TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend
        //    }
        //);

        //MainScene.AddPlane(
        //    $"{visualElementName}Plane",

        //    new GrBabylonJsPlane.PlaneOptions
        //    {
        //        Width = $"{visualElementScalingFactor.GetBabylonJsCode()} * {visualElementName}ElementRect.width",
        //        Height = $"{visualElementScalingFactor.GetBabylonJsCode()} * {visualElementName}ElementRect.height"
        //    },

        //    new GrBabylonJsMesh.MeshProperties
        //    {
        //        BillboardMode = GrBabylonJsBillboardMode.All,
        //        Material = $"{visualElementName}Material",
        //        Position = visualElementPosition.GetBabylonJsCode(),
        //        Visibility = visualElementVisibility
        //        //AlphaIndex = int.MaxValue
        //    }
        //);


        var htmlCode = HtmlComposer.GetHtmlCode();

        var htmlFilePath = WorkingFolder.GetFilePath(
            @$"AnimationExample1",
            "html"
        );

        File.WriteAllText(htmlFilePath, htmlCode);
    }

    public static void Example2()
    {
        const int frameRate = 25;
        const double maxTime = 10;

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);

        InitializeSceneComposers(0);

        MainSceneComposer
            .AddDefaultGridZx(GridUnitCount)
            .AddDefaultAxes(AxesOrigin)
            .AddDefaultEnvironment(GridUnitCount)
            .AddDefaultPerspectiveCamera(
                CameraDistance,
                "2 * Math.PI / 20",
                "2 * Math.PI / 5"
            );

        MainSceneComposer.AddCircleCurve(
            GrVisualCircleCurve3D.CreateStatic(
                "c1",
                Color.DarkTurquoise.CreateSolidLineCurveStyle(),
                Float64Vector3D.Create(0, 0, 0),
                Float64Vector3D.Create(1, 1, 1),
                5
            )
        );

        MainSceneComposer.AddCircleCurve(
            GrVisualCircleCurve3D.CreateStatic(
                "c2",
                Color.LightBlue.CreateSolidLineCurveStyle(),
                Float64Vector3D.Create(0, 0, 0),
                Float64Vector3D.Create(0, 1, 0),
                3
            )
        );

        var material1 =
            Color.DarkTurquoise.ToBabylonJsSimpleMaterial("material1");

        var material2 =
            Color.LightBlue.ToBabylonJsSimpleMaterial("material2");

        var redMaterial =
            Color.Red.ToBabylonJsSimpleMaterial("redMaterial");

        var greenMaterial =
            Color.Green.ToBabylonJsSimpleMaterial("greenMaterial");

        var blueMaterial =
            Color.Blue.ToBabylonJsSimpleMaterial("blueMaterial");

        MainSceneComposer.AddMaterials(
            material1,
            material2,
            redMaterial,
            greenMaterial,
            blueMaterial
        );


        var curve1 = new ParametricCircle3D(
            Float64Vector3D.Create(0, 0, 0),
            Float64Vector3D.Create(1, 1, 1).ToUnitVector(),
            5,
            -3
        );

        var curve2 = new ParametricCircleZx3D(
            3,
            1
        );

        var curve3 = ComputedParametricCurve3D.Create(t => curve2.GetPoint(t) - curve1.GetPoint(t));

        var path1 = animationSpecs.CreateAnimatedVector3D(Float64ScalarRange.ZeroToOne, curve1);
        var path2 = animationSpecs.CreateAnimatedVector3D(Float64ScalarRange.ZeroToOne, curve2);
        var path3 = animationSpecs.CreateAnimatedVector3D(Float64ScalarRange.ZeroToOne, curve3);


        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                "point1",
                material1.CreateThickSurfaceStyle(0.06),
                path1
            )
        );

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                "point2",
                material2.CreateThickSurfaceStyle(0.06),
                path2
            )
        );

        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                "line1",
                redMaterial.CreateTubeCurveStyle(0.03),
                path1,
                path2
            )
        );

        var htmlCode = HtmlComposer.GetHtmlCode();

        var htmlFilePath = WorkingFolder.GetFilePath(
            @$"AnimationExample2",
            "html"
        );

        File.WriteAllText(htmlFilePath, htmlCode);
    }

    public static void Example3()
    {
        const int frameRate = 10;
        const double magnitude = 4d;
        const double frequencyHz = 0.1d;
        const double frequency = 2d * Math.PI * frequencyHz;
        const double thickness = 0.05;

        var timeRange = Float64ScalarRange.Create(1d / frequencyHz);
        var frameCount = (int)(1 + frameRate * timeRange.Length / 2);
        var animationSpecs = GrVisualAnimationSpecs.Create(frameRate, timeRange);

        var xPhasorFunction = DfCosPhasor.Create(
            magnitude,
            frequency,
            0d.DegreesToAngle()
        );

        var yPhasorFunction = DfCosPhasor.Create(
            magnitude,
            frequency,
            120d.DegreesToAngle()
        );

        var zPhasorFunction = DfCosPhasor.Create(
            magnitude,
            frequency,
            240d.DegreesToAngle()
        );


        var xPhasorTangentFunction =
            xPhasorFunction.GetDerivative1();

        var yPhasorTangentFunction =
            yPhasorFunction.GetDerivative1();

        var zPhasorTangentFunction =
            zPhasorFunction.GetDerivative1();


        var xPhasorCurve = ComputedParametricCurve3D.Create(t => xPhasorFunction.GetValue(t) * Float64Vector3D.E1,
            t => xPhasorTangentFunction.GetValue(t) * Float64Vector3D.E1);

        var yPhasorCurve = ComputedParametricCurve3D.Create(t => yPhasorFunction.GetValue(t) * Float64Vector3D.E2,
            t => yPhasorTangentFunction.GetValue(t) * Float64Vector3D.E2);

        var zPhasorCurve = ComputedParametricCurve3D.Create(t => zPhasorFunction.GetValue(t) * Float64Vector3D.E3,
            t => zPhasorTangentFunction.GetValue(t) * Float64Vector3D.E3);

        var uPhasorCurve = ComputedParametricCurve3D.Create(t => Float64Vector3D.Create(xPhasorFunction.GetValue(t),
                yPhasorFunction.GetValue(t),
                zPhasorFunction.GetValue(t)),
            t => Float64Vector3D.Create(xPhasorTangentFunction.GetValue(t),
                yPhasorTangentFunction.GetValue(t),
                zPhasorTangentFunction.GetValue(t)));


        var originCurve = ConstantParametricCurve3D.Create(
            Float64Vector3D.Zero,
            Float64Vector3D.UnitSymmetric
        );

        var uDiscRadiusCurve = ConstantParametricScalar.Create(
            magnitude * Math.Sqrt(3d / 2d) / 4,
            1d
        );

        var uDiscRadiusPath = 
            animationSpecs.CreateAnimatedScalar(uDiscRadiusCurve);

        var originPath = 
            animationSpecs.CreateAnimatedVector3D(originCurve);

        var xPhasorPath = 
            animationSpecs.CreateAnimatedVector3D(xPhasorCurve);

        var yPhasorPath = 
            animationSpecs.CreateAnimatedVector3D(yPhasorCurve);

        var zPhasorPath = 
            animationSpecs.CreateAnimatedVector3D(zPhasorCurve);

        var uPhasorPath = 
            animationSpecs.CreateAnimatedVector3D(uPhasorCurve);

        InitializeSceneComposers(0);

        InitializeImageCache();

        MainSceneComposer
            .AddDefaultGridZx(GridUnitCount)
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


        var xVector = GrVisualVector3D.CreateAnimated(
            "xVector",
            redMaterial.CreateTubeCurveStyle(thickness),
            xPhasorPath
        );

        var yVector = GrVisualVector3D.CreateAnimated(
            "yVector",
            greenMaterial.CreateTubeCurveStyle(thickness),
            yPhasorPath
        );

        var zVector = GrVisualVector3D.CreateAnimated(
            "zVector",
            blueMaterial.CreateTubeCurveStyle(thickness),
            zPhasorPath
        );

        var uVector = GrVisualVector3D.CreateAnimated(
            "uVector",
            orangeMaterial.CreateTubeCurveStyle(thickness),
            uPhasorPath
        );

        var kVector = GrVisualVector3D.CreateStatic(
            "kVector",
            orangeMaterial.CreateTubeCurveStyle(thickness),
            Float64Vector3D.Zero,
            magnitude * Math.Sqrt(3d / 2d) * Float64Vector3D.UnitSymmetric
        );

        MainSceneComposer.AddElements(
            xVector,
            yVector,
            zVector,
            uVector,
            kVector
        );

        MainSceneComposer.AddLaTeXText(
            "v1VectorText",
            ImageCache,
            xVector.AnimatedDirection.AddLength(0.25),
            HtmlComposer.LaTeXScalingFactor
        );
        
        MainSceneComposer.AddLaTeXText(
            "v2VectorText",
            ImageCache,
            yVector.AnimatedDirection.AddLength(0.25),
            HtmlComposer.LaTeXScalingFactor
        );
        
        MainSceneComposer.AddLaTeXText(
            "v3VectorText",
            ImageCache,
            zVector.AnimatedDirection.AddLength(0.25),
            HtmlComposer.LaTeXScalingFactor
        );
        
        MainSceneComposer.AddLaTeXText(
            "vVectorText",
            ImageCache,
            uVector.AnimatedDirection.AddLength(0.25),
            HtmlComposer.LaTeXScalingFactor
        );
        
        MainSceneComposer.AddLaTeXText(
            "kVectorText",
            ImageCache,
            kVector.Direction.AddLength(0.25),
            HtmlComposer.LaTeXScalingFactor
        );

        var dashSpecs = new GrVisualDashedLineSpecs(3, 2, 16);

        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.Create(
                "uxLine",
                Color.Red.CreateDashedLineCurveStyle(dashSpecs),
                uPhasorPath.GetPoint(0),
                xPhasorPath.GetPoint(0),
                animationSpecs
            ).SetAnimatedPositions(uPhasorPath, xPhasorPath)
        );

        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.Create(
                "uyLine",
                Color.Green.CreateDashedLineCurveStyle(dashSpecs),
                uPhasorPath.GetPoint(0),
                yPhasorPath.GetPoint(0),
                animationSpecs
            ).SetAnimatedPositions(uPhasorPath, yPhasorPath)
        );

        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.Create(
                "uzLine",
                Color.Blue.CreateDashedLineCurveStyle(dashSpecs),
                uPhasorPath.GetPoint(0),
                zPhasorPath.GetPoint(0),
                animationSpecs
            ).SetAnimatedPositions(uPhasorPath, zPhasorPath)
        );


        var uxTriangle = GrVisualTriangleSurface3D.CreateAnimated(
            "uxTriangle",
            redMaterial.CreateThinSurfaceStyle(),
            originPath,
            xPhasorPath,
            uPhasorPath
        );

        uxTriangle.Visibility = 0.5d;
        
        var uyTriangle = GrVisualTriangleSurface3D.CreateAnimated(
            "uyTriangle",
            greenMaterial.CreateThinSurfaceStyle(),
            originPath,
            yPhasorPath,
            uPhasorPath
        );

        uyTriangle.Visibility = 0.5d;

        var uzTriangle = GrVisualTriangleSurface3D.CreateAnimated(
            "uzTriangle",
            blueMaterial.CreateThinSurfaceStyle(),
            originPath,
            zPhasorPath,
            uPhasorPath
        );

        uzTriangle.Visibility = 0.5d;

        MainSceneComposer.AddElements(
            uxTriangle,
            uyTriangle,
            uzTriangle
        );
        

        MainSceneComposer.AddCircleSurface(
            GrVisualCircleSurface3D.Create(
                "uDisc",
                orangeMaterial.CreateThinSurfaceStyle(),
                originPath.GetPoint(0),
                uPhasorPath.GetPoint(0),
                uDiscRadiusPath.GetValue(0),
                false,
                animationSpecs
            ).SetAnimatedCenterNormalRadius(
                originPath,
                uPhasorPath,
                uDiscRadiusPath
            )
        );

        MainSceneComposer.AddCircleCurve(
            GrVisualCircleCurve3D.CreateStatic(
                "uRing",
                orangeMaterial.CreateTubeCurveStyle(thickness),
                Float64Vector3D.Zero,
                Float64Vector3D.UnitSymmetric,
                magnitude * Math.Sqrt(3d / 2d)
            )
        );

        //var linePathPoints = new ParametricCircle3D(
        //    Float64Tuple3D.Zero,
        //    Float64Tuple3D.UnitSymmetric,
        //    magnitude * Math.Sqrt(3d / 2d),
        //    1
        //).GetUniformParameterSampler(
        //    Float64Range1D.ZeroToOne, 
        //    360, 
        //    false
        //).GetPoints().ToImmutableArray();

        //var linePointsPath =
        //    GrVisualAnimatedVectorPath3D.Create(
        //        animationSpecs.TimeRange,
        //        linePathPoints.Select(
        //            p =>
        //                GrVisualAnimatedVector3D.Create(
        //                    new ArcLengthLineSegment3D(p, Float64Tuple3D.Zero),
        //                    Float64Range1D.ZeroToOne,
        //                    timeRange
        //                )
        //        ).ToArray()
        //    );

        //var linePath = GrVisualPointPathCurve3D.Create(
        //    "linePath", 
        //    orangeMaterial.CreateTubeCurveStyle(thickness),
        //    linePointsPath.GetPointsPath(0), 
        //    animationSpecs
        //).SetAnimatedPositionPath(linePointsPath);

        //MainSceneComposer.AddLinePath(linePath);


        var htmlCode = HtmlComposer.GetHtmlCode();

        var htmlFilePath = WorkingFolder.GetFilePath(
            @$"AnimationExample3",
            "html"
        );

        File.WriteAllText(htmlFilePath, htmlCode);
    }

    public static void Example4()
    {
        const int frameRate = 10;
        const double maxTime = 15;
        const double vectorLength = 3d;

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);

        InitializeSceneComposers(0);

        MainSceneComposer
            .AddDefaultGridZx(GridUnitCount)
            .AddDefaultAxes(AxesOrigin)
            .AddDefaultEnvironment(GridUnitCount)
            .AddDefaultPerspectiveCamera(
                8,
                "2 * Math.PI / 20",
                "2 * Math.PI / 5"
            );

        
        //var planarRotation1 = LinFloat64PlanarRotation3D.CreateFromSpanningVectors(
        //    new Float64Tuple3D(1, 1, 0).ToUnitVector(),
        //    new Float64Tuple3D(0, 0, 1).ToUnitVector(),
        //    45d.DegreesToAngle()
        //);

        //var planarRotation2 = LinFloat64PlanarRotation3D.CreateFromSpanningVectors(
        //    new Float64Tuple3D(1, 0, 1).ToUnitVector(),
        //    new Float64Tuple3D(0, 1, 0).ToUnitVector(),
        //    120d.DegreesToAngle()
        //);

        var planarRotation1 = LinFloat64PlanarRotation3D.CreateFromRotatedVector(
            Float64Vector3D.Create(1, 1, 0).ToUnitVector(),
            Float64Vector3D.Create(0, 0, 1).ToUnitVector(),
            true
        );

        var planarRotation2 = LinFloat64PlanarRotation3D.CreateFromRotatedVector(
            Float64Vector3D.Create(1, 0, 1).ToUnitVector(),
            Float64Vector3D.Create(1, 2, 3).ToUnitVector(),
            false
        );

        //var planarRotationList = 
        //    planarRotation1.InterpolateTo(
        //        planarRotation2,
        //        10,
        //        false
        //    ).ToImmutableArray();


        var tValueCurve = ComputedParametricScalar.Create(t =>
            t.CosWave(0d, 1d, 1)
        );

        var vector1Curve = 
            animationSpecs.CreateAnimatedVector3D(
                Float64ScalarRange.ZeroToOne,
                t => 
                    vectorLength * planarRotation1
                        .InterpolateTo(planarRotation2, tValueCurve.GetValue(t))
                        .BasisVector1
            );
        
        var vector2Curve = animationSpecs.CreateAnimatedVector3D(
            Float64ScalarRange.ZeroToOne,
            t => 
                vectorLength * planarRotation1
                    .InterpolateTo(planarRotation2, tValueCurve.GetValue(t))
                    .BasisVector2
        );
        
        var vector3Curve = animationSpecs.CreateAnimatedVector3D(
            Float64ScalarRange.ZeroToOne,
            t => 
                vectorLength * planarRotation1
                    .InterpolateTo(planarRotation2, tValueCurve.GetValue(t))
                    .MapBasisVector1()
        );
        
        var angleCurve = 
            animationSpecs.CreateAnimatedScalar(
                Float64ScalarRange.ZeroToOne,
                t => 
                planarRotation1
                    .InterpolateTo(planarRotation2, tValueCurve.GetValue(t))
                    .RotationAngle.Radians
            );
        
        var radiusCurve = 
            animationSpecs.CreateAnimatedScalar(
                Float64ScalarRange.ZeroToOne, 
                vectorLength
            );


        var vector1Style =
            MainSceneComposer
                .AddOrGetColorMaterial(Color.Khaki)
                .CreateTubeCurveStyle(0.03);
        
        var vector2Style =
            MainSceneComposer
                .AddOrGetColorMaterial(Color.DarkKhaki)
                .CreateTubeCurveStyle(0.03);
        
        var vector3Style =
            MainSceneComposer
                .AddOrGetColorMaterial(Color.BlueViolet)
                .CreateTubeCurveStyle(0.03);


        MainSceneComposer.AddVectors(
            GrVisualVector3D.CreateAnimated(
                "basisVector1",
                vector1Style,
                vector1Curve
            ),

            //GrVisualVector3D.CreateAnimated(
            //    "basisVector2",
            //    vector2Style,
            //    vector2Curve,
            //    animationSpecs
            //),

            GrVisualVector3D.CreateAnimated(
                "basisVector3",
                vector3Style,
                vector3Curve
            )
        );

        MainSceneComposer.AddCircleArc(
            GrVisualCircleArcCurve3D.CreateAnimated(
                "rotationArc",
                vector1Style,
                vector1Curve,
                vector2Curve,
                angleCurve,
                radiusCurve
            )
        );


        MainSceneComposer.AddLinePath(
            GrVisualPointPathCurve3D.CreateStatic(
                "basisVector1Path",
                vector1Style,
                vector1Curve
                    .FrameTimeRange
                    .GetLinearSamples(150, false)
                    .Select(t => vector1Curve.GetPoint(t))
                    .ToImmutableArray()
            )
        );
        
        //MainSceneComposer.AddLinePath(
        //    GrVisualPointPathCurve3D.CreateStatic(
        //        "basisVector2Path",
        //        vector2Style,
        //        vector2Curve
        //            .TimeRange
        //            .GetLinearSamples(150, false)
        //            .Select(vector2Curve.GetPoint).ToImmutableArray()
        //    )
        //);
        
        MainSceneComposer.AddLinePath(
            GrVisualPointPathCurve3D.CreateStatic(
                "basisVector3Path",
                vector3Style,
                vector3Curve
                    .FrameTimeRange
                    .GetLinearSamples(150, false)
                    .Select(t => vector3Curve.GetPoint(t))
                    .ToImmutableArray()
            )
        );


        MainSceneComposer.AddVectors(
            GrVisualVector3D.CreateStatic(
                "rotation1Vector1",
                vector1Style,
                vectorLength * planarRotation1.BasisVector1
            ),
            
            //GrVisualVector3D.CreateStatic(
            //    "rotation1Vector2",
            //    vector2Style,
            //    vectorLength * planarRotation1.BasisVector2
            //),
            
            GrVisualVector3D.CreateStatic(
                "rotation1Vector3",
                vector3Style,
                vectorLength * planarRotation1.MapBasisVector1()
            )
        );
        
        MainSceneComposer.AddCircleArc(
            GrVisualCircleArcCurve3D.CreateStatic(
                "rotation1Arc",
                vector1Style,
                Float64Vector3D.Zero,
                planarRotation1.BasisVector1,
                planarRotation1.BasisVector2,
                vectorLength,
                planarRotation1.RotationAngle
            )
        );

        MainSceneComposer.AddVectors(
            GrVisualVector3D.CreateStatic(
                $"rotation2Vector1",
                vector1Style,
                vectorLength * planarRotation2.BasisVector1
            ),
            
            //GrVisualVector3D.CreateStatic(
            //    $"rotation2Vector2",
            //    vector2Style,
            //    vectorLength * planarRotation2.BasisVector2
            //),
            
            GrVisualVector3D.CreateStatic(
                $"rotation2Vector3",
                vector3Style,
                vectorLength * planarRotation2.MapBasisVector1()
            )
        );

        MainSceneComposer.AddCircleArc(
            GrVisualCircleArcCurve3D.CreateStatic(
                "rotation2Arc",
                vector1Style,
                Float64Vector3D.Zero,
                planarRotation2.BasisVector1,
                planarRotation2.BasisVector2,
                vectorLength,
                planarRotation2.RotationAngle
            )
        );


        var sphereStyle = 
            MainSceneComposer.AddOrGetColorMaterial(
                Color.Blue.WithAlpha(0.25f)
            ).CreateThinSurfaceStyle();

        MainSceneComposer.AddSphereSurface(
            GrVisualSphereSurface3D.CreateStatic(
                "rotationSphere",
                sphereStyle,
                vectorLength
            )
        );


        var htmlCode = HtmlComposer.GetHtmlCode();

        var htmlFilePath = WorkingFolder.GetFilePath(
            @$"AnimationExample4",
            "html"
        );

        File.WriteAllText(htmlFilePath, htmlCode);
    }
}