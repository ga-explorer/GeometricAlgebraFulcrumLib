using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Adaptive;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Composers;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Graphics;

public class GrBabylonJsRouletteParametricVisualizer :
    GrBabylonJsSceneSequenceComposer
{
    private Float64RoulettePath3D? _activeCurve;


    public Func<double, Float64RoulettePath3D> CurveFunction { get; }
    
    public int FixedCurveFrameCount { get; }
    
    public int MovingCurveFrameCount { get; }


    public GrBabylonJsRouletteParametricVisualizer(string workingFolder, Float64SamplingSpecs samplingSpecs, Func<double, Float64RoulettePath3D> curveFunction, int fixedCurveFrameCount, int movingCurveFrameCount)
        : base(workingFolder, samplingSpecs)
    {
        CurveFunction = curveFunction;
        FixedCurveFrameCount = fixedCurveFrameCount;
        MovingCurveFrameCount = movingCurveFrameCount;
    } 

    
    protected override void InitializeSceneComposers(int index)
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
                FileName = GetFrameName(index) + ".png"
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
    }

    private void AddFixedCurve()
    {
        if (_activeCurve is null)
            throw new NullReferenceException();

        var (tMin, tMax) = 
            _activeCurve.FixedCurve.TimeRange;
            
        var tValues =
            tMin.GetLinearRange(tMax, 501, false).ToImmutableArray();

        var tValuesFrames = 
            tMin.GetLinearRange(tMax, FixedCurveFrameCount, false).ToImmutableArray();
        
        MainSceneComposer.AddParametricCurve(
            "fixedCurve", 
            _activeCurve.FixedCurve, 
            tValues, 
            tValuesFrames,
            Color.DarkRed,
            0.035
        );
    }
    
    private void AddMovingCurve()
    {
        if (_activeCurve is null)
            throw new NullReferenceException();

        var (tMin, tMax) = 
            _activeCurve.MovingCurve.TimeRange;
            
        var tValues =
            tMin.GetLinearRange(tMax, 501, false).ToImmutableArray();
            
        var tValuesFrames = 
            tMin.GetLinearRange(tMax, MovingCurveFrameCount, false).ToImmutableArray();
            
        MainSceneComposer.AddParametricCurve(
            "movingCurve",
            _activeCurve.MovingCurve,
            tValues,
            tValuesFrames,
            Color.DarkGreen,
            0.035
        );

        //var frame = _activeCurve.FixedCurve.GetFrame(_activeCurve.FixedCurve.LengthToTime(t));
        //MainSceneComposer.AddElement(
        //    new GrVisualFrame3D("frame")
        //    {
        //        Origin = frame.Point,

        //        Direction1 = frame.Tangent,
        //        Direction2 = frame.Normal1,
        //        Direction3 = frame.Normal2,

        //        Style = new GrVisualFrameStyle3D
        //        {
        //            OriginThickness = 0.075,
        //            DirectionThickness = 0.035,
        //            OriginMaterial = scene.AddSimpleMaterial("frameOrigin", Color.Gray),
        //            DirectionMaterial1 = scene.AddSimpleMaterial("frameTangent", Color.DarkRed),
        //            DirectionMaterial2 = scene.AddSimpleMaterial("frameNormal1", Color.DarkGreen),
        //            DirectionMaterial3 = scene.AddSimpleMaterial("frameNormal2", Color.DarkBlue)
        //        }
        //    }
        //);
    }

    private void AddGeneratorPoint()
    {
        if (_activeCurve is null)
            throw new NullReferenceException();

        var scene = MainSceneComposer.SceneObject;

        var material = scene.AddStandardMaterial(
            "generatorPointMaterial", 
            Color.Blue
        );

        MainSceneComposer.AddPoint(
            "generatorPoint", 
            _activeCurve.GeneratorPoint, 
            material, 
            0.2
        );
    }

    private void AddRouletteCurve()
    {
        if (_activeCurve is null)
            throw new NullReferenceException();
        
        var sampledCurve = _activeCurve.CreateAdaptiveCurve3D(
            _activeCurve.TimeRange, 
            new Float64AdaptivePath3DSamplingOptions(5.DegreesToDirectedAngle(), 3, 16)
        );

        var pointList =
            sampledCurve.Curve.GetPoints().ToImmutableArray();

        MainSceneComposer.AddLinePath(
            "rouletteCurve",
            pointList,
            Color.Blue,
            0.08
        );
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

        _activeCurve = CurveFunction(frameIndex / (ImageCount - 1d));

        AddFixedCurve();
        AddMovingCurve();
        AddGeneratorPoint();
        AddRouletteCurve();
    }
}