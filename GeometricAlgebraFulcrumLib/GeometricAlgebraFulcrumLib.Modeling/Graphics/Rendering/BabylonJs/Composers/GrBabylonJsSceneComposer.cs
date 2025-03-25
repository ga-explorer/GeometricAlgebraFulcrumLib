using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.VGa.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Cameras;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Grids;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Images;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using GeometricAlgebraFulcrumLib.Utilities.Web.Html.Media;
using GeometricAlgebraFulcrumLib.Utilities.Web.Images;
using Humanizer;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Composers;

public class GrBabylonJsSceneComposer :
    GrVisualSceneComposer3D<GrBabylonJsScene>
{
    private readonly Dictionary<string, GrBabylonJsMaterial> _colorMaterialCache
        = new Dictionary<string, GrBabylonJsMaterial>();


    public override GrBabylonJsScene SceneObject { get; }

    public GrBabylonJsSnapshotSpecs SnapshotSpecs { get; }

    public GrBabylonJsColor3Value BackgroundColor { get; set; }
        = "BABYLON.Color3.Teal()";

    public GrBabylonJsGridMaterialKind GridMaterialKind { get; set; }
        = GrBabylonJsGridMaterialKind.GridMaterial;

    public bool ShowDebugLayer { get; set; }

    public GrBabylonJsDisc PrototypeDisc { get; private set; }

    public GrBabylonJsSphere PrototypeInnerSphere { get; private set; }

    public GrBabylonJsSphere PrototypeOuterSphere { get; private set; }

    public GrBabylonJsCylinder PrototypeCone { get; private set; }


    public GrBabylonJsSceneComposer()
    {
        SceneObject = new GrBabylonJsScene("scene")
            .SetOptions(new GrBabylonJsSceneOptions())
            .SetProperties(new GrBabylonJsSceneProperties());

        SnapshotSpecs = new GrBabylonJsSnapshotSpecs();

        AddInitialObjects();
    }

    public GrBabylonJsSceneComposer(string constName)
    {
        SceneObject = new GrBabylonJsScene(constName)
            .SetOptions(new GrBabylonJsSceneOptions())
            .SetProperties(new GrBabylonJsSceneProperties());

        SnapshotSpecs = new GrBabylonJsSnapshotSpecs();

        AddInitialObjects();
    }

    public GrBabylonJsSceneComposer(string constName, GrBabylonJsSnapshotSpecs snapshotSpecs)
    {
        SceneObject = new GrBabylonJsScene(constName)
            .SetOptions(new GrBabylonJsSceneOptions())
            .SetProperties(new GrBabylonJsSceneProperties());

        SnapshotSpecs = snapshotSpecs;

        AddInitialObjects();
    }


    private void AddInitialObjects()
    {
        SceneObject.AddFreeCode(
            @"
function createOriginPathPoints() {
    const path = new Array(361);
    const n = path.length;
    const v = new BABYLON.Vector3(0, 0, 0);

    for (let i = 0; i < n; i++) {
        path[i] = v;
    }

    return path;
}

function createXyCirclePathPoints(arcRatio) {
    const path = new Array(361);
    const n = path.length;

    for (let i = 0; i < n; i++) {
        const a = Math.Tau * arcRatio * i / (n - 1);
        
        path[i] = new BABYLON.Vector3(Math.cos(a), Math.sin(a), 0);
    }

    return path;
}

function updateXyCirclePathPoints(path, radius, arcRatio) {
    const n = path.length;

    for (let i = 0; i < n; i++) {
        const a = Math.Tau * arcRatio * i / (n - 1);
        
        path[i].x = radius * Math.cos(a);
        path[i].y = radius * Math.sin(a);
    }
}".Trim()
        );

        PrototypeDisc = SceneObject.AddDisc(
            $"prototypeDisc",

            new GrBabylonJsDiscOptions
            {
                Radius = 1d,
                SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack,
                Tessellation = 512,
                //Updatable = true
            },

            new GrBabylonJsMeshProperties
            {
                IsVisible = false,
                Position = LinFloat64Vector3D.Zero
            }
        );

        //PrototypeSphere = SceneObject.AddIcoSphere(
        //    $"prototypeIcoSphere",

        //    new GrBabylonJsIcoSphere.IcoSphereOptions
        //    {
        //        Radius = 1d,
        //        Subdivisions = 9,
        //        Flat = false,
        //        SideOrientation = GrBabylonJsMeshOrientation.Front,
        //        //Updatable = true
        //    },

        //    new GrBabylonJsMesh.MeshProperties
        //    {
        //        IsVisible = false,
        //        //Material = visualElement.Style.Material.MaterialName,
        //        Position = Float64Tuple3D.Zero
        //    }
        //);

        PrototypeInnerSphere = SceneObject.AddSphere(
            $"prototypeInnerSphere",

            new GrBabylonJsSphereOptions
            {
                Diameter = 2d,
                Segments = 512,
                SideOrientation = GrBabylonJsMeshOrientation.Back,
                //Updatable = true
            },

            new GrBabylonJsMeshProperties
            {
                IsVisible = false,
                Position = LinFloat64Vector3D.Zero
            }
        );

        PrototypeOuterSphere = SceneObject.AddSphere(
            $"prototypeOuterSphere",

            new GrBabylonJsSphereOptions
            {
                Diameter = 2d,
                Segments = 512,
                SideOrientation = GrBabylonJsMeshOrientation.Front,
                //Updatable = true
            },

            new GrBabylonJsMeshProperties
            {
                IsVisible = false,
                Position = LinFloat64Vector3D.Zero
            }
        );

        PrototypeCone = SceneObject.AddCylinder(
            $"prototypeCone",

            new GrBabylonJsCylinderOptions
            {
                DiameterTop = 0,
                DiameterBottom = 2d,
                Height = 1d,
                Cap = GrBabylonJsMeshCap.Start,
                Subdivisions = 1,
                Tessellation = 512,
                SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack,
                //Updatable = true
            },

            new GrBabylonJsMeshProperties
            {
                IsVisible = false,
                Position = LinFloat64Vector3D.Zero
            }
        );
    }

    public override IGrVisualElementMaterial3D AddOrGetColorMaterial(Color color)
    {
        var key = color.ToPixel<Rgba32>().ToHex();

        if (_colorMaterialCache.TryGetValue(key, out var material))
            return material;

        material = color.ToPixel<Rgba32>().A == 255
            ? SceneObject.AddSimpleMaterial($"colorMaterial{key}", color)
            : SceneObject.AddStandardMaterial($"colorMaterial{key}", color);

        _colorMaterialCache.Add(key, material);

        return material;
    }

    public override void AddMaterial(IGrVisualElementMaterial3D material)
    {
        if (material is not GrBabylonJsMaterial babylonMaterial)
            throw new ArgumentException(nameof(material));

        if (SceneObject.ObjectList.Contains(babylonMaterial))
            return;

        SceneObject.ObjectList.Add(babylonMaterial);
    }


    public override GrVisualImage3D AddImage(GrVisualImage3D visualElement)
    {
        var latexImage =
            visualElement.Texture;

        var latexImageString =
            latexImage.GetImageDataUrlBase64();

        SceneObject.AddTexture(
            $"{visualElement.Name}Texture",

            latexImageString,

            new GrBabylonJsTextureProperties
            {
                HasAlpha = true,
                UScale = -1
            }
        );

        SceneObject.AddStandardMaterial(
            $"{visualElement.Name}Material",

            new GrBabylonJsStandardMaterialProperties
            {
                DiffuseTexture = $"{visualElement.Name}Texture",
                UseAlphaFromDiffuseTexture = true,
                BackFaceCulling = true,
                TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend
            }
        );

        SceneObject.AddPlane(
            $"{visualElement.Name}Plane",

            new GrBabylonJsPlaneOptions
            {
                Width = visualElement.ScalingFactor * latexImage.ImageWidth,
                Height = visualElement.ScalingFactor * latexImage.ImageHeight
            },

            new GrBabylonJsMeshProperties
            {
                BillboardMode = GrBabylonJsBillboardMode.All,
                Material = $"{visualElement.Name}Material",
                Position = visualElement.Position.GetBabylonJsCode(),
                Visibility = visualElement.Visibility
                //AlphaIndex = int.MaxValue
            }
        );

        if (visualElement.IsAnimated)
            AddLaTeXTextAnimation(visualElement);

        return visualElement;
    }

    private void AddLaTeXTextAnimation(GrVisualImage3D visualElement)
    {
        var samplingSpecs = new GrBabylonJsAnimationSpecs(visualElement)
        {
            LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
            EnableBlending = false,
            Loop = true
        };

        var keyPositions = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();

        foreach (var keyFrame in visualElement.GetKeyFrameRecords())
        {
            keyPositions.SetKeyFrameValue(
                keyFrame.FrameIndex,
                keyFrame.Position
            );
        }

        var positionAnimations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "PlanePosition",
                samplingSpecs,
                keyPositions
            );

        var animationGroup = SceneObject.AddAnimationGroup(
            visualElement.Name + "Animation",
            new GrBabylonJsAnimationGroupProperties()
            {
                LoopAnimation = true,
                IsAdditive = false,
                SpeedRatio = 1d
            }
        );

        animationGroup.AddAnimations(
            $"{visualElement.Name}Plane.position",
            positionAnimations
        );
    }


    public override GrVisualSquareGrid3D AddSquareGrid(GrVisualSquareGrid3D visualElement)
    {
        if (GridMaterialKind == GrBabylonJsGridMaterialKind.TexturedMaterial)
        {
            var gridImage =
                visualElement.Texture.GetImage();

            //gridImageString.Save(
            //    $"{visualElement.Name}.png"
            //);

            var gridImageString =
                gridImage.PngToHtmlDataUrlBase64();

            SceneObject.AddTexture(
                $"{visualElement.Name}Texture",

                gridImageString, //@"'./Textures/{visualElement.Name}.png'",

                new GrBabylonJsTextureProperties
                {
                    HasAlpha = true,
                    UScale = visualElement.UnitCount1,
                    VScale = visualElement.UnitCount2
                }
            );

            SceneObject.AddStandardMaterial(
                $"{visualElement.Name}Material",

                new GrBabylonJsStandardMaterialProperties
                {
                    BackFaceCulling = false,
                    SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack,
                    DiffuseTexture = $"{visualElement.Name}Texture",
                    UseAlphaFromDiffuseTexture = true,
                    TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend,
                    Alpha = visualElement.Opacity
                }
            );
        }
        else
        {
            //SceneObject.AddGridMaterial(
            //    $"{visualElement.Name}Material",

            //    new GrBabylonJsGridMaterialProperties
            //    {
            //        BackFaceCulling = false,
            //        SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack,
            //        LineColor = visualElement.BaseLineColor,
            //        MainColor = visualElement.BaseSquareColor,
            //        Opacity = visualElement.Opacity,
            //        //Alpha = visualElement.Opacity,
            //        //TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend,
            //        GridRatio = 1d,
            //        MajorUnitFrequency = 1,
            //        MinorUnitVisibility = 0.5,
            //        UseMaxLine = true
            //    }
            //);
        }

        var q = 
            LinFloat64Quaternion.ZxToXy.Concatenate(visualElement.Orientation);

        SceneObject.AddGround(
            $"{visualElement.Name}Ground",

            new GrBabylonJsGroundOptions
            {
                Width = visualElement.Size1,
                Height = visualElement.Size2
            },

            new GrBabylonJsMeshProperties
            {
                Material = $"{visualElement.Name}Material",
                Position = visualElement.Center,
                RotationQuaternion = q
            }
        );

        return visualElement;
    }

    public override void AddGrid(string name, ITriplet<Float64Scalar> center, LinFloat64Quaternion orientation, int unitCount, double unitSize = 1, double opacity = 1)
    {
        GridMaterialKind =
            GrBabylonJsGridMaterialKind.TexturedMaterial;

        AddSquareGrid(
            GrVisualSquareGrid3D.Default(
                name,
                center, 
                orientation, 
                unitCount, 
                unitSize, 
                opacity
            )
        );
    }

    public override void AddAxes(string name, ITriplet<Float64Scalar> origin, LinFloat64Quaternion orientation, double scalingFactor = 1, double opacity = 1)
    {
        AddElement(
            GrVisualFrame3D.CreateStatic(
                name,
                new GrVisualFrameStyle3D
                {
                    OriginStyle = AddOrGetColorMaterial(Color.DarkGray.WithAlpha(opacity)).CreateThickSurfaceStyle(0.075),
                    Direction1Style = AddOrGetColorMaterial(Color.DarkRed.WithAlpha(opacity)).CreateTubeCurveStyle(0.035),
                    Direction2Style = AddOrGetColorMaterial(Color.DarkGreen.WithAlpha(opacity)).CreateTubeCurveStyle(0.035),
                    Direction3Style = AddOrGetColorMaterial(Color.DarkBlue.WithAlpha(opacity)).CreateTubeCurveStyle(0.035)
                },
                origin.ToLinVector3D(),
                orientation,
                scalingFactor
            )
        );
    }

    public override IGrVisualImage3D AddImage(IGrVisualImage3D visualElement)
    {
        switch (visualElement)
        {
            case GrVisualImage3D latexTextImage:
                AddImage(latexTextImage);

                return visualElement;

            //case GrVisualSquareGridImageComposer3D xzSquareGridImage:
            //    AddSquareGrid(xzSquareGridImage);

            //    return visualElement;
        }

        var latexImage =
            visualElement.GetImage();

        //latexImage.Save(
        //    $"{visualElement.Name}.png"
        //);

        var latexImageString =
            latexImage.PngToHtmlDataUrlBase64();

        SceneObject.AddTexture(
            $"{visualElement.Name}Texture",

            latexImageString, //@"'./Textures/{visualElement.Name}.png'",

            new GrBabylonJsTextureProperties
            {
                HasAlpha = true
            }
        );

        return visualElement;
    }


    public override GrVisualPoint3D AddPoint(GrVisualPoint3D visualElement)
    {
        SceneObject.AddClone(
            $"{visualElement.Name}Sphere",
            PrototypeOuterSphere.ConstName,
            new GrBabylonJsMeshProperties
            {
                IsVisible = true,
                Material = visualElement.Style.Material.MaterialName,
                Visibility = visualElement.Visibility,
                Position = visualElement.Position.ToBabylonJsVector3Value(),
                Scaling = LinFloat64Vector3D.CreateEqualXyz(visualElement.Style.Thickness)
            }
        );

        if (visualElement.IsAnimated)
            AddPointAnimation(visualElement);

        return visualElement;
    }

    private void AddPointAnimation(GrVisualPoint3D visualElement)
    {
        var samplingSpecs = new GrBabylonJsAnimationSpecs(visualElement)
        {
            LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
            EnableBlending = false,
            Loop = true
        };

        var keyPositions = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();

        foreach (var keyFrame in visualElement.GetKeyFrameRecords())
        {
            keyPositions.SetKeyFrameValue(
                keyFrame.FrameIndex,
                keyFrame.Position
            );
        }

        var positionAnimations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "SpherePosition",
                samplingSpecs,
                keyPositions
            );

        var animationGroup = SceneObject.AddAnimationGroup(
            visualElement.Name + "Animation",
            new GrBabylonJsAnimationGroupProperties()
            {
                LoopAnimation = true,
                IsAdditive = false,
                SpeedRatio = 1d
            }
        );

        animationGroup.AddAnimations(
            $"{visualElement.Name}Sphere.position",
            positionAnimations
        );
    }


    public override GrVisualArrowHead3D AddArrowHead(GrVisualArrowHead3D visualElement)
    {
        var cylinderBaseDiameter = visualElement.Style.Thickness * 3;
        var cylinderHeight = cylinderBaseDiameter * 1.5d;

        var unitDirection = visualElement.Direction;
        var maxHeight = visualElement.MaxHeight;

        if (cylinderHeight > maxHeight)
        {
            cylinderHeight = maxHeight;
            cylinderBaseDiameter = cylinderHeight * 2d / 3d;
        }

        //var scalingFactor = 
        //    cylinderHeight <= maxHeight 
        //        ? 1d : (maxHeight / cylinderHeight);

        var quaternion =
            LinBasisVector3D.Py.VectorToVectorRotationQuaternion(unitDirection);

        SceneObject.AddClone(
            $"{visualElement.Name}Cylinder",
            PrototypeCone.ConstName,
            new GrBabylonJsMeshProperties
            {
                IsVisible = true,
                Material = visualElement.Style.Material.MaterialName,
                Visibility = visualElement.Visibility,
                Position = visualElement.Position - unitDirection * cylinderHeight / 2d,
                RotationQuaternion = quaternion,
                Scaling = LinFloat64Vector3D.Create(
                    cylinderBaseDiameter / 2d,
                    cylinderHeight,
                    cylinderBaseDiameter / 2d
                )
            }
        );

        if (visualElement.IsAnimated)
            AddArrowHeadAnimation(visualElement);

        return visualElement;
    }

    private void AddArrowHeadAnimation(GrVisualArrowHead3D visualElement)
    {
        var samplingSpecs = new GrBabylonJsAnimationSpecs(visualElement)
        {
            LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
            EnableBlending = false,
            Loop = true
        };

        var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
        var keyPositions = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var keyScalings = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var keyQuaternions = new GrBabylonJsKeyFrameDictionary<LinFloat64Quaternion>();

        var thickness = visualElement.Style.Thickness;

        foreach (var (frameIndex, _, visibility, origin, unitDirection, maxHeight) in visualElement.GetKeyFrameRecords())
        {
            var cylinderBaseDiameter = thickness * 3;
            var cylinderHeight = cylinderBaseDiameter * 1.5d;

            if (cylinderHeight > maxHeight)
            {
                cylinderHeight = maxHeight;
                cylinderBaseDiameter = cylinderHeight * 2d / 3d;
            }

            var quaternion =
                LinBasisVector3D.Py.VectorToVectorRotationQuaternion(unitDirection);

            var position =
                origin - unitDirection * cylinderHeight / 2d;

            //var scalingFactor = 
            //    cylinderHeight <= maxHeight 
            //        ? 1d : maxHeight / cylinderHeight;

            var scaling = LinFloat64Vector3D.Create(cylinderBaseDiameter / 2d,
                cylinderHeight,
                cylinderBaseDiameter / 2d);

            keyVisibility.SetKeyFrameValue(frameIndex, visibility);
            keyPositions.SetKeyFrameValue(frameIndex, position);
            keyScalings.SetKeyFrameValue(frameIndex, scaling);
            keyQuaternions.SetKeyFrameValue(frameIndex, quaternion);
        }

        var visibilityAnimation =
            SceneObject.AddFloat32Animation(
                visualElement.Name + "CylinderVisibility",
                "visibility",
                samplingSpecs,
                keyVisibility
            );

        var positionAnimations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "CylinderPosition",
                samplingSpecs,
                keyPositions
            );

        var scalingAnimations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "CylinderScaling",
                samplingSpecs,
                keyScalings
            );

        var quaternionAnimation =
            SceneObject.AddQuaternionAnimation(
                visualElement.Name + "Quaternion",
                "rotationQuaternion",
                samplingSpecs,
                keyQuaternions.OptimizeQuaternionKeyFrames()
            );

        SceneObject
            .AddAnimationGroup(
                visualElement.Name + "Animation",
                new GrBabylonJsAnimationGroupProperties()
                {
                    LoopAnimation = true,
                    IsAdditive = false,
                    SpeedRatio = 1d
                }
            ).AddAnimation(
                $"{visualElement.Name}Cylinder",
                visibilityAnimation
            ).AddAnimations(
                $"{visualElement.Name}Cylinder.position",
                positionAnimations
            ).AddAnimations(
                $"{visualElement.Name}Cylinder.scaling",
                scalingAnimations
            ).AddAnimations(
                $"{visualElement.Name}Cylinder",
                quaternionAnimation
            );
    }


    public override GrVisualParametricCurve3D AddParametricCurve(GrVisualParametricCurve3D visualElement)
    {
        //if (visualElement.TextImage is not null)
        //    AddImage(visualElement.TextImage);

        if (visualElement.ShowFrames)
        {
            var lineArrayList = new List<LinFloat64Vector3D[]>(visualElement.ParameterValues.Count);
            var colorArrayList = new List<Color[]>(visualElement.ParameterValues.Count);

            var xColor1 = Color.Red.SetAlpha(0.9f); //Color.Red.SetAlpha(128);
            var xColor2 = Color.Red.SetAlpha(0.1f);
            var yColor1 = Color.Green.SetAlpha(0.9f);
            var yColor2 = Color.Green.SetAlpha(0.1f);
            var zColor1 = Color.Blue.SetAlpha(0.9f);
            var zColor2 = Color.Blue.SetAlpha(0.1f);

            var length = visualElement.FrameSize;
            foreach (var t in visualElement.FrameParameterValues)
            {
                var frame = visualElement.Curve.GetFrame(t);

                var origin = frame.Point;
                var xPoint = origin + length * frame.Tangent.ToUnitLinVector3D();
                var yPoint = origin + length * frame.Normal1.ToUnitLinVector3D();
                var zPoint = origin + length * frame.Normal2.ToUnitLinVector3D();

                lineArrayList.Add(new[] { origin, xPoint });
                lineArrayList.Add(new[] { origin, yPoint });
                lineArrayList.Add(new[] { origin, zPoint });

                colorArrayList.Add(new[] { xColor1, xColor2 });
                colorArrayList.Add(new[] { yColor1, yColor2 });
                colorArrayList.Add(new[] { zColor1, zColor2 });
            }

            SceneObject.AddLineSystem(
                $"{visualElement.Name}FrameLines",
                new GrBabylonJsLinesSystemOptions
                {
                    Lines = lineArrayList.ToArray(),
                    Colors = colorArrayList.ToArray()
                }
            );
        }

        var pointListCode =
            visualElement
                .ParameterValues
                .Select(visualElement.Curve.GetValue)
                .GetBabylonJsCode();

        SceneObject.AddFreeCode(
            $"const {visualElement.Name}Path = {pointListCode};"
        );

        if (visualElement.Style is GrVisualCurveTubeStyle3D tubeStyle)
        {
            SceneObject.AddTube(
                $"{visualElement.Name}Tube",

                new GrBabylonJsTubeOptions
                {
                    Radius = tubeStyle.Thickness / 2d,
                    Path = $"{visualElement.Name}Path",
                    Tessellation = 32
                },

                new GrBabylonJsMeshProperties
                {
                    Material = tubeStyle.Material.MaterialName
                }
            );

            return visualElement;
        }

        if (visualElement.Style is GrVisualCurveSolidLineStyle3D solidLineStyle)
        {
            SceneObject.AddLines(
                $"{visualElement.Name}Line",

                new GrBabylonJsLinesOptions
                {
                    Points = $"{visualElement.Name}Path"
                },

                new GrBabylonJsLinesMeshProperties
                {
                    Color = solidLineStyle.Color
                }
            );

            return visualElement;
        }

        throw new ArgumentOutOfRangeException();
    }

    public override GrVisualRightAngle3D AddRightAngle(GrVisualRightAngle3D visualElement)
    {
        if (visualElement.InnerStyle is not null)
        {
            var quaternion = 
                LinBasisVectorPair3D
                    .PxPy
                    .VectorPairToVectorPairRotationQuaternion(
                        visualElement.Direction1,
                        visualElement.Direction2
                    );

            SceneObject.AddPlane(
                $"{visualElement.Name}Plane",

                new GrBabylonJsPlaneOptions
                {
                    Width = visualElement.Width,
                    Height = visualElement.Height,
                    SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack
                },

                new GrBabylonJsMeshProperties
                {
                    Material = visualElement.InnerStyle.Material.MaterialName,
                    Visibility = visualElement.Visibility,
                    Position = visualElement.Origin + (visualElement.Direction1 + visualElement.Direction2) * visualElement.Radius / 8d.Sqrt(),
                    RotationQuaternion = quaternion
                }
            );
        }

        var pathCode =
            visualElement
                .GetArcPointsTriplet()
                .GetBabylonJsCode();

        if (visualElement.Style is GrVisualCurveTubeStyle3D tubeStyle)
        {
            SceneObject.AddTube(
                $"{visualElement.Name}Tube",

                new GrBabylonJsTubeOptions
                {
                    Path = pathCode,
                    Radius = tubeStyle.Thickness / 2d,
                    Tessellation = 32,
                    Cap = GrBabylonJsMeshCap.StartAndEnd
                },

                new GrBabylonJsMeshProperties
                {
                    Material = tubeStyle.Material.MaterialName,
                    Visibility = visualElement.Visibility,
                }
            );

            return visualElement;
        }

        if (visualElement.Style is GrVisualCurveSolidLineStyle3D solidLineStyle)
        {
            SceneObject.AddLines(
                $"{visualElement.Name}Line",

                new GrBabylonJsLinesOptions
                {
                    Points = pathCode
                },

                new GrBabylonJsLinesMeshProperties
                {
                    Color = solidLineStyle.Color,
                    Visibility = visualElement.Visibility,
                }
            );

            return visualElement;
        }

        if (visualElement.Style is GrVisualCurveDashedLineStyle3D dashedLineStyle)
        {
            SceneObject.AddLines(
                $"{visualElement.Name}Line",

                new GrBabylonJsLinesOptions
                {
                    Points = pathCode
                },

                new GrBabylonJsLinesMeshProperties
                {
                    Color = dashedLineStyle.Color,
                    Visibility = visualElement.Visibility,
                }
            );

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
        if (visualElement.IsAnimated)
        {
            SceneObject.AddFreeCode(
                $"const {visualElement.Name}Points = createXyCirclePathPoints(1);"
            );

            SceneObject.AddTube(
                $"{visualElement.Name}Tube",

                new GrBabylonJsTubeOptions
                {
                    Radius = tubeStyle.Thickness / 2d,
                    Path = $"{visualElement.Name}Points",
                    Tessellation = 32,
                    Cap = GrBabylonJsMeshCap.None,
                    Updatable = visualElement.IsAnimated
                },

                new GrBabylonJsMeshProperties
                {
                    Material = tubeStyle.Material.MaterialName,
                    Visibility = visualElement.Visibility,
                }
            );

            SceneObject.AddFreeCode($"{visualElement.Name}Tube.circleRadius = 1;");

            AddCircleCurveAnimation(visualElement, tubeStyle);
        }
        else
        {
            var quaternion =
                LinBasisVector3D
                    .Py
                    .VectorToVectorRotationQuaternion(
                        visualElement.Normal.ToUnitLinVector3D()
                    );

            SceneObject.AddTorus(
                $"{visualElement.Name}Torus",

                new GrBabylonJsTorusOptions
                {
                    Diameter = visualElement.Radius * 2,
                    Thickness = tubeStyle.Thickness,
                    Tessellation = 360
                },

                new GrBabylonJsMeshProperties
                {
                    IsVisible = true,
                    Material = tubeStyle.Material.MaterialName,
                    Visibility = visualElement.Visibility,
                    Position = visualElement.Center.ToBabylonJsVector3Value(),
                    RotationQuaternion = quaternion
                }
            );
        }

        return visualElement;
    }

    private GrVisualCircleCurve3D AddCircleCurve(GrVisualCircleCurve3D visualElement, GrVisualCurveSolidLineStyle3D solidLineStyle)
    {
        var (point1, point2, point3) =
            visualElement.IsAnimated
                ? EuclideanGeometryUtils.GetUnitXyCirclePointsTriplet3D()
                : visualElement.Center.GetCirclePointsTriplet3D(
                    visualElement.Normal,
                    visualElement.Radius
                );

        var pathName = $"{visualElement.Name}Path";

        SceneObject.AddArcThru3Points(
            pathName,
            point1,
            point2,
            point3,
            visualElement.PathPointCount,
            false,
            true
        );

        SceneObject.AddLines(
            $"{visualElement.Name}Line",

            new GrBabylonJsLinesOptions
            {
                Points = $"{pathName}.getPoints()"
            },

            new GrBabylonJsLinesMeshProperties
            {
                Color = solidLineStyle.Color,
                Visibility = visualElement.Visibility
            }
        );

        if (visualElement.IsAnimated)
            AddCircleCurveAnimation(visualElement, solidLineStyle);

        return visualElement;
    }

    private GrVisualCircleCurve3D AddCircleCurve(GrVisualCircleCurve3D visualElement, GrVisualCurveDashedLineStyle3D dashedLineStyle)
    {
        var (point1, point2, point3) =
            visualElement.IsAnimated
                ? EuclideanGeometryUtils.GetUnitXyCirclePointsTriplet3D()
                : visualElement.Center.GetCirclePointsTriplet3D(
                    visualElement.Normal,
                    visualElement.Radius
                );

        var pathName = $"{visualElement.Name}Path";

        SceneObject.AddArcThru3Points(
            pathName,
            point1,
            point2,
            point3,
            visualElement.PathPointCount,
            false,
            true
        );

        SceneObject.AddDashedLines(
            $"{visualElement.Name}DashedLines",

            new GrBabylonJsDashedLinesOptions
            {
                Points = $"{pathName}.getPoints()",
                DashNumber = 10 * dashedLineStyle.DashPerLine,
                DashSize = dashedLineStyle.DashOn,
                GapSize = dashedLineStyle.DashOff,
                Updatable = visualElement.IsAnimated
            },

            new GrBabylonJsLinesMeshProperties
            {
                Color = dashedLineStyle.Color,
                Visibility = visualElement.Visibility,
            }
        );

        if (visualElement.IsAnimated)
            AddCircleCurveAnimation(visualElement, dashedLineStyle);

        return visualElement;
    }

    private void AddCircleCurveAnimation(GrVisualCircleCurve3D visualElement, GrVisualCurveTubeStyle3D tubeStyle)
    {
        var samplingSpecs = new GrBabylonJsAnimationSpecs(visualElement)
        {
            LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
            EnableBlending = false,
            Loop = true
        };

        var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
        var keyRadius = new GrBabylonJsKeyFrameDictionary<double>();
        var keyPositions = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var keyQuaternions = new GrBabylonJsKeyFrameDictionary<LinFloat64Quaternion>();

        foreach (var (frameIndex, _, visibility, center, normal, radius) in visualElement.GetKeyFrameRecords())
        {
            var quaternion = LinBasisVector3D.Pz.VectorToVectorRotationQuaternion(normal.ToUnitLinVector3D());

            keyVisibility.SetKeyFrameValue(frameIndex, visibility);
            keyRadius.SetKeyFrameValue(frameIndex, radius);
            keyPositions.SetKeyFrameValue(frameIndex, center);
            keyQuaternions.SetKeyFrameValue(frameIndex, quaternion);
        }

        var visibilityAnimation =
            SceneObject.AddFloat32Animation(
                visualElement.Name + "Visibility",
                "visibility",
                samplingSpecs,
                keyVisibility
            );

        var radiusAnimation =
            SceneObject.AddFloat32Animation(
                visualElement.Name + "CircleRadius",
                "circleRadius",
                samplingSpecs,
                keyRadius
            );

        var positionAnimations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Position",
                samplingSpecs,
                keyPositions
            );

        var quaternionAnimation =
            SceneObject.AddQuaternionAnimation(
                visualElement.Name + "Quaternion",
                "rotationQuaternion",
                samplingSpecs,
                keyQuaternions.OptimizeQuaternionKeyFrames()
            );

        SceneObject
            .AddAnimationGroup(
                visualElement.Name + "Animations",
                new GrBabylonJsAnimationGroupProperties()
                {
                    LoopAnimation = true,
                    IsAdditive = false,
                    SpeedRatio = 1d
                }
            ).AddAnimation(
                $"{visualElement.Name}Tube",
                visibilityAnimation
            ).AddAnimation(
                $"{visualElement.Name}Tube",
                radiusAnimation
            ).AddAnimations(
                $"{visualElement.Name}Tube.position",
                positionAnimations
            ).AddAnimation(
                $"{visualElement.Name}Tube",
                quaternionAnimation
            );

        SceneObject.BeforeSceneRenderCode.Add(
            $@"
updateXyCirclePathPoints({visualElement.Name}Points, {visualElement.Name}Tube.circleRadius, 1);

{visualElement.Name}Tube = BABYLON.MeshBuilder.CreateTube(null, {{path: {visualElement.Name}Points, instance: {visualElement.Name}Tube}});
".Trim()
        );
    }

    private void AddCircleCurveAnimation(GrVisualCircleCurve3D visualElement, GrVisualCurveSolidLineStyle3D solidLineStyle)
    {
        var samplingSpecs = new GrBabylonJsAnimationSpecs(visualElement)
        {
            LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
            EnableBlending = false,
            Loop = true
        };

        var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
        var keyPositions = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var keyScalings = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var keyQuaternions = new GrBabylonJsKeyFrameDictionary<LinFloat64Quaternion>();

        foreach (var (frameIndex, _, visibility, center, normal, radius) in visualElement.GetKeyFrameRecords())
        {
            var quaternion = LinBasisVector3D.Pz.VectorToVectorRotationQuaternion(normal.ToUnitLinVector3D());
            var scaling = LinFloat64Vector3D.Create(
                radius,
                radius,
                1
            );

            keyVisibility.SetKeyFrameValue(frameIndex, visibility);
            keyPositions.SetKeyFrameValue(frameIndex, center);
            keyScalings.SetKeyFrameValue(frameIndex, scaling);
            keyQuaternions.SetKeyFrameValue(frameIndex, quaternion);
        }

        var visibilityAnimation =
            SceneObject.AddFloat32Animation(
                visualElement.Name + "Visibility",
                "visibility",
                samplingSpecs,
                keyVisibility
            );

        var positionAnimations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Position",
                samplingSpecs,
                keyPositions
            );

        var scalingAnimations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Scaling",
                samplingSpecs,
                keyScalings
            );

        var quaternionAnimation =
            SceneObject.AddQuaternionAnimation(
                visualElement.Name + "Quaternion",
                "rotationQuaternion",
                samplingSpecs,
                keyQuaternions.OptimizeQuaternionKeyFrames()
            );

        SceneObject
            .AddAnimationGroup(
                visualElement.Name + "Animations",
                new GrBabylonJsAnimationGroupProperties()
                {
                    LoopAnimation = true,
                    IsAdditive = false,
                    SpeedRatio = 1d
                }
            ).AddAnimation(
                $"{visualElement.Name}Line",
                visibilityAnimation
            ).AddAnimations(
                $"{visualElement.Name}Line.position",
                positionAnimations
            ).AddAnimations(
                $"{visualElement.Name}Line.scaling",
                scalingAnimations
            ).AddAnimation(
                $"{visualElement.Name}Line",
                quaternionAnimation
            );
    }

    private void AddCircleCurveAnimation(GrVisualCircleCurve3D visualElement, GrVisualCurveDashedLineStyle3D dashedLineStyle)
    {
        var samplingSpecs = new GrBabylonJsAnimationSpecs(visualElement)
        {
            LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
            EnableBlending = false,
            Loop = true
        };

        var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
        var keyPositions = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var keyScalings = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var keyQuaternions = new GrBabylonJsKeyFrameDictionary<LinFloat64Quaternion>();

        foreach (var (frameIndex, _, visibility, center, normal, radius) in visualElement.GetKeyFrameRecords())
        {
            var quaternion = LinBasisVector3D.Pz.VectorToVectorRotationQuaternion(normal.ToUnitLinVector3D());
            var scaling = LinFloat64Vector3D.Create(
                radius,
                radius,
                1
            );

            keyVisibility.SetKeyFrameValue(frameIndex, visibility);
            keyPositions.SetKeyFrameValue(frameIndex, center);
            keyScalings.SetKeyFrameValue(frameIndex, scaling);
            keyQuaternions.SetKeyFrameValue(frameIndex, quaternion);
        }

        var visibilityAnimation =
            SceneObject.AddFloat32Animation(
                visualElement.Name + "Visibility",
                "visibility",
                samplingSpecs,
                keyVisibility
            );

        var positionAnimations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Position",
                samplingSpecs,
                keyPositions
            );

        var scalingAnimations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Scaling",
                samplingSpecs,
                keyScalings
            );

        var quaternionAnimation =
            SceneObject.AddQuaternionAnimation(
                visualElement.Name + "Quaternion",
                "rotationQuaternion",
                samplingSpecs,
                keyQuaternions.OptimizeQuaternionKeyFrames()
            );

        SceneObject
            .AddAnimationGroup(
                visualElement.Name + "Animations",
                new GrBabylonJsAnimationGroupProperties()
                {
                    LoopAnimation = true,
                    IsAdditive = false,
                    SpeedRatio = 1d
                }
            ).AddAnimation(
                $"{visualElement.Name}DashedLines",
                visibilityAnimation
            ).AddAnimations(
                $"{visualElement.Name}DashedLines.position",
                positionAnimations
            ).AddAnimations(
                $"{visualElement.Name}DashedLines.scaling",
                scalingAnimations
            ).AddAnimation(
                $"{visualElement.Name}DashedLines",
                quaternionAnimation
            );
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

        SceneObject.AddFreeCode(
            $"const {visualElement.Name}Points = {pointPath.GetBabylonJsCode()};"
        );

        SceneObject.AddTube(
            $"{visualElement.Name}Tube",

            new GrBabylonJsTubeOptions
            {
                Radius = tubeStyle.Thickness / 2d,
                Path = $"{visualElement.Name}Points",
                Tessellation = 32,
                Cap = GrBabylonJsMeshCap.StartAndEnd,
                Updatable = visualElement.IsAnimated
            },

            new GrBabylonJsMeshProperties
            {
                Material = tubeStyle.Material.MaterialName,
                Visibility = visualElement.Visibility,
            }
        );

        if (visualElement.IsAnimated)
            AddCurveAnimation(visualElement, tubeStyle);
    }

    private void AddCurve(GrVisualCurveWithAnimation3D visualElement, GrVisualCurveSolidLineStyle3D solidLineStyle)
    {
        var pointPath = visualElement.GetPositionsPath();

        SceneObject.AddFreeCode(
            $"const {visualElement.Name}Points = {pointPath.GetBabylonJsCode()};"
        );

        SceneObject.AddLines(
            $"{visualElement.Name}Lines",

            new GrBabylonJsLinesOptions
            {
                Points = $"{visualElement.Name}Points",
                Updatable = visualElement.IsAnimated
            },

            new GrBabylonJsLinesMeshProperties
            {
                Color = solidLineStyle.Color,
                Visibility = visualElement.Visibility

            }
        );

        if (visualElement.IsAnimated)
            AddCurveAnimation(visualElement, solidLineStyle);
    }

    private void AddCurve(GrVisualCurveWithAnimation3D visualElement, GrVisualCurveDashedLineStyle3D dashedLineStyle)
    {
        var pointPath = visualElement.GetPositionsPath();

        SceneObject.AddFreeCode(
            $"const {visualElement.Name}Points = {pointPath.GetBabylonJsCode()};"
        );

        SceneObject.AddDashedLines(
            $"{visualElement.Name}DashedLines",

            new GrBabylonJsDashedLinesOptions
            {
                Points = $"{visualElement.Name}Points",
                DashNumber = pointPath.Count * dashedLineStyle.DashPerLine,
                DashSize = dashedLineStyle.DashOn,
                GapSize = dashedLineStyle.DashOff,
                Updatable = visualElement.IsAnimated
            },

            new GrBabylonJsLinesMeshProperties
            {
                Color = dashedLineStyle.Color,
                Visibility = visualElement.Visibility,
            }
        );

        if (visualElement.IsAnimated)
            AddCurveAnimation(visualElement, dashedLineStyle);
    }

    private void AddCurveAnimation(GrVisualCurveWithAnimation3D visualElement, GrVisualCurveTubeStyle3D tubeStyle)
    {
        if (!visualElement.IsAnimated)
            throw new InvalidOperationException();

        var samplingSpecs = new GrBabylonJsAnimationSpecs(visualElement)
        {
            LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
            EnableBlending = false,
            Loop = true
        };

        var pointCount = visualElement.PathPointCount;

        var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
        var keyPositionsList =
            pointCount.GetRange(
                _ => new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>()
            ).ToArray();

        var positionAnimationsList =
            new Triplet<GrBabylonJsAnimationOfFloat>[pointCount];

        foreach (var (frameIndex, _, visibility, pointsPath) in visualElement.GetKeyPointsPathRecords())
        {
            keyVisibility.SetKeyFrameValue(frameIndex, visibility);

            for (var i = 0; i < pointCount; i++)
                keyPositionsList[i].SetKeyFrameValue(
                    frameIndex,
                    pointsPath[i].ToLinVector3D()
                );
        }

        var visibilityAnimation =
            SceneObject.AddFloat32Animation(
                $"{visualElement.Name}Visibility",
                "visibility",
                samplingSpecs,
                keyVisibility
            );

        for (var i = 0; i < pointCount; i++)
            positionAnimationsList[i] =
                SceneObject.AddVector3ComponentAnimations(
                    $"{visualElement.Name}Points{i}",
                    samplingSpecs,
                    keyPositionsList[i]
                );

        var animationGroup = SceneObject.AddAnimationGroup(
            visualElement.Name + "Animation",
            new GrBabylonJsAnimationGroupProperties()
            {
                LoopAnimation = true,
                IsAdditive = false,
                SpeedRatio = 1d
            }
        );

        animationGroup.AddAnimation(
            $"{visualElement.Name}Tube",
            visibilityAnimation
        );

        for (var i = 0; i < pointCount; i++)
            animationGroup.AddAnimations(
                $"{visualElement.Name}Points[{i}]",
                positionAnimationsList[i]
            );

        SceneObject.BeforeSceneRenderCode.Add(
            $"{visualElement.Name}Tube = BABYLON.MeshBuilder.CreateTube(null, {{path: {visualElement.Name}Points, instance: {visualElement.Name}Tube}});"
        );
    }

    private void AddCurveAnimation(GrVisualCurveWithAnimation3D visualElement, GrVisualCurveSolidLineStyle3D solidLineStyle)
    {
        if (!visualElement.IsAnimated)
            throw new InvalidOperationException();

        var samplingSpecs = new GrBabylonJsAnimationSpecs(visualElement)
        {
            LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
            EnableBlending = false,
            Loop = true
        };

        var pointCount = visualElement.PathPointCount;

        var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
        var keyPositionsList =
            pointCount.GetRange(
                _ => new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>()
            ).ToArray();

        var positionAnimationsList =
            new Triplet<GrBabylonJsAnimationOfFloat>[pointCount];

        foreach (var (frameIndex, _, visibility, pointsPath) in visualElement.GetKeyPointsPathRecords())
        {
            keyVisibility.SetKeyFrameValue(frameIndex, visibility);

            for (var i = 0; i < pointCount; i++)
                keyPositionsList[i].SetKeyFrameValue(
                    frameIndex,
                    pointsPath[i].ToLinVector3D()
                );
        }

        var visibilityAnimation =
            SceneObject.AddFloat32Animation(
                $"{visualElement.Name}Visibility",
                "visibility",
                samplingSpecs,
                keyVisibility
            );

        for (var i = 0; i < pointCount; i++)
            positionAnimationsList[i] =
                SceneObject.AddVector3ComponentAnimations(
                    $"{visualElement.Name}Points{i}",
                    samplingSpecs,
                    keyPositionsList[i]
                );

        var animationGroup = SceneObject.AddAnimationGroup(
            visualElement.Name + "Animation",
            new GrBabylonJsAnimationGroupProperties()
            {
                LoopAnimation = true,
                IsAdditive = false,
                SpeedRatio = 1d
            }
        );

        animationGroup.AddAnimation(
            $"{visualElement.Name}Lines",
            visibilityAnimation
        );

        for (var i = 0; i < pointCount; i++)
            animationGroup.AddAnimations(
                $"{visualElement.Name}Points[{i}]",
                positionAnimationsList[i]
            );

        SceneObject.BeforeSceneRenderCode.Add(
            $"{visualElement.Name}Lines = BABYLON.MeshBuilder.CreateLines(null, {{points: {visualElement.Name}Points, instance: {visualElement.Name}Lines}});"
        );
    }

    private void AddCurveAnimation(GrVisualCurveWithAnimation3D visualElement, GrVisualCurveDashedLineStyle3D dashedLineStyle)
    {
        if (!visualElement.IsAnimated)
            throw new InvalidOperationException();

        var samplingSpecs = new GrBabylonJsAnimationSpecs(visualElement)
        {
            LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
            EnableBlending = false,
            Loop = true
        };

        var pointCount = visualElement.PathPointCount;

        var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
        var keyPositionsList =
            pointCount.GetRange(
                _ => new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>()
            ).ToArray();

        var positionAnimationsList =
            new Triplet<GrBabylonJsAnimationOfFloat>[pointCount];

        foreach (var (frameIndex, _, visibility, pointsPath) in visualElement.GetKeyPointsPathRecords())
        {
            keyVisibility.SetKeyFrameValue(frameIndex, visibility);

            for (var i = 0; i < pointCount; i++)
                keyPositionsList[i].SetKeyFrameValue(
                    frameIndex,
                    pointsPath[i].ToLinVector3D()
                );
        }

        var visibilityAnimation =
            SceneObject.AddFloat32Animation(
                $"{visualElement.Name}Visibility",
                "visibility",
                samplingSpecs,
                keyVisibility
            );

        for (var i = 0; i < pointCount; i++)
            positionAnimationsList[i] =
                SceneObject.AddVector3ComponentAnimations(
                    $"{visualElement.Name}Points{i}",
                    samplingSpecs,
                    keyPositionsList[i]
                );

        var animationGroup = SceneObject.AddAnimationGroup(
            visualElement.Name + "Animation",
            new GrBabylonJsAnimationGroupProperties()
            {
                LoopAnimation = true,
                IsAdditive = false,
                SpeedRatio = 1d
            }
        );

        animationGroup.AddAnimation(
            $"{visualElement.Name}DashedLines",
            visibilityAnimation
        );

        for (var i = 0; i < pointCount; i++)
            animationGroup.AddAnimations(
                $"{visualElement.Name}Points[{i}]",
                positionAnimationsList[i]
            );

        SceneObject.BeforeSceneRenderCode.Add(
            $"{visualElement.Name}DashedLines = BABYLON.MeshBuilder.CreateDashedLines(null, {{points: {visualElement.Name}Points, instance: {visualElement.Name}DashedLines}});"
        );
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

        //    new GrBabylonJsExtrudeShape.ExtrudeShapeOptions
        //    {
        //        AdjustFrame = true,
        //        Cap = GrBabylonJsMeshCap.StartAndEnd,
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

        //    new GrBabylonJsMesh.MeshProperties
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
        var path0Point0 = visualElement.Position1.GetBabylonJsCode();
        var path0Point1 = visualElement.Position2.GetBabylonJsCode();

        var path1Point0 = visualElement.Position1.GetBabylonJsCode();
        var path1Point1 = visualElement.Position3.GetBabylonJsCode();

        SceneObject.AddFreeCode(
            $"const {visualElement.Name}RibbonPathArray = [[{path0Point0}, {path0Point1}], [{path1Point0}, {path1Point1}]];"
        );

        SceneObject.AddRibbon(
            $"{visualElement.Name}Ribbon",

            new GrBabylonJsRibbonOptions
            {
                CloseArray = false,
                ClosePath = false,
                PathArray = $"{visualElement.Name}RibbonPathArray",
                SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack,
                Updatable = visualElement.IsAnimated
            },

            new GrBabylonJsMeshProperties
            {
                Material = visualElement.Style.Material.MaterialName,
                Visibility = visualElement.Visibility
            }
        );

        if (visualElement.IsAnimated)
            AddTriangleAnimation(visualElement, thinStyle);

        return visualElement;
    }

    private void AddTriangleAnimation(GrVisualTriangleSurface3D visualElement, GrVisualSurfaceThickStyle3D thickStyle)
    {
        //if (visualElement.IsStatic || 
        //    visualElement.OriginPath is null ||
        //    visualElement.Direction1Path is null ||
        //    visualElement.Direction2Path is null
        //   )
        //    throw new InvalidOperationException();

        //var samplingSpecs = new GrBabylonJsAnimationSpecs(visualElement)
        //{
        //    LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
        //    EnableBlending = false,
        //    Loop = true
        //};

        //var frameRate = samplingSpecs.FrameRate;
        //var pointCount = visualElement.PointCount;

        //var positionAnimationsList = 
        //    new Triplet<GrBabylonJsAnimationOfFloat>[pointCount];

        //for (var i = 0; i < pointCount; i++)
        //{
        //    var frameIndexPositionPairs =
        //        visualElement
        //            .AnimatedPositionPath[i]
        //            .GetKeyFrameIndexPositionPairs(frameRate);

        //    var keyPositions = new GrBabylonJsKeyFrameDictionary<Float64Tuple3D>();

        //    foreach (var (frameIndex, position) in frameIndexPositionPairs)
        //        keyPositions.SetKeyFrameValue(frameIndex, position);

        //    positionAnimationsList[i] =
        //        SceneObject.AddVector3ComponentAnimations(
        //            visualElement.Name + $"{i}Position",
        //            samplingSpecs,
        //            keyPositions
        //        );
        //}

        //var animationGroup = SceneObject.AddAnimationGroup(
        //    visualElement.Name + "Animation",
        //    new GrBabylonJsAnimationGroup.AnimationGroupProperties()
        //    {
        //        LoopAnimation = true,
        //        IsAdditive = false,
        //        SpeedRatio = 1d
        //    }
        //);

        //for (var i = 0; i < pointCount; i++)
        //{
        //    animationGroup.AddAnimations(
        //        $"{visualElement.Name}Path[{i}]",
        //        positionAnimationsList[i]
        //    );
        //}

        //SceneObject.BeforeSceneRenderCode.Add(
        //    $"{visualElement.Name}DashedLines = BABYLON.MeshBuilder.CreateDashedLines(null, {{points: {visualElement.Name}DashedLinesPoints, instance: {visualElement.Name}DashedLines}});"
        //);
    }

    private void AddTriangleAnimation(GrVisualTriangleSurface3D visualElement, GrVisualSurfaceThinStyle3D thinStyle)
    {
        var samplingSpecs = new GrBabylonJsAnimationSpecs(visualElement)
        {
            LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
            EnableBlending = false,
            Loop = true
        };

        var keyFrameRecords =
            visualElement.GetKeyFrameRecords();

        var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();

        var keyPositions1 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var keyPositions2 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var keyPositions3 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();

        foreach (var (frameIndex, _, visibility, position1, position2, position3) in keyFrameRecords)
        {
            keyVisibility.SetKeyFrameValue(frameIndex, visibility);

            keyPositions1.Add(frameIndex, position1);
            keyPositions2.Add(frameIndex, position2);
            keyPositions3.Add(frameIndex, position3);
        }

        var visibilityAnimation =
            SceneObject.AddFloat32Animation(
                visualElement.Name + "Visibility",
                "visibility",
                samplingSpecs,
                keyVisibility
            );

        var position1Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Position1",
                samplingSpecs,
                keyPositions1
            );

        var position2Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Position2",
                samplingSpecs,
                keyPositions2
            );

        var position3Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Position3",
                samplingSpecs,
                keyPositions3
            );

        SceneObject.AddAnimationGroup(
            visualElement.Name + "Animation",
            new GrBabylonJsAnimationGroupProperties()
            {
                LoopAnimation = true,
                IsAdditive = false,
                SpeedRatio = 1d
            }
        ).AddAnimation(
            $"{visualElement.Name}Ribbon",
            visibilityAnimation
        ).AddAnimations(
            $"{visualElement.Name}RibbonPathArray[0][0]",
            position1Animations
        ).AddAnimations(
            $"{visualElement.Name}RibbonPathArray[0][1]",
            position2Animations
        ).AddAnimations(
            $"{visualElement.Name}RibbonPathArray[1][0]",
            position1Animations
        ).AddAnimations(
            $"{visualElement.Name}RibbonPathArray[1][1]",
            position3Animations
        );

        SceneObject.BeforeSceneRenderCode.Add(
            $"{visualElement.Name}Ribbon = BABYLON.MeshBuilder.CreateRibbon(null, {{pathArray: {visualElement.Name}RibbonPathArray, instance: {visualElement.Name}Ribbon}});"
        );
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
        var path0Point0 = visualElement.Position.GetBabylonJsCode();
        var path0Point1 = visualElement.Position.VectorAdd(visualElement.Direction1).GetBabylonJsCode();

        var path1Point0 = visualElement.Position.VectorAdd(visualElement.Direction2).GetBabylonJsCode();
        var path1Point1 = visualElement.Position.VectorAdd(visualElement.Direction2, visualElement.Direction1).GetBabylonJsCode();

        SceneObject.AddFreeCode(
            $"const {visualElement.Name}RibbonPathArray = [[{path0Point0}, {path0Point1}], [{path1Point0}, {path1Point1}]];"
        );

        SceneObject.AddRibbon(
            $"{visualElement.Name}Ribbon",

            new GrBabylonJsRibbonOptions
            {
                CloseArray = false,
                ClosePath = false,
                PathArray = $"{visualElement.Name}RibbonPathArray",
                SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack,
                Updatable = visualElement.IsAnimated
            },

            new GrBabylonJsMeshProperties
            {
                Material = visualElement.Style.Material.MaterialName,
                Visibility = visualElement.Visibility
            }
        );

        if (visualElement.IsAnimated)
            AddParallelogramSurfaceAnimation(visualElement, thinStyle);

        return visualElement;
    }

    private void AddParallelogramSurfaceAnimation(GrVisualParallelogramSurface3D visualElement, GrVisualSurfaceThickStyle3D thickStyle)
    {
        //if (visualElement.IsStatic || 
        //    visualElement.OriginPath is null ||
        //    visualElement.Direction1Path is null ||
        //    visualElement.Direction2Path is null
        //   )
        //    throw new InvalidOperationException();

        //var samplingSpecs = new GrBabylonJsAnimationSpecs(visualElement)
        //{
        //    LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
        //    EnableBlending = false,
        //    Loop = true
        //};

        //var frameRate = samplingSpecs.FrameRate;
        //var pointCount = visualElement.PointCount;

        //var positionAnimationsList = 
        //    new Triplet<GrBabylonJsAnimationOfFloat>[pointCount];

        //for (var i = 0; i < pointCount; i++)
        //{
        //    var frameIndexPositionPairs =
        //        visualElement
        //            .AnimatedPositionPath[i]
        //            .GetKeyFrameIndexPositionPairs(frameRate);

        //    var keyPositions = new GrBabylonJsKeyFrameDictionary<Float64Tuple3D>();

        //    foreach (var (frameIndex, position) in frameIndexPositionPairs)
        //        keyPositions.SetKeyFrameValue(frameIndex, position);

        //    positionAnimationsList[i] =
        //        SceneObject.AddVector3ComponentAnimations(
        //            visualElement.Name + $"{i}Position",
        //            samplingSpecs,
        //            keyPositions
        //        );
        //}

        //var animationGroup = SceneObject.AddAnimationGroup(
        //    visualElement.Name + "Animation",
        //    new GrBabylonJsAnimationGroup.AnimationGroupProperties()
        //    {
        //        LoopAnimation = true,
        //        IsAdditive = false,
        //        SpeedRatio = 1d
        //    }
        //);

        //for (var i = 0; i < pointCount; i++)
        //{
        //    animationGroup.AddAnimations(
        //        $"{visualElement.Name}Path[{i}]",
        //        positionAnimationsList[i]
        //    );
        //}

        //SceneObject.BeforeSceneRenderCode.Add(
        //    $"{visualElement.Name}DashedLines = BABYLON.MeshBuilder.CreateDashedLines(null, {{points: {visualElement.Name}DashedLinesPoints, instance: {visualElement.Name}DashedLines}});"
        //);
    }

    private void AddParallelogramSurfaceAnimation(GrVisualParallelogramSurface3D visualElement, GrVisualSurfaceThinStyle3D thinStyle)
    {
        var samplingSpecs = new GrBabylonJsAnimationSpecs(visualElement)
        {
            LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
            EnableBlending = false,
            Loop = true
        };

        var keyFrameRecords =
            visualElement.GetKeyFrameRecords();

        var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
        var path0KeyPositions0 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var path0KeyPositions1 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var path1KeyPositions0 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var path1KeyPositions1 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();

        foreach (var (frameIndex, _, visibility, position, direction1, direction2) in keyFrameRecords)
        {
            var path0Position0 = position;
            var path0Position1 = position.VectorAdd(direction1);
            var path1Position0 = position.VectorAdd(direction2);
            var path1Position1 = position.VectorAdd(direction2, direction1);

            keyVisibility.SetKeyFrameValue(frameIndex, visibility);
            path0KeyPositions0.Add(frameIndex, path0Position0);
            path0KeyPositions1.Add(frameIndex, path0Position1);
            path1KeyPositions0.Add(frameIndex, path1Position0);
            path1KeyPositions1.Add(frameIndex, path1Position1);
        }

        var visibilityAnimation =
            SceneObject.AddFloat32Animation(
                visualElement.Name + "Visibility",
                "visibility",
                samplingSpecs,
                keyVisibility
            );

        var path0Position0Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "RibbonPathArray0_0",
                samplingSpecs,
                path0KeyPositions0
            );

        var path0Position1Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "RibbonPathArray0_1",
                samplingSpecs,
                path0KeyPositions1
            );

        var path1Position0Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "RibbonPathArray1_0",
                samplingSpecs,
                path1KeyPositions0
            );

        var path1Position1Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "RibbonPathArray1_1",
                samplingSpecs,
                path1KeyPositions1
            );

        SceneObject.AddAnimationGroup(
            visualElement.Name + "Animation",
            new GrBabylonJsAnimationGroupProperties()
            {
                LoopAnimation = true,
                IsAdditive = false,
                SpeedRatio = 1d
            }
        ).AddAnimation(
            $"{visualElement.Name}Ribbon",
            visibilityAnimation
        ).AddAnimations(
            $"{visualElement.Name}RibbonPathArray[0][0]",
            path0Position0Animations
        ).AddAnimations(
            $"{visualElement.Name}RibbonPathArray[0][1]",
            path0Position1Animations
        ).AddAnimations(
            $"{visualElement.Name}RibbonPathArray[1][0]",
            path1Position0Animations
        ).AddAnimations(
            $"{visualElement.Name}RibbonPathArray[1][1]",
            path1Position1Animations
        );

        SceneObject.BeforeSceneRenderCode.Add(
            $"{visualElement.Name}Ribbon = BABYLON.MeshBuilder.CreateRibbon(null, {{pathArray: {visualElement.Name}RibbonPathArray, instance: {visualElement.Name}Ribbon}});"
        );
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
        var position000 = visualElement.Position.GetBabylonJsCode();
        var position001 = visualElement.Position1.GetBabylonJsCode();

        var position010 = visualElement.Position2.GetBabylonJsCode();
        var position011 = visualElement.Position12.GetBabylonJsCode();

        var position100 = visualElement.Position3.GetBabylonJsCode();
        var position101 = visualElement.Position13.GetBabylonJsCode();

        var position110 = visualElement.Position23.GetBabylonJsCode();
        var position111 = visualElement.Position123.GetBabylonJsCode();

        SceneObject.AddFreeCode(
            $"const {visualElement.Name}Ribbon1PathArray = [[{position000}, {position001}], [{position010}, {position011}], [{position110}, {position111}], [{position100}, {position101}]];",
            $"const {visualElement.Name}Ribbon2PathArray = [[{position000}, {position010}], [{position100}, {position110}]];",
            $"const {visualElement.Name}Ribbon3PathArray = [[{position001}, {position011}], [{position101}, {position111}]];"
        );

        SceneObject.AddRibbon(
            $"{visualElement.Name}Ribbon1",

            new GrBabylonJsRibbonOptions
            {
                CloseArray = true,
                ClosePath = false,
                PathArray = $"{visualElement.Name}Ribbon1PathArray",
                SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack,
                Updatable = visualElement.IsAnimated
            },

            new GrBabylonJsMeshProperties
            {
                Material = visualElement.Style.Material.MaterialName,
                Visibility = visualElement.Visibility
            }
        );

        SceneObject.AddRibbon(
            $"{visualElement.Name}Ribbon2",

            new GrBabylonJsRibbonOptions
            {
                CloseArray = false,
                ClosePath = false,
                PathArray = $"{visualElement.Name}Ribbon2PathArray",
                SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack,
                Updatable = visualElement.IsAnimated
            },

            new GrBabylonJsMeshProperties
            {
                Material = visualElement.Style.Material.MaterialName,
                Visibility = visualElement.Visibility
            }
        );

        SceneObject.AddRibbon(
            $"{visualElement.Name}Ribbon3",

            new GrBabylonJsRibbonOptions
            {
                CloseArray = false,
                ClosePath = false,
                PathArray = $"{visualElement.Name}Ribbon3PathArray",
                SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack,
                Updatable = visualElement.IsAnimated
            },

            new GrBabylonJsMeshProperties
            {
                Material = visualElement.Style.Material.MaterialName,
                Visibility = visualElement.Visibility
            }
        );

        if (visualElement.IsAnimated)
            AddParallelepipedSurfaceAnimation(visualElement, thinStyle);

        return visualElement;
    }

    private void AddParallelepipedSurfaceAnimation(GrVisualParallelepipedSurface3D visualElement, GrVisualSurfaceThickStyle3D thickStyle)
    {
        //if (visualElement.IsStatic || 
        //    visualElement.OriginPath is null ||
        //    visualElement.Direction1Path is null ||
        //    visualElement.Direction2Path is null
        //   )
        //    throw new InvalidOperationException();

        //var samplingSpecs = new GrBabylonJsAnimationSpecs(visualElement)
        //{
        //    LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
        //    EnableBlending = false,
        //    Loop = true
        //};

        //var frameRate = samplingSpecs.FrameRate;
        //var pointCount = visualElement.PointCount;

        //var positionAnimationsList = 
        //    new Triplet<GrBabylonJsAnimationOfFloat>[pointCount];

        //for (var i = 0; i < pointCount; i++)
        //{
        //    var frameIndexPositionPairs =
        //        visualElement
        //            .AnimatedPositionPath[i]
        //            .GetKeyFrameIndexPositionPairs(frameRate);

        //    var keyPositions = new GrBabylonJsKeyFrameDictionary<Float64Tuple3D>();

        //    foreach (var (frameIndex, position) in frameIndexPositionPairs)
        //        keyPositions.SetKeyFrameValue(frameIndex, position);

        //    positionAnimationsList[i] =
        //        SceneObject.AddVector3ComponentAnimations(
        //            visualElement.Name + $"{i}Position",
        //            samplingSpecs,
        //            keyPositions
        //        );
        //}

        //var animationGroup = SceneObject.AddAnimationGroup(
        //    visualElement.Name + "Animation",
        //    new GrBabylonJsAnimationGroup.AnimationGroupProperties()
        //    {
        //        LoopAnimation = true,
        //        IsAdditive = false,
        //        SpeedRatio = 1d
        //    }
        //);

        //for (var i = 0; i < pointCount; i++)
        //{
        //    animationGroup.AddAnimations(
        //        $"{visualElement.Name}Path[{i}]",
        //        positionAnimationsList[i]
        //    );
        //}

        //SceneObject.BeforeSceneRenderCode.Add(
        //    $"{visualElement.Name}DashedLines = BABYLON.MeshBuilder.CreateDashedLines(null, {{points: {visualElement.Name}DashedLinesPoints, instance: {visualElement.Name}DashedLines}});"
        //);
    }

    private void AddParallelepipedSurfaceAnimation(GrVisualParallelepipedSurface3D visualElement, GrVisualSurfaceThinStyle3D thinStyle)
    {
        if (visualElement.IsStatic ||
            visualElement.AnimatedPosition is null ||
            visualElement.AnimatedDirection1 is null ||
            visualElement.AnimatedDirection2 is null ||
            visualElement.AnimatedDirection3 is null
           )
            throw new InvalidOperationException();

        var samplingSpecs = new GrBabylonJsAnimationSpecs(visualElement)
        {
            LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
            EnableBlending = false,
            Loop = true
        };

        var keyFrameRecords =
            visualElement.GetKeyFrameRecords();

        var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();

        var path0KeyPositions0 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var path0KeyPositions1 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();

        var path1KeyPositions0 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var path1KeyPositions1 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();

        var path2KeyPositions0 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var path2KeyPositions1 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();

        var path3KeyPositions0 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var path3KeyPositions1 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();

        var path4KeyPositions0 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var path4KeyPositions1 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();

        var path5KeyPositions0 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var path5KeyPositions1 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();

        var path6KeyPositions0 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var path6KeyPositions1 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();

        var path7KeyPositions0 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var path7KeyPositions1 = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();

        foreach (var (frameIndex, _, visibility, position, direction1, direction2, direction3) in keyFrameRecords)
        {
            var position000 = position;

            var position001 = position.VectorAdd(direction1);
            var position010 = position.VectorAdd(direction2);
            var position100 = position.VectorAdd(direction3);

            var position011 = position001.VectorAdd(direction2);
            var position101 = position100.VectorAdd(direction1);
            var position110 = position010.VectorAdd(direction3);

            var position111 = position110.VectorAdd(direction1);

            keyVisibility.SetKeyFrameValue(frameIndex, visibility);

            // Ribbon 1
            path0KeyPositions0.Add(frameIndex, position000);
            path0KeyPositions1.Add(frameIndex, position001);

            path1KeyPositions0.Add(frameIndex, position010);
            path1KeyPositions1.Add(frameIndex, position011);

            path2KeyPositions0.Add(frameIndex, position110);
            path2KeyPositions1.Add(frameIndex, position111);

            path3KeyPositions0.Add(frameIndex, position100);
            path3KeyPositions1.Add(frameIndex, position101);

            // Ribbon 2
            path4KeyPositions0.Add(frameIndex, position000);
            path4KeyPositions1.Add(frameIndex, position010);

            path5KeyPositions0.Add(frameIndex, position100);
            path5KeyPositions1.Add(frameIndex, position110);

            // Ribbon 3
            path6KeyPositions0.Add(frameIndex, position001);
            path6KeyPositions1.Add(frameIndex, position011);

            path7KeyPositions0.Add(frameIndex, position101);
            path7KeyPositions1.Add(frameIndex, position111);
        }

        var visibilityAnimation =
            SceneObject.AddFloat32Animation(
                visualElement.Name + "Visibility",
                "visibility",
                samplingSpecs,
                keyVisibility
            );

        // Ribbon 1
        var path0Position0Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Ribbon1PathArray0_0",
                samplingSpecs,
                path0KeyPositions0
            );

        var path0Position1Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Ribbon1PathArray0_1",
                samplingSpecs,
                path0KeyPositions1
            );

        var path1Position0Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Ribbon1PathArray1_0",
                samplingSpecs,
                path1KeyPositions0
            );

        var path1Position1Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Ribbon1PathArray1_1",
                samplingSpecs,
                path1KeyPositions1
            );

        var path2Position0Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Ribbon1PathArray2_0",
                samplingSpecs,
                path2KeyPositions0
            );

        var path2Position1Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Ribbon1PathArray2_1",
                samplingSpecs,
                path2KeyPositions1
            );

        var path3Position0Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Ribbon1PathArray3_0",
                samplingSpecs,
                path3KeyPositions0
            );

        var path3Position1Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Ribbon1PathArray3_1",
                samplingSpecs,
                path3KeyPositions1
            );

        // Ribbon 2
        var path4Position0Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Ribbon2PathArray0_0",
                samplingSpecs,
                path4KeyPositions0
            );

        var path4Position1Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Ribbon2PathArray0_1",
                samplingSpecs,
                path4KeyPositions1
            );

        var path5Position0Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Ribbon2PathArray1_0",
                samplingSpecs,
                path5KeyPositions0
            );

        var path5Position1Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Ribbon2PathArray1_1",
                samplingSpecs,
                path5KeyPositions1
            );

        // Ribbon 3
        var path6Position0Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Ribbon3PathArray0_0",
                samplingSpecs,
                path6KeyPositions0
            );

        var path6Position1Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Ribbon3PathArray0_1",
                samplingSpecs,
                path6KeyPositions1
            );

        var path7Position0Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Ribbon3PathArray1_0",
                samplingSpecs,
                path7KeyPositions0
            );

        var path7Position1Animations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Ribbon3PathArray1_1",
                samplingSpecs,
                path7KeyPositions1
            );

        SceneObject.AddAnimationGroup(
            visualElement.Name + "Animation",
            new GrBabylonJsAnimationGroupProperties()
            {
                LoopAnimation = true,
                IsAdditive = false,
                SpeedRatio = 1d
            }
        ).AddAnimation(
            $"{visualElement.Name}Ribbon1",
            visibilityAnimation
        ).AddAnimation(
            $"{visualElement.Name}Ribbon2",
            visibilityAnimation
        ).AddAnimation(
            $"{visualElement.Name}Ribbon3",
            visibilityAnimation
        ).AddAnimations(
            $"{visualElement.Name}Ribbon1PathArray[0][0]",
            path0Position0Animations
        ).AddAnimations(
            $"{visualElement.Name}Ribbon1PathArray[0][1]",
            path0Position1Animations
        ).AddAnimations(
            $"{visualElement.Name}Ribbon1PathArray[1][0]",
            path1Position0Animations
        ).AddAnimations(
            $"{visualElement.Name}Ribbon1PathArray[1][1]",
            path1Position1Animations
        ).AddAnimations(
            $"{visualElement.Name}Ribbon1PathArray[2][0]",
            path2Position0Animations
        ).AddAnimations(
            $"{visualElement.Name}Ribbon1PathArray[2][1]",
            path2Position1Animations
        ).AddAnimations(
            $"{visualElement.Name}Ribbon1PathArray[3][0]",
            path3Position0Animations
        ).AddAnimations(
            $"{visualElement.Name}Ribbon1PathArray[3][1]",
            path3Position1Animations
        ).AddAnimations(
            $"{visualElement.Name}Ribbon2PathArray[0][0]",
            path4Position0Animations
        ).AddAnimations(
            $"{visualElement.Name}Ribbon2PathArray[0][1]",
            path4Position1Animations
        ).AddAnimations(
            $"{visualElement.Name}Ribbon2PathArray[1][0]",
            path5Position0Animations
        ).AddAnimations(
            $"{visualElement.Name}Ribbon2PathArray[1][1]",
            path5Position1Animations
        ).AddAnimations(
            $"{visualElement.Name}Ribbon3PathArray[0][0]",
            path6Position0Animations
        ).AddAnimations(
            $"{visualElement.Name}Ribbon3PathArray[0][1]",
            path6Position1Animations
        ).AddAnimations(
            $"{visualElement.Name}Ribbon3PathArray[1][0]",
            path7Position0Animations
        ).AddAnimations(
            $"{visualElement.Name}Ribbon3PathArray[1][1]",
            path7Position1Animations
        );

        SceneObject.BeforeSceneRenderCode.Add(
            $"{visualElement.Name}Ribbon1 = BABYLON.MeshBuilder.CreateRibbon(null, {{pathArray: {visualElement.Name}Ribbon1PathArray, instance: {visualElement.Name}Ribbon1}});"
        );

        SceneObject.BeforeSceneRenderCode.Add(
            $"{visualElement.Name}Ribbon2 = BABYLON.MeshBuilder.CreateRibbon(null, {{pathArray: {visualElement.Name}Ribbon2PathArray, instance: {visualElement.Name}Ribbon2}});"
        );

        SceneObject.BeforeSceneRenderCode.Add(
            $"{visualElement.Name}Ribbon3 = BABYLON.MeshBuilder.CreateRibbon(null, {{pathArray: {visualElement.Name}Ribbon3PathArray, instance: {visualElement.Name}Ribbon3}});"
        );
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

        SceneObject.AddClone(
            $"{visualElement.Name}OuterSphere",
            PrototypeOuterSphere.ConstName,
            new GrBabylonJsMeshProperties
            {
                IsVisible = true,
                Material = visualElement.Style.Material.MaterialName,
                Visibility = visualElement.Visibility,
                Position = visualElement.Center.ToBabylonJsVector3Value(),
                Scaling = LinFloat64Vector3D.CreateEqualXyz(outerRadius)
            }
        );

        SceneObject.AddClone(
            $"{visualElement.Name}InnerSphere",
            PrototypeInnerSphere.ConstName,
            new GrBabylonJsMeshProperties
            {
                IsVisible = true,
                Material = visualElement.Style.Material.MaterialName,
                Visibility = visualElement.Visibility,
                Position = visualElement.Center.ToBabylonJsVector3Value(),
                Scaling = LinFloat64Vector3D.CreateEqualXyz(innerRadius)
            }
        );

        if (visualElement.IsAnimated)
            AddSphereAnimation(visualElement, thickStyle);

        return visualElement;
    }

    private GrVisualSphereSurface3D AddSphere(GrVisualSphereSurface3D visualElement, GrVisualSurfaceThinStyle3D thinStyle)
    {
        SceneObject.AddClone(
            $"{visualElement.Name}Sphere",
            PrototypeOuterSphere.ConstName,
            new GrBabylonJsMeshProperties
            {
                IsVisible = true,
                Material = visualElement.Style.Material.MaterialName,
                Visibility = visualElement.Visibility,
                Position = visualElement.Center.ToBabylonJsVector3Value(),
                Scaling = LinFloat64Vector3D.CreateEqualXyz(visualElement.Radius)
            }
        );

        if (visualElement.IsAnimated)
            AddSphereAnimation(visualElement, thinStyle);

        return visualElement;
    }

    private void AddSphereAnimation(GrVisualSphereSurface3D visualElement, GrVisualSurfaceThickStyle3D thickStyle)
    {
        var samplingSpecs = new GrBabylonJsAnimationSpecs(visualElement)
        {
            LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
            EnableBlending = false,
            Loop = true
        };

        var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
        var keyPositions = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var keyOuterScaling = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var keyInnerScaling = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();

        foreach (var (frameIndex, _, visibility, center, radius) in visualElement.GetKeyFrameRecords())
        {
            var outerRadius = radius + thickStyle.Thickness * 0.25d;
            var innerRadius = visualElement.Radius - thickStyle.Thickness * 0.25d;

            keyVisibility.SetKeyFrameValue(
                frameIndex,
                visibility
            );

            keyPositions.SetKeyFrameValue(
                frameIndex,
                center
            );

            keyInnerScaling.SetKeyFrameValue(
                frameIndex,
                innerRadius * LinFloat64Vector3D.Symmetric
            );

            keyOuterScaling.SetKeyFrameValue(
                frameIndex,
                outerRadius * LinFloat64Vector3D.Symmetric
            );
        }

        var visibilityAnimation =
            SceneObject.AddFloat32Animation(
                visualElement.Name + "SphereVisibility",
                "visibility",
                samplingSpecs,
                keyVisibility
            );

        var positionAnimations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "SpherePosition",
                samplingSpecs,
                keyPositions
            );

        var innerScalingAnimations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "InnerSphereScaling",
                samplingSpecs,
                keyInnerScaling
            );

        var outerScalingAnimations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "OuterSphereScaling",
                samplingSpecs,
                keyOuterScaling
            );

        SceneObject.AddAnimationGroup(
            visualElement.Name + "Animation",
            new GrBabylonJsAnimationGroupProperties()
            {
                LoopAnimation = true,
                IsAdditive = false,
                SpeedRatio = 1d
            }
        ).AddAnimation(
            $"{visualElement.Name}InnerSphere",
            visibilityAnimation
        ).AddAnimation(
            $"{visualElement.Name}OuterSphere",
            visibilityAnimation
        ).AddAnimations(
            $"{visualElement.Name}InnerSphere.position",
            positionAnimations
        ).AddAnimations(
            $"{visualElement.Name}OuterSphere.position",
            positionAnimations
        ).AddAnimations(
            $"{visualElement.Name}InnerSphere.scaling",
            innerScalingAnimations
        ).AddAnimations(
            $"{visualElement.Name}OuterSphere.scaling",
            outerScalingAnimations
        );
    }

    private void AddSphereAnimation(GrVisualSphereSurface3D visualElement, GrVisualSurfaceThinStyle3D thinStyle)
    {
        var samplingSpecs = new GrBabylonJsAnimationSpecs(visualElement)
        {
            LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
            EnableBlending = false,
            Loop = true
        };

        var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
        var keyPositions = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var keyScaling = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();

        foreach (var (frameIndex, _, visibility, center, radius) in visualElement.GetKeyFrameRecords())
        {
            keyVisibility.SetKeyFrameValue(
                frameIndex,
                visibility
            );

            keyPositions.SetKeyFrameValue(
                frameIndex,
                center
            );

            keyScaling.SetKeyFrameValue(
                frameIndex,
                radius * LinFloat64Vector3D.Symmetric
            );
        }

        var visibilityAnimation =
            SceneObject.AddFloat32Animation(
                visualElement.Name + "SphereVisibility",
                "visibility",
                samplingSpecs,
                keyVisibility
            );

        var positionAnimations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "SpherePosition",
                samplingSpecs,
                keyPositions
            );

        var scalingAnimations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "SphereScaling",
                samplingSpecs,
                keyScaling
            );

        SceneObject.AddAnimationGroup(
            visualElement.Name + "Animation",
            new GrBabylonJsAnimationGroupProperties()
            {
                LoopAnimation = true,
                IsAdditive = false,
                SpeedRatio = 1d
            }
        ).AddAnimation(
            $"{visualElement.Name}Sphere",
            visibilityAnimation
        ).AddAnimations(
            $"{visualElement.Name}Sphere.position",
            positionAnimations
        ).AddAnimations(
            $"{visualElement.Name}Sphere.scaling",
            scalingAnimations
        );
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
        var normal = visualElement.Normal.ToUnitLinVector3D();

        var quaternion = LinBasisVector3D.Py.VectorToVectorRotationQuaternion(normal);

        SceneObject.AddClone(
            $"{visualElement.Name}Sphere",
            PrototypeOuterSphere.ConstName,
            new GrBabylonJsMeshProperties
            {
                IsVisible = true,
                Material = visualElement.Style.Material.MaterialName,
                Visibility = visualElement.Visibility,
                Position = visualElement.Center.ToBabylonJsVector3Value(),
                RotationQuaternion = quaternion,
                Scaling = LinFloat64Vector3D.Create(visualElement.Radius,
                    thickStyle.Thickness * 0.5d,
                    visualElement.Radius)
            }
        );

        if (visualElement.DrawEdge)
        {
            SceneObject.AddTorus(
                $"{visualElement.Name}EdgeTorus",

                new GrBabylonJsTorusOptions
                {
                    Diameter = visualElement.Radius * 2,
                    Thickness = thickStyle.Thickness,
                    Tessellation = 320
                },

                new GrBabylonJsMeshProperties
                {
                    Material = thickStyle.EdgeMaterial.MaterialName,
                    Visibility = visualElement.Visibility,
                    Position = visualElement.Center.ToBabylonJsVector3Value(),
                    RotationQuaternion = quaternion
                }
            );
        }

        if (visualElement.IsAnimated)
            AddDiscAnimation(visualElement, thickStyle);

        return visualElement;
    }

    private GrVisualCircleSurface3D AddDisc(GrVisualCircleSurface3D visualElement, GrVisualSurfaceThinStyle3D thinStyle)
    {
        var normal = visualElement.Normal.ToUnitLinVector3D();

        var quaternion = LinBasisVector3D.Pz.VectorToVectorRotationQuaternion(normal);

        SceneObject.AddClone(
            $"{visualElement.Name}Disc",
            PrototypeDisc.ConstName,
            new GrBabylonJsMeshProperties
            {
                IsVisible = true,
                Material = visualElement.Style.Material.MaterialName,
                Visibility = visualElement.Visibility,
                Position = visualElement.Center.ToBabylonJsVector3Value(),
                RotationQuaternion = quaternion,
                Scaling = LinFloat64Vector3D.Create(visualElement.Radius,
                    visualElement.Radius,
                    1d)
            }
        );

        if (visualElement.DrawEdge)
        {
            var (point1, point2, point3) =
                visualElement.GetEdgePointsTriplet();

            var pathName = $"{visualElement.Name}EdgePath";

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
                $"{visualElement.Name}EdgeLine",

                new GrBabylonJsLinesOptions
                {
                    Points = $"{pathName}.getPoints()"
                },

                new GrBabylonJsLinesMeshProperties
                {
                    Color = thinStyle.EdgeColor,
                    Visibility = visualElement.Visibility
                }
            );
        }

        if (visualElement.IsAnimated)
            AddDiscAnimation(visualElement, thinStyle);

        return visualElement;
    }

    private void AddDiscAnimation(GrVisualCircleSurface3D visualElement, GrVisualSurfaceThickStyle3D thickStyle)
    {
        var samplingSpecs = new GrBabylonJsAnimationSpecs(visualElement)
        {
            LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
            EnableBlending = false,
            Loop = true
        };

        var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
        var keyPositions = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var keyScalings = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var keyQuaternions = new GrBabylonJsKeyFrameDictionary<LinFloat64Quaternion>();

        foreach (var (frameIndex, _, visibility, center, normal, radius) in visualElement.GetKeyFrameRecords())
        {
            var quaternion = LinBasisVector3D.Py.VectorToVectorRotationQuaternion(normal.ToUnitLinVector3D());
            var scaling = LinFloat64Vector3D.Create(radius,
                thickStyle.Thickness * 0.5d,
                radius);

            keyVisibility.SetKeyFrameValue(frameIndex, visibility);
            keyPositions.SetKeyFrameValue(frameIndex, center);
            keyScalings.SetKeyFrameValue(frameIndex, scaling);
            keyQuaternions.SetKeyFrameValue(frameIndex, quaternion);
        }

        var visibilityAnimation =
            SceneObject.AddFloat32Animation(
                visualElement.Name + "Visibility",
                "visibility",
                samplingSpecs,
                keyVisibility
            );

        var positionAnimations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Position",
                samplingSpecs,
                keyPositions
            );

        var scalingAnimations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Scaling",
                samplingSpecs,
                keyScalings
            );

        var quaternionAnimation =
            SceneObject.AddQuaternionAnimation(
                visualElement.Name + "Quaternion",
                "rotationQuaternion",
                samplingSpecs,
                keyQuaternions.OptimizeQuaternionKeyFrames()
            );

        SceneObject
            .AddAnimationGroup(
                visualElement.Name + "Animations",
                new GrBabylonJsAnimationGroupProperties()
                {
                    LoopAnimation = true,
                    IsAdditive = false,
                    SpeedRatio = 1d
                }
            ).AddAnimation(
                $"{visualElement.Name}Sphere",
                visibilityAnimation
            ).AddAnimations(
                $"{visualElement.Name}Sphere.position",
                positionAnimations
            ).AddAnimations(
                $"{visualElement.Name}Sphere.scaling",
                scalingAnimations
            ).AddAnimation(
                $"{visualElement.Name}Sphere",
                quaternionAnimation
            );
    }

    private void AddDiscAnimation(GrVisualCircleSurface3D visualElement, GrVisualSurfaceThinStyle3D thinStyle)
    {
        var samplingSpecs = new GrBabylonJsAnimationSpecs(visualElement)
        {
            LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
            EnableBlending = false,
            Loop = true
        };

        var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
        var keyPositions = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var keyScalings = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var keyQuaternions = new GrBabylonJsKeyFrameDictionary<LinFloat64Quaternion>();

        foreach (var (frameIndex, _, visibility, center, normal, radius) in visualElement.GetKeyFrameRecords())
        {
            var quaternion = LinBasisVector3D.Pz.VectorToVectorRotationQuaternion(normal.ToUnitLinVector3D());
            var scaling = LinFloat64Vector3D.Create(radius,
                radius,
                1d);

            keyVisibility.SetKeyFrameValue(frameIndex, visibility);
            keyPositions.SetKeyFrameValue(frameIndex, center);
            keyScalings.SetKeyFrameValue(frameIndex, scaling);
            keyQuaternions.SetKeyFrameValue(frameIndex, quaternion);
        }

        var visibilityAnimation =
            SceneObject.AddFloat32Animation(
                visualElement.Name + "Visibility",
                "visibility",
                samplingSpecs,
                keyVisibility
            );

        var positionAnimations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Position",
                samplingSpecs,
                keyPositions
            );

        var scalingAnimations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Scaling",
                samplingSpecs,
                keyScalings
            );

        var quaternionAnimation =
            SceneObject.AddQuaternionAnimation(
                visualElement.Name + "Quaternion",
                "rotationQuaternion",
                samplingSpecs,
                keyQuaternions.OptimizeQuaternionKeyFrames()
            );

        SceneObject
            .AddAnimationGroup(
                visualElement.Name + "Animations",
                new GrBabylonJsAnimationGroupProperties()
                {
                    LoopAnimation = true,
                    IsAdditive = false,
                    SpeedRatio = 1d
                }
            ).AddAnimation(
                $"{visualElement.Name}Disc",
                visibilityAnimation
            ).AddAnimations(
                $"{visualElement.Name}Disc.position",
                positionAnimations
            ).AddAnimations(
                $"{visualElement.Name}Disc.scaling",
                scalingAnimations
            ).AddAnimation(
                $"{visualElement.Name}Disc",
                quaternionAnimation
            );
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
        var vector = visualElement.Direction1;
        var normal = visualElement.Normal;

        var quaternion =
            LinBasisVectorPair3D
                .PxPy
                .VectorPairToVectorPairRotationQuaternion(
                    vector,
                    normal
                );

        SceneObject.AddSphere(
            $"{visualElement.Name}Sphere",

            new GrBabylonJsSphereOptions
            {
                Arc = visualElement.ArcRatio,
                DiameterX = visualElement.Radius * 2,
                DiameterY = thickStyle.Thickness,
                DiameterZ = visualElement.Radius * 2,
                Segments = 320
            },

            new GrBabylonJsMeshProperties
            {
                Material = thickStyle.Material.MaterialName,
                Visibility = visualElement.Visibility,
                Position = visualElement.Center.ToBabylonJsVector3Value(),
                RotationQuaternion = quaternion
            }
        );

        if (visualElement.DrawEdge)
        {
            var length = visualElement.ArcLength;
            var pathPointCount = (int)(2 * length + 1).Ceiling();
            var (point1, point2, point3) =
                visualElement.GetArcPointsTriplet();

            var pathName = $"{visualElement.Name}EdgePath";

            SceneObject.AddArcThru3Points(
                pathName,
                point1,
                point2,
                point3,
                pathPointCount,
                false,
                false
            );

            SceneObject.AddTube(
                $"{visualElement.Name}EdgeTube",

                new GrBabylonJsTubeOptions
                {
                    Path = $"{pathName}.getPoints()",
                    Radius = thickStyle.Thickness / 2d,
                    Tessellation = 320,
                    Cap = GrBabylonJsMeshCap.StartAndEnd
                },

                new GrBabylonJsMeshProperties
                {
                    Material = thickStyle.Material.MaterialName,
                    Visibility = visualElement.Visibility,
                }
            );
        }

        var qYx =
            LinBasisVector3D.Py.VectorToVectorRotationQuaternion(LinBasisVector3D.Px);

        SceneObject.AddDisc(
            $"{visualElement.Name}Disc1",

            new GrBabylonJsDiscOptions
            {
                Arc = 0.5d,
                Radius = visualElement.Radius,
                Tessellation = 320,
                SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack
            },

            new GrBabylonJsMeshProperties
            {
                Material = thickStyle.Material.MaterialName,
                Visibility = visualElement.Visibility,
                Scaling = LinFloat64Vector3D.Create(thickStyle.Thickness / (2d * visualElement.Radius), 1, 1),
                Position = visualElement.Center.ToBabylonJsVector3Value(),
                RotationQuaternion = qYx.Concatenate(quaternion)
            }
        );


        var q2 = LinFloat64Vector3D.E2.RotationAxisAngleToQuaternion(visualElement.Angle);

        SceneObject.AddDisc(
            $"{visualElement.Name}Disc2",

            new GrBabylonJsDiscOptions()
            {
                Arc = 0.5d,
                Radius = visualElement.Radius,
                Tessellation = 320,
                SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack
            },

            new GrBabylonJsMeshProperties
            {
                Material = visualElement.Style.Material.MaterialName,
                Visibility = visualElement.Visibility,
                Scaling = LinFloat64Vector3D.Create(thickStyle.Thickness / (2d * visualElement.Radius), 1, 1),
                Position = visualElement.Center.ToBabylonJsVector3Value(),
                RotationQuaternion = qYx.Concatenate(q2, quaternion)
            }
        );

        return visualElement;
    }

    private GrVisualCircleArcSurface3D AddDiscSector(GrVisualCircleArcSurface3D visualElement, GrVisualSurfaceThinStyle3D thinStyle)
    {
        if (visualElement.IsAnimated)
        {
            SceneObject.AddFreeCode(
                $"const {visualElement.Name}Points0 = createOriginPathPoints();",
                $"const {visualElement.Name}Points1 = createXyCirclePathPoints({visualElement.Angle.RadiansValue});",
                $"const {visualElement.Name}RibbonPathArray = [{visualElement.Name}Points0, {visualElement.Name}Points1];"
            );

            SceneObject.AddRibbon(
                $"{visualElement.Name}Ribbon",

                new GrBabylonJsRibbonOptions
                {
                    CloseArray = false,
                    ClosePath = false,
                    PathArray = $"{visualElement.Name}RibbonPathArray",
                    SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack,
                    Updatable = true
                },

                new GrBabylonJsMeshProperties
                {
                    Material = visualElement.Style.Material.MaterialName,
                    Visibility = visualElement.Visibility
                }
            );

            SceneObject.AddFreeCode(
                $"{visualElement.Name}Ribbon.circleArcRadius = 1;",
                $"{visualElement.Name}Ribbon.circleArcRatio = 1;"
            );

            AddDiscSectorAnimation(visualElement, thinStyle);
        }
        else
        {
            var vector = visualElement.Direction1;
            var normal = visualElement.Normal;

            var quaternion =
                LinBasisVectorPair3D
                    .PxPz
                    .VectorPairToVectorPairRotationQuaternion(
                        vector,
                        normal
                    );

            SceneObject.AddDisc(
                $"{visualElement.Name}Disc",

                new GrBabylonJsDiscOptions
                {
                    Arc = visualElement.ArcRatio,
                    Radius = visualElement.Radius,
                    SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack,
                    Tessellation = 320
                },

                new GrBabylonJsMeshProperties
                {
                    Material = visualElement.Style.Material.MaterialName,
                    Visibility = visualElement.Visibility,
                    Position = visualElement.Center.ToBabylonJsVector3Value(),
                    RotationQuaternion = quaternion
                }
            );

            if (visualElement.DrawEdge)
            {
                var length = visualElement.ArcLength;
                var pathPointCount = (int)(2 * length + 1).Ceiling();
                var (point1, point2, point3) =
                    visualElement.GetArcPointsTriplet();

                var pathName = $"{visualElement.Name}EdgePath";

                SceneObject.AddArcThru3Points(
                    pathName,
                    point1,
                    point2,
                    point3,
                    pathPointCount,
                    false,
                    false
                );

                SceneObject.AddLines(
                    $"{visualElement.Name}Line",

                    new GrBabylonJsLinesOptions
                    {
                        Points = $"{pathName}.getPoints()"
                    },

                    new GrBabylonJsLinesMeshProperties
                    {
                        Color = thinStyle.EdgeColor,
                        Visibility = visualElement.Visibility,
                    }
                );
            }
        }

        return visualElement;
    }

    private void AddDiscSectorAnimation(GrVisualCircleArcSurface3D visualElement, GrVisualSurfaceThinStyle3D thinStyle)
    {
        var samplingSpecs = new GrBabylonJsAnimationSpecs(visualElement)
        {
            LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
            EnableBlending = false,
            Loop = true
        };

        var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
        var keyRadius = new GrBabylonJsKeyFrameDictionary<double>();
        var keyArcRatio = new GrBabylonJsKeyFrameDictionary<double>();
        var keyPositions = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();
        var keyQuaternions = new GrBabylonJsKeyFrameDictionary<LinFloat64Quaternion>();

        foreach (var (frameIndex, _, visibility, center, direction1, direction2, angle, radius) in visualElement.GetKeyFrameRecords())
        {
            var quaternion =
                LinBasisVectorPair3D
                    .PxPy
                    .VectorPairToVectorPairRotationQuaternion(
                        direction1,
                        direction2
                    );

            keyVisibility.SetKeyFrameValue(frameIndex, visibility);
            keyRadius.SetKeyFrameValue(frameIndex, radius);
            keyArcRatio.SetKeyFrameValue(frameIndex, angle.RadiansValue / (Math.Tau));
            keyPositions.SetKeyFrameValue(frameIndex, center);
            keyQuaternions.SetKeyFrameValue(frameIndex, quaternion);
        }

        var visibilityAnimation =
            SceneObject.AddFloat32Animation(
                visualElement.Name + "Visibility",
                "visibility",
                samplingSpecs,
                keyVisibility
            );

        var radiusAnimation =
            SceneObject.AddFloat32Animation(
                visualElement.Name + "CircleArcRadius",
                "circleArcRadius",
                samplingSpecs,
                keyRadius
            );

        var angleAnimation =
            SceneObject.AddFloat32Animation(
                visualElement.Name + "CircleArcRatio",
                "circleArcRatio",
                samplingSpecs,
                keyArcRatio
            );

        var positionAnimations =
            SceneObject.AddVector3ComponentAnimations(
                visualElement.Name + "Position",
                samplingSpecs,
                keyPositions
            );

        var quaternionAnimation =
            SceneObject.AddQuaternionAnimation(
                visualElement.Name + "Quaternion",
                "rotationQuaternion",
                samplingSpecs,
                keyQuaternions.OptimizeQuaternionKeyFrames()
            );

        SceneObject
            .AddAnimationGroup(
                visualElement.Name + "Animations",
                new GrBabylonJsAnimationGroupProperties()
                {
                    LoopAnimation = true,
                    IsAdditive = false,
                    SpeedRatio = 1d
                }
            ).AddAnimation(
                $"{visualElement.Name}Ribbon",
                visibilityAnimation
            ).AddAnimation(
                $"{visualElement.Name}Ribbon",
                radiusAnimation
            ).AddAnimation(
                $"{visualElement.Name}Ribbon",
                angleAnimation
            ).AddAnimations(
                $"{visualElement.Name}Ribbon.position",
                positionAnimations
            ).AddAnimation(
                $"{visualElement.Name}Ribbon",
                quaternionAnimation
            );

        SceneObject.BeforeSceneRenderCode.Add(
            $@"
updateXyCirclePathPoints({visualElement.Name}Points1, {visualElement.Name}Ribbon.circleArcRadius, {visualElement.Name}Ribbon.circleArcRatio);

{visualElement.Name}Ribbon = BABYLON.MeshBuilder.CreateRibbon(null, {{pathArray: {visualElement.Name}RibbonPathArray, instance: {visualElement.Name}Ribbon}});
".Trim()
        );
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
        var normal = visualElement.Normal.ToUnitLinVector3D();

        var quaternion = LinBasisVector3D.Py.VectorToVectorRotationQuaternion(normal);

        var diameter =
            visualElement.MaxRadius + visualElement.MinRadius;

        var thickness =
            Math.Abs(visualElement.MaxRadius - visualElement.MinRadius);

        SceneObject.AddTorus(
            $"{visualElement.Name}Torus",

            new GrBabylonJsTorusOptions
            {
                Diameter = diameter,
                Thickness = thickness,
                Tessellation = 320
            },

            new GrBabylonJsMeshProperties
            {
                Material = visualElement.Style.Material.MaterialName,
                Visibility = visualElement.Visibility,
                Position = visualElement.Center.ToBabylonJsVector3Value(),
                Scaling = LinFloat64Vector3D.Create(1, thickStyle.Thickness / thickness, 1),
                RotationQuaternion = quaternion
            }
        );

        return visualElement;
    }

    private GrVisualCircleRingSurface3D AddRing(GrVisualCircleRingSurface3D visualElement, GrVisualSurfaceThinStyle3D thinStyle)
    {
        throw new NotImplementedException();
    }
    

    public GrBabylonJsSceneComposer AddDefaultAxes(ILinFloat64Vector3D axesOrigin)
    {
        // Add reference unit axis frame
        AddElement(
            GrVisualFrame3D.CreateStatic(
                "axisFrame",
                new GrVisualFrameStyle3D
                {
                    OriginStyle = SceneObject.AddSimpleMaterial("axisFrameOriginMaterial", Color.DarkGray).CreateThickSurfaceStyle(0.075),
                    Direction1Style = SceneObject.AddSimpleMaterial("axisFrameXMaterial", Color.DarkRed).CreateTubeCurveStyle(0.035),
                    Direction2Style = SceneObject.AddSimpleMaterial("axisFrameYMaterial", Color.DarkGreen).CreateTubeCurveStyle(0.035),
                    Direction3Style = SceneObject.AddSimpleMaterial("axisFrameZMaterial", Color.DarkBlue).CreateTubeCurveStyle(0.035)
                },
                axesOrigin
            )
        );

        return this;
    }


    public GrBabylonJsSceneComposer AddDefaultGridXy(IGrVisualImageSource texture, int gridUnitCount, double unitSize = 1, double zValue = 0, double opacity = 0.25)
    {
        AddSquareGrid(
            GrVisualSquareGrid3D.DefaultXy(
                zValue * LinFloat64Vector3D.E3,
                texture,
                gridUnitCount,
                unitSize,
                opacity
            )
        );

        return this;
    }

    public GrBabylonJsSceneComposer AddDefaultGridYz(IGrVisualImageSource texture, int gridUnitCount, double unitSize = 1, double xValue = 0, double opacity = 0.25)
    {
        AddSquareGrid(
            GrVisualSquareGrid3D.DefaultYz(
                xValue * LinFloat64Vector3D.E1,
                texture,
                gridUnitCount,
                unitSize,
                opacity
            )
        );

        return this;
    }

    public GrBabylonJsSceneComposer AddDefaultGridZx(IGrVisualImageSource texture, int gridUnitCount, double unitSize = 1, double yValue = 0, double opacity = 0.25)
    {
        AddSquareGrid(
            GrVisualSquareGrid3D.DefaultZx(
                yValue * LinFloat64Vector3D.E2,
                texture,
                gridUnitCount,
                unitSize,
                opacity
            )
        );

        return this;
    }

    public GrBabylonJsSceneComposer AddDefaultGridXyz(IGrVisualImageSource texture, int gridUnitCount, double unitSize = 1, double opacity = 0.25)
    {
        AddSquareGrid(
            GrVisualSquareGrid3D.DefaultXy(
                LinFloat64Vector3D.Zero,
                texture,
                gridUnitCount,
                unitSize,
                opacity
            )
        );

        AddSquareGrid(
            GrVisualSquareGrid3D.DefaultYz(
                LinFloat64Vector3D.Zero,
                texture,
                gridUnitCount,
                unitSize,
                opacity
            )
        );

        AddSquareGrid(
            GrVisualSquareGrid3D.DefaultZx(
                LinFloat64Vector3D.Zero,
                texture,
                gridUnitCount,
                unitSize,
                opacity
            )
        );

        return this;
    }


    public GrBabylonJsSceneComposer AddDefaultEnvironment(int gridUnitCount)
    {
        //var scene = MainSceneComposer.SceneObject;
        //scene.SceneProperties.AmbientColor = Color.AliceBlue;

        // Add scene environment
        SceneObject.AddEnvironmentHelper(
            "environmentHelper",

            new GrBabylonJsEnvironmentHelperOptions
            {
                GroundYBias = 0.01,
                SkyboxColor = Color.LightSkyBlue,
                GroundColor = Color.White,
                CreateGround = true,
                GroundSize = 8,
                SkyboxSize = gridUnitCount + 10
            }
        );

        return this;
    }

    public GrBabylonJsSceneComposer AddDefaultPerspectiveCamera(GrBabylonJsFloat32Value cameraDistance, GrBabylonJsAngleValue alpha, GrBabylonJsAngleValue beta)
    {
        // Add main scene camera
        SceneObject.AddArcRotateCamera(
            "defaultCamera",
            alpha,
            beta,
            cameraDistance,
            "BABYLON.Vector3.Zero()",
            new GrBabylonJsArcRotateCameraProperties
            {
                Mode = GrBabylonJsCameraMode.PerspectiveCamera
            }
        );

        return this;
    }

    public GrBabylonJsSceneComposer AddDefaultOrthographicCamera(GrBabylonJsFloat32Value cameraDistance, GrBabylonJsAngleValue alpha, GrBabylonJsAngleValue beta, Float64BoundingBox2D size)
    {
        // Add main scene camera
        SceneObject.AddArcRotateCamera(
            "defaultCamera",
            alpha,
            beta,
            cameraDistance,
            "BABYLON.Vector3.Zero()",
            new GrBabylonJsArcRotateCameraProperties
            {
                Mode = GrBabylonJsCameraMode.OrthographicCamera,
                OrthoLeft = size.MinX,
                OrthoRight = size.MaxX,
                OrthoBottom = size.MinY,
                OrthoTop = size.MaxY
            }
        );

        return this;
    }


    public string GetCreateSceneCode()
    {
        var sceneName = SceneObject.ConstName;
        var sceneCode = SceneObject.GetBabylonJsCode();

        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine($"function create{sceneName.Pascalize()}() {{")
            .IncreaseIndentation();

        codeComposer.AppendLineAtNewLine(@$"
{sceneCode}

const light = new BABYLON.HemisphericLight(""light"", new BABYLON.Vector3(0, 1, 0), {sceneName});
//light.intensity = 0.7;

");

        codeComposer.AppendLineAtNewLine(
            SceneObject.KeyFramesCache.GetCode()
        );

        foreach (var babylonObject in SceneObject.ObjectList)
            codeComposer.AppendLineAtNewLine(babylonObject.GetBabylonJsCode());

        if (SceneObject.BeforeSceneRenderCode.Count > 0)
        {
            var beforeSceneRenderCode =
                SceneObject.BeforeSceneRenderCode.Concatenate(Environment.NewLine);

            codeComposer
                .AppendLineAtNewLine($"{sceneName}.registerBeforeRender(function() {{")
                .IncreaseIndentation()
                .AppendLineAtNewLine(beforeSceneRenderCode)
                .DecreaseIndentation()
                .AppendLineAtNewLine("});");
        }

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