using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Materials;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.StandardMaterial
/// </summary>
public sealed class GrBabylonJsStandardMaterial :
    GrBabylonJsMaterial
{
    public sealed class StandardMaterialProperties :
        MaterialProperties
    {
        //public GrBabylonJsColor3Value? Color
        //{
        //    get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("color");
        //    set => SetAttributeValue("color", value);
        //}

        public GrBabylonJsColor3Value? AmbientColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("ambientColor");
            set => SetAttributeValue("ambientColor", value);
        }

        public GrBabylonJsColor3Value? EmissiveColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("emissiveColor");
            set => SetAttributeValue("emissiveColor", value);
        }

        public GrBabylonJsColor3Value? DiffuseColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("diffuseColor");
            set => SetAttributeValue("diffuseColor", value);
        }

        public GrBabylonJsColor3Value? SpecularColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("specularColor");
            set => SetAttributeValue("specularColor", value);
        }

        public GrBabylonJsTextureValue? AmbientTexture
        {
            get => GetAttributeValueOrNull<GrBabylonJsTextureValue>("ambientTexture");
            set => SetAttributeValue("ambientTexture", value);
        }

        public GrBabylonJsTextureValue? EmissiveTexture
        {
            get => GetAttributeValueOrNull<GrBabylonJsTextureValue>("emissiveTexture");
            set => SetAttributeValue("emissiveTexture", value);
        }

        public GrBabylonJsTextureValue? DiffuseTexture
        {
            get => GetAttributeValueOrNull<GrBabylonJsTextureValue>("diffuseTexture");
            set => SetAttributeValue("diffuseTexture", value);
        }

        public GrBabylonJsTextureValue? SpecularTexture
        {
            get => GetAttributeValueOrNull<GrBabylonJsTextureValue>("specularTexture");
            set => SetAttributeValue("specularTexture", value);
        }

        public GrBabylonJsTextureValue? BumpTexture
        {
            get => GetAttributeValueOrNull<GrBabylonJsTextureValue>("bumpTexture");
            set => SetAttributeValue("bumpTexture", value);
        }

        public GrBabylonJsTextureValue? OpacityTexture
        {
            get => GetAttributeValueOrNull<GrBabylonJsTextureValue>("opacityTexture");
            set => SetAttributeValue("opacityTexture", value);
        }

        public GrBabylonJsTextureValue? ReflectionTexture
        {
            get => GetAttributeValueOrNull<GrBabylonJsTextureValue>("reflectionTexture");
            set => SetAttributeValue("reflectionTexture", value);
        }

        public GrBabylonJsTextureValue? RefractionTexture
        {
            get => GetAttributeValueOrNull<GrBabylonJsTextureValue>("refractionTexture");
            set => SetAttributeValue("refractionTexture", value);
        }

        public GrBabylonJsTextureValue? LightmapTexture
        {
            get => GetAttributeValueOrNull<GrBabylonJsTextureValue>("lightmapTexture");
            set => SetAttributeValue("lightmapTexture", value);
        }

        public GrBabylonJsFresnelParametersValue? DiffuseFresnelParameters
        {
            get => GetAttributeValueOrNull<GrBabylonJsFresnelParametersValue>("diffuseFresnelParameters");
            set => SetAttributeValue("diffuseFresnelParameters", value);
        }

        public GrBabylonJsFresnelParametersValue? EmissiveFresnelParameters
        {
            get => GetAttributeValueOrNull<GrBabylonJsFresnelParametersValue>("emissiveFresnelParameters");
            set => SetAttributeValue("emissiveFresnelParameters", value);
        }

        public GrBabylonJsFresnelParametersValue? OpacityFresnelParameters
        {
            get => GetAttributeValueOrNull<GrBabylonJsFresnelParametersValue>("opacityFresnelParameters");
            set => SetAttributeValue("opacityFresnelParameters", value);
        }

        public GrBabylonJsFresnelParametersValue? ReflectionFresnelParameters
        {
            get => GetAttributeValueOrNull<GrBabylonJsFresnelParametersValue>("reflectionFresnelParameters");
            set => SetAttributeValue("reflectionFresnelParameters", value);
        }

        public GrBabylonJsFresnelParametersValue? RefractionFresnelParameters
        {
            get => GetAttributeValueOrNull<GrBabylonJsFresnelParametersValue>("refractionFresnelParameters");
            set => SetAttributeValue("refractionFresnelParameters", value);
        }

        public GrBabylonJsFloat32Value? AlphaCutOff
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("alphaCutOff");
            set => SetAttributeValue("alphaCutOff", value);
        }

        public GrBabylonJsFloat32Value? Roughness
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("roughness");
            set => SetAttributeValue("roughness", value);
        }

        public GrBabylonJsFloat32Value? SpecularPower
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("specularPower");
            set => SetAttributeValue("specularPower", value);
        }

        public GrBabylonJsFloat32Value? IndexOfRefraction
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("indexOfRefraction");
            set => SetAttributeValue("indexOfRefraction", value);
        }

        public GrBabylonJsFloat32Value? ParallaxScaleBias
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("parallaxScaleBias");
            set => SetAttributeValue("parallaxScaleBias", value);
        }

        public GrBabylonJsFloat32Value? CameraContrast
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("cameraContrast");
            set => SetAttributeValue("cameraContrast", value);
        }

        public GrBabylonJsFloat32Value? CameraExposure
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("cameraExposure");
            set => SetAttributeValue("cameraExposure", value);
        }

        public GrBabylonJsInt32Value? MaxSimultaneousLights
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("maxSimultaneousLights");
            set => SetAttributeValue("maxSimultaneousLights", value);
        }

        public GrBabylonJsBooleanValue? DisableLighting
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("disableLighting");
            set => SetAttributeValue("disableLighting", value);
        }

        public GrBabylonJsBooleanValue? TwoSidedLighting
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("twoSidedLighting");
            set => SetAttributeValue("twoSidedLighting", value);
        }

        public GrBabylonJsBooleanValue? LinkEmissiveWithDiffuse
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("linkEmissiveWithDiffuse");
            set => SetAttributeValue("linkEmissiveWithDiffuse", value);
        }

        public GrBabylonJsBooleanValue? InvertNormalMapX
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("invertNormalMapX");
            set => SetAttributeValue("invertNormalMapX", value);
        }

        public GrBabylonJsBooleanValue? InvertNormalMapY
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("invertNormalMapY");
            set => SetAttributeValue("invertNormalMapY", value);
        }

        public GrBabylonJsBooleanValue? InvertRefractionY
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("invertRefractionY");
            set => SetAttributeValue("invertRefractionY", value);
        }

        public GrBabylonJsBooleanValue? AmbientTextureEnabled
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("ambientTextureEnabled");
            set => SetAttributeValue("ambientTextureEnabled", value);
        }

        public GrBabylonJsBooleanValue? BumpTextureEnabled
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("bumpTextureEnabled");
            set => SetAttributeValue("bumpTextureEnabled", value);
        }

        public GrBabylonJsBooleanValue? ColorGradingTextureEnabled
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("colorGradingTextureEnabled");
            set => SetAttributeValue("colorGradingTextureEnabled", value);
        }

        public GrBabylonJsBooleanValue? DetailTextureEnabled
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("detailTextureEnabled");
            set => SetAttributeValue("detailTextureEnabled", value);
        }

        public GrBabylonJsBooleanValue? DiffuseTextureEnabled
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("diffuseTextureEnabled");
            set => SetAttributeValue("diffuseTextureEnabled", value);
        }

        public GrBabylonJsBooleanValue? EmissiveTextureEnabled
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("emissiveTextureEnabled");
            set => SetAttributeValue("emissiveTextureEnabled", value);
        }

        public GrBabylonJsBooleanValue? LightmapTextureEnabled
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("lightmapTextureEnabled");
            set => SetAttributeValue("lightmapTextureEnabled", value);
        }

        public GrBabylonJsBooleanValue? OpacityTextureEnabled
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("opacityTextureEnabled");
            set => SetAttributeValue("opacityTextureEnabled", value);
        }

        public GrBabylonJsBooleanValue? ReflectionTextureEnabled
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("reflectionTextureEnabled");
            set => SetAttributeValue("reflectionTextureEnabled", value);
        }

        public GrBabylonJsBooleanValue? RefractionTextureEnabled
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("refractionTextureEnabled");
            set => SetAttributeValue("refractionTextureEnabled", value);
        }

        public GrBabylonJsBooleanValue? SpecularTextureEnabled
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("specularTextureEnabled");
            set => SetAttributeValue("specularTextureEnabled", value);
        }

        public GrBabylonJsBooleanValue? UseLogarithmicDepth
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useLogarithmicDepth");
            set => SetAttributeValue("useLogarithmicDepth", value);
        }

        public GrBabylonJsBooleanValue? UseAlphaFromDiffuseTexture
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useAlphaFromDiffuseTexture");
            set => SetAttributeValue("useAlphaFromDiffuseTexture", value);
        }

        public GrBabylonJsBooleanValue? UseEmissiveAsIllumination
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useEmissiveAsIllumination");
            set => SetAttributeValue("useEmissiveAsIllumination", value);
        }

        public GrBabylonJsBooleanValue? UseGlossinessFromSpecularMapAlpha
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useGlossinessFromSpecularMapAlpha");
            set => SetAttributeValue("useGlossinessFromSpecularMapAlpha", value);
        }

        public GrBabylonJsBooleanValue? UseLightmapAsShadowmap
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useLightmapAsShadowmap");
            set => SetAttributeValue("useLightmapAsShadowmap", value);
        }

        public GrBabylonJsBooleanValue? UseObjectSpaceNormalMap
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useObjectSpaceNormalMap");
            set => SetAttributeValue("useObjectSpaceNormalMap", value);
        }

        public GrBabylonJsBooleanValue? UseParallax
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useParallax");
            set => SetAttributeValue("useParallax", value);
        }

        public GrBabylonJsBooleanValue? UseParallaxOcclusion
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useParallaxOcclusion");
            set => SetAttributeValue("useParallaxOcclusion", value);
        }

        public GrBabylonJsBooleanValue? UseReflectionFresnelFromSpecular
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useReflectionFresnelFromSpecular");
            set => SetAttributeValue("useReflectionFresnelFromSpecular", value);
        }

        public GrBabylonJsBooleanValue? UseReflectionOverAlpha
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useReflectionOverAlpha");
            set => SetAttributeValue("useReflectionOverAlpha", value);
        }

        public GrBabylonJsBooleanValue? UseSpecularOverAlpha
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useSpecularOverAlpha");
            set => SetAttributeValue("useSpecularOverAlpha", value);
        }

        public GrBabylonJsBooleanValue? FresnelEnabled
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("fresnelEnabled");
            set => SetAttributeValue("fresnelEnabled", value);
        }


        public StandardMaterialProperties()
        {
        }
            
        public StandardMaterialProperties(GrBabylonJsColor3Value color)
        {
            AmbientColor = color;
            DiffuseColor = color;
            SpecularColor = color;
        }

        public StandardMaterialProperties(StandardMaterialProperties properties)
        {
            SetAttributeValues(properties);
        }
    }


    protected override string ConstructorName
        => "new BABYLON.StandardMaterial";

    public StandardMaterialProperties Properties { get; private set; }
        = new StandardMaterialProperties();

    public override GrBabylonJsObjectProperties ObjectProperties
        => Properties;


    public GrBabylonJsStandardMaterial(string constName)
        : base(constName)
    {
    }

    public GrBabylonJsStandardMaterial(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
    }


    public GrBabylonJsStandardMaterial SetProperties(StandardMaterialProperties properties)
    {
        Properties = new StandardMaterialProperties(properties);

        return this;
    }
}