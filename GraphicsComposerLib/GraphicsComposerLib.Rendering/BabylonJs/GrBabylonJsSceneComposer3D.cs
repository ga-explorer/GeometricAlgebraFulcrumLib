using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using GraphicsComposerLib.Rendering.BabylonJs.Animations;
using GraphicsComposerLib.Rendering.BabylonJs.Cameras;
using GraphicsComposerLib.Rendering.BabylonJs.Constants;
using GraphicsComposerLib.Rendering.BabylonJs.Materials;
using GraphicsComposerLib.Rendering.BabylonJs.Meshes;
using GraphicsComposerLib.Rendering.BabylonJs.Textures;
using GraphicsComposerLib.Rendering.BabylonJs.Values;
using GraphicsComposerLib.Rendering.Visuals.Space3D;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Basic;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Curves;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Grids;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Images;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Styles;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;
using Humanizer;
using TextComposerLib.Text;
using TextComposerLib.Text.Linear;
using WebComposerLib.Colors;
using WebComposerLib.Html.Media;

namespace GraphicsComposerLib.Rendering.BabylonJs
{
    public class GrBabylonJsSceneComposer3D :
        GrVisualElementsSceneComposer3D<GrBabylonJsScene>
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


        public GrBabylonJsSceneComposer3D()
        {
            SceneObject = new GrBabylonJsScene("scene")
                .SetOptions(new GrBabylonJsScene.SceneOptions())
                .SetProperties(new GrBabylonJsScene.SceneProperties());

            SnapshotSpecs = new GrBabylonJsSnapshotSpecs();

            AddInitialObjects();
        }

        public GrBabylonJsSceneComposer3D(string constName)
        {
            SceneObject = new GrBabylonJsScene(constName)
                .SetOptions(new GrBabylonJsScene.SceneOptions())
                .SetProperties(new GrBabylonJsScene.SceneProperties());

            SnapshotSpecs = new GrBabylonJsSnapshotSpecs();

            AddInitialObjects();
        }

        public GrBabylonJsSceneComposer3D(string constName, GrBabylonJsSnapshotSpecs snapshotSpecs)
        {
            SceneObject = new GrBabylonJsScene(constName)
                .SetOptions(new GrBabylonJsScene.SceneOptions())
                .SetProperties(new GrBabylonJsScene.SceneProperties());

            SnapshotSpecs = snapshotSpecs;

            AddInitialObjects();
        }


        private void AddInitialObjects()
        {
            PrototypeDisc = SceneObject.AddDisc(
                $"prototypeDisc",

                new GrBabylonJsDisc.DiscOptions
                {
                    Radius = 1d,
                    SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack,
                    Tessellation = 512,
                    //Updatable = true
                },

                new GrBabylonJsMesh.MeshProperties
                {
                    IsVisible = false,
                    Position = Float64Vector3D.Zero
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

                new GrBabylonJsSphere.SphereOptions
                {
                    Diameter = 2d,
                    Segments = 512,
                    SideOrientation = GrBabylonJsMeshOrientation.Back,
                    //Updatable = true
                },

                new GrBabylonJsMesh.MeshProperties
                {
                    IsVisible = false,
                    Position = Float64Vector3D.Zero
                }
            );

            PrototypeOuterSphere = SceneObject.AddSphere(
                $"prototypeOuterSphere",

                new GrBabylonJsSphere.SphereOptions
                {
                    Diameter = 2d,
                    Segments = 512,
                    SideOrientation = GrBabylonJsMeshOrientation.Front,
                    //Updatable = true
                },

                new GrBabylonJsMesh.MeshProperties
                {
                    IsVisible = false,
                    Position = Float64Vector3D.Zero
                }
            );

            PrototypeCone = SceneObject.AddCylinder(
                $"prototypeCone",

                new GrBabylonJsCylinder.CylinderOptions
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

                new GrBabylonJsMesh.MeshProperties
                {
                    IsVisible = false,
                    Position = Float64Vector3D.Zero
                }
            );
        }

        public override IGrVisualElementMaterial3D AddOrGetColorMaterial(Color color)
        {
            var key = color.ToHex();

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


        public override GrVisualLaTeXText3D AddLaTeXText(GrVisualLaTeXText3D visualElement)
        {
            if (!visualElement.ImageCache.ContainsKey(visualElement.Key))
                return visualElement;

            var latexImage =
                visualElement.GetImageData();

            var latexImageString =
                latexImage.GetUrl();

            SceneObject.AddTexture(
                $"{visualElement.Name}Texture",

                latexImageString,

                new GrBabylonJsTexture.TextureProperties
                {
                    HasAlpha = true,
                    UScale = -1
                }
            );

            SceneObject.AddStandardMaterial(
                $"{visualElement.Name}Material",

                new GrBabylonJsStandardMaterial.StandardMaterialProperties
                {
                    DiffuseTexture = $"{visualElement.Name}Texture",
                    UseAlphaFromDiffuseTexture = true,
                    BackFaceCulling = true,
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
                    Position = visualElement.Position.GetBabylonJsCode(),
                    Visibility = visualElement.Visibility
                    //AlphaIndex = int.MaxValue
                }
            );

            if (visualElement.IsAnimated)
                AddLaTeXTextAnimation(visualElement);

            return visualElement;
        }

        private void AddLaTeXTextAnimation(GrVisualLaTeXText3D visualElement)
        {
            var animationSpecs = new GrBabylonJsAnimationSpecs(visualElement)
            {
                LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
                EnableBlending = false,
                Loop = true
            };

            var keyPositions = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            
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
                    animationSpecs,
                    keyPositions
                );
            
            var animationGroup = SceneObject.AddAnimationGroup(
                visualElement.Name + "Animation",
                new GrBabylonJsAnimationGroup.AnimationGroupProperties()
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


        public override GrVisualXzSquareGrid3D AddXzSquareGrid(GrVisualXzSquareGrid3D visualElement)
        {
            if (GridMaterialKind == GrBabylonJsGridMaterialKind.TexturedMaterial)
            {
                var gridImage =
                    visualElement.GetImage();

                //gridImageString.Save(
                //    $"{visualElement.Name}.png"
                //);

                var gridImageString =
                    gridImage.PngToHtmlDataUrlBase64();

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
                               Float64Vector3D.Create(0.5d * visualElement.SizeX,
                                   0,
                                   0.5d * visualElement.SizeZ)
                }
            );

            return visualElement;
        }

        public override IGrVisualImage3D AddImage(IGrVisualImage3D visualElement)
        {
            switch (visualElement)
            {
                case GrVisualLaTeXText3D latexTextImage:
                    AddLaTeXText(latexTextImage);

                    return visualElement;

                case GrVisualXzSquareGrid3D xzSquareGridImage:
                    AddXzSquareGrid(xzSquareGridImage);

                    return visualElement;
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

                new GrBabylonJsTexture.TextureProperties
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
                new GrBabylonJsMesh.MeshProperties
                {
                    IsVisible = true,
                    Material = visualElement.Style.Material.MaterialName,
                    Visibility = visualElement.Visibility,
                    Position = visualElement.Position.ToBabylonJsVector3Value(),
                    Scaling = Float64Vector3D.CreateSymmetricVector(visualElement.Style.Thickness)
                }
            );
            
            if (visualElement.IsAnimated)
                AddPointAnimation(visualElement);

            return visualElement;
        }
        
        private void AddPointAnimation(GrVisualPoint3D visualElement)
        {
            var animationSpecs = new GrBabylonJsAnimationSpecs(visualElement)
            {
                LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
                EnableBlending = false,
                Loop = true
            };

            var keyPositions = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            
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
                    animationSpecs,
                    keyPositions
                );
            
            var animationGroup = SceneObject.AddAnimationGroup(
                visualElement.Name + "Animation",
                new GrBabylonJsAnimationGroup.AnimationGroupProperties()
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
                LinUnitBasisVector3D.PositiveY.CreateAxisToVectorRotationQuaternion(unitDirection);
            
            SceneObject.AddClone(
                $"{visualElement.Name}Cylinder",
                PrototypeCone.ConstName,
                new GrBabylonJsMesh.MeshProperties
                {
                    IsVisible = true,
                    Material = visualElement.Style.Material.MaterialName,
                    Visibility = visualElement.Visibility,
                    Position = visualElement.Position - unitDirection * cylinderHeight / 2d,
                    RotationQuaternion = quaternion,
                    Scaling = Float64Vector3D.Create(cylinderBaseDiameter / 2d,
                        cylinderHeight,
                        cylinderBaseDiameter / 2d)
                }
            );
            
            if (visualElement.IsAnimated)
                AddArrowHeadAnimation(visualElement);

            return visualElement;
        }
        
        private void AddArrowHeadAnimation(GrVisualArrowHead3D visualElement)
        {
            var animationSpecs = new GrBabylonJsAnimationSpecs(visualElement)
            {
                LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
                EnableBlending = false,
                Loop = true
            };
            
            var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
            var keyPositions = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            var keyScalings = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            var keyQuaternions = new GrBabylonJsKeyFrameDictionary<Float64Quaternion>();
            
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
                    LinUnitBasisVector3D.PositiveY.CreateAxisToVectorRotationQuaternion(unitDirection);
                
                var position = 
                    origin - unitDirection * cylinderHeight / 2d;

                //var scalingFactor = 
                //    cylinderHeight <= maxHeight 
                //        ? 1d : maxHeight / cylinderHeight;

                var scaling = Float64Vector3D.Create(cylinderBaseDiameter / 2d,
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
                    animationSpecs,
                    keyVisibility
                );

            var positionAnimations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "CylinderPosition",
                    animationSpecs,
                    keyPositions
                );
            
            var scalingAnimations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "CylinderScaling",
                    animationSpecs,
                    keyScalings
                );

            var quaternionAnimation =
                SceneObject.AddQuaternionAnimation(
                    visualElement.Name + "Quaternion",
                    "rotationQuaternion",
                    animationSpecs,
                    keyQuaternions.OptimizeQuaternionKeyFrames()
                );
            
            SceneObject
                .AddAnimationGroup(
                    visualElement.Name + "Animation",
                    new GrBabylonJsAnimationGroup.AnimationGroupProperties()
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
                var lineArrayList = new List<Float64Vector3D[]>(visualElement.ParameterValues.Count);
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
                    var xPoint = origin + length * frame.Tangent.ToUnitVector();
                    var yPoint = origin + length * frame.Normal1.ToUnitVector();
                    var zPoint = origin + length * frame.Normal2.ToUnitVector();

                    lineArrayList.Add(new[] { origin, xPoint });
                    lineArrayList.Add(new[] { origin, yPoint });
                    lineArrayList.Add(new[] { origin, zPoint });

                    colorArrayList.Add(new[] { xColor1, xColor2 });
                    colorArrayList.Add(new[] { yColor1, yColor2 });
                    colorArrayList.Add(new[] { zColor1, zColor2 });
                }

                SceneObject.AddLineSystem(
                    $"{visualElement.Name}FrameLines",
                    new GrBabylonJsLineSystem.LineSystemOptions
                    {
                        Lines = lineArrayList.ToArray(),
                        Colors = colorArrayList.ToArray()
                    }
                );
            }
            
            var pointListCode = 
                visualElement
                    .ParameterValues
                    .Select(visualElement.Curve.GetPoint)
                    .GetBabylonJsCode();

            SceneObject.AddFreeCode(
                $"const {visualElement.Name}Path = {pointListCode};"
            );

            if (visualElement.Style is GrVisualCurveTubeStyle3D tubeStyle)
            {
                SceneObject.AddTube(
                    $"{visualElement.Name}Tube",

                    new GrBabylonJsTube.TubeOptions
                    {
                        Radius = tubeStyle.Thickness / 2d,
                        Path = $"{visualElement.Name}Path",
                        Tessellation = 32
                    },

                    new GrBabylonJsMesh.MeshProperties
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

                    new GrBabylonJsLines.LinesOptions
                    {
                        Points = $"{visualElement.Name}Path"
                    },

                    new GrBabylonJsLinesMesh.LinesMeshProperties
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
                var quaternion = LinUnitBasisVector3D.PositiveX.CreateAxisPairToVectorPairRotationQuaternion(
                    LinUnitBasisVector3D.PositiveY,
                    visualElement.Direction1,
                    visualElement.Direction2
                );

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

                    new GrBabylonJsTube.TubeOptions
                    {
                        Path = pathCode,
                        Radius = tubeStyle.Thickness / 2d,
                        Tessellation = 32,
                        Cap = GrBabylonJsMeshCap.StartAndEnd
                    },

                    new GrBabylonJsMesh.MeshProperties
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

                    new GrBabylonJsLines.LinesOptions
                    {
                        Points = pathCode
                    },

                    new GrBabylonJsLinesMesh.LinesMeshProperties
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

                    new GrBabylonJsLines.LinesOptions
                    {
                        Points = pathCode
                    },

                    new GrBabylonJsLinesMesh.LinesMeshProperties
                    {
                        Color = dashedLineStyle.Color,
                        Visibility = visualElement.Visibility,
                    }
                );

                return visualElement;
            }

            throw new ArgumentOutOfRangeException();
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

                new GrBabylonJsTube.TubeOptions
                {
                    Radius = tubeStyle.Thickness / 2d,
                    Path = $"{visualElement.Name}Points",
                    Tessellation = 32,
                    Cap = GrBabylonJsMeshCap.StartAndEnd,
                    Updatable = visualElement.IsAnimated
                },

                new GrBabylonJsMesh.MeshProperties
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

                new GrBabylonJsLines.LinesOptions
                {
                    Points = $"{visualElement.Name}Points",
                    Updatable = visualElement.IsAnimated
                },

                new GrBabylonJsLinesMesh.LinesMeshProperties
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

                new GrBabylonJsDashedLines.DashedLinesOptions
                {
                    Points = $"{visualElement.Name}Points",
                    DashNumber = pointPath.Count * dashedLineStyle.DashPerLine,
                    DashSize = dashedLineStyle.DashOn,
                    GapSize = dashedLineStyle.DashOff,
                    Updatable = visualElement.IsAnimated
                },

                new GrBabylonJsLinesMesh.LinesMeshProperties
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

            var animationSpecs = new GrBabylonJsAnimationSpecs(visualElement)
            {
                LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
                EnableBlending = false,
                Loop = true
            };

            var pointCount = visualElement.PathPointCount;
            
            var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
            var keyPositionsList = 
                pointCount.GetRange(
                    _ => new GrBabylonJsKeyFrameDictionary<Float64Vector3D>()
                ).ToArray();

            var positionAnimationsList = 
                new Triplet<GrBabylonJsAnimationOfFloat>[pointCount];

            foreach (var (frameIndex, _, visibility, pointsPath) in visualElement.GetKeyPointsPathRecords())
            {
                keyVisibility.SetKeyFrameValue(frameIndex, visibility);

                for (var i = 0; i < pointCount; i++)
                    keyPositionsList[i].SetKeyFrameValue(
                        frameIndex,
                        pointsPath[i].ToVector3D()
                    );
            }

            var visibilityAnimation =
                SceneObject.AddFloat32Animation(
                    $"{visualElement.Name}Visibility",
                    "visibility",
                    animationSpecs,
                    keyVisibility
                );

            for (var i = 0; i < pointCount; i++)
                positionAnimationsList[i] =
                    SceneObject.AddVector3ComponentAnimations(
                        $"{visualElement.Name}Points{i}",
                        animationSpecs,
                        keyPositionsList[i]
                    );

            var animationGroup = SceneObject.AddAnimationGroup(
                visualElement.Name + "Animation",
                new GrBabylonJsAnimationGroup.AnimationGroupProperties()
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

            var animationSpecs = new GrBabylonJsAnimationSpecs(visualElement)
            {
                LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
                EnableBlending = false,
                Loop = true
            };

            var pointCount = visualElement.PathPointCount;
            
            var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
            var keyPositionsList = 
                pointCount.GetRange(
                    _ => new GrBabylonJsKeyFrameDictionary<Float64Vector3D>()
                ).ToArray();

            var positionAnimationsList = 
                new Triplet<GrBabylonJsAnimationOfFloat>[pointCount];

            foreach (var (frameIndex, _, visibility, pointsPath) in visualElement.GetKeyPointsPathRecords())
            {
                keyVisibility.SetKeyFrameValue(frameIndex, visibility);

                for (var i = 0; i < pointCount; i++)
                    keyPositionsList[i].SetKeyFrameValue(
                        frameIndex,
                        pointsPath[i].ToVector3D()
                    );
            }

            var visibilityAnimation =
                SceneObject.AddFloat32Animation(
                    $"{visualElement.Name}Visibility",
                    "visibility",
                    animationSpecs,
                    keyVisibility
                );

            for (var i = 0; i < pointCount; i++)
                positionAnimationsList[i] =
                    SceneObject.AddVector3ComponentAnimations(
                        $"{visualElement.Name}Points{i}",
                        animationSpecs,
                        keyPositionsList[i]
                    );

            var animationGroup = SceneObject.AddAnimationGroup(
                visualElement.Name + "Animation",
                new GrBabylonJsAnimationGroup.AnimationGroupProperties()
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

            var animationSpecs = new GrBabylonJsAnimationSpecs(visualElement)
            {
                LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
                EnableBlending = false,
                Loop = true
            };

            var pointCount = visualElement.PathPointCount;
            
            var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
            var keyPositionsList = 
                pointCount.GetRange(
                    _ => new GrBabylonJsKeyFrameDictionary<Float64Vector3D>()
                ).ToArray();

            var positionAnimationsList = 
                new Triplet<GrBabylonJsAnimationOfFloat>[pointCount];

            foreach (var (frameIndex, _, visibility, pointsPath) in visualElement.GetKeyPointsPathRecords())
            {
                keyVisibility.SetKeyFrameValue(frameIndex, visibility);

                for (var i = 0; i < pointCount; i++)
                    keyPositionsList[i].SetKeyFrameValue(
                        frameIndex,
                        pointsPath[i].ToVector3D()
                    );
            }

            var visibilityAnimation =
                SceneObject.AddFloat32Animation(
                    $"{visualElement.Name}Visibility",
                    "visibility",
                    animationSpecs,
                    keyVisibility
                );

            for (var i = 0; i < pointCount; i++)
                positionAnimationsList[i] =
                    SceneObject.AddVector3ComponentAnimations(
                        $"{visualElement.Name}Points{i}",
                        animationSpecs,
                        keyPositionsList[i]
                    );

            var animationGroup = SceneObject.AddAnimationGroup(
                visualElement.Name + "Animation",
                new GrBabylonJsAnimationGroup.AnimationGroupProperties()
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

            //var quaternion = Axis3D.PositiveZ.CreateAxisToVectorRotationQuaternion(
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
                
                new GrBabylonJsRibbon.RibbonOptions
                {
                    CloseArray = false,
                    ClosePath = false,
                    PathArray = $"{visualElement.Name}RibbonPathArray",
                    SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack,
                    Updatable = visualElement.IsAnimated
                },

                new GrBabylonJsMesh.MeshProperties
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

            //var animationSpecs = new GrBabylonJsAnimationSpecs(visualElement)
            //{
            //    LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
            //    EnableBlending = false,
            //    Loop = true
            //};

            //var frameRate = animationSpecs.FrameRate;
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
            //            animationSpecs,
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
            var animationSpecs = new GrBabylonJsAnimationSpecs(visualElement)
            {
                LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
                EnableBlending = false,
                Loop = true
            };
            
            var keyFrameRecords =
                visualElement.GetKeyFrameRecords();

            var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();

            var keyPositions1 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            var keyPositions2 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            var keyPositions3 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();

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
                    animationSpecs,
                    keyVisibility
                );

            var position1Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Position1",
                    animationSpecs,
                    keyPositions1
                );
            
            var position2Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Position2",
                    animationSpecs,
                    keyPositions2
                );
            
            var position3Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Position3",
                    animationSpecs,
                    keyPositions3
                );
            
            SceneObject.AddAnimationGroup(
                visualElement.Name + "Animation",
                new GrBabylonJsAnimationGroup.AnimationGroupProperties()
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
            var path0Point1 = visualElement.Position.Add(visualElement.Direction1).GetBabylonJsCode();

            var path1Point0 = visualElement.Position.Add(visualElement.Direction2).GetBabylonJsCode();
            var path1Point1 = visualElement.Position.Add(visualElement.Direction2, visualElement.Direction1).GetBabylonJsCode();
            
            SceneObject.AddFreeCode(
                $"const {visualElement.Name}RibbonPathArray = [[{path0Point0}, {path0Point1}], [{path1Point0}, {path1Point1}]];"
            );
            
            SceneObject.AddRibbon(
                $"{visualElement.Name}Ribbon",
                
                new GrBabylonJsRibbon.RibbonOptions
                {
                    CloseArray = false,
                    ClosePath = false,
                    PathArray = $"{visualElement.Name}RibbonPathArray",
                    SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack,
                    Updatable = visualElement.IsAnimated
                },

                new GrBabylonJsMesh.MeshProperties
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

            //var animationSpecs = new GrBabylonJsAnimationSpecs(visualElement)
            //{
            //    LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
            //    EnableBlending = false,
            //    Loop = true
            //};

            //var frameRate = animationSpecs.FrameRate;
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
            //            animationSpecs,
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
            var animationSpecs = new GrBabylonJsAnimationSpecs(visualElement)
            {
                LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
                EnableBlending = false,
                Loop = true
            };
            
            var keyFrameRecords =
                visualElement.GetKeyFrameRecords();

            var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
            var path0KeyPositions0 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            var path0KeyPositions1 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            var path1KeyPositions0 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            var path1KeyPositions1 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();

            foreach (var (frameIndex, _, visibility, position, direction1, direction2) in keyFrameRecords)
            {
                var path0Position0 = position;
                var path0Position1 = position.Add(direction1);
                var path1Position0 = position.Add(direction2);
                var path1Position1 = position.Add(direction2, direction1);

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
                    animationSpecs,
                    keyVisibility
                );

            var path0Position0Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "RibbonPathArray0_0",
                    animationSpecs,
                    path0KeyPositions0
                );
            
            var path0Position1Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "RibbonPathArray0_1",
                    animationSpecs,
                    path0KeyPositions1
                );
            
            var path1Position0Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "RibbonPathArray1_0",
                    animationSpecs,
                    path1KeyPositions0
                );
            
            var path1Position1Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "RibbonPathArray1_1",
                    animationSpecs,
                    path1KeyPositions1
                );
            
            SceneObject.AddAnimationGroup(
                visualElement.Name + "Animation",
                new GrBabylonJsAnimationGroup.AnimationGroupProperties()
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
                
                new GrBabylonJsRibbon.RibbonOptions
                {
                    CloseArray = true,
                    ClosePath = false,
                    PathArray = $"{visualElement.Name}Ribbon1PathArray",
                    SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack,
                    Updatable = visualElement.IsAnimated
                },

                new GrBabylonJsMesh.MeshProperties
                {
                    Material = visualElement.Style.Material.MaterialName,
                    Visibility = visualElement.Visibility
                }
            );
            
            SceneObject.AddRibbon(
                $"{visualElement.Name}Ribbon2",
                
                new GrBabylonJsRibbon.RibbonOptions
                {
                    CloseArray = false,
                    ClosePath = false,
                    PathArray = $"{visualElement.Name}Ribbon2PathArray",
                    SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack,
                    Updatable = visualElement.IsAnimated
                },

                new GrBabylonJsMesh.MeshProperties
                {
                    Material = visualElement.Style.Material.MaterialName,
                    Visibility = visualElement.Visibility
                }
            );
            
            SceneObject.AddRibbon(
                $"{visualElement.Name}Ribbon3",
                
                new GrBabylonJsRibbon.RibbonOptions
                {
                    CloseArray = false,
                    ClosePath = false,
                    PathArray = $"{visualElement.Name}Ribbon3PathArray",
                    SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack,
                    Updatable = visualElement.IsAnimated
                },

                new GrBabylonJsMesh.MeshProperties
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

            //var animationSpecs = new GrBabylonJsAnimationSpecs(visualElement)
            //{
            //    LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
            //    EnableBlending = false,
            //    Loop = true
            //};

            //var frameRate = animationSpecs.FrameRate;
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
            //            animationSpecs,
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

            var animationSpecs = new GrBabylonJsAnimationSpecs(visualElement)
            {
                LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
                EnableBlending = false,
                Loop = true
            };
            
            var keyFrameRecords =
                visualElement.GetKeyFrameRecords();

            var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
            
            var path0KeyPositions0 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            var path0KeyPositions1 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            
            var path1KeyPositions0 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            var path1KeyPositions1 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            
            var path2KeyPositions0 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            var path2KeyPositions1 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();

            var path3KeyPositions0 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            var path3KeyPositions1 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            
            var path4KeyPositions0 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            var path4KeyPositions1 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            
            var path5KeyPositions0 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            var path5KeyPositions1 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            
            var path6KeyPositions0 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            var path6KeyPositions1 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();

            var path7KeyPositions0 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            var path7KeyPositions1 = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();

            foreach (var (frameIndex, _, visibility, position, direction1, direction2, direction3) in keyFrameRecords)
            {
                var position000 = position;
                
                var position001 = position.Add(direction1);
                var position010 = position.Add(direction2);
                var position100 = position.Add(direction3);
                
                var position011 = position001.Add(direction2);
                var position101 = position100.Add(direction1);
                var position110 = position010.Add(direction3);

                var position111 = position110.Add(direction1);
                
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
                    animationSpecs,
                    keyVisibility
                );

            // Ribbon 1
            var path0Position0Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Ribbon1PathArray0_0",
                    animationSpecs,
                    path0KeyPositions0
                );
            
            var path0Position1Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Ribbon1PathArray0_1",
                    animationSpecs,
                    path0KeyPositions1
                );
            
            var path1Position0Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Ribbon1PathArray1_0",
                    animationSpecs,
                    path1KeyPositions0
                );
            
            var path1Position1Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Ribbon1PathArray1_1",
                    animationSpecs,
                    path1KeyPositions1
                );
            
            var path2Position0Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Ribbon1PathArray2_0",
                    animationSpecs,
                    path2KeyPositions0
                );
            
            var path2Position1Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Ribbon1PathArray2_1",
                    animationSpecs,
                    path2KeyPositions1
                );
            
            var path3Position0Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Ribbon1PathArray3_0",
                    animationSpecs,
                    path3KeyPositions0
                );
            
            var path3Position1Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Ribbon1PathArray3_1",
                    animationSpecs,
                    path3KeyPositions1
                );
            
            // Ribbon 2
            var path4Position0Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Ribbon2PathArray0_0",
                    animationSpecs,
                    path4KeyPositions0
                );

            var path4Position1Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Ribbon2PathArray0_1",
                    animationSpecs,
                    path4KeyPositions1
                );
            
            var path5Position0Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Ribbon2PathArray1_0",
                    animationSpecs,
                    path5KeyPositions0
                );

            var path5Position1Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Ribbon2PathArray1_1",
                    animationSpecs,
                    path5KeyPositions1
                );
            
            // Ribbon 3
            var path6Position0Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Ribbon3PathArray0_0",
                    animationSpecs,
                    path6KeyPositions0
                );

            var path6Position1Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Ribbon3PathArray0_1",
                    animationSpecs,
                    path6KeyPositions1
                );
            
            var path7Position0Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Ribbon3PathArray1_0",
                    animationSpecs,
                    path7KeyPositions0
                );

            var path7Position1Animations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Ribbon3PathArray1_1",
                    animationSpecs,
                    path7KeyPositions1
                );

            SceneObject.AddAnimationGroup(
                visualElement.Name + "Animation",
                new GrBabylonJsAnimationGroup.AnimationGroupProperties()
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


        public override GrVisualSphereSurface3D AddSphereSurface(GrVisualSphereSurface3D visualElement)
        {
            return visualElement.Style switch
            {
                GrVisualSurfaceThickStyle3D thickStyle => 
                    AddSphereSurface(visualElement, thickStyle),

                GrVisualSurfaceThinStyle3D thinStyle => 
                    AddSphereSurface(visualElement, thinStyle),

                _ => throw new InvalidOperationException()
            };
        }

        private GrVisualSphereSurface3D AddSphereSurface(GrVisualSphereSurface3D visualElement, GrVisualSurfaceThickStyle3D thickStyle)
        {
            var outerRadius = visualElement.Radius + thickStyle.Thickness * 0.25d;
            var innerRadius = visualElement.Radius - thickStyle.Thickness * 0.25d;

            SceneObject.AddClone(
                $"{visualElement.Name}OuterSphere",
                PrototypeOuterSphere.ConstName,
                new GrBabylonJsMesh.MeshProperties
                {
                    IsVisible = true,
                    Material = visualElement.Style.Material.MaterialName,
                    Visibility = visualElement.Visibility,
                    Position = visualElement.Center.ToBabylonJsVector3Value(),
                    Scaling = Float64Vector3D.CreateSymmetricVector(outerRadius)
                }
            );
                
            SceneObject.AddClone(
                $"{visualElement.Name}InnerSphere",
                PrototypeInnerSphere.ConstName,
                new GrBabylonJsMesh.MeshProperties
                {
                    IsVisible = true,
                    Material = visualElement.Style.Material.MaterialName,
                    Visibility = visualElement.Visibility,
                    Position = visualElement.Center.ToBabylonJsVector3Value(),
                    Scaling = Float64Vector3D.CreateSymmetricVector(innerRadius)
                }
            );

            if (visualElement.IsAnimated)
                AddSphereAnimation(visualElement, thickStyle);

            return visualElement;
        }

        private GrVisualSphereSurface3D AddSphereSurface(GrVisualSphereSurface3D visualElement, GrVisualSurfaceThinStyle3D thinStyle)
        {
            SceneObject.AddClone(
                $"{visualElement.Name}Sphere",
                PrototypeOuterSphere.ConstName,
                new GrBabylonJsMesh.MeshProperties
                {
                    IsVisible = true,
                    Material = visualElement.Style.Material.MaterialName,
                    Visibility = visualElement.Visibility,
                    Position = visualElement.Center.ToBabylonJsVector3Value(),
                    Scaling = Float64Vector3D.CreateSymmetricVector(visualElement.Radius)
                }
            );

            if (visualElement.IsAnimated)
                AddSphereAnimation(visualElement, thinStyle);

            return visualElement;
        }
        
        private void AddSphereAnimation(GrVisualSphereSurface3D visualElement, GrVisualSurfaceThickStyle3D thickStyle)
        {
            var animationSpecs = new GrBabylonJsAnimationSpecs(visualElement)
            {
                LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
                EnableBlending = false,
                Loop = true
            };

            var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
            var keyPositions = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            var keyOuterScaling = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            var keyInnerScaling = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();

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
                    innerRadius * Float64Vector3D.Symmetric
                );
                
                keyOuterScaling.SetKeyFrameValue(
                    frameIndex,
                    outerRadius * Float64Vector3D.Symmetric
                );
            }
            
            var visibilityAnimation =
                SceneObject.AddFloat32Animation(
                    visualElement.Name + "SphereVisibility",
                    "visibility",
                    animationSpecs,
                    keyVisibility
                );

            var positionAnimations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "SpherePosition",
                    animationSpecs,
                    keyPositions
                );
            
            var innerScalingAnimations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "InnerSphereScaling",
                    animationSpecs,
                    keyInnerScaling
                );
            
            var outerScalingAnimations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "OuterSphereScaling",
                    animationSpecs,
                    keyOuterScaling
                );

            SceneObject.AddAnimationGroup(
                visualElement.Name + "Animation",
                new GrBabylonJsAnimationGroup.AnimationGroupProperties()
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
            var animationSpecs = new GrBabylonJsAnimationSpecs(visualElement)
            {
                LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
                EnableBlending = false,
                Loop = true
            };

            var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
            var keyPositions = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            var keyScaling = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            
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
                    radius * Float64Vector3D.Symmetric
                );
            }
            
            var visibilityAnimation =
                SceneObject.AddFloat32Animation(
                    visualElement.Name + "SphereVisibility",
                    "visibility",
                    animationSpecs,
                    keyVisibility
                );

            var positionAnimations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "SpherePosition",
                    animationSpecs,
                    keyPositions
                );
            
            var scalingAnimations =
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "SphereScaling",
                    animationSpecs,
                    keyScaling
                );

            SceneObject.AddAnimationGroup(
                visualElement.Name + "Animation",
                new GrBabylonJsAnimationGroup.AnimationGroupProperties()
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


        public override GrVisualCircleSurface3D AddCircleSurface(GrVisualCircleSurface3D visualElement)
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
            var normal = visualElement.Normal.ToUnitVector();

            var quaternion = LinUnitBasisVector3D.PositiveY.CreateAxisToVectorRotationQuaternion(normal);
            
            SceneObject.AddClone(
                $"{visualElement.Name}Sphere",
                PrototypeOuterSphere.ConstName,
                new GrBabylonJsMesh.MeshProperties
                {
                    IsVisible = true,
                    Material = visualElement.Style.Material.MaterialName,
                    Visibility = visualElement.Visibility,
                    Position = visualElement.Center.ToBabylonJsVector3Value(),
                    RotationQuaternion = quaternion,
                    Scaling = Float64Vector3D.Create(visualElement.Radius,
                        thickStyle.Thickness * 0.5d,
                        visualElement.Radius)
                }
            );
            
            if (visualElement.DrawEdge)
            {
                SceneObject.AddTorus(
                    $"{visualElement.Name}EdgeTorus",

                    new GrBabylonJsTorus.TorusOptions
                    {
                        Diameter = visualElement.Radius * 2,
                        Thickness = thickStyle.Thickness,
                        Tessellation = 320
                    },

                    new GrBabylonJsMesh.MeshProperties
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
            var normal = visualElement.Normal.ToUnitVector();

            var quaternion = LinUnitBasisVector3D.PositiveZ.CreateAxisToVectorRotationQuaternion(normal);
            
            SceneObject.AddClone(
                $"{visualElement.Name}Disc",
                PrototypeDisc.ConstName,
                new GrBabylonJsMesh.MeshProperties
                {
                    IsVisible = true,
                    Material = visualElement.Style.Material.MaterialName,
                    Visibility = visualElement.Visibility,
                    Position = visualElement.Center.ToBabylonJsVector3Value(),
                    RotationQuaternion = quaternion,
                    Scaling = Float64Vector3D.Create(visualElement.Radius,
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

                    new GrBabylonJsLines.LinesOptions
                    {
                        Points = $"{pathName}.getPoints()"
                    },

                    new GrBabylonJsLinesMesh.LinesMeshProperties
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
            var animationSpecs = new GrBabylonJsAnimationSpecs(visualElement)
            {
                LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
                EnableBlending = false,
                Loop = true
            };
            
            var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
            var keyPositions = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            var keyScalings = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            var keyQuaternions = new GrBabylonJsKeyFrameDictionary<Float64Quaternion>();

            foreach (var (frameIndex, _, visibility, center, normal, radius) in visualElement.GetKeyFrameRecords())
            {
                var quaternion = LinUnitBasisVector3D.PositiveY.CreateAxisToVectorRotationQuaternion(normal.ToUnitVector());
                var scaling = Float64Vector3D.Create(radius,
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
                    animationSpecs,
                    keyVisibility
                );

            var positionAnimations = 
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Position",
                    animationSpecs,
                    keyPositions
                );
            
            var scalingAnimations = 
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Scaling",
                    animationSpecs,
                    keyScalings
                );
            
            var quaternionAnimation = 
                SceneObject.AddQuaternionAnimation(
                    visualElement.Name + "Quaternion",
                    "rotationQuaternion",
                    animationSpecs,
                    keyQuaternions.OptimizeQuaternionKeyFrames()
                );
            
            SceneObject
                .AddAnimationGroup(
                    visualElement.Name + "Animations",
                    new GrBabylonJsAnimationGroup.AnimationGroupProperties()
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
            var animationSpecs = new GrBabylonJsAnimationSpecs(visualElement)
            {
                LoopMode = GrBabylonJsAnimationLoopMode.Cycle,
                EnableBlending = false,
                Loop = true
            };
            
            var keyVisibility = new GrBabylonJsKeyFrameDictionary<double>();
            var keyPositions = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            var keyScalings = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();
            var keyQuaternions = new GrBabylonJsKeyFrameDictionary<Float64Quaternion>();

            foreach (var (frameIndex, _, visibility, center, normal, radius) in visualElement.GetKeyFrameRecords())
            {
                var quaternion = LinUnitBasisVector3D.PositiveZ.CreateAxisToVectorRotationQuaternion(normal.ToUnitVector());
                var scaling = Float64Vector3D.Create(radius,
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
                    animationSpecs,
                    keyVisibility
                );

            var positionAnimations = 
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Position",
                    animationSpecs,
                    keyPositions
                );
            
            var scalingAnimations = 
                SceneObject.AddVector3ComponentAnimations(
                    visualElement.Name + "Scaling",
                    animationSpecs,
                    keyScalings
                );
            
            var quaternionAnimation = 
                SceneObject.AddQuaternionAnimation(
                    visualElement.Name + "Quaternion",
                    "rotationQuaternion",
                    animationSpecs,
                    keyQuaternions.OptimizeQuaternionKeyFrames()
                );
            
            SceneObject
                .AddAnimationGroup(
                    visualElement.Name + "Animations",
                    new GrBabylonJsAnimationGroup.AnimationGroupProperties()
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


        public override GrVisualCircleArcSurface3D AddCircleArcSurface(GrVisualCircleArcSurface3D visualElement)
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
                LinUnitBasisVector3D.PositiveX.CreateAxisPairToVectorPairRotationQuaternion(
                    LinUnitBasisVector3D.PositiveY,
                    vector,
                    normal
                );

            SceneObject.AddSphere(
                $"{visualElement.Name}Sphere",

                new GrBabylonJsSphere.SphereOptions
                {
                    Arc = visualElement.ArcRatio,
                    DiameterX = visualElement.Radius * 2,
                    DiameterY = thickStyle.Thickness,
                    DiameterZ = visualElement.Radius * 2,
                    Segments = 320
                },

                new GrBabylonJsMesh.MeshProperties
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

                    new GrBabylonJsTube.TubeOptions
                    {
                        Path = $"{pathName}.getPoints()",
                        Radius = thickStyle.Thickness / 2d,
                        Tessellation = 320,
                        Cap = GrBabylonJsMeshCap.StartAndEnd
                    },

                    new GrBabylonJsMesh.MeshProperties
                    {
                        Material = thickStyle.Material.MaterialName,
                        Visibility = visualElement.Visibility,
                    }
                );
            }

            var qYx =
                LinUnitBasisVector3D.PositiveY.CreateAxisToAxisRotationQuaternion(LinUnitBasisVector3D.PositiveX);

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
                    Visibility = visualElement.Visibility,
                    Scaling = Float64Vector3D.Create(thickStyle.Thickness / (2d * visualElement.Radius), 1, 1),
                    Position = visualElement.Center.ToBabylonJsVector3Value(),
                    RotationQuaternion = qYx.Concatenate(quaternion)
                }
            );


            var q2 = Float64Vector3D.E2.CreateQuaternion(visualElement.Angle);

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
                    Visibility = visualElement.Visibility,
                    Scaling = Float64Vector3D.Create(thickStyle.Thickness / (2d * visualElement.Radius), 1, 1),
                    Position = visualElement.Center.ToBabylonJsVector3Value(),
                    RotationQuaternion = qYx.Concatenate(q2, quaternion)
                }
            );

            return visualElement;
        }

        private GrVisualCircleArcSurface3D AddDiscSector(GrVisualCircleArcSurface3D visualElement, GrVisualSurfaceThinStyle3D thinStyle)
        {
            var vector = visualElement.Direction1;
            var normal = visualElement.Normal;

            var quaternion =
                LinUnitBasisVector3D.PositiveX.CreateAxisPairToVectorPairRotationQuaternion(
                    LinUnitBasisVector3D.PositiveZ,
                    vector,
                    normal
                );

            SceneObject.AddDisc(
                $"{visualElement.Name}Disc",

                new GrBabylonJsDisc.DiscOptions
                {
                    Arc = visualElement.ArcRatio,
                    Radius = visualElement.Radius,
                    SideOrientation = GrBabylonJsMeshOrientation.FrontAndBack,
                    Tessellation = 320
                },

                new GrBabylonJsMesh.MeshProperties
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

                    new GrBabylonJsLines.LinesOptions
                    {
                        Points = $"{pathName}.getPoints()"
                    },

                    new GrBabylonJsLinesMesh.LinesMeshProperties
                    {
                        Color = thinStyle.EdgeColor,
                        Visibility = visualElement.Visibility,
                    }
                );
            }

            return visualElement;
        }


        public override GrVisualCircleRingSurface3D AddCircleRingSurface(GrVisualCircleRingSurface3D visualElement)
        {
            return visualElement.Style switch
            {
                GrVisualSurfaceThickStyle3D thickStyle => 
                    AddRingSurface(visualElement, thickStyle),

                GrVisualSurfaceThinStyle3D thinStyle => 
                    AddRingSurface(visualElement, thinStyle),

                _ => throw new InvalidCastException()
            };
        }

        private GrVisualCircleRingSurface3D AddRingSurface(GrVisualCircleRingSurface3D visualElement, GrVisualSurfaceThickStyle3D thickStyle)
        {
            var normal = visualElement.Normal.ToUnitVector();

            var quaternion = LinUnitBasisVector3D.PositiveY.CreateAxisToVectorRotationQuaternion(normal);

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
                    Visibility = visualElement.Visibility,
                    Position = visualElement.Center.ToBabylonJsVector3Value(),
                    Scaling = Float64Vector3D.Create(1, thickStyle.Thickness / thickness, 1),
                    RotationQuaternion = quaternion
                }
            );
            
            return visualElement;
        }

        private GrVisualCircleRingSurface3D AddRingSurface(GrVisualCircleRingSurface3D visualElement, GrVisualSurfaceThinStyle3D thinStyle)
        {
            throw new NotImplementedException();
        }


        
        public GrBabylonJsSceneComposer3D AddDefaultAxes(IFloat64Vector3D axesOrigin)
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
        
        public GrBabylonJsSceneComposer3D AddDefaultGrid(int gridUnitCount)
        {
            // Add ground coordinates grid
            GridMaterialKind =
                GrBabylonJsGridMaterialKind.TexturedMaterial;

            AddXzSquareGrid(
                new GrVisualXzSquareGrid3D("defaultGrid")
                {
                    UnitCountX = gridUnitCount,
                    UnitCountZ = gridUnitCount,
                    UnitSize = 1,
                    Origin = Float64Vector3D.Create(-0.5d * gridUnitCount, 0, -0.5d * gridUnitCount),
                    Opacity = 0.25,
                    BaseSquareColor = Color.LightYellow,
                    BaseLineColor = Color.BurlyWood,
                    MidLineColor = Color.SandyBrown,
                    BorderLineColor = Color.SaddleBrown,
                    BaseSquareCount = 4,
                    BaseSquareSize = 64,
                    BaseLineWidth = 2,
                    MidLineWidth = 4,
                    BorderLineWidth = 3
                }
            );

            return this;
        }
        
        public GrBabylonJsSceneComposer3D AddDefaultEnvironment(int gridUnitCount)
        {
            //var scene = MainSceneComposer.SceneObject;
            //scene.SceneProperties.AmbientColor = Color.AliceBlue;

            // Add scene environment
            SceneObject.AddEnvironmentHelper(
                "environmentHelper",

                new GrBabylonJsEnvironmentHelper.EnvironmentHelperOptions
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
        
        public GrBabylonJsSceneComposer3D AddDefaultPerspectiveCamera(GrBabylonJsFloat32Value cameraDistance, GrBabylonJsFloat32Value alpha, GrBabylonJsFloat32Value beta)
        {
            // Add main scene camera
            SceneObject.AddArcRotateCamera(
                "defaultCamera",
                alpha,
                beta,
                cameraDistance,
                "BABYLON.Vector3.Zero()",
                new GrBabylonJsArcRotateCamera.ArcRotateCameraProperties
                {
                    Mode = GrBabylonJsCameraMode.PerspectiveCamera
                }
            );

            return this;
        }
        
        public GrBabylonJsSceneComposer3D AddDefaultOrthographicCamera(GrBabylonJsFloat32Value cameraDistance, GrBabylonJsFloat32Value alpha, GrBabylonJsFloat32Value beta, BoundingBox2D size)
        {
            // Add main scene camera
            SceneObject.AddArcRotateCamera(
                "defaultCamera",
                alpha,
                beta,
                cameraDistance,
                "BABYLON.Vector3.Zero()",
                new GrBabylonJsArcRotateCamera.ArcRotateCameraProperties
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

            codeComposer.AppendLineAtNewLine(
                SceneObject.KeyFramesCache.GetCode()
            );

            foreach (var babylonObject in SceneObject.ObjectList)
                codeComposer.AppendLineAtNewLine(babylonObject.GetCode());

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
}