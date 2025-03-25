using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Adaptive;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Graphics.BabylonJs;

public static class Sample1
{
    private const string WorkingPath = @"D:\Projects\Study\Web\Babylon.js";


    public static void Example1()
    {
        const double thickness = 0.05d;

        var codeFilesComposer = new GrBabylonJsCodeFilesComposer("scene");

        var composer = codeFilesComposer.GetSceneComposer("scene");
        var scene = codeFilesComposer.GetScene("scene");

        scene.AddArcRotateCamera(
            "camera",
            30d.DegreesToRadians(),
            30d.DegreesToRadians(),
            5,
            LinFloat64Vector3D.Zero
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
            LinFloat64Vector3D.Create(5, 0, 0)
        );

        var pointB = GrVisualPoint3D.CreateStatic(
            "pointB",
            greenMaterial.CreateThickSurfaceStyle(thickness * 2),
            LinFloat64Vector3D.Create(0, 5, 0)
        );

        var pointC = GrVisualPoint3D.CreateStatic(
            "pointC",
            blueMaterial.CreateThickSurfaceStyle(thickness * 2),
            LinFloat64Vector3D.Create(0, 0, 5)
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
            LinFloat64Vector3D.Zero
        );

        var xAxis = GrVisualVector3D.CreateStatic(
            "xAxis",
            darkRedMaterial.CreateTubeCurveStyle(thickness),
            LinFloat64Vector3D.Create(2, 0, 0),
            3 * LinFloat64Vector3D.E1
        );

        var yAxis = GrVisualVector3D.CreateStatic(
            "yAxis",
            darkGreenMaterial.CreateTubeCurveStyle(thickness),
            LinFloat64Vector3D.Create(0, 2, 0),
            3 * LinFloat64Vector3D.E2
        );

        var zAxis = GrVisualVector3D.CreateStatic(
            "zAxis",
            darkBlueMaterial.CreateTubeCurveStyle(thickness),
            LinFloat64Vector3D.Create(0, 0, 2),
            3 * LinFloat64Vector3D.E3
        );

        var lineSegment = GrVisualLineSegment3D.Create(
            "lineSegment",
            yellowMaterial.CreateTubeCurveStyle(thickness * 1.5d),
            LinFloat64Vector3D.Create(1, 1, 1),
            LinFloat64Vector3D.Create(4, 4, 4),
            Float64SamplingSpecs.Static
        );

        var lineSegmentX = GrVisualLineSegment3D.Create(
            "lineSegmentX",
            Color.Red.CreateDashedLineCurveStyle(3, 1, 16),
            LinFloat64Vector3D.Create(4, 4, 4),
            LinFloat64Vector3D.Create(0, 4, 4),
            Float64SamplingSpecs.Static
        );

        var lineSegmentY = GrVisualLineSegment3D.Create(
            "lineSegmentY",
            Color.Green.CreateDashedLineCurveStyle(3, 1, 16),
            LinFloat64Vector3D.Create(4, 4, 4),
            LinFloat64Vector3D.Create(4, 0, 4),
            Float64SamplingSpecs.Static
        );

        var lineSegmentZ = GrVisualLineSegment3D.Create(
            "lineSegmentZ",
            Color.Blue.CreateDashedLineCurveStyle(3, 1, 16),
            LinFloat64Vector3D.Create(4, 4, 4),
            LinFloat64Vector3D.Create(4, 4, 0),
            Float64SamplingSpecs.Static
        );

        var rightAngleX = GrVisualRightAngle3D.CreateStatic(
            "angleXy",
            Color.Red.CreateSolidLineCurveStyle(),
            scene.AddSimpleMaterial("angleXyMaterial", Color.Red).CreateThinSurfaceStyle(),
            LinFloat64Vector3D.Create(4, 4, 4),
            LinFloat64Vector3D.Create(-1, 0, 0),
            LinFloat64Vector3D.Create(0, -1, 0),
            0.25
        );

        var circleCurve = GrVisualCircleCurve3D.CreateStatic(
            "circleCurve",
            yellowMaterial.CreateTubeCurveStyle(2 * thickness),
            LinFloat64Vector3D.Create(1, 1, 1),
            LinFloat64Vector3D.Create(1, 1, 1),
            3
        );

        var circleCurveArc = GrVisualCircleArcCurve3D.CreateStatic(
            "circleCurveArc",
            yellowMaterial.CreateTubeCurveStyle(2 * thickness),
            LinFloat64Vector3D.Zero,
            LinFloat64Vector3D.Symmetric,
            LinFloat64Vector3D.E2,
            4,
            true
        );

        var circleDisk = GrVisualCircleSurface3D.CreateStatic(
            "circleDisk",
            rosyBrownMaterial.CreateThinSurfaceStyle(),
            LinFloat64Vector3D.Create(1, 1, 1),
            LinFloat64Vector3D.Create(1, 1, 1),
            4,
            false
        );

        var circleDiskArc = GrVisualCircleArcSurface3D.CreateStatic(
            "circleDiskArc",
            rosyBrownMaterial.CreateThinSurfaceStyle(),
            LinFloat64Vector3D.Zero,
            LinFloat64Vector3D.Symmetric,
            LinFloat64Vector3D.E2,
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

        var sceneCode = codeFilesComposer.GetCreateScenesCode();
        var htmlCode = codeFilesComposer.GetHtmlCode();

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

        var codeFilesComposer = new GrBabylonJsCodeFilesComposer("scene");

        var composer = codeFilesComposer.GetSceneComposer("scene");
        var scene = codeFilesComposer.GetScene("scene");

        //scene.Properties.UseOrderIndependentTransparency = true;

        scene.AddArcRotateCamera(
            "camera",
            30d.DegreesToRadians(),
            30d.DegreesToRadians(),
            5,
            LinFloat64Vector3D.Zero
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
            LinFloat64Vector3D.Zero
        );

        const string curveOpacityTextureName = "curveOpacityTexture";
        scene.AddTexture(
            curveOpacityTextureName,

            @"./Textures/opacityTexture2.png".ValueToQuotedLiteral(),

            new GrBabylonJsTextureProperties
            {
                HasAlpha = true,
                UScale = 1,
                VScale = 6
            }
        );

        const string curveMaterialName = "curveMaterial";
        scene.AddStandardMaterial(
            curveMaterialName,

            new GrBabylonJsStandardMaterialProperties()
            {
                DiffuseColor = Color.RosyBrown,
                SpecularColor = Color.RosyBrown,
                BackFaceCulling = true,
                OpacityTexture = curveOpacityTextureName
            }
        );

        var curvePointList = new[]
        {
            LinFloat64Vector3D.Create(1.5, 0, 0),
            LinFloat64Vector3D.Create(0, 1.5, 0),
            LinFloat64Vector3D.Create(0, 0, 1.5)
        };

        var sampledCurve =
            curvePointList.CreateCatmullRomSpline3D(
                CatmullRomSplineType.Centripetal,
                true
            ).CreateAdaptiveCurve3D(
                new Float64AdaptivePath3DSamplingOptions(
                    5d.DegreesToDirectedAngle(),
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

        var sceneCode = codeFilesComposer.GetCreateScenesCode();
        var htmlCode = codeFilesComposer.GetHtmlCode();

        File.WriteAllText(
            Path.Combine(WorkingPath, "Frame.html"),
            htmlCode
        );

        Console.WriteLine(sceneCode);
    }
}