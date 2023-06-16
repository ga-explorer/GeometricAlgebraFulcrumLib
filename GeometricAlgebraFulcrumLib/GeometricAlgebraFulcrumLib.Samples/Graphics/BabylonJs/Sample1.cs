using System;
using System.IO;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Adaptive;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using GraphicsComposerLib.Rendering.BabylonJs;
using GraphicsComposerLib.Rendering.BabylonJs.Materials;
using GraphicsComposerLib.Rendering.BabylonJs.Textures;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Animations;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Basic;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Curves;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Styles;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;
using SixLabors.ImageSharp;
using TextComposerLib;

namespace GeometricAlgebraFulcrumLib.Samples.Graphics.BabylonJs
{
    public static class Sample1
    {
        private const string WorkingPath = @"D:\Projects\Study\Web\Babylon.js";


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
                Float64Vector3D.Zero
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

            var pointA = GrVisualPoint3D.CreateStatic(
                "pointA", 
                redMaterial.CreateThickSurfaceStyle(thickness * 2), 
                Float64Vector3D.Create(5, 0, 0)
            );

            var pointB = GrVisualPoint3D.CreateStatic(
                "pointB", 
                greenMaterial.CreateThickSurfaceStyle(thickness * 2),
                Float64Vector3D.Create(0, 5, 0)
            );

            var pointC = GrVisualPoint3D.CreateStatic(
                "pointC", 
                blueMaterial.CreateThickSurfaceStyle(thickness * 2),
                Float64Vector3D.Create(0, 0, 5)
            );

            var worldFrame = GrVisualFrame3D.CreateStatic(
                "worldFrame",
                new GrVisualFrameStyle3D
                {
                    OriginStyle = grayMaterial.CreateThickSurfaceStyle(thickness * 1.5),
                    Direction1Style = darkRedMaterial.CreateTubeCurveStyle(thickness),
                    Direction2Style = darkGreenMaterial.CreateTubeCurveStyle(thickness),
                    Direction3Style = darkBlueMaterial.CreateTubeCurveStyle(thickness)
                },
                Float64Vector3D.Zero
            );

            var xAxis = GrVisualVector3D.CreateStatic(
                "xAxis", 
                darkRedMaterial.CreateTubeCurveStyle(thickness), 
                Float64Vector3D.Create(2, 0, 0), 
                3 * Float64Vector3D.E1
            );
            
            var yAxis = GrVisualVector3D.CreateStatic(
                "yAxis", 
                darkGreenMaterial.CreateTubeCurveStyle(thickness), 
                Float64Vector3D.Create(0, 2, 0), 
                3 * Float64Vector3D.E2
            );
            
            var zAxis = GrVisualVector3D.CreateStatic(
                "zAxis", 
                darkBlueMaterial.CreateTubeCurveStyle(thickness), 
                Float64Vector3D.Create(0, 0, 2), 
                3 * Float64Vector3D.E3
            );

            var lineSegment = GrVisualLineSegment3D.Create(
                "lineSegment", 
                yellowMaterial.CreateTubeCurveStyle(thickness * 1.5d),
                Float64Vector3D.Create(1, 1, 1), 
                Float64Vector3D.Create(4, 4, 4),
                GrVisualAnimationSpecs.Static
            );
            
            var lineSegmentX = GrVisualLineSegment3D.Create(
                "lineSegmentX",
                Color.Red.CreateDashedLineCurveStyle(3, 1, 16),
                Float64Vector3D.Create(4, 4, 4),
                Float64Vector3D.Create(0, 4, 4),
                GrVisualAnimationSpecs.Static
            );
            
            var lineSegmentY = GrVisualLineSegment3D.Create(
                "lineSegmentY",
                Color.Green.CreateDashedLineCurveStyle(3, 1, 16),
                Float64Vector3D.Create(4, 4, 4),
                Float64Vector3D.Create(4, 0, 4),
                GrVisualAnimationSpecs.Static
            );
            
            var lineSegmentZ = GrVisualLineSegment3D.Create(
                "lineSegmentZ",
                Color.Blue.CreateDashedLineCurveStyle(3, 1, 16),
                Float64Vector3D.Create(4, 4, 4),
                Float64Vector3D.Create(4, 4, 0),
                GrVisualAnimationSpecs.Static
            );

            var rightAngleX = GrVisualRightAngle3D.CreateStatic(
                "angleXy",
                Color.Red.CreateSolidLineCurveStyle(),
                scene.AddSimpleMaterial("angleXyMaterial", Color.Red).CreateThinSurfaceStyle(),
                Float64Vector3D.Create(4, 4, 4),
                Float64Vector3D.Create(-1, 0, 0),
                Float64Vector3D.Create(0, -1, 0),
                0.25
            );

            var circleCurve = GrVisualCircleCurve3D.CreateStatic(
                "circleCurve",
                yellowMaterial.CreateTubeCurveStyle(2 * thickness),
                Float64Vector3D.Create(1, 1, 1),
                Float64Vector3D.Create(1, 1, 1),
                3
            );
            
            var circleCurveArc = GrVisualCircleArcCurve3D.CreateStatic(
                "circleCurveArc",
                yellowMaterial.CreateTubeCurveStyle(2 * thickness),
                Float64Vector3D.Zero,
                Float64Vector3D.Symmetric,
                Float64Vector3D.E2,
                4,
                true
            );
            
            var circleDisk = GrVisualCircleSurface3D.CreateStatic(
                "circleDisk",
                rosyBrownMaterial.CreateThinSurfaceStyle(),
                Float64Vector3D.Create(1, 1, 1),
                Float64Vector3D.Create(1, 1, 1),
                4,
                false
            );

            var circleDiskArc = GrVisualCircleArcSurface3D.CreateStatic(
                "circleDiskArc",
                rosyBrownMaterial.CreateThinSurfaceStyle(),
                Float64Vector3D.Zero,
                Float64Vector3D.Symmetric,
                Float64Vector3D.E2,
                4d,
                true,
                false
            );
            
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

            File.WriteAllText(
                Path.Combine(WorkingPath, "Sample1.html"), 
                htmlCode
            );

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
                Float64Vector3D.Zero
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

            var worldFrame = GrVisualFrame3D.CreateStatic(
                "worldFrame",
                new GrVisualFrameStyle3D
                {
                    OriginStyle = grayMaterial.CreateThickSurfaceStyle(thickness * 1.5),
                    Direction1Style = darkRedMaterial.CreateTubeCurveStyle(thickness),
                    Direction2Style = darkGreenMaterial.CreateTubeCurveStyle(thickness),
                    Direction3Style = darkBlueMaterial.CreateTubeCurveStyle(thickness)
                },
                Float64Vector3D.Zero
            );

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
                Float64Vector3D.Create(1.5, 0, 0),
                Float64Vector3D.Create(0, 1.5, 0),
                Float64Vector3D.Create(0, 0, 1.5)
            };

            var sampledCurve =
                curvePointList.CreateCatmullRomSpline3D(
                    CatmullRomSplineType.Centripetal, 
                    true
                ).CreateAdaptiveCurve3D(
                    new AdaptiveCurveSamplingOptions3D(
                        5d.DegreesToAngle(), 
                        0, 
                        10
                    )
                );

            var curve = GrVisualPointPathCurve3D.CreateStatic(
                "curve", 
                scene.GetMaterial(curveMaterialName).CreateTubeCurveStyle(2 * thickness),
                sampledCurve
            );

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
