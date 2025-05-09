using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Images;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;
using SixLabors.ImageSharp;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Grids;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Angles;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Adaptive;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Visualizer;

public sealed class GrBabylonJsGeometryAnimationComposer :
    GrBabylonJsAnimationComposer
{
    private ComputedSequenceCollection<string> SceneObjectNameSequences { get; }
        = new ComputedSequenceCollection<string>(
            (key, index) => $"{key}{index}"
        );

    public double CameraDistance { get; set; } = 15;

    public int GridUnitCount { get; set; } = 24;

    public double GridUnitSize { get; set; } = 1;

    public double GridOpacity { get; set; } = 1;

    public LinFloat64Vector3D GridCenter { get; set; } = LinFloat64Vector3D.Zero;

    public GrBabylonJsGeometryVisualizerPointStyle PointStyle { get; }

    public GrBabylonJsGeometryVisualizerVectorStyle VectorStyle { get; }

    public GrBabylonJsGeometryVisualizerCurveStyle CurveStyle { get; }

    public GrBabylonJsGeometryVisualizerSurfaceStyle SurfaceStyle { get; }


    public GrBabylonJsGeometryAnimationComposer(string workingFolder, Float64SamplingSpecs samplingSpecs)
        : base(workingFolder, samplingSpecs)
    {
        PointStyle = new GrBabylonJsGeometryVisualizerPointStyle(this, 0.08);
        VectorStyle = new GrBabylonJsGeometryVisualizerVectorStyle(this, 0.08);
        CurveStyle = new GrBabylonJsGeometryVisualizerCurveStyle(this);
        SurfaceStyle = new GrBabylonJsGeometryVisualizerSurfaceStyle(this);
    }

    public GrBabylonJsGeometryAnimationComposer(string workingFolder, int frameRate, double maxTime) 
        : this(workingFolder, Float64SamplingSpecs.Create(frameRate, maxTime))
    {
    }


    internal string GetNewSceneObjectName(string key)
    {
        return SceneObjectNameSequences.GetNextItem(key);
    }

    protected override void AddImageTextures()
    {
        if (ShowCopyright)
        {
            ImageSet.AddImageFromPngFile(
                "gui",
                "Copyright"
            );
        }
    }

    protected override void AddLaTeXTextures()
    {
        
    }

    protected override void AddGuiLayer()
    {
        //throw new NotImplementedException();
    }

    protected override void ComposeScene()
    {
        throw new InvalidOperationException();
    }

    public override GrBabylonJsAnimationComposer SetCamera(Float64ScalarSignal alpha, Float64ScalarSignal beta, Float64ScalarSignal distance)
    {
        throw new InvalidOperationException();
    }
    
    public GrBabylonJsGeometryAnimationComposer SetGrid(ITriplet<Float64Scalar> center, int unitCount, double unitSize = 1, double opacity = 1)
    {
        GridCenter = center.ToLinVector3D();
        GridUnitCount = unitCount;
        GridUnitSize = unitSize;
        GridOpacity = opacity;

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer BeginDrawing2D()
    {
        SceneObjectNameSequences.Reset();

        PointStyle.SetStyle(0.08);
        VectorStyle.SetStyle(0.08);
        CurveStyle.SetTubeStyle(0.08);

        BeginDrawing();

        SceneComposer.AddSquareGrid(
            GrVisualSquareGrid3D.DefaultXy(
                LinFloat64Vector3D.Zero, 
                GridUnitCount,
                GridUnitSize,
                GridOpacity
            )
        );
        
        SceneComposer.AddSquareGrid(
            GrVisualSquareGrid3D.DefaultYz(
                LinFloat64Vector3D.Zero,
                GridUnitCount,
                GridUnitSize,
                GridOpacity
            )
        );
        
        SceneComposer.AddSquareGrid(
            GrVisualSquareGrid3D.DefaultZx(
                LinFloat64Vector3D.Zero,
                GridUnitCount,
                GridUnitSize,
                GridOpacity
            )
        );

        SceneComposer
            .AddDefaultAxes(GridCenter)
            .AddDefaultEnvironment(GridUnitCount)
            .AddDefaultOrthographicCamera(
                CameraDistance,
                "Math.PI / 2",
                "Math.PI / 2",
                Float64BoundingBox2D.CreateAround(
                    0,
                    0,
                    0.5 * GridUnitCount * GridUnitSize,
                    0.5 * GridUnitCount * GridUnitSize * CameraSpecs.CanvasHeightToWidth
                )
            );

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer BeginDrawing3D()
    {
        SceneObjectNameSequences.Reset();

        PointStyle.SetStyle(0.08);
        VectorStyle.SetStyle(0.08);
        CurveStyle.SetTubeStyle(0.08);
        
        BeginDrawing();

        SceneComposer.AddSquareGrid(
            GrVisualSquareGrid3D.DefaultZx(
                GridCenter,
                GridUnitCount,
                GridUnitSize,
                GridOpacity
            )
        );

        SceneComposer
            .AddDefaultAxes(GridCenter)
            .AddDefaultEnvironment(GridUnitCount)
            .AddDefaultPerspectiveCamera(
                CameraDistance,
                "Math.Tau / 20",
                "Math.Tau / 5"
            );

        return this;
    }


    public GrBabylonJsGeometryAnimationComposer SetPointStyle(double thickness)
    {
        PointStyle.SetStyle(thickness);

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer SetVectorStyle(double thickness)
    {
        VectorStyle.SetStyle(thickness);

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer SetCurveStyleTube(double thickness)
    {
        CurveStyle.SetTubeStyle(thickness);

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer SetCurveStyleSolid()
    {
        CurveStyle.SetSolidStyle();

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer SetCurveStyleDashed(int dashOn, int dashOff, int dashPerLine)
    {
        CurveStyle.SetDashedStyle(
            dashOn,
            dashOff,
            dashPerLine
        );

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer SetSurfaceStyleThick(double thickness)
    {
        SurfaceStyle.SetThickStyle(thickness);

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer SetSurfaceStyleThin()
    {
        SurfaceStyle.SetThinStyle();

        return this;
    }


    public GrBabylonJsGeometryAnimationComposer DrawLaTeX(string key, LinFloat64Vector2D origin)
    {
        return DrawLaTeX(key, origin.ToXyLinVector3D());
    }

    public GrBabylonJsGeometryAnimationComposer DrawLaTeX(string key, LinFloat64Vector3D origin)
    {
        var name = GetNewSceneObjectName("laTeX");

        SceneComposer.AddImage(
            GrVisualImage3D.CreateStatic(
                name,
                ImageSet["latex", key],
                origin,
                CodeFilesComposer.LaTeXScalingFactor
            )
        );

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawLaTeX(string key, Float64Path2D originCurve)
    {
        return DrawLaTeX(key, originCurve.ToXyParametricCurve3D());
    }

    public GrBabylonJsGeometryAnimationComposer DrawLaTeX(string key, Float64Path3D originCurve)
    {
        SceneComposer.AddImage(
            GrVisualImage3D.CreateAnimated(
                GetNewSceneObjectName("laTeX"),
                ImageSet["latex", key],
                SceneSamplingSpecs.CreateAnimatedVector3D(originCurve),
                CodeFilesComposer.LaTeXScalingFactor
            )
        );

        return this;
    }


    public GrBabylonJsGeometryAnimationComposer DrawPoint(Color color, LinFloat64Vector2D position)
    {
        return DrawPoint(color, position.ToXyLinVector3D());
    }

    public GrBabylonJsGeometryAnimationComposer DrawPoint(Color color, LinFloat64Vector3D position)
    {
        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                GetNewSceneObjectName("point"),
                PointStyle.GetVisualStyle(color),
                position
            )
        );

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawPoints(Color color, params LinFloat64Vector2D[] mvList)
    {
        foreach (var mv in mvList)
            DrawPoint(color, mv.ToXyLinVector3D());

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawPoints(Color color, params LinFloat64Vector3D[] mvList)
    {
        foreach (var mv in mvList)
            DrawPoint(color, mv);

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawPoints(Color color, IEnumerable<LinFloat64Vector2D> mvList)
    {
        foreach (var mv in mvList)
            DrawPoint(color, mv.ToXyLinVector3D());

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawPoints(Color color, IEnumerable<LinFloat64Vector3D> mvList)
    {
        foreach (var mv in mvList)
            DrawPoint(color, mv);

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawPointCoordinateLines(Color color, LinFloat64Vector2D point, bool drawCoordinatePoints = true)
    {
        return DrawPointCoordinateLines(color, point.ToXyLinVector3D(), drawCoordinatePoints);
    }

    public GrBabylonJsGeometryAnimationComposer DrawPointCoordinateLines(Color color, LinFloat64Vector3D point, bool drawCoordinatePoints = true)
    {
        var pointXy = point.ProjectOnXyPlane();
        var pointXz = point.ProjectOnXzPlane();
        var pointYz = point.ProjectOnYzPlane();

        var pointX = point.ProjectOnXAxis();
        var pointY = point.ProjectOnYAxis();
        var pointZ = point.ProjectOnZAxis();

        DrawLineCurvesTo(
            color,
            pointXy,
            pointX,
            pointY
        );

        DrawLineCurvesTo(
            color,
            pointXz,
            pointX,
            pointZ
        );

        DrawLineCurvesTo(
            color,
            pointYz,
            pointY,
            pointZ
        );

        DrawLineCurvesTo(
            color,
            point,
            pointXy,
            pointXz,
            pointYz
        );

        DrawLineCurvesTo(
            color,
            LinFloat64Vector3D.Zero,
            pointX,
            pointY,
            pointZ
        );

        if (drawCoordinatePoints)
            DrawPoints(
                color,
                pointX,
                pointY,
                pointZ,
                pointXy,
                pointXz,
                pointYz
            );

        return this;
    }


    public GrBabylonJsGeometryAnimationComposer DrawPoint(Color color, Float64Path2D pointCurve)
    {
        return DrawPoint(color, pointCurve.ToXyParametricCurve3D());
    }

    public GrBabylonJsGeometryAnimationComposer DrawPoint(Color color, Float64Path3D pointCurve)
    {
        if (SceneSamplingSpecs.IsStatic)
            throw new InvalidOperationException();

        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
                PointStyle.GetVisualStyle(color),
                SceneSamplingSpecs.CreateAnimatedVector3D(pointCurve)
            )
        );

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawPoints(Color color, params Float64Path2D[] mvList)
    {
        foreach (var mv in mvList)
            DrawPoint(color, mv.ToXyParametricCurve3D());

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawPoints(Color color, params Float64Path3D[] mvList)
    {
        foreach (var mv in mvList)
            DrawPoint(color, mv);

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawPoints(Color color, IEnumerable<Float64Path2D> mvList)
    {
        foreach (var mv in mvList)
            DrawPoint(color, mv.ToXyParametricCurve3D());

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawPoints(Color color, IEnumerable<Float64Path3D> mvList)
    {
        foreach (var mv in mvList)
            DrawPoint(color, mv);

        return this;
    }


    public GrBabylonJsGeometryAnimationComposer DrawVector(Color color, LinFloat64Vector2D vector)
    {
        return DrawVector(color, LinFloat64Vector3D.Zero, vector.ToXyLinVector3D());
    }

    public GrBabylonJsGeometryAnimationComposer DrawVector(Color color, LinFloat64Vector3D vector)
    {
        return DrawVector(color, LinFloat64Vector3D.Zero, vector);
    }

    public GrBabylonJsGeometryAnimationComposer DrawVector(Color color, LinFloat64Vector2D origin, LinFloat64Vector2D vector)
    {
        return DrawVector(color, origin.ToXyLinVector3D(), vector.ToXyLinVector3D());
    }

    public GrBabylonJsGeometryAnimationComposer DrawVector(Color color, LinFloat64Vector3D origin, LinFloat64Vector3D vector)
    {
        SceneComposer.AddVector(
            GrVisualVector3D.CreateStatic(
                GetNewSceneObjectName("vector"),
                VectorStyle.GetVisualStyle(color),
                origin,
                vector
            )
        );

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawVector(Color color, LinFloat64Vector2D origin, LinFloat64Vector2D vector1, LinFloat64Vector2D vector2)
    {
        DrawVector(color, origin.ToXyLinVector3D(), vector1.ToXyLinVector3D());
        DrawVector(color, origin.ToXyLinVector3D(), vector2.ToXyLinVector3D());

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawVector(Color color, LinFloat64Vector3D origin, LinFloat64Vector3D vector1, LinFloat64Vector3D vector2)
    {
        DrawVector(color, origin, vector1);
        DrawVector(color, origin, vector2);

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawVector(Color color, LinFloat64Vector2D origin, LinFloat64Vector2D vector1, LinFloat64Vector2D vector2, LinFloat64Vector2D vector3)
    {
        DrawVector(color, origin.ToXyLinVector3D(), vector1.ToXyLinVector3D());
        DrawVector(color, origin.ToXyLinVector3D(), vector2.ToXyLinVector3D());
        DrawVector(color, origin.ToXyLinVector3D(), vector3.ToXyLinVector3D());

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawVector(Color color, LinFloat64Vector3D origin, LinFloat64Vector3D vector1, LinFloat64Vector3D vector2, LinFloat64Vector3D vector3)
    {
        DrawVector(color, origin, vector1);
        DrawVector(color, origin, vector2);
        DrawVector(color, origin, vector3);

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawVectors(Color color, LinFloat64Vector2D origin, params LinFloat64Vector2D[] vectorList)
    {
        foreach (var vector in vectorList)
            DrawVector(color, origin.ToXyLinVector3D(), vector.ToXyLinVector3D());

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawVectors(Color color, LinFloat64Vector3D origin, params LinFloat64Vector3D[] vectorList)
    {
        foreach (var vector in vectorList)
            DrawVector(color, origin, vector);

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawVectors(Color color, LinFloat64Vector2D origin, IEnumerable<LinFloat64Vector2D> vectorList)
    {
        foreach (var vector in vectorList)
            DrawVector(color, origin.ToXyLinVector3D(), vector.ToXyLinVector3D());

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawVectors(Color color, LinFloat64Vector3D origin, IEnumerable<LinFloat64Vector3D> vectorList)
    {
        foreach (var vector in vectorList)
            DrawVector(color, origin, vector);

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawVector(Color color, LinFloat64Vector2D origin, Float64Path2D vectorCurve)
    {
        return DrawVector(color, origin.ToXyLinVector3D(), vectorCurve.ToXyParametricCurve3D());
    }

    public GrBabylonJsGeometryAnimationComposer DrawVector(Color color, LinFloat64Vector3D origin, Float64Path3D vectorCurve)
    {
        var name = GetNewSceneObjectName("vector");

        var visualVector = GrVisualVector3D.CreateAnimated(
            name,
            VectorStyle.GetVisualStyle(color),
            origin,
            SceneSamplingSpecs.CreateAnimatedVector3D(vectorCurve)
        );

        SceneComposer.AddVector(visualVector);

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawVector(Color color, Float64Path2D originCurve, Float64Path2D vectorCurve)
    {
        return DrawVector(color, originCurve.ToXyParametricCurve3D(), vectorCurve.ToXyParametricCurve3D());
    }

    public GrBabylonJsGeometryAnimationComposer DrawVector(Color color, Float64Path3D originCurve, Float64Path3D vectorCurve)
    {
        var name = GetNewSceneObjectName("vector");

        var visualVector = GrVisualVector3D.CreateAnimated(
            name,
            VectorStyle.GetVisualStyle(color),
            SceneSamplingSpecs.CreateAnimatedVector3D(originCurve),
            SceneSamplingSpecs.CreateAnimatedVector3D(vectorCurve)
        );

        SceneComposer.AddVector(visualVector);

        return this;
    }


    public GrBabylonJsGeometryAnimationComposer DrawLineCurve(Color color, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        return DrawLineCurve(color, point1.ToXyLinVector3D(), point2.ToXyLinVector3D());
    }

    public GrBabylonJsGeometryAnimationComposer DrawLineCurve(Color color, LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        var name = GetNewSceneObjectName("curve");

        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateStatic(
                name,
                CurveStyle.GetVisualStyle(color),
                point1,
                point2
            )
        );

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawLineCurve(Color color, LinFloat64Vector2D point1, Float64Path2D point2)
    {
        return DrawLineCurve(color, point1.ToXyLinVector3D(), point2.ToXyParametricCurve3D());
    }

    public GrBabylonJsGeometryAnimationComposer DrawLineCurve(Color color, LinFloat64Vector3D point1, Float64Path3D point2)
    {
        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                GetNewSceneObjectName("curve"),
                CurveStyle.GetVisualStyle(color),
                SceneSamplingSpecs.CreateAnimatedVector3D(point1),
                SceneSamplingSpecs.CreateAnimatedVector3D(point2)
            )
        );

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawLineCurve(Color color, Float64Path3D point1, LinFloat64Vector3D point2)
    {
        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                GetNewSceneObjectName("curve"),
                CurveStyle.GetVisualStyle(color),
                SceneSamplingSpecs.CreateAnimatedVector3D(point1),
                SceneSamplingSpecs.CreateAnimatedVector3D(point2)
            )
        );

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawLineCurve(Color color, Float64Path2D point1, Float64Path2D point2)
    {
        return DrawLineCurve(color, point1.ToXyParametricCurve3D(), point2.ToXyParametricCurve3D());
    }

    public GrBabylonJsGeometryAnimationComposer DrawLineCurve(Color color, Float64Path3D point1, Float64Path3D point2)
    {
        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                GetNewSceneObjectName("curve"),
                CurveStyle.GetVisualStyle(color),
                SceneSamplingSpecs.CreateAnimatedVector3D(point1),
                SceneSamplingSpecs.CreateAnimatedVector3D(point2)
            )
        );

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawLineCurvesTo(Color color, LinFloat64Vector2D point, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        DrawLineCurve(color, point.ToXyLinVector3D(), point1.ToXyLinVector3D());
        DrawLineCurve(color, point.ToXyLinVector3D(), point2.ToXyLinVector3D());

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawLineCurvesTo(Color color, LinFloat64Vector3D point, LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        DrawLineCurve(color, point, point1);
        DrawLineCurve(color, point, point2);

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawLineCurvesTo(Color color, LinFloat64Vector2D point, LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
    {
        DrawLineCurve(color, point.ToXyLinVector3D(), point1.ToXyLinVector3D());
        DrawLineCurve(color, point.ToXyLinVector3D(), point2.ToXyLinVector3D());
        DrawLineCurve(color, point.ToXyLinVector3D(), point3.ToXyLinVector3D());

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawLineCurvesTo(Color color, LinFloat64Vector3D point, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
    {
        DrawLineCurve(color, point, point1);
        DrawLineCurve(color, point, point2);
        DrawLineCurve(color, point, point3);

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawLineCurvesTo(Color color, LinFloat64Vector2D point1, IEnumerable<LinFloat64Vector2D> pointList)
    {
        foreach (var point2 in pointList)
            DrawLineCurve(color, point1.ToXyLinVector3D(), point2.ToXyLinVector3D());

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawLineCurvesTo(Color color, LinFloat64Vector3D point1, IEnumerable<LinFloat64Vector3D> pointList)
    {
        foreach (var point2 in pointList)
            DrawLineCurve(color, point1, point2);

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawLineCurvesTo(Color color, LinFloat64Vector2D point1, params LinFloat64Vector2D[] pointList)
    {
        foreach (var point2 in pointList)
            DrawLineCurve(color, point1.ToXyLinVector3D(), point2.ToXyLinVector3D());

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawLineCurvesTo(Color color, LinFloat64Vector3D point1, params LinFloat64Vector3D[] pointList)
    {
        foreach (var point2 in pointList)
            DrawLineCurve(color, point1, point2);

        return this;
    }


    public GrBabylonJsGeometryAnimationComposer DrawCircleCurve(Color color, LinFloat64Vector3D center, LinFloat64Vector3D normal, double radius)
    {
        SceneComposer.AddCircleCurve(
            GrVisualCircleCurve3D.CreateStatic(
                GetNewSceneObjectName("circle"),
                CurveStyle.GetVisualStyle(color),
                center,
                normal,
                radius
            )
        );

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawCircleCurve(Color color, LinFloat64Vector3D center, Float64Path3D normal, double radius)
    {
        SceneComposer.AddCircleCurve(
            GrVisualCircleCurve3D.CreateAnimated(
                GetNewSceneObjectName("circle"),
                CurveStyle.GetVisualStyle(color),
                center,
                SceneSamplingSpecs.CreateAnimatedVector3D(normal),
                radius
            )
        );

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawCircleCurve(Color color, LinFloat64Vector3D center, LinFloat64Vector3D normal, Float64ScalarSignal radius)
    {
        SceneComposer.AddCircleCurve(
            GrVisualCircleCurve3D.CreateAnimated(
                GetNewSceneObjectName("circle"),
                CurveStyle.GetVisualStyle(color),
                center,
                normal,
                SceneSamplingSpecs.CreateAnimatedScalar(radius)
            )
        );

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawCircleCurve(Color color, LinFloat64Vector3D center, Float64Path3D normal, Float64ScalarSignal radius)
    {
        SceneComposer.AddCircleCurve(
            GrVisualCircleCurve3D.CreateAnimated(
                GetNewSceneObjectName("circle"),
                CurveStyle.GetVisualStyle(color),
                center,
                SceneSamplingSpecs.CreateAnimatedVector3D(normal),
                SceneSamplingSpecs.CreateAnimatedScalar(radius)
            )
        );

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawCircleCurve(Color color, Float64Path3D center, Float64Path3D normal, double radius)
    {
        SceneComposer.AddCircleCurve(
            GrVisualCircleCurve3D.CreateAnimated(
                GetNewSceneObjectName("circle"),
                CurveStyle.GetVisualStyle(color),
                SceneSamplingSpecs.CreateAnimatedVector3D(center),
                SceneSamplingSpecs.CreateAnimatedVector3D(normal),
                radius
            )
        );

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawCircleCurve(Color color, Float64Path3D center, Float64Path3D normal, Float64ScalarSignal radius)
    {
        SceneComposer.AddCircleCurve(
            GrVisualCircleCurve3D.CreateAnimated(
                GetNewSceneObjectName("circle"),
                CurveStyle.GetVisualStyle(color),
                SceneSamplingSpecs.CreateAnimatedVector3D(center),
                SceneSamplingSpecs.CreateAnimatedVector3D(normal),
                SceneSamplingSpecs.CreateAnimatedScalar(radius)
            )
        );

        return this;
    }


    public GrBabylonJsGeometryAnimationComposer DrawCurve(Color color, Float64Path2D curve, Float64ScalarRange timeRange)
    {
        return DrawCurve(
            color,
            curve.ToXyParametricCurve3D(),
            timeRange
        );
    }

    public GrBabylonJsGeometryAnimationComposer DrawCurve(Color color, Float64Path3D curve, Float64ScalarRange timeRange)
    {
        var samplingSpecs =
            new Float64AdaptivePath3DSamplingOptions(
                5d.DegreesToDirectedAngle(),
                3,
                10
            );

        return DrawCurve(
            color,
            curve,
            timeRange,
            samplingSpecs
        );
    }

    public GrBabylonJsGeometryAnimationComposer DrawCurve(Color color, Float64Path2D curve, Float64ScalarRange timeRange, Float64AdaptivePath3DSamplingOptions samplingSpecs)
    {
        return DrawCurve(color, curve.ToXyParametricCurve3D(), timeRange, samplingSpecs);
    }

    public GrBabylonJsGeometryAnimationComposer DrawCurve(Color color, Float64Path3D curve, Float64ScalarRange timeRange, Float64AdaptivePath3DSamplingOptions samplingSpecs)
    {
        SceneComposer.AddLinePath(
            GrVisualPointPathCurve3D.CreateStatic(
                GetNewSceneObjectName("curve"),
                CurveStyle.GetVisualStyle(color),
                curve.CreateAdaptiveCurve3D(
                    timeRange,
                    samplingSpecs
                )
            )
        );

        return this;
    }


    public GrBabylonJsGeometryAnimationComposer DrawCircleSurface(Color color, LinFloat64Vector3D center, LinFloat64Vector3D normal, double radius)
    {
        SceneComposer.AddDisc(
            GrVisualCircleSurface3D.CreateStatic(
                GetNewSceneObjectName("circle"),
                SurfaceStyle.GetVisualStyle(color),
                center,
                normal,
                radius,
                false
            )
        );

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawCircleSurface(Color color, LinFloat64Vector3D center, Float64Path3D normal, double radius)
    {
        SceneComposer.AddDisc(
            GrVisualCircleSurface3D.CreateAnimated(
                GetNewSceneObjectName("circle"),
                SurfaceStyle.GetVisualStyle(color),
                center,
                SceneSamplingSpecs.CreateAnimatedVector3D(normal),
                radius,
                false
            )
        );

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawCircleSurface(Color color, LinFloat64Vector3D center, LinFloat64Vector3D normal, Float64ScalarSignal radius)
    {
        SceneComposer.AddDisc(
            GrVisualCircleSurface3D.CreateAnimated(
                GetNewSceneObjectName("circle"),
                SurfaceStyle.GetVisualStyle(color),
                center,
                normal,
                SceneSamplingSpecs.CreateAnimatedScalar(radius),
                false
            )
        );

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawCircleSurface(Color color, LinFloat64Vector3D center, Float64Path3D normal, Float64ScalarSignal radius)
    {
        SceneComposer.AddDisc(
            GrVisualCircleSurface3D.CreateAnimated(
                GetNewSceneObjectName("circle"),
                SurfaceStyle.GetVisualStyle(color),
                center,
                SceneSamplingSpecs.CreateAnimatedVector3D(normal),
                SceneSamplingSpecs.CreateAnimatedScalar(radius),
                false
            )
        );

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawCircleSurface(Color color, Float64Path3D center, Float64Path3D normal, double radius)
    {
        SceneComposer.AddDisc(
            GrVisualCircleSurface3D.CreateAnimated(
                GetNewSceneObjectName("circle"),
                SurfaceStyle.GetVisualStyle(color),
                SceneSamplingSpecs.CreateAnimatedVector3D(center),
                SceneSamplingSpecs.CreateAnimatedVector3D(normal),
                radius,
                false
            )
        );

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawCircleSurface(Color color, Float64Path3D center, Float64Path3D normal, Float64ScalarSignal radius)
    {
        SceneComposer.AddDisc(
            GrVisualCircleSurface3D.CreateAnimated(
                GetNewSceneObjectName("circle"),
                SurfaceStyle.GetVisualStyle(color),
                SceneSamplingSpecs.CreateAnimatedVector3D(center),
                SceneSamplingSpecs.CreateAnimatedVector3D(normal),
                SceneSamplingSpecs.CreateAnimatedScalar(radius),
                false
            )
        );

        return this;
    }


    public GrBabylonJsGeometryAnimationComposer DrawCircleSector(Color color, LinFloat64Vector3D center, LinFloat64Vector3D direction1, LinFloat64Vector3D direction2, double radius, LinFloat64PolarAngleTimeSignal angle)
    {
        SceneComposer.AddDiscSector(
            GrVisualCircleArcSurface3D.CreateAnimated(
                GetNewSceneObjectName("circleArc"),
                SurfaceStyle.GetVisualStyle(color),
                center,
                direction1,
                direction2,
                SceneSamplingSpecs.CreateAnimatedScalar(angle),
                radius,
                false
            )
        );

        return this;
    }

    public GrBabylonJsGeometryAnimationComposer DrawCircleSector(Color color, Float64Path3D center, Float64Path3D direction1, Float64Path3D direction2, Float64ScalarSignal radius, LinFloat64PolarAngleTimeSignal angle)
    {
        SceneComposer.AddDiscSector(
            GrVisualCircleArcSurface3D.CreateAnimated(
                GetNewSceneObjectName("circleArc"),
                SurfaceStyle.GetVisualStyle(color),
                SceneSamplingSpecs.CreateAnimatedVector3D(center),
                SceneSamplingSpecs.CreateAnimatedVector3D(direction1),
                SceneSamplingSpecs.CreateAnimatedVector3D(direction2),
                SceneSamplingSpecs.CreateAnimatedScalar(angle),
                SceneSamplingSpecs.CreateAnimatedScalar(radius),
                false
            )
        );

        return this;
    }


}