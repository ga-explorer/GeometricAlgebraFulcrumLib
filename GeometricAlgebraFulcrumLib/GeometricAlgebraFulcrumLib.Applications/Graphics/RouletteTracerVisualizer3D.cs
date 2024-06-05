using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Files;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves.Adaptive;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves.Roulettes;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.GUI;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Maps.Space3D;
using SixLabors.ImageSharp;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;

namespace GeometricAlgebraFulcrumLib.Applications.Graphics;

public class RouletteTracerVisualizer3D :
    GrBabylonJsSnapshotComposer3D
{
    public sealed record GeneratorPoint(LinFloat64Vector3D Point, Color PointColor);

    
    public IArcLengthCurve3D FixedCurve { get; }

    public IArcLengthCurve3D MovingCurve { get; }
    
    public IReadOnlyList<GeneratorPoint> GeneratorPointList { get; }
    
    public int FixedCurveFrameCount { get; }
    
    public int MovingCurveFrameCount { get; }

    public double RouletteDistance { get; }

    private readonly List<RouletteCurve3D> _rouletteCurveList = new List<RouletteCurve3D>();
    public IReadOnlyList<RouletteCurve3D> RouletteCurveList 
        => _rouletteCurveList;

    private readonly List<AdaptiveCurve3D> _sampledCurveList = new List<AdaptiveCurve3D>();
    public IReadOnlyList<AdaptiveCurve3D> SampledCurveList 
        => _sampledCurveList;



    public RouletteTracerVisualizer3D(IReadOnlyList<double> cameraAlphaValues, IReadOnlyList<double> cameraBetaValues, IArcLengthCurve3D fixedCurve, IArcLengthCurve3D movingCurve, IReadOnlyList<GeneratorPoint> generatorPointList, double rouletteDistance, int fixedCurveFrameCount, int movingCurveFrameCount)
        : base(cameraAlphaValues, cameraBetaValues)
    {
        FixedCurve = fixedCurve;
        MovingCurve = movingCurve;
        RouletteDistance = rouletteDistance;
        FixedCurveFrameCount = fixedCurveFrameCount;
        MovingCurveFrameCount = movingCurveFrameCount;
        GeneratorPointList = generatorPointList;

        _rouletteCurveList.AddRange(
            GeneratorPointList.Select(p => 
                new RouletteCurve3D(FixedCurve, MovingCurve, p.Point, RouletteDistance)
            )
        );

        _sampledCurveList.AddRange(
            _rouletteCurveList.Select(curve =>
                curve.CreateAdaptiveCurve3D(
                    Float64ScalarRange.Create(0, rouletteDistance),
                    new AdaptiveCurveSamplingOptions3D(3.DegreesToDirectedAngle(), 3, 16)
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
        //        .Repeat(2 * Math.PI / 6, FrameCount)
        //        .CreateSignal(samplingRate);
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
            ShowDebugLayer = false,
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

    protected override void InitializeImageCache()
    {
        var workingPath = Path.Combine(WorkingFolder, "images");

        Console.Write("Generating images cache .. ");

        ImageCache.MarginSize = 0;
        ImageCache.BackgroundColor = Color.FromRgba(255, 255, 255, 0);

        ImageCache.AddPngFromFile(
            "copyright",
            workingPath.GetFilePath("Copyright-1.png")
        );
        
        Console.WriteLine("done.");
        Console.WriteLine();
    }
    
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
                copyrightImage.GetUrl(),
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
    }

    private void AddFixedCurve()
    {
        var (tMin, tMax) = 
            FixedCurve.ParameterRange;

        var tValues =
            tMin.ScalarValue.GetLinearRange(tMax, 501, false).ToImmutableArray();

        var tValuesFrames = 
            tMin.ScalarValue.GetLinearRange(tMax, FixedCurveFrameCount, false).ToImmutableArray();
        
        MainSceneComposer.AddParametricCurve(
            "fixedCurve",
            FixedCurve,
            tValues,
            tValuesFrames,
            Color.Red.SetAlpha(0.5f),
            0.035
        );
    }
    
    private RouletteMap3D GetRouletteMap(double parameterValue)
    {
        var t1 = MovingCurve.LengthToParameter(parameterValue);
        var movingFrame = MovingCurve.GetFrame(t1);

        var t2 = FixedCurve.LengthToParameter(parameterValue);
        var fixedFrame = FixedCurve.GetFrame(t2);

        var quaternion =
            movingFrame.CreateFrameToFrameRotationQuaternion(fixedFrame);

        return new RouletteMap3D(
            fixedFrame.Point,
            movingFrame.Point,
            quaternion
        );
    }

    private void AddMovingCurve(int index)
    {
        var (tMin, tMax) = 
            MovingCurve.ParameterRange;

        var tValues =
            tMin.ScalarValue.GetLinearRange(tMax, 501, false).ToImmutableArray();
        
        var tValuesFrames = 
            tMin.ScalarValue.GetLinearRange(tMax, MovingCurveFrameCount, false).ToImmutableArray();

        var t = RouletteDistance * index / (FrameCount - 1);
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

        var frame = FixedCurve.GetFrame(FixedCurve.LengthToParameter(t));
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
                GrVisualAnimationSpecs.Static
            )
        );
    }

    private void AddGeneratorPoints(int index)
    {
        var t = RouletteDistance * index / (FrameCount - 1);
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

    private void AddRouletteCurve(int index)
    {
        if (index < 1) return;
        
        var t = index / (double)(FrameCount - 1) * RouletteDistance;

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

    protected override GrBabylonJsHtmlComposer3D GenerateSnapshotCode(int index)
    {
        base.GenerateSnapshotCode(index);

        AddFixedCurve();
        AddMovingCurve(index);
        AddGeneratorPoints(index);
        AddRouletteCurve(index);

        return HtmlComposer;
    }
}