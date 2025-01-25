using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Polytopes.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Cameras;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lights;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Finishes;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Graphics.PovRay;

public static class SquareGridGenerator
{
    public static string WorkingFolder { get; }
        = @"D:\Projects\Study\POV-Ray\Samples";

    public static Float64SquareGrid2D GridSpecs { get; }
        = Float64SquareGrid2D.Create(0.5, GridWidth, GridHeight);

    public static double CellSideRadius
        => 0.05;

    public static int ImageWidth 
        => 1920;

    public static int ImageHeight 
        => 1080;

    public static double GridWidth 
        => 16;
        
    public static double GridHeight 
        => GridWidth * ImageHeight / ImageWidth;

    public static int GridSubdivisions 
        => 2;

    public static double SubGridScalingFactor 
        => 1d / GridSubdivisions;

    public static LinFloat64Vector3D SideVector1 
        => GridSpecs.BaseVectors.Item1.ToZxLinVector3D();
    
    public static LinFloat64Vector3D SideVector2 
        => GridSpecs.BaseVectors.Item2.ToZxLinVector3D();

    public static GrPovRaySceneComposer Composer { get; }
        = new GrPovRaySceneComposer(WorkingFolder)
        {
            RenderingOptions =
            {
                Width = ImageWidth,
                Height = ImageHeight,
                AntiAlias = true,
                AntiAliasDepth = 10,
                Display = true,
                MaxImageBufferMemory = 10240,
                OutputFileType = GrPovRayOutputFileTypeValue.Png,
                Quality = 9
            },

            BackgroundColor = Color.BlanchedAlmond,

            SceneObject =
            {
                Camera = GrPovRayCamera.ArcRotateOrthographic(
                    LinFloat64PolarAngle.Angle0,
                    LinFloat64PolarAngle.Angle0,
                    10,
                    LinFloat64Vector3D.Zero,
                    GridWidth,
                    GridHeight
                )
            }
        };

        
    private static void InitializeComposer()
    {
        //// Sunlight
        //composer.AddStatement(
        //    GrPovRayLightSource.PointLight(
        //        LinFloat64Vector3D.Create(-1500, 2500, -2500),
        //        GrPovRayColorValue.Rgb(0.9)
        //    )
        //);

        // Camera flashlight
        var camera = Composer.SceneObject.Camera;

        if (camera.FlashLightColor is not null)
            Composer.AddStatement(
                GrPovRayLightSource.PointLight(
                    camera.Position,
                    camera.FlashLightColor //GrPovRayColorValue.Rgb(0.9, 0.9, 1)
                )
            );
    }
    
    private static void AddGrid(LinFloat64Vector2D origin, int subDivisionLevel, LinFloat64Angle angle, Color color, GrPovRayFinish finish)
    {
        Debug.Assert(subDivisionLevel >= 0);

        var scalingFactor = 
            1d / Math.Pow(GridSubdivisions, subDivisionLevel);

        var gridSpecs = 
            GridSpecs.GetSubGrid(GridSubdivisions, subDivisionLevel, origin, angle);

        foreach (var lineSegment in gridSpecs)
        {
            var (p1, p2) = lineSegment.GetEndPoints();

            Composer.AddStatement(
                GrPovRayObject
                    .Cylinder(
                        p1.ToZxLinVector3D(), 
                        p2.ToZxLinVector3D(), 
                        CellSideRadius * scalingFactor, 
                        true
                    )
                    .SetMaterial(color, finish)
                    .Rotate(LinFloat64Vector3D.E2, angle)
            );
        }
    }

    public static void GenerateImage()
    {
        const string fileName = "SquareGridScene";

        InitializeComposer();

        //DefineCellSide("cellSide");

        //DefineCell("cell", "cellSide");

        AddGrid(
            LinFloat64Vector2D.Zero,
            0,
            LinFloat64PolarAngle.Angle0, 
            Color.BlanchedAlmond.ScaleRgbBy(0.4),
            GrPovRayFinish.PhongShiny
        );

        AddGrid(
            LinFloat64Vector2D.Zero,
            1,
            LinFloat64PolarAngle.Angle0,
            Color.BlanchedAlmond.ScaleRgbBy(0.6),
            GrPovRayFinish.PhongShiny
        );

        AddGrid(
            LinFloat64Vector2D.Zero,
            2,
            LinFloat64PolarAngle.Angle0,
            Color.BlanchedAlmond.ScaleRgbBy(0.8),
            GrPovRayFinish.PhongShiny
        );
        
        AddGrid(
            LinFloat64Vector2D.Zero,
            3,
            LinFloat64PolarAngle.Angle0,
            Color.BlanchedAlmond,
            GrPovRayFinish.PhongShiny
        );

        Composer.AddStatement(
            GrPovRayObject
                .PlaneFromNormalDistance(LinFloat64Vector3D.E2, 0)
                .SetMaterial(Color.BlanchedAlmond)
        );
        
        Composer.AddStatement(
            GrPovRayObject
                .Sphere(LinFloat64Vector3D.Zero, CellSideRadius * 1.2)
                .SetMaterial(Color.DarkOrange)
        );

        Composer.RenderScene(fileName);
    }
}