using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;

public sealed class GrBabylonJsBackgroundMaterialProperties :
    GrBabylonJsMaterialProperties
{
    public GrBabylonJsColor3Value? PrimaryColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("primaryColor");
        set => SetAttributeValue("primaryColor", value);
    }

    public GrBabylonJsColor3Value? PerceptualColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("perceptualColor");
        set => SetAttributeValue("perceptualColor", value);
    }

    public GrBabylonJsVector3Value? SceneCenter
    {
        get => GetAttributeValueOrNull<GrBabylonJsVector3Value>("sceneCenter");
        set => SetAttributeValue("sceneCenter", value);
    }

    public GrBabylonJsTextureValue? DiffuseTexture
    {
        get => GetAttributeValueOrNull<GrBabylonJsTextureValue>("diffuseTexture");
        set => SetAttributeValue("diffuseTexture", value);
    }

    public GrBabylonJsTextureValue? ReflectionTexture
    {
        get => GetAttributeValueOrNull<GrBabylonJsTextureValue>("reflectionTexture");
        set => SetAttributeValue("reflectionTexture", value);
    }

    public GrBabylonJsFloat32Value? ReflectionAmount
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("reflectionAmount");
        set => SetAttributeValue("reflectionAmount", value);
    }

    public GrBabylonJsFloat32Value? ReflectionBlur
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("reflectionBlur");
        set => SetAttributeValue("reflectionBlur", value);
    }

    public GrBabylonJsFloat32Value? ReflectionFalloffDistance
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("reflectionFalloffDistance");
        set => SetAttributeValue("reflectionFalloffDistance", value);
    }

    public GrBabylonJsFloat32Value? ReflectionReflectance0
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("reflectionReflectance0");
        set => SetAttributeValue("reflectionReflectance0", value);
    }

    public GrBabylonJsFloat32Value? ReflectionReflectance90
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("reflectionReflectance90");
        set => SetAttributeValue("reflectionReflectance90", value);
    }

    public GrBabylonJsFloat32Value? StandardReflectance0
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("standardReflectance0");
        set => SetAttributeValue("standardReflectance0", value);
    }

    public GrBabylonJsFloat32Value? StandardReflectance90
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("standardReflectance90");
        set => SetAttributeValue("standardReflectance90", value);
    }

    public GrBabylonJsFloat32Value? ShadowLevel
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("shadowLevel");
        set => SetAttributeValue("shadowLevel", value);
    }

    public GrBabylonJsFloat32Value? FovMultiplier
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("fovMultiplier");
        set => SetAttributeValue("fovMultiplier", value);
    }

    public GrBabylonJsFloat32Value? PrimaryColorHighlightLevel
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("primaryColorHighlightLevel");
        set => SetAttributeValue("primaryColorHighlightLevel", value);
    }

    public GrBabylonJsFloat32Value? PrimaryColorShadowLevel
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("primaryColorShadowLevel");
        set => SetAttributeValue("primaryColorShadowLevel", value);
    }

    public GrBabylonJsFloat32Value? ReflectionStandardFresnelWeight
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("reflectionStandardFresnelWeight");
        set => SetAttributeValue("reflectionStandardFresnelWeight", value);
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

    public GrBabylonJsBooleanValue? EnableNoise
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("enableNoise");
        set => SetAttributeValue("enableNoise", value);
    }

    public GrBabylonJsBooleanValue? ReflectionFresnel
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("reflectionFresnel");
        set => SetAttributeValue("reflectionFresnel", value);
    }

    public GrBabylonJsBooleanValue? OpacityFresnel
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("opacityFresnel");
        set => SetAttributeValue("opacityFresnel", value);
    }

    public GrBabylonJsBooleanValue? ShadowOnly
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("shadowOnly");
        set => SetAttributeValue("shadowOnly", value);
    }

    public GrBabylonJsBooleanValue? SwitchToBgr
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("switchToBgr");
        set => SetAttributeValue("switchToBgr", value);
    }

    public GrBabylonJsBooleanValue? UseEquirectangularFov
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useEquirectangularFov");
        set => SetAttributeValue("useEquirectangularFov", value);
    }

    public GrBabylonJsBooleanValue? UseRgbColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useRgbColor");
        set => SetAttributeValue("useRgbColor", value);
    }


    public GrBabylonJsBackgroundMaterialProperties()
    {
    }

    public GrBabylonJsBackgroundMaterialProperties(GrBabylonJsBackgroundMaterialProperties properties)
    {
        SetAttributeValues(properties);
    }
            
}