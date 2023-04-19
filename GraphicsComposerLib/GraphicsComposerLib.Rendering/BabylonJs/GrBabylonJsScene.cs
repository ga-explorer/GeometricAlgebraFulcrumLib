using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Cameras;
using GraphicsComposerLib.Rendering.BabylonJs.Constants;
using GraphicsComposerLib.Rendering.BabylonJs.Curves;
using GraphicsComposerLib.Rendering.BabylonJs.GUI;
using GraphicsComposerLib.Rendering.BabylonJs.Materials;
using GraphicsComposerLib.Rendering.BabylonJs.Meshes;
using GraphicsComposerLib.Rendering.BabylonJs.Textures;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs
{
    /// <summary>
    /// https://doc.babylonjs.com/typedoc/classes/BABYLON.Scene
    /// </summary>
    public sealed class GrBabylonJsScene :
        GrBabylonJsObject
    {
        public sealed class SceneOptions :
            GrBabylonJsObjectOptions
        {
            public GrBabylonJsBooleanValue? UseClonedMeshMap { get; set; }

            public GrBabylonJsBooleanValue? UseGeometryUniqueIdsMap { get; set; }
            
            public GrBabylonJsBooleanValue? UseMaterialMeshMap { get; set; }

            public GrBabylonJsBooleanValue? Virtual { get; set; }

            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                yield return UseClonedMeshMap.GetNameValueCodePair("useClonedMeshMap");
                yield return UseGeometryUniqueIdsMap.GetNameValueCodePair("useGeometryUniqueIdsMap");
                yield return UseMaterialMeshMap.GetNameValueCodePair("useMaterialMeshMap");
                yield return Virtual.GetNameValueCodePair("virtual");
            }
        }

        public sealed class SceneProperties :
            GrBabylonJsObjectProperties
        {
            public GrBabylonJsMaterialValue? DefaultMaterial { get; set; }

            public GrBabylonJsColor3Value? AmbientColor { get; set; }

            public GrBabylonJsColor4Value? ClearColor { get; set; }

            public GrBabylonJsBooleanValue? FogEnabled { get; set; }

            public GrBabylonJsFogModeValue? FogMode { get; set; }

            public GrBabylonJsColor3Value? FogColor { get; set; }

            public GrBabylonJsFloat32Value? FogDensity { get; set; }

            public GrBabylonJsFloat32Value? FogStart { get; set; }

            public GrBabylonJsFloat32Value? FogEnd { get; set; }

            public GrBabylonJsFloat32Value? AnimationTimeScale { get; set; }

            public GrBabylonJsBooleanValue? AnimationsEnabled { get; set; }

            public GrBabylonJsBooleanValue? AudioEnabled { get; set; }

            public GrBabylonJsFloat32Value? AudioPositioningRefreshRate { get; set; }

            public GrBabylonJsBooleanValue? Headphone { get; set; }

            public GrBabylonJsFloat32Value? DeltaTime { get; set; }

            public GrBabylonJsFloat32Value? MinDeltaTime { get; set; }

            public GrBabylonJsFloat32Value? MaxDeltaTime { get; set; }

            public GrBabylonJsBooleanValue? UseConstantAnimationDeltaTime { get; set; }

            public GrBabylonJsFloat32Value? PointerX { get; set; }

            public GrBabylonJsFloat32Value? PointerY { get; set; }

            public GrBabylonJsBooleanValue? UseOrderIndependentTransparency { get; set; }

            public GrBabylonJsBooleanValue? AutoClear { get; set; }

            public GrBabylonJsBooleanValue? AutoClearDepthAndStencil { get; set; }

            public GrBabylonJsBooleanValue? PhysicsEnabled { get; set; }

            public GrBabylonJsBooleanValue? ParticlesEnabled { get; set; }

            public GrBabylonJsBooleanValue? CollisionsEnabled { get; set; }

            public GrBabylonJsVector3Value? Gravity { get; set; }

            public GrBabylonJsBooleanValue? ConstantlyUpdateMeshUnderPointer { get; set; }

            public GrBabylonJsBooleanValue? ForceShowBoundingBoxes { get; set; }

            public GrBabylonJsBooleanValue? ForcePointsCloud { get; set; }

            public GrBabylonJsBooleanValue? ForceWireFrame { get; set; }

            public GrBabylonJsBooleanValue? LightsEnabled { get; set; }

            public GrBabylonJsBooleanValue? ShadowsEnabled { get; set; }

            public GrBabylonJsBooleanValue? SkeletonsEnabled { get; set; }

            public GrBabylonJsBooleanValue? SkipFrustumClipping { get; set; }

            public GrBabylonJsBooleanValue? TexturesEnabled { get; set; }

            public GrBabylonJsBooleanValue? UseRightHandedSystem { get; set; } 
                = true;

            public GrBabylonJsBooleanValue? RequireLightSorting { get; set; }

            public GrBabylonJsBooleanValue? SpritesEnabled { get; set; }


            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                yield return DefaultMaterial.GetNameValueCodePair("defaultMaterial");
                yield return AmbientColor.GetNameValueCodePair("ambientColor");
                yield return ClearColor.GetNameValueCodePair("clearColor");
                yield return FogEnabled.GetNameValueCodePair("fogEnabled");
                yield return FogMode.GetNameValueCodePair("fogMode");
                yield return FogColor.GetNameValueCodePair("fogColor");
                yield return FogDensity.GetNameValueCodePair("fogDensity");
                yield return FogStart.GetNameValueCodePair("fogStart");
                yield return FogEnd.GetNameValueCodePair("fogEnd");
                yield return AnimationTimeScale.GetNameValueCodePair("animationTimeScale");
                yield return AnimationsEnabled.GetNameValueCodePair("animationsEnabled");
                yield return AudioEnabled.GetNameValueCodePair("audioEnabled");
                yield return AudioPositioningRefreshRate.GetNameValueCodePair("audioPositioningRefreshRate");
                yield return Headphone.GetNameValueCodePair("headphone");
                yield return DeltaTime.GetNameValueCodePair("deltaTime");
                yield return MinDeltaTime.GetNameValueCodePair("MinDeltaTime");
                yield return MaxDeltaTime.GetNameValueCodePair("MaxDeltaTime");
                yield return UseConstantAnimationDeltaTime.GetNameValueCodePair("useConstantAnimationDeltaTime");
                yield return PointerX.GetNameValueCodePair("pointerX");
                yield return PointerY.GetNameValueCodePair("pointerY");
                yield return UseOrderIndependentTransparency.GetNameValueCodePair("useOrderIndependentTransparency");
                yield return AutoClear.GetNameValueCodePair("autoClear");
                yield return AutoClearDepthAndStencil.GetNameValueCodePair("autoClearDepthAndStencil");
                yield return PhysicsEnabled.GetNameValueCodePair("physicsEnabled");
                yield return ParticlesEnabled.GetNameValueCodePair("particlesEnabled");
                yield return CollisionsEnabled.GetNameValueCodePair("collisionsEnabled");
                yield return Gravity.GetNameValueCodePair("gravity");
                yield return ConstantlyUpdateMeshUnderPointer.GetNameValueCodePair("constantlyUpdateMeshUnderPointer");
                yield return ForceShowBoundingBoxes.GetNameValueCodePair("forceShowBoundingBoxes");
                yield return ForcePointsCloud.GetNameValueCodePair("forcePointsCloud");
                yield return ForceWireFrame.GetNameValueCodePair("forceWireframe");
                yield return LightsEnabled.GetNameValueCodePair("lightsEnabled");
                yield return ShadowsEnabled.GetNameValueCodePair("shadowsEnabled");
                yield return SkeletonsEnabled.GetNameValueCodePair("skeletonsEnabled");
                yield return SkipFrustumClipping.GetNameValueCodePair("skipFrustumClipping");
                yield return TexturesEnabled.GetNameValueCodePair("texturesEnabled");
                yield return UseRightHandedSystem.GetNameValueCodePair("useRightHandedSystem");
                yield return RequireLightSorting.GetNameValueCodePair("requireLightSorting");
                yield return SpritesEnabled.GetNameValueCodePair("spritesEnabled");
            }
        }


        protected override string ConstructorName 
            => "new BABYLON.Scene";

        public SceneOptions? Options { get; private set; }
            = new SceneOptions();

        public SceneProperties? Properties { get; private set; }
            = new SceneProperties();

        public override GrBabylonJsObjectOptions? ObjectOptions 
            => Options;

        public override GrBabylonJsObjectProperties? ObjectProperties 
            => Properties;

        public GrBabylonJsCodeValue Engine { get; set; } 
            = "engine";

        public GrBabylonJsObjectList ObjectList { get; } 
            = new GrBabylonJsObjectList();

        public IEnumerable<GrBabylonJsMaterial> Materials
            => ObjectList
                .Where(s => s is GrBabylonJsMaterial)
                .Cast<GrBabylonJsMaterial>();
        
        public IEnumerable<GrBabylonJsCurve3> Curves
            => ObjectList
                .Where(s => s is GrBabylonJsCurve3)
                .Cast<GrBabylonJsCurve3>();
        
        public IEnumerable<GrBabylonJsMesh> Meshes
            => ObjectList
                .Where(s => s is GrBabylonJsMesh)
                .Cast<GrBabylonJsMesh>();
        
        
        public GrBabylonJsScene(string constName) 
            : base(constName)
        {
        }


        public GrBabylonJsScene SetOptions([NotNull] SceneOptions? options)
        {
            Options = options;

            return this;
        }

        public GrBabylonJsScene SetProperties([NotNull] SceneProperties? properties)
        {
            Properties = properties;

            return this;
        }


        public GrBabylonJsMaterial GetMaterial(string name)
        {
            if (!ObjectList.TryGetObject(name, out var sceneObject))
                throw new KeyNotFoundException();

            return sceneObject as GrBabylonJsMaterial 
                   ?? throw new KeyNotFoundException();;
        }
        
        public GrBabylonJsGuiFullScreenUi GetGuiFullScreenUi(string name)
        {
            if (!ObjectList.TryGetObject(name, out var sceneObject))
                throw new KeyNotFoundException();

            return sceneObject as GrBabylonJsGuiFullScreenUi 
                   ?? throw new KeyNotFoundException();;
        }

        public GrBabylonJsArcRotateCamera AddArcRotateCamera(string name, GrBabylonJsFloat32Value alpha, GrBabylonJsFloat32Value beta, GrBabylonJsFloat32Value radius, GrBabylonJsVector3Value target)
        {
            var sceneObject = 
                new GrBabylonJsArcRotateCamera(name, this)
                {
                    Alpha = alpha,
                    Beta = beta,
                    Radius = radius,
                    Target = target,
                    SetActiveOnSceneIfNoneActive = true
                };

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsArcRotateCamera AddArcRotateCamera(string name, GrBabylonJsFloat32Value alpha, GrBabylonJsFloat32Value beta, GrBabylonJsFloat32Value radius, GrBabylonJsVector3Value target, GrBabylonJsArcRotateCamera.ArcRotateCameraProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsArcRotateCamera(name, this)
                {
                    Alpha = alpha,
                    Beta = beta,
                    Radius = radius,
                    Target = target,
                    SetActiveOnSceneIfNoneActive = true
                }.SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsUniversalCamera AddUniversalCamera(string name, GrBabylonJsVector3Value position)
        {
            var sceneObject = 
                new GrBabylonJsUniversalCamera(name, this)
                {
                    Position = position
                };

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsUniversalCamera AddUniversalCamera(string name, GrBabylonJsVector3Value position, GrBabylonJsUniversalCamera.UniversalCameraProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsUniversalCamera(name, this)
                {
                    Position = position
                }.SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsEnvironmentHelper AddEnvironmentHelper(string name)
        {
            var sceneObject = 
                new GrBabylonJsEnvironmentHelper(name, this);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsEnvironmentHelper AddEnvironmentHelper(string name, GrBabylonJsEnvironmentHelper.EnvironmentHelperOptions? options)
        {
            var sceneObject = 
                new GrBabylonJsEnvironmentHelper(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsEnvironmentHelper AddEnvironmentHelper(string name, GrBabylonJsEnvironmentHelper.EnvironmentHelperProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsEnvironmentHelper(name, this)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsEnvironmentHelper AddEnvironmentHelper(string name, GrBabylonJsEnvironmentHelper.EnvironmentHelperOptions? options, GrBabylonJsEnvironmentHelper.EnvironmentHelperProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsEnvironmentHelper(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsTexture AddTexture(string name, string url)
        {
            var sceneObject = 
                new GrBabylonJsTexture(name, this)
                {
                    Url = url
                };

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsTexture AddTexture(string name, string url, GrBabylonJsTexture.TextureOptions? options)
        {
            var sceneObject = 
                new GrBabylonJsTexture(name, this)
                {
                    Url = url
                }
                .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsTexture AddTexture(string name, string url, GrBabylonJsTexture.TextureProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsTexture(name, this)
                {
                    Url = url
                }
                .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsTexture AddTexture(string name, string url, GrBabylonJsTexture.TextureOptions? options, GrBabylonJsTexture.TextureProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsTexture(name, this)
                {
                    Url = url
                }
                .SetOptions(options)
                .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsSimpleMaterial AddSimpleMaterial(string name, GrBabylonJsSimpleMaterial.SimpleMaterialProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsSimpleMaterial(name, this)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsSimpleMaterial AddSimpleMaterial(string name, GrBabylonJsColor3Value color)
        {
            var alpha = color.Value.ToPixel<Rgba32>().A;

            if (alpha == 255)
                return AddSimpleMaterial(
                    name, 
                    new GrBabylonJsSimpleMaterial.SimpleMaterialProperties
                    {
                        DiffuseColor = color
                    }
                );

            return AddSimpleMaterial(
                name, 
                new GrBabylonJsSimpleMaterial.SimpleMaterialProperties
                {
                    DiffuseColor = color,
                    Alpha = alpha / 255f,
                    TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend,
                    BackFaceCulling = true
                }
            );
        }
        
        public GrBabylonJsStandardMaterial AddStandardMaterial(string name, GrBabylonJsStandardMaterial.StandardMaterialProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsStandardMaterial(name, this)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsStandardMaterial AddStandardMaterial(string name, GrBabylonJsColor3Value color)
        {
            var alpha = color.Value.ToPixel<Rgba32>().A;

            if (alpha == 255)
                return AddStandardMaterial(
                    name, 
                    new GrBabylonJsStandardMaterial.StandardMaterialProperties
                    {
                        Color = color
                    }
                );

            return AddStandardMaterial(
                name, 
                new GrBabylonJsStandardMaterial.StandardMaterialProperties
                {
                    Color = color,
                    Alpha = alpha / 255f,
                    TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend,
                    BackFaceCulling = true
                }
            );
        }
        
        public GrBabylonJsShadowOnlyMaterial AddShadowOnlyMaterial(string name, GrBabylonJsShadowOnlyMaterial.ShadowOnlyMaterialProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsShadowOnlyMaterial(name, this)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsOcclusionMaterial AddOcclusionMaterial(string name, GrBabylonJsOcclusionMaterial.OcclusionMaterialProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsOcclusionMaterial(name, this)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsNormalMaterial AddNormalMaterial(string name, GrBabylonJsNormalMaterial.NormalMaterialProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsNormalMaterial(name, this)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsHandleMaterial AddHandleMaterial(string name, GrBabylonJsHandleMaterial.HandleMaterialProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsHandleMaterial(name, this)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsGridMaterial AddGridMaterial(string name, GrBabylonJsGridMaterial.GridMaterialProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsGridMaterial(name, this)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsGradientMaterial AddGradientMaterial(string name, GrBabylonJsGradientMaterial.GradientMaterialProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsGradientMaterial(name, this)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsBackgroundMaterial AddBackgroundMaterial(string name, GrBabylonJsBackgroundMaterial.BackgroundMaterialProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsBackgroundMaterial(name, this)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsCurve3 AddCurve3(string name, GrBabylonJsVector3ArrayValue points)
        {
            var sceneObject = 
                new GrBabylonJsCurve3(name)
                    {
                        Points = points
                    };

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsArcThru3Points AddArcThru3Points(string name, GrBabylonJsVector3Value point1, GrBabylonJsVector3Value point2, GrBabylonJsVector3Value point3, GrBabylonJsInt32Value steps, GrBabylonJsBooleanValue closed, GrBabylonJsBooleanValue fullCircle)
        {
            var sceneObject = 
                new GrBabylonJsArcThru3Points(name)
                {
                    Point1 = point1,
                    Point2 = point2,
                    Point3 = point3,
                    Steps = steps,
                    Closed = closed,
                    FullCircle = fullCircle
                };

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsCatmullRomSpline AddCatmullRomSpline(string name, GrBabylonJsVector3ArrayValue points, GrBabylonJsVector3Value pointNumber, GrBabylonJsBooleanValue closed)
        {
            var sceneObject = 
                new GrBabylonJsCatmullRomSpline(name)
                {
                    Points = points,
                    PointNumber = pointNumber,
                    Closed = closed
                };

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsCubicBezier AddQuadraticBezier(string name, GrBabylonJsVector3Value point1, GrBabylonJsVector3Value point2, GrBabylonJsVector3Value point3, GrBabylonJsVector3Value pointNumber)
        {
            var sceneObject = 
                new GrBabylonJsCubicBezier(name)
                {
                    Point1 = point1,
                    Point2 = point2,
                    Point3 = point3,
                    PointNumber = pointNumber
                };

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsCubicBezier AddCubicBezier(string name, GrBabylonJsVector3Value point1, GrBabylonJsVector3Value point2, GrBabylonJsVector3Value point3, GrBabylonJsVector3Value point4, GrBabylonJsVector3Value pointNumber)
        {
            var sceneObject = 
                new GrBabylonJsCubicBezier(name)
                {
                    Point1 = point1,
                    Point2 = point2,
                    Point3 = point3,
                    Point4 = point4,
                    PointNumber = pointNumber
                };

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsHermiteSpline AddHermiteSpline(string name, GrBabylonJsVector3Value point1, GrBabylonJsVector3Value point2, GrBabylonJsVector3Value tangent1, GrBabylonJsVector3Value tangent2, GrBabylonJsVector3Value pointNumber)
        {
            var sceneObject = 
                new GrBabylonJsHermiteSpline(name)
                {
                    Point1 = point1,
                    Point2 = point2,
                    Tangent1 = tangent1,
                    Tangent2 = tangent2,
                    PointNumber = pointNumber
                };

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsCapsule AddCapsule(string name, GrBabylonJsCapsule.CapsuleOptions? options)
        {
            var sceneObject = 
                new GrBabylonJsCapsule(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsCapsule AddCapsule(string name, GrBabylonJsCapsule.CapsuleOptions? options, GrBabylonJsMesh.MeshProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsCapsule(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsCylinder AddCylinder(string name, GrBabylonJsCylinder.CylinderOptions? options)
        {
            var sceneObject = 
                new GrBabylonJsCylinder(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsCylinder AddCylinder(string name, GrBabylonJsCylinder.CylinderOptions? options, GrBabylonJsMesh.MeshProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsCylinder(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsDisc AddDisc(string name, GrBabylonJsDisc.DiscOptions? options)
        {
            var sceneObject = 
                new GrBabylonJsDisc(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsDisc AddDisc(string name, GrBabylonJsDisc.DiscOptions? options, GrBabylonJsMesh.MeshProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsDisc(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsGround AddGround(string name, GrBabylonJsGround.GroundOptions? options)
        {
            var sceneObject = 
                new GrBabylonJsGround(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsGround AddGround(string name, GrBabylonJsGround.GroundOptions? options, GrBabylonJsMesh.MeshProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsGround(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsPlane AddPlane(string name, GrBabylonJsPlane.PlaneOptions? options)
        {
            var sceneObject = 
                new GrBabylonJsPlane(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsPlane AddPlane(string name, GrBabylonJsPlane.PlaneOptions? options, GrBabylonJsMesh.MeshProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsPlane(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsBox AddBox(string name, GrBabylonJsBox.BoxOptions? options)
        {
            var sceneObject =
                new GrBabylonJsBox(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsBox AddBox(string name, GrBabylonJsBox.BoxOptions? options, GrBabylonJsMesh.MeshProperties? properties)
        {
            var sceneObject =
                new GrBabylonJsBox(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsSphere AddSphere(string name, GrBabylonJsSphere.SphereOptions? options)
        {
            var sceneObject = 
                new GrBabylonJsSphere(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsSphere AddSphere(string name, GrBabylonJsSphere.SphereOptions? options, GrBabylonJsMesh.MeshProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsSphere(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsTorus AddTorus(string name, GrBabylonJsTorus.TorusOptions? options)
        {
            var sceneObject = 
                new GrBabylonJsTorus(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsTorus AddTorus(string name, GrBabylonJsTorus.TorusOptions? options, GrBabylonJsMesh.MeshProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsTorus(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsTube AddTube(string name, GrBabylonJsTube.TubeOptions? options)
        {
            var sceneObject = 
                new GrBabylonJsTube(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsTube AddTube(string name, GrBabylonJsTube.TubeOptions? options, GrBabylonJsMesh.MeshProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsTube(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsLines AddLines(string name, GrBabylonJsLines.LinesOptions? options)
        {
            var sceneObject = 
                new GrBabylonJsLines(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsLines AddLines(string name, GrBabylonJsLines.LinesOptions options, GrBabylonJsLinesMesh.LinesMeshProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsLines(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsDashedLines AddDashedLines(string name, GrBabylonJsDashedLines.DashedLinesOptions? options)
        {
            var sceneObject = 
                new GrBabylonJsDashedLines(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsDashedLines AddDashedLines(string name, GrBabylonJsDashedLines.DashedLinesOptions? options, GrBabylonJsLinesMesh.LinesMeshProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsDashedLines(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsLineSystem AddLineSystem(string name, GrBabylonJsLineSystem.LineSystemOptions options)
        {
            var sceneObject = 
                new GrBabylonJsLineSystem(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsLineSystem AddLineSystem(string name, GrBabylonJsLineSystem.LineSystemOptions options, GrBabylonJsLinesMesh.LinesMeshProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsLineSystem(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsGuiFullScreenUi AddGuiFullScreenUi(string name, GrBabylonJsBooleanValue isForeground = null, GrBabylonJsTextureSamplingModeValue samplingMode = null, GrBabylonJsBooleanValue adaptiveScaling = null)
        {
            var sceneObject = 
                new GrBabylonJsGuiFullScreenUi(name, this)
                {
                    IsForeground = isForeground,
                    SamplingMode = samplingMode,
                    AdaptiveScaling = adaptiveScaling
                };

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsGuiFullScreenUi AddGuiFullScreenUi(string name, GrBabylonJsBooleanValue isForeground, GrBabylonJsTextureSamplingMode samplingMode, GrBabylonJsBooleanValue adaptiveScaling, GrBabylonJsAdvancedDynamicTexture.AdvancedDynamicTextureProperties? properties)
        {
            var sceneObject = 
                new GrBabylonJsGuiFullScreenUi(name, this)
                {
                    IsForeground = isForeground,
                    SamplingMode = samplingMode,
                    AdaptiveScaling = adaptiveScaling
                };
            
            sceneObject.SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        protected override IEnumerable<string> GetConstructorArguments()
        {
            yield return Engine.GetCode();

            var optionsCode = 
                ObjectOptions is null 
                    ? "{}" 
                    : ObjectOptions.GetCode();

            yield return optionsCode;

        }
    }
}
