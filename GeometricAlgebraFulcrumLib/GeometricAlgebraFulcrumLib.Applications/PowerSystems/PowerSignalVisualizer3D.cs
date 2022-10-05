using System.Collections.Immutable;
using DataStructuresLib.Basic;
using DataStructuresLib.Files;
using GeometricAlgebraFulcrumLib.Algebra.SignalProcessing;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GraphicsComposerLib.Rendering.BabylonJs;
using GraphicsComposerLib.Rendering.BabylonJs.Cameras;
using GraphicsComposerLib.Rendering.BabylonJs.Constants;
using GraphicsComposerLib.Rendering.BabylonJs.GUI;
using GraphicsComposerLib.Rendering.BabylonJs.Materials;
using GraphicsComposerLib.Rendering.BabylonJs.Meshes;
using GraphicsComposerLib.Rendering.BabylonJs.Textures;
using GraphicsComposerLib.Rendering.Colors;
using GraphicsComposerLib.Rendering.Images;
using GraphicsComposerLib.Rendering.LaTeX.ImageComposers;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Basic;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Curves;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Grids;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Groups;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Images;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Constants;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.GeometricAlgebra.Euclidean3D;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems;

public class PowerSignalVisualizer3D :
    GrBabylonJsSnapshotComposer3D
{
    private static GrBabylonJsHtmlComposer3D InitializeSceneComposers(int index, int canvasWidth, int canvasHeight)
    {
        var mainSceneComposer = new GrBabylonJsSceneComposer3D(
            "mainScene",
            new GrBabylonJsSnapshotSpecs
            {
                Enabled = true,
                Width = canvasWidth,
                Height = canvasHeight,
                Precision = 1,
                UsePrecision = true,
                Delay = index == 0 ? 1800 : 800,
                FileName = $"Frame-{index:D6}.png"
            }
        )
        {
            BackgroundColor = Color.AliceBlue,
            ShowDebugLayer = false,
        };

        //mainSceneComposer.SceneObject.SceneProperties.UseOrderIndependentTransparency = true;

        var omegaSceneComposer = new GrBabylonJsSceneComposer3D("omegaScene")
        {
            BackgroundColor = Color.AliceBlue,
            ShowDebugLayer = false
        };

        omegaSceneComposer.SceneObject.Properties.AutoClear = false;

        var htmlComposer = new GrBabylonJsHtmlComposer3D(mainSceneComposer)
        {
            CanvasWidth = canvasWidth,
            CanvasHeight = canvasHeight,
            CanvasFullScreen = false
        };

        htmlComposer.AddSceneComposer(omegaSceneComposer, false);

        //htmlComposer.SetActiveSceneComposer(constName);

        return htmlComposer;
    }
    

    public ComputedPowerSignal3D PowerSignal { get; }
 
    public int CanvasWidth { get; set; } = 1280;

    public int CanvasHeight { get; set; } = 720;

    public int GridUnitCount { get; set; } = 24;

    public double CameraDistance { get; set; } = 13;

    public int CameraRotationCount { get; set; } = 1;

    public int TrailSampleCount { get; set; }

    public int PlotSampleCount { get; set; }

    public int FrameSeparationCount { get; set; } = 20;

    public bool ShowLeftPanel { get; set; } = true;

    public bool ShowRightPanel { get; set; } = true;

    public bool ShowCopyright { get; set; } = true;

    public Tuple3D OmegaFrameOrigin { get; set; } = new Tuple3D(-6, 2, 1);

    public ScalarSignalFloat64 CameraAlphaValues { get; private set; }

    public ScalarSignalFloat64 CameraBetaValues { get; private set; }
    
    public int SignalTextImageMaxWidth { get; private set; }

    public int SignalTextImageMaxHeight { get; private set; }

    public GrBabylonJsHtmlComposer3D HtmlComposer { get; private set; }

    public GrBabylonJsSceneComposer3D MainSceneComposer 
        => HtmlComposer.GetSceneComposer("mainScene");

    public GrBabylonJsSceneComposer3D OmegaSceneComposer 
        => HtmlComposer.GetSceneComposer("omegaScene");


    public PowerSignalVisualizer3D(ComputedPowerSignal3D powerSignal)
    {
        PowerSignal = powerSignal;
        CodeGenerationFunc = GenerateSnapshotCode;
        FrameCount = powerSignal.SampleCount;
    }

    
    private void InitializeImageCache()
    {
        var workingPath = Path.Combine(WorkingPath, "images");

        Console.Write("Generating images cache .. ");

        ImageCache.MarginSize = 0;
        ImageCache.BackgroundColor = Color.FromRgba(255, 255, 255, 0);

        ImageCache.AddPngFromFile(
            "copyright", 
            workingPath.GetFilePath("Copyright.png")
        );
            
        for (var i = 0; i < PowerSignal.TimeValues.Count; i++)
        {
            ImageCache.AddPng(
                $"SignalPlot-{i:D6}", 
                PowerSignal.GetSignalPlotImage(i, PlotSampleCount)
            );

            ImageCache.AddPng(
                $"CurvaturePlot-{i:D6}", 
                PowerSignal.GetCurvaturesPlotImage(i, PlotSampleCount)
            );

            ImageCache.AddPng(
                $"FrequencyHzPlot-{i:D6}",
                PowerSignal.GetFrequencyHzPlotImage(i, PlotSampleCount)
            );
        }

        Console.WriteLine("done.");
        Console.WriteLine();

        Console.Write("Generating LaTeX images .. ");

        ImageCache.MarginSize = 0;
        ImageCache.BackgroundColor = Color.FromRgba(255, 255, 255, 0);
            
        ImageCache.AddLaTeXEquation(
            "basis1VectorText",
            @"\boldsymbol{\sigma}_{1}"
        );
            
        ImageCache.AddLaTeXEquation(
            "basis2VectorText",
            @"\boldsymbol{\sigma}_{2}"
        );
            
        ImageCache.AddLaTeXEquation(
            "basis3VectorText",
            @"\boldsymbol{\sigma}_{3}"
        );

        ImageCache.AddLaTeXEquation(
            "v1VectorText",
            @"\boldsymbol{v}_{1}"
        );

        ImageCache.AddLaTeXEquation(
            "v2VectorText",
            @"\boldsymbol{v}_{2}"
        );

        ImageCache.AddLaTeXEquation(
            "v3VectorText",
            @"\boldsymbol{v}_{3}"
        );

        ImageCache.AddLaTeXEquation(
            "vVectorText",
            @"\boldsymbol{v}"
        );
            
        ImageCache.AddLaTeXEquation(
            "e1VectorText",
            @"\boldsymbol{e}_{1}"
        );

        ImageCache.AddLaTeXEquation(
            "e2VectorText",
            @"\boldsymbol{e}_{2}"
        );

        ImageCache.AddLaTeXEquation(
            "e3VectorText",
            @"\boldsymbol{e}_{3}"
        );

        ImageCache.AddLaTeXEquation(
            "kVectorText",
            @"\hat{\boldsymbol{k}}"
        );

        ImageCache.AddLaTeXEquation(
            "e1DsVectorText",
            @"\dot{\boldsymbol{e}}_{1}"
        );

        ImageCache.AddLaTeXEquation(
            "e2DsVectorText",
            @"\dot{\boldsymbol{e}}_{2}"
        );

        ImageCache.AddLaTeXEquation(
            "e3DsVectorText",
            @"\dot{\boldsymbol{e}}_{3}"
        );

        ImageCache.AddLaTeXEquation(
            "omega1BivectorText",
            @"\boldsymbol{\Omega}_{1}"
        );

        ImageCache.AddLaTeXEquation(
            "omega2BivectorText",
            @"\boldsymbol{\Omega}_{2}"
        );

        ImageCache.AddLaTeXEquation(
            "omega3BivectorText",
            @"\boldsymbol{\Omega}_{3}"
        );


        ImageCache.MarginSize = 20;
        //ImageCache.BackgroundColor = Color.FromRgba(32, 32, 255, 16);
        
        ImageCache.AddLaTeXCode("symbolicSignalText", PowerSignal.LaTeXCode);

        for (var i = 0; i < PowerSignal.TimeValues.Count; i++)
        {
            var t = PowerSignal.TimeValues[i];
            var (x, y, z) = PowerSignal.GetPhaseVectors(t);
            var v = x + y + z;

            var frame = PowerSignal.FrameList[i]; //GetSignalFrame(t);

            var e1 = frame.Direction1.ToUnitVector();
            var e2 = frame.Direction2.ToUnitVector();
            var e3 = frame.Direction3.ToUnitVector();
            
            var sDt = PowerSignal.GetDerivative1Norm(t);

            var (kappa1, kappa2) = PowerSignal.CurvatureList[i];

            var omega = PowerSignal.DarbouxBivectorList[i];
            var omegaNorm = omega.Norm();

            var omegaMean = PowerSignal.GetDarbouxBivectorMean(i);
            var omegaMeanNorm = omegaMean.Norm();

            var frequencyHz = omegaNorm / (2d * Math.PI);
            var frequencyHzMean = omegaMeanNorm / (2 * Math.PI);

            var e1Ds = kappa1 * e2;
            var e2Ds = kappa2 * e3 - kappa1 * e1;
            var e3Ds = -kappa2 * e2;

            var kVector = omega.UnDual().ToUnitVector();
            var kVectorMean = omegaMean.UnDual().ToUnitVector();

            ImageCache.AddLaTeXAlignedEquations(
                $"signalText-{i:D6}",
                new Pair<string>[]
                {
                    new (@"t", @$"{t:F4}"),
                    new (@"\boldsymbol{v}\left( t \right)", @$"\left( {v.X:F4}, {v.Y:F4}, {v.Z:F4} \right)"),
                    new (@"\left\Vert \boldsymbol{v}^{\prime}\left(t\right)\right\Vert", @$"s^{{\prime}} \left( t \right) = {sDt:F4}"),
                    new (@"\boldsymbol{e}_{1}\left( t \right)", @$"\left( {e1.X:F4}, {e1.Y:F4}, {e1.Z:F4} \right)"),
                    new (@"\boldsymbol{e}_{2}\left( t \right)", @$"\left( {e2.X:F4}, {e2.Y:F4}, {e2.Z:F4} \right)"),
                    new (@"\boldsymbol{e}_{3}\left( t \right)", @$"\left( {e3.X:F4}, {e3.Y:F4}, {e3.Z:F4} \right)"),
                    new (@"\left\Vert \boldsymbol{\Omega}_{1}\left(t\right)\right\Vert", @$"\left| s^{{\prime}} \kappa_{{1}} \right| = {kappa1:F4}"),
                    new (@"\left\Vert \boldsymbol{\Omega}_{2}\left(t\right)\right\Vert", @$"\left| s^{{\prime}} \right| \sqrt{{\kappa_{{1}}^{{2}}+\kappa_{{2}}^{{2}}}} = {omegaNorm:F4}"),
                    new (@"\left\Vert \boldsymbol{\Omega}_{3}\left(t\right)\right\Vert", @$"\left| s^{{\prime}} \kappa_{{2}} \right| = {kappa2:F4}"),
                    new (@"f \left( t \right)", $@"{frequencyHz:F4} \textrm{{ Hz}}"),
                    new (@"\hat{\boldsymbol{k}}\left( t \right)", @$"\left( {kVector.X:F4}, {kVector.Y:F4}, {kVector.Z:F4} \right)"),
                    new (@"\overline{f}", $@"{frequencyHzMean:F4} \textrm{{ Hz}}"),
                    new (@"\overline{\boldsymbol{k}}", @$"\left( {kVectorMean.X:F4}, {kVectorMean.Y:F4}, {kVectorMean.Z:F4} \right)")
                }
            );
        }

        var latexImageComposer = new GrLaTeXImageComposer
        {
            LaTeXBinFolder = @"D:\texlive\2021\bin\win32\",
            Resolution = 150
        };

        ImageCache.GeneratePngBase64Strings(latexImageComposer);

        var maxWidth = 0;
        var maxHeight = 0;
        for (var i = 0; i < PowerSignal.TimeValues.Count; i++)
        {
            var imageData = ImageCache[$"signalText-{i:D6}"];

            if (maxWidth < imageData.Width) maxWidth = imageData.Width;
            if (maxHeight < imageData.Height) maxHeight = imageData.Height;
        }

        SignalTextImageMaxWidth = maxWidth;
        SignalTextImageMaxHeight = maxHeight;

        Console.WriteLine("done.");
        Console.WriteLine();
    }

    private void AddCamera(double alpha, double beta)
    {
        // Add main scene camera
        MainSceneComposer.SceneObject.AddArcRotateCamera(
            "camera1",
            alpha, //"2 * Math.PI / 20",
            beta, //"2 * Math.PI / 5",
            CameraDistance,
            "BABYLON.Vector3.Zero()",
            new GrBabylonJsArcRotateCamera.ArcRotateCameraProperties
            {
                Mode = GrBabylonJsCameraMode.PerspectiveCamera,
                //OrthoLeft = -8,
                //OrthoRight = 8,
                //OrthoBottom = -8,
                //OrthoTop = 8
            }
        );

        // Add omega scene camera
        var omegaSceneCamera = OmegaSceneComposer.SceneObject.AddArcRotateCamera(
            "camera2",
            "2 * Math.PI / 6",  //alpha,
            "2 * Math.PI / 8",  // beta,
            CameraDistance,
            "BABYLON.Vector3.Zero()",
            new GrBabylonJsArcRotateCamera.ArcRotateCameraProperties
            {
                Mode = GrBabylonJsCameraMode.PerspectiveCamera,
                //OrthoLeft = -8,
                //OrthoRight = 8,
                //OrthoBottom = -8,
                //OrthoTop = 8
            }
        );

        //omegaSceneCamera.AttachControl = false;
    }

    private void AddEnvironment()
    {
        //var scene = MainSceneComposer.SceneObject;
        //scene.SceneProperties.AmbientColor = Color.AliceBlue;

        // Add scene environment
        MainSceneComposer.SceneObject.AddEnvironmentHelper(
            "environmentHelper",

            new GrBabylonJsEnvironmentHelper.EnvironmentHelperOptions
            {
                GroundYBias = 0.01,
                SkyBoxColor = Color.LightSkyBlue,
                GroundColor = Color.White,
                CreateGround = true,
                GroundSize = 8,
                SkyBoxSize = GridUnitCount + 10
            }
        );
    }

    private void AddGrid()
    {
        var scene = MainSceneComposer.SceneObject;

        // Add ground coordinates grid
        MainSceneComposer.GridMaterialKind =
            GrBabylonJsGridMaterialKind.TexturedMaterial;

        MainSceneComposer.AddXzSquareGrid(
            new GrVisualXzSquareGrid3D("grid")
            {
                UnitCountX = GridUnitCount,
                UnitCountZ = GridUnitCount,
                UnitSize = 1,
                Origin = new Tuple3D(-0.5d * GridUnitCount, 0, -0.5d * GridUnitCount),
                Opacity = 0.25,
                BaseSquareColor = Color.LightYellow,
                BaseLineColor = Color.BurlyWood,
                MidLineColor = Color.SandyBrown,
                BorderLineColor = Color.SaddleBrown,
                BaseSquareCount = 4,
                BaseSquareSize = 64,
                BaseLineWidth = 2,
                MidLineWidth = 4,
                BorderLineWidth = 3
            }
        );

        // Add reference unit axis frame
        var axisFrameOriginMaterial = scene.AddSimpleMaterial("axisFrameOriginMaterial", Color.DarkGray);
        var axisFrameXMaterial = scene.AddSimpleMaterial("axisFrameXMaterial", Color.DarkRed);
        var axisFrameYMaterial = scene.AddSimpleMaterial("axisFrameYMaterial", Color.DarkGreen);
        var axisFrameZMaterial = scene.AddSimpleMaterial("axisFrameZMaterial", Color.DarkBlue);

        var frameOrigin = new Tuple3D(-4, 0, -4);
        MainSceneComposer.AddElement(
            new GrVisualFrame3D("axisFrame")
            {
                Origin = frameOrigin,

                Direction1 = Tuple3D.E1,
                Direction2 = Tuple3D.E2,
                Direction3 = Tuple3D.E3,

                Style = new GrVisualFrameStyle3D
                {
                    OriginThickness = 0.075,
                    DirectionThickness = 0.035,
                    OriginMaterial = axisFrameOriginMaterial,
                    DirectionMaterial1 = axisFrameXMaterial,
                    DirectionMaterial2 = axisFrameYMaterial,
                    DirectionMaterial3 = axisFrameZMaterial
                },
                
                Direction1TextImage = new GrVisualLaTeXText3D(ImageCache, "basis1VectorText")
                {
                    Origin = frameOrigin + Tuple3D.E1 + 0.25 * Tuple3D.E2,
                    ScalingFactor = LaTeXScalingFactor
                },
                
                Direction2TextImage = new GrVisualLaTeXText3D(ImageCache, "basis2VectorText")
                {
                    Origin = frameOrigin + 1.25 * Tuple3D.E2,
                    ScalingFactor = LaTeXScalingFactor
                },
                
                Direction3TextImage = new GrVisualLaTeXText3D(ImageCache, "basis3VectorText")
                {
                    Origin = frameOrigin + Tuple3D.E3 + 0.25 * Tuple3D.E2,
                    ScalingFactor = LaTeXScalingFactor
                }
            }
        );

        // Add ground coordinates grid
        OmegaSceneComposer.GridMaterialKind =
            GrBabylonJsGridMaterialKind.TexturedMaterial;

        OmegaSceneComposer.AddXzSquareGrid(
            new GrVisualXzSquareGrid3D("grid")
            {
                UnitCountX = 4,
                UnitCountZ = 4,
                UnitSize = 1,
                Origin = OmegaFrameOrigin + new Tuple3D(-0.5d * 4, 0, -0.5d * 4),
                Opacity = 0.2,
                BaseSquareColor = System.Drawing.Color.LightYellow.ToImageSharpColor(),
                BaseLineColor = System.Drawing.Color.BurlyWood.ToImageSharpColor(),
                MidLineColor = System.Drawing.Color.SandyBrown.ToImageSharpColor(),
                BorderLineColor = System.Drawing.Color.Black.ToImageSharpColor(),
                BaseSquareCount = 4,
                BaseSquareSize = 64,
                BaseLineWidth = 2,
                MidLineWidth = 4,
                BorderLineWidth = 3
            }
        );
    }

    private void AddGuiLayer(int index)
    {
        var scene = MainSceneComposer.SceneObject;

        // Add GUI layer
        var uiTexture = scene.AddGuiFullScreenUi("uiTexture");

        if (ShowLeftPanel)
        {
            var uiPanel1Width = HtmlComposer.CanvasWidth * 0.275;
            var uiPanel1 = uiTexture.AddGuiStackPanel(
                "uiPanel1",
                new GrBabylonJsGuiStackPanel.GuiStackPanelProperties
                {
                    IsVertical = true,
                    Spacing = 10,
                    //Color = Color.Blue,
                    BackgroundColor = Color.FromRgba(208, 206, 226, 24),
                    PaddingLeftInPixels = 10,
                    PaddingTopInPixels = 10,
                    PaddingBottomInPixels = 10,
                    PaddingRightInPixels = 10,
                    WidthInPixels = uiPanel1Width,
                    HorizontalAlignment = GrBabylonJsHorizontalAlignment.Left,
                    VerticalAlignment = GrBabylonJsVerticalAlignment.Top
                }
            );
            
            uiPanel1.AddGuiTextBlock(
                "uiTextTitle",
                $"'{Title}'",
                new GrBabylonJsGuiTextBlock.GuiTextBlockProperties
                {
                    WidthInPixels = uiPanel1Width - 20,
                    HeightInPixels = 50,
                    ResizeToFit = true,
                    FontSize = 16,
                    FontWeight = "'500'",
                    FontFamily = "'Georgia,Times,Times New Roman,serif'",
                    TextHorizontalAlignment = GrBabylonJsHorizontalAlignment.Left,
                    Color = Color.Black,
                    PaddingLeftInPixels = 10,
                    PaddingTopInPixels = 10,
                    PaddingBottomInPixels = 10,
                    PaddingRightInPixels = 10,
                    HorizontalAlignment = GrBabylonJsHorizontalAlignment.Left,
                    VerticalAlignment = GrBabylonJsVerticalAlignment.Center,
                }
            );
                
            uiPanel1.AddGuiLine(
                "uiLine",
                new GrBabylonJsGuiLine.GuiLineProperties
                {
                    X1 = 10,
                    X2 = uiPanel1Width - 10,
                    Y1 = 35,
                    Y2 = 35,
                    Color = Color.Black,
                    LineWidth = 1
                }
            );

            var latexPngData1 = ImageCache["symbolicSignalText"];
            uiPanel1.AddGuiImage(
                "latexGuiImage1",
                latexPngData1.GetBase64HtmlString(),
                new GrBabylonJsGuiImage.GuiImageProperties
                {
                    //Alpha = 0.5d,
                    WidthInPixels = uiPanel1Width - 20,
                    HeightInPixels = (uiPanel1Width - 20) * latexPngData1.HeightToWidthRatio,
                    PaddingLeftInPixels = 0,
                    PaddingTopInPixels = 0,
                    PaddingBottomInPixels = 0,
                    PaddingRightInPixels = 0,
                    HorizontalAlignment = GrBabylonJsHorizontalAlignment.Left,
                    VerticalAlignment = GrBabylonJsVerticalAlignment.Center,
                    Stretch = GrBabylonJsImageStretch.Uniform
                }
            );
        }

        if (ShowRightPanel)
        {
            var uiPanel2Width = HtmlComposer.CanvasWidth * 0.275;
            var uiPanel2 = uiTexture.AddGuiStackPanel(
                "uiPanel2",
                new GrBabylonJsGuiStackPanel.GuiStackPanelProperties
                {
                    IsVertical = true,
                    Spacing = 10,
                    //Color = Color.Blue,
                    BackgroundColor = Color.FromRgba(208, 206, 226, 24),
                    PaddingLeftInPixels = 10,
                    PaddingTopInPixels = 10,
                    PaddingBottomInPixels = 10,
                    PaddingRightInPixels = 10,
                    WidthInPixels = uiPanel2Width,
                    HorizontalAlignment = GrBabylonJsHorizontalAlignment.Right,
                    VerticalAlignment = GrBabylonJsVerticalAlignment.Top
                }
            );

            var signalPlotData = ImageCache[$"SignalPlot-{index:D6}"];
            uiPanel2.AddGuiImage(
                "signalPlotGuiImage",
                signalPlotData.GetBase64HtmlString(),
                new GrBabylonJsGuiImage.GuiImageProperties
                {
                    Stretch = GrBabylonJsImageStretch.Uniform,
                    //Alpha = 0.5d,
                    WidthInPixels = uiPanel2Width - 20,
                    HeightInPixels = (uiPanel2Width - 20) * signalPlotData.HeightToWidthRatio,
                    PaddingLeftInPixels = 0,
                    PaddingTopInPixels = 0,
                    PaddingBottomInPixels = 0,
                    PaddingRightInPixels = 0,
                    HorizontalAlignment = GrBabylonJsHorizontalAlignment.Center,
                    VerticalAlignment = GrBabylonJsVerticalAlignment.Center,
                }
            );
                
            var curvaturePlotData = ImageCache[$"CurvaturePlot-{index:D6}"];
            uiPanel2.AddGuiImage(
                "curvaturePlotGuiImage",
                curvaturePlotData.GetBase64HtmlString(),
                new GrBabylonJsGuiImage.GuiImageProperties
                {
                    Stretch = GrBabylonJsImageStretch.Uniform,
                    //Alpha = 0.5d,
                    WidthInPixels = uiPanel2Width - 20,
                    HeightInPixels = (uiPanel2Width - 20) * curvaturePlotData.HeightToWidthRatio,
                    PaddingLeftInPixels = 0,
                    PaddingTopInPixels = 0,
                    PaddingBottomInPixels = 0,
                    PaddingRightInPixels = 0,
                    HorizontalAlignment = GrBabylonJsHorizontalAlignment.Center,
                    VerticalAlignment = GrBabylonJsVerticalAlignment.Center,
                }
            );

            var frequencyHzPlotData = ImageCache[$"FrequencyHzPlot-{index:D6}"];
            uiPanel2.AddGuiImage(
                "frequencyHzPlotGuiImage",
                frequencyHzPlotData.GetBase64HtmlString(),
                new GrBabylonJsGuiImage.GuiImageProperties
                {
                    Stretch = GrBabylonJsImageStretch.Uniform,
                    //Alpha = 0.5d,
                    WidthInPixels = uiPanel2Width - 20,
                    HeightInPixels = (uiPanel2Width - 20) * frequencyHzPlotData.HeightToWidthRatio,
                    PaddingLeftInPixels = 0,
                    PaddingTopInPixels = 0,
                    PaddingBottomInPixels = 0,
                    PaddingRightInPixels = 0,
                    HorizontalAlignment = GrBabylonJsHorizontalAlignment.Center,
                    VerticalAlignment = GrBabylonJsVerticalAlignment.Center,
                }
            );

            var signalTextImageData = ImageCache[$"signalText-{index:D6}"];
            var signalTextImageWidth = uiPanel2Width * signalTextImageData.Width / SignalTextImageMaxWidth;

            uiPanel2.AddGuiImage(
                "latexGuiImage2",
                signalTextImageData.GetBase64HtmlString(),
                new GrBabylonJsGuiImage.GuiImageProperties
                {
                    //Alpha = 0.5d,
                    WidthInPixels = signalTextImageWidth,
                    HeightInPixels = signalTextImageWidth * signalTextImageData.HeightToWidthRatio,
                    PaddingLeftInPixels = 5,
                    PaddingTopInPixels = 0,
                    PaddingBottomInPixels = 0,
                    PaddingRightInPixels = 5,
                    HorizontalAlignment = GrBabylonJsHorizontalAlignment.Left,
                    VerticalAlignment = GrBabylonJsVerticalAlignment.Top
                }
            );
        }

        if (ShowCopyright)
        {
            var copyrightImage = ImageCache["copyright"];
            var copyrightImageWidth = 0.4d * HtmlComposer.CanvasWidth;
            var copyrightImageHeight = 0.4d * HtmlComposer.CanvasWidth * copyrightImage.HeightToWidthRatio;

            uiTexture.AddGuiImage(
                "copyrightImage",
                copyrightImage.GetBase64HtmlString(),
                new GrBabylonJsGuiImage.GuiImageProperties
                {
                    Stretch = GrBabylonJsImageStretch.Uniform,
                    //Alpha = 0.75d,
                    WidthInPixels = copyrightImageWidth,
                    HeightInPixels = copyrightImageHeight,
                    PaddingLeftInPixels = 10,
                    PaddingBottomInPixels = 10,
                    HorizontalAlignment = GrBabylonJsHorizontalAlignment.Left,
                    VerticalAlignment = GrBabylonJsVerticalAlignment.Bottom,
                }
            );
        }
    }

    private void AddPhaseVector1(Tuple3D x)
    {
        var scene = MainSceneComposer.SceneObject;

        var material = scene.AddSimpleMaterial("v1VectorMaterial", Color.Red);

        MainSceneComposer.AddVector(
            new GrVisualVector3D("v1Vector")
            {
                Origin = Tuple3D.Zero,
                Direction = x,

                Style = new GrVisualVectorStyle3D
                {
                    Material = material,
                    Thickness = 0.05
                },

                TextImage = new GrVisualLaTeXText3D(ImageCache, "v1VectorText")
                {
                    ScalingFactor = LaTeXScalingFactor,
                    Origin = x + x.ToUnitVector() * 0.25d + (Tuple3D.E2 + Tuple3D.E3) * 0.25d / 2d.Sqrt()
                }
            }
        );

        MainSceneComposer.AddLineSegment(
            new GrVisualLineSegment3D("v1Trail")
            {
                Position1 = new Tuple3D(PowerSignal.VectorBounds.MinX, 0, 0),
                Position2 = new Tuple3D(PowerSignal.VectorBounds.MaxX, 0, 0),

                Style = new GrVisualCurveTubeStyle3D
                {
                    Material = scene.AddSimpleMaterial("v1TrailMaterial", Color.Red.WithAlpha(0.25f)),
                    Thickness = 0.045
                }
            }
        );
    }

    private void AddPhaseVector2(Tuple3D y)
    {
        var scene = MainSceneComposer.SceneObject;

        var material = scene.AddSimpleMaterial("v2VectorMaterial", Color.Green);

        MainSceneComposer.AddVector(
            new GrVisualVector3D("v2Vector")
            {
                Origin = Tuple3D.Zero,
                Direction = y,

                Style = new GrVisualVectorStyle3D
                {
                    Material = material,
                    Thickness = 0.05
                },

                TextImage = new GrVisualLaTeXText3D(ImageCache, "v2VectorText")
                {
                    ScalingFactor = LaTeXScalingFactor,
                    Origin = y + y.ToUnitVector() * 0.25d + (Tuple3D.E1 + Tuple3D.E3) * 0.25d / 2d.Sqrt()
                }
            }
        );
            
        MainSceneComposer.AddLineSegment(
            new GrVisualLineSegment3D("v2TrailSegment")
            {
                Position1 = new Tuple3D(0, PowerSignal.VectorBounds.MinY, 0),
                Position2 = new Tuple3D(0, PowerSignal.VectorBounds.MaxY, 0),

                Style = new GrVisualCurveTubeStyle3D
                {
                    Material = scene.AddSimpleMaterial("v2TrailMaterial", Color.Green.WithAlpha(0.25f)),
                    Thickness = 0.045
                }
            }
        );
    }

    private void AddPhaseVector3(Tuple3D z)
    {
        var scene = MainSceneComposer.SceneObject;

        var material = scene.AddSimpleMaterial("v3VectorMaterial", Color.Blue);

        MainSceneComposer.AddVector(
            new GrVisualVector3D("v3Vector")
            {
                Origin = Tuple3D.Zero,
                Direction = z,

                Style = new GrVisualVectorStyle3D
                {
                    Material = material,
                    Thickness = 0.05
                },

                TextImage = new GrVisualLaTeXText3D(ImageCache, "v3VectorText")
                {
                    ScalingFactor = LaTeXScalingFactor,
                    Origin = z + z.ToUnitVector() * 0.25d + (Tuple3D.E1 + Tuple3D.E2) * 0.25d / 2d.Sqrt()
                }
            }
        );
            
        MainSceneComposer.AddLineSegment(
            new GrVisualLineSegment3D("v3TrailSegment")
            {
                Position1 = new Tuple3D(0, 0, PowerSignal.VectorBounds.MinZ),
                Position2 = new Tuple3D(0, 0, PowerSignal.VectorBounds.MaxZ),

                Style = new GrVisualCurveTubeStyle3D
                {
                    Material = scene.AddSimpleMaterial("v3TrailMaterial", Color.Blue.WithAlpha(0.25f)),
                    Thickness = 0.045
                }
            }
        );
    }

    private void AddSignalVector(Tuple3D x, Tuple3D y, Tuple3D z)
    {
        var v = x + y + z;

        var scene = MainSceneComposer.SceneObject;

        var material = 
            scene.AddStandardMaterial("vVectorMaterial", System.Drawing.Color.DarkOrange);

        //SceneComposer.AddLineSegment(
        //    new GrVisualLineSegment3D("vVector")
        //    {
        //        Position1 = Tuple3D.Zero,
        //        Position2 = v,
        //        Style = new GrVisualCurveTubeStyle3D
        //        {
        //            Material = material,
        //            Thickness = 0.035
        //        }
        //    }
        //);

        MainSceneComposer.AddVector(
            new GrVisualVector3D("vVector")
            {
                Origin = Tuple3D.Zero,
                Direction = v,

                Style = new GrVisualVectorStyle3D
                {
                    Material = material,
                    Thickness = 0.05
                },

                TextImage = new GrVisualLaTeXText3D(ImageCache, "vVectorText")
                {
                    ScalingFactor = LaTeXScalingFactor,
                    Origin = v + v.ToUnitVector() * 0.25d
                }
            }
        );
            
        var dashedStyle = new GrVisualCurveDashedLineStyle3D
        {
            Color = Color.Gray,
            DashOn = 1,
            DashOff = 1,
            DashPerLine = 20
        };

        MainSceneComposer.AddLineSegment(
            new GrVisualLineSegment3D("xySegment1")
            {
                Position1 = x + y,
                Position2 = x,
                Style = dashedStyle
            }
        );

        MainSceneComposer.AddLineSegment(
            new GrVisualLineSegment3D("xySegment2")
            {
                Position1 = x + y,
                Position2 = y,
                Style = dashedStyle
            }
        );

        MainSceneComposer.AddLineSegment(
            new GrVisualLineSegment3D("xySegment3")
            {
                Position1 = x + y,
                Position2 = v,
                Style = dashedStyle
            }
        );

        MainSceneComposer.AddLineSegment(
            new GrVisualLineSegment3D("yzSegment1")
            {
                Position1 = y + z,
                Position2 = y,
                Style = dashedStyle
            }
        );

        MainSceneComposer.AddLineSegment(
            new GrVisualLineSegment3D("yzSegment2")
            {
                Position1 = y + z,
                Position2 = z,
                Style = dashedStyle
            }
        );

        MainSceneComposer.AddLineSegment(
            new GrVisualLineSegment3D("yzSegment3")
            {
                Position1 = y + z,
                Position2 = v,
                Style = dashedStyle
            }
        );

        MainSceneComposer.AddLineSegment(
            new GrVisualLineSegment3D("zxSegment1")
            {
                Position1 = z + x,
                Position2 = z,
                Style = dashedStyle
            }
        );

        MainSceneComposer.AddLineSegment(
            new GrVisualLineSegment3D("zxSegment2")
            {
                Position1 = z + x,
                Position2 = x,
                Style = dashedStyle
            }
        );

        MainSceneComposer.AddLineSegment(
            new GrVisualLineSegment3D("zxSegment3")
            {
                Position1 = z + x,
                Position2 = v,
                Style = dashedStyle
            }
        );
    }

    //private void AddSignalNormal(Tuple3D k)
    //{
    //    var scene = SceneComposer.SceneObject;

    //    var material = scene.AddSimpleMaterial("kVectorMaterial", Color.SaddleBrown);

    //    SceneComposer.AddVector(
    //        new GrVisualVector3D("e1Vector")
    //        {
    //            Origin = Tuple3D.Zero,
    //            Direction = k,
    //            Style = new GrVisualVectorStyle3D
    //            {
    //                Material = material,
    //                Thickness = 0.075
    //            }
    //        }
    //    );

    //    SceneComposer.AddLaTeXText(
    //        new GrVisualLaTeXText3D(ImageCache, "e1VectorText")
    //        {
    //            ScalingFactor = LaTeXScalingFactor,
    //            Origin = k + k.ToUnitVector() * 0.5d
    //        }
    //    );

    //    return SceneComposer;
    //}

    private void AddSignalPlane(ITuple3D k)
    {
        var scene = MainSceneComposer.SceneObject;

        scene.AddStandardMaterial(
            "discMaterial",

            new GrBabylonJsStandardMaterial.StandardMaterialProperties
            {
                Alpha = 0.55,
                Color = Color.BurlyWood,
                TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend,
                BackFaceCulling = true
            }
        );

        var disc = new GrVisualCircleSurface3D("disc")
        {
            Center = Tuple3D.Zero,
            Radius = PowerSignal.ScalarBounds.MaxValue * 1.5d, //Math.Sqrt(3d / 2d),
            Normal = k,
            Style = new GrVisualThickSurfaceStyle3D
            {
                Material = scene.GetMaterial("discMaterial"),
                Thickness = 0.025
            }
        };

        var ring = new GrVisualRingSurface3D("ring")
        {
            Center = Tuple3D.Zero,
            MinRadius = PowerSignal.ScalarBounds.MaxValue * Math.Sqrt(3d / 2d) - 0.5d,
            MaxRadius = PowerSignal.ScalarBounds.MaxValue * Math.Sqrt(3d / 2d) + 0.5d,
            Normal = k,
            Style = new GrVisualThickSurfaceStyle3D
            {
                Material = scene.GetMaterial("discMaterial"),
                Thickness = 0.025
            }
        };

        MainSceneComposer.AddElement(ring);
    }

    private void AddSignalCurve(int index)
    {
        if (index < 2 || PowerSignal.FrameList is null) 
            return;

        var scene = MainSceneComposer.SceneObject;

        //var material = scene.AddStandardMaterial(
        //    "curveMaterial",
        //    Color.Yellow
        //);

        //var length = 
        //    sampledCurve.Take(index + 1).ComputeCurveLength();

        var textureImage = new GrVisualComputedImage3D("")
        {
            Width = 1,
            Height = 256,
            ColorFunc = (i, j) => Color.Yellow.WithAlpha(1f - j / 255f)
        }.GetImage().PngToBase64HtmlString();

        var texture = scene.AddTexture(
            "curveMaterialTexture",
            textureImage,
            new GrBabylonJsTexture.TextureProperties
            {
                HasAlpha = true
            }
        );

        var material = scene.AddStandardMaterial(
            "curveMaterial",
            new GrBabylonJsStandardMaterial.StandardMaterialProperties
            {
                //DiffuseColor = Color.Yellow,
                TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend,
                DiffuseTexture = texture.ConstName,
                UseAlphaFromDiffuseTexture = true,
                BackFaceCulling = true
            }
        );


        var trailStartIndex = Math.Max(0, index - TrailSampleCount);
        var trailLength = index - trailStartIndex;

        var pointList = 
            PowerSignal
                .FrameList
                .Skip(trailStartIndex)
                .Take(trailLength)
                .ToImmutableArray();

        MainSceneComposer.AddLineCurve(
            new GrVisualLineCurve3D("curve")
            {
                PositionList = pointList,
                Style = new GrVisualCurveTubeStyle3D
                {
                    Material = material,
                    Thickness = 0.05
                }
            }
        );

        //var magnitude = Magnitudes.Max();
        //SceneComposer.AddElement(
        //    new GrVisualCircleCurve3D("curve")
        //    {
        //        Center = Tuple3D.Zero,
        //        Radius = magnitude * Math.Sqrt(3d / 2d),
        //        Normal = new Tuple3D(1, 1, 1).ToUnitVector(),
        //        Style = new GrVisualCurveTubeStyle3D
        //        {
        //            Material = material,
        //            Thickness = 0.035
        //        }
        //    }
        //);
    }

    private void AddSignalFrame(int index)
    {
        if (PowerSignal.FrameList is null || PowerSignal.CurvatureList is null || PowerSignal.SampledCurve is null)
            return;

        var scene = MainSceneComposer.SceneObject;

        var lineArrayList = new List<Tuple3D[]>(PowerSignal.SampledCurve.CornerCount);
        var colorArrayList = new List<Color[]>(PowerSignal.SampledCurve.CornerCount);

        var xColor1 = Color.Red.WithAlpha(0.8f); //Color.Red.SetAlpha(128);
        var xColor2 = Color.Red.WithAlpha(0.0f);
        var yColor1 = Color.Green.WithAlpha(0.8f);
        var yColor2 = Color.Green.WithAlpha(0.0f);
        var zColor1 = Color.Blue.WithAlpha(0.8f);
        var zColor2 = Color.Blue.WithAlpha(0.0f);

        const double length = 0.8d;

        var trailStartIndex = Math.Max(0, index - TrailSampleCount);
        var trailLength = index - trailStartIndex;

        var frameList = 
            PowerSignal
                .FrameList
                .Skip(trailStartIndex)
                .Take(trailLength)
                .ToImmutableArray();

        var i = trailStartIndex - 1;
        foreach (var frame in frameList)
        {
            if (i % FrameSeparationCount == 0)
            {
                var origin = frame.Origin;
                var xPoint = origin + length * frame.Direction1.ToUnitVector();
                var yPoint = origin + length * frame.Direction2.ToUnitVector();
                var zPoint = origin + length * frame.Direction3.ToUnitVector();

                lineArrayList.Add(new[] { origin, xPoint });
                lineArrayList.Add(new[] { origin, yPoint });
                lineArrayList.Add(new[] { origin, zPoint });

                colorArrayList.Add(new[] { xColor1, xColor2 });
                colorArrayList.Add(new[] { yColor1, yColor2 });
                colorArrayList.Add(new[] { zColor1, zColor2 });
            }

            i++;
        }

        scene.AddLineSystem(
            "curveFrameLines",
            new GrBabylonJsLineSystem.LineSystemOptions
            {
                Lines = lineArrayList.ToArray(),
                Colors = colorArrayList.ToArray()
            }
        );

        var curveFrame = PowerSignal.FrameList[index];
        MainSceneComposer.AddElement(
            new GrVisualFrame3D("curveFrame")
            {
                Origin = curveFrame.Origin,
                Direction1 = curveFrame.Direction1,
                Direction2 = curveFrame.Direction2,
                Direction3 = curveFrame.Direction3,

                Style = new GrVisualFrameStyle3D
                {
                    OriginThickness = 0.075,
                    DirectionThickness = 0.035,
                    OriginMaterial = scene.AddStandardMaterial("curveFrameOriginMaterial", Color.DarkGray),
                    DirectionMaterial1 = scene.AddStandardMaterial("curveFrameDirectionMaterial1", Color.DarkRed),
                    DirectionMaterial2 = scene.AddStandardMaterial("curveFrameDirectionMaterial2", Color.DarkGreen),
                    DirectionMaterial3 = scene.AddStandardMaterial("curveFrameDirectionMaterial3", Color.DarkBlue)
                },

                Direction1TextImage = new GrVisualLaTeXText3D(ImageCache, "e1VectorText")
                {
                    ScalingFactor = LaTeXScalingFactor,
                    Origin = curveFrame.Origin + 
                             curveFrame.Direction1 + 
                             curveFrame.Direction1.ToUnitVector() * 0.25d
                },

                Direction2TextImage = new GrVisualLaTeXText3D(ImageCache, "e2VectorText")
                {
                    ScalingFactor = LaTeXScalingFactor,
                    Origin = curveFrame.Origin + 
                             curveFrame.Direction2 + 
                             curveFrame.Direction2.ToUnitVector() * 0.25d
                },

                Direction3TextImage = new GrVisualLaTeXText3D(ImageCache, "e3VectorText")
                {
                    ScalingFactor = LaTeXScalingFactor,
                    Origin = curveFrame.Origin + 
                             curveFrame.Direction3 + 
                             curveFrame.Direction3.ToUnitVector() * 0.25d
                }
            }
        );

        var (kappa1, kappa2) = PowerSignal.CurvatureList[index];

        var e1 = curveFrame.Direction1.ToUnitVector();
        var e2 = curveFrame.Direction2.ToUnitVector();
        var e3 = curveFrame.Direction3.ToUnitVector();

        var t = PowerSignal.TimeValues[index];
        var sDt = PowerSignal.GetDerivative1Norm(t);
        var k21Vector = (kappa2 * e3 - kappa1 * e1) / 2;
        var omegaNorm = (kappa1.Square() + kappa2.Square()).Sqrt() / 2;
        
        var radius = sDt / (2 * omegaNorm);
        var center = Tuple3D.Zero; //curveFrame.Origin + e2 * radius; 
        var normal = e2.VectorUnitCross(k21Vector);

        MainSceneComposer.AddCircleSurface(
            new GrVisualCircleSurface3D("curveFrameCircleSurface")
            {
                Center = center,
                Radius = radius,
                Normal = normal,

                Style = new GrVisualThickSurfaceStyle3D
                {
                    Material = scene.AddStandardMaterial(
                        "curveFrameCircleSurfaceMaterial", 
                        Color.YellowGreen.WithAlpha(0.2f)
                    ),
                    Thickness = 0.035d
                }
            }
        );

        MainSceneComposer.AddVector(
            new GrVisualVector3D("curveFrameCircleNormal")
            {
                Origin = center,
                Direction = normal,

                Style = new GrVisualVectorStyle3D
                {
                    Material = scene.AddStandardMaterial(
                        "curveFrameCircleNormalMaterial",
                        System.Drawing.Color.Brown
                    ),
                    Thickness = 0.05d
                },

                TextImage = new GrVisualLaTeXText3D(ImageCache, "kVectorText")
                {
                    ScalingFactor = LaTeXScalingFactor,
                    Origin = center + normal * 1.25d
                }
            }
        );

        MainSceneComposer.AddCircleCurve(
            new GrVisualCircleCurve3D("curveFrameCircleCurve")
            {
                Center = center,
                Radius = radius,
                Normal = normal,

                Style = new GrVisualCurveTubeStyle3D
                {
                    Material = scene.GetMaterial("curveFrameCircleNormalMaterial"),
                    Thickness = 0.035d
                }
            }
        );
    }

    private void AddSignalFrameBivectors(int index)
    {
        if (PowerSignal.FrameList is null || PowerSignal.CurvatureList is null || PowerSignal.SampledCurve is null)
            return;

        var scene = HtmlComposer.GetScene("omegaScene");

        var curveFrame = PowerSignal.FrameList[index];

        // Display bivector omega = kappa1 e12 + kappa2 e23
        var (kappa1, kappa2) = PowerSignal.CurvatureList[index];

        var e1 = Tuple3D.E1;
        var e2 = Tuple3D.E2;
        var e3 = Tuple3D.E3;

        var e1Ds = kappa1 * e2;
        var e2Ds = kappa2 * e3 - kappa1 * e1;
        var e3Ds = -kappa2 * e2;
        
        OmegaSceneComposer.AddElement(
            new GrVisualFrame3D("curveFrame")
            {
                Origin = OmegaFrameOrigin,
                Direction1 = e1,
                Direction2 = e2,
                Direction3 = e3,

                Style = new GrVisualFrameStyle3D
                {
                    OriginThickness = 0.075,
                    DirectionThickness = 0.035,
                    OriginMaterial = scene.AddStandardMaterial("curveFrameOriginMaterial", Color.DarkGray),
                    DirectionMaterial1 = scene.AddStandardMaterial("curveFrameDirectionMaterial1", Color.DarkRed),
                    DirectionMaterial2 = scene.AddStandardMaterial("curveFrameDirectionMaterial2", Color.DarkGreen),
                    DirectionMaterial3 = scene.AddStandardMaterial("curveFrameDirectionMaterial3", Color.DarkBlue)
                },

                Direction1TextImage = new GrVisualLaTeXText3D(ImageCache, "e1VectorText")
                {
                    ScalingFactor = LaTeXScalingFactor,
                    Origin = OmegaFrameOrigin + e1 * 1.25d
                },

                Direction2TextImage = new GrVisualLaTeXText3D(ImageCache, "e2VectorText")
                {
                    ScalingFactor = LaTeXScalingFactor,
                    Origin = OmegaFrameOrigin + e2 * 1.25d
                },

                Direction3TextImage = new GrVisualLaTeXText3D(ImageCache, "e3VectorText")
                {
                    ScalingFactor = LaTeXScalingFactor,
                    Origin = OmegaFrameOrigin + e3 * 1.25d
                }
            }
        );

        if (e1Ds.GetLength() > 0.1)
        {
            var e1DsVectorMaterial = OmegaSceneComposer.SceneObject.AddStandardMaterial(
                "e1DsVectorMaterial",
                new GrBabylonJsStandardMaterial.StandardMaterialProperties
                {
                    Color = System.Drawing.Color.IndianRed
                }
            );

            OmegaSceneComposer.AddVector(
                new GrVisualVector3D("e1DsVector")
                {
                    Origin = OmegaFrameOrigin,
                    Direction = e1Ds,

                    Style = new GrVisualVectorStyle3D
                    {
                        Thickness = 0.035,
                        Material = e1DsVectorMaterial
                    },

                    TextImage = new GrVisualLaTeXText3D(ImageCache, "e1DsVectorText")
                    {
                        ScalingFactor = LaTeXScalingFactor,
                        Origin = OmegaFrameOrigin + e1Ds + e1Ds.ToUnitVector() * 0.25d
                    }
                }
            );

            var curveFrameOmega1BivectorMaterial = OmegaSceneComposer.SceneObject.AddStandardMaterial(
                "curveFrameOmega1BivectorMaterial",
                new GrBabylonJsStandardMaterial.StandardMaterialProperties
                {
                    Alpha = 0.15,
                    TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend,
                    Color = System.Drawing.Color.IndianRed,
                    BackFaceCulling = false
                }
            );

            OmegaSceneComposer.AddRectangleSurface(
                new GrVisualRectangleSurface3D(
                    "omega1Bivector",
                    OmegaFrameOrigin,
                    e1,
                    e1Ds
                )
                {
                    Style = new GrVisualThinSurfaceStyle3D
                    {
                        Material = curveFrameOmega1BivectorMaterial
                    },

                    TextImage = new GrVisualLaTeXText3D(ImageCache, "omega1BivectorText")
                    {
                        Origin = OmegaFrameOrigin + 0.75d * (e1 + e1Ds),
                        ScalingFactor = LaTeXScalingFactor
                    }
                }
            );
        }

        if (e2Ds.GetLength() > 0.1)
        {
            var e2DsVectorMaterial = OmegaSceneComposer.SceneObject.AddStandardMaterial(
                "e2DsVectorMaterial",
                new GrBabylonJsStandardMaterial.StandardMaterialProperties
                {
                    Color = System.Drawing.Color.LimeGreen
                }
            );

            OmegaSceneComposer.AddVector(
                new GrVisualVector3D("e2DsVector")
                {
                    Origin = OmegaFrameOrigin,
                    Direction = e2Ds,

                    Style = new GrVisualVectorStyle3D
                    {
                        Thickness = 0.035,
                        Material = e2DsVectorMaterial
                    },

                    TextImage = new GrVisualLaTeXText3D(ImageCache, "e2DsVectorText")
                    {
                        ScalingFactor = LaTeXScalingFactor,
                        Origin = OmegaFrameOrigin + e2Ds + e2Ds.ToUnitVector() * 0.25d
                    }
                }
            );

            var curveFrameOmegaBivectorMaterial = OmegaSceneComposer.SceneObject.AddStandardMaterial(
                "curveFrameOmegaBivectorMaterial",
                new GrBabylonJsStandardMaterial.StandardMaterialProperties
                {
                    Alpha = 0.15,
                    TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend,
                    Color = System.Drawing.Color.LimeGreen,
                    BackFaceCulling = false
                }
            );

            OmegaSceneComposer.AddRectangleSurface(
                new GrVisualRectangleSurface3D(
                    "omegaBivector",
                    OmegaFrameOrigin,
                    e2,
                    e2Ds
                )
                {
                    Style = new GrVisualThinSurfaceStyle3D
                    {
                        Material = curveFrameOmegaBivectorMaterial
                    },

                    TextImage = new GrVisualLaTeXText3D(ImageCache, "omega2BivectorText")
                    {
                        Origin = OmegaFrameOrigin + 0.75 * (e2 + e2Ds),
                        ScalingFactor = LaTeXScalingFactor
                    }
                }
            );
        }

        if (e3Ds.GetLength() > 0.1)
        {
            var e3DsVectorMaterial = OmegaSceneComposer.SceneObject.AddStandardMaterial(
                "e3DsVectorMaterial",
                new GrBabylonJsStandardMaterial.StandardMaterialProperties
                {
                    Color = System.Drawing.Color.DodgerBlue
                }
            );

            OmegaSceneComposer.AddVector(
                new GrVisualVector3D("e3DsVector")
                {
                    Origin = OmegaFrameOrigin,
                    Direction = e3Ds,

                    Style = new GrVisualVectorStyle3D
                    {
                        Thickness = 0.035,
                        Material = e3DsVectorMaterial
                    },

                    TextImage = new GrVisualLaTeXText3D(ImageCache, "e3DsVectorText")
                    {
                        ScalingFactor = LaTeXScalingFactor,
                        Origin = OmegaFrameOrigin + e3Ds + e3Ds.ToUnitVector() * 0.25d
                    }
                }
            );

            var curveFrameOmega3BivectorMaterial = OmegaSceneComposer.SceneObject.AddStandardMaterial(
                "curveFrameOmega3BivectorMaterial",
                new GrBabylonJsStandardMaterial.StandardMaterialProperties
                {
                    Alpha = 0.15,
                    TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend,
                    Color = System.Drawing.Color.DodgerBlue,
                    BackFaceCulling = false
                }
            );

            OmegaSceneComposer.AddRectangleSurface(
                new GrVisualRectangleSurface3D(
                    "omega3Bivector",
                    OmegaFrameOrigin,
                    e3,
                    e3Ds
                )
                {
                    Style = new GrVisualThinSurfaceStyle3D
                    {
                        Material = curveFrameOmega3BivectorMaterial
                    },

                    TextImage = new GrVisualLaTeXText3D(ImageCache, "omega3BivectorText")
                    {
                        Origin = OmegaFrameOrigin + 0.75d * (e3 + e3Ds),
                        ScalingFactor = LaTeXScalingFactor
                    }
                }
            );
        }
    }

    private string GenerateSnapshotCode(int index)
    {
        var t = PowerSignal.TimeValues[index];
        var alpha = CameraAlphaValues[index];
        var beta = CameraBetaValues[index];

        var (x, y, z) = PowerSignal.GetPhaseVectors(t);

        HtmlComposer = InitializeSceneComposers(
            index, 
            CanvasWidth, 
            CanvasHeight
        );
        
        AddCamera(alpha, beta);
        AddEnvironment();
        AddGrid();
        AddGuiLayer(index);
        AddPhaseVector1(x);
        AddPhaseVector2(y);
        AddPhaseVector3(z);
        AddSignalVector(x, y, z);
        //AddSignalNormal(frame.Direction3);
        //AddSignalPlane(frame.Direction3);
        AddSignalCurve(index);
        AddSignalFrame(index);
        AddSignalFrameBivectors(index);

        return HtmlComposer.GetHtmlCode();
    }


    public override void GenerateSnapshots()
    {
        Console.Write("Computing signal values .. ");

        CameraAlphaValues =
            30d.DegreesToRadians().GetCosRange(
                150d.DegreesToRadians(),
                FrameCount,
                CameraRotationCount,
                true
            ).CreateSignal(PowerSignal.SamplingRate);

        CameraBetaValues =
            Enumerable
                .Repeat(2 * Math.PI / 5, FrameCount)
                .CreateSignal(PowerSignal.SamplingRate);

        Console.WriteLine("done.");
        Console.WriteLine();

        InitializeImageCache();

        GenerateHtml = true;
        AnimatedGifFrameDelay = (int) Math.Ceiling(1d / PowerSignal.SamplingRate);
        Mp4FrameRate = PowerSignal.SamplingRate;

        base.GenerateSnapshots();
    }
}