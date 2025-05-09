using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Cameras;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using GeometricAlgebraFulcrumLib.Utilities.Web.Html.Media;
using GeometricAlgebraFulcrumLib.Utilities.Web.Images;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.KaTeX;
using SixLabors.ImageSharp;
using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems;

public class GrBabylonJsPowerSignalVisualizer :
    GrBabylonJsSceneSequenceComposer
{
    public Float64PowerSignal3D Signal { get; }
 
    public double TimeScaling { get; set; } = 1;

    public double ValueScaling { get; set; } = 1;

    public int TrailSampleCount { get; set; }

    public int PlotSampleCount { get; set; }

    public int FrameSeparationCount { get; set; } = 20;

    public bool ShowLeftPanel { get; set; } = true;

    public bool ShowRightPanel { get; set; } = true;
    
    public LinFloat64Vector3D OmegaFrameOrigin { get; set; } = LinFloat64Vector3D.Create(-6, 2, 1);
    
    public GrBabylonJsSceneComposer OmegaSceneComposer 
        => CodeFilesComposer.GetSceneComposer("omegaScene");


    public GrBabylonJsPowerSignalVisualizer(string workingFolder, Float64SamplingSpecs samplingSpecs, Float64PowerSignal3D powerSignal)
        : base(workingFolder, samplingSpecs)
    {
        Signal = powerSignal;
    }


    private Image RenderSignalTextLaTeXImage(int frameIndex)
    {
        var katexComposer = new WclKaTeXComposer(WorkingFolder)
        {
            FontSizeEm = 2,
            Output = WclKaTeXComposer.OutputKind.Html,
            ThrowOnError = false,
            SaveImages = false
        };

        var t = Signal.TimeValues[frameIndex];
        var (x, y, z) = Signal.GetComponentVectors(t);
        var v = x + y + z;

        var frame = Signal.FrameList[frameIndex]; //GetSignalFrame(t);

        var e1 = frame.Direction1.ToUnitLinVector3D();
        var e2 = frame.Direction2.ToUnitLinVector3D();
        var e3 = frame.Direction3.ToUnitLinVector3D();
        
        var sDt = Signal.GetTangentNormValue(t);

        var (kappa1, kappa2) = Signal.CurvatureList[frameIndex];

        var omega = Signal.DarbouxBivectorList[frameIndex];
        var omegaNorm = omega.Norm();

        var omegaMean = Signal.GetDarbouxBivectorMean(frameIndex);
        var omegaMeanNorm = omegaMean.Norm();

        var frequencyHz = omegaNorm / (Math.Tau);
        var frequencyHzMean = omegaMeanNorm / (Math.Tau);

        var e1Ds = kappa1 * e2;
        var e2Ds = kappa2 * e3 - kappa1 * e1;
        var e3Ds = -kappa2 * e2;

        var kVector = omega.NormalToUnitDirection3D();
        var kVectorMean = omegaMean.NormalToUnitDirection3D();

        var imageKey = $"signalText-{frameIndex:D6}";

        katexComposer.AddLaTeXAlignedEquations(
            imageKey,
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

        katexComposer.RenderKaTeX();

        return katexComposer[imageKey].PngImage;
    }

    protected override void AddLaTeXTextures()
    {
        KaTeXComposer.AddLaTeXEquation(
            "basis1VectorText",
            @"\boldsymbol{\sigma}_{1}"
        );
            
        KaTeXComposer.AddLaTeXEquation(
            "basis2VectorText",
            @"\boldsymbol{\sigma}_{2}"
        );
            
        KaTeXComposer.AddLaTeXEquation(
            "basis3VectorText",
            @"\boldsymbol{\sigma}_{3}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "v1VectorText",
            @"\boldsymbol{v}_{1}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "v2VectorText",
            @"\boldsymbol{v}_{2}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "v3VectorText",
            @"\boldsymbol{v}_{3}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "vVectorText",
            @"\boldsymbol{v}"
        );
            
        KaTeXComposer.AddLaTeXEquation(
            "e1VectorText",
            @"\boldsymbol{e}_{1}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "e2VectorText",
            @"\boldsymbol{e}_{2}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "e3VectorText",
            @"\boldsymbol{e}_{3}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "kVectorText",
            @"\hat{\boldsymbol{k}}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "e1DsVectorText",
            @"\dot{\boldsymbol{e}}_{1}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "e2DsVectorText",
            @"\dot{\boldsymbol{e}}_{2}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "e3DsVectorText",
            @"\dot{\boldsymbol{e}}_{3}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "omega1BivectorText",
            @"\boldsymbol{\Omega}_{1}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "omega2BivectorText",
            @"\boldsymbol{\Omega}_{2}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "omega3BivectorText",
            @"\boldsymbol{\Omega}_{3}"
        );


        KaTeXComposer.AddLaTeXCode("symbolicSignalText", Signal.LaTeXCode);

        //for (var i = 0; i < Signal.TimeValues.Count; i++)
        //{
        //    var t = Signal.TimeValues[i];
        //    var (x, y, z) = Signal.GetComponentVectors(t);
        //    var v = x + y + z;

        //    var frame = Signal.FrameList[i]; //GetSignalFrame(t);

        //    var e1 = frame.Direction1.ToUnitLinVector3D();
        //    var e2 = frame.Direction2.ToUnitLinVector3D();
        //    var e3 = frame.Direction3.ToUnitLinVector3D();
            
        //    var sDt = Signal.GetTangentNormValue(t);

        //    var (kappa1, kappa2) = Signal.CurvatureList[i];

        //    var omega = Signal.DarbouxBivectorList[i];
        //    var omegaNorm = omega.Norm();

        //    var omegaMean = Signal.GetDarbouxBivectorMean(i);
        //    var omegaMeanNorm = omegaMean.Norm();

        //    var frequencyHz = omegaNorm / (Math.Tau);
        //    var frequencyHzMean = omegaMeanNorm / (Math.Tau);

        //    var e1Ds = kappa1 * e2;
        //    var e2Ds = kappa2 * e3 - kappa1 * e1;
        //    var e3Ds = -kappa2 * e2;

        //    var kVector = omega.NormalToUnitDirection3D();
        //    var kVectorMean = omegaMean.NormalToUnitDirection3D();

        //    KaTeXComposer.AddLaTeXAlignedEquations(
        //        $"signalText-{i:D6}",
        //        new Pair<string>[]
        //        {
        //            new (@"t", @$"{t:F4}"),
        //            new (@"\boldsymbol{v}\left( t \right)", @$"\left( {v.X:F4}, {v.Y:F4}, {v.Z:F4} \right)"),
        //            new (@"\left\Vert \boldsymbol{v}^{\prime}\left(t\right)\right\Vert", @$"s^{{\prime}} \left( t \right) = {sDt:F4}"),
        //            new (@"\boldsymbol{e}_{1}\left( t \right)", @$"\left( {e1.X:F4}, {e1.Y:F4}, {e1.Z:F4} \right)"),
        //            new (@"\boldsymbol{e}_{2}\left( t \right)", @$"\left( {e2.X:F4}, {e2.Y:F4}, {e2.Z:F4} \right)"),
        //            new (@"\boldsymbol{e}_{3}\left( t \right)", @$"\left( {e3.X:F4}, {e3.Y:F4}, {e3.Z:F4} \right)"),
        //            new (@"\left\Vert \boldsymbol{\Omega}_{1}\left(t\right)\right\Vert", @$"\left| s^{{\prime}} \kappa_{{1}} \right| = {kappa1:F4}"),
        //            new (@"\left\Vert \boldsymbol{\Omega}_{2}\left(t\right)\right\Vert", @$"\left| s^{{\prime}} \right| \sqrt{{\kappa_{{1}}^{{2}}+\kappa_{{2}}^{{2}}}} = {omegaNorm:F4}"),
        //            new (@"\left\Vert \boldsymbol{\Omega}_{3}\left(t\right)\right\Vert", @$"\left| s^{{\prime}} \kappa_{{2}} \right| = {kappa2:F4}"),
        //            new (@"f \left( t \right)", $@"{frequencyHz:F4} \textrm{{ Hz}}"),
        //            new (@"\hat{\boldsymbol{k}}\left( t \right)", @$"\left( {kVector.X:F4}, {kVector.Y:F4}, {kVector.Z:F4} \right)"),
        //            new (@"\overline{f}", $@"{frequencyHzMean:F4} \textrm{{ Hz}}"),
        //            new (@"\overline{\boldsymbol{k}}", @$"\left( {kVectorMean.X:F4}, {kVectorMean.Y:F4}, {kVectorMean.Z:F4} \right)")
        //        }
        //    );
        //}
    } 

    protected override void InitializeSceneComposers(int index)
    {
        var mainSceneComposer = new GrBabylonJsSceneComposer(
            "mainScene",
            new GrBabylonJsSnapshotSpecs
            {
                Enabled = true,
                Width = ImageWidth,
                Height = ImageHeight,
                Precision = 1,
                UsePrecision = true,
                Delay = 2000,
                FileName = GetFrameName(index) + ".png"
            }
        )
        {
            BackgroundColor = Color.AliceBlue,
            ShowDebugLayer = false,
        };

        //mainSceneComposer.SceneObject.SceneProperties.UseOrderIndependentTransparency = true;

        var omegaSceneComposer = new GrBabylonJsSceneComposer("omegaScene")
        {
            BackgroundColor = Color.AliceBlue,
            ShowDebugLayer = false
        };

        omegaSceneComposer.SceneObject.Properties.AutoClear = false;

        CodeFilesComposer = new GrBabylonJsCodeFilesComposer(mainSceneComposer)
        {
            CanvasWidth = ImageWidth,
            CanvasHeight = ImageHeight,
            CanvasFullScreen = false
        };

        CodeFilesComposer.AddSceneComposer(omegaSceneComposer, false);

        //htmlComposer.SetActiveSceneComposer(constName);
    }
    
    protected override void SetCameraAndLights(int frameIndex)
    {
        base.SetCameraAndLights(frameIndex);

        // Add omega scene camera
        var omegaSceneCamera = OmegaSceneComposer.SceneObject.AddArcRotateCamera(
            "camera2",
            "Math.Tau / 6",  //alpha,
            "Math.Tau / 8",  // beta,
            11,
            "BABYLON.Vector3.Zero()",
            new GrBabylonJsArcRotateCameraProperties
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
    
    protected override void AddGuiLayer(int frameIndex)
    {
        var scene = MainSceneComposer.SceneObject;

        // Add GUI layer
        var uiTexture = scene.AddGuiFullScreenUi("uiTexture");

        if (ShowLeftPanel)
        {
            var uiPanel1Width = CodeFilesComposer.CanvasWidth * 0.275;
            var uiPanel1 = uiTexture.AddGuiStackPanel(
                "uiPanel1",
                new GrBabylonJsGuiStackPanelProperties
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
                $"'{SceneTitle}'",
                new GrBabylonJsGuiTextBlockProperties
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
                new GrBabylonJsGuiLineProperties
                {
                    X1 = 10,
                    X2 = uiPanel1Width - 10,
                    Y1 = 35,
                    Y2 = 35,
                    Color = Color.Black,
                    LineWidth = 1
                }
            );

            var latexPngData1 = ImageSet["latex", "symbolicSignalText"];
            uiPanel1.AddGuiImage(
                "latexGuiImage1",
                latexPngData1.GetImageDataUrlBase64(),
                new GrBabylonJsGuiImageProperties
                {
                    //Alpha = 0.5d,
                    WidthInPixels = uiPanel1Width - 20,
                    HeightInPixels = (uiPanel1Width - 20) * latexPngData1.ImageHeightToWidth,
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
            var uiPanel2Width = CodeFilesComposer.CanvasWidth * 0.275;
            var uiPanel2 = uiTexture.AddGuiStackPanel(
                "uiPanel2",
                new GrBabylonJsGuiStackPanelProperties
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

            var signalAnalyzer = Signal.CreateAnalyzer();

            var signalPlotImage = 
                signalAnalyzer.GetSignalPlotImage(frameIndex, PlotSampleCount); 
                //ImageSet["gui", $"SignalPlot-{frameIndex:D6}"];

            uiPanel2.AddGuiImage(
                "signalPlotGuiImage",
                signalPlotImage.PngToHtmlDataUrlBase64(),
                new GrBabylonJsGuiImageProperties
                {
                    Stretch = GrBabylonJsImageStretch.Uniform,
                    //Alpha = 0.5d,
                    WidthInPixels = uiPanel2Width - 20,
                    HeightInPixels = (uiPanel2Width - 20) * signalPlotImage.HeightToWidth(),
                    PaddingLeftInPixels = 0,
                    PaddingTopInPixels = 0,
                    PaddingBottomInPixels = 0,
                    PaddingRightInPixels = 0,
                    HorizontalAlignment = GrBabylonJsHorizontalAlignment.Center,
                    VerticalAlignment = GrBabylonJsVerticalAlignment.Center,
                }
            );
                
            var curvaturePlotImage = 
                signalAnalyzer.GetCurvaturesPlotImage(frameIndex, PlotSampleCount); 
                //ImageSet["gui", $"CurvaturePlot-{frameIndex:D6}"];

            uiPanel2.AddGuiImage(
                "curvaturePlotGuiImage",
                curvaturePlotImage.PngToHtmlDataUrlBase64(),
                new GrBabylonJsGuiImageProperties
                {
                    Stretch = GrBabylonJsImageStretch.Uniform,
                    //Alpha = 0.5d,
                    WidthInPixels = uiPanel2Width - 20,
                    HeightInPixels = (uiPanel2Width - 20) * curvaturePlotImage.HeightToWidth(),
                    PaddingLeftInPixels = 0,
                    PaddingTopInPixels = 0,
                    PaddingBottomInPixels = 0,
                    PaddingRightInPixels = 0,
                    HorizontalAlignment = GrBabylonJsHorizontalAlignment.Center,
                    VerticalAlignment = GrBabylonJsVerticalAlignment.Center,
                }
            );

            var frequencyHzPlotImage = 
                signalAnalyzer.GetFrequencyHzPlotImage("Frequency", frameIndex, PlotSampleCount);
                //ImageSet["gui", $"FrequencyHzPlot-{frameIndex:D6}"];
            
            uiPanel2.AddGuiImage(
                "frequencyHzPlotGuiImage",
                frequencyHzPlotImage.PngToHtmlDataUrlBase64(),
                new GrBabylonJsGuiImageProperties
                {
                    Stretch = GrBabylonJsImageStretch.Uniform,
                    //Alpha = 0.5d,
                    WidthInPixels = uiPanel2Width - 20,
                    HeightInPixels = (uiPanel2Width - 20) * frequencyHzPlotImage.HeightToWidth(),
                    PaddingLeftInPixels = 0,
                    PaddingTopInPixels = 0,
                    PaddingBottomInPixels = 0,
                    PaddingRightInPixels = 0,
                    HorizontalAlignment = GrBabylonJsHorizontalAlignment.Center,
                    VerticalAlignment = GrBabylonJsVerticalAlignment.Center,
                }
            );

            var signalTextImage = RenderSignalTextLaTeXImage(frameIndex); // ImageSet["latex", $"signalText-{frameIndex:D6}"];
            var signalTextImageWidth = uiPanel2Width; // * signalTextImageData.ImageWidth / SignalTextImageMaxWidth;

            uiPanel2.AddGuiImage(
                "latexGuiImage2",
                signalTextImage.PngToHtmlDataUrlBase64(),
                new GrBabylonJsGuiImageProperties
                {
                    //Alpha = 0.5d,
                    WidthInPixels = signalTextImageWidth,
                    HeightInPixels = signalTextImageWidth * signalTextImage.HeightToWidth(),
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
            var copyrightImage = ImageSet["gui", "Copyright"];
            var copyrightImageWidth = 0.4d * CodeFilesComposer.CanvasWidth;
            var copyrightImageHeight = 0.4d * CodeFilesComposer.CanvasWidth * copyrightImage.ImageHeightToWidth;

            uiTexture.AddGuiImage(
                "copyrightImage",
                copyrightImage.GetImageDataUrlBase64(),
                new GrBabylonJsGuiImageProperties
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

    private void AddPhaseVector1(LinFloat64Vector3D x)
    {
        MainSceneComposer.AddVector(
            "v1Vector",
            LinFloat64Vector3D.Zero,
            x,
            Color.Red,
            0.05
        ).AddLaTeXText(
            "v1VectorText",
            ImageSet["latex", "v1VectorText"],
            x + x.ToUnitLinVector3D() * 0.25d + (LinFloat64Vector3D.E2 + LinFloat64Vector3D.E3) * 0.25d / 2d.Sqrt(),
            LaTeXScalingFactor
        );

        MainSceneComposer.AddLineSegment(
            "v1Trail",
            LinFloat64Vector3D.Create(Signal.VectorBounds.MinX, 0, 0),
            LinFloat64Vector3D.Create(Signal.VectorBounds.MaxX, 0, 0),
            Color.Red.SetAlpha(0.25f),
            0.045
        );
    }

    private void AddPhaseVector2(LinFloat64Vector3D y)
    {
        MainSceneComposer.AddVector(
            "v2Vector", 
            LinFloat64Vector3D.Zero, 
            y,
            Color.Green,
            0.05
        ).AddLaTeXText(
            "v2VectorText",
            ImageSet["latex", "v2VectorText"],
            y + y.ToUnitLinVector3D() * 0.25d + (LinFloat64Vector3D.E1 + LinFloat64Vector3D.E3) * 0.25d / 2d.Sqrt(),
            LaTeXScalingFactor
        );
            
        MainSceneComposer.AddLineSegment(
            "v2TrailSegment",
            LinFloat64Vector3D.Create(0, Signal.VectorBounds.MinY, 0),
            LinFloat64Vector3D.Create(0, Signal.VectorBounds.MaxY, 0),
            Color.Green.SetAlpha(0.25f),
            0.045
        );
    }

    private void AddPhaseVector3(LinFloat64Vector3D z)
    {
        MainSceneComposer.AddVector(
            "v3Vector", 
            LinFloat64Vector3D.Zero, 
            z,
            Color.Blue,
            0.05
        ).AddLaTeXText(
            "v3VectorText",
            ImageSet["latex", "v3VectorText"],
            z + z.ToUnitLinVector3D() * 0.25d + (LinFloat64Vector3D.E1 + LinFloat64Vector3D.E2) * 0.25d / 2d.Sqrt(),
            LaTeXScalingFactor
        );
    }

    private void AddSignalVector(LinFloat64Vector3D x, LinFloat64Vector3D y, LinFloat64Vector3D z)
    {
        var v = x + y + z;
        
        MainSceneComposer.AddVector(
            "vVector",
            LinFloat64Vector3D.Zero,
            v,
            Color.DarkOrange,
            0.05
        ).AddLaTeXText(
            "vVectorText",
            ImageSet["latex", "vVectorText"],
            v + v.ToUnitLinVector3D() * 0.25d,
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
    //        GrVisualVector3D.Create("e1Vector")
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

    private void AddSignalPlane(ILinFloat64Vector3D k)
    {
        var scene = MainSceneComposer.SceneObject;

        scene.AddStandardMaterial(
            "discMaterial",

            new GrBabylonJsStandardMaterialProperties(Color.BurlyWood)
            {
                Alpha = 0.55,
                TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend,
                BackFaceCulling = true
            }
        );

        var disc = GrVisualCircleSurface3D.CreateStatic(
            "disc",
            scene.GetMaterial("discMaterial").CreateThickSurfaceStyle(0.025),
            LinFloat64Vector3D.Zero,
            k,
            Signal.ScalarBounds.MaxValue * 1.5d, //Math.Sqrt(3d / 2d)
            false
        );

        var ring = GrVisualCircleRingSurface3D.Create(
            "ring",
            scene.GetMaterial("discMaterial").CreateThickSurfaceStyle(0.025),
            LinFloat64Vector3D.Zero,
            k,
            Signal.ScalarBounds.MaxValue * Math.Sqrt(3d / 2d) - 0.5d,
            Signal.ScalarBounds.MaxValue * Math.Sqrt(3d / 2d) + 0.5d,
            Float64SamplingSpecs.Static
        );

        MainSceneComposer.AddElement(ring);
    }

    private void AddSignalCurve(int frameIndex)
    {
        if (frameIndex < 2 || Signal.FrameList is null) 
            return;

        var scene = MainSceneComposer.SceneObject;

        //var material = scene.AddStandardMaterial(
        //    "curveMaterial",
        //    Color.Yellow
        //);

        //var length = 
        //    sampledCurve.Take(index + 1).ComputeCurveLength();

        var textureImage = GrVisualImageUtils.Create(
            1, 
            256,
            (i, j) => Color.Yellow.SetAlpha(1f - j / 255f)
        ).PngToHtmlDataUrlBase64();

        var texture = scene.AddTexture(
            "curveMaterialTexture",
            textureImage,
            new GrBabylonJsTextureProperties
            {
                HasAlpha = true
            }
        );

        var material = scene.AddStandardMaterial(
            "curveMaterial",
            new GrBabylonJsStandardMaterialProperties
            {
                //DiffuseColor = Color.Yellow,
                TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend,
                DiffuseTexture = texture.ConstName,
                UseAlphaFromDiffuseTexture = true,
                BackFaceCulling = true
            }
        );


        var trailStartIndex = Math.Max(0, frameIndex - TrailSampleCount);
        var trailLength = frameIndex - trailStartIndex;

        var pointList = 
            Signal
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

    private void AddSignalFrame(int frameIndex)
    {
        if (Signal.FrameList is null || Signal.CurvatureList is null || Signal.SampledCurve is null)
            return;

        var lineArrayList = new List<LinFloat64Vector3D[]>(Signal.SampledCurve.CornerCount);
        var colorArrayList = new List<Color[]>(Signal.SampledCurve.CornerCount);

        var xColor1 = Color.Red.SetAlpha(0.8f); //Color.Red.SetAlpha(128);
        var xColor2 = Color.Red.SetAlpha(0.0f);
        var yColor1 = Color.Green.SetAlpha(0.8f);
        var yColor2 = Color.Green.SetAlpha(0.0f);
        var zColor1 = Color.Blue.SetAlpha(0.8f);
        var zColor2 = Color.Blue.SetAlpha(0.0f);

        const double length = 0.8d;

        var trailStartIndex = Math.Max(0, frameIndex - TrailSampleCount);
        var trailLength = frameIndex - trailStartIndex;

        var frameList = 
            Signal
                .FrameList
                .Skip(trailStartIndex)
                .Take(trailLength)
                .ToImmutableArray();

        var i = trailStartIndex - 1;
        foreach (var frame in frameList)
        {
            if (i % FrameSeparationCount == 0)
            {
                MainSceneComposer.AddAxes(
                    $"curveFrameLines{i}",
                    frame.Origin,
                    LinBasisVectorPair3D.PxPy.VectorPairToVectorPairRotationQuaternion(
                        frame.Direction1.ToUnitLinVector3D(),
                        frame.Direction2.RejectOnVector(frame.Direction1).ToUnitLinVector3D()
                    ),
                    0.5
                );


                //var origin = frame.Origin;
                //var xPoint = origin + length * frame.Direction1.SetLength(0.6);
                //var yPoint = origin + length * frame.Direction2.SetLength(0.6);
                //var zPoint = origin + length * frame.Direction3.SetLength(0.6);

                //lineArrayList.Add(new[] { origin, xPoint });
                //lineArrayList.Add(new[] { origin, yPoint });
                //lineArrayList.Add(new[] { origin, zPoint });

                //colorArrayList.Add(new[] { xColor1, xColor2 });
                //colorArrayList.Add(new[] { yColor1, yColor2 });
                //colorArrayList.Add(new[] { zColor1, zColor2 });
            }

            i++;
        }
        
        

        //MainScene.AddLineSystem(
        //    "curveFrameLines",
        //    new GrBabylonJsLinesSystemOptions
        //    {
        //        Lines = lineArrayList.ToArray(),
        //        Colors = colorArrayList.ToArray()
        //    }
        //);

        var curveFrame = Signal.FrameList[frameIndex];
        MainSceneComposer.AddElement(
            GrVisualFrame3D.Create(
                "curveFrame",
                new GrVisualFrameStyle3D
                {
                    OriginStyle = 
                        MainScene
                            .AddStandardMaterial("curveFrameOriginMaterial", Color.DarkGray)
                            .CreateThickSurfaceStyle(0.075),

                    Direction1Style = 
                        MainScene
                            .AddStandardMaterial("curveFrameDirectionMaterial1", Color.DarkRed)
                            .CreateTubeCurveStyle(0.035),

                    Direction2Style = 
                        MainScene
                            .AddStandardMaterial("curveFrameDirectionMaterial2", Color.DarkGreen)
                            .CreateTubeCurveStyle(0.035),

                    Direction3Style = 
                        MainScene
                            .AddStandardMaterial("curveFrameDirectionMaterial3", Color.DarkBlue)
                            .CreateTubeCurveStyle(0.035)
                },
                curveFrame.Origin,
                curveFrame.Direction1,
                curveFrame.Direction2,
                curveFrame.Direction3,
                Float64SamplingSpecs.Static
            )
        );

        MainSceneComposer.AddLaTeXText(
            "e1VectorText",
            ImageSet["latex", "e1VectorText"],
            curveFrame.Origin + 
            curveFrame.Direction1 + 
            curveFrame.Direction1.ToUnitLinVector3D() * 0.25d,
            LaTeXScalingFactor
        );
        
        MainSceneComposer.AddLaTeXText(
            "e2VectorText",
            ImageSet["latex", "e2VectorText"],
            curveFrame.Origin + 
            curveFrame.Direction2 + 
            curveFrame.Direction2.ToUnitLinVector3D() * 0.25d,
            LaTeXScalingFactor
        );
        
        MainSceneComposer.AddLaTeXText(
            "e3VectorText",
            ImageSet["latex", "e3VectorText"],
            curveFrame.Origin + 
            curveFrame.Direction3 + 
            curveFrame.Direction3.ToUnitLinVector3D() * 0.25d,
            LaTeXScalingFactor
        );

        var (kappa1, kappa2) = Signal.CurvatureList[frameIndex];

        var e1 = curveFrame.Direction1.ToUnitLinVector3D();
        var e2 = curveFrame.Direction2.ToUnitLinVector3D();
        var e3 = curveFrame.Direction3.ToUnitLinVector3D();

        var t = Signal.TimeValues[frameIndex];
        var sDt = Signal.GetTangentNormValue(t);
        var k21Vector = (kappa2 * e3 - kappa1 * e1) / 2;
        var omegaNorm = (kappa1.Square() + kappa2.Square()).Sqrt() / 2;
        
        var radius = sDt / (2 * omegaNorm);
        var center = LinFloat64Vector3D.Zero; //curveFrame.Origin + e2 * radius; 
        var normal = e2.VectorUnitCross(k21Vector);

        MainSceneComposer.AddDisc(
            "curveFrameCircleSurface",
            center,
            normal,
            radius,
            Color.YellowGreen.SetAlpha(0.2f),
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
            ImageSet["latex", "kVectorText"],
            center + normal * 1.25d,
            LaTeXScalingFactor
        );

        var material = MainScene.AddStandardMaterial(
            "curveFrameCircleMaterial",
            Color.Yellow
        );

        MainSceneComposer.AddCircleCurve(
            "curveFrameCircleCurve",
            center,
            normal,
            radius,
            material,
            0.035d
        );
    }

    private void AddSignalFrameBivectors(int frameIndex)
    {
        if (Signal.FrameList is null || Signal.CurvatureList is null || Signal.SampledCurve is null)
            return;

        var scene = CodeFilesComposer.GetScene("omegaScene");
        
        // Display bivector omega = kappa1 e12 + kappa2 e23
        var (kappa1, kappa2) = Signal.CurvatureList[frameIndex];

        var e1 = LinFloat64Vector3D.E1;
        var e2 = LinFloat64Vector3D.E2;
        var e3 = LinFloat64Vector3D.E3;

        var e1Ds = kappa1 * e2;
        var e2Ds = kappa2 * e3 - kappa1 * e1;
        var e3Ds = -kappa2 * e2;
        
        OmegaSceneComposer.AddElement(
            GrVisualFrame3D.Create(
                "curveFrame",
                new GrVisualFrameStyle3D
                {
                    OriginStyle = 
                        scene
                            .AddStandardMaterial("curveFrameOriginMaterial", Color.DarkGray)
                            .CreateThickSurfaceStyle(0.075),

                    Direction1Style = 
                        scene
                            .AddStandardMaterial("curveFrameDirectionMaterial1", Color.DarkRed)
                            .CreateTubeCurveStyle(0.035),

                    Direction2Style = 
                        scene
                            .AddStandardMaterial("curveFrameDirectionMaterial2", Color.DarkGreen)
                            .CreateTubeCurveStyle(0.035),

                    Direction3Style = 
                        scene
                            .AddStandardMaterial("curveFrameDirectionMaterial3", Color.DarkBlue)
                            .CreateTubeCurveStyle(0.035)
                },
                OmegaFrameOrigin,
                e1,
                e2,
                e3,
                Float64SamplingSpecs.Static
            )
        );

        OmegaSceneComposer.AddLaTeXText(
            "e1VectorText",
            ImageSet["latex", "e1VectorText"],
            OmegaFrameOrigin + e1 * 1.25d,
            LaTeXScalingFactor
        ).AddLaTeXText(
            "e2VectorText",
            ImageSet["latex", "e2VectorText"],
            OmegaFrameOrigin + e2 * 1.25d,
            LaTeXScalingFactor
        ).AddLaTeXText(
            "e3VectorText",
            ImageSet["latex", "e3VectorText"],
            OmegaFrameOrigin + e3 * 1.25d,
            LaTeXScalingFactor
        );

        if (e1Ds.VectorENorm() > 0.1)
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
                ImageSet["latex", "e1DsVectorText"],
                OmegaFrameOrigin + e1Ds + e1Ds.ToUnitLinVector3D() * 0.25d,
                LaTeXScalingFactor
            );

            var curveFrameOmega1BivectorMaterial = OmegaSceneComposer.SceneObject.AddStandardMaterial(
                "curveFrameOmega1BivectorMaterial",
                new GrBabylonJsStandardMaterialProperties(System.Drawing.Color.IndianRed)
                {
                    Alpha = 0.15,
                    TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend,
                    BackFaceCulling = false
                }
            );

            OmegaSceneComposer.AddParallelogram(
                "omega1Bivector",
                OmegaFrameOrigin,
                e1,
                e1Ds,
                curveFrameOmega1BivectorMaterial
            );
            
            OmegaSceneComposer.AddLaTeXText(
                "omega1BivectorText",
                ImageSet["latex", "omega1BivectorText"],
                OmegaFrameOrigin + 0.75d * (e1 + e1Ds),
                LaTeXScalingFactor
            );
        }

        if (e2Ds.VectorENorm() > 0.1)
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
                ImageSet["latex", "e2DsVectorText"],
                OmegaFrameOrigin + e2Ds + e2Ds.ToUnitLinVector3D() * 0.25d,
                LaTeXScalingFactor
            );

            var curveFrameOmegaBivectorMaterial = OmegaSceneComposer.SceneObject.AddStandardMaterial(
                "curveFrameOmegaBivectorMaterial",
                new GrBabylonJsStandardMaterialProperties(System.Drawing.Color.LimeGreen)
                {
                    Alpha = 0.15,
                    TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend,
                    BackFaceCulling = false
                }
            );

            OmegaSceneComposer.AddParallelogram(
                "omegaBivector",
                OmegaFrameOrigin,
                e2,
                e2Ds,
                curveFrameOmegaBivectorMaterial
            );
            
            OmegaSceneComposer.AddLaTeXText(
                "omega2BivectorText",
                ImageSet["latex", "omega2BivectorText"],
                OmegaFrameOrigin + 0.75 * (e2 + e2Ds),
                LaTeXScalingFactor
            );
        }

        if (e3Ds.VectorENorm() > 0.1)
        {
            var e3DsVectorMaterial = OmegaSceneComposer.SceneObject.AddStandardMaterial(
                "e3DsVectorMaterial",
                new GrBabylonJsStandardMaterialProperties(System.Drawing.Color.DodgerBlue)
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
                ImageSet["latex", "e3DsVectorText"],
                OmegaFrameOrigin + e3Ds + e3Ds.ToUnitLinVector3D() * 0.25d,
                LaTeXScalingFactor
            );

            var curveFrameOmega3BivectorMaterial = OmegaSceneComposer.SceneObject.AddStandardMaterial(
                "curveFrameOmega3BivectorMaterial",
                new GrBabylonJsStandardMaterialProperties(System.Drawing.Color.DodgerBlue)
                {
                    Alpha = 0.15,
                    TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend,
                    BackFaceCulling = false
                }
            );

            OmegaSceneComposer.AddParallelogram(
                "omega3Bivector",
                OmegaFrameOrigin,
                e3,
                e3Ds,
                curveFrameOmega3BivectorMaterial
            );
            
            OmegaSceneComposer.AddLaTeXText(
                "omega3BivectorText",
                ImageSet["latex", "omega3BivectorText"],
                OmegaFrameOrigin + 0.75d * (e3 + e3Ds),
                LaTeXScalingFactor
            );
        }
    }

    protected override void ComposeScene(int frameIndex)
    {
        if (ShowGrid)
            MainSceneComposer.AddGrid(
                "defaultZx",
                LinFloat64Vector3D.Zero, 
                LinFloat64Quaternion.XyToZx, 
                GridUnitCount,
                1,
                0.25
            );

        if (ShowAxes)
            MainSceneComposer.AddAxes(
                "defaultAxes",
                AxesOrigin,
                LinFloat64Quaternion.Identity,
                1,
                1
            );

        var t = Signal.TimeValues[frameIndex];
        var (x, y, z) = Signal.GetComponentVectors(t);
        
        AddPhaseVector1(x);
        AddPhaseVector2(y);
        AddPhaseVector3(z);
        AddSignalVector(x, y, z);
        //AddSignalNormal(frame.Direction3);
        //AddSignalPlane(frame.Direction3);
        AddSignalCurve(frameIndex);
        AddSignalFrame(frameIndex);
        AddSignalFrameBivectors(frameIndex);
    }
}