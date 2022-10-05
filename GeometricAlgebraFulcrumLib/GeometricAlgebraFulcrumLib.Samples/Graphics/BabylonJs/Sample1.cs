using System;
using System.Drawing;
using System.IO;
using GraphicsComposerLib.Geometry.ParametricShapes.Curves;
using GraphicsComposerLib.Geometry.ParametricShapes.Curves.Sampled;
using GraphicsComposerLib.Rendering.BabylonJs;
using GraphicsComposerLib.Rendering.BabylonJs.Materials;
using GraphicsComposerLib.Rendering.BabylonJs.Textures;
using GraphicsComposerLib.Rendering.Colors;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Basic;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Curves;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Groups;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Calculus;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using TextComposerLib;

namespace GeometricAlgebraFulcrumLib.Samples.Graphics.BabylonJs
{
    public static class Sample1
    {
        public const string WorkingPath = @"D:\Projects\Study\Babylon.js";

        public static void Example1()
        {
            const double thickness = 0.05d;

            var htmlComposer = new GrBabylonJsHtmlComposer3D("scene");

            var composer = htmlComposer.GetSceneComposer("scene");
            var scene = htmlComposer.GetScene("scene");

            scene.AddArcRotateCamera(
                "camera", 
                30d.DegreesToRadians(), 
                30d.DegreesToRadians(), 
                5, 
                Tuple3D.Zero
            );

            var grayMaterial = Color.Gray.ToBabylonJsStandardMaterial("grayMaterial");
            var redMaterial = Color.Red.ToBabylonJsStandardMaterial("redMaterial");
            var greenMaterial = Color.Green.ToBabylonJsStandardMaterial("greenMaterial");
            var blueMaterial = Color.Blue.ToBabylonJsStandardMaterial("blueMaterial");
            var yellowMaterial = Color.Yellow.ToBabylonJsStandardMaterial("yellowMaterial");
            var darkRedMaterial = Color.DarkRed.ToBabylonJsStandardMaterial("darkRedMaterial");
            var darkGreenMaterial = Color.DarkGreen.ToBabylonJsStandardMaterial("darkGreenMaterial");
            var darkBlueMaterial = Color.DarkBlue.ToBabylonJsStandardMaterial("darkBlueMaterial");
            var rosyBrownMaterial = Color.RosyBrown.ToBabylonJsStandardMaterial("rosyBrownMaterial");

            composer.AddMaterials(
                grayMaterial,
                redMaterial,
                greenMaterial,
                blueMaterial,
                yellowMaterial,
                darkRedMaterial,
                darkGreenMaterial,
                darkBlueMaterial,
                rosyBrownMaterial
            );

            var pointA = new GrVisualPoint3D("pointA")
            {
                Position = new Tuple3D(5, 0, 0),
                Style = new GrVisualThickSurfaceStyle3D
                {
                    Material = redMaterial,
                    Thickness = thickness * 2
                }
            };

            var pointB = new GrVisualPoint3D("pointB")
            {
                Position = new Tuple3D(0, 5, 0),
                Style = new GrVisualThickSurfaceStyle3D
                {
                    Material = greenMaterial,
                    Thickness = thickness * 2
                }
            };

            var pointC = new GrVisualPoint3D("pointC")
            {
                Position = new Tuple3D(0, 0, 5),
                Style = new GrVisualThickSurfaceStyle3D
                {
                    Material = blueMaterial,
                    Thickness = thickness * 2
                }
            };

            var worldFrame = new GrVisualFrame3D("worldFrame")
            {
                Origin = Tuple3D.Zero,
                Direction1 = Tuple3D.E1,
                Direction2 = Tuple3D.E2,
                Direction3 = Tuple3D.E3,
                Style = new GrVisualFrameStyle3D
                {
                    OriginMaterial = grayMaterial,
                    DirectionMaterial1 = darkRedMaterial,
                    DirectionMaterial2 = darkGreenMaterial,
                    DirectionMaterial3 = darkBlueMaterial,
                    OriginThickness = thickness * 1.5,
                    DirectionThickness = thickness
                }
            };

            var xAxis = new GrVisualVector3D("xAxis")
            {
                Origin = new Tuple3D(2, 0, 0),
                Direction = 3 * Tuple3D.E1,
                Style = new GrVisualVectorStyle3D
                {
                    Material = darkRedMaterial,
                    Thickness = thickness
                }
            };
            
            var yAxis = new GrVisualVector3D("yAxis")
            {
                Origin = new Tuple3D(0, 2, 0),
                Direction = 3 * Tuple3D.E2,
                Style = new GrVisualVectorStyle3D
                {
                    Material = darkGreenMaterial,
                    Thickness = thickness
                }
            };
            
            var zAxis = new GrVisualVector3D("zAxis")
            {
                Origin = new Tuple3D(0, 0, 2),
                Direction = 3 * Tuple3D.E3,
                Style = new GrVisualVectorStyle3D
                {
                    Material = darkBlueMaterial,
                    Thickness = thickness
                }
            };

            var lineSegment = new GrVisualLineSegment3D("lineSegment")
            {
                Position1 = new Tuple3D(1, 1, 1),
                Position2 = new Tuple3D(4, 4, 4),
                Style = new GrVisualCurveTubeStyle3D
                {
                    Material = yellowMaterial,
                    Thickness = thickness * 1.5d
                }
            };
            
            var lineSegmentX = new GrVisualLineSegment3D("lineSegmentX")
            {
                Position1 = new Tuple3D(4, 4, 4),
                Position2 = new Tuple3D(0, 4, 4),
                Style = new GrVisualCurveDashedLineStyle3D
                {
                    DashOn = 3,
                    DashOff = 1,
                    DashPerLine = 16,
                    Color = Color.Red.ToImageSharpColor()
                }
            };
            
            var lineSegmentY = new GrVisualLineSegment3D("lineSegmentY")
            {
                Position1 = new Tuple3D(4, 4, 4),
                Position2 = new Tuple3D(4, 0, 4),
                Style = new GrVisualCurveDashedLineStyle3D
                {
                    DashOn = 3,
                    DashOff = 1,
                    DashPerLine = 16,
                    Color = Color.Green.ToImageSharpColor()
                }
            };
            
            var lineSegmentZ = new GrVisualLineSegment3D("lineSegmentZ")
            {
                Position1 = new Tuple3D(4, 4, 4),
                Position2 = new Tuple3D(4, 4, 0),
                Style = new GrVisualCurveDashedLineStyle3D
                {
                    DashOn = 3,
                    DashOff = 1,
                    DashPerLine = 16,
                    Color = Color.Blue.ToImageSharpColor()
                }
            };

            var rightAngleX = new GrVisualRightAngle3D("angleXy")
            {
                Center = new Tuple3D(4, 4, 4),
                Direction1 = new Tuple3D(-1, 0, 0),
                Direction2 = new Tuple3D(0, -1, 0),
                Radius = 0.25,
                Style = new GrVisualCurveSolidLineStyle3D
                {
                    Color = Color.Red.ToImageSharpColor()
                }
            };

            var circleCurve = new GrVisualCircleCurve3D("circleCurve")
            {
                Center = new Tuple3D(1, 1, 1),
                Normal = new Tuple3D(1, 1, 1),
                Radius = 3,
                Style = new GrVisualCurveTubeStyle3D()
                {
                    Material = yellowMaterial,
                    Thickness = 2 * thickness
                }
            };
            
            var circleCurveArc = new GrVisualCircleCurveArc3D("circleCurveArc")
            {
                InnerArc = true,
                Center = new Tuple3D(0, 0, 0),
                Direction1 = new Tuple3D(1, 1, 1),
                Direction2 = new Tuple3D(0, 1, 0),
                Radius = 4,
                Style = new GrVisualCurveTubeStyle3D()
                {
                    Thickness = 2 * thickness,
                    Material = yellowMaterial
                }
            };
            
            var circleDisk = new GrVisualCircleSurface3D("circleDisk")
            {
                Center = new Tuple3D(1, 1, 1),
                Normal = new Tuple3D(1, 1, 1),
                Radius = 4,
                Style = new GrVisualThinSurfaceStyle3D
                {
                    Material = rosyBrownMaterial
                }
            };

            var circleDiskArc = new GrVisualCircleSurfaceArc3D("circleDiskArc")
            {
                InnerArc = true,
                Center = new Tuple3D(0, 0, 0),
                Direction1 = new Tuple3D(1, 1, 1),
                Direction2 = new Tuple3D(0, 1, 0),
                Radius = 4,
                Style = new GrVisualThinSurfaceStyle3D
                {
                    Material = rosyBrownMaterial
                }
            };
            
            composer.AddElements(
                worldFrame,
                pointA,
                pointB,
                pointC,
                xAxis,
                yAxis,
                zAxis,
                lineSegment,
                lineSegmentX,
                lineSegmentY,
                lineSegmentZ,
                rightAngleX,
                //circleCurve,
                circleCurveArc,
                //circleDisk,
                circleDiskArc
            );

            var sceneCode = htmlComposer.GetCreateScenesCode();
            var htmlCode = htmlComposer.GetHtmlCode();

            File.WriteAllText(@"D:\Projects\Study\Babylon.js\Sample1.html", htmlCode);

            Console.WriteLine(sceneCode);
            Console.WriteLine();
        }

        public static void Example2()
        {
            const double thickness = 0.05d;

            var htmlComposer = new GrBabylonJsHtmlComposer3D("scene");

            var composer = htmlComposer.GetSceneComposer("scene");
            var scene = htmlComposer.GetScene("scene");

            //scene.Properties.UseOrderIndependentTransparency = true;

            scene.AddArcRotateCamera(
                "camera", 
                30d.DegreesToRadians(), 
                30d.DegreesToRadians(), 
                5, 
                Tuple3D.Zero
            );

            var grayMaterial = Color.Gray.ToBabylonJsSimpleMaterial("grayMaterial");
            var redMaterial = Color.Red.ToBabylonJsSimpleMaterial("redMaterial");
            var greenMaterial = Color.Green.ToBabylonJsSimpleMaterial("greenMaterial");
            var blueMaterial = Color.Blue.ToBabylonJsSimpleMaterial("blueMaterial");
            var yellowMaterial = Color.Yellow.ToBabylonJsSimpleMaterial("yellowMaterial");
            var darkRedMaterial = Color.DarkRed.ToBabylonJsSimpleMaterial("darkRedMaterial");
            var darkGreenMaterial = Color.DarkGreen.ToBabylonJsSimpleMaterial("darkGreenMaterial");
            var darkBlueMaterial = Color.DarkBlue.ToBabylonJsSimpleMaterial("darkBlueMaterial");
            var rosyBrownMaterial = Color.RosyBrown.ToBabylonJsSimpleMaterial("rosyBrownMaterial");

            composer.AddMaterials(
                grayMaterial,
                redMaterial,
                greenMaterial,
                blueMaterial,
                yellowMaterial,
                darkRedMaterial,
                darkGreenMaterial,
                darkBlueMaterial,
                rosyBrownMaterial
            );
            
            var worldFrame = new GrVisualFrame3D("worldFrame")
            {
                Origin = Tuple3D.Zero,
                Direction1 = Tuple3D.E1,
                Direction2 = Tuple3D.E2,
                Direction3 = Tuple3D.E3,
                Style = new GrVisualFrameStyle3D
                {
                    OriginMaterial = grayMaterial,
                    DirectionMaterial1 = darkRedMaterial,
                    DirectionMaterial2 = darkGreenMaterial,
                    DirectionMaterial3 = darkBlueMaterial,
                    OriginThickness = thickness * 1.5,
                    DirectionThickness = thickness
                }
            };

            const string curveOpacityTextureName = "curveOpacityTexture";
            scene.AddTexture(
                curveOpacityTextureName, 

                @"./Textures/opacityTexture2.png".ValueToQuotedLiteral(),

                new GrBabylonJsTexture.TextureProperties
                {
                    HasAlpha = true,
                    UScale = 1, 
                    VScale = 6
                }
            );

            const string curveMaterialName = "curveMaterial";
            scene.AddStandardMaterial(
                curveMaterialName,

                new GrBabylonJsStandardMaterial.StandardMaterialProperties()
                {
                    DiffuseColor = Color.RosyBrown,
                    SpecularColor = Color.RosyBrown,
                    BackFaceCulling = true,
                    OpacityTexture = curveOpacityTextureName
                }
            );

            var curvePointList = new[]
            {
                new Tuple3D(1.5, 0, 0),
                new Tuple3D(0, 1.5, 0),
                new Tuple3D(0, 0, 1.5)
            };

            var sampledCurve =
                curvePointList.CreateCatmullRomSpline3D(
                    CatmullRomSplineType.Centripetal, 
                    true
                ).CreateSampledCurve3D(
                    new GrParametricCurveTreeOptions3D(
                        5d.DegreesToAngle(), 
                        0, 
                        10
                    )
                );

            var curve = new GrVisualLineCurve3D("curve")
            {
                PositionList = sampledCurve,
                Style = new GrVisualCurveTubeStyle3D()
                {
                    Material = scene.GetMaterial(curveMaterialName),
                    Thickness = 2 * thickness
                }
            };

            composer.AddElements(
                worldFrame,
                curve
            );

            var sceneCode = htmlComposer.GetCreateScenesCode();
            var htmlCode = htmlComposer.GetHtmlCode();

            File.WriteAllText(
                Path.Combine(WorkingPath, "Frame.html"),
                htmlCode
            );

            Console.WriteLine(sceneCode);
        }
    }
}
