using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs;

public sealed class GrBabylonJsEnvironmentHelperProperties :
    GrBabylonJsObjectProperties
{
    // groundMirrorTextureType

    public GrBabylonJsFloat32Value? BackgroundYRotation
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("backgroundYRotation");
        set => SetAttributeValue("backgroundYRotation", value);
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

    public GrBabylonJsBooleanValue? CreateGround
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("createGround");
        set => SetAttributeValue("createGround", value);
    }

    public GrBabylonJsBooleanValue? CreateSkybox
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("createSkybox");
        set => SetAttributeValue("createSkybox", value);
    }

    public GrBabylonJsBooleanValue? EnableGroundMirror
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("enableGroundMirror");
        set => SetAttributeValue("enableGroundMirror", value);
    }

    public GrBabylonJsBooleanValue? EnableGroundShadow
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("enableGroundShadow");
        set => SetAttributeValue("enableGroundShadow", value);
    }

    public GrBabylonJsBooleanValue? ToneMappingEnabled
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("toneMappingEnabled");
        set => SetAttributeValue("toneMappingEnabled", value);
    }

    public GrBabylonJsTextureValue? EnvironmentTexture
    {
        get => GetAttributeValueOrNull<GrBabylonJsTextureValue>("environmentTexture");
        set => SetAttributeValue("environmentTexture", value);
    }

    public GrBabylonJsColor3Value? GroundColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("groundColor");
        set => SetAttributeValue("groundColor", value);
    }

    public GrBabylonJsFloat32Value? GroundMirrorAmount
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("groundMirrorAmount");
        set => SetAttributeValue("groundMirrorAmount", value);
    }

    public GrBabylonJsFloat32Value? GroundMirrorBlurKernel
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("groundMirrorBlurKernel");
        set => SetAttributeValue("groundMirrorBlurKernel", value);
    }

    public GrBabylonJsFloat32Value? GroundMirrorFallOffDistance
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("groundMirrorFallOffDistance");
        set => SetAttributeValue("groundMirrorFallOffDistance", value);
    }

    public GrBabylonJsFloat32Value? GroundMirrorFresnelWeight
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("groundMirrorFresnelWeight");
        set => SetAttributeValue("groundMirrorFresnelWeight", value);
    }

    public GrBabylonJsFloat32Value? GroundMirrorSizeRatio
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("groundMirrorSizeRatio");
        set => SetAttributeValue("groundMirrorSizeRatio", value);
    }

    public GrBabylonJsFloat32Value? GroundOpacity
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("groundOpacity");
        set => SetAttributeValue("groundOpacity", value);
    }

    public GrBabylonJsFloat32Value? GroundShadowLevel
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("groundShadowLevel");
        set => SetAttributeValue("groundShadowLevel", value);
    }

    public GrBabylonJsFloat32Value? GroundSize
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("groundSize");
        set => SetAttributeValue("groundSize", value);
    }

    public GrBabylonJsTextureValue? GroundTexture
    {
        get => GetAttributeValueOrNull<GrBabylonJsTextureValue>("groundTexture");
        set => SetAttributeValue("groundTexture", value);
    }

    public GrBabylonJsFloat32Value? GroundYBias
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("groundYBias");
        set => SetAttributeValue("groundYBias", value);
    }

    public GrBabylonJsVector3Value? RootPosition
    {
        get => GetAttributeValueOrNull<GrBabylonJsVector3Value>("rootPosition");
        set => SetAttributeValue("rootPosition", value);
    }

    public GrBabylonJsBooleanValue? SizeAuto
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("sizeAuto");
        set => SetAttributeValue("sizeAuto", value);
    }

    public GrBabylonJsColor3Value? SkyboxColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("skyboxColor");
        set => SetAttributeValue("skyboxColor", value);
    }

    public GrBabylonJsBooleanValue? SetupImageProcessing
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("setupImageProcessing");
        set => SetAttributeValue("setupImageProcessing", value);
    }

    public GrBabylonJsFloat32Value? SkyboxSize
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("skyboxSize");
        set => SetAttributeValue("skyboxSize", value);
    }

    public GrBabylonJsTextureValue? SkyboxTexture
    {
        get => GetAttributeValueOrNull<GrBabylonJsTextureValue>("skyBoxTexture");
        set => SetAttributeValue("skyBoxTexture", value);
    }


    public GrBabylonJsEnvironmentHelperProperties()
    {
    }

    public GrBabylonJsEnvironmentHelperProperties(GrBabylonJsEnvironmentHelperProperties properties)
    {
        SetAttributeValues(properties);
    }
            
}