using System.Collections.Immutable;
using DataStructuresLib.Files;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Adaptive;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Roulettes;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.GUI;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Applications.Graphics;

public class RouletteParametricVisualizer3D :
    GrBabylonJsSnapshotComposer3D
{
    private RouletteCurve3D? _activeCurve;


    public Func<double, RouletteCurve3D> CurveFunction { get; }
    
    public int FixedCurveFrameCount { get; }
    
    public int MovingCurveFrameCount { get; }


    public RouletteParametricVisualizer3D(IReadOnlyList<double> cameraAlphaValues, IReadOnlyList<double> cameraBetaValues, Func<double, RouletteCurve3D> curveFunction, int fixedCurveFrameCount, int movingCurveFrameCount)
        : base(cameraAlphaValues, cameraBetaValues)
    {
        CurveFunction = curveFunction;
        FixedCurveFrameCount = fixedCurveFrameCount;
        MovingCurveFrameCount = movingCurveFrameCount;
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

    protected override void AddCamera(int index)
    {
        base.AddCamera(index);
    }

    protected override void AddEnvironment()
    {
        base.AddEnvironment();
    }

    protected override void AddGrid()
    {
        base.AddGrid();
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
        if (_activeCurve is null)
            throw new NullReferenceException();

        var (tMin, tMax) = 
            _activeCurve.FixedCurve.ParameterRange;
            
        var tValues =
            tMin.Value.GetLinearRange(tMax, 501, false).ToImmutableArray();

        var tValuesFrames = 
            tMin.Value.GetLinearRange(tMax, FixedCurveFrameCount, false).ToImmutableArray();
        
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
            tMin.Value.GetLinearRange(tMax, 501, false).ToImmutableArray();
            
        var tValuesFrames = 
            tMin.Value.GetLinearRange(tMax, MovingCurveFrameCount, false).ToImmutableArray();
            
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
            new AdaptiveCurveSamplingOptions3D(5.DegreesToAngle(), 3, 16)
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

    protected override GrBabylonJsHtmlComposer3D GenerateSnapshotCode(int index)
    {
        base.GenerateSnapshotCode(index);

        _activeCurve = CurveFunction(index / (FrameCount - 1d));

        AddFixedCurve();
        AddMovingCurve();
        AddGeneratorPoint();
        AddRouletteCurve();

        return HtmlComposer;
    }
}