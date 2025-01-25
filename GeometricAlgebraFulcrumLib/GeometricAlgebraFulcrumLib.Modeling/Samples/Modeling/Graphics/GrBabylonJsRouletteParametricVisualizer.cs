using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves.Adaptive;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves.Roulettes;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Graphics;

public class GrBabylonJsRouletteParametricVisualizer :
    GrBabylonJsSceneSequenceComposer
{
    private RouletteCurve3D? _activeCurve;


    public Func<double, RouletteCurve3D> CurveFunction { get; }
    
    public int FixedCurveFrameCount { get; }
    
    public int MovingCurveFrameCount { get; }


    public GrBabylonJsRouletteParametricVisualizer(string workingFolder, Float64SamplingSpecs samplingSpecs, Func<double, RouletteCurve3D> curveFunction, int fixedCurveFrameCount, int movingCurveFrameCount)
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

        CodeFilesComposer = new GrBabylonJsCodeFilesComposer(mainSceneComposer)
        {
            CanvasWidth = CanvasWidth,
            CanvasHeight = CanvasHeight,
            CanvasFullScreen = false
        };
    }

    protected override void InitializeTextureSet()
    {
        var workingPath = Path.Combine(WorkingFolder, "images");

        Console.Write("Generating images cache .. ");

        //ImageCache.MarginSize = 0;
        //ImageCache.BackgroundColor = Color.FromRgba(255, 255, 255, 0);

        if (ShowCopyright)
        {
            TextureSet.AddTextureFromPngFile(
                "gui",
                "Copyright"
            );
        }
        
        Console.WriteLine("done.");
        Console.WriteLine();
    }

    protected override void SetCameraAndLights(int frameIndex)
    {
        base.SetCameraAndLights(frameIndex);
    }

    protected override void AddEnvironment()
    {
        base.AddEnvironment();
    }

    protected override void AddGrid()
    {
        base.AddGrid();
    }

    protected override void AddGuiLayer(int frameIndex)
    {
        var scene = MainSceneComposer.SceneObject;

        // Add GUI layer
        var uiTexture = scene.AddGuiFullScreenUi("uiTexture");
        
        if (ShowCopyright)
        {
            var copyrightImage = TextureSet["gui", "Copyright"];
            var copyrightImageWidth = 0.4d * CodeFilesComposer.CanvasWidth;
            var copyrightImageHeight = 0.4d * CodeFilesComposer.CanvasWidth * copyrightImage.ImageHeightToWidth;

            uiTexture.AddGuiImage(
                "copyrightImage",
                copyrightImage.GetImageUrl(),
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
            _activeCurve.FixedCurve.ParameterRange;
            
        var tValues =
            tMin.ScalarValue.GetLinearRange(tMax, 501, false).ToImmutableArray();

        var tValuesFrames = 
            tMin.ScalarValue.GetLinearRange(tMax, FixedCurveFrameCount, false).ToImmutableArray();
        
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
            _activeCurve.MovingCurve.ParameterRange;
            
        var tValues =
            tMin.ScalarValue.GetLinearRange(tMax, 501, false).ToImmutableArray();
            
        var tValuesFrames = 
            tMin.ScalarValue.GetLinearRange(tMax, MovingCurveFrameCount, false).ToImmutableArray();
            
        MainSceneComposer.AddParametricCurve(
            "movingCurve",
            _activeCurve.MovingCurve,
            tValues,
            tValuesFrames,
            Color.DarkGreen,
            0.035
        );

        //var frame = _activeCurve.FixedCurve.GetFrame(_activeCurve.FixedCurve.LengthToParameter(t));
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
            _activeCurve.ParameterRange, 
            new AdaptiveCurveSamplingOptions3D(5.DegreesToDirectedAngle(), 3, 16)
        );

        var pointList =
            sampledCurve.GetPoints().ToImmutableArray();

        MainSceneComposer.AddLinePath(
            "rouletteCurve",
            pointList,
            Color.Blue,
            0.08
        );
    }

    protected override void ComposeFrame(int frameIndex)
    {
        base.ComposeFrame(frameIndex);

        _activeCurve = CurveFunction(frameIndex / (FrameCount - 1d));

        AddFixedCurve();
        AddMovingCurve();
        AddGeneratorPoint();
        AddRouletteCurve();
    }
}