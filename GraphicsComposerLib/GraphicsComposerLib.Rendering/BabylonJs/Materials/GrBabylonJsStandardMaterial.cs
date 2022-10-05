using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Materials
{
    /// <summary>
    /// https://doc.babylonjs.com/typedoc/classes/BABYLON.StandardMaterial
    /// </summary>
    public sealed class GrBabylonJsStandardMaterial :
        GrBabylonJsMaterial
    {
        public sealed class StandardMaterialProperties :
            MaterialProperties
        {
            public GrBabylonJsColor3Value? Color { get; set; }

            public GrBabylonJsColor3Value? AmbientColor { get; set; }

            public GrBabylonJsColor3Value? EmissiveColor { get; set; }

            public GrBabylonJsColor3Value? DiffuseColor { get; set; }

            public GrBabylonJsColor3Value? SpecularColor { get; set; }
            
            public GrBabylonJsTextureValue? AmbientTexture { get; set; }

            public GrBabylonJsTextureValue? EmissiveTexture { get; set; }
            
            public GrBabylonJsTextureValue? DiffuseTexture { get; set; }

            public GrBabylonJsTextureValue? SpecularTexture { get; set; }

            public GrBabylonJsTextureValue? BumpTexture { get; set; }

            public GrBabylonJsTextureValue? OpacityTexture { get; set; }

            public GrBabylonJsTextureValue? ReflectionTexture { get; set; }

            public GrBabylonJsTextureValue? RefractionTexture { get; set; }

            public GrBabylonJsTextureValue? LightmapTexture { get; set; }

            public GrBabylonJsFresnelParametersValue? DiffuseFresnelParameters { get; set; }

            public GrBabylonJsFresnelParametersValue? EmissiveFresnelParameters { get; set; }

            public GrBabylonJsFresnelParametersValue? OpacityFresnelParameters { get; set; }

            public GrBabylonJsFresnelParametersValue? ReflectionFresnelParameters { get; set; }

            public GrBabylonJsFresnelParametersValue? RefractionFresnelParameters { get; set; }

            public GrBabylonJsFloat32Value? AlphaCutOff { get; set; }

            public GrBabylonJsFloat32Value? Roughness { get; set; }

            public GrBabylonJsFloat32Value? SpecularPower { get; set; }

            public GrBabylonJsFloat32Value? IndexOfRefraction { get; set; }

            public GrBabylonJsFloat32Value? ParallaxScaleBias { get; set; }

            public GrBabylonJsFloat32Value? CameraContrast { get; set; }

            public GrBabylonJsFloat32Value? CameraExposure { get; set; }

            public GrBabylonJsInt32Value? MaxSimultaneousLights { get; set; }

            public GrBabylonJsBooleanValue? DisableLighting { get; set; }

            public GrBabylonJsBooleanValue? TwoSidedLighting { get; set; }

            public GrBabylonJsBooleanValue? LinkEmissiveWithDiffuse { get; set; }

            public GrBabylonJsBooleanValue? InvertNormalMapX { get; set; }

            public GrBabylonJsBooleanValue? InvertNormalMapY { get; set; }

            public GrBabylonJsBooleanValue? InvertRefractionY { get; set; }

            public GrBabylonJsBooleanValue? AmbientTextureEnabled { get; set; }

            public GrBabylonJsBooleanValue? BumpTextureEnabled { get; set; }

            public GrBabylonJsBooleanValue? ColorGradingTextureEnabled { get; set; }

            public GrBabylonJsBooleanValue? DetailTextureEnabled { get; set; }

            public GrBabylonJsBooleanValue? DiffuseTextureEnabled { get; set; }

            public GrBabylonJsBooleanValue? EmissiveTextureEnabled { get; set; }

            public GrBabylonJsBooleanValue? LightmapTextureEnabled { get; set; }

            public GrBabylonJsBooleanValue? OpacityTextureEnabled { get; set; }

            public GrBabylonJsBooleanValue? ReflectionTextureEnabled { get; set; }

            public GrBabylonJsBooleanValue? RefractionTextureEnabled { get; set; }

            public GrBabylonJsBooleanValue? SpecularTextureEnabled { get; set; }

            public GrBabylonJsBooleanValue? UseLogarithmicDepth { get; set; }
            
            public GrBabylonJsBooleanValue? UseAlphaFromDiffuseTexture { get; set; }

            public GrBabylonJsBooleanValue? UseEmissiveAsIllumination { get; set; }

            public GrBabylonJsBooleanValue? UseGlossinessFromSpecularMapAlpha { get; set; }

            public GrBabylonJsBooleanValue? UseLightmapAsShadowmap { get; set; }

            public GrBabylonJsBooleanValue? UseObjectSpaceNormalMap { get; set; }

            public GrBabylonJsBooleanValue? UseParallax { get; set; }

            public GrBabylonJsBooleanValue? UseParallaxOcclusion { get; set; }

            public GrBabylonJsBooleanValue? UseReflectionFresnelFromSpecular { get; set; }

            public GrBabylonJsBooleanValue? UseReflectionOverAlpha { get; set; }

            public GrBabylonJsBooleanValue? UseSpecularOverAlpha { get; set; }

            public GrBabylonJsBooleanValue? FresnelEnabled { get; set; }


            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                foreach (var pair in base.GetNameValuePairs())
                    yield return pair;

                yield return AmbientColor.GetNameValueCodePair("ambientColor", Color);
                yield return DiffuseColor.GetNameValueCodePair("diffuseColor", Color);
                yield return SpecularColor.GetNameValueCodePair("specularColor", Color);
                yield return EmissiveColor.GetNameValueCodePair("emissiveColor");
                
                yield return AmbientTexture.GetNameValueCodePair("ambientTexture");
                yield return EmissiveTexture.GetNameValueCodePair("emissiveTexture");
                yield return DiffuseTexture.GetNameValueCodePair("diffuseTexture");
                yield return SpecularTexture.GetNameValueCodePair("specularTexture");
                yield return BumpTexture.GetNameValueCodePair("bumpTexture");
                yield return OpacityTexture.GetNameValueCodePair("opacityTexture");
                yield return ReflectionTexture.GetNameValueCodePair("reflectionTexture");
                yield return RefractionTexture.GetNameValueCodePair("refractionTexture");
                yield return LightmapTexture.GetNameValueCodePair("lightmapTexture");
                
                yield return EmissiveFresnelParameters.GetNameValueCodePair("emissiveFresnelParameters");
                yield return DiffuseFresnelParameters.GetNameValueCodePair("diffuseFresnelParameters");
                yield return OpacityFresnelParameters.GetNameValueCodePair("opacityFresnelParameters");
                yield return ReflectionFresnelParameters.GetNameValueCodePair("reflectionFresnelParameters");
                yield return RefractionFresnelParameters.GetNameValueCodePair("refractionFresnelParameters");
                
                yield return AlphaCutOff.GetNameValueCodePair("alphaCutOff");
                yield return Roughness.GetNameValueCodePair("roughness");
                yield return SpecularPower.GetNameValueCodePair("specularPower");
                yield return IndexOfRefraction.GetNameValueCodePair("indexOfRefraction");
                yield return ParallaxScaleBias.GetNameValueCodePair("parallaxScaleBias");
                yield return CameraContrast.GetNameValueCodePair("cameraContrast");
                yield return CameraExposure.GetNameValueCodePair("cameraExposure");

                yield return MaxSimultaneousLights.GetNameValueCodePair("maxSimultaneousLights");
                yield return DisableLighting.GetNameValueCodePair("disableLighting");
                yield return TwoSidedLighting.GetNameValueCodePair("twoSidedLighting");
                yield return LinkEmissiveWithDiffuse.GetNameValueCodePair("linkEmissiveWithDiffuse");
                yield return InvertNormalMapX.GetNameValueCodePair("invertNormalMapX");
                yield return InvertNormalMapY.GetNameValueCodePair("invertNormalMapY");
                yield return InvertRefractionY.GetNameValueCodePair("invertRefractionY");

                yield return AmbientTextureEnabled.GetNameValueCodePair("AmbientTextureEnabled");
                yield return BumpTextureEnabled.GetNameValueCodePair("BumpTextureEnabled");
                yield return ColorGradingTextureEnabled.GetNameValueCodePair("ColorGradingTextureEnabled");
                yield return DetailTextureEnabled.GetNameValueCodePair("DetailTextureEnabled");
                yield return DiffuseTextureEnabled.GetNameValueCodePair("DiffuseTextureEnabled");
                yield return EmissiveTextureEnabled.GetNameValueCodePair("EmissiveTextureEnabled");
                yield return LightmapTextureEnabled.GetNameValueCodePair("LightmapTextureEnabled");
                yield return OpacityTextureEnabled.GetNameValueCodePair("OpacityTextureEnabled");
                yield return ReflectionTextureEnabled.GetNameValueCodePair("ReflectionTextureEnabled");
                yield return RefractionTextureEnabled.GetNameValueCodePair("RefractionTextureEnabled");
                yield return SpecularTextureEnabled.GetNameValueCodePair("SpecularTextureEnabled");
                yield return FresnelEnabled.GetNameValueCodePair("FresnelEnabled");

                yield return UseAlphaFromDiffuseTexture.GetNameValueCodePair("useAlphaFromDiffuseTexture");
                yield return UseEmissiveAsIllumination.GetNameValueCodePair("useEmissiveAsIllumination");
                yield return UseGlossinessFromSpecularMapAlpha.GetNameValueCodePair("useGlossinessFromSpecularMapAlpha");
                yield return UseLightmapAsShadowmap.GetNameValueCodePair("useLightmapAsShadowmap");
                yield return UseObjectSpaceNormalMap.GetNameValueCodePair("useObjectSpaceNormalMap");
                yield return UseParallax.GetNameValueCodePair("useParallax");
                yield return UseParallaxOcclusion.GetNameValueCodePair("useParallaxOcclusion");
                yield return UseReflectionFresnelFromSpecular.GetNameValueCodePair("useReflectionFresnelFromSpecular");
                yield return UseReflectionOverAlpha.GetNameValueCodePair("useReflectionOverAlpha");
                yield return UseSpecularOverAlpha.GetNameValueCodePair("useSpecularOverAlpha");
                yield return UseLogarithmicDepth.GetNameValueCodePair("useLogarithmicDepth");
            }
        }


        protected override string ConstructorName
            => "new BABYLON.StandardMaterial";

        public StandardMaterialProperties? Properties { get; set; }
        
        public override GrBabylonJsObjectProperties? ObjectProperties 
            => Properties;


        public GrBabylonJsStandardMaterial(string constName) 
            : base(constName)
        {
        }
        
        public GrBabylonJsStandardMaterial(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }


        public GrBabylonJsStandardMaterial SetProperties([NotNull] StandardMaterialProperties? properties)
        {
            Properties = properties;

            return this;
        }
    }
}
