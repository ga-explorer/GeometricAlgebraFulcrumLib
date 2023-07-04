using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Applications.Graphics;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Circles;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Lines;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Roulettes;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using GraphicsComposerLib.Rendering.BabylonJs;
using GraphicsComposerLib.Rendering.BabylonJs.Cameras;
using GraphicsComposerLib.Rendering.BabylonJs.Constants;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Basic;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Grids;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Styles;
using Color = SixLabors.ImageSharp.Color;

namespace GeometricAlgebraFulcrumLib.Samples.Graphics.BabylonJs
{
    public static class CurveSamples
    {
        private const string WorkingPath = @"D:\Projects\Study\Web\Babylon.js";


        public static void Example1()
        {
            const int gridUnitCount = 20;
        
            var htmlComposer = new GrBabylonJsHtmlComposer3D("scene");

            var sceneComposer = htmlComposer.GetSceneComposer("scene");
            var scene = htmlComposer.GetScene("scene");

            scene.AddArcRotateCamera(
                "camera1",
                "2 * Math.PI / 20",
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
                    GroundSize = 8,
                    SkyBoxSize = gridUnitCount + 10
                }
            );

            // Add ground coordinates grid
            sceneComposer.GridMaterialKind =
                GrBabylonJsGridMaterialKind.TexturedMaterial;

            sceneComposer.AddXzSquareGrid(
                new GrVisualXzSquareGrid3D("grid")
                {
                    UnitCountX = gridUnitCount,
                    UnitCountZ = gridUnitCount,
                    UnitSize = 1,
                    Origin = Float64Vector3D.Create(-0.5d * gridUnitCount, 0, -0.5d * gridUnitCount),
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
            sceneComposer.AddElement(
                GrVisualFrame3D.CreateStatic(
                    "axisFrame",
                    new GrVisualFrameStyle3D
                    {
                        OriginStyle = 
                            scene
                                .AddSimpleMaterial("axisFrameOriginMaterial", Color.DarkGray)
                                .CreateThickSurfaceStyle(0.075),

                        Direction1Style = 
                            scene
                                .AddSimpleMaterial("axisFrameXMaterial", Color.Red)
                                .CreateTubeCurveStyle(0.035),

                        Direction2Style = 
                            scene
                                .AddSimpleMaterial("axisFrameYMaterial", Color.Green)
                                .CreateTubeCurveStyle(0.035),

                        Direction3Style = 
                            scene
                                .AddSimpleMaterial("axisFrameZMaterial", Color.Blue)
                                .CreateTubeCurveStyle(0.035)
                    },
                    Float64Vector3D.Zero
                )
            );

            const double tMin = 0d;
            const double tMax = 1d;

            const int fixedCurveFactor = 5;
            const int movingCurveFactor = 3;
        
            var fixedCurve = 
                new ParametricCircleZx3D(fixedCurveFactor, 1).GetRotatedNormalsCurve(
                    t => Float64PlanarAngle.Angle45
                    //t => t.CosWave(0, 1 * Math.PI, 3) //-0.25 * Math.PI //t * 2 * Math.PI
                );

            var movingCurve =
                new ParametricCircleZx3D(movingCurveFactor, 1).GetRotatedNormalsCurve(
                    t => 0
                    //t => t.CosWave(-0.75 * Math.PI, 0.25 * Math.PI, 3) //-0.25 * Math.PI //t * 2 * Math.PI
                );
        
            var maxLength = 
                movingCurve.GetLength() * movingCurveFactor.Lcm(fixedCurveFactor) / movingCurveFactor;

            var curve = new RouletteCurve3D(
                fixedCurve, 
                movingCurve,
                movingCurve.GetPoint(movingCurve.ParameterRange.MinValue),
                maxLength
            );

            var tValues =
                tMin.GetLinearRange(tMax, 501, false).ToImmutableArray();

            var tValuesMovingCurveFrames =
                tMin.GetLinearRange(tMax, movingCurveFactor * 10 + 1, false).ToImmutableArray();

            sceneComposer.AddParametricCurve(
                "curve1",
                movingCurve,
                tValues,
                tValuesMovingCurveFrames,
                Color.Orange,
                0.05
            );
        
            var tValuesFixedCurveFrames =
                tMin.GetLinearRange(tMax, fixedCurveFactor * 10 + 1, false).ToImmutableArray();

            sceneComposer.AddParametricCurve(
                "curve2",
                fixedCurve,
                tValues,
                tValuesFixedCurveFrames,
                Color.LawnGreen,
                0.05
            );
        
            var t1Values =
                0d.GetLinearRange(maxLength, 5001, false).ToImmutableArray();
        
            var t1ValuesFrames =
                0d.GetLinearRange(maxLength, 501, false).ToImmutableArray();

            sceneComposer.AddParametricCurve(
                "curve",
                curve,
                t1Values,
                t1ValuesFrames,
                Color.Blue,
                0.05
            );

            var sceneCode = htmlComposer.GetCreateScenesCode();
            var htmlCode = htmlComposer.GetHtmlCode();

            File.WriteAllText(
                Path.Combine(WorkingPath, "CurveSamples_Example1.html"),
                htmlCode
            );

            Console.WriteLine(sceneCode);
            Console.WriteLine();
        }

        public static void Example2()
        {
            const int frameCount = 1000;
            const int fixedCurveFactor = 5;
            const int movingCurveFactor = 3;

            var fixedCurve =
                new ParametricCircleZx3D(fixedCurveFactor, 1).GetRotatedNormalsCurve(
                    t => 45.DegreesToAngle()
                );

            //var harmonicCurve = new GrHarmonicCurve3D();

            //harmonicCurve.SetHarmonic(1, 3, 4, 2);
            //harmonicCurve.SetHarmonic(2, 0.6, 0.8, 0.4);
            //harmonicCurve.SetHarmonic(7, 0.3, 0.4, 0.2);

            //harmonicCurve.SetHarmonic(1, 5, 5, 5);
            //harmonicCurve.SetHarmonic(2, 0.65, 0.75, 0.35);
            //harmonicCurve.SetHarmonic(3, 0.1, 0.1, 0.05);
            //harmonicCurve.SetHarmonic(7, -0.6, -0.4, -0.5);

            //var fixedCurve = harmonicCurve.GetRotatedNormalsCurve(
            //    t => 0 // -PlanarAngle.Angle45
            //);

            var movingCurveLength = 
                fixedCurve.GetLength() * movingCurveFactor / fixedCurveFactor;

            //var movingCurve =
            //    new GrParametricLineSegment3D(
            //        new Tuple3D(0, 0, -movingCurveLength / 4), 
            //        new Tuple3D(0, 0, movingCurveLength / 4),
            //        true
            //    ).GetRotatedNormalsCurve(
            //        t => 2 * t * PlanarAngle.Angle360
            //        //t => t.CosWave(0, 360, 5).DegreesToAngle()
            //    );

            var radius = movingCurveLength / (2 * Math.PI);
            var movingCurve =
                new ParametricCircleZx3D(
                    radius,
                    1
                ).GetRotatedNormalsCurve(
                    t => 
                        //-PlanarAngle.Angle45
                        //5 * t * PlanarAngle.Angle360
                        t.CosWave(-45 + 10, -45 - 10, 3).DegreesToAngle()
                );

            var rouletteDistance =
                movingCurve.GetLength() * 
                movingCurveFactor.Lcm(fixedCurveFactor) / 
                movingCurveFactor;

            var t1 = 0d.Lerp(movingCurve.ParameterRange);
            var t2 = (1d / 3d).Lerp(movingCurve.ParameterRange);
            var t3 = (2d / 3d).Lerp(movingCurve.ParameterRange);

            var generatorPointList = new List<RouletteTracerVisualizer3D.GeneratorPoint>
            {
                new(Float64Vector3D.Create(0, -1, 0), Color.Red),
                new(Float64Vector3D.Create(radius / 2, 0, 0), Color.Green),
                new(Float64Vector3D.Create(radius, 1, 0), Color.Blue),
                //new(movingCurve.GetPoint(t1), Color.Red),
                //new(movingCurve.GetPoint(t2), Color.Green),
                //new(movingCurve.GetPoint(t3), Color.Blue),
            };
        
            var cameraAlphaValues =
                30d.DegreesToRadians().GetCosRange(
                    150d.DegreesToRadians(),
                    frameCount,
                    1,
                    true
                ).ToImmutableArray();

            var cameraBetaValues =
                Enumerable
                    .Repeat(2 * Math.PI / 6, frameCount)
                    .ToImmutableArray();

            var visualizer = new RouletteTracerVisualizer3D(
                cameraAlphaValues, 
                cameraBetaValues,
                fixedCurve,
                movingCurve,
                generatorPointList,
                rouletteDistance,
                fixedCurveFactor * 32 + 1,
                movingCurveFactor * 32 + 1
            )
            {
                Title = "Roulette Curve in 3D",
                WorkingFolder = @"D:\Projects\Study\Web\Babylon.js\",
                HostUrl = "http://localhost:5200/", 
                //LiveReloadWebServer "D:/Projects/Study/Babylon.js/" --port 5200 --UseSsl False --LiveReloadEnabled False --OpenBrowser True
            
                CameraDistance = 20,
                CameraRotationCount = 2,
                ShowCopyright = true,

                GenerateHtml = true,
                GeneratePng = true,
                GenerateMp4 = true,
            };

            visualizer.GenerateSnapshots();
        }

        private static RouletteCurve3D GetParametricRouletteCurve(double parameterValue)
        {
            const int fixedCurveFactor = 5;
            const int movingCurveFactor = 3;

            var fixedCurve =
                new ParametricCircleZx3D(fixedCurveFactor, 1).GetRotatedNormalsCurve(
                    t => 0d.DegreesToAngle()
                );
        
            var movingCurveLength = 
                fixedCurve.GetLength() * movingCurveFactor / fixedCurveFactor;
        
            var radius = movingCurveLength / (2 * Math.PI);
            var movingCurve =
                new ParametricCircleZx3D(
                    radius,
                    1
                ).GetRotatedNormalsCurve(
                    t =>
                        -45.DegreesToAngle()
                    //(parameterValue * 360).DegreesToAngle()
                );

            var rouletteDistance =
                movingCurve.GetLength() * 
                movingCurveFactor.Lcm(fixedCurveFactor) / 
                movingCurveFactor;

            var generatorPointCurve = new ArcLengthLineSegment3D(
                Float64Vector3D.Create(-radius, -radius, -radius),
                Float64Vector3D.Create(radius, radius, radius)
            ).GetMappedParameterCurveCosWave(2);

            var generatorPoint = 
                generatorPointCurve.GetPoint(parameterValue);

            return new RouletteCurve3D(
                fixedCurve, 
                movingCurve, 
                generatorPoint,
                rouletteDistance
            );
        }

        public static void Example3()
        {
            const int fixedCurveFactor = 5;
            const int movingCurveFactor = 3;
            const int frameCount = 500;

            var cameraAlphaValues =
                30d.DegreesToRadians().GetCosRange(
                    150d.DegreesToRadians(),
                    frameCount,
                    1,
                    true
                ).ToImmutableArray();

            var cameraBetaValues =
                Enumerable
                    .Repeat(2 * Math.PI / 8, frameCount)
                    .ToImmutableArray();

            var visualizer = new RouletteParametricVisualizer3D(
                cameraAlphaValues, 
                cameraBetaValues,
                GetParametricRouletteCurve,
                fixedCurveFactor * 32 + 1,
                movingCurveFactor * 32 + 1
            )
            {
                Title = "Parametric Roulette Curve in 3D-3",
                WorkingFolder = @"D:\Projects\Study\Web\Babylon.js\",
                HostUrl = "http://localhost:5200/", 
                //LiveReloadWebServer "D:/Projects/Study/Babylon.js/" --port 5200 --UseSsl False --LiveReloadEnabled False --OpenBrowser True
            
                CameraDistance = 20,
                CameraRotationCount = 2,
                ShowCopyright = true,

                GenerateHtml = true,
                GeneratePng = true,
                GenerateMp4 = true,
            };

            visualizer.GenerateSnapshots();
        }
    }
}