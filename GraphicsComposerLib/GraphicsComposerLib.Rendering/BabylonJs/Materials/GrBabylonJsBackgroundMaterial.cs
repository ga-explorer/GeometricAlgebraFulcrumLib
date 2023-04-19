using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Materials
{
    /// <summary>
    /// https://doc.babylonjs.com/typedoc/classes/BABYLON.BackgroundMaterial
    /// </summary>
    public sealed class GrBabylonJsBackgroundMaterial :
        GrBabylonJsMaterial
    {
        public sealed class BackgroundMaterialProperties :
            MaterialProperties
        {
            public GrBabylonJsColor3Value? PrimaryColor { get; set; }

            public GrBabylonJsColor3Value? PerceptualColor { get; set; }

            public GrBabylonJsVector3Value? SceneCenter { get; set; }
        
            public GrBabylonJsTextureValue? DiffuseTexture { get; set; }
        
            public GrBabylonJsTextureValue? ReflectionTexture { get; set; }
        
            public GrBabylonJsFloat32Value? ReflectionAmount { get; set; }

            public GrBabylonJsFloat32Value? ReflectionBlur { get; set; }

            public GrBabylonJsFloat32Value? ReflectionFalloffDistance { get; set; }

            public GrBabylonJsFloat32Value? ReflectionReflectance0 { get; set; }

            public GrBabylonJsFloat32Value? ReflectionReflectance90 { get; set; }

            public GrBabylonJsFloat32Value? StandardReflectance0 { get; set; }

            public GrBabylonJsFloat32Value? StandardReflectance90 { get; set; }

            public GrBabylonJsFloat32Value? ShadowLevel { get; set; }
        
            public GrBabylonJsFloat32Value? FovMultiplier { get; set; }

            public GrBabylonJsFloat32Value? PrimaryColorHighlightLevel { get; set; }
        
            public GrBabylonJsFloat32Value? PrimaryColorShadowLevel { get; set; }
        
            public GrBabylonJsFloat32Value? ReflectionStandardFresnelWeight { get; set; }
        
            public GrBabylonJsFloat32Value? CameraContrast { get; set; }

            public GrBabylonJsFloat32Value? CameraExposure { get; set; }

            public GrBabylonJsInt32Value? MaxSimultaneousLights { get; set; }

            public GrBabylonJsBooleanValue? EnableNoise { get; set; }

            public GrBabylonJsBooleanValue? ReflectionFresnel { get; set; }

            public GrBabylonJsBooleanValue? OpacityFresnel { get; set; }

            public GrBabylonJsBooleanValue? ShadowOnly { get; set; }

            public GrBabylonJsBooleanValue? SwitchToBgr { get; set; }

            public GrBabylonJsBooleanValue? UseEquirectangularFov { get; set; }

            public GrBabylonJsBooleanValue? UseRgbColor { get; set; }


            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                foreach (var pair in base.GetNameValuePairs())
                    yield return pair;

                yield return PrimaryColor.GetNameValueCodePair("primaryColor");
                yield return PerceptualColor.GetNameValueCodePair("_perceptualColor");

                yield return DiffuseTexture.GetNameValueCodePair("diffuseTexture");
                yield return ReflectionTexture.GetNameValueCodePair("reflectionTexture");

                yield return SceneCenter.GetNameValueCodePair("sceneCenter");
                yield return ReflectionAmount.GetNameValueCodePair("reflectionAmount");
                yield return ReflectionBlur.GetNameValueCodePair("reflectionBlur");
                yield return ReflectionFalloffDistance.GetNameValueCodePair("reflectionFalloffDistance");
                yield return ReflectionReflectance0.GetNameValueCodePair("reflectionReflectance0");
                yield return ReflectionReflectance90.GetNameValueCodePair("reflectionReflectance90");
                yield return StandardReflectance0.GetNameValueCodePair("StandardReflectance0");
                yield return StandardReflectance90.GetNameValueCodePair("StandardReflectance90");
                yield return ShadowLevel.GetNameValueCodePair("shadowLevel");
                yield return FovMultiplier.GetNameValueCodePair("fovMultiplier");
                yield return PrimaryColorHighlightLevel.GetNameValueCodePair("primaryColorHighlightLevel");
                yield return PrimaryColorShadowLevel.GetNameValueCodePair("primaryColorShadowLevel");
                yield return ReflectionStandardFresnelWeight.GetNameValueCodePair("reflectionStandardFresnelWeight");
            
                yield return EnableNoise.GetNameValueCodePair("enableNoise");
                yield return ReflectionFresnel.GetNameValueCodePair("reflectionFresnel");
                yield return OpacityFresnel.GetNameValueCodePair("opacityFresnel");
                yield return ShadowOnly.GetNameValueCodePair("shadowOnly");
                yield return SwitchToBgr.GetNameValueCodePair("switchToBGR");
                yield return UseEquirectangularFov.GetNameValueCodePair("useEquirectangularFOV");
                yield return CameraContrast.GetNameValueCodePair("cameraContrast");
                yield return CameraExposure.GetNameValueCodePair("cameraExposure");

                yield return MaxSimultaneousLights.GetNameValueCodePair("maxSimultaneousLights");
                yield return UseRgbColor.GetNameValueCodePair("useRGBColor");
            }
        }


        protected override string ConstructorName
            => "new BABYLON.BackgroundMaterial";

        public BackgroundMaterialProperties? Properties { get; private set; }
            = new BackgroundMaterialProperties();
    
        public override GrBabylonJsObjectProperties? ObjectProperties 
            => Properties;


        public GrBabylonJsBackgroundMaterial(string constName) 
            : base(constName)
        {
        }
    
        public GrBabylonJsBackgroundMaterial(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }


        public GrBabylonJsBackgroundMaterial SetProperties(BackgroundMaterialProperties properties)
        {
            Properties = properties;

            return this;
        }
    }
}