using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs;
using GraphicsComposerLib.Rendering.BabylonJs.Cameras;
using GraphicsComposerLib.Rendering.BabylonJs.Constants;
using GraphicsComposerLib.Rendering.BabylonJs.GUI;
using GraphicsComposerLib.Rendering.BabylonJs.Materials;
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
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Samples.Graphics.BabylonJs
{
    public static class BabylonJsSnapshotsSample
    {
        public const string WorkingPath 
            = @"D:\Projects\Study\Babylon.js\";
        
        public static GrImageBase64StringCache ImageCache { get; }
            = new GrImageBase64StringCache();
        

        public static void InitializeLaTeXPngCache(IEnumerable<double> tValues)
        {
            Console.Write("Generating LaTeX images .. ");

            var latexImageComposer = new GrLaTeXImageComposer()
            {
                LaTeXBinFolder = @"D:\texlive\2021\bin\win32\",
                Resolution = 300
            };

            ImageCache.MarginSize = 0;
            ImageCache.BackgroundColor = Color.FromRgba(255, 255, 255, 0);

            ImageCache.Clear();

            ImageCache.AddLaTeXEquation(
                "vText", 
                @"\boldsymbol{v} \left( t \right)"
            );

            ImageCache.AddLaTeXEquation(
                "v1Text", 
                @"v_{1} \left( t \right)"
            );
            
            ImageCache.AddLaTeXEquation(
                "v2Text", 
                @"v_{2} \left( t \right)"
            );
            
            ImageCache.AddLaTeXEquation(
                "v3Text", 
                @"v_{3} \left( t \right)"
            );

            ImageCache.AddLaTeXEquation(
                "kText", 
                @"\boldsymbol{k}"
            );

            ImageCache.MarginSize = 20;
            //ImageCache.BackgroundColor = Color.FromRgba(32, 32, 255, 16);

            ImageCache.AddLaTeXAlignedEquations(
                $"signalText", 
                new Pair<string>[]
                {
                    new (@"\boldsymbol{k}", @"\boldsymbol{\sigma}_{1} + \boldsymbol{\sigma}_{2} + \boldsymbol{\sigma}_{3}"),
                    new (@"\boldsymbol{v}\left( t \right)", @"v_{1}\left(t\right)\boldsymbol{\sigma}_{1}+v_{2}\left(t\right)\boldsymbol{\sigma}_{2}+v_{3}\left(t\right)\boldsymbol{\sigma}_{3}"),
                    new (@"v_{1}\left( t \right)", @"V \cos\left( \omega t \right)"),
                    new (@"v_{2}\left( t \right)", @"V \cos\left( \omega t + \frac{2\pi}{3} \right)"),
                    new (@"v_{3}\left( t \right)", @"V \cos\left( \omega t - \frac{2\pi}{3} \right)")
                }
            );

            var i = 0;
            foreach (var t in tValues)
            {
                ImageCache.AddLaTeXEquation(
                    $"tText-{i:D6}", 
                    @$"t = {t:F2}"
                );

                i++;
            }
            
            ImageCache.GeneratePngBase64Strings(latexImageComposer);

            Console.WriteLine("done.");
        }

        public static string GetBabylonJsCode(int index, Func<int, double> tFunc, Func<int, double> alphaFunc)
        {
            const double latexScalingFactor = 1d / 150d;
            const int gridUnitCount = 24;
            var t = tFunc(index);
            var alpha = alphaFunc(index);

            //var text =
            //    TextUtils.Concatenate(new[]
            //    {
            //        $@"Balanced 3-phase signal",
            //        $@"$\boldsymbol{{v}}\left(t\right)=v_{{1}}\left(t\right)\boldsymbol{{\sigma}}_{{1}}+v_{{2}}\left(t\right)\boldsymbol{{\sigma}}_{{2}}+v_{{3}}\left(t\right)\boldsymbol{{\sigma}}_{{3}}$",
            //        $@"$v_{{1}}\left(t\right)=V\cos\left(\omega t\right)$",
            //        $@"$v_{{2}}\left(t\right)=V\cos\left(\omega t+\frac{{2\pi}}{{3}}\right)$",
            //        $@"$v_{{3}}\left(t\right)=V\cos\left(\omega t-\frac{{2\pi}}{{3}}\right)$",
            //        $@"$t = {t:F2}$"
            //    }, $" <br>{Environment.NewLine}");

            const int canvasWidth = 1280;
            const int canvasHeight = 720;
            var htmlComposer = new GrBabylonJsHtmlComposer3D(
                new GrBabylonJsSceneComposer3D(
                    "scene",
                    new GrBabylonJsSnapshotSpecs
                    {
                        Enabled = false,
                        Width = canvasWidth,
                        Height = canvasHeight,
                        Precision = 1,
                        UsePrecision = true,
                        Delay = 1000,
                        FileName = $"Frame-{index:D6}.png"
                    }
                )
                {
                    BackgroundColor = Color.AliceBlue,
                    ShowDebugLayer = false,
                }
            )
            {
                CanvasWidth = canvasWidth,
                CanvasHeight = canvasHeight,
                CanvasFullScreen = false,
                //Text = text
            };

            var composer = htmlComposer.GetSceneComposer("scene");
            var scene = htmlComposer.GetScene("scene");

            //composer.SceneObject.SceneProperties.UseOrderIndependentTransparency = true;
            //composer.SceneObject.SceneProperties.AmbientColor = Color.AliceBlue;

            scene.AddArcRotateCamera(
                    "camera",
                    alpha, //"2 * Math.PI / 20",
                    "2 * Math.PI / 5",
                    15,
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

            scene.AddEnvironmentHelper(
                    "environmentHelper",

                    new GrBabylonJsEnvironmentHelper.EnvironmentHelperOptions
                    {
                        GroundYBias = 0.01,
                        SkyBoxColor = Color.LightSkyBlue,
                        GroundColor = Color.White,
                        CreateGround = true,
                        GroundSize = gridUnitCount - 1,
                        SkyBoxSize = gridUnitCount + 10
                    }
                );

            scene
                .AddSimpleMaterial("kMaterial", Color.SaddleBrown)
                .ParentScene.Value.AddSimpleMaterial("xMaterial", Color.Red)
                .ParentScene.Value.AddSimpleMaterial("yMaterial", Color.Green)
                .ParentScene.Value.AddSimpleMaterial("zMaterial", Color.Blue)
                .ParentScene.Value.AddSimpleMaterial("uMaterial", Color.Yellow)
                .ParentScene.Value.AddSimpleMaterial("curveMaterial", Color.SaddleBrown)
                .ParentScene.Value.AddStandardMaterial(
                    "discMaterial",

                    new GrBabylonJsStandardMaterial.StandardMaterialProperties
                    {
                        Alpha = 0.55,
                        Color = Color.BurlyWood,
                        TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend,
                        BackFaceCulling = true
                    }
                );
   
            
            const double magnitude = 4d;
            const double frequency = 0.1;
            
            var xAngle = 2d * Math.PI * frequency * t;
            var yAngle = 2d * Math.PI * frequency * t - 2d * Math.PI / 3;
            var zAngle = 2d * Math.PI * frequency * t + 2d * Math.PI / 3;

            var k = new Tuple3D(1, 1, 1);
            var x = new Tuple3D(magnitude * Math.Cos(xAngle), 0, 0);
            var y = new Tuple3D(0, magnitude * Math.Cos(yAngle), 0);
            var z = new Tuple3D(0, 0, magnitude * Math.Cos(zAngle));
            var v = x + y + z;
            
            var kVector = new GrVisualVector3D("kVector")
            {
                Origin = Tuple3D.Zero,
                Direction = k,
                Style = new GrVisualVectorStyle3D
                {
                    Material = scene.GetMaterial("kMaterial"),
                    Thickness = 0.075
                }
            };

            var xVector = new GrVisualVector3D("xVector")
            {
                Origin = Tuple3D.Zero,
                Direction = x,
                Style = new GrVisualVectorStyle3D
                {
                    Material = scene.GetMaterial("xMaterial"),
                    Thickness = 0.075
                }
            };

            var xTrailSegment = new GrVisualLineSegment3D("xTrailSegment")
            {
                Position1 = new Tuple3D(-magnitude, 0, 0),
                Position2 = new Tuple3D(magnitude, 0, 0),
                Style = new GrVisualCurveTubeStyle3D()
                {
                    Material = scene.GetMaterial("xMaterial"),
                    Thickness = 0.075 / 3
                }
            };

            var yVector = new GrVisualVector3D("yVector")
            {
                Origin = Tuple3D.Zero,
                Direction = y,
                Style = new GrVisualVectorStyle3D
                {
                    Material = scene.GetMaterial("yMaterial"),
                    Thickness = 0.075
                }
            };
            
            var yTrailSegment = new GrVisualLineSegment3D("yTrailSegment")
            {
                Position1 = new Tuple3D(0, -magnitude, 0),
                Position2 = new Tuple3D(0, magnitude, 0),
                Style = new GrVisualCurveTubeStyle3D()
                {
                    Material = scene.GetMaterial("yMaterial"),
                    Thickness = 0.075 / 3
                }
            };

            var zVector = new GrVisualVector3D("zVector")
            {
                Origin = Tuple3D.Zero,
                Direction = z,
                Style = new GrVisualVectorStyle3D
                {
                    Material = scene.GetMaterial("zMaterial"),
                    Thickness = 0.075
                }
            };
            
            var zTrailSegment = new GrVisualLineSegment3D("zTrailSegment")
            {
                Position1 = new Tuple3D(0, 0, -magnitude),
                Position2 = new Tuple3D(0, 0, magnitude),
                Style = new GrVisualCurveTubeStyle3D()
                {
                    Material = scene.GetMaterial("zMaterial"),
                    Thickness = 0.075 / 3
                }
            };

            var vVector = new GrVisualVector3D("uVector")
            {
                Origin = Tuple3D.Zero,
                Direction = v,
                Style = new GrVisualVectorStyle3D
                {
                    Material = scene.GetMaterial("uMaterial"),
                    Thickness = 0.075
                }
            };

            var dashedStyle = new GrVisualCurveDashedLineStyle3D
            {
                Color = Color.Gray,
                DashOn = 1,
                DashOff = 1,
                DashPerLine = 20
            };

            var xySegment1 = new GrVisualLineSegment3D("xySegment1")
            {
                Position1 = x + y,
                Position2 = x,
                Style = dashedStyle
            };
            
            var xySegment2 = new GrVisualLineSegment3D("xySegment2")
            {
                Position1 = x + y,
                Position2 = y,
                Style = dashedStyle
            };
            
            var xySegment3 = new GrVisualLineSegment3D("xySegment3")
            {
                Position1 = x + y,
                Position2 = v,
                Style = dashedStyle
            };
            
            var yzSegment1 = new GrVisualLineSegment3D("yzSegment1")
            {
                Position1 = y + z,
                Position2 = y,
                Style = dashedStyle
            };
            
            var yzSegment2 = new GrVisualLineSegment3D("yzSegment2")
            {
                Position1 = y + z,
                Position2 = z,
                Style = dashedStyle
            };
            
            var yzSegment3 = new GrVisualLineSegment3D("yzSegment3")
            {
                Position1 = y + z,
                Position2 = v,
                Style = dashedStyle
            };
            
            var zxSegment1 = new GrVisualLineSegment3D("zxSegment1")
            {
                Position1 = z + x,
                Position2 = z,
                Style = dashedStyle
            };
            
            var zxSegment2 = new GrVisualLineSegment3D("zxSegment2")
            {
                Position1 = z + x,
                Position2 = x,
                Style = dashedStyle
            };
            
            var zxSegment3 = new GrVisualLineSegment3D("zxSegment3")
            {
                Position1 = z + x,
                Position2 = v,
                Style = dashedStyle
            };
            
            var disc = new GrVisualCircleSurface3D("disc")
            {
                Center = Tuple3D.Zero,
                Radius = magnitude * 1.5d, //Math.Sqrt(3d / 2d),
                Normal = new Tuple3D(1, 1, 1).ToUnitVector(),
                Style = new GrVisualThickSurfaceStyle3D
                {
                    Material = scene.GetMaterial("discMaterial"),
                    Thickness = 0.025
                }
            };
            
            var ring = new GrVisualRingSurface3D("ring")
            {
                Center = Tuple3D.Zero,
                MinRadius = magnitude * Math.Sqrt(3d / 2d) - 0.5d,
                MaxRadius = magnitude * Math.Sqrt(3d / 2d) + 0.5d,
                Normal = new Tuple3D(1, 1, 1).ToUnitVector(),
                Style = new GrVisualThickSurfaceStyle3D
                {
                    Material = scene.GetMaterial("discMaterial"),
                    Thickness = 0.025
                }
            };

            var curve = new GrVisualCircleCurve3D("curve")
            {
                Center = Tuple3D.Zero,
                Radius = magnitude * Math.Sqrt(3d / 2d),
                Normal = new Tuple3D(1, 1, 1).ToUnitVector(),
                Style = new GrVisualCurveTubeStyle3D
                {
                    Material = scene.GetMaterial("curveMaterial"),
                    Thickness = 0.035
                }
            };

            var axisFrame = new GrVisualFrame3D("axisFrame")
            {
                Origin = new Tuple3D(1, 0, 7),
                Direction1 = Tuple3D.E1,
                Direction2 = Tuple3D.E2,
                Direction3 = Tuple3D.E3,
                Style = new GrVisualFrameStyle3D
                {
                    OriginThickness = 0.15,
                    DirectionThickness = 0.05,
                    OriginMaterial = scene.GetMaterial("kMaterial"),
                    DirectionMaterial1 = scene.GetMaterial("xMaterial"),
                    DirectionMaterial2 = scene.GetMaterial("yMaterial"),
                    DirectionMaterial3 = scene.GetMaterial("zMaterial")
                }
            };

            composer.AddElements(ring, curve, axisFrame);
            composer.AddElements(kVector, vVector);
            composer.AddElements(xVector, yVector, zVector);
            composer.AddElements(xTrailSegment, yTrailSegment, zTrailSegment);
            composer.AddElements(xySegment1, xySegment2, xySegment3);
            composer.AddElements(yzSegment1, yzSegment2, yzSegment3);
            composer.AddElements(zxSegment1, zxSegment2, zxSegment3);

            composer.GridMaterialKind = GrBabylonJsGridMaterialKind.TexturedMaterial;
            composer.AddXzSquareGrid(
                new GrVisualXzSquareGrid3D("grid")
                {
                    UnitCountX = gridUnitCount,
                    UnitCountZ = gridUnitCount,
                    UnitSize = 1,
                    Origin = new Tuple3D(-0.5d * gridUnitCount, 0, -0.5d * gridUnitCount),
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

            
            composer.AddLaTeXText(
                new GrVisualLaTeXText3D(ImageCache, "vText")
                {
                    ScalingFactor = latexScalingFactor,
                    Origin = v + v.ToUnitVector() * 0.5d
                }
            );
            
            composer.AddLaTeXText(
                new GrVisualLaTeXText3D(ImageCache, "v1Text")
                {
                    ScalingFactor = latexScalingFactor,
                    Origin = x + x.ToUnitVector() * 0.5d + Tuple3D.E2 * 0.5d
                }
            );
            
            composer.AddLaTeXText(
                new GrVisualLaTeXText3D(ImageCache, "v2Text")
                {
                    ScalingFactor = latexScalingFactor,
                    Origin = y + y.ToUnitVector() * 0.5d + Tuple3D.E3 * 0.5d
                }
            );

            composer.AddLaTeXText(
                new GrVisualLaTeXText3D(ImageCache, "v3Text")
                {
                    ScalingFactor = latexScalingFactor,
                    Origin = z + z.ToUnitVector() * 0.5d + Tuple3D.E2 * 0.5d
                }
            );
            
            composer.AddLaTeXText(
                new GrVisualLaTeXText3D(ImageCache, "kText")
                {
                    ScalingFactor = latexScalingFactor,
                    Origin = k + k.ToUnitVector() * 0.5d
                }
            );


            var uiTexture = scene.AddGuiFullScreenUi("uiTexture");

            var uiPanel = uiTexture.AddGuiStackPanel(
                "uiPanel",
                new GrBabylonJsGuiStackPanel.GuiStackPanelProperties
                {
                    IsVertical = true,
                    Spacing = 10,
                    //Color = Color.Blue,
                    BackgroundColor = Color.Beige.SetAlpha(24),
                    TopInPixels = 20,
                    LeftInPixels = 20,
                    WidthInPixels = ImageCache["signalText"].Width / 2 + 20,
                    HorizontalAlignment = GrBabylonJsHorizontalAlignment.Left,
                    VerticalAlignment = GrBabylonJsVerticalAlignment.Top
                }
            );
            
            uiPanel.AddGuiTextBlock(
                "uiTextTitle",
                "'Balanced 3-phase signal'",
                new GrBabylonJsGuiTextBlock.GuiTextBlockProperties
                {
                    ResizeToFit = true,
                    FontWeight = "'500'",
                    //Underline = true,
                    TextHorizontalAlignment = GrBabylonJsHorizontalAlignment.Left,
                    Color = Color.Black,
                    HeightInPixels = 50,
                    //OutlineColor = Color.Black,
                    //OutlineWidth = 2,
                    TopInPixels = 10,
                    LeftInPixels = 10,
                    HorizontalAlignment = GrBabylonJsHorizontalAlignment.Left,
                    VerticalAlignment = GrBabylonJsVerticalAlignment.Center,
                }
            );
            
            //uiPanel.AddGuiLine(
            //    "uiLine",
            //    new GrBabylonJsGuiLine.GuiLineProperties
            //    {

            //    }
            //);

            var latexPngData1 = ImageCache["signalText"];

            uiPanel.AddGuiImage(
                "latexGuiImage1",
                latexPngData1.GetBase64HtmlString(),
                new GrBabylonJsGuiImage.GuiImageProperties
                {
                    //Alpha = 0.5d,
                    WidthInPixels = latexPngData1.Width / 2,
                    HeightInPixels = latexPngData1.Height / 2,
                    TopInPixels = 10,
                    LeftInPixels = 10,
                    HorizontalAlignment = GrBabylonJsHorizontalAlignment.Left,
                    VerticalAlignment = GrBabylonJsVerticalAlignment.Center,
                }
            );
            
            var latexPngData2 = ImageCache[$"tText-{index:D6}"];
            
            uiPanel.AddGuiImage(
                "latexGuiImage2",
                latexPngData2.GetBase64HtmlString(),
                new GrBabylonJsGuiImage.GuiImageProperties
                {
                    //Alpha = 0.5d,
                    WidthInPixels = latexPngData2.Width / 2,
                    HeightInPixels = latexPngData2.Height / 2,
                    TopInPixels = 10,
                    LeftInPixels = 10,
                    HorizontalAlignment = GrBabylonJsHorizontalAlignment.Left,
                    VerticalAlignment = GrBabylonJsVerticalAlignment.Center,
                }
            );

            return htmlComposer.GetHtmlCode();
        }

        public static void TakeSnapshots()
        {
            const double timeSpan = 10d;
            const int stepCount = 500;
            const string workingPath = @"D:\Projects\Study\Babylon.js\";

            var tValues = 
                0d.GetLinearRange(
                    timeSpan, 
                    stepCount, 
                    true
                ).ToArray();

            var alphaValues =
                30d.DegreesToRadians().GetCosRange(
                    150d.DegreesToRadians(), 
                    stepCount,
                    1,
                    true
                ).ToArray();

            InitializeLaTeXPngCache(tValues);

            var generator = new GrBabylonJsSnapshotComposer3D(
                index => GetBabylonJsCode(
                    index, 
                    i => tValues[i],
                    i => alphaValues[i]
                )
            )
            {
                GenerateHtml = true,
                GeneratePng = true,
                GenerateAnimatedGif = true,
                AnimatedGifFrameDelay = (int) (timeSpan / stepCount),
                GenerateMp4 = true,
                Mp4FrameRate = stepCount / timeSpan,
                FrameCount = stepCount,
                WorkingPath = workingPath,
                HostUrl = @"http://localhost:5200/"
                //LiveReloadWebServer "D:/Projects/Study/Babylon.js/" --port 5200 --UseSsl False --LiveReloadEnabled False --OpenBrowser True
            };

            generator.GenerateSnapshots();
        }
    }
}
