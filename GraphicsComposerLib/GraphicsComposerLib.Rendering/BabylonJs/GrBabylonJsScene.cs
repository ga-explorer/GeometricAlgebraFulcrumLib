using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GraphicsComposerLib.Rendering.BabylonJs.Animations;
using GraphicsComposerLib.Rendering.BabylonJs.Cameras;
using GraphicsComposerLib.Rendering.BabylonJs.Constants;
using GraphicsComposerLib.Rendering.BabylonJs.Curves;
using GraphicsComposerLib.Rendering.BabylonJs.GUI;
using GraphicsComposerLib.Rendering.BabylonJs.Materials;
using GraphicsComposerLib.Rendering.BabylonJs.Meshes;
using GraphicsComposerLib.Rendering.BabylonJs.Textures;
using GraphicsComposerLib.Rendering.BabylonJs.Values;
using TextComposerLib.Text;

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
            public GrBabylonJsBooleanValue? UseClonedMeshMap
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useClonedMeshMap");
                set => SetAttributeValue("useClonedMeshMap", value);
            }

            public GrBabylonJsBooleanValue? UseGeometryUniqueIdsMap
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useGeometryUniqueIdsMap");
                set => SetAttributeValue("useGeometryUniqueIdsMap", value);
            }
            
            public GrBabylonJsBooleanValue? UseMaterialMeshMap
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useMaterialMeshMap");
                set => SetAttributeValue("useMaterialMeshMap", value);
            }

            public GrBabylonJsBooleanValue? Virtual
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("virtual");
                set => SetAttributeValue("virtual", value);
            }
            
            
            public SceneOptions()
            {
            }
            
            public SceneOptions(SceneOptions properties)
            {
                SetAttributeValues(properties);
            }
        }

        public sealed class SceneProperties :
            GrBabylonJsObjectProperties
        {
            public GrBabylonJsMaterialValue? DefaultMaterial
            {
                get => GetAttributeValueOrNull<GrBabylonJsMaterialValue>("defaultMaterial");
                set => SetAttributeValue("defaultMaterial", value);
            }

            public GrBabylonJsColor3Value? AmbientColor
            {
                get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("ambientColor");
                set => SetAttributeValue("ambientColor", value);
            }

            public GrBabylonJsColor4Value? ClearColor
            {
                get => GetAttributeValueOrNull<GrBabylonJsColor4Value>("clearColor");
                set => SetAttributeValue("clearColor", value);
            }

            public GrBabylonJsBooleanValue? FogEnabled
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("fogEnabled");
                set => SetAttributeValue("fogEnabled", value);
            }

            public GrBabylonJsFogModeValue? FogMode
            {
                get => GetAttributeValueOrNull<GrBabylonJsFogModeValue>("fogMode");
                set => SetAttributeValue("fogMode", value);
            }

            public GrBabylonJsColor3Value? FogColor
            {
                get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("fogColor");
                set => SetAttributeValue("fogColor", value);
            }

            public GrBabylonJsFloat32Value? FogDensity
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("fogDensity");
                set => SetAttributeValue("fogDensity", value);
            }

            public GrBabylonJsFloat32Value? FogStart
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("fogStart");
                set => SetAttributeValue("fogStart", value);
            }

            public GrBabylonJsFloat32Value? FogEnd
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("fogEnd");
                set => SetAttributeValue("fogEnd", value);
            }

            public GrBabylonJsFloat32Value? AnimationTimeScale
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("animationTimeScale");
                set => SetAttributeValue("animationTimeScale", value);
            }

            public GrBabylonJsBooleanValue? AnimationsEnabled
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("animationsEnabled");
                set => SetAttributeValue("animationsEnabled", value);
            }

            public GrBabylonJsBooleanValue? AudioEnabled
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("audioEnabled");
                set => SetAttributeValue("audioEnabled", value);
            }

            public GrBabylonJsFloat32Value? AudioPositioningRefreshRate
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("audioPositioningRefreshRate");
                set => SetAttributeValue("audioPositioningRefreshRate", value);
            }

            public GrBabylonJsBooleanValue? Headphone
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("headphone");
                set => SetAttributeValue("headphone", value);
            }

            public GrBabylonJsFloat32Value? DeltaTime
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("deltaTime");
                set => SetAttributeValue("deltaTime", value);
            }

            public GrBabylonJsFloat32Value? MinDeltaTime
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("minDeltaTime");
                set => SetAttributeValue("minDeltaTime", value);
            }

            public GrBabylonJsFloat32Value? MaxDeltaTime
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("maxDeltaTime");
                set => SetAttributeValue("maxDeltaTime", value);
            }

            public GrBabylonJsBooleanValue? UseConstantAnimationDeltaTime
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useConstantAnimationDeltaTime");
                set => SetAttributeValue("useConstantAnimationDeltaTime", value);
            }

            public GrBabylonJsFloat32Value? PointerX
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("pointerX");
                set => SetAttributeValue("pointerX", value);
            }

            public GrBabylonJsFloat32Value? PointerY
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("pointerY");
                set => SetAttributeValue("pointerY", value);
            }

            public GrBabylonJsBooleanValue? UseOrderIndependentTransparency
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useOrderIndependentTransparency");
                set => SetAttributeValue("useOrderIndependentTransparency", value);
            }

            public GrBabylonJsBooleanValue? AutoClear
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("autoClear");
                set => SetAttributeValue("autoClear", value);
            }

            public GrBabylonJsBooleanValue? AutoClearDepthAndStencil
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("autoClearDepthAndStencil");
                set => SetAttributeValue("autoClearDepthAndStencil", value);
            }

            public GrBabylonJsBooleanValue? PhysicsEnabled
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("physicsEnabled");
                set => SetAttributeValue("physicsEnabled", value);
            }

            public GrBabylonJsBooleanValue? ParticlesEnabled
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("particlesEnabled");
                set => SetAttributeValue("particlesEnabled", value);
            }

            public GrBabylonJsBooleanValue? CollisionsEnabled
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("collisionsEnabled");
                set => SetAttributeValue("collisionsEnabled", value);
            }

            public GrBabylonJsVector3Value? Gravity
            {
                get => GetAttributeValueOrNull<GrBabylonJsVector3Value>("gravity");
                set => SetAttributeValue("gravity", value);
            }

            public GrBabylonJsBooleanValue? ConstantlyUpdateMeshUnderPointer
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("constantlyUpdateMeshUnderPointer");
                set => SetAttributeValue("constantlyUpdateMeshUnderPointer", value);
            }

            public GrBabylonJsBooleanValue? ForceShowBoundingBoxes
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("forceShowBoundingBoxes");
                set => SetAttributeValue("forceShowBoundingBoxes", value);
            }

            public GrBabylonJsBooleanValue? ForcePointsCloud
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("forcePointsCloud");
                set => SetAttributeValue("forcePointsCloud", value);
            }

            public GrBabylonJsBooleanValue? ForceWireFrame
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("forceWireFrame");
                set => SetAttributeValue("forceWireFrame", value);
            }

            public GrBabylonJsBooleanValue? LightsEnabled
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("lightsEnabled");
                set => SetAttributeValue("lightsEnabled", value);
            }

            public GrBabylonJsBooleanValue? ShadowsEnabled
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("shadowsEnabled");
                set => SetAttributeValue("shadowsEnabled", value);
            }

            public GrBabylonJsBooleanValue? SkeletonsEnabled
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("skeletonsEnabled");
                set => SetAttributeValue("skeletonsEnabled", value);
            }

            public GrBabylonJsBooleanValue? SkipFrustumClipping
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("skipFrustumClipping");
                set => SetAttributeValue("skipFrustumClipping", value);
            }

            public GrBabylonJsBooleanValue? TexturesEnabled
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("texturesEnabled");
                set => SetAttributeValue("texturesEnabled", value);
            }

            public GrBabylonJsBooleanValue? UseRightHandedSystem
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useRightHandedSystem");
                set => SetAttributeValue("useRightHandedSystem", value);
            }

            public GrBabylonJsBooleanValue? RequireLightSorting
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("requireLightSorting");
                set => SetAttributeValue("requireLightSorting", value);
            }

            public GrBabylonJsBooleanValue? SpritesEnabled
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("spritesEnabled");
                set => SetAttributeValue("spritesEnabled", value);
            }

            
            public SceneProperties()
            {
                UseRightHandedSystem = true;
            }
            
            public SceneProperties(SceneProperties properties)
            {
                UseRightHandedSystem = true;
                SetAttributeValues(properties);
            }
        }


        protected override string ConstructorName 
            => "new BABYLON.Scene";

        public SceneOptions Options { get; private set; }
            = new SceneOptions();

        public SceneProperties Properties { get; private set; }
            = new SceneProperties();

        public override GrBabylonJsObjectOptions ObjectOptions 
            => Options;

        public override GrBabylonJsObjectProperties ObjectProperties 
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
        
        public IEnumerable<GrBabylonJsAnimation> Animations
            => ObjectList
                .Where(s => s is GrBabylonJsAnimation)
                .Cast<GrBabylonJsAnimation>();

        public GrBabylonJsKeyFrameDictionaryCache KeyFramesCache { get; }
            = new GrBabylonJsKeyFrameDictionaryCache();

        public List<GrBabylonJsCodeValue> BeforeSceneRenderCode { get; }
            = new List<GrBabylonJsCodeValue>();


        public GrBabylonJsScene(string constName) 
            : base(constName)
        {
        }


        public GrBabylonJsScene SetOptions(SceneOptions options)
        {
            Options = new SceneOptions(options);

            return this;
        }

        public GrBabylonJsScene SetProperties(SceneProperties properties)
        {
            Properties = new SceneProperties(properties);

            return this;
        }


        public GrBabylonJsMaterial GetMaterial(string name)
        {
            if (!ObjectList.TryGetObject(name, out var sceneObject))
                throw new KeyNotFoundException();

            return sceneObject as GrBabylonJsMaterial 
                   ?? throw new KeyNotFoundException();
        }
        
        public GrBabylonJsGuiFullScreenUi GetGuiFullScreenUi(string name)
        {
            if (!ObjectList.TryGetObject(name, out var sceneObject))
                throw new KeyNotFoundException();

            return sceneObject as GrBabylonJsGuiFullScreenUi 
                   ?? throw new KeyNotFoundException();
        }

        public GrBabylonJsFreeCode AddFreeCode(string code)
        {
            var sceneObject = 
                new GrBabylonJsFreeCode(code);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsFreeCode AddFreeCode(params string[] codeLines)
        {
            return AddFreeCode(codeLines.Concatenate(Environment.NewLine));
        }
        
        public GrBabylonJsFreeCode AddFreeCode(IEnumerable<string> codeLines)
        {
            return AddFreeCode(codeLines.Concatenate(Environment.NewLine));
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
        
        public GrBabylonJsArcRotateCamera AddArcRotateCamera(string name, GrBabylonJsFloat32Value alpha, GrBabylonJsFloat32Value beta, GrBabylonJsFloat32Value radius, GrBabylonJsVector3Value target, GrBabylonJsArcRotateCamera.ArcRotateCameraProperties properties)
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

        public GrBabylonJsUniversalCamera AddUniversalCamera(string name, GrBabylonJsVector3Value position, GrBabylonJsUniversalCamera.UniversalCameraProperties properties)
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

        public GrBabylonJsEnvironmentHelper AddEnvironmentHelper(string name, GrBabylonJsEnvironmentHelper.EnvironmentHelperOptions options)
        {
            var sceneObject = 
                new GrBabylonJsEnvironmentHelper(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsEnvironmentHelper AddEnvironmentHelper(string name, GrBabylonJsEnvironmentHelper.EnvironmentHelperProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsEnvironmentHelper(name, this)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsEnvironmentHelper AddEnvironmentHelper(string name, GrBabylonJsEnvironmentHelper.EnvironmentHelperOptions options, GrBabylonJsEnvironmentHelper.EnvironmentHelperProperties properties)
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
        
        public GrBabylonJsTexture AddTexture(string name, string url, GrBabylonJsTexture.TextureOptions options)
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

        public GrBabylonJsTexture AddTexture(string name, string url, GrBabylonJsTexture.TextureProperties properties)
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

        public GrBabylonJsTexture AddTexture(string name, string url, GrBabylonJsTexture.TextureOptions options, GrBabylonJsTexture.TextureProperties properties)
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

        
        public GrBabylonJsHtmlElementTexture AddHtmlElementTexture(string name, GrBabylonJsCodeValue element)
        {
            var sceneObject = 
                new GrBabylonJsHtmlElementTexture(name, this, element);
            
            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsHtmlElementTexture AddHtmlElementTexture(string name, GrBabylonJsCodeValue element, GrBabylonJsHtmlElementTexture.HtmlElementTextureOptions options)
        {
            var sceneObject = 
                new GrBabylonJsHtmlElementTexture(name, this, element)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsHtmlElementTexture AddHtmlElementTexture(string name, GrBabylonJsCodeValue element, GrBabylonJsHtmlElementTexture.HtmlElementTextureOptions options, GrBabylonJsHtmlElementTexture.HtmlElementTextureProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsHtmlElementTexture(name, this, element)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }


        public GrBabylonJsSimpleMaterial AddSimpleMaterial(string name, GrBabylonJsSimpleMaterial.SimpleMaterialProperties properties)
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
        
        public GrBabylonJsStandardMaterial AddStandardMaterial(string name, GrBabylonJsStandardMaterial.StandardMaterialProperties properties)
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
                    new GrBabylonJsStandardMaterial.StandardMaterialProperties(color)
                );

            return AddStandardMaterial(
                name, 
                new GrBabylonJsStandardMaterial.StandardMaterialProperties(color)
                {
                    Alpha = alpha / 255f,
                    TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend,
                    BackFaceCulling = true
                }
            );
        }
        
        public GrBabylonJsShadowOnlyMaterial AddShadowOnlyMaterial(string name, GrBabylonJsShadowOnlyMaterial.ShadowOnlyMaterialProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsShadowOnlyMaterial(name, this)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsOcclusionMaterial AddOcclusionMaterial(string name, GrBabylonJsOcclusionMaterial.OcclusionMaterialProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsOcclusionMaterial(name, this)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsNormalMaterial AddNormalMaterial(string name, GrBabylonJsNormalMaterial.NormalMaterialProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsNormalMaterial(name, this)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsHandleMaterial AddHandleMaterial(string name, GrBabylonJsHandleMaterial.HandleMaterialProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsHandleMaterial(name, this)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsGridMaterial AddGridMaterial(string name, GrBabylonJsGridMaterial.GridMaterialProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsGridMaterial(name, this)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsGradientMaterial AddGradientMaterial(string name, GrBabylonJsGradientMaterial.GradientMaterialProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsGradientMaterial(name, this)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsBackgroundMaterial AddBackgroundMaterial(string name, GrBabylonJsBackgroundMaterial.BackgroundMaterialProperties properties)
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
        
        public GrBabylonJsMeshClone AddClone(string name, string parentMeshConstName, GrBabylonJsMesh.MeshProperties properties)
        {
            var sceneObject =
                new GrBabylonJsMeshClone(name, parentMeshConstName)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsMeshClone AddClone(string name, GrBabylonJsMesh parentMesh, GrBabylonJsMesh.MeshProperties properties)
        {
            var sceneObject =
                new GrBabylonJsMeshClone(name, parentMesh.ConstName)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsCapsule AddCapsule(string name, GrBabylonJsCapsule.CapsuleOptions options)
        {
            var sceneObject = 
                new GrBabylonJsCapsule(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsCapsule AddCapsule(string name, GrBabylonJsCapsule.CapsuleOptions options, GrBabylonJsMesh.MeshProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsCapsule(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsCylinder AddCylinder(string name, GrBabylonJsCylinder.CylinderOptions options)
        {
            var sceneObject = 
                new GrBabylonJsCylinder(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsCylinder AddCylinder(string name, GrBabylonJsCylinder.CylinderOptions options, GrBabylonJsMesh.MeshProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsCylinder(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsDisc AddDisc(string name, GrBabylonJsDisc.DiscOptions options)
        {
            var sceneObject = 
                new GrBabylonJsDisc(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsDisc AddDisc(string name, GrBabylonJsDisc.DiscOptions options, GrBabylonJsMesh.MeshProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsDisc(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsGround AddGround(string name, GrBabylonJsGround.GroundOptions options)
        {
            var sceneObject = 
                new GrBabylonJsGround(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsGround AddGround(string name, GrBabylonJsGround.GroundOptions options, GrBabylonJsMesh.MeshProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsGround(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsPlane AddPlane(string name, GrBabylonJsPlane.PlaneOptions options)
        {
            var sceneObject = 
                new GrBabylonJsPlane(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsPlane AddPlane(string name, GrBabylonJsPlane.PlaneOptions options, GrBabylonJsMesh.MeshProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsPlane(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsBox AddBox(string name, GrBabylonJsBox.BoxOptions options)
        {
            var sceneObject =
                new GrBabylonJsBox(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsBox AddBox(string name, GrBabylonJsBox.BoxOptions options, GrBabylonJsMesh.MeshProperties properties)
        {
            var sceneObject =
                new GrBabylonJsBox(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsSphere AddSphere(string name, GrBabylonJsSphere.SphereOptions options)
        {
            var sceneObject = 
                new GrBabylonJsSphere(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsSphere AddSphere(string name, GrBabylonJsSphere.SphereOptions options, GrBabylonJsMesh.MeshProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsSphere(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsIcoSphere AddIcoSphere(string name, GrBabylonJsIcoSphere.IcoSphereOptions options)
        {
            var sceneObject = 
                new GrBabylonJsIcoSphere(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsIcoSphere AddIcoSphere(string name, GrBabylonJsIcoSphere.IcoSphereOptions options, GrBabylonJsMesh.MeshProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsIcoSphere(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsTorus AddTorus(string name, GrBabylonJsTorus.TorusOptions options)
        {
            var sceneObject = 
                new GrBabylonJsTorus(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsTorus AddTorus(string name, GrBabylonJsTorus.TorusOptions options, GrBabylonJsMesh.MeshProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsTorus(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsTube AddTube(string name, GrBabylonJsTube.TubeOptions options)
        {
            var sceneObject = 
                new GrBabylonJsTube(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsTube AddTube(string name, GrBabylonJsTube.TubeOptions options, GrBabylonJsMesh.MeshProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsTube(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsLines AddLines(string name, GrBabylonJsLines.LinesOptions options)
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
        
        public GrBabylonJsDashedLines AddDashedLines(string name, GrBabylonJsDashedLines.DashedLinesOptions options)
        {
            var sceneObject = 
                new GrBabylonJsDashedLines(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsDashedLines AddDashedLines(string name, GrBabylonJsDashedLines.DashedLinesOptions options, GrBabylonJsLinesMesh.LinesMeshProperties properties)
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
        
        public GrBabylonJsLathe AddLathe(string name, GrBabylonJsLathe.LatheOptions options)
        {
            var sceneObject = 
                new GrBabylonJsLathe(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsLathe AddLathe(string name, GrBabylonJsLathe.LatheOptions options, GrBabylonJsMesh.MeshProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsLathe(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsRibbon AddRibbon(string name, GrBabylonJsRibbon.RibbonOptions options)
        {
            var sceneObject = 
                new GrBabylonJsRibbon(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsRibbon AddRibbon(string name, GrBabylonJsRibbon.RibbonOptions options, GrBabylonJsMesh.MeshProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsRibbon(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsExtrudeShape AddExtrudeShape(string name, GrBabylonJsExtrudeShape.ExtrudeShapeOptions options)
        {
            var sceneObject = 
                new GrBabylonJsExtrudeShape(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsExtrudeShape AddExtrudeShape(string name, GrBabylonJsExtrudeShape.ExtrudeShapeOptions options, GrBabylonJsMesh.MeshProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsExtrudeShape(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsExtrudeShapeCustom AddExtrudeShapeCustom(string name, GrBabylonJsExtrudeShapeCustom.ExtrudeShapeCustomOptions options)
        {
            var sceneObject = 
                new GrBabylonJsExtrudeShapeCustom(name, this)
                    .SetOptions(options);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsExtrudeShapeCustom AddExtrudeShapeCustom(string name, GrBabylonJsExtrudeShapeCustom.ExtrudeShapeCustomOptions options, GrBabylonJsMesh.MeshProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsExtrudeShapeCustom(name, this)
                    .SetOptions(options)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        
        public GrBabylonJsGuiFullScreenUi AddGuiFullScreenUi(string name, GrBabylonJsBooleanValue? isForeground = null, GrBabylonJsTextureSamplingModeValue? samplingMode = null, GrBabylonJsBooleanValue? adaptiveScaling = null)
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

        public GrBabylonJsGuiFullScreenUi AddGuiFullScreenUi(string name, GrBabylonJsBooleanValue isForeground, GrBabylonJsTextureSamplingMode samplingMode, GrBabylonJsBooleanValue adaptiveScaling, GrBabylonJsAdvancedDynamicTexture.AdvancedDynamicTextureProperties properties)
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
        

        public GrBabylonJsAnimationOfFloat AddFloat32Animation(string name, string targetPropertyName, GrBabylonJsAnimationSpecs animationSpecs, GrBabylonJsKeyFrameDictionary<double> keyFrames)
        {
            var (keyFramesIndex, _) = 
                KeyFramesCache.AddOrGetFloatKeyFrames(keyFrames);

            var sceneObject = 
                new GrBabylonJsAnimationOfFloat(
                    name, 
                    targetPropertyName, 
                    animationSpecs, 
                    KeyFramesCache,
                    keyFramesIndex
                );

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsAnimationOfVector2 AddVector2Animation(string name, string targetPropertyName, GrBabylonJsAnimationSpecs animationSpecs, GrBabylonJsKeyFrameDictionary<Float64Vector2D> keyFrames)
        {
            var (keyFramesIndex, _) = 
                KeyFramesCache.AddOrGetVector2KeyFrames(keyFrames);

            var sceneObject = 
                new GrBabylonJsAnimationOfVector2(
                    name, 
                    targetPropertyName, 
                    animationSpecs, 
                    KeyFramesCache,
                    keyFramesIndex
                );

            ObjectList.Add(sceneObject);

            return sceneObject;
        }
        
        public GrBabylonJsAnimationOfVector3 AddVector3Animation(string name, string targetPropertyName, GrBabylonJsAnimationSpecs animationSpecs, GrBabylonJsKeyFrameDictionary<Float64Vector3D> keyFrames)
        {
            var (keyFramesIndex, _) = 
                KeyFramesCache.AddOrGetVector3KeyFrames(keyFrames);

            var sceneObject = 
                new GrBabylonJsAnimationOfVector3(
                    name, 
                    targetPropertyName, 
                    animationSpecs, 
                    KeyFramesCache,
                    keyFramesIndex
                );

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsAnimationOfQuaternion AddQuaternionAnimation(string name, string targetPropertyName, GrBabylonJsAnimationSpecs animationSpecs, GrBabylonJsKeyFrameDictionary<Float64Quaternion> keyFrames)
        {
            var (keyFramesIndex, _) = 
                KeyFramesCache.AddOrGetQuaternionKeyFrames(keyFrames);

            var sceneObject = 
                new GrBabylonJsAnimationOfQuaternion(
                    name, 
                    targetPropertyName, 
                    animationSpecs, 
                    KeyFramesCache,
                    keyFramesIndex
                );

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public Pair<GrBabylonJsAnimationOfFloat> AddVector2ComponentAnimations(string name, GrBabylonJsAnimationSpecs animationSpecs, GrBabylonJsKeyFrameDictionary<Float64Vector2D> keyPositions, GrBabylonJsKeyFrameOptimizationKind optimizationKind = GrBabylonJsKeyFrameOptimizationKind.Separate)
        {
            var (keyXValues, keyYValues) = 
                keyPositions.SeparateComponents(optimizationKind);

            var xAnimation = AddFloat32Animation(
                name + "XAnimation",
                "x",
                animationSpecs,
                keyXValues
            );
            
            var yAnimation = AddFloat32Animation(
                name + "YAnimation",
                "y",
                animationSpecs,
                keyYValues
            );
            
            return new Pair<GrBabylonJsAnimationOfFloat>(
                xAnimation,
                yAnimation
            );
        }

        public Triplet<GrBabylonJsAnimationOfFloat> AddVector3ComponentAnimations(string name, GrBabylonJsAnimationSpecs animationSpecs, IEnumerable<KeyValuePair<int, Float64Vector3D>> frameIndexPositionPairs, GrBabylonJsKeyFrameOptimizationKind optimizationKind = GrBabylonJsKeyFrameOptimizationKind.Separate)
        {
            var keyPositions = new GrBabylonJsKeyFrameDictionary<Float64Vector3D>();

            foreach (var (frameIndex, position) in frameIndexPositionPairs)
                keyPositions.SetKeyFrameValue(frameIndex, position);

            return AddVector3ComponentAnimations(
                name,
                animationSpecs,
                keyPositions,
                optimizationKind
            );
        }

        public Triplet<GrBabylonJsAnimationOfFloat> AddVector3ComponentAnimations(string name, GrBabylonJsAnimationSpecs animationSpecs, GrBabylonJsKeyFrameDictionary<Float64Vector3D> keyPositions, GrBabylonJsKeyFrameOptimizationKind optimizationKind = GrBabylonJsKeyFrameOptimizationKind.Separate)
        {
            var (keyXValues, keyYValues, keyZValues) = 
                keyPositions.SeparateComponents(optimizationKind);

            var xAnimation = AddFloat32Animation(
                name + "XAnimation",
                "x",
                animationSpecs,
                keyXValues
            );
            
            var yAnimation = AddFloat32Animation(
                name + "YAnimation",
                "y",
                animationSpecs,
                keyYValues
            );
            
            var zAnimation = AddFloat32Animation(
                name + "ZAnimation",
                "z",
                animationSpecs,
                keyZValues
            );

            return new Triplet<GrBabylonJsAnimationOfFloat>(
                xAnimation,
                yAnimation,
                zAnimation
            );
        }
        
        //public Quad<GrBabylonJsFloat32Animation> AddComponentAnimations(string name, GrBabylonJsAnimationSpecs animationSpecs, GrBabylonJsKeyFrameDictionary<Float64Tuple4D> keyPositions, GrBabylonJsKeyFrameOptimizationKind optimizationKind = GrBabylonJsKeyFrameOptimizationKind.Separate)
        //{
        //    var (keyXValues, keyYValues, keyZValues, keyWValues) = 
        //        keyPositions.SeparateComponents(optimizationKind);

        //    var xAnimation = AddFloat32Animation(
        //        name + "XAnimation",
        //        "x",
        //        animationSpecs,
        //        keyXValues
        //    );
            
        //    var yAnimation = AddFloat32Animation(
        //        name + "YAnimation",
        //        "y",
        //        animationSpecs,
        //        keyYValues
        //    );
            
        //    var zAnimation = AddFloat32Animation(
        //        name + "ZAnimation",
        //        "z",
        //        animationSpecs,
        //        keyZValues
        //    );
            
        //    var wAnimation = AddFloat32Animation(
        //        name + "WAnimation",
        //        "w",
        //        animationSpecs,
        //        keyWValues
        //    );

        //    return new Quad<GrBabylonJsFloat32Animation>(
        //        xAnimation,
        //        yAnimation,
        //        zAnimation,
        //        wAnimation
        //    );
        //}
        
        public GrBabylonJsAnimationGroup AddAnimationGroup(string name)
        {
            var sceneObject = 
                new GrBabylonJsAnimationGroup(name, this);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        public GrBabylonJsAnimationGroup AddAnimationGroup(string name, GrBabylonJsAnimationGroup.AnimationGroupProperties properties)
        {
            var sceneObject = 
                new GrBabylonJsAnimationGroup(name, this)
                    .SetProperties(properties);

            ObjectList.Add(sceneObject);

            return sceneObject;
        }

        //public JArray GetAnimationKeyFramesJson()
        //{
        //    var array = new JArray();

        //    foreach (var animation in Animations)
        //    {
        //        var name = animation.ConstName + "KeyFrames";
        //        var value = animation.GetKeyFramesJson();

        //        array.Add(
        //            JObject.FromObject(new {animation = name}) (name, value)
        //        );
        //    }

        //    return array;
        //}

        protected override IEnumerable<string> GetConstructorArguments()
        {
            yield return Engine.GetCode();

            var optionsCode = 
                ObjectOptions.GetCode();

            yield return optionsCode;

        }
    }
}
