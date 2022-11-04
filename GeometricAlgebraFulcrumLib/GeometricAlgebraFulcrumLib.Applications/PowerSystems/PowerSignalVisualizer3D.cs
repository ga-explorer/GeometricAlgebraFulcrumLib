using System.Collections.Immutable;
using DataStructuresLib.Basic;
using DataStructuresLib.Files;
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
using GraphicsComposerLib.Rendering.Visuals.Space3D.Curves;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Grids;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Groups;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Images;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.GeometricAlgebra.Euclidean3D;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems;

public class PowerSignalVisualizer3D :
    GrBabylonJsSnapshotComposer3D
{
    public ComputedPowerSignal3D PowerSignal { get; }
 
    public int TrailSampleCount { get; set; }

    public int PlotSampleCount { get; set; }

    public int FrameSeparationCount { get; set; } = 20;

    public bool ShowLeftPanel { get; set; } = true;

    public bool ShowRightPanel { get; set; } = true;
    
    public Tuple3D OmegaFrameOrigin { get; set; } = new Tuple3D(-6, 2, 1);
    
    public int SignalTextImageMaxWidth { get; private set; }

    public int SignalTextImageMaxHeight { get; private set; }
    
    public GrBabylonJsSceneComposer3D OmegaSceneComposer 
        => HtmlComposer.GetSceneComposer("omegaScene");


    public PowerSignalVisualizer3D(IReadOnlyList<double> cameraAlphaValues, IReadOnlyList<double> cameraBetaValues, ComputedPowerSignal3D powerSignal)
        : base(cameraAlphaValues, cameraBetaValues)
    {
        PowerSignal = powerSignal;
    }

    
    protected override void InitializeImageCache()
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

    protected override GrBabylonJsHtmlComposer3D InitializeSceneComposers(int index)
    {
        var mainSceneComposer = new GrBabylonJsSceneComposer3D(
            "mainScene",
            new GrBabylonJsSnapshotSpecs
            {
                Enabled = true,
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

        //mainSceneComposer.SceneObject.SceneProperties.UseOrderIndependentTransparency = true;

        var omegaSceneComposer = new GrBabylonJsSceneComposer3D("omegaScene")
        {
            BackgroundColor = Color.AliceBlue,
            ShowDebugLayer = false
        };

        omegaSceneComposer.SceneObject.Properties.AutoClear = false;

        var htmlComposer = new GrBabylonJsHtmlComposer3D(mainSceneComposer)
        {
            CanvasWidth = CanvasWidth,
            CanvasHeight = CanvasHeight,
            CanvasFullScreen = false
        };

        htmlComposer.AddSceneComposer(omegaSceneComposer, false);

        //htmlComposer.SetActiveSceneComposer(constName);

        return htmlComposer;
    }
    
    protected override void AddCamera(int index)
    {
        base.AddCamera(index);

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
     
    protected override void AddGrid()
    {
        base.AddGrid();
        
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

    protected override void AddGuiLayer(int index)
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
        MainSceneComposer.AddVector(
            "v1Vector",
            Tuple3D.Zero,
            x,
            Color.Red,
            0.05
        ).AddLaTeXText(
            "v1VectorText",
            ImageCache,
            x + x.ToUnitVector() * 0.25d + (Tuple3D.E2 + Tuple3D.E3) * 0.25d / 2d.Sqrt(),
            LaTeXScalingFactor
        );

        MainSceneComposer.AddLineSegment(
            "v1Trail",
            new Tuple3D(PowerSignal.VectorBounds.MinX, 0, 0),
            new Tuple3D(PowerSignal.VectorBounds.MaxX, 0, 0),
            Color.Red.WithAlpha(0.25f),
            0.045
        );
    }

    private void AddPhaseVector2(Tuple3D y)
    {
        MainSceneComposer.AddVector(
            "v2Vector", 
            Tuple3D.Zero, 
            y,
            Color.Green,
            0.05
        ).AddLaTeXText(
            "v2VectorText",
            ImageCache,
            y + y.ToUnitVector() * 0.25d + (Tuple3D.E1 + Tuple3D.E3) * 0.25d / 2d.Sqrt(),
            LaTeXScalingFactor
        );
            
        MainSceneComposer.AddLineSegment(
            "v2TrailSegment",
            new Tuple3D(0, PowerSignal.VectorBounds.MinY, 0),
            new Tuple3D(0, PowerSignal.VectorBounds.MaxY, 0),
            Color.Green.WithAlpha(0.25f),
            0.045
        );
    }

    private void AddPhaseVector3(Tuple3D z)
    {
        MainSceneComposer.AddVector(
            "v3Vector", 
            Tuple3D.Zero, 
            z,
            Color.Blue,
            0.05
        ).AddLaTeXText(
            "v3VectorText",
            ImageCache,
            z + z.ToUnitVector() * 0.25d + (Tuple3D.E1 + Tuple3D.E2) * 0.25d / 2d.Sqrt(),
            LaTeXScalingFactor
        );
    }

    private void AddSignalVector(Tuple3D x, Tuple3D y, Tuple3D z)
    {
        var v = x + y + z;
        
        MainSceneComposer.AddVector(
            "vVector",
            Tuple3D.Zero,
            v,
            Color.DarkOrange,
            0.05
        ).AddLaTeXText(
            "vVectorText",
            ImageCache,
            v + v.ToUnitVector() * 0.25d,
            LaTeXScalingFactor
        );
        
        var dashSpecs = 
            new GrVisualDashedLineSpecs(1, 1, 20);

        MainSceneComposer.AddLineSegment(
            "xySegment1",
            x + y,
            x,
            Color.Gray,
            dashSpecs
        ).AddLineSegment(
            "xySegment2",
            x + y,
            y,
            Color.Gray,
            dashSpecs
        ).AddLineSegment(
            "xySegment3",
            x + y,
            v,
            Color.Gray,
            dashSpecs
        );
        
        MainSceneComposer.AddLineSegment(
            "yzSegment1",
            y + z,
            y,
            Color.Gray,
            dashSpecs
        ).AddLineSegment(
            "yzSegment2",
            y + z,
            z,
            Color.Gray,
            dashSpecs
        ).AddLineSegment(
            "yzSegment3",
            y + z,
            v,
            Color.Gray,
            dashSpecs
        );

        MainSceneComposer.AddLineSegment(
            "zxSegment1",
            z + x,
            z,
            Color.Gray,
            dashSpecs
        ).AddLineSegment(
            "zxSegment2",
            z + x,
            x,
            Color.Gray,
            dashSpecs
        ).AddLineSegment(
            "zxSegment3",
            z + x,
            v,
            Color.Gray,
            dashSpecs
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

            Style = new GrVisualSurfaceThickStyle3D(
                scene.GetMaterial("discMaterial"),
                0.025
            )
        };

        var ring = new GrVisualRingSurface3D("ring")
        {
            Center = Tuple3D.Zero,
            MinRadius = PowerSignal.ScalarBounds.MaxValue * Math.Sqrt(3d / 2d) - 0.5d,
            MaxRadius = PowerSignal.ScalarBounds.MaxValue * Math.Sqrt(3d / 2d) + 0.5d,
            Normal = k,

            Style = new GrVisualSurfaceThickStyle3D(
                scene.GetMaterial("discMaterial"),
                0.025
            )
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

        MainSceneComposer.AddLinePath(
            "curve",
            pointList,
            material, 
            0.05
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
                }
            }
        );

        MainSceneComposer.AddLaTeXText(
            "e1VectorText",
            ImageCache,
            curveFrame.Origin + 
            curveFrame.Direction1 + 
            curveFrame.Direction1.ToUnitVector() * 0.25d,
            LaTeXScalingFactor
        );
        
        MainSceneComposer.AddLaTeXText(
            "e2VectorText",
            ImageCache,
            curveFrame.Origin + 
            curveFrame.Direction2 + 
            curveFrame.Direction2.ToUnitVector() * 0.25d,
            LaTeXScalingFactor
        );
        
        MainSceneComposer.AddLaTeXText(
            "e3VectorText",
            ImageCache,
            curveFrame.Origin + 
            curveFrame.Direction3 + 
            curveFrame.Direction3.ToUnitVector() * 0.25d,
            LaTeXScalingFactor
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

        MainSceneComposer.AddDisc(
            "curveFrameCircleSurface",
            center,
            normal,
            radius,
            Color.YellowGreen.WithAlpha(0.2f),
            0.035d
        );

        MainSceneComposer.AddVector(
            "curveFrameCircleNormal",
            center,
            normal,
            Color.Brown,
            0.05d
        );

        MainSceneComposer.AddLaTeXText(
            "kVectorText",
            ImageCache,
            center + normal * 1.25d,
            LaTeXScalingFactor
        );

        MainSceneComposer.AddCircle(
            "curveFrameCircleCurve",
            center,
            normal,
            radius,
            scene.GetMaterial("curveFrameCircleNormalMaterial"),
            0.035d
        );
    }

    private void AddSignalFrameBivectors(int index)
    {
        if (PowerSignal.FrameList is null || PowerSignal.CurvatureList is null || PowerSignal.SampledCurve is null)
            return;

        var scene = HtmlComposer.GetScene("omegaScene");
        
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
                }
            }
        );

        OmegaSceneComposer.AddLaTeXText(
            "e1VectorText",
            ImageCache,
            OmegaFrameOrigin + e1 * 1.25d,
            LaTeXScalingFactor
        ).AddLaTeXText(
            "e2VectorText",
            ImageCache,
            OmegaFrameOrigin + e2 * 1.25d,
            LaTeXScalingFactor
        ).AddLaTeXText(
            "e2VectorText",
            ImageCache,
            OmegaFrameOrigin + e3 * 1.25d,
            LaTeXScalingFactor
        );

        if (e1Ds.GetLength() > 0.1)
        {
            OmegaSceneComposer.AddVector(
                "e1DsVector",
                OmegaFrameOrigin,
                e1Ds,
                Color.IndianRed,
                0.035
            );

            OmegaSceneComposer.AddLaTeXText(
                "e1DsVectorText",
                ImageCache,
                OmegaFrameOrigin + e1Ds + e1Ds.ToUnitVector() * 0.25d,
                LaTeXScalingFactor
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

            OmegaSceneComposer.AddRectangle(
                "omega1Bivector",
                OmegaFrameOrigin,
                e1,
                e1Ds,
                curveFrameOmega1BivectorMaterial
            );
            
            OmegaSceneComposer.AddLaTeXText(
                "omega1BivectorText",
                ImageCache,
                OmegaFrameOrigin + 0.75d * (e1 + e1Ds),
                LaTeXScalingFactor
            );
        }

        if (e2Ds.GetLength() > 0.1)
        {
            OmegaSceneComposer.AddVector(
                "e2DsVector",
                OmegaFrameOrigin,
                e2Ds,
                Color.LimeGreen,
                0.035
            );
            
            OmegaSceneComposer.AddLaTeXText(
                "e2DsVectorText",
                ImageCache,
                OmegaFrameOrigin + e2Ds + e2Ds.ToUnitVector() * 0.25d,
                LaTeXScalingFactor
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

            OmegaSceneComposer.AddRectangle(
                "omegaBivector",
                OmegaFrameOrigin,
                e2,
                e2Ds,
                curveFrameOmegaBivectorMaterial
            );
            
            OmegaSceneComposer.AddLaTeXText(
                "omega2BivectorText",
                ImageCache,
                OmegaFrameOrigin + 0.75 * (e2 + e2Ds),
                LaTeXScalingFactor
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
                "e3DsVector",
                OmegaFrameOrigin,
                e3Ds,
                Color.DodgerBlue,
                0.035
            );
            
            OmegaSceneComposer.AddLaTeXText(
                "e3DsVectorText",
                ImageCache,
                OmegaFrameOrigin + e3Ds + e3Ds.ToUnitVector() * 0.25d,
                LaTeXScalingFactor
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

            OmegaSceneComposer.AddRectangle(
                "omega3Bivector",
                OmegaFrameOrigin,
                e3,
                e3Ds,
                curveFrameOmega3BivectorMaterial
            );
            
            OmegaSceneComposer.AddLaTeXText(
                "omega3BivectorText",
                ImageCache,
                OmegaFrameOrigin + 0.75d * (e3 + e3Ds),
                LaTeXScalingFactor
            );
        }
    }

    protected override GrBabylonJsHtmlComposer3D GenerateSnapshotCode(int index)
    {
        base.GenerateSnapshotCode(index);

        var t = PowerSignal.TimeValues[index];
        var (x, y, z) = PowerSignal.GetPhaseVectors(t);
        
        AddPhaseVector1(x);
        AddPhaseVector2(y);
        AddPhaseVector3(z);
        AddSignalVector(x, y, z);
        //AddSignalNormal(frame.Direction3);
        //AddSignalPlane(frame.Direction3);
        AddSignalCurve(index);
        AddSignalFrame(index);
        AddSignalFrameBivectors(index);

        return HtmlComposer;
    }
}