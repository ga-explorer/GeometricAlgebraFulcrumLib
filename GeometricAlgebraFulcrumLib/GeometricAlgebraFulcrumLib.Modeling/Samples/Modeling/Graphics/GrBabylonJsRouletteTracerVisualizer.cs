﻿using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Adaptive;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Graphics;

public class GrBabylonJsRouletteTracerVisualizer :
    GrBabylonJsSceneSequenceComposer
{
    public sealed record GeneratorPoint(LinFloat64Vector3D Point, Color PointColor);

    
    public Float64ArcLengthPath3D FixedCurve { get; }

    public Float64ArcLengthPath3D MovingCurve { get; }
    
    public IReadOnlyList<GeneratorPoint> GeneratorPointList { get; }
    
    public int FixedCurveFrameCount { get; }
    
    public int MovingCurveFrameCount { get; }

    public double RouletteDistance { get; }

    private readonly List<Float64RoulettePath3D> _rouletteCurveList = new List<Float64RoulettePath3D>();
    public IReadOnlyList<Float64RoulettePath3D> RouletteCurveList 
        => _rouletteCurveList;

    private readonly List<Float64AdaptivePath3D> _sampledCurveList = new List<Float64AdaptivePath3D>();
    public IReadOnlyList<Float64AdaptivePath3D> SampledCurveList 
        => _sampledCurveList;


    public GrBabylonJsRouletteTracerVisualizer(string workingFolder, Float64SamplingSpecs samplingSpecs, Float64ArcLengthPath3D fixedCurve, Float64ArcLengthPath3D movingCurve, IReadOnlyList<GeneratorPoint> generatorPointList, double rouletteDistance, int fixedCurveFrameCount, int movingCurveFrameCount)
        : base(workingFolder, samplingSpecs)
    {
        FixedCurve = fixedCurve;
        MovingCurve = movingCurve;
        RouletteDistance = rouletteDistance;
        FixedCurveFrameCount = fixedCurveFrameCount;
        MovingCurveFrameCount = movingCurveFrameCount;
        GeneratorPointList = generatorPointList;

        _rouletteCurveList.AddRange(
            GeneratorPointList.Select(p => 
                new Float64RoulettePath3D(
                    FixedCurve.IsPeriodic, 
                    FixedCurve, 
                    MovingCurve, 
                    p.Point, 
                    RouletteDistance
                )
            )
        );

        _sampledCurveList.AddRange(
            _rouletteCurveList.Select(curve =>
                curve.CreateAdaptiveCurve3D(
                    Float64ScalarRange.Create(0, rouletteDistance),
                    new Float64AdaptivePath3DSamplingOptions(3.DegreesToDirectedAngle(), 3, 16)
                )
            )
        );
        
        //CameraAlphaValues =
        //    30d.DegreesToRadians().GetCosRange(
        //        150d.DegreesToRadians(),
        //        FrameCount,
        //        CameraRotationCount,
        //        true
        //    ).CreateSignal(samplingRate);

        //CameraBetaValues =
        //    Enumerable
        //        .Repeat(Math.Tau / 6, FrameCount)
        //        .CreateSignal(samplingRate);
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
            ShowDebugLayer = false,
        };

        //mainSceneComposer.SceneObject.SceneProperties.UseOrderIndependentTransparency = true;

        CodeFilesComposer = new GrBabylonJsCodeFilesComposer(mainSceneComposer)
        {
            CanvasWidth = ImageWidth,
            CanvasHeight = ImageHeight,
            CanvasFullScreen = false
        };
    }

    protected override void AddImageTextures()
    {
        
    }
    
    protected override void AddGuiLayer(int index)
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
    }

    private void AddFixedCurve()
    {
        var (tMin, tMax) = 
            FixedCurve.TimeRange;

        var tValues =
            tMin.GetLinearRange(tMax, 501, false).ToImmutableArray();

        var tValuesFrames = 
            tMin.GetLinearRange(tMax, FixedCurveFrameCount, false).ToImmutableArray();
        
        MainSceneComposer.AddParametricCurve(
            "fixedCurve",
            FixedCurve,
            tValues,
            tValuesFrames,
            Color.Red.SetAlpha(0.5f),
            0.035
        );
    }
    
    private Float64RouletteAffineMap3D GetRouletteMap(double parameterValue)
    {
        var t1 = MovingCurve.LengthToTime(parameterValue);
        var movingFrame = MovingCurve.GetFrame(t1);

        var t2 = FixedCurve.LengthToTime(parameterValue);
        var fixedFrame = FixedCurve.GetFrame(t2);

        var quaternion =
            movingFrame.FrameToFrameRotationQuaternion(fixedFrame);

        return new Float64RouletteAffineMap3D(
            fixedFrame.Point,
            movingFrame.Point,
            quaternion
        );
    }

    private void AddMovingCurve(int frameIndex)
    {
        var (tMin, tMax) = 
            MovingCurve.TimeRange;

        var tValues =
            tMin.GetLinearRange(tMax, 501, false).ToImmutableArray();
        
        var tValuesFrames = 
            tMin.GetLinearRange(tMax, MovingCurveFrameCount, false).ToImmutableArray();

        var t = RouletteDistance * frameIndex / (ImageCount - 1);
        var rouletteMap = GetRouletteMap(t);
        var movingCurve = MovingCurve.GetRouletteMappedCurve(rouletteMap);

        var scene = MainSceneComposer.SceneObject;

        MainSceneComposer.AddParametricCurve(
            "movingCurve",
            movingCurve,
            tValues,
            tValuesFrames,
            Color.Green.SetAlpha(0.5f),
            0.035
        );

        var frame = FixedCurve.GetFrame(FixedCurve.LengthToTime(t));
        MainSceneComposer.AddElement(
            GrVisualFrame3D.Create(
                "frame",
                new GrVisualFrameStyle3D
                {
                    OriginStyle = 
                        scene
                            .AddSimpleMaterial("frameOrigin", Color.Gray)
                            .CreateThickSurfaceStyle(0.075),

                    Direction1Style = 
                        scene
                            .AddSimpleMaterial("frameTangent", Color.DarkRed)
                            .CreateTubeCurveStyle(0.035),

                    Direction2Style = 
                        scene
                            .AddSimpleMaterial("frameNormal1", Color.DarkGreen)
                            .CreateTubeCurveStyle(0.035),

                    Direction3Style = 
                        scene
                            .AddSimpleMaterial("frameNormal2", Color.DarkBlue)
                            .CreateTubeCurveStyle(0.035)
                },
                frame.Point,
                frame.Tangent,
                frame.Normal1,
                frame.Normal2,
                Float64SamplingSpecs.Static
            )
        );
    }

    private void AddGeneratorPoints(int frameIndex)
    {
        var t = RouletteDistance * frameIndex / (ImageCount - 1);
        var rouletteMap = GetRouletteMap(t);

        var scene = MainSceneComposer.SceneObject;

        var material = scene.AddStandardMaterial(
            "generatorPointMaterial", 
            Color.Orange
        );

        var i = 0;
        foreach (var visualPoint in GeneratorPointList)
        {
            MainSceneComposer.AddPoint(
                $"generatorPoint{i}",
                rouletteMap.MapPoint(visualPoint.Point),
                material, 0.15
            );

            i++;
        }
    }

    private void AddRouletteCurve(int frameIndex)
    {
        if (frameIndex < 1) return;
        
        var t = frameIndex / (double)(ImageCount - 1) * RouletteDistance;

        for (var i = 0; i < GeneratorPointList.Count; i++)
        {
            var pointList =
                SampledCurveList[i].GetPoints(t).ToImmutableArray();

            MainSceneComposer.AddLinePath(
                $"rouletteCurve{i}",
                pointList,
                GeneratorPointList[i].PointColor,
                0.025
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



        AddFixedCurve();
        AddMovingCurve(frameIndex);
        AddGeneratorPoints(frameIndex);
        AddRouletteCurve(frameIndex);
    }
}