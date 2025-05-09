using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Graphics;


public class GrBabylonJsRotorFamilyVisualizer :
    GrBabylonJsSceneSequenceComposer
{
    public LinFloat64Vector3D SourceVector { get; }

    public LinFloat64Vector3D TargetVector { get; }

    public IReadOnlyList<LinFloat64Angle> ThetaValues { get; }
    
    public bool DrawRotorTrace { get; set; }

    public int DataTextImageMaxWidth { get; private set; }

    public int DataTextImageMaxHeight { get; private set; }


    public GrBabylonJsRotorFamilyVisualizer(string workingFolder, Float64SamplingSpecs samplingSpecs, ILinFloat64Vector3D sourceVector, ILinFloat64Vector3D targetVector, IReadOnlyList<LinFloat64Angle> thetaValues) 
        : base(workingFolder, samplingSpecs)
    {
        SourceVector = sourceVector.ToUnitLinVector3D();
        TargetVector = targetVector.ToUnitLinVector3D();
        ThetaValues = thetaValues;
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

    public LinFloat64Angle GetTheta(int frameIndex)
    {
        return ThetaValues[frameIndex];

        //return (index / (FrameCount - 1d)).Lerp(-90, 90).DegreesToAngle();
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

        var theta = ThetaValues[frameIndex];
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


    protected override void AddLaTeXTextures()
    {
        KaTeXComposer.AddLaTeXEquation(
            "sourceVectorText",
            @"\boldsymbol{u}"
        );
            
        KaTeXComposer.AddLaTeXEquation(
            "targetVectorText",
            @"\boldsymbol{v}"
        );
        
        KaTeXComposer.AddLaTeXEquation(
            "sourceVector1Text",
            @"\boldsymbol{u}\left(\theta\right)"
        );
            
        KaTeXComposer.AddLaTeXEquation(
            "targetVector1Text",
            @"\boldsymbol{v}\left(\theta\right)"
        );
        
        KaTeXComposer.AddLaTeXEquation(
            "phiMinRotorText",
            @"\boldsymbol{R}\left(0\right)"
        );

        KaTeXComposer.AddLaTeXEquation(
            "thetaRotorText",
            @"\boldsymbol{S}\left(\theta\right)"
        );
        
        KaTeXComposer.AddLaTeXEquation(
            "phiRotorText",
            @"\boldsymbol{R}\left(\theta\right)"
        );
        
        KaTeXComposer.AddLaTeXEquation(
            "phiRotorCopyText",
            @"\boldsymbol{R}\left(\theta\right)"
        );
        
        KaTeXComposer.AddLaTeXEquation(
            "thetaRotorPlaneText",
            @"\boldsymbol{B}"
        );
        
        KaTeXComposer.AddLaTeXEquation(
            "phiRotorPlaneText",
            @"\boldsymbol{N}\left(\theta\right)"
        );

        var phiMin = GetPhiMin();
        var vuDiff = (TargetVector - SourceVector).ToUnitLinVector3D();
        var uvNormal = SourceVector.VectorUnitCross(TargetVector);
        
        KaTeXComposer.AddLaTeXAlignedEquations(
            "modelText",
            new Pair<string>[]
            {
                new (@"\boldsymbol{u}", @$"\left( {SourceVector.X:F4}, {SourceVector.Y:F4}, {SourceVector.Z:F4} \right)"),
                new (@"\boldsymbol{v}", @$"\left( {TargetVector.X:F4}, {TargetVector.Y:F4}, {TargetVector.Z:F4} \right)"),
                new (@"\phi", @$"\cos^{{-1}}\left(\boldsymbol{{u}}\cdot\boldsymbol{{v}}\right) = {phiMin.DegreesValue:F4}"),
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

        for (var i = 0; i < ImageCount; i++)
        {
            var theta = ThetaValues[i];
            var phi = ThetaToPhi(theta);
            var uvNormal1 = uvNormal.RotateVectorUsingAxisAngle(vuDiff, theta);
            var u1 = SourceVector.RejectOnVector(uvNormal1);
            var v1 = TargetVector.RejectOnVector(uvNormal1);

            KaTeXComposer.AddLaTeXAlignedEquations(
                $"dataText-{i:D6}",
                new Pair<string>[]
                {
                    new (@"\theta", @$"{theta.DegreesValue:F4}"),
                    new (@"\boldsymbol{\hat{N}}\left( \theta \right)", @$"\left( {uvNormal1.X:F4}, {uvNormal1.Y:F4}, {uvNormal1.Z:F4} \right)^{{*}}"),
                    new (@"\boldsymbol{u}\left( \theta \right)", @$"\left( {u1.X:F4}, {u1.Y:F4}, {u1.Z:F4} \right)"),
                    new (@"\boldsymbol{v}\left( \theta \right)", @$"\left( {v1.X:F4}, {v1.Y:F4}, {v1.Z:F4} \right)"),
                    new (@"\varphi\left( \theta \right)", @$"{phi.DegreesValue:F4}")
                }
            );
        }
    }

    protected override void AddImageTextures()
    {
        for (var i = 0; i < ImageCount; i++)
        {
            ImageSet.AddImage(
                "gui",
                $"ThetaPhiPlot-{i:D6}", 
                GetThetaPhiPlotImage(i)
            );
        }
    }

    protected override void InitializeSceneComposers(int frameIndex)
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
                FileName = GetFrameName(frameIndex) + ".png"
            }
        )
        {
            BackgroundColor = Color.AliceBlue,
            ShowDebugLayer = false
        };

        //mainSceneComposer.SceneObject.SceneProperties.UseOrderIndependentTransparency = true;

        CodeFilesComposer = new GrBabylonJsCodeFilesComposer(mainSceneComposer)
        {
            CanvasWidth = ImageWidth,
            CanvasHeight = ImageHeight,
            CanvasFullScreen = false
        };
    }

    protected override void AddGuiLayer(int frameIndex)
    {
        var scene = MainSceneComposer.SceneObject;

        // Add GUI layer
        var uiTexture = scene.AddGuiFullScreenUi("uiTexture");
        
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
            new GrBabylonJsGuiLineProperties
            {
                X1 = 10,
                X2 = uiPanel1Width - 10,
                Y1 = 70,
                Y2 = 70,
                Color = Color.Black,
                LineWidth = 3
            }
        );

        var latexPngData1 = ImageSet["latex", "modelText"];
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

        var uiPanel2Width = CodeFilesComposer.CanvasWidth * 0.25;
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

        var thetaPhiPlotData = ImageSet["gui", $"ThetaPhiPlot-{frameIndex:D6}"];
        uiPanel2.AddGuiImage(
            "thetaPhiPlotGuiImage",
            thetaPhiPlotData.GetImageDataUrlBase64(),
            new GrBabylonJsGuiImageProperties
            {
                Stretch = GrBabylonJsImageStretch.Uniform,
                //Alpha = 0.5d,
                WidthInPixels = uiPanel2Width - 20,
                HeightInPixels = (uiPanel2Width - 20) * thetaPhiPlotData.ImageHeightToWidth,
                PaddingLeftInPixels = 0,
                PaddingTopInPixels = 0,
                PaddingBottomInPixels = 0,
                PaddingRightInPixels = 0,
                HorizontalAlignment = GrBabylonJsHorizontalAlignment.Center,
                VerticalAlignment = GrBabylonJsVerticalAlignment.Center,
            }
        );

        var dataTextImageData = ImageSet["latex", $"dataText-{frameIndex:D6}"];
        var dataTextImageWidth = uiPanel2Width * dataTextImageData.ImageWidth / DataTextImageMaxWidth;

        uiPanel2.AddGuiImage(
            "latexGuiImage2",
            dataTextImageData.GetImageDataUrlBase64(),
            new GrBabylonJsGuiImageProperties
            {
                //Alpha = 0.5d,
                WidthInPixels = dataTextImageWidth,
                HeightInPixels = dataTextImageWidth * dataTextImageData.ImageHeightToWidth,
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
            ImageSet["latex", "sourceVectorText"], 
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
            ImageSet["latex", "targetVectorText"],
            TargetVector * 4.35d,
            LaTeXScalingFactor
        );
    }

    private void AddProjectedVectors(int frameIndex)
    {
        var u = SourceVector * 4;
        var v = TargetVector * 4;
        var uvNormal = u.VectorUnitCross(v);
        var thetaPlaneNormal = (v - u).ToUnitLinVector3D();
        var theta = ThetaValues[frameIndex];
        var uvNormal1 = uvNormal.RotateVectorUsingAxisAngle(thetaPlaneNormal, theta);
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

        MainSceneComposer
            .AddVector(
                "sourceVector1", 
                u1, 
                Color.Red, 
                0.075
            ).AddLaTeXText(
                "sourceVector1Text", 
                ImageSet["latex", "sourceVector1Text"], 
                u1 + u1.ToUnitLinVector3D() * 0.35d, 
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
                ImageSet["latex", "targetVector1Text"], 
                v1 + v1.ToUnitLinVector3D() * 0.35d, 
                LaTeXScalingFactor
            );
    }

    private void AddRotors(int index)
    {
        var scene = MainSceneComposer.SceneObject;

        var u = SourceVector * 4;
        var v = TargetVector * 4;
        var vuSum = (v + u) * 0.5;
        var vuDiff = (v - u).ToUnitLinVector3D();
        var uvNormal = u.VectorUnitCross(v);
        var theta = ThetaValues[index];
        var uvNormal1 = uvNormal.RotateVectorUsingAxisAngle(vuDiff, theta);
        var u1 = u.RejectOnVector(uvNormal1);
        var v1 = v.RejectOnVector(uvNormal1);
        var vuSum1 = vuSum.RotateVectorUsingAxisAngle(vuDiff, theta);

        MainSceneComposer.AddDiscSector(
            "phiMinRotor",
            LinFloat64Vector3D.Zero,
            u,
            v,
            u1.VectorENorm(),
            true,
            System.Drawing.Color.IndianRed.ToImageSharpColor(128),
            0.05,
            System.Drawing.Color.IndianRed.ToImageSharpColor(128)
        ).AddLaTeXText(
            "phiMinRotorText",
            ImageSet["latex", "phiMinRotorText"],
            0.5d.Lerp(u, v).SetLength(u1.VectorENorm() + 0.35d),
            LaTeXScalingFactor
        );

        if (!theta.DegreesValue.IsNearZero(1e-5))
        {
            MainSceneComposer.AddDiscSector(
                "thetaRotor",
                LinFloat64Vector3D.Zero,
                vuSum,
                vuSum1,
                u1.VectorENorm(),
                true,
                System.Drawing.Color.LightGreen.ToImageSharpColor(128),
                0.05,
                System.Drawing.Color.LightGreen.ToImageSharpColor(128)
            ).AddLaTeXText(
                "thetaRotorText",
                ImageSet["latex", "thetaRotorText"],
                0.5d.Lerp(vuSum, vuSum1).SetLength(u1.VectorENorm() + 0.35d),
                LaTeXScalingFactor
            );
        }

        MainSceneComposer.AddDiscSector(
            "phiRotor",
            LinFloat64Vector3D.Zero,
            u1,
            v1,
            u1.VectorENorm(),
            true,
            System.Drawing.Color.DarkSlateBlue.ToImageSharpColor(128),
            0.05,
            System.Drawing.Color.DarkSlateBlue.ToImageSharpColor(128)
        ).AddLaTeXText(
            "phiRotorText",
            ImageSet["latex", "phiRotorText"],
            0.5d.Lerp(u1, v1).SetLength(u1.VectorENorm() + 0.35d),
            LaTeXScalingFactor
        );

        MainSceneComposer.AddDiscSector(
            "phiRotorCopy",
            u - u1,
            u1,
            v1,
            u1.VectorENorm(),
            true,
            System.Drawing.Color.DarkSlateBlue.ToImageSharpColor(128),
            0.05,
            System.Drawing.Color.DarkSlateBlue.ToImageSharpColor(128)
        ).AddLaTeXText(
            "phiRotorCopyText",
            ImageSet["latex", "phiRotorCopyText"],
            u - u1 + 0.5d.Lerp(u1, v1).SetLength(u1.VectorENorm() + 0.35d),
            LaTeXScalingFactor
        );

        MainSceneComposer.AddDisc(
            "thetaRotorPlane",
            LinFloat64Vector3D.Zero,
            vuDiff,
            5d,
            System.Drawing.Color.YellowGreen.ToImageSharpColor(64),
            0.035,
            System.Drawing.Color.YellowGreen.ToImageSharpColor(64)
        ).AddLaTeXText(
            "thetaRotorPlaneText",
            ImageSet["latex", "thetaRotorPlaneText"],
            vuSum.SetLength(-4d),
            LaTeXScalingFactor
        );

        MainSceneComposer.AddDisc(
            "phiRotorPlane",
            LinFloat64Vector3D.Zero,
            uvNormal1,
            5d,
            System.Drawing.Color.BurlyWood.ToImageSharpColor(64),
            0.035,
            System.Drawing.Color.BurlyWood.ToImageSharpColor(64)
        ).AddLaTeXText(
            "phiRotorPlaneText",
            ImageSet["latex", "phiRotorPlaneText"],
            vuSum1.SetLength(-4d),
            LaTeXScalingFactor
        );
    }

    private void AddRotorTrace()
    {
        var scene = MainSceneComposer.SceneObject;

        var thetaDegrees = 
            ThetaValues.Select(t => t.DegreesValue).ToImmutableArray();

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

            MainSceneComposer.AddCircleCurve(
                $"phiRotorTrace{i}",
                u - u1,
                u1.VectorUnitCross(v1),
                u1.VectorENorm(),
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

    private void AddIntersections(int frameIndex)
    {
        var scene = MainSceneComposer.SceneObject;

        var u = SourceVector * 4;
        var v = TargetVector * 4;
        var vuSum = (v + u) * 0.5;
        var vuDiff = (v - u).ToUnitLinVector3D();
        var uvNormal = u.VectorUnitCross(v);
        var theta = ThetaValues[frameIndex];
        var uvNormal1 = uvNormal.RotateVectorUsingAxisAngle(vuDiff, theta);
        var u1 = u.RejectOnVector(uvNormal1);
        var v1 = v.RejectOnVector(uvNormal1);
        var vuSum1 = vuSum.RotateVectorUsingAxisAngle(vuDiff, theta);

        var intersectionPointColor = Color.DarkSlateGray;
        var intersectionLineColor = Color.DarkSlateGray.SetAlpha(0.5f);
        
        MainSceneComposer.AddPoints(
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

        MainSceneComposer.AddLineSegment(
            "intersectionLine", 
            vuSum1.SetLength(5d), 
            vuSum1.SetLength(-5d),
            intersectionLineColor, 
            0.035
        );

        if ((u - u1).VectorENorm() > 1)
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

        if (DrawRotorTrace) AddRotorTrace();

        AddInputVectors();
        AddRotors(frameIndex);
        AddProjectedVectors(frameIndex);
        AddIntersections(frameIndex);
    }

}