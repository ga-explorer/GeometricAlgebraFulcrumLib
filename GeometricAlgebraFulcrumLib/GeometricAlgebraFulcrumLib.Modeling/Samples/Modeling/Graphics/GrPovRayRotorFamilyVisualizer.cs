using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Finishes;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Patterns;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Grids;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using GeometricAlgebraFulcrumLib.Utilities.Web.Images;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.KaTeX;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Graphics;

public class GrPovRayRotorFamilyVisualizer :
    GrPovRaySceneSequenceComposer
{
    public LinFloat64Vector3D SourceVector { get; }

    public LinFloat64Vector3D TargetVector { get; }

    public TemporalFloat64Scalar ThetaValues 
        => TemporalScalarSet["theta"];
    
    public bool DrawRotorTrace { get; init; }

    public GrPovRayFinish DefaultMaterialFinish { get; set; }
        = GrPovRayFinish.Shiny;


    public GrPovRayRotorFamilyVisualizer(string workingFolder, Float64SamplingSpecs samplingSpecs, ILinFloat64Vector3D sourceVector, ILinFloat64Vector3D targetVector) 
        : base(workingFolder, samplingSpecs)
    {
        SourceVector = sourceVector.ToUnitLinVector3D();
        TargetVector = targetVector.ToUnitLinVector3D();
    }


    public LinFloat64Angle GetPhiMin()
    {
        return GetPhiMinCos().CosToPolarAngle();
    }
    
    public double GetPhiMinCos()
    {
        return SourceVector.GetUnitVectorsAngleCos(TargetVector);
    }

    public LinFloat64Angle ThetaToPhi(LinFloat64Angle theta)
    {
        return ThetaToPhiCos(theta).CosToPolarAngle();
    }

    public LinFloat64Vector3D ThetaToUnitNormal(LinFloat64Angle theta)
    {
        var n = 
            SourceVector.VectorUnitCross(TargetVector);

        return n.RotateVectorUsingAxisAngle(
            (TargetVector - SourceVector).ToUnitLinVector3D(),
            theta
        );
    }

    public double ThetaToPhiCos(LinFloat64Angle theta)
    {
        if (!theta.DegreesValue.IsNearInRange(-90, 90))
            throw new ArgumentOutOfRangeException(nameof(theta));

        var phiMinCos = GetPhiMinCos();

        return 1d + (2d * (1 - phiMinCos)) / (theta.Sin().Square() * (1 + phiMinCos) - 2d);
    }

    public LinFloat64Quaternion ThetaToQuaternion(LinFloat64Angle theta)
    {
        var phi = ThetaToPhi(theta);
        var normal = ThetaToUnitNormal(theta);

        return normal.RotationAxisAngleToQuaternion(phi);
    }

    public LinFloat64Angle PhiToTheta(LinFloat64Angle phi)
    {
        return PhiToThetaSin(phi).SinToPolarAngle();
    }

    public double PhiToThetaSin(LinFloat64Angle phi)
    {
        var phiMinCos = GetPhiMinCos();

        if (phi.RadiansValue < phiMinCos.ArcCos() || phi.RadiansValue > Math.PI)
            throw new ArgumentOutOfRangeException(nameof(phi));

        var phiCos = phi.Cos();

        return (2d * (phiCos - phiMinCos) / ((1 + phiMinCos) * (phiCos - 1))).Sqrt();
    }
    
    public LinFloat64Vector3D PhiToUnitNormal(LinFloat64Angle phi)
    {
        return ThetaToUnitNormal(PhiToTheta(phi));
    }
    
    public LinFloat64Quaternion PhiToQuaternion(LinFloat64Angle phi)
    {
        var normal = PhiToUnitNormal(phi);

        return normal.RotationAxisAngleToQuaternion(phi);
    }

    public LinFloat64Angle GetThetaAtFrame(int frameIndex)
    {
        return TemporalScalarSet
            .GetScalarAtFrame("theta", frameIndex)
            .RadiansToDirectedAngle();
    }

    private Image GetThetaPhiPlotImage(int frameIndex)
    {
        const int width = 1024;
        const int height = 768;
        const float resolution = 150f;
        
        var model = new PlotModel
        {
            Title = "Rotation Angle",
            DefaultFont = "Georgia",
            Background = OxyColors.Transparent,
            //PlotMargins = new OxyThickness(0, 0, 0, 0),
            Padding = new OxyThickness(0, 0, 0, 0)
        };

        model.Axes.Add(
            new LinearAxis
            {
                Minimum = -90,
                Maximum = 90,
                Position = AxisPosition.Bottom
            }
        );
        
        model.Axes.Add(
            new LinearAxis
            {
                Minimum = 0,
                Maximum = 185,
                Position = AxisPosition.Left
            }
        );

        model.Series.Add(
            new FunctionSeries(theta => ThetaToPhi(theta.DegreesToDirectedAngle()).DegreesValue, -90, 90, 180d / 450)
            {
                Color = System.Drawing.Color.DarkBlue.ToOxyColor(),
                CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );

        var theta = GetThetaAtFrame(frameIndex);
        var phi = ThetaToPhi(theta);
        model.Series.Add(
            new ScatterSeries
            {
                MarkerSize = 7,
                MarkerStroke = System.Drawing.Color.DarkRed.ToOxyColor(),
                MarkerFill = System.Drawing.Color.DarkRed.ToOxyColor(192),
                MarkerStrokeThickness = 1,
                MarkerType = MarkerType.Circle,
                Points =
                {
                    new ScatterPoint(theta.DegreesValue, phi.DegreesValue)
                }
            }
        );

        var pngExporter = new PngExporter
        {
            Width = width, 
            Height = height, 
            Dpi = resolution
        };

        using var stream = new MemoryStream();
        pngExporter.Export(model, stream);
        stream.Position = 0;

        return Image.Load(stream);
    }
    

    protected override void InitializeTemporalValues()
    {
        Console.Write("Generating temporal values .. ");

        var thetaLimit = 90d.DegreesToRadians() - 5e-5d;
        TemporalScalarSet.SetScalar(
            "theta", 
            TemporalFloat64Scalar
                .FullCos()
                .Repeat(
                    1,
                    0, 
                    1,
                    -thetaLimit, 
                    thetaLimit
                )
        );

        TemporalScalarSet.SetScalar(
            "sourceVector.color.alpha",
            TemporalFloat64Scalar
                .SmoothRectangle()
                .Repeat(10, 0, 1, 0, 1)
        );
        
        TemporalScalarSet.SetScalar(
            "targetVector.color.alpha",
            TemporalFloat64Scalar
                .SmoothRectangle()
                .Repeat(10, 0, 1, 0, 1)
        );

        Console.WriteLine("done.");
        Console.WriteLine();
    }

    private WclKaTeXComposer RenderLaTeXTextures()
    {
        Console.Write("Generating LaTeX images .. ");

        var katexComposer = new WclKaTeXComposer(WorkingFolder)
        {
            FontSizeEm = 2,
            Output = WclKaTeXComposer.OutputKind.Html,
            ThrowOnError = false,
            SaveImages = false
        };

        katexComposer.AddLaTeXEquation(
            "sourceVectorText",
            @"\boldsymbol{u}"
        );

        katexComposer.AddLaTeXEquation(
            "targetVectorText",
            @"\boldsymbol{v}"
        );

        katexComposer.AddLaTeXEquation(
            "sourceVector1Text",
            @"\boldsymbol{u}\left(\theta\right)"
        );

        katexComposer.AddLaTeXEquation(
            "targetVector1Text",
            @"\boldsymbol{v}\left(\theta\right)"
        );

        katexComposer.AddLaTeXEquation(
            "phiMinRotorText",
            @"\boldsymbol{R}\left(0\right)"
        );

        katexComposer.AddLaTeXEquation(
            "thetaRotorText",
            @"\boldsymbol{S}\left(\theta\right)"
        );

        katexComposer.AddLaTeXEquation(
            "phiRotorText",
            @"\boldsymbol{R}\left(\theta\right)"
        );

        katexComposer.AddLaTeXEquation(
            "phiRotorCopyText",
            @"\boldsymbol{R}\left(\theta\right)"
        );

        katexComposer.AddLaTeXEquation(
            "thetaRotorPlaneText",
            @"\boldsymbol{B}"
        );

        katexComposer.AddLaTeXEquation(
            "phiRotorPlaneText",
            @"\boldsymbol{N}\left(\theta\right)"
        );

        //ImageCache.MarginSize = 20;
        ////ImageCache.BackgroundColor = Color.FromRgba(32, 32, 255, 16);

        var phiMin = GetPhiMin();
        var vuDiff = (TargetVector - SourceVector).ToUnitLinVector3D();
        var uvNormal = SourceVector.VectorUnitCross(TargetVector);

        //katexComposer.AddLaTeXAlignedEquations(
        //    "modelText",
        //    new Pair<string>[]
        //    {
        //        new (@"\boldsymbol{u}", @$"\left( {SourceVector.X:F4}, {SourceVector.Y:F4}, {SourceVector.Z:F4} \right)"),
        //        new (@"\boldsymbol{v}", @$"\left( {TargetVector.X:F4}, {TargetVector.Y:F4}, {TargetVector.Z:F4} \right)"),
        //        new (@"\phi", @$"\cos^{{-1}}\left(\boldsymbol{{u}}\cdot\boldsymbol{{v}}\right) = {phiMin.DegreesValue:F4}"),
        //        new (@"\boldsymbol{B}", @"\left(\boldsymbol{v}-\boldsymbol{u}\right)\rfloor\boldsymbol{e}^{-1}_{123}"),
        //        new (@"\boldsymbol{\hat{B}}", @$"\frac{{\boldsymbol{{B}}}}{{\sqrt{{-\boldsymbol{{B}}^{{2}}}}}}"),
        //        new (@"", @$"\left( {vuDiff.X:F4}, {vuDiff.Y:F4}, {vuDiff.Z:F4} \right)^{{*}}"),
        //        new (@"\boldsymbol{S}\left( \theta \right)", @"\exp\left(\frac{1}{2}\theta\hat{\boldsymbol{B}}\right)"),
        //        new (@"\boldsymbol{N}\left( \theta \right)", @"\boldsymbol{S}\left(\theta\right)\left(\boldsymbol{v}\wedge\boldsymbol{u}\right)\boldsymbol{S}^{\sim}\left(\theta\right)"),
        //        new (@"\boldsymbol{\hat{N}}\left( \theta \right)", @$"\frac{{\boldsymbol{{N}}\left( \theta \right)}}{{\sqrt{{-\boldsymbol{{N}}^{{2}}\left( \theta \right)}}}}"),
        //        new (@"\boldsymbol{u}\left( \theta \right)", @"\left(\boldsymbol{u}\rfloor\boldsymbol{N}\left(\theta\right)\right)\rfloor\boldsymbol{N}^{-1}\left(\theta\right)"),
        //        new (@"\boldsymbol{v}\left( \theta \right)", @"\left(\boldsymbol{v}\rfloor\boldsymbol{N}\left(\theta\right)\right)\rfloor\boldsymbol{N}^{-1}\left(\theta\right)"),
        //        new (@"\boldsymbol{u}\left( \theta \right) \cdot \boldsymbol{v}\left( \theta \right)", @"1+\frac{2\left(1-\boldsymbol{u}\cdot\boldsymbol{v}\right)}{\sin^{2}\left(\theta\right)\left(1+\boldsymbol{u}\cdot\boldsymbol{v}\right)-2}"),
        //        new (@"\varphi\left( \theta \right)", @"\cos^{-1}\left(\boldsymbol{u}\left( \theta \right) \cdot \boldsymbol{v}\left( \theta \right) \right)"),
        //        new (@"\boldsymbol{R}\left( \theta \right)", @"\exp\left(\frac{1}{2}\varphi\left(\theta\right)\hat{\boldsymbol{N}}\left(\theta\right)\right)")
        //    }
        //);

        //for (var frameIndex = 0; frameIndex < FrameCount; frameIndex++)
        //{
        //    var theta = GetThetaAtFrame(frameIndex);
        //    var phi = ThetaToPhi(theta);
        //    var uvNormal1 = uvNormal.RotateVectorUsingAxisAngle(vuDiff, theta);
        //    var u1 = SourceVector.RejectOnVector(uvNormal1);
        //    var v1 = TargetVector.RejectOnVector(uvNormal1);

        //    katexComposer.AddLaTeXAlignedEquations(
        //        $"dataText-{frameIndex:D6}",
        //        new Pair<string>[]
        //        {
        //            new (@"\theta", @$"{theta.DegreesValue:F4}"),
        //            new (@"\boldsymbol{\hat{N}}\left( \theta \right)", @$"\left( {uvNormal1.X:F4}, {uvNormal1.Y:F4}, {uvNormal1.Z:F4} \right)^{{*}}"),
        //            new (@"\boldsymbol{u}\left( \theta \right)", @$"\left( {u1.X:F4}, {u1.Y:F4}, {u1.Z:F4} \right)"),
        //            new (@"\boldsymbol{v}\left( \theta \right)", @$"\left( {v1.X:F4}, {v1.Y:F4}, {v1.Z:F4} \right)"),
        //            new (@"\varphi\left( \theta \right)", @$"{phi.DegreesValue:F4}")
        //        }
        //    );
        //}

        katexComposer.RenderKaTeX();
        
        Console.WriteLine("done.");
        Console.WriteLine();

        return katexComposer;
    }

    protected override void InitializeTextureSet()
    {
        Console.Write("Generating images cache .. ");

        //ImageCache.MarginSize = 0;
        //ImageCache.BackgroundColor = Color.FromRgba(255, 255, 255, 0);

        //if (ShowCopyright)
        //{
        //    ImageSet.AddImageFromPngFile("images", "Copyright");

        //    //ImageCache.AddPngFromFile(
        //    //    "copyright",
        //    //    workingPath.GetFilePath("Copyright.png")
        //    //);
        //}

        //ImageSet.AddImages(
        //    "ThetaPhiPlot",
        //    FrameCount.MapRange(i => 
        //        new KeyValuePair<string, Image>(
        //            $"ThetaPhiPlot-{i:D6}", 
        //            GetThetaPhiPlotImage(i)
        //        )
        //    )
        //);

        ////for (var i = 0; i < FrameCount; i++)
        ////{
        ////    ImageCache.AddPng(
        ////        $"ThetaPhiPlot-{i:D6}",
        ////        GetThetaPhiPlotImage(i)
        ////    );
        ////}

        TextureSet.AddTextures(
            "latex", 
            RenderLaTeXTextures()
        );

        TextureSet.FinalizeTextures();

        Console.WriteLine("done.");
        Console.WriteLine();
    }

    protected override void InitializeSceneComposers(int frameIndex)
    {
        //ActiveSceneComposer = new GrPovRaySceneComposer(
        //    "mainScene",
        //    new GrPovRaySnapshotSpecs
        //    {
        //        Enabled = true,
        //        DefaultSceneOptions = new GrPovRayRenderingOptions()
        //        {
        //            Width = CanvasWidth,
        //            Height = CanvasHeight,
                    
        //        }
        //        Precision = 1,
        //        UsePrecision = true,
        //        Delay = index == 0 ? 2000 : 1000,
        //        FileName = $"Frame-{index:D6}.png"
        //    }
        //)
        //{
        //    BackgroundColor = Color.AliceBlue,
        //    ShowDebugLayer = false
        //};
    }
    

    protected override void AddGrid()
    {
        // Add ground coordinates grid
        ActiveSceneComposer.GridMaterialKind =
            GrPovRayGridMaterialKind.TexturedMaterial;
        
        var imageComposer = new GrVisualGridImageComposer()
        {
            BaseSquareColor = Color.LightYellow,
            BaseLineColor = Color.BurlyWood,
            MidLineColor = Color.SandyBrown,
            BorderLineColor = Color.SaddleBrown,
            BaseSquareCount = 4,
            BaseSquareSize = 64,
            BaseLineWidth = 2,
            MidLineWidth = 4,
            BorderLineWidth = 3
        };

        imageComposer.SetGridColorsOpacity(1);

        ActiveSceneComposer.AddSquareGrid(
            GrVisualSquareGrid3D.DefaultZx(
                -5 * LinFloat64Vector3D.E2, 
                GridUnitCount,
                1,
                1
            )
        );
    }

    protected override void AddGuiLayer(int frameIndex)
    {
        //var scene = ActiveSceneComposer.SceneObject;

        //// Add GUI layer
        //var uiTexture = scene.AddGuiFullScreenUi("uiTexture");
        
        //if (ShowCopyright)
        //{
        //    var copyrightImage = ImageCache["copyright"];
        //    var copyrightImageWidth = 0.4d * CodeFilesComposer.CanvasWidth;
        //    var copyrightImageHeight = 0.4d * CodeFilesComposer.CanvasWidth * copyrightImage.HeightToWidthRatio;

        //    uiTexture.AddGuiImage(
        //        "copyrightImage",
        //        copyrightImage.GetUrl(),
        //        new GrPovRayGuiImageProperties
        //        {
        //            Stretch = GrPovRayImageStretch.Uniform,
        //            //Alpha = 0.75d,
        //            WidthInPixels = copyrightImageWidth,
        //            HeightInPixels = copyrightImageHeight,
        //            PaddingLeftInPixels = 10,
        //            PaddingBottomInPixels = 10,
        //            HorizontalAlignment = GrPovRayHorizontalAlignment.Left,
        //            VerticalAlignment = GrPovRayVerticalAlignment.Bottom,
        //        }
        //    );
        //}

        //var uiPanel1Width = CodeFilesComposer.CanvasWidth * 0.275;
        //var uiPanel1 = uiTexture.AddGuiStackPanel(
        //    "uiPanel1",
        //    new GrPovRayGuiStackPanelProperties
        //    {
        //        IsVertical = true,
        //        Spacing = 10,
        //        //Color = Color.Blue,
        //        BackgroundColor = Color.FromRgba(208, 206, 226, 24),
        //        PaddingLeftInPixels = 10,
        //        PaddingTopInPixels = 10,
        //        PaddingBottomInPixels = 10,
        //        PaddingRightInPixels = 10,
        //        WidthInPixels = uiPanel1Width,
        //        HorizontalAlignment = GrPovRayHorizontalAlignment.Left,
        //        VerticalAlignment = GrPovRayVerticalAlignment.Top
        //    }
        //);
            
        //uiPanel1.AddGuiTextBlock(
        //    "uiTextTitle",
        //    $"'{Title}'",
        //    new GrPovRayGuiTextBlockProperties
        //    {
        //        WidthInPixels = uiPanel1Width - 20,
        //        HeightInPixels = 50,
        //        ResizeToFit = true,
        //        FontSize = 36,
        //        FontWeight = "'750'",
        //        FontFamily = "'Georgia,Times,Times New Roman,serif'",
        //        TextHorizontalAlignment = GrPovRayHorizontalAlignment.Left,
        //        Color = Color.Black,
        //        PaddingLeftInPixels = 10,
        //        PaddingTopInPixels = 10,
        //        PaddingBottomInPixels = 10,
        //        PaddingRightInPixels = 10,
        //        HorizontalAlignment = GrPovRayHorizontalAlignment.Left,
        //        VerticalAlignment = GrPovRayVerticalAlignment.Center,
        //    }
        //);
            
        //uiPanel1.AddGuiLine(
        //    "uiLine",
        //    new GrPovRayGuiLineProperties
        //    {
        //        X1 = 10,
        //        X2 = uiPanel1Width - 10,
        //        Y1 = 70,
        //        Y2 = 70,
        //        Color = Color.Black,
        //        LineWidth = 3
        //    }
        //);

        //var latexPngData1 = ImageCache["modelText"];
        //uiPanel1.AddGuiImage(
        //    "latexGuiImage1",
        //    latexPngData1.GetUrl(),
        //    new GrPovRayGuiImageProperties
        //    {
        //        //Alpha = 0.5d,
        //        WidthInPixels = uiPanel1Width - 20,
        //        HeightInPixels = (uiPanel1Width - 20) * latexPngData1.HeightToWidthRatio,
        //        PaddingLeftInPixels = 0,
        //        PaddingTopInPixels = 0,
        //        PaddingBottomInPixels = 0,
        //        PaddingRightInPixels = 0,
        //        HorizontalAlignment = GrPovRayHorizontalAlignment.Left,
        //        VerticalAlignment = GrPovRayVerticalAlignment.Center,
        //        Stretch = GrPovRayImageStretch.Uniform
        //    }
        //);

        //var uiPanel2Width = CodeFilesComposer.CanvasWidth * 0.25;
        //var uiPanel2 = uiTexture.AddGuiStackPanel(
        //    "uiPanel2",
        //    new GrPovRayGuiStackPanelProperties
        //    {
        //        IsVertical = true,
        //        Spacing = 10,
        //        //Color = Color.Blue,
        //        BackgroundColor = Color.FromRgba(208, 206, 226, 24),
        //        PaddingLeftInPixels = 10,
        //        PaddingTopInPixels = 10,
        //        PaddingBottomInPixels = 10,
        //        PaddingRightInPixels = 10,
        //        WidthInPixels = uiPanel2Width,
        //        HorizontalAlignment = GrPovRayHorizontalAlignment.Right,
        //        VerticalAlignment = GrPovRayVerticalAlignment.Top
        //    }
        //);

        //var thetaPhiPlotData = ImageCache[$"ThetaPhiPlot-{index:D6}"];
        //uiPanel2.AddGuiImage(
        //    "thetaPhiPlotGuiImage",
        //    thetaPhiPlotData.GetUrl(),
        //    new GrPovRayGuiImageProperties
        //    {
        //        Stretch = GrPovRayImageStretch.Uniform,
        //        //Alpha = 0.5d,
        //        WidthInPixels = uiPanel2Width - 20,
        //        HeightInPixels = (uiPanel2Width - 20) * thetaPhiPlotData.HeightToWidthRatio,
        //        PaddingLeftInPixels = 0,
        //        PaddingTopInPixels = 0,
        //        PaddingBottomInPixels = 0,
        //        PaddingRightInPixels = 0,
        //        HorizontalAlignment = GrPovRayHorizontalAlignment.Center,
        //        VerticalAlignment = GrPovRayVerticalAlignment.Center,
        //    }
        //);

        //var dataTextImageData = ImageCache[$"dataText-{index:D6}"];
        //var dataTextImageWidth = uiPanel2Width * dataTextImageData.Width / DataTextImageMaxWidth;

        //uiPanel2.AddGuiImage(
        //    "latexGuiImage2",
        //    dataTextImageData.GetUrl(),
        //    new GrPovRayGuiImageProperties
        //    {
        //        //Alpha = 0.5d,
        //        WidthInPixels = dataTextImageWidth,
        //        HeightInPixels = dataTextImageWidth * dataTextImageData.HeightToWidthRatio,
        //        PaddingLeftInPixels = 5,
        //        PaddingTopInPixels = 0,
        //        PaddingBottomInPixels = 0,
        //        PaddingRightInPixels = 5,
        //        HorizontalAlignment = GrPovRayHorizontalAlignment.Left,
        //        VerticalAlignment = GrPovRayVerticalAlignment.Top
        //    }
        //);
    }

    private void AddInputVectors(int frameIndex)
    {
        var sourceVectorColorAlpha = 
            TemporalScalarSet.GetScalarAtFrame("sourceVector.color.alpha", frameIndex);

        var targetVectorColorAlpha = 
            TemporalScalarSet.GetScalarAtFrame("targetVector.color.alpha", frameIndex);

        ActiveSceneComposer.AddVector(
            "sourceVector",
            4 * SourceVector,
            Color.Red
                //.SetAlpha(sourceVectorColorAlpha)
                .ToPovRayMaterial(DefaultMaterialFinish),
            0.075
        ).AddLaTeXText(
            "sourceVectorText", 
            TextureSet["latex", "sourceVectorText"], 
            SourceVector * 4.35d, 
            LaTeXScalingFactor
        );

        ActiveSceneComposer.AddVector(
            "targetVector",
            4 * TargetVector,
            Color.Blue
                //.SetAlpha(targetVectorColorAlpha)
                .ToPovRayMaterial(DefaultMaterialFinish),
            0.075
        ).AddLaTeXText(
            "targetVectorText",
            TextureSet["latex", "targetVectorText"],
            TargetVector * 4.35d,
            LaTeXScalingFactor
        );
    }

    private void AddProjectedVectors(int index)
    {
        var u = SourceVector * 4;
        var v = TargetVector * 4;
        var uvNormal = u.VectorUnitCross(v);
        var thetaPlaneNormal = (v - u).ToUnitLinVector3D();
        var theta = GetThetaAtFrame(index);
        var uvNormal1 = uvNormal.RotateVectorUsingAxisAngle(thetaPlaneNormal, theta);
        var u1 = u.RejectOnVector(uvNormal1);
        var v1 = v.RejectOnVector(uvNormal1);

        var dashSpecs = new GrVisualDashedLineSpecs(1, 1, 20);

        ActiveSceneComposer.AddLineSegment(
            "sourceSegment",
            u,
            u1,
            Color.Red,
            dashSpecs
        ).AddLineSegment(
            "originSegment",
            u - u1,
            LinFloat64Vector3D.Zero,
            Color.DarkGreen,
            dashSpecs
        ).AddLineSegment(
            "targetSegment",
            v,
            v1,
            Color.Blue, 
            dashSpecs
        );

        ActiveSceneComposer
            .AddVector(
                "sourceVector1", 
                u1, 
                Color.Red.ToPovRayMaterial(DefaultMaterialFinish), 
                0.075
            ).AddLaTeXText(
                "sourceVector1Text", 
                TextureSet["latex", "sourceVector1Text"], 
                u1 + u1.ToUnitLinVector3D() * 0.35d, 
                LaTeXScalingFactor
            );

        ActiveSceneComposer
            .AddVector(
                "targetVector1", 
                v1, 
                Color.Blue.ToPovRayMaterial(DefaultMaterialFinish), 
                0.075
            ).AddLaTeXText(
                "targetVector1Text", 
                TextureSet["latex", "targetVector1Text"], 
                v1 + v1.ToUnitLinVector3D() * 0.35d, 
                LaTeXScalingFactor
            );
    }

    private void AddRotors(int frameIndex)
    {
        var scene = ActiveSceneComposer.SceneObject;

        var u = SourceVector * 4;
        var v = TargetVector * 4;
        var vuSum = (v + u) * 0.5;
        var vuDiff = (v - u).ToUnitLinVector3D();
        var uvNormal = u.VectorUnitCross(v);
        var theta = GetThetaAtFrame(frameIndex);
        var uvNormal1 = uvNormal.RotateVectorUsingAxisAngle(vuDiff, theta);
        var u1 = u.RejectOnVector(uvNormal1);
        var v1 = v.RejectOnVector(uvNormal1);
        var vuSum1 = vuSum.RotateVectorUsingAxisAngle(vuDiff, theta);

        ActiveSceneComposer.AddDiscSector(
            "phiMinRotor",
            LinFloat64Vector3D.Zero,
            u,
            v,
            u1.VectorENorm(),
            true,
            System.Drawing.Color.IndianRed.ToImageSharpColor(128).ToPovRayMaterial(DefaultMaterialFinish),
            0.05,
            System.Drawing.Color.IndianRed.ToImageSharpColor(128).ToPovRayMaterial(DefaultMaterialFinish)
        ).AddLaTeXText(
            "phiMinRotorText",
            TextureSet["latex", "phiMinRotorText"],
            0.5d.Lerp(u, v).SetLength(u1.VectorENorm() + 0.35d),
            LaTeXScalingFactor
        );

        if (!theta.DegreesValue.IsNearZero(1e-5))
        {
            ActiveSceneComposer.AddDiscSector(
                "thetaRotor",
                LinFloat64Vector3D.Zero,
                vuSum,
                vuSum1,
                u1.VectorENorm(),
                true,
                System.Drawing.Color.LightGreen.ToImageSharpColor(128).ToPovRayMaterial(DefaultMaterialFinish),
                0.05,
                System.Drawing.Color.LightGreen.ToImageSharpColor(128).ToPovRayMaterial(DefaultMaterialFinish)
            ).AddLaTeXText(
                "thetaRotorText",
                TextureSet["latex", "thetaRotorText"],
                0.5d.Lerp(vuSum, vuSum1).SetLength(u1.VectorENorm() + 0.35d),
                LaTeXScalingFactor
            );
        }

        ActiveSceneComposer.AddDiscSector(
            "phiRotor",
            LinFloat64Vector3D.Zero,
            u1,
            v1,
            u1.VectorENorm(),
            true,
            System.Drawing.Color.DarkSlateBlue.ToImageSharpColor(128).ToPovRayMaterial(DefaultMaterialFinish),
            0.05,
            System.Drawing.Color.DarkSlateBlue.ToImageSharpColor(128).ToPovRayMaterial(DefaultMaterialFinish)
        ).AddLaTeXText(
            "phiRotorText",
            TextureSet["latex", "phiRotorText"],
            0.5d.Lerp(u1, v1).SetLength(u1.VectorENorm() + 0.35d),
            LaTeXScalingFactor
        );

        ActiveSceneComposer.AddDiscSector(
            "phiRotorCopy",
            u - u1,
            u1,
            v1,
            u1.VectorENorm(),
            true,
            System.Drawing.Color.DarkSlateBlue.ToImageSharpColor(128).ToPovRayMaterial(DefaultMaterialFinish),
            0.05,
            System.Drawing.Color.DarkSlateBlue.ToImageSharpColor(128).ToPovRayMaterial(DefaultMaterialFinish)
        ).AddLaTeXText(
            "phiRotorCopyText",
            TextureSet["latex", "phiRotorCopyText"],
            u - u1 + 0.5d.Lerp(u1, v1).SetLength(u1.VectorENorm() + 0.35d),
            LaTeXScalingFactor
        );

        ActiveSceneComposer.AddDisc(
            "thetaRotorPlane",
            LinFloat64Vector3D.Zero,
            vuDiff,
            5d,
            System.Drawing.Color.YellowGreen.ToImageSharpColor(64).ToPovRayMaterial(DefaultMaterialFinish),
            0.035,
            System.Drawing.Color.YellowGreen.ToImageSharpColor(64)
        ).AddLaTeXText(
            "thetaRotorPlaneText",
            TextureSet["latex", "thetaRotorPlaneText"],
            vuSum.SetLength(-4d),
            LaTeXScalingFactor
        );

        ActiveSceneComposer.AddDisc(
            "phiRotorPlane",
            LinFloat64Vector3D.Zero,
            uvNormal1,
            5d,
            System.Drawing.Color.BurlyWood.ToImageSharpColor(64).ToPovRayMaterial(DefaultMaterialFinish),
            0.035,
            System.Drawing.Color.BurlyWood.ToImageSharpColor(64)
        ).AddLaTeXText(
            "phiRotorPlaneText",
            TextureSet["latex", "phiRotorPlaneText"],
            vuSum1.SetLength(-4d),
            LaTeXScalingFactor
        );
    }

    private void AddRotorTrace()
    {
        var scene = ActiveSceneComposer.SceneObject;

        var thetaDegrees = 
            ThetaValues
                .GetSampledSignal(FrameCount)
                .Select(t => t.RadiansToDegrees())
                .ToImmutableArray();

        var thetaMin = thetaDegrees.Min();
        var thetaMax = thetaDegrees.Max();

        var thetaDegreesArray = 
            thetaMin.GetLinearRange(thetaMax, 31, false).ToImmutableArray();

        var u = SourceVector * 4;
        var v = TargetVector * 4;
        var vuDiff = (v - u).ToUnitLinVector3D();
        var uvNormal = u.VectorUnitCross(v);
        
        for (var i = 0; i < thetaDegreesArray.Length; i++)
        {
            var theta = thetaDegreesArray[i].DegreesToDirectedAngle();
            var uvNormal1 = uvNormal.RotateVectorUsingAxisAngle(vuDiff, theta);
            var u1 = u.RejectOnVector(uvNormal1);
            var v1 = v.RejectOnVector(uvNormal1);

            ActiveSceneComposer.AddCircleCurve(
                $"phiRotorTrace{i}",
                u - u1,
                u1.VectorUnitCross(v1),
                u1.VectorENorm(),
                System.Drawing.Color.DarkSlateBlue.ToImageSharpColor(192).ToPovRayMaterial(DefaultMaterialFinish),
                0.025
            );

            //ActiveSceneComposer.AddCircleCurveArc(
            //    new GrVisualCircleCurveArc3D($"phiRotorTrace{i}")
            //    {
            //        Radius = u1.GetLength(),
            //        Center = u - u1,
            //        Direction1 = u1,
            //        Direction2 = v1,
            //        InnerArc = true,

            //        Style = new GrVisualCurveTubeStyle3D(
            //            material,
            //            0.05
            //        )
            //    }
            //);
        }
    }

    private void AddIntersections(int frameIndex)
    {
        var scene = ActiveSceneComposer.SceneObject;

        var u = SourceVector * 4;
        var v = TargetVector * 4;
        var vuSum = (v + u) * 0.5;
        var vuDiff = (v - u).ToUnitLinVector3D();
        var uvNormal = u.VectorUnitCross(v);
        var theta = GetThetaAtFrame(frameIndex);
        var uvNormal1 = uvNormal.RotateVectorUsingAxisAngle(vuDiff, theta);
        var u1 = u.RejectOnVector(uvNormal1);
        var v1 = v.RejectOnVector(uvNormal1);
        var vuSum1 = vuSum.RotateVectorUsingAxisAngle(vuDiff, theta);

        var intersectionPointColor = Color.DarkSlateGray.ToPovRayMaterial(DefaultMaterialFinish);
        var intersectionLineColor = Color.DarkSlateGray.SetAlpha(0.5f).ToPovRayMaterial(DefaultMaterialFinish);
        
        ActiveSceneComposer.AddPoints(
            i=> $"intersectionPoint{i + 1}", 
            intersectionPointColor, 
            0.08, 
            LinFloat64Vector3D.Zero, 
            vuSum1.SetLength(5d),
            vuSum1.SetLength(-5d),
            u - u1,
            u,
            v,
            u1,
            v1,
            vuSum.SetLength(u1.VectorENorm()),
            vuSum1.SetLength(u1.VectorENorm()),
            u.SetLength(u1.VectorENorm()),
            v.SetLength(u1.VectorENorm())
        );

        ActiveSceneComposer.AddLineSegment(
            "intersectionLine", 
            vuSum1.SetLength(5d), 
            vuSum1.SetLength(-5d),
            intersectionLineColor, 
            0.035
        );

        if ((u - u1).VectorENorm() > 1)
        {
            ActiveSceneComposer.AddRightAngle(
                "rightAngle1",
                u1,
                -u1,
                u - u1,
                0.5,
                Color.Red,
                Color.Red.SetAlpha(0.25f).ToPovRayMaterial(DefaultMaterialFinish)
            );

            ActiveSceneComposer.AddRightAngle(
                "rightAngle2",
                v1,
                -v1,
                v - v1,
                0.5,
                Color.Blue,
                Color.Blue.SetAlpha(0.25f).ToPovRayMaterial(DefaultMaterialFinish)
            );
        }
    }

    private void AddGroundPlane()
    {
        var pigment = GrPovRayPattern.Checker(
            Color.LightGoldenrodYellow,
            Color.LightGoldenrodYellow.ScaleRgbBy(0.75)
        ).ToColorListPigment();

        var finish = new GrPovRayFinish().SetProperties(
            new GrPovRayFinishProperties
            {
                ConserveEnergy = true,
                DiffuseAmount = 0.2,
                AmbientColor = 0,
                SpecularHighlightAmount = 0.7,
                Metallic = true,
                ReflectionColor = Color.AliceBlue,
                Reflection = new GrPovRayFinishReflectionProperties(Color.BlueViolet)
                {
                    Metallic = 1
                }
            }
        );

        ActiveSceneObject.AddStatement(
            GrPovRayObject
                .Box(GridUnitCount, 0.075, GridUnitCount)
                .Translate(AxesOrigin - 0.075 / 2 * LinFloat64Vector3D.E2)
                .SetMaterial(pigment, finish)
        );

        //ActiveSceneComposer.AddDisc(
        //    GrVisualCircleSurface3D.CreateStatic(
        //        "ground",
        //        Color.LightGoldenrodYellow.WithAlpha(0.4)
        //            .ToPovRayMaterial(finish)
        //            .CreateThickSurfaceStyle(
        //                Color.OrangeRed.ToPovRayMaterial(DefaultMaterialFinish), 
        //                0.075
        //            ),
        //        AxesOrigin - 0.075 / 2 * LinFloat64Vector3D.E2,
        //        LinFloat64Vector3D.E2, 
        //        GridUnitCount / 2d,
        //        true
        //    )
        //);
    }

    protected override void ComposeFrame(int frameIndex)
    {
        base.ComposeFrame(frameIndex);

        if (DrawRotorTrace) AddRotorTrace();

        AddInputVectors(frameIndex);
        AddRotors(frameIndex);
        AddProjectedVectors(frameIndex);
        AddIntersections(frameIndex);

        ActiveSceneComposer.AddSphere(
            GrVisualSphereSurface3D.CreateStatic(
                "rotorTraceSphere",
                Color.Yellow.WithAlpha(0.1)
                    .ToPovRayMaterial(DefaultMaterialFinish)
                    .CreateThinSurfaceStyle(),
                4 * SourceVector.Norm()
            )
        );

        AddGroundPlane();
    }

}