using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Grids;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Images;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;
using SixLabors.ImageSharp;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Cameras;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Finishes;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.ISP;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Files;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using Instances;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Textures;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Composers;

public class GrPovRaySceneComposer :
    GrVisualSceneComposer3D<GrPovRayScene>
{
    public string PovRayFolder { get; set; }
        = @"C:\Program Files\POV-Ray\v3.7\bin\";

    public string WorkingFolder { get; }

    public override GrPovRayScene SceneObject { get; }

    public GrPovRayRenderingOptions RenderingOptions 
        => SceneObject.RenderingOptions;

    public GrPovRayStatementList SceneStatements
        => SceneObject.Statements;

    public GrPovRayFullCamera SceneCamera 
        => SceneObject.Camera;

    public GrPovRayColorValue BackgroundColor { get; set; }
        = Color.Teal;

    public GrPovRayGridMaterialKind GridMaterialKind { get; set; }
        = GrPovRayGridMaterialKind.GridMaterial;

    public bool ShowDebugLayer { get; set; }

    public double MinThickness { get; set; } = 0.01;


    public GrPovRaySceneComposer(string workingFolder)
        : this(workingFolder, "Scene")
    {
    }
    
    public GrPovRaySceneComposer(string workingFolder, GrPovRayRenderingOptions sceneOptions)
        : this(workingFolder, "Scene", sceneOptions)
    {

    }

    public GrPovRaySceneComposer(string workingFolder, string sceneName)
    {
        WorkingFolder = workingFolder;
        SceneObject = new GrPovRayScene(sceneName);

        AddInitialObjects();
    }

    public GrPovRaySceneComposer(string workingFolder, string sceneName, GrPovRayRenderingOptions sceneOptions)
    {
        WorkingFolder = workingFolder;
        SceneObject = new GrPovRayScene(sceneName, sceneOptions);

        AddInitialObjects();
    }


    public GrPovRaySceneComposer AddStatement(IGrPovRayStatement statement)
    {
        SceneObject.Statements.Add(statement);

        return this;
    }

    public GrPovRaySceneComposer AddStatements(params IGrPovRayStatement[] statementList)
    {
        foreach (var statement in statementList)
            SceneObject.Statements.Add(statement);

        return this;
    }

    private void AddInitialObjects()
    {
        SceneObject.IncludeGlobal("colors", "finish", "functions");

        SceneObject.GlobalSettings(
            new GrPovRayGlobalSettingsProperties
            {
                AssumedGamma = 1,
                AmbientLight = GrPovRayColorValue.Rgb(0.1, 0.1, 0.1)
            }
        );

        SceneStatements.Default(
            new GrPovRayFinish().SetProperties(
                new GrPovRayFinishProperties
                {
                    AmbientColor = GrPovRayColorValue.Rgb(0.1),
                    DiffuseAmount = 0.9,
                    ConserveEnergy = GrPovRayFlagValue.True
                }
            )
        );

        SceneStatements.FreeCode(@"
sky_sphere {
    pigment {
        gradient <0,1,0>
        color_map {
            [ 0   color rgb<1,1,1>          ] //White
            [ 0.4 color rgb<0.24,0.34,0.56> ] //~Navy
            [ 0.6 color rgb<0.24,0.34,0.56> ] //~Navy
            [ 1.0 color rgb<1,1,1>          ] //White
        }
        scale 2
    }
    emission rgb <0.8, 0.8, 1.0>
}
".Trim());
    }

    public override IGrVisualElementMaterial3D AddOrGetColorMaterial(Color color)
    {
        return GrPovRayMaterial.Create(color);

        //var key = color.ToPixel<Rgba32>().ToHex();

        //if (_colorMaterialCache.TryGetValue(key, out var material))
        //    return material;

        //material = GrPovRayMaterial.Create(
        //    $"colorMaterial{key}",
        //    color
        //);

        //_colorMaterialCache.Add(key, material);

        //return material;
    }

    public override void AddMaterial(IGrVisualElementMaterial3D material)
    {
        throw new NotImplementedException();
    }


    public override GrVisualImage3D AddImage(GrVisualImage3D visualElement)
    {
        var imageData = (GrVisualTexture) visualElement.Texture;

        //var imageData = visualElement.GetImageData();

        var rect = imageData.CreateImageRectangleZx(0.4 / 32);

        //var rect = SceneObject.RectanglePolygonZx(
        //    visualElement.ScalingFactor * imageData.Width,
        //    visualElement.ScalingFactor * imageData.Height
        //);

        rect.AffineMap
            .Transform(SceneObject.Camera.RigidMap.GetRotationPart())
            .Translate(visualElement.Position);

        SceneObject.AddStatement(rect);

        //rect.SetMaterial(Color.OrangeRed);
        
        //var pigment = new GrPovRayImageMapPigment(
        //    imageData.PngImageFileName, 
        //    GrPovRayImageMapBitmapType.Png
        //)
        //{
        //    Properties =
        //    {
        //        MapType = GrPovRayImageMapTypeValue.Planar,
        //        Once = GrPovRayFlagValue.True
        //    }
        //};

        ////if (imageData.Width > imageData.Height)
        ////    pigment.AffineMap.Scale(1, imageData.HeightToWidthRatio, 1);

        ////else if (imageData.Width < imageData.Height)
        ////    pigment.AffineMap.Scale(imageData.WidthToHeightRatio, 1, 1);
        
        //pigment.AffineMap.Scale(1, imageData.HeightToWidthRatio, 1);

        //pigment.AffineMap
        //    .TranslateX(-visualElement.ScalingFactor * imageData.Width / 2d)
        //    .TranslateY(-visualElement.ScalingFactor * imageData.Height / 2d);
        
        ////pigment.AffineMap.Scale(0.25);

        //rect.SetMaterial(pigment);

        return visualElement;
    }


    public override GrVisualSquareGrid3D AddSquareGrid(GrVisualSquareGrid3D visualElement)
    {
        //visualElement.UpdateGridColorsOpacity();

        //var normal = visualElement.GridPlane switch
        //{
        //    GrVisualSquareGridPlane3D.XyPlane => GrPovRayVector3Value.E3,
        //    GrVisualSquareGridPlane3D.YzPlane => GrPovRayVector3Value.E1,
        //    GrVisualSquareGridPlane3D.ZxPlane => GrPovRayVector3Value.E2,
        //    _ => throw new ArgumentOutOfRangeException()
        //};
        
        //var plane = SceneStatements.Plane(
        //    normal, 
        //    visualElement.DistanceToOrigin - 0.0001
        //);
        
        //var (gridSize1, gridSize2) = visualElement.GetGridSize();

        //var plane = SceneStatements.RectanglePolygonXy(
        //    visualElement.Size1,
        //    visualElement.Size2
        //);

        //if (visualElement.GridPlane == GrVisualSquareGridPlane3D.ZxPlane)
        //    plane.AffineMap.RotateX(LinFloat64PolarAngle.Angle90);

        //else if (visualElement.GridPlane == GrVisualSquareGridPlane3D.YzPlane)
        //    plane.AffineMap.RotateY(LinFloat64PolarAngle.Angle90);

        //plane.AffineMap.Translate(visualElement.MidPoint);

        //plane.SetMaterial(
        //    GrPovRayPattern.Checker(
        //        visualElement.BaseSquareColor,
        //        visualElement.BaseSquareColor.ScaleRgbBy(0.75)
        //    ).ToColorListPigment()
        //);

        //foreach (var value1 in visualElement.GetBorderLineValues1())
        //{
        //    SceneStatements.Cylinder(
        //        (value1, -gridSize2 * 0.5d).ToZxLinVector3D(),
        //        (value1, gridSize2 * 0.5d).ToZxLinVector3D(),
        //        visualElement.BorderLineWidthNormalized
        //    ).SetMaterial(visualElement.BorderLineColor);
        //}

        //foreach (var value2 in visualElement.GetBorderLineValues2())
        //{
        //    SceneStatements.Cylinder(
        //        (-gridSize1 * 0.5d, value2).ToZxLinVector3D(),
        //        (gridSize1 * 0.5d, value2).ToZxLinVector3D(),
        //        visualElement.BorderLineWidthNormalized
        //    ).SetMaterial(visualElement.BorderLineColor);
        //}
        
        //foreach (var value1 in visualElement.GetMidLineValues1())
        //{
        //    SceneStatements.Cylinder(
        //        (value1, -gridSize2 * 0.5d).ToZxLinVector3D(),
        //        (value1, gridSize2 * 0.5d).ToZxLinVector3D(),
        //        visualElement.MidLineWidthNormalized / 2
        //    ).SetMaterial(visualElement.MidLineColor);
        //}
        
        //foreach (var value2 in visualElement.GetMidLineValues2())
        //{
        //    SceneStatements.Cylinder(
        //        (-gridSize1 * 0.5d, value2).ToZxLinVector3D(),
        //        (gridSize1 * 0.5d, value2).ToZxLinVector3D(),
        //        visualElement.MidLineWidthNormalized / 2
        //    ).SetMaterial(visualElement.MidLineColor);
        //}

        var rect = GrPovRayObject.RectanglePolygonXy(
            visualElement.Size1,
            visualElement.Size2
        );

        if (visualElement.GridPlane == GrVisualSquareGridPlane3D.ZxPlane)
            rect.AffineMap.RotateX(LinFloat64PolarAngle.Angle90);

        else if (visualElement.GridPlane == GrVisualSquareGridPlane3D.YzPlane)
            rect.AffineMap.RotateY(LinFloat64PolarAngle.Angle90);

        rect.AffineMap.Translate(visualElement.Center);

        var gridImageFileName = $@"{visualElement.Name}Texture.png";

        visualElement.Texture.GetImage().SaveAsPng(
            WorkingFolder.GetFilePath(gridImageFileName)
        );

        var pigment = new GrPovRayImageMapPigment(
            gridImageFileName, 
            GrPovRayImageMapBitmapType.Png
        );

        //pigment.AffineMap.Scale(visualElement.BaseSquareSize);

        var finish = new GrPovRayFinish().SetProperties(
            new GrPovRayFinishProperties
            {
                ConserveEnergy = true,
                DiffuseAmount = 0.2,
                AmbientColor = 0,
                SpecularHighlightAmount = 0.7,
                Metallic = true,
                ReflectionColor = Color.AliceBlue,
                Reflection = new GrPovRayFinishReflectionProperties(Color.BlueViolet)
                {
                    Metallic = 1
                }
            }
        );

        rect.SetMaterial(pigment, finish);
        //rect.SetMaterial(pigment, GrPovRayFinish.Mirror);

        AddStatement(rect);

        return visualElement;
    }

    public override IGrVisualImage3D AddImage(IGrVisualImage3D visualElement)
    {
        throw new NotImplementedException();
        
        //return visualElement;

        //switch (visualElement)
        //{
        //    case GrVisualLaTeXText3D latexTextImage:
        //        AddLaTeXText(latexTextImage);

        //        return visualElement;

        //    case GrVisualSquareGrid3D xzSquareGridImage:
        //        AddSquareGrid(xzSquareGridImage);

        //        return visualElement;
        //}

        //var latexImage =
        //    visualElement.GetImage();

        ////latexImage.Save(
        ////    $"{visualElement.Name}.png"
        ////);

        //var latexImageString =
        //    latexImage.PngToHtmlDataUrlBase64();

        //SceneObject.AddTexture(
        //    $"{visualElement.Name}Texture",

        //    latexImageString, //@"'./Textures/{visualElement.Name}.png'",

        //    new GrPovRayTextureProperties
        //    {
        //        HasAlpha = true
        //    }
        //);

        //return visualElement;
    }


    public override GrVisualPoint3D AddPoint(GrVisualPoint3D visualElement)
    {
        AddStatement(
            GrPovRayObject.Sphere(
                visualElement.Position.ToPovRayVector3Value(),
                visualElement.Style.Thickness / 2d
            ).SetMaterial((GrPovRayMaterial) visualElement.Style.Material)
        );

        return visualElement;
    }

    public override GrVisualArrowHead3D AddArrowHead(GrVisualArrowHead3D visualElement)
    {
        var coneBaseDiameter = visualElement.Style.Thickness * 3;
        var coneHeight = coneBaseDiameter * 1.5d;

        var unitDirection = visualElement.Direction;
        var maxHeight = visualElement.MaxHeight;

        if (coneHeight > maxHeight)
        {
            coneHeight = maxHeight;
            coneBaseDiameter = coneHeight * 2d / 3d;
        }

        //var scalingFactor = 
        //    coneHeight <= maxHeight 
        //        ? 1d : (maxHeight / coneHeight);
        
        AddStatement(
            GrPovRayObject.Cone(
                visualElement.Position - unitDirection * coneHeight,
                visualElement.Position.ToPovRayVector3Value(),
                coneBaseDiameter / 2d,
                0
            ).SetMaterial((GrPovRayMaterial)visualElement.Style.Material)
        );

        return visualElement;
    }

    public void AddThinLineSegment(GrPovRayVector3Value point1, GrPovRayVector3Value point2, Color lineColor)
    {
        var sphereSweep = 
            GrPovRayObject.SphereSweep(
                GrPovRaySphereSweepType.LinearSpline
            );

        sphereSweep.SetMaterial(lineColor);

        sphereSweep.Spheres.Add(
            point1, 
            MinThickness / 2d
        );
        
        sphereSweep.Spheres.Add(
            point2, 
            MinThickness / 2d
        );

        AddStatement(sphereSweep);
    }

    public override GrVisualParametricCurve3D AddParametricCurve(GrVisualParametricCurve3D visualElement)
    {
        if (visualElement.ShowFrames)
        {
            var length = visualElement.FrameSize;

            foreach (var t in visualElement.FrameParameterValues)
            {
                var frame = visualElement.Curve.GetFrame(t);

                var origin = frame.Point;
                var xPoint = origin + length * frame.Tangent.ToUnitLinVector3D();
                var yPoint = origin + length * frame.Normal1.ToUnitLinVector3D();
                var zPoint = origin + length * frame.Normal2.ToUnitLinVector3D();

                AddThinLineSegment(origin, xPoint, Color.Red.SetAlpha(0.9f));
                AddThinLineSegment(origin, yPoint, Color.Green.SetAlpha(0.9f));
                AddThinLineSegment(origin, zPoint, Color.Blue.SetAlpha(0.9f));
            }
        }

        var pointPath =
            visualElement
                .ParameterValues
                .Select(visualElement.Curve.GetPoint);

        var sphereSweep = 
            GrPovRayObject.SphereSweep(
                GrPovRaySphereSweepType.LinearSpline
            );

        if (visualElement.Style is GrVisualCurveTubeStyle3D tubeStyle)
        {
            sphereSweep.SetMaterial((GrPovRayMaterial) tubeStyle.Material);

            foreach (var point in pointPath)
                sphereSweep.Spheres.Add(
                    point.ToPovRayVector3Value(), 
                    tubeStyle.Thickness / 2d
                );

            AddStatement(sphereSweep);

            return visualElement;
        }

        if (visualElement.Style is GrVisualCurveSolidLineStyle3D solidLineStyle)
        {
            sphereSweep.SetMaterial(solidLineStyle.Color);

            foreach (var point in pointPath)
                sphereSweep.Spheres.Add(
                    point.ToPovRayVector3Value(), 
                    MinThickness / 2d
                );

            AddStatement(sphereSweep);

            return visualElement;
        }

        throw new ArgumentOutOfRangeException();
    }

    public override GrVisualRightAngle3D AddRightAngle(GrVisualRightAngle3D visualElement)
    {
        if (visualElement.InnerStyle is not null)
        {
            var quaternion = 
                LinBasisVectorPair3D.PxPy
                    .VectorPairToVectorPairRotationQuaternion(
                        visualElement.Direction1,
                        visualElement.Direction2
                    );

            var rect = 
                GrPovRayObject.RectanglePolygonXy(
                    visualElement.Width, 
                    visualElement.Height
                ).SetMaterial((GrPovRayMaterial) visualElement.InnerStyle.Material);

            rect.AffineMap
                .Reset()
                .Rotate(quaternion)
                .Translate(
                    visualElement.Origin + 
                    (visualElement.Direction1 + visualElement.Direction2) * visualElement.Radius / 8d.Sqrt()
                );

            AddStatement(rect);
        }

        var pathPoints =
            visualElement.GetArcPointsTriplet();

        if (visualElement.Style is GrVisualCurveTubeStyle3D tubeStyle)
        {
            var tube = GrPovRayObject.SphereSweep(
                GrPovRaySphereSweepType.LinearSpline
            );

            tube.Spheres
                .Add(pathPoints.Item1, tubeStyle.Thickness / 2d)
                .Add(pathPoints.Item2, tubeStyle.Thickness / 2d)
                .Add(pathPoints.Item3, tubeStyle.Thickness / 2d);

            tube.SetMaterial((GrPovRayMaterial)tubeStyle.Material);

            AddStatement(tube);

            return visualElement;
        }

        if (visualElement.Style is GrVisualCurveSolidLineStyle3D solidLineStyle)
        {
            var tube = GrPovRayObject.SphereSweep(
                GrPovRaySphereSweepType.LinearSpline
            );

            tube.Spheres
                .Add(pathPoints.Item1, MinThickness / 2d)
                .Add(pathPoints.Item2, MinThickness / 2d)
                .Add(pathPoints.Item3, MinThickness / 2d);

            tube.SetMaterial(solidLineStyle.Color);

            AddStatement(tube);

            return visualElement;
        }

        if (visualElement.Style is GrVisualCurveDashedLineStyle3D dashedLineStyle)
        {
            var tube = GrPovRayObject.SphereSweep(
                GrPovRaySphereSweepType.LinearSpline
            );

            tube.Spheres
                .Add(pathPoints.Item1, MinThickness / 2d)
                .Add(pathPoints.Item2, MinThickness / 2d)
                .Add(pathPoints.Item3, MinThickness / 2d);

            tube.SetMaterial(dashedLineStyle.Color);

            AddStatement(tube);

            return visualElement;
        }

        throw new ArgumentOutOfRangeException();
    }


    public override GrVisualCircleCurve3D AddCircleCurve(GrVisualCircleCurve3D visualElement)
    {
        //AddCurve(visualElement);

        //return visualElement;

        return visualElement.Style switch
        {
            GrVisualCurveTubeStyle3D tubeStyle =>
                AddCircleCurve(visualElement, tubeStyle),

            GrVisualCurveSolidLineStyle3D solidLineStyle =>
                AddCircleCurve(visualElement, solidLineStyle),

            GrVisualCurveDashedLineStyle3D dashedLineStyle =>
                AddCircleCurve(visualElement, dashedLineStyle),

            _ => throw new InvalidOperationException()
        };
    }

    private GrVisualCircleCurve3D AddCircleCurve(GrVisualCircleCurve3D visualElement, GrVisualCurveTubeStyle3D tubeStyle)
    {
        var tube = GrPovRayObject.Torus(
            visualElement.Radius,
            tubeStyle.Thickness / 2
        ).SetMaterial((GrPovRayMaterial) tubeStyle.Material);

        tube.AffineMap
            .Reset()
            .Rotate(
                LinFloat64Vector3D.E2,
                visualElement.Normal
            ).Translate(visualElement.Center);

        AddStatement(tube);

        return visualElement;
    }

    private GrVisualCircleCurve3D AddCircleCurve(GrVisualCircleCurve3D visualElement, GrVisualCurveSolidLineStyle3D solidLineStyle)
    {
        var tube = GrPovRayObject.Torus(
            visualElement.Radius,
            MinThickness / 2d
        ).SetMaterial(solidLineStyle.Color);

        tube.AffineMap
            .Reset()
            .Rotate(
                LinFloat64Vector3D.E2,
                visualElement.Normal
            ).Translate(visualElement.Center);

        AddStatement(tube);

        return visualElement;
    }

    private GrVisualCircleCurve3D AddCircleCurve(GrVisualCircleCurve3D visualElement, GrVisualCurveDashedLineStyle3D dashedLineStyle)
    {
        var tube = GrPovRayObject.Torus(
            visualElement.Radius,
            MinThickness / 2d
        ).SetMaterial(dashedLineStyle.Color);

        tube.AffineMap
            .Reset()
            .Rotate(
                LinFloat64Vector3D.E2,
                visualElement.Normal
            ).Translate(visualElement.Center);

        AddStatement(tube);

        return visualElement;
    }


    public override GrVisualCurveWithAnimation3D AddCurve(GrVisualCurveWithAnimation3D visualElement)
    {
        if (visualElement.Style is GrVisualCurveTubeStyle3D tubeStyle)
            AddCurve(visualElement, tubeStyle);

        else if (visualElement.Style is GrVisualCurveSolidLineStyle3D solidLineStyle)
            AddCurve(visualElement, solidLineStyle);

        else if (visualElement.Style is GrVisualCurveDashedLineStyle3D dashedLineStyle)
            AddCurve(visualElement, dashedLineStyle);

        else
            throw new ArgumentOutOfRangeException();

        return visualElement;
    }

    private void AddCurve(GrVisualCurveWithAnimation3D visualElement, GrVisualCurveTubeStyle3D tubeStyle)
    {
        var pointPath = visualElement.GetPositionsPath();

        var sphereSweep = 
            GrPovRayObject.SphereSweep(
                GrPovRaySphereSweepType.LinearSpline
            );

        sphereSweep.SetMaterial((GrPovRayMaterial)tubeStyle.Material);

        foreach (var point in pointPath)
            sphereSweep.Spheres.Add(
                point.ToPovRayVector3Value(), 
                tubeStyle.Thickness / 2d
            );

        AddStatement(sphereSweep);
    }

    private void AddCurve(GrVisualCurveWithAnimation3D visualElement, GrVisualCurveSolidLineStyle3D solidLineStyle)
    {
        var pointPath = visualElement.GetPositionsPath();

        var sphereSweep = 
            GrPovRayObject.SphereSweep(
                GrPovRaySphereSweepType.LinearSpline
            );

        sphereSweep.SetMaterial(solidLineStyle.Color);

        foreach (var point in pointPath)
            sphereSweep.Spheres.Add(
                point.ToPovRayVector3Value(), 
                MinThickness / 2d
            );

        AddStatement(sphereSweep);
    }

    private void AddCurve(GrVisualCurveWithAnimation3D visualElement, GrVisualCurveDashedLineStyle3D dashedLineStyle)
    {
        var pointPath = visualElement.GetPositionsPath();

        var sphereSweep = 
            GrPovRayObject.SphereSweep(
                GrPovRaySphereSweepType.LinearSpline
            );

        sphereSweep.SetMaterial(dashedLineStyle.Color);

        foreach (var point in pointPath)
            sphereSweep.Spheres.Add(
                point.ToPovRayVector3Value(), 
                MinThickness / 2d
            );

        AddStatement(sphereSweep);

        //var pointPath = visualElement.GetPositionsPath();

        //SceneObject.AddFreeCode(
        //    $"const {visualElement.Name}Points = {pointPath.GetPovRayCode()};"
        //);

        //SceneObject.AddDashedLines(
        //    $"{visualElement.Name}DashedLines",

        //    new GrPovRayDashedLinesOptions
        //    {
        //        Points = $"{visualElement.Name}Points",
        //        DashNumber = pointPath.Count * dashedLineStyle.DashPerLine,
        //        DashSize = dashedLineStyle.DashOn,
        //        GapSize = dashedLineStyle.DashOff,
        //        Updatable = visualElement.IsAnimated
        //    },

        //    new GrPovRayLinesMeshProperties
        //    {
        //        Color = dashedLineStyle.Color,
        //        Visibility = visualElement.Visibility,
        //    }
        //);

        //if (visualElement.IsAnimated)
        //    AddCurveAnimation(visualElement, dashedLineStyle);
    }


    public override GrVisualTriangleSurface3D AddTriangleSurface(GrVisualTriangleSurface3D visualElement)
    {
        return visualElement.Style switch
        {
            GrVisualSurfaceThickStyle3D thickStyle =>
                AddTriangle(visualElement, thickStyle),

            GrVisualSurfaceThinStyle3D thinStyle =>
                AddTriangle(visualElement, thinStyle),

            _ => throw new InvalidOperationException()
        };
    }

    private GrVisualTriangleSurface3D AddTriangle(GrVisualTriangleSurface3D visualElement, GrVisualSurfaceThickStyle3D thickStyle)
    {
        throw new NotImplementedException();

        //var thickness = thickStyle.Thickness;

        //var quaternion = Axis3D.Pz.CreateAxisToVectorRotationQuaternion(
        //    visualElement.UnitNormal
        //);

        //SceneObject.AddExtrudeShape(
        //    $"{visualElement.Name}ExtrudeShape",

        //    new GrPovRayExtrudeShape.ExtrudeShapeOptions
        //    {
        //        AdjustFrame = true,
        //        Cap = GrPovRayMeshCap.StartAndEnd,
        //        Rotation = 0d,
        //        Scale = 1d,
        //        Path = new[]
        //        {
        //            new Float64Tuple3D(0, 0, -thickness / 2),
        //            new Float64Tuple3D(0, 0, thickness / 2)
        //        },
        //        Shape = new[]
        //        {

        //        },
        //        CloseShape = true,
        //        Updatable = visualElement.IsAnimated
        //    },

        //    new GrPovRayMesh.MeshProperties
        //    {
        //        Material = visualElement.Style.Material.MaterialName,
        //        Position = visualElement.Origin.ToTuple3D(),
        //        RotationQuaternion = quaternion
        //    }
        //);

        //if (visualElement.IsAnimated)
        //    AddTriangleAnimation(visualElement, thickStyle);
    }

    private GrVisualTriangleSurface3D AddTriangle(GrVisualTriangleSurface3D visualElement, GrVisualSurfaceThinStyle3D thinStyle)
    {
        AddStatement(
            GrPovRayObject.Triangle(
                visualElement.Position1.ToPovRayVector3Value(),
                visualElement.Position2.ToPovRayVector3Value(),
                visualElement.Position3.ToPovRayVector3Value()
            ).SetMaterial((GrPovRayMaterial)thinStyle.Material)
        );

        return visualElement;
    }


    public override GrVisualParallelogramSurface3D AddParallelogramSurface(GrVisualParallelogramSurface3D visualElement)
    {
        return visualElement.Style switch
        {
            GrVisualSurfaceThickStyle3D thickStyle =>
                AddParallelogramSurface(visualElement, thickStyle),

            GrVisualSurfaceThinStyle3D thinStyle =>
                AddParallelogramSurface(visualElement, thinStyle),

            _ => throw new InvalidOperationException()
        };
    }

    private GrVisualParallelogramSurface3D AddParallelogramSurface(GrVisualParallelogramSurface3D visualElement, GrVisualSurfaceThickStyle3D thickStyle)
    {
        throw new NotImplementedException();
    }

    private GrVisualParallelogramSurface3D AddParallelogramSurface(GrVisualParallelogramSurface3D visualElement, GrVisualSurfaceThinStyle3D thinStyle)
    {
        throw new NotImplementedException();

        //var path0Point0 = visualElement.Position.GetPovRayCode();
        //var path0Point1 = visualElement.Position.VectorAdd(visualElement.Direction1).GetPovRayCode();

        //var path1Point0 = visualElement.Position.VectorAdd(visualElement.Direction2).GetPovRayCode();
        //var path1Point1 = visualElement.Position.VectorAdd(visualElement.Direction2, visualElement.Direction1).GetPovRayCode();

        //SceneObject.AddFreeCode(
        //    $"const {visualElement.Name}RibbonPathArray = [[{path0Point0}, {path0Point1}], [{path1Point0}, {path1Point1}]];"
        //);

        //SceneObject.AddRibbon(
        //    $"{visualElement.Name}Ribbon",

        //    new GrPovRayRibbonOptions
        //    {
        //        CloseArray = false,
        //        ClosePath = false,
        //        PathArray = $"{visualElement.Name}RibbonPathArray",
        //        SideOrientation = GrPovRayMeshOrientation.FrontAndBack,
        //        Updatable = visualElement.IsAnimated
        //    },

        //    new GrPovRayMeshProperties
        //    {
        //        Material = visualElement.Style.Material.MaterialName,
        //        Visibility = visualElement.Visibility
        //    }
        //);

        //if (visualElement.IsAnimated)
        //    AddParallelogramSurfaceAnimation(visualElement, thinStyle);

        //return visualElement;
    }


    public override GrVisualParallelepipedSurface3D AddParallelepipedSurface(GrVisualParallelepipedSurface3D visualElement)
    {
        foreach (var element in visualElement.GetParallelogramSurfaces())
            AddParallelogramSurface(element);

        return visualElement;

        //return visualElement.Style switch
        //{
        //    GrVisualSurfaceThickStyle3D thickStyle => 
        //        AddParallelepiped(visualElement, thickStyle),

        //    GrVisualSurfaceThinStyle3D thinStyle => 
        //        AddParallelepiped(visualElement, thinStyle),

        //    _ => throw new InvalidOperationException()
        //};
    }

    private GrVisualParallelepipedSurface3D AddParallelepipedSurface(GrVisualParallelepipedSurface3D visualElement, GrVisualSurfaceThickStyle3D thickStyle)
    {
        throw new NotImplementedException();
    }

    private GrVisualParallelepipedSurface3D AddParallelepipedSurface(GrVisualParallelepipedSurface3D visualElement, GrVisualSurfaceThinStyle3D thinStyle)
    {
        throw new NotImplementedException();

        //var position000 = visualElement.Position.GetPovRayCode();
        //var position001 = visualElement.Position1.GetPovRayCode();

        //var position010 = visualElement.Position2.GetPovRayCode();
        //var position011 = visualElement.Position12.GetPovRayCode();

        //var position100 = visualElement.Position3.GetPovRayCode();
        //var position101 = visualElement.Position13.GetPovRayCode();

        //var position110 = visualElement.Position23.GetPovRayCode();
        //var position111 = visualElement.Position123.GetPovRayCode();

        //SceneObject.AddFreeCode(
        //    $"const {visualElement.Name}Ribbon1PathArray = [[{position000}, {position001}], [{position010}, {position011}], [{position110}, {position111}], [{position100}, {position101}]];",
        //    $"const {visualElement.Name}Ribbon2PathArray = [[{position000}, {position010}], [{position100}, {position110}]];",
        //    $"const {visualElement.Name}Ribbon3PathArray = [[{position001}, {position011}], [{position101}, {position111}]];"
        //);

        //SceneObject.AddRibbon(
        //    $"{visualElement.Name}Ribbon1",

        //    new GrPovRayRibbonOptions
        //    {
        //        CloseArray = true,
        //        ClosePath = false,
        //        PathArray = $"{visualElement.Name}Ribbon1PathArray",
        //        SideOrientation = GrPovRayMeshOrientation.FrontAndBack,
        //        Updatable = visualElement.IsAnimated
        //    },

        //    new GrPovRayMeshProperties
        //    {
        //        Material = visualElement.Style.Material.MaterialName,
        //        Visibility = visualElement.Visibility
        //    }
        //);

        //SceneObject.AddRibbon(
        //    $"{visualElement.Name}Ribbon2",

        //    new GrPovRayRibbonOptions
        //    {
        //        CloseArray = false,
        //        ClosePath = false,
        //        PathArray = $"{visualElement.Name}Ribbon2PathArray",
        //        SideOrientation = GrPovRayMeshOrientation.FrontAndBack,
        //        Updatable = visualElement.IsAnimated
        //    },

        //    new GrPovRayMeshProperties
        //    {
        //        Material = visualElement.Style.Material.MaterialName,
        //        Visibility = visualElement.Visibility
        //    }
        //);

        //SceneObject.AddRibbon(
        //    $"{visualElement.Name}Ribbon3",

        //    new GrPovRayRibbonOptions
        //    {
        //        CloseArray = false,
        //        ClosePath = false,
        //        PathArray = $"{visualElement.Name}Ribbon3PathArray",
        //        SideOrientation = GrPovRayMeshOrientation.FrontAndBack,
        //        Updatable = visualElement.IsAnimated
        //    },

        //    new GrPovRayMeshProperties
        //    {
        //        Material = visualElement.Style.Material.MaterialName,
        //        Visibility = visualElement.Visibility
        //    }
        //);

        //if (visualElement.IsAnimated)
        //    AddParallelepipedSurfaceAnimation(visualElement, thinStyle);

        //return visualElement;
    }


    public override GrVisualSphereSurface3D AddSphere(GrVisualSphereSurface3D visualElement)
    {
        return visualElement.Style switch
        {
            GrVisualSurfaceThickStyle3D thickStyle =>
                AddSphere(visualElement, thickStyle),

            GrVisualSurfaceThinStyle3D thinStyle =>
                AddSphere(visualElement, thinStyle),

            _ => throw new InvalidOperationException()
        };
    }

    private GrVisualSphereSurface3D AddSphere(GrVisualSphereSurface3D visualElement, GrVisualSurfaceThickStyle3D thickStyle)
    {
        var outerRadius = visualElement.Radius + thickStyle.Thickness * 0.25d;
        var innerRadius = visualElement.Radius - thickStyle.Thickness * 0.25d;

        AddStatement(
            GrPovRayObject.Sphere(
                visualElement.Center.ToPovRayVector3Value(),
                outerRadius
            ).SetMaterial((GrPovRayMaterial)thickStyle.Material)
        );

        AddStatement(
            GrPovRayObject.Sphere(
                visualElement.Center.ToPovRayVector3Value(),
                innerRadius
            ).SetProperties(
                new GrPovRayObjectProperties
                {
                    Inverse = true
                }
            ).SetMaterial((GrPovRayMaterial)thickStyle.Material)
        );

        return visualElement;
    }

    private GrVisualSphereSurface3D AddSphere(GrVisualSphereSurface3D visualElement, GrVisualSurfaceThinStyle3D thinStyle)
    {
        AddStatement(
            GrPovRayObject.Sphere(
                visualElement.Center.ToPovRayVector3Value(),
                visualElement.Radius
            ).SetMaterial((GrPovRayMaterial)thinStyle.Material)
        );

        return visualElement;
    }


    public override GrVisualCircleSurface3D AddDisc(GrVisualCircleSurface3D visualElement)
    {
        return visualElement.Style switch
        {
            GrVisualSurfaceThickStyle3D thickStyle =>
                AddDisc(visualElement, thickStyle),

            GrVisualSurfaceThinStyle3D thinStyle =>
                AddDisc(visualElement, thinStyle),

            _ => throw new InvalidOperationException()
        };
    }

    private GrVisualCircleSurface3D AddDisc(GrVisualCircleSurface3D visualElement, GrVisualSurfaceThickStyle3D thickStyle)
    {
        var sphere = GrPovRayObject.Sphere(
            LinFloat64Vector3D.Zero, 
            visualElement.Radius,
            thickStyle.Thickness / 2d,
            visualElement.Radius
        ).SetMaterial((GrPovRayMaterial) thickStyle.Material);

        sphere.AffineMap
            .Rotate(
                LinFloat64Vector3D.E2,
                visualElement.Normal
            ).Translate(visualElement.Center);

        AddStatement(sphere);

        if (visualElement.DrawEdge)
        {
            var tube = GrPovRayObject.Torus(
                visualElement.Radius,
                thickStyle.Thickness / 2
            ).SetMaterial((GrPovRayMaterial) thickStyle.EdgeMaterial);

            tube.AffineMap
                .Rotate(
                    LinFloat64Vector3D.E2,
                    visualElement.Normal
                ).Translate(visualElement.Center);

            AddStatement(tube);
        }

        return visualElement;
    }

    private GrVisualCircleSurface3D AddDisc(GrVisualCircleSurface3D visualElement, GrVisualSurfaceThinStyle3D thinStyle)
    {
        AddStatement(
            GrPovRayObject.Disc(
                visualElement.Center.ToPovRayVector3Value(),
                visualElement.Normal.ToPovRayVector3Value(),
                visualElement.Radius
            ).SetMaterial((GrPovRayMaterial)thinStyle.Material)
        );
        
        if (visualElement.DrawEdge)
        {
            var tube = GrPovRayObject.Torus(
                visualElement.Radius,
                MinThickness / 2
            ).SetMaterial((GrPovRayMaterial) thinStyle.EdgeMaterial);

            tube.AffineMap
                .Reset()
                .Rotate(
                    LinFloat64Vector3D.E2,
                    visualElement.Normal
                ).Translate(visualElement.Center);

            AddStatement(tube);
        }

        return visualElement;
    }


    public override GrVisualCircleArcSurface3D AddDiscSector(GrVisualCircleArcSurface3D visualElement)
    {
        return visualElement.Style switch
        {
            GrVisualSurfaceThickStyle3D thickStyle =>
                AddDiscSector(visualElement, thickStyle),

            GrVisualSurfaceThinStyle3D thinStyle =>
                AddDiscSector(visualElement, thinStyle),

            _ => throw new InvalidOperationException()
        };
    }

    private GrVisualCircleArcSurface3D AddDiscSector(GrVisualCircleArcSurface3D visualElement, GrVisualSurfaceThickStyle3D thickStyle)
    {
        var disc = GrPovRaySphere.Create(
            LinFloat64Vector3D.Zero, 
            visualElement.Radius, 
            thickStyle.Thickness / 2d, 
            visualElement.Radius
        );

        var plane1 = GrPovRayPlane.CreateFromNormalDistance(
            LinFloat64Vector3D.E3, 
            GrPovRayFloat32Value.Zero
        );

        var plane2 = GrPovRayPlane.CreateFromNormalDistance(
            LinFloat64Vector3D.NegativeE3.RotateVectorUsingAxisAngle(
                LinFloat64Vector3D.E2,
                visualElement.Angle
            ), 
            GrPovRayFloat32Value.Zero
        );

        var sector = GrPovRayObject.Intersection(
            disc, plane1, plane2
        ).SetMaterial((GrPovRayMaterial)thickStyle.Material);

        var vector = visualElement.Direction1;
        var normal = visualElement.Normal;
        
        sector.AffineMap
            .Rotate(
                LinBasisVectorPair3D.PxPy,
                vector,
                normal
            ).Translate(visualElement.Center);

        AddStatement(sector);

        return visualElement;
    }

    private GrVisualCircleArcSurface3D AddDiscSector(GrVisualCircleArcSurface3D visualElement, GrVisualSurfaceThinStyle3D thinStyle)
    {
        //var disc = SceneStatements.Disc(
        //    LinFloat64Vector3D.Zero,
        //    LinFloat64Vector3D.E2,
        //    visualElement.Radius
        //).SetMaterial((GrPovRayMaterial)thinStyle.Material);

        var disc = GrPovRaySphere.Create(
            LinFloat64Vector3D.Zero, 
            visualElement.Radius, 
            MinThickness / 2d, 
            visualElement.Radius
        );

        var plane1 = GrPovRayPlane.CreateFromNormalDistance(
            LinFloat64Vector3D.E3, 
            GrPovRayFloat32Value.Zero
        );

        var plane2 = GrPovRayPlane.CreateFromNormalDistance(
            LinFloat64Vector3D.NegativeE3.RotateVectorUsingAxisAngle(
                LinFloat64Vector3D.E2,
                visualElement.Angle
            ), 
            GrPovRayFloat32Value.Zero
        );

        var sector = GrPovRayObject.Intersection(
            disc, plane1, plane2
        ).SetMaterial((GrPovRayMaterial)thinStyle.Material);

        var vector = visualElement.Direction1;
        var normal = visualElement.Normal;
        
        sector.AffineMap
            .Rotate(
                LinBasisVectorPair3D.PxPy,
                vector,
                normal
            ).Translate(visualElement.Center);

        AddStatement(sector);

        return visualElement;
    }


    public override GrVisualCircleRingSurface3D AddRing(GrVisualCircleRingSurface3D visualElement)
    {
        return visualElement.Style switch
        {
            GrVisualSurfaceThickStyle3D thickStyle =>
                AddRing(visualElement, thickStyle),

            GrVisualSurfaceThinStyle3D thinStyle =>
                AddRing(visualElement, thinStyle),

            _ => throw new InvalidCastException()
        };
    }

    private GrVisualCircleRingSurface3D AddRing(GrVisualCircleRingSurface3D visualElement, GrVisualSurfaceThickStyle3D thickStyle)
    {
        var diameter =
            visualElement.MaxRadius + visualElement.MinRadius;

        var thickness =
            Math.Abs(visualElement.MaxRadius - visualElement.MinRadius);

        var tube = GrPovRayObject.Torus(
            diameter / 2,
            thickness / 2
        ).SetMaterial((GrPovRayMaterial) thickStyle.Material);

        tube.AffineMap
            .Reset()
            .Rotate(
                LinFloat64Vector3D.E2,
                visualElement.Normal
            ).Scale(1, thickStyle.Thickness / thickness, 1)
            .Translate(visualElement.Center);

        AddStatement(tube);

        return visualElement;
    }

    private GrVisualCircleRingSurface3D AddRing(GrVisualCircleRingSurface3D visualElement, GrVisualSurfaceThinStyle3D thinStyle)
    {
        var diameter =
            visualElement.MaxRadius + visualElement.MinRadius;

        var thickness =
            Math.Abs(visualElement.MaxRadius - visualElement.MinRadius);

        var tube = GrPovRayObject.Torus(
            diameter / 2,
            thickness / 2
        ).SetMaterial((GrPovRayMaterial) thinStyle.Material);

        tube.AffineMap
            .Reset()
            .Rotate(
                LinFloat64Vector3D.E2,
                visualElement.Normal
            ).Scale(1, MinThickness / thickness, 1)
            .Translate(visualElement.Center);

        AddStatement(tube);

        return visualElement;
    }

    
    public GrPovRaySceneComposer AddDefaultAxes(ILinFloat64Vector3D axesOrigin)
    {
        // Add reference unit axis frame
        AddElement(
            GrVisualFrame3D.CreateStatic(
                "axisFrame",
                new GrVisualFrameStyle3D
                {
                    OriginStyle = SceneStatements.DeclareMaterial("axisFrameOriginMaterial", Color.DarkGray).CreateThickSurfaceStyle(0.075),
                    Direction1Style = SceneStatements.DeclareMaterial("axisFrameXMaterial", Color.DarkRed).CreateTubeCurveStyle(0.035),
                    Direction2Style = SceneStatements.DeclareMaterial("axisFrameYMaterial", Color.DarkGreen).CreateTubeCurveStyle(0.035),
                    Direction3Style = SceneStatements.DeclareMaterial("axisFrameZMaterial", Color.DarkBlue).CreateTubeCurveStyle(0.035)
                },
                axesOrigin
            )
        );

        return this;
    }


    public GrPovRaySceneComposer AddDefaultGridXy(int gridUnitCount, double unitSize = 1, double zValue = 0, double opacity = 0.25)
    {
        AddSquareGrid(
            GrVisualSquareGrid3D.DefaultXy(
                zValue * LinFloat64Vector3D.E3, 
                gridUnitCount,
                unitSize, 
                opacity
            )
        );

        return this;
    }

    public GrPovRaySceneComposer AddDefaultGridYz(int gridUnitCount, double unitSize = 1, double xValue = 0, double opacity = 0.25)
    {
        AddSquareGrid(
            GrVisualSquareGrid3D.DefaultYz(
                xValue * LinFloat64Vector3D.E1, 
                gridUnitCount,
                unitSize, 
                opacity
            )
        );

        return this;
    }

    public GrPovRaySceneComposer AddDefaultGridZx(int gridUnitCount, double unitSize = 1, double yValue = 0, double opacity = 0.25)
    {
        AddSquareGrid(
            GrVisualSquareGrid3D.DefaultZx(
                yValue * LinFloat64Vector3D.E2, 
                gridUnitCount,
                unitSize, 
                opacity
            )
        );

        return this;
    }

    public GrPovRaySceneComposer AddDefaultGridXyz(int gridUnitCount, double unitSize = 1, double opacity = 0.25)
    {
        AddSquareGrid(
            GrVisualSquareGrid3D.DefaultXy(
                LinFloat64Vector3D.Zero, 
                gridUnitCount,
                unitSize, 
                opacity
            )
        );

        AddSquareGrid(
            GrVisualSquareGrid3D.DefaultYz(
                LinFloat64Vector3D.Zero, 
                gridUnitCount,
                unitSize, 
                opacity
            )
        );

        AddSquareGrid(
            GrVisualSquareGrid3D.DefaultZx(
                LinFloat64Vector3D.Zero, 
                gridUnitCount,
                unitSize, 
                opacity
            )
        );

        return this;
    }


    public GrPovRaySceneComposer AddDefaultEnvironment(int gridUnitCount)
    {
        throw new NotImplementedException();

        ////var scene = MainSceneComposer.SceneObject;
        ////scene.SceneProperties.AmbientColor = Color.AliceBlue;

        //// Add scene environment
        //SceneObject.AddEnvironmentHelper(
        //    "environmentHelper",

        //    new GrPovRayEnvironmentHelperOptions
        //    {
        //        GroundYBias = 0.01,
        //        SkyboxColor = Color.LightSkyBlue,
        //        GroundColor = Color.White,
        //        CreateGround = true,
        //        GroundSize = 8,
        //        SkyboxSize = gridUnitCount + 10
        //    }
        //);

        //return this;
    }

    public GrPovRaySceneComposer AddDefaultPerspectiveCamera(double cameraDistance, LinFloat64Angle alpha, LinFloat64Angle beta)
    {
        SceneObject.Camera =
            GrPovRayCamera.ArcRotatePerspective(
                alpha,
                beta,
                cameraDistance,
                LinFloat64Vector3D.E2 * 1.001,
                67.DegreesToPolarAngle(),
                RenderingOptions.AspectRatio
            );

        return this;
    }

    public GrPovRaySceneComposer AddDefaultOrthographicCamera(double cameraDistance, LinFloat64Angle alpha, LinFloat64Angle beta, double width, double height)
    {
        SceneObject.Camera = GrPovRayCamera.ArcRotateOrthographic(
            alpha,
            beta,
            cameraDistance,
            LinFloat64Vector3D.Zero,
            width, 
            height
        );

        return this;
    }


    public GrPovRaySceneComposer RenderScene(string fileName)
    {
        var sceneCode = SceneObject.GetPovRayCode();
        var optionsCode = RenderingOptions.GetPovRayCode();

        var sceneFilePath = WorkingFolder.GetFilePath(fileName, "pov");
        var optionsFilePath = WorkingFolder.GetFilePath(fileName, "ini");
        var imageFilePath = WorkingFolder.GetFilePath(fileName, "png");
            
        File.WriteAllText(sceneFilePath, sceneCode);
        File.WriteAllText(optionsFilePath, optionsCode);

        if (File.Exists(imageFilePath))
            File.Delete(imageFilePath);

        using var instance = Instance.Start(
            PovRayFolder.GetFilePath("pvengine.exe"),
            @$"/NORESTORE ""{optionsFilePath}"" /EXIT /RENDER ""{sceneFilePath}"""
        );
                
        var result = instance.WaitForExit();

        return this;
    }
}