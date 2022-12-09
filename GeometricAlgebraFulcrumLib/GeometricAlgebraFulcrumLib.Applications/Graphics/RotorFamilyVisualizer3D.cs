using System.Collections.Immutable;
using DataStructuresLib.Basic;
using DataStructuresLib.Files;
using GraphicsComposerLib.Rendering.BabylonJs;
using GraphicsComposerLib.Rendering.BabylonJs.Constants;
using GraphicsComposerLib.Rendering.BabylonJs.GUI;
using GraphicsComposerLib.Rendering.Colors;
using GraphicsComposerLib.Rendering.LaTeX.ImageComposers;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Curves;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Applications.Graphics;

public class RotorFamilyVisualizer3D :
    GrBabylonJsSnapshotComposer3D
{
    public Float64Tuple3D SourceVector { get; }

    public Float64Tuple3D TargetVector { get; }

    public IReadOnlyList<PlanarAngle> ThetaValues { get; }
    
    public bool DrawRotorTrace { get; set; }

    public int DataTextImageMaxWidth { get; private set; }

    public int DataTextImageMaxHeight { get; private set; }


    public RotorFamilyVisualizer3D(IReadOnlyList<double> cameraAlphaValues, IReadOnlyList<double> cameraBetaValues, IFloat64Tuple3D sourceVector, IFloat64Tuple3D targetVector, IReadOnlyList<PlanarAngle> thetaValues) 
        : base(cameraAlphaValues, cameraBetaValues)
    {
        SourceVector = sourceVector.ToUnitVector();
        TargetVector = targetVector.ToUnitVector();
        ThetaValues = thetaValues;
    }


    public PlanarAngle GetPhiMin()
    {
        return GetPhiMinCos().ArcCos();
    }
    
    public double GetPhiMinCos()
    {
        return SourceVector.GetUnitVectorsAngleCos(TargetVector);
    }

    public PlanarAngle ThetaToPhi(PlanarAngle theta)
    {
        return ThetaToPhiCos(theta).ArcCos();
    }

    public Float64Tuple3D ThetaToUnitNormal(PlanarAngle theta)
    {
        var n = 
            SourceVector.VectorUnitCross(TargetVector);

        return n.RotateUsingAxisAngle(
            (TargetVector - SourceVector).ToUnitVector(),
            theta
        );
    }

    public double ThetaToPhiCos(PlanarAngle theta)
    {
        if (!theta.Degrees.IsNearInRange(-90, 90))
            throw new ArgumentOutOfRangeException(nameof(theta));

        var phiMinCos = GetPhiMinCos();

        return 1d + (2d * (1 - phiMinCos)) / (theta.Sin().Square() * (1 + phiMinCos) - 2d);
    }

    public Float64Tuple4D ThetaToQuaternion(PlanarAngle theta)
    {
        var phi = ThetaToPhi(theta);
        var normal = ThetaToUnitNormal(theta);

        return normal.CreateQuaternionFromAxisAngle(phi);
    }

    public PlanarAngle PhiToTheta(PlanarAngle phi)
    {
        return PhiToThetaSin(phi).ArcSin();
    }

    public double PhiToThetaSin(PlanarAngle phi)
    {
        var phiMinCos = GetPhiMinCos();

        if (phi < phiMinCos.ArcCos() || phi > Math.PI)
            throw new ArgumentOutOfRangeException(nameof(phi));

        var phiCos = phi.Cos();

        return (2d * (phiCos - phiMinCos) / ((1 + phiMinCos) * (phiCos - 1))).Sqrt();
    }
    
    public Float64Tuple3D PhiToUnitNormal(PlanarAngle phi)
    {
        return ThetaToUnitNormal(PhiToTheta(phi));
    }
    
    public Float64Tuple4D PhiToQuaternion(PlanarAngle phi)
    {
        var normal = PhiToUnitNormal(phi);

        return normal.CreateQuaternionFromAxisAngle(phi);
    }

    public PlanarAngle GetTheta(int index)
    {
        return ThetaValues[index];

        //return (index / (FrameCount - 1d)).Lerp(-90, 90).DegreesToAngle();
    }

    private Image GetThetaPhiPlotImage(int index)
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
            new FunctionSeries(theta => ThetaToPhi(theta.DegreesToAngle()).Degrees, -90, 90, 180d / 450)
            {
                Color = System.Drawing.Color.DarkBlue.ToOxyColor(),
                CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );

        var theta = ThetaValues[index];
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
                    new ScatterPoint(theta.Degrees, phi.Degrees)
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
        
        for (var i = 0; i < FrameCount; i++)
        {
            ImageCache.AddPng(
                $"ThetaPhiPlot-{i:D6}", 
                GetThetaPhiPlotImage(i)
            );
        }

        Console.WriteLine("done.");
        Console.WriteLine();

        
        Console.Write("Generating LaTeX images .. ");

        ImageCache.MarginSize = 0;
        ImageCache.BackgroundColor = Color.FromRgba(255, 255, 255, 0);
            
        ImageCache.AddLaTeXEquation(
            "sourceVectorText",
            @"\boldsymbol{u}"
        );
            
        ImageCache.AddLaTeXEquation(
            "targetVectorText",
            @"\boldsymbol{v}"
        );
        
        ImageCache.AddLaTeXEquation(
            "sourceVector1Text",
            @"\boldsymbol{u}\left(\theta\right)"
        );
            
        ImageCache.AddLaTeXEquation(
            "targetVector1Text",
            @"\boldsymbol{v}\left(\theta\right)"
        );
        
        ImageCache.AddLaTeXEquation(
            "phiMinRotorText",
            @"\boldsymbol{R}\left(0\right)"
        );

        ImageCache.AddLaTeXEquation(
            "thetaRotorText",
            @"\boldsymbol{S}\left(\theta\right)"
        );
        
        ImageCache.AddLaTeXEquation(
            "phiRotorText",
            @"\boldsymbol{R}\left(\theta\right)"
        );
        
        ImageCache.AddLaTeXEquation(
            "phiRotorCopyText",
            @"\boldsymbol{R}\left(\theta\right)"
        );
        
        ImageCache.AddLaTeXEquation(
            "thetaRotorPlaneText",
            @"\boldsymbol{B}"
        );
        
        ImageCache.AddLaTeXEquation(
            "phiRotorPlaneText",
            @"\boldsymbol{N}\left(\theta\right)"
        );

        ImageCache.MarginSize = 20;
        //ImageCache.BackgroundColor = Color.FromRgba(32, 32, 255, 16);

        var phiMin = GetPhiMin();
        var vuDiff = (TargetVector - SourceVector).ToUnitVector();
        var uvNormal = SourceVector.VectorUnitCross(TargetVector);
        
        ImageCache.AddLaTeXAlignedEquations(
            "modelText",
            new Pair<string>[]
            {
                new (@"\boldsymbol{u}", @$"\left( {SourceVector.X:F4}, {SourceVector.Y:F4}, {SourceVector.Z:F4} \right)"),
                new (@"\boldsymbol{v}", @$"\left( {TargetVector.X:F4}, {TargetVector.Y:F4}, {TargetVector.Z:F4} \right)"),
                new (@"\phi", @$"\cos^{{-1}}\left(\boldsymbol{{u}}\cdot\boldsymbol{{v}}\right) = {phiMin.Degrees:F4}"),
                new (@"\boldsymbol{B}", @"\left(\boldsymbol{v}-\boldsymbol{u}\right)\rfloor\boldsymbol{e}^{-1}_{123}"),
                new (@"\boldsymbol{\hat{B}}", @$"\frac{{\boldsymbol{{B}}}}{{\sqrt{{-\boldsymbol{{B}}^{{2}}}}}}"),
                new (@"", @$"\left( {vuDiff.X:F4}, {vuDiff.Y:F4}, {vuDiff.Z:F4} \right)^{{*}}"),
                new (@"\boldsymbol{S}\left( \theta \right)", @"\exp\left(\frac{1}{2}\theta\hat{\boldsymbol{B}}\right)"),
                new (@"\boldsymbol{N}\left( \theta \right)", @"\boldsymbol{S}\left(\theta\right)\left(\boldsymbol{v}\wedge\boldsymbol{u}\right)\boldsymbol{S}^{\sim}\left(\theta\right)"),
                new (@"\boldsymbol{\hat{N}}\left( \theta \right)", @$"\frac{{\boldsymbol{{N}}\left( \theta \right)}}{{\sqrt{{-\boldsymbol{{N}}^{{2}}\left( \theta \right)}}}}"),
                new (@"\boldsymbol{u}\left( \theta \right)", @"\left(\boldsymbol{u}\rfloor\boldsymbol{N}\left(\theta\right)\right)\rfloor\boldsymbol{N}^{-1}\left(\theta\right)"),
                new (@"\boldsymbol{v}\left( \theta \right)", @"\left(\boldsymbol{v}\rfloor\boldsymbol{N}\left(\theta\right)\right)\rfloor\boldsymbol{N}^{-1}\left(\theta\right)"),
                new (@"\boldsymbol{u}\left( \theta \right) \cdot \boldsymbol{v}\left( \theta \right)", @"1+\frac{2\left(1-\boldsymbol{u}\cdot\boldsymbol{v}\right)}{\sin^{2}\left(\theta\right)\left(1+\boldsymbol{u}\cdot\boldsymbol{v}\right)-2}"),
                new (@"\varphi\left( \theta \right)", @"\cos^{-1}\left(\boldsymbol{u}\left( \theta \right) \cdot \boldsymbol{v}\left( \theta \right) \right)"),
                new (@"\boldsymbol{R}\left( \theta \right)", @"\exp\left(\frac{1}{2}\varphi\left(\theta\right)\hat{\boldsymbol{N}}\left(\theta\right)\right)")
            }
        );

        for (var i = 0; i < FrameCount; i++)
        {
            var theta = ThetaValues[i];
            var phi = ThetaToPhi(theta);
            var uvNormal1 = uvNormal.RotateUsingAxisAngle(vuDiff, theta);
            var u1 = SourceVector.RejectOnVector(uvNormal1);
            var v1 = TargetVector.RejectOnVector(uvNormal1);

            ImageCache.AddLaTeXAlignedEquations(
                $"dataText-{i:D6}",
                new Pair<string>[]
                {
                    new (@"\theta", @$"{theta.Degrees:F4}"),
                    new (@"\boldsymbol{\hat{N}}\left( \theta \right)", @$"\left( {uvNormal1.X:F4}, {uvNormal1.Y:F4}, {uvNormal1.Z:F4} \right)^{{*}}"),
                    new (@"\boldsymbol{u}\left( \theta \right)", @$"\left( {u1.X:F4}, {u1.Y:F4}, {u1.Z:F4} \right)"),
                    new (@"\boldsymbol{v}\left( \theta \right)", @$"\left( {v1.X:F4}, {v1.Y:F4}, {v1.Z:F4} \right)"),
                    new (@"\varphi\left( \theta \right)", @$"{phi.Degrees:F4}")
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
        for (var i = 0; i < FrameCount; i++)
        {
            var imageData = ImageCache[$"dataText-{i:D6}"];

            if (maxWidth < imageData.Width) maxWidth = imageData.Width;
            if (maxHeight < imageData.Height) maxHeight = imageData.Height;
        }

        DataTextImageMaxWidth = maxWidth;
        DataTextImageMaxHeight = maxHeight;

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
            ShowDebugLayer = false
        };

        //mainSceneComposer.SceneObject.SceneProperties.UseOrderIndependentTransparency = true;

        var htmlComposer = new GrBabylonJsHtmlComposer3D(mainSceneComposer)
        {
            CanvasWidth = CanvasWidth,
            CanvasHeight = CanvasHeight,
            CanvasFullScreen = false
        };

        return htmlComposer;
    }

    //protected override void AddGrid()
    //{
    //}
    
    protected override void AddGuiLayer(int index)
    {
        var scene = MainSceneComposer.SceneObject;

        // Add GUI layer
        var uiTexture = scene.AddGuiFullScreenUi("uiTexture");
        
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
                FontSize = 36,
                FontWeight = "'750'",
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
                Y1 = 70,
                Y2 = 70,
                Color = Color.Black,
                LineWidth = 3
            }
        );

        var latexPngData1 = ImageCache["modelText"];
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

        var uiPanel2Width = HtmlComposer.CanvasWidth * 0.25;
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

        var thetaPhiPlotData = ImageCache[$"ThetaPhiPlot-{index:D6}"];
        uiPanel2.AddGuiImage(
            "thetaPhiPlotGuiImage",
            thetaPhiPlotData.GetBase64HtmlString(),
            new GrBabylonJsGuiImage.GuiImageProperties
            {
                Stretch = GrBabylonJsImageStretch.Uniform,
                //Alpha = 0.5d,
                WidthInPixels = uiPanel2Width - 20,
                HeightInPixels = (uiPanel2Width - 20) * thetaPhiPlotData.HeightToWidthRatio,
                PaddingLeftInPixels = 0,
                PaddingTopInPixels = 0,
                PaddingBottomInPixels = 0,
                PaddingRightInPixels = 0,
                HorizontalAlignment = GrBabylonJsHorizontalAlignment.Center,
                VerticalAlignment = GrBabylonJsVerticalAlignment.Center,
            }
        );

        var dataTextImageData = ImageCache[$"dataText-{index:D6}"];
        var dataTextImageWidth = uiPanel2Width * dataTextImageData.Width / DataTextImageMaxWidth;

        uiPanel2.AddGuiImage(
            "latexGuiImage2",
            dataTextImageData.GetBase64HtmlString(),
            new GrBabylonJsGuiImage.GuiImageProperties
            {
                //Alpha = 0.5d,
                WidthInPixels = dataTextImageWidth,
                HeightInPixels = dataTextImageWidth * dataTextImageData.HeightToWidthRatio,
                PaddingLeftInPixels = 5,
                PaddingTopInPixels = 0,
                PaddingBottomInPixels = 0,
                PaddingRightInPixels = 5,
                HorizontalAlignment = GrBabylonJsHorizontalAlignment.Left,
                VerticalAlignment = GrBabylonJsVerticalAlignment.Top
            }
        );
    }

    private void AddInputVectors()
    {
        MainSceneComposer.AddVector(
            "sourceVector",
            4 * SourceVector,
            Color.Red,
            0.075
        ).AddLaTeXText(
            "sourceVectorText", 
            ImageCache, 
            SourceVector * 4.35d, 
            LaTeXScalingFactor
        );

        MainSceneComposer.AddVector(
            "targetVector",
            4 * TargetVector,
            Color.Blue,
            0.075
        ).AddLaTeXText(
            "targetVectorText",
            ImageCache,
            TargetVector * 4.35d,
            LaTeXScalingFactor
        );
    }

    private void AddProjectedVectors(int index)
    {
        var u = SourceVector * 4;
        var v = TargetVector * 4;
        var uvNormal = u.VectorUnitCross(v);
        var thetaPlaneNormal = (v - u).ToUnitVector();
        var theta = ThetaValues[index];
        var uvNormal1 = uvNormal.RotateUsingAxisAngle(thetaPlaneNormal, theta);
        var u1 = u.RejectOnVector(uvNormal1);
        var v1 = v.RejectOnVector(uvNormal1);

        var dashSpecs = new GrVisualDashedLineSpecs(1, 1, 20);

        MainSceneComposer.AddLineSegment(
            "sourceSegment",
            u,
            u1,
            Color.Red,
            dashSpecs
        ).AddLineSegment(
            "originSegment",
            u - u1,
            Float64Tuple3D.Zero,
            Color.DarkGreen,
            dashSpecs
        ).AddLineSegment(
            "targetSegment",
            v,
            v1,
            Color.Blue, dashSpecs
        );

        MainSceneComposer
            .AddVector(
                "sourceVector1", 
                u1, 
                Color.Red, 
                0.075
            ).AddLaTeXText(
                "sourceVector1Text", 
                ImageCache, 
                u1 + u1.ToUnitVector() * 0.35d, 
                LaTeXScalingFactor
            );

        MainSceneComposer
            .AddVector(
                "targetVector1", 
                v1, 
                Color.Blue, 
                0.075
            ).AddLaTeXText(
                "targetVector1Text", 
                ImageCache, 
                v1 + v1.ToUnitVector() * 0.35d, 
                LaTeXScalingFactor
            );
    }

    private void AddRotors(int index)
    {
        var scene = MainSceneComposer.SceneObject;

        var u = SourceVector * 4;
        var v = TargetVector * 4;
        var vuSum = (v + u) * 0.5;
        var vuDiff = (v - u).ToUnitVector();
        var uvNormal = u.VectorUnitCross(v);
        var theta = ThetaValues[index];
        var uvNormal1 = uvNormal.RotateUsingAxisAngle(vuDiff, theta);
        var u1 = u.RejectOnVector(uvNormal1);
        var v1 = v.RejectOnVector(uvNormal1);
        var vuSum1 = vuSum.RotateUsingAxisAngle(vuDiff, theta);

        MainSceneComposer.AddDiscSector(
            "phiMinRotor",
            Float64Tuple3D.Zero,
            u,
            v,
            u1.GetVectorNorm(),
            true,
            System.Drawing.Color.IndianRed.ToImageSharpColor(128),
            0.05,
            System.Drawing.Color.IndianRed.ToImageSharpColor(128)
        ).AddLaTeXText(
            "phiMinRotorText",
            ImageCache,
            0.5d.Lerp(u, v).SetLength(u1.GetVectorNorm() + 0.35d),
            LaTeXScalingFactor
        );

        if (!theta.Degrees.IsNearZero(1e-5))
        {
            MainSceneComposer.AddDiscSector(
                "thetaRotor",
                Float64Tuple3D.Zero,
                vuSum,
                vuSum1,
                u1.GetVectorNorm(),
                true,
                System.Drawing.Color.LightGreen.ToImageSharpColor(128),
                0.05,
                System.Drawing.Color.LightGreen.ToImageSharpColor(128)
            ).AddLaTeXText(
                "thetaRotorText",
                ImageCache,
                0.5d.Lerp(vuSum, vuSum1).SetLength(u1.GetVectorNorm() + 0.35d),
                LaTeXScalingFactor
            );
        }

        MainSceneComposer.AddDiscSector(
            "phiRotor",
            Float64Tuple3D.Zero,
            u1,
            v1,
            u1.GetVectorNorm(),
            true,
            System.Drawing.Color.DarkSlateBlue.ToImageSharpColor(128),
            0.05,
            System.Drawing.Color.DarkSlateBlue.ToImageSharpColor(128)
        ).AddLaTeXText(
            "phiRotorText",
            ImageCache,
            0.5d.Lerp(u1, v1).SetLength(u1.GetVectorNorm() + 0.35d),
            LaTeXScalingFactor
        );

        MainSceneComposer.AddDiscSector(
            "phiRotorCopy",
            u - u1,
            u1,
            v1,
            u1.GetVectorNorm(),
            true,
            System.Drawing.Color.DarkSlateBlue.ToImageSharpColor(128),
            0.05,
            System.Drawing.Color.DarkSlateBlue.ToImageSharpColor(128)
        ).AddLaTeXText(
            "phiRotorCopyText",
            ImageCache,
            u - u1 + 0.5d.Lerp(u1, v1).SetLength(u1.GetVectorNorm() + 0.35d),
            LaTeXScalingFactor
        );

        MainSceneComposer.AddDisc(
            "thetaRotorPlane",
            Float64Tuple3D.Zero,
            vuDiff,
            5d,
            System.Drawing.Color.YellowGreen.ToImageSharpColor(64),
            0.035,
            System.Drawing.Color.YellowGreen.ToImageSharpColor(64)
        ).AddLaTeXText(
            "thetaRotorPlaneText",
            ImageCache,
            vuSum.SetLength(-4d),
            LaTeXScalingFactor
        );

        MainSceneComposer.AddDisc(
            "phiRotorPlane",
            Float64Tuple3D.Zero,
            uvNormal1,
            5d,
            System.Drawing.Color.BurlyWood.ToImageSharpColor(64),
            0.035,
            System.Drawing.Color.BurlyWood.ToImageSharpColor(64)
        ).AddLaTeXText(
            "phiRotorPlaneText",
            ImageCache,
            vuSum1.SetLength(-4d),
            LaTeXScalingFactor
        );
    }

    private void AddRotorTrace()
    {
        var scene = MainSceneComposer.SceneObject;

        var thetaDegrees = 
            ThetaValues.Select(t => t.Degrees).ToImmutableArray();

        var thetaMin = thetaDegrees.Min();
        var thetaMax = thetaDegrees.Max();

        var thetaDegreesArray = 
            thetaMin.GetLinearRange(thetaMax, 31, false).ToImmutableArray();

        var u = SourceVector * 4;
        var v = TargetVector * 4;
        var vuDiff = (v - u).ToUnitVector();
        var uvNormal = u.VectorUnitCross(v);
        
        for (var i = 0; i < thetaDegreesArray.Length; i++)
        {
            var theta = thetaDegreesArray[i].DegreesToAngle();
            var uvNormal1 = uvNormal.RotateUsingAxisAngle(vuDiff, theta);
            var u1 = u.RejectOnVector(uvNormal1);
            var v1 = v.RejectOnVector(uvNormal1);

            MainSceneComposer.AddCircle(
                $"phiRotorTrace{i}",
                u - u1,
                u1.VectorUnitCross(v1),
                u1.GetVectorNorm(),
                System.Drawing.Color.DarkSlateBlue.ToImageSharpColor(64),
                0.025
            );

            //MainSceneComposer.AddCircleCurveArc(
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

    private void AddIntersections(int index)
    {
        var scene = MainSceneComposer.SceneObject;

        var u = SourceVector * 4;
        var v = TargetVector * 4;
        var vuSum = (v + u) * 0.5;
        var vuDiff = (v - u).ToUnitVector();
        var uvNormal = u.VectorUnitCross(v);
        var theta = ThetaValues[index];
        var uvNormal1 = uvNormal.RotateUsingAxisAngle(vuDiff, theta);
        var u1 = u.RejectOnVector(uvNormal1);
        var v1 = v.RejectOnVector(uvNormal1);
        var vuSum1 = vuSum.RotateUsingAxisAngle(vuDiff, theta);

        var intersectionPointColor = Color.DarkSlateGray;
        var intersectionLineColor = Color.DarkSlateGray.SetAlpha(0.5f);
        
        MainSceneComposer.AddPoints(
            i=> $"intersectionPoint{i + 1}", 
            intersectionPointColor, 
            0.08, 
            Float64Tuple3D.Zero, 
            vuSum1.SetLength(5d),
            vuSum1.SetLength(-5d),
            u - u1,
            u,
            v,
            u1,
            v1,
            vuSum.SetLength(u1.GetVectorNorm()),
            vuSum1.SetLength(u1.GetVectorNorm()),
            u.SetLength(u1.GetVectorNorm()),
            v.SetLength(u1.GetVectorNorm())
        );

        MainSceneComposer.AddLineSegment(
            "intersectionLine", 
            vuSum1.SetLength(5d), 
            vuSum1.SetLength(-5d),
            intersectionLineColor, 
            0.035
        );

        if ((u - u1).GetVectorNorm() > 1)
        {
            MainSceneComposer.AddRightAngle(
                "rightAngle1",
                u1,
                -u1,
                u - u1,
                0.5,
                Color.Red,
                Color.Red.SetAlpha(0.25f)
            );

            MainSceneComposer.AddRightAngle(
                "rightAngle2",
                v1,
                -v1,
                v - v1,
                0.5,
                Color.Blue,
                Color.Blue.SetAlpha(0.25f)
            );
        }
    }

    protected override GrBabylonJsHtmlComposer3D GenerateSnapshotCode(int index)
    {
        base.GenerateSnapshotCode(index);

        if (DrawRotorTrace) AddRotorTrace();

        AddInputVectors();
        AddRotors(index);
        AddProjectedVectors(index);
        AddIntersections(index);

        return HtmlComposer;
    }

}