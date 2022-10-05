using System.Numerics;
using GraphicsComposerLib.Rendering.BabylonJs.Constants;
using GraphicsComposerLib.Rendering.BabylonJs.Materials;
using GraphicsComposerLib.Rendering.BabylonJs.Meshes;
using GraphicsComposerLib.Rendering.BabylonJs.Textures;
using GraphicsComposerLib.Rendering.BabylonJs.Values;
using GraphicsComposerLib.Rendering.Images;
using GraphicsComposerLib.Rendering.Visuals.Space3D;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Basic;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Curves;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Grids;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Groups;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Images;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;
using Humanizer;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Constants;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Rendering.BabylonJs;

public class GrBabylonJsSceneComposer3D :
    GrVisualElementsSceneComposer3D<GrBabylonJsScene>
{
    public override GrBabylonJsScene SceneObject { get; }

    public GrBabylonJsSnapshotSpecs SnapshotSpecs { get; }

    public GrBabylonJsColor3Value BackgroundColor { get; set; }
        = "BABYLON.Color3.Teal()";

    public GrBabylonJsGridMaterialKind GridMaterialKind { get; set; }
        = GrBabylonJsGridMaterialKind.GridMaterial;

    public bool ShowDebugLayer { get; set; }


    public GrBabylonJsSceneComposer3D()
    {
        SceneObject = new GrBabylonJsScene("scene")
            .SetOptions(new GrBabylonJsScene.SceneOptions())
            .SetProperties(new GrBabylonJsScene.SceneProperties());

        SnapshotSpecs = new GrBabylonJsSnapshotSpecs();
    }

    public GrBabylonJsSceneComposer3D(string constName)
    {
        SceneObject = new GrBabylonJsScene(constName)
            .SetOptions(new GrBabylonJsScene.SceneOptions())
            .SetProperties(new GrBabylonJsScene.SceneProperties());

        SnapshotSpecs = new GrBabylonJsSnapshotSpecs();
    }

    public GrBabylonJsSceneComposer3D(string constName, GrBabylonJsSnapshotSpecs snapshotSpecs)
    {
        SceneObject = new GrBabylonJsScene(constName)
            .SetOptions(new GrBabylonJsScene.SceneOptions())
            .SetProperties(new GrBabylonJsScene.SceneProperties());

        SnapshotSpecs = snapshotSpecs;
    }


    public override void AddMaterial(IGrVisualElementMaterial3D material)
    {
        if (material is not GrBabylonJsMaterial babylonMaterial)
            throw new ArgumentException(nameof(material));

        if (SceneObject.ObjectList.Contains(babylonMaterial))
            return;

        SceneObject.ObjectList.Add(babylonMaterial);
    }

    public override void AddLaTeXText(GrVisualLaTeXText3D visualElement)
    {
        var latexImage =
            visualElement.GetImageData();

        var latexImageString =
            latexImage.GetBase64HtmlString();

        SceneObject.AddTexture(
            $"{visualElement.Name}Texture",

            latexImageString,

            new GrBabylonJsTexture.TextureProperties
            {
                HasAlpha = true
            }
        );

        SceneObject.AddStandardMaterial(
            $"{visualElement.Name}Material",

            new GrBabylonJsStandardMaterial.StandardMaterialProperties
            {
                DiffuseTexture = $"{visualElement.Name}Texture",
                UseAlphaFromDiffuseTexture = true,
                BackFaceCulling = false,
                TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend
            }
        );

        SceneObject.AddPlane(
            $"{visualElement.Name}Plane",

            new GrBabylonJsPlane.PlaneOptions
            {
                Width = visualElement.ScalingFactor * latexImage.Width,
                Height = visualElement.ScalingFactor * latexImage.Height
            },

            new GrBabylonJsMesh.MeshProperties
            {
                BillboardMode = GrBabylonJsBillboardMode.All,
                Material = $"{visualElement.Name}Material",
                Position = visualElement.Origin.GetBabylonJsCode(),
                //AlphaIndex = int.MaxValue
            }
        );
    }

    public override void AddXzSquareGrid(GrVisualXzSquareGrid3D visualElement)
    {
        if (GridMaterialKind == GrBabylonJsGridMaterialKind.TexturedMaterial)
        {
            var gridImage =
                visualElement.GetImage();

            //gridImageString.Save(
            //    $"{visualElement.Name}.png"
            //);

            var gridImageString =
                gridImage.PngToBase64HtmlString();

            SceneObject.AddTexture(
                $"{visualElement.Name}Texture",

                gridImageString, //@"'./Textures/{visualElement.Name}.png'",

                new GrBabylonJsTexture.TextureProperties
                {
                    HasAlpha = true,
                    UScale = visualElement.UnitCountX,
                    VScale = visualElement.UnitCountZ
                }
            );

            SceneObject.AddStandardMaterial(
                $"{visualElement.Name}Material",

                new GrBabylonJsStandardMaterial.StandardMaterialProperties
                {
                    DiffuseTexture = $"{visualElement.Name}Texture",
                    UseAlphaFromDiffuseTexture = true,
                    TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend,
                    Alpha = visualElement.Opacity
                }
            );
        }
        else
        {
            SceneObject.AddGridMaterial(
                $"{visualElement.Name}Material",

                new GrBabylonJsGridMaterial.GridMaterialProperties
                {
                    LineColor = visualElement.BaseLineColor,
                    MainColor = visualElement.BaseSquareColor,
                    Opacity = visualElement.Opacity,
                    //Alpha = visualElement.Opacity,
                    //TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend,
                    GridRatio = 1d,
                    MajorUnitFrequency = 1,
                    MinorUnitVisibility = 0.5,
                    UseMaxLine = true
                }
            );
        }

        SceneObject.AddGround(
            $"{visualElement.Name}Ground",

            new GrBabylonJsGround.GroundOptions
            {
                Width = visualElement.SizeX,
                Height = visualElement.SizeZ
            },

            new GrBabylonJsMesh.MeshProperties
            {
                Material = $"{visualElement.Name}Material",
                Position = visualElement.Origin +
                           new Tuple3D(
                               0.5d * visualElement.SizeX,
                               0,
                               0.5d * visualElement.SizeZ
                           )
            }
        );
    }

    public override void AddImage(GrVisualImage3D visualElement)
    {
        switch (visualElement)
        {
            case GrVisualLaTeXText3D latexTextImage:
                AddLaTeXText(latexTextImage);

                return;

            case GrVisualXzSquareGrid3D xzSquareGridImage:
                AddXzSquareGrid(xzSquareGridImage);

                return;
        }

        var latexImage =
            visualElement.GetImage();

        //latexImage.Save(
        //    $"{visualElement.Name}.png"
        //);

        var latexImageString =
            latexImage.PngToBase64HtmlString();

        SceneObject.AddTexture(
            $"{visualElement.Name}Texture",

            latexImageString, //@"'./Textures/{visualElement.Name}.png'",

            new GrBabylonJsTexture.TextureProperties
            {
                HasAlpha = true
            }
        );
    }

    public override void AddPoint(GrVisualPoint3D visualElement)
    {
        if (visualElement.TextImage is not null)
            AddImage(visualElement.TextImage);

        SceneObject.AddSphere(
            $"{visualElement.Name}Sphere",

            new GrBabylonJsSphere.SphereOptions
            {
                Diameter = visualElement.Style.Thickness,
                Segments = 32
            },

            new GrBabylonJsMesh.MeshProperties
            {
                Material = visualElement.Style.Material.MaterialName,
                Position = visualElement.Position.ToBabylonJsVector3Value()
            }
        );
    }

    public override void AddVector(GrVisualVector3D visualElement)
    {
        var length = visualElement.GetLength();

        if (length == 0)
            return;

        if (visualElement.TextImage is not null)
            AddImage(visualElement.TextImage);

        var cylinderBaseDiameter = visualElement.Style.Thickness * 3;
        var cylinderHeight = cylinderBaseDiameter * 1.5d;
        var lineLength = length - cylinderHeight;

        if (lineLength < 0)
        {
            cylinderHeight = length;
            cylinderBaseDiameter = cylinderHeight * 2d / 3d;
        }

        var direction = visualElement.GetUnitDirection();
        const Axis3D axis = Axis3D.PositiveY;
        var axisCode = axis.GetBabylonJsCode();
        var quaternion = axis.CreateAxisToVectorRotationQuaternion(direction);

        SceneObject.AddCylinder(
            $"{visualElement.Name}Cylinder",

            new GrBabylonJsCylinder.CylinderOptions
            {
                DiameterTop = 0,
                DiameterBottom = cylinderBaseDiameter,
                Height = cylinderHeight,
                Cap = GrBabylonJsMeshCap.Start
            },

            new GrBabylonJsMesh.MeshProperties
            {
                Material = visualElement.Style.Material.MaterialName,
                Position = visualElement.Origin + direction * (length - cylinderHeight / 2d),
                RotationQuaternion = quaternion
            }
        );

        if (lineLength <= 0)
            return;

        lineLength += visualElement.Style.Thickness;

        SceneObject.AddCapsule(
            $"{visualElement.Name}Line",

            new GrBabylonJsCapsule.CapsuleOptions
            {
                Radius = visualElement.Style.Thickness / 2d,
                Height = lineLength,
                Tessellation = 32,
                Subdivisions = 32,
                CapSubdivisions = 32,
                Orientation = axisCode
            },

            new GrBabylonJsMesh.MeshProperties
            {
                Material = visualElement.Style.Material.MaterialName,
                Position = visualElement.Origin + direction * (lineLength / 2d),
                RotationQuaternion = quaternion
            }
        );
    }

    public override void AddLineSegment(GrVisualLineSegment3D visualElement)
    {
        if (visualElement.GetLength().IsNearZero())
            return;
        
        if (visualElement.TextImage is not null)
            AddImage(visualElement.TextImage);

        var length = visualElement.GetLength();
        var midPoint = 0.5d.Lerp(visualElement.Position1, visualElement.Position2);
        var direction = visualElement.GetUnitDirection();
        const Axis3D axis = Axis3D.PositiveY;
        var axisCode = axis.GetBabylonJsCode();
        var quaternion = axis.CreateAxisToVectorRotationQuaternion(direction);

        if (visualElement.Style is GrVisualCurveTubeStyle3D tubeStyle)
        {
            length += tubeStyle.Thickness;

            SceneObject.AddCapsule(
                $"{visualElement.Name}Capsule",

                new GrBabylonJsCapsule.CapsuleOptions
                {
                    Radius = tubeStyle.Thickness / 2d,
                    Height = length,
                    Tessellation = 32,
                    Subdivisions = 32,
                    CapSubdivisions = 32,
                    Orientation = axisCode
                },

                new GrBabylonJsMesh.MeshProperties
                {
                    Material = tubeStyle.Material.MaterialName,
                    Position = midPoint,
                    RotationQuaternion = quaternion
                }
            );

            return;
        }

        if (visualElement.Style is GrVisualCurveSolidLineStyle3D solidLineStyle)
        {
            SceneObject.AddLines(
                $"{visualElement.Name}Line",

                new GrBabylonJsLines.LinesOptions
                {
                    Points = new[]
                    {
                        visualElement.Position1,
                        visualElement.Position2
                    }
                },

                new GrBabylonJsLinesMesh.LinesMeshProperties
                {
                    Color = solidLineStyle.Color
                }
            );

            return;
        }

        if (visualElement.Style is GrVisualCurveDashedLineStyle3D dashedLineStyle)
        {
            SceneObject.AddDashedLines(
                $"{visualElement.Name}DashedLine",

                new GrBabylonJsDashedLines.DashedLinesOptions
                {
                    Points = new[]
                    {
                        visualElement.Position1,
                        visualElement.Position2
                    },

                    DashNumber = dashedLineStyle.DashPerLine,
                    DashSize = dashedLineStyle.DashOn,
                    GapSize = dashedLineStyle.DashOff
                },

                new GrBabylonJsLinesMesh.LinesMeshProperties
                {
                    Color = dashedLineStyle.Color
                }
            );

            return;
        }

        throw new ArgumentOutOfRangeException();
    }

    public override void AddLineCurve(GrVisualLineCurve3D visualElement)
    {
        if (visualElement.TextImage is not null)
            AddImage(visualElement.TextImage);

        var pathName = $"{visualElement.Name}Path";

        SceneObject.AddCurve3(
            pathName,
            visualElement.PositionList.GetBabylonJsCode()
        );

        if (visualElement.Style is GrVisualCurveTubeStyle3D tubeStyle)
        {
            SceneObject.AddTube(
                $"{visualElement.Name}Tube",

                new GrBabylonJsTube.TubeOptions
                {
                    Radius = tubeStyle.Thickness / 2d,
                    Path = $"{pathName}.getPoints()",
                    Tessellation = 32
                },

                new GrBabylonJsMesh.MeshProperties
                {
                    Material = tubeStyle.Material.MaterialName
                }
            );

            return;
        }

        if (visualElement.Style is GrVisualCurveSolidLineStyle3D solidLineStyle)
        {
            SceneObject.AddLines(
                $"{visualElement.Name}Line",

                new GrBabylonJsLines.LinesOptions
                {
                    Points = $"{pathName}.getPoints()"
                },

                new GrBabylonJsLinesMesh.LinesMeshProperties
                {
                    Color = solidLineStyle.Color
                }
            );

            return;
        }

        throw new ArgumentOutOfRangeException();
    }

    public override void AddCircleCurve(GrVisualCircleCurve3D visualElement)
    {
        if (visualElement.TextImage is not null)
            AddImage(visualElement.TextImage);

        var normal = visualElement.Normal.ToUnitVector();
        var quaternion = Axis3D.PositiveY.CreateAxisToVectorRotationQuaternion(normal);

        if (visualElement.Style is GrVisualCurveTubeStyle3D tubeStyle)
        {
            SceneObject.AddTorus(
                $"{visualElement.Name}Torus",

                new GrBabylonJsTorus.TorusOptions
                {
                    Diameter = visualElement.Radius * 2,
                    Thickness = tubeStyle.Thickness,
                    Tessellation = 320
                },

                new GrBabylonJsMesh.MeshProperties
                {
                    Material = tubeStyle.Material.MaterialName,
                    Position = visualElement.Center.ToBabylonJsVector3Value(),
                    RotationQuaternion = quaternion
                }
            );

            return;
        }

        if (visualElement.Style is GrVisualCurveSolidLineStyle3D solidLineStyle)
        {
            var (point1, point2, point3) =
                visualElement.GetPointsTriplet();

            var pathName = $"{visualElement.Name}Path";

            SceneObject.AddArcThru3Points(
                pathName,
                point1,
                point2,
                point3,
                320,
                false,
                true
            );

            SceneObject.AddLines(
                $"{visualElement.Name}Line",

                new GrBabylonJsLines.LinesOptions
                {
                    Points = $"{pathName}.getPoints()"
                },

                new GrBabylonJsLinesMesh.LinesMeshProperties
                {
                    Color = solidLineStyle.Color
                }
            );

            return;
        }

        if (visualElement.Style is GrVisualCurveDashedLineStyle3D dashedLineStyle)
        {
            var length = visualElement.GetLength();
            var pathPointCount = (int)(2 * length + 1).Ceiling();

            var (point1, point2, point3) =
                visualElement.GetPointsTriplet();

            var pathName = $"{visualElement.Name}Path";

            SceneObject.AddArcThru3Points(
                pathName,
                point1,
                point2,
                point3,
                pathPointCount,
                false,
                true
            );

            SceneObject.AddDashedLines(
                $"{visualElement.Name}Line",

                new GrBabylonJsDashedLines.DashedLinesOptions
                {
                    Points = $"{pathName}.getPoints()",
                    DashNumber = pathPointCount * dashedLineStyle.DashPerLine,
                    DashSize = dashedLineStyle.DashOn,
                    GapSize = dashedLineStyle.DashOff
                },

                new GrBabylonJsLinesMesh.LinesMeshProperties
                {
                    Color = dashedLineStyle.Color
                }
            );

            return;
        }

        throw new ArgumentOutOfRangeException();
    }

    public override void AddCircleCurveArc(GrVisualCircleCurveArc3D visualElement)
    {
        if (visualElement.TextImage is not null)
            AddImage(visualElement.TextImage);

        var length = visualElement.GetLength();
        var pathPointCount = (int)(2 * length + 1).Ceiling();

        var (point1, point2, point3) =
            visualElement.GetArcPointsTriplet();

        var pathName = $"{visualElement.Name}Path";

        SceneObject.AddArcThru3Points(
            pathName,
            point1,
            point2,
            point3,
            pathPointCount,
            false,
            false
        );

        if (visualElement.Style is GrVisualCurveTubeStyle3D tubeStyle)
        {
            SceneObject.AddTube(
                $"{visualElement.Name}Tube",

                new GrBabylonJsTube.TubeOptions
                {
                    Path = $"{pathName}.getPoints()",
                    Radius = tubeStyle.Thickness / 2d,
                    Tessellation = 320,
                    Cap = GrBabylonJsMeshCap.StartAndEnd
                },

                new GrBabylonJsMesh.MeshProperties
                {
                    Material = tubeStyle.Material.MaterialName
                }
            );

            return;
        }

        if (visualElement.Style is GrVisualCurveSolidLineStyle3D solidLineStyle)
        {
            SceneObject.AddLines(
                $"{visualElement.Name}Line",

                new GrBabylonJsLines.LinesOptions
                {
                    Points = $"{pathName}.getPoints()"
                },

                new GrBabylonJsLinesMesh.LinesMeshProperties
                {
                    Color = solidLineStyle.Color
                }
            );

            return;
        }

        if (visualElement.Style is GrVisualCurveDashedLineStyle3D dashedLineStyle)
        {
            SceneObject.AddDashedLines(
                $"{visualElement.Name}Line",

                new GrBabylonJsDashedLines.DashedLinesOptions
                {
                    Points = $"{pathName}.getPoints()",
                    DashNumber = pathPointCount * dashedLineStyle.DashPerLine,
                    DashSize = dashedLineStyle.DashOn,
                    GapSize = dashedLineStyle.DashOff
                },

                new GrBabylonJsLinesMesh.LinesMeshProperties
                {
                    Color = dashedLineStyle.Color
                }
            );

            return;
        }

        throw new ArgumentOutOfRangeException();
    }

    public override void AddRectangleSurface(GrVisualRectangleSurface3D visualElement)
    {
        if (visualElement.TextImage is not null)
            AddImage(visualElement.TextImage);

        var quaternion = Axis3D.PositiveX.CreateAxisPairToVectorPairRotationQuaternion(
            Axis3D.PositiveY,
            visualElement.WidthUnitDirection,
            visualElement.HeightUnitDirection
        );

        if (visualElement.Style is GrVisualThickSurfaceStyle3D thickStyle)
        {
            SceneObject.AddBox(
                $"{visualElement.Name}Box",

                new GrBabylonJsBox.BoxOptions
                {
                    Width = visualElement.Width,
                    Height = visualElement.Height,
                    Depth = thickStyle.Thickness
                },

                new GrBabylonJsMesh.MeshProperties
                {
                    Material = visualElement.Style.Material.MaterialName,
                    Position = visualElement.BottomLeftCorner + 0.5d * (visualElement.WidthDirection + visualElement.HeightDirection),
                    RotationQuaternion = quaternion
                }
            );
        }
        else if (visualElement.Style is GrVisualThinSurfaceStyle3D thinStyle)
        {
            SceneObject.AddPlane(
                $"{visualElement.Name}Plane",

                new GrBabylonJsPlane.PlaneOptions
                {
                    Width = visualElement.Width,
                    Height = visualElement.Height,
                    SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack
                },

                new GrBabylonJsMesh.MeshProperties
                {
                    Material = visualElement.Style.Material.MaterialName,
                    Position = visualElement.BottomLeftCorner + 0.5d * (visualElement.WidthDirection + visualElement.HeightDirection),
                    RotationQuaternion = quaternion
                }
            );
        }
    }

    public override void AddCircleSurface(GrVisualCircleSurface3D visualElement)
    {
        if (visualElement.TextImage is not null)
            AddImage(visualElement.TextImage);

        var normal = visualElement.Normal.ToUnitVector();

        if (visualElement.Style is GrVisualThickSurfaceStyle3D thickStyle)
        {
            var quaternion = Axis3D.PositiveY.CreateAxisToVectorRotationQuaternion(normal);

            SceneObject.AddSphere(
                $"{visualElement.Name}Sphere",

                new GrBabylonJsSphere.SphereOptions
                {
                    DiameterX = visualElement.Radius * 2,
                    DiameterY = thickStyle.Thickness,
                    DiameterZ = visualElement.Radius * 2,
                    Segments = 320
                },

                new GrBabylonJsMesh.MeshProperties
                {
                    Material = visualElement.Style.Material.MaterialName,
                    Position = visualElement.Center.ToBabylonJsVector3Value(),
                    RotationQuaternion = quaternion
                }
            );
        }
        else if (visualElement.Style is GrVisualThinSurfaceStyle3D thinStyle)
        {
            var quaternion = Axis3D.PositiveZ.CreateAxisToVectorRotationQuaternion(normal);

            SceneObject.AddDisc(
                $"{visualElement.Name}Disc",

                new GrBabylonJsDisc.DiscOptions
                {
                    Radius = visualElement.Radius,
                    SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack,
                    Tessellation = 320
                },

                new GrBabylonJsMesh.MeshProperties
                {
                    Material = visualElement.Style.Material.MaterialName,
                    Position = visualElement.Center.ToBabylonJsVector3Value(),
                    RotationQuaternion = quaternion
                }
            );
        }
    }

    public override void AddCircleSurfaceArc(GrVisualCircleSurfaceArc3D visualElement)
    {
        if (visualElement.TextImage is not null)
            AddImage(visualElement.TextImage);

        var vector = visualElement.GetArcStartUnitVector();
        var normal = visualElement.GetUnitNormal();

        if (visualElement.Style is GrVisualThickSurfaceStyle3D thickStyle)
        {
            var quaternion =
                Axis3D.PositiveX.CreateAxisPairToVectorPairRotationQuaternion(
                    Axis3D.PositiveY,
                    vector,
                    normal
                );

            SceneObject.AddSphere(
                $"{visualElement.Name}Sphere",

                new GrBabylonJsSphere.SphereOptions
                {
                    Arc = visualElement.GetArcRatio(),
                    DiameterX = visualElement.Radius * 2,
                    DiameterY = thickStyle.Thickness,
                    DiameterZ = visualElement.Radius * 2,
                    Segments = 320
                },

                new GrBabylonJsMesh.MeshProperties
                {
                    Material = thickStyle.Material.MaterialName,
                    Position = visualElement.Center.ToBabylonJsVector3Value(),
                    RotationQuaternion = quaternion
                }
            );


            var qYx =
                Axis3D.PositiveY.CreateAxisToAxisRotationQuaternion(Axis3D.PositiveX);

            SceneObject.AddDisc(
                $"{visualElement.Name}Disc1",

                new GrBabylonJsDisc.DiscOptions
                {
                    Arc = 0.5d,
                    Radius = visualElement.Radius,
                    Tessellation = 320,
                    SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack
                },

                new GrBabylonJsMesh.MeshProperties
                {
                    Material = thickStyle.Material.MaterialName,
                    Scaling = new Tuple3D(thickStyle.Thickness / (2d * visualElement.Radius), 1, 1),
                    Position = visualElement.Center.ToBabylonJsVector3Value(),
                    RotationQuaternion = qYx.Concatenate(quaternion)
                }
            );


            var q2 = Quaternion.CreateFromAxisAngle(
                Vector3.UnitY,
                (float)visualElement.GetAngle()
            );

            SceneObject.AddDisc(
                $"{visualElement.Name}Disc2",

                new GrBabylonJsDisc.DiscOptions()
                {
                    Arc = 0.5d,
                    Radius = visualElement.Radius,
                    Tessellation = 320,
                    SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack
                },

                new GrBabylonJsMesh.MeshProperties
                {
                    Material = visualElement.Style.Material.MaterialName,
                    Scaling = new Tuple3D(thickStyle.Thickness / (2d * visualElement.Radius), 1, 1),
                    Position = visualElement.Center.ToBabylonJsVector3Value(),
                    RotationQuaternion = qYx.Concatenate(q2, quaternion)
                }
            );
        }
        else
        {
            var quaternion =
                Axis3D.PositiveX.CreateAxisPairToVectorPairRotationQuaternion(
                    Axis3D.PositiveZ,
                    vector,
                    normal
                );

            SceneObject.AddDisc(
                $"{visualElement.Name}Disc",

                new GrBabylonJsDisc.DiscOptions
                {
                    Arc = visualElement.GetArcRatio(),
                    Radius = visualElement.Radius,
                    SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack,
                    Tessellation = 320
                },

                new GrBabylonJsMesh.MeshProperties
                {
                    Material = visualElement.Style.Material.MaterialName,
                    Position = visualElement.Center.ToBabylonJsVector3Value(),
                    RotationQuaternion = quaternion
                }
            );
        }
    }

    public override void AddRingSurface(GrVisualRingSurface3D visualElement)
    {
        if (visualElement.TextImage is not null)
            AddImage(visualElement.TextImage);

        var normal = visualElement.Normal.ToUnitVector();

        if (visualElement.Style is GrVisualThickSurfaceStyle3D thickStyle)
        {
            var quaternion = Axis3D.PositiveY.CreateAxisToVectorRotationQuaternion(normal);

            var diameter =
                (visualElement.MaxRadius + visualElement.MinRadius);

            var thickness =
                Math.Abs(visualElement.MaxRadius - visualElement.MinRadius);

            SceneObject.AddTorus(
                $"{visualElement.Name}Torus",

                new GrBabylonJsTorus.TorusOptions
                {
                    Diameter = diameter,
                    Thickness = thickness,
                    Tessellation = 320
                },

                new GrBabylonJsMesh.MeshProperties
                {
                    Material = visualElement.Style.Material.MaterialName,
                    Position = visualElement.Center.ToBabylonJsVector3Value(),
                    Scaling = new Tuple3D(1, thickStyle.Thickness / thickness, 1),
                    RotationQuaternion = quaternion
                }
            );

            return;
        }
        //else
        //{
        //    var quaternion = Axis3D.PositiveZ.CreateAxisToVectorRotationQuaternion(normal);

        //    SceneObject.AddDisc(
        //        $"{visualElement.Name}Disc",

        //        new GrBabylonJsDisc.DiscOptions
        //        {
        //            Radius = visualElement.Radius,
        //            SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack,
        //            Tessellation = 320
        //        },

        //        new GrBabylonJsMesh.MeshProperties
        //        {
        //            Material = visualElement.Style.Material.MaterialName,
        //            Position = visualElement.Center.ToBabylonJsVector3Value(),
        //            Orientation = quaternion
        //        }
        //    );
        //}

        throw new ArgumentOutOfRangeException();
    }

    public override void AddRightAngle(GrVisualRightAngle3D visualElement)
    {
        if (visualElement.TextImage is not null)
            AddImage(visualElement.TextImage);

        //var pathPointCount = 3;

        var pathCode =
            visualElement
                .GetArcPointsTriplet()
                .GetBabylonJsCode();

        if (visualElement.Style is GrVisualCurveTubeStyle3D tubeStyle)
        {
            SceneObject.AddTube(
                $"{visualElement.Name}Tube",

                new GrBabylonJsTube.TubeOptions
                {
                    Path = pathCode,
                    Radius = tubeStyle.Thickness / 2d,
                    Tessellation = 32,
                    Cap = GrBabylonJsMeshCap.StartAndEnd
                },

                new GrBabylonJsMesh.MeshProperties
                {
                    Material = tubeStyle.Material.MaterialName
                }
            );

            return;
        }

        if (visualElement.Style is GrVisualCurveSolidLineStyle3D solidLineStyle)
        {
            SceneObject.AddLines(
                $"{visualElement.Name}Line",

                new GrBabylonJsLines.LinesOptions
                {
                    Points = pathCode
                },

                new GrBabylonJsLinesMesh.LinesMeshProperties
                {
                    Color = solidLineStyle.Color
                }
            );

            return;
        }

        if (visualElement.Style is GrVisualCurveDashedLineStyle3D dashedLineStyle)
        {
            SceneObject.AddLines(
                $"{visualElement.Name}Line",

                new GrBabylonJsLines.LinesOptions
                {
                    Points = pathCode
                },

                new GrBabylonJsLinesMesh.LinesMeshProperties
                {
                    Color = dashedLineStyle.Color
                }
            );

            return;
        }

        throw new ArgumentOutOfRangeException();
    }


    public string GetCreateSceneCode()
    {
        var sceneName = SceneObject.ConstName;
        var sceneCode = SceneObject.GetCode();

        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine($"function create{sceneName.Pascalize()}() {{")
            .IncreaseIndentation();

        codeComposer.AppendLineAtNewLine(@$"
{sceneCode}

const light = new BABYLON.HemisphericLight(""light"", new BABYLON.Vector3(0, 1, 0), {sceneName});
//light.intensity = 0.7;

");

        foreach (var babylonObject in SceneObject.ObjectList)
            codeComposer.AppendLineAtNewLine(babylonObject.GetCode());

        if (SnapshotSpecs.Enabled)
            codeComposer.AppendLineAtNewLine(
                SnapshotSpecs.GetSnapshotCode(sceneName)
            );

        else if (ShowDebugLayer)
            codeComposer.AppendLineAtNewLine(
                $"{sceneName}.debugLayer.show();"
            );

        codeComposer
            .AppendLine()
            .AppendLine($"return {sceneName};")
            .DecreaseIndentation()
            .AppendLine("}");

        return codeComposer.ToString();
    }

    public string GetAddSceneCode()
    {
        var sceneName = SceneObject.ConstName;

        return $"window.scenes.push( create{sceneName.Pascalize()}() );";
    }
}