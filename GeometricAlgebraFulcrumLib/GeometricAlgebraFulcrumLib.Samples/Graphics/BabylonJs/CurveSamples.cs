﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using GeometricAlgebraFulcrumLib.Applications.Graphics;
using GraphicsComposerLib.Geometry.ParametricShapes.Curves;
using GraphicsComposerLib.Geometry.ParametricShapes.Curves.Circles;
using GraphicsComposerLib.Geometry.ParametricShapes.Curves.Lines;
using GraphicsComposerLib.Geometry.ParametricShapes.Curves.Roulettes;
using GraphicsComposerLib.Rendering.BabylonJs;
using GraphicsComposerLib.Rendering.BabylonJs.Cameras;
using GraphicsComposerLib.Rendering.BabylonJs.Constants;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Grids;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Groups;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using Color = SixLabors.ImageSharp.Color;

namespace GeometricAlgebraFulcrumLib.Samples.Graphics.BabylonJs;

public static class CurveSamples
{
    private const string WorkingPath = @"D:\Projects\Study\Babylon.js";


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

        // Add reference unit axis frame
        var axisFrameOriginMaterial = scene.AddSimpleMaterial("axisFrameOriginMaterial", Color.DarkGray);
        var axisFrameXMaterial = scene.AddSimpleMaterial("axisFrameXMaterial", Color.Red);
        var axisFrameYMaterial = scene.AddSimpleMaterial("axisFrameYMaterial", Color.Green);
        var axisFrameZMaterial = scene.AddSimpleMaterial("axisFrameZMaterial", Color.Blue);

        var frameOrigin = Tuple3D.Zero;
        sceneComposer.AddElement(
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
                }
            }
        );

        const double tMin = 0d;
        const double tMax = 1d;

        const int fixedCurveFactor = 5;
        const int movingCurveFactor = 3;
        
        var fixedCurve = 
            new GrParametricCircleZx3D(fixedCurveFactor, false).GetRotatedNormalsCurve(
                t => PlanarAngle.Angle45
                //t => t.CosWave(0, 1 * Math.PI, 3) //-0.25 * Math.PI //t * 2 * Math.PI
            );

        var movingCurve =
            new GrParametricCircleZx3D(movingCurveFactor, false).GetRotatedNormalsCurve(
                t => 0
                //t => t.CosWave(-0.75 * Math.PI, 0.25 * Math.PI, 3) //-0.25 * Math.PI //t * 2 * Math.PI
            );
        
        var maxLength = 
            movingCurve.GetLength() * movingCurveFactor.Lcm(fixedCurveFactor) / movingCurveFactor;

        var curve = new GrRouletteCurve3D(
            fixedCurve, 
            movingCurve,
            movingCurve.GetPoint(movingCurve.ParameterValueMin),
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
            new GrParametricCircleZx3D(fixedCurveFactor, false).GetRotatedNormalsCurve(
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
            new GrParametricCircleZx3D(
                radius,
                false
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

        var t1 = 0d.Lerp(movingCurve.ParameterValueMin, movingCurve.ParameterValueMax);
        var t2 = (1d / 3d).Lerp(movingCurve.ParameterValueMin, movingCurve.ParameterValueMax);
        var t3 = (2d / 3d).Lerp(movingCurve.ParameterValueMin, movingCurve.ParameterValueMax);

        var generatorPointList = new List<RouletteTracerVisualizer3D.GeneratorPoint>
        {
            new(new Tuple3D(0, -1, 0), Color.Red),
            new(new Tuple3D(radius / 2, 0, 0), Color.Green),
            new(new Tuple3D(radius, 1, 0), Color.Blue),
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
            WorkingPath = @"D:\Projects\Study\Babylon.js\",
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

    private static GrRouletteCurve3D GetParametricRouletteCurve(double parameterValue)
    {
        const int fixedCurveFactor = 5;
        const int movingCurveFactor = 3;

        var fixedCurve =
            new GrParametricCircleZx3D(fixedCurveFactor, false).GetRotatedNormalsCurve(
                t => 0d.DegreesToAngle()
            );
        
        var movingCurveLength = 
            fixedCurve.GetLength() * movingCurveFactor / fixedCurveFactor;
        
        var radius = movingCurveLength / (2 * Math.PI);
        var movingCurve =
            new GrParametricCircleZx3D(
                radius,
                false
            ).GetRotatedNormalsCurve(
                t =>
                    -45.DegreesToAngle()
                    //(parameterValue * 360).DegreesToAngle()
            );

        var rouletteDistance =
            movingCurve.GetLength() * 
            movingCurveFactor.Lcm(fixedCurveFactor) / 
            movingCurveFactor;

        var generatorPointCurve = new GrArcLengthLineSegment3D(
            new Tuple3D(-radius, -radius, -radius),
            new Tuple3D(radius, radius, radius)
        ).GetMappedParameterCurveCosWave(2);

        var generatorPoint = 
            generatorPointCurve.GetPoint(parameterValue);

        return new GrRouletteCurve3D(
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
            WorkingPath = @"D:\Projects\Study\Babylon.js\",
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